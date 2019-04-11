using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonInterface.CommonLibrary
{
  /// <summary>
  /// C#で コメントの削除
  /// </summary>
  /// http://gushwell.ldblog.jp/archives/52383412.html
  public class DeComment
  {
    delegate void Scan(char c);
    private static string result = "";
    private static Scan eat;

    private static Scan outComment = (c) => {
      if (c == '/')
        eat = preInComment;
      else
      {
        if (c == '"')
          eat = InLiteralString;
         result += c;
      }
    };

    private static Scan preInComment = (c) => {
      if (c == '*')
        eat = inMultiLineComment;
      else if (c == '/')
        eat = inOneLineComment;
      else
      {
        eat = outComment;
        result += "/" + c;
      }
    };

    private static Scan inOneLineComment = (c) => {
      if (c == '\r' || c == '\n')
      {
        eat = outComment;
        result += c;
      }
    };

    private static Scan inMultiLineComment = (c) => {
      if (c == '*')
        eat = preOutMultiLineComment;
    };

    private static Scan preOutMultiLineComment = (c) => {
      if (c == '/')
        eat = outComment;
      else
        eat = inMultiLineComment;
    };

    private static Scan InLiteralString = (c) => {
      if (c == '"')
        eat = outComment;
      else if (c == '\\')
        eat = InLiteralStringEscape;
      result += c;
    };

    private static Scan InLiteralStringEscape = (c) => {
      eat = InLiteralString;
      result += c;
    };

    public string Execute(string line)
    {
      result = "";
      eat = outComment;
      foreach (var c in line)
      {
        eat(c);
      }
      return result;
    }
  }
}
/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
 
namespace Doukaku.Org
{
  class Program
  {
    static void Main(string[] args)
    {
      var cc = new DeComment();
      var text = File.ReadAllText("Sample.cs");
      var s = cc.Execute(text);
      Console.WriteLine(s);
      Console.ReadLine();
    }
  }

}
*/