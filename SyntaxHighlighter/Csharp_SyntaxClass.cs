using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
  public class Csharp_SyntaxClass
  {
    private static String csharp_primary_keywords = "abstract as base bool break by byte case catch char checked class"
      + " const continue decimal default delegate do double descending explicit event extern else enum false"
      + " finally fixed float for foreach from goto group if implicit in int interface internal into is lock"
      + " long new null namespace object operator out override orderby params private protected public"
      + " readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this"
      + " throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while "
      + "where yield";
    private static String csharp_additional_keywords = "Boolean Byte Char DateTime Decimal Double Int16"
      + " Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void";
    public static string csharp_block_comment = @"/\*([^*]|\*[^/])*?\*/";
    public static string csharp_line_comment = @"(//[^/].+)";
    public static string csharp_doc_comment = @"(///.+)";
    public static string csharp_strings = "\".+?\"";
    public static string csharp_preprocessor = @"(^#.+)";
    public static string primary_keywords = @"\b(" + csharp_primary_keywords.Replace(" ", "|") + @")\b";
    public static string additional_keywords = @"\b(" + csharp_additional_keywords.Replace(" ", "|") + @")\b";

    public static Color defaultColor = Color.Black;
    public static Color defaultBakColor = Color.White;

    public static Dictionary<String, HighlightClass> Coloring = new Dictionary<String, HighlightClass>()
    {
      { "primary_keywords", new HighlightClass("primary_keywordss",primary_keywords, Color.Blue)  },
      { "additional_keywords", new HighlightClass("additional_keywords",additional_keywords, Color.Blue)},
      { "block_comment", new HighlightClass("block_comment", csharp_block_comment, Color.Green)},
      { "line_comment", new  HighlightClass("line_comment", csharp_line_comment,Color.Green)},
      // OK http://dobon.net/vb/dotnet/graphics/getcolorfromhtml.html
      //{ "line_comment", new  HighlightClass("line_comment", csharp_line_comment,ColorTranslator.FromHtml("#FF6347"))},
      { "strings", new HighlightClass("strings",csharp_strings,Color.Brown)}
    };
  }
}

/*
        <style name="default" fore="0x000000" back="0xffffff" size="15" font="Consolas" />
        <style name="comment" fore="0x008000" />
        <style name="commentline" fore="0x008000" />
        <style name="commentdoc" fore="0x008000" />
        <style name="number" fore="0x000099" />
        <style name="word" fore="0x000099" />
        <style name="string" fore="0xa31515" />
        <style name="character" fore="0xa31515" />
        <style name="uuid" />
        <style name="preprocessor" fore="0x000099" />
        <style name="operator" />
        <style name="identifier" />
        <style name="stringeol" />
        <style name="verbatim" />
        <style name="regex" fore="0xff00ff" />
        <style name="commentlinedoc" fore="0x008000" />
        <style name="word2" fore="0x008080" />
        <style name="commentdockeyword" fore="0x800000" />
        <style name="commentdockeyworderror" fore="0xff0000" />
        <style name="globalclass" fore="0x1518ff" />
        <style name="gdefault" fore="0xefece9" />
        <style name="linenumber" fore="0x666666" />
        <style name="bracelight" fore="0x0000cc" back="0xcdcdff" bold="true" />
        <style name="bracebad" bold="true" />
        <style name="controlchar" fore="0xffffff" />
        <style name="indentguide" fore="0xc0c0c0" />
        <style name="lastpredefined" fore="0x666666" />
*/
