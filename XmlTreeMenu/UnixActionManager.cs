using AntPlugin.XmlTreeMenu;
using AntPlugin.XmlTreeMenu.Managers;
using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AntPanelApplication.Managers
{
  public class UnixActionManager
  {
    public static XmlMenuTree menuTree
    {
      get; internal set;
    }

    public static String currentDocumentPath;

    //public String CurrentDocumentPath
    //{
      //get { return GetCurrentDocumentPath(); }
    //}

    public static string ProcessVariable(string strVar)
    {
      string arg = strVar;// string.Empty;
      try
      {
        arg = arg.Replace("$(CurProjectDir)", Globals.AntPanel.projectDir);
        arg = arg.Replace("$(CurProjectName)", Globals.AntPanel.projectName);
        arg = arg.Replace("$(Quote)", "\"");
        arg = arg.Replace("$(Dollar)", "$");
        arg = arg.Replace("$(AppDir)", Path.GetDirectoryName(Application.ExecutablePath));
        arg = arg.Replace("$(BaseDir)", Path.GetDirectoryName(Application.ExecutablePath));
        //arg = arg.Replace("$(SelText)", GetSelText()); // TODO
        arg = arg.Replace("$(CurFile)", ActionManager.GetCurrentDocumentPath());
        arg = arg.Replace("$(CurFilename)", Path.GetFileName(ActionManager.GetCurrentDocumentPath()));
        arg = arg.Replace("$(CurFilenameNoExt)", Path.GetFileNameWithoutExtension(ActionManager.GetCurrentDocumentPath()));
        //arg = arg.Replace("$(CurWord)", GetCurWord()); // TODO
        //arg = arg.Replace("$(Timestamp)", GetTimestamp());// TODO
        //arg = arg.Replace("$(DesktopDir)", GetDesktopDir()); // TODO
        //arg = arg.Replace("$(SystemDir)", GetSystemDir()); // TODO
        //arg = arg.Replace("$(ProgramsDir)", GetProgramsDir()); // TODO
        //arg = arg.Replace("$(PersonalDir)", GetPersonalDir()); // TODO
        //arg = arg.Replace("$(WorkingDir)", GetWorkingDir()); // TODO
        //arg = arg.Replace("$(Clipboard)", GetClipboard()); // TODO
        //arg = arg.Replace("$(Locale)", GetLocale()); // TODO
        //arg = arg.Replace("$(CBI)", GetCBI()); // TODO
        //arg = arg.Replace("$(STC)", GetSTC()); // TODO
        //arg = arg.Replace("$(UserAppDir)", GetUserAppDir()); // TODO
        //arg = arg.Replace("$(TemplateDir)", GetTemplateDir()); // TODO
        //arg = arg.Replace("$(CurSyntax)", GetCurSyntax()); // TODO
        //arg = arg.Replace("$(OpenFile)", GetOpenFile()); // TODO
        //arg = arg.Replace("$(SaveFile)", GetSaveFile()); // TODO
        arg = ApplyAntProperties(arg);
      }
      catch { }
      return arg;
    }

    public static string ApplyAntProperties(string strVar)
    {
      /*
      foreach (KeyValuePair<string, string> item in XmlMenuTree.AntProperties)
      {
        //Console.WriteLine("[{0}:{1}]", item.Key, item.Value);
        strVar = strVar.Replace("$(" + item.Key + ")", item.Value);
      }
      */
      return strVar;
    }

    public static void NodeAction(object sender, EventArgs e)
    {
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      if (button.Tag is String)
      {
        String argstring = button.Tag as String;
        ActionManager.NodeAction(argstring);
      }

      else if (button.Tag is NodeInfo)
      {
        NodeInfo ni = button.Tag as NodeInfo;
        ActionManager.NodeAction(ni);
      }
    }

    public static void NodeAction(String tagstring)
    {
      string argstring = String.Empty;
      string action = String.Empty;
      String command = String.Empty;
      String args = String.Empty;
      String path = String.Empty;
      String option = String.Empty;

      if (String.IsNullOrEmpty(tagstring)) return;
      try
      {
        String[] tmp0 = tagstring.Split('@');
        if (tmp0.Length > 1) { action = tmp0[0]; argstring = tmp0[1]; }
        else if (tmp0.Length == 1) { action = String.Empty; argstring = tmp0[0]; }
        else return;
        //Dictionary<string, string> param = StringHandler.Get_Values(argstring, '|', '=');
        String[] tmpstr = argstring.Split('|');
        command = ProcessVariable(tmpstr[0]);
        args = (tmpstr.Length > 1) ? ProcessVariable(tmpstr[1]) : null;
        path = (tmpstr.Length > 2) ? ProcessVariable(tmpstr[2]) : null;
        option = (tmpstr.Length > 3) ? ProcessVariable(tmpstr[3]) : null;
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
        return;
      }
      ///////////////////////////////////////////////////////////////
      //String result = "";
      // command前処理					
      if (command == String.Empty)
      {
        //if(path != String.Empty && Path)
        command = path;
      }
      else if (args == String.Empty) args = path;
      NodeInfo ni = new NodeInfo();
      ni.Action = action;
      ni.Command = command;
      ni.Args = args;
      ni.Path = path;
      ni.Option = option;
      ActionManager.NodeAction(ni);
    }

    public static void NodeAction(NodeInfo ni)
    {
      ToolStripMenuItem button = new ToolStripMenuItem();
      //String pathbase = PluginBase.MainForm.ProcessArgString(ni.PathBase);
      //String action = PluginBase.MainForm.ProcessArgString(ni.Action);
      //String command = ProcessVariable(PluginBase.MainForm.ProcessArgString(ni.Command));
      String pathbase = ProcessVariable(ni.PathBase);
      String action = ProcessVariable(ni.Action);
      String command = ProcessVariable(ni.Command);
      String innerText = ProcessVariable(ni.InnerText);
      String path = String.Empty; ;
      if (ni.Path.IndexOf('|') < 0)
      {
        path =Path.Combine(ProcessVariable(ni.pathbase),ProcessVariable(ni.Path));
      }
      else path = ni.Path;
      String icon = ProcessVariable(ni.Icon);
      String args = ProcessVariable(ni.args);//String.Empty;
      String option = ProcessVariable(ni.Option);
      String filebody = String.Empty;
      String result = String.Empty;
      String dir = String.Empty;
      String code = String.Empty;

      //DialogResult res;

      if (path != String.Empty)
      {
        if (System.IO.Directory.Exists(path)) dir = path;
        else if (System.IO.File.Exists(path)) dir = Path.GetDirectoryName(path);
        if (dir != String.Empty) System.IO.Directory.SetCurrentDirectory(dir);
      }

      if (ni.Path.IndexOf('|') < 0)
      {
        filebody = Path.GetFileNameWithoutExtension(path);
      }

      // FIXME
      //this.ApplyPropertySetting();

      // kokokoko
      //PluginBase.MainForm.StatusStrip.Tag = ni.Action.ToLower() + "!" + command + "|" + args + "|" + path + "|" + option;
      button.Tag = command + "|" + args + "|" + path + "|" + option;

      switch (ni.Action.ToLower())
      {
        case "menu":
          //ActionManager.AddBuildFiles(button, null);
          break;
        case "browse":
          //ActionManager.BrowseEx(path);
          break;
        case "ant":
          //ActionManager.RunTarget(button, null);
          break;
        case "wsf":
          //ActionManager.RunWshScript(button, null);
          break;
        case "conexe":
          button.Tag = command + "|" + args + "|" + path + "|" + "Console";
          //ActionManager.RunProcess(button, null);
          break;
        case "winexe":
        case "runprocess":
          UnixActionManager.RunProcess(ni);
          break;
        case "openeditabledocument":
        case "opendocument":
        case "openproject":
        case "open":
        case "openedit":
        case "openfile":
          //ActionManager.OpenDocument(button, null);
          break;
        case "picture":
          //ActionManager.Picture(command + "|" + args + "|" + path + "|" + option);
          break;
        case "custom":
        case "customdocument":
        case "customdoc":
          //ActionManager.CustomDocument(button, null);
          break;
        case "plugincommand":
          //ActionManager.PluginCommand(button, null);
          break;
        case "callcommand":
          //ActionManager.CallCommand(button, null);
          break;
        case "executescript":
        case "embedscript":
        case "runscript":
        case "script":
          //ScriptManager.RunScript(ni);
          break;
        case "evalscript":
          //ScriptManager.EvalScript(ni.Command);
          break;
        default:
          UnixActionManager.DefaultAction(ni);
          break;
      }
    }

    public static void RunProcess(NodeInfo ni)
    {
      String command = String.Empty;
      String args = String.Empty;
      String path = String.Empty;
      String option = String.Empty;
      String dir = String.Empty;

      if (ni == null) return;
      try
      {
        command = ProcessVariable(ni.Command);
        args = ProcessVariable(ni.Args);
        path = ProcessVariable(ni.Path);
        option = ProcessVariable(ni.Option);
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
        return;
      }
      if (!String.IsNullOrEmpty(path))
      {
        if (System.IO.Directory.Exists(path)) dir = path;
        else if (System.IO.File.Exists(path)) dir = Path.GetDirectoryName(path);
        if (dir != String.Empty) System.IO.Directory.SetCurrentDirectory(dir);
      }
      ///////////////////////////////////////////////////////////////
      String result = "";
      // command前処理					
      if (command == String.Empty) command = path;
      else if (args == String.Empty) args = path;
      try
      {
        if (String.IsNullOrEmpty(option))
        {
          Process.Start(command, args);
        }
        else
        {
          if (option.ToLower().IndexOf("doc") >= 0)
          {
            result = ProcessHandler.getStandardOutput(command, args);
            MessageBox.Show(result);
            
            
            //おかしい FIXME
            //AntPanel.richTextBox1.Text = result;

          }
          else if (option.ToLower().IndexOf("con") >= 0 || option.ToLower().IndexOf("term") >= 0)
          {
            //OSの情報を取得する
            System.OperatingSystem os = System.Environment.OSVersion;
            if ((os.ToString()).IndexOf("Unix") >= 0)
            {
              String comm = "gnome-terminal";
              String arguments = "-e \"sh -c \'" + ni.Command + " " + ni.Args + "; exec bash\'\"";
              Process.Start(comm, arguments);
            }
            else
            {
              Process.Start("cmd.exe", "/k " + command + " " + args);
            }
          }
        }
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
      }
    }
 
    public static void DefaultAction(NodeInfo ni)
    {
      String command = String.Empty;
      String args = String.Empty;
      String path = String.Empty;
      String option = String.Empty;
      String dir = String.Empty;

      if (ni == null) return;
      try
      {
        command = ProcessVariable(ni.Command);
        args = ProcessVariable(ni.Args);
        path = ProcessVariable(ni.Path);
        option = ProcessVariable(ni.Option);
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
        return;
      }
      if (!String.IsNullOrEmpty(path))
      {
        if (System.IO.Directory.Exists(path)) dir = path;
        else if (System.IO.File.Exists(path)) dir = Path.GetDirectoryName(path);
        if (dir != String.Empty) System.IO.Directory.SetCurrentDirectory(dir);
      }
      String result = "";
      // command前処理					
      if (command == String.Empty) command = path;
      else if (args == String.Empty) args = path;
      ///////////////////////////////////////////////////////////////
      if (File.Exists(ni.Path))
      {
        if (Lib.IsTextFile(ni.Path))
        {
          //PluginBase.MainForm.OpenEditableDocument(ni.Path);
          //Process.Start("gedit",ni.Path);
          Process.Start("emacs", ni.Path);
        }
        else if (Lib.IsImageFile(ni.Path))
        {
          Process.Start("eog", ni.Path);
        }
        else if (Lib.IsSoundFile(ni.Path) || Lib.IsVideoFile(ni.Path))
        {
          Process.Start("totem", ni.Path);
        }
        else if (Path.GetExtension(ni.Path) == ".url")
        {
          StringBuilder sb = new StringBuilder(1024);
          IniFileHandler.GetPrivateProfileString(
            "InternetShortcut", "URL", "default", sb, (uint)sb.Capacity, ni.Path);
          //PluginBase.MainForm.CallCommand("Browse", sb.ToString());
          Process.Start(sb.ToString());
        }
        else
        {
          if (System.IO.Directory.Exists(Path.GetDirectoryName(ni.Path)))
          {
            System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(ni.Path));
            Process.Start(ni.Path, ni.Args);
          }
        }
      }
      else if (Directory.Exists(ni.Path))
      {
        Process.Start("nautilus",ni.Path);
        //PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.BrowseTo;" + ni.path);
      }
      else if (Lib.IsWebSite(ni.Path))
      {
        Process.Start(ni.Path);
      }
    }




















  }
}
