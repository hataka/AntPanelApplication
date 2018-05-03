using System;
using System.Drawing;

//namespace MDIForm
//{
	public struct DrawImages
	{
		private Bitmap image;

		public string file;

		public bool flag;

		public Bitmap Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
			}
		}

		public void Initialize()
		{
			this.image = null;
			this.file = "";
			this.flag = false;
		}
	}
//}
