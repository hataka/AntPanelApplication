using AntPanelApplication.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AntPanelApplication
{
	public class Globals
	{
    static global::AntPanelApplication.Properties.Settings settings;
    /// <summary>
		/// Quick reference to Form1 
		/// </summary> 
		public static Form1 MainForm
		{
			get; internal set;
		}

    /// <summary>
    /// Quick reference to Form1 
    /// </summary> 
    public static AntPanel AntPanel
    {
      get; internal set;
    }



    /// <summary>
    /// Quick reference to CurrentDocument 
    /// </summary>

    public static Control CurrentDocument 
		{
      get;internal set;
    }

    /// <summary>
    /// Quick reference to SciControl 
    /// </summary>
    /*
    public static ScintillaControl SciControl
		{
			get { return CurrentDocument.SciControl; }
		}
    */
    /// <summary>
    /// Quick reference to PreviousDocuments 
    /// </summary>
    /*
    public static List<String> PreviousDocuments
		{
			get { return Settings.PreviousDocuments; }
		}
    */
    /// <summary>
    /// Quick reference to Settings 
    /// </summary>
    /*
    public static Settings Settings
		{
			get { return settings; }

    }
    */
  }

}
