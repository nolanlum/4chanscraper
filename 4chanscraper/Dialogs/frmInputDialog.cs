using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scraper.Dialogs
{
	public partial class frmInputDialog : Form
	{
		public frmInputDialog(string prompt)
		{
			InitializeComponent();

			this.lblPrompt.Text = prompt;
		}

		public string InputText
		{
			get { return this.textBox1.Text; }
			set { this.textBox1.Text = value; }
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
