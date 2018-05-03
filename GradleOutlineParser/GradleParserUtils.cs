using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AntPlugin.CommonLibrary
{
	public class GradleParserUtils
	{
		///カレットの位置までの行を数える
		public static Position GetCurrentPosition(string str, Int32 point)
		{
			//文字列
			//string str = TextBox1.Text;
			//カレットの位置を取得
			//int selectPos = TextBox1.SelectionStart;

			//カレットの位置までの行を数える
			int row = 1, startPos = 0;
			for (int endPos = 0;
					(endPos = str.IndexOf('\n', startPos)) < point && endPos > -1;
					row++)
			{
				startPos = endPos + 1;
			}

			//列の計算
			int col = point - startPos + 1;

			//結果を表示
			//Console.WriteLine("行:{0} 列:{1}", row, col);
			return new Position(col, row);
		}

		public static Int32 BraceMatch(string text, int start)
		{
			int level = 0;
			int i = 0;
			//foreach (char c in text)
			//for (i = ptr; i < text.Length; i++)
			i = start;
			while (i < text.Length)
			{
				// コメント（注釈を探す）
				if (text[i] == '/')
				{
					if (text[i + 1] == '*')
					{	// ブロックコメントの場合
						i += 2;
						do
						{				// コメントの終わりを探す
							while (text[i] != '*') i++;
							i++;
						} while (text[i] != '/');
						i++;
					}
					if (text[i + 1] == '/')
					{	// ラインコメントの場合
						i += 2;
						do
						{				// コメントの終わりを探す
							i++;
						} while (text[i] != '\n');
						i++;
					}
				}

				if (text[i] == '{') level++; // opening brace detected
				if (text[i] == '}')
				{
					level--;
          if (level == 0) return i;// break;
				}
				i++;
			}
      // closing brace detected, without a corresponding opening brace
      if (level < 0) throw new ApplicationException("Opening brace missing.");
      // more open than closing braces
      if (level > 0) throw new ApplicationException("Closing brace missing.");
     return i;
		}

		public static Int32 BraceMatch(string text, int start, char start_brace, char end_brace)
		{
			int level = 0;
			int i = 0;
			//foreach (char c in text)
			//for (i = ptr; i < text.Length; i++)
			i = start;
			while (i < text.Length)
			{
				// コメント（注釈を探す）
				if (text[i] == '/')
				{
					if (text[i + 1] == '*')
					{	// ブロックコメントの場合
						i += 2;
						do
						{				// コメントの終わりを探す
							while (text[i] != '*') i++;
							i++;
						} while (text[i] != '/');
						i++;
					}
					if (text[i + 1] == '/')
					{	// ラインコメントの場合
						i += 2;
						do
						{				// コメントの終わりを探す
							i++;
						} while (text[i] != '\n');
						i++;
					}
				}

				if (text[i] == start_brace) level++; // opening brace detected
				if (text[i] == end_brace)
				{
					level--;
					if (level == 0) break;
				}
				i++;
			}
			// closing brace detected, without a corresponding opening brace
			if (level < 0) throw new ApplicationException("Opening brace missing.");
			// more open than closing braces
			if (level > 0) throw new ApplicationException("Closing brace missing.");
			return i;
		}

		public static String GetOuterCode(string text, int start)
		{
			string buffer = string.Empty;
			int i = start;
			try
			{
				while (i < text.Length - start)
				{
					/// Kahata bugfix コメント内の{を読み飛ばし unmatch を防止する	Time-stamp: <2016-05-14 7:00:27 kahata>
					// コメント（注釈を探す）
					if (text[i] == '/')
					{
						if (text[i + 1] == '*')
						{	// ブロックコメントの場合
							i += 2;
							do
							{				// コメントの終わりを探す
								while (text[i] != '*') i++;
								i++;
							} while (text[i] != '/');
							i++;
						}
						if (text[i + 1] == '/')
						{	// ラインコメントの場合
							i += 2;
							do
							{				// コメントの終わりを探す
								i++;
							} while (text[i] != '\n');
							i++;
						}
					}
					
					
					
					if (text[i] != '{') buffer += text[i++];
					else
					{
						buffer += "{";
						i = BraceMatch(text, i);
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
				buffer = text.Substring(start);
			}
				return buffer;
		}



    public static String DeComment(string text)
    {
      string buffer = string.Empty;
      int i = 0;
      text += "\n";
      try
      {
        while (i < text.Length - 1)
        {
          /// Kahata bugfix コメント内の{を読み飛ばし unmatch を防止する	Time-stamp: <2016-05-14 7:00:27 kahata>
          // コメント（注釈を探す）
          if (text[i] == '/')
          {
            if (text[i + 1] == '*')
            { // ブロックコメントの場合
              i += 2;
              do
              {       // コメントの終わりを探す
                while (text[i] != '*') i++;
                i++;
              } while (text[i] != '/');
              i++;
            }
            if (text[i + 1] == '/')
            { // ラインコメントの場合
              i += 2;
              do
              {       // コメントの終わりを探す
                i++;
              } while (text[i] != '\n');
              i += 2;
            }
          }



          //if (text[i] != '{')
          buffer += text[i++];
          //else
          //{
            //buffer += "{";
            //i = BraceMatch(text, i);
          //}
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
        MessageBox.Show(buffer);
      }
      return buffer;
    }





    public static string GetLine(string text, Int32 num)
		{
			string[] lines = text.Split('\n');
			return lines[num - 1];
		}

		public static Int32 Position2Point(string text, Position pos)
		{
			string[] lines = text.Split('\n');
			int ptr = 0;
			for (int i = 0; i < pos.row - 1; i++) ptr += lines[i].Length + 1;
			return ptr + pos.col - 1;
		}

		public static Position SafePosition(string text, Position pos)
		{
			string[] lines = text.Split('\n');
			Int32 safeRow = pos.row > lines.Length ? lines.Length : pos.row;
			Int32 safeCol = pos.col > lines[safeRow - 1].Length ? lines[safeRow - 1].Length : pos.col;
			return new Position(safeRow, safeCol);
		}
	
		public static bool IsWhite(char c)
		{
			if (c == ' ' || c == '\t') return true;
			else return false;
		}

		public static bool IsDelimiter(char c)
		{
			if ("+-*^/%=;{}[](),.<>!\"\'\\s\\t#&~|$?{}`\\@:".IndexOf(c) > -1) return true;
			else return false;
		}

    public int LookUpEnd(int pos, string src)
    {
      int brace = pos;
      //\s 空白文字。改行文字、タブ文字、半角/全角スペース文字など。[\f\n\r\t\v\x85\p{Z}]と同じ。
      while (Regex.IsMatch(src[brace].ToString(), "\\s")) brace++;
      if (src[brace] == '{')
      {
        int end = GradleParserUtils.BraceMatch(src, brace);
        return end + 1;
      }
      //else return this.start + this.definition.Length;
      else return 0;
    }

    public static string GetSource(string path)
    {
      string text = string.Empty;
      using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8")))
      {
        text = sr.ReadToEnd();
      }
      return text;
    }

    /*
		public static string JoinString<T>(this IEnumerable<T> values, string separator, Func<T, string> converter = null)
		{
			if (converter != null)
			{
				List<string> buff = new List<string>();
				foreach (var item in values)
					buff.Add(converter(item));
				return string.Join(separator, buff);
			}
			else
				return string.Join(separator, values);
		}
	*/

  }

  public class Position
	{
		public int row;
		public int col;
		public Position() { }
		public Position(int col, int row) { this.col = col; this.row = row; }
	}

	public class Range
	{
		public int start;
		public int end;
		public Range() { }
		public Range(int start, int end) { this.start = start; this.end = end; }
	}



}
