using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scraper
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		#region Event Listeners
		private void mnuMain_HelpDebug_Click(object sender, EventArgs e)
		{
			if (this.mnuMain_HelpDebug.Checked)
				DebugConsole.HideConsole();
			else
				DebugConsole.ShowConsole();

			this.mnuMain_HelpDebug.Checked = !this.mnuMain_HelpDebug.Checked;
		}
		#endregion
	}
}
