using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AntPlugin.CommonLibrary;
using AntPanelApplication;
using CommonLibrary;
//using PluginCore.Helpers;

namespace AntPlugin.CommonLibrary
{

  public partial class GradleTree : UserControl
  {
    public AntPanel pluginUI;
    //public PluginMain pluginMain;

    public static string appDir = System.Windows.Forms.Application.StartupPath;

    public ImageList imagelist1;
    public static String targetpublic_iconPath = Path.Combine(appDir, @"SettingData\targetpublic_obj.png");
    public static String defaulttarget_iconPath = Path.Combine(appDir, @"SettingData\defaulttarget_obj.png");
    public static String targetinternal_iconPath = Path.Combine(appDir, @"SettingData\targetinternal_obj.png");
    public static List<TaskInfo> taskList = new List<TaskInfo>();
    public static List<String> publicTaskList = new List<string>();
    public static Dictionary<String, TaskInfo> publicTasksDic = new Dictionary<string, TaskInfo>();

    public GradleTree()
    {
      InitializeComponent();
    }

    public GradleTree(AntPanel ui)
    {
      this.pluginUI = ui;
      //this.pluginMain = ui.pluginMain;
      this.imagelist1 = ui.imageList;
      InitializeComponent();
    }

    /// <summary>
    /// 正規表現を使って文字列を検索し抽出する
    /// </summary>
    /// https://dobon.net/vb/dotnet/string/regexmatch.html
    /// <param name="path"></param>
    /// <returns></returns>
    public AntTreeNode GetGradleOutlineTreeNode(string path)
    {
      string defaultTask = GetDefaultTask(path);
      TaskInfo ti;

      AntTreeNode gradleNode = new AntTreeNode(Path.GetFileName(path), 5);

      if (!String.IsNullOrEmpty(defaultTask))
      {
        AntTreeNode defaultTargetNode = new AntTreeNode(defaultTask, 1);
        defaultTargetNode.NodeFont = new Font("Meiryo UI", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 128);
        defaultTargetNode.File = path;
        ti = new TaskInfo();
        ti.Name = defaultTask;
        ti.BuildFile = path;
        ti.IconPath = defaulttarget_iconPath;
        ti.publicTask = true;
        ti.defaultTask = true;
        defaultTargetNode.Tag = ti;
        gradleNode.Nodes.Add(defaultTargetNode);
      }

      publicTasksDic = GetPublicTasksDic(path);
      foreach (var k in publicTasksDic)
      {
        String targetName = ((TaskInfo)k.Value).Name;
        AntTreeNode targetNode = new AntTreeNode(targetName, 3);
        targetNode.File = ((TaskInfo)k.Value).BuildFile;
        targetNode.Target = targetName;
        targetNode.Tag = (TaskInfo)k.Value;
        if (((TaskInfo)k.Value).Description != String.Empty) targetNode.ToolTipText = ((TaskInfo)k.Value).Description;
        gradleNode.Nodes.Add(targetNode);
      }

      String gradleAllTasksPath = Path.Combine(Path.GetDirectoryName(path), @"obj\GradleAllTasks.xml");
      String folder = Path.Combine(Path.GetDirectoryName(path), "obj");
      if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
      if (!File.Exists(gradleAllTasksPath)) Lib.File_SaveUtf8(gradleAllTasksPath, GetXmlStringGradleAllTasks(path));


      //仮 commentout
      //gradleNode.Nodes.Add(this.pluginUI.menuTree.getXmlTreeNode(gradleAllTasksPath, true));



      TaskInfo tii = new TaskInfo();
      tii.Args = null;
      tii.BuildFile = path;
      tii.DefaultTask = false;
      tii.Description = String.Empty;
      tii.IconPath = String.Empty;
      tii.Name = "GradleBuildNode";
      tii.PublicTask = false;
      tii.InnerCode = String.Empty;

      gradleNode.Tag = tii;
      //gradleNode.Tag = path;
      gradleNode.Target = String.Empty;// "GradleBuildNode";
      gradleNode.File = path;
      gradleNode.ToolTipText = path;
      return gradleNode;
    }

    public static String GetXmlStringGradleAllTasks(String file)
    {
      String xmlstr = String.Empty;
      String defaultTasks = String.Empty;

      String iconPath = Path.Combine(System.Windows.Forms.Application.StartupPath, @"SettingData\targetpublic_obj.png");
      publicTasksDic = GetPublicTasksDic(file);

      String output = ProcessHandler.getStandardOutput(@"F:\gradle-3.5\bin\gradle.bat",
        "-q tasks --all -b " + file);// @"F:\java\gradle\swt-snippets\Snippet001\build.gradle");
      string[] lines = output.Split('\n');

      String tag = String.Empty;
      string tooltip = String.Empty;
      Boolean firstflag = true;
      for (int i = 0; i < lines.Length; i++)
      {
        if (lines[i].IndexOf("Rules") != -1)
        {
          //xmlstr += "</folder>\n";
          break;
        }

        
        if (lines[i].IndexOf("Default tasks:") > -1)
        {

          //String[] tmp = lines[i].Split(':');

          defaultTasks = lines[i].Replace("Default tasks:", "").Trim().Replace("\n", "").Replace("\'", "");
          /*
          String target = lines[i].Replace("Default tasks:","").Trim().Replace("\n", "").Replace("\'","");
          tooltip = lines[i].Trim().Replace("\n", "");
          iconPath = defaulttarget_iconPath;
          tag = "<record title=\""
            //+ target + "\" action=\"RunProcess\" command=\"cmd.exe\" args=\"/k gradle -q "
            + target + "\" action=\"RunProcess\" command=\"cmd.exe\" args=\"/k gradle "
            + target + " -b " + file + "\" path=\"" + file + "\""
            + " tooltip=\"" + tooltip + "\""
            //+ " icon=\"image:10\" />" + "\n";
            + " icon=\"" + iconPath + "\" />" + "\n";
          xmlstr += tag;
          tag = "";
          */
        }
       
        if (lines[i].Trim().Replace("\n", "").EndsWith("tasks"))
        {
          String folderName = lines[i].Trim().Replace("\n", "");
          //if (folderName == "Application tasks")
          if(firstflag == true)
          {
            xmlstr += "<folder title=\"" + folderName + "\" >\n";
            firstflag = false;
          }
          else
          {
            xmlstr += "</folder>\n<folder title=\"" + folderName + "\" >\n";
          }
        }
        if (lines[i].IndexOf("---") == -1 && lines[i].IndexOf("Default") == -1
          && lines[i].IndexOf(" tasks") == -1 && lines[i].Trim() != String.Empty)
        {
          string line = lines[i].Replace(" - ", "@");
          String[] tmp = line.Split('@');
          String target = tmp[0].Trim().Replace("\n", "");
          try
          {
            tooltip = tmp[1].Trim().Replace("\n", "");
          }
          catch
          {
            tooltip = target; //String.Empty;
          }
          //if (target == GetDefaultTask(file)) iconPath = defaulttarget_iconPath;
          if (target == defaultTasks) iconPath = defaulttarget_iconPath;
          else if (publicTasksDic.ContainsKey(target)) iconPath = targetpublic_iconPath;
          else iconPath = targetinternal_iconPath;

          tag = "<record title=\""
            //+ target + "\" action=\"RunProcess\" command=\"cmd.exe\" args=\"/k gradle -q "
            + target + "\" action=\"RunProcess\" command=\"cmd.exe\" args=\"/k gradle "
            + target + " -b " + file + "\" path=\"" + file + "\""
            + " tooltip=\"" + tooltip + "\""
            //+ " icon=\"image:10\" />" + "\n";
            + " icon=\"" + iconPath + "\" />" + "\n";
          xmlstr += tag;
          tag = "";
        }
      }
      xmlstr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<root title = \""
      + Path.GetFileName(file) + " All Tasks \">\n" + xmlstr + "</folder></root>\n";
      return xmlstr;
    }

    public static String GetTextStringGradleAllTasks(String file)
    {
      String output = ProcessHandler.getStandardOutput(@"F:\gradle-3.5\bin\gradle.bat",
        "-q tasks --all -b " + file);// @"F:\java\gradle\swt-snippets\Snippet001\build.gradle");
      return output;
    }

    /// <summary>
    /// gradle tasks --all 出力保存ファイルよりxml生成
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static String GetXmlStringGradleAllTasksFromTextFile(String file)
    {
      String xmlstr = String.Empty;
      String defaultTasks = String.Empty;

      String iconPath = Path.Combine(System.Windows.Forms.Application.StartupPath, @"SettingData\targetpublic_obj.png");
      publicTasksDic = GetPublicTasksDic(file);

      //String output = ProcessHandler.getStandardOutput(@"F:\gradle-3.5\bin\gradle.bat",
      //  "-q tasks --all -b " + file);// @"F:\java\gradle\swt-snippets\Snippet001\build.gradle");

      String output = GradleParserUtils.GetSource(file);
      string[] lines = output.Split('\n');

      String tag = String.Empty;
      string tooltip = String.Empty;
      Boolean firstflag = true;
      for (int i = 0; i < lines.Length; i++)
      {
        if (lines[i].IndexOf("Rules") != -1)
        {
          //xmlstr += "</folder>\n";
          break;
        }
        if (lines[i].IndexOf("Default tasks:") > -1)
        {
          defaultTasks = lines[i].Replace("Default tasks:", "").Trim().Replace("\n", "").Replace("\'", "");
        }
        if (lines[i].Trim().Replace("\n", "").EndsWith("tasks"))
        {
          String folderName = lines[i].Trim().Replace("\n", "");
          //if (folderName == "Application tasks")
          if (firstflag == true)
          {
            xmlstr += "<folder title=\"" + folderName + "\" >\n";
            firstflag = false;
          }
          else
          {
            xmlstr += "</folder>\n<folder title=\"" + folderName + "\" >\n";
          }
        }
        if (lines[i].IndexOf("---") == -1 && lines[i].IndexOf("Default") == -1
          && lines[i].IndexOf(" tasks") == -1 && lines[i].Trim() != String.Empty)
        {
          string line = lines[i].Replace(" - ", "@");
          String[] tmp = line.Split('@');
          String target = tmp[0].Trim().Replace("\n", "");
          try
          {
            tooltip = tmp[1].Trim().Replace("\n", "");
          }
          catch
          {
            tooltip = target; //String.Empty;
          }
          if (target == defaultTasks) iconPath = defaulttarget_iconPath;
          else if (publicTasksDic.ContainsKey(target)) iconPath = targetpublic_iconPath;
          else iconPath = targetinternal_iconPath;

          tag = "<record title=\""
            //+ target + "\" action=\"RunProcess\" command=\"cmd.exe\" args=\"/k gradle -q "
            + target + "\" action=\"RunProcess\" command=\"cmd.exe\" args=\"/k gradle "
            + target + " -b " + file + "\" path=\"" + file + "\""
            + " tooltip=\"" + tooltip + "\""
            //+ " icon=\"image:10\" />" + "\n";
            + " icon=\"" + iconPath + "\" />" + "\n";
          xmlstr += tag;
          tag = "";
        }
      }
      xmlstr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<root title = \""
      + Path.GetFileName(file) + " All Tasks \">\n" + xmlstr + "</folder></root>\n";
      return xmlstr;
    }

    public static Dictionary<String, TaskInfo> GetPublicTasksDic(String path)
    {
      publicTasksDic = new Dictionary<string, TaskInfo>();
      string defaultTask = GetDefaultTask(path);
      TaskInfo ti;

      String rawCode = Lib.File_ReadToEndDecode(path);
      //var cc = new DeComment();
      //String code = cc.Execute(rawCode);
      string code = GradleParserUtils.DeComment(rawCode);
      
      string taskRegex1 = @"task\s+(?<task>[\w\d\:\.]+)\s*" + @"\((?<args>.*)\)(?=\s*\{)";
      string taskRegex2 = @"task\s+(?<task>[\w\d\:\.]+)\s*" + @"(?=\s*\{)";
      string taskRegex = "(" + taskRegex1 + ")|(" + taskRegex2 + ")";
      string descriptionRegex = "description\\s*=\\s*[\"\'](?<description>.+?)[\"\']";

      System.Text.RegularExpressions.MatchCollection mc =
            System.Text.RegularExpressions.Regex.Matches(
               code, taskRegex,
              System.Text.RegularExpressions.RegexOptions.IgnoreCase
        | System.Text.RegularExpressions.RegexOptions.Multiline);

      foreach (System.Text.RegularExpressions.Match m in mc)
      {
        String targetName = m.Groups["task"].Value;
        int start = m.Groups["task"].Index;
        String args = m.Groups["args"].Value;
        int innerStart = code.IndexOf("{", start);
        string definition = code.Substring(start, innerStart - start).Trim();
        
        int innerEnd = GradleParserUtils.BraceMatch(code, innerStart);
        string innerCode = code.Substring(innerStart, innerEnd - innerStart);
        string description = String.Empty;
        try
        {
          System.Text.RegularExpressions.MatchCollection mc1 =
            System.Text.RegularExpressions.Regex.Matches(
            innerCode, descriptionRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase
              | System.Text.RegularExpressions.RegexOptions.Singleline);
          description = mc1[0].Groups["description"].Value;
        }
        catch { }

        start = rawCode.IndexOf(definition);
        Int32 end = GradleParserUtils.BraceMatch(rawCode, start);
        string outerCode = rawCode.Substring(start, end - start + 1);
        innerCode = rawCode.Substring(rawCode.IndexOf("{", start) + 1, end - rawCode.IndexOf("{", start) - 1);

        ti = new TaskInfo();
        ti.Name = targetName;
        ti.BuildFile = path;
        ti.DefaultTask = false;
        ti.Definition = definition;
        ti.Args = args;
        ti.IconPath = targetpublic_iconPath;
        ti.PublicTask = true;
        ti.Description = description;
        ti.outerStart = start;
        ti.outerEnd = end;
        ti.InnerCode = innerCode;
        ti.OuterCode = outerCode;
        try
        {
          publicTasksDic.Add(targetName, ti);
        }
        catch(Exception exc)
        {
          String excMsg = exc.Message.ToString();
          MessageBox.Show(excMsg,targetName);
        }
      }
      return publicTasksDic;
    }

    public static String GetDefaultTask(String file)
    {
      String code = Lib.File_ReadToEndDecode(file);
      String defaultTask = String.Empty;
      System.Text.RegularExpressions.MatchCollection mc =
            System.Text.RegularExpressions.Regex.Matches(
        code, @"defaultTasks\s+\'(?<task>[\w\:\.]+)\'",
        System.Text.RegularExpressions.RegexOptions.IgnoreCase
        | System.Text.RegularExpressions.RegexOptions.Singleline);

      if (mc.Count > 0)
      {
        defaultTask = mc[0].Groups["task"].Value;
        //MessageBox.Show(defaultTask);
      }
      return defaultTask;
    }
    
    public static List<TaskInfo> GetAllTasksList(String file)
    {
      taskList = new List<TaskInfo>();
 
      String output = ProcessHandler.getStandardOutput(@"F:\gradle-3.5\bin\gradle.bat",
        "-q tasks --all -b " + file);// @"F:\java\gradle\swt-snippets\Snippet001\build.gradle");

      // コメントの除去
      var cc = new DeComment();
      //var text = File.ReadAllText("Sample.cs");
      output = cc.Execute(output);

      string[] lines = output.Split('\n');

      String tag = String.Empty;
      string tooltip = String.Empty;
      for (int i = 0; i < lines.Length; i++)
      {
        if (lines[i].IndexOf("Rules") != -1) break;

        if (lines[i].IndexOf("---") == -1 && lines[i].IndexOf("Default") == -1
           && lines[i].IndexOf(" tasks") == -1 && lines[i].Trim() != String.Empty)
        {
          string line = lines[i].Replace(" - ", "@");
          String[] tmp = line.Split('@');
          String target = tmp[0].Trim().Replace("\n", "");
          try
          {
            tooltip = tmp[1].Trim().Replace("\n", "");
          }
          catch
          {
            tooltip = target; //String.Empty;
          }
          TaskInfo ti = new TaskInfo();
          ti.Name = target;
          ti.BuildFile = file;
          ti.Description = tooltip;
          if (target == GetDefaultTask(file))
          {
            ti.IconPath = defaulttarget_iconPath;
            ti.DefaultTask = true;
          }
          else
          {
            ti.IconPath = targetinternal_iconPath;
            ti.DefaultTask = false;
          }
        }

        String code = Lib.File_ReadToEndDecode(file);
        System.Text.RegularExpressions.MatchCollection mc =
              System.Text.RegularExpressions.Regex.Matches(
              code, @"task\s+(?<task>\w+)",
              System.Text.RegularExpressions.RegexOptions.IgnoreCase
              //| System.Text.RegularExpressions.RegexOptions.Singleline);
              | System.Text.RegularExpressions.RegexOptions.Multiline);
        foreach (System.Text.RegularExpressions.Match m in mc)
        {
          String targetName = m.Groups["task"].Value;
          TaskInfo ti1 = new TaskInfo();
          ti1.Name = targetName;
          ti1.IconPath = targetpublic_iconPath;
          ti1.publicTask = true;

          publicTasksDic.Add(targetName, ti1);
          //publicTaskList.Add(targetName);
        }


        foreach (TaskInfo ti in taskList)
        {
          if (publicTasksDic.ContainsKey(ti.Name)) //  publicTaskList.Contains(ti.Name))
          {
            ti.iconPath = targetpublic_iconPath;
            ti.PublicTask = true;
          }
        }
      }
      return taskList;
    }
  }
}

/*
else if (Path.GetFileName(path) == "pom.xml")
{
String output = String.Empty;
try
{
if (File.Exists(Path.Combine(Path.GetDirectoryName(path), @"obj\mavenGoalsData.xml")))
{
this.importXmlDocument.Load(Path.Combine(Path.GetDirectoryName(path), @"obj\mavenGoalsData.xml"));
}
else
{
String defaultpom = Path.Combine(PathHelper.BaseDir, @"SettingData\mavenGoalsData.xml");
this.importXmlDocument.Load(defaultpom);
}
xmlNode = this.importXmlDocument.DocumentElement;
}
catch { }
}
*/






