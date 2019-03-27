using AntPanelApplication.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AntPanelApplication.Managers
{
  public class ActionManager
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
      /*
      //XmlMenuTree tree = new XmlMenuTree();
      //try
      //{
        //arg = PluginBase.MainForm.ProcessArgString(strVar);
      //}
      //catch { arg = strVar; }
      try
      {
        arg = arg.Replace("$(CurProjectDir)", Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath));
        arg = arg.Replace("$(CurProjectName)", Path.GetFileNameWithoutExtension(PluginBase.CurrentProject.ProjectPath));
        //arg = arg.Replace("$(CurProjectUrl)", Lib.Path2Url(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), this.settings.DocumentRoot, this.settings.ServerRoot));
        arg = arg.Replace("$(CurProjectUrl)", Lib.Path2Url(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), "localhost"));
        arg = arg.Replace("$(Quote)", "\"");
        arg = arg.Replace("$(Dollar)", "$");
        arg = arg.Replace("$(AppDir)", PathHelper.AppDir);
        arg = arg.Replace("$(BaseDir)", PathHelper.BaseDir);
        arg = arg.Replace("$(CurSciText)", PluginBase.MainForm.CurrentDocument.SciControl.Text);
        //arg = arg.Replace("$(CurFileUrl)", Lib.Path2Url(PluginBase.MainForm.CurrentDocument.FileName, this.settings.DocumentRoot, this.settings.ServerRoot));
        arg = arg.Replace("$(CurFileUrl)", Lib.Path2Url(PluginBase.MainForm.CurrentDocument.FileName, "localhost"));
        //arg = arg.Replace("$(ControlCurFilePath)", this.controlCurrentFilePath);
        //arg = arg.Replace("$(ControlCurFileDir)", Path.GetDirectoryName(this.controlCurrentFilePath));
        //arg = arg.Replace("$(CurControlFilePath)", this.controlCurrentFilePath);
        //arg = arg.Replace("$(CurControlFileDir)", Path.GetDirectoryName(this.controlCurrentFilePath));
        arg = ApplyAntProperties(arg);
      }
      catch { }
      */
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
          //ActionManager.RunProcess(button, null);
          ActionManager.RunProcess(ni);
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
          //ActionManager.DefaultAction(button, null);
          break;
      }
    }

    public static void RunProcess(NodeInfo ni)
    {
/*
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstring = button.Tag as String;

      String command = String.Empty;
      String args = String.Empty;
      String path = String.Empty;
      String option = String.Empty;
      String dir = String.Empty;

      if (String.IsNullOrEmpty(argstring)) return;
      try
      {
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

      PluginBase.MainForm.WorkingDirectory = Path.GetDirectoryName(path);

      //実行出力
      try
      {
        if (String.IsNullOrEmpty(option))
        {
          Process.Start(command, args);
        }
        else if (option.ToLower().IndexOf("inplace") >= 0 || option.ToLower().IndexOf("inpanel") >= 0)
        {
          button.Tag = argstring;
          //menuTree.ExecuteInPlace(argstring);
          ExecuteInPlace(button, null);
        }
        else
        {
          //if (option.ToLower().IndexOf("doc") >= 0 || this.settings.IsDocumentOutput)
          if (option.ToLower().IndexOf("doc") >= 0)
          {
            result = ProcessHandler.getStandardOutput(command, args);
            PluginBase.MainForm.CallCommand("New", "");
            PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
            PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(result);
          }
          //if (option.ToLower().IndexOf("trace") >= 0 || this.settings.IsPanelOutput)
          if (option.ToLower().IndexOf("trace") >= 0)
          {
            result = ProcessHandler.getStandardOutput(command, args);
            TraceManager.Add(result);
          }
          if (option.ToLower().IndexOf("silent") >= 0)
          {
            result = ProcessHandler.getStandardOutput(command, args);
            TraceManager.Add(result);
          }
          // テストステージ
          if (option.ToLower().IndexOf("textlog") >= 0 || option.ToLower().IndexOf("richtext") >= 0)
          {
            result = ProcessHandler.getStandardOutput(command, args);
            String title = Path.GetFileNameWithoutExtension(command);
            menuTree.CreateCustomDocument("RichTextEditor", "[出力]" + command + "!" + result);
          }
          if (option.ToLower().IndexOf("con") >= 0)
          {
            Process.Start("cmd.exe", "/k " + command + " " + args);
          }
        }
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
      }
*/
    }






















  }
}
