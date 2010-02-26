using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

			new Scraper.Html.BoardParser("").Parse();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(mainForm = new frmMain());
		}
	}
}
