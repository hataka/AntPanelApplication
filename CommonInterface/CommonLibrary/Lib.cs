using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace CommonInterface.CommonLibrary
{
  public class Lib : IDisposable
  {
    public const string OpenFileFilter = "All files(*.*)|*.*|Supported files|*.txt;*.log;*.ini;*.inf;*.tex;*.htm;*.html;*.css;*.js;*.xml;*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx;*.cs;*.java;*.py;*.rb;*.pl;*.vbs;*.bat|Text file(*.txt, *.log, *.tex, ...)|*.txt;*.log;*.ini;*.inf;*.tex|HTML file(*.htm, *.html)|*.htm;*.html|CSS file(*.css)|*.css|Javascript file(*.js)|*.js|XML file(*.xml)|*.xml|C/C++ source(*.c, *.h, ...)|*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx|C# source(*.cs)|*.cs|Java source(*.java)|*.java|Python script(*.py)|*.py|Ruby script(*.rb)|*.rb|Perl script(*.pl)|*.pl|VB script(*.vbs)|*.vbs|Batch file(*.bat)|*.bat";
    public const string SaveFileFilter = "All files(*.*)|*.*|Text file(*.txt, *.log, *.tex, ...)|*.txt;*.log;*.ini;*.inf;*.tex|HTML file(*.htm, *.html)|*.htm;*.html|CSS file(*.css)|*.css|Javascript file(*.js)|*.js|XML file(*.xml)|*.xml|C/C++ source(*.c, *.h, ...)|*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx|C# source(*.cs)|*.cs|Java source(*.java)|*.java|Python script(*.py)|*.py|Ruby script(*.rb)|*.rb|Perl script(*.pl)|*.pl|VB script(*.vbs)|*.vbs|Batch file(*.bat)|*.bat";
    public const string CommonFileFilter = "Text file(*.txt, *.log, *.tex, ...)|*.txt;*.log;*.ini;*.inf;*.tex|HTML file(*.htm, *.html)|*.htm;*.html|CSS file(*.css)|*.css|Javascript file(*.js)|*.js|XML file(*.xml)|*.xml|C/C++ source(*.c, *.h, ...)|*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx|C# source(*.cs)|*.cs|Java source(*.java)|*.java|Python script(*.py)|*.py|Ruby script(*.rb)|*.rb|Perl script(*.pl)|*.pl|VB script(*.vbs)|*.vbs|Batch file(*.bat)|*.bat";
    public const string MSG_BOX_STRING = "ファイルは保存されていません。\n\n編集中のテキストは破棄されます。\n\nよろしいですか?";

    public static ArrayList textfile = new ArrayList(new String[]
    {".txt",".rtf",".c",".cpp",".cs",".java",".php",".vbs",".js",".wsf",//".hta",".html",".htm",
      ".tcl",".pl",".tex",".as",".xml",".mxml",".ns",".ini",".sql",".gradle"});

    public static ArrayList executablefile = new ArrayList { ".exe", ".lnk", ".doc", ".xls", ".ttf", ".ppt", ".ppr", ".pptx", ".docx", ".xlsx", ".pdf", ".zip" };

    public static ArrayList imagefile = new ArrayList { ".jpg", ".bmp", ".png", ".gif", ".ico", ".jpeg" };

    public static ArrayList soundfile = new ArrayList { ".asf", ".wax", ".asx", ".mp3", ".wav", ".wma" };

    public static ArrayList videofile = new ArrayList { ".wmv", ".asx", ".mpg", ".m2p", ".asf" };

    public static ArrayList databasefile = new ArrayList { ".mdf", ".mdb", ".sqlit3", ".sqlite" };

    public static Form form;

    public static Process extProcess = null;

    public static Thread extThread = null;

    public static Form MainForm
    {
      get
      {
        return Lib.form;
      }
      set
      {
        Lib.form = value;
      }
    }

    ~Lib()
    {
      this.Dispose();
    }

    public void Dispose()
    {
    }

    public static Control[] GetAllControls(Control top)
    {
      ArrayList arrayList = new ArrayList();
      foreach (Control control in top.Controls)
      {
        arrayList.Add(control);
        arrayList.AddRange(Lib.GetAllControls(control));
      }
      return (Control[])arrayList.ToArray(typeof(Control));
    }

    public static Control FindChildControlByType(Control ctrl, string type)
    {
      for (int i = 0; i < ctrl.Controls.Count; i++)
      {
        if (ctrl.Controls[i].GetType().Name == type)
        {
          return ctrl.Controls[i];
        }
      }
      return null;
    }

    public static string File_OpenDialog(string FileName, string InitialDirectory, string Filter)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.FileName = FileName;
      if (!String.IsNullOrEmpty(InitialDirectory)) openFileDialog.InitialDirectory = InitialDirectory;
      openFileDialog.Filter = Filter;
      openFileDialog.FilterIndex = 0;
      openFileDialog.Title = "開くファイルを選択してください";
      openFileDialog.RestoreDirectory = true;
      //ダイアログボックスを閉じる前に現在のディレクトリを復元するよ うにする
      //openFileDialog.RestoreDirectory = true;
      openFileDialog.CheckFileExists = true;
      openFileDialog.CheckPathExists = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
      {
        return "";
      }
      if (File.Exists(openFileDialog.FileName))
      {
        return openFileDialog.FileName;
      }
      return "";
    }

    public static string File_OpenDialogNoCheckFileExists(string FileName, string InitialDirectory, string Filter)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.FileName = FileName;
      openFileDialog.InitialDirectory = InitialDirectory;
      openFileDialog.Filter = Filter;
      openFileDialog.FilterIndex = 0;
      openFileDialog.Title = "開くファイルを選択してください";
      openFileDialog.RestoreDirectory = true;
      openFileDialog.CheckFileExists = false;
      openFileDialog.CheckPathExists = true;
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        return openFileDialog.FileName;
      }
      return "";
    }

    public static string File_SaveDialog(string FileName, string InitialDirectory, string Filter)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.FileName = FileName;
      saveFileDialog.InitialDirectory = InitialDirectory;
      saveFileDialog.Filter = Filter;
      saveFileDialog.FilterIndex = 2;
      saveFileDialog.Title = "保存先のファイルを選択してください";
      saveFileDialog.RestoreDirectory = true;
      saveFileDialog.OverwritePrompt = true;
      saveFileDialog.CheckPathExists = true;
      if (saveFileDialog.ShowDialog() == DialogResult.OK)
      {
        return saveFileDialog.FileName;
      }
      return "";
    }

    public static string File_ReadToEnd(string filepath)
    {
      if (!File.Exists(filepath))
      {
        return "";
      }
      StreamReader streamReader = new StreamReader(filepath, Encoding.GetEncoding("shift_jis"));
      string result = streamReader.ReadToEnd();
      streamReader.Close();
      return result;
    }

    public static string File_GetCode(string filepath)
    {
      if (!File.Exists(filepath))
      {
        return null;
      }
      FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
      byte[] array = new byte[fileStream.Length];
      fileStream.Read(array, 0, array.Length);
      fileStream.Close();
      Encoding code = StringHandler.GetCode(array);
      string result;
      try
      {
        result = code.WebName.ToUpper();
      }
      catch
      {
        result = "";
      }
      return result;
    }

    public static string File_GetEofCode(string filepath)
    {
      string text = Lib.File_ReadToEnd(filepath);
      if (!File.Exists(filepath))
      {
        return null;
      }
      if (text.IndexOf("\r\n") >= 0)
      {
        return "CRLF";
      }
      if (text.IndexOf("\r") >= 0)
      {
        return "CR";
      }
      if (text.IndexOf("\n") >= 0)
      {
        return "LR";
      }
      return "";
    }

    public static string File_ReadToEndDecode(string filepath)
    {
      if (!File.Exists(filepath))
      {
        return null;
      }
      FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
      byte[] array = new byte[fileStream.Length];
      fileStream.Read(array, 0, array.Length);
      fileStream.Close();
      Encoding code = StringHandler.GetCode(array);
      return code.GetString(array);
    }

    public static string File_ReadToEndToUTF8(string filepath)
    {
      if (!File.Exists(filepath))
      {
        return null;
      }
      FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
      byte[] array = new byte[fileStream.Length];
      fileStream.Read(array, 0, array.Length);
      fileStream.Close();
      return Encoding.UTF8.GetString(array);
    }

    public static string File_ReadConvertEncoding(string file, Encoding destEnc)
    {
      byte[] bytes = File.ReadAllBytes(file);
      byte[] bytes2 = Encoding.Convert(Encoding.ASCII, destEnc, bytes);
      return destEnc.GetString(bytes2);
    }

    public static bool confirmDestructionText(string msgboxTitle)
    {
      return MessageBox.Show("ファイルは保存されていません。\n\n編集中のテキストは破棄されます。\n\nよろしいですか?", msgboxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }

    public static bool confirmDestructionText(string msgboxTitle, string msgboxString)
    {
      return MessageBox.Show(msgboxString, msgboxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }

    public static string GetTargetExt(string dir, string target)
    {
      ArrayList arrayList = new ArrayList
      {
        ".cs",
        ".c",
        ".cc",
        ".cpp",
        ".java",
        ".tex",
        ".as",
        ".mxml"
      };
      foreach (string text in arrayList)
      {
        if (File.Exists(dir + "\\" + target + text))
        {
          return text;
        }
      }
      return string.Empty;
    }

    public static void File_BackUpCopy(String path)
    {
      if (!File.Exists(path)) return;
      DateTime dt = DateTime.Now;
      String strtime = String.Format("_{0:00}{1:00}{2:00}{3:00}{4:00}{5:00}",
          dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
      String newfilepath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path))
        + strtime + Path.GetExtension(path);
      System.IO.File.Copy(path, newfilepath);
    }

    public static void File_SaveEncode(String path, String text, String strenc)
    {
      /*
      //BOM無しのUTF8でテキストファイルを作成する
      System.Text.Encoding enc = new System.Text.UTF8Encoding(false);
      //または、次のようにすることもできる
      //System.Text.Encoding enc = new System.Text.UTF8Encoding();

      System.IO.StreamWriter sw = new System.IO.StreamWriter(
          @"C:\test\1.txt", false, enc);
      sw.Write("こんにちは。");
      sw.Close();
      */
      System.Text.Encoding enc;

      //if (!File.Exists(path)) return;
      //Lib.File_BackUpCopy(path);
      if (strenc.ToLower() == "auto")

        if (File.Exists(path)) strenc = Lib.File_GetCode(path);
        else strenc = "utf-8";
      // 文字エンコーディングの必要
      // http://dobon.net/vb/dotnet/file/writefile.html
      //書き込むファイルが既に存在している場合は、上書きする
      //String enc = Lib.File_GetCode(this.currentPath);

      if (strenc.ToLower() == "utf-8" && strenc == String.Empty)
      {
        enc = new System.Text.UTF8Encoding(false);
      }
      else
      {
        enc = System.Text.Encoding.GetEncoding(strenc);
      }
      System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, enc);
      //TextBox1.Textの内容を書き込む
      sw.Write(text);
      //閉じる
      sw.Close();
    }


    public static void File_SaveUtf8(String path, String text)
    {
      /*
      //BOM無しのUTF8でテキストファイルを作成する
      System.Text.Encoding enc = new System.Text.UTF8Encoding(false);
      //または、次のようにすることもできる
      //System.Text.Encoding enc = new System.Text.UTF8Encoding();

      System.IO.StreamWriter sw = new System.IO.StreamWriter(
          @"C:\test\1.txt", false, enc);
      sw.Write("こんにちは。");
      sw.Close();
      */
      System.Text.Encoding enc = new System.Text.UTF8Encoding(); ;
      //System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, enc);
      System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false);
      //TextBox1.Textの内容を書き込む
      sw.Write(text);
      //閉じる
      sw.Close();
    }



    public static string Path2Url(string path, string serverName = "localhost")
    {
      // serverName localhost, hata, 192.168.1.53
      //ローカルファイルのパスを表すURI
      //string uriPath = "file:///c:/temp/doc.txt";
      string result = String.Empty;
      if (Lib.IsWebSite(path)) return path;
      try
      {
        result = "file:///" + path.Replace(@"\", "/").ToLower();
        result = result.Replace("file:///c:/apache2.2/htdocs", "http://" + serverName);
        result = result.Replace("file:///f:/", "http://" + serverName + "/f/");
        result = result.Replace("file:///c:/eclipse461/xampp/htdocs", "http://" + serverName + ":8081");
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
        result = string.Empty;
      }
      return result;
    }

    public static string Path2Url(string path, string documentRoot, string serverRoot)
    {
      string result;
      try
      {
        result = path.Replace(documentRoot, serverRoot).Replace("\\", "/");
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
        result = string.Empty;
      }
      return result;
    }



    public static string Url2Path(string url)
    {
      string result;
      try
      {
        result = url.Replace("http://localhost", "F:").Replace("/", "\\");
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
        result = string.Empty;
      }
      return result;
    }

    public static string Url2Path(string url, string path, string documentRoot, string serverRoot)
    {
      string result;
      try
      {
        result = url.Replace(serverRoot, documentRoot).Replace("/", "\\");
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
        result = string.Empty;
      }
      return result;
    }


    public static void GetAllFiles(string folder, string searchPattern, ref ArrayList files)
    {
      string[] files2 = Directory.GetFiles(folder, searchPattern);
      files.AddRange(files2);
    }

    public static string OutputError(string Message)
    {
      StackFrame stackFrame = new StackFrame(1, true);
      string fileName = stackFrame.GetFileName();
      return string.Concat(new string[]
      {
        "Error: ",
        Message,
        " - File: ",
        fileName,
        " Line: ",
        stackFrame.GetFileLineNumber().ToString()
      });
    }

    public static bool IsWebSite(string path)
    {
      return Regex.IsMatch(path, "^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$");
    }

    public static bool IsEditable(string path)
    {
      return Lib.IsTextFile(path);
    }

    public static bool IsBrowsable(string path)
    {
      string extension = Path.GetExtension(path.ToLower());
      return Lib.IsWebSite(path) || (extension == ".php" || extension == ".html" || extension == ".htm");
    }

    public static bool IsTextFile(string path)
    {
      return Lib.textfile.Contains(Path.GetExtension(path.ToLower()));
    }

    public static bool IsImageFile(string path)
    {
      return Lib.imagefile.Contains(Path.GetExtension(path.ToLower()));
    }

    public static bool IsSoundFile(string path)
    {
      return Lib.soundfile.Contains(Path.GetExtension(path.ToLower()));
    }

    public static bool IsVideoFile(string path)
    {
      return Lib.videofile.Contains(Path.GetExtension(path.ToLower()));
    }

    public static bool IsDataBaseFile(string path)
    {
      return Lib.databasefile.Contains(Path.GetExtension(path.ToLower()));
    }

    public static bool IsExecutableFile(string path)
    {
      return Lib.executablefile.Contains(Path.GetExtension(path.ToLower()));
    }

    public static bool ContainsInvalidFileNameChars(string path)
    {
      char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
      return path.IndexOfAny(invalidFileNameChars) >= 0;
    }

    public static bool IsBinaryFile(string filePath)
    {
      FileStream fileStream = File.OpenRead(filePath);
      int num = (int)fileStream.Length;
      int num2 = 0;
      byte[] array = new byte[num];
      int num3 = fileStream.Read(array, 0, num);
      for (int i = 0; i < num3; i++)
      {
        if (array[i] == 0)
        {
          num2++;
          if (num2 == 4)
          {
            return true;
          }
        }
        else
        {
          num2 = 0;
        }
      }
      return false;
    }

    public bool IsTxtFile(string filePath)
    {
      FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      byte[] array = new byte[1];
      while (fileStream.Read(array, 0, array.Length) > 0)
      {
        if (array[0] == 0)
        {
          return false;
        }
      }
      return true;
    }

    public static bool IsWinXP()
    {
      OperatingSystem oSVersion = Environment.OSVersion;
      return oSVersion.Version.Major != 5 || oSVersion.Version.Minor != 1 || true;
    }


    public static List<KeyValuePair<string, int>> DictionarySortByValue(Dictionary<string, int> dict)
    {
      List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(dict);
      list.Sort((KeyValuePair<string, int> kvp1, KeyValuePair<string, int> kvp2) => kvp2.Value - kvp1.Value);
      return list;
    }
  }
}
