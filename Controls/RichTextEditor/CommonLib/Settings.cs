using System;
using System.Data;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
//using PluginCore.Helpers;
using System.Collections.Generic;

namespace XMLSettings
{
	/// <summary>
	/// Stores the various user settings for the screensaver
	/// </summary>
	/// <remarks>
	/// The class provides two methods to take care of the serialization
	/// of the class for the programmer.
	[Serializable]
	public class Settings
	{
		protected Hashtable settings;
		//private static string companyName;
		//private static string productName;

		#region Constructors
		/// <summary>
		/// Static constructor to retreive the companyName and productName
		/// from the assembly
		/// </summary>
		/*
		static Settings()
		{
			Assembly assembly = typeof(Settings).Assembly;

			AssemblyCompanyAttribute [] acas = (AssemblyCompanyAttribute[]) 
				assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
			/*
			if( acas.Length > 0 )
			{
				AssemblyCompanyAttribute aca = acas[0];
				companyName = aca.Company;
			}
			else
			{
				companyName = "";
			}

			AssemblyProductAttribute [] apas = (AssemblyProductAttribute[]) assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), true);
			if( apas.Length > 0 )
			{
				AssemblyProductAttribute apa = apas[0];
				productName = apa.Product;
			}
			else
			{
				productName = "";
			}

		}
		*/
		/// <summary>
		/// Creates a new instance of the settings class, loading in the default values
		/// </summary>
		public Settings()
		{
			settings = new Hashtable(10);
			LoadDefaultSettings();
		}
		#endregion

		#region Static Methods
/*
		/// <summary>
		/// Loads a serialized instance of the settings from a file, returns
		/// default values if the file doesn't exist or an error occurs
		/// </summary>
		/// <returns>The persisted settings from the file</returns>
		public static Settings LoadSettingsFromFile()
		{
			string userfile;
			try
			{
				Directory.CreateDirectory(SettingsDirectory);
			}
			catch
			{
			}

			userfile = SettingsDirectory + @"\config.dat";
			
			return LoadSettingsFromFile(userfile);
		}
*/
		/// <summary>
		/// Loads a serialized instance of the settings from the specified file
		/// returns default values if the file doesn't exist or an error occurs
		/// </summary>
		/// <returns>The persisted settings from the file</returns>
		public static Settings LoadSettingsFromFile(string filename)
		{
			Settings settings;
			XmlSerializer xs = new XmlSerializer(typeof(Settings));
			
			if( File.Exists(filename) )
			{
				FileStream fs = null;

				try
				{	
					fs = File.Open(filename, FileMode.Open, FileAccess.Read);
				}
				catch
				{
					return new Settings();
				}

				try
				{
					settings = (Settings) xs.Deserialize(fs);
				}
				catch
				{
					settings = new Settings();
				}
				finally
				{
					fs.Close();
				}	
			}
			else
			{
				settings = new Settings();
			}

			return settings;
		}	
/*
		/// <summary>
		/// Persists the settings to a default filename
		/// </summary>
		/// <param name="settings">The instance of the Settings class to persist</param>
		public static void SaveSettingsToFile(Settings settings)
		{
			string userfile;

			try
			{
				Directory.CreateDirectory(SettingsDirectory);
			}
			catch
			{
			}

			userfile = SettingsDirectory + @"\config.dat";
			
			SaveSettingsToFile(userfile, settings);
		}
*/	
		/// <summary>
		/// Persists the settings to the specified filename
		/// </summary>
		/// <param name="file">The filename to use for saving</param>
		/// <param name="settings">The instance of the Settings class to persist</param>
		public static void SaveSettingsToFile(string file, Settings settings)
		{
			FileStream fs = null;
			XmlSerializer xs = new XmlSerializer(typeof(Settings));

			fs = File.Open(file, FileMode.Create, FileAccess.Write);

			try
			{
				xs.Serialize(fs, settings);
			}
			finally
			{
				fs.Close();
			}
		}

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
		public static void Save(string filename, Settings settings)
		{
			XmlSerializer xs = new XmlSerializer(typeof(Settings));
			FileStream fs = new FileStream(filename, FileMode.Create);
			xs.Serialize(fs, settings);
			fs.Close();
			fs.Dispose();
		}

		// 設定を読み込み
		public static Settings Load(string filename)
		{
			XmlSerializer xs = new XmlSerializer(typeof(Settings));
			try
			{
				FileStream fs = new FileStream(filename, FileMode.Open);
				Settings me = (Settings)xs.Deserialize(fs);
				fs.Close();
				fs.Dispose();
				return me;
			}
			catch
			{
				// 最初の場合は初期値
				return new Settings();
			}
		}
		/// <summary>
		/// Gets the directory used for storing the settings.
		/// </summary>
		/*
		public static string SettingsDirectory
		{
			get
			{
				/ *
				string userfiles;
				userfiles = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
				userfiles += "\\" + companyName + "\\" + productName;
				return userfiles;
				* /
				//return Path.GetDirectoryName(Application.ExecutablePath);
				return PathHelper.SettingDir;
			}
		}
		*/
		#endregion

		/// <summary>
		/// Sets all settings to reasonable default values
		/// </summary>
		public void LoadDefaultSettings()
		{
			StringType = "";
			IntType = 0;
			ColorType = Color.Black;
			FontObject = new Font("ＭＳ ゴシック", 11.0f);
		}

		#region Properties
		// Property grid attributes
		[Browsable(true)] 
		[Description("A string object")]
		[DefaultValue("")]
		public string StringType
		{
			get
			{
				return (string) settings["string"];
			}
			set
			{
				settings["string"] = value;
			}
		}

		// Property grid attributes
		[Browsable(true)] 
		[Description("An int")]
		[DefaultValue(0)]
		public int IntType
		{
			get
			{
				return (int) settings["int"];
			}
			set
			{
				settings["int"] = value;
			}
		}

		// Property grid attributes
		[Browsable(true)] 
		[Description("A complex object")]
		[DefaultValue(typeof(Color), "Black")]
		[XmlIgnore()] // Needed because the XmlSerializer can't handle Color objects
		public Color ColorType
		{
			get
			{
				return (Color) settings["color"];
			}
			set
			{
				settings["color"] = value;
			}
		}

		[Browsable(false)]
		[XmlElement("ColorType")]
		public string XmlColorType
		{
			get
			{
				return Settings.SerializeColor(ColorType);
			}
			set
			{
				ColorType = Settings.DeserializeColor(value);
			}
		}

		[Browsable(true)]
		[XmlIgnore()]
		[DisplayName("Font")]
		public Font FontObject
		{
			get
			{
				return (Font) settings["font"];
			}
			set
			{
				settings["font"] = value;
			}
		}

		[Browsable(false)]
		[XmlElement("Font")]
		public XmlFont XmlFontObject
		{
			get
			{
				return Settings.SerializeFont(FontObject);
			}
			set
			{
				FontObject = Settings.DeserializeFont(value);
			}
		}
		#endregion

		#region Serialization Helpers
		protected enum ColorFormat
		{
			NamedColor,
			ARGBColor
		}

		protected static string SerializeColor(Color color)
		{
			if( color.IsNamedColor )
				return string.Format("{0}:{1}", ColorFormat.NamedColor, color.Name);
			else
				return string.Format("{0}:{1}:{2}:{3}:{4}", ColorFormat.ARGBColor, color.A, color.R, color.G, color.B);
		}

		protected static Color DeserializeColor(string color)
		{
			byte a, r, g, b;

			string [] pieces = color.Split(new char[] {':'});
				
			ColorFormat colorType = (ColorFormat) Enum.Parse(typeof(ColorFormat), pieces[0], true);

			switch(colorType)
			{
				case ColorFormat.NamedColor:
					return Color.FromName(pieces[1]);

				case ColorFormat.ARGBColor:
					a = byte.Parse(pieces[1]);
					r = byte.Parse(pieces[2]);
					g = byte.Parse(pieces[3]);
					b = byte.Parse(pieces[4]);
					
					return Color.FromArgb(a, r, g, b);
			}
			return Color.Empty;
		}

		protected static XmlFont SerializeFont(Font font)
		{
			return new XmlFont(font);
		}

		protected static Font DeserializeFont(XmlFont font)
		{
			return font.ToFont();
		}
		#endregion

		#region Helper classes/structs
		public struct XmlFont
		{
			public string FontFamily;
			public GraphicsUnit GraphicsUnit; // enum
			public float Size;
			public FontStyle Style; // enum

			public XmlFont(Font f)
			{
				FontFamily = f.FontFamily.Name;
				GraphicsUnit = f.Unit;
				Size = f.Size;
				Style = f.Style;
			}

			public Font ToFont()
			{
				return new Font(FontFamily, Size, Style, GraphicsUnit);
			}
		}


		/// <summary>
		/// ///////////////////////////////////////////////////////////////////////////////
		/// MainForm
		/// </summary>
		private Boolean _consoleOutput = true;
		[Category("MainForm")]
		[Description("Windows コンソール出力")]
		[Browsable(true)]
		public Boolean ConsoleOutput
		{
			get { return _consoleOutput; }
			set { _consoleOutput = value; }
		}

		private Boolean _panelOutput = true;
		[Category("MainForm")]
		[Description("FlashDevelop パネル出力")]
		[Browsable(true)]
		public Boolean PanelOutput
		{
			get { return _panelOutput; }
			set { _panelOutput = value; }
		}

		private Boolean _scintillaMode = true;
		[Category("MainForm")]
		[Description("FlashDevelop ScintillaMode")]
		[Browsable(true)]
		public Boolean ScintillaMode
		{
			get { return _scintillaMode; }
			set { _scintillaMode = value; }
		}

		private Boolean _multiTabMode = true;
		[Category("MainForm")]
		[Description("FlashDevelop MultiTabMode")]
		[Browsable(true)]
		public Boolean MultiTabMode
		{
			get { return _multiTabMode; }
			set { _multiTabMode = value; }
		}

		#region Browser Properties
		/// <summary>
		/// ///////////////////////////////////////////////////////////////////////////////
		/// Browser
		/// </summary>
		private Boolean _BrowserMenuStrip = true;
		[Category("Browser")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		[DisplayName("MenuBar")]
		[Browsable(true)]
		public Boolean BrowserMenuStrip
		{
			get { return _BrowserMenuStrip; }
			set { _BrowserMenuStrip = value; }
		}

		private Boolean _BrowserToolStrip = true;
		[Category("Browser")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		[DisplayName("ToolBar")]
		[Browsable(true)]
		public Boolean BrowserToolStrip
		{
			get { return _BrowserToolStrip; }
			set { _BrowserToolStrip = value; }
		}

		private Boolean _BrowserStatusStrip = false;
		[Category("Browser")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(false)]
		[DisplayName("StatusBar")]
		[Browsable(true)]
		public Boolean BrowserStatusStrip
		{
			get { return _BrowserStatusStrip; }
			set { _BrowserStatusStrip = value; }
		}

		private Boolean _BrowserTabPageMode = false;
		[Category("Browser")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(false)]
		[DisplayName("TabPageMode")]
		[Browsable(true)]
		public Boolean BrowserTabPageMode
		{
			get { return _BrowserTabPageMode; }
			set { _BrowserTabPageMode = value; }
		}

		private String _BrowserPersistTabPage = String.Empty;
		[Category("Browser")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("PersistTabPage")]
		//[DefaultValue("")]
		[Browsable(true)]
		public string BrowserPersistTabPage
		{
			get { return _BrowserPersistTabPage; }
			set { _BrowserPersistTabPage = value; }
		}
		
		#endregion

		/// <summary>
		/// ///////////////////////////////////////////////////////////////////////////////
		/// Player
		/// </summary>
		private Boolean _PlayerMenuStrip = true;
		[Category("Player")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		[DisplayName("MenuBar")]
		[Browsable(true)]
		public Boolean PlayerMenuStrip
		{
			get { return _PlayerMenuStrip; }
			set { _PlayerMenuStrip = value; }
		}

		/// <summary>
		/// ///////////////////////////////////////////////////////////////////////////////
		/// PictureBoxControl
		/// </summary>
		private Boolean _PictureBoxControlMenuStrip = false;
		[Category("PictureControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(false)]
		[DisplayName("MenuBar")]
		[Browsable(true)]
		public Boolean PictureBoxControlMenuStrip
		{
			get { return _PictureBoxControlMenuStrip; }
			set { _PictureBoxControlMenuStrip = value; }
		}

		/// <summary>
		/// ///////////////////////////////////////////////////////////////////////////////
		/// SpreadSheet
		/// </summary>
		private Boolean _SpreadSheetMenuStrip = true;
		[Category("SpreadSheet")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		[DisplayName("MenuBar")]
		[Browsable(true)]
		public Boolean SpreadSheetMenuStrip
		{
			get { return _SpreadSheetMenuStrip; }
			set { _SpreadSheetMenuStrip = value; }
		}

		/// <summary>
		/// ///////////////////////////////////////////////////////////////////////////////
		/// DataGrid
		/// </summary>
		private Boolean _DataGridMenuStrip = true;
		[Category("DataGrid")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		[DisplayName("MenuBar")]
		[Browsable(true)]
		public Boolean DataGridMenuStrip
		{
			get { return _DataGridMenuStrip; }
			set { _DataGridMenuStrip = value; }
		}

		private Boolean _tabPageMode = false;
		[Category("DataGrid")]
		[Description("DataGrid TabPageMode")]
		[Browsable(true)]
		public Boolean TabPageMode
		{
			get { return _tabPageMode; }
			set { _tabPageMode = value; }
		}
		/*
		private String _currentDbPath = Path.Combine(PathHelper.DataDir, "PUBS.MDF");
		[Category("DataGrid")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("CurrentDBPath")]
		//[DefaultValue(".c .cpp .cxx .h .hpp .hxx")]
		[Browsable(true)]
		public string CurrentDbPath
		{
			get { return _currentDbPath; }
			set { _currentDbPath = value; }
		}
		*/
		private String _currentSql = "SELECT * FROM ";
		[Category("DataGrid")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("CurrentSql")]
		//[DefaultValue(".c .cpp .cxx .h .hpp .hxx")]
		[Browsable(true)]
		public string CurrentSql
		{
			get { return _currentSql; }
			set { _currentSql = value; }
		}
		
		private String _currentProvider = String.Empty;
		[Category("DataGrid")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("CurrentProvider")]
		//[DefaultValue(".c .cpp .cxx .h .hpp .hxx")]
		[Browsable(true)]
		public string CurrentProvider
		{
			get { return _currentProvider; }
			set { _currentProvider = value; }
		}

		private String _currentDataBase = String.Empty;
		[Category("DataGrid")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("CurrentDataBase")]
		//[DefaultValue(".c .cpp .cxx .h .hpp .hxx")]
		[Browsable(true)]
		public string CurrentDataBase
		{
			get { return _currentDataBase; }
			set { _currentDataBase = value; }
		}
		
		private String _currentTable = String.Empty;
		[Category("DataGrid")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("CurrentTable")]
		//[DefaultValue(".c .cpp .cxx .h .hpp .hxx")]
		[Browsable(true)]
		public string CurrentTable
		{
			get { return _currentTable; }
			set { _currentTable = value; }
		}

		
		/// <summary>
		/// ///////////////////////////////////////////////////////////////////////////////
		/// Azuki Control
		/// </summary>
		private Boolean _AzukiEditorMenuStrip = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		[DisplayName("MenuBar")]
		[Browsable(true)]
		public Boolean AzukiEditorMenuStrip
		{
			get { return _AzukiEditorMenuStrip; }
			set { _AzukiEditorMenuStrip = value; }
		}

		private string _CppFileType = ".c .cpp .cxx .h .hpp .hxx";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("CppFileType")]
		[DefaultValue(".c .cpp .cxx .h .hpp .hxx")]
		[Browsable(false)]
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
		[Browsable(false)]
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
		[Browsable(false)]
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
		[Browsable(false)]
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
		[Browsable(false)]
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
		[Browsable(false)]
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

		private Boolean _DrawsFullWidthSpace = true;
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

		private Boolean _ShowsLineNumber = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		public Boolean ShowsLineNumber
		{
			get { return _ShowsLineNumber; }
			set { _ShowsLineNumber = value; }
		}

		private Boolean _ShowsHRuler = true;
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
		[Browsable(false)]
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
		[Browsable(false)]
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
		[Browsable(false)]
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
		[Browsable(false)]
		public string HRulerIndicatorType
		{
			get { return _HRulerIndicatorType; }
			set { _HRulerIndicatorType = value; }
		}

		private Boolean _ScrollsBeyondLastLine = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		[Browsable(false)]
		public Boolean ScrollsBeyondLastLine
		{
			get { return _ScrollsBeyondLastLine; }
			set { _ScrollsBeyondLastLine = value; }
		}

		private string _Antialias = "Default";
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DisplayName("Antialias")]
		[DefaultValue("Default")]
		[Browsable(false)]
		public string Antialias
		{
			get { return _Antialias; }
			set { _Antialias = value; }
		}

		private Int32 _AutoScrollMargin = 3;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(3)]
		[Browsable(false)]
		public Int32 AutoScrollMargin
		{
			get { return _AutoScrollMargin; }
			set { _AutoScrollMargin = value; }
		}

		private Boolean _CopyLineWhenNoSelection = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		[Browsable(false)]
		public Boolean CopyLineWhenNoSelection
		{
			get { return _CopyLineWhenNoSelection; }
			set { _CopyLineWhenNoSelection = value; }
		}

		private Boolean _UseTextForEofMark = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		[Browsable(false)]
		public Boolean UseTextForEofMark
		{
			get { return _UseTextForEofMark; }
			set { _UseTextForEofMark = value; }
		}

		private Int32 _WindowWidth = 300;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(300)]
		[Browsable(false)]
		public Int32 WindowWidth
		{
			get { return _WindowWidth; }
			set { _WindowWidth = value; }
		}

		private Int32 _WindowHeight = 400;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(400)]
		[Browsable(false)]
		public Int32 WindowHeight
		{
			get { return _WindowHeight; }
			set { _WindowHeight = value; }
		}

		private Boolean _WindowMaximized = true;
		[Category("AzukiControl")]
		[Description("ここにStringValueの説明を書きます。")]
		[DefaultValue(true)]
		[Browsable(false)]
		public Boolean WindowMaximized
		{
			get { return _WindowMaximized; }
			set { _WindowMaximized = value; }
		}

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

		[Category("AzukiControl")]
		[XmlElement("FontColor")]
		[Browsable(false)]
		public string FontColorHtml
		{
			get { return ColorTranslator.ToHtml(FontColor); }
			set { FontColor = ColorTranslator.FromHtml(value); }
		}

		private System.Drawing.Color _fontcolor = System.Drawing.Color.Black;
		[Category("AzukiControl")]
		[XmlIgnore]
		public System.Drawing.Color FontColor
		{
			get { return _fontcolor; }
			set { _fontcolor = value; }
		}

		[Category("AzukiControl")]
		[XmlElement("BackColor")]
		[Browsable(false)]
		public string BakColorHtml
		{
			get { return ColorTranslator.ToHtml(BackColor); }
			set { BackColor = ColorTranslator.FromHtml(value); }
		}

		//private System.Drawing.Color _backcolor = System.Drawing.Color.White;
		private System.Drawing.Color _backcolor = System.Drawing.Color.FromArgb(255, 250, 240);
		[Category("AzukiControl")]
		[XmlIgnore]
		public System.Drawing.Color BackColor
		{
			get { return _backcolor; }
			set { _backcolor = value; }
		}


		/// <summary>
		/// ////////////////////////////////////////////////////////////////////////////////////////
		/// </summary>
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
		/*
				[Category("表示")]
				private Font _fontValue = new Font("ＭＳ ゴシック",11);
				public Font FontValue
				{
					get { return _fontValue; }
					set { _fontValue = value; }
				}
		*/

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

		private PersistorClass _persistor = new PersistorClass();
		[ReadOnly(false)]
		public PersistorClass Persistor
		{
			get { return _persistor; }
			set { _persistor = value; }
		}
		#endregion
	}

	//保存内容の保持クラス（親）
	public class PersistorClass
	{
		public int Count;
		public string Name;
		public List<PersistCustomDocument> DocumentList;// = null;
		public PersistorClass()
		{
			this.Name = "CustomDocument";
			this.Count = 0;
			DocumentList = new List<PersistCustomDocument>();

			//PersistCustomDocument obj = new PersistCustomDocument();
			//obj.Name = "国仲 涼子";
			//obj.Age = 29;
			//obj.Height = 157.5f;

			//this.DocumentList.Add(obj);

			//obj = new PersistCustomDocument();
			//obj.Name = "堀北 真希";
			//obj.Age = 20;
			//obj.Height = 160.2f;

			//this.DocumentList.Add(obj);
		}
	}

	//保存内容の保持クラス（子）
	public class PersistCustomDocument
	{
		public string Name;
		public string PathTag;
		public Boolean tabPageMode;
		public List<String> tabPgeTag;
		//public int Age;
		//public float Height;
	}

}
