using System;
using System.Drawing;

namespace AntPlugin.XMLTreeMenu.Controls
{
	public struct DrawPoints
	{
		public Point start_point;

		public Point end_point;

		public bool flag;

		public void SetPoints(Point sp, Point ep, int adjust)
		{
			this.start_point.X = sp.X - adjust;
			this.start_point.Y = sp.Y - adjust;
			this.end_point.X = ep.X - adjust;
			this.end_point.Y = ep.Y - adjust;
		}

		public void SetPoints(Point ep, int adjust)
		{
			this.end_point.X = ep.X - adjust;
			this.end_point.Y = ep.Y - adjust;
		}

		public void SetPoints(Point ep)
		{
			this.end_point = ep;
		}

		public Rectangle GetRectangle()
		{
			int width = Math.Abs(this.start_point.X - this.end_point.X);
			if (this.start_point.X > this.end_point.X)
			{
				this.start_point.X = this.end_point.X;
			}
			int height = Math.Abs(this.start_point.Y - this.end_point.Y);
			if (this.start_point.Y > this.end_point.Y)
			{
				this.start_point.Y = this.end_point.Y;
			}
			return new Rectangle(this.start_point.X, this.start_point.Y, width, height);
		}

		public Rectangle GetDragRectangle()
		{
			int width = this.end_point.X - this.start_point.X;
			int height = this.end_point.Y - this.start_point.Y;
			return new Rectangle(this.start_point.X, this.start_point.Y, width, height);
		}
	}
}
