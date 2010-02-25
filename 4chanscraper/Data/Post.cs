using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Scraper.Data
{
	[Serializable]
	class Post
	{
		#region Private Members
		private int id;
		private string body;
		private string imgName;
		[NonSerialized] private Bitmap imgBmap;
		#endregion

		#region Public Properties
		public int Id
		{
			get { return this.id; }
		}
		public string PostBody
		{
			get { return this.body; }
		}
		public string ImagePath
		{
			get { return this.imgName; }
		}
		public Bitmap ImageBitmap
		{
			get
			{
				try
				{
					if (this.imgBmap == null)
						this.imgBmap = new Bitmap(this.imgName);

					return this.imgBmap;
				}
				catch (Exception e)
				{
					DebugConsole.ShowWarning("Error loading image for post ID " + this.id + ": " + e.GetType().Name + " " + e.Message);
					return null;
				}
			}
		}
		#endregion

		public Post(int id, string body, string imgPath)
		{
			this.id = id;
			this.body = body;
			this.imgName = imgPath;
		}

		public void PreloadBitmap()
		{
			this.ImageBitmap.ToString();
		}
	}
}
