//using CSScriptLibrary;
using AntPlugin.CommonLibrary;
using MDIForm;
using PluginCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
//using XMLTreeMenu;
//using MDIForm;

namespace AntPlugin.XmlTreeMenu.Managers
{
  public class ScriptManager
  {

    public static void EvalScript(String code)
    {
      // PlayerPanel の EvalScriptを実行する
      XmlMenuTree menuTree = new XmlMenuTree();
      Control player = menuTree.CreateCustomDockControl("PlayerPanel", "");
      ((IMDIForm)player).Instance.callPluginCommand("EvalScript", code);
      player.Dispose();
      menuTree.Dispose();
      // TODO: scripting.dll を参照しない実装
      //MessageBox.Show(code);
      //Assembly assembly = CSScript.LoadCode(code);
      //AsmHelper helper = new AsmHelper(assembly);
      //helper.Invoke("*.Execute", new object[] { }); //sum == 3; 
      //MessageBox.Show(sum.ToString());
  }

    public static string ProcessVariable(string strVar)
    {
      string arg = string.Empty;
      try
      {
        arg = PluginBase.MainForm.ProcessArgString(strVar);
      }
      catch { arg = strVar; }
      try
      {
        arg = arg.Replace("$(CurProjectDir)", Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath));
        arg = arg.Replace("$(CurProjectName)", Path.GetFileNameWithoutExtension(PluginBase.CurrentProject.ProjectPath));
        arg = arg.Replace("$(ProjectDir)", Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath));
        arg = arg.Replace("$(ProjectName)", Path.GetFileNameWithoutExtension(PluginBase.CurrentProject.ProjectPath));
        //arg = arg.Replace("$(CurProjectUrl)", Lib.Path2Url(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), this.settings.DocumentRoot, this.settings.ServerRoot));
        //arg = arg.Replace("$(Quote)", "\"");
        //arg = arg.Replace("$(Dollar)", "$");
        //arg = arg.Replace("$(AppDir)", PathHelper.AppDir);
        //arg = arg.Replace("$(BaseDir)", PathHelper.BaseDir);
        //arg = arg.Replace("$(CurSciText)", PluginBase.MainForm.CurrentDocument.SciControl.Text);
        //arg = arg.Replace("$(CurFileUrl)", Lib.Path2Url(PluginBase.MainForm.CurrentDocument.FileName, this.settings.DocumentRoot, this.settings.ServerRoot));
        //arg = arg.Replace("$(ControlCurFilePath)", this.controlCurrentFilePath);
        //arg = arg.Replace("$(ControlCurFileDir)", Path.GetDirectoryName(this.controlCurrentFilePath));
        //arg = arg.Replace("$(CurControlFilePath)", this.controlCurrentFilePath);
        //arg = arg.Replace("$(CurControlFileDir)", Path.GetDirectoryName(this.controlCurrentFilePath));

        //string[] PluginBase.CurrentProject.SourcePaths

        arg = ApplyAntProperties(arg);
      }
      catch
      {
      }
      return arg;
    }

    public static string ApplyAntProperties(string strVar)
    {
      foreach (KeyValuePair<string, string> item in XmlMenuTree.AntProperties)
      {
        //Console.WriteLine("[{0}:{1}]", item.Key, item.Value);
        strVar = strVar.Replace("$(" + item.Key + ")", item.Value);
      }
      return strVar;
    }


    public static void RunScript(NodeInfo ni)
    {
      String command = ProcessVariable(PluginBase.MainForm.ProcessArgString(ni.Command));
      String innerText = ProcessVariable(ni.InnerText);
      String option = ProcessVariable(ni.Option);
      String args = ProcessVariable(ni.Args);
      String path = ProcessVariable(ni.Path); ;

      String executable = String.Empty;
      String output = String.Empty;
      String code = innerText.Replace("<![CDATA[", "").Replace("]]>", "").Trim();
      string arguments = String.Empty;
      code = code.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
      //メッセージボックスを表示する
      DialogResult result = MessageBox.Show(command + " : 実行しますか？",
        ni.Title,
      //DialogResult result = MessageBox.Show(code,
      //    "以下のスクリプトを実行しますか？",
          MessageBoxButtons.YesNoCancel,
          MessageBoxIcon.Exclamation,
          MessageBoxDefaultButton.Button1);
      //何が選択されたか調べる
      if (result == DialogResult.No || result == DialogResult.Cancel) return;

      if (command == String.Empty) command = path;
      else if (args == String.Empty) args = path;

      // パラメーター設定
      switch (Path.GetExtension(command))
      {
        case ".php":
          executable = @"C:\eclipse461\xampp\php\php.exe";//version 5.6.24
          if (Path.GetFileNameWithoutExtension(command) == "embed") command = Create_F_TempFile("Main.php", code);
          else if (Lib.IsWebSite(command))
          {
            //WebHandler.DownLoadfile(command, @"F:\temp\Main.php");
            //command = @"F:\temp\Main.php";
            code = WebHandler.file_get_contents(command);
            //MessageBox.Show(code);
            command = Create_F_TempFile("Main.php", code);
          }
          break;
          case ".js":
          executable = @"C:\windows\system32\cscript.exe";
          if (Path.GetFileNameWithoutExtension(command) == "embed") command = Create_F_TempFile("Main.js", code);
          //else if (WebHandler.SiteExists(command))
          else if (Lib.IsWebSite(command))
          {
            //WebHandler.DownLoadfile(command, @"F:\temp\Main.js");
            //command = @"F:\temp\Main.js";
            code = WebHandler.file_get_contents(command);
            //MessageBox.Show(code);
            command = Create_F_TempFile("Main.js", code);
          }
          break;
          case ".vbs":
          executable = @"C:\windows\system32\cscript.exe";
          if (Path.GetFileNameWithoutExtension(command) == "embed") command = Create_F_TempFile("Main.vbs", code);
          //else if (WebHandler.SiteExists(command))
          else if (Lib.IsWebSite(command))
          {
            //WebHandler.DownLoadfile(command, @"F:\temp\Main.vbs");
            //command = @"F:\temp\Main.vbs";
            code = WebHandler.file_get_contents(command);
            //MessageBox.Show(code);
            command = Create_F_TempFile("Main.vbs", code);
          }
          break;
        case ".wsf":
          executable = @"C:\windows\system32\cscript.exe";
          if (Path.GetFileNameWithoutExtension(command) == "embed") command = Create_F_TempFile("Main.wsf", code);
          //else if (WebHandler.SiteExists(command))
          else if (Lib.IsWebSite(command))
          {
            //WebHandler.DownLoadfile(command, @"F:\temp\Main.wsf");
            //command = @"F:\temp\Main.wsf";
            code = WebHandler.file_get_contents(command);
            //MessageBox.Show(code);
            command = Create_F_TempFile("Main.wsf", code);
          }
          break;
        case ".cs":
          executable = @"C:\HDD_F\Programs\cs-script\cscs.exe";
          if (Path.GetFileNameWithoutExtension(command) == "embed") command = Create_F_TempFile("Main.cs", code);
          //else if (WebHandler.SiteExists(command))
          else if (Lib.IsWebSite(command))
          {
            //WebHandler.DownLoadfile(command, @"F:\temp\Main.cs");
            //command = @"F:\temp\Main.cs";
            code = WebHandler.file_get_contents(command);
            //MessageBox.Show(code);
            command = Create_F_TempFile("Main.cs", code);
          }
          break;
        case ".ant":
          // TODO : test
          executable = @"F:\ant\apache-ant-1.10.1\bin\ant.bat";//version 1.10.1
          if (Path.GetFileNameWithoutExtension(command) == "embed") command = Create_F_TempFile("build.xml", code);
          break;
        case ".gradle":
          // TODO : test
          executable = @"F:\gradle-3.5\bin\gradle.bat";//version 3.5
          if (Path.GetFileNameWithoutExtension(command) == "embed") command = Create_F_TempFile("build.gradle", code);
          break;
        case ".bat":
        case ".cmd":
          // TODO : test
          if (Path.GetFileNameWithoutExtension(command) == "embed") executable = Create_F_TempFile("Main.bat", code);
          command = String.Empty;
          break;
        default:
          //TODO coding??
          break;
      }

      // カレントディレクトリ設定
      String dir = String.Empty;
      if (!String.IsNullOrEmpty(path))
      {
        if (System.IO.Directory.Exists(path)) dir = path;
        else if (System.IO.File.Exists(path)) dir = Path.GetDirectoryName(path);
        if (dir != String.Empty) System.IO.Directory.SetCurrentDirectory(dir);
      }

      // 実行
      if (command.ToLower() == "fdscript" || command.ToLower() == "development")
      {
        if (Path.GetExtension(args.ToLower()) == ".cs" && File.Exists(args))
        {
          PluginBase.MainForm.CallCommand("ExecuteScript", "Development;" + args);
        }
        else if (Path.GetExtension(path.ToLower()) == ".cs" && File.Exists(path))
        {
          PluginBase.MainForm.CallCommand("ExecuteScript", "Development;" + path);
        }
        else if (code != String.Empty)
        {
          //EvalScript(code);
          args = Create_F_TempFile("Main.cs", code);
          PluginBase.MainForm.CallCommand("ExecuteScript", "Development;" + args);
        }
      }
      else if (File.Exists(command))
      {
        switch (option.ToLower())
        {
          case "trace":
            //TraceManager}.Add(result);
            break;
          case "document":
            if (Path.GetExtension(command) == ".php") arguments = "-f " + command + " " + args;
            else if (Path.GetExtension(command) == ".gradle") arguments = "-b " + command + " " + args;
            else if (Path.GetFileName(command) =="build.xml") arguments = "-buildfile " + command + " " + args;
            else arguments =command + " " + args;
            output = ProcessHandler.getStandardOutput(executable, arguments);
            StringHandler.ConvertEncoding(output, Encoding.UTF8);
            PluginBase.MainForm.CallCommand("New", "");
            PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
            PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(output);
            PluginBase.MainForm.CurrentDocument.Text = "[出力:php]" + Path.GetFileNameWithoutExtension(command);//title
            break;
          case "chrome":
            if (Path.GetExtension(command) == ".php")
            {
              arguments = (args != String.Empty) ? Lib.Path2Url(command) + "?" + args : Lib.Path2Url(command);
              Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", arguments);
            }
            break;
          case "browse":
            if (Path.GetExtension(command) == ".php")
            {
              arguments = (args != String.Empty) ? Lib.Path2Url(command) + "?" + args : Lib.Path2Url(command);
              PluginBase.MainForm.CallCommand("Browse", arguments);
            }
            break;
          case "console":
          default:
            if (Path.GetExtension(command) == ".php")
              Process.Start("cmd.exe", "/k " + executable + " -f " + command + " " + args);
            else if (Path.GetExtension(command) == ".gradle")
              Process.Start("cmd.exe", "/k " + executable + " -b " + command + " " + args);
            else if (Path.GetFileName(command) == "build.xml")
            {
              //MessageBox.Show(executable + " -buildfile " + command + " " + args);
              Process.Start("cmd.exe", "/k " + executable + " -buildfile " + command + " " + args);
            }
            else
              Process.Start("cmd.exe", "/k " + executable + " " + command + " " + args);
            break;
        }
      }
    }

    public static void RunScript(TreeNode selectedNode)
    {
      NodeInfo ni = selectedNode.Tag as NodeInfo;
      //String pathbase = PluginBase.MainForm.ProcessArgString(ni.PathBase);
      String action = PluginBase.MainForm.ProcessArgString(ni.Action);
      String command = ProcessVariable(PluginBase.MainForm.ProcessArgString(ni.Command));
      String innerText = ProcessVariable(ni.InnerText);
      String option = ProcessVariable(ni.Option);
      String args = ProcessVariable(ni.Args);
      String path = ProcessVariable(ni.Path); ;
      String title = ProcessVariable(ni.Title);
      String executable = "php";
      String output = String.Empty;
      String code = innerText.Replace("<![CDATA[", "").Replace("]]>", "");
      String argstring = String.Empty;

      string content = ni.XmlNode.InnerText.Trim().Trim('\n');
      String tempFilePath = String.Empty;

      switch (command)
      {
        case "php":
          executable = @"C:\eclipse461\xampp\php\php.exe";
          if (code != String.Empty) tempFilePath = Create_F_TempFile("Main.php", code);
          switch (option.ToLower())
          {
            case "console":
              if (File.Exists(path))
              {
                argstring = "/k " + executable + " -f " + path + " " + args;
              }
              else if (path == "code")
              {
                if (code != String.Empty)
                {
                  code = code.Replace("<?php", "").Replace("?>", "").Replace("\"", @"\""").Replace("\n", "");
                  argstring = "/k " + executable + " -r " + "\"" + code + "\" " + " " + args;
                }
                else argstring = String.Empty;
              }
              else
              {
                if (tempFilePath != String.Empty)
                {
                  argstring = "/k " + executable + " -f " + tempFilePath + " " + args;
                }
                else argstring = String.Empty;
              }
              if (argstring != String.Empty) Process.Start("cmd.exe", argstring);
              break;
            case "trace":
              //TraceManager}.Add(result);
              break;
            case "document":
              argstring = "-f " + tempFilePath + " " + args;
              output = ProcessHandler.getStandardOutput(executable, argstring);
              StringHandler.ConvertEncoding(output, Encoding.UTF8);
              PluginBase.MainForm.CallCommand("New", "");
              PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
              PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(output);
              PluginBase.MainForm.CurrentDocument.Text = "[出力:php]" + title;
              break;
            case "chrome":

              if (File.Exists(path))
              {
                argstring = (args != String.Empty) ? Lib.Path2Url(path) + "?" + args : Lib.Path2Url(path);
                Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", argstring);
              }
              else if (File.Exists(tempFilePath))
              {
                argstring = (args != String.Empty) ? Lib.Path2Url(tempFilePath) + "?" + args : Lib.Path2Url(tempFilePath);
                Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", argstring);
              }
              break;
            case "browse":
            default:
              if (File.Exists(path))
              {
                PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseEx;" + Lib.Path2Url(path));
              }
              else if (path == "localhost" && File.Exists(tempFilePath))
              {
                argstring = (args != String.Empty) ? Lib.Path2Url(tempFilePath) + "?" + args : Lib.Path2Url(tempFilePath);
                PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseEx;" + argstring);
              }
              else if (path != String.Empty && File.Exists(tempFilePath))
              {
                output = ProcessHandler.getStandardOutput(executable, " -f " + tempFilePath + " " + args);
                PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseExString;" + output.Replace(";", "semicolon"));
              }
              else if (code != String.Empty)
              {
                // ラインコメントを有効にするため \n削除を外す 2017-06-29 19:05:00 要注意
                code = code.Replace("<?php", "").Replace("?>", "").Replace("\"", @"\""");//.Replace("\n", "");
                //MessageBox.Show(code);
                output = ProcessHandler.getStandardOutput(executable, " -r " + "\"" + code + "\" " + args);
                //MessageBox.Show(output);
                //this.BrowseExString(output);
                // NG output うまく引き渡せない fix OK!!
                PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseExString;" + output.Replace(";", "semicolon"));
              }
              break;
              //default:
              //Process.Start("cmd.exe", "/k " + executable + " " + argstring);
              //break;
          }
          break;
        case "fdscript":
          if (File.Exists(path)) PluginBase.MainForm.CallCommand("ExecuteScript", "Development;" + path);
          else if (code != String.Empty) EvalScript(code);
          break;
        case "wsf":
        case "js":
        case "vbs":
          executable = @"C:\windows\system32\cscript.exe";
          if (File.Exists(path)) Process.Start("cmd.exe", "/k " + executable + " " + path + " " + args);
          else if (code != String.Empty)
          {
            if (command == "wsf") tempFilePath = Create_F_TempFile("Main.wsf", code);
            else if (command == "js") tempFilePath = Create_F_TempFile("Main.js", code);
            else if (command == "vbs") tempFilePath = Create_F_TempFile("Main.vbs", code);
            Process.Start("cmd.exe", "/k " + executable + " " + tempFilePath + " " + args);
          }
          break;
        case "cs":
          executable = @"C:\HDD_F\Programs\cs-script\cscs.exe";

          if (File.Exists(path))
          {
            System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
            Process.Start("cmd.exe", "/k " + executable + " " + path + " " + args);
          }
          else if (WebHandler.SiteExists(path))
          {
            string temppath = Path.Combine(@"F:\temp", "Main.cs");
            WebHandler.DownLoadfile(path, temppath);
            argstring = temppath + " " + args;

            ni.Path = temppath;
            System.IO.Directory.SetCurrentDirectory(@"F:\temp");
            Process.Start("cmd.exe", "/k " + executable + " " + argstring);
          }
          else if (code != String.Empty)
          {
            tempFilePath = Create_F_TempFile("Main.cs", code);
            argstring = tempFilePath + " " + args;
            System.IO.Directory.SetCurrentDirectory(@"F:\temp");
            Process.Start("cmd.exe", "/k " + executable + " " + argstring);
          }
          break;
      }
    }

    public static string CreateTempFile(string filename, string code)
    {
      //一時ファイル名の取得と作成
      //MessageBox.Show(System.IO.Path.GetTempFileName(),"TempFileName");
      //一時ディレクトリ名
      //MessageBox.Show(System.IO.Path.GetTempPath(),"TempDir");
      string tempFilePath = Path.Combine(Path.GetTempPath(), filename);
      Lib.File_SaveEncode(tempFilePath, code, "UTF-8");
      return tempFilePath;
    }

    public static string Create_F_TempFile(string filename, string code)
    {
      //一時ファイル名の取得と作成
      //MessageBox.Show(System.IO.Path.GetTempFileName(),"TempFileName");
      //一時ディレクトリ名
      //MessageBox.Show(System.IO.Path.GetTempPath(),"TempDir");
      string tempFilePath = Path.Combine(@"F:\temp", filename);
      //Lib.File_SaveEncode(tempFilePath, code, "UTF-8");
      Lib.File_SaveUtf8(tempFilePath, code);
      return tempFilePath;
    }
 
    public static List<String> getFiles(String startPath, String filePattern)
    {
      List<String> files = new List<String>();
      foreach (String file in Directory.GetFiles(startPath, filePattern, SearchOption.AllDirectories))
      {
        files.Add(file.ToString());
        // do something with this file
      }
      return files;
    }

    public static String GetFDTreeMenuDataStorageFolder()
    {
      String projectFolder = Path.GetDirectoryName(
        PluginBase.CurrentProject.ProjectPath);
      String FDTreeMenuDataStorageFolder = Path.Combine(projectFolder, "obj");

      if (PluginBase.CurrentProject.PreferredSDK != null)
      {
        //MessageBox.Show("FDTreeMenuDataStorageFolder: " + PluginBase.CurrentProject.PreferredSDK);
        FDTreeMenuDataStorageFolder = PluginBase.CurrentProject.PreferredSDK;
      }
      return FDTreeMenuDataStorageFolder;
    }

    public static String GetFDTreeMenuPath()
    {
      String AppDir = Path.GetDirectoryName(Application.ExecutablePath);
      // 例外 ??
      String ProjectDir =  System.Environment.CurrentDirectory;
      try
      {
        ProjectDir = Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath);
      }catch { }
      String FDTreeMenuPath = Path.Combine(AppDir, @"SettingData\XmlTreeMenu.xml");
      if (File.Exists(PluginBase.CurrentProject.OutputPathAbsolute)
        && Path.GetFileName(PluginBase.CurrentProject.OutputPathAbsolute) == "FDTreeMenu.xml")
      {
        FDTreeMenuPath = PluginBase.CurrentProject.OutputPathAbsolute;
      }
      else if (File.Exists(Path.Combine(ProjectDir, @"FDTreeMenu.xml")))
      {
        FDTreeMenuPath = Path.Combine(ProjectDir, @"FDTreeMenu.xml");
      }
      else if (File.Exists(Path.Combine(ProjectDir, @"obj\FDTreeMenu.xml")))
      {
        FDTreeMenuPath = Path.Combine(ProjectDir, @"obj\FDTreeMenu.xml");
      }
      //MessageBox.Show(FDTreeMenuPath);
      return FDTreeMenuPath;
    }

  }

}
 
