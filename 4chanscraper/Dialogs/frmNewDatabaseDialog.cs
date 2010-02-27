using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Scraper.Html;
using System.IO;

namespace Scraper.Dialogs
{
	public partial class frmNewDatabaseDialog : Form
	{
		public frmNewDatabaseDialog()
		{
			InitializeComponent();
		}

		public bool StartNow
		{
			get { return this.chkScrapeNow.Checked; }
		}
		public bool ScrapeAll
		{ get { return this.chkCrawlAll.Checked; } }
		public string DBName
		{
			get { return this.txtDbName.Text; }
		}
		public string DBLoc
		{ get { return this.txtDbLoc.Text; } }
		public string DBUrl
		{
			get { return this.cbBoardURL.Text; }
		}

		#region Event Listeners
		void txtDbLoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.Handled = true;
				this.btnBrowse_Click(sender, new EventArgs());
			}
		}
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			SaveFileDialog fd = new SaveFileDialog();
			fd.AddExtension = true;
			fd.CheckPathExists = true;
			fd.DefaultExt = ".db";
			fd.Filter = "Database files (*.db)|*.db|All files (*.*)|*.*";
			fd.FilterIndex = 0;
			fd.OverwritePrompt = true;
			fd.RestoreDirectory = true;
			fd.Title = "Select Database File";

			if (this.txtDbLoc.Text != "")
				fd.FileName = this.txtDbLoc.Text;

			DialogResult r = fd.ShowDialog();
			if (r == DialogResult.OK)
			{
				this.txtDbLoc.Text = fd.FileName;
			}
		}

		private void cbBoardURL_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cbBoardURL.SelectedIndex >= 0)
			{
				this.txtDbName.Text = this.cbBoardURL.SelectedItem.ToString().Replace("http://boards.4chan.org", "");
			}
		}
		private void btnAutoPopulate_Click(object sender, EventArgs e)
		{
			this.lblStatus.Text = "Retrieving board index...";
			BoardListParser blp = new BoardListParser("http://boards.4chan.org/b/");
			Application.DoEvents();

			string[] s = blp.GetBoardURLs();
			this.cbBoardURL.Items.Clear();
			this.cbBoardURL.Items.AddRange(s);
			this.cbBoardURL.SelectedIndex = 0;

			this.lblStatus.Text = "Done!";
			MessageBox.Show("Found " + this.cbBoardURL.Items.Count + " boards.", "4chanscraper", MessageBoxButtons.OK, MessageBoxIcon.Information);
			this.lblStatus.Text = "Waiting for user input...";
		}
		#endregion

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.lblStatus.Text = "Validating input...";
			Application.DoEvents();

			try
			{
				FileInfo fi = new FileInfo(this.txtDbLoc.Text);
				bool exists = fi.Exists;
				Directory.CreateDirectory(fi.DirectoryName);
				fi.Create().Close();
				if(!exists) fi.Delete();
			}
			catch
			{
				MessageBox.Show("An error occurred creating the database file; ensure you have permissions to the directory in which you are creating it.", "4chanscraper", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				using (BoardParser bp = new BoardParser(this.cbBoardURL.Text))
				{
					if (bp.DetectPageCount() < 1) throw new Exception();
				}
			}
			catch
			{
				MessageBox.Show("An error occured parsing your board URL; ensure the URL is a valid 4chan board.", "4chanscraper", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
