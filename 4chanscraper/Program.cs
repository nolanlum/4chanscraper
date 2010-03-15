using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Scraper
{
	static class Program
	{
		public static frmMain mainForm;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			DebugConsole.ShowConsole();
			Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

			DebugConsole.ShowInfo("4Chanscraper -- by Yuki`N (nol888@gmail.com)");
#if DEBUG
			DebugConsole.ShowInfo("             -- Version " + v + "-DEBUG");
#else
			DebugConsole.ShowInfo("             -- Version " + v + "-RELEASE");
#endif	
			DebugConsole.ShowInfo("             -- Build Date " + new DateTime(v.Build * TimeSpan.TicksPerDay + v.Revision * TimeSpan.TicksPerSecond * 2).AddYears(1999).AddDays(-1).AddHours(DateTime.Now.IsDaylightSavingTime() ? 1 : 0));

			DebugConsole.ShowStatus("Program startup...showing main form.");
#if !DEBUG
			System.Threading.Thread.Sleep(500);
			DebugConsole.HideConsole();
#endif

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				Application.Run(mainForm = new frmMain());
			}
			catch (ObjectDisposedException) { }
		}

		public static string _humanReadableFileSize(long size)
		{
			if (size > 1048576L)
				return Math.Round(size / 1048576.0, 2) + " MB";
			else if (size > 1024L)
				return Math.Round(size / 1024.0, 2) + " KB";
			else
				return size + " bytes";
		}

		public static void _genericMessageBox(string message, MessageBoxIcon i)
		{
			MessageBox.Show(message, "4chanscraper", MessageBoxButtons.OK, i);
		}
		public static bool _genericConfimBox(string message, MessageBoxIcon i)
		{
			return DialogResult.OK == MessageBox.Show(message, "4chanscraper", MessageBoxButtons.OKCancel, i);
		}
	}
}
