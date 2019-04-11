using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommonInterface.CommonLibrary
{
  public class StringHandler
  {
    public enum tok_types
    {
      DELIMITER,
      IDENTIFIER,
      NUMBER,
      KEYWORD,
      TEMP,
      STRING,
      BLOCK
    }

    public enum tokens
    {
      ARRAY,
      CHAR,
      INT,
      LONG,
      FLOAT,
      DOUBLE,
      VOID,
      STATIC,
      UNSIGNED,
      IF,
      ELSE,
      FOR,
      DO,
      WHILE,
      SWITCH,
      CASE,
      DEFAULT,
      GOTO,
      BREAK,
      CONTINUE,
      RETURN,
      EOL,
      FINISHED,
      END
    }

    public enum double_ops
    {
      LT = 1,
      LE,
      GT,
      GE,
      EQ,
      NE,
      INC,
      DEC,
      AND,
      OR
    }

    //private string expression;

    public static ArrayList AccessModifiers = new ArrayList
    {
      "public",
      "protected",
      "internal",
      "private"
    };

    public static ArrayList Modifiers = new ArrayList
    {
      "public",
      "private",
      "internal",
      "protected",
      "abstract",
      "const",
      "event",
      "extern",
      "new",
      "override",
      "partial",
      "readonly",
      "sealed",
      "static",
      "unsafe",
      "virtual",
      "volatile"
    };

    public static ArrayList KeyWords = new ArrayList
    {
      "abstract",
      "event",
      "new",
      "struct",
      "as",
      "explicit",
      "null",
      "switch",
      "base",
      "extern",
      "object",
      "this",
      "bool",
      "false",
      "operator",
      "throw",
      "break",
      "finally",
      "out",
      "true",
      "byte",
      "fixed",
      "override",
      "try",
      "case",
      "float",
      "params",
      "typeof",
      "catch",
      "for",
      "private",
      "uint",
      "char",
      "foreach",
      "protected",
      "ulong",
      "checked",
      "goto",
      "public",
      "unchecked",
      "class",
      "if",
      "readonly",
      "unsafe",
      "const",
      "implicit",
      "ref",
      "ushort",
      "continue",
      "in",
      "return",
      "using",
      "decimal",
      "int",
      "sbyte",
      "virtual",
      "default",
      "interface",
      "sealed",
      "volatile",
      "delegate",
      "internal",
      "short",
      "void",
      "do",
      "is",
      "sizeof",
      "while",
      "double",
      "lock",
      "stackalloc",
      "else",
      "long",
      "static",
      "enum",
      "namespace",
      "string"
    };

    public static ArrayList Operators = new ArrayList
    {
      "+",
      "-",
      "*",
      "/",
      "%",
      "&",
      "|",
      "^",
      "!",
      "~",
      "&&",
      "||",
      "true",
      "false",
      "++",
      "--",
      "<<",
      ">>",
      "==",
      "!=",
      "<",
      ">",
      "<=",
      ">=",
      "=",
      "+=",
      "-=",
      "*=",
      "/=",
      "%=",
      "&=",
      "|=",
      "^=",
      "<<=",
      ">>=",
      "?",
      ":",
      "new",
      "as",
      "is",
      "sizeof",
      "typeof",
      "control",
      "checked",
      "unchecked",
      "->"
    };

    public static ArrayList Directives = new ArrayList
    {
      "#if",
      "#else",
      "#elif",
      "#endif",
      "#define",
      "#undef",
      "#warning",
      "#error",
      "#line",
      "#region",
      "#endregion"
    };

    public static byte[] StringToBytes(string str)
    {
      Encoding encoding = Encoding.GetEncoding("Shift_JIS");
      return encoding.GetBytes(str);
    }

    public static string ConvertEncoding(string src, Encoding destEnc)
    {
      byte[] bytes = Encoding.ASCII.GetBytes(src);
      byte[] bytes2 = Encoding.Convert(Encoding.ASCII, destEnc, bytes);
      return destEnc.GetString(bytes2);
    }

    public static Encoding GetCode(byte[] bytes)
    {
      int num = bytes.Length;
      bool flag = false;
      for (int i = 0; i < num; i++)
      {
        byte b = bytes[i];
        if (b <= 6 || b == 127 || b == 255)
        {
          flag = true;
          if (b == 0 && i < num - 1 && bytes[i + 1] <= 127)
          {
            return Encoding.Unicode;
          }
        }
      }
      if (flag)
      {
        return null;
      }
      bool flag2 = true;
      for (int j = 0; j < num; j++)
      {
        byte b = bytes[j];
        if (b == 27 || 128 <= b)
        {
          flag2 = false;
          break;
        }
      }
      if (flag2)
      {
        return Encoding.ASCII;
      }
      for (int k = 0; k < num - 2; k++)
      {
        byte b = bytes[k];
        byte b2 = bytes[k + 1];
        byte b3 = bytes[k + 2];
        if (b == 27)
        {
          if (b2 == 36 && b3 == 64)
          {
            return Encoding.GetEncoding(50220);
          }
          if (b2 == 36 && b3 == 66)
          {
            return Encoding.GetEncoding(50220);
          }
          if (b2 == 40 && (b3 == 66 || b3 == 74))
          {
            return Encoding.GetEncoding(50220);
          }
          if (b2 == 40 && b3 == 73)
          {
            return Encoding.GetEncoding(50220);
          }
          if (k < num - 3)
          {
            byte b4 = bytes[k + 3];
            if (b2 == 36 && b3 == 40 && b4 == 68)
            {
              return Encoding.GetEncoding(50220);
            }
            if (k < num - 5 && b2 == 38 && b3 == 64 && b4 == 27 && bytes[k + 4] == 36 && bytes[k + 5] == 66)
            {
              return Encoding.GetEncoding(50220);
            }
          }
        }
      }
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      for (int l = 0; l < num - 1; l++)
      {
        byte b = bytes[l];
        byte b2 = bytes[l + 1];
        if (((129 <= b && b <= 159) || (224 <= b && b <= 252)) && ((64 <= b2 && b2 <= 126) || (128 <= b2 && b2 <= 252)))
        {
          num2 += 2;
          l++;
        }
      }
      for (int m = 0; m < num - 1; m++)
      {
        byte b = bytes[m];
        byte b2 = bytes[m + 1];
        if ((161 <= b && b <= 254 && 161 <= b2 && b2 <= 254) || (b == 142 && 161 <= b2 && b2 <= 223))
        {
          num3 += 2;
          m++;
        }
        else if (m < num - 2)
        {
          byte b3 = bytes[m + 2];
          if (b == 143 && 161 <= b2 && b2 <= 254 && 161 <= b3 && b3 <= 254)
          {
            num3 += 3;
            m += 2;
          }
        }
      }
      for (int n = 0; n < num - 1; n++)
      {
        byte b = bytes[n];
        byte b2 = bytes[n + 1];
        if (192 <= b && b <= 223 && 128 <= b2 && b2 <= 191)
        {
          num4 += 2;
          n++;
        }
        else if (n < num - 2)
        {
          byte b3 = bytes[n + 2];
          if (224 <= b && b <= 239 && 128 <= b2 && b2 <= 191 && 128 <= b3 && b3 <= 191)
          {
            num4 += 3;
            n += 2;
          }
        }
      }
      if (num3 > num2 && num3 > num4)
      {
        return Encoding.GetEncoding(51932);
      }
      if (num2 > num3 && num2 > num4)
      {
        return Encoding.GetEncoding(932);
      }
      if (num4 > num3 && num4 > num2)
      {
        return Encoding.UTF8;
      }
      return null;
    }

    public static string timestamp()
    {
      DateTime now = DateTime.Now;
      return string.Format("Time-stamp: <{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00} kahata>", new object[]
      {
        now.Year,
        now.Month,
        now.Day,
        now.Hour,
        now.Minute,
        now.Second
      });
    }

    public static string CHeading(string path)
    {
      string text = Lib.File_ReadToEnd("C:\\home\\hidemaru\\template\\c_heading_001.txt");
      if (File.Exists(path))
      {
        try
        {
          string newValue = path.Replace("F:", "http://localhost").Replace("\\", "/");
          text = text.Replace("%%FILEEXT%%", Path.GetExtension(path)).Replace(".", "");
          text = text.Replace("%%FILENAME%%", Path.GetFileName(path));
          text = text.Replace("%%FILEPATH%%", path);
          text = text.Replace("%%LOCALURL%%", newValue);
          text = text.Replace("%%TIMESTAMP%%", StringHandler.timestamp());
        }
        catch (Exception ex)
        {
          string message = ex.Message.ToString();
          MessageBox.Show(Lib.OutputError(message));
          return ex.Message.ToString();
        }
        return text;
      }
      return text;
    }

    public static Dictionary<string, string> Get_Values(string param, char delim1 = ';', char delim2 = '=')
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      Dictionary<string, string> result;
      try
      {
        //string[] array = param.Split(new char[]{';'});
        string[] array = param.Split(delim1);

        string[] array2 = array;
        for (int i = 0; i < array2.Length; i++)
        {
          string text = array2[i];
          //string[] array3 = text.Split(new char[]{'='});
          string[] array3 = text.Split(delim2);
          dictionary.Add(array3[0], array3[1]);
        }
        result = dictionary;
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
        result = null;
      }
      return result;
    }

    public static string Put_Values(Dictionary<string, string> dict, string delim1, string delim2)
    {
      string empty = string.Empty;
      List<string> list = null;
      foreach (KeyValuePair<string, string> current in dict)
      {
        list.Add(current.Key + delim2 + current.Value);
      }
      return string.Join(delim1, list.ToArray());
    }

    public static Point Ptr2Pos(string str, int pos)
    {
      int num = 1;
      int num2 = 0;
      int num3;
      while ((num3 = str.IndexOf('\n', num2)) < pos && num3 > -1)
      {
        num2 = num3 + 1;
        num++;
      }
      int num4 = pos - num2 + 1;
      return new Point(num4 - 1, num - 1);
    }

    public static int Pos2Ptr(string str, Point point)
    {
      int num = 0;
      string[] array = str.Split(new char[]
      {
        '\n'
      });
      for (int i = 0; i < point.Y; i++)
      {
        num += array[i].Length + 1;
      }
      return num + point.X;
    }

    public static int LineFromPos(string S, int Pos)
    {
      int num = 1;
      for (int i = 0; i <= Pos - 1; i++)
      {
        if (S[i] == '\n')
        {
          num++;
        }
      }
      return num;
    }

    public static string GetLine(string str, int row)
    {
      string[] array = str.Split(new char[]
      {
        '\n'
      });
      return array[row].TrimEnd(new char[0]);
    }

    public static string GetLineFromPtr(string str, int ptr)
    {
      return StringHandler.GetLine(str, StringHandler.Ptr2Pos(str, ptr).Y);
    }

    public static int GetBlockLevel(string str, int ptr, char delim)
    {
      int num = 0;
      for (int i = 0; i < ptr; i++)
      {
        if (delim == '{' || delim == '}')
        {
          if (str[i].ToString() == "{")
          {
            num++;
          }
          if (str[i].ToString() == "}")
          {
            num--;
          }
        }
        else if (delim == '[' || delim == ']')
        {
          if (str[i].ToString() == "[")
          {
            num++;
          }
          if (str[i].ToString() == "]")
          {
            num--;
          }
        }
        else if (delim == '(' || delim == ')')
        {
          if (str[i].ToString() == "(")
          {
            num++;
          }
          if (str[i].ToString() == ")")
          {
            num--;
          }
        }
      }
      return num;
    }

    public static int GetBlockLevel(string str, int ptr)
    {
      int num = 0;
      for (int i = 0; i < ptr; i++)
      {
        if (str[i].ToString() == "{")
        {
          num++;
        }
        if (str[i].ToString() == "}")
        {
          num--;
        }
      }
      return num;
    }

    public static List<string> ListIdentifiers(string text)
    {
      List<string> list = new List<string>();
      string[] array = text.Split(new char[]
      {
        '\n'
      });
      string arg_1F_0 = string.Empty;
      for (int i = 0; i < array.Length; i++)
      {
        if (StringHandler.GetDataType(array[i]) != string.Empty)
        {
          list.Add(StringHandler.GetPrototype(array[i]));
        }
      }
      return list;
    }

    public static string GetDataType(string data)
    {
      string empty = string.Empty;
      if (data.ToLower().IndexOf("using") > -1)
      {
        return "using";
      }
      if (data.ToLower().IndexOf("import") > -1)
      {
        return "import";
      }
      if (data.ToLower().IndexOf("delegate") > -1)
      {
        return "delegate";
      }
      if (data.ToLower().IndexOf("class") > -1)
      {
        return "class";
      }
      if (data.ToLower().IndexOf("struct") > -1)
      {
        return "struct";
      }
      if (data.ToLower().IndexOf("interface") > -1)
      {
        return "interface";
      }
      if (data.ToLower().IndexOf("enum") > -1)
      {
        return "enum";
      }
      if (data.ToLower().IndexOf("const") > -1)
      {
        return "const";
      }
      if (data.ToLower().IndexOf("public") <= -1 && data.ToLower().IndexOf("private") <= -1 && data.ToLower().IndexOf("internal") <= -1 && data.ToLower().IndexOf("protected") <= -1)
      {
        return empty;
      }
      if (data.ToLower().IndexOf("(") > -1 && data.ToLower().IndexOf("=") < 0 && data.ToLower().IndexOf(";") < 0)
      {
        return "method";
      }
      return "variable";
    }

    public static string GetPrototype(string text)
    {
      if (text.IndexOf("//") > -1)
      {
        text = text.Substring(0, text.IndexOf("//"));
      }
      if (text.IndexOf("/*") > -1)
      {
        text = text.Substring(0, text.IndexOf("/*"));
      }
      text = text.Replace(";", "");
      return text.Trim();
    }

    public static int[] GetBlock(string text, int pos)
    {
      int num = 0;
      int[] array = new int[3];
      int num2 = text.IndexOf('{', pos);
      if (num2 < -1)
      {
        return null;
      }
      array[0] = num2;
      num++;
      for (int i = num2 + 1; i < text.Length; i++)
      {
        if (text[i].ToString() == "{")
        {
          num++;
        }
        if (text[i].ToString() == "}")
        {
          num--;
        }
        if (num == 0)
        {
          array[1] = i;
          return array;
        }
      }
      return null;
    }

    public static int[] GetBlock(string text, int pos, char delim)
    {
      int num = 0;
      int[] array = new int[3];
      int num2 = text.IndexOf('{', pos);
      if (num2 < -1)
      {
        return null;
      }
      array[0] = num2;
      num++;
      for (int i = num2 + 1; i < text.Length; i++)
      {
        if (delim == '{' || delim == '}')
        {
          if (text[i].ToString() == "{")
          {
            num++;
          }
          if (text[i].ToString() == "}")
          {
            num--;
          }
        }
        else if (delim == '[' || delim == ']')
        {
          if (text[i].ToString() == "[")
          {
            num++;
          }
          if (text[i].ToString() == "]")
          {
            num--;
          }
        }
        else if (delim == '(' || delim == ')')
        {
          if (text[i].ToString() == "(")
          {
            num++;
          }
          if (text[i].ToString() == ")")
          {
            num--;
          }
        }
        if (num == 0)
        {
          array[1] = i;
          return array;
        }
      }
      return null;
    }

    public static int FindForwardMatchingBrace(string text, int pos)
    {
      int num = 0;
      int num2 = text.IndexOf('{', pos);
      if (num2 < -1)
      {
        return -1;
      }
      num++;
      for (int i = num2 + 1; i < text.Length; i++)
      {
        if (text[i].ToString() == "{")
        {
          num++;
        }
        if (text[i].ToString() == "}")
        {
          num--;
        }
        if (num == 0)
        {
          return i;
        }
      }
      return -1;
    }

    public static string GetToken(ref string text, ref int ptr)
    {
      string text2 = string.Empty;
      while (char.IsWhiteSpace(text[ptr]) && !string.IsNullOrEmpty(text[ptr].ToString()))
      {
        ptr++;
      }
      if (ptr == text.Length)
      {
        return string.Empty;
      }
      if ("{}".IndexOf(text[ptr].ToString()) > -1)
      {
        ptr++;
        return text[ptr - 1].ToString();
      }
      while (text[ptr] == '/')
      {
        if (text[ptr + 1] != '*' && text[ptr + 1] != '/')
        {
          break;
        }
        if (text[ptr + 1] == '*')
        {
          ptr += 2;
          while (true)
          {
            if (text[ptr] == '*')
            {
              ptr++;
              if (text[ptr] == '/')
              {
                break;
              }
            }
            else
            {
              ptr++;
            }
          }
          ptr++;
        }
        else if (text[ptr + 1] == '/')
        {
          ptr += 2;
          while (text[ptr] != '\n')
          {
            ptr++;
          }
        }
        while (char.IsWhiteSpace(text[ptr]) && !string.IsNullOrEmpty(text[ptr].ToString()))
        {
          ptr++;
        }
      }
      while (char.IsWhiteSpace(text[ptr]) && !string.IsNullOrEmpty(text[ptr].ToString()))
      {
        ptr++;
      }
      if ("!<>=".IndexOf(text[ptr].ToString()) > -1)
      {
        char c = text[ptr];
        if (c != '!')
        {
          switch (c)
          {
            case '<':
              if (text[ptr + 1] == '<' && text[ptr + 2] == '=')
              {
                ptr++;
                ptr++;
                ptr++;
                return "<<=";
              }
              if (text[ptr + 1] == '=')
              {
                ptr++;
                ptr++;
                return "<=";
              }
              if (text[ptr + 1] == '<')
              {
                ptr++;
                ptr++;
                return "<<";
              }
              ptr++;
              return "<";
            case '=':
              if (text[ptr + 1] == '=')
              {
                ptr++;
                ptr++;
                return "==";
              }
              break;
            case '>':
              if (text[ptr + 1] == '>' && text[ptr + 2] == '=')
              {
                ptr++;
                ptr++;
                ptr++;
                return ">>=";
              }
              if (text[ptr + 1] == '=')
              {
                ptr++;
                ptr++;
                return ">=";
              }
              if (text[ptr + 1] == '>')
              {
                ptr++;
                ptr++;
                return ">>";
              }
              ptr++;
              return ">";
          }
        }
        else if (text[ptr + 1] == '=')
        {
          ptr++;
          ptr++;
          return "!=";
        }
      }
      if ("+-*/%^=;(),'?:~!&|".IndexOf(text[ptr].ToString()) > -1)
      {
        char c2 = text[ptr];
        switch (c2)
        {
          case '%':
            if (text[ptr + 1] == '=')
            {
              ptr++;
              ptr++;
              return "%=";
            }
            ptr++;
            return "%";
          case '&':
            if (text[ptr + 1] == '&')
            {
              ptr++;
              ptr++;
              return "&&";
            }
            if (text[ptr + 1] == '=')
            {
              ptr++;
              ptr++;
              return "&=";
            }
            ptr++;
            return "&";
          case '\'':
          case '(':
          case ')':
          case ',':
          case '.':
            break;
          case '*':
            if (text[ptr + 1] == '=')
            {
              ptr++;
              ptr++;
              return "*=";
            }
            ptr++;
            return "*";
          case '+':
            if (text[ptr + 1] == '+')
            {
              ptr++;
              ptr++;
              return "++";
            }
            if (text[ptr + 1] == '=')
            {
              ptr++;
              ptr++;
              return "+=";
            }
            ptr++;
            return "+";
          case '-':
            if (text[ptr + 1] == '-')
            {
              ptr++;
              ptr++;
              return "--";
            }
            if (text[ptr + 1] == '=')
            {
              ptr++;
              ptr++;
              return "-=";
            }
            if (text[ptr + 1] == '>')
            {
              ptr++;
              ptr++;
              return "->";
            }
            ptr++;
            return "-";
          case '/':
            if (text[ptr + 1] == '=')
            {
              ptr++;
              ptr++;
              return "/=";
            }
            ptr++;
            return "/";
          default:
            if (c2 != '^')
            {
              if (c2 == '|')
              {
                if (text[ptr + 1] == '|')
                {
                  ptr++;
                  ptr++;
                  return "||";
                }
                if (text[ptr + 1] == '=')
                {
                  ptr++;
                  ptr++;
                  return "|=";
                }
                ptr++;
                return "|";
              }
            }
            else
            {
              if (text[ptr + 1] == '=')
              {
                ptr++;
                ptr++;
                return "^=";
              }
              ptr++;
              return "^";
            }
            break;
        }
        ptr++;
        return text[ptr - 1].ToString();
      }
      if (text[ptr] == '"')
      {
        ptr++;
        while (text[ptr] != '"' && text[ptr] != '\n')
        {
          text2 += text[ptr++].ToString();
        }
        if (text[ptr] == '\n')
        {
          MessageBox.Show("シンタックスエラー");
        }
        ptr++;
        return "\"" + text2 + "\"";
      }
      if (char.IsDigit(text[ptr]))
      {
        text2 = string.Empty;
        while (!StringHandler.isdelim(text[ptr]))
        {
          text2 += text[ptr].ToString();
          ptr++;
        }
        return text2;
      }
      if (char.IsLetter(text[ptr]))
      {
        text2 = string.Empty;
        while (!StringHandler.isdelim(text[ptr]))
        {
          text2 += text[ptr].ToString();
          ptr++;
        }
        return text2;
      }
      return text2;
    }

    public static bool isdelim(char c)
    {
      return " !;,+-<>'/*%^=()".IndexOf(c.ToString()) > -1 || c == '\t' || c == '\r' || c == '\0';
    }

    public static string GetTokenType(string token)
    {
      long num = 0L;
      bool flag = long.TryParse(token, out num);
      if (flag)
      {
        return "number";
      }
      if (token.StartsWith("\"") && token.EndsWith("\""))
      {
        return "string";
      }
      if (token == "}" || token == "{")
      {
        return "block";
      }
      if (StringHandler.Operators.Contains(token))
      {
        return "operator";
      }
      if ("+-*/%^=;(),'?:~!&|".IndexOf(token) > -1)
      {
        return "delimiter";
      }
      if (StringHandler.AccessModifiers.Contains(token))
      {
        return "accessmodifier";
      }
      if (StringHandler.Modifiers.Contains(token))
      {
        return "modifier";
      }
      if (StringHandler.KeyWords.Contains(token))
      {
        return "keyword";
      }
      if (StringHandler.Directives.Contains(token))
      {
        return "directive";
      }
      if (token != string.Empty)
      {
        return "identifier";
      }
      return "unknown";
    }

    public static void StackTrace(string text, int ptr)
    {
      MessageBox.Show(ptr.ToString());
      MessageBox.Show(text[ptr].ToString());
      Point point = StringHandler.Ptr2Pos(text, ptr);
      MessageBox.Show(string.Format("行:{0} 列:{1}", point.Y + 1, point.X + 1));
      MessageBox.Show(StringHandler.GetLine(text, point.Y));
    }

    /// <summary>
    /// ARGB16進カラーcodeをColorに変換する
    /// </summary>
    /// <param name="colorCode">#00000000</param>
    /// <returns></returns>
    /// あまりきれいなコードとは言えませんが、上記コードで16進数カラーコードをColorで返すことができます。
    /// http://dobon.net/vb/dotnet/graphics/getcolorfromhtml.html
    public static Color GetArbgColor(string colorCode)
    {
      try
      {
        // #で始まっているか
        var index = colorCode.IndexOf("#", StringComparison.Ordinal);
        // 文字数の確認と#がおかしな位置にいないか
        if (colorCode.Length != 9 || index != 0)
        {
          // 例外を投げる
          throw new ArgumentOutOfRangeException();
        }

        // 分解する
        var alpha = Convert.ToByte(Convert.ToInt32(colorCode.Substring(1, 2), 16));
        var red = Convert.ToByte(Convert.ToInt32(colorCode.Substring(3, 2), 16));
        var green = Convert.ToByte(Convert.ToInt32(colorCode.Substring(5, 2), 16));
        var blue = Convert.ToByte(Convert.ToInt32(colorCode.Substring(7, 2), 16));

        return Color.FromArgb(alpha, red, green, blue);
      }
      catch (ArgumentOutOfRangeException)
      {
        throw new ArgumentOutOfRangeException("GetArbgColor : colorCode OutOfRange");
      }
      catch (ArgumentNullException)
      {
        throw new ArgumentOutOfRangeException("GetArbgColor : \"#\" not found");
      }
      catch (AggregateException)
      {
        throw new ArgumentOutOfRangeException("GetArbgColor : \"#\" not found");
      }
    }

    public static String GetCurrentWord(Int32 pos, String text)
    {
      //String word = String.Empty;
      //text = "this is a beautiful sleeping lady.";
      //pos = 12;
      //int start = text.Substring(0, pos).LastIndexOf(" ");
      //int length = pos-start+text.Substring(pos).IndexOf(" ");
      //word = text.Substring(start, length);
      return text.Substring(text.Substring(0, pos).LastIndexOf(" "),
        pos - text.Substring(0, pos).LastIndexOf(" ") + text.Substring(pos).IndexOf(" ")); ;
    }

  }
}
