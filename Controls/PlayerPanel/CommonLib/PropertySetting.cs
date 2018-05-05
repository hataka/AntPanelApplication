// -*- mode: cs -*-  Time-stamp: <2011-05-31 17:44:33 kahata>
/*================================================================
 * title: 
 * file: MySetting.cs
 * path: F:\VCSharp\PropertyGrid\PropertyGrid02\MySetting.cs
 * url:  http://localhost/VCSharp/PropertyGrid/PropertyGrid02/MySetting.cs
 * created: Time-stamp: <2011-05-31 17:44:33 kahata>
 * revision: $Id$
 * Programmed By: kahata
 * To compile:
 * To run: 
 * link: http://www.moonmile.net/blog/archives/1328
 * link: http://www.atmarkit.co.jp/fdotnet/dotnettips/288pgridevent/pgridevent.html
 * description: 
 *================================================================*/
using System;
using System.IO;
using System.Xml.Serialization;
using System.Drawing;
using System.ComponentModel;

namespace MDIForm
{
	/// <summary>
	/// 設定用のクラス
	/// </summary>
	[Serializable]
	public class PropertySetting
	{
		public enum TestEnum
		{
			One,
			Two,
			Tree
		}
		private int _integerValue = 0;
		private string _stringValue = "こんにちは";
		private bool _booleanValue = false;
		private TestEnum _enumValue = TestEnum.One;
		private System.Drawing.Color _colorValue = System.Drawing.Color.Red;
		private Size _size = new Size(10, 10);
	
		// 設定を保存
		public void Save(string filename)
		{
			XmlSerializer xs = new XmlSerializer(this.GetType());
			FileStream fs = new FileStream(filename, FileMode.Create);
			xs.Serialize(fs, this);
		}
		// 設定を読み込み
		public PropertySetting Load(string filename)
		{
			XmlSerializer xs = new XmlSerializer(this.GetType());
			try
			{
				FileStream fs = new FileStream(filename, FileMode.Open);
				PropertySetting me = (PropertySetting)xs.Deserialize(fs);
				return me;
			}
			catch
			{
				// 最初の場合は初期値
				return new PropertySetting();
			}
		}
		/// <summary>
		/// ///////////////////////////////////////////////////////////////////////////////
		/// Azuki Control
		/// </summary>
		private string _CppFileType = ".c .cpp .cxx .h .hpp .hxx";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("CppFileType")]
		[DefaultValue(".c .cpp .cxx .h .hpp .hxx")]
		public string CppFileType
		{
			get { return _CppFileType; }
			set { _CppFileType = value; }
		}

		private string _CSharpFileType = ".cs";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("CSharpFileType")]
		[DefaultValue(".cs")]
		public string CSharpFileType
		{
			get { return _CSharpFileType; }
			set { _CSharpFileType = value; }
		}

		private string _JavaFileType = ".java";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("JavaFileType")]
		[DefaultValue(".java")]
		public string JavaFileType
		{
			get { return _JavaFileType; }
			set { _JavaFileType = value; }
		}

		private string _LatexFileType = ".tex";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("LatexFileType")]
		[DefaultValue(".tex")]
		public string LatexFileType
		{
			get { return _LatexFileType; }
			set { _LatexFileType = value; }
		}

		private string _RubyFileType = ".rb";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("RubyFileType")]
		[DefaultValue(".rb")]
		public string RubyFileType
		{
			get { return _RubyFileType; }
			set { _RubyFileType = value; }
		}

		private string _XmlFileType = ".xml .xsl .htm .html";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("XmlFileType")]
		[DefaultValue(".xml .xsl .htm .html")]
		public string XmlFileType
		{
			get { return _XmlFileType; }
			set { _XmlFileType = value; }
		}
		
		private Boolean _DrawsEolCode = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean DrawsEolCode
		{
			get { return _DrawsEolCode; }
			set { _DrawsEolCode = value; }
		}
		
		private Boolean _DrawsFullWidthSpace=true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean DrawsFullWidthSpace
		{
			get { return _DrawsFullWidthSpace; }
			set { _DrawsFullWidthSpace = value; }
		}

		private Boolean _DrawsSpace = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean DrawsSpace
		{
			get { return _DrawsSpace; }
			set { _DrawsSpace = value; }
		}

		private Boolean _DrawsTab = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean DrawsTab
		{
			get { return _DrawsTab; }
			set { _DrawsTab = value; }
		}
		
		private Boolean _DrawsEofMark = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean DrawsEofMark
		{
			get { return _DrawsEofMark; }
			set { _DrawsEofMark = value; }
		}
		
		private Boolean _HighlightsCurrentLine = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean HighlightsCurrentLine
		{
			get { return _HighlightsCurrentLine; }
			set { _HighlightsCurrentLine = value; }
		}
		
		private Boolean _ShowsLineNumber=true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean ShowsLineNumber
		{
			get { return _ShowsLineNumber; }
			set { _ShowsLineNumber = value; }
		}
	
		private Boolean _ShowsHRuler=true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean ShowsHRuler
		{
			get { return _ShowsHRuler; }
			set { _ShowsHRuler = value; }
		}

	
		private Boolean _ShowsDirtBar = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean ShowsDirtBar
		{
			get { return _ShowsDirtBar; }
			set { _ShowsDirtBar = value; }
		}

		private Int32 _TabWidth = 2;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(2)]
		public Int32 TabWidth
		{
			get { return _TabWidth; }
			set { _TabWidth = value; }
		}

		private Int32 _LinePadding = 1;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(1)]
		public Int32 LinePadding
		{
			get { return _LinePadding; }
			set { _LinePadding = value; }
		}

		private Int32 _LeftMargin = 1;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(1)]
		public Int32 LeftMargin
		{
			get { return _LeftMargin; }
			set { _LeftMargin = value; }
		}

		private string _ViewType = "Proportional";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("ViewType")]
		[DefaultValue("Proportional")]
		public string ViewType
		{
			get { return _ViewType; }
			set { _ViewType = value; }
		}

		private Boolean _ConvertsFullWidthSpaceToSpace = false;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(false)]
		public Boolean ConvertsFullWidthSpaceToSpace
		{
			get { return _ConvertsFullWidthSpaceToSpace; }
			set { _ConvertsFullWidthSpaceToSpace = value; }
		}

		private Boolean _UsesTabForIndent = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean UsesTabForIndent
		{
			get { return _UsesTabForIndent; }
			set { _UsesTabForIndent = value; }
		}

		private string _HRulerIndicatorType = "Segment";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("HRulerIndicatorType")]
		[DefaultValue("Segment")]
		public string HRulerIndicatorType
		{
			get { return _HRulerIndicatorType; }
			set { _HRulerIndicatorType = value; }
		}
	
		/*
		ScrollsBeyondLastLine=True
		Antialias=Default
		AutoScrollMargin=3
		CopyLineWhenNoSelection=True
		UseTextForEofMark=True
		WindowWidth=300
		WindowHeight=400
		WindowMaximized=True
	
		*/

		private string _FontName = "ＭＳ ゴシック";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("FontName")]
		[DefaultValue("ＭＳ ゴシック")]
		public string FontName
		{
			get { return _FontName; }
			set { _FontName = value; }
		}

		private Int32 _FontSize = 11;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(11)]
		[DisplayName("FontSize")]
		public Int32 FontSize
		{
			get { return _FontSize; }
			set { _FontSize = value; }
		}

		[Category("表示")]
		[XmlElement("ColorValue")]
		public string ColorValueHtml
		{
			get { return ColorTranslator.ToHtml(ColorValue); }
			set { ColorValue = ColorTranslator.FromHtml(value); }
		}


		//Color clrGrid = Color.FromArgb(0, 0, 0);
		//public Color ClrGrid
		//{
		//	get { return clrGrid; }
		//	set { clrGrid = value; }
		//}
		// 以下 public で設定を羅列
		[Category("表示")]
		[XmlIgnore]
		public System.Drawing.Color ColorValue
		{
			get { return _colorValue; }
			set { _colorValue = value; }
		}
		//ところで、Color型、Font型、Size型などのデフォルト値は上記の方法では指定できません（注）。
		//これらのデフォルトを指定するには、"ShouldSerializeMyProperty"メソッドを使用します。
		//クラスに"ShouldSerializeMyProperty"という名前でbool(Boolean)を返すメソッドを作り、
		//デフォルト値ならFalse、そうでなければTrueを返すようにします。
		//（詳しくはMSDNの「PropertyDescriptor.ShouldSerializeValue メソッド」等をご覧ください。）
		private bool ShouldSerializeColorValue()
		{
			return ColorValue != System.Drawing.Color.Red;
		}
		// 数値
		public int X { get; set; }
		public int Y { get; set; }
		// 文字列
		public string Version { get; set; }
		// 構造体
		public Point XY { get; set; }
		// 更新日時
		public DateTime UpdateDate { get; set; }

		[ReadOnly(true)]
		public int IntegerValue
		{
			get { return _integerValue; }
			set { _integerValue = value; }
		}
		/*
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor),
						typeof(System.Drawing.Design.UITypeEditor))]
		[Description("ここにStringValueの説明を書きます。")]
		public string StringValue
		{
			get { return _stringValue; }
			set { _stringValue = value; }
		}
		*/

		[Browsable(false)]
		public bool BooleanValue
		{
			get { return _booleanValue; }
			set { _booleanValue = value; }
		}

		public TestEnum EnumValue
		{
			get { return _enumValue; }
			set { _enumValue = value; }
		}

		public Size Size
		{
			get { return _size; }
			set { _size = value; }
		}

	
		



	}
}
