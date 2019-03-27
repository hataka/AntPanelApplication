// -*- mode: cs -*-  Time-stamp: <2017-04-08 20:06:26 kahata>
/*================================================================
 * title: 
 * file: xmlmanager.cs
 * path: f:\VCSharp\flashdevelop5.1.1-ll\External\Plugins\xmltreemenu\Managers\xmlmanager.cs
 * url:  f:/VCSharp/flashdevelop5.1.1-ll/External/Plugins/xmltreemenu/Managers/xmlmanager.cs
 * created: Time-stamp: <2017-04-08 20:06:26 kahata>
 * revision: $Id$
 * Programmed By: kahata
 * To compile:
 * To run: 
 * link: http://stackoverflow.com/questions/5389525/converting-xelement-into-xmlnode
 * description: 
 *================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace XMLTreeMenu.Managers
{
  public static class XMLManager
  {
    public static List<String>  get_ElementStringByTagName(string name, string xml)
    {
      List<String> elements = new List<string>();

      //パターンを指定してRegexオブジェクトを作成
      Regex r1 = new Regex(@"<!--(.+?)-->",RegexOptions.IgnoreCase | RegexOptions.Singleline);
      //コメントを削除
      xml = r1.Replace(xml, "");
      
      //正規表現パターンとオプションを指定してRegexオブジェクトを作成
      Regex r2 = new Regex(
       // "<"+ name+ "/>|<"+ name + "( +?)(.*?)/>",RegexOptions.IgnoreCase | RegexOptions.Singleline);
      "<" + name + "/>|<" + name + "([^>] +?)/>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
      //"<" + name + "/>",RegexOptions.IgnoreCase | RegexOptions.Singleline);
      //fdpstring内で正規表現と一致する対象をすべて検索
      MatchCollection mc2 = r2.Matches(xml);
      for (int i = 0; i < mc2.Count; i++) elements.Add(mc2[i].Groups[0].Value);

      Regex r3 = new Regex(
        "<"+ name+">(.*?)</"+ name + "( *?)>|<"+ name + "( +?)(.*?)>(.*?)</"+name+ "( *?)>"
        , RegexOptions.IgnoreCase | RegexOptions.Singleline);
      MatchCollection mc3 = r3.Matches(xml);
      for (int i = 0; i < mc3.Count; i++) elements.Add(mc3[i].Groups[0].Value);

      return elements;
      /*
      $xml = preg_replace("'<!--(.+?)-->'si", '', $xml);
		  $output = array();
		  $regexp1 = "<".$name."/>".'|'."<".$name."( +?)(.*?)/>";
  		
		  $count1 = preg_match_all("'".$regexp1."'i",$xml,$matches1);
      for ($i = 0; $i <$count1; $i++) array_push($output,$matches1[0][$i]);

		  $regexp2 = "<".$name.">(.*?)</".$name."( *?)>".'|'."<".$name."( +?)(.*?)>(.*?)</".$name."( *?)>";
		  $count2 = preg_match_all("'".$regexp2."'si",$xml,$matches2);
      for ($i = 0; $i <$count2; $i++) array_push($output,$matches2[0][$i]);
      return $output;
      */
    }

    public static List<XmlNode> get_NodesByTagName(string name, string xml)
    {

      List<String> elements = new List<String>();
      List<XmlNode> nodes = new List<XmlNode>();
      //パターンを指定してRegexオブジェクトを作成
      Regex r1 = new Regex(@"<!--(.+?)-->", RegexOptions.IgnoreCase | RegexOptions.Singleline);
      //コメントを削除
      xml = r1.Replace(xml, "");

      //正規表現パターンとオプションを指定してRegexオブジェクトを作成
      Regex r2 = new Regex(
      // "<"+ name+ "/>|<"+ name + "( +?)(.*?)/>",RegexOptions.IgnoreCase | RegexOptions.Singleline);
      "<" + name + "/>|<" + name + "([^>] +?)/>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
      //"<" + name + "/>",RegexOptions.IgnoreCase | RegexOptions.Singleline);
      //fdpstring内で正規表現と一致する対象をすべて検索
      MatchCollection mc2 = r2.Matches(xml);
      for (int i = 0; i < mc2.Count; i++) elements.Add(mc2[i].Groups[0].Value);

      Regex r3 = new Regex(
        "<" + name + ">(.*?)</" + name + "( *?)>|<" + name + "( +?)(.*?)>(.*?)</" + name + "( *?)>"
        , RegexOptions.IgnoreCase | RegexOptions.Singleline);
      MatchCollection mc3 = r3.Matches(xml);
      for (int i = 0; i < mc3.Count; i++) elements.Add(mc3[i].Groups[0].Value);
      for (int i = 0; i < elements.Count; i++) nodes.Add(GetNode(elements[i]));
      return nodes;
    }

    /// <summary>
    /// http://stackoverflow.com/questions/5389525/converting-xelement-into-xmlnode
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static XmlNode OutterXMLToNode(string xml)
    {
      XmlDocument xmlDoc  = new XmlDocument();
      xmlDoc.LoadXml(xml);
      return xmlDoc.FirstChild;
    }

    public static XmlNode GetNode(string xml)
    {
      return OutterXMLToNode(xml);
    }



    public static String StripWhiteSpace(String xml)
    {
      xml = xml.Replace("\r", "").Replace("\n", "");
      xml = xml.Replace("\t", "");
      xml = Regex.Replace(xml, @">\s+<", "><");
      xml = Regex.Replace(xml, @">\s+", ">");
      xml = Regex.Replace(xml, @"\s+<", "<");
      xml = xml.Trim();
      return xml;
    }


    /*================================================================
     * title: 
     * file: login.cs
     * path: C:\Users\和彦\Desktop\login.cs
     * url:  C:/Users/和彦/Desktop/login.cs
     * created: Time-stamp: <2017-04-09 7:49:28 kahata>
     * revision: $Id$
     * Programmed By: kahata
     * To compile:
     * To run: 
     * link: http://stackoverflow.com/questions/882143/adding-attributes-to-an-xml-node
     * description: 
     *================================================================*/
    /*******************************************
   <Login>
     <id userName="Tushar" passWord="Tushar">
       <Name>Tushar</Name>
       <Age>24</Age>
     </id>
   </Login>
   ********************************************/

    public static void CreateXmlDocument_Test()
    {
      XmlDocument doc = new XmlDocument();
      XmlElement root = doc.CreateElement("Login");
      XmlElement id = doc.CreateElement("id");
      id.SetAttribute("userName", "Tushar");
      id.SetAttribute("passWord", "Tushar");
      XmlElement name = doc.CreateElement("Name");
      name.InnerText = "Tushar";
      XmlElement age = doc.CreateElement("Age");
      age.InnerText = "24";

      id.AppendChild(name);
      id.AppendChild(age);
      root.AppendChild(id);
      doc.AppendChild(root);

      //doc.Save("test.xml");
      MessageBox.Show(doc.OuterXml);
    }


  }


  public static class XmlEx
  {
    /// <summary>
    /// XML ドキュメントを UTF-8 形式の文字列に変換します。
    /// http://techoh.net/get-xml-as-string-from-doc/
    /// XmlDocument doc = new XmlDocument();
    /// doc.CreateXmlDeclaration("1.0", null, null);
    /// ～要素を追加～
    /// 文字列でXMLを取得！
    /// string xml = doc.ToStringXml();
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    public static string ToStringXml(this XmlDocument doc)
    {
      StringWriterUTF8 writer = new StringWriterUTF8();
      doc.Save(writer);
      string r = writer.ToString();
      writer.Close();
      return r;
    }
    /// <summary>
    /// UTF-8 用 StringWriter です。
    /// </summary>
    class StringWriterUTF8 : System.IO.StringWriter
    {
      public override Encoding Encoding
      {
        get { return Encoding.UTF8; }
      }
    }
  }






}
