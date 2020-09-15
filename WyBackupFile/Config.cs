using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WyBackupFile
{
	public class Config
	{
		public Config() { }

		private string sourceFilePath;
		private string targetFilePath;
		private int duration;
		private int maxBackupCount;
		private string processPath;
		public string TargetFilePath { get => targetFilePath; set => targetFilePath = value; }
		public string SourceFilePath { get => sourceFilePath; set => sourceFilePath = value; }
		public int Duration { get => duration; set => duration = value; }
		public int MaxBackupCount { get => maxBackupCount; set => maxBackupCount = value; }
        public string ProcessPath { get => processPath; set => processPath = value; }
    }
}
