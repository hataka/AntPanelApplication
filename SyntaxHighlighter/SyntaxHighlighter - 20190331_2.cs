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

/*
      foreach (KeyValuePair<string, HighlightClass> kvp in Csharp_HighLightClass.Csharp_Coloring)
      {
         if (kvp.Key.IndexOf("block") > -1) kvp.Value.RegOption = RegexOptions.Multiline;
        else kvp.Value.RegOption = RegexOptions.Singleline;
        kvp.Value.Matches = Regex.Matches(rtb.Text, kvp.Value.Regexp, kvp.Value.RegOption);

        foreach (Match m in kvp.Value.Matches)
        {
          rtb.SelectionStart = m.Index;
          rtb.SelectionLength = m.Length;
          rtb.SelectionColor = kvp.Value.Color;
        }
        //hilighter.Add(kvp.Value);
      }
*/
      
      /*
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
      
     */ 
      
      
      
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

      foreach (KeyValuePair<string, HighlightClass> kvp in Csharp_HighLightClass.Csharp_Coloring)
      {
        if (kvp.Key.IndexOf("block") > -1) kvp.Value.RegOption = RegexOptions.Multiline;
        // bugfix
        //else kvp.Value.RegOption = RegexOptions.Singleline;
        kvp.Value.Matches = Regex.Matches(rtb.Text, kvp.Value.Regexp, kvp.Value.RegOption);

        foreach (Match m in kvp.Value.Matches)
        {
          rtb.SelectionStart = m.Index;
          rtb.SelectionLength = m.Length;
          rtb.SelectionColor = kvp.Value.Color;
          MessageBox.Show(m.Index.ToString() + " : " + m.Length.ToString()
            +" : " + m.Value.ToString(), kvp.Value.Color.ToString());
        }
      }






      /*
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

      */


      // restoring the original colors, for further writing
      rtb.SelectionStart = originalIndex;
      rtb.SelectionLength = originalLength;
      rtb.SelectionColor = originalColor;

      // giving back the focus
      rtb.Focus();


    }

  }
}
