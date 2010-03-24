namespace Scraper.Dialogs
{
	partial class frmNewDatabaseDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtDbLoc = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.cbBoardURL = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnAutoPopulate = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtDbName = new System.Windows.Forms.TextBox();
			this.chkCrawlAll = new System.Windows.Forms.CheckBox();
			this.chkScrapeNow = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtDbLoc
			// 
			this.txtDbLoc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtDbLoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.txtDbLoc.Location = new System.Drawing.Point(12, 25);
			this.txtDbLoc.Name = "txtDbLoc";
			this.txtDbLoc.Size = new System.Drawing.Size(356, 20);
			this.txtDbLoc.TabIndex = 0;
			this.txtDbLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDbLoc_KeyDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Database location:";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(280, 51);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// cbBoardURL
			// 
			this.cbBoardURL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbBoardURL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbBoardURL.FormattingEnabled = true;
			this.cbBoardURL.Location = new System.Drawing.Point(12, 96);
			this.cbBoardURL.Name = "cbBoardURL";
			this.cbBoardURL.Size = new System.Drawing.Size(356, 21);
			this.cbBoardURL.TabIndex = 3;
			this.cbBoardURL.SelectedIndexChanged += new System.EventHandler(this.cbBoardURL_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Board URL:";
			// 
			// btnAutoPopulate
			// 
			this.btnAutoPopulate.Location = new System.Drawing.Point(259, 123);
			this.btnAutoPopulate.Name = "btnAutoPopulate";
			this.btnAutoPopulate.Size = new System.Drawing.Size(96, 23);
			this.btnAutoPopulate.TabIndex = 5;
			this.btnAutoPopulate.Text = "Auto-populate list";
			this.btnAutoPopulate.UseVisualStyleBackColor = true;
			this.btnAutoPopulate.Click += new System.EventHandler(this.btnAutoPopulate_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 156);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(87, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Database Name:";
			// 
			// txtDbName
			// 
			this.txtDbName.Location = new System.Drawing.Point(102, 153);
			this.txtDbName.Name = "txtDbName";
			this.txtDbName.Size = new System.Drawing.Size(266, 20);
			this.txtDbName.TabIndex = 7;
			// 
			// chkCrawlAll
			// 
			this.chkCrawlAll.AutoSize = true;
			this.chkCrawlAll.Checked = true;
			this.chkCrawlAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCrawlAll.Location = new System.Drawing.Point(102, 179);
			this.chkCrawlAll.Name = "chkCrawlAll";
			this.chkCrawlAll.Size = new System.Drawing.Size(175, 17);
			this.chkCrawlAll.TabIndex = 8;
			this.chkCrawlAll.Text = "Crawl All Pages On First Scrape";
			this.chkCrawlAll.UseVisualStyleBackColor = true;
			// 
			// chkScrapeNow
			// 
			this.chkScrapeNow.AutoSize = true;
			this.chkScrapeNow.Checked = true;
			this.chkScrapeNow.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkScrapeNow.Location = new System.Drawing.Point(102, 196);
			this.chkScrapeNow.Name = "chkScrapeNow";
			this.chkScrapeNow.Size = new System.Drawing.Size(151, 17);
			this.chkScrapeNow.TabIndex = 9;
			this.chkScrapeNow.Text = "Start Scraping Immediately";
			this.chkScrapeNow.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(100, 219);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(207, 219);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 10;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 246);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(380, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 12;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(132, 17);
			this.lblStatus.Text = "Waiting for User Input...";
			// 
			// frmNewDatabaseDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(380, 268);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.chkScrapeNow);
			this.Controls.Add(this.chkCrawlAll);
			this.Controls.Add(this.txtDbName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnAutoPopulate);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cbBoardURL);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtDbLoc);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
			this.Name = "frmNewDatabaseDialog";
			this.Text = "Create New Database";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.TextBox txtDbLoc;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.ComboBox cbBoardURL;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnAutoPopulate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtDbName;
		private System.Windows.Forms.CheckBox chkCrawlAll;
		private System.Windows.Forms.CheckBox chkScrapeNow;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;

	}
}