using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scraper.Data
{
	[Serializable]
	public class Thread
	{
		private int id;
		private string name;
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
		public string Name
		{
			get { return this.Name; }
			set { this.Name = value; }
		}

		public Thread(int id, string name)
		{
			this.id = id;
			this.name = name;
			this.posts = new List<Post>();
		}
		public Thread(int id) : this(id, null) { }

		public void AddPost(Post post)
		{
			this.posts.Add(post);
		}
	}
}
