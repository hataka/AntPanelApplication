using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace AntPanelApplication
{
  public partial class AntPanel : UserControl
  {
    public const int ICON_FILE = 0;
    public const int ICON_DEFAULT_TARGET = 1;
    public const int ICON_INTERNAL_TARGET = 2;
    public const int ICON_PUBLIC_TARGET = 3;

    private ContextMenuStrip buildFileMenu;
    private ContextMenuStrip targetMenu;
    private List<String> buildFilesList = new List<string>();

    private const String STORAGE_FILE_NAME = "antPluginData.txt";
    public String projectPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath)
      ,@"Nashorn.fdp");

    //public String projectPath = "./Nashorn.fdp";


    //public String projectPath = @"F:\codingground\java\Nashorn\Nashorn.fdp";
    //public String antPath = @"F:\ant\apache-ant-1.10.1\bin\ant.bat"; //String.Empty;
    public String antPath = @"F:\ant\apache-ant-1.10.1";
    
    // DisplayName: Additional Arguments
    // Description: More parameters to add to the Ant call (e.g. -inputhandler <classname>)
    public String addArgs;
    private int toggleIndex = 1;
    public ImageList imageList2;

    public String sakuraPath = @"C:\Program Files (x86)\sakura\sakura.exe";


    public List<string> BuildFilesList
    {
      get { return buildFilesList; }
    }


    public AntPanel()
    {
      InitializeComponent();
      InitializeGraphics();
      CreateMenus();

      this.splitContainer1.Panel2Collapsed = true;
      this.splitContainer1.Panel1Collapsed = false;
      this.propertyGrid1.HelpVisible = false;
      this.propertyGrid1.ToolbarVisible = false;

      ReadBuildFiles();
      RefreshData();
    }

    public AntPanel(string path)
    {
      projectPath = path;
      InitializeComponent();
      InitializeGraphics();

      CreateMenus();

      this.splitContainer1.Panel2Collapsed = true;
      this.splitContainer1.Panel1Collapsed = false;
      this.propertyGrid1.HelpVisible = false;
      this.propertyGrid1.ToolbarVisible = false;

      ReadBuildFiles();
      RefreshData();
    }

    private void InitializeGraphics()
    {
      this.imageList2 = new ImageList();
      Bitmap value = ((System.Drawing.Bitmap)(this.imageListStripButton.Image));
      imageList2.Images.AddStrip(value);
      imageList2.TransparentColor = Color.FromArgb(233, 229, 215);

      imageList2.Images.Add(this.サクラエディタToolStripMenuItem.Image);
      imageList2.Images.Add(this.pSPadToolStripMenuItem.Image);
      imageList2.Images.Add(this.scintillaCToolStripMenuItem.Image);//Scintilla.ico
      imageList2.Images.Add(this.azukiEditorZToolStripMenuItem.Image);//AnnCompact.ico
      imageList2.Images.Add(this.richTextBoxToolStripMenuItem.Image);//EmEditor_16x16.png
      imageList2.Images.Add(this.エクスプローラToolStripMenuItem.Image);
      imageList2.Images.Add(this.コマンドプロンプトToolStripMenuItem.Image);

      this.imageList.Tag = "Ant";

      this.tabControl1.ImageList = imageList2;
      this.tabPage1.ImageIndex = 53;  //61;  //Ant
      this.tabPage2.ImageIndex = 61;  //Dir
      this.tabPage3.ImageIndex = 99;  //FTP
      this.tabPage4.ImageIndex = 100; //お気に入り
     }




    private void CreateMenus()
    {
      buildFileMenu = new ContextMenuStrip();
      buildFileMenu.Items.Add("Run default target", runButton.Image, MenuRunClick);
      buildFileMenu.Items.Add("Edit file", null, MenuEditClick);
      buildFileMenu.Items.Add(new ToolStripSeparator());
      buildFileMenu.Items.Add("Remove",removeButton.Image, MenuRemoveClick);

      targetMenu = new ContextMenuStrip();
      targetMenu.Items.Add("Run target", runButton.Image, MenuRunClick);
      targetMenu.Items.Add("Show in Editor", null, MenuEditClick);
    }
    
    private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        AntTreeNode currentNode = treeView.GetNodeAt(e.Location) as AntTreeNode;
        treeView.SelectedNode = currentNode;
        if (currentNode.Parent == null)
          buildFileMenu.Show(treeView, e.Location);
        else
          targetMenu.Show(treeView, e.Location);
      }
    }
    
    private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      RunTarget();
    }
    
    private void MenuRunClick(object sender, EventArgs e)
    {
      RunTarget();
    }
    
    private void MenuEditClick(object sender, EventArgs e)
    {
      AntTreeNode node = treeView.SelectedNode as AntTreeNode;
      //Globals.MainForm.OpenEditableDocument(node.File, false);
      Process.Start(sakuraPath, node.File);
      /*
      //ScintillaControl sci = Globals.SciControl;
      String text = sci.Text;
      Regex regexp = new Regex("<target[^>]+name\\s*=\\s*\"" + node.Target + "\".*>");
      Match match = regexp.Match(text);
      if (match != null)
      {
        sci.GotoPos(match.Index);
        sci.SetSel(match.Index, match.Index + match.Length);
      }
      */
    }
    
    private void MenuRemoveClick(object sender, EventArgs e)
    {
      RemoveBuildFile((treeView.SelectedNode as AntTreeNode).File);
    }
    
    private void addButton_Click(object sender, EventArgs e)
    {
      MessageBox.Show(projectPath);
      /*
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Filter = "BuildFiles (*.xml)|*.XML|" + "All files (*.*)|*.*";
      dialog.Multiselect = true;
      if (!String.IsNullOrEmpty(projectPath))dialog.InitialDirectory = Path.GetDirectoryName(projectPath);

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        AddBuildFiles(dialog.FileNames);
      }
      */
    }

    private void runButton_Click(object sender, EventArgs e)
    {
      RunTarget();
    }
    
    private void RunTarget()
    {
      AntTreeNode node = treeView.SelectedNode as AntTreeNode;
      if (node == null) return;
      RunTarget(node.File, node.Target);
    }
    
    public void RunTarget(String file, String target)
    {
      //OSの情報を取得する
      System.OperatingSystem os = System.Environment.OSVersion;
      //MessageBox.Show(os.ToString());
      if ((os.ToString()).IndexOf("Unix")>=0)
      {
        String command = "gnome-terminal";
        String arguments = "-e \"sh -c \'" + "ant -buildfile " + file + " " + target +"; exec bash\'\""; 
        Process.Start(command, arguments);
        return;
      }
      else
      {
        String command = Environment.SystemDirectory + "\\cmd.exe";

        String arguments = "/k ";

        if (antPath.Length > 0) arguments += Path.Combine(antPath, "bin") + "\\ant";
        else
          arguments += "ant";
        if (!string.IsNullOrEmpty(addArgs)) arguments += " " + addArgs;
        arguments += " -buildfile \"" + file + "\" \"" + target + "\"";
        //TraceManager.Add(command + " " + arguments);
        //Globals.MainForm.CallCommand("RunProcessCaptured", command + ";" + arguments);
        Process.Start(command, arguments);
      }
    }
    
    public void AddBuildFiles(String[] files)
    {
      foreach (String file in files)
      {
        if (!buildFilesList.Contains(file))
          buildFilesList.Add(file);
      }
      SaveBuildFiles();
      RefreshData();
    }
   
    public void RemoveBuildFile(String file)
    {
      if (buildFilesList.Contains(file)) buildFilesList.Remove(file);
      SaveBuildFiles();
      RefreshData();
    }
    
    private void ReadBuildFiles()
    {
      buildFilesList.Clear();
      String folder = GetBuildFilesStorageFolder();
      String fullName = Path.Combine(folder,STORAGE_FILE_NAME);

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
      String fullName = Path.Combine(folder,STORAGE_FILE_NAME);
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
      String projectFolder = Path.GetDirectoryName(projectPath);
      return Path.Combine(projectFolder, "obj");
    }
     
    private void refreshButton_Click(object sender, EventArgs e)
    {
      RefreshData();
    }
    
    public void RefreshData()
    {
      //Boolean projectExists = !(String.IsNullOrEmpty(projectPath));
      Boolean projectExists = (File.Exists(projectPath));
      Enabled = projectExists;
      if (projectExists)
      {
        FillTree();
      }
      else
      {
        treeView.Nodes.Clear();
        treeView.Nodes.Add(new TreeNode("No project opened"));
      }
    }
    
    private void FillTree()
    {
      treeView.BeginUpdate();
      treeView.Nodes.Clear();
      // bug fix 2018-03-16
      // タブコントロールに組み込むと最初に加えたノードが表示されなくなる
      // 間に合せのパッチ
      TreeNode dummy = new TreeNode("dummy");
      treeView.Nodes.Add(dummy);
      foreach (String file in BuildFilesList)
      {
        //MessageBox.Show(file);
        if (File.Exists(file))
        {
          //MessageBox.Show(file,"exists");
          if (GetBuildFileNode(file) != null) treeView.Nodes.Add(GetBuildFileNode(file));
        }
      }
      treeView.EndUpdate();
    }

    private TreeNode GetBuildFileNode(string file)
    {
      try
      {
        XmlDocument xml = new XmlDocument();
        xml.Load(file);

        XmlAttribute defTargetAttr = xml.DocumentElement.Attributes["default"];
        String defaultTarget = (defTargetAttr != null) ? defTargetAttr.InnerText : "";

        XmlAttribute nameAttr = xml.DocumentElement.Attributes["name"];
        String projectName = (nameAttr != null) ? nameAttr.InnerText : file;

        XmlAttribute descrAttr = xml.DocumentElement.Attributes["description"];
        String description = (descrAttr != null) ? descrAttr.InnerText : "";

        if (projectName.Length == 0)
          projectName = file;

        AntTreeNode rootNode = new AntTreeNode(projectName, ICON_FILE);
        rootNode.File = file;
        rootNode.Target = defaultTarget;
        rootNode.ToolTipText = description;

        XmlNodeList nodes = xml.DocumentElement.ChildNodes;
        int nodeCount = nodes.Count;
        for (int i = 0; i < nodeCount; i++)
        {
          XmlNode child = nodes[i];
          if (child.Name == "target")
          {
            // skip private targets
            XmlAttribute targetNameAttr = child.Attributes["name"];
            if (targetNameAttr != null)
            {
              String targetName = targetNameAttr.InnerText;
              if (!String.IsNullOrEmpty(targetName) && (targetName[0] == '-'))
              {
                continue;
              }
            }
            AntTreeNode targetNode = GetBuildTargetNode(child, defaultTarget);
            targetNode.File = file;
            rootNode.Nodes.Add(targetNode);
          }
        }
        //rootNode.Expand();
        return rootNode;
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
      }
      return null;
    }
    
    private AntTreeNode GetBuildTargetNode(XmlNode node, string defaultTarget)
    {
      XmlAttribute nameAttr = node.Attributes["name"];
      String targetName = (nameAttr != null) ? nameAttr.InnerText : "";

      XmlAttribute descrAttr = node.Attributes["description"];
      String description = (descrAttr != null) ? descrAttr.InnerText : "";

      AntTreeNode targetNode;
      if (targetName == defaultTarget)
      {
        targetNode = new AntTreeNode(targetName, ICON_PUBLIC_TARGET);
        targetNode.NodeFont = new Font(
            treeView.Font.Name,
            treeView.Font.Size,
            FontStyle.Bold);
      }
      else if (description.Length > 0)
      {
        targetNode = new AntTreeNode(targetName, ICON_PUBLIC_TARGET);
      }
      else
      {
        targetNode = new AntTreeNode(targetName, ICON_INTERNAL_TARGET);
      }

      targetNode.Target = targetName;
      targetNode.ToolTipText = description;
      return targetNode;
    }

    private void toolStripButton8_Click(object sender, EventArgs e)
    {
      int num = this.toggleIndex % 3;

      if (this.treeView.SelectedNode != null)
      {

        /*

        this.propertyGrid1.SelectedObject = null;
        if (this.treeView.SelectedNode.Tag.GetType().Name == "NodeInfo")
        {
          //NodeInfo selectedObject = SelectedNodeInfo();// new NodeInfo();
          //this.propertyGrid1.SelectedObject = selectedObject;
        }
        //else if (this.treeView.SelectedNode.Tag is TaskInfo)
        //{
          //this.propertyGrid1.SelectedObject = this.treeView.SelectedNode.Tag as TaskInfo;
        //}
        else if (this.treeView.SelectedNode.Tag is XmlElement)
        {
          try { this.propertyGrid1.SelectedObject = (XmlElement)this.treeView.SelectedNode.Tag; }
          catch { this.propertyGrid1.SelectedObject = null; }
        }
        else if (this.treeView.SelectedNode.Tag is String)
        {
          String path = this.treeView.SelectedNode.Tag as String;
          if (File.Exists(path))
          {
            this.propertyGrid1.SelectedObject = new FileInfo(path);
          }
          else if (Directory.Exists(path))
          {
            this.propertyGrid1.SelectedObject = new DirectoryInfo(path);
          }
          //else if (Lib.IsWebSite(path))
          //{
            //NodeInfo ni = new NodeInfo();
            //ni.Path = path;
            //this.propertyGrid1.SelectedObject = ni;
          //}
          else
          {
            //NodeInfo ni2 = new NodeInfo();
            //ni2.Command = path;
            //this.propertyGrid1.SelectedObject = ni2;
          }
        }
        else
        {
          this.propertyGrid1.SelectedObject = null;
        }
*/
      }

      if (num == 0)
      {
        this.splitContainer1.Panel2Collapsed = true;
        this.splitContainer1.Panel1Collapsed = false;
        this.propertyGrid1.HelpVisible = false;
        this.propertyGrid1.ToolbarVisible = false;
      }
      else if (num == 1)
      {
        this.splitContainer1.Panel2Collapsed = false;
        this.splitContainer1.Panel1Collapsed = true;
        this.propertyGrid1.HelpVisible = true;
        this.propertyGrid1.ToolbarVisible = true;
      }
      else if (num == 2)
      {
        this.splitContainer1.Panel2Collapsed = false;
        this.splitContainer1.Panel1Collapsed = false;
        this.propertyGrid1.HelpVisible = false;
        this.propertyGrid1.ToolbarVisible = false;
      }
      this.toggleIndex++;

    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Filter = "BuildFiles (*.xml)|*.XML|" + "WshFiles (*.wsf)|*.wsf|" + "All files (*.*)|*.*";
      dialog.Multiselect = true;
      if (!String.IsNullOrEmpty(projectPath))
      {
        dialog.InitialDirectory = Path.GetDirectoryName(projectPath);
      }
      if (dialog.ShowDialog() == DialogResult.OK)
      {
        AddBuildFiles(dialog.FileNames);
      }
    }

    private void runButton_Click_1(object sender, EventArgs e)
    {
      RunTarget();
    }

    private void refreshButton_Click_1(object sender, EventArgs e)
    {
      RefreshData();
    }
  }

  internal class AntTreeNode : TreeNode
  {
    public String File;
    public String Target;

    public AntTreeNode(string text, int imageIndex)
        : base(text, imageIndex, imageIndex)
    {
    }
  }







}
