using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scraper.Data
{
	[Serializable]
	class Thread
	{
		private int id;
		private List<Post> posts;

		public int Id
		{
			get { return this.id; }
		}
		public Post this[int index]
		{
			get { return this.posts[index]; }
		}
		public int Count
		{
			get { return this.posts.Count; }
		}

		public Thread(int id)
		{
			this.id = id;
			this.posts = new List<Post>();
		}

		public void AddPost(Post post)
		{
			this.posts.Add(post);
		}
	}
}
