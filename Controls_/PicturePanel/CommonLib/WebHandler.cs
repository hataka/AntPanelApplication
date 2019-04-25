using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing.Printing;
namespace CommonLibrary
{
	public class WebHandler
	{
		//////////////////////////////////////////////////////////////////////////
		// Path Url 操作関数
		public static String Path2Url(String path)
		{
			return path.Replace("F:\\", "http://localhost/").Replace("\\", "/");
		}

		public static String Url2Path(String url)
		{
			return url.Replace("http://localhost", "F:").Replace("/", "\\");
		}

		public static Boolean IsWebSite(String path)
		{
			//URLっぽいか調べる
			//http://dobon.net/vb/dotnet/string/regexismatch.html
			if (System.Text.RegularExpressions.Regex.IsMatch(
				path, @"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$"))
			{
				return true;
			}
			else return false;
		}

		//////////////////////////////////////////////////////////////////////////
		// Web Document 操作 処理関数
		/// <summary>
		/// id = fileurl の filePathを取得する
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static String GetPathByID(String url)
		{
			// create a hidden web browser, which will navigate to the page
			System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();
			web.ScrollBarsEnabled = false; // we don't want scrollbars on our image
			web.ScriptErrorsSuppressed = true; // don't let any errors shine through
			web.Navigate(url); // let's load up that page!

			// wait until the page is fully loaded
			while (web.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
				System.Windows.Forms.Application.DoEvents();
			//System.Threading.Thread.Sleep(1500); // allow time for page scripts to update
			// the appearance of the page

			HtmlWindow currentWindow = web.Document.Window;
			String fileurl = "";
			if (web.Document.GetElementById("fileurl") != null)
			{
				fileurl = web.Document.GetElementById("fileurl").InnerText;
				fileurl = Url2Path(fileurl);
			}
			web.Dispose();
			return fileurl;
		}

		/// <summary>
		/// http://msdn.microsoft.com/en-us/library/system.windows.forms.htmlwindow.windowframeelement(VS.80).aspx
		/// The following code example compares the SRC attribute of frames in a FRAMESET 
		/// to the current location. 
		/// If they are different, the frames are reset to their original URLs.
		///
		/// http://social.msdn.microsoft.com/Forums/ja-JP/vsgeneralja/thread/41e23caf-05d6-4dff-b5a1-9b1ecb12b4ed/
		/// WebBrowserコントロールからフレームを利用したのページのHTMLソース取得方法
		/// Frames（HtmlWindowCollection）には Document プロパティなんて存在していませんが。
		///			foreach (HtmlWindow window in webBrowser1.Document.Window) {
		///							Debug.WriteLine(window.Document.Title); 
		///			}
		/// ではちゃんととれてますね。
		/// ところで、メモリ上で処理することばかり考えていたので IPersistStreamInit を挙げましたが、
		/// ファイルに保存するだけなら System.Runtime.InteropServices.ComTypes.IPersistFile を使えば
		/// 簡単に実現できます。
		/// HtmlDocument の DomDocument を IPersistFile にキャストして Save メソッドを使うだけ
		/// </summary>
		/// <param name="browser"></param>
		public static void ResetFrames(WebBrowser browser)
		{
			if (!(browser.Document == null))
			{
				HtmlElement frameElement = null;
				HtmlWindow docWindow = browser.Document.Window;

				foreach (HtmlWindow frameWindow in docWindow.Frames)
				{
					frameElement = frameWindow.WindowFrameElement;
					String originalUrl = frameElement.GetAttribute("SRC");

					if (!originalUrl.Equals(frameWindow.Url.ToString()))
					{
						frameWindow.Navigate(new Uri(originalUrl));
					}
				}
			}
		}

		/// <summary>
		/// http://www.slotware.net/blog/2009/03/cwebbrowser.html
		/// WebBrowserコントロール上のテキストが選択されているかいないかを判別するのは、
		/// ちょっとやっかいで、標準のDocumentプロパティだけではできません。
		/// 代わりにmshtml.IHTMLDocument2なるものを使う必要があります。
		/// この時「Microsoft.mshtml」を参照に追加するのを忘れないでください。
		/// さて実際に、選択範囲のテキストを取得するには
		/// mshtml.IHTMLDocument2 doc = (mshtml.IHTMLDocument2)this.webBrowser1.Document.DomDocument;
		/// で、DomDocumentオブジェクトを取得してから、
		/// mshtml.IHTMLTxtRange range = (mshtml.IHTMLTxtRange)doc.selection.createRange();
		/// string selection = range.text;
		/// </summary>
		/// <param name="browser"></param>
		/// <returns>選択範囲の文字列</returns>
		/*
    public static String GetSelection(WebBrowser browser)
		{
			mshtml.IHTMLDocument2 doc = (mshtml.IHTMLDocument2)browser.Document.DomDocument;
			mshtml.IHTMLTxtRange range = (mshtml.IHTMLTxtRange)doc.selection.createRange();
			string selection = range.text;
			return selection;
		}
     */ 
		
		/// <summary>
		/// WebページをMHT形式（.mhtファイル）で保存する
		/// http://www.atmarkit.co.jp/fdotnet/dotnettips/690createmht/createmht.html
		/// </summary>
		/// <param name="url">保存するページのURL</param>
		/// <param name="filepath">保存先のファイルのパス名</param>
		/// <returns>true:成功 false:失敗</returns>
		/*
    public static bool CreateMHT(string url, string filepath)
		{
			CDO.MessageClass msg = new CDO.MessageClass();
			try
			{
				// CDO.CdoMHTMLFlags.cdoSuppressNoneは
				// ページ内で参照しているすべてのリソースをダウンロード
				msg.CreateMHTMLBody(
					url, // 保存するページのURL
					CDO.CdoMHTMLFlags.cdoSuppressNone,
					"",  // 認証が必要な場合のユーザー名
					""); // およびパスワード
			}
			catch (Exception exc)
			{
				String s = exc.Message.ToString();
				//System.Console.WriteLine("ページ取得失敗");
				MessageBox.Show(Lib.OutputError(s), "ページ取得失敗");
				return false;
			}
			ADODB.Stream st = msg.GetStream();
			st.SaveToFile(
				filepath, // 保存先のファイルのパス名
				ADODB.SaveOptionsEnum.adSaveCreateOverWrite);
			st.Close();
			return true;
		}
    */
		/// <summary>
		/// http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/ed830c39-563f-4c79-a023-28104179695e/
		/// 
		///		you can try this to save web pages:
		///		1. Add COM in Reference
		///				this is the Microsoft CDO for Windows Library (C:\WINDOWS\System32\cdosys.dll)
		///		2. code：
		///			CDO.Message msg = new CDO.MessageClass();
		///			CDO.Configuration cfg = new CDO.ConfigurationClass();		
		///			msg.Configuration = cfg;
		///			msg.CreateMHTMLBody("http://www.google.com", CDO.CdoMHTMLFlags.cdoSuppressAll, "", "");
		///			msg.GetStream().SaveToFile("c:\\text.mht", ADODB.SaveOptionsEnum.adSaveCreateOverWrite);
		/// </summary>
		public static void SaveWebPageAsMHT()
		{
			//CDO.Message msg = new CDO.MessageClass();
			//CDO.Configuration cfg = new CDO.ConfigurationClass();
			//msg.Configuration = cfg;
			//msg.CreateMHTMLBody("http://www.google.com", CDO.CdoMHTMLFlags.cdoSuppressAll, "", "");
			//msg.GetStream().SaveToFile(@"F:\textgoogle.mht", ADODB.SaveOptionsEnum.adSaveCreateOverWrite);
		}		
		
		/// <summary>
		/// http://www.atmarkit.co.jp/bbs/phpBB/viewtopic.php?topic=43520&forum=7
		/// </summary>
		private static Bitmap bmp;  // 印刷イベントに渡すブラウザイメージ
		public static  void PreviewPrintPage(WebBrowser browser)
		{
			System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
			pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(pd_PrintPage);
			pd.DefaultPageSettings.Landscape = true; // 横向き 
			pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);// 用紙サイズ
			PrintPreviewDialog ppd = new PrintPreviewDialog();
			ppd.Document = pd;
			ppd.SetBounds(0, 0, 1024, 768); // プレビュー画面サイズ 
			ppd.Document.DocumentName = "サンプル";

			// 追加開始
			//Rectangle rect = this.RectangleToScreen(browser.Bounds);
			Rectangle rect = browser.RectangleToScreen(browser.Bounds);
			bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(bmp);
			g.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
			// 追加終了

			//印刷プレビューダイアログを表示する 
			ppd.ShowDialog();

		}
		private static void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			e.Graphics.DrawImage(bmp, new Point(0, 0));
		}
	
		
		/// <summary>
		/// http://jeanne.wankuma.com/tips/csharp/dialog/print.html
		/// </summary>
		public static void PrintPage()
		{
			// PrintDialog の新しいインスタンスを生成する (デザイナから追加している場合は必要ない)
			PrintDialog printDialog1 = new PrintDialog();
			// PrinterSettings の新しいインスタンスを生成する (必須)
			printDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
			// プリンタ名を指定する
			//printDialog1.PrinterSettings.PrinterName = "PrinterName";
			// 印刷範囲を「ページ指定」にする
			printDialog1.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.SomePages;
			// 印刷範囲で指定可能な最大ページ数を設定する
			printDialog1.PrinterSettings.MaximumPage = short.MaxValue;
			// 印刷範囲で指定可能な最小ページ数を設定する
			printDialog1.PrinterSettings.MinimumPage = 1;
			// 印刷開始ページを設定する
			printDialog1.PrinterSettings.FromPage = 1;
			// 印刷終了ページを設定する
			printDialog1.PrinterSettings.ToPage = short.MaxValue;
			// [ファイルへ出力] チェックボックスを有効にする (初期値 true)
			//printDialog1.AllowPrintToFile = true;
			// [ファイルへ出力] チェックボックスをオンにする (初期値 false)
			printDialog1.PrintToFile = true;
			// 印刷範囲のページ指定を有効にする (初期値 false)
			printDialog1.AllowSelection = true;
			// 印刷範囲のページ指定を有効にする (初期値 false)
			printDialog1.AllowSomePages = true;
			// [部単位で印刷] チェックボックスをオンにする (初期値 true)
			//printDialog1.PrinterSettings.Collate = true;
			// 部数を設定する
			printDialog1.PrinterSettings.Copies = 8;
			// [ヘルプ] ボタンを表示する (初期値 false)
			printDialog1.ShowHelp = true;
			// [ネットワーク] ボタンを表示する (初期値 true)
			//printDialog1.ShowNetwork = true;
			// ダイアログを表示し、戻り値が [OK] の場合は印刷の処理を実行する
			if (printDialog1.ShowDialog() == DialogResult.OK)
			{
				MessageBox.Show("ここに、印刷の処理を実装してください");
				//this.currentWebBrowser().Print();
			}
			// 不要になった時点で破棄する (正しくは オブジェクトの破棄を保証する を参照)
			printDialog1.Dispose();
		}
		
		/// <summary>
		/// A method to capture a webpage as a System.Drawing.Bitmap
		/// </summary>
		/// <param name="URL">The URL of the webpage to capture</param>
		/// <returns>A System.Drawing.Bitmap of the entire page</returns>
		public static System.Drawing.Bitmap CaptureWebPage(string URL)
		{
			// create a hidden web browser, which will navigate to the page
			System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();
			web.ScrollBarsEnabled = false; // we don't want scrollbars on our image
			web.ScriptErrorsSuppressed = true; // don't let any errors shine through
			web.Navigate(URL); // let's load up that page!

			// wait until the page is fully loaded
			while (web.ReadyState != System.Windows.Forms.WebBrowserReadyState.Complete)
				System.Windows.Forms.Application.DoEvents();
			System.Threading.Thread.Sleep(1500); // allow time for page scripts to update
			// the appearance of the page

			// set the size of our web browser to be the same size as the page
			int width = web.Document.Body.ScrollRectangle.Width;
			int height = web.Document.Body.ScrollRectangle.Height;
			web.Width = width;
			web.Height = height;
			// a bitmap that we will draw to
			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height);
			// draw the web browser to the bitmap
			web.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, width, height));
			return bmp; // return the bitmap for processing
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		// TODO: コード調整 
		//[C#]
		System.Net.WebClient downloadClient = null;
		//Button1のClickイベントハンドラ
		private void Button1_Click(object sender, EventArgs e)
		{
			//Button1.Enabled = false;
			//Button2.Enabled = true;

			//ダウンロードしたファイルの保存先
			string fileName = "C:\\test.gif";
			//ダウンロード基のURL
			Uri u = new Uri("http://localhost/image.gif");

			//WebClientの作成
			if (downloadClient == null)
			{
				downloadClient = new System.Net.WebClient();
				//イベントハンドラの作成
				downloadClient.DownloadProgressChanged +=
						new System.Net.DownloadProgressChangedEventHandler(
								downloadClient_DownloadProgressChanged);
				downloadClient.DownloadFileCompleted +=
						new System.ComponentModel.AsyncCompletedEventHandler(
								downloadClient_DownloadFileCompleted);
			}
			//非同期ダウンロードを開始する
			downloadClient.DownloadFileAsync(u, fileName);
		}

		//Button2のClickイベントハンドラ
		private void Button2_Click(object sender, EventArgs e)
		{
			//非同期ダウンロードをキャンセルする
			if (downloadClient != null)
				downloadClient.CancelAsync();
		}

		private void downloadClient_DownloadProgressChanged(object sender,
			System.Net.DownloadProgressChangedEventArgs e)
		{
			Console.WriteLine("{0}% ({1}byte 中 {2}byte) ダウンロードが終了しました。",
					e.ProgressPercentage, e.TotalBytesToReceive, e.BytesReceived);
		}

		private void downloadClient_DownloadFileCompleted(object sender,
			System.ComponentModel.AsyncCompletedEventArgs e)
		{
			if (e.Error != null)
				Console.WriteLine("エラー:{0}", e.Error.Message);
			else if (e.Cancelled)
				Console.WriteLine("キャンセルされました。");
			else
				Console.WriteLine("ダウンロードが完了しました。");

			//Button1.Enabled = true;
			//Button2.Enabled = false;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////

		
		public static List<String> ExtractUrlFromText(String text)
		{
			List<String> url = new List<string>();
			//Regexオブジェクトを作成
			System.Text.RegularExpressions.Regex r =
				new System.Text.RegularExpressions.Regex(
					@"https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$",
					System.Text.RegularExpressions.RegexOptions.IgnoreCase
					| System.Text.RegularExpressions.RegexOptions.Multiline
					);
			//TextBox1.Text内で正規表現と一致する対象を1つ検索
			System.Text.RegularExpressions.Match m = r.Match(text);
			//次のように一致する対象をすべて検索することもできる
			//System.Text.RegularExpressions.MatchCollection mc = r.Matches(TextBox1.Text);
			while (m.Success)
			{
				//一致した対象が見つかったときキャプチャした部分文字列を表示
				//Console.WriteLine(m.Value);
				url.Add(m.Value);
				//次に一致する対象を検索
				m = m.NextMatch();
			}
			return url;
		}

		// http://www.atmarkit.co.jp/fdotnet/dotnettips/039inifile/inifile.html
		[DllImport("KERNEL32.DLL")]
		public static extern uint
			GetPrivateProfileString(string lpAppName,
			string lpKeyName, string lpDefault,
			StringBuilder lpReturnedString, uint nSize,
			string lpFileName);
		/*
		[DllImport("KERNEL32.DLL",EntryPoint="GetPrivateProfileStringA")]
		public static extern uint 
			GetPrivateProfileStringByByteArray(string lpAppName, 
			string lpKeyName, string lpDefault, 
			byte [] lpReturnedString, uint nSize, 
			string lpFileName);

		[DllImport("KERNEL32.DLL")]
		public static extern uint 
			GetPrivateProfileInt( string lpAppName, 
			string lpKeyName, int nDefault, string lpFileName );

		[DllImport("KERNEL32.DLL")]
			public static extern uint WritePrivateProfileString(
			string lpAppName,
			string lpKeyName,
			string lpString,
			string lpFileName);
		*/
		// http://uchukamen.com/Programming/iniFile/#SEC5
		//uint entryLength;
		//string strEntryStringValue;
		//System.Text.StringBuilder strEntryString = new System.Text.StringBuilder( 256 );
		//entryLength = GetPrivateProfileString( "SECTION", "ENTRY", "Nothing", strEntryString, (uint)(strEntryString.Capacity), "C:\\TEMP\\TEST.ini" );
		//strEntryStringValue = strEntryString.ToString();
		// この例では、C:\TEMP\Test.ini 
		// ファイルのセクション名 "SECTION" に、キーネーム"ENTRY" に値 "Hello" が設定されます。
		//[DEFAULT]
		//BASEURL=http://msdn.microsoft.com/en-us/library/ms533049.aspx
		//[InternetShortcut]
		//URL=http://msdn.microsoft.com/en-us/library/ms533049.aspx
		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static String GetUrlFromInternetShortcut(String path)
		{
			uint entryLength;
			string strEntryStringValue;

			System.Text.StringBuilder strEntryString = new System.Text.StringBuilder(256);
			entryLength = GetPrivateProfileString(
				"InternetShortcut", "URL", "Nothing", strEntryString,
				(uint)(strEntryString.Capacity), path);
			strEntryStringValue = strEntryString.ToString();
			return strEntryStringValue;
		}

		public static String WebClientGet3(String url)
		{
			WebClient wc = new WebClient();
			//byte[] data = wc.DownloadData("http://www.google.co.jp/");
			byte[] data = wc.DownloadData(url);
			Encoding enc = Encoding.GetEncoding("Shift_JIS");
			string html = enc.GetString(data);
			//Console.WriteLine(html);
			return html;
		}

		public static String WebClientGet3(String url, String encoding)
		{
			WebClient wc = new WebClient();
			//byte[] data = wc.DownloadData("http://www.google.co.jp/");
			byte[] data = wc.DownloadData(url);
			Encoding enc = Encoding.GetEncoding(encoding);
			string html = enc.GetString(data);
			//Console.WriteLine(html);
			return html;
		}


	
		
		public static String StripTags(String html)
		{
			//[C#]
			// commentsAndPhpTags = /<!--[\s\S]*?-->|<\?(?:php)?[\s\S]*?\?>/gi;
			//URLにリンクを付ける
			html = System.Text.RegularExpressions.Regex.Replace(
					html, @"<!--[\s\S]*?-->", "");
			return System.Text.RegularExpressions.Regex.Replace(
					html, @"<\/?([a-z][a-z0-9]*)\b[^>]*>", "");
		}
		
		/// <summary>
		/// http://dobon.net/vb/dotnet/internet/analyzeurl.html
		/// 
		/// </summary>
		/// <param name="link"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		public static String AbsoluteURLFromLink(String url, String link)
		{
			Uri uri = new Uri(new Uri(url), link);
			if (link == String.Empty || url == String.Empty) return "";
			//絶対URIを表示する
			return uri.AbsoluteUri; //public string AbsoluteUri
		}

		/// <summary>
		/// 絶対パス 
		/// string url = "http://user:pass@www.dobon.net:80/vb/bbs.cgi?id=a%20b&n=1#top";
		/// 結果: /vb/bbs.cgi
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static String AbsolutePath(String fullurl)
		{
			Uri uri = new Uri(fullurl);
			return uri.AbsolutePath; 
		}
		/// <summary>
		/// ローカルホストを参照するかどうか
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static Boolean IsLocalhost(String url)
		{
			Uri uri = new Uri(url);
			return uri.IsLoopback; 
		}

		/// <summary>
		/// 絶対インスタンスであるかどうか
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static Boolean IsAbsoluteUri(String url)
		{
			Uri uri = new Uri(url);
			return uri.IsAbsoluteUri;
		}


	}
}
//http://rucio.cloudapp.net/ThreadDetail.aspx?ThreadId=10188
/*
[C#]
//解析するURL（このURLは実在しません）
string url = "http://user:pass@www.dobon.net:80/vb/bbs.cgi?id=a%20b&n=1#top";

//絶対URI
Console.WriteLine(u.AbsoluteUri);
//結果: http://user:pass@www.dobon.net/vb/bbs.cgi?id=a%20b&n=1#top

//サーバーのDNS(Domain Name System)ホスト名またはIPアドレスと、ポート番号
Console.WriteLine(u.Authority);
//結果: www.dobon.net

//DNSの解決に安全に使用できるエスケープ解除されたホスト名
Console.WriteLine(u.DnsSafeHost);
//結果: www.dobon.net

//エスケープフラグメント
Console.WriteLine(u.Fragment);
//結果: #top

//サーバーのDNSホスト名またはIPアドレス
Console.WriteLine(u.Host);
//結果: www.dobon.net

//ホスト名の型
switch (u.HostNameType)
{
    case UriHostNameType.Basic:
        Console.WriteLine(
            "ホストは設定されましたが、型を決定できません。");
        break;
    case UriHostNameType.Dns:
        Console.WriteLine(
            "ホスト名は、ドメイン名システム形式のホスト名です。");
        break;
    case UriHostNameType.IPv4:
        Console.WriteLine(
            "ホスト名は、IP Version 4 形式のホストアドレスです。");
        break;
    case UriHostNameType.IPv6:
        Console.WriteLine(
            "ホスト名は、IP Version 6 形式のホスト アドレスです。");
        break;
    case UriHostNameType.Unknown:
        Console.WriteLine("ホスト名の型が指定されていません。");
        break;
}
//結果: ホスト名は、ドメイン名システム形式のホスト名です。

//絶対インスタンスであるかどうか
Console.WriteLine(u.IsAbsoluteUri);
//結果: True

//ポート値がこのスキームの既定のポート値かどうか
Console.WriteLine(u.IsDefaultPort);
//結果: True

//ファイルURIかどうか
Console.WriteLine(u.IsFile);
//結果: False

//ローカルホストを参照するかどうか
Console.WriteLine(u.IsLoopback);
//結果: False

//UNCパスかどうか
Console.WriteLine(u.IsUnc);
//結果: False

//Uriコンストラクタに渡された元のURI文字列
Console.WriteLine(u.OriginalString);
//結果: http://user:pass@www.dobon.net:80/vb/bbs.cgi?id=a%20b&n=1#top

//ローカルオペレーティングシステムでのファイル名表現
Console.WriteLine(u.LocalPath);
//結果: /vb/bbs.cgi

//AbsolutePathプロパティとQueryプロパティを疑問符(?)で区切った形式
Console.WriteLine(u.PathAndQuery);
//結果: /vb/bbs.cgi?id=a%20b&n=1

//ポート番号
Console.WriteLine(u.Port);
//結果: 80

//クエリ情報
Console.WriteLine(u.Query);
//結果: ?id=a%20b&n=1

//スキーム名
Console.WriteLine(u.Scheme);
//結果: http

//セグメント
foreach (string s in u.Segments)
    Console.WriteLine("\t" + s);
//結果: 
//    /
//    vb/
//    bbs.cgi

//Uriインスタンスの作成前に、URI文字列がエスケープされているか
Console.WriteLine(u.UserEscaped);
//結果: False

//ユーザー名、パスワードなどのユーザー固有の情報
Console.WriteLine(u.UserInfo);
//結果: user:pass

//左端からスキームまで
Console.WriteLine(u.GetLeftPart(UriPartial.Scheme));
//結果: http://

//左端から権限まで
Console.WriteLine(u.GetLeftPart(UriPartial.Authority));
//結果: http://user:pass@www.dobon.net

//左端からパスまで
Console.WriteLine(u.GetLeftPart(UriPartial.Path));
//結果: http://user:pass@www.dobon.net/vb/bbs.cgi

//左端からクエリまで
Console.WriteLine(u.GetLeftPart(UriPartial.Query));
//結果: http://user:pass@www.dobon.net/vb/bbs.cgi?id=a%20b&n=1

//エスケープ解除された正規形式のURI 
Console.WriteLine(u.ToString());
//結果: http://user:pass@www.dobon.net/vb/bbs.cgi?id=a b&n=1#top
*/