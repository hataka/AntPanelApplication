using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace AntPlugin
{
	[Serializable]
	public class PropertySetting
	{
		public enum TestEnum
		{
			One,
			Two,
			Tree
		}

		private int _integerValue;

		private string _stringValue = "こんにちは";

		private bool _booleanValue;

		private PropertySetting.TestEnum _enumValue;

		private Color _colorValue = Color.Red;

		private Size _size = new Size(10, 10);

		private string _CppFileType = ".c .cpp .cxx .h .hpp .hxx";

		private string _CSharpFileType = ".cs";

		private string _JavaFileType = ".java";

		private string _LatexFileType = ".tex";

		private string _RubyFileType = ".rb";

		private string _XmlFileType = ".xml .xsl .htm .html";

		private bool _DrawsEolCode = true;

		private bool _DrawsFullWidthSpace = true;

		private bool _DrawsSpace = true;

		private bool _DrawsTab = true;

		private bool _DrawsEofMark = true;

		private bool _HighlightsCurrentLine = true;

		private bool _ShowsLineNumber = true;

		private bool _ShowsHRuler = true;

		private bool _ShowsDirtBar = true;

		private int _TabWidth = 2;

		private int _LinePadding = 1;

		private int _LeftMargin = 1;

		private string _ViewType = "Proportional";

		private bool _ConvertsFullWidthSpaceToSpace;

		private bool _UsesTabForIndent = true;

		private string _HRulerIndicatorType = "Segment";

		private string _FontName = "ＭＳ ゴシック";

		private int _FontSize = 11;

		[Category("AzukiControl"), DefaultValue(".c .cpp .cxx .h .hpp .hxx"), Description("ここにStringValueの説明を書きます。"), DisplayName("CppFileType")]
		public string CppFileType
		{
			get
			{
				return this._CppFileType;
			}
			set
			{
				this._CppFileType = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(".cs"), Description("ここにStringValueの説明を書きます。"), DisplayName("CSharpFileType")]
		public string CSharpFileType
		{
			get
			{
				return this._CSharpFileType;
			}
			set
			{
				this._CSharpFileType = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(".java"), Description("ここにStringValueの説明を書きます。"), DisplayName("JavaFileType")]
		public string JavaFileType
		{
			get
			{
				return this._JavaFileType;
			}
			set
			{
				this._JavaFileType = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(".tex"), Description("ここにStringValueの説明を書きます。"), DisplayName("LatexFileType")]
		public string LatexFileType
		{
			get
			{
				return this._LatexFileType;
			}
			set
			{
				this._LatexFileType = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(".rb"), Description("ここにStringValueの説明を書きます。"), DisplayName("RubyFileType")]
		public string RubyFileType
		{
			get
			{
				return this._RubyFileType;
			}
			set
			{
				this._RubyFileType = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(".xml .xsl .htm .html"), Description("ここにStringValueの説明を書きます。"), DisplayName("XmlFileType")]
		public string XmlFileType
		{
			get
			{
				return this._XmlFileType;
			}
			set
			{
				this._XmlFileType = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(true), Description("ここにStringValueの説明を書きます。")]
		public bool DrawsEolCode
		{
			get
			{
				return this._DrawsEolCode;
			}
			set
			{
				this._DrawsEolCode = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(true), Description("ここにStringValueの説明を書きます。")]
		public bool DrawsFullWidthSpace
		{
			get
			{
				return this._DrawsFullWidthSpace;
			}
			set
			{
				this._DrawsFullWidthSpace = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(true), Description("ここにStringValueの説明を書きます。")]
		public bool DrawsSpace
		{
			get
			{
				return this._DrawsSpace;
			}
			set
			{
				this._DrawsSpace = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(true), Description("ここにStringValueの説明を書きます。")]
		public bool DrawsTab
		{
			get
			{
				return this._DrawsTab;
			}
			set
			{
				this._DrawsTab = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(true), Description("ここにStringValueの説明を書きます。")]
		public bool DrawsEofMark
		{
			get
			{
				return this._DrawsEofMark;
			}
			set
			{
				this._DrawsEofMark = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(true), Description("ここにStringValueの説明を書きます。")]
		public bool HighlightsCurrentLine
		{
			get
			{
				return this._HighlightsCurrentLine;
			}
			set
			{
				this._HighlightsCurrentLine = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(true), Description("ここにStringValueの説明を書きます。")]
		public bool ShowsLineNumber
		{
			get
			{
				return this._ShowsLineNumber;
			}
			set
			{
				this._ShowsLineNumber = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(true), Description("ここにStringValueの説明を書きます。")]
		public bool ShowsHRuler
		{
			get
			{
				return this._ShowsHRuler;
			}
			set
			{
				this._ShowsHRuler = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(true), Description("ここにStringValueの説明を書きます。")]
		public bool ShowsDirtBar
		{
			get
			{
				return this._ShowsDirtBar;
			}
			set
			{
				this._ShowsDirtBar = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(2), Description("ここにStringValueの説明を書きます。")]
		public int TabWidth
		{
			get
			{
				return this._TabWidth;
			}
			set
			{
				this._TabWidth = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(1), Description("ここにStringValueの説明を書きます。")]
		public int LinePadding
		{
			get
			{
				return this._LinePadding;
			}
			set
			{
				this._LinePadding = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(1), Description("ここにStringValueの説明を書きます。")]
		public int LeftMargin
		{
			get
			{
				return this._LeftMargin;
			}
			set
			{
				this._LeftMargin = value;
			}
		}

		[Category("AzukiControl"), DefaultValue("Proportional"), Description("ここにStringValueの説明を書きます。"), DisplayName("ViewType")]
		public string ViewType
		{
			get
			{
				return this._ViewType;
			}
			set
			{
				this._ViewType = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(false), Description("ここにStringValueの説明を書きます。")]
		public bool ConvertsFullWidthSpaceToSpace
		{
			get
			{
				return this._ConvertsFullWidthSpaceToSpace;
			}
			set
			{
				this._ConvertsFullWidthSpaceToSpace = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(true), Description("ここにStringValueの説明を書きます。")]
		public bool UsesTabForIndent
		{
			get
			{
				return this._UsesTabForIndent;
			}
			set
			{
				this._UsesTabForIndent = value;
			}
		}

		[Category("AzukiControl"), DefaultValue("Segment"), Description("ここにStringValueの説明を書きます。"), DisplayName("HRulerIndicatorType")]
		public string HRulerIndicatorType
		{
			get
			{
				return this._HRulerIndicatorType;
			}
			set
			{
				this._HRulerIndicatorType = value;
			}
		}

		[Category("AzukiControl"), DefaultValue("ＭＳ ゴシック"), Description("ここにStringValueの説明を書きます。"), DisplayName("FontName")]
		public string FontName
		{
			get
			{
				return this._FontName;
			}
			set
			{
				this._FontName = value;
			}
		}

		[Category("AzukiControl"), DefaultValue(11), Description("ここにStringValueの説明を書きます。"), DisplayName("FontSize")]
		public int FontSize
		{
			get
			{
				return this._FontSize;
			}
			set
			{
				this._FontSize = value;
			}
		}

		[Category("表示"), XmlElement("ColorValue")]
		public string ColorValueHtml
		{
			get
			{
				return ColorTranslator.ToHtml(this.ColorValue);
			}
			set
			{
				this.ColorValue = ColorTranslator.FromHtml(value);
			}
		}

		[Category("表示"), XmlIgnore]
		public Color ColorValue
		{
			get
			{
				return this._colorValue;
			}
			set
			{
				this._colorValue = value;
			}
		}

		public int X
		{
			get;
			set;
		}

		public int Y
		{
			get;
			set;
		}

		public string Version
		{
			get;
			set;
		}

		public Point XY
		{
			get;
			set;
		}

		public DateTime UpdateDate
		{
			get;
			set;
		}

		[ReadOnly(true)]
		public int IntegerValue
		{
			get
			{
				return this._integerValue;
			}
			set
			{
				this._integerValue = value;
			}
		}

		[Description("ここにStringValueの説明を書きます。")]
		public string StringValue
		{
			get
			{
				return this._stringValue;
			}
			set
			{
				this._stringValue = value;
			}
		}

		[Browsable(false)]
		public bool BooleanValue
		{
			get
			{
				return this._booleanValue;
			}
			set
			{
				this._booleanValue = value;
			}
		}

		public PropertySetting.TestEnum EnumValue
		{
			get
			{
				return this._enumValue;
			}
			set
			{
				this._enumValue = value;
			}
		}

		public Size Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		public void Save(string filename)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(base.GetType());
			FileStream stream = new FileStream(filename, FileMode.Create);
			xmlSerializer.Serialize(stream, this);
		}

		public PropertySetting Load(string filename)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(base.GetType());
			PropertySetting result;
			try
			{
				FileStream stream = new FileStream(filename, FileMode.Open);
				PropertySetting propertySetting = (PropertySetting)xmlSerializer.Deserialize(stream);
				result = propertySetting;
			}
			catch
			{
				result = new PropertySetting();
			}
			return result;
		}

		private bool ShouldSerializeColorValue()
		{
			return this.ColorValue != Color.Red;
		}
	}
}
