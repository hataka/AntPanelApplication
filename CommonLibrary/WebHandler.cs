using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace AntPlugin.CommonLibrary
{
	public class WebHandler
	{
		private static Bitmap bmp;

		private WebClient downloadClient;

		public static string Path2Url(string path)
		{
      return Lib.Path2Url(path);
      //string machineName = Environment.MachineName;
			//string arg_0B_0 = Environment.UserName;
			//string text = path.Replace("F:\\", "http://" + machineName + "/f/");
			//text = text.Replace("C:\\Apach2.2\\htdoc\\", "http://" + machineName + "/");
			//return text.Replace("\\", "/");
		}

		public static string Url2Path(string url)
		{
			string machineName = Environment.MachineName;
			string arg_0B_0 = Environment.UserName;
			string text = url.Replace("http://localhost", "F:").Replace("/", "\\");
			text = text.Replace("http://hata2" + machineName, "F:\\");
			text = text.Replace("http://HATA2" + machineName, "F:\\");
			text = text.Replace("http://hata" + machineName, "C:\\APache2.2\\htdoc\\");
			return text.Replace("http://HATA" + machineName, "C:\\APache2.2\\htdoc\\");
		}

		public static bool IsWebSite(string path)
		{
			return Regex.IsMatch(path, "^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$");
		}

		public static string GetPathByID(string url)
		{
			WebBrowser webBrowser = new WebBrowser();
			webBrowser.ScrollBarsEnabled = false;
			webBrowser.ScriptErrorsSuppressed = true;
			webBrowser.Navigate(url);
			while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
			{
				Application.DoEvents();
			}
			HtmlWindow arg_36_0 = webBrowser.Document.Window;
			string text = "";
			if (webBrowser.Document.GetElementById("fileurl") != null)
			{
				text = webBrowser.Document.GetElementById("fileurl").InnerText;
				text = WebHandler.Url2Path(text);
			}
			webBrowser.Dispose();
			return text;
		}


    // urlにアクセスしてステータス・コードを返す
    // pagecheck.cs
    // http://www.atmarkit.co.jp/fdotnet/dotnettips/817httpstatus/httpstatus.html
    // http://www.atmarkit.co.jp/fdotnet/dotnettips/817httpstatus/httpstatus.html
    // https://teratail.com/questions/31623
    /*
    コード値	RFC 2616で定義された意味	HttpStatuCode列挙体の値
    100	Continue	Continue
    101	Switching Protocols	SwitchingProtocols
    200	OK	OK
    201	Created	Created
    202	Accepted	Accepted
    203	Non-Authoritative Information	NonAuthoritativeInformation
    204	No Content	NoContent
    205	Reset Content	ResetContent
    206	Partial Content	PartialContent
    300	Multiple Choices	MultipleChoices
    300	Ambiguous
    301	Moved Permanently	Moved
    301	MovedPermanently
    302	Found	Found
    302	Found	Redirect
    303	See Other	RedirectMethod
    303	SeeOther
    304	Not Modified	NotModified
    305	Use Proxy	UseProxy
    306	－	Unused
    307	Temporary Redirect	RedirectKeepVerb
    307	TemporaryRedirect
    400	Bad Request	BadRequest
    401	Unauthorized	Unauthorized
    402	Payment Required	PaymentRequired
    403	Forbidden	Forbidden
    404	Not Found	NotFound
    405	Method Not Allowed	MethodNotAllowed
    406	Not Acceptable	NotAcceptable
    407	Proxy Authentication Required	ProxyAuthenticationRequired
    408	Request Time-out	RequestTimeout
    409	Conflict	Conflict
    410	Gone	Gone
    411	Length Required	LengthRequired
    412	Precondition Failed	PreconditionFailed
    413	Request Entity Too Large	RequestEntityTooLarge
    414	Request-URI Too Large	RequestUriTooLong
    415	Unsupported Media Type	UnsupportedMediaType
    416	Requested range not satisfiable	RequestedRangeNotSatisfiable
    417	Expectation Failed	ExpectationFailed
    500	Internal Server Error	InternalServerError
    501	Not Implemented	NotImplemented
    502	Bad Gateway	BadGateway
    503	Service Unavailable	ServiceUnavailable
    504	Gateway Time-out	GatewayTimeout
    505	HTTP Version not supported	HttpVersionNotSuppor
    */
    public static HttpStatusCode GetStatusCode(string url)
    {

      HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
      HttpWebResponse res = null;
      HttpStatusCode statusCode;

      try
      {
        res = (HttpWebResponse)req.GetResponse();
        statusCode = res.StatusCode;

      }
      catch (WebException ex)
      {

        res = (HttpWebResponse)ex.Response;

        if (res != null)
        {
          statusCode = res.StatusCode;
        }
        else
        {
          throw; // サーバ接続不可などの場合は再スロー
        }
      }
      finally
      {
        if (res != null)
        {
          res.Close();
        }
      }
      return statusCode;
    }

    public static Boolean SiteExists(string url)
    {
      //string url = "http://www.atmarkit.co.jp/nosuchpage.html";
      WebRequest.DefaultWebProxy = null; // プロキシ未使用を明示

      HttpStatusCode statusCode = GetStatusCode(url);
      int code = (int)statusCode; // 列挙体の値を数値に変換
      if (code >= 400)
      { // 4xx、5xxはアクセス失敗とする
        // 出力：ページは存在しないようです：404
        //Console.WriteLine("ページは存在しないようです：" + code);
        return false;
      }
      else
      {
        //Console.WriteLine("ページは存在します：" + code);
        return true;
      }
    }

    /// <summary>
    /// csharp: Function that acts like PHP's file_get_contents
    /// </summary>
    /// https://gist.github.com/devfred/3980418
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string file_get_contents(string fileName)
    {

      string sContents = string.Empty;
      if (fileName.ToLower().IndexOf("http:") > -1 || fileName.ToLower().IndexOf("https:") > -1)
      {
        // URL 
        System.Net.WebClient wc = new System.Net.WebClient();
        byte[] response = wc.DownloadData(fileName);
        sContents = System.Text.Encoding.ASCII.GetString(response);
      }
      else
      {
        // Regular Filename 
        System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
        sContents = sr.ReadToEnd();
        sr.Close();
      }
      return sContents;
    }


    /// <summary>
    /// ファイルをダウンロードし保存する
    /// https://dobon.net/vb/dotnet/internet/downloadfile.html
    /// </summary>
    /// <param name="url"></param>
    /// <param name="fileName"></param>
    public static void DownLoadfile(String url,String fileName)
    {
      //ダウンロード基のURL
      Uri u = new Uri(url);
      System.Net.WebClient downloadClient = new System.Net.WebClient();
      //非同期ダウンロードを開始する
      downloadClient.DownloadFileAsync(u, fileName);
    }

    public static void ResetFrames(WebBrowser browser)
		{
			if (!(browser.Document == null))
			{
				HtmlWindow window = browser.Document.Window;
				foreach (HtmlWindow htmlWindow in window.Frames)
				{
					HtmlElement windowFrameElement = htmlWindow.WindowFrameElement;
					string attribute = windowFrameElement.GetAttribute("SRC");
					if (!attribute.Equals(htmlWindow.Url.ToString()))
					{
						htmlWindow.Navigate(new Uri(attribute));
					}
				}
			}
		}

		public static void PreviewPrintPage(WebBrowser browser)
		{
			PrintDocument printDocument = new PrintDocument();
			printDocument.PrintPage += new PrintPageEventHandler(WebHandler.pd_PrintPage);
			printDocument.DefaultPageSettings.Landscape = true;
			printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
			PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
			printPreviewDialog.Document = printDocument;
			printPreviewDialog.SetBounds(0, 0, 1024, 768);
			printPreviewDialog.Document.DocumentName = "サンプル";
			Rectangle rectangle = browser.RectangleToScreen(browser.Bounds);
			WebHandler.bmp = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(WebHandler.bmp);
			graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
			printPreviewDialog.ShowDialog();
		}

		private static void pd_PrintPage(object sender, PrintPageEventArgs e)
		{
			e.Graphics.DrawImage(WebHandler.bmp, new Point(0, 0));
		}

		public static void PrintPage()
		{
			PrintDialog printDialog = new PrintDialog();
			printDialog.PrinterSettings = new PrinterSettings();
			printDialog.PrinterSettings.PrintRange = PrintRange.SomePages;
			printDialog.PrinterSettings.MaximumPage = 32767;
			printDialog.PrinterSettings.MinimumPage = 1;
			printDialog.PrinterSettings.FromPage = 1;
			printDialog.PrinterSettings.ToPage = 32767;
			printDialog.PrintToFile = true;
			printDialog.AllowSelection = true;
			printDialog.AllowSomePages = true;
			printDialog.PrinterSettings.Copies = 8;
			printDialog.ShowHelp = true;
			if (printDialog.ShowDialog() == DialogResult.OK)
			{
				MessageBox.Show("ここに、印刷の処理を実装してください");
			}
			printDialog.Dispose();
		}

		public static Bitmap CaptureWebPage(string URL)
		{
			WebBrowser webBrowser = new WebBrowser();
			webBrowser.ScrollBarsEnabled = false;
			webBrowser.ScriptErrorsSuppressed = true;
			webBrowser.Navigate(URL);
			while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
			{
				Application.DoEvents();
			}
			Thread.Sleep(1500);
			int width = webBrowser.Document.Body.ScrollRectangle.Width;
			int height = webBrowser.Document.Body.ScrollRectangle.Height;
			webBrowser.Width = width;
			webBrowser.Height = height;
			Bitmap bitmap = new Bitmap(width, height);
			webBrowser.DrawToBitmap(bitmap, new Rectangle(0, 0, width, height));
			return bitmap;
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			string fileName = "C:\\test.gif";
			Uri address = new Uri("http://localhost/image.gif");
			if (this.downloadClient == null)
			{
				this.downloadClient = new WebClient();
				this.downloadClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.downloadClient_DownloadProgressChanged);
				this.downloadClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.downloadClient_DownloadFileCompleted);
			}
			this.downloadClient.DownloadFileAsync(address, fileName);
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			if (this.downloadClient != null)
			{
				this.downloadClient.CancelAsync();
			}
		}

		private void downloadClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			Console.WriteLine("{0}% ({1}byte 中 {2}byte) ダウンロードが終了しました。", e.ProgressPercentage, e.TotalBytesToReceive, e.BytesReceived);
		}

		private void downloadClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				Console.WriteLine("エラー:{0}", e.Error.Message);
				return;
			}
			if (e.Cancelled)
			{
				Console.WriteLine("キャンセルされました。");
				return;
			}
			Console.WriteLine("ダウンロードが完了しました。");
		}

		public static List<string> ExtractUrlFromText(string text)
		{
			List<string> list = new List<string>();
			Regex regex = new Regex("https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			Match match = regex.Match(text);
			while (match.Success)
			{
				list.Add(match.Value);
				match = match.NextMatch();
			}
			return list;
		}

		[DllImport("KERNEL32.DLL")]
		public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

		public static string WebClientGet3(string url)
		{
			WebClient webClient = new WebClient();
			byte[] bytes = webClient.DownloadData(url);
			Encoding encoding = Encoding.GetEncoding("Shift_JIS");
			return encoding.GetString(bytes);
		}

		public static string WebClientGet3(string url, string encoding)
		{
			WebClient webClient = new WebClient();
			byte[] bytes = webClient.DownloadData(url);
			Encoding encoding2 = Encoding.GetEncoding(encoding);
			return encoding2.GetString(bytes);
		}

		public static string StripTags(string html)
		{
			html = Regex.Replace(html, "<!--[\\s\\S]*?-->", "");
			return Regex.Replace(html, "<\\/?([a-z][a-z0-9]*)\\b[^>]*>", "");
		}

		public static string AbsoluteURLFromLink(string url, string link)
		{
			Uri uri = new Uri(new Uri(url), link);
			if (link == string.Empty || url == string.Empty)
			{
				return "";
			}
			return uri.AbsoluteUri;
		}

		public static string AbsolutePath(string fullurl)
		{
			Uri uri = new Uri(fullurl);
			return uri.AbsolutePath;
		}

		public static bool IsLocalhost(string url)
		{
			Uri uri = new Uri(url);
			return uri.IsLoopback;
		}

		public static bool IsAbsoluteUri(string url)
		{
			Uri uri = new Uri(url);
			return uri.IsAbsoluteUri;
		}
	}
}
