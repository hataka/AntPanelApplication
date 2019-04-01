using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static CommonLibrary.Csharp_SyntaxClass;
using CommpmLibrary;

namespace CommonLibrary.Controls
{
  public partial class SyntaxHighlighter : UserControl
  {
    public static RichTextBox textBox2 = new RichTextBox();
    public static List<String> csharp_syntaxfile = new List<String> { ".cs", ".java", ".gradle",".js" };
    public static List<String> xml_syntaxfile 
      = new List<String> { ".xml", ".xsl", ".mxml", ".fdp", ".html", ".htm", ".wsf", ".asx"
        , ".wax", ".resx", ".csproj" };

    public SyntaxHighlighter()
    {
      InitializeComponent();
    }

    public static void Highlight(RichTextBox rtb,String language ="")
    {
      //MessageBox.Show(rtb.AccessibleName);
      String path = rtb.AccessibleName;
      // saving the original caret position + forecolor
      int originalIndex = rtb.SelectionStart;
      int originalLength = rtb.SelectionLength;
      Color originalColor = Color.Black;
      Dictionary<String, HighlightClass> Coloring = new Dictionary<String, HighlightClass>();

      if (language != "") { }
      else
      {
        if (csharp_syntaxfile.Contains(System.IO.Path.GetExtension(path).ToLower()))
        {
          originalColor = Csharp_SyntaxClass.defaultColor;
          Coloring = Csharp_SyntaxClass.Coloring;
        }
        if (xml_syntaxfile.Contains(System.IO.Path.GetExtension(path).ToLower()))
        {
          originalColor = XML_SyntaxClass.defaultColor;
          Coloring = XML_SyntaxClass.Coloring;
        }
      }
      // MANDATORY - focuses a label before highlighting (avoids blinking)

      textBox2.Focus();
      
      
      // 背景色 http://d.hatena.ne.jp/S_Amemiya/20100107/1262872794
      // richTextBox.SelectionBackColor = Color.Yellow; // 色を設定
      // 文字を斜体にする https://dobon.net/vb/dotnet/control/rtbchangecolorandfont.html
      //ItalicをFontStyleに追加したFontを作成する
      //Font fnt = new Font(baseFont.FontFamily,
      //    baseFont.Size,
      //    baseFont.Style | FontStyle.Italic);
      //      //Fontを変更する
      //      RichTextBox1.SelectionFont = fnt;
      // removes any previous highlighting (so modified words won't remain highlighted)
      rtb.SelectionStart = 0;
      rtb.SelectionLength = rtb.Text.Length;
      rtb.SelectionColor = originalColor;

      foreach (KeyValuePair<string, HighlightClass> kvp in Coloring)
      {
        if (kvp.Key.IndexOf("block") > -1)
        {
          kvp.Value.RegOption = RegexOptions.Multiline;
          kvp.Value.Matches = Regex.Matches(rtb.Text, kvp.Value.Regexp, RegexOptions.Multiline);
        }
        else
        {
          kvp.Value.Matches = Regex.Matches(rtb.Text, kvp.Value.Regexp);
        }
        foreach (Match m in kvp.Value.Matches)
        {
          rtb.SelectionStart = m.Index;
          rtb.SelectionLength = m.Length;
          rtb.SelectionColor = kvp.Value.Color;
        }
      }
      // restoring the original colors, for further writing
      rtb.SelectionStart = originalIndex;
      rtb.SelectionLength = originalLength;
      rtb.SelectionColor = originalColor;

      // giving back the focus
      rtb.Focus();
    }



    /*
    public static void Highlight(RichTextBox rtb ,String language = "")
    {
      String path = rtb.AccessibleName;
      Dictionary<String, HighlightClass> Coloring = new Dictionary<String, HighlightClass>();

      if (language != "") { }
      else
      {
        if (csharp_syntaxfile.Contains(System.IO.Path.GetExtension(path).ToLower()))
        {
          Coloring = Csharp_SyntaxClass.Coloring;
        }
        if (xml_syntaxfile.Contains(System.IO.Path.GetExtension(path).ToLower()))
        {
          Coloring = XML_SyntaxClass.Coloring;
        }
      }
      Highlight(rtb, Coloring);
    }


    public static void Highlight(RichTextBox rtb, Dictionary<String, HighlightClass> Coloring)
    {
      //MessageBox.Show(rtb.AccessibleName);
      String path = rtb.AccessibleName;
      // saving the original caret position + forecolor
      int originalIndex = rtb.SelectionStart;
      int originalLength = rtb.SelectionLength;
      Color originalColor = Color.Black;
      if (csharp_syntaxfile.Contains(System.IO.Path.GetExtension(path).ToLower()))
      {
        originalColor = Csharp_SyntaxClass.defaultColor;
      }
      if (xml_syntaxfile.Contains(System.IO.Path.GetExtension(path).ToLower()))
      {
        originalColor = XML_SyntaxClass.defaultColor;
      }

      // MANDATORY - focuses a label before highlighting (avoids blinking)
      textBox2.Focus();
      // 背景色 http://d.hatena.ne.jp/S_Amemiya/20100107/1262872794
      // richTextBox.SelectionBackColor = Color.Yellow; // 色を設定
      // 文字を斜体にする https://dobon.net/vb/dotnet/control/rtbchangecolorandfont.html
      //ItalicをFontStyleに追加したFontを作成する
      //Font fnt = new Font(baseFont.FontFamily,
      //    baseFont.Size,
      //    baseFont.Style | FontStyle.Italic);
      //      //Fontを変更する
      //      RichTextBox1.SelectionFont = fnt;
      // removes any previous highlighting (so modified words won't remain highlighted)
      rtb.SelectionStart = 0;
      rtb.SelectionLength = rtb.Text.Length;
      rtb.SelectionColor = originalColor;

      foreach (KeyValuePair<string, HighlightClass> kvp in Coloring)
      {
        if (kvp.Key.IndexOf("block") > -1)
        {
          kvp.Value.RegOption = RegexOptions.Multiline;
          kvp.Value.Matches = Regex.Matches(rtb.Text, kvp.Value.Regexp, RegexOptions.Multiline);
        }
        else
        {
          kvp.Value.Matches = Regex.Matches(rtb.Text, kvp.Value.Regexp);
        }
        foreach (Match m in kvp.Value.Matches)
        {
          rtb.SelectionStart = m.Index;
          rtb.SelectionLength = m.Length;
          rtb.SelectionColor = kvp.Value.Color;
        }
      }
      // restoring the original colors, for further writing
      rtb.SelectionStart = originalIndex;
      rtb.SelectionLength = originalLength;
      rtb.SelectionColor = originalColor;

      // giving back the focus
      rtb.Focus();
    }
    */

  }
}
