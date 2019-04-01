using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static CommonLibrary.Csharp_HighLightClass;

namespace CommonLibrary.Controls
{
  public partial class SyntaxHighlighter : UserControl
  {
    //private RichTextBox codeRichTextBox;
    private static RichTextBox textBox2 = new RichTextBox();

    private static String primary_keywords;
    private static String additional_keywords;
    private static string block_comment;
    private static string line_comment;
    private static string strings;
    
    /*
    private static String  = "abstract as base bool break by byte case catch char checked class"
      + " const continue decimal default delegate do double descending explicit event extern else enum false"
      + " finally fixed float for foreach from goto group if implicit in int interface internal into is lock"
      + " long new null namespace object operator out override orderby params private protected public"
      + " readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this"
      + " throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while "
      + "where yield";
    private static String additional_keywords = "Boolean Byte Char DateTime Decimal Double Int16"
      + " Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void";
    private static string block_comment = @"/\*([^*]|\*[^/])*\*       /";
    private static string line_comment = @"(\/\/.+?$)";
    private static string strings = "\".+?\"";
    */
    public static List<HighlightClass> hilighter = new List<HighlightClass>();
    public SyntaxHighlighter()
    {
      InitializeComponent();
    }

    public static void Highlight(RichTextBox rtb)
    {
      primary_keywords = Csharp_HighLightClass.primary_keywords;
      additional_keywords = Csharp_HighLightClass.additional_keywords;
      block_comment = Csharp_HighLightClass.block_comment;
      line_comment = Csharp_HighLightClass.line_comment;
      strings = Csharp_HighLightClass.strings;


      foreach (KeyValuePair<string, hilit> kvp in Csharp_HighLightClass.Csharp_Coloring)
      {
        HighlightClass hl = new HighlightClass();
        //hl.Text = rtb.Text;
        hl.Name = kvp.Key;
        hl.Keyword = kvp.Key;
        hl.language = "cs";
        hl.Regexp = kvp.Value.regexp;
        if (kvp.Key.IndexOf("block") > -1) hl.RegOption = RegexOptions.Multiline;
        else hl.RegOption = RegexOptions.Singleline;
        hl.Color = kvp.Value.color;
        hl.Matches = Regex.Matches(rtb.Text, hl.Regexp, hl.RegOption);
        hilighter.Add(hl);
      }

      // getting keywords/functions
      // string keywords = @"\b(public|private|partial|static|namespace|class|using|void|foreach|in)\b";
      string keywords = @"\b(" + primary_keywords.Replace(" ","|") +@")\b";
      MatchCollection keywordMatches = Regex.Matches(rtb.Text, keywords);

      // getting types/classes from the text 
      //string types = @"\b(Console)\b";
      string types = @"\b(" + additional_keywords.Replace(" ","|") + @")\b";
      MatchCollection typeMatches = Regex.Matches(rtb.Text, types);

      // getting comments (inline or multiline)
      //string comments = @"(\/\/.+?$|\/\*.+?\*\/)";

      //MatchCollection commentMatches = Regex.Matches(codeRichTextBox.Text, comments, RegexOptions.Multiline);
      MatchCollection commentMatches1 = Regex.Matches(rtb.Text, block_comment, RegexOptions.Multiline);
      MatchCollection commentMatches2 = Regex.Matches(rtb.Text, line_comment, RegexOptions.Multiline);

      // getting strings
      MatchCollection stringMatches = Regex.Matches(rtb.Text, strings);

      
      
      
      
      
      // saving the original caret position + forecolor
      int originalIndex = rtb.SelectionStart;
      int originalLength = rtb.SelectionLength;
      Color originalColor = Color.Black;

      // MANDATORY - focuses a label before highlighting (avoids blinking)
      //titleLabel.Focus();
      textBox2.Focus();

      // removes any previous highlighting (so modified words won't remain highlighted)
      rtb.SelectionStart = 0;
      rtb.SelectionLength = rtb.Text.Length;
      rtb.SelectionColor = originalColor;

      /* 不具合有り
      foreach(HighlightClass h2 in hilighter)
      {
        foreach (Match m in h2.Matches)
        {
          rtb.SelectionStart = m.Index;
          rtb.SelectionLength = m.Length;
          rtb.SelectionColor = h2.Color;
        }

      }
      */
      
      // scanning...
      foreach (Match m in keywordMatches)
      {
        rtb.SelectionStart = m.Index;
        rtb.SelectionLength = m.Length;
        rtb.SelectionColor = Color.Blue;
      }

      foreach (Match m in typeMatches)
      {
        rtb.SelectionStart = m.Index;
        rtb.SelectionLength = m.Length;
        rtb.SelectionColor = Color.DarkCyan;
      }
      foreach (Match m in commentMatches1)
      {
        rtb.SelectionStart = m.Index;
        rtb.SelectionLength = m.Length;
        rtb.SelectionColor = Color.Green;
      }

      foreach (Match m in commentMatches2)
      {
        rtb.SelectionStart = m.Index;
        rtb.SelectionLength = m.Length;
        rtb.SelectionColor = Color.Green;
      }
      foreach (Match m in stringMatches)
      {
        rtb.SelectionStart = m.Index;
        rtb.SelectionLength = m.Length;
        rtb.SelectionColor = Color.Brown;
      }

      // restoring the original colors, for further writing
      rtb.SelectionStart = originalIndex;
      rtb.SelectionLength = originalLength;
      rtb.SelectionColor = originalColor;

      // giving back the focus
      rtb.Focus();


    }

  }
}
