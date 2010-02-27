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
#if DEBUG
			DebugConsole.ShowConsole();
#endif

			DebugConsole.ShowStatus("Program startup...showing main form.");
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
	}
}
