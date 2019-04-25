using System;
using System.Data;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

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
		private static string companyName;
		private static string productName;

		#region Constructors
		/// <summary>
		/// Static constructor to retreive the companyName and productName
		/// from the assembly
		/// </summary>
		static Settings()
		{
			Assembly assembly = typeof(Settings).Assembly;

			AssemblyCompanyAttribute [] acas = (AssemblyCompanyAttribute[]) assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
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

		/// <summary>
		/// Gets the directory used for storing the settings.
		/// </summary>
		public static string SettingsDirectory
		{
			get
			{
				string userfiles;

				userfiles = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
			
				userfiles += "\\" + companyName + "\\" + productName;

				return userfiles;
			}
		}
		#endregion

		/// <summary>
		/// Sets all settings to reasonable default values
		/// </summary>
		public void LoadDefaultSettings()
		{
			StringType = "";
			IntType = 0;
			ColorType = Color.Black;
			FontObject = new Font("Arial", 72.0f);
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
		[XmlElement("FontObject")]
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
			public GraphicsUnit GraphicsUnit;
			public float Size;
			public FontStyle Style;

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
		#endregion
	}
}
