using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Scraper.Dialogs
{
	public partial class frmDetailDialog : Form
	{
		const int WM_NCLBUTTONDOWN = 0x0A1;
		const int WM_KEYDOWN = 0x100;
		const int WM_KEYUP = 0x101;
		const int WM_SYSKEYDOWN = 0x104;
		const int WM_SYSKEYUP = 0x105;
		const int WM_SIZING = 0x214;

		const int WMSZ_LEFT = 1;
		const int WMSZ_RIGHT = 2;
		const int WMSZ_TOP = 3;
		const int WMSZ_BOTTOM = 6;

		const int HT_CAPTION = 0x2;

		[DllImportAttribute("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		private static extern bool ReleaseCapture();

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		private double widthRatio = 4, heightRatio = 3;
		private int widthOverhead = 0, heightOverhead = 0;

		public Image PostImage
		{
			get { return this.picPicture.Tag as Image; }
			set { this.picPicture.Tag = value; this.ResizeImageBestFit(); }
		}
		public string PostString
		{
			get { return this.txtPost.Text; }
			set { this.txtPost.Text = value.Replace("\n", Environment.NewLine); }
		}

		public frmDetailDialog()
		{
			InitializeComponent();

			this.picPicture.Tag = new Bitmap(640, 480);

			this.MouseDown += new MouseEventHandler(this.frmDetailDialog_MouseDown);
			this.picPicture.MouseDown += new MouseEventHandler(this.frmDetailDialog_MouseDown);
		}

		public void ResizeImageBestFit()
		{
			Size max = SystemInformation.PrimaryMonitorSize - new Size(50, 100);
			Size twenty = new Size((int) (this.PostImage.Width * .2), (int) (this.PostImage.Height * .2));
			Size form;
			Image newI = (Image) this.PostImage.Clone();

			while(newI.Width >= max.Width || newI.Height >= max.Height)
				newI = new Bitmap(newI, newI.Size - twenty);

			// Set autosizing to true.
			this.AutoSize = true;
			this.picPicture.SizeMode = PictureBoxSizeMode.AutoSize;

			// Set image and then save size.
			this.picPicture.Image = newI;
			form = this.Size;

			// Un-autosize and reset size.
			this.AutoSize = false;
			this.picPicture.SizeMode = PictureBoxSizeMode.StretchImage;
			this.Size = form;
			this.picPicture.Size = newI.Size;

			// Set scaling information.
			this.widthRatio = newI.Width;
			this.heightRatio = newI.Height;
			this.widthOverhead = (form - newI.Size).Width;
			this.heightOverhead = (form - newI.Size).Height;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void frmDetailDialog_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_SIZING && (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				RECT rc = (RECT) Marshal.PtrToStructure(m.LParam, typeof(RECT));
				int res = m.WParam.ToInt32();

				if (res == WMSZ_LEFT || res == WMSZ_RIGHT)
				{
					rc.Bottom = rc.Top + (int) (this.heightRatio * this.picPicture.Width / this.widthRatio) + this.heightOverhead;
				}
				else if (res == WMSZ_TOP || res == WMSZ_BOTTOM)
				{
					//Up or down resize -> adjust width (right)
					rc.Right = rc.Left + (int) (this.widthRatio * this.picPicture.Height / this.heightRatio) + this.widthOverhead;
				}
				else if (res == WMSZ_RIGHT + WMSZ_BOTTOM)
				{
					//Lower-right corner resize -> adjust height (could have been width)
					rc.Bottom = rc.Top + (int) (this.heightRatio * this.picPicture.Width / this.widthRatio) + this.heightOverhead;
				}
				else if (res == WMSZ_LEFT + WMSZ_TOP)
				{
					//Upper-left corner -> adjust width (could have been height)
					rc.Left = rc.Right - (int) (this.widthRatio * this.picPicture.Height / this.heightRatio) + this.widthOverhead;
				}
				Marshal.StructureToPtr(rc, m.LParam, true);
			}

			base.WndProc(ref m);
		}
	}
}
