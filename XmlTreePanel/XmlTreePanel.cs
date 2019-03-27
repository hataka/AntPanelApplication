using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using AntPanelApplication.XmlMenuTree;
//using PluginCore;
using System.IO;
//using PluginCore.Helpers;
using AntPanelApplication.CommonLibrary;
using AntPanelApplication.Managers;
using System.Diagnostics;
using System.Xml;
//using FlashDevelop;
using System.Text.RegularExpressions;
using AntPanelApplication.Helpers;

namespace AntPanelApplication
{
  public partial class XmlTreePanel : UserControl
  {
    #region Variables
    global::AntPanelApplication.Properties.Settings settings
          = new global::AntPanelApplication.Properties.Settings();

    //private PluginMain pluginMain;
    private AntPanel antPanel;
    public XmlMenuTree menuTree = null;
    // TODO: AntPlugin Settings
    public static string menuPath = Path.Combine(PathHelper.SettingDataDir, "FDTreeMenu.xml");
    public string currentTreeMenuFilepath = Path.Combine(PathHelper.SettingDataDir, "FDTreeMenu.xml");

    public MDIForm.ParentFormClass mainForm;
    public MDIForm.ChildFormControlClass instance;

    private ContextMenuStrip buildFileMenu;
    private ContextMenuStrip targetMenu;

    #endregion

    #region Constructor
    public XmlTreePanel()
    {
      InitializeComponent();
    }

    public XmlTreePanel(AntPanel ui)
    {
      this.antPanel = ui;
      //this.pluginMain = ui.pluginMain;
      InitializeComponent();
      // FIXME 例外発生 !!
      //CreateMenus();
      InitializeInterface();
      InitializeTreeView();
    }

    #endregion

    private void InitializeInterface()
    {
      this.mainForm = new MDIForm.ParentFormClass();
      this.mainForm.imageList1 = Globals.AntPanel.imageList;
      this.mainForm.imageList2 = AntPanel.imageList2;
      this.mainForm.propertyGrid1 = this.propertyGrid1;
      this.treeView1.Tag = mainForm;
      this.treeView1.AccessibleName = "AntPlugin.Controls.XmlTreePanel.treeView1";
    }


    // FIXME 例外発生 !!
    private void CreateMenus()
    {
      buildFileMenu = new ContextMenuStrip();
      buildFileMenu.Items.Add("Run default target", Globals.AntPanel.runButton.Image, this.MenuRunClick);
      buildFileMenu.Items.Add("Edit file", null, this.MenuEditClick);
      buildFileMenu.Items.Add(new ToolStripSeparator());
      buildFileMenu.Items.Add("Remove", Globals.MainForm.FindImage("153"), this.MenuRemoveClick);
      buildFileMenu.Tag = this.treeView1;
      
      targetMenu = new ContextMenuStrip();
      targetMenu.Items.Add("Run target", Globals.AntPanel.runButton.Image, this.MenuRunClick);
      targetMenu.Items.Add("Show in Editor", null, this.MenuEditClick);
      targetMenu.Items.Add("Show OuterXml", null, this.ShowOuterXmlClick);
      targetMenu.Tag = this.treeView1;
    }

    public void InitializeTreeView()
    {
      XmlTreePanel.menuPath = Path.Combine(PathHelper.BaseDir, "FDTreeMenu.xml");// this.settings.HomeMenuPath;
      this.currentTreeMenuFilepath = Path.Combine(PathHelper.BaseDir, "FDTreeMenu.xml");//this.pluginUI.settings.HomeMenuPath;

      this.menuTree = Globals.AntPanel.menuTree;// new XmlMenuTree(ui);
      this.treeView1.ImageList = Globals.AntPanel.imageList;
      this.splitContainer1.Panel2Collapsed = true;
      this.splitContainer1.Panel1Collapsed = false;
      this.propertyGrid1.HelpVisible = false;
      this.propertyGrid1.ToolbarVisible = false;

      this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
      this.treeView1.DoubleClick += new EventHandler(this.treeView_DoubleClick);
      this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);

      //this.treeView.AfterExpand += new TreeViewEventHandler(this.treeView_AfterExpand);
      //this.treeView.AfterCollapse += new TreeViewEventHandler(this.treeView_AfterCollapse);
      //this.treeView.Click += new TreeViewEventHandler(this.treeView_Click);

      this.treeView1.Nodes.Add(menuTree.getXmlTreeNode(menuPath, true));
    }

    #region TreeView Event Handler

    // No reffered 将来削除
    public string ProcessVariable(string strVar)
    {
      /*
      string arg = string.Empty;
      try
      {
        arg = Globals.MainForm.ProcessArgString(strVar);
      }
      catch { arg = strVar; }
      try
      {
        arg = arg.Replace("$(CurProjectDir)", Path.GetDirectoryName(GlobalsPluginBase.CurrentProject.ProjectPath));
        arg = arg.Replace("$(CurProjectName)", Path.GetFileNameWithoutExtension(PluginBase.CurrentProject.ProjectPath));
        //arg = arg.Replace("$(CurProjectUrl)", Lib.Path2Url(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), this.settings.DocumentRoot, this.settings.ServerRoot));
        arg = arg.Replace("$(CurProjectUrl)", Lib.Path2Url(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), "localhost"));
        arg = arg.Replace("$(Quote)", "\"");
        arg = arg.Replace("$(Dollar)", "$");
        arg = arg.Replace("$(AppDir)", PathHelper.AppDir);
        arg = arg.Replace("$(BaseDir)", PathHelper.BaseDir);
        arg = arg.Replace("$(CurSciText)", PluginBase.MainForm.CurrentDocument.SciControl.Text);
        arg = arg.Replace("$(CurFileUrl)", Lib.Path2Url(PluginBase.MainForm.CurrentDocument.FileName, "localhost"));
      }
      catch { }
      */    
      return strVar;
    }

    public void treeView_DoubleClick(object sender, EventArgs e)
    {
      /// XmlTreeView カプセル化のため追加 2018-02-23
      // fixed 2018-2-23
      //TreeNode treeNode = treeView.SelectedNode;
      TreeView tree = sender as TreeView;
      TreeNode treeNode = tree.SelectedNode;

      ToolStripMenuItem button = new ToolStripMenuItem();
      try
      {
        //if (treeNode.Tag.GetType().Name != "NodeInfo") return;
        if (this.treeView1.SelectedNode.Parent == null)
        {
          Globals.MainForm.OpenEditableDocument(((NodeInfo)treeNode.Tag).Path);
          return;
        }
        else if (treeNode is AntTreeNode)
        {
          XmlNode xmlNode = treeNode.Tag as XmlNode;
          XmlNode tag = treeNode.Tag as XmlNode;
          XmlAttribute descrAttr = tag.Attributes["description"];
          if (xmlNode.Name == "project" || xmlNode.Name == "package")
          {
            Globals.MainForm.OpenEditableDocument(((AntTreeNode)treeNode).File, false);
          }
          else
          {
            //MessageBox.Show(((AntTreeNode)treeNode).File, ((AntTreeNode)treeNode).Target);
            Globals.AntPanel.RunTarget(((AntTreeNode)treeNode).File, ((AntTreeNode)treeNode).Target);
          }
          return;
        }
        else if (treeNode.Tag is NodeInfo)
        {
          this.menuTree.treeView_DoubleClick(sender, e);
          return;
        }
      }
      catch (Exception ex2)
      {
        String errorMsg = ex2.Message.ToString();
        //MessageBox.Show(errorMsg);
        MessageBox.Show(Lib.OutputError(errorMsg), "XmlTreePanel:treeView_DoubleClick");
      }
    }

    // no reffered
    public NodeInfo SelectedNodeInfo()
    {
      return (NodeInfo)this.treeView1.SelectedNode.Tag;
    }

    private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      this.menuTree.treeView_AfterSelect(sender, e);
    }

    // no reffered
    private void ShowNodeInfo(TreeNode treeNode)
    {
      if (treeNode != null && treeNode.Tag.GetType().Name == "NodeInfo" && treeNode.Tag != null)
      {
        NodeInfo selectedObject = new NodeInfo();
        selectedObject = (NodeInfo)treeNode.Tag;
        this.propertyGrid1.SelectedObject = selectedObject;
      }
    }

    private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TreeNode currentNode = treeView1.GetNodeAt(e.Location) as TreeNode;
        treeView1.SelectedNode = currentNode;
        if (currentNode.Tag == null) return;
        if (currentNode.Tag.GetType().Name == "NodeInfo")
        {
          try
          {
            this.contextMenuStrip1.Show(treeView1, e.Location);
          }
          catch (Exception Exception)
          {
            MessageBox.Show(Exception.Message.ToString());
          }
        }
        else if (currentNode is AntTreeNode)
        {
          XmlNode xmlNode = currentNode.Tag as XmlNode;
          try
          {
            if (currentNode.Parent != null)
            {
              this.buildFileMenu.Items[3].Enabled = false;
            }
            if (xmlNode.Name == "project" || xmlNode.Name == "package")
              this.buildFileMenu.Show(treeView1, e.Location);
            else
              this.targetMenu.Show(treeView1, e.Location);
          }
          catch (Exception Exception)
          {
            MessageBox.Show(Exception.Message.ToString(), "XmlTreePanel:treeView_NodeMouseClick:353");
          }
        }
      }
    }

    // TODO: 将来 PluginUIのMethodを呼び出す
    public void MenuRunClick(object sender, EventArgs e)
    {
      AntTreeNode node = treeView1.SelectedNode as AntTreeNode;
      if (node == null)
        return;
      Globals.AntPanel.RunTarget(node.File, node.Target);
    }

    // TODO: 将来 PluginUIのMethodを呼び出す
    public void MenuEditClick(object sender, EventArgs e)
    {
      Globals.AntPanel.MenuEditClick(sender, e);
      /*
      if (treeView1.SelectedNode.GetType().Name == "AntTreeNode")
      {
        AntTreeNode node = treeView1.SelectedNode as AntTreeNode;
        Globals.
        
        PluginBase.MainForm.OpenEditableDocument(node.File, false);
        //ScintillaControl sci = Globals.SciControl;
        ScintillaControl sci = PluginBase.MainForm.CurrentDocument.SciControl;
        
        String text = sci.Text;
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
      */
    }

    // TODO: 将来 PluginUIのMethodを呼び出す
    public void MenuRemoveClick(object sender, EventArgs e)
    {
      if (treeView1.SelectedNode.GetType().Name == "AntTreeNode")
      {
        Globals.AntPanel.RemoveBuildFile(
          (treeView1.SelectedNode as AntTreeNode).File);
      }
    }

    // TODO: 将来 PluginUIのMethodを呼び出す
    public void ShowOuterXmlClick(object sender, EventArgs e)
    {
      //AntTreeNode node = treeView.SelectedNode as AntTreeNode;
      AntTreeNode node = this.treeView1.SelectedNode as AntTreeNode;
      XmlNode tag = node.Tag as XmlNode;
      MessageBox.Show(tag.OuterXml.Replace("\t", "  ").Replace("	", "  "), "OuterXML : " + node.Target);
    }

    #endregion

    #region ToolButton Click Handler
    private int toggleIndex = 1;
    private void 表示ToolStripButton_Click(object sender, EventArgs e)
    {
      int num = this.toggleIndex % 3;
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

    private void sakuraToolStripButton_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentTreeMenuFilepath))
      {
        Process.Start(@"C:\Program Files (x86)\sakura\sakura.exe", this.currentTreeMenuFilepath);
      }
    }
    #endregion







  }
}
