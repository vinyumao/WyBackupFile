namespace WyBackupFile
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tbSourceFilePath = new System.Windows.Forms.TextBox();
            this.fBDSourcePath = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTargetFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nUDDuration = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nUDMaxBackupCount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxFileList = new System.Windows.Forms.ListBox();
            this.contextMenuStripList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSorceFloderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recoverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnBackupToggle = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.backupResultLabel = new System.Windows.Forms.Label();
            this.btnBackupNow = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nUDDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDMaxBackupCount)).BeginInit();
            this.contextMenuStripList.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "  源文件路径:";
            // 
            // tbSourceFilePath
            // 
            this.tbSourceFilePath.CausesValidation = false;
            this.tbSourceFilePath.Location = new System.Drawing.Point(109, 26);
            this.tbSourceFilePath.Name = "tbSourceFilePath";
            this.tbSourceFilePath.ReadOnly = true;
            this.tbSourceFilePath.Size = new System.Drawing.Size(332, 21);
            this.tbSourceFilePath.TabIndex = 1;
            this.tbSourceFilePath.TabStop = false;
            this.tbSourceFilePath.WordWrap = false;
            this.tbSourceFilePath.Click += new System.EventHandler(this.chooseSourceFilePath);
            this.tbSourceFilePath.GotFocus += new System.EventHandler(this.textBoxGotFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "备份文件路径:";
            // 
            // tbTargetFilePath
            // 
            this.tbTargetFilePath.Location = new System.Drawing.Point(109, 55);
            this.tbTargetFilePath.Name = "tbTargetFilePath";
            this.tbTargetFilePath.ReadOnly = true;
            this.tbTargetFilePath.Size = new System.Drawing.Size(332, 21);
            this.tbTargetFilePath.TabIndex = 3;
            this.tbTargetFilePath.TabStop = false;
            this.tbTargetFilePath.WordWrap = false;
            this.tbTargetFilePath.Click += new System.EventHandler(this.chooseTargetFilePath);
            this.tbTargetFilePath.GotFocus += new System.EventHandler(this.textBoxGotFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "时间间隔(秒):";
            // 
            // nUDDuration
            // 
            this.nUDDuration.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nUDDuration.Location = new System.Drawing.Point(111, 109);
            this.nUDDuration.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nUDDuration.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nUDDuration.Name = "nUDDuration";
            this.nUDDuration.Size = new System.Drawing.Size(62, 21);
            this.nUDDuration.TabIndex = 5;
            this.nUDDuration.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "最大备份数量:";
            // 
            // nUDMaxBackupCount
            // 
            this.nUDMaxBackupCount.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nUDMaxBackupCount.Location = new System.Drawing.Point(323, 109);
            this.nUDMaxBackupCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDMaxBackupCount.Name = "nUDMaxBackupCount";
            this.nUDMaxBackupCount.Size = new System.Drawing.Size(63, 21);
            this.nUDMaxBackupCount.TabIndex = 7;
            this.nUDMaxBackupCount.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "备份列表";
            // 
            // listBoxFileList
            // 
            this.listBoxFileList.BackColor = System.Drawing.SystemColors.Window;
            this.listBoxFileList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxFileList.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxFileList.FormattingEnabled = true;
            this.listBoxFileList.ItemHeight = 25;
            this.listBoxFileList.Location = new System.Drawing.Point(26, 168);
            this.listBoxFileList.Name = "listBoxFileList";
            this.listBoxFileList.Size = new System.Drawing.Size(415, 196);
            this.listBoxFileList.TabIndex = 10;
            this.listBoxFileList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxFileList_DrawItem);
            // 
            // contextMenuStripList
            // 
            this.contextMenuStripList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInExplorerMenuItem,
            this.openSorceFloderToolStripMenuItem,
            this.recoverToolStripMenuItem,
            this.deletFileToolStripMenuItem});
            this.contextMenuStripList.Name = "contextMenuStripList";
            this.contextMenuStripList.Size = new System.Drawing.Size(149, 92);
            // 
            // showInExplorerMenuItem
            // 
            this.showInExplorerMenuItem.Name = "showInExplorerMenuItem";
            this.showInExplorerMenuItem.Size = new System.Drawing.Size(148, 22);
            this.showInExplorerMenuItem.Text = "打开文件夹";
            this.showInExplorerMenuItem.Click += new System.EventHandler(this.showInExplorer);
            // 
            // openSorceFloderToolStripMenuItem
            // 
            this.openSorceFloderToolStripMenuItem.Name = "openSorceFloderToolStripMenuItem";
            this.openSorceFloderToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openSorceFloderToolStripMenuItem.Text = "打开源文件夹";
            this.openSorceFloderToolStripMenuItem.Click += new System.EventHandler(this.openSorceFloder);
            // 
            // recoverToolStripMenuItem
            // 
            this.recoverToolStripMenuItem.Name = "recoverToolStripMenuItem";
            this.recoverToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.recoverToolStripMenuItem.Text = "还原";
            this.recoverToolStripMenuItem.Click += new System.EventHandler(this.recoverBackupFile);
            // 
            // deletFileToolStripMenuItem
            // 
            this.deletFileToolStripMenuItem.Name = "deletFileToolStripMenuItem";
            this.deletFileToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.deletFileToolStripMenuItem.Text = "删除";
            this.deletFileToolStripMenuItem.Click += new System.EventHandler(this.deleteFile);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnBackupToggle
            // 
            this.btnBackupToggle.Location = new System.Drawing.Point(129, 370);
            this.btnBackupToggle.Name = "btnBackupToggle";
            this.btnBackupToggle.Size = new System.Drawing.Size(188, 38);
            this.btnBackupToggle.TabIndex = 11;
            this.btnBackupToggle.Text = "启动";
            this.btnBackupToggle.UseVisualStyleBackColor = true;
            this.btnBackupToggle.Click += new System.EventHandler(this.btnBackupToggleClick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 426);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "上一次备份结果:";
            // 
            // backupResultLabel
            // 
            this.backupResultLabel.AutoSize = true;
            this.backupResultLabel.Location = new System.Drawing.Point(104, 426);
            this.backupResultLabel.Name = "backupResultLabel";
            this.backupResultLabel.Size = new System.Drawing.Size(17, 12);
            this.backupResultLabel.TabIndex = 13;
            this.backupResultLabel.Text = "无";
            this.backupResultLabel.MouseEnter += new System.EventHandler(this.backupResultLabel_MouseEnter);
            // 
            // btnBackupNow
            // 
            this.btnBackupNow.Location = new System.Drawing.Point(345, 385);
            this.btnBackupNow.Name = "btnBackupNow";
            this.btnBackupNow.Size = new System.Drawing.Size(96, 23);
            this.btnBackupNow.TabIndex = 14;
            this.btnBackupNow.Text = "立即备份一次";
            this.btnBackupNow.UseVisualStyleBackColor = true;
            this.btnBackupNow.Click += new System.EventHandler(this.backupNow);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 445);
            this.Controls.Add(this.btnBackupNow);
            this.Controls.Add(this.backupResultLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnBackupToggle);
            this.Controls.Add(this.listBoxFileList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nUDMaxBackupCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nUDDuration);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTargetFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSourceFilePath);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Wy定时备份工具";
            ((System.ComponentModel.ISupportInitialize)(this.nUDDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDMaxBackupCount)).EndInit();
            this.contextMenuStripList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbSourceFilePath;
		private System.Windows.Forms.FolderBrowserDialog fBDSourcePath;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbTargetFilePath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nUDDuration;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown nUDMaxBackupCount;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox listBoxFileList;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripList;
		private System.Windows.Forms.ToolStripMenuItem showInExplorerMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deletFileToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.Button btnBackupToggle;
		private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label backupResultLabel;
        private System.Windows.Forms.Button btnBackupNow;
        private System.Windows.Forms.ToolStripMenuItem recoverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSorceFloderToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

