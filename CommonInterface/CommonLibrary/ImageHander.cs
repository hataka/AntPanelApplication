using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace CommonInterface.CommonLibrary
{
	public class ImageHander
	{
		public static Bitmap RotateBitmap(Bitmap bmp, float angle, int x, int y)
		{
			Bitmap bitmap = new Bitmap(bmp.Width, bmp.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.Clear(Color.Black);
			graphics.TranslateTransform((float)(-(float)x), (float)(-(float)y));
			graphics.RotateTransform(angle, MatrixOrder.Append);
			graphics.TranslateTransform((float)x, (float)y, MatrixOrder.Append);
			graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
			graphics.DrawImageUnscaled(bmp, 0, 0);
			graphics.Dispose();
			return bitmap;
		}

		public static Bitmap RotateBitmap(Bitmap bmp, float angle)
		{
			if (bmp != null)
			{
				double num = (double)angle / 57.295779513082323;
				float num2 = (float)bmp.Width * (float)Math.Cos(num);
				float num3 = (float)bmp.Width * (float)Math.Sin(num);
				float num4 = (float)bmp.Height * (float)Math.Sin(num);
				float num5 = (float)bmp.Height * (float)Math.Cos(num);
				PointF[] destPoints = new PointF[]
				{
					new PointF(0f, 0f),
					new PointF(num2, num3),
					new PointF(num4, num5)
				};
				Bitmap bitmap = new Bitmap((int)Math.Abs(num4 - num2), (int)Math.Abs(num5 - num3));
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.Clear(Color.Transparent);
				graphics.DrawImage(bmp, destPoints);
				graphics.Dispose();
				bmp.Dispose();
				return bitmap;
			}
			return bmp;
		}

		public static Icon BitmapToIcon(Bitmap bitmap)
		{
			IntPtr hicon = bitmap.GetHicon();
			return Icon.FromHandle(hicon);
		}

		public static Bitmap IconToBitmap(Icon icon)
		{
			return icon.ToBitmap();
		}

		public static Bitmap ResizeImage(Bitmap image, double dw, double dh)
		{
			double num = (double)image.Width;
			double num2 = (double)image.Height;
			double num3;
			if (dh / dw <= num2 / num)
			{
				num3 = dh / num2;
			}
			else
			{
				num3 = dw / num;
			}
			int width = (int)(num * num3);
			int height = (int)(num2 * num3);
			Bitmap bitmap = new Bitmap(width, height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
			return bitmap;
		}

		public ImageList ImageListCombine(ImageList imagelist1, ImageList imagelist2)
		{
			ImageList imageList = new ImageList();
			for (int i = 0; i < imagelist1.Images.Count; i++)
			{
				imageList.Images.Add(imagelist1.Images[i]);
			}
			for (int j = 0; j < imagelist2.Images.Count; j++)
			{
				imageList.Images.Add(imagelist2.Images[j]);
			}
			return imageList;
		}

		public static Icon getIconFromUrl(string url)
		{
			Icon result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					using (MemoryStream memoryStream = new MemoryStream(webClient.DownloadData(url)))
					{
						result = new Icon(memoryStream);
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static Icon getIconFromWebUrl(string url)
		{
			Uri uri = new Uri(url);
			WebBrowser webBrowser = new WebBrowser();
			webBrowser.ScrollBarsEnabled = false;
			webBrowser.ScriptErrorsSuppressed = true;
			webBrowser.Navigate(url);
			while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
			{
				Application.DoEvents();
			}
			Thread.Sleep(1500);
			foreach (HtmlElement htmlElement in webBrowser.Document.GetElementsByTagName("link"))
			{
				string attribute = htmlElement.GetAttribute("rel");
				if (attribute == "shortcut icon" || attribute == "icon")
				{
					string attribute2 = htmlElement.GetAttribute("href");
					Icon iconFromUrl;
					if (attribute2.StartsWith("http"))
					{
						iconFromUrl = ImageHander.getIconFromUrl(attribute2);
						return iconFromUrl;
					}
					if (attribute2.StartsWith("/"))
					{
						iconFromUrl = ImageHander.getIconFromUrl("http://" + uri.Host + attribute2);
						return iconFromUrl;
					}
					iconFromUrl = ImageHander.getIconFromUrl(url + "/" + attribute2);
					return iconFromUrl;
				}
			}
			return ImageHander.getIconFromUrl("http://" + uri.Host + "/favicon.ico");
		}

		public static Point[] GetPolygonVertex(int number, Point center, double radius, double rotate)
		{
			Point[] result;
			try
			{
				if (number <= 2)
				{
					throw new ArgumentException();
				}
				Point[] array = new Point[number];
				for (int i = 0; i < number; i++)
				{
					array[i] = new Point
					{
						X = (int)(Math.Sin(((double)i + rotate) * 6.2831853071795862 / (double)number) * radius) + center.X,
						Y = (int)(Math.Cos(((double)i + rotate) * 6.2831853071795862 / (double)number) * radius) + center.Y
					};
				}
				result = array;
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}

		private static string ImageType(Image img)
		{
			if (img.RawFormat.Equals(ImageFormat.Bmp))
			{
				return "bmp";
			}
			if (img.RawFormat.Equals(ImageFormat.Gif))
			{
				return "gif";
			}
			if (img.RawFormat.Equals(ImageFormat.Jpeg))
			{
				return "jpeg";
			}
			if (img.RawFormat.Equals(ImageFormat.Png))
			{
				return "png";
			}
			if (img.RawFormat.Equals(ImageFormat.Exif))
			{
				return "exif";
			}
			if (img.RawFormat.Equals(ImageFormat.Tiff))
			{
				return "tiff";
			}
			if (img.RawFormat.Equals(ImageFormat.Icon))
			{
				return "icon";
			}
			if (img.RawFormat.Equals(ImageFormat.Emf))
			{
				return "emf";
			}
			if (img.RawFormat.Equals(ImageFormat.Wmf))
			{
				return "wmf";
			}
			if (img.RawFormat.Equals(ImageFormat.MemoryBmp))
			{
				return "memorybmp";
			}
			return "";
		}

		public Control[] GetAllControls(Control top)
		{
			ArrayList arrayList = new ArrayList();
			foreach (Control control in top.Controls)
			{
				arrayList.Add(control);
				arrayList.AddRange(this.GetAllControls(control));
			}
			return (Control[])arrayList.ToArray(typeof(Control));
		}

    //
    //関数をコピペしbmp1, bmp2を生成してる下に
    //Bitamap bmp3 = chain(bmp1, bmp2);
    //bmp3.Save(@"test3.png", System.Drawing.Imaging.ImageFormat.Png);
    //で横連結できると思います
    //http://detail.chiebukuro.yahoo.co.jp/qa/question_detail/q11111031706
    //
    public static Bitmap chain(Bitmap img1, Bitmap img2)
    {
      int width = img1.Width + img2.Width;
      int height = img1.Height > img2.Height ? img1.Height : img2.Height;
      Bitmap img3 = new Bitmap(width, height);
      Graphics g = Graphics.FromImage(img3);
      g.DrawImage(img1, new Point(0, 0));
      g.DrawImage(img2, new Point(img1.Width, 0));
      g.Dispose();
      return img3;
    }  
  
  
  }
}
