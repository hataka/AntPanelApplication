using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
//using FlashDevelop;
using PluginCore.Utilities;
using PluginCore.Managers;
using PluginCore.Helpers;
using PluginCore;
using WeifenLuo.WinFormsUI.Docking;
using System.Diagnostics;
using CSParser.BuildTree;
using AntPlugin.XmlTreeMenu.Managers;

namespace AntPlugin
{
	public class PluginMain : IPlugin
	{
		private const Int32 PLUGIN_API = 1;
		private const String PLUGIN_NAME = "AntPanel";
		private const String PLUGIN_GUID = "92d9a647-6cd3-4347-9db6-95f324292399";
		private const String PLUGIN_HELP = "www.flashdevelop.org/community/";
		private const String PLUGIN_AUTH = "Canab";
		private const String SETTINGS_FILE = "Settings.fdb";
		private const String PLUGIN_DESC = "Ant Panel";

		private const String STORAGE_FILE_NAME = "antPluginData.txt";
		
		private List<String> buildFilesList = new List<string>();
		public List<string> BuildFilesList
		{
			get { return buildFilesList; }
		}
		
		private String settingFilename;
		private Settings settingObject;
		private DockContent pluginPanel;
		public PluginUI pluginUI;
		private Image pluginImage;

		#region Required Properties

		public int Api
		{
			get { return PLUGIN_API; }
		}
		
		/// <summary>
		/// Name of the plugin
		/// </summary> 
		public String Name
		{
			get { return PLUGIN_NAME; }
		}

		/// <summary>
		/// GUID of the plugin
		/// </summary>
		public String Guid
		{
			get { return PLUGIN_GUID; }
		}

		/// <summary>
		/// Author of the plugin
		/// </summary> 
		public String Author
		{
			get { return PLUGIN_AUTH; }
		}

		/// <summary>
		/// Description of the plugin
		/// </summary> 
		public String Description
		{
			get { return PLUGIN_DESC; }
		}

		/// <summary>
		/// Web address for help
		/// </summary> 
		public String Help
		{
			get { return PLUGIN_HELP; }
		}

		/// <summary>
		/// Object that contains the settings
		/// </summary>
		[Browsable(false)]
		public Object Settings
		{
			get { return settingObject; }
		}
		
		#endregion
		
		#region Required Methods
		
		/// <summary>
		/// Initializes the plugin
		/// </summary>
		public void Initialize()
		{
			InitBasics();
			LoadSettings();
			AddEventHandlers();
			CreateMenuItems();
			CreatePluginPanel();
		}

		/// <summary>
		/// Disposes the plugin
		/// </summary>
		public void Dispose()
		{
      if (this.pluginUI.ftpClientPanel.FtpClient != null) MessageBox.Show("FTPê⁄ë±ÇÇêÿífÇµÇ‹Ç∑");
      this.pluginUI.ftpClientPanel.disconnectButton_Click(null, null);
      SaveSettings();
		}

    /// <summary>
    /// Handles the incoming events
    /// </summary>
    public void AddEventHandlers()
		{
      // Set events you want to listen (combine as flags)
      EventManager.AddEventHandler(this, EventType.FileSwitch | EventType.Command);
		}
		
		public void HandleEvent(Object sender, NotifyEvent e, HandlingPriority prority)
		{
      if (e.Type == EventType.FileSwitch)
      {
        try
        {
          string fileName = PluginBase.MainForm.CurrentDocument.FileName;
          if (fileName != null && this.pluginUI != null && File.Exists(fileName))
          {
            this.pluginUI.CsOutlineParse();
          }
        }
        catch { }
      }
      if (e.Type == EventType.Command)
			{
				string cmd = (e as DataEvent).Action;
        DataEvent evnt = (DataEvent)e;
        string path = String.Empty;
        if (cmd == "ProjectManager.Project")
				{
					if (PluginBase.CurrentProject != null)
						ReadBuildFiles();
					pluginUI.RefreshData();
				}
        // 2017-07-17
        else if (cmd == "Ant.CsOutlineParse")
        {
          pluginUI.CsOutlineParse();
        }
        else if (cmd == "Ant.LoadIn")
        {
          path = PluginBase.MainForm.ProcessArgString(evnt.Data.ToString());
          pluginUI.LoadIn(path);
        }
        else if (cmd == "Ant.LoadOut")
        {
          path = PluginBase.MainForm.ProcessArgString(evnt.Data.ToString());
          pluginUI.LoadOut(path);
        }
        else if (cmd == "Ant.CallCommand")
        {
          string[] tmp = (evnt.Data.ToString()).Split('!');
          string name = tmp[0];
          string arg = tmp.Length > 1 ? tmp[1] : string.Empty;
          this.pluginUI.CallCommand(name, arg);
          evnt.Handled = true;
        }
        else if (cmd == "Ant.MenuCommand")
        {
          string[] tmp2 = (evnt.Data.ToString()).Split('!');
          string name = tmp2[0];
          string arg = tmp2.Length > 1 ? tmp2[1] : string.Empty;
          this.pluginUI.menuTree.CallMenuCommand(name, arg);
          evnt.Handled = true;
        }
        else if (cmd == "Ant.NodeAction")
        {
          ActionManager.NodeAction(evnt.Data.ToString());
          evnt.Handled = true;
        }
        else if (cmd == "Ant.TreeCommand")
        {
          string[] tmp2 = (evnt.Data.ToString()).Split('!');
          string name = tmp2[0];
          string arg = tmp2.Length > 1 ? tmp2[1] : string.Empty;
          this.pluginUI.menuTree.CallPluginCommand(name, arg);
          evnt.Handled = true;
        }
      }
    }
		
		#endregion

		#region Custom Methods

		public void InitBasics()
		{
			pluginImage = PluginBase.MainForm.FindImage("486");
			String dataPath = Path.Combine(PathHelper.DataDir, PLUGIN_NAME);
			if (!Directory.Exists(dataPath))
				Directory.CreateDirectory(dataPath);
			settingFilename = Path.Combine(dataPath, SETTINGS_FILE);
		}

		public void CreateMenuItems()
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)PluginBase.MainForm.FindMenuItem("ViewMenu");
			ToolStripMenuItem menuItem;

			menuItem = new ToolStripMenuItem("Ant Window",
				pluginImage, new EventHandler(ShowAntWindow));
			menu.DropDownItems.Add(menuItem);
		}

		private void CreatePluginPanel()
		{
			pluginUI = new PluginUI(this);
			pluginUI.Text = "Ant";
			pluginUI.StartDragHandling();
			pluginPanel = PluginBase.MainForm.CreateDockablePanel(
			  pluginUI, PLUGIN_GUID, pluginImage, DockState.DockRight);
		}

		private void ShowAntWindow(object sender, EventArgs e)
		{
			pluginPanel.Show();
		}

    public void RunTarget(String file, String target)
    {
      // KAHATA í«â¡ 2018-02-08
      if (Path.GetExtension(file).ToLower() == ".gradle")
      {
        this.RunGradle(file, target);
        return;
      }
      else if (Path.GetExtension(file).ToLower()==".wsf")
      {
        this.RunJob(file, target);
        return;
      }
      String command = Environment.SystemDirectory + "\\cmd.exe";

      String arguments = "/c ";
      if (settingObject.AntPath.Length > 0)
        arguments += Path.Combine(settingObject.AntPath, "bin") + "\\ant";
      else if (this.pluginUI.ant17ToolStripMenuItem.Checked == true) arguments += "ant17";
      else arguments += "ant";

      if (!string.IsNullOrEmpty(settingObject.AddArgs)) arguments += " " + settingObject.AddArgs;
      arguments += " -buildfile \"" + file + "\" \"" + target + "\"";
      //arguments += " -buildfile \"" + file + "\" \"" + target + "\"";
      arguments += " -DprojectDir=\"" + Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath) + "\"";
      arguments += " -DcurDir=\"" + PluginBase.MainForm.ProcessArgString("$(CurDir)") + "\"";

      //kahata: Time-stamp: <2016-04-23 5:41:26 kahata>
      System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(file));

      if (this.pluginUI.consoleToolStripMenuItem.Checked == true)
      {
        Process.Start("cmd.exe", "/k " + command + " " + arguments);
      }
      if (this.pluginUI.documentToolStripMenuItem.Checked == true)
      {
        String result = this.getStandardOutput("cmd.exe", command + " " + arguments);
        PluginBase.MainForm.CallCommand("New", "");
        PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
        PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(result); 
        //String output = Lib.StabdardOutput()
        //Process.Start("cmd.exe", "/k " + command + " " + arguments);
      }
      if (this.pluginUI.traceToolStripMenuItem.Checked == true)
      {
        //TraceManager.Add(command + " " + arguments);
        PluginBase.MainForm.CallCommand("RunProcessCaptured", command + ";" + arguments);
      }
    }

    public void RunJob(String file, String job)
    {
      //CScript //Job:MyFirstJob MyScripts.wsf
      String command = Environment.SystemDirectory + @"\Wscript.exe";
      //file = "\"" + file + "\"";
      String arguments = " //job:" + job;
      arguments += " \"" + file + "\"";
      //kahata: Time-stamp: <2017-06-23 08:27:26 kahata>
      try
      {
        System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(file));
      }
      catch { }
      PluginBase.MainForm.CallCommand("RunProcessCaptured", command + ";" + arguments);
    }

    public void RunGradle(String file, String target)
    {
      String command = @"F:\gradle-3.5\bin\gradle.bat";
      String arguments = "/k " + command + " -q " + target + " -b ";
      arguments += " \"" + file + "\"";
      //kahata: Time-stamp: <2017-06-23 08:27:26 kahata>
      try
      {
        System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(file));
      }
      catch { }
      Process.Start("cmd.exe", arguments);
      //PluginBase.MainForm.CallCommand("RunProcessCaptured", command + ";" + arguments);
    }

    public string getStandardOutput(string command, string arguments)
    {
      Process process = Process.Start(new ProcessStartInfo
      {
        FileName = command,
        Arguments = arguments,
        CreateNoWindow = true,
        UseShellExecute = false,
        RedirectStandardOutput = true
      });
      string text = process.StandardOutput.ReadToEnd();
      return text.Replace("\r\r\n", "\n");
    }

    public void AddBuildFiles(String[] files)
		{
			foreach (String file in files)
			{
				if (!buildFilesList.Contains(file))
					buildFilesList.Add(file);
			}
			SaveBuildFiles();
			pluginUI.RefreshData();
		}

		public void RemoveBuildFile(String file)
		{
			if (buildFilesList.Contains(file))
				buildFilesList.Remove(file);
			SaveBuildFiles();
			pluginUI.RefreshData();
		}

		private void ReadBuildFiles()
		{
			buildFilesList.Clear();
			String folder = GetBuildFilesStorageFolder();
			String fullName = folder + "\\" + STORAGE_FILE_NAME;

			if (File.Exists(fullName))
			{
				StreamReader file = new StreamReader(fullName);
				String line;
				while ((line = file.ReadLine()) != null)
				{
					if (line.Length > 0 && !buildFilesList.Contains(line))
						buildFilesList.Add(line);
				}
				file.Close();
			}
		}

		private void SaveBuildFiles()
		{
			String folder = GetBuildFilesStorageFolder();
			String fullName = folder + "\\" + STORAGE_FILE_NAME;
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);
			StreamWriter file = new StreamWriter(fullName);
			foreach (String line in buildFilesList)
			{
				file.WriteLine(line);
			}
			file.Close();
		}

		private String GetBuildFilesStorageFolder()
		{
			String projectFolder = Path.GetDirectoryName(
				PluginBase.CurrentProject.ProjectPath);
			return Path.Combine(projectFolder, "obj");
		}
		
		public void LoadSettings()
		{
			if (File.Exists(settingFilename))
			{
				try
				{
					settingObject = new Settings();
					settingObject = (Settings) ObjectSerializer.Deserialize(settingFilename, settingObject);
				}
				catch
				{
					settingObject = new Settings();
					SaveSettings();
				}
			}
			else
			{
				settingObject = new Settings();
				SaveSettings();
			}
		}

		public void SaveSettings()
		{
			ObjectSerializer.Serialize(settingFilename, settingObject);
		}


      #endregion

  }

}
