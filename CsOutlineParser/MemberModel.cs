using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSParser.Model;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace CSParser.Model
{
	public class MemberModel
	{
		public string kind = String.Empty;
		public String definition = String.Empty;
		public String id = String.Empty;
		//public MemberModel parent = new MemberModel(); // stack overflow を起こす
		public string parent = String.Empty;
		public Int32 start = 0;
		public Int32 end = 0;
		public String access = String.Empty;
		public String modifier = String.Empty;
		public string type = String.Empty;
		public string name = String.Empty;
		public Position curPos = new Position(1, 1);

		public string content = String.Empty;
		public MemberRegExp regexp = new MemberRegExp();
		public string path = String.Empty;
		public string src = String.Empty;

		public Range innerCode = new Range();
		public List<string> implements = new List<string>();
		public string value = String.Empty;
		public string args = String.Empty;

    #region Properties

    [ReadOnly(true)]
    public string Kind
    {
      get
      {
        return this.kind;
      }
      set
      {
        this.kind = value;
      }
    }

    public string Definition
    {
      get
      {
        return this.definition;
      }
      set
      {
        this.definition = value;
      }
    }

    public string Id
    {
      get
      {
        return this.id;
      }
      set
      {
        this.id = value;
      }
    }

    public string Parent
    {
      get
      {
        return this.parent;
      }
      set
      {
        this.parent = value;
      }
    }

    public Int32 Start
    {
      get
      {
        return this.start;
      }
      set
      {
        this.start = value;
      }
    }

    public Int32 End
    {
      get
      {
        return this.end;
      }
      set
      {
        this.end = value;
      }
    }

    public string Access
    {
      get
      {
        return this.access;
      }
      set
      {
        this.access = value;
      }
    }

    public string Modifier
    {
      get
      {
        return this.modifier;
      }
      set
      {
        this.modifier = value;
      }
    }

    public string Type
    {
      get
      {
        return this.type;
      }
      set
      {
        this.type = value;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public Position CurPos
    {
      get
      {
        return this.curPos;
      }
      set
      {
        this.curPos = value;
      }
    }

    public string Content
    {
      get
      {
        return this.content;
      }
      set
      {
        this.content = value;
      }
    }

    public MemberRegExp Regexp
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

    public string SrcPath
    {
      get
      {
        return this.path;
      }
      set
      {
        this.path = value;
      }
    }

    public string Src
    {
      get
      {
        return this.src;
      }
      set
      {
        this.src = value;
      }
    }

    public Range InnerCode
    {
      get
      {
        return this.innerCode;
      }
      set
      {
        this.innerCode = value;
      }
    }

    public List<string> Implements
    {
      get
      {
        return this.implements;
      }
      set
      {
        this.implements = value;
      }
    }

    public string Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    public string Args
    {
      get
      {
        return this.args;
      }
      set
      {
        this.args = value;
      }
    }
    #endregion

    public MemberModel() {} 
		public MemberModel(string definition, string src)
		{
			this.kind = "member";
			this.definition = definition;
			this.start = src.IndexOf(definition);
			this.curPos = CSParserUtils.GetCurrentPosition(src, start);
			try
			{
				this.end = LookUpEnd(start+this.definition.Length, src);//BraceMatch(src, start);
				this.innerCode = new Range(src.IndexOf('{',start)+1, src.LastIndexOf('}',end)-1);
				this.content = src.Substring(this.innerCode.start, this.innerCode.end - this.innerCode.start);
			}
			catch (Exception ex)
			{
        String errMsg = ex.Message.ToString();
        //MessageBox.Show("brace matching に失敗しました\n" + ex.Message.ToString());
				this.end = start + definition.Length;
				this.content = "";
				this.innerCode = new Range(start,start);
			}
		}

		public MemberModel(string definition, MemberRegExp regexp, string src, string path)
		{
			List<String> variable_exclude = new List<string>() { "using", "return", "as", "is", "throw", "using", "else" };

			this.kind = regexp.name;
			this.regexp = regexp;
			this.definition = definition;
			this.start = src.IndexOf(definition);
			this.curPos = CSParserUtils.GetCurrentPosition(src, start);
			try
			{
				this.end = LookUpEnd(start + this.definition.Length, src);
				this.innerCode = new Range(src.IndexOf('{', start) + 1, src.LastIndexOf('}', end) - 1);
				this.content = src.Substring(this.innerCode.start, this.innerCode.end - this.innerCode.start);
			}
			catch (Exception ex)
			{
        String errMsg = ex.Message.ToString();
        this.end = start + definition.Length;
				this.content = "";
				this.innerCode = new Range(start, start);
			}
			Match match = Regex.Match(definition, regexp.pattern);
			//this.regexp = regexp;
			//this.kind = regexp.name;
			this.path = path;
			this.access = match.Groups["accessor"].Value;
			this.modifier = match.Groups["modifiers"].Value;
			this.type = match.Groups["type"].Value;
			this.name = match.Groups["name"].Value;
			this.args = match.Groups["args"].Value;
			this.id = this.path + "@" + this.start;
			if ((this.kind == "class" || this.kind == "struct" || this.kind == "interface") && !string.IsNullOrEmpty(this.args))
			{
				string[] implements = this.args.Trim().Split(',');
				foreach (string implement in implements) this.implements.Add(implement.Trim());
			}
			// TODO ここの処理 Time-stamp: <2016-05-12 9:33:27 kahata>
			//if (this.kind == "variable")
			//{
					//if (!variable_exclude.Contains(match.Groups["type"].Value)) memberList.Add(member);
			//}
			//else ;//memberList.Add(member);
		}
		
		public int LookUpEnd(int pos, string src)
		{
			int brace = pos;
			//\s 空白文字。改行文字、タブ文字、半角/全角スペース文字など。[\f\n\r\t\v\x85\p{Z}]と同じ。
			while (Regex.IsMatch(src[brace].ToString(), "\\s")) brace++;
			if (src[brace] == '{')
			{
				int end = CSParserUtils.BraceMatch(src, brace);
				return end + 1;
			}
			else return this.start+ this.definition.Length;
		}

		public string GetSource(string path)
		{
			string text = string.Empty;
			using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8")))
			{
				text = sr.ReadToEnd();
			}
			return text;
		}
	}
}
