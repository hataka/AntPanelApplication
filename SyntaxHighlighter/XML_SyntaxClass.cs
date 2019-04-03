using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CommpmLibrary
{
  public class XML_SyntaxClass
  {
    private static String php_keywords = "and array as bool boolean break case cfunction"
      + " class const continue declare default die directory do double echo else elseif"
      + " empty enddeclare endfor endforeach endif endswitch endwhile eval exit extends false float"
      + " for foreach function global if include include_once int integer isset list new null object"
      + " old_function or parent print real require require_once resource return static stdclass"
      + " string switch true unset use var while xor abstract catch clone exception final implements"
      + " interface php_user_filter private protected public this throw try"
      + " __class__ __file__ __function__ __line__ __method__ __sleep __wakeup";
    public static string javascript_keywords =
          "enum throw throws class extends try const finally long private package final"
          + " protected public byte int short char interface static double enum float package"
          + " boolean implements while function export if in assign ref do for import var"
          + " else switch with abstract native catch return default instanceOf new goto delete"
          + " alert arguments break callee caller captureEvents clearInterval clearTimeout close"
          + " closed comment confirm constructor continue debugger defaultStatus document"
          + " escape eval export false find focus frames getClass history home Infinity innerHeight"
          + " innerWidth isFinite isNaN isNan java label length Link location Location locationbar"
          + " menubar moveBy moveTo name NaN navigate navigator netscape null onBlur onError"
          + " onFocus onLoad onUnload open opener outerHeight outerWidth pageXoffset pageYoffset"
          + " parent parseFloat parseInt personalbar print prompt prototype releaseEvents resizeBy"
          + " resizeTo routeEvent scroll scrollbars scrollBy scrollTo Select self setInterval"
          + " setTimeout status statusbar stop sun super synchronized taint this toolbar top toString"
          + " transient true typeof undefined unescape untaint unwatch valueOf void watch window"
          + " ActiveXObject Anchor Area Array blur Boolean Button Date Checkbox Document Element"
          + " FileUpload Form Frame Function Hidden History Image JavaArray JavaClass JavaObject JavaPackage"
          + " Math MimeType Navigator Number Object Option Packages Password Plugin Radio RegExp"
          + " Text Textarea Submit Window Reset String XMLHttpRequest";

    public static string php_keywords_regex = @"\b(" + php_keywords.Replace(" ", "|") + @")\b";
    public static string javascript_keywords_regex = @"\b(" + javascript_keywords.Replace(" ", "|") + @")\b";
    public static string block_comment_regex = @"(<!--[\s\S]*?-->)";
    //public static string csharp_line_comment = @"(//[^/].+)";
    //public static string csharp_doc_comment = @"(///.+)";
    public static string block_cdata_regex = @"<\!\[CDATA\[[\s\S]*?\]\]>";
    public static string strings_regex = "\".+?\"";

    // 要検討
    public static string block_tag_regex = @"<(?<headingtag>.*)>.*</\k<headingtag>>"
      +"|"+ @"<\w.*>.*?/>";

    //var reg = @"<a\s+[^>]*href\s*=\s*[""'](?<href>[^""']*)[""'][^>]*>(?<text>[^<]*)</a>";
    public static Color defaultColor = Color.FromArgb(0x000099);// Color.FromArgb(0x000000);
    public static Color defaultBackColor = Color.White;


    public static Dictionary<String, HighlightClass> Coloring = new Dictionary<String, HighlightClass>()
    {
      //重くなるのでコメントアウト
      //{ "php_keywords", new HighlightClass("php_keywords",php_keywords_regex, Color.FromArgb(0x0000ff))  },
      //{ "javascript_keywords", new HighlightClass("javascript_keywords",javascript_keywords_regex, Color.FromArgb(0x000099))},
      //{ "tag", new HighlightClass("block_tag", block_tag_regex, Color.FromArgb(0x000099))},
      { "defaultColor", new HighlightClass("primary_keywordss",String.Empty, defaultColor)  },
      { "defaultBackColor", new HighlightClass("primary_keywordss",String.Empty, defaultBackColor)  },
      { "strings", new HighlightClass("strings",strings_regex,Color.FromArgb(0x009900)) },
      { "block_cdata", new HighlightClass("block_cdata",block_cdata_regex,Color.FromArgb(0x800000)) },
      { "block_comment", new HighlightClass("block_comment", block_comment_regex, Color.FromArgb(0x808080))}
    };


  }
}
/*----------------------


<?xml version="1.0" encoding="utf-8"?>
<Scintilla xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <keyword-classes>
    <keyword-class name="sgml-and-dtd-keywords">
      ELEMENT DOCTYPE ATTLIST ENTITY NOTATION
    </keyword-class>
    <keyword-class name="php-keywords">
      and array as bool boolean break case cfunction class const continue declare default die directory do double 
      echo else elseif empty enddeclare endfor endforeach endif endswitch endwhile eval exit extends false float 
      for foreach function global if include include_once int integer isset list new null object old_function or 
      parent print real require require_once resource return static stdclass string switch true unset use var while 
      xor abstract catch clone exception final implements interface php_user_filter private protected public this
      throw try __class__ __file__ __function__ __line__ __method__ __sleep __wakeup
    </keyword-class>
    </keyword-class>
  </keyword-classes>
  <languages>
    <language name="xml">
      <lexer name="xml" style-bits="7" />
      <character-class><![CDATA[qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789-_$]]></character-class>

case 



fda,fdm,nmml,plist,dae,lime,resx,csproj</file-extensions>
      <comment-start><![CDATA[<!--]]></comment-start>
      <comment-end><![CDATA[-->]]></comment-end>
      <use-keywords>
        <keyword key="1" class="javascript-keywords" />
        <keyword key="4" class="php-keywords" />
        <keyword key="5" class="sgml-and-dtd-keywords" />
      </use-keywords>
      <editor-style caret-fore="0x000000" caretline-back="0xececec" selection-fore="0xffffff" selection-back="0x3399ff" colorize-marker-back="true" margin-fore="0xffffff" margin-back="0xffffff" marker-back="0xbcbcbc" />
      <use-styles>
        <style name="default" fore="0x000000" back="0xffffff" size="16" font="Consolas" />
        <style name="tag" fore="0x000099" />
        <style name="tagunknown" fore="0xff0000" />
        <style name="attribute" fore="0x000099" />
        <style name="attributeunknown" fore="0x000099" />
        <style name="number" fore="0x000099" />
        <style name="doublestring" fore="0x009900" />
        <style name="singlestring" fore="0x009900" />
        <style name="other" fore="0x000099" />
        <style name="comment" fore="0x808080" />
        <style name="entity" fore="0x000099" />
        <style name="tagend" fore="0x000099" />
        <style name="xmlstart" fore="0x000099" />
        <style name="xmlend" fore="0x000099" />
        <style name="script" fore="0x000080" bold="true" />
        <style name="asp" fore="0xffff00" />
        <style name="aspat" fore="0xffdf00" />
        <style name="cdata" fore="0x800000" />
        <style name="question" fore="0xff0000" bold="true" />
        <style name="value" fore="0x800080" />
        <style name="xccomment" fore="0x808080" />
        <style name="sgml_default" fore="0x000080" />
        <style name="sgml_command" fore="0x000080" bold="true" />
        <style name="sgml_1st_param" fore="0x006600" />
        <style name="sgml_doublestring" fore="0x800000" />
        <style name="sgml_simplestring" fore="0x993300" />
        <style name="sgml_error" fore="0xff0000" />
        <style name="sgml_special" fore="0x3366ff" />
        <style name="sgml_entity" fore="0x333333" />
        <style name="sgml_comment" fore="0x808080" />
        <style name="sgml_1st_param_comment" fore="0x808080" />
        <style name="sgml_block_default" fore="0x000066" />
        <style name="j_comment" fore="0x808080" />
        <style name="j_commentline" fore="0x808080" />
        <style name="j_commentdoc" fore="0x808080" />
        <style name="j_number" fore="0x009900" />
        <style name="j_keyword" fore="0x000099" bold="true" />
        <style name="j_doublestring" fore="0x0000ff" />
        <style name="j_singlestring" fore="0x0000ff" />
        <style name="j_symbols" bold="true" />
        <style name="php_hstring" fore="0xff0000" />
        <style name="php_simplestring" fore="0xff0000" />
        <style name="php_word" fore="0x0000ff" />
        <style name="php_number" fore="0xff0000" />
        <style name="php_comment" fore="0x808080" />
        <style name="php_commentline" fore="0x808080" />
        <style name="php_operator" fore="0x000099" />
        <style name="gdefault" fore="0xefece9" />
        <style name="linenumber" fore="0x666666" />
        <style name="bracelight" fore="0x0000cc" back="0xcdcdff" bold="true" />
        <style name="controlchar" fore="0xffffff" />
        <style name="indentguide" fore="0xc0c0c0" />
        <style name="lastpredefined" fore="0x666666" />
      </use-styles>
      <!-- COLORING_END -->
    </language>
  </languages>
</Scintilla>
*/



