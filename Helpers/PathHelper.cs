using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
//using PluginCore.Managers;

namespace AntPanelApplication.Helpers
{
	public class PathHelper
	{
		/// <summary>
		/// Path to the current application directory
		/// </summary>
		public static String BaseDir
		{
			get
			{
			  //if (PluginBase.MainForm.StandaloneMode) return AppDir;
			   //else return UserAppDir;
			  return AppDir;
			}
		}

		/// <summary>
		/// Path to the main application directory
		/// </summary>
		public static String AppDir
		{
			get
			{
				return Path.GetDirectoryName(Application.ExecutablePath);
			}
		}

		/// <summary>
		/// Path to the user's application directory
		/// </summary>
		public static String UserAppDir
		{
			get
			{
		//String userAppDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		//return Path.Combine(userAppDir, DistroConfig.DISTRIBUTION_NAME);
		return AppDir;
			}
		}

		/// <summary>
		/// Path to the docs directory
		/// </summary>
		public static String DocDir
		{
			get
			{
				return Path.Combine(AppDir, "Docs");
			}
		}

		/// <summary>
		/// Path to the data directory
		/// </summary>
		public static String DataDir
		{
			get
			{
				return Path.Combine(BaseDir, "Data");
			}
		}

	/// <summary>
	/// Path to the snippets directory
	/// </summary>
	/*
	public static String SnippetDir
		{
			get
			{
				String custom = PluginBase.Settings.CustomSnippetDir;
				if (!String.IsNullOrEmpty(custom) && Directory.Exists(custom)) return custom;
				else return Path.Combine(BaseDir, "Snippets");
			}
		}
	*/
	/// <summary>
	/// Path to the templates directory
	/// </summary>
		public static String TemplateDir
		{
			get
			{
				//String custom = PluginBase.Settings.CustomTemplateDir;
				//if (!String.IsNullOrEmpty(custom) && Directory.Exists(custom)) return custom;
				//else
        return Path.Combine(BaseDir, "Templates");
			}
		}
	
	/// <summary>
	/// Path to the project templates directory
	/// </summary>
	/*
	public static String ProjectsDir
		{
			get
			{
				String custom = PluginBase.Settings.CustomProjectsDir;
				if (!String.IsNullOrEmpty(custom) && Directory.Exists(custom)) return custom;
				else return Path.Combine(AppDir, "Projects");
			}
		}
	*/
		/// <summary>
		/// Path to the settings directory
		/// </summary>
		public static String SettingDir
		{
			get
			{
				return Path.Combine(BaseDir, "Settings");
			}
		}

	/// <summary>
	/// Path to the settingdata directory
	/// </summary>
	  public static String SettingDataDir
	{
	  get
	  {
		return Path.Combine(BaseDir, "SettingData");
	  }
	}

    public static String DockableControlsDir
    {
      get
      {
        return Path.Combine(BaseDir, "DockableControls");
      }
    }

    public static String CsMacroDir
    {
      get
      {
        return Path.Combine(BaseDir, "CsMacro");
      }
    }

    /// <summary>
    /// Path to the custom shortcut directory
    /// </summary>
    public static String ShortcutsDir
  {
    get
		{
			return Path.Combine(SettingDir, "Shortcuts");
		}
	}

		/// <summary>
		/// Path to the themes directory
		/// </summary>
		public static String ThemesDir
		{
			get
			{
				return Path.Combine(SettingDir, "Themes");
			}
		}

		/// <summary>
		/// Path to the user project templates directory
		/// </summary>
		public static String UserProjectsDir
		{
			get
			{
				return Path.Combine(UserAppDir, "Projects");
			}
		}

		/// <summary>
		/// Path to the user lirbrary directory
		/// </summary>
		public static String UserLibraryDir
		{
			get
			{
				return Path.Combine(UserAppDir, "Library");
			}
		}

		/// <summary>
		/// Path to the library directory
		/// </summary>
		public static String LibraryDir
		{
			get
			{
				return Path.Combine(AppDir, "Library");
			}
		}

		/// <summary>
		/// Path to the plugin directory
		/// </summary>
		public static String PluginDir
		{
			get
			{
				return Path.Combine(AppDir, "Plugins");
			}
		}

		/// <summary>
		/// Path to the users plugin directory
		/// </summary>
		public static String UserPluginDir
		{
			get
			{
				return Path.Combine(UserAppDir, "Plugins");
			}
		}

		/// <summary>
		/// Path to the tools directory
		/// </summary>
		public static String ToolDir
		{
			get
			{
				return Path.Combine(AppDir, "Tools");
			}
		}

    //public static string TemplateDir { get; internal set; }
  }
}
