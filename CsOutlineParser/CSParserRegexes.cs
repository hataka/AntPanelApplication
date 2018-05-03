using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CSParser.Model
{
	class CSParserRegexes
	{
		public static List<string> KeyWords = new List<string>() {"abstract", "as", "base", "bool", "break",
			"byte", "case", "catch", "char","checked", "class", "const", "continue", "decimal", "default", 
			"delegate", "do", "double", "else", "enum", "event", "explicit","extern", "false", "finally",
			"fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal",
			"is","lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
			"private", "protected", "public", "readonly", "ref","return", "sbyte", "sealed", "short", "sizeof",
			"stackalloc", "static","string", "struct", "switch","this", "throw", "true", "try", "typeof","uint",
			"ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void","volatile", "while"};
		/*
		コンテキスト キーワード
		コンテキスト キーワードを使用して、コード内で特定の意味を与えることができます。
		ただし C# ではコンテキスト キーワードは予約語ではありません。 
		partial や where などの一部のコンテキスト キーワードは、複数のコンテキストで特別な意味を持っています。
		*/
		public static List<string> TypeKeyWords = new List<string>() {"bool","byte",  "char", "class",  "decimal",  
			"double", "delegate", "enum", "float", "int", "interface", "long", "namespace","object", "operator",
			 "sbyte", "sealed", "short", "string", "struct", "uint","ulong",  "ushort", "void"};

		public static List<string> OtherKeyWords = new List<string>() { "as", "base", "case", "catch",
			"checked", "continue", "default","fixed", "do", "else","event", "explicit","extern", "false",
			"finally", "for", "foreach", "goto", "if", "implicit", "in","is","lock", "new", "null",
			 "new", "null","ref","return", "sizeof","stackalloc", "switch","this", "throw", "true", "try",
			 "typeof","unchecked", "unsafe","using", "virtual", "volatile", "while"};

		public static List<string> ContextKeyWords = new List<string>() {"add", "alias", "ascending", "async",
			"descending", "dynamic", "from", "get", "global", "group", "into", "join", "let", "orderby",
			"partial", "remove", "select", "set", "value", "var", "where", "yield"};
		
		public static List<string> Modifiers = new List<string>() { "abstract","async","const","event","extern",
			"new","override","partial","readonly","sealed","static","unsafe","virtual","volatile"};
		
		public static List<string> Accessors = new List<string>() { "public", "protected", "internal", "private" };

		// RegExpressions
		// https://gist.github.com/FabienDehopre/5245476
		// definition of a valid C# identifier: http://msdn.microsoft.com/en-us/library/aa664670(v=vs.71).aspx
		private const string FORMATTING_CHARACTER = @"\p{Cf}";
		private const string CONNECTING_CHARACTER = @"\p{Pc}";
		private const string DECIMAL_DIGIT_CHARACTER = @"\p{Nd}";
		private const string COMBINING_CHARACTER = @"\p{Mn}|\p{Mc}";
		private const string LETTER_CHARACTER = @"\p{Lu}|\p{Ll}|\p{Lt}|\p{Lm}|\p{Lo}|\p{Nl}";
		private const string IDENTIFIER_PART_CHARACTER = LETTER_CHARACTER + "|" +
																										 DECIMAL_DIGIT_CHARACTER + "|" +
																										 CONNECTING_CHARACTER + "|" +
																										 COMBINING_CHARACTER + "|" +
																										 FORMATTING_CHARACTER;
		private const string IDENTIFIER_PART_CHARACTERS = "(" + IDENTIFIER_PART_CHARACTER + ")+";
		private const string IDENTIFIER_START_CHARACTER = "(" + LETTER_CHARACTER + "|_)";
		private const string IDENTIFIER_OR_KEYWORD = IDENTIFIER_START_CHARACTER + "(" +
																								 IDENTIFIER_PART_CHARACTERS + ")*";

		public static string nameRegex = IDENTIFIER_OR_KEYWORD;

		public static string accessorRegEx = String.Join("|", Accessors.ToArray());
		public static string nameaccessorRegEx =  "(?<accessor>" + accessorRegEx + ")";
		public static string modifierRegEx = String.Join("|", Modifiers.ToArray());

		public static string namemodifierRegEx = @"(?<modifier>" + modifierRegEx + ")";
		public static string namemodifiersRegEx = @"(?<modifiers>((?<modifier>" + modifierRegEx + @")\s+))";

		//import //using
		public static string usingRegex = @"using\s+(?<name>[\w\.]+)(?=\s*;)";
		public static string importRegex0 = @"import\s+(?<name>[\w\.\*]+)(?=\s*;)";
		public static string importRegex = "(" + usingRegex + ")|(" + importRegex0 + ")";
		public static MemberRegExp importRegExp = new MemberRegExp("import", importRegex);

		//package
		public static string packageRegex = @"package\s+(?<name>[\w\.]+)(?=\s*;)";
		public static MemberRegExp packageRegExp = new MemberRegExp("package", packageRegex);
		
		// namespace
		public static string namespaceRegex = @"namespace\s+(?<name>\S+)(?=\s*\{)";
		public static MemberRegExp namespaceRegExp = new MemberRegExp("namespace", namespaceRegex);
		// class
		/*
		public static string classRegex = nameaccessorRegEx + "?" + namemodifierRegEx + "*"
			+ @"\s+class\s+"
			+ @"(?<name>\S+)\s+"
			+ @"((\:|extends)\s+(?<args>.+)*)*"
		+ @"(?=\s+\{)";
		*/
		
		public static string classRegex = nameaccessorRegEx + "?" + namemodifiersRegEx + "*"
			+ @"\s+class\s+"
			+ @"(?<name>\S+)\s*"
			+ @"((\:|extends)\s+(?<args>.+)*)*"
		+ @"(?=\s*\{)";
		public static MemberRegExp classRegExp = new MemberRegExp("class", classRegex);

		public static string structRegex = nameaccessorRegEx + "?" +@"\s*"+ namemodifiersRegEx + "*"
			+ @"\s+struct\s+"
			+ @"(?<name>\S+)\s*"
			+ @"((\:|extends)\s(?<args>.+)*)*"
		+ @"(?=\s+\{)";
		
		public static MemberRegExp structRegExp = new MemberRegExp("struct", structRegex);
		// interface
		public static string interfaceRegex = nameaccessorRegEx + "?" + namemodifiersRegEx + "*"
			+ @"\s+interface\s+"
			+ @"(?<name>\S+)\s+"
			+ @"((\:|extends)\s+(?<args>.+)*)*"
		+ @"(?=\s+\{)";
		public static MemberRegExp interfaceRegExp = new MemberRegExp("interface", interfaceRegex);
		// field
		public static string fieldRegex = nameaccessorRegEx + namemodifiersRegEx + "*"
		+ @"\s+(((?!class)(?<type>\w+))\s+)(?<name>[\w\d\.]+)(?=\s*[=;])";
		public static MemberRegExp fieldRegExp = new MemberRegExp("field", fieldRegex);
		// property
		public static string propertyRegex = nameaccessorRegEx + namemodifiersRegEx + "*"
		+ @"\s+(((?!class)(?<type>\w+))\s+)(?<name>[\w\d\.]+)(?=\s*\{)";
		public static MemberRegExp propertyRegExp = new MemberRegExp("property", propertyRegex);
		
		// method
		/* backup
		public static string methodRegex = @"(?<accessor>" + accessorRegEx+ @")"
				+ @"\s*(?<modifiers>((?<modifier>" + modifierRegEx
				+ @")\s+))*(?<type>\w+"  //+nameRegex
				+ @")\s+(?<name>\S+)\s*" //+ nameRegex
				+ @"\((?<args>.*)\)(?=\s*\{)";
		*/
		// accessorがない場合を拾わない	
		public static string methodRegex = nameaccessorRegEx + @"\s*"
		+ namemodifiersRegEx + "*"
		//	+ @"(?<modifiers>(" +@"(?<modifier>" + modifierRegEx	+ @")"+ @"\s+))*"
		+ @"(?<type>\w+)"  
		+ @"\s+(?<name>[\w\d]+)\s*" 
		+ @"\s*\((?<args>.*)\)(?=\s*\{)";
		public static MemberRegExp methodRegExp = new MemberRegExp("method", CSParserRegexes.methodRegex);
		
		// MainForm.cs で例外発生
		public static string methodRegex2 = "(" + nameaccessorRegEx + @"\s*)?"//koko//@"(?<accessor>" + accessorRegEx + @")"
		 + @"(?<modifiers>(" + @"(?<modifier>" + modifierRegEx + @")" + @"\s+))*"
		+ @"(?<type>\w+)"
		+ @"\s+(?<name>[\w\d]+)\s*"
		+ @"\s*\((?<args>.*)\)(?=\s*\{)";
		//public static MemberRegExp methodRegExp2 = new MemberRegExp("method2", CSParserRegexes.methodRegex2);
		
		// variable
		//public static string raw_variableRegex = @"(\w+\s+\w+\s*(?=\s*\;))|(\w+\s+\w+\s*=.+(?=\s*\;))";
		//直後に「ABC」も「XYZ」もこないY//
		//Y(?!(ABC|XYZ))
		//public static string raw_variableRegex = "(?=!(" + accessorRegEx + @"\s*))" + namemodifierRegEx + "*"
		public static string raw_variableRegex = namemodifiersRegEx + "*"
		+ @"\s+(((?!class)(?<type>\w+))\s+)(?<name>[\w\d\.]+)(?=\s*[=;])";
		public static MemberRegExp variableRegExp = new MemberRegExp("variable", raw_variableRegex);

		// interfaceproperty
		public static string interfacepropertyRegex = @"\s*(?<type>\w+)\s+(?<name>[\w\d\.]+)(?=\s*{)";
		public static MemberRegExp interfacepropertyRegExp = new MemberRegExp("interfaceproperty", interfacepropertyRegex);

		// interfacemethod
		public static string interfacemethodRegex = @"\s*(?<type>\w+)\s+(?<name>[\w\d\.]+)\((?<args>.*)\)(?=\s*;)";
		public static MemberRegExp interfacemethodRegExp = new MemberRegExp("interfacemethod", interfacemethodRegex);
	
		// delegate [attributes] [modifiers] delegate result-type identifier ([formal-parameters]);
		public static string delegateRegex = nameaccessorRegEx + "?" + namemodifiersRegEx + "*"
			+ @"\s+delegate\s+"
			+ @"(?<type>\w+)\s+"
			+ @"(?<name>[\w\d\.]+)\s*"
			+ @"(\((?<args>.*)\))?"
			+ @"(?=\s*\;)";
		public static MemberRegExp delegateRegExp = new MemberRegExp("delegate", delegateRegex);

		// enum 列挙体
		//[attributes] [modifiers] enum identifier [:base-type] {enumerator-list};
		public static string enumRegex = nameaccessorRegEx + "?" + namemodifiersRegEx + "*"
			 + @"enum\s+"
			 + @"(?<name>[\w\d\.]+)\s*"
			+ @"(\:\s*(?<args>.*))*"
			+ @"(?=\s*\{)";
		public static MemberRegExp enumRegExp = new MemberRegExp("enum", enumRegex);
	
		public static Dictionary<string, MemberRegExp> RegExpDict = new Dictionary<string, MemberRegExp>()
		{
			{"import",		importRegExp},
			{"package",		packageRegExp},
			{"namespace", namespaceRegExp},
			{"class",     classRegExp},
			{"struct",    structRegExp},
			{"interface", interfaceRegExp},
			{"field",			fieldRegExp},
			{"property",	propertyRegExp},
			{"method",    methodRegExp},
			//{"method2",    methodRegExp2},
			{"interfaceproperty",    interfacepropertyRegExp},
			{"interfacemethod",    interfacemethodRegExp},
			{"delegate",    delegateRegExp},
			{"enum",    enumRegExp},
			{"variable",  variableRegExp}
		};
		/// キーの列挙
		///foreach (string key in dict.Keys)
		///{
		///Console.WriteLine("{0} : {1}", key, dict[key]);
		///}
		
		public static List<MemberModel> ScanMembers(string text, string regExp, string src)
		{
			List<MemberModel> memberList = new List<MemberModel>();
			MatchCollection matches = Regex.Matches(text, regExp);

			foreach (Match match in matches)
				memberList.Add(new MemberModel(match.Groups[0].Value, src));
			return memberList;
		}
		
		public static List<MemberModel> ScanMembers(string text, MemberRegExp regexp, string src)
		{
			List<MemberModel> memberList = new List<MemberModel>();
			MatchCollection matches = Regex.Matches(text, regexp.pattern);
			foreach (Match match in matches)
			{
				MemberModel member = new MemberModel(match.Groups[0].Value, src);
				member.regexp = regexp;
				member.kind = regexp.name;
				//member.path = path;
				member.access = match.Groups["accessor"].Value;
				member.modifier = match.Groups["modifiers"].Value;
				member.type = match.Groups["type"].Value;
				member.name = match.Groups["name"].Value;
				member.args = match.Groups["args"].Value;
				if ((member.kind == "class" || member.kind == "struct" || member.kind == "interface") 
					&& !string.IsNullOrEmpty(member.args))
				{
					string[] implements = member.args.Trim().Split(',');
					foreach (string implement in implements) member.implements.Add(implement.Trim());
				}
				memberList.Add(member);
			}
			return memberList;
		}

		public static List<MemberModel> ScanMembers(string text, MemberRegExp regexp, string src, string path)
		{
			List<String> variable_exclude = new List<string>() { "using", "return", "as", "is", "throw", "using", "else" };
			List<MemberModel> memberList = new List<MemberModel>();
			MatchCollection matches = Regex.Matches(text, regexp.pattern);
			foreach (Match match in matches)
			{
				MemberModel member = new MemberModel(match.Groups[0].Value, src);
				member.regexp = regexp;
				member.kind = regexp.name;
				member.path = path;
				member.access = match.Groups["accessor"].Value;
				member.modifier = match.Groups["modifiers"].Value;
				member.type = match.Groups["type"].Value;
				member.name = match.Groups["name"].Value;
				member.args = match.Groups["args"].Value;
				member.id = member.path + "@" + member.start;
				if ((member.kind == "class" || member.kind == "struct" || member.kind == "interface") 
					&& !string.IsNullOrEmpty(member.args))
				{
					string[] implements = member.args.Trim().Split(',');
					foreach (string implement in implements) member.implements.Add(implement.Trim());
				}
				if (member.kind == "variable")
				{
					if(!variable_exclude.Contains(match.Groups["type"].Value)) memberList.Add(member);
				}
				else memberList.Add(member);
			}
			return memberList;
		}

	}

	public class MemberRegExp
	{
		public string name;
		public string pattern;

		public MemberRegExp() { }
		public MemberRegExp(string name, string regExp)
		{
			this.name = name; this.pattern = regExp;
		}
	}
}
