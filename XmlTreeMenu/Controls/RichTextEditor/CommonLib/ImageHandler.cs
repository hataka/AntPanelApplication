using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
// 追加 Time-stamp: <2011-01-24 9:29:57 kahata>
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
//using PluginCore.Helpers;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

// 画像を回転、反転する
// http://dobon.net/vb/dotnet/graphics/rotateflip.html
// 複数の画像をサイズ変更するには？ 
// http://www.atmarkit.co.jp/bbs/phpBB/viewtopic.php?forum=7&topic=27502
// サムネールイメージの作成
// http://dobon.net/vb/dotnet/graphics/thumbnail.html
// 画像をファイルに保存するには？
// http://www.atmarkit.co.jp/fdotnet/dotnettips/020savebmp/savebmp.html
// 画像を高品質に拡大／縮小するには？ resize.cs
// http://www.atmarkit.co.jp/fdotnet/dotnettips/023resize/resize.html
/*
using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

class ResizeBitmap {
  public static void Main() {

    string url = "http://www.atmarkit.co.jp/fdotnet/"
                   + "dotnettips/index/dotnettips_s.jpg";

    WebClient wc = new WebClient();
    Stream stream = wc.OpenRead(url);
    Bitmap src = new Bitmap(stream);
    stream.Close();

    int w = src.Width * 10;
    int h = src.Height * 10;

    Bitmap dest = new Bitmap(w, h);
    Graphics g = Graphics.FromImage(dest);

    foreach (InterpolationMode im
          in Enum.GetValues(typeof(InterpolationMode))) {
      if (im == InterpolationMode.Invalid)
        continue;
      g.InterpolationMode = im;
      g.DrawImage(src, 0, 0, w, h);
      dest.Save(im.ToString() + ".png", ImageFormat.Png);
    }
  }
}

// コンパイル方法：csc resize.cs
*/

namespace CommonLibrary
{
	public class ImageHander
	{
		/// <summary>
		/// Bitmapを中心座標と角度を指定して回転するサンプル(Form1.cs)
		/// http://homepage2.nifty.com/nonnon/SoftSample/CS.NET/SampleRotateBitmap.html
		/// ビットマップ(Bitmap)を回転する
		/// </summary>
		/// <param name="bmp">ビットマップ</param>
		/// <param name="angle">回転角度</param>
		/// <param name="x">中心点Ｘ</param>
		/// <param name="y">中心点Ｙ</param>
		/// <returns></returns>
		static public Bitmap RotateBitmap(Bitmap bmp, float angle, int x, int y)
		{
			Bitmap bmp2 = new Bitmap((int)bmp.Width, (int)bmp.Height);
			Graphics g = Graphics.FromImage(bmp2);
			g.Clear(Color.Black);

			g.TranslateTransform(-x, -y);
			g.RotateTransform(angle, System.Drawing.Drawing2D.MatrixOrder.Append);
			g.TranslateTransform(x, y, System.Drawing.Drawing2D.MatrixOrder.Append);

			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

			g.DrawImageUnscaled(bmp, 0, 0);
			g.Dispose();

			return bmp2;
		}


		static public Bitmap RotateBitmap(Bitmap bmp, float angle)
		{
			// dotnetの方法
			if (bmp != null)
			{
				//ラジアン単位に変換
				double d = angle / (180 / Math.PI);
				//新しい座標位置を計算する
				float x1 = bmp.Width * (float)Math.Cos(d);
				float y1 = bmp.Width * (float)Math.Sin(d);
				float x2 = bmp.Height * (float)Math.Sin(d);
				float y2 = bmp.Height * (float)Math.Cos(d);
				//画像を表示
				//DrawImage() メソッドで、座標の配列を表す destPoints パラメータを指定すると
				//指定した3つの頂点を元にした、平行四辺形でイメージを描画します
				//このシグネチャを使えば、極めて柔軟にイメージを表現することができます
				//http://wisdom.sakura.ne.jp/system/msnet/msnet_win13.html
				//destPoints に指定する配列は、3つの頂点を持たなければなりません
				//最初の頂点はイメージの左上角、次の頂点はイメージの右上角
				//そして、最後の頂点は左下角の座標をあらわします
				//これらの情報から、右下角の座標は推論することができるため、メソッドが計算してくれます

				//PointF配列を作成
				PointF[] destinationPoints = {new PointF(0, 0),
			              new PointF(x1, y1),
				            new PointF(x2, y2)};

				Bitmap bmp2 = new Bitmap((int)Math.Abs(x2-x1), (int)Math.Abs(y2-y1));
				Graphics g = Graphics.FromImage(bmp2);
				g.Clear(Color.Transparent);
				
				g.DrawImage(bmp, destinationPoints);
				//Graphicsオブジェクトを破棄
				g.Dispose();
				bmp.Dispose();
				return bmp2;
			}
			return bmp;
		}
		
		/// <summary>
		/// ビットマップ⇔アイコンは可逆変換が可能です。
		/// http://ou812.web.fc2.com/CsTips/GraphicIconBmp.html
		/// ビットマップに変換
		/// </summary>
		/// <param name="bitmap"></param>
		/// <returns></returns>
		static public Icon BitmapToIcon(Bitmap bitmap)
		{
			IntPtr handle = bitmap.GetHicon();
			return  Icon.FromHandle( handle );
		}

		/// <summary>
		/// ビットマップ⇔アイコンは可逆変換が可能です。
		/// http://ou812.web.fc2.com/CsTips/GraphicIconBmp.html
		/// アイコンに変換
		/// </summary>
		/// <param name="icon"></param>
		/// <returns></returns>
		static public Bitmap IconToBitmap(Icon icon)
		{
			return icon.ToBitmap();
		}

		/// <summary>
		/// Bitmap クラスで表される画像を、縦横比を維持したまま
		/// 任意の矩形内に収まるようにリサイズして返すメソッド。
		/// http://www.shise.net/wiki/wiki.cgi?page=C%23%2FClass%2FBitmap%A5%AF%A5%E9%A5%B9%A4%F2%A5%EA%A5%B5%A5%A4%A5%BA
		/// </summary>
		/// <param name="image"></param>
		/// <param name="dw"></param>
		/// <param name="dh"></param>
		/// <returns></returns>
		public static Bitmap ResizeImage(Bitmap image, double dw, double dh)
		{
			double hi;
			double imagew = image.Width;
			double imageh = image.Height;

			if ((dh / dw) <= (imageh / imagew))
			{
				hi = dh / imageh;
			}
			else
			{
				hi = dw / imagew;
			}
			int w = (int)(imagew * hi);
			int h = (int)(imageh * hi);

			Bitmap result = new Bitmap(w, h);
			Graphics g = Graphics.FromImage(result);
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(image, 0, 0, result.Width, result.Height);

			return result;
		}

		public ImageList ImageListCombine(ImageList imagelist1, ImageList imagelist2)
		{
			ImageList imagelist = new ImageList();
			for(int i =0; i< imagelist1.Images.Count; i++)
			{
				imagelist.Images.Add(imagelist1.Images[i]);
			}
			for (int i = 0; i < imagelist2.Images.Count; i++)
			{
				imagelist.Images.Add(imagelist2.Images[i]);
			}
			return imagelist;
		}

		// http://d.hatena.ne.jp/anis774/20091101/1257070643
		public static Icon getIconFromUrl(string url)
		{
			try
			{
				using (WebClient webClient = new WebClient())
				using (MemoryStream stream = new MemoryStream(webClient.DownloadData(url)))
				{
					return new Icon(stream);
				}
			}
			catch
			{
				return null;
			}
		}

		public static Icon getIconFromWebUrl(String url)
		{
			Uri objurl = new Uri(url);
			// create a hidden web browser, which will navigate to the page
			System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();
			web.ScrollBarsEnabled = false; // we don't want scrollbars on our image
			web.ScriptErrorsSuppressed = true; // don't let any errors shine through
			web.Navigate(url); // let's load up that page!

			// wait until the page is fully loaded
			while (web.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
				System.Windows.Forms.Application.DoEvents();
			System.Threading.Thread.Sleep(1500); // allow time for page scripts to update
			// the appearance of the page

			foreach (HtmlElement linkTag in web.Document.GetElementsByTagName("link"))
			{
				string relAttribute = linkTag.GetAttribute("rel");
				string iconUrl;

				if (relAttribute == "shortcut icon" || relAttribute == "icon")
				{
					iconUrl = linkTag.GetAttribute("href");

					if (iconUrl.StartsWith("http"))
					{        //完全なURLの場合 
						return getIconFromUrl(iconUrl);
					}
					else if (iconUrl.StartsWith("/"))
					{    //絶対パスの場合 
						return getIconFromUrl("http://" + objurl.Host + iconUrl);
					}
					else
					{                                //相対パスの場合 
						return getIconFromUrl(url + "/" + iconUrl);
					}
				}
			}
			//タグでの指定が無い場合
			return getIconFromUrl("http://" + objurl.Host + "/favicon.ico");
		}

		/// <summary>
		/// 正多角形の頂点座標
		/// </summary>
		/// <param name="number">頂点の数</param>
		/// <param name="center">中心座標</param>
		/// <param name="radius">半径</param>
		/// <param name="rotate">回転</param>
		/// <returns></returns>
		public static  Point[] GetPolygonVertex(int number, Point center, double radius, double rotate)
		{
			try
			{
				if (number <= 2)
					throw new ArgumentException();

				Point[] vertexes = new Point[number];
				for (int pos = 0; pos < number; pos++)
				{
					Point vertex = new Point();
					vertex.X = (int)(Math.Sin(((pos + rotate) * (2 * Math.PI)) / number) * radius) + center.X;
					vertex.Y = (int)(Math.Cos(((pos + rotate) * (2 * Math.PI)) / number) * radius) + center.Y;
					vertexes[pos] = vertex;
				}
				return vertexes;
			}
			catch (Exception)
			{
				throw;
			}
		}
	
		/// <summary>
		/// //イメージのファイル形式を調べる
		/// </summary>
		/// <param name="img"></param>
		/// <returns></returns>
		static String ImageType(Image img)
		{
			if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
			{
				return "bmp";// "ビットマップ (BMP) イメージ形式です";
			}
			else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
			{
				return "gif";// "GIF (Graphics Interchange Format) イメージ形式です";
			}
			else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
			{
				return "jpeg";// "JPEG (Joint Photographic Experts Group) イメージ形式です";
			}
			else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
			{
				return "png";// "W3C PNG (Portable Network Graphics) イメージ形式です";
			}
			else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Exif))
			{
				return "exif";//"Exif (Exchangeable Image File) 形式です";
			}
			else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
			{
				return "tiff";// "TIFF (Tagged Image File Format) イメージ形式です";
			}
			else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
			{
				return "icon";// "Windows アイコン イメージ形式です";
			}
			else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Emf))
			{
				return "emf";// "拡張メタファイル (EMF) イメージ形式です";
			}
			else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Wmf))
			{
				return "wmf"; // "Windows メタファイル (WMF) イメージ形式です";
			}
			else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
			{
				return "memorybmp";// "メモリ ビットマップ イメージ形式です";
			}		
			return "";
		}

		public Control[] GetAllControls(Control top)
		{
			ArrayList buf = new ArrayList();
			foreach (Control c in top.Controls)
			{
				buf.Add(c);
				buf.AddRange(GetAllControls(c));
			}
			return (Control[])buf.ToArray(typeof(Control));
		}
	}
}