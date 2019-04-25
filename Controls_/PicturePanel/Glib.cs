using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AntPanelApplication.Controls
{
	public class Glib
	{
		public int Lpx;

		public int Lpy;

		public int Txx;

		public int Txy;

		public Color fgColor;

		public Color textColor;

		public Brush textBrush;

		public Color bgcolor;

		public Brush myBrush;

		public Font textFont;

		public Pen myPen = new Pen(Color.White, 1f);

		public float[] mylinestyle = new float[]
		{
			1.1f
		};

		public Bitmap myBitmap = new Bitmap(640, 400);

		public void _ginit(Graphics g)
		{
			this.myBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
			g.FillRectangle(this.myBrush, new Rectangle(0, 0, 640, 480));
			this.textFont = new Font("FixedSys", 8f);
		}

		public void _settextposition(Graphics g, int y, int x)
		{
			this.Txx = x * 8;
			this.Txy = y * 16;
		}

		public void _setcolor(Graphics g, int c)
		{
			switch (c)
			{
			case 0:
				this.fgColor = Color.FromKnownColor(KnownColor.Black);
				this.textBrush = (this.myBrush = Brushes.Black);
				this.myPen = new Pen(Color.Black, 1f);
				return;
			case 1:
				this.fgColor = Color.FromKnownColor(KnownColor.Blue);
				this.textBrush = (this.myBrush = Brushes.Blue);
				this.myPen = new Pen(Color.Blue, 1f);
				return;
			case 2:
				this.fgColor = Color.FromKnownColor(KnownColor.Green);
				this.textBrush = (this.myBrush = Brushes.Cyan);
				this.myPen = new Pen(Color.Green, 1f);
				return;
			case 3:
				this.fgColor = Color.FromKnownColor(KnownColor.Cyan);
				this.textBrush = (this.myBrush = Brushes.Green);
				this.myPen = new Pen(Color.Cyan, 1f);
				return;
			case 4:
				this.fgColor = Color.FromKnownColor(KnownColor.Red);
				this.textBrush = (this.myBrush = Brushes.Red);
				this.myPen = new Pen(Color.Red, 1f);
				return;
			case 5:
				this.fgColor = Color.FromKnownColor(KnownColor.Magenta);
				this.textBrush = (this.myBrush = Brushes.Magenta);
				this.myPen = new Pen(Color.Magenta, 1f);
				return;
			case 6:
				this.fgColor = Color.FromKnownColor(KnownColor.Yellow);
				this.textBrush = (this.myBrush = Brushes.Yellow);
				this.myPen = new Pen(Color.Yellow, 1f);
				return;
			case 7:
				this.fgColor = Color.FromKnownColor(KnownColor.White);
				this.textBrush = (this.myBrush = Brushes.White);
				this.myPen = new Pen(Color.White, 1f);
				return;
			default:
				this.fgColor = Color.FromKnownColor(KnownColor.White);
				this.textBrush = (this.myBrush = Brushes.White);
				this.myPen = new Pen(Color.White, 1f);
				return;
			}
		}

		public void _settextcolor(Graphics g, int c)
		{
			this._setcolor(g, c);
		}

		public void _outtext(Graphics g, string s)
		{
			PointF point = new PointF((float)this.Txx, (float)this.Txy);
			g.DrawString(s, this.textFont, this.textBrush, point);
		}

		public void _moveto(Graphics g, int x, int y)
		{
			this.Lpx = x;
			this.Lpy = y;
		}

		public void _lineto(Graphics g, int x, int y)
		{
			int lpx = this.Lpx;
			int lpy = this.Lpy;
			this.Lpx = x;
			this.Lpy = y;
			this.myPen.DashPattern = this.mylinestyle;
			g.DrawLine(this.myPen, lpx, lpy, x, y);
		}

		public void _setpixel(Graphics g, int x, int y)
		{
			g.FillEllipse(this.textBrush, x, y, 2, 2);
		}

		public void _setpixel2(Graphics g, int x, int y)
		{
			g.DrawImage(this.myBitmap, 0, 0, this.myBitmap.Width, this.myBitmap.Height);
			this.myBitmap.SetPixel(x, y, this.fgColor);
		}

		public void _rectangle(Graphics g, int x1, int y1, int x2, int y2, int style)
		{
			if (style == 0)
			{
				g.DrawRectangle(this.myPen, x1, y1, x2 - x1, y2 - y1);
				return;
			}
			g.FillRectangle(this.myBrush, x1, y1, x2 - x1, y2 - y1);
		}

		public void _fillpolygon(Graphics g, int[] x, int[] y, int n)
		{
			Point[] array = new Point[x.Length];
			for (int i = 0; i < x.Length; i++)
			{
				array[i] = new Point(x[i], y[i]);
			}
			g.FillPolygon(this.textBrush, array, FillMode.Winding);
		}

		public void _filloval(Graphics g, int x1, int y1, int x2, int y2)
		{
			g.FillEllipse(this.textBrush, x1, y1, x2 - x1, y2 - y1);
		}

		public void _setlinestyle(Graphics g, float[] dash)
		{
			this.mylinestyle = dash;
		}
	}
}
