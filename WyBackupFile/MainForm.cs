using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace WyBackupFile
{

    public partial class MainForm : Form
    {

        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")] //导入API函数
        extern static System.IntPtr GetSystemMenu(System.IntPtr hWnd, System.IntPtr bRevert);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);

        static int MF_BYPOSITION = 0x400;
        static int MF_REMOVE = 0x1000;

        private Config config = new Config();
        private string xmlFile = Directory.GetCurrentDirectory() + "\\Config.xml";
        private bool isRunning = false;
        private int minDuration = 5;
        private int maxDuration = 86400;
        private int defaultDuration = 120;
        private int minBackupCount = 1;
        private int maxBackupCount = 500;
        private int defaultBackupCount = 50;

        public MainForm()
        {
            InitializeComponent();
            RemoveMenu(GetSystemMenu(Handle, IntPtr.Zero), 0, MF_BYPOSITION | MF_REMOVE);
            //改变窗体风格，使之不能用鼠标拖拽改变大小
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //禁止使用最大化按钮
            this.MaximizeBox = false;
            this.nUDDuration.Maximum = maxDuration;
            this.nUDDuration.Minimum = minDuration;
            this.nUDDuration.Value = defaultDuration;
            this.nUDMaxBackupCount.Maximum = maxBackupCount;
            this.nUDMaxBackupCount.Minimum = minBackupCount;
            this.nUDMaxBackupCount.Value = defaultBackupCount;
            readConfigXml();
            initToolTip();
        }

        private void hideCursor(object sender) 
        {
            try
            {
                HideCaret(((TextBox)sender).Handle);
            }
            catch (Exception e) { }
        }

        private void chooseSourceFilePath(object sender, EventArgs e)
        {
            hideCursor(sender);
            if (tbSourceFilePath.Text != null && tbSourceFilePath.Text != "")
            {
                DirectoryInfo sourceFile = new DirectoryInfo(tbSourceFilePath.Text);
                if (sourceFile.Exists)
                {
                    fBDSourcePath.SelectedPath = sourceFile.FullName;
                }
            }
            else
            {
                fBDSourcePath.RootFolder = Environment.SpecialFolder.Desktop;
            }

            if (fBDSourcePath.ShowDialog() == DialogResult.OK)
            {
                tbSourceFilePath.Text = fBDSourcePath.SelectedPath;
            }
        }

        private void textBoxGotFocus(object sender, EventArgs e)
        {
            hideCursor(sender);
        }

        private void chooseTargetFilePath(object sender, EventArgs e)
        {
            hideCursor(sender);
            if (tbTargetFilePath.Text != null && tbTargetFilePath.Text != "")
            {
                DirectoryInfo targetFile = new DirectoryInfo(tbTargetFilePath.Text);
                if (targetFile.Exists)
                {
                    fBDSourcePath.SelectedPath = targetFile.FullName;
                }
            }
            else
            {
                fBDSourcePath.RootFolder = Environment.SpecialFolder.Desktop;
            }

            if (fBDSourcePath.ShowDialog() == DialogResult.OK)
            {
                tbTargetFilePath.Text = fBDSourcePath.SelectedPath;
                loadBackupFileToList(tbTargetFilePath.Text, true);
            }
        }

        private void showInExplorer(object sender, EventArgs e)
        {
            string filePath = contextMenuStripList.SourceControl.Text;
            if (filePath != null || filePath != "")
            {
                DirectoryInfo targetFile = new DirectoryInfo(tbTargetFilePath.Text);
                if (!targetFile.Exists)
                {
                    MessageBox.Show("路径不存在");
                    return;
                }
                else
                {
                    System.Diagnostics.Process.Start(targetFile.FullName);
                }
            }
            else
            {
                MessageBox.Show("路径不存在");
            }
        }

        private void deleteFile(object sender, EventArgs e)
        {
            string filePath = contextMenuStripList.SourceControl.Text;
            if (filePath != null || filePath != "")
            {
                if (MessageBox.Show("确定删除" + filePath + "?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) 
                {
               
                    FileInfo file = new FileInfo(tbTargetFilePath.Text + "\\" + filePath);
                    if (file.Exists)
                    {
                        file.Delete();
                        refreshList(tbTargetFilePath.Text);
                        //loadBackupFileToList(tbTargetFilePath.Text, false);
                    }
                    else
                    {
                        MessageBox.Show("找不到文件");
                    }
                }
            }
            else
            {
                MessageBox.Show("路径不存在");
            }
        }


        //添加右键菜单到备份列表
        private void attachMenuToList()
        {
            if (this.listBoxFileList.Items.Count > 0)
            {
                this.listBoxFileList.ContextMenuStrip = this.contextMenuStripList;
                this.listBoxFileList.SetSelected(0,true);
            }
        }

        private void loadBackupFileToList(string filePath, bool showMsg)
        {
            DirectoryInfo root = new DirectoryInfo(filePath);
            if (root.Exists)
            {
                try
                {
                    FileInfo[] fileInfos = root.GetFiles("*.zip");
                    string[] filesName = new string[fileInfos.Length];
                    Console.WriteLine("fileInfos.Length;"+ fileInfos.Length);
                    for (int i = 0; i < fileInfos.Length; i++)
                    {
                        Console.WriteLine("fileInfos ;"+ i + ":"+ fileInfos[i].Name);
                        filesName[i] = fileInfos[i].Name;
                    }
                    this.listBoxFileList.BeginUpdate();
                    this.listBoxFileList.Items.Clear();
                    if (filesName.Length > 0)
                    {
                        Array.Reverse(filesName);
                        this.listBoxFileList.Items.AddRange(filesName);
                        attachMenuToList();
                    }
                    this.listBoxFileList.EndUpdate();
                }
                catch (Exception e)
                {
                    backupResultLabel.Text = "加载列表失败:"+e.Message;
                    backupResultLabel.ForeColor = Color.Red;
                }
            }
            else if (showMsg)
            {
                MessageBox.Show("文件路径不存在!");
            }
        }

        private void refreshList(string filePath)
        {
            DirectoryInfo root = new DirectoryInfo(filePath);
            if (root.Exists)
            {
                try
                {
                    FileInfo[] fileInfos = root.GetFiles("*.zip");
                    string[] filesName = new string[fileInfos.Length];
                    Console.WriteLine("fileInfos.Length;" + fileInfos.Length);
                    for (int i = 0; i < fileInfos.Length; i++)
                    {
                        Console.WriteLine("fileInfos ;" + i + ":" + fileInfos[i].Name);
                        filesName[i] = fileInfos[i].Name;
                    }
                    this.listBoxFileList.BeginUpdate();
                    this.listBoxFileList.Items.Clear();
                    if (filesName.Length > 0)
                    {
                        Array.Reverse(filesName);
                        this.listBoxFileList.Items.AddRange(filesName);
                        attachMenuToList();
                    }
                    this.listBoxFileList.EndUpdate();

                }
                catch (Exception e)
                {
                    backupResultLabel.Text = "加载列表失败:" + e.Message;
                    backupResultLabel.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("文件路径不存在!");
            }
        }

        //重写listBox的_DrawItem
        private void listBoxFileList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (listBoxFileList.Items.Count < 1)
            {
                return;
            }
            e.DrawBackground();
            e.DrawFocusRectangle();
            StringFormat strFmt = new System.Drawing.StringFormat();
            //strFmt.Alignment = StringAlignment.Center; //文本垂直居中
            //strFmt.LineAlignment = StringAlignment.Center; //文本水平居中
            e.Graphics.DrawString(listBoxFileList.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, strFmt);
        }

        private void readConfigXml()
        {

            FileInfo fileInfo = new FileInfo(xmlFile);
            if (!fileInfo.Exists)
            {
                return;
            }

            XmlTextReader reader = new XmlTextReader(@xmlFile);

            try
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "SourcePath")
                        {
                            config.SourceFilePath = reader.ReadElementString().Trim();
                        } 
                        else if (reader.Name == "TargetPath")
                        {
                            config.TargetFilePath = reader.ReadElementString().Trim();
                        }
                        else if (reader.Name == "Duration")
                        {
                            config.Duration = reader.ReadElementContentAsInt();

                        }
                        else if (reader.Name == "MaxBackupCount")
                        {
                            config.MaxBackupCount = reader.ReadElementContentAsInt();
                        }
                        else if(reader.Name == "ProcessPath")
                        { 
                            config.ProcessPath = reader.ReadElementString().Trim();
                        }
                        else if(reader.Name == "IsAutoBackup")
                        {
                            config.IsAutoBackup = Boolean.Parse(reader.ReadElementString());
                        }
                    }
                }
            }
            catch (Exception exception) { }
            finally
            {
                reader.Close();
            }


            if (config.SourceFilePath != null && config.SourceFilePath != "")
            {
                tbSourceFilePath.Text = config.SourceFilePath;
            }
            if (config.TargetFilePath != null && config.TargetFilePath != "")
            {
                tbTargetFilePath.Text = config.TargetFilePath;
                loadBackupFileToList(tbTargetFilePath.Text, false);
            }
            if (config.ProcessPath != null && config.ProcessPath != "")
            {
                textBoxProcessPath.Text = config.ProcessPath;
            }
            if (config.Duration >= minDuration && config.Duration <= maxDuration)
            {
                nUDDuration.Value = config.Duration;
            }
            else
            {
                nUDDuration.Value = defaultDuration;
            }
            if (config.MaxBackupCount >= minBackupCount && config.MaxBackupCount <= maxBackupCount)
            {
                nUDMaxBackupCount.Value = config.MaxBackupCount;
            }
            else
            {
                nUDMaxBackupCount.Value = defaultBackupCount;
            }
            if (config.IsAutoBackup == true)
            {
                autoRunCB.Checked = true;
            }
            else 
            {
                autoRunCB.Checked = false;
            }
        }

        private void saveConfigXml()
        {
            XmlTextWriter writer = new XmlTextWriter(@xmlFile, Encoding.UTF8);
            try
            {
                //使用 Formatting 属性指定希望将 XML 设定为何种格式。 这样，子元素就可以通过使用 Indentation 和 IndentChar 属性来缩进。
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Setting");
                writer.WriteElementString("SourcePath", tbSourceFilePath.Text);
                writer.WriteElementString("TargetPath", tbTargetFilePath.Text);
                if(textBoxProcessPath.Text != null && textBoxProcessPath.Text.Trim() != "")
                {
                    writer.WriteElementString("ProcessPath", textBoxProcessPath.Text);
                }
                writer.WriteElementString("Duration", nUDDuration.Value + "");
                writer.WriteElementString("MaxBackupCount", nUDMaxBackupCount.Value + "");
                writer.WriteElementString("IsAutoBackup", autoRunCB.Checked.ToString());
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            catch (Exception e) { }
            finally
            {
                //将XML写入文件并且关闭XmlTextWriter
                writer.Close();
            }
        }

        private void btnBackupToggleClick(object sender, EventArgs e)
        {
            realAtuoBackup();
        }

        private Boolean checkPath()
        {
            try
            {
                DirectoryInfo sourceFile = new DirectoryInfo(tbSourceFilePath.Text);
                DirectoryInfo targetFile = new DirectoryInfo(tbTargetFilePath.Text);
                if (!sourceFile.Exists || !targetFile.Exists)
                {
                    MessageBox.Show("路径不存在,请检查两个路径是否存在");
                    return false;
                }
                return true;
            }
            catch (Exception e) 
            {
                MessageBox.Show(e.Message);
                return false;
            }
            
        }

        private void realAtuoBackup() 
        {
            if (!checkPath())
            {
                return;
            }
            isRunning = !isRunning;
            initTimer();
            if (isRunning)
            {
                btnBackupToggle.Text = "停止";
                tbSourceFilePath.Enabled = false;
                tbTargetFilePath.Enabled = false;
                nUDDuration.Enabled = false;
                nUDMaxBackupCount.Enabled = false;
                btnBackupNow.Enabled = false;
                timer1.Start();
            }
            else
            {
                btnBackupToggle.Text = "启动";
                tbSourceFilePath.Enabled = true;
                tbTargetFilePath.Enabled = true;
                nUDDuration.Enabled = true;
                nUDMaxBackupCount.Enabled = true;
                btnBackupNow.Enabled = true;
                timer1.Stop();
            }
            saveConfigXml();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            backupFile();
            loadBackupFileToList(tbTargetFilePath.Text, false);
        }

        private void initTimer()
        {
            this.timer1.Interval = (int)this.nUDDuration.Value * 1000;
        }

        private void initToolTip()
        {
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            toolTip2.AutoPopDelay = 5000;
            toolTip2.InitialDelay = 1000;
            toolTip2.ReshowDelay = 500;
            toolTip2.ShowAlways = true;

        }

        private void backupFile()
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(tbSourceFilePath.Text);
            DirectoryInfo targetDirectory = new DirectoryInfo(tbTargetFilePath.Text);
            string sourceFilePath = sourceDirectory.FullName;
            string sourceFileName = sourceDirectory.Name;
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            string targetFileName = "[" + nowTime + "]" + sourceFileName + ".zip";
            string targetFileFullName = targetDirectory + "\\" + targetFileName;
            if (sourceDirectory.Exists && targetDirectory.Exists)
            {
                try
                {
                    DirectoryInfo tempeDirectory = copySourceToTemp();
                    if (tempeDirectory != null)
                    {
                        sourceFilePath = tempeDirectory.FullName + "\\" + sourceFileName + "\\";
                        ZipFile.CreateFromDirectory(@sourceFilePath, @targetFileFullName); //压缩
                        backupResultLabel.Text = targetFileName;
                        backupResultLabel.ForeColor = Color.Green;
                        deleteOldFile(targetDirectory);
                        deleteDirChildFile(tempeDirectory.FullName);
                    }
                    else
                    {
                        backupResultLabel.Text = "备份失败:临时文件处理失败";
                        backupResultLabel.ForeColor = Color.Red;
                    }
                }
                catch (Exception e)
                {
                    backupResultLabel.Text = "失败:" + e.Message;
                    backupResultLabel.ForeColor = Color.Red;
                }
            }
            else
            {
                backupResultLabel.Text = "路径错误备份失败";
                backupResultLabel.ForeColor = Color.Red;
            }
        }

        private void deleteOldFile(DirectoryInfo targetDirectory)
        {
            if (targetDirectory != null)
            {
                FileInfo[] fileInfos = targetDirectory.GetFiles("*.zip");
                if (fileInfos.Length > (int)nUDMaxBackupCount.Value)
                {
                    int over = fileInfos.Length - (int)nUDMaxBackupCount.Value;
                    for (int i = 0; over > 0; i++, over--)
                    {
                        try
                        {
                            fileInfos[i].Delete();
                        }
                        catch (Exception e)
                        {
                            backupResultLabel.Text = "删除超过的文件失败:" + e.Message;
                            backupResultLabel.ForeColor = Color.Red;
                        }
                    }
                }
            }
        }

        private void backupNow(object sender, EventArgs e)
        {
            backupFile();
            loadBackupFileToList(tbTargetFilePath.Text, false);
            saveConfigXml();
        }

        private DirectoryInfo copySourceToTemp()
        {
            try
            {
                DirectoryInfo tempDirectory = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\temp\\");
                if (!tempDirectory.Exists)
                {
                    tempDirectory.Create();
                }
                deleteDirChildFile(tempDirectory.FullName);
                DirectoryInfo sourceDirectory = new DirectoryInfo(tbSourceFilePath.Text);
                if (!sourceDirectory.Exists)
                {
                    MessageBox.Show("源文件不存在");
                }
                else
                {
                    copyFolder(sourceDirectory.FullName, tempDirectory.FullName);
                }
                return tempDirectory;
            }
            catch (Exception exce)
            {
                backupResultLabel.Text = "备份文件失败:" + exce.Message;
                backupResultLabel.ForeColor = Color.Red;
                return null;
            }
        }

        private void recoverBackupFile(object sender, EventArgs e)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(tbSourceFilePath.Text);
            DirectoryInfo targetDirectory = new DirectoryInfo(tbTargetFilePath.Text);
            string sourceFilePath = sourceDirectory.FullName;
            string filePath = contextMenuStripList.SourceControl.Text;
            string targetFileFullName = targetDirectory + "\\" + filePath;
            if (sourceDirectory.Exists && targetDirectory.Exists)
            {
                try
                {
                    FileInfo zipFile = new FileInfo(targetFileFullName);
                    if (!zipFile.Exists)
                    {
                        MessageBox.Show("备份文件没找到");
                        return;
                    }
                    else
                    {
                        deleteDirChildFile(sourceFilePath);
                        String zipFileName = zipFile.FullName;
                        ZipFile.ExtractToDirectory(@zipFileName, @sourceFilePath); //解压
                        MessageBox.Show("还原成功!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("还原失败:" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("路径不存在");
            }
        }

        public static void deleteDirChildFile(string srcPath)
        {
            try
            {
                if (Directory.Exists(srcPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(srcPath);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                        }
                        else
                        {
                            File.Delete(i.FullName);      //删除指定文件
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void openSorceFloder(object sender, EventArgs e)
        {
            if (Directory.Exists(tbSourceFilePath.Text))
            {
                DirectoryInfo sourceDirectory = new DirectoryInfo(tbSourceFilePath.Text);
                System.Diagnostics.Process.Start(sourceDirectory.FullName);
            }
            else
            {
                MessageBox.Show("源文件没找到");
            }
        }

        private void backupResultLabel_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.backupResultLabel, this.backupResultLabel.Text == null ? "无" : this.backupResultLabel.Text);
        }

        public void copyFolder(string sourceFolder, string destFolder)
        {
            try
            {
                string folderName = System.IO.Path.GetFileName(sourceFolder);
                string destfolderdir = System.IO.Path.Combine(destFolder, folderName);
                string[] filenames = System.IO.Directory.GetFileSystemEntries(sourceFolder);
                foreach (string file in filenames)// 遍历所有的文件和目录
                {
                    if (System.IO.Directory.Exists(file))
                    {
                        string currentdir = System.IO.Path.Combine(destfolderdir, System.IO.Path.GetFileName(file));
                        if (!System.IO.Directory.Exists(currentdir))
                        {
                            System.IO.Directory.CreateDirectory(currentdir);
                        }
                        copyFolder(file, destfolderdir);
                    }
                    else
                    {
                        string srcfileName = System.IO.Path.Combine(destfolderdir, System.IO.Path.GetFileName(file));
                        if (!System.IO.Directory.Exists(destfolderdir))
                        {
                            System.IO.Directory.CreateDirectory(destfolderdir);
                        }
                        System.IO.File.Copy(file, srcfileName, true);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void RecoverAndRunGame(object sender, EventArgs e)
        {
            realRecoverAndRunGame();
        }

        private void realRecoverAndRunGame() 
        {
            if (textBoxProcessPath.Text == null || textBoxProcessPath.Text.Trim() == "")
            {
                MessageBox.Show("请先输入程序路径!");
                return;
            }
            DirectoryInfo sourceDirectory = new DirectoryInfo(tbSourceFilePath.Text);
            DirectoryInfo targetDirectory = new DirectoryInfo(tbTargetFilePath.Text);
            string sourceFilePath = sourceDirectory.FullName;
            string filePath = listBoxFileList.SelectedItem.ToString();
            string targetFileFullName = targetDirectory + "\\" + filePath;
            if (sourceDirectory.Exists && targetDirectory.Exists)
            {
                try
                {
                    FileInfo zipFile = new FileInfo(targetFileFullName);
                    if (!zipFile.Exists)
                    {
                        MessageBox.Show("备份文件没找到");
                        return;
                    }
                    else
                    {
                        deleteDirChildFile(sourceFilePath);
                        String zipFileName = zipFile.FullName;
                        ZipFile.ExtractToDirectory(@zipFileName, @sourceFilePath); //解压
                        if (textBoxProcessPath.Text != null && textBoxProcessPath.Text.Trim() != "")
                        {
                            RunProcess(textBoxProcessPath.Text);
                        }
                        backupResultLabel.Text = "还原成功!程序启动中..";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("还原运行程序失败:" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("路径不存在");
            }
        }

        private void RunProcess(String processPath)
        {
            System.Diagnostics.Process startProc = new System.Diagnostics.Process();
            startProc.StartInfo.FileName = processPath;
            startProc.Start();
        }

        //快捷键
        private void fromKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        realRecoverAndRunGame();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮 
            if (WindowState == FormWindowState.Minimized)
            {
                //图标显示在托盘区 
                notifyIcon1.Visible = true;
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            //还原窗体显示 
            WindowState = FormWindowState.Normal;
            //激活窗体并给予它焦点 
            this.Activate();
            //任务栏区显示图标 
            this.ShowInTaskbar = true;
            //托盘区图标隐藏 
            notifyIcon1.Visible = false;
        }

        private void autoRunCB_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.autoRunCB, "软件运行则自动启动备份,前提需要先设置好上面的参数");
        }

        private void autoRunCB_Click(object sender, EventArgs e)
        {
            if (autoRunCB.Checked == true)
            {
                if (checkPath()) 
                {
                    autoRunCB.Checked = true;
                    config.IsAutoBackup = true;
                }
            }
            else 
            {
                autoRunCB.Checked = false;
                config.IsAutoBackup = false;
            }
            saveConfigXml();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (config.IsAutoBackup)
            {
                realAtuoBackup();
            }
        }
    }
}
