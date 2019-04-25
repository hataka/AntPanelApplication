using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Xml.Serialization;
using Ookii.Dialogs;
//using PluginCore.Localization;

namespace FileExplorer
{
    [Serializable]
    public class Settings
    {
        private Int32 sortOrder = 0;
        private Int32 sortColumn = 0;
        private String filePath = "C:\\";
        private Boolean synchronizeToProject = true;

        /// <summary> 
        /// Get and sets the filePath.
        /// </summary>
        [DisplayName("Active Path")]
        [Description("FileExplorer.Description.FilePath"), DefaultValue("C:\\")]
        [Editor(typeof(VistaFolderNameEditor), typeof(UITypeEditor))]
        public String FilePath 
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }

        /// <summary> 
        /// Get and sets the synchronizeToProject.
        /// </summary>
        [DisplayName("Synchronize To Project")]
        [Description("FileExplorer.Description.SynchronizeToProject"), DefaultValue(true)]
        public Boolean SynchronizeToProject
        {
            get { return this.synchronizeToProject; }
            set { this.synchronizeToProject = value; }
        }

        /// <summary> 
        /// Get and sets the sortColumn.
        /// </summary>
        [DisplayName("Active Column")]
        [Description("FileExplorer.Description.SortColumn"), DefaultValue(0)]
        public Int32 SortColumn
        {
            get { return this.sortColumn; }
            set { this.sortColumn = value; }
        }

        /// <summary> 
        /// Get and sets the sortOrder.
        /// </summary>
        [DisplayName("Sort Order")]
        [Description("FileExplorer.Description.SortOrder"), DefaultValue(0)]
        public Int32 SortOrder
        {
            get { return this.sortOrder; }
            set { this.sortOrder = value; }
        }


    public static void Serialize(string file, Settings settings)
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

    public static Settings Deserialize(string filename)
    {
      Settings settings;
      XmlSerializer xs = new XmlSerializer(typeof(Settings));

      if (File.Exists(filename))
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
          settings = (Settings)xs.Deserialize(fs);
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




  }

}
