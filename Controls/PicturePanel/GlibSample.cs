using System;
using System.Drawing;

namespace AntPlugin.XMLTreeMenu.Controls
{
	public class GlibSample
	{
		private Glib q = new Glib();

		private double Dist = -1.0;

		private short m;

		private short mm;

		private short nn;

		private short sf;

		private double xp=0.0;

		private double yp=0.0;

		private double x;

		private double y;

		private int[] px = new int[11];

		private int[] py = new int[11];

		private double t;

		private double centerX = 319.5;

		private double centerY = 222.5;

		private int Xhen = 640;

		private int Yhen = 532;

		private double RealBegin;

		private double RealEnd;

		private double ImagiBegin;

		private double ImagiEnd;

		private double xstep;

		private double ystep;

		private double thres;

		private double xscale;

		private double yscale;

		private int xhen;

		private int yhen;

		private int xleft;

		private int xright;

		private int ytop;

		private int ybottom;

		private int count;

		private int max_color = 7;

		private double aspect = 1.189;

		private double dxx;

		private double dyy;

		private void line1(Graphics g, Glib q)
		{
			q._settextposition(g, 3, 26);
			q._setcolor(g, 6);
			q._outtext(g, "-------- p.2  直線（１）  samp11.c --------");
			q._setcolor(g, 4);
			q._moveto(g, 120, 200);
			q._lineto(g, 520, 200);
		}

		public void samp11(Graphics g)
		{
			this.q._ginit(g);
			this.q._setcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆");
			this.line1(g, this.q);
		}

		public void samp12(Graphics g)
		{
			this.q._ginit(g);
			this.q._setcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆");
			short num = 120;
			short num2 = 200;
			short num3 = 520;
			short num4 = num2;
			this.q._settextposition(g, 3, 26);
			this.q._setcolor(g, 6);
			this.q._outtext(g, "-------- p.2  直線（２）  samp12.c --------");
			this.q._setcolor(g, 5);
			this.q._moveto(g, (int)num, (int)num2);
			this.q._lineto(g, (int)num3, (int)num4);
		}

		private void line3(Graphics g, Glib q)
		{
			short num = 120;
			short num2 = 100;
			short num3 = 520;
			short num4 = 300;
			q._setcolor(g, 4);
			q._moveto(g, (int)num, (int)num2);
			q._lineto(g, (int)num3, (int)num4);
		}

		public void samp13(Graphics g)
		{
			this.q._ginit(g);
			this.q._setcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆");
			this.line3(g, this.q);
		}

		private void triangle1(Graphics g, Glib q)
		{
			short[] array = new short[]
			{
				320,
				120,
				520,
				320
			};
			short[] array2 = new short[]
			{
				100,
				300,
				300,
				100
			};
			q._settextcolor(g, 7);
			q._settextposition(g, 2, 32);
			q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			q._settextcolor(g, 6);
			q._settextposition(g, 3, 26);
			q._outtext(g, "-------- p.15  三角形（１） sampl4.c -------\n");
			q._setcolor(g, 5);
			for (short num = 0; num < 3; num += 1)
			{
				q._moveto(g, (int)array[(int)num], (int)array2[(int)num]);
				q._lineto(g, (int)array[(int)(num + 1)], (int)array2[(int)(num + 1)]);
			}
		}

		public void samp14(Graphics g)
		{
			this.q._ginit(g);
			this.triangle1(g, this.q);
		}

		private void triangle2(Graphics g, Glib q)
		{
			int[] array = new int[]
			{
				320,
				120,
				520,
				320
			};
			int[] array2 = new int[]
			{
				100,
				300,
				300,
				100,
				100
			};
			q._settextcolor(g, 7);
			q._settextposition(g, 2, 32);
			q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			q._settextcolor(g, 6);
			q._settextposition(g, 3, 26);
			q._outtext(g, "-------- p.15  三角形（２） sampl5.c -------\n");
			q._settextcolor(g, 5);
			q._fillpolygon(g, array, array2, 4);
		}

		public void samp15(Graphics g)
		{
			this.q._ginit(g);
			this.triangle2(g, this.q);
		}

		public void samp23(Graphics g)
		{
			float[] dash = new float[]
			{
				5f,
				1f,
				1f,
				1f,
				3f,
				1f
			};
			float[] dash2 = new float[]
			{
				4f,
				4f,
				4f,
				4f
			};
			float[] dash3 = new float[]
			{
				1f,
				1f,
				1f,
				1f
			};
			float[] dash4 = new float[]
			{
				2f,
				2f,
				2f,
				2f
			};
			float[] dash5 = new float[]
			{
				5f,
				2f,
				2f,
				2f,
				5f
			};
			float[] dash6 = new float[]
			{
				4f,
				2f,
				1f,
				3f,
				1f,
				1f,
				4f
			};
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "-------- p.40  直線の型\tsamp23.c -------\n");
			int num = 120;
			int num2 = 120;
			int num3 = 520;
			int num4 = num2;
			int num5 = 6;
			for (int i = 0; i < 6; i++)
			{
				switch (i)
				{
				case 0:
					this.q._setlinestyle(g, dash);
					break;
				case 1:
					this.q._setlinestyle(g, dash2);
					break;
				case 2:
					this.q._setlinestyle(g, dash3);
					break;
				case 3:
					this.q._setlinestyle(g, dash4);
					break;
				case 4:
					this.q._setlinestyle(g, dash5);
					break;
				case 5:
					this.q._setlinestyle(g, dash6);
					break;
				}
				this.q._setcolor(g, num5);
				this.q._moveto(g, num, num2);
				this.q._lineto(g, num3, num4);
				num2 += 50;
				num4 += 50;
				num5--;
			}
		}

		public void samp24(Graphics g)
		{
			long num = 13L;
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "------ p.44  乱数による濃淡  samp24.c -------\n");
			int x = 120;
			int num2 = 82;
			int x2 = 520;
			int num3 = 182;
			int num4 = 6;
			for (int i = 1; i <= 3; i++)
			{
				this.q._setcolor(g, num4);
				this.q._rectangle(g, x, num2, x2, num3, 0);
				int num5 = 50 + 100 * (i - 1);
				int num6 = 150 + 100 * (i - 1);
				for (int j = num5; j <= num6; j++)
				{
					for (int k = 120; k <= 520; k++)
					{
						int num7 = (int)((double)((num * 109L + 1021L) % 32768L) / 3276.7);
						if (num7 <= i * 2)
						{
							this.q._setpixel(g, k, j + 32);
						}
					}
				}
				num2 += 100;
				num3 += 100;
				num4--;
			}
		}

		public void samp25(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "------  p.46  放射線状の線  samp25.c -------\n");
			int num = 30;
			int num2 = 280;
			int num3 = 150;
			int x = 320;
			int y = 240;
			for (double num4 = 0.0; num4 <= 6.2831853071795862; num4 += 0.062831853071795868)
			{
				int cl = (int)(num4 * 50.0 / 3.1415926535897931 % 5.0 + 1.0);
				int x2 = (int)((double)num * Math.Cos(num4));
				int y2 = (int)((double)num * Math.Sin(num4));
				int x3 = (int)((double)num2 * Math.Cos(num4));
				int y3 = (int)((double)num3 * Math.Sin(num4));
				this.draw11(g, x2, y2, x3, y3, x, y, cl);
			}
		}

		private void draw11(Graphics g, int x1, int y1, int x2, int y2, int x0, int y0, int cl)
		{
			this.q._setcolor(g, cl);
			this.q._moveto(g, x1 + x0, (int)((double)y1 * this.Dist) + y0);
			this.q._lineto(g, x2 + x0, (int)((double)y2 * this.Dist) + y0);
		}

		private void draw22(Graphics g, short xx, short yy, short x0, short y0, short cl)
		{
			if (this.m == 1)
			{
				this.m = 2;
				this.q._moveto(g, (int)(xx + x0), (int)((double)yy * this.Dist + (double)y0));
				return;
			}
			this.q._setcolor(g, (int)cl);
			this.q._lineto(g, (int)(xx + x0), (int)((double)yy * this.Dist + (double)y0));
		}

		private void star11(Graphics g, short no, short r, double xp, double yp, short x0, short y0, short cl, short sf)
		{
			this.m = 1;
			for (int i = 1; i <= (int)(no * 2 + 1); i++)
			{
				double num = 6.2831853071795862 / (double)no;
				double num2 = num * (double)(i - 1) / 2.0 + 1.5707963267948966;
				int num3 = i % 2;
				double num4;
				if (num3 == 1)
				{
					num4 = (double)r;
				}
				else
				{
					num4 = (double)(r / 2);
				}
				double num5 = num4 * Math.Cos(num2) * (double)sf + xp;
				double num6 = num4 * Math.Sin(num2) * (double)sf + yp;
				this.draw22(g, (short)num5, (short)num6, x0, y0, cl);
			}
		}

		public void samp26(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 28);
			this.q._outtext(g, "------  p.58  星型\t\tsamp26.c  ------\n");
			short num = 1;
			short no = 5;
			short r = 10;
			short x = 320;
			short y = 250;
			this.sf = 10;
			for (short num2 = 1; num2 <= 6; num2 += 1)
			{
				num += 1;
				this.sf += 1;
				this.star11(g, no, r, this.xp, this.yp, x, y, num, this.sf);
			}
		}

		public void samp27(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "------  p.78  楕円の変換\u3000  samp27.c  ------\n");
			short num = 200;
			short num2 = 40;
			short x = 320;
			short y = 240;
			short num3 = 1;
			double tx = 50.0;
			double ty = 90.0;
			double dt = 2.3561944901923448;
			double num4 = 0.5;
			double num5 = 0.5;
			for (short num6 = 1; num6 < 6; num6 += 1)
			{
				this.m = 1;
				num3 += 1;
				for (double num7 = 0.0; num7 <= 6.5973445725385655; num7 += 0.078539816339744828)
				{
					double num8 = (double)num * Math.Cos(num7);
					double num9 = (double)num2 * Math.Sin(num7);
					switch (num6)
					{
					case 2:
						num8 = this.trans2(num8, num9, tx, ty)[0];
						num9 = this.trans2(num8, num9, tx, ty)[1];
						break;
					case 3:
						num8 = this.rotat2(num8, num9, dt)[0];
						num9 = this.rotat2(num8, num9, dt)[1];
						break;
					case 4:
						num8 = this.scale2(num8, num9, num4, num5)[0];
						num9 = this.scale2(num8, num9, num4, num5)[1];
						break;
					case 5:
						num8 = this.shear2(num8, num9, num4, num5)[0];
						num9 = this.shear2(num8, num9, num4, num5)[1];
						break;
					}
					this.draw22(g, (short)num8, (short)num9, x, y, num3);
				}
			}
		}

		private double[] trans2(double fx, double fy, double tx, double ty)
		{
			return new double[]
			{
				fx + tx,
				fy + ty
			};
		}

		private double[] rotat2(double fx, double fy, double dt)
		{
			return new double[]
			{
				fx * Math.Cos(dt) - fy * Math.Sin(dt),
				fx * Math.Sin(dt) + fy * Math.Cos(dt)
			};
		}

		private double[] scale2(double fx, double fy, double sx, double sy)
		{
			return new double[]
			{
				fx * sx,
				fy * sy
			};
		}

		private double[] shear2(double fx, double fy, double shx, double shy)
		{
			return new double[]
			{
				fx + shx * fy,
				fy + shy * fx
			};
		}

		private void scale2(double sx, double sy)
		{
			this.x *= sx;
			this.y *= sy;
		}

		private void cycloi(double r, double t)
		{
			this.x = r * (t - Math.Sin(t));
			this.y = r * (1.0 - Math.Cos(t));
		}

		public void samp31(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "------  p.88  サイクロイド  samp31.c  ------\n");
			short x = 170;
			short y = 400;
			double r = 15.0;
			double num = 1.0;
			for (short num2 = 1; num2 <= 6; num2 += 1)
			{
				this.m = 1;
				short cl = num2;
				for (double num3 = 0.0; num3 <= 18.849555921538759; num3 += 0.15707963267948966)
				{
					this.cycloi(r, num3);
					this.scale2(1.0, num);
					this.draw22(g, (short)this.x, (short)this.y, x, y, cl);
				}
				num += 1.8;
			}
		}

		public void samp32(Graphics g)
		{
			short num = 5;
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "------ p.100 サイクロイド 星型\u3000samp32.c  ----\n");
			double num2 = 170.0;
			double num3 = 400.0;
			double num4 = 6.0;
			short num5 = 1;
			double r = 15.0;
			double num6 = 1.0;
			short r2 = 6;
			for (short num7 = 1; num7 <= 6; num7 += 1)
			{
				for (double num8 = 0.0; num8 <= 18.849555921538759; num8 += 0.31415926535897931)
				{
					this.cycloi(r, num8);
					this.scale2(1.0, num6);
					double num9 = this.x;
					double num10 = this.y;
					this.star11(g, num, r2, num9, num10, (short)num2, (short)num3, (short)num4, num5);
					this.q._settextcolor(g, (int)(2 * num));
					this.q._fillpolygon(g, this.px, this.py, (int)(2 * num));
					num4 -= 1.0;
					if (num4 == 1.0)
					{
						num4 = 6.0;
					}
				}
				num6 += 1.8;
			}
		}

		public void ex31(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "------ p.104 練習問題3-1 四葉形 \u3000ex31.c -----\n");
			short num = 6;
			short x = 320;
			short y = 240;
			short num2 = 7;
			for (short num3 = 40; num3 <= 160; num3 += 40)
			{
				this.m = 1;
				num2 -= 1;
				for (double num4 = 0.0; num4 <= 6.5973445725385655; num4 += 0.026179938779914941)
				{
					double num5 = (double)num3 * Math.Cos((double)num * num4);
					double num6 = num5 * Math.Cos(num4);
					double num7 = num5 * Math.Sin(num4);
					this.x = num6;
					this.y = num7;
					this.draw22(g, (short)num6, (short)num7, x, y, num2);
				}
			}
		}

		public void ex32(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "-- p.106 練習問題3-2 正六角形による図形 ex32.c --\n");
			short num = 24;
			double num2 = 180.0;
			short x = 320;
			short y = 250;
			short num3 = 6;
			double num4 = 0.0;
			for (short num5 = 1; num5 <= num; num5 += 1)
			{
				this.m = 1;
				for (double num6 = num4; num6 <= 6.5973445725385655 + num4; num6 += 1.0471975511965976)
				{
					double num7 = num2 * Math.Cos(num6);
					double num8 = num2 * Math.Sin(num6);
					this.x = num7;
					this.y = num8;
					this.draw22(g, (short)num7, (short)num8, x, y, num3);
				}
				num3 -= 1;
				if (num3 < 1)
				{
					num3 = 6;
				}
				num4 += 0.52359877559829882;
				num2 = Math.Sqrt(3.0) / 2.0 * num2;
			}
		}

		public void samp41(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "---------- p.114 正弦曲線 ex32.c -----------\n");
			short x = 100;
			short y = 200;
			short num = 7;
			this.q._setcolor(g, (int)num);
			this.q._moveto(g, 100, 50);
			this.q._lineto(g, 100, 350);
			this.q._moveto(g, 50, 200);
			this.q._lineto(g, 540, 200);
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 13);
			this.q._outtext(g, "Y");
			this.q._settextposition(g, 14, 11);
			this.q._outtext(g, "0");
			this.q._settextposition(g, 13, 69);
			this.q._outtext(g, "X");
			this.q._settextcolor(g, 7);
			for (double num2 = 0.0; num2 <= 25.0; num2 += 5.0)
			{
				this.m = 1;
				this.t = 0.0;
				while (this.t <= 25.132741228718345)
				{
					double sy = num2 * Math.Exp(this.t / 12.0);
					this.x = this.t;
					this.y = Math.Sin(this.t);
					this.scale2(15.0, sy);
					this.draw22(g, (short)this.x, (short)this.y, x, y, num);
					this.t += 0.15707963267948966;
				}
				num -= 1;
			}
		}

		private void rotat2(double dt)
		{
			double num = this.x * Math.Cos(dt) - this.y * Math.Sin(dt);
			double num2 = this.x * Math.Sin(dt) + this.y * Math.Cos(dt);
			this.x = num;
			this.y = num2;
		}

		public void samp42(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "---------- p.124  扇   samp42.c ----------\n");
			short x = 320;
			short y = 360;
			short num = 0;
			double num2 = 0.31415926535897931;
			for (double num3 = num2; num3 < 10.0 * num2; num3 += num2)
			{
				for (double num4 = 0.0; num4 <= 25.0; num4 += 5.0)
				{
					this.m = 1;
					num += 1;
					if (num == 7)
					{
						num = 1;
					}
					for (double num5 = 0.0; num5 <= 80.0 * num2; num5 += num2 / 2.0)
					{
						double sy = num4 * Math.Exp(num5 / 30.0);
						this.x = num5;
						this.y = Math.Sin(num5);
						this.scale2(10.0, sy);
						this.rotat2(num3);
						this.draw22(g, (short)this.x, (short)this.y, x, y, num);
					}
				}
			}
		}

		public void ex41(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "--------- p.124 三葉形の回転  ex41.c --------\n");
			short x = 310;
			short y = 240;
			short num = 6;
			for (double num2 = 0.0; num2 <= 1.7278759594743864; num2 += 0.52359877559829882)
			{
				for (short num3 = 40; num3 <= 200; num3 += 40)
				{
					this.m = 1;
					num -= 1;
					if (num == 1)
					{
						num = 6;
					}
					for (double num4 = 0.0; num4 <= 6.2831853071795862; num4 += 0.062831853071795868)
					{
						double num5 = 0.8 * (double)num3 * Math.Sin(3.0 * num4);
						this.x = num5 * Math.Cos(num4);
						this.y = num5 * Math.Sin(num4);
						this.rotat2(num2);
						this.draw22(g, (short)this.x, (short)this.y, x, y, num);
					}
				}
			}
		}

		private void astero(Graphics g, short a, double x0, double y0, short cl)
		{
			this.m = 1;
			for (double num = 0.0; num <= 6.2831853071795862; num += 0.062831853071795868)
			{
				double num2 = (double)a * Math.Cos(num) * Math.Cos(num) * Math.Cos(num);
				double num3 = (double)a * Math.Sin(num) * Math.Sin(num) * Math.Sin(num);
				this.draw22(g, (short)num2, (short)num3, (short)x0, (short)y0, cl);
			}
		}

		public void ex42(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "------- p.128 アステロイドの合成 ex42.c -------\n");
			double num = 88.0;
			double num2 = num;
			double num3 = 320.0;
			double num4 = 240.0;
			double num5 = 6.0;
			for (double num6 = 0.0; num6 <= 4.0; num6 += 1.0)
			{
				double num7;
				double num8;
				if (num6 == 0.0)
				{
					num7 = 320.0;
					num8 = 200.0;
				}
				else
				{
					double num9 = num6 * 3.1415926535897931 / 2.0;
					num7 = num2 * Math.Cos(num9) + num3;
					num8 = num2 * Math.Sin(num9) * this.Dist + num4;
					num = 72.0;
					num5 = 5.0;
					this.astero(g, (short)num, num7, num8, (short)num5);
					num = 48.0;
					num5 = 3.0;
				}
				this.astero(g, (short)num, num7, num8, (short)num5);
			}
		}

		public void samp51(Graphics g)
		{
			long[] array = new long[8];
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "---- p.136 放射線状の線の回転運動 samp51.c ----\n");
			array[0] = 0L;
			array[1] = 983040L;
			array[2] = 3840L;
			array[3] = 986880L;
			array[4] = 15L;
			array[5] = 983055L;
			array[6] = 3855L;
			array[7] = 986895L;
			short num = 60;
			short num2 = 300;
			short num3 = 180;
			short x = 320;
			short y = 260;
			short num4 = 7;
			for (double num5 = 0.0; num5 <= 6.2831853071795862; num5 += 0.044879895051282759)
			{
				double num6 = (double)num * Math.Cos(num5);
				double num7 = (double)num * Math.Sin(num5);
				double num8 = (double)num2 * Math.Cos(num5);
				double num9 = (double)num3 * Math.Sin(num5);
				this.draw11(g, (int)num6, (int)num7, (int)num8, (int)num9, (int)x, (int)y, (int)num4);
				num4 -= 1;
				if (num4 < 1)
				{
					num4 = 7;
				}
			}
		}

		public void samp52(Graphics g)
		{
			long[] array = new long[2];
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 22);
			this.q._outtext(g, "--- p.136 外転サイクロイドの円の運動 samp52.c ---\n");
			array[0] = 986880L;
			array[1] = 0L;
			double num = 100.0;
			double num2 = 16.66;
			double num3 = num2;
			short num4 = 310;
			short num5 = 260;
			this.m = 1;
			for (double num6 = 0.0; num6 <= 6.5973445725385655; num6 += 1.0471975511965976)
			{
				double num7 = num * Math.Cos(num6);
				double num8 = num * Math.Sin(num6);
				this.draw22(g, (short)num7, (short)num8, num4, num5, 6);
			}
			for (short num9 = 1; num9 <= 2; num9 += 1)
			{
				this.m = 1;
				short num10 = 8;
				for (double num6 = 0.0; num6 <= 6.5973445725385655; num6 += 0.054165390579134366)
				{
					double num7 = (num + num2) * Math.Cos(num6) - num3 * Math.Cos((num + num2) * num6 / num2);
					double num8 = (num + num2) * Math.Sin(num6) - num3 * Math.Sin((num + num2) * num6 / num2);
					this.draw22(g, (short)num7, (short)num8, num4, num5, 6);
					short num11 = (short)(num7 + (double)num4);
					short num12 = (short)(num8 * this.Dist + (double)num5);
					num10 -= 1;
					if (num10 < 1)
					{
						num10 = 7;
					}
					this.q._setcolor(g, (int)num10);
					this.q._filloval(g, (int)(num11 - 9), (int)(num12 - 9), (int)(num11 + 9), (int)(num12 + 9));
				}
				num3 += 42.0;
			}
		}

		private void flower(Graphics g, double xa, double ya, short cl, short cs)
		{
			short c = 0;
			for (short num = 0; num <= 5; num += 1)
			{
				double num2 = 3.1415926535897931 * (double)num * 0.33;
				double num3 = 45.0 * Math.Cos(num2) + xa;
				double num4 = 45.0 * Math.Sin(num2) * this.Dist + ya;
				for (short num5 = 1; num5 <= 14; num5 += 1)
				{
					double num6 = (double)num5 * 3.1415926535897931 * 0.143;
					double num7 = 15.0 * Math.Cos(num6) + num3;
					double num8 = 15.0 * Math.Sin(num6) * this.Dist + num4;
					for (short num9 = 1; num9 <= 14; num9 += 1)
					{
						double num10 = 3.1415926535897931 * (double)num9 * 0.143;
						double num11 = 5.0 * Math.Cos(num10) + num7;
						double num12 = 5.0 * Math.Sin(num10) * this.Dist + num8;
						short c2 = (short)(num9 % 7 + 1);
						this.q._setcolor(g, (int)c2);
						this.q._filloval(g, (int)num11, (int)num12, (int)(num11 + 1.0), (int)(num12 + 1.0));
					}
					c = (short)(num5 % 7 + 1);
					this.q._setcolor(g, (int)c);
					this.q._moveto(g, (int)num3, (int)num4);
					this.q._lineto(g, (int)num7, (int)num8);
				}
				this.q._setcolor(g, (int)c);
				this.q._moveto(g, (int)xa, (int)ya);
				this.q._lineto(g, (int)num3, (int)num4);
			}
			for (short num13 = 0; num13 <= 5; num13 += 1)
			{
				double num14 = 3.1415926535897931 * (0.5 + (double)num13) * 0.33;
				double num15 = 30.0 * Math.Cos(num14) + xa;
				double num16 = 30.0 * Math.Sin(num14) * this.Dist + ya;
				this.q._setcolor(g, 2);
				this.q._moveto(g, (int)xa, (int)ya);
				this.q._lineto(g, (int)num15, (int)num16);
			}
			for (short num17 = 0; num17 <= 11; num17 += 1)
			{
				double num18 = 3.1415926535897931 * ((double)num17 + 0.5) * 0.167;
				double num19 = 15.0 * Math.Cos(num18) + xa;
				double num20 = 15.0 * Math.Sin(num18) * this.Dist + ya;
				this.q._setcolor(g, (int)cs);
				this.q._moveto(g, (int)xa, (int)ya);
				this.q._lineto(g, (int)num19, (int)num20);
			}
		}

		public void ex51(Graphics g)
		{
			long[] array = new long[8];
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "----------- p.146 花の動画 ex51.c -----------\n");
			array[0] = 0L;
			array[1] = 983040L;
			array[2] = 3840L;
			array[3] = 986880L;
			array[4] = 15L;
			array[5] = 983055L;
			array[6] = 3855L;
			array[7] = 986895L;
			short num = 320;
			short num2 = 260;
			short cl = 4;
			for (short num3 = 0; num3 <= 6; num3 += 1)
			{
				double num4 = 3.1415926535897931 * (double)(num3 - 1) * 0.33;
				short num5 = (short)(145.0 * Math.Cos(num4) + (double)num);
				short num6 = (short)(145.0 * Math.Sin(num4) * this.Dist + (double)num2);
				short cs = (short)(num3 + 1);
				if (num3 == 0)
				{
					num5 = 320;
					num6 = 260;
				}
				this.flower(g, (double)num5, (double)num6, cl, cs);
			}
		}

		public void ex52(Graphics g)
		{
			long[] array = new long[2];
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "---- p.149 カージオイド上の正方形の運動 ex52.c ----\n");
			short num = 60;
			short num2 = 320;
			short num3 = 220;
			short num4 = 6;
			double dt = 1.5707963267948966;
			short num5 = 20;
			array[0] = 986885L;
			array[1] = 0L;
			for (double num6 = 0.0; num6 <= 6.2831853071795862; num6 += 0.074799825085471269)
			{
				this.x = (double)num * (2.0 * Math.Cos(num6) - Math.Cos(2.0 * num6));
				this.y = (double)num * (2.0 * Math.Sin(num6) - Math.Sin(2.0 * num6));
				this.rotat2(dt);
				short num7 = (short)(this.x + (double)num2);
				short num8 = (short)(this.y * this.Dist + (double)num3);
				this.q._setcolor(g, (int)num4);
				this.q._rectangle(g, (int)(num7 - num5), (int)(num8 - num5), (int)(num7 + num5), (int)(num8 + num5), 1);
				num4 -= 1;
				if (num4 < 1)
				{
					num4 = 7;
				}
			}
		}

		public void samp61(Graphics g)
		{
			double num = 0.0;
			double num2 = 0.0;
			double[] array = new double[32];
			double[] array2 = new double[32];
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "--- p.158 Ｊｏｕｋｏｗｓｋｉの翼型 samp61.c ---\n");
			double num3 = 3.0;
			short num4 = 5;
			short num5 = 150;
			short num6 = 200;
			short num7 = 25;
			for (short num8 = 1; num8 <= 2; num8 += 1)
			{
				short num9 = 0;
				if (num8 == 1)
				{
					num5 = 150;
					num = -0.76;
					num2 = 0.0;
				}
				if (num8 == 2)
				{
					num5 = 450;
					num = -0.6;
					num2 = 1.0;
				}
				for (double num10 = 0.0; num10 < 6.5973445725385655; num10 += 0.20943951023931953)
				{
					double num11 = Math.Cos(num10) * num;
					double num12 = Math.Sin(num10) * num2;
					double num13 = num3 * Math.Cos(num10) + num;
					double num14 = num3 * Math.Sin(num10) + num2;
					double num15 = num3 * num3 + 2.0 * num3 * (num11 + num12) + num * num + num2 * num2;
					array[(int)num9] = num13 * (1.0 + (double)num4 / num15);
					array2[(int)num9] = num14 * (1.0 - (double)num4 / num15);
					num9 += 1;
				}
				this.q._setcolor(g, 4);
				this.q._moveto(g, (int)num5, (int)(num6 - 120));
				this.q._lineto(g, (int)num5, (int)(num6 + 100));
				this.q._moveto(g, (int)(num5 - 140), (int)num6);
				this.q._lineto(g, (int)(num5 + 130), (int)num6);
				short cl = 6;
				for (short num16 = 0; num16 < 30; num16 += 1)
				{
					double num17 = array[(int)num16] * (double)num7;
					double num18 = array2[(int)num16] * (double)num7;
					double num19 = array[(int)(num16 + 1)] * (double)num7;
					double num20 = array2[(int)(num16 + 1)] * (double)num7;
					this.draw11(g, (int)num17, (int)num18, (int)num19, (int)num20, (int)num5, (int)num6, (int)cl);
				}
			}
			this.q._settextcolor(g, 5);
			this.q._settextposition(g, 5, 57);
			this.q._outtext(g, "V");
			this.q._settextposition(g, 5, 19);
			this.q._outtext(g, "V");
			this.q._settextposition(g, 13, 74);
			this.q._outtext(g, "U");
			this.q._settextposition(g, 13, 37);
			this.q._outtext(g, "U");
			this.q._settextposition(g, 14, 55);
			this.q._outtext(g, "0");
			this.q._settextposition(g, 14, 17);
			this.q._outtext(g, "0");
			this.q._settextposition(g, 1, 1);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 23, 2);
		}

		public void samp62(Graphics g)
		{
			double[][] array = new double[13][];
			short num;
			for (num = 0; num < 13; num += 1)
			{
				array[(int)num] = new double[21];
			}
			double[][] array2 = new double[13][];
			for (num = 0; num < 13; num += 1)
			{
				array2[(int)num] = new double[21];
			}
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "--------- p.164 流線     samp62.c --------\n");
			short num2 = 310;
			short num3 = 245;
			short num4 = 7;
			num = 0;
			short cl = 6;
			this.q._setcolor(g, 4);
			this.q._moveto(g, (int)num2, (int)(num3 - 160));
			this.q._lineto(g, (int)num2, (int)(num3 + 160));
			this.q._moveto(g, (int)(num2 - 160), (int)num3);
			this.q._lineto(g, (int)(num2 + 190), (int)num3);
			this.q._settextcolor(g, 5);
			this.q._settextposition(g, 4, 38);
			this.q._outtext(g, "Y");
			this.q._settextposition(g, 14, 64);
			this.q._outtext(g, "X");
			this.q._settextposition(g, 14, 36);
			this.q._outtext(g, "0");
			this.q._settextposition(g, 1, 1);
			this.q._settextcolor(g, 7);
			for (double num5 = -3.0; num5 <= 3.1; num5 += 0.5)
			{
				short num6 = 0;
				double num7 = 0.31415926535897931;
				for (double num8 = -10.0 * num7; num8 <= 10.1 * num7; num8 += num7)
				{
					double num9 = Math.Exp(num5) * Math.Cos(num8);
					double num10 = Math.Exp(num5) * Math.Sin(num8);
					array[(int)num][(int)num6] = (num5 + num9) * (double)num4;
					array2[(int)num][(int)num6] = (num8 + num10) * (double)num4;
					num6 += 1;
				}
				num += 1;
			}
			for (short num6 = 0; num6 < 21; num6 += 1)
			{
				for (num = 0; num < 12; num += 1)
				{
					double num11 = array[(int)num][(int)num6];
					double num12 = array2[(int)num][(int)num6];
					double num13 = array[(int)(num + 1)][(int)num6];
					double num14 = array2[(int)(num + 1)][(int)num6];
					this.draw11(g, (int)((short)num11), (int)((short)num12), (int)((short)num13), (int)((short)num14), (int)num2, (int)num3, (int)cl);
				}
			}
			for (num = 0; num < 13; num += 1)
			{
				for (short num6 = 0; num6 < 20; num6 += 1)
				{
					double num11 = array[(int)num][(int)num6];
					double num12 = array2[(int)num][(int)num6];
					double num13 = array[(int)num][(int)(num6 + 1)];
					double num14 = array2[(int)num][(int)(num6 + 1)];
					this.draw11(g, (int)((short)num11), (int)((short)num12), (int)((short)num13), (int)((short)num14), (int)num2, (int)num3, (int)cl);
				}
			}
		}

		public void ex61(Graphics g)
		{
			double[] array = new double[32];
			double[] array2 = new double[32];
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "------- p.166 w=z+c/zの写像 ex61.c -------\n");
			double num = 3.0;
			short num2 = 5;
			short num3 = 150;
			short num4 = 200;
			short num5 = 20;
			double num6 = 0.0;
			double num7 = 0.0;
			this.q._setcolor(g, 4);
			this.q._moveto(g, (int)num3, (int)(num4 - 120));
			this.q._lineto(g, (int)num3, (int)(num4 + 120));
			this.q._moveto(g, (int)(num3 - 140), (int)num4);
			this.q._lineto(g, (int)(num3 + 130), (int)num4);
			num3 = 450;
			this.q._moveto(g, (int)num3, (int)(num4 - 120));
			this.q._lineto(g, (int)num3, (int)(num4 + 120));
			this.q._moveto(g, (int)(num3 - 140), (int)num4);
			this.q._lineto(g, (int)(num3 + 130), (int)num4);
			this.q._settextcolor(g, 5);
			this.q._settextposition(g, 5, 56);
			this.q._outtext(g, "V  [ W平面 ]");
			this.q._settextposition(g, 5, 18);
			this.q._outtext(g, "Y  [ Z平面 ]");
			this.q._settextposition(g, 12, 73);
			this.q._outtext(g, "U");
			this.q._settextposition(g, 12, 36);
			this.q._outtext(g, "X");
			this.q._settextposition(g, 14, 55);
			this.q._outtext(g, "0");
			this.q._settextposition(g, 14, 17);
			this.q._outtext(g, "0");
			for (short num8 = 1; num8 <= 3; num8 += 1)
			{
				short num9 = 0;
				this.m = 1;
				num += 0.6;
				num3 = 150;
				short cl = 6;
				for (double num10 = 0.0; num10 <= 6.5973445725385655; num10 += 0.20943951023931953)
				{
					double num11 = Math.Cos(num10) * num6;
					double num12 = Math.Sin(num10) * num7;
					double num13 = num * Math.Cos(num10) + num6;
					double num14 = num * Math.Sin(num10) + num7;
					double num15 = num * num + 2.0 * num * (num11 + num12) + num6 * num6 + num7 * num7;
					array[(int)num9] = num13 * (1.0 + (double)num2 / num15);
					array2[(int)num9] = num14 * (1.0 - (double)num2 / num15);
					double num16 = num * Math.Cos(num10) * (double)num5;
					double num17 = num * Math.Sin(num10) * (double)num5;
					this.draw22(g, (short)num16, (short)num17, num3, num4, cl);
					num9 += 1;
				}
				num3 = 450;
				for (short num18 = 0; num18 < 30; num18 += 1)
				{
					double num19 = array[(int)num18] * (double)num5;
					double num20 = array2[(int)num18] * (double)num5;
					double num21 = array[(int)(num18 + 1)] * (double)num5;
					double num22 = array2[(int)(num18 + 1)] * (double)num5;
					this.draw11(g, (int)num19, (int)num20, (int)num21, (int)num22, (int)num3, (int)num4, (int)cl);
				}
			}
		}

		private Obj[] grotat(Obj[] rot, Obj t)
		{
			rot[0].x = Math.Cos(t.y) * Math.Cos(t.z);
			rot[0].y = Math.Sin(t.x) * Math.Sin(t.y) * Math.Cos(t.z) - Math.Cos(t.x) * Math.Sin(t.z);
			rot[0].z = Math.Cos(t.x) * Math.Sin(t.y) * Math.Cos(t.z) + Math.Sin(t.x) * Math.Sin(t.z);
			rot[1].x = Math.Cos(t.y) * Math.Sin(t.z);
			rot[1].y = Math.Sin(t.x) * Math.Sin(t.y) * Math.Sin(t.z) + Math.Cos(t.x) * Math.Cos(t.z);
			rot[1].z = Math.Cos(t.x) * Math.Sin(t.y) * Math.Sin(t.z) - Math.Sin(t.x) * Math.Cos(t.z);
			rot[2].x = -Math.Sin(t.y);
			rot[2].y = Math.Sin(t.x) * Math.Cos(t.y);
			rot[2].z = Math.Cos(t.x) * Math.Cos(t.y);
			return rot;
		}

		public void samp71(Graphics g)
		{
			double[] array = new double[2540];
			double[] array2 = new double[2540];
			double[] array3 = new double[2];
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "---- p.174 コッホ曲線による図形 samp.71.c ----\n");
			short num = 7;
			short x = 320;
			short y = 260;
			this.m = 1;
			short cl = 6;
			short num2 = 2;
			double dt = 1.0471975511965976;
			int num3 = 4;
			short num4 = (short)Math.Pow(4.0, (double)num3);
			short num5 = (short)(num4 * (num - 1) + 1);
			array[0] = 0.0;
			array2[0] = 90.0;
			array[(int)num4] = -78.0;
			array2[(int)num4] = -45.0;
			array[(int)(2 * num4)] = 78.0;
			array2[(int)(2 * num4)] = -45.0;
			array[(int)(3 * num4)] = 0.0;
			array2[(int)(3 * num4)] = 90.0;
			array[(int)(4 * num4)] = 78.0;
			array2[(int)(4 * num4)] = -45.0;
			array[(int)(5 * num4)] = -78.0;
			array2[(int)(5 * num4)] = -45.0;
			array[(int)(6 * num4)] = 0.0;
			array2[(int)(6 * num4)] = 90.0;
			for (int i = 0; i < (int)num5; i += (int)num4)
			{
				double num6 = array[i] * (double)num2;
				double num7 = array2[i] * (double)num2;
				this.draw22(g, (short)num6, (short)num7, x, y, cl);
			}
			for (int i = 1; i <= num3; i++)
			{
				double num8 = Math.Pow(4.0, (double)(num3 + 1 - i));
				short num9 = (short)num8;
				for (int j = 0; j <= (int)(num5 - 1); j += (int)num9)
				{
					double num10 = Math.Pow(4.0, (double)(num3 - i));
					short num11 = (short)num10;
					if (j < (int)(num5 - 1))
					{
						array[j + (int)num11] = (array[j + (int)num9] - array[j]) / 3.0 + array[j];
						array2[j + (int)num11] = (array2[j + (int)num9] - array2[j]) / 3.0 + array2[j];
						array[j + (int)(num11 * 3)] = 2.0 * (array[j + (int)num9] - array[j]) / 3.0 + array[j];
						array2[j + (int)(num11 * 3)] = 2.0 * (array2[j + (int)num9] - array2[j]) / 3.0 + array2[j];
						this.dxx = array[j + (int)(num11 * 3)] - array[j + (int)num11];
						this.dyy = array2[j + (int)(num11 * 3)] - array2[j + (int)num11];
						array3 = this.rotat2(this.dxx, this.dyy, dt);
						this.dxx = array3[0];
						this.dyy = array3[1];
						array[j + (int)(num11 * 2)] = array[j + (int)num11] + this.dxx;
						array2[j + (int)(num11 * 2)] = array2[j + (int)num11] + this.dyy;
						double num12 = array[j + (int)num11] * (double)num2;
						double num13 = array2[j + (int)num11] * (double)num2;
						double num14 = array[j + (int)(num11 * 2)] * (double)num2;
						double num15 = array2[j + (int)(num11 * 2)] * (double)num2;
						double num16 = array[j + (int)(num11 * 3)] * (double)num2;
						double num17 = array2[j + (int)(num11 * 3)] * (double)num2;
						this.draw11(g, (int)num12, (int)num13, (int)num14, (int)num15, (int)x, (int)y, (int)cl);
						this.draw11(g, (int)num14, (int)num15, (int)num16, (int)num17, (int)x, (int)y, (int)cl);
					}
				}
			}
		}

		public void frac11(Graphics g, double ar, double ai, double br, double bi, double cr, double ci, double dr, double di, double t, double r2, short rep, short sf, short x0, short y0, short cl)
		{
			double[] array = new double[12];
			double[] array2 = new double[12];
			double[] array3 = new double[12];
			double[] array4 = new double[12];
			double[] array5 = new double[12];
			double[] array6 = new double[12];
			short[] array7 = new short[12];
			double[] array8 = new double[2];
			short num = 1;
			short num2 = 1;
			array[0] = (array2[0] = 0.0);
			while (true)
			{
				switch (num2)
				{
				case 1:
				{
					double num3 = array[(int)(num - 1)];
					double num4 = array2[(int)(num - 1)];
					array[(int)num] = num3 * (ar + br) - num4 * (ai - bi);
					array2[(int)num] = num3 * (ai + bi) + num4 * (ar - br);
					break;
				}
				case 2:
				{
					double num3 = array[(int)(num - 1)];
					double num4 = array2[(int)(num - 1)];
					array[(int)num] = num3 * (cr + dr) - num4 * (ci - di) - cr - dr + 1.0;
					array2[(int)num] = num3 * (ci + di) + num4 * (cr - dr) - ci - di;
					break;
				}
				}
				double num5 = r2 * Math.Cos(t);
				double num6 = r2 * Math.Sin(t);
				array3[(int)num] = (array5[(int)num] = array[(int)num]);
				array4[(int)num] = array2[(int)num];
				array6[(int)num] = -array2[(int)num];
				this.dxx = array3[(int)num];
				this.dyy = array4[(int)num];
				array8 = this.rotat2(this.dxx, this.dyy, t);
				this.dxx = array8[0];
				this.dyy = array8[1];
				array3[(int)num] = this.dxx;
				array4[(int)num] = this.dyy;
				this.dxx = array5[(int)num];
				this.dyy = array6[(int)num];
				array8 = this.rotat2(this.dxx, this.dyy, t);
				this.dxx = array8[0];
				this.dyy = array8[1];
				array5[(int)num] = this.dxx;
				array6[(int)num] = this.dyy;
				array3[(int)num] += num5;
				array5[(int)num] += num5;
				array4[(int)num] += num6;
				array6[(int)num] += num6;
				array3[(int)num] *= (double)sf;
				array5[(int)num] *= (double)sf;
				array4[(int)num] *= (double)sf;
				array6[(int)num] *= (double)sf;
				this.draw11(g, (int)array3[(int)num], (int)array4[(int)num], (int)(array3[(int)num] + 1.0), (int)(array4[(int)num] + 1.0), (int)x0, (int)y0, (int)cl);
				this.draw11(g, (int)array5[(int)num], (int)array6[(int)num], (int)(array5[(int)num] + 1.0), (int)(array6[(int)num] + 1.0), (int)x0, (int)y0, (int)cl);
				short[] expr_312_cp_0 = array7;
				short expr_312_cp_1 = num;
				expr_312_cp_0[(int)expr_312_cp_1] = (short)(expr_312_cp_0[(int)expr_312_cp_1] + 1);
				num2 = 1;
				num += 1;
				if (num > rep)
				{
					do
					{
						array7[(int)num] = 0;
						num2 = 2;
						num -= 1;
					}
					while (array7[(int)num] >= 2);
					if (num < 1)
					{
						break;
					}
				}
			}
		}

		public void samp72(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "--------- p.186 雪の結晶 samp.72.c ---------\n");
			short num = 6;
			double num2 = 0.0;
			double num3 = 0.0;
			short x = 320;
			short y = 260;
			short cl = 7;
			short num4 = 1;
			short rep = 10;
			double num5 = 0.0;
			double num6 = 0.0;
			double num7 = 0.53;
			double num8 = 0.333;
			double num9 = 0.0;
			double num10 = 0.0;
			double num11 = 0.884;
			double num12 = 0.0;
			for (short num13 = 6; num13 <= 30; num13 += 12)
			{
				this.star11(g, num, num13, num2, num3, x, y, cl, num4);
			}
			double num14 = 6.2831853071795862 / (double)num;
			double num15 = 0.12;
			num4 = 240;
			for (short num16 = 1; num16 <= num; num16 += 1)
			{
				double num17 = num14 * (double)(num16 - 1) + 1.5707963267948966;
				this.frac11(g, num5, num6, num7, num8, num9, num10, num11, num12, num17, num15, rep, num4, x, y, cl);
			}
		}

		public void frac22(Graphics g, double ar, double ai, double br, double bi, double cr, double ci, double dr, double di, double t, short rep, short sf, short x0, short y0, short cl)
		{
			double[] array = new double[12];
			double[] array2 = new double[12];
			double[] array3 = new double[12];
			double[] array4 = new double[12];
			short[] array5 = new short[12];
			double[] array6 = new double[2];
			short num = 1;
			short num2 = 1;
			array[0] = (array2[0] = 0.0);
			while (true)
			{
				switch (num2)
				{
				case 1:
				{
					double num3 = array[(int)(num - 1)];
					double num4 = array2[(int)(num - 1)];
					array[(int)num] = num3 * (ar + br) - num4 * (ai - bi);
					array2[(int)num] = num3 * (ai + bi) + num4 * (ar - br);
					break;
				}
				case 2:
				{
					double num3 = array[(int)(num - 1)];
					double num4 = array2[(int)(num - 1)];
					array[(int)num] = num3 * (cr + dr) - num4 * (ci - di) - cr - dr + 1.0;
					array2[(int)num] = num3 * (ci + di) + num4 * (cr - dr) - ci - di;
					break;
				}
				}
				array3[(int)num] = array[(int)num] * (double)sf;
				array4[(int)num] = array2[(int)num] * (double)sf;
				this.dxx = array3[(int)num];
				this.dyy = array4[(int)num];
				array6 = this.rotat2(this.dxx, this.dyy, t);
				this.dxx = array6[0];
				this.dyy = array6[1];
				array3[(int)num] = this.dxx;
				array4[(int)num] = this.dyy;
				this.draw11(g, (int)array3[(int)num], (int)array4[(int)num], (int)(array3[(int)num] + 1.0), (int)(array4[(int)num] + 1.0), (int)x0, (int)y0, (int)cl);
				short[] expr_195_cp_0 = array5;
				short expr_195_cp_1 = num;
				expr_195_cp_0[(int)expr_195_cp_1] = (short)(expr_195_cp_0[(int)expr_195_cp_1] + 1);
				num2 = 1;
				num += 1;
				if (num > rep)
				{
					do
					{
						array5[(int)num] = 0;
						num2 = 2;
						num -= 1;
					}
					while (array5[(int)num] >= 2);
					if (num < 1)
					{
						break;
					}
				}
			}
		}

		public void ex71(Graphics g)
		{
			short num = 0;
			short x = 0;
			short y = 0;
			short cl = 0;
			short rep = 0;
			double num2 = 0.0;
			double ar = 0.0;
			double br = 0.0;
			double cr = 0.0;
			double dr = 0.0;
			double ai = 0.0;
			double bi = 0.0;
			double ci = 0.0;
			double di = 0.0;
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "----------- p.190 盆栽  ex7-1.c  -----------\n");
			for (short num3 = 1; num3 <= 2; num3 += 1)
			{
				if (num3 == 1)
				{
					ar = 0.0;
					ai = 0.0;
					br = 0.5;
					bi = 0.288;
					cr = 0.0;
					ci = 0.0;
					dr = 0.666;
					di = 0.0;
					num2 = 1.5707963267948966;
					rep = 10;
					num = 240;
					x = 320;
					y = 320;
					cl = 2;
				}
				if (num3 == 2)
				{
					ar = 0.5;
					ai = 0.5;
					br = 0.0;
					bi = 0.0;
					cr = 0.5;
					ci = -0.5;
					dr = 0.0;
					di = 0.0;
					num2 = 0.0;
					rep = 10;
					num = 80;
					x = 280;
					y = 400;
					cl = 6;
				}
				this.frac22(g, ar, ai, br, bi, cr, ci, dr, di, num2, rep, num, x, y, cl);
			}
		}

		public void ex72(Graphics g)
		{
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "------------- p.186 雲  ex7-2.c  ----------\n");
			double ar = 0.461;
			double ai = 0.461;
			double br = 0.0;
			double bi = 0.0;
			double cr = 0.622;
			double ci = -0.196;
			double dr = 0.0;
			double di = 0.0;
			double num = 0.0;
			short rep = 10;
			short num2 = 150;
			short x = 250;
			short y = 340;
			short cl = 2;
			this.frac22(g, ar, ai, br, bi, cr, ci, dr, di, num, rep, num2, x, y, cl);
			num = -0.52359877559829882;
			rep = 10;
			num2 = 150;
			x = 400;
			y = 190;
			cl = 4;
			this.frac22(g, ar, ai, br, bi, cr, ci, dr, di, num, rep, num2, x, y, cl);
			num = -0.26179938779914941;
			rep = 10;
			num2 = 150;
			x = 100;
			y = 190;
			cl = 6;
			this.frac22(g, ar, ai, br, bi, cr, ci, dr, di, num, rep, num2, x, y, cl);
		}

		public Obj sphere(Obj pp, double r, double ta, double tb)
		{
			pp.x = r * Math.Sin(ta) * Math.Cos(tb);
			pp.y = r * Math.Sin(ta) * Math.Sin(tb);
			pp.z = r * Math.Cos(ta);
			return pp;
		}

		public void drawbb(Graphics g, Obj ss, short x0, short y0, short cl)
		{
			if (this.m == 1)
			{
				this.m = 2;
				this.q._moveto(g, (int)(ss.x + (double)x0), (int)(ss.y * this.Dist + (double)y0));
				return;
			}
			this.q._setcolor(g, (int)cl);
			this.q._lineto(g, (int)(ss.x + (double)x0), (int)(ss.y * this.Dist + (double)y0));
		}

		public void samp81(Graphics g)
		{
			double ta = 0.0;
			double tb = 0.0;
			Obj obj = new Obj();
			Obj[] array = new Obj[3];
			Obj obj2 = new Obj();
			Obj obj3 = new Obj();
			new Obj();
			for (short num = 0; num < 3; num += 1)
			{
				array[(int)num] = new Obj();
			}
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "        ----------- p.202  球  samp81.c -----------\n");
			short num2 = 36;
			double r = 156.0;
			short x = 320;
			short y = 248;
			double num3 = 0.017453292519943295;
			obj2.x = -50.0 * num3;
			obj2.y = 30.0 * num3;
			obj2.z = 0.0;
			short cl = 6;
			double arg_123_0 = 3.1415926535897931 / (double)num2;
			array = this.grotat(array, obj2);
			double num4 = array[0].x;
			double num5 = array[0].y;
			double z = array[0].z;
			double num6 = array[1].x;
			double num7 = array[1].y;
			double z2 = array[1].z;
			double num8 = array[2].x;
			double num9 = array[2].y;
			double z3 = array[2].z;
			for (short num10 = 1; num10 <= 2; num10 += 1)
			{
				for (short num11 = 0; num11 <= num2; num11 += 1)
				{
					if (num10 == 1)
					{
						tb = (double)num11 * 3.1415926535897931 / (double)num2;
					}
					if (num10 == 2)
					{
						ta = (double)num11 * 3.1415926535897931 / (double)num2;
					}
					this.m = 1;
					for (short num12 = 0; num12 <= 2 * num2; num12 += 1)
					{
						if (num10 == 1)
						{
							ta = (double)num12 * 3.1415926535897931 / (double)num2;
						}
						if (num10 == 2)
						{
							tb = (double)num12 * 3.1415926535897931 / (double)num2;
						}
						obj = this.sphere(obj, r, ta, tb);
						obj3.z = num8 * obj.x + num9 * obj.y + z3 * obj.z;
						if (obj3.z < 0.0)
						{
							this.m = 1;
						}
						else
						{
							obj3.x = num4 * obj.x + num5 * obj.y + z * obj.z;
							obj3.y = num6 * obj.x + num7 * obj.y + z2 * obj.z;
							this.drawbb(g, obj3, x, y, cl);
						}
					}
				}
			}
		}

		public void samp82(Graphics g)
		{
			Obj[] array = new Obj[3];
			Obj obj = new Obj();
			Obj obj2 = new Obj();
			Obj obj3 = new Obj();
			for (short num = 0; num < 3; num += 1)
			{
				array[(int)num] = new Obj();
			}
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 28);
			this.q._outtext(g, "------------ p.212 手鞠 samp82.c ------------\n");
			short num2 = 72;
			double r = 156.0;
			short x = 320;
			short y = 248;
			double num3 = 0.017453292519943295;
			double num4 = 3.1415926535897931 / (double)num2;
			obj3.x = -55.0 * num3;
			obj3.y = 30.0 * num3;
			obj3.z = 10.0 * num3;
			array = this.grotat(array, obj3);
			double num5 = array[0].x;
			double num6 = array[0].y;
			double z = array[0].z;
			double num7 = array[1].x;
			double num8 = array[1].y;
			double z2 = array[1].z;
			double num9 = array[2].x;
			double num10 = array[2].y;
			double z3 = array[2].z;
			for (short num = 1; num <= 3; num += 1)
			{
				this.mm = 1;
				short num11 = 1;
				for (double num12 = 10.0 * num4; num12 <= 3.1415926535897931 + 10.0 * num4; num12 += num4)
				{
					this.m = 1;
					this.mm %= 7;
					if (this.mm == 1)
					{
						num12 += 5.0 * num4;
						num11 = 1;
					}
					for (double num13 = 0.0; num13 <= 6.5973445725385655; num13 += num4)
					{
						obj = this.sphere(obj, r, num13, num12);
						switch (num - 1)
						{
						case 1:
						{
							double z4 = obj.x;
							obj.x = obj.y;
							obj.y = obj.z;
							obj.z = z4;
							break;
						}
						case 2:
						{
							double z4 = obj.x;
							obj.x = obj.z;
							obj.z = obj.y;
							obj.y = z4;
							break;
						}
						}
						obj2.z = num9 * obj.x + num10 * obj.y + z3 * obj.z;
						if (obj2.z < 0.0)
						{
							this.m = 1;
						}
						else
						{
							obj2.x = num5 * obj.x + num6 * obj.y + z * obj.z;
							obj2.y = num7 * obj.x + num8 * obj.y + z2 * obj.z;
							this.drawbb(g, obj2, x, y, num11);
						}
					}
					this.mm += 1;
					num11 += 1;
				}
			}
		}

		public void ex81(Graphics g)
		{
			Obj[] array = new Obj[3];
			Obj obj = new Obj();
			Obj obj2 = new Obj();
			Obj obj3 = new Obj();
			long[] array2 = new long[8];
			for (int i = 0; i < 3; i++)
			{
				array[i] = new Obj();
			}
			this.q._ginit(g);
			this.q._settextcolor(g, 7);
			this.q._settextposition(g, 2, 32);
			this.q._outtext(g, "◆ Ｃ言語と基礎図形 ◆\n");
			this.q._settextcolor(g, 6);
			this.q._settextposition(g, 3, 26);
			this.q._outtext(g, "  ---------- p.214  球の回転 ex81.c ----------\n");
			array2[0] = 0L;
			array2[1] = 983040L;
			array2[2] = 3840L;
			array2[3] = 986880L;
			array2[4] = 15L;
			array2[5] = 983055L;
			array2[6] = 3855L;
			array2[7] = 986895L;
			this.nn = 42;
			double r = 156.0;
			short x = 320;
			short y = 248;
			double num = 0.017453292519943295;
			obj3.x = -50.0 * num;
			obj3.y = 30.0 * num;
			obj3.z = 0.0;
			short num2 = 6;
			double num3 = 3.1415926535897931 / (double)this.nn;
			array = this.grotat(array, obj3);
			double num4 = array[0].x;
			double num5 = array[0].y;
			double z = array[0].z;
			double num6 = array[1].x;
			double num7 = array[1].y;
			double z2 = array[1].z;
			double num8 = array[2].x;
			double num9 = array[2].y;
			double z3 = array[2].z;
			for (double num10 = 0.0; num10 <= 3.1415926535897931; num10 += num3)
			{
				this.m = 1;
				for (double num11 = 0.0; num11 <= 6.5973445725385655; num11 += num3)
				{
					this.sphere(obj, r, num11, num10);
					obj2.z = num8 * obj.x + num9 * obj.y + z3 * obj.z;
					if (obj2.z < 0.0)
					{
						this.m = 1;
					}
					else
					{
						obj2.x = num4 * obj.x + num5 * obj.y + z * obj.z;
						obj2.y = num6 * obj.x + num7 * obj.y + z2 * obj.z;
						this.drawbb(g, obj2, x, y, num2);
					}
				}
				num2 -= 1;
				if (num2 < 1)
				{
					num2 = 7;
				}
			}
		}

		private int Mset(double real, double imagi)
		{
			int num = 0;
			int num2 = this.count;
			double num4;
			double num3 = num4 = 0.0;
			double num5;
			double num6;
			while (num < num2 && (num5 = num4 * num4) + (num6 = num3 * num3) < this.thres)
			{
				double num7 = num5 - num6 + real;
				num3 = 2.0 * num4 * num3 + imagi;
				num4 = num7;
				num++;
			}
			if (num == num2)
			{
				return 0;
			}
			return num;
		}

		private int parminp()
		{
			this.RealBegin = -2.0;
			this.RealEnd = 0.5;
			this.ImagiBegin = -1.25;
			this.ImagiEnd = 1.25;
			this.xscale = 1.0;
			this.yscale = 1.0;
			this.xhen = this.Xhen;
			this.yhen = this.Yhen;
			this.xleft = (int)(this.centerX - (double)(this.xhen / 2));
			this.xright = (int)(this.centerX + (double)(this.xhen / 2));
			this.ytop = (int)(this.centerY - (double)(this.yhen / 2) / this.aspect);
			this.ybottom = (int)(this.centerY + (double)(this.yhen / 2) / this.aspect);
			this.xstep = (this.RealEnd - this.RealBegin) / (double)(this.xright - this.xleft) * this.xscale;
			this.ystep = (this.ImagiEnd - this.ImagiBegin) / (double)(this.ybottom - this.ytop) * this.yscale;
			this.thres = 4.0;
			this.count = 50;
			return 0;
		}

		public void mandegrobe(Graphics g)
		{
			this.q._ginit(g);
			this.parminp();
			double num = this.ImagiBegin;
			for (int i = this.ytop; i <= this.ybottom; i++)
			{
				double num2 = this.RealBegin;
				for (int j = this.xleft; j <= this.xright; j++)
				{
					int num3 = this.Mset(num2, num);
					if (num3 > 0)
					{
						this.q._setcolor(g, num3 % this.max_color + 1);
						this.q._setpixel(g, j, i);
					}
					num2 += this.xstep;
				}
				num += this.ystep;
			}
		}
	}
}
