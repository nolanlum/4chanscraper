﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Scraper
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			new Scraper.Html.BoardParser("").Parse();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain());
		}
	}
}
