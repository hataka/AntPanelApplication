using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonInterface;
using CommonInterface.Managers;

namespace FileExplorer
{
    public class PluginMain :IPlugin
    {
      private String pluginName = "FileExplorer";
      private String pluginGuid = "f534a520-bcc7-4fe4-a4b9-6931948b2686";
      private String pluginHelp = "www.flashdevelop.org/community/";
      private String pluginDesc = "Adds a file explorer panel to FlashDevelop.";
      private String pluginAuth = "FlashDevelop Team";
      private String settingFilename;
      private String configFilename;
    //private Settings settingObject;
    //private DockContent pluginPanel;
      private PluginUI pluginUI;
      private Image pluginImage;
      private const String explorerAction = "explorer.exe /e,{0}";

    #region Required Properties

    /// <summary>
    /// Api level of the plugin
    /// </summary>
    public Int32 Api
    {
      get { return 1; }
    }

    /// <summary>
    /// Name of the plugin
    /// </summary> 
    public String Name
    {
      get { return this.pluginName; }
    }

    /// <summary>
    /// GUID of the plugin
    /// </summary>
    public String Guid
    {
      get { return this.pluginGuid; }
    }

    /// <summary>
    /// Author of the plugin
    /// </summary> 
    public String Author
    {
      get { return this.pluginAuth; }
    }

    /// <summary>
    /// Description of the plugin
    /// </summary> 
    public String Description
    {
      get { return this.pluginDesc; }
    }

    /// <summary>
    /// Web address for help
    /// </summary> 
    public String Help
    {
      get { return this.pluginHelp; }
    }

    public object Settings => throw new NotImplementedException();

    /// <summary>
    /// Object that contains the settings
    /// </summary>
    [Browsable(false)]
    //Object IPlugin.Settings
    //{
    //  get { return this.settingObject; }
    //}

    /// <summary>
    /// Internal access to settings
    /// </summary>
    //[Browsable(false)]
    //public Settings Settings
    //{
    //get { return this.settingObject; }
    //}

    #endregion

    #region Required Methods

    /// <summary>
    /// Initializes the plugin
    /// </summary>

    /// <summary>
    /// Disposes the plugin
    /// </summary>
    public void Dispose()
    {
      //this.SaveSettings();
    }

    /// <summary>
    /// Handles the incoming events
    /// </summary>
    public void HandleEvent(Object sender, NotifyEvent e, HandlingPriority priority)
    {
      /*
      switch (e.Type)
      {
        case EventType.UIStarted:
          this.pluginUI.Initialize(null, null);
          break;

        case EventType.Command:
          DataEvent evnt = (DataEvent)e;
          switch (evnt.Action)
          {
            case "FileExplorer.BrowseTo":
              this.pluginUI.BrowseTo(evnt.Data.ToString());
              this.OpenPanel(null, null);
              evnt.Handled = true;
              break;

            case "FileExplorer.Explore":
              ExploreDirectory(evnt.Data.ToString());
              evnt.Handled = true;
              break;

            case "FileExplorer.FindHere":
              FindHere((String[])evnt.Data);
              evnt.Handled = true;
              break;

            case "FileExplorer.PromptHere":
              PromptHere(evnt.Data.ToString());
              evnt.Handled = true;
              break;

            case "FileExplorer.GetContextMenu":
              evnt.Data = this.pluginUI.GetContextMenu();
              evnt.Handled = true;
              break;
          }
          break;

        case EventType.FileOpen:
          TextEvent evnt2 = (TextEvent)e;
          if (File.Exists(evnt2.Value))
          {
            this.pluginUI.AddToMRU(evnt2.Value);
          }
          break;
      }
    */
    }

    public void Initialize()
    {
      this.InitBasics();
      //this.LoadSettings();
      //this.AddEventHandlers();
      this.CreatePluginPanel();
      //this.CreateMenuItem();
      //throw new NotImplementedException();
    }


    /// <summary>
    /// Initializes important variables
    /// </summary>
    public void InitBasics()
    {
      //String dataPath = Path.Combine(PathHelper.DataDir, "FileExplorer");
      //if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);
      //this.settingFilename = Path.Combine(dataPath, "Settings.fdb");
      //this.configFilename = Path.Combine(dataPath, "Config.ini");
      //this.pluginDesc = TextHelper.GetString("Info.Description");
      this.pluginImage = PluginBase.MainForm.FindImage("209");
    }


    /// <summary>
    /// Creates a plugin panel for the plugin
    /// </summary>
    public void CreatePluginPanel()
    {
      this.pluginUI = new PluginUI(this);
      this.pluginUI.Text = "FileExplorer";
      PluginBase.MainForm.CreateDockableTabPage(this.pluginUI, Guid, this.pluginImage, "DockRight");
    }
    #endregion
  }
}
