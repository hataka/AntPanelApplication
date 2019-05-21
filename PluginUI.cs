using AntPlugin.CommonLibrary;
using AntPlugin.Controls;
using AntPlugin.XmlTreeMenu;
using AntPlugin.XmlTreeMenu.Managers;
using CSParser.BuildTree;
using PluginCore;
using PluginCore.Helpers;
using ScintillaNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace AntPlugin
{
  public partial class PluginUI : UserControl
	{
    #region Variables
    public const int ICON_FILE = 0;
		public const int ICON_DEFAULT_TARGET = 1;
		public const int ICON_INTERNAL_TARGET = 2;
		public const int ICON_PUBLIC_TARGET = 3;
		public const int ICON_WSF_FILE = 36;
		public const int ICON_FDP_FILE = 37;
		public const int ICON_MENU_ROOT = 38;

		public PluginMain pluginMain;
		public ContextMenuStrip buildFileMenu;
		public ContextMenuStrip targetMenu;

		//kahata: 追加
		public Settings settings;
		public static ImageList imageList2;

		public List<string> previousDocuments = new List<string>();

		private String defaultTarget;
		private ContextMenuStrip csOutlineMenu=null;
		public static List<string> csOutlineTreePath = new List<string>();
		public List<string> MemberId = new List<string>();

		public static List<string> csOutlinePanelTreePath = new List<string>();
		public List<string> OutLinePanelMemberId = new List<string>();

		public List<string> PreviousDocuments
		{
			get
			{
				return this.previousDocuments;
			}
			set
			{
				this.previousDocuments = value;
			}
		}

		public ASCompletion.PluginMain aSpluginMain;
		public ASCompletion.PluginUI aSpluginUI;

		public FixedTreeView outlineTree;
		private int toggleIndex=1;

		public BuildTree buildTree = new BuildTree();
    public GradleTree gradleTree = new GradleTree();
    public XmlMenuTree menuTree = null;

    public DirTreePanel dirTreePanel;
    public FTPClientPanel ftpClientPanel;
    public XmlTreePanel xmlTreePanel;

    public MDIForm.ParentFormClass mainForm;
    public MDIForm.ChildFormControlClass instance;
    #endregion

    public PluginUI(PluginMain pluginMain)
		{
			this.pluginMain = pluginMain;
			this.settings = pluginMain.Settings as Settings;
			StripBarManager.pluginUI = this;
      TreeViewManager.pluginUI = this;
      InitializeComponent();

      InitializeGraphics();
      InitializeInterface();

      CreateMenus();

      InitializeGradleTree();
      IntializeXmlMenuTree();
      IntializeDirTreePanel();
			IntializeFTPClientPanel();
      InitializeXmlTreePanel();

      RefreshData();

      this.propertyGrid2.SelectedObject = this.settings;
      // TODO: 実装
      //this.previousDocuments = this.settings.PreviousCustomDocuments;
			//this.PopulatePreviousDocumentsMenu();
			//this.AddPreviousDocuments(path);

			this.PopulateFileStateMenu();
			this.PopulatePreviousCustomDocumentsMenu();
		}

    #region Initialization
    private void InitializeGraphics()
		{
      // 概要:
      //     System.Windows.Forms.ToolStrip の外観をカスタマイズするために使用される 
      //     System.Windows.Forms.ToolStripRenderer
      //     を取得または設定します。
      //
      // 戻り値:
      //     System.Windows.Forms.ToolStrip の外観をカスタマイズするために使用される
      //     System.Windows.Forms.ToolStripRenderer。

      this.toolStrip.Renderer = new DockPanelStripRenderer();

      addButton.Image = PluginBase.MainForm.FindImage("33");
			runButton.Image = PluginBase.MainForm.FindImage("487");
			refreshButton.Image = PluginBase.MainForm.FindImage("66");
			// Fontが変になる外す
			//this.toolStrip.Font = this.settings.AntPanelDefaultFont;
			//this.treeView.Font = this.settings.AntPanelDefaultFont;
			AntPlugin.PluginUI.imageList2 = new ImageList();
			Bitmap value = ((System.Drawing.Bitmap)(this.imageListStripButton.Image));
			AntPlugin.PluginUI.imageList2.Images.AddStrip(value);
			AntPlugin.PluginUI.imageList2.TransparentColor = Color.FromArgb(233, 229, 215);

			imageList2.Images.Add(this.サクラエディタToolStripMenuItem.Image);
			imageList2.Images.Add(this.pSPadToolStripMenuItem.Image);
			imageList2.Images.Add(this.scintillaCToolStripMenuItem.Image);//Scintilla.ico
			imageList2.Images.Add(this.azukiEditorZToolStripMenuItem.Image);//AnnCompact.ico
			imageList2.Images.Add(this.richTextBoxToolStripMenuItem.Image);//EmEditor_16x16.png
			imageList2.Images.Add(this.エクスプローラToolStripMenuItem.Image);
			imageList2.Images.Add(this.コマンドプロンプトToolStripMenuItem.Image);

			this.syncronizeButton.Image = PluginBase.MainForm.FindImage("203|9|-3|-3");
			this.syncronizeDodument.Image = PluginBase.MainForm.FindImage("203|1|-3|-3");
			this.csOutlineButton1.Image = PluginBase.MainForm.FindImage("99");
			this.toolStripDropDownButton1.Image = PluginBase.MainForm.FindImage("263");
			this.homeStripButton.Image = PluginBase.MainForm.FindImage("224"); ;

			this.imageList.Tag = "Ant";
      //this.removeToolStripMenuItem.Image = PluginBase.MainForm.FindImage("153");

      this.tabControl1.ImageList = imageList2;
      this.tabPage1.ImageIndex = 53;  //61;  //Ant
      this.tabPage2.ImageIndex = 61;  //Dir
      this.tabPage3.ImageIndex = 99;  //FTP
      this.tabPage4.ImageIndex = 100; //お気に入り

      /// .NET versuinの場合分け
      /// if( clrVersionRuntime.Contains("v4"){ ,NET 4の処理}else {}
      /// http://tennoji.seesaa.net/article/196084262.html
      /// string clrVersionRuntime = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();
      /// MessageBox.Show(clrVersionRuntime);//v2.0.50727
    }

    private void InitializeInterface()
    {
      this.mainForm = new MDIForm.ParentFormClass();
      this.mainForm.imageList1 = this.imageList;
      this.mainForm.imageList2 = imageList2;
      this.mainForm.propertyGrid1 = this.propertyGrid1;
      this.mainForm.contextMenuStrip1 = this.targetMenu;
      this.treeView.Tag = mainForm;
      this.treeView.AccessibleName = "AntPlugin.PluginUI.treeView";
    }

    private void InitializeGradleTree()
    {
      this.gradleTree = new GradleTree(this);
    }

    private void IntializeXmlMenuTree()
    {
      //this.menuTree = new XmlMenuTree(this.pluginMain, this, this.treeView, this.imageList);
      this.menuTree = new XmlMenuTree(this);
    }

    private void IntializeDirTreePanel()
		{
			//this.dirTreePanel = new DirTreePanel(this.pluginMain);
      this.dirTreePanel = new DirTreePanel(this);
      dirTreePanel.Dock = DockStyle.Fill;
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

    private void InitializeXmlTreePanel()
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
      System.Drawing.Image img 
        = System.Drawing.Image.FromFile(Path.Combine(PathHelper.AppDir,@"SettingData\gradle.bmp"));

      ToolStripMenuItem gradleToolStripMenuItem = new ToolStripMenuItem("gradle",img);
      ToolStripMenuItem saveXmlToolStripMenuItem 
        = new ToolStripMenuItem("save all tasks in xml file", 
        PluginBase.MainForm.FindImage("168"), MenuSaveAllTasksInXmlClick);
      ToolStripMenuItem saveTextToolStripMenuItem
        = new ToolStripMenuItem("save all tasks in text file",
        PluginBase.MainForm.FindImage("169"), MenuSaveAllTasksInTextClick);
      ToolStripMenuItem removeXmlToolStripMenuItem
         = new ToolStripMenuItem("remove tasks xml file",
         PluginBase.MainForm.FindImage("166"), MenuRemoveAllTasksXmlClick);

      gradleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
        saveXmlToolStripMenuItem,saveTextToolStripMenuItem,removeXmlToolStripMenuItem});
      buildFileMenu = new ContextMenuStrip();
			buildFileMenu.Items.Add("Run default target", runButton.Image, MenuRunClick);
			buildFileMenu.Items.Add("Edit file", null, MenuEditClick);
			buildFileMenu.Items.Add(new ToolStripSeparator());
			buildFileMenu.Items.Add("Remove",PluginBase.MainForm.FindImage("153"), MenuRemoveClick);
      buildFileMenu.Items.Add(new ToolStripSeparator());
      buildFileMenu.Items.Add(gradleToolStripMenuItem);
      buildFileMenu.Tag = this.treeView;

      targetMenu = new ContextMenuStrip();
			targetMenu.Items.Add("Run target", runButton.Image, MenuRunClick);
			targetMenu.Items.Add("Show in Editor", null, MenuEditClick);
			// 旧版 AntPlugin (session)からimport)
			targetMenu.Items.Add("Show OuterXml", null, ShowOuterXmlClick);
      targetMenu.Tag = this.treeView;
    }
    #endregion

    private void PluginUI_Load(object sender, EventArgs e)
		{
			string aSpluginMain_guid = "078c7c1a-c667-4f54-9e47-d45c0e835c4e"; //ASCompletion
			this.aSpluginMain = PluginBase.MainForm.FindPlugin(aSpluginMain_guid) as ASCompletion.PluginMain;
			this.aSpluginUI = this.aSpluginMain.PluginPanel.Controls[0] as ASCompletion.PluginUI;
			this.outlineTree = this.aSpluginUI.OutlineTree;

			this.propertyGrid1.HelpVisible = false;
			this.propertyGrid1.ToolbarVisible = false;
			
      // TODO: PropertyGrid の編集と保存 実装???
      //this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
			this.splitContainer1.Panel2Collapsed = true;
			this.splitContainer1.Panel1Collapsed = false;
      RefreshData();
		}

    public TreeNode currentNode=null;

    public void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
      TreeView tree = sender as TreeView;
      if (e.Button == MouseButtons.Right)
			{
        this.currentNode = tree.GetNodeAt(e.Location) as TreeNode;
        tree.SelectedNode = currentNode;
        
        // FIXME java cs outline 右クリック
        if (currentNode.Tag == null) return;

        if (tree.Name == "outlineTreeView")
        {
          if (tree.SelectedNode.Tag is NodeInfo)
          {
            targetMenu.Items[0].Visible = false;
            targetMenu.Tag = this.outlineTreeView;
            targetMenu.Show(tree, e.Location);
          }
          return;
        }
        else targetMenu.Tag = treeView;

        if (currentNode.Tag.GetType().Name != "NodeInfo")
				{
          AntTreeNode antNode = treeView.SelectedNode as AntTreeNode;
          if (antNode == null) return;

          XmlNode xmlNode = null;
          if (antNode.Tag is XmlNode)
          {
            xmlNode = antNode.Tag as XmlNode;
            try
            {
              if (currentNode.Parent != null)
              {
                buildFileMenu.Items[3].Enabled = false;
              }
              if (xmlNode.Name == "project" || xmlNode.Name == "package")
                buildFileMenu.Show(treeView, e.Location);
              else
                targetMenu.Show(treeView, e.Location);
            }
            catch (Exception Exception)
            {
              MessageBox.Show(Exception.Message.ToString(), "PluginUI:treeView_NodeMouseClick:272");
            }
          }
          else if (antNode.Tag is TaskInfo)
          {
            if (((TaskInfo)antNode.Tag).Name == "GradleBuildNode")
            {
              buildFileMenu.Show(treeView, e.Location);
            }
            else targetMenu.Show(treeView, e.Location);
          }
          else if (antNode.Tag is String)
          {
            try
            {
              MessageBox.Show((String)antNode.Tag);
              if (currentNode.Parent != null)
              {
                targetMenu.Show(treeView, e.Location);
              }
              else buildFileMenu.Show(treeView, e.Location);
            }
            catch (Exception Exception)
            {
              MessageBox.Show(Exception.Message.ToString(), "PluginUI:treeView_NodeMouseClick:272");
            }
          }
        }
      }
		}

    /// <summary>
    /// TODO: 外部TreeView使用の場合
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ShowOuterXmlClick(object sender, EventArgs e)
    {
      if (((TreeView)this.targetMenu.Tag) == outlineTreeView)
      {
        TreeView tree = outlineTreeView;
        if(tree.SelectedNode.Tag is NodeInfo)
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
        MessageBox.Show(ti.OuterCode,ti.Name);
      }
      else if (node.Tag is XmlNode)
      {
        XmlNode tag = node.Tag as XmlNode;
        MessageBox.Show(tag.OuterXml.Replace("\t", "  ").Replace("	", "  "), "OuterXML : " + node.Target);
      }
    }

		public void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			RunTarget(sender,e);
		}

		public void MenuRunClick(object sender, EventArgs e)
		{
			//RunTarget();
      RunTarget_org(sender,e);
    }

    public void MenuEditClick(object sender, EventArgs e)
    {
      if (((TreeView)this.targetMenu.Tag) == outlineTreeView)
      {
        this.LocateOutLinePanelMember(null, null);
        return;
      }

      TreeView tree = this.treeView;
      if (tree.SelectedNode is AntTreeNode)
			{
				AntTreeNode node = tree.SelectedNode as AntTreeNode;
        if (node.Tag is String)
        {
          PluginBase.MainForm.OpenEditableDocument((String)node.Tag, false);
          return;
        }
        PluginBase.MainForm.OpenEditableDocument(node.File, false);
        ScintillaControl sci = PluginBase.MainForm.CurrentDocument.SciControl;
        String text = sci.Text;
        if (node.Tag is TaskInfo)
        {
          TaskInfo ti = node.Tag as TaskInfo;
          Int32 start = sci.MBSafePosition(text.IndexOf(ti.Definition)); // wchar to byte position
          Int32 end = start + sci.MBSafeTextLength(ti.Definition); // wchar to byte text length
          Int32 line = sci.LineFromPosition(start);
          sci.EnsureVisible(line);
          sci.SetSel(start, end);
        }
        else
        {
          Regex regexp = new Regex("<target[^>]+name\\s*=\\s*\"" + node.Target + "\".*>");
          Match match = regexp.Match(text);
          if (match != null)
          {
            Int32 start = sci.MBSafePosition(match.Index); // wchar to byte position
            Int32 end = start + sci.MBSafeTextLength(match.Value); // wchar to byte text length
            Int32 line = sci.LineFromPosition(start);
            sci.EnsureVisible(line);
            sci.SetSel(start, end);
          }
        }
      }
			else
			{
				if (tree.SelectedNode.Tag.GetType().Name == "NodeInfo")
				{
          PluginBase.MainForm.OpenEditableDocument(((NodeInfo)tree.SelectedNode.Tag).Path, false);
        }
        else
				{
					PluginBase.MainForm.OpenEditableDocument((string)tree.SelectedNode.Tag, false);
				}
			}
		}

		public void MenuRemoveClick(object sender, EventArgs e)
		{
      TreeView tree = this.treeView;
      // これがおかしい
      //try { tree = sender as TreeView; } catch { };
      if (tree.SelectedNode is AntTreeNode)
			{
        String file = ((AntTreeNode)tree.SelectedNode).File;
        String target = ((AntTreeNode)tree.SelectedNode).Target;
        //if (tree.SelectedNode.Tag is TaskInfo)
        //{
        //  String gradleAllTasksPath = Path.Combine(Path.GetDirectoryName(file), @"obj\GradleAllTasks.xml");
        //  String folder = Path.Combine(Path.GetDirectoryName(file), "obj");
        //  if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        //  //if (!File.Exists(gradleAllTasksPath))
        //  Lib.File_SaveUtf8(gradleAllTasksPath, GradleTree.GetXmlStringGradleAllTasks(file));
        //  MessageBox.Show(gradleAllTasksPath,"保存しました");
        //}
        //else
        //{
          pluginMain.RemoveBuildFile((tree.SelectedNode as AntTreeNode).File);
        //}
      }
			else
			{
				if(tree.SelectedNode.Tag.GetType().Name=="NodeInfo")
				{
					pluginMain.RemoveBuildFile(((NodeInfo)tree.SelectedNode.Tag).Path);
				}
				else
				{
					pluginMain.RemoveBuildFile((string)tree.SelectedNode.Tag);
				}
			}
		}

    public void MenuSaveAllTasksInXmlClick(object sender, EventArgs e)
    {
      TreeView tree = this.treeView;
      // これがおかしい
      //try { tree = sender as TreeView; } catch { };
      if (tree.SelectedNode is AntTreeNode)
      {
        String file = ((AntTreeNode)tree.SelectedNode).File;
        String target = ((AntTreeNode)tree.SelectedNode).Target;
        if (tree.SelectedNode.Tag is TaskInfo)
        {
          String gradleAllTasksPath = Path.Combine(Path.GetDirectoryName(file), @"obj\GradleAllTasks.xml");
          String folder = Path.Combine(Path.GetDirectoryName(file), "obj");
          if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
          //if (!File.Exists(gradleAllTasksPath))
          Lib.File_SaveUtf8(gradleAllTasksPath, GradleTree.GetXmlStringGradleAllTasks(file));
          MessageBox.Show(gradleAllTasksPath, "保存しました");
        }
      }
    }

    public void MenuRemoveAllTasksXmlClick(object sender, EventArgs e)
    {
      TreeView tree = this.treeView;
      // これがおかしい
      //try { tree = sender as TreeView; } catch { };
      if (tree.SelectedNode is AntTreeNode)
      {

        String file = ((AntTreeNode)tree.SelectedNode).File;
        String target = ((AntTreeNode)tree.SelectedNode).Target;
        if (tree.SelectedNode.Tag is TaskInfo)
        {
          String gradleAllTasksPath = Path.Combine(Path.GetDirectoryName(file), @"obj\GradleAllTasks.xml");
          String folder = Path.Combine(Path.GetDirectoryName(file), "obj");
          //if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
          //if (!File.Exists(gradleAllTasksPath))

          /////////////////////////////////////////////////////////////
          // ファイルを削除する
          // "C:\test\3.txt"を削除する
          // 指定したファイルが存在しなくても例外は発生しない
          // 読み取り専用ファイルだと、例外UnauthorizedAccessExceptionが発生
          System.IO.File.Delete(gradleAllTasksPath);
          MessageBox.Show(gradleAllTasksPath, "削除");
        }
      }
    }

    public void MenuSaveAllTasksInTextClick(object sender, EventArgs e)
    {
      TreeView tree = this.treeView;
      // これがおかしい
      //try { tree = sender as TreeView; } catch { };
      if (tree.SelectedNode is AntTreeNode)
      {
        String file = ((AntTreeNode)tree.SelectedNode).File;
        String target = ((AntTreeNode)tree.SelectedNode).Target;
        if (tree.SelectedNode.Tag is TaskInfo)
        {
          String gradleAllTasksPath = Path.Combine(Path.GetDirectoryName(file), @"obj\GradleAllTasks.txt");
          String folder = Path.Combine(Path.GetDirectoryName(file), "obj");
          if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
          //if (!File.Exists(gradleAllTasksPath))
          Lib.File_SaveUtf8(gradleAllTasksPath, GradleTree.GetTextStringGradleAllTasks(file));
          MessageBox.Show(gradleAllTasksPath, "保存しました");
          
        }
      }
    }

    public String dirViewSelectedDir = String.Empty;
    public void addButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "BuildFiles (*.xml)|*.XML|" + "WshFiles (*.wsf)|*.wsf|" + "All files (*.*)|*.*";
			dialog.Multiselect = true;
      if(!String.IsNullOrEmpty(this.dirViewSelectedDir))
      {
        dialog.InitialDirectory = this.dirViewSelectedDir;
      }
      else if (PluginBase.CurrentProject != null)
				dialog.InitialDirectory = Path.GetDirectoryName(
					PluginBase.CurrentProject.ProjectPath);

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				pluginMain.AddBuildFiles(dialog.FileNames);
			}
      this.dirViewSelectedDir = String.Empty;
    }

		private void runButton_Click(object sender, EventArgs e)
		{
			//RunTarget();
      RunTarget_org(sender,e);
    }

    // ==================================================================================================
    public void RunTarget_org(object sender, EventArgs e)
    {
      //AntTreeNode node = treeView.SelectedNode as AntTreeNode;
      AntTreeNode node = this.currentNode as AntTreeNode;
      if (node == null)
        return;
      if(node.Tag is String)
      {
        //MessageBox.Show(node.File);
        Process.Start((String)node.Tag);
        return;
      }
      if (node.Target == "GradleBuildNode") node.Target = String.Empty;
      // Globals.MainForm.OpenEditableDocument(node.File, false);
      pluginMain.RunTarget(node.File, node.Target);
    }

    public void RunTarget(object sender, TreeNodeMouseClickEventArgs e)
		{
      try
			{
        TreeView tree = sender as TreeView;
        //TreeNode treeNode = treeView.SelectedNode;
        TreeNode treeNode = tree.SelectedNode;

        if (treeNode is AntTreeNode)
				{
					AntTreeNode antNode = treeView.SelectedNode as AntTreeNode;
					if (antNode == null) return;
          if (antNode.Tag is String)
          {
            String path = antNode.Tag as String;
            //===========================================
            // Fixed Time-stamp: <2019-05-21 08:27:41 kahata>
            path = menuTree.ProcessVariable(path);
            if (!File.Exists(path)
              && File.Exists(Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), path)))
            {
              path = Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), path);
            }
            //===========================================
            //if (path == "GradleTargetNode") this.pluginMain.RunTarget(antNode.File, antNode.Target);
            if (File.Exists(path))
            {
              PluginBase.MainForm.OpenEditableDocument(path, false);
            }
            return;
          }
          else if(antNode.Tag is TaskInfo)
          {
            if (((TaskInfo)antNode.Tag).Name == "GradleBuildNode")
            {
              PluginBase.MainForm.OpenEditableDocument(antNode.File, false);
            }
            else this.pluginMain.RunTarget(antNode.File, antNode.Target);
          }
          else if (antNode.Tag is NodeInfo)
          {
            return;
          }
          else// if (antNode.Tag is XmlNode)
          {
            XmlNode xmlNode = antNode.Tag as XmlNode;
            XmlNode tag = antNode.Tag as XmlNode;
            XmlAttribute descrAttr = tag.Attributes["description"];
            String description = (descrAttr != null) ? descrAttr.InnerText : "";
            switch (xmlNode.Name)
            {
              case "target":
              case "job":
                if (description.StartsWith("Click"))
                {
                  //MessageBox.Show(description + xmlNode.InnerText);
                  //if (description.Contains(";")) this.RunCommand(description+ xmlNode.InnerText.Trim());
                  //else this.RunCommand(description + ";"+ xmlNode.InnerText.Trim());
                  this.RunCommand(description);
                }
                else if (description.StartsWith("Plugin"))
                {
                  this.PluginCommand(xmlNode);
                }
                else pluginMain.RunTarget(antNode.File, antNode.Target);
                return;
              case "property":
                if (description.StartsWith("Click")) this.RunCommand(description);
                else MessageBox.Show(xmlNode.OuterXml.Replace("\t", "  ").Replace("	", "  "), "property : " + xmlNode.Attributes["name"].InnerText);
                return;
              case "taskdef":
                MessageBox.Show(xmlNode.OuterXml.Replace("\t", "  ").Replace("    ", "  "), "taskdef : " + xmlNode.Attributes["resource"].InnerText);
                return;
              case "package":
              case "project":
                AntTreeNode rootnode = treeView.SelectedNode as AntTreeNode;
                //MessageBox.Show(xmlNode.OuterXml.Replace("\t", "  ").Replace("    ", "  "), "taskdef : " + xmlNode.Attributes["resource"].InnerText);
                //Globals.MainForm.OpenEditableDocument(rootnode.File, false);
                PluginBase.MainForm.OpenEditableDocument(rootnode.File, false);
                return;
              default:
                if (treeNode.Parent == null)
                {
                  AntTreeNode node = treeView.SelectedNode as AntTreeNode;
                  //Globals.MainForm.OpenEditableDocument(node.File, false);
                  PluginBase.MainForm.OpenEditableDocument(node.File, false);
                }
                else
                {
                  MessageBox.Show(xmlNode.OuterXml.Replace("\t", "  ").Replace("    ", "  "), "NodeName : " + xmlNode.Name);
                }
                return;
            }
          }
        }
				else if(treeNode.Tag.GetType().Name=="String")
				{
					if (treeNode.Parent == null)
					{
						if(Lib.IsTextFile((string)treeNode.Tag)) PluginBase.MainForm.OpenEditableDocument((string)treeNode.Tag);
						else Process.Start((string)treeNode.Tag);
					}
					else
					{
						TreeNode rootnode = treeNode;
						while (rootnode.Parent != null) rootnode = rootnode.Parent;
						if (Lib.IsTextFile((string)rootnode.Tag)) PluginBase.MainForm.OpenEditableDocument((string)rootnode.Tag);
						else Process.Start((string)rootnode.Tag);

						//ScintillaControl sci = Globals.SciControl;
            ScintillaControl sci = PluginBase.MainForm.CurrentDocument.SciControl;
            String text = sci.Text;
						//Regex regexp = new Regex("<target[^>]+name\\s*=\\s*\"" + node.Target + "\".*>");

						// Time-stamp: <2016-05-13 9:03:27 kahata>
						//string search = ((MemberModel)treeNode.Tag).definition;
						string search = ((CSParser.Model.MemberModel)treeNode.Tag).definition;

						//Match match = regexp.Match(text);
						int index = text.IndexOf(search);
						if (index > 0)
						{
              Int32 start = sci.MBSafePosition(index); // wchar to byte position
              Int32 end = start + sci.MBSafeTextLength(search); // wchar to byte text length
              Int32 line = sci.LineFromPosition(start);
              sci.EnsureVisible(line);
              sci.SetSel(start, end);
						}
					}
				}
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message.ToString(),"PluginUI:RunTarget:555");
			}
		}

		public void LocateMember(CSParser.Model.MemberModel model)
		{
			//if (PluginBase.MainForm.CurrentDocument.FileName  != model.path) 
			PluginBase.MainForm.OpenEditableDocument(model.path);

			//ScintillaControl sci = Globals.SciControl;
      ScintillaControl sci = PluginBase.MainForm.CurrentDocument.SciControl;
      String text = sci.Text;
			//Regex regexp = new Regex("<target[^>]+name\\s*=\\s*\"" + node.Target + "\".*>");

			// Time-stamp: <2016-05-13 9:03:27 kahata>
			//string search = ((MemberModel)treeNode.Tag).definition;
			string search = model.definition;

			//Match match = regexp.Match(text);
			int index = text.IndexOf(search);
			if (index > 0)
			{
        Int32 start = sci.MBSafePosition(index); // wchar to byte position
        Int32 end = start + sci.MBSafeTextLength(search); // wchar to byte text length
        Int32 line = sci.LineFromPosition(start);
        sci.EnsureVisible(line);
        sci.SetSel(start, end);
        //sci.GotoPos(index);
				//sci.SetSel(index, index + search.Length);
			}
		}

    public void LocateMember(NodeInfo ni)
    {
      PluginBase.MainForm.OpenEditableDocument(ni.Path);
       ScintillaControl sci = PluginBase.MainForm.CurrentDocument.SciControl;
      String text = sci.Text;
      // Time-stamp: <2018-05-07 18:03:27 kahata>
      string search = this.menuTree.GetHeadTagFromXmlNode(ni.XmlNode);
 
      int index = text.IndexOf(search);
      if (index > 0)
      {
        Int32 start = sci.MBSafePosition(index); // wchar to byte position
        Int32 end = start + sci.MBSafeTextLength(search); // wchar to byte text length
        Int32 line = sci.LineFromPosition(start);
        sci.EnsureVisible(line);
        sci.SetSel(start, end);
       }
    }

    // kahata: Time-stamp: <2016-04-23 7:24:26 kahata>
		private void RunCommand(String argstring)
		{
			String command = String.Empty;
			String tag = String.Empty;
			try
			{
				string[] args = argstring.Split(',');
				command = args[0].Split('=')[1];
				if (args.Length > 1) tag = args[1].Split('=')[1];
				//Globals.MainForm.CallCommand(command, ProcessVariable(tag));
        PluginBase.MainForm.CallCommand(command, ProcessVariable(tag));
      }
      catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"PluginUI:RunCommand:554");
			}
		}

		public void PluginCommand(XmlNode xmlNode)
		{
		}

		public string ProcessVariable(string strVar)
		{
			string arg = string.Empty;
			arg = PluginBase.MainForm.ProcessArgString(strVar);
			try
			{
				arg = arg.Replace("$(CurProjectDir)", Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath));
				arg = arg.Replace("$(CurProjectName)", Path.GetFileNameWithoutExtension(PluginBase.CurrentProject.ProjectPath));
				arg = arg.Replace("$(Quote)", "\"");
				arg = arg.Replace("$(Dollar)", "$");
				arg = arg.Replace("$(AppDir)", PathHelper.AppDir);
				arg = arg.Replace("$(BaseDir)", PathHelper.BaseDir);
				arg = arg.Replace("$(CurDirName)", Path.GetFileNameWithoutExtension(Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName)));
			}
			catch
			{
			}
			return arg;
		}

		/* backup original
		private void RunTarget()
		{
			AntTreeNode node = treeView.SelectedNode as AntTreeNode;
			if (node == null)
				return;
			pluginMain.RunTarget(node.File, node.Target);
		}
		*/
		// ==================================================================================================

		private void refreshButton_Click(object sender, EventArgs e)
		{
      RefreshData();
		}
		
		public void RefreshData()
		{
			Boolean projectExists = (PluginBase.CurrentProject != null);
			Enabled = projectExists;
			// Fix 2017-01-09
			csOutlineTreePath.Clear();
			csOutlinePanelTreePath.Clear();
			this.MemberId.Clear();
			this.OutLinePanelMemberId.Clear();


      /*
      XmlMenuTree.ToolBarSettingsFiles.Clear();
      XmlMenuTree.toolStripList.Clear();
      InitializeComponent();
      */
      
      if (XmlMenuTree.toolStripList.Count > 0)
      {
        foreach (ToolStrip toolStrip in XmlMenuTree.toolStripList)
        {
          ToolStripManager.RevertMerge(toolStrip, PluginBase.MainForm.ToolStrip);
          toolStrip.Dispose();
        }
        XmlMenuTree.toolStripList.Clear();
      }
     
      if (projectExists)
			{
        AddAntDropdownButton();
				FillTree();
			}
			else
			{
				treeView.Nodes.Clear();
				treeView.Nodes.Add(new TreeNode("No project opened"));
			}
			treeView.CollapseAll();
		}

    public static ToolStripItem antDropDownButton;// = new ToolStripItem();

		private void AddAntDropdownButton()
		{
			String buidfile = Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), "build.xml");

			if(PluginBase.MainForm.ToolStrip.Items.Contains(antDropDownButton))
			{
				PluginBase.MainForm.ToolStrip.Items.Remove(antDropDownButton);
			}

			if (PluginBase.CurrentProject != null && File.Exists(buidfile))
			{
				antDropDownButton = StripBarManager.GetAntDropDownButton(buidfile);
				PluginBase.MainForm.ToolStrip.Items.Add(antDropDownButton);
			}
		}

		public void antDropDownMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem targetItem = sender as ToolStripMenuItem;
			String target = targetItem.Tag as String;
			String buildfile = targetItem.AccessibleDescription;
			//MessageBox.Show(buildfile, target);
			this.pluginMain.RunTarget(buildfile, target);
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

			foreach (String line in pluginMain.BuildFilesList)
			{
        //Fixed Time-stamp: <2019-05-20 17:44:56 kahata>
        //String file = line.Trim();
        String file = menuTree.ProcessVariable(line);
        if(!File.Exists(file) 
          && File.Exists(Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath),file)))
        {
          file = Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), file);
        }
        if (File.Exists(file))
				{
          //Fix 2017-01-09
          if (Path.GetExtension(file) == ".cs" || Path.GetExtension(file) == ".java")
          {
            this.LoadIn(file);
          }
          // kahata 2018-02-09
          else if (Path.GetExtension(file) == ".xml")
          {
            bool isMenu = (Path.GetFileNameWithoutExtension(file).ToLower() == "fdtreemenu"
              || Path.GetFileNameWithoutExtension(file).ToLower() == "xmltreemenu") ? true : false;
            treeView.Nodes.Add(this.menuTree.getXmlTreeNode(file, isMenu));
          }
          else if (Path.GetExtension(file) == ".fdp"
            //|| Path.GetExtension(file) == ".asx"
            //|| Path.GetExtension(file) == ".wax"
            || Path.GetExtension(file) == ".wsf")
          {
            treeView.Nodes.Add(GetBuildFileNode(file));
            // 追加 2017-01-12
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            this.propertyGrid1.SelectedObject = new XmlNodeWrapper(doc.DocumentElement);
          }
          else if (Path.GetExtension(file) == ".gradle")
          {
            //AntTreeNode gradleNode = new AntTreeNode(Path.GetFileName(file), 3);
            //gradleNode.Tag = file;
            //gradleNode.File = file;
            //gradleNode.ToolTipText = file;
            //gradleNode.Target = "run";
            AntTreeNode gradleNode = this.gradleTree.GetGradleOutlineTreeNode(file);
            treeView.Nodes.Add(gradleNode);
          }
          else
          {
            // Fixed Time-stamp: <2019-05-20 17:48:45 kahata>
            int imageindex = this.menuTree.GetIconImageIndex(file);
            TreeNode linkNode = new TreeNode(Path.GetFileName(file), imageindex, imageindex);
            NodeInfo ni = new NodeInfo();
            ni.Type = "record";
            ni.Title = Path.GetFileName(file);
            ni.Command = ni.Path = file;
            // Fixed Time-stamp: <2019-05-20 18:02:07 kahata>
            //linkNode.Tag = file;
            linkNode.Tag = ni;
            linkNode.ToolTipText = file;
            treeView.Nodes.Add(linkNode);
          }
					this.AddPreviousCustomDocuments(file);
				}
			}
			treeView.EndUpdate();
		}

		// ==================================================================================================
		private string File_ReadToEnd(string filepath)
		{
			if (!File.Exists(filepath))
			{
				return "";
			}
			StreamReader streamReader = new StreamReader(filepath, Encoding.GetEncoding("UTF-8"));
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			return result;
		}

    // kokokoko
    public TreeNode GetBuildFileNode(string file)
		{
      //===========================================
      // Fixed Time-stamp: <2019-05-21 08:27:41 kahata>
      file = menuTree.ProcessVariable(file);
      if (!File.Exists(file)
        && File.Exists(Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), file)))
      {
        file = Path.Combine(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), file);
      }
      //===========================================
      XmlDocument xml = new XmlDocument();
			xml.PreserveWhitespace = true;
			xml.Load(file);

			// targetがない 普通の処理
			// XmlNodeList elemList = xml.GetElementsByTagName("target");
			//MessageBox.Show(elemList.Count.ToString());

			XmlAttribute defTargetAttr = xml.DocumentElement.Attributes["default"];
			this.defaultTarget = (defTargetAttr != null) ? defTargetAttr.InnerText : "";

			XmlAttribute nameAttr = xml.DocumentElement.Attributes["name"];
			String projectName = (nameAttr != null) ? nameAttr.InnerText : Path.GetFileName(file);

			XmlAttribute descrAttr = xml.DocumentElement.Attributes["description"];
			String description = (descrAttr != null) ? descrAttr.InnerText : "";

			XmlNodeList elm = xml.GetElementsByTagName("description");
			//MessageBox.Show(elm.Count.ToString());
			
			if (elm.Count > 0)
			{
				description = elm.Item(0).InnerText.Trim();
			}
			else description = file;


      if (projectName.Length == 0) projectName = Path.GetFileName(file);
 
      AntTreeNode rootNode = new AntTreeNode(projectName, ICON_FILE);


      if (Path.GetExtension(file).ToLower()==".wsf") rootNode = new AntTreeNode(projectName, ICON_WSF_FILE);
			if (Path.GetExtension(file).ToLower() == ".fdp") rootNode = new AntTreeNode(projectName, ICON_FDP_FILE);
			rootNode.File = file;
			rootNode.Target = defaultTarget;

      //===========================================
      // Fixed Time-stamp: <2019-05-21 08:27:41 kahata>
      NodeInfo ni = new NodeInfo();
      ni.Type = "record";
      ni.Title = Path.GetFileName(file);
      ni.Command = ni.Path = file;
      rootNode.Tag = ni;
      //rootNode.Tag = xml.DocumentElement;
      //===========================================
      //rootNode.
      //TimeStamp: 2017-01- 07 2016/04/22 14:03 //<2016-01-26 14:25:26 kahata>
      rootNode.ToolTipText = description;

			XmlNodeList nodes = xml.DocumentElement.ChildNodes;
			int nodeCount = nodes.Count;
      //MessageBox.Show(rootNode.File,"expand前");
      for (int i = 0; i < nodeCount; i++)
			{
      
        XmlNode child = nodes[i];
				AntTreeNode antNode = null;
				switch (child.Name)
				{
					case "target":
						// skip private targets
						XmlAttribute targetNameAttr = child.Attributes["name"];
						if (targetNameAttr != null)
						{
							String targetName = targetNameAttr.InnerText;
							if (!String.IsNullOrEmpty(targetName) && (targetName[0] == '-')) continue;
						}
						antNode = GetBuildTargetNode(child, this.defaultTarget);
						antNode.File = file;
						//Kahata TimeStamp: 2016-04-23
						antNode.Tag = child;
						if (子ノード表示ToolStripMenuItem.Checked == true) this.populateChildNodes(child, antNode);
						//Kahata TimeStamp: 2016-04-22
						if (this.内部ターゲット表示ToolStripMenuItem.Checked == true) rootNode.Nodes.Add(antNode);
						else if (antNode.ImageIndex != ICON_INTERNAL_TARGET) rootNode.Nodes.Add(antNode);
						break;

					//Kahata TimeStamp: 2018-02-08
					case "job":
						XmlAttribute jobNameAttr = child.Attributes["id"];
						if (jobNameAttr != null)
						{
							String jobName = jobNameAttr.InnerText;
							if (!String.IsNullOrEmpty(jobName) && (jobName[0] == '-')) continue;
						}
						antNode = GetBuildTargetNode(child, this.defaultTarget);
						antNode.File = file;
						//Kahata TimeStamp: 2016-04-23
						antNode.Tag = child;
						if (子ノード表示ToolStripMenuItem.Checked == true) this.populateChildNodes(child, antNode);
						//Kahata TimeStamp: 2016-04-22
						if (this.内部ターゲット表示ToolStripMenuItem.Checked == true) rootNode.Nodes.Add(antNode);
						else if (antNode.ImageIndex != ICON_INTERNAL_TARGET) rootNode.Nodes.Add(antNode);
						break;

          // 追加 Time-stamp: <2019-03-09 12:21:14 kahata>
          case "xmltreenode":
          case "xmltreemenu":
          case "treemenu":
          case "treenode":
            rootNode.Nodes.Add(this.menuTree.getXmlTreeNodeFromString(child.InnerText, file));
            break;
          case "property":
					case "scriptdef":
					case "taskdef":
					case "macrodef":
						// skip private targets
						XmlAttribute propertyNameAttr = child.Attributes["name"];
						XmlAttribute propertyResourceAttr = child.Attributes["resource"];
            String propertyName = String.Empty;
            if (propertyNameAttr != null)
            {
              propertyName = propertyNameAttr.InnerText;
              if (!String.IsNullOrEmpty(propertyName) && (propertyName[0] == '-')) continue;
            }
            // 追加 Time-stamp: <2019-03-11 10:29:45 kahata>
            if (propertyName.ToLower() == "xmltreemenu" && !String.IsNullOrEmpty(child.InnerText))
            {
              rootNode.Nodes.Add(this.menuTree.getXmlTreeNodeFromString(child.InnerText, file));
            }
            // 追加 Time-stamp: <2019-05-18 08:08:09 kahata>
            else if (propertyName.ToLower()=="wsf" && !String.IsNullOrEmpty(child.InnerText))
            {
              String tmpfile = Path.Combine(@"F:\temp", projectName+".wsf");
              String xmlstr = this.GetFormattedXmlText(child.InnerText);
              Lib.File_SaveEncode(tmpfile, xmlstr, "utf-8");
              TreeNode wsfNode = this.GetBuildFileNode(tmpfile);
              rootNode.Nodes.Add(wsfNode);
            }
            else
            {
              antNode = GetBuildTargetNode(child, defaultTarget);
              antNode.File = file;
              //Kahata TimeStamp: 2016-04-23
              antNode.Tag = child;
              if (子ノード表示ToolStripMenuItem.Checked == true) this.populateChildNodes(child, antNode);
              if (プロパティ表示ToolStripMenuItem.Checked == true) rootNode.Nodes.Add(antNode);
            }
            break;
					default:
						//例外発生 外す
						//antNode = GetBuildTargetNode(child, defaultTarget);
						//antNode.File = file;
						//Kahata TimeStamp: 2016-04-23
						//antNode.Tag = child;
						//if(子ノード表示ToolStripMenuItem.Checked == true) this.populateChildNodes(child, antNode);
						//if(プロパティ表示ToolStripMenuItem.Checked == true) rootNode.Nodes.Add(antNode);
						break;
				}
      }
      //KaHata TimeStamp: 2016-04-22
      try
      {
        if (Path.GetDirectoryName(file) == Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath))
        {
          rootNode.Expand();
        }
      }
      catch { }
      //MessageBox.Show(rootNode.File,"expand後");
      return rootNode;
		}

    /// <summary>
    ///  XML 文字列にインデントつき整形を施す
    /// </summary>
    /// https://qiita.com/otagaisama-1/items/668e7c913b728b3218fc
    /// <param name="s"></param>
    /// <returns></returns>
    private string GetFormattedXmlText(string s)
    {
      var v = new System.Xml.XmlDocument();
      v.LoadXml(s);

      var ws = new System.Xml.XmlWriterSettings();
      ws.Indent = true;
      ws.IndentChars = "  "; // <- インデントの空白数ではなくて、1つ分のインデントとして使う文字列を直接指定します。

      using (var ms = new System.IO.MemoryStream())
      {
        using (var wr = System.Xml.XmlWriter.Create(ms, ws))
        {
          v.WriteContentTo(wr);
          wr.Flush();
          ms.Flush();
        }
        ms.Position = 0;
        using (var rd = new System.IO.StreamReader(ms))
        {
          return rd.ReadToEnd();
        }
      }
    }

    private AntTreeNode GetBuildChild(XmlNode node, string defaultTarget)
		{
			int imageIndex = 0;
			XmlAttribute nameAttr;// = node.Attributes["name"] ?? null;
			//XmlAttribute resourceAttr = node.Attributes["resource"];
			//String targetName = (nameAttr != null) ? nameAttr.InnerText : "";
			String targetName = node.Name;// = (nameAttr != null) ? nameAttr.InnerText : node.Name;// resourceAttr.InnerText;
			String description = node.OuterXml;
			try
			{
				XmlAttribute descrAttr = node.Attributes["description"];
				description = (descrAttr != null) ? descrAttr.InnerText : "";
			}
			catch { }
			/*
				this.imageList.Images.SetKeyName(15, "Method.png");
				this.imageList.Images.SetKeyName(16, "MethodPrivate.png");
				this.imageList.Images.SetKeyName(17, "MethodProtected.png");
				this.imageList.Images.SetKeyName(18, "MethodStatic.png");
				this.imageList.Images.SetKeyName(19, "MethodStaticPrivate.png");
				this.imageList.Images.SetKeyName(20, "MethodStaticProtected.png");
				this.imageList.Images.SetKeyName(30, "Variable.png");
				this.imageList.Images.SetKeyName(31, "VariablePrivate.png");
				this.imageList.Images.SetKeyName(32, "VariableProtected.png");
				this.imageList.Images.SetKeyName(33, "VariableStatic.png");
				this.imageList.Images.SetKeyName(34, "VariableStaticPrivate.png");
				this.imageList.Images.SetKeyName(35, "VariableStaticProtected.png");		
			*/
			AntTreeNode targetNode;
			switch (node.Name)
			{
				case "java":
				case "javac":
					imageIndex = imageList.Images.IndexOfKey("MethodStatic.png");
					targetNode = new AntTreeNode(node.Name, imageIndex);
					break;
				case "mxmlc":
					imageIndex = imageList.Images.IndexOfKey("MethodPrivate.png");
					targetNode = new AntTreeNode(node.Name, imageIndex);
					break;
				case "exec":
					imageIndex = imageList.Images.IndexOfKey("MethodStaticProtected.png");
					targetNode = new AntTreeNode(node.Name, imageIndex);
					break;
				case "property":
					nameAttr = node.Attributes["name"];
					targetName = (nameAttr != null) ? nameAttr.InnerText : "";
					if (description.StartsWith("Click"))
					{
						imageIndex = imageList.Images.IndexOfKey("Method.png");
						targetNode = new AntTreeNode(targetName, imageIndex);
						targetNode.NodeFont = new Font(treeView.Font.Name, treeView.Font.Size, FontStyle.Bold);
						targetNode.ForeColor = Color.Blue;
					}
					else
					{
						int access = description.Length > 0 ? imageList.Images.IndexOfKey("Property.png") : imageList.Images.IndexOfKey("PropertyPrivate.png");
						targetNode = new AntTreeNode(targetName, access);
						targetNode.ForeColor = Color.Green;
					}
					break;
				default:
					imageIndex = imageList.Images.IndexOfKey("VariablePrivate.png");
					targetNode = new AntTreeNode(node.Name, imageIndex);
					break;
			}
			targetNode.Target = targetName;
			targetNode.ToolTipText = node.OuterXml.Replace("\t", "  ").Replace("    ", "  ");
			//AntTreeNode targetNode = new AntTreeNode(node.Name, imageIndex);
			return targetNode;
		}

		private AntTreeNode GetBuildTargetNode(XmlNode node, string defaultTarget)
		{
			XmlAttribute nameAttr = node.Attributes["name"];
			XmlAttribute resourceAttr = node.Attributes["resource"];
			String targetName = "";

			// fix 2017-01-07
			try
			{
				targetName = (nameAttr != null) ? nameAttr.InnerText : resourceAttr.InnerText;
			}
			catch
			{
				targetName = (nameAttr != null) ? nameAttr.InnerText : "";
			}

			
			XmlAttribute descrAttr = node.Attributes["description"];
			String description = (descrAttr != null) ? descrAttr.InnerText : "";

			AntTreeNode targetNode = null;
			int access;

			switch (node.Name)
			{
				case "property":
					if (description.StartsWith("Click"))
					{
						targetNode = new AntTreeNode(targetName, imageList.Images.IndexOfKey("Method.png"));
						targetNode.ForeColor = Color.Blue;
					}
					 else
					{
						access = description.Length > 0 ? imageList.Images.IndexOfKey("Property.png") : imageList.Images.IndexOfKey("PropertyPrivate.png");
						targetNode = new AntTreeNode(targetName, access);
						targetNode.ForeColor = Color.Green;
					}
					break;
				case "taskdef":
					targetName = resourceAttr.InnerText;
					targetNode = new AntTreeNode(targetName, imageList.Images.IndexOfKey("MethodPrivate.png"));
					targetNode.ForeColor = Color.Blue;
					break;
				case "target":
				case "job":
					if (targetName == defaultTarget)
					{
						//targetNode = new AntTreeNode(targetName, ICON_PUBLIC_TARGET);
						targetNode = new AntTreeNode(targetName, ICON_DEFAULT_TARGET);
            /*
            targetNode.NodeFont = new Font(
						treeView.Font.Name,//Meiryo UI, 12pt
						treeView.Font.Size,
						FontStyle.Bold);
            */
            //targetNode
            targetNode.NodeFont = new Font("Meiryo UI", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 128);
          }
					else if (description.Length > 0)
					{
						targetNode = new AntTreeNode(targetName, ICON_PUBLIC_TARGET);
						if (description.StartsWith("Click"))
						{
							//targetNode.NodeFont = new Font(treeView.Font.Name, treeView.Font.Size, FontStyle.Bold);
              targetNode.NodeFont = new Font("Meiryo UI", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 128);
              targetNode.ForeColor = Color.Blue;
						}
						else if (description.StartsWith("Plugin"))
						{
							//targetNode.NodeFont = new Font(treeView.Font.Name, treeView.Font.Size, FontStyle.Bold);
              targetNode.NodeFont = new Font("Meiryo UI", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 128);
              targetNode.ForeColor = Color.Green;
						}
					}
					else
					{
						targetNode = new AntTreeNode(targetName, ICON_INTERNAL_TARGET);
					}
					break;
				default:
					targetNode = new AntTreeNode(node.Name, imageList.Images.IndexOfKey("MethodPrivate.png"));
					break;
			}
			targetNode.Target = targetName;
			//targetNode.ToolTipText = node.OuterXml.Replace("\t", "  ").Replace("    ", "  ");
			targetNode.ToolTipText = description;
			return targetNode;
		}

		private void populateChildNodes(XmlNode parentXmlnode, AntTreeNode parentAntTreenode)
		{
			XmlNodeList childNodeList = parentXmlnode.ChildNodes;  // Get all children for the past node (parent)
			foreach (XmlNode xmlnode in childNodeList)  // loop through all children
			{
				String attrString = String.Empty;
				//MessageBox.Show(xmlnode.Attributes.Count.ToString());
				/*
				for (int j = 0; j < xmlnode.Attributes.Count; j++)
				{
					XmlAttribute xmlAttr = xmlnode.Attributes[j];
					//attrString += "LocalName; " + xmlAttr.LocalName + " ";
					attrString += "Name; " + xmlAttr.Name + " ";
					attrString += "Value; " + xmlAttr.Value + "\r\n";
				}
				*/
				AntTreeNode antNode = GetBuildChild(xmlnode, this.defaultTarget);
				antNode.File = parentAntTreenode.File;// file;
				antNode.Tag = xmlnode;
				antNode.ToolTipText = xmlnode.OuterXml.Replace("\t", "  ").Replace("	", "  ");
				if (!xmlnode.Name.StartsWith("#")) parentAntTreenode.Nodes.Add(antNode);   // add it to the parent node tree
				populateChildNodes(xmlnode, antNode);  // get any children for this node
			}
		}

    /* Original Backup
		private TreeNode GetBuildFileNode(string file)
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

			rootNode.Expand();
			return rootNode;
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
		*/
    // ==================================================================================================

    private void 表示ToolStripButton_Click(object sender, EventArgs e)
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
        else if(this.treeView.SelectedNode.Tag is TaskInfo)
        {
          this.propertyGrid1.SelectedObject = this.treeView.SelectedNode.Tag as TaskInfo;
        }
        else if (this.treeView.SelectedNode.Tag is XmlElement)
        {
          try { this.propertyGrid1.SelectedObject = (XmlElement)this.treeView.SelectedNode.Tag; }
          catch {this.propertyGrid1.SelectedObject = null; }
        }
        else if(this.treeView.SelectedNode.Tag is String)
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
          else if(Lib.IsWebSite(path))
          {
            NodeInfo ni = new NodeInfo();
            ni.Path = path;
            this.propertyGrid1.SelectedObject = ni;
          }
          else
          {
            NodeInfo ni2 = new NodeInfo();
            ni2.Command = path;
            this.propertyGrid1.SelectedObject = ni2;
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

		private void サクラエディタToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AntTreeNode node = treeView.SelectedNode as AntTreeNode;
			String sakuraPath = this.settings.SakuraPath;
			//Globals.MainForm.CallCommand("RunProcessCaptured", sakuraPath + ";" + node.File);
      PluginBase.MainForm.CallCommand("RunProcessCaptured", sakuraPath + ";" + node.File);
    }

    private void エクスプローラToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AntTreeNode node = treeView.SelectedNode as AntTreeNode;
			String buildfileDir = Path.GetDirectoryName(node.File);
			PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.Explore;" + buildfileDir);
		}

		private void コマンドプロンプトToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AntTreeNode node = treeView.SelectedNode as AntTreeNode;
			String buildfileDir = Path.GetDirectoryName(node.File);
			PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.PromptHere;" + buildfileDir);
		}

		private void プロパティ表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.RefreshData();
		}

		private void 子ノード表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.RefreshData();
		}

		private void 内部ターゲット表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//this.内部ターゲット表示ToolStripMenuItem.C
			this.RefreshData();
		}

    private void ツールボタンToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void syncronizeButton_Click(object sender, EventArgs e)
		{
			//String buildfileDir;
			String path;
			try
			{
				TreeNode treeNode = treeView.SelectedNode;
				if (treeNode.GetType().Name == "AntTreeNode")
				{
					AntTreeNode node = treeView.SelectedNode as AntTreeNode;
					//Globals.MainForm.OpenEditableDocument(node.File, false);
					//buildfileDir = Path.GetDirectoryName(node.File);
					path = node.File;
				}
				else if(treeNode.Tag.GetType().Name=="NodeInfo")
				{
					MessageBox.Show(((NodeInfo)treeNode.Tag).Path);
					path = ((NodeInfo)treeNode.Tag).Path;
				}
				else if(treeNode != null)
				{
					path = (String)treeNode.Tag;
				}
				else 
				{
					//buildfileDir = Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName);
					path = PluginBase.MainForm.CurrentDocument.FileName;
				}
			}
			catch (Exception ex1)
			{
				//buildfileDir = Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName);
				MessageBox.Show(ex1.Message.ToString(),"PluginUI:syncronizeButton_Click:1269");
				path = PluginBase.MainForm.CurrentDocument.FileName;
			}
			//PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.BrowseTo;" + buildfileDir);
			this.LoadOut(path);
		}

		public void LoadOut(string path)
		{
			// tab切替の必要???
			//this.UpdateCsOutlineParser();
			try
			{
				path = PluginBase.MainForm.ProcessArgString(path);
				//MessageBox.Show(path);
				string buildfileDir = Path.GetDirectoryName(path);

				if (this.CSOutlinePanelMenuItem.Checked == true)
				{
					//string path = PluginBase.MainForm.CurrentDocument.FileName;
					if (Path.GetExtension(path) == ".cs" || Path.GetExtension(path) == ".java")
					{
						// http://www.atmarkit.co.jp/fdotnet/dotnettips/053allfiles/allfiles.html				
						//	Directoryクラスのメソッドでは、次のようにして検索を行える。
						//	string[] files = Directory.GetFiles("c:\\", "*.cs");
						//	string[] dirs  = Directory.GetDirectories("c:\\", "*Microsoft*");
						//	string[] both  = Directory.GetFileSystemEntries("c:\\", "??");
						//	この例では、上から順に、拡張子が“.cs”のファイル、“Microsoft”を含むディレクトリ、
						//	名前が2文字以内のディレクトリかファイルを、Cドライブのルート・ディレクトリから検索する。					

						buildfileDir = Path.GetDirectoryName(PluginBase.MainForm.CurrentDocument.FileName);
						//string[] files = Directory.GetFiles(buildfileDir, "*.cs");

						// Kahata 未完成 Time-stamp: <2016-05-17 14:21:27 kahata>
						//foreach (string file in files)
						//{
						//TreeNode rootNode = this.tree.CsOutlineTreeNode(file, this.imageList, this.OutLinePanelMemberId);
						//if (!csOutlinePanelTreePath.Contains(file))
						//{
						//csOutlinePanelTreePath.Add(file);
						//this.outlineTree.Nodes.Add(rootNode);
						//}
						//}

						this.imageList.Tag = "OutlinePanel";
						TreeNode rootNode = this.buildTree.CsOutlineTreeNode(path, this.imageList, this.OutLinePanelMemberId);
						if (!csOutlinePanelTreePath.Contains(path))
						{
							csOutlinePanelTreePath.Add(path);
							this.outlineTree.Nodes.Clear();
							this.outlineTree.Nodes.Add(rootNode);
						}
						// kahata contextmenustrip メニューの内容未完成 Time-stamp: <2016-05-17 15:47:27 kahata>
						//MessageBox.Show(this.imageList.Tag.ToString());//this.outlineTree.ImageList.GetType().FullName);
						//this.outlineTree.ImageList = this.imageList;
						this.outlineTree.ContextMenuStrip = this.csOutlineMenu;
						this.outlineTree.NodeMouseDoubleClick
								+= new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.LocateOutLinePanelMember);
					}
				}
				if (Directory.Exists(buildfileDir))
				{
					PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.BrowseTo;" + buildfileDir);
				}
			}
			catch (Exception ex1)
			{
				MessageBox.Show(ex1.Message.ToString(),"PluginUI:LoadOut:1336");
			}
		}

		private void syncronizeDodument_Click(object sender, EventArgs e)
		{
			string path = PluginBase.MainForm.CurrentDocument.FileName;
			this.LoadIn(path);
		}

		public void LoadIn(string path)
		{
			try
			{
				path = PluginBase.MainForm.ProcessArgString(path);
				if (Path.GetExtension(path) == ".xml")
				{
					string[] s1 = new string[1] { PluginBase.MainForm.CurrentDocument.FileName };
					//String[] filenames = new String[];
					pluginMain.AddBuildFiles(s1);
				}
				else if (Path.GetExtension(path) == ".cs" || Path.GetExtension(path) == ".java")
				{
					if (!csOutlineTreePath.Contains(path))
					{
						csOutlineTreePath.Add(path);
						//CsOutlineTree.BuildCSOutlineTree(path);
						treeView.BeginUpdate();
						this.imageList.Tag = "Ant";
						//treeView.Nodes.Clear();
						TreeNode rootNode = this.buildTree.CsOutlineTreeNode(path, this.imageList, this.MemberId);
						treeView.Nodes.Add(rootNode);
						this.AddPreviousCustomDocuments(path);
						treeView.EndUpdate();
					}
				}
			}
			catch (Exception ex1)
			{
				MessageBox.Show(ex1.Message.ToString(),"PluginUI:LoadIn:1375");
			}
		}

		private void CSOutlinePanelMenuItem_Click(object sender, EventArgs e)
		{
			//this.CSOutlinePanelMenuItem.Checked = true;
		}

		private void testToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CsOutlineParse();
		}

		private void csOutlineButton1_Click(object sender, EventArgs e)
		{
			this.CsOutlineParse();
		}

    private void homeStripButton_Click(object sender, EventArgs e)
    {
      treeView.Nodes.Clear();
      // bug fix 2018-03-16
      // タブコントロールに組み込むと最初に加えたノードが表示されなくなる
      // 間に合せのパッチ
      TreeNode dummy = new TreeNode("dummy");
      treeView.Nodes.Add(dummy);
      treeView.Nodes.Add(this.menuTree.getXmlTreeNode(this.settings.HomeMenuPath, true));
    }

    public TreeView outlineTreeView = new TreeView();
    public void CsOutlineParse()
		{
      string path = PluginBase.MainForm.CurrentDocument.FileName;
      TreeNode rootNode=null;
      if (this.aSpluginUI.Controls.Contains(outlineTreeView))
      {
        this.aSpluginUI.Controls.Remove(outlineTreeView);
        outlineTreeView.Dispose();
      }
      if (Path.GetExtension(path) == ".cs" || Path.GetExtension(path) == ".java")
      {
        this.imageList.Tag = "Ant";
        try
        {
          outlineTreeView = new TreeView();
          outlineTreeView.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.LocateOutLinePanelMember);
           outlineTreeView.ImageList = this.imageList;
          outlineTreeView.Dock = DockStyle.Fill;
          this.OutLinePanelMemberId.Clear();

          rootNode = this.buildTree.CsOutlineTreeNode(path, this.imageList, this.OutLinePanelMemberId);

          //おかしい
          //TreeNode rootNode = this.tree.CsOutlineTreeNode(path);
          outlineTreeView.Nodes.Add(rootNode);
          this.aSpluginUI.Controls.Add(outlineTreeView);
          outlineTreeView.BringToFront();

        }
        catch { }
      }
      else if (Path.GetExtension(path) == ".xml" 
        || Path.GetExtension(path) == ".wsf"
        || Path.GetExtension(path) == ".asx"
        || Path.GetExtension(path) == ".wax"
       || Path.GetExtension(path) == ".fdp"
        )
      {
        try
        {
          outlineTreeView = new TreeView();
          outlineTreeView.Name = "outlineTreeView";
          outlineTreeView.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.LocateOutLinePanelMember);
          outlineTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
          //outlineTreeView.ImageList = this.imageList;
          outlineTreeView.Dock = DockStyle.Fill;
          outlineTreeView.ShowNodeToolTips = true;

          //rootNode = this.menuTree.getXmlTreeNode(path, true);
          //XMLファイルを読み込む
          System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
          xmlDoc.Load(path);
          System.Xml.XmlElement xmlRoot = xmlDoc.DocumentElement;

          NodeInfo nodeInfo = this.menuTree.SetNodeinfo(xmlRoot, path);
          //TreeNode trvRoot = this.menuTree.BuildTreeNode(nodeInfo, path);
          //TreeNode trvRoot = new TreeNode(path);
          TreeNode trvRoot = new TreeNode(xmlRoot.Name);
          trvRoot.ToolTipText = this.menuTree.GetHeadTagFromXmlNode(xmlRoot);// path;
          trvRoot.Tag = nodeInfo;
          //XMLをツリーノードに変換する
          rootNode = this.menuTree.MakeXmlTreeMode(xmlRoot, trvRoot);
          //XMLをツリービューに変換したノードを追加する
          outlineTreeView.Nodes.Add(rootNode);
          this.aSpluginUI.Controls.Add(outlineTreeView);
          outlineTreeView.BringToFront();
        }
        catch { }
      }
    }

    public void CsOutlineParse_old()
    {
      string path = PluginBase.MainForm.CurrentDocument.FileName;
      if (Path.GetExtension(path) != ".cs" && Path.GetExtension(path) != ".java") return;
      this.imageList.Tag = "Ant";
      //MessageBox.Show(path);
      try
      {
        TreeNode rootNode = this.buildTree.CsOutlineTreeNode(path, this.imageList, this.OutLinePanelMemberId);
        this.outlineTree.NodeMouseDoubleClick
              += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.LocateOutLinePanelMember);
        if (!csOutlinePanelTreePath.Contains(path))
        {
          csOutlinePanelTreePath.Add(path);
          this.outlineTree.Nodes.Add(rootNode);
        }
      }catch { }
    }
	
		private void LocateOutLinePanelMember(object sender, EventArgs e)
		{
			string path = PluginBase.MainForm.CurrentDocument.FileName;
      Object tag = outlineTreeView.SelectedNode.Tag;
      if (tag.GetType().FullName == "CSParser.Model.MemberModel")
			{
				CSParser.Model.MemberModel model = tag as CSParser.Model.MemberModel;
				this.LocateMember(model);
			}
      else if (tag.GetType().Name == "NodeInfo")
      {
        NodeInfo ni = tag as NodeInfo;
        this.LocateMember(ni);
      }
      else if (tag.GetType().FullName == "System.String" && File.Exists((string)tag))
			{
				PluginBase.MainForm.OpenEditableDocument((string)tag);
			}
    }

		public NodeInfo SelectedNodeInfo()
		{
			return (NodeInfo)this.treeView.SelectedNode.Tag;
		}

		/// <summary>
		/// Calls a normal MainForm method
		/// </summary>
		public Boolean CallCommand(String name, String tag = "")
		{
			try
			{
				//MessageBox.Show(name,tag);
				Type mfType = this.GetType();
				System.Reflection.MethodInfo method = mfType.GetMethod(name);
				if (method == null) throw new MethodAccessException();
				ToolStripMenuItem button = new ToolStripMenuItem();

				// Bugfix 2018-03-06
				//button.Tag = new ItemData(null, tag, null); // Tag is used for args
				button.Tag = tag;// new ItemData(null, tag, null); // Tag is used for args

				Object[] parameters = new Object[2];
				parameters[0] = button; parameters[1] = null;
				//string[] parameters = tag.Split('|');
				method.Invoke(this, parameters);

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"PluginUI:CallCommand:1532");
				return false;
			}




			/*
			try
			{
				tag = this.ProcessVariable(tag);
				Type mfType = this.GetType();
				System.Reflection.MethodInfo method = mfType.GetMethod(name);
				if (method == null) throw new MethodAccessException();
				//button.Tag = new ItemData(null, tag, null); // Tag is used for args
				//Object[] parameters = new Object[2];
				//parameters[0] = button; parameters[1] = null;
				//string[] parameters = tag.Split('|');
				method.Invoke(this, new object[] { tag });
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
				return false;
			}
			*/


		}

		#region Previous Document
		public void PopulatePreviousCustomDocumentsMenu()
		{
			try
			{
				ToolStripMenuItem toolStripMenuItem = this.最近開いたカスタムドキュメントToolStripMenuItem;
				toolStripMenuItem.DropDownItems.Clear();
				for (int i = 0; i < this.settings.PreviousCustomDocuments.Count; i++)
				{
					string text = this.settings.PreviousCustomDocuments[i];
					ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
					toolStripMenuItem2.Click += new EventHandler(this.PreviousCustomDocumentsMenuItem_Click);
					toolStripMenuItem2.Tag = text;
					toolStripMenuItem2.Text = PathHelper.GetCompactPath(text);
					if (i < 15)
					{
						toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
					}
					else
					{
						this.settings.PreviousCustomDocuments.Remove(text);
					}
				}
				if (this.settings.PreviousCustomDocuments.Count > 0)
				{
					toolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
					toolStripMenuItem.DropDownItems.Add(this.最近開いたカスタムドキュメントをクリアToolStripMenuItem);
					toolStripMenuItem.Enabled = true;
				}
				else
				{
					toolStripMenuItem.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"PluginUI:PopulatePreviousCustomDocumentsMenu:1599");
			}
		}

		public void PopulateFileStateMenu()
		{
			try
			{
				ToolStripMenuItem toolStripMenuItem = this.ファイル状態の保存と復元ToolStripMenuItem;
				toolStripMenuItem.DropDownItems.Clear();

				for (int i = 0; i < this.settings.FileStates.Count; i++)
				{
					string text = this.settings.FileStates[i];
					string[] array = text.Split(new char[] { ';' });
					string text2 = array[0];
					ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
					toolStripMenuItem2.Click += new EventHandler(this.FileStatesRecover_Click);
					toolStripMenuItem2.Tag = text;
					toolStripMenuItem2.Text = PathHelper.GetCompactPath(text2);
					if (i < 15)
					{
						this.ファイル状態の保存と復元ToolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
					}
					else
					{
						this.settings.FileStates.Remove(text);
					}
				}
				this.ファイル状態の保存と復元ToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
				this.ファイル状態の保存と復元ToolStripMenuItem.DropDownItems.Add(this.前回セッションの復元ToolStripMenuItem);
				this.ファイル状態の保存と復元ToolStripMenuItem.DropDownItems.Add(this.復元ポイントの保存ToolStripMenuItem);
				this.ファイル状態の保存と復元ToolStripMenuItem.DropDownItems.Add(this.全復元ポイントのクリアToolStripMenuItem);
				this.ファイル状態の保存と復元ToolStripMenuItem.Enabled = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"PluginUI:PopulateFileStateMenu:1640");
			}
		}

		private void PreviousCustomDocumentsMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
			string tagstring = toolStripMenuItem.Tag as string;
			string[] files = new string[1]; files[0] = tagstring;
			pluginMain.AddBuildFiles(files);

			/*
			List<string> xmlFiles = new List<string>();
			List<string> csFiles = new List<string>();
			if (Path.GetExtension(tagstring).ToLower() == ".xml") xmlFiles.Add(tagstring);
			if (Path.GetExtension(tagstring).ToLower() == ".cs") csFiles.Add(tagstring);
			if (Path.GetExtension(tagstring).ToLower() == ".java") csFiles.Add(tagstring);
			pluginMain.AddBuildFiles(xmlFiles.ToArray());
			treeView.BeginUpdate();
			*/
			/*
			this.AddPreviousCustomDocuments(tagstring);


			//treeView.Nodes.Clear();
			foreach (string path in csFiles)
			{
				if (!csOutlineTreePath.Contains(path))
				{
					csOutlineTreePath.Add(path);
					//TreeNode rootNode = CsOutlineTree.CsOutlineTreeNode(path);
					//TreeNode rootNode = this.tree.CsOutlineTreeNode(path, this.imageList);
					this.imageList.Tag = "Ant";
					TreeNode rootNode = this.tree.CsOutlineTreeNode(path, this.imageList, this.MemberId);
					treeView.Nodes.Add(rootNode);
					this.AddPreviousCustomDocuments(path);
				}
			}
			treeView.EndUpdate();
			*/
		}

		public void AddPreviousCustomDocuments(string data)
		{
			try
			{
				if (this.settings.PreviousCustomDocuments.Contains(data))
				{
					this.settings.PreviousCustomDocuments.Remove(data);
				}
				this.settings.PreviousCustomDocuments.Insert(0, data);
				// ここがポイント
				this.pluginMain.SaveSettings();
				this.PopulatePreviousCustomDocumentsMenu();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"PlugnUI:AddPreviousCustomDocuments:1693");
			}
		}

		private void 最近開いたカスタムドキュメントをクリアToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			this.settings.PreviousCustomDocuments.Clear();
			this.PopulatePreviousCustomDocumentsMenu();
		}
		#endregion

		#region Session Management
		private void 前回セッションの復元ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("前回のカスタムセッションを復元します\nよろしいですか？", "セッションの復元", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			if (dialogResult == DialogResult.OK)
			{
				this.RestoreCustomSession();
			}
		}

		private void 復元ポイントの保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string name = string.Empty;
			string path = string.Empty;
			string type = string.Empty;// PluginBase.MainForm.Documents[i].Text;
			string item = string.Empty;
			DateTime now = DateTime.Now;
			item = string.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", new object[] { now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second });

			//MessageBox.Show(this.treeView.Nodes.Count.ToString());
			for (int i = 0; i < this.treeView.Nodes.Count; i++)
			{
				if (this.treeView.Nodes[i].GetType().FullName == "AntPlugin.AntTreeNode")
				{
					type = "AntTreeNode";
					name = ((AntPlugin.AntTreeNode)this.treeView.Nodes[i]).Text;
					path = ((AntPlugin.AntTreeNode)this.treeView.Nodes[i]).File;
				}
				else
				{
					type = "TreeNode";
					name = ((TreeNode)this.treeView.Nodes[i]).Text;
					path = ((TreeNode)this.treeView.Nodes[i]).Tag.ToString();
				}
				string str = string.Concat(new string[] { type, "|", name, "|", path });
				item = item + ";" + str;
			}
			this.AddFileStateMenuItem(item);
		}

		private void RestoreCustomSession()
		{
			this.treeView.Nodes.Clear();
			List<string> customSessionData = this.settings.CustomSessionData;
			for (int i = 0; i < customSessionData.Count; i++)
			{
				string[] array = customSessionData[i].Split(new char[] { '|' });
				string type = array.Length > 0 ? array[0] : string.Empty;
				string name = array.Length > 1 ? array[1] : string.Empty;
				string path = array.Length > 2 ? array[2] : string.Empty;
				switch (type)
				{
					case "AntTreeNode":
						//MessageBox.Show(path);
						treeView.Nodes.Add(GetBuildFileNode(path));
						this.AddPreviousCustomDocuments(path);
						break;
					case "TreeNode":
						this.LoadIn(path);
						this.AddPreviousCustomDocuments(path);
						break;
				}
			}
		}

		public void AddFileStateMenuItem(string data)
		{
			try
			{
				if (this.settings.FileStates.Contains(data))
				{
					this.settings.FileStates.Remove(data);
				}
				this.settings.FileStates.Insert(0, data);
				this.PopulateFileStateMenu();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"PluginUI:AddFileStateMenuItem:1782");
			}
		}

		public void SaveCustomSession()
		{
			this.settings.CustomSessionData.Clear();
			string name = string.Empty;
			string path = string.Empty;
			string type = string.Empty;
			for (int i = 0; i < this.treeView.Nodes.Count; i++)
			{
				if (this.treeView.Nodes[i].GetType().Name == "AntTreeNode")
				{
					type = "AntTreeNode";
					name = ((AntPlugin.AntTreeNode)this.treeView.Nodes[i]).Text;
					path = ((AntPlugin.AntTreeNode)this.treeView.Nodes[i]).File;
				}
				else
				{
					type = "TreeNode";
					name = ((TreeNode)this.treeView.Nodes[i]).Text;
					path = ((TreeNode)this.treeView.Nodes[i]).Tag.ToString();
				}
				string item = string.Concat(new string[] { type, "|", name, "|", path });
				this.settings.CustomSessionData.Add(item);
			}
		}

		private void FileStatesRecover_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
			string text = toolStripMenuItem.Tag as string;
			string[] array = text.Split(new char[] { ';' });
			this.treeView.Nodes.Clear();
			for (int i = 1; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[] { '|' });
				string type = array2.Length > 0 ? array2[0] : string.Empty;
				string name = array2.Length > 1 ? array2[1] : string.Empty;
				string path = array2.Length > 2 ? array2[2] : string.Empty;
				switch (type)
				{
					case "AntTreeNode":
						//MessageBox.Show(path);
						treeView.Nodes.Add(GetBuildFileNode(path));
						this.AddPreviousCustomDocuments(path);
						break;
					case "TreeNode":
						this.LoadIn(path);
						this.AddPreviousCustomDocuments(path);
						break;
				}
			}
		}

		private void 全復元ポイントのクリアToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.settings.FileStates.Clear();
			this.PopulateFileStateMenu();
		}

    #endregion

    public void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
      String projectDir = Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath);
      this.treeView.SelectedNode.Tag = this.propertyGrid1.SelectedObject;
      this.treeView.SelectedNode.Text = ((NodeInfo)treeView.SelectedNode.Tag).Title;
      // BeseNodeの探索
      TreeNode baseNode = new TreeNode();
      foreach (TreeNode tn in this.treeView.Nodes) if (TreeViewManager.IsChildNode(tn, this.treeView.SelectedNode)) baseNode = tn;
      NodeInfo ni = baseNode.Tag as NodeInfo;
      String xmlPath = ni.Path;
      if (!File.Exists(ni.Path) && File.Exists(Path.Combine(projectDir, ni.Path)))
      {
        xmlPath = Path.Combine(projectDir, ni.Path);
      }
      String msgboxTitle = treeView.SelectedNode.Text + " が変更されました";
      String msgboxString = "メニューファイルを\n" + xmlPath + "\nに保存しますか?";
      if (MessageBox.Show(msgboxString, msgboxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        Lib.File_BackUpCopy(xmlPath);
        //TODO FIXME
        //this.menuTree.SaveFile(@"F:\temp\test.xml");
        this.menuTree.SaveFile(xmlPath);
      }
      //LoadFile(this.filepath);
    }
  }

  //internal class AntTreeNode : TreeNode
  public class AntTreeNode : TreeNode
  {
    public String File;
		public String Target;

		public AntTreeNode(string text, int imageIndex)
			: base(text, imageIndex, imageIndex)
		{
		}
	}

  /// <summary>
  /// How to load xml document in Property Grid
  /// </summary>
  /// http://stackoverflow.com/questions/4591115/how-to-load-xml-document-in-property-grid
  /// 追加 2017-01-12
  [TypeConverter(typeof(XmlNodeWrapperConverter))]
	class XmlNodeWrapper
	{
		private readonly XmlNode node;
		public XmlNodeWrapper(XmlNode node) { this.node = node; }

		class XmlNodeWrapperConverter : ExpandableObjectConverter
		{
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				List<PropertyDescriptor> props = new List<PropertyDescriptor>();
				XmlElement el = ((XmlNodeWrapper)value).node as XmlElement;
				if (el != null)
				{
					foreach (XmlAttribute attr in el.Attributes)
					{
						props.Add(new XmlNodeWrapperPropertyDescriptor(attr));
					}
				}
				foreach (XmlNode child in ((XmlNodeWrapper)value).node.ChildNodes)
				{
					props.Add(new XmlNodeWrapperPropertyDescriptor(child));
				}
				return new PropertyDescriptorCollection(props.ToArray(), true);
			}

			public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
			{
				return destinationType == typeof(string)
					? ((XmlNodeWrapper)value).node.InnerXml
					: base.ConvertTo(context, culture, value, destinationType);
			}
		}

		class XmlNodeWrapperPropertyDescriptor : PropertyDescriptor
		{
			private static readonly Attribute[] nix = new Attribute[0];
			private readonly XmlNode node;
			public XmlNodeWrapperPropertyDescriptor(XmlNode node)
				: base(GetName(node), nix)
			{
				this.node = node;
			}
			static string GetName(XmlNode node)
			{
				switch (node.NodeType)
				{
					case XmlNodeType.Attribute: return "@" + node.Name;
					case XmlNodeType.Element: return node.Name;
					case XmlNodeType.Comment: return "<!-- -->";
					case XmlNodeType.Text: return "(text)";
					default: return node.NodeType + ":" + node.Name;
				}
			}

			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			public override void SetValue(object component, object value)
			{
				node.Value = (string)value;
			}

			public override bool CanResetValue(object component)
			{
				return !IsReadOnly;
			}

			public override void ResetValue(object component)
			{
				SetValue(component, "");
			}

			public override Type PropertyType
			{
				get
				{
					switch (node.NodeType)
					{
						case XmlNodeType.Element:
							return typeof(XmlNodeWrapper);
						default:
							return typeof(string);
					}
				}
			}

			public override bool IsReadOnly
			{
				get
				{
					switch (node.NodeType)
					{
						case XmlNodeType.Attribute:
						case XmlNodeType.Text:
							return false;
						default:
							return true;
					}
				}
			}

			public override object GetValue(object component)
			{
				switch (node.NodeType)
				{
					case XmlNodeType.Element:
						return new XmlNodeWrapper(node);
					default:
						return node.Value;
				}
			}

			public override Type ComponentType
			{
				get { return typeof(XmlNodeWrapper); }
			}
		}
	}

}


