using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLibrary
{
  public class HighlightClass
  {
    public string name;
    public string text;
    public string language;
    public string keyword;
    public string regexp;
    public RegexOptions regoption;
    public Color color;
    public MatchCollection matches;

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.text = value;
      }
    }
    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        this.text = value;
      }
    }
    public string Keyword
    {
      get
      {
        return this.keyword;
      }
      set
      {
        this.keyword = value;
      }
    }
    public string Regexp
    {
      get
      {
        return this.regexp;
      }
      set
      {
        this.regexp = value;
      }
    }
    public RegexOptions RegOption
    {
      get
      {
        return this.regoption;
      }
      set
      {
        this.regoption = value;
      }
    }
    public Color Color
    {
      get
      {
        return this.color;
      }
      set
      {
        this.color = value;
      }
    }
    public MatchCollection Matches
    {
      get
      {
        return this.matches;
      }
      set
      {
        this.matches = value;
      }
    }

    public HighlightClass()
    { }
    
    public HighlightClass(String kw, string rgex, Color clr)
    {
      this.keyword = kw;
      this.regexp =  rgex;
      this.color = clr;
    }
}







}
