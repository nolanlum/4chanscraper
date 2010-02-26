using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Scraper
{
	public partial class frmMain : Form
	{
		public delegate void __UpdateStatusText(string newtext);

		private bool _enableAutoScrape;
		private int _downloaderThreads;

		public bool EnableAutoScrape
		{
			get { return this._enableAutoScrape; }
			set
			{
				this.cmTaskTray_Enabled.Checked = value;
				this.mnuMain_ScraperEnabled.Checked = value;
				this._enableAutoScrape = value;
			}
		}
		public int DownloaderThreads
		{
			get { return this._downloaderThreads; }
			set
			{
				if (!(value >= 1 && value <= 4)) return;
				this.mnuMain_ScraperThreads.SelectedIndex = value - 1;
				this._downloaderThreads = value;
			}
		}

		public frmMain()
		{
			InitializeComponent();
#if DEBUG
			this.mnuMain_HelpDebug.Checked = true;
#endif
			this.mnuMain_ScraperThreads.SelectedIndex = 0;
			this.FormClosing += new FormClosingEventHandler(frmMain_FormClosing);
		}

		public void UpdateStatusText(string newText)
		{
			this.lblStatus.Text = newText;
		}

		#region Event Listeners
		#region Menu Items
		#region File
		private void mnuMain_FileExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion
		#region Scraper Config
		private void mnuMain_ScraperConf_Click(object sender, EventArgs e)
		{
			Dialogs.frmInputDialog input = new Scraper.Dialogs.frmInputDialog("Enter the amount of time between automatic scrapes, with a time unit following the number. (h=hour,m=minute,s=second)\nYou may choose any combination of the units.\nEx: 30s = 30 seconds; 5m30s = 5 minutes, 30 seconds");
			TimeSpan sp = new TimeSpan(this.timerAutoScrape.Interval * 10000);
			input.InputText = string.Format("{0}h{1}m{2}s", sp.Hours, sp.Minutes, sp.Seconds).Replace("0h", "").Replace("0m","").Replace("0s","");
			input.ShowDialog();

			string timestring = input.InputText.Trim();
			if (timestring == "")
				return;

			Regex h = new Regex("([0-9]*)h", RegexOptions.IgnoreCase), m = new Regex("([0-9]*)m", RegexOptions.IgnoreCase), s = new Regex("([0-9]*)s", RegexOptions.IgnoreCase);
			Match hm = h.Match(timestring), mm = m.Match(timestring), sm = s.Match(timestring);
			int newInterval = 0;

			if(hm.Success)
				newInterval += int.Parse(hm.Groups[1].Value) * 3600000;
			if(mm.Success)
				newInterval += int.Parse(mm.Groups[1].Value) * 60000;
			if (sm.Success)
				newInterval += int.Parse(sm.Groups[1].Value) * 1000;

			if (newInterval == 0)
				MessageBox.Show("There was an error parsing your input; ensure you specified a valid time string.", "4chanscraper", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else
				this.timerAutoScrape.Interval = newInterval;
		}
		private void mnuMain_ScraperThreads_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this._downloaderThreads = this.mnuMain_ScraperThreads.SelectedIndex - 1;
		}
		#endregion
		#region Help
		private void mnuMain_HelpAbout_Click(object sender, EventArgs e)
		{
			new frmAbout().ShowDialog();
		}
		private void mnuMain_HelpDebug_Click(object sender, EventArgs e)
		{
			if (this.mnuMain_HelpDebug.Checked)
				DebugConsole.HideConsole();
			else
				DebugConsole.ShowConsole();

			this.mnuMain_HelpDebug.Checked = !this.mnuMain_HelpDebug.Checked;
		}
		#endregion
		#endregion

		#region Taskbar Items
		private void cmTaskTray_Show_Click(object sender, EventArgs e)
		{
			this.taskTrayIcon_DoubleClick(sender, e);
		}
		
		private void cmTaskTray_Enabled_Click(object sender, EventArgs e)
		{
			this.EnableAutoScrape = !this.EnableAutoScrape;
		}

		private void cmTaskTray_Close_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion

		void frmMain_Resize(object sender, System.EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized && this.mnuMain_FileMinimize.Checked)
				this.Hide();
		}
		void taskTrayIcon_DoubleClick(object sender, System.EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;

			this.TopMost = true;
			this.Focus();
			this.BringToFront();
			this.TopMost = false;
		}
		#endregion

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Put stuff to interrupt downloads, save databases, etc.
			DebugConsole.ShowStatus("Main form received close request; terminating all background tasks.");
		}
	}
}
