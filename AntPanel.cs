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
using AntPanelApplication.Controls;
using AntPanelApplication.Controls.SimplePanel;
using AntPanelApplication.Controls.PicturePanel;
using AntPanelApplication.Managers;
using AntPanelApplication.CommonLibrary;

namespace AntPanelApplication
{
  public partial class AntPanel : UserControl
  {
    #region AntPanel Valiables
    public const int ICON_FILE = 0;
    public const int ICON_DEFAULT_TARGET = 1;
    public const int ICON_INTERNAL_TARGET = 2;
    public const int ICON_PUBLIC_TARGET = 3;
    private const String STORAGE_FILE_NAME = "antPluginData.txt";

    private ContextMenuStrip buildFileMenu;
    private ContextMenuStrip targetMenu;
    private List<String> buildFilesList = new List<string>();
    static TabPageManager tabPageManager = null;

    public static String projectPath = Application.ExecutablePath;
    public static String projectDir = Path.GetDirectoryName(Application.ExecutablePath);
    public static String projectName = "AntPanel";
    public static RichTextEditor editor = null;
    public static PicturePanel picturePanel = null;
    public static SimplePanel panel = null;
    public static TreeNode currentNode = null;
    public static Image fdImage;
    public static Image fdImage32;
    public static AntPanel instance;
    public static MainForm parent;

    public String antPath = @"F:\ant\apache-ant-1.10.1";
    global::AntPanelApplication.Properties.Settings settings
      = new global::AntPanelApplication.Properties.Settings();

    // DisplayName: Additional Arguments
    // Description: More parameters to add to the Ant call (e.g. -inputhandler <classname>)
    public String addArgs;
    private int toggleIndex = 1;
    public static ImageList imageList2;

    public XmlMenuTree menuTree = null;

    public DirTreePanel dirTreePanel;
    public FTPClientPanel ftpClientPanel;
    public XmlTreePanel xmlTreePanel;

    public String sakuraPath = @"C:\Program Files (x86)\sakura\sakura.exe";
    public List<string> BuildFilesList
    {
      get { return buildFilesList; }
    }

    //OSの情報を取得する
    public static OperatingSystem os = Environment.OSVersion;
    public static bool IsRunningOnMono = (Type.GetType("Mono.Runtime") != null);
    public static bool IsRunningUnix = ((Environment.OSVersion.ToString()).IndexOf("Unix") >= 0) ? true : false;
    public static bool IsRunningWindows = ((Environment.OSVersion.ToString()).IndexOf("Windows") >= 0) ? true : false;
    #endregion

    #region Properties
    public MenuStrip MenuBar
    {
      get
      {
        return this.menuStrip1;
      }
      set
      {
        this.menuStrip1 = value;
      }
    }
    public ToolStrip ToolBar
    {
      get
      {
        return this.toolStrip1;
      }
      set
      {
        this.toolStrip1 = value;
      }
    }
    public StatusStrip StatusBar
    {
      get
      {
        return this.statusStrip1;
      }
      set
      {
        this.statusStrip1 = value;
      }
    }
    public TabPageManager TabPageManager
    {
      get
      {
        return AntPanel.tabPageManager;
      }
      set
      {
        AntPanel.tabPageManager = value;
      }
    }
    #endregion

    public AntPanel()
    {
      AntPanel.instance = this;
      Globals.AntPanel = this;

      InitializeComponent();
      InitializeGraphics();
      InitializeControls();


      RefreshData();

      // TODO: 実装
      //this.previousDocuments = this.settings.PreviousCustomDocuments;
      //this.PopulatePreviousDocumentsMenu();
      //this.AddPreviousDocuments(path);

      //this.PopulateFileStateMenu();
      //this.PopulatePreviousCustomDocumentsMenu();

      CreateMenus();

      this.splitContainer1.Panel2Collapsed = true;
      this.splitContainer1.Panel1Collapsed = false;
      this.propertyGrid1.HelpVisible = false;
      this.propertyGrid1.ToolbarVisible = false;
      ReadBuildFiles();
      RefreshData();
    }

    private void InitializeControls()
    {
      //TabPageManagerオブジェクトの作成
      tabPageManager = new TabPageManager(this.tabControl1);
      this.tabControl1.MouseClick += new MouseEventHandler(AntPanel.tabPageManager.tabControl_MouseClick);

      this.tabPage1.Name = this.tabPage1.AccessibleDescription = "AntPanel;Ant";
      this.tabPage2.Name = this.tabPage2.AccessibleDescription = "AntPanel;Dir";
      this.tabPage3.Name = this.tabPage3.AccessibleDescription = "AntPanel;FTP";
      this.tabPage4.Name = this.tabPage4.AccessibleDescription = "AntPanel;Link";
      this.tabPage5.Name = this.tabPage5.AccessibleDescription = "AntPanel;Settings";

      //InitializeInterface();
      IntializeXmlMenuTree();
      //InitializeGradleTree();
      //IntializeDirTreePanel(); //AntPanel_Loadに移動
      IntializeFTPClientPanel();
      InitializeXmlTreePanel();
      try
      {
        //this.tabControl1.MouseClick += new MouseEventHandler(AntPanel._tabPageManager.tabControl_MouseClick);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
      }
      /*
      //MessageBox.Show(os.ToString());
      // MIcrosoft Windows NT 6.2.9200.0
      // Unix 4.15.0.45
      AntPanel.editor = new RichTextEditor();
      RichTextEditor.antPanel = this;
      editor.Dock = DockStyle.Fill;
      this.tabPage5.Tag = editor;
      this.tabPage5.Controls.Add(editor);
      ((RichTextBox)editor.Tag).Text= "こんにちは！\nきょうはよいお天気です";
      // 
      // pictureBox1
      // 
      AntPanel.picturePanel = new PicturePanel();
      picturePanel.Dock = System.Windows.Forms.DockStyle.Fill;
      AntPanel.pictureBox1 = new System.Windows.Forms.PictureBox();
      AntPanel.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      AntPanel.pictureBox1.Location = new System.Drawing.Point(3, 3);
      AntPanel.pictureBox1.Name = "pictureBox1";
      AntPanel.pictureBox1.Size = new System.Drawing.Size(1179, 485);
      AntPanel.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      AntPanel.pictureBox1.TabIndex = 0;
      AntPanel.pictureBox1.TabStop = false;
      
      
      
      
      if (AntPanel.IsRunningUnix)
      {
        AntPanel.pictureBox1.Tag = "/media/sf_ShareFolder/Picture/sea020.jpg";
        AntPanel.pictureBox1.Image = System.Drawing.Image.FromFile("/media/sf_ShareFolder/Picture/sea020.jpg");
        ((PictureBox)(picturePanel.Tag)).Tag = "/media/sf_ShareFolder/Picture/sea020.jpg";
      }
      else if(AntPanel.IsRunningWindows)
      {
        AntPanel.pictureBox1.Tag = @"F:\VirtualBox\ShareFolder\Picture/sea020.jpg";
        AntPanel.pictureBox1.Image = System.Drawing.Image.FromFile(@"F:\VirtualBox\ShareFolder\Picture/sea020.jpg");

        ((PictureBox)(picturePanel.Tag)).Tag = @"F:\VirtualBox\ShareFolder\Picture/sea020.jpg";
      }
      //this.tabPage7.Tag = AntPanel.pictureBox1;
      //this.tabPage7.Controls.Add(AntPanel.pictureBox1);
      this.tabPage7.Tag = picturePanel;
      this.tabPage7.Controls.Add(picturePanel);
      // 
      // webBrowser1
      // 
      AntPanel.webBrowser1 = new System.Windows.Forms.WebBrowser();
      AntPanel.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
      AntPanel.webBrowser1.Location = new System.Drawing.Point(0, 0);
      AntPanel.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      AntPanel.webBrowser1.Name = "webBrowser1";
      AntPanel.webBrowser1.Size = new System.Drawing.Size(1185, 491);
      AntPanel.webBrowser1.TabIndex = 0;
      this.tabPage6.Tag = AntPanel.webBrowser1;
      this.tabPage6.Controls.Add(AntPanel.webBrowser1);
      if (AntPanel.IsRunningWindows)
      {
        AntPanel.webBrowser1.Tag = "http://www.google.co.jp";
        AntPanel.webBrowser1.Navigate("http://www.google.co.jp");

      }
      */
      /*
      if (IsRunningWindows)
      {
        //
        // axWindowsMediaPlayer1
        // 
        AntPanel.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
        AntPanel.axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
        AntPanel.axWindowsMediaPlayer1.Enabled = true;
        AntPanel.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
        AntPanel.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
        //AntPanel.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
        AntPanel.axWindowsMediaPlayer1.Size = new System.Drawing.Size(1185, 491);
        AntPanel.axWindowsMediaPlayer1.TabIndex = 0;
        this.tabPage8.Tag = AntPanel.axWindowsMediaPlayer1;
        this.tabPage8.Controls.Add(AntPanel.axWindowsMediaPlayer1);
        //AntPanel.axWindowsMediaPlayer1.URL = @"F:\VirtualBox\ShareFolder\Music\03-Monteverdi.mp3";
      }
      */
      //
      // SimplePanel
      //
      /*
      AntPanel.panel = new SimplePanel(this);
      panel.Dock = DockStyle.Fill;
      this.tabPage9.Tag = panel;
      this.tabPage9.Controls.Add(panel);
      //((RichTextBox)editor.Tag).Text = "こんにちは！\nきょうはよいお天気です";
      */
    }

    private void InitializeGraphics()
    {
      AntPanel.imageList2 = new ImageList();
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

      this.imageList.Images.Add(imageList2.Images[98]);  // 39 folder
      this.imageList.Images.Add(imageList2.Images[56]);   // 40 blank file
      this.imageList.Images.Add(imageList2.Images[77]);  // 41 website
      this.imageList.Images.Add(imageList2.Images[146]); // 42 command

      this.imageList.Tag = "Ant";

      this.tabControl1.ImageList = AntPanel.imageList2;
      this.tabPage1.ImageIndex = 53;  //61;  //Ant
      this.tabPage2.ImageIndex = 61;  //Dir
      this.tabPage3.ImageIndex = 99;  //FTP
      this.tabPage4.ImageIndex = 100; //お気に入り
      this.tabPage5.ImageIndex = 101; //Settings
      AntPanel.fdImage = this.fdImageButton.Image;
      AntPanel.fdImage32 = this.fdImage32Button.Image;
    }

    private void InitializeInterface()
    {
      /*
      this.mainForm = new MDIForm.ParentFormClass();
      this.mainForm.imageList1 = this.imageList;
      this.mainForm.imageList2 = imageList2;
      this.mainForm.propertyGrid1 = this.propertyGrid1;
      this.mainForm.contextMenuStrip1 = this.targetMenu;
      this.treeView.Tag = mainForm;
      this.treeView.AccessibleName = "AntPlugin.PluginUI.treeView";
      */
    }
   
    private void InitializeGradleTree()
    {
      //this.gradleTree = new GradleTree(this);
    }

    private void IntializeXmlMenuTree()
    {
      this.menuTree = new XmlMenuTree(this);
    }
    
    private void IntializeDirTreePanel()
    {
      this.dirTreePanel = new DirTreePanel(this);
      dirTreePanel.Dock = DockStyle.Fill;
      this.dirTreePanel.currentRootDir = AntPanel.projectDir;
      this.tabPage2.Controls.Add(dirTreePanel);
    }

    private void IntializeFTPClientPanel()
    {
      try
      {
        //this.ftpClientPanel = new FTPClientPanel(this.pluginMain);
        this.ftpClientPanel = new FTPClientPanel(this);
        this.ftpClientPanel.Dock = DockStyle.Fill;
        this.tabPage3.Controls.Add(this.ftpClientPanel);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString(), "IntializeFTPClientPanel");
      }
    }

    public void InitializeXmlTreePanel()
    {
      
      try
      {
        this.xmlTreePanel = new XmlTreePanel(this);
        this.xmlTreePanel.Dock = DockStyle.Fill;
        this.tabPage4.Controls.Add(this.xmlTreePanel);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString(), "IntializeFTPClientPanel");
      }
      
    }

    private void CreateMenus()
    {
      buildFileMenu = new ContextMenuStrip();
      buildFileMenu.Items.Add("Run default target", runButton.Image, MenuRunClick);
      buildFileMenu.Items.Add("Edit file", null, MenuEditClick);
      buildFileMenu.Items.Add(new ToolStripSeparator());
      buildFileMenu.Items.Add("Remove", removeButton.Image, MenuRemoveClick);

      targetMenu = new ContextMenuStrip();
      targetMenu.Items.Add("Run target", runButton.Image, MenuRunClick);
      targetMenu.Items.Add("Show in Editor", null, MenuEditClick);
    }

    private void AntPanel_Load(object sender, EventArgs e)
    {
      String path = String.Empty;
      if (MainForm.Arguments.Length > 0)
      {
        path = MainForm.Arguments[0];
        //MessageBox.Show(path);
      }
      if (File.Exists(path))
      {
        AntPanel.projectPath = path;
        AntPanel.projectDir = Path.GetDirectoryName(AntPanel.projectPath);
        AntPanel.projectName = Path.GetFileNameWithoutExtension(AntPanel.projectPath);
        if (Lib.IsTextFile(path)) Globals.MainForm.OpenCustomDocument(this.settings.DefaultEditor, path);
        else if (Lib.IsImageFile(path)) Globals.MainForm.OpenCustomDocument("PicturePanel", path);
        else if (Lib.IsSoundFile(path) || Lib.IsVideoFile(path)) Globals.MainForm.OpenCustomDocument("PlayerPanel", path);
      }
      else if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(),path)))
      {
        AntPanel.projectPath = Path.Combine(Directory.GetCurrentDirectory(), path);
        AntPanel.projectDir = Path.GetDirectoryName(AntPanel.projectPath);
        AntPanel.projectName = Path.GetFileNameWithoutExtension(AntPanel.projectPath);
      }
      else if (Directory.Exists(path))
      {
        AntPanel.projectPath = path;
        AntPanel.projectDir = AntPanel.projectPath;
        AntPanel.projectName = Path.GetFileNameWithoutExtension(AntPanel.projectPath);
      }

      IntializeDirTreePanel();

      // https://dobon.net/vb/dotnet/control/tabpagehide.html
      this.propertyGrid3.SelectedObject = this.settings;
    }

    private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TreeView tree = sender as TreeView;
        AntPanel.currentNode = tree.GetNodeAt(e.Location) as TreeNode;
        tree.SelectedNode = AntPanel.currentNode;

        if (AntPanel.currentNode.Tag.GetType().Name != "NodeInfo")
        {
          AntTreeNode antNode = treeView.GetNodeAt(e.Location) as AntTreeNode;
          treeView.SelectedNode = antNode;
          if (antNode.Parent == null)
            buildFileMenu.Show(treeView, e.Location);
          else
            targetMenu.Show(treeView, e.Location);
        }
      }
    }

    private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      RunTarget();
    }

    public void MenuRunClick(object sender, EventArgs e)
    {
      RunTarget();
    }

    public void MenuEditClick(object sender, EventArgs e)
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

    public void MenuRemoveClick(object sender, EventArgs e)
    {
      RemoveBuildFile((treeView.SelectedNode as AntTreeNode).File);
    }

    public void addButton_Click(object sender, EventArgs e)
    {
      //MessageBox.Show(projectPath);
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Filter = "BuildFiles (*.xml)|*.XML|" + "All files (*.*)|*.*";
      dialog.Multiselect = true;
      if (!String.IsNullOrEmpty(projectPath)) dialog.InitialDirectory = Path.GetDirectoryName(projectPath);

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        AddBuildFiles(dialog.FileNames);
      }
    }

    /// <summary>
    /// TODO: 外部TreeView使用の場合
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ShowOuterXmlClick(object sender, EventArgs e)
    {
      /*
      if (((TreeView)this.targetMenu.Tag) == outlineTreeView)
      {
        TreeView tree = outlineTreeView;
        if (tree.SelectedNode.Tag is NodeInfo)
        {
          NodeInfo ni = tree.SelectedNode.Tag as NodeInfo;
          MessageBox.Show(ni.XmlNode.OuterXml.Replace("\t", "  ").Replace("	", "  "), "OuterXML : " + ni.Type);
        }
        return;
      }
      AntTreeNode node = this.currentNode as AntTreeNode;

      if (node.Tag is TaskInfo)
      {
        TaskInfo ti = node.Tag as TaskInfo;
        MessageBox.Show(ti.OuterCode, ti.Name);
      }
      else if (node.Tag is XmlNode)
      {
        XmlNode tag = node.Tag as XmlNode;
        MessageBox.Show(tag.OuterXml.Replace("\t", "  ").Replace("	", "  "), "OuterXML : " + node.Target);
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
      if ((os.ToString()).IndexOf("Unix") >= 0)
      {
        String command = "gnome-terminal";
        String arguments = "-e \"sh -c \'" + "ant -buildfile " + file + " " + target + "; exec bash\'\"";
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
      String fullName = Path.Combine(folder, STORAGE_FILE_NAME);

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
      String fullName = Path.Combine(folder, STORAGE_FILE_NAME);
      if (!Directory.Exists(folder))
        Directory.CreateDirectory(folder);
      StreamWriter file = new StreamWriter(fullName);
      foreach (String line in buildFilesList)
      {
        file.WriteLine(line);
      }
      file.Close();
    }
    //kokokoko
    private String GetBuildFilesStorageFolder()
    {
      String projectFolder = Path.GetDirectoryName(projectPath);
      if (File.Exists(projectPath)) projectFolder = Path.GetDirectoryName(projectPath);
      else if (Directory.Exists(projectPath)) projectFolder = projectPath;




      return Path.Combine(projectFolder, "obj");
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
      RefreshData();
    }

    public void RefreshData()
    {
      //Boolean projectExists = !(String.IsNullOrEmpty(projectPath));

      // bug-fix for ubuntu Time-stamp: <2019-02-23 11:15:09 kahata>
      Boolean projectExists = false;
      if (File.Exists(projectPath)) projectExists = (File.Exists(projectPath));
      else if (Directory.Exists(projectPath)) projectExists = (Directory.Exists(projectPath));

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
        if (File.Exists(file))
        {
          //Fix 2017-01-09
          if (Path.GetExtension(file) == ".cs" || Path.GetExtension(file) == ".java")
          {
            // 追加要  //koko
            //this.LoadIn(file);
          }
          else if (Path.GetExtension(file) == ".xml")
          {
            bool isMenu = (Path.GetFileNameWithoutExtension(file).ToLower() == "fdtreemenu"
              || Path.GetFileNameWithoutExtension(file).ToLower() == "xmltreemenu") ? true : false;


            // kokokoko
            treeView.Nodes.Add(this.menuTree.getXmlTreeNode(file, isMenu));

            //if (GetBuildFileNode(file) != null) treeView.Nodes.Add(GetBuildFileNode(file));




          }
          else if (Path.GetExtension(file) == ".fdp" || Path.GetExtension(file) == ".wsf")
          {
            treeView.Nodes.Add(GetBuildFileNode(file));
            // 追加 2017-01-12
            XmlDocument doc = new XmlDocument();
            //doc.LoadXml("<xml a=\"b\"><c>d<e f=\"g\">h</e>i</c>j</xml>");
            doc.Load(file);

            //kokokoko
            //this.propertyGrid1.SelectedObject = new XmlNodeWrapper(doc.DocumentElement);
          }
          else if (Path.GetExtension(file) == ".gradle")
          {
            // kokokoko
            //AntTreeNode gradleNode = this.gradleTree.GetGradleOutlineTreeNode(file);
            //treeView.Nodes.Add(gradleNode);
          }
        }
      }
      treeView.EndUpdate();
    }

    public TreeNode GetBuildFileNode(string file)
    {
      //MessageBox.Show(file);
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
      catch (Exception ex)
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
        this.propertyGrid1.SelectedObject = null;
        if (this.treeView.SelectedNode.Tag.GetType().Name == "NodeInfo")
        {
          NodeInfo selectedObject = SelectedNodeInfo();// new NodeInfo();
          this.propertyGrid1.SelectedObject = selectedObject;
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

    public NodeInfo SelectedNodeInfo()
    {
      return (NodeInfo)this.treeView.SelectedNode.Tag;
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

    private void 試験ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //OSの情報を取得する
      //System.OperatingSystem os = System.Environment.OSVersion;
      if ((os.ToString()).IndexOf("Unix") < 0)
      {
        AntPanel.axWindowsMediaPlayer1.URL = @"F:\VirtualBox\ShareFolder\Music\03-Monteverdi.mp3";
        this.tabControl1.SelectedIndex = 7;
      }

      else
      {
        Process.Start("http://www.google.com");
      }
    }


    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch(this.tabControl1.SelectedIndex)
      {
        case 4:
        case 5:
          this.MenuBar.Visible = false;
          this.ToolBar.Visible = false;
          this.StatusBar.Visible = false;
          break;
        default:
          this.MenuBar.Visible = true;
          this.ToolBar.Visible = true;
          this.StatusBar.Visible = true;
          break;
      }
    }

    private void propertyGrid3_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
    //MessageBox.Show("propertyGrid1_PropertyValueChanged");
      this.settings.Save();
      this.ApplySettings();
    }

    private void ApplySettings()
    {
      /*
			try
			{
				this.toolStrip1.Visible = this.settings.ToolBarVisible;
				this.ツールバーTToolStripMenuItem.Checked = this.toolStrip1.Visible;
				this.statusStrip1.Visible = this.settings.StatusBarVisible;
				this.ステータスバーSToolStripMenuItem.Checked = this.statusStrip1.Visible;
				// 最近開いたファイル
				StringCollection strs = this.settings.PreviousDocuments;
				this.previousDocuments = strs.Cast<string>().ToList<string>();
				this.PopulatePreviousDocumentsMenu();
				// お気に入り
				StringCollection bookmarks = this.settings.BookMarks;
				this.favorateDocuments = bookmarks.Cast<string>().ToList<string>();
				this.PopulateFavorateDocumentsMenu();
				this.propertyGrid1.SelectedObject = this.settings;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(), "ApplySetting:error");
			}
			*/
    }


    private void SaveSettings()
    {
      //this.settings.PreviousDocuments.Clear();
      //this.settings.PreviousDocuments.AddRange(this.previousDocuments.ToArray());
      //this.settings.MenuBarVisible = this.menuStrip1.Visible;
      //this.settings.ToolBarVisible = this.toolStrip1.Visible;
      //this.settings.StatusBarVisible = this.statusStrip1.Visible;
      //this.settings.BookMarks.Clear();
      //this.settings.BookMarks.AddRange(this.favorateDocuments.ToArray());

      // F:\VCSharp\Flashdevelop5.1.1-LL\External\CustomControl\AntPanelApplication\bin\Debug\AntPanelApplication.exe.config
      this.settings.Save();
    }



    private void メニューバーMToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.menuStrip1.Visible = !this.menuStrip1.Visible;
    }

    private void ステータスバーSToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.statusStrip1.Visible = !this.statusStrip1.Visible;
      this.ステータスバーSToolStripMenuItem.Checked = this.ステータスバーSToolStripMenuItem1.Checked;
    }

    private void ツールバーTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.toolStrip1.Visible = !this.toolStrip1.Visible;
    }

    private void ステータスバーSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.statusStrip1.Visible = !this.statusStrip1.Visible;
      ステータスバーSToolStripMenuItem1.Checked = ステータスバーSToolStripMenuItem.Checked;
    }

    Int32 mainForm_toggleIndex = 0;
    private void mainForm_toggle_Click(object sender, EventArgs e)
    {
      if(this.mainForm_toggleButton.Checked == true)
      {
        Globals.MainForm.splitContainer1.Panel2Collapsed = false;
        Globals.MainForm.splitContainer1.Panel1Collapsed = false;
      }
      else
      {
        Globals.MainForm.splitContainer1.Panel2Collapsed = true;
        Globals.MainForm.splitContainer1.Panel1Collapsed = false;
      }
      /*
      int num = this.mainForm_toggleIndex % 3;
      if (num == 0)
      {
        Globals.MainForm.splitContainer1.Panel2Collapsed = true;
        Globals.MainForm.splitContainer1.Panel1Collapsed = false;
      }
      else if (num == 1)
      {
        Globals.MainForm.splitContainer1.Panel2Collapsed = false;
        Globals.MainForm.splitContainer1.Panel1Collapsed = true;
      }
      else if (num == 2)
      {
        //Globals.MainForm.splitContainer1.Panel2Collapsed = false;
        //Globals.MainForm.splitContainer1.Panel1Collapsed = false;
        //this.propertyGrid1.HelpVisible = false;
        //this.propertyGrid1.ToolbarVisible = false;
      }
      this.mainForm_toggleIndex++;
      */
    }

    public void Test1(object sender, EventArgs e)
    {
      if (sender != null)
      {
        ToolStripItem button = (ToolStripItem)sender;
        String msg = Globals.AntPanel.menuTree.ProcessVariable(((ItemData)button.Tag).Tag);
        MessageBox.Show(msg, "AntPanel Testからの送信です");
      }
    }
    private void Test2(object sender, EventArgs e)
    {
      if (sender != null)
      {
        ToolStripItem button = (ToolStripItem)sender;
        String msg = Globals.AntPanel.menuTree.ProcessVariable(((ItemData)button.Tag).Tag);
        MessageBox.Show(msg, "AntPanel Testからの送信です");
      }
    }
    public static void Test3(object sender, EventArgs e)
    {
      if (sender != null)
      {
        ToolStripItem button = (ToolStripItem)sender;
        String msg = Globals.AntPanel.menuTree.ProcessVariable(((ItemData)button.Tag).Tag);
        MessageBox.Show(msg, "AntPanel Testからの送信です");
      }
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
