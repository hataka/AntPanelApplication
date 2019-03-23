using AntPanelApplication;
using AntPlugin.CommonLibrary;
using AxWMPLib;
using CommonLibrary;
//using AntPlugin.XmlTreeMenu.Controls;
//using AntPlugin.XMLTreeMenu.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AntPlugin.XmlTreeMenu.Managers
{
  public class ActionManager
  {
    static global::AntPanelApplication.Properties.Settings 
      settings = new global::AntPanelApplication.Properties.Settings();

    public static XmlMenuTree menuTree
    {
      get; internal set;
    }

    public static String currentDocumentPath;

    public String CurrentDocumentPath
    {
      get { return GetCurrentDocumentPath(); }
    }

    public static string ProcessVariable(string strVar)
    {
      string arg = strVar;
      try
      {
        arg = arg.Replace("$(CurProjectDir)", Path.GetDirectoryName(menuTree.antPanel.projectPath));
        arg = arg.Replace("$(CurProjectName)", Path.GetFileNameWithoutExtension(menuTree.antPanel.projectPath));
        arg = arg.Replace("$(CurProjectUrl)", Lib.Path2Url(Path.GetDirectoryName(menuTree.antPanel.projectPath), 
          settings.DocumentRoot, settings.ServerRoot));
        arg = arg.Replace("$(Quote)", "\"");
        arg = arg.Replace("$(Dollar)", "$");
        arg = arg.Replace("$(AppDir)", Path.GetDirectoryName(menuTree.antPanel.targetPath));
        arg = arg.Replace("$(BasePath)", settings.Devenv15Path);
        arg = arg.Replace("$(CurSciText)", menuTree.antPanel.curSelText);
        arg = arg.Replace("$(CurFileUrl)", Lib.Path2Url(menuTree.antPanel.itemPath,settings.DocumentRoot, settings.ServerRoot));
        /*
        //arg = arg.Replace("$(ControlCurFilePath)", this.controlCurrentFilePath);
        //arg = arg.Replace("$(ControlCurFileDir)", Path.GetDirectoryName(this.controlCurrentFilePath));
        //arg = arg.Replace("$(CurControlFilePath)", this.controlCurrentFilePath);
        //arg = arg.Replace("$(CurControlFileDir)", Path.GetDirectoryName(this.controlCurrentFilePath));
        */
      }
      catch { }
 
      return arg;
    }

    public static void NodeAction(object sender, EventArgs e)
    {
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      if(button.Tag is String)
      {
        String argstring = button.Tag as String;
        ActionManager.NodeAction(argstring);
      }

      else if(button.Tag is NodeInfo)
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
      String pathbase = ProcessVariable(ni.PathBase);
      String action = ProcessVariable(ni.Action);
      String command = ProcessVariable(ni.Command);
      String innerText = ProcessVariable(ni.InnerText);
      String path = String.Empty; ;
      if (ni.Path.IndexOf('|') < 0)
      {
        path = ProcessVariable(Path.Combine(ProcessVariable(ni.pathbase),ProcessVariable(ni.Path)));
      }
      else   path = ni.Path;
      String icon = ProcessVariable(ni.Icon);
      String args = ProcessVariable(ni.args);//String.Empty;
      String option = ProcessVariable(ni.Option);
      String filebody = String.Empty;
      String result = String.Empty;
      String dir = String.Empty;
      String code = String.Empty;
 
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
      //PluginBase.MainForm.StatusStrip.Tag = ni.Action.ToLower() + "!" + command + "|" + args + "|" + path + "|" + option;
      button.Tag = command + "|" + args + "|" + path + "|" + option;
      switch (ni.Action.ToLower())
      {
        case "menu":
          ActionManager.AddBuildFiles(button, null);
          break;
        case "browse":
          ActionManager.Browse(ni);
          break;
        case "ant":
          ActionManager.RunTarget(button, null);
          break;
        case "wsf":
          ActionManager.RunWshScript(button, null);
           break;
        case "conexe":
          button.Tag = command + "|" + args + "|" + path + "|" + "Console";
          ActionManager.RunProcess(button, null);
          break;
        case "winexe":
        case "runprocess":
          ActionManager.RunProcess(button, null);
          break;

        //case "runprocessdialog":
        //RunProcessDialog();
        //break;

        case "openeditabledocument":
        case "opendocument":
        case "openproject":
        case "open":
        case "openedit":
        case "openfile":
          ActionManager.OpenDocument(button, null);
          break;
        case "picture":
          //ActionManager.Picture(command + "|" + args + "|" + path + "|" + option);
          ActionManager.Picture(ni);
          break;
        case "player":
          //ActionManager.Player(path);
          ActionManager.Player(ni);
          ((Form1)menuTree.Tag).ActivatePlayer();
          break;
        // opengl.dllが,NET4.0で互換性がないので廃止 
        //.NET3.5 のFlashdevelop 5.2.0ではdockableControlで処理
        //case "opengl":
        //this.OpenGL_Action(command);
        //break;
        case "custom":
        case "customdocument":
        case "customdoc":
          ActionManager.CustomDocument(button, null);
          break;
        //  2013-02-27 追加
        // excel13 のspredheetはwindows8以上で使用不可
        // 代替のReogrid.dll は.NET4.0で互換性なし(.NET3.5のFD5.2.0はdockablecontrol
        //case "spreadsheet":
        //  this.SpreadSheet(path);
        //  break;
        //case "executescript":
        //ActionManager.ExecuteScript(button, null);
        //break;
        case "plugincommand":
          ActionManager.PluginCommand(button, null);
          break;
        case "callcommand":
          ActionManager.CallCommand(button, null);
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
          ActionManager.DefaultAction(button, null);
          break;
      }
    }

    public static void AddBuildFiles(object sender, EventArgs e)
    {
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstring = button.Tag as String;
      String dir = String.Empty;

      if (argstring == "") return;
      NodeInfo ni = MakeNodeInfo(argstring);
      if (ni.Path != String.Empty)
      {
        if (System.IO.Directory.Exists(ni.Path)) dir = ni.Path;
        else if (System.IO.File.Exists(ni.Path)) dir = Path.GetDirectoryName(ni.Path);
        if (dir != String.Empty) System.IO.Directory.SetCurrentDirectory(dir);
      }
      if (ni.Command == "OpenTreeDataDialog") menuTree.antPanel.addButton_Click(null, null);
      else if (File.Exists(ni.Path))
      {
        menuTree.treeView.Nodes.Clear();
        // bug fix 2018-03-16
        // タブコントロールに組み込むと最初に加えたノードが表示されなくなる
        // 間に合せのパッチ
        TreeNode dummy = new TreeNode("dummy");
        menuTree.treeView.Nodes.Add(dummy);
        menuTree.treeView.Nodes.Add(menuTree.getXmlTreeNode(ni.Path, true));
      }
      else return;
    }

    public static void RunProcess(object sender, EventArgs e)
    {
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
      //////////////////////////////////////////////////////////////
      String result = "";
      // command前処理					
      if (command == String.Empty) command = path;
      else if (args == String.Empty) args = path;

      Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
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
            String tempfile = @"F:\temp\output.txt";
            result = ProcessHandler.getStandardOutput(command, args);
            WriteFileEncoding(tempfile, result, Encoding.UTF8);
            menuTree.antPanel.OpenDocument(tempfile);
            //PluginBase.MainForm.CallCommand("New", "");
            //PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
            //PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(result);
          }
          //if (option.ToLower().IndexOf("trace") >= 0 || this.settings.IsPanelOutput)
          if (option.ToLower().IndexOf("trace") >= 0)
          {
            // https://msdn.microsoft.com/ja-jp/library/0xca6kdd.aspx
            String arguments = "/Command " + "\"Shell /c " + command + " " + args + "\"";
            Process.Start(settings.Devenv15Path, arguments);
            //result = ProcessHandler.getStandardOutput(command, args);
            //TraceManager.Add(result);
          }
          if (option.ToLower().IndexOf("silent") >= 0)
          {
            //result = ProcessHandler.getStandardOutput(command, args);
            //TraceManager.Add(result);
          }
          // テストステージ
          if (option.ToLower().IndexOf("textlog") >= 0 || option.ToLower().IndexOf("richtext") >= 0)
          {
            //[テキストとしてファイルを挿入] InsertFile          Edit.InsertFileAsText
            //result = ProcessHandler.getStandardOutput(command, args);
            //String title = Path.GetFileNameWithoutExtension(command);
            //menuTree.CreateCustomDocument("RichTextEditor", "[出力]" + command + "!" + result);
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
    }

    public static void ExecuteInPlace(object sender, EventArgs e)
    {
      /*
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstring = button.Tag as String;

      String command = String.Empty;// null;
      String args = String.Empty;// null;
      String path = String.Empty;// null;
      String option = String.Empty;// null;
      String dir = String.Empty;
      try
      {
        String[] tmpstr = argstring.Split('|');
        command = ProcessVariable(tmpstr[0]);
        args = (tmpstr.Length > 1) ? ProcessVariable(tmpstr[1]) : String.Empty;// null;
        path = (tmpstr.Length > 2) ? ProcessVariable(tmpstr[2]) : String.Empty; //Enull;
        option = (tmpstr.Length > 3) ? ProcessVariable(tmpstr[3]) : String.Empty; //null;
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
      }
      if (argstring == "") return;
      // command前処理					
      if (command == String.Empty) command = path;
      else if (args == String.Empty) args = path;
      if (path != String.Empty)
      {
        if (System.IO.Directory.Exists(path)) dir = path;
        else if (System.IO.File.Exists(path)) dir = Path.GetDirectoryName(path);
        if (dir != String.Empty) System.IO.Directory.SetCurrentDirectory(dir);
      }
      ///////////////////////////////////////////////////////////////	
      try
      {
        SimplePanel simplePanel = new SimplePanel(menuTree.pluginUI);
        ((Control)simplePanel.Tag).Tag = argstring;
        simplePanel.Dock = DockStyle.Fill;
        DockContent document = PluginBase.MainForm.CreateCustomDocument(simplePanel);
        document.Tag = simplePanel;

        if (File.Exists(args) && File.Exists(command))
        {
          document.TabText = Path.GetFileNameWithoutExtension(command) + "!" + Path.GetFileName(args);
        }
        else if (File.Exists(command))
        {
          document.TabText = Path.GetFileName(command);
        }
        else
        {
          document.TabText = "Execute In Place";
        }
        // Patch 2016-03-23
        //this.AddPreviousCustomDocuments("SimplePanel!" + command + "|" + args);
        document.FormClosing += new FormClosingEventHandler(menuTree.CustomDocument_FormClosing);
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
      }
      */
    }

    public static void OpenDocument(object sender, EventArgs e)
    {
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstring = button.Tag as String;


      String command = String.Empty;
      String args = String.Empty;
      String path = String.Empty;
      String option = String.Empty;

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
      ///////////////////////////////////////////////////////////////
      //String result = "";
      // command前処理					
      if (command == String.Empty)
      {
        //if(path != String.Empty && Path)
        command = path;
      }
      else if (args == String.Empty) args = path;

      if (!File.Exists(command))
      {
        MessageBox.Show("path: " + command + " が見つかりません", "ActionManager.OpenDocument");
        return;
      }
      String file = command;
      Directory.SetCurrentDirectory(Path.GetDirectoryName(file));
      //PluginBase.MainForm.WorkingDirectory = Path.GetDirectoryName(file);
      //実行出力
      try
      {
        if (Lib.IsImageFile(file))
        {
          ActionManager.Picture(file);
          return;
        }
        else if (Lib.IsWebSite(file))
        {
          //menuTree.BrowseEx(file);
          ActionManager.BrowseEx(file);
          return;
        }
        //if (Lib.IsSoundFile(file) || Lib.IsVideoFile(file))
        //{
        //this.Player(file);
        //return;
        //}
        else if (!Lib.IsExecutableFile(file) && !Lib.IsSoundFile(file) && !Lib.IsVideoFile(file))
        {
          //MessageBox.Show(file);
          //PluginBase.MainForm.OpenEditableDocument(file);
          Process.Start(menuTree.antPanel.devenv15Path, "/edit " + "\"" + path + "\"");
          return;
        }
        else
        {
          if (option.ToLower().IndexOf("inplace") >= 0 || option.ToLower().IndexOf("inpanel") >= 0)
          {
            button.Tag = argstring;
            //menuTree.ExecuteInPlace(argstring);
            ExecuteInPlace(button, null);
            return;
          }
          else
          {
            Process.Start(command, args);
            return;
          }
        }
      }
      catch (Exception exc)
      {
        String errmsg = exc.Message.ToString();
        MessageBox.Show(errmsg, "ActionManager.OpenDocument");
      }
      return;
    }

    public static void OpenDocument(NodeInfo ni)
    {
      if (ni == null) return;

      ///////////////////////////////////////////////////////////////
      //String result = "";
      // command前処理					
      if (ni.Command == String.Empty) ni.Command = ni.Path;
      else if (ni.Args == String.Empty) ni.Args = ni.Path;

      if (!File.Exists(ni.Command))
      {
        MessageBox.Show("path: " + ni.Command + " が見つかりません", "ActionManager.OpenDocument");
        return;
      }
      String file = ni.Command;
      Directory.SetCurrentDirectory(Path.GetDirectoryName(file));
      //実行出力
      try
      {
        if (Lib.IsImageFile(file))
        {
          ni.Action = "picture";
          //ActionManager.Picture(file);
          ActionManager.Picture(ni);
          return;
        }
        else if (Lib.IsWebSite(file))
        {
          //menuTree.BrowseEx(file);
          ActionManager.BrowseEx(file);
          return;
        }
        else if (Lib.IsSoundFile(file) || Lib.IsVideoFile(file))
        {
          ni.Action = "player";
          //this.Player(file);
          ActionManager.Player(ni);
          return;
        }
        else if (!Lib.IsExecutableFile(file) && !Lib.IsSoundFile(file) && !Lib.IsVideoFile(file))
        {
          switch (ni.Option.ToLower())
          {
            case "sakura":
              Process.Start(settings.SakuraPath, file);
              break;
            case "pspad":
              Process.Start(settings.PspadPath, file);
              break;
            case "flashdevelop":
            case "fd":
              Process.Start(settings.FlashdevelopPath,file);
              break;
            case "editor":
            case "ant":
            case "panel":
              if (Path.GetExtension(file) == ".rtf")
              {
                ((Form1)menuTree.Tag).editor.richTextBox1.LoadFile(file);
              }
              else
              {
                ((Form1)menuTree.Tag).editor.richTextBox1.Text 
                  = Lib.File_ReadToEndDecode(file);
              }
              break;
            case "vs":
            case "visualstudio":
            default:
              Process.Start(menuTree.antPanel.devenv15Path, "/edit " + "\"" + file + "\"");
              return;
          }
        }
        else
        {
          if (ni.Option.ToLower().IndexOf("inplace") >= 0 || ni.Option.ToLower().IndexOf("inpanel") >= 0)
          {
            //button.Tag = argstring;
            //menuTree.ExecuteInPlace(argstring);
            //E/xecuteInPlace(button, null);
            return;
          }
          else
          {
            Process.Start(ni.Command, ni.Args);
            return;
          }
        }
      }
      catch (Exception exc)
      {
        String errmsg = exc.Message.ToString();
        MessageBox.Show(errmsg, "ActionManager.OpenDocument");
      }
      return;
    }
    
    public static void CustomDocument(object sender, EventArgs e)
    {
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstring = button.Tag as String;
      String command = String.Empty;
      String args = String.Empty;
      String path = String.Empty;
      String option = String.Empty;
 /*
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
      if (command == String.Empty)
      {
        MessageBox.Show("Command が設定されていません", "ActionManager.CustomDocument");
        return;
      }
      if (args == String.Empty) args = path;
      if (!string.IsNullOrEmpty(option)) menuTree.CreateCustomDocument(command, args, option);
      else menuTree.CreateCustomDocument(command, args);
      */
    }

    public static void RunTarget(object sender, EventArgs e)
    {
      //public void RunTarget(String file, String target)
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstring = button.Tag as String;
      String buildfile = String.Empty;
      /*
      if (argstring == "") return;
      NodeInfo ni = MakeNodeInfo(argstring);
      String command = Environment.SystemDirectory + "\\cmd.exe";

      if (File.Exists(ni.Path)) buildfile = ni.Path;
      else if (File.Exists(Path.Combine(Path.GetDirectoryName(ni.Path), "build.xml")))
      {
        buildfile = Path.Combine(Path.GetDirectoryName(ni.Path), "build.xml");
      }
      else if (File.Exists(Path.Combine(Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName), "build.xml")))
      {
        buildfile = Path.Combine(Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName), "build.xml");
      }
      else if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(),"build.xml")))
      {
        buildfile = Path.Combine(Directory.GetCurrentDirectory(), "build.xml");
      }
      else if(File.Exists(Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath),"build.xml")))
      {
        buildfile = Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), "build.xml");
      }
      if(buildfile == String.Empty)
      {
        MessageBox.Show("buildfile が存在しません", "ActionManager.RubTarget");
        return;
      }
      String arguments = "/c ";
      arguments += "ant";
      arguments += " -buildfile \"" + buildfile + "\" \"" + ni.Command + "\"";
      arguments += " -DprojectDir=\"" + Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath) + "\"";
      arguments += " -DcurDir=\"" + ProcessVariable("$(CurDir)") + "\"";
      arguments += " -DcurFile=\"" + ProcessVariable("$(CurFile)") + "\"";
      //kahata: Time-stamp: <2016-04-23 5:41:26 kahata>
      System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(buildfile));
      Process.Start("cmd.exe", "/k " + command + " " + arguments);
      if(ni.Option.ToLower() == "doclument")
      {
        String result = ProcessHandler.getStandardOutput("cmd.exe", command + " " + arguments);
        PluginBase.MainForm.CallCommand("New", "");
        PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
        PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(result);       //String output = Lib.StabdardOutput()
        Process.Start("cmd.exe", "/k " + command + " " + arguments);
      }
      else Process.Start("cmd.exe", "/k " + command + " " + arguments);
    */
    }

    public static void RunWshScript(object sender, EventArgs e)
    {
      /*
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstring = button.Tag as String;
      String buildfile = String.Empty;
       if (argstring == "") return;
      NodeInfo ni = MakeNodeInfo(argstring);

      String filename = Path.GetFileName(ni.Path);

      if (File.Exists(ni.Path)) buildfile = ni.Path;
      else if (File.Exists(Path.Combine(Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName), filename)))
      {
        buildfile = Path.Combine(Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName), filename);
      }
      else if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), filename)))
      {
        buildfile = Path.Combine(Directory.GetCurrentDirectory(), filename);
      }
      else if (File.Exists(Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), filename)))
      {
        buildfile = Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), filename);
      }
      if (buildfile == String.Empty)
      {
        MessageBox.Show("buildfile が存在しません", "ActionManager.RubTarget");
        return;
      }
      //CScript //Job:MyFirstJob MyScripts.wsf
      String command = Environment.SystemDirectory + "\\CScript.exe";
      String arguments = " //job:" + ni.Command;
      arguments += " \"" + buildfile + "\"";
      //kahata: Time-stamp: <2017-06-23 08:27:26 kahata>
      arguments += " /projectPath:\"" + PluginBase.CurrentProject.ProjectPath + "\"";
      arguments += " /projectDir:\"" + Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath) + "\"";
      arguments += " /curFile:\"" + ProcessVariable("$(CurFile)") + "\"";
      arguments += " /curDir:\"" + ProcessVariable("$(CurDir)") + "\"";
      //kahata: Time-stamp: <2016-04-23 5:41:26 kahata>
      try
      {
        System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(buildfile));
      }
      catch { }
      if (ni.Option.ToLower() == "document")
      {
        String result = ProcessHandler.getStandardOutput("cmd.exe", command + " " + arguments);
        PluginBase.MainForm.CallCommand("New", "");
        PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
        PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(result);       //String output = Lib.StabdardOutput()
      }
      else if (ni.Option.ToLower() == "trace")
      {
        //TraceManager.Add(command + " " + arguments);
        PluginBase.MainForm.CallCommand("RunProcessCaptured", command + ";" + arguments);
      }
      else
      {
        //MessageBox.Show("/k " + command + " " + arguments);
        Process.Start("cmd.exe", "/k " + command + " " + arguments);
      }
      */
    }

    /// ファイルをダウンロードし保存する
    /// https://dobon.net/vb/dotnet/internet/downloadfile.html
    /// WebHandler.DownLoadfile(String url, String fileName)
    public static void ExecuteScript(object sender, EventArgs e)
    {
/*
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstring = button.Tag as String;
      if (argstring == "") return;
      NodeInfo ni = MakeNodeInfo(argstring);
      ni.InnerText = String.Empty;
      ScriptManager.RunScript(ni);
*/
    }

    public static void PluginCommand(object sender, EventArgs e)
    {
      /*
            ToolStripMenuItem button = sender as ToolStripMenuItem;
            String argstring = button.Tag as String;
            if (argstring == "") return;

            //MessageBox.Show(argstring);

            NodeInfo ni = MakeNodeInfo(argstring);
            try
            {
              //MessageBox.Show(ni.Args, ni.Command);

              //PluginBase.MainForm.CallCommand("PluginCommand", ni.Command + ";" + ni.Path);
              PluginBase.MainForm.CallCommand("PluginCommand", ni.Command + ";" + ni.Args.Replace(";","semicolon"));
            }
            catch (Exception exc)
            {
              MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
            }
      */
    }

    public static void CallCommand(object sender, EventArgs e)
    {
      /*
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstring = button.Tag as String;
      if (argstring == "") return;
      NodeInfo ni = MakeNodeInfo(argstring);

       if (ni.Command == String.Empty) return;
       // その他 Menu の Click  
       if (ni.Path != String.Empty)
       {
         if (System.IO.Directory.Exists(Path.GetDirectoryName(ni.Path)))
         {
           System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(ni.Path));
         }
       }
       try
       {
         PluginBase.MainForm.CallCommand(ni.Command, ni.Args);
       }
       catch (Exception exc)
       {
         MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
       }
*/
    }

    public static void RunEmbedScript(object sender, EventArgs e)
    {
      /*
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      if(button.Tag is NodeInfo)
      {
        ScriptManager.RunScript((NodeInfo)button.Tag);
        return;
      }
      String argstring = button.Tag as String;
      if (argstring == "") return;
      */
    }

    public static void DefaultAction(object sender, EventArgs e)
    {
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstring = button.Tag as String;
      NodeInfo ni = MakeNodeInfo(argstring);

      if (File.Exists(ni.Path))
      {
        if (Lib.IsTextFile(ni.Path))
        {
          ActionManager.OpenDocument(ni);
        }
        else if (Path.GetExtension(ni.Path) == ".url")
        {
          StringBuilder sb = new StringBuilder(1024);
          IniFileHandler.GetPrivateProfileString(
            "InternetShortcut", "URL", "default", sb, (uint)sb.Capacity, ni.Path);
          //PluginBase.MainForm.CallCommand("Browse", sb.ToString());
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
         //PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.BrowseTo;" + ni.path);
      }
      else if (Lib.IsWebSite(ni.Path))
      {
        //ActionManager.BrowseEx(ni.Path);
        ActionManager.Browse(ni);
      }
    }

    public static NodeInfo MakeNodeInfo(String argstring)
    {
      NodeInfo ni = new NodeInfo();
      try
      {
        String[] tmpstr = argstring.Split('|');
        ni.Command = ProcessVariable(tmpstr[0]);
        ni.Args = (tmpstr.Length > 1) ? ProcessVariable(tmpstr[1]) : String.Empty;// null;
        ni.Path = (tmpstr.Length > 2) ? ProcessVariable(tmpstr[2]) : String.Empty; //Enull;
        ni.Option = (tmpstr.Length > 3) ? ProcessVariable(tmpstr[3]) : String.Empty; //null;
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
      }
      return ni;
    }

    public static void EvalScript(object sender, EventArgs e)
    {
      /*
       ToolStripMenuItem button = sender as ToolStripMenuItem;
      if(button.Tag is String)
      {
        String argstring = button.Tag as String;
        if (argstring == "") return;

        NodeInfo ni = MakeNodeInfo(argstring);
        ScriptManager.EvalScript(ni.Command.Replace("semicolon",";"));
      }
      */
    }

    /// <summary>
    /// static でないと callPluginCommandエラー
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void Hello(object sender, EventArgs e)
    {
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String msg = (string)button.Tag;
      //NodeInfo ni = MakeNodeInfo(sender);
      MessageBox.Show("ActionManager Hello関数からのご挨拶です " + msg, "Hello");
    }

    public static void InsertTimeStamp(object sender, EventArgs e)
    {
      /*
      try
      {
        string timestamp = StringHandler.timestamp();
        PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(timestamp);
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
      */
    }
    
    public static void InsertCHeading(object sender, EventArgs e)
    {
      /*
      try
      {
        string fileName = PluginBase.MainForm.CurrentDocument.FileName;
        string cheading = StringHandler.CHeading(fileName);
        PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
        PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(cheading);
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
      */
    }

    public static void InsertLocalUrl(object sender, EventArgs e)
    {
      /*
      try
      {
        //string fileName = PluginBase.MainForm.CurrentDocument.FileName;
        string fileName = PluginBase.MainForm.CurrentDocument.FileName;
        string text = fileName.Replace("F:", "http://localhost/f").Replace("\\", "/");
        //string text = fileName.Replace(AntPlugin.settings.DocumentRoot, this.settings.ServerRoot).Replace("\\", "/");
        PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(text);
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
      */
    }

    public static void SwitchToBrowseEx(object sender, EventArgs e)
    {
      /*
      List<string> list = new List<string>();
      for (int i = 0; i < PluginBase.MainForm.Documents.Length; i++)
      {
        if (PluginBase.MainForm.Documents[i].IsBrowsable)
        {
          string item = ((WebBrowser)((UserControl)PluginBase.MainForm.Documents[i].Controls[0]).Controls[0]).Url.ToString();
          list.Add(item);
        }
      }
      ITabbedDocument[] documents = PluginBase.MainForm.Documents;
      for (int j = 0; j < documents.Length; j++)
      {
        ITabbedDocument tabbedDocument = documents[j];
        if (tabbedDocument.IsBrowsable)
        {
          tabbedDocument.Close();
        }
      }
      foreach (string current in list)
      {
        //menuTree.BrowseEx(current);
        ActionManager.BrowseEx(current);
      }
    */
    }

    public void SwitchToMainFormBrowser(object sender, EventArgs e)
    {
      /*
      List<string> list = new List<string>();
      for (int i = 0; i < PluginBase.MainForm.Documents.Length; i++)
      {
        if (PluginBase.MainForm.Documents[i].Controls[0].GetType().FullName == "AntPlugin.XmlTreeMenu.Controls.Browser")
        {
          string item = ((Control)PluginBase.MainForm.Documents[i].Controls[0].Tag).Tag.ToString();
          list.Add(item);
        }
      }
      ITabbedDocument[] documents = PluginBase.MainForm.Documents;
      for (int j = 0; j < documents.Length; j++)
      {
        ITabbedDocument tabbedDocument = documents[j];
        if (tabbedDocument.Controls[0].GetType().FullName == "AntPlugin.XmlTreeMenu.Controls.Browser")
        {
          tabbedDocument.Close();
        }
      }
      foreach (string current in list)
      {
        PluginBase.MainForm.CallCommand("Browse", current);
      }
      */
    }

    public static void RunProcessDialog(object sender, EventArgs e)
    {
      /*
      if (menuTree.runProcessDialog.ShowDialog(menuTree) == DialogResult.OK)
      {
        Directory.SetCurrentDirectory(menuTree.runProcessDialog.executeDirectoryComboBox.Text);
        if (menuTree.runProcessDialog.panelOutputCheckBox.Checked)
        {
          Path.GetFileName(menuTree.runProcessDialog.commandComboBox.Text);
          string standardOutput = ProcessHandler.getStandardOutput(menuTree.runProcessDialog.commandComboBox.Text, menuTree.runProcessDialog.argumentComboBox.Text);
          TraceManager.Add(standardOutput);
        }
        if (menuTree.runProcessDialog.consoleOutputCheckBox.Checked)
        {
          Process.Start(@"C:\windows\system32\cmd.exe", "/k, " + menuTree.runProcessDialog.commandComboBox.Text + " " + menuTree.runProcessDialog.argumentComboBox.Text);
          return;
        }
        Process.Start(menuTree.runProcessDialog.commandComboBox.Text, menuTree.runProcessDialog.argumentComboBox.Text);
        return;
      }
      else
      {
        if (menuTree.runProcessDialog.DialogResult == DialogResult.Yes)
        {
          MessageBox.Show("YESボタンが押されました。");
          return;
        }
        if (menuTree.runProcessDialog.DialogResult == DialogResult.Cancel)
        {
          MessageBox.Show("キャンセルボタンが押されました。");
        }
        return;
      }
      */
    }

    public static void Open(object sender, EventArgs e)
    {
      /*
      menuTree.openFileDialog.Multiselect = true;
      menuTree.openFileDialog.InitialDirectory = PluginBase.MainForm.WorkingDirectory;
      if (menuTree.openFileDialog.ShowDialog(menuTree) == DialogResult.OK && menuTree.openFileDialog.FileName.Length != 0)
      {
        int num = menuTree.openFileDialog.FileNames.Length;
        for (int i = 0; i < num; i++)
        {
          menuTree.OpenDocument(menuTree.openFileDialog.FileNames[i]);
        }
      }
      menuTree.openFileDialog.Multiselect = false;
      */
    }

    public static void EncodeSave(object sender, EventArgs e)
    {
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      if (button.Tag is String)
      {
        string encoding = button.Tag as String;
        EncodeSave(encoding);
      }
    }

    public static void EncodeSave(string encoding)
    {
      /*
      ScintillaControl sciControl = PluginBase.MainForm.CurrentDocument.SciControl;
      string fileName = PluginBase.MainForm.CurrentDocument.FileName;
      int codePage = sciControl.CodePage;
      if (encoding == "SHIFT_JIS")
      {
        DataConverter.ChangeEncoding(sciControl.Text, codePage, 932);
        WriteFileEncoding(fileName, sciControl.Text, Encoding.GetEncoding("Shift_JIS"));
        return;
      }
      if (encoding == "UTF-8")
      {
        //FIXME
        //sciControl.set_SaveBOM(false);
        DataConverter.ChangeEncoding(sciControl.Text, codePage, 65001);
        WriteFileEncoding(fileName, sciControl.Text, Encoding.UTF8, false);
      }
      */
    }

    public static void WriteFileEncoding(string file, string text, Encoding encoding)
    {
      try
      {
        using (StreamWriter streamWriter = new StreamWriter(file, false, encoding))
        {
          streamWriter.Write(text);
          streamWriter.Close();
        }
      }
      catch (Exception ex)
      {
        //ErrorManager.ShowError(ex);
      }
      
    }

    public static void WriteFileEncoding(string file, string text, Encoding encoding, bool saveBOM)
    {
      try
      {
        using (StreamWriter streamWriter = (encoding == Encoding.UTF8 && !saveBOM) ? new StreamWriter(file, false) : new StreamWriter(file, false, encoding))
        {
          streamWriter.Write(text);
          streamWriter.Close();
        }
      }
      catch (Exception ex)
      {
        //ErrorManager.ShowError(ex);
      }
    }

    public static void CloseOpen(object sender, EventArgs e)
    {
      /*
      string arg_0F_0 = PluginBase.MainForm.CurrentDocument.FileName;
      PluginBase.MainForm.CurrentDocument.Close();
      menuTree.openFileDialog.Multiselect = true;
      menuTree.openFileDialog.InitialDirectory = PluginBase.MainForm.WorkingDirectory;
      if (menuTree.openFileDialog.ShowDialog(menuTree) == DialogResult.OK && menuTree.openFileDialog.FileName.Length != 0)
      {
        int num = menuTree.openFileDialog.FileNames.Length;
        for (int i = 0; i < num; i++)
        {
          menuTree.OpenDocument(menuTree.openFileDialog.FileNames[i]);
        }
      }
      menuTree.openFileDialog.Multiselect = false;
    */
    }

    public static void CloseReOpen(object sender, EventArgs e)
    {
      /*
      string fileName = PluginBase.MainForm.CurrentDocument.FileName;
      PluginBase.MainForm.CurrentDocument.Close();
      menuTree.OpenDocument(fileName);
      */
    }

    public static void Picture(string argstring)
    {
      /*
      String command = String.Empty;// null;
      String args = String.Empty;// null;
      String path = String.Empty;// null;
      String option = String.Empty;// null;
      String dir = String.Empty;
      try
      {
        String[] tmpstr = argstring.Split('|');
        command = ProcessVariable(tmpstr[0]);
        args = (tmpstr.Length > 1) ? ProcessVariable(tmpstr[1]) : String.Empty;// null;
        path = (tmpstr.Length > 2) ? ProcessVariable(tmpstr[2]) : String.Empty; //Enull;
        option = (tmpstr.Length > 3) ? ProcessVariable(tmpstr[3]) : String.Empty; //null;
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()), "TreeMenu:Picture:1647");
        //return;
      }

      if (argstring == "") return;

      if (path != String.Empty)
      {
        if (System.IO.Directory.Exists(path)) dir = path;
        else if (System.IO.File.Exists(path)) dir = Path.GetDirectoryName(path);
        if (dir != String.Empty) System.IO.Directory.SetCurrentDirectory(dir);
      }
      ///////////////////////////////////////////////////////////////	
      System.Windows.Forms.PictureBox pictureBox2 = new System.Windows.Forms.PictureBox();
      pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

      try
      {
        switch (command.ToLower())
        {
          case "qcgraph":
            break;
          case "script":
            pictureBox2.Tag = "script!" + path;
            break;
          default:
            //if (path == String.Empty) path = command;
            if (File.Exists(path))
            {
              pictureBox2.Image = System.Drawing.Image.FromFile(path);
              pictureBox2.Tag = path;
              pictureBox2.DoubleClick += new System.EventHandler(pictureBox2_DoubleClick);
            }
            else if (File.Exists(command))
            {
              pictureBox2.Image = System.Drawing.Image.FromFile(command);
              pictureBox2.Tag = command;
              pictureBox2.DoubleClick += new System.EventHandler(pictureBox2_DoubleClick);
            }
            else
            {
              pictureBox2.Image = null;// System.Drawing.Image.FromFile(command);
              pictureBox2.Tag = "dummy";//  command;
            }
            break;
        }
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()), "TreeMenu:Picture:1708");
        pictureBox2.Image = null;// System.Drawing.Image.FromFile(path);
        pictureBox2.Tag = null;// path;
      }
      DockContent document2 = PluginBase.MainForm.CreateCustomDocument(pictureBox2);
      document2.Text = Path.GetFileName((String)pictureBox2.Tag).Replace("qcgraph!", "");
      //String data = document2.Text + "|" + pictureBox2.GetType().FullName + "|" + pictureBox2.Tag.ToString();
      //String data = pictureBox2.Tag.ToString();
      //this.AddPreviousCustomDocuments(data);
      //document2.FormClosing += new FormClosingEventHandler(this.CustomDocument_FormClosing);
      */
    }

    public static void Picture(NodeInfo ni)
    {
      String dir = String.Empty;
      if (ni == null) return;

      if (ni.Path != String.Empty)
      {
        if (System.IO.Directory.Exists(ni.Path)) dir = ni.Path;
        else if (System.IO.File.Exists(ni.Path)) dir = Path.GetDirectoryName(ni.Path);
        if (dir != String.Empty) System.IO.Directory.SetCurrentDirectory(dir);
      }
      ///////////////////////////////////////////////////////////////	
      PictureBox pictureBox2 = ((Form1)menuTree.antPanel.Tag).picturePanel.pictureBox1;
      try
      {
        switch (ni.Command.ToLower())
        {
          case "qcgraph":
            break;
          case "script":
            pictureBox2.Tag = "script!" + ni.Path;
            break;
          default:
            if (File.Exists(ni.Path))
            {
              pictureBox2.Image = System.Drawing.Image.FromFile(ni.Path);
              pictureBox2.Tag = ni.Path;
              pictureBox2.DoubleClick += new System.EventHandler(pictureBox2_DoubleClick);
            }
            else if (File.Exists(ni.Command))
            {
              pictureBox2.Image = System.Drawing.Image.FromFile(ni.Command);
              pictureBox2.Tag = ni.Command;
              pictureBox2.DoubleClick += new System.EventHandler(pictureBox2_DoubleClick);
            }
            else
            {
              pictureBox2.Image = null;// System.Drawing.Image.FromFile(command);
              pictureBox2.Tag = "dummy";//  command;
            }
            break;
        }
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()), "TreeMenu:Picture:1708");
        pictureBox2.Image = null;// System.Drawing.Image.FromFile(path);
        pictureBox2.Tag = null;// path;
      }
      //String data = pictureBox2.Tag.ToString();
      //this.AddPreviousCustomDocuments(data);
      //document2.FormClosing += new FormClosingEventHandler(this.CustomDocument_FormClosing);
    }

    public static void Player(String path)
    {
      if (Lib.IsSoundFile(path) || Lib.IsVideoFile(path))
      {
        ((Form1)menuTree.antPanel.Tag).axWindowsMediaPlayer1.URL = path;
      }
    }

    public static void Player(NodeInfo ni)
    {
      if (Lib.IsSoundFile(ni.Path) || Lib.IsVideoFile(ni.Path))
      {
        //((Form1)menuTree.antPanel.Tag).axWindowsMediaPlayer1.URL = ni.Path;
        ((Form1)menuTree.antPanel.Tag).player.axWindowsMediaPlayer1.URL = ni.Path;
      }
    }

    private static void pictureBox2_DoubleClick(object sender, EventArgs e)
    {
      System.Windows.Forms.PictureBox item = (System.Windows.Forms.PictureBox)sender;
      String path = item.Tag as String;
      Process.Start(path);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    public static void BrowseEx(String path,bool inner =false)
    {
      String url = string.Empty;
      path = ProcessVariable(path);
      try
      {
        if (Path.GetExtension(path) == ".url")
        {
          StringBuilder sb = new StringBuilder(1024);
          IniFileHandler.GetPrivateProfileString("InternetShortcut", "URL", "default", sb, (uint)sb.Capacity, path);
          url = sb.ToString();
        }
        else if (Path.GetExtension(path).ToLower() == ".php")
        {
          url = path.Replace(@"C:\Apache2.2\htdocs", "http://localhost").Replace("\\", "/");
          url = url.Replace(@"F:\", "http://localhost/f").Replace("\\", "/");
        }
        else url = path;
        // https://msdn.microsoft.com/ja-jp/library/c42zyyew.aspx
        //View.ShowWebBrowser URL[/ new][/ext]
        // /new省略可能です。 ページを Web ブラウザーの新しいページに表示します。
        /// ext 省略可能です。 IDE の外部にある既定の Web ブラウザーにページを表示します。
        if(inner == true)
        {
          String arguments = "/Command \"navigate " + url + "\"";
          //Process.Start(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe",arguments);
          Process.Start(menuTree.antPanel.devenv15Path, arguments);
        }
        else
        {
          //Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", url);
          Process.Start(settings.ChromePath, url);
        }
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()), "TreeMenu:BrowseEx:1763");
      }
    }

    public static void Browse(NodeInfo ni)
    {
      String url = string.Empty;
      String path = ProcessVariable(ni.Path);
      try
      {
        if (Path.GetExtension(path) == ".url")
        {
          StringBuilder sb = new StringBuilder(1024);
          IniFileHandler.GetPrivateProfileString("InternetShortcut", "URL", "default", sb, (uint)sb.Capacity, path);
          url = sb.ToString();
        }
        else if (Path.GetExtension(path).ToLower() == ".php")
        {
          url = path.Replace(@"C:\Apache2.2\htdocs", "http://localhost").Replace("\\", "/");
          url = url.Replace(@"F:\", "http://localhost/f").Replace("\\", "/");
        }
        else url = path;
        switch (ni.Option.ToLower())
        {
          // https://msdn.microsoft.com/ja-jp/library/c42zyyew.aspx
          //View.ShowWebBrowser URL[/ new][/ext]
          // /new省略可能です。 ページを Web ブラウザーの新しいページに表示します。
          /// ext 省略可能です。 IDE の外部にある既定の Web ブラウザーにページを表示します。
          case "vs":
          case "inner":
          case "visualstudio":
            String arguments = "/Command \"navigate " + url + "\"";
            Process.Start(settings.Devenv15Path, arguments);
            break;
          case "antpanel":
          case "ant":
          case "panel":
            if (url.Trim() != "") ((Form1)menuTree.antPanel.Tag).browser.webBrowser1.Navigate(url);
            else ((Form1)menuTree.antPanel.Tag).browser.webBrowser1.GoHome();
            break;
          case "ie":
            Process.Start(settings.IePath, url);
            break;
          case "chrome":
          default:
            Process.Start(settings.ChromePath, url);
            break;
        }
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()), "TreeMenu:BrowseEx:1763");
      }
    }


    public static void BrowseExString(object sender, EventArgs e)
    {
      /*
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      if (button.Tag is String)
      {
        string htmlString = button.Tag as string;
        BrowseExString(htmlString);
      }
      */
    }

    //http://red-treasure.com/report/?p=101
    public static void BrowseExString(String htmlString)
    {
      /*
      try
      {
        Browser browser = new Browser();
        browser.Dock = DockStyle.Fill;
        DockContent document = PluginBase.MainForm.CreateCustomDocument(browser);
        document.TabText = "[BrowseExString]";
        document.FormClosing += new FormClosingEventHandler(menuTree.CustomDocument_FormClosing);
        if (browser.WebBrowser.Document != null)
        {
          browser.WebBrowser.Document.OpenNew(true);
          browser.WebBrowser.Document.Write(htmlString);
        }
        else
        {
          browser.WebBrowser.DocumentText = htmlString;
        }
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
      }
      */
    }

    public static void PSPadTemplate(object sender, EventArgs e)
    {
      /*
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      if (button.Tag is String)
      {
        string msg = button.Tag as string;
        PSPadTemplate(msg);
      }
      */
    }

    public static void PSPadTemplate(string msg)
    {
      /*
      DialogResult dialogResult = MessageBox.Show("pprプロジェクトを作成しますか？", msg, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
      if (dialogResult == DialogResult.Cancel) return;
      switch (msg)
      {
        case "make_VC2008_CLR_ConsoleApplication_ppr":
          PSPadProject.make_VC2008_CLR_ConsoleApplication_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_VC2008_CLR_WindowsApplication_ppr":
          PSPadProject.make_VC2008_CLR_WindowsApplication_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_VCS2008_ConsoleApplication_ppr":
          PSPadProject.make_VCS2008_ConsoleApplication_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_VCS2008_WindowsApplication_ppr":
          PSPadProject.make_VCS2008_WindowsApplication_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_VC6_ConsoleApplication_ppr":
          PSPadProject.make_VC6_ConsoleApplication_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_VC6_WindowApplication_ppr":
          PSPadProject.make_VC6_WindowApplication_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_Qt4_ppr":
          PSPadProject.make_Qt4_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_java_ConApp_ppr":
          PSPadProject.make_java_ConApp_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_java_GUIApp_ppr":
          PSPadProject.make_java_GUIApp_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_java_Applet_ppr":
          PSPadProject.make_java_Applet_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_appletviewer_wsf":
          PSPadProject.make_appletviewer_wsf(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_flex3_ppr":
          PSPadProject.make_flex3_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_flex4_ppr":
          PSPadProject.make_flex4_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_air4_ppr":
          PSPadProject.make_air4_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_svg_ppr":
          PSPadProject.make_svg_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_latex_ppr":
          PSPadProject.make_latex_ppr(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_redirect_refresh_html":
          PSPadProject.make_redirect_refresh_html(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_redirect_frame_html":
          PSPadProject.make_redirect_frame_html(PluginBase.MainForm.CurrentDocument.FileName);
          return;
        case "make_redirect_reopen_html":
          PSPadProject.make_redirect_reopen_html(PluginBase.MainForm.CurrentDocument.FileName);
          return;
      }
      */
    }

    public static string GetCurrentDocumentPath()
    {
      /*
      string text = "";
      if (PluginBase.MainForm.CurrentDocument.IsEditable)
      {
        text = PluginBase.MainForm.CurrentDocument.FileName;
      }
      else if (PluginBase.MainForm.CurrentDocument.IsBrowsable)
      {
        text = ((WebBrowser)((UserControl)PluginBase.MainForm.CurrentDocument.Controls[0]).Controls[0]).Url.ToString();
      }
      else
      {
        try
        {
          text = ((Control)PluginBase.MainForm.CurrentDocument.Controls[0].Tag).Tag.ToString();
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message.ToString());
          return "";
        }
      }
      currentDocumentPath = text;
      PluginBase.MainForm.StatusLabel.Text = text;

      return text;
      */
      return string.Empty;
    }

    public static void Exit(object sender, EventArgs e)
    {
/*
      menuTree.pluginUI.SaveCustomSession();
      // Not Installed
      //PluginBase.MainForm.CallCommand("PluginCommand", "Ant.SaveCustomSession");
      PluginBase.MainForm.CallCommand("Exit", "");
      */
    }

    #region XmlTreeMenu Plugin Function EventType.Command:

    public void LoadFile(string path, TreeNode parentNode = null)
    {
      /*
      //MessageBox.Show(path);
      if (MDIForm.CommonLibrary.Lib.IsWebSite(path))
      {
        //「+」が半角スペースにデコードされるようにするには、次のようにする
        string urlDec = Uri.UnescapeDataString(path.Replace('+', ' '));
        NodeInfo ni = new NodeInfo();
        ni.Path = path;
        ni.Title = urlDec;
        ni.Type = "record";
        TreeNode tn = new TreeNode(ni.Title);
        tn.Tag = ni;
        //tn.ImageIndex = tn.SelectedImageIndex = 15;// 9;//8;// 5;
        tn.ImageIndex = tn.SelectedImageIndex = this.GetIconImageIndexFromIconPath(path);

        if (parentNode != null) parentNode.Nodes.Add(tn);
        else this.treeView1.Nodes.Add(tn);
        return;
      }

      // This path is a directory Time-stamp: <2017-07-27 9:38:14 kahata>
      else if (Directory.Exists(path))
      {
        TreeNode tn = this.GetDirectoryTreeNode(path);
        if (tn != null)
        {
          if (parentNode != null) parentNode.Nodes.Add(tn);
          else this.treeView1.Nodes.Add(tn);
          this.treeView1.ShowNodeToolTips = true;
        }
        return;
      }

      else if (path.IndexOf(";") > -1)
      {
        try
        {
          Dictionary<string, string> dic = MDIForm.CommonLibrary.StringHandler.Get_Values(path, ';', '=');
          NodeInfo ni = new NodeInfo();
          try { ni.Type = dic["Type"]; } catch { ni.Type = "record"; }
          try { ni.Title = dic["Title"]; } catch { ni.Title = "無題"; }
          try { ni.Tooltip = dic["Tooltip"]; } catch { ni.Tooltip = String.Empty; }
          try { ni.PathBase = dic["PathBase"]; } catch { ni.PathBase = String.Empty; }
          try { ni.Action = dic["Action"]; } catch { ni.Action = String.Empty; }
          try { ni.Command = dic["Command"]; } catch { ni.Command = String.Empty; }
          try { ni.Path = dic["Path"]; } catch { ni.Path = String.Empty; }
          try { ni.Args = dic["Args"]; } catch { ni.Args = String.Empty; }
          try { ni.Icon = dic["Icon"]; } catch { ni.Icon = String.Empty; }
          try { ni.Option = dic["Option"]; } catch { ni.Option = String.Empty; }
          TreeNode tn = new TreeNode(ni.Title);
          tn.Tag = ni;

          if (ni.Icon != string.Empty)
          {
            tn.ImageIndex = tn.SelectedImageIndex = this.GetIconImageIndexFromIconPath(ni.Icon);
          }
          else if (ni.Command != string.Empty && ni.Command != null)
          {
            tn.ImageIndex = tn.SelectedImageIndex = 9;
          }
          else if (File.Exists(ni.Path))
          {
            tn.ImageIndex = tn.SelectedImageIndex = this.GetIconImageIndex(ni.Path);
          }
          else
          {
            tn.ImageIndex = tn.SelectedImageIndex = 8;
          }
          if (parentNode != null) parentNode.Nodes.Add(tn);
          else this.treeView1.Nodes.Add(tn);
          //this.treeView1.Nodes.Add(tn);
        }
        catch { }
        return;
      }
      if (this.treeMenuFile.Contains(path)) return;
      this.currentTreeMenuFilepath = path;

      if (this.toolStripMenuItem1.Checked == false) this.treeView1.Nodes.Clear();
      this.importXmlDocument = new XmlDocument();
      XmlNode xmlNode = null;

      //http://stackoverflow.com/questions/11492705/how-to-create-xml-document-using-xmldocument
      if (Path.GetExtension(path) == ".wax" || Path.GetExtension(path) == ".asx")
      {
        try
        {
          String asxstring = MDIForm.CommonLibrary.Lib.File_ReadToEndDecode(path);
          List<String> asxs = XMLManager.get_ElementStringByTagName("ASX", asxstring);
          if (asxs.Count == 0) return;
          XmlDocument xmldoc = new XmlDocument();
          xmldoc.LoadXml(asxs[0]);
          xmlNode = xmldoc.DocumentElement;
        }
        catch { }
      }
      // patch 2017-06-24 11:24:30
      else if (Path.GetExtension(path) != ".html"
        && Path.GetExtension(path) != ".htm" && Path.GetExtension(path) != ".hta")// その他一般ファイルpath
      {
        try
        {
          this.importXmlDocument.Load(path);
          xmlNode = this.importXmlDocument.DocumentElement;
        }
        catch { }
      }
      // <XmlTreeMenu>の処理
      String fdpstring = String.Empty;
      if (Path.GetExtension(path) != ".gradle" && Path.GetFileName(path) != "pom.xml")
      {
        fdpstring = MDIForm.CommonLibrary.Lib.File_ReadToEndDecode(path);
      }
      List<String> treeMenuTag = XMLManager.get_ElementStringByTagName("XmlTreeMenu", fdpstring);
      string xml = String.Empty;
      if (xmlNode == null)
      {
        //http://stackoverflow.com/questions/11492705/how-to-create-xml-document-using-xmldocument
        XmlDocument xmldoc = new XmlDocument();
        XmlElement element_link = xmldoc.CreateElement(string.Empty, "link", string.Empty);
        if (treeMenuTag.Count > 0)
        {
          try
          {
            xml = XMLManager.GetNode(treeMenuTag[0]).InnerXml;
            xml = XMLManager.StripWhiteSpace(xml);
            XmlDocumentFragment xfrag = xmldoc.CreateDocumentFragment();
            xfrag.InnerXml = xml;// "<folder title=\"試験 うまくいくかな\"></folder>";
            //xmldoc.DocumentElement.AppendChild(xfrag);
            element_link.AppendChild(xfrag);
          }
          catch { }
        }
        // cs, java, php, js 等 xml形式で解析できない一犯ファイルは全てここを通る
        // 一般ファイルのxmlNodeを生成する
        //MessageBox.Show(path, "xmlNode == null");
        xmldoc.AppendChild(element_link);
        xmlNode = xmldoc.DocumentElement;
      }
      else
      {
        if (treeMenuTag.Count > 0)
        {
          try
          {
            xml = XMLManager.OutterXMLToNode(treeMenuTag[0]).InnerXml;
            xml = XMLManager.StripWhiteSpace(xml);
            XmlDocumentFragment xfrag = this.importXmlDocument.CreateDocumentFragment();
            xfrag.InnerXml = xml;// "<folder title=\"試験 うまくいくかな\"></folder>";
            this.importXmlDocument.DocumentElement.AppendChild(xfrag);
          }
          catch { }
        }
      }
      //全てのファイルはここを通る
      NodeInfo nodeInfo = this.SetNodeinfo(xmlNode, path);
      TreeNode treeNode = this.BuildTreeNode(nodeInfo, path);

      if (Path.GetExtension(path) == ".fdp")
      {
        //MessageBox.Show(path);
        TreeNode tn = this.GetDirectoryTreeNode(Path.GetDirectoryName(path));
        treeNode.Nodes.Add(tn);
      }

      if (parentNode != null) parentNode.Nodes.Add(treeNode);
      else this.treeView1.Nodes.Add(treeNode);

      this.RecursiveBuildToTreeNode(xmlNode, treeNode);
      if (nodeInfo.Expand)
      {
        treeNode.Expand();

        this.UpdateExpand(treeNode);
        this.currentTreeMenuFilepath = path;
        this.currentTreeNode = treeNode;
      }
      this.treeMenuFile.Add(path);
    */
    }

    public void ImportXml()
    {
      /*
      string path = this.FileOpenDialog();
      this.treeMenuFile.Clear();
      this.treeView1.Nodes.Clear();
      this.LoadFile(path);
      this.currentTreeMenuFilepath = path;
      */
    }

    public void Test(string msg)
    {
      /*
      switch (msg)
      {
        case "Ant.Properties":
          MessageBox.Show(msg);
          foreach (KeyValuePair<string, string> pair in AntProperties)
          {
            MessageBox.Show(string.Format("Key : {0} / Value : {1}", pair.Key, pair.Value));
          }
          break;

        case "IProject.Properties":
          //MessageBox.Show(msg);
          MessageBox.Show(PluginBase.CurrentProject.PreferredSDK, "PreferredSDK");
          MessageBox.Show(PluginBase.CurrentProject.Language, "Language");
          MessageBox.Show(PluginBase.CurrentProject.Name, "Name");
          MessageBox.Show(PluginBase.CurrentProject.CurrentSDK, "CurrentSDK");
          MessageBox.Show(PluginBase.CurrentProject.DefaultSearchFilter, "DefaultSearchFilter");
          MessageBox.Show(PluginBase.CurrentProject.TraceEnabled.ToString(), "Name");
          MessageBox.Show(PluginBase.CurrentProject.EnableInteractiveDebugger.ToString(), "Name");
          MessageBox.Show(PluginBase.CurrentProject.Name, "Name");
          MessageBox.Show(PluginBase.CurrentProject.OutputPathAbsolute, "OutputPathAbsolute");
          MessageBox.Show(PluginBase.CurrentProject.ProjectPath, "ProjectPath");

          for (int i = 0; i < PluginBase.CurrentProject.SourcePaths.Length; i++)
          {
            MessageBox.Show(PluginBase.CurrentProject.SourcePaths[i], "SourcePaths");
          }
          break;

        case "CSScript.LoadCode":
          Assembly assembly = CSScript.LoadCode(
                @"using System;
              public class Calculator
              {
                    static public int Add(int a, int b)
                    {
                       return a + b;
                    }
              }");

          AsmHelper calc = new AsmHelper(assembly);
          int sum = (int)calc.Invoke("Calculator.Add", 1, 2); //sum == 3; 

          MessageBox.Show(sum.ToString());
          break;
      }




      //FileInfo[] selectedPathsAndFiles = this.GetFileExlorerSelecredItems();
      //foreach (FileInfo info in selectedPathsAndFiles) MessageBox.Show(info.FullName);

      // Pojectを開く
      // 以下が正解
      //String swfPath = "F:\\HomePage\\travel.coocan-kahata\\Flash\\ApacheFlex\\mx\\controls\\CheckBox\\CheckBoxExample.swf";
      ////PluginBase.MainForm.CallCommand("PluginCommand", "FlashViewer.Document;" + swfPath);
      //PluginBase.MainForm.CallCommand("PluginCommand", "ProjectManagerCommands.PlayOutput;" + swfPath);


      ////////////////////////////////////////////////////////////////////////////////////			
			// * FtpClint
			//http://dobon.net/vb/dotnet/internet/ftpwebrequest.html
			//Uri requestUri = new Uri("ftp://ftp.travel.coocan.jp/kahata.travel.coocan.jp/homepage/");
			//FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUri);
			//ftpWebRequest.Credentials = new NetworkCredential("kahata.travel.coocan.jp", "QE5Xq7pT");
			//ftpWebRequest.Method = "LIST";
			//ftpWebRequest.KeepAlive = false;
			//ftpWebRequest.UsePassive = false;
			//FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
			//StreamReader streamReader = new StreamReader(ftpWebResponse.GetResponseStream());
			//string text = streamReader.ReadToEnd();
			//MessageBox.Show(text);
			//TraceManager.Add(text);
			//streamReader.Close();
			//MessageBox.Show(string.Format("{0}: {1}", ftpWebResponse.StatusCode, ftpWebResponse.StatusDescription));
			//TraceManager.Add(string.Format("{0}: {1}", ftpWebResponse.StatusCode, ftpWebResponse.StatusDescription));
			//ftpWebResponse.Close();
      //ProjectManager.OpenProject
      */
      }
 
    #endregion

  }
}

