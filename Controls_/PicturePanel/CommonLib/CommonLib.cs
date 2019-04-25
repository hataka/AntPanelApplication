using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
// 追加 Time-stamp: <2011-01-24 9:29:57 kahata>
using System.Threading;
using System.Windows.Forms;

namespace CommonLibrary
//namespace MDIForm
{
	
	public class Lib : IDisposable
	{

		#region Fields
	public const string OpenFileFilter =
		"All files(*.*)|*.*"
		+ "|Supported files|*.txt;*.log;*.ini;*.inf;*.tex;*.htm;*.html;*.css;*.js;*.xml;*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx;*.cs;*.java;*.py;*.rb;*.pl;*.vbs;*.bat"
		+ "|" + CommonFileFilter;

	public const string SaveFileFilter =
		"All files(*.*)|*.*"
		+ "|" + CommonFileFilter;

	public const string CommonFileFilter =
		"Text file(*.txt, *.log, *.tex, ...)|*.txt;*.log;*.ini;*.inf;*.tex"
		+ "|HTML file(*.htm, *.html)|*.htm;*.html"
		+ "|CSS file(*.css)|*.css"
		+ "|Javascript file(*.js)|*.js"
		+ "|XML file(*.xml)|*.xml"
		+ "|C/C++ source(*.c, *.h, ...)|*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx"
		+ "|C# source(*.cs)|*.cs"
		+ "|Java source(*.java)|*.java"
		+ "|Python script(*.py)|*.py"
		+ "|Ruby script(*.rb)|*.rb"
		+ "|Perl script(*.pl)|*.pl"
		+ "|VB script(*.vbs)|*.vbs"
		+ "|Batch file(*.bat)|*.bat";

	public const string MSG_BOX_STRING = "ファイルは保存されていません。\n\n"
		+ "編集中のテキストは破棄されます。\n\nよろしいですか?";

	public static ArrayList textfile =
			new ArrayList(new string[]{ ".txt",".rtf", ".c", ".cpp", ".cs", ".java", ".php",".vbs",".js",".wsf",".hta"
				,".html", ".htm",".tcl"	,".pl", ".tex", ".as",".xml",".mxml",".ns",".ini",".sql"
			});
	
	public static ArrayList executablefile = 
		new ArrayList(new string[]{ ".exe", ".lnk",  ".doc", ".xls",  ".ttf",  ".ppt"
			, ".ppr"
			, ".pptx", ".docx", ".xlsx", ".pdf", ".zip" });
	//".fla", ".pps", ".psd", ".png", ".jpg", ".gif",".docproj",".otf",  ".mp3",, ".ai", ".rar"
	public static ArrayList imagefile = new ArrayList(new string[]{ ".jpg", ".bmp", ".png", ".gif", ".ico", ".jpeg" }) ;
	public static ArrayList soundfile = new ArrayList(new string[]{ ".asf", ".wax", ".asx", ".mp3",".wav", ".wma" }) ;
	public static ArrayList videofile = new ArrayList(new string[]{ ".wmv", ".asx", ".mpg", ".m2p", ".asf" }) ;
	public static ArrayList databasefile = new ArrayList(new string[]{ ".mdf", ".mdb" }) ;

	public static Form form;
	public static Process extProcess = null;
	public static Thread extThread = null;

	#endregion

		public Lib()
		{
		}
/*
		public Lib(MDIForm.MDIParent1 _form)
		{
			form = _form;
		}
*/
		~Lib()
		{
			Dispose();
		}

		public void Dispose()
		{
		}

		public static Form MainForm
		{
			get { return form; }
			set {	form = value; }
		}
	
		public static Control[] GetAllControls(Control top)
		{
			ArrayList buf = new ArrayList();
			foreach (Control c in top.Controls)
			{
				buf.Add(c);
				buf.AddRange(GetAllControls(c));
			}
			return (Control[])buf.ToArray(typeof(Control));
		}

		public static String File_OpenDialog(String FileName, String InitialDirectory, String Filter)
		{
			// http://dobon.net/vb/dotnet/form/openfiledialog.html
			//OpenFileDialogクラスのインスタンスを作成
			OpenFileDialog ofd = new OpenFileDialog();

			//はじめのファイル名を指定する
			//はじめに「ファイル名」で表示される文字列を指定する
			ofd.FileName = FileName;
			//はじめに表示されるフォルダを指定する
			//指定しない（空の文字列）の時は、現在のディレクトリが表示される
			ofd.InitialDirectory = InitialDirectory;
			//[ファイルの種類]に表示される選択肢を指定する
			//指定しないとすべてのファイルが表示される
			ofd.Filter = Filter;
			//	"XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
			//[ファイルの種類]ではじめに
			//「すべてのファイル」が選択されているようにする
			ofd.FilterIndex = 0;
			//タイトルを設定する
			ofd.Title = "開くファイルを選択してください";
			//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
			ofd.RestoreDirectory = true;
			//存在しないファイルの名前が指定されたとき警告を表示する
			//デフォルトでTrueなので指定する必要はない
			ofd.CheckFileExists = true;
			//存在しないパスが指定されたとき警告を表示する
			//デフォルトでTrueなので指定する必要はない
			ofd.CheckPathExists = true;

			//ダイアログを表示する
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				if (File.Exists(ofd.FileName))
				{
					return ofd.FileName;
				}
				else
				{
					return "";
				}
			}
			else return "";
		}

		public static String File_OpenDialogNoCheckFileExists(String FileName, String InitialDirectory, String Filter)
		{
			// http://dobon.net/vb/dotnet/form/openfiledialog.html
			//OpenFileDialogクラスのインスタンスを作成
			OpenFileDialog ofd = new OpenFileDialog();

			//はじめのファイル名を指定する
			//はじめに「ファイル名」で表示される文字列を指定する
			ofd.FileName = FileName;
			//はじめに表示されるフォルダを指定する
			//指定しない（空の文字列）の時は、現在のディレクトリが表示される
			ofd.InitialDirectory = InitialDirectory;
			//[ファイルの種類]に表示される選択肢を指定する
			//指定しないとすべてのファイルが表示される
			ofd.Filter = Filter;
			//	"XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
			//[ファイルの種類]ではじめに
			//「すべてのファイル」が選択されているようにする
			ofd.FilterIndex = 0;
			//タイトルを設定する
			ofd.Title = "開くファイルを選択してください";
			//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
			ofd.RestoreDirectory = true;
			//存在しないファイルの名前が指定されたとき警告を表示する
			//デフォルトでTrueなので指定する必要はない
			ofd.CheckFileExists = false;// true;
			//存在しないパスが指定されたとき警告を表示する
			//デフォルトでTrueなので指定する必要はない
			ofd.CheckPathExists = true;

			//ダイアログを表示する
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				return ofd.FileName;
			}
			else return "";
		}
		
		public static String File_SaveDialog(String FileName, String InitialDirectory, String Filter)
		{
			//[C#] http://dobon.net/vb/dotnet/form/savefiledialog.html
			//SaveFileDialogクラスのインスタンスを作成
			String savefilepath;
			SaveFileDialog sfd = new SaveFileDialog();

			//はじめのファイル名を指定する
			sfd.FileName = FileName;
			//はじめに表示されるフォルダを指定する
			//sfd.InitialDirectory = @"C:\";
			sfd.InitialDirectory = InitialDirectory;
			//[ファイルの種類]に表示される選択肢を指定する
			sfd.Filter = Filter;
			//[ファイルの種類]ではじめに
			//「すべてのファイル」が選択されているようにする
			sfd.FilterIndex = 2;
			//タイトルを設定する
			sfd.Title = "保存先のファイルを選択してください";
			//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
			sfd.RestoreDirectory = true;
			//既に存在するファイル名を指定したとき警告する
			//デフォルトでTrueなので指定する必要はない
			sfd.OverwritePrompt = true;
			//存在しないパスが指定されたとき警告を表示する
			//デフォルトでTrueなので指定する必要はない
			sfd.CheckPathExists = true;

			//ダイアログを表示する
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				//OKボタンがクリックされたとき
				//選択されたファイル名を表示する
				//MessageBox.Show(sfd.FileName);
				//Shift JISで書き込む http://dobon.net/vb/dotnet/file/writefile.html
				//書き込むファイルが既に存在している場合は、上書きする
				//
				//	パラメーター http://msdn.microsoft.com/ja-jp/library/f5f5x7kt.aspx
				//	path  型 : System.String 書き込まれる完全なファイルパス。
				//	append 型 : System.Boolean データをファイルの末尾に追加するかどうかを判断します。
				//							ファイルが存在し、append が false の場合は、ファイルが上書きされます。
				//							ファイルが存在し、append が true の場合は、データがファイルの末尾に追加されます。
				//							それ以外の場合は、新しいファイルが作成されます。
				//	encoding 型 : System.Text.Encoding 使用する文字エンコーディング。
				savefilepath = sfd.FileName;
				/*
							System.IO.StreamWriter sw = new System.IO.StreamWriter(
								//@"c:\test.txt",
							sfd.FileName,
							false,
							System.Text.Encoding.GetEncoding("shift_jis"));
							//TextBox1.Textの内容を書き込む
							sw.Write(this.richTextBox1.Text);
							//閉じる
							sw.Close();
							//this.tabPage2.Text = Path.GetFileName(sfd.FileName);
							//this.richTextBox1.Modified = false;
							//this.tabPage2.Text = this.tabPage2.Text.Replace(" *", "");
							//this.toolStripStatusLabel1.Text = this.filepath = sfd.FileName;
							//this.dirtyFlag = false;
				*/
				return savefilepath;
			}
			return "";
		}

		public static String File_ReadToEnd(String filepath)
		{
			if (!System.IO.File.Exists(filepath)) return "";
			//"C:\test.txt"をShift-JISコードとして開く
			System.IO.StreamReader sr = new System.IO.StreamReader(
				filepath,
				System.Text.Encoding.GetEncoding("shift_jis"));
			//内容をすべて読み込む
			String s = sr.ReadToEnd();
			//閉じる
			sr.Close();
			//結果を出力する
			//Console.WriteLine(s);
			return s;
		}

		public static String File_GetCode(String filepath)
		{

			if (!System.IO.File.Exists(filepath)) return null;

			//テキストファイルを開く
			System.IO.FileStream fs = new System.IO.FileStream(
				filepath, System.IO.FileMode.Open,
				System.IO.FileAccess.Read);
			byte[] bs = new byte[fs.Length];
			//byte配列に読み込む
			fs.Read(bs, 0, bs.Length);
			fs.Close();

			//文字コードを取得する
			System.Text.Encoding enc = StringHandler.GetCode(bs);
			//MessageBox.Show(String.Format("{0}",enc));
			//デコードして表示する
			try
			{
				return enc.WebName.ToUpper();
			}
			catch
			{
				return "";
			}
		}
	
		public static String File_GetEofCode(String filepath)
		{
			String text = File_ReadToEnd(filepath);

			if (!System.IO.File.Exists(filepath)) return null;

			if (text.IndexOf("\r\n") >= 0)
			{
				return "CRLF";
			}
			else if (text.IndexOf("\r") >= 0)
			{
				return "CR";
			}
			else if (text.IndexOf("\n") >= 0)
			{
				return "LR";
			}
			return "";
		}
	
		public static String File_ReadToEndDecode(String filepath)
		{
			if (!System.IO.File.Exists(filepath)) return null;
			//テキストファイルを開く
			System.IO.FileStream fs = new System.IO.FileStream(
				filepath, System.IO.FileMode.Open,
				System.IO.FileAccess.Read);
			byte[] bs = new byte[fs.Length];
			//byte配列に読み込む
			fs.Read(bs, 0, bs.Length);
			fs.Close();

			//文字コードを取得する
			System.Text.Encoding enc = StringHandler.GetCode(bs);
			//MessageBox.Show(String.Format("{0}",enc));
			//デコードして表示する
			return enc.GetString(bs);
		}

		public static String File_ReadToEndToUTF8(String filepath)
		{
			if (!System.IO.File.Exists(filepath)) return null;
			//テキストファイルを開く
			System.IO.FileStream fs = new System.IO.FileStream(
				filepath, System.IO.FileMode.Open,
				System.IO.FileAccess.Read);
				byte[] bs = new byte[fs.Length];
			//byte配列に読み込む
			fs.Read(bs, 0, bs.Length);
			fs.Close();

			//文字コードを取得する
			//System.Text.Encoding enc = GetCode(bs);
			String str = System.Text.Encoding.UTF8.GetString(bs);
			return str;
		}

		public static String File_ReadConvertEncoding(string file, System.Text.Encoding destEnc)
		{
			Byte[] bytes = File.ReadAllBytes(file);
			byte[] dest_temp = System.Text.Encoding.Convert(System.Text.Encoding.ASCII, destEnc, bytes);
			string ret = destEnc.GetString(dest_temp);
			return ret;
		}

    public static void File_BackUpCopy(string path)
    {
      MessageBox.Show("FIXME!!","未実装");
    }

  public static void File_SaveEncode(string path, string text, string code)
  {
      MessageBox.Show("FIXME!!","未実装");
  }
  /*
    public static void EncodeSave(String encoding)
    {
      //ToolStripItem button = (ToolStripItem)sender;
      //String encoding = this.ProcessArgString(((ItemData)button.Tag).Tag);
      ScintillaControl sci = Globals.SciControl;
      String path = Globals.MainForm.CurrentDocument.FileName;

      Int32 curMode = sci.CodePage; // From current..
      if (encoding == "SHIFT_JIS")
      {
        String converted = DataConverter.ChangeEncoding(sci.Text, curMode, 932);
        WriteFileEncoding(path, sci.Text, Encoding.GetEncoding("Shift_JIS"));
      }
      else if (encoding == "UTF-8")
      {
        sci.SaveBOM = false;
        String converted = DataConverter.ChangeEncoding(sci.Text, curMode, 65001);
        WriteFileEncoding(path, sci.Text, Encoding.UTF8, false);
      }
    }

    public static void WriteFileEncoding(String file, String text, Encoding encoding)
    {
      try
      {
        using (StreamWriter sw = new StreamWriter(file, false, encoding))
        {
          sw.Write(text);
          sw.Close();
        }
      }
      catch (Exception ex)
      {
        ErrorManager.ShowError(ex);
      }
    }

    public static void WriteFileEncoding(String file, String text, Encoding encoding, Boolean saveBOM)
    {
      try
      {
        Boolean useSkipBomWriter = (encoding == Encoding.UTF8 && !saveBOM);
        using (StreamWriter sw = useSkipBomWriter ? new StreamWriter(file, false) : new StreamWriter(file, false, encoding))
        {
          sw.Write(text);
          sw.Close();
        }
      }
      catch (Exception ex)
      {
        ErrorManager.ShowError(ex);
      }
    }	

      public static void EncodeSave(String encoding)
      {
        //ToolStripItem button = (ToolStripItem)sender;
        //String encoding = this.ProcessArgString(((ItemData)button.Tag).Tag);
        ScintillaControl sci = Globals.SciControl;
        String path = Globals.MainForm.CurrentDocument.FileName;

        Int32 curMode = sci.CodePage; // From current..
        if (encoding == "SHIFT_JIS")
        {
          String converted = DataConverter.ChangeEncoding(sci.Text, curMode, 932);
          WriteFileEncoding(path, sci.Text, Encoding.GetEncoding("Shift_JIS"));
        }
        else if (encoding == "UTF-8")
        {
          sci.SaveBOM = false;
          String converted = DataConverter.ChangeEncoding(sci.Text, curMode, 65001);
          WriteFileEncoding(path, sci.Text, Encoding.UTF8, false);
        }
      }

      public static void WriteFileEncoding(String file, String text, Encoding encoding)
      {
        try
        {
          using (StreamWriter sw = new StreamWriter(file, false, encoding))
          {
            sw.Write(text);
            sw.Close();
          }
        }
        catch (Exception ex)
        {
          ErrorManager.ShowError(ex);
        }
      }

      public static void WriteFileEncoding(String file, String text, Encoding encoding, Boolean saveBOM)
      {
        try
        {
          Boolean useSkipBomWriter = (encoding == Encoding.UTF8 && !saveBOM);
          using (StreamWriter sw = useSkipBomWriter ? new StreamWriter(file, false) : new StreamWriter(file, false, encoding))
          {
            sw.Write(text);
            sw.Close();
          }
        }
        catch (Exception ex)
        {
          ErrorManager.ShowError(ex);
        }
      }	
 
 
 
   */

  //////////////////////////////////////////////////////////////////////////
		//保存していない編集中のテキストがある場合に確認するための関数
		public static bool confirmDestructionText(string msgboxTitle)
		{
			//		const string MSG_BOX_STRING = "ファイルは保存されていません。\n\n"
			//			+ "編集中のテキストは破棄されます。\n\nよろしいですか?";
			return (MessageBox.Show(MSG_BOX_STRING, msgboxTitle, MessageBoxButtons.YesNo,
				MessageBoxIcon.Question) == DialogResult.Yes);
		}

		//保存していない編集中のテキストがある場合に確認するための関数
		public static bool confirmDestructionText(string msgboxTitle, String msgboxString)
		{
			return (MessageBox.Show(msgboxString, msgboxTitle, MessageBoxButtons.YesNo,
				MessageBoxIcon.Question) == DialogResult.Yes);
		}

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

		/// <summary>
		/// 指定されたフォルダ以下にあるすべてのファイルを取得する
		/// </summary>
		/// <param name="folder">ファイルを検索するフォルダ名。</param>
		/// <param name="searchPattern">ファイル名検索文字列
		/// ワイルドカード指定子(*, ?)を使用する。</param>
		/// <param name="files">見つかったファイル名のリスト</param>
		public static void GetAllFiles(
				string folder, string searchPattern, ref ArrayList files)
		{
			//folderにあるファイルを取得する
			string[] fs =
				System.IO.Directory.GetFiles(folder, searchPattern);
			//ArrayListに追加する
			files.AddRange(fs);

			//folderのサブフォルダを取得する
			//string[] ds = System.IO.Directory.GetDirectories(folder);
			//サブフォルダにあるファイルも調べる
			//foreach (string d in ds)
			//	GetAllFiles(d, searchPattern, ref files);
		}
		
		public static String OutputError(string Message)
		{
			StackFrame CallStack = new StackFrame(1, true);

			//string currentFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
			//int currentLine = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber();  

			String SourceFile = CallStack.GetFileName();
			int SourceLine = CallStack.GetFileLineNumber();
			String errMsg = "Error: " + Message + " - File: " + SourceFile + " Line: " + SourceLine.ToString();
			return errMsg;
		}

		/// <summary>
		/// $link = ereg_replace("(https?|ftp|news)(://[[:alnum:]\+\$\;\?\.%,!#~*/:@&=_-]+)",
		/// "＜a href=”\\1\\2” target=”_blank”＞\\1\\2＜/a＞",$link);
		/// return path.IndexOf("http://") != -1 ? true : false;
		/// URLっぽいか調べる
		/// http://dobon.net/vb/dotnet/string/regexismatch.html
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static Boolean IsWebSite(String path)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(
    		path, @"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$"))
			{
    		return true;
			}
			else return false;
		}

		public static Boolean IsEditable(String path)
		{
			return Lib.IsTextFile(path);
		}

		public static Boolean IsBrowsable(String path)
		{
			String extension = Path.GetExtension(path.ToLower());
			if(Lib.IsWebSite(path)) return true;
			else if (extension == ".php" || extension == ".html" || extension == ".htm") return true;
			else return false;
		}
	
		public static Boolean IsTextFile(String path)
		{
			return Lib.textfile.Contains(Path.GetExtension(path.ToLower()));
		}

		public static Boolean IsImageFile(String path)
		{
			return Lib.imagefile.Contains(Path.GetExtension(path.ToLower()));
		}

		public static Boolean IsSoundFile(String path)
		{
			return Lib.soundfile.Contains(Path.GetExtension(path.ToLower()));
		}

		public static Boolean IsVideoFile(String path)
		{
			return Lib.videofile.Contains(Path.GetExtension(path.ToLower()));
		}

		public static Boolean IsDataBaseFile(String path)
		{
			return Lib.databasefile.Contains(Path.GetExtension(path.ToLower()));
		}

		public static Boolean IsExecutableFile(String path)
		{
			return Lib.executablefile.Contains(Path.GetExtension(path.ToLower()));
		}

	
		public static Boolean ContainsInvalidFileNameChars(String path)
		{
			//ファイル名に使用できない文字を取得
			char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
			if (path.IndexOfAny(invalidChars) < 0)
			{
				//Console.WriteLine("ファイル名に使用できない文字は使われていません。");
				return false;
			}
			else
			{
				//Console.WriteLine("ファイル名に使用できない文字が使われています。");
				return true;
			}
		}

		/// <summary>
		/// ファイルはバイナリファイルであるかどうか
		/// </summary>
		/// <param name="filePath">パス</param>
		/// <returns>バイナリファイルの場合trueを返す</returns>
		public static bool IsBinaryFile(string filePath)
		{
			FileStream fs = File.OpenRead(filePath);
			int len = (int)fs.Length;
			int count = 0;
			byte[] content = new byte[len];
			int size = fs.Read(content, 0, len);

			for (int i = 0; i < size; i++)
			{
				if (content[i] == 0)
				{
					count++;
					if (count == 4)
					{
						return true;
					}
				}
				else
				{
					count = 0;
				}
			}
			return false;
		}

		/// <summary>
		/// ファイルはテキストファイルであるかどうか
		/// </summary>
		/// http://yellow.ribbon.to/~tuotehhou/index.php?%2BC%EF%BC%83%2B%E3%83%90%E3%82%A4%E3%83%8A%E3%83%AA%E3%83%BB%E3%83%86%E3%82%AD%E3%82%B9%E3%83%88%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB%E3%81%AE%E5%88%A4%E6%96%AD
		/// http://www.xuehi.com/
		/// <param name="filePath">パス</param>
		/// <returns>テキストファイルの場合Trueを返す</returns>
		public bool IsTxtFile(string filePath)
		{
			FileStream file = new System.IO.FileStream(filePath, FileMode.Open, FileAccess.Read);
			byte[] byteData = new byte[1];
			while (file.Read(byteData, 0, byteData.Length) > 0)
			{
				if (byteData[0] == 0)
					return false;
			}
			return true;
		}

		/*	
		static void Main() {
			Dictionary<string, int> dict = new Dictionary<string, int>();
			//makeDict(dict);

			List<KeyValuePair<string, int>> sorted = sortByValue(dict);
			// sorted.Reverse(); // 逆順にする場合

			foreach (KeyValuePair<string, int> kvp in sorted) {
				Console.WriteLine(kvp.Key + ":" + kvp.Value);
			}
			// 出力例：
			// a:542
			// td:394
			// 0:328
			// width:289
			// tr:284
			// ……
		}
		*/

		//// http://www.atmarkit.co.jp/fdotnet/dotnettips/441sortbyvalue/sortbyvalue.html
		public static List<KeyValuePair<string, int>>
			DictionarySortByValue(Dictionary<string, int> dict)
		{
			List<KeyValuePair<string, int>> list
				= new List<KeyValuePair<string, int>>(dict);

			// Valueの大きい順にソート
			list.Sort(
				delegate(KeyValuePair<string, int> kvp1, KeyValuePair<string, int> kvp2) {
					return kvp2.Value - kvp1.Value;
				});
			return list;
		}
	}
	
	/*
	[C#]
	//Button1のクリックイベントハンドラ
	private void Button1_Click(object sender, System.EventArgs e)
	{
		//テキストファイルを開く
		System.IO.FileStream fs = new System.IO.FileStream(
			TextBox1.Text, System.IO.FileMode.Open,
			System.IO.FileAccess.Read);
		byte[] bs = new byte[fs.Length];
		//byte配列に読み込む
		fs.Read(bs, 0, bs.Length);
		fs.Close();

		//文字コードを取得する
		System.Text.Encoding enc = GetCode(bs);

		//デコードして表示する
		RichTextBox1.Text = enc.GetString(bs);
	}
	*/

	public class TwoKeysHashTable
	{
		public Hashtable ht;
		public String this[String key1, String key2]
		{
			get
			{
				try
				{
					return (String)((Hashtable)ht[key1])[key2];
				}
				catch (System.Exception exc)
				{
					String err_msg = exc.Message.ToString();
					//String s = "知りません";
					return "";
				}
			}
			set
			{
				if (ht == null) ht = new Hashtable();
				if (!ht.Contains(key1))
				{
					ht[key1] = new Hashtable();
				}
				((Hashtable)ht[key1])[key2] = value;
			}
		}
	
		public TwoKeysHashTable()
		{
			//ht = new Hashtable();
		}
	}

	class TwoKeysDictionary<T, U> : IEnumerable<KeyValuePair< T , Dictionary<U, Object>>>
	{
		Dictionary<T, Dictionary<U, Object>> dictionary = new Dictionary<T, Dictionary<U, Object>>();
		public TwoKeysDictionary() { }
		public Object this[T key1, U key2]
		{
			get
			{
				return dictionary[key1][key2];
			}
			set
			{
				if (!dictionary.ContainsKey(key1))
				{
					dictionary[key1] = new Dictionary<U, Object>();
				}
				dictionary[key1][key2] = value;
			}
		}

		public IEnumerator<KeyValuePair<T, Dictionary<U, Object>>> GetEnumerator()
		{
			foreach (KeyValuePair<T, Dictionary<U, Object>> obj in dictionary)
			{
				yield return obj;
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<Dictionary<U, Object>>)this).GetEnumerator();
		}

		/*
		public void Add(String key1, String key2, String svalue)
    {
			if (!dictionary.ContainsKey(key1))
			{
				dictionary[key1] = new Dictionary<U, Object>();
			}
			dictionary[key1][key2] = value;
			}
		*/
	}

}
