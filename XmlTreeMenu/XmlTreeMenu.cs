using AntPlugin.CommonLibrary;
using AntPlugin.XmlTreeMenu.Managers;
using AntPlugin.XMLTreeMenu.Controls;
using AntPlugin.XMLTreeMenu.Dialogs;
using MDIForm;
using PluginCore;
using PluginCore.Controls;
using PluginCore.FRService;
using PluginCore.Helpers;
using PluginCore.Managers;
using PluginCore.Utilities;
using ScintillaNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace AntPlugin.XmlTreeMenu
{
  public partial class XmlMenuTree : UserControl
  {
    #region Variables
    public TreeView treeView = new TreeView();
    public ImageList imageList;
    public PluginMain pluginMain;
    public PluginUI pluginUI;
    public RunProcessDialog runProcessDialog;
    public OpenFileDialog openFileDialog;
    public String currentTreeMenuFilepath = String.Empty;

    private ContextMenuStrip buildFileMenu;
    private ContextMenuStrip targetMenu;
    #endregion

    #region Constructor
    public XmlMenuTree()
    {
      InitializeComponent();
      CreateMenus();
      InitializeXmlMenuTree();
    }

    /*
    public XmlMenuTree(TreeView tv, ImageList imgList)
    {
      this.treeView = tv;
      this.imageList = imgList;
      InitializeComponent();
      InitializeXmlMenuTree();
    }
 
    public XmlMenuTree(PluginMain pm, TreeView tv, ImageList imgList)
    {
      this.pluginMain = pm;
      this.treeView = tv;
      this.imageList = imgList;
      InitializeComponent();
      InitializeXmlMenuTree();
    }
 
    public XmlMenuTree(PluginMain pm, PluginUI ui, TreeView tv, ImageList imgList)
    {
      this.pluginMain = pm;
      this.pluginUI = ui;
      this.treeView = tv;
      this.imageList = imgList;
      InitializeComponent();
      InitializeXmlMenuTree();
    }
    */

    public XmlMenuTree(PluginUI ui)
    {
      this.pluginMain = ui.pluginMain;
      this.pluginUI = ui;
      this.treeView = ui.treeView;
      this.imageList = ui.imageList;
      InitializeComponent();
      CreateMenus();
      InitializeXmlMenuTree();
    }
    #endregion

    #region Initialization
    private void InitializeXmlMenuTree()
    {
      ActionManager.menuTree = this;
      TreeViewManager.menuTree = this;
      //this.treeView.ItemDrag += new ItemDragEventHandler(this.treeView_ItemDrag);
      //this.treeView.DragOver += new DragEventHandler(this.treeView_DragOver);
      //this.treeView.DragDrop += new DragEventHandler(this.treeView_DragDrop);
      //this.treeView.KeyUp += new KeyEventHandler(this.treeView1_KeyUp);
      //this.treeView.ItemDrag += new ItemDragEventHandler(this.treeView_ItemDrag);
      //this.treeView.MouseDown += new MouseEventHandler(this.treeView_MouseDown);
      //this.treeView.BeforeLabelEdit += new NodeLabelEditEventHandler(this.treeView_BeforeLabelEdit);
      //this.treeView.AfterLabelEdit += new NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
      this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
      //this.treeView.AfterExpand += new TreeViewEventHandler(this.treeView_AfterExpand);
      //this.treeView.AfterCollapse += new TreeViewEventHandler(this.treeView_AfterCollapse);
      //this.treeView.Click += new TreeViewEventHandler(this.treeView_Click);
      this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
      this.treeView.DoubleClick += new EventHandler(this.treeView_DoubleClick);
    }
    #endregion

    #region TreeView Ivent Handler

    private void CreateMenus()
    {
      buildFileMenu = new ContextMenuStrip();
      buildFileMenu.Items.Add("Run default target", this.pluginUI.runButton.Image, this.pluginUI.MenuRunClick);
      buildFileMenu.Items.Add("Edit file", null, this.pluginUI.MenuEditClick);
      buildFileMenu.Items.Add(new ToolStripSeparator());
      buildFileMenu.Items.Add("Remove",
      PluginBase.MainForm.FindImage("153"), this.pluginUI.MenuRemoveClick);
      targetMenu = new ContextMenuStrip();
      targetMenu.Items.Add("Run target", this.pluginUI.runButton.Image, this.pluginUI.MenuRunClick);
      targetMenu.Items.Add("Show in Editor", null, this.pluginUI.MenuEditClick);
      // 旧版 AntPlugin (session)からimport)
      targetMenu.Items.Add("Show OuterXml", null, this.pluginUI.ShowOuterXmlClick);
    }

    private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TreeNode currentNode = treeView.GetNodeAt(e.Location) as TreeNode;
        treeView.SelectedNode = currentNode;
        if (currentNode.Tag == null) return;
        if (currentNode.Tag.GetType().Name == "NodeInfo")
        {
          try
          {
            this.contextMenuStrip1.Show(treeView, e.Location);
          }
          catch (Exception Exception)
          {
            MessageBox.Show(Exception.Message.ToString(), "TreeMenu:treeView_NodeMouseClick:125");
          }
        }
      }
    }

    public void treeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      TreeView tv = (TreeView)sender;
      TreeNode selectedNode = tv.SelectedNode;
      //this.ShowNodeInfo(selectedNode);
      this.ShowNodeInfo(selectedNode,tv);
    }

    private void ShowNodeInfo(TreeNode treeNode)
    {
      if (treeNode != null && treeNode.Tag.GetType().Name == "NodeInfo" && treeNode.Tag != null)
      {
        //MessageBox.Show(treeNode.GetType().FullName);
        NodeInfo selectedObject = new NodeInfo();
        selectedObject = (NodeInfo)treeNode.Tag;
        this.pluginUI.propertyGrid1.SelectedObject = selectedObject;
      }
    }

    private void ShowNodeInfo(TreeNode treeNode,TreeView treeView)
    {
      MDIForm.ParentFormClass mainForm;
      if (treeNode != null && treeNode.Tag != null)
      {
        if(treeNode.Tag.GetType().Name == "NodeInfo")
        {
          NodeInfo selectedObject = new NodeInfo();
          selectedObject = (NodeInfo)treeNode.Tag;
          mainForm = treeView.Tag as MDIForm.ParentFormClass;
          mainForm.propertyGrid1.SelectedObject = selectedObject;
        }
        else if(treeNode.Tag is TaskInfo)
        {
          TaskInfo selectedObject = new TaskInfo();
          selectedObject = (TaskInfo)treeNode.Tag;
          mainForm = treeView.Tag as MDIForm.ParentFormClass;
          mainForm.propertyGrid1.SelectedObject = selectedObject;
        }

        else if (treeNode.Tag is XmlElement)
        {
          mainForm = treeView.Tag as MDIForm.ParentFormClass;
          mainForm.propertyGrid1.SelectedObject = (XmlElement)treeNode.Tag;
        }

        else if (treeNode.Tag is CSParser.Model.MemberModel)
        {
          mainForm = treeView.Tag as MDIForm.ParentFormClass;
          mainForm.propertyGrid1.SelectedObject = (CSParser.Model.MemberModel)treeNode.Tag;
        }

        else if (treeNode.Tag is String)
        {
          String path = treeNode.Tag as String;
          if (File.Exists(path))
          {
            mainForm = treeView.Tag as MDIForm.ParentFormClass;
            mainForm.propertyGrid1.SelectedObject = new FileInfo(path);
          }
          else if (Directory.Exists(path))
          {
            mainForm = treeView.Tag as MDIForm.ParentFormClass;
            mainForm.propertyGrid1.SelectedObject = new DirectoryInfo(path);
          }
          else if (Lib.IsWebSite(path))
          {
            NodeInfo ni = new NodeInfo();
            ni.Path = path;
            mainForm = treeView.Tag as MDIForm.ParentFormClass;
            mainForm.propertyGrid1.SelectedObject = ni;
          }
          else
          {
            NodeInfo ni2 = new NodeInfo();
            ni2.Command = path;
            mainForm = treeView.Tag as MDIForm.ParentFormClass;
            mainForm.propertyGrid1.SelectedObject = ni2;
          }
        }
        else
        {
          mainForm = treeView.Tag as MDIForm.ParentFormClass;
          mainForm.propertyGrid1.SelectedObject = null;
        }
      }
    }

    private void treeView_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        Point mousePosition = Control.MousePosition;
        Point point = this.treeView.PointToClient(new Point(mousePosition.X, mousePosition.Y));
        TreeNode nodeAt = this.treeView.GetNodeAt(point.X, point.Y);
        this.treeView.SelectedNode = nodeAt;

        if (GetTagType(this.treeView.SelectedNode) == "record" || GetTagType(this.treeView.SelectedNode) == null)
        {
          this.新規項目MenuItem.Visible = false;
          this.新規フォルダMenuItem.Visible = false;
          return;
        }
        this.新規項目MenuItem.Visible = true;
        this.新規フォルダMenuItem.Visible = true;
      }
    }

    private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
    {
      TreeView treeView = (TreeView)sender;
      treeView.SelectedNode = (TreeNode)e.Item;
      treeView.Focus();
      DragDropEffects dragDropEffects = treeView.DoDragDrop(e.Item, DragDropEffects.All);
      if ((dragDropEffects & DragDropEffects.Move) == DragDropEffects.Move)
      {
        treeView.Nodes.Remove((TreeNode)e.Item);
      }
    }

    private void treeView_DragOver(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(typeof(TreeNode)))
      {
        if ((e.KeyState & 8) == 8 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
        {
          e.Effect = DragDropEffects.Copy;
        }
        else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
        {
          e.Effect = DragDropEffects.Move;
        }
        else
        {
          e.Effect = DragDropEffects.None;
        }
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }
      if (e.Effect != DragDropEffects.None)
      {
        TreeView treeView = (TreeView)sender;
        TreeNode nodeAt = treeView.GetNodeAt(treeView.PointToClient(new Point(e.X, e.Y)));
        TreeNode treeNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
        if (nodeAt != null && nodeAt != treeNode && !IsChildNode(treeNode, nodeAt))
        {
          if (!nodeAt.IsSelected)
          {
            treeView.SelectedNode = nodeAt;
            return;
          }
        }
        else
        {
          e.Effect = DragDropEffects.None;
        }
      }
    }

    private void treeView_DragDrop(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(typeof(TreeNode)))
      {
        e.Effect = DragDropEffects.None;
        return;
      }
      TreeView treeView = (TreeView)sender;
      TreeNode treeNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
      TreeNode nodeAt = treeView.GetNodeAt(treeView.PointToClient(new Point(e.X, e.Y)));
      if (nodeAt != null && nodeAt != treeNode && !IsChildNode(treeNode, nodeAt))
      {
        TreeNode treeNode2 = (TreeNode)treeNode.Clone();
        nodeAt.Nodes.Add(treeNode2);
        nodeAt.Expand();
        treeView.SelectedNode = treeNode2;
        return;
      }
      e.Effect = DragDropEffects.None;
    }

    private void treeView_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F2 && this.treeView.SelectedNode != null)
      {
        this.treeView.LabelEdit = true;
        this.treeView.SelectedNode.BeginEdit();
      }
    }

    private void treeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
      if (this.treeView.SelectedNode.Parent == null)
      {
        e.CancelEdit = true;
      }
    }

    private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
    {
      if (e.Action != TreeViewAction.Unknown)
      {
        TreeNode node = e.Node;
        NodeInfo nodeInfo = new NodeInfo();
        nodeInfo = (NodeInfo)node.Tag;
        nodeInfo.Expand = true;
        node.Tag = nodeInfo;
        this.ShowNodeInfo(node);
      }
    }

    private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
    {
      if (e.Action != TreeViewAction.Unknown)
      {
        TreeNode node = e.Node;
        NodeInfo nodeInfo = new NodeInfo();
        nodeInfo = (NodeInfo)node.Tag;
        nodeInfo.Expand = false;
        node.Tag = nodeInfo;
        this.ShowNodeInfo(node);
      }
    }

    private static bool IsChildNode(TreeNode parent, TreeNode child)
    {
      return child.Parent == parent || (child.Parent != null && IsChildNode(parent, child.Parent));
    }

    private static string GetTagType(TreeNode node)
    {
      if (node != null)
      {
        NodeInfo nodeInfo = new NodeInfo();
        nodeInfo = (NodeInfo)node.Tag;
        return nodeInfo.Type;
      }
      return null;
    }

    private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
      if (e.Label != null)
      {
        if (e.Label.Length > 0)
        {
          if (e.Label.IndexOfAny(new char[]
          {
            '@',
            ',',
            '!'
          }) == -1)
          {
            e.Node.EndEdit(false);
          }
          else
          {
            e.CancelEdit = true;
            MessageBox.Show("ラベルには以下の文字は使用できません\n'@', ',', '!'","TreeMenu:treeView_AfterLabelEdit:344");
            e.Node.BeginEdit();
          }
        }
        else
        {
          e.CancelEdit = true;
          MessageBox.Show("ラベル名を入力してください");
          e.Node.BeginEdit();
        }
        this.treeView.LabelEdit = false;
      }
    }

    #endregion

    public TreeNode getXmlTreeNode(String file, Boolean fullNode = false)
    {
      XmlNode xmlNode = null;
      XmlDocument xmldoc = new XmlDocument();



      xmldoc.Load(file);



      xmlNode = xmldoc.DocumentElement;
      //MessageBox.Show(xmlNode.Name);
      if (xmlNode.Name == "project" && fullNode==false)
      {
        return pluginUI.GetBuildFileNode(file);
      }

      NodeInfo nodeInfo = this.SetNodeinfo(xmlNode, file);

      TreeNode treeNode = this.BuildTreeNode(nodeInfo, file);
      treeNode.Name = nodeInfo.Title;
      this.RecursiveBuildToTreeNode(xmlNode, treeNode, fullNode);

      if (nodeInfo.Expand)
      {
        treeNode.Expand();
      }
       return treeNode;
    }

    /// <summary>
    /// 新規 NodeInfo ni_arg の継承を可能にする Time-stamp: 2019-10-23 10:12:12 kahata
    /// </summary>
    /// <param name="ni_arg"></param>
    /// <param name="fullNode"></param>
    /// <returns></returns>
    public TreeNode getXmlTreeNode(NodeInfo ni_arg, Boolean fullNode = false)
    {
      XmlNode xmlNode = null;
      XmlDocument xmldoc = new XmlDocument();
      String file = ni_arg.Path;

      String xmlStr = TreeViewManager.File_ReadToEnd(file);
      //// Include処理
      //xmlStr = ProcessXmlString(xmlStr, ni);
      xmlStr = xmlStr.Replace("$(IncludePath)", file);
      xmlStr = xmlStr.Replace("$(IncludeFileName)", Path.GetFileName(file));
      xmlStr = xmlStr.Replace("$(IncludeDir)", Path.GetDirectoryName(file));
      //MessageBox.Show(xmlStr);
      if (ni_arg != null)
      {
        if (!String.IsNullOrEmpty(ni_arg.PathBase))
        {
          if (Directory.Exists(ni_arg.PathBase))
          {
            //MessageBox.Show()
            xmlStr = xmlStr.Replace("$(PathBase)", ni_arg.PathBase);
            //xmlStr = xmlStr.Replace("$(TargetDir)", ni_arg.PathBase);
            xmlStr = xmlStr.Replace("$(Target)", Path.GetFileName(ni_arg.PathBase));
          }
          else if (File.Exists(ni_arg.PathBase))
          {
            xmlStr = xmlStr.Replace("$(PathBase)", Path.GetDirectoryName(ni_arg.PathBase));
            xmlStr = xmlStr.Replace("$(Target)",Path.GetFileName(Path.GetDirectoryName(ni_arg.PathBase)));
          }
        }
      }

      // <import file="aaa"/> の処理 Time-stamp: <2019-10-20 20:29:23 kahata>
      foreach (KeyValuePair<string, string> item in TreeViewManager.GetImportFileFromXmlString(xmlStr))
      {
        if (File.Exists(item.Value))
        {
          string content = TreeViewManager.File_ReadToEnd(item.Value);
          if (Path.GetExtension(item.Value).ToLower() == ".xml")
          {
            XmlNode tmpXmlNode = null;
            XmlDocument tmpXmldoc = new XmlDocument();
            tmpXmldoc.PreserveWhitespace = true;
            tmpXmldoc.LoadXml(content);
            tmpXmlNode = xmldoc.DocumentElement;
            content = xmlNode.InnerXml;
          }
          xmlStr = xmlStr.Replace(item.Key, content);
        }
        //  Console.WriteLine("[{0}:{1}]", item.Key, item.Value);
      }

      // <property name="aaa" value="bbb"/> → ${aaa} の bbb置換 Time-stamp: <2019-10-20 20:17:04 kahata>
      //MessageBox.Show(xmlStr);
      foreach (KeyValuePair<string, string> item in TreeViewManager.GetPropertiesFromXmlString(xmlStr))
      {
        //MessageBox.Show(item.Key + " " + item.Value);
        if(!String.IsNullOrEmpty(item.Value)) xmlStr = xmlStr.Replace("$(" + item.Key + ")", item.Value);
      }
      //MessageBox.Show(xmlStr);
      // Hack   追加 Time-stamp: <2019-10-22 11:11:18 kahata>
      // 継承したnodeInfo からの$(プロパティ)をプロパティでハード置換する
      if (ni_arg != null)
      {
        foreach (KeyValuePair<string, string> item in TreeViewManager.GetNodeInfoProperties(ni_arg))
        {
          if (!String.IsNullOrEmpty(item.Value)) xmlStr = xmlStr.Replace("$(" + item.Key + ")", item.Value);
        }
        if (!String.IsNullOrEmpty(ni_arg.PathBase))// && Directory.Exists(nodeInfo.PathBase))
        {
          //MessageBox.Show(ni_arg.PathBase);
          xmlStr = xmlStr.Replace("$(PathBase)", ni_arg.PathBase);
        }
      }
      //MessageBox.Show(xmlStr);
      //xmldoc.Load(file);
      xmldoc.LoadXml(xmlStr);
      xmlNode = xmldoc.DocumentElement;
      //MessageBox.Show(xmlNode.Name);
      if (xmlNode.Name == "project" && fullNode == false)
      {
        return pluginUI.GetBuildFileNode(file);
      }

      NodeInfo nodeInfo = this.SetNodeinfo(xmlNode, file);

      TreeNode treeNode = this.BuildTreeNode(nodeInfo, file);
      treeNode.Name = nodeInfo.Title;
      this.RecursiveBuildToTreeNode(xmlNode, treeNode, fullNode);

      if (nodeInfo.Expand)
      {
        treeNode.Expand();
      }
      return treeNode;
    }

    /// <summary>
    /// 追加 Time-stamp: 2019-05-18 07:32:22 kahata
    /// 変更 ni_arg追加 (NodeInfoを継承可能にする Time-stamp: 2019-10-22 11:52:47 kahata
    /// </summary>
    /// <param name="xmlStr"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    //public TreeNode getXmlTreeNodeFromString(String xml, String file,NodeInfo ni_arg=null)
    public TreeNode getXmlTreeNodeFromString(String xmlStr, String file, NodeInfo ni_arg = null)
    {
      XmlNode xmlNode = null;
      XmlDocument xmldoc = new XmlDocument();

      xmlStr = xmlStr.Replace("$(IncludePath)", file);
      xmlStr = xmlStr.Replace("$(IncludeFileName)", Path.GetFileName(file));
      xmlStr = xmlStr.Replace("$(IncludeDir)", Path.GetDirectoryName(file));

      if (ni_arg != null)
      {
        if (!String.IsNullOrEmpty(ni_arg.PathBase))
        {
          if (Directory.Exists(ni_arg.PathBase))
          {
            //MessageBox.Show(Path.GetFileName(ni_arg.PathBase));
            xmlStr = xmlStr.Replace("$(PathBase)", ni_arg.PathBase);
            xmlStr = xmlStr.Replace("$(Target)", Path.GetFileName(ni_arg.PathBase));
          }
          else if (File.Exists(ni_arg.PathBase))
          {
            xmlStr = xmlStr.Replace("$(PathBase)", Path.GetDirectoryName(ni_arg.PathBase));
            xmlStr = xmlStr.Replace("$(Target)", Path.GetFileName(Path.GetDirectoryName(ni_arg.PathBase)));
          }
        }
      }

      // <import file="aaa"/> の処理 Time-stamp: <2019-10-20 20:29:23 kahata>
      foreach (KeyValuePair<string, string> item in TreeViewManager.GetImportFileFromXmlString(xmlStr))
      {
        if (File.Exists(item.Value))
        {
          string content = TreeViewManager.File_ReadToEnd(item.Value);
          if (Path.GetExtension(item.Value).ToLower() == ".xml")
          {
            XmlNode tmpXmlNode = null;
            XmlDocument tmpXmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml(content);
            xmlNode = tmpXmldoc.DocumentElement;
            content = tmpXmlNode.InnerXml;
          }
          xmlStr = xmlStr.Replace(item.Key, content);
        }
        //  Console.WriteLine("[{0}:{1}]", item.Key, item.Value);
      }

      // <property name="aaa" value="bbb"/> → ${aaa} の bbb置換 Time-stamp: <2019-10-20 20:17:04 kahata>
      foreach (KeyValuePair<string, string> item in TreeViewManager.GetPropertiesFromXmlString(xmlStr))
      {
        xmlStr = xmlStr.Replace("$(" + item.Key + ")", item.Value);
      }

      // Hack   追加 Time-stamp: <2019-10-22 11:11:18 kahata>
      if (ni_arg != null)
      {
        foreach (KeyValuePair<string, string> item in TreeViewManager.GetNodeInfoProperties(ni_arg))
        {
          if (!String.IsNullOrEmpty(item.Value)) xmlStr = xmlStr.Replace("$(" + item.Key + ")", item.Value);
        }
        if (!String.IsNullOrEmpty(ni_arg.PathBase))// && Directory.Exists(nodeInfo.PathBase))
        {
          //MessageBox.Show(ni_arg.PathBase);
          xmlStr = xmlStr.Replace("$(PathBase)", ni_arg.PathBase);
        }
      }

      xmldoc.LoadXml(xmlStr);
      xmlNode = xmldoc.DocumentElement;

      //変更 Time-stamp: <2019-10-22 12:52:27 kahata>
      //NodeInfo nodeInfo = this.SetNodeinfo(xmlNode, file);
      NodeInfo nodeInfo = this.SetNodeinfo(xmlNode, file, ni_arg);
      TreeNode treeNode = this.BuildTreeNode(nodeInfo, file);
      this.RecursiveBuildToTreeNode(xmlNode, treeNode, false);
      treeNode.Tag = nodeInfo;
      treeNode.ToolTipText = file;
      return treeNode;
    }

    public TreeNode loadfile(String file)
    {
      TreeNode treeNode = null;
      //NodeInfo nodeInfo = this.SetNodeinfo(file);
      NodeInfo nodeInfo = TreeViewManager.SetNodeinfo(file);

      if (Lib.IsWebSite(file))
      {
        //「+」が半角スペースにデコードされるようにするには、次のようにする
        string urlDec = Uri.UnescapeDataString(file.Replace('+', ' '));
        NodeInfo ni = new NodeInfo();
        ni.Path = file;
        ni.Title = urlDec;
        ni.Type = "uri";
        TreeNode tn = new TreeNode(ni.Title);
        tn.Tag = ni;

        tn.ImageIndex = tn.SelectedImageIndex = 10;// this.getImageIndexFromNodeInfo(ni);

        return tn;
      }


      switch (Path.GetExtension(file).ToLower())
      {
        case ".xml":
        case ".asx":
        case ".wax":
          treeNode = this.getXmlTreeNode(file);
          break;
        case ".cs":
        case ".java":
          //pluginUI.LoadIn(file);
          break;
        case ".fdp":
        case ".wsf":
          treeNode = pluginUI.GetBuildFileNode(file);

          break;
        default:
          //treeNode = new TreeNode(Path.GetFileName(file), 10, 10);
          NodeInfo ni1 = new NodeInfo();
          ni1.Path = file;
          treeNode = new TreeNode(nodeInfo.Title, 10, 10);
          //treeNode.SelectedImageIndex = treeNode.ImageIndex = this.getImageIndexFromNodeInfo(ni1);
          treeNode.Tag = nodeInfo;
          treeNode.ToolTipText = file;
          break;
      }
      return treeNode;
    }

    public TreeNode loadfile(String file, Int32 imageIndex)
    {
      TreeNode treeNode = null;
      //NodeInfo nodeInfo = this.SetNodeinfo(file);
      NodeInfo nodeInfo = TreeViewManager.SetNodeinfo(file);

      if (Lib.IsWebSite(file))
      {
        //「+」が半角スペースにデコードされるようにするには、次のようにする
        string urlDec = Uri.UnescapeDataString(file.Replace('+', ' '));
        NodeInfo ni = new NodeInfo();
        ni.Path = file;
        ni.Title = urlDec;
        ni.Type = "uri";
        TreeNode tn = new TreeNode(ni.Title);
        tn.Tag = ni;

        tn.ImageIndex = tn.SelectedImageIndex = imageIndex;
        return tn;
      }


      switch (Path.GetExtension(file).ToLower())
      {
        case ".xml":
        case ".asx":
        case ".wax":
          treeNode = this.getXmlTreeNode(file);
          break;
        case ".cs":
        case ".java":
          //pluginUI.LoadIn(file);
          break;
        case ".fdp":
        case ".wsf":
          treeNode = pluginUI.GetBuildFileNode(file);

          break;
        default:
          //treeNode = new TreeNode(Path.GetFileName(file), 10, 10);
          NodeInfo ni1 = new NodeInfo();
          ni1.Path = file;
          treeNode = new TreeNode(nodeInfo.Title, 10, 10);
          //treeNode.SelectedImageIndex = treeNode.ImageIndex = this.getImageIndexFromNodeInfo(ni1);
          treeNode.Tag = nodeInfo;
          treeNode.ToolTipText = file;
          break;
      }
      return treeNode;
    }

    public TreeNode loadfile(NodeInfo nodeInfo, Int32 imageIndex)
    {
      TreeNode treeNode = null;

      //if (!String.IsNullOrEmpty(nodeInfo.Option)) MessageBox.Show(nodeInfo.Option);
 
      if (Lib.IsWebSite(nodeInfo.Path))
      {
        //「+」が半角スペースにデコードされるようにするには、次のようにする
        string urlDec = Uri.UnescapeDataString(nodeInfo.Path.Replace('+', ' '));
        NodeInfo ni = new NodeInfo();
        ni.Path = nodeInfo.Path;
        if (!String.IsNullOrEmpty(nodeInfo.Title)) ni.Title = nodeInfo.Title;
        else ni.Title = urlDec;
        ni.Type = "uri";
        TreeNode tn = new TreeNode(ni.Title, imageIndex, imageIndex);
        tn.Name = ni.Title;
        tn.Tag = ni;
        return tn;
      }

      else if(Directory.Exists(nodeInfo.Path))
      {
        //treeView.Nodes.Clear();
        DirectoryInfo rootDirectoryInfo = new DirectoryInfo(nodeInfo.Path);
        TreeNode tn = this.RecursiveCreateDirectoryNode(rootDirectoryInfo);
        /*
        if (!String.IsNullOrEmpty(nodeInfo.Title))
        {
          //MessageBox.Show(nodeInfo.Title);
          tn.Name = nodeInfo.Title;
          tn.Text = nodeInfo.Title;
        }
        if (!String.IsNullOrEmpty(nodeInfo.icon)) tn.ImageIndex = GetIconImageIndexFromIconPath(nodeInfo.icon);
        if (!String.IsNullOrEmpty(nodeInfo.Tooltip)) tn.ToolTipText = nodeInfo.Tooltip;
        //tn.Tag = rootDirectoryInfo;
        */
        tn.Name = nodeInfo.Title;
        tn.Tag = nodeInfo;
        return tn;
      }

      switch (Path.GetExtension(nodeInfo.Path.ToLower()))
      {
        case ".xml":
        case ".asx":
        case ".wax":
          if (nodeInfo.Option.ToLower() == "fullnode")
          {
            //変更 Time-stamp: <2019-10-23 11:57:11 kahata>
            //treeNode = this.getXmlTreeNode(nodeInfo.Path, true);
            treeNode = this.getXmlTreeNode(nodeInfo, true);
            treeNode.Name = nodeInfo.Title;
          }
          else
          {
            //変更 Time-stamp: <2019-10-23 11:57:11 kahata>
            //treeNode = this.getXmlTreeNode(nodeInfo.Path);
            treeNode = this.getXmlTreeNode(nodeInfo);
          }
          if (String.IsNullOrEmpty(treeNode.ToolTipText))treeNode.ToolTipText = nodeInfo.Path;
          break;
        case ".cs":
        case ".java":
          if (!PluginUI.csOutlineTreePath.Contains(nodeInfo.Path))
          {
            PluginUI.csOutlineTreePath.Add(nodeInfo.Path);
            this.imageList.Tag = "Ant";
            treeNode = this.pluginUI.buildTree.CsOutlineTreeNode(nodeInfo.Path, this.imageList, this.pluginUI.MemberId);
            treeNode.Tag = nodeInfo;
          }
          break;
        case ".fdp":
        case ".wsf":
          // 変更 nodeInfo 追加 Time-stamp: <2019-10-22 12:02:46 kahata>
          //treeNode = pluginUI.GetBuildFileNode(nodeInfo.Path);
          treeNode = pluginUI.GetBuildFileNode(nodeInfo.Path, nodeInfo);
          break;
        case ".gradle":
          treeNode = pluginUI.gradleTree.GetGradleOutlineTreeNode(nodeInfo.Path);
          break;
        default:
          NodeInfo ni1 = new NodeInfo();
          ni1.Path = nodeInfo.Path;
          nodeInfo.Title = Path.GetFileName(nodeInfo.Path);
          //treeNode = new TreeNode(nodeInfo.Title, 10, 10);
          treeNode = new TreeNode(nodeInfo.Title, imageIndex, imageIndex);
          treeNode.Tag = nodeInfo;
          treeNode.ToolTipText = nodeInfo.Path;
          break;
      }
      // おかしい      
      //if (!String.IsNullOrEmpty(nodeInfo.Title)) treeNode.Text = nodeInfo.Title;
      //if (!String.IsNullOrEmpty(nodeInfo.icon)) treeNode.ImageIndex = GetIconImageIndexFromIconPath(nodeInfo.icon);
      //if (!String.IsNullOrEmpty(nodeInfo.Tooltip)) treeNode.ToolTipText = nodeInfo.Tooltip;
      treeNode.Name = nodeInfo.Title;
      return treeNode;
    }

    /// <summary>
    /// 追加 未完 Time-stamp: <2019-05-18 07:30:56 kahata>
    /// </summary>
    /// <param name="xmlTreeMenuPath"></param>
    public void SaveFile(String xmlTreeMenuPath)
    {
      String projectDir = Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath);
      // 出力用 XmlDocument インスタンスの初期化
      exportXmlDocument = new XmlDocument();
      // XMLヘッダ出力(バージョン 1.0, エンコード UTF-8)
      XmlDeclaration xmlDeclaration = exportXmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
      exportXmlDocument.AppendChild(xmlDeclaration);

      // ヘッダの後に1行改行(好みの問題です)
      XmlWhitespace xmlWhitespace = exportXmlDocument.CreateWhitespace(System.Environment.NewLine);
      exportXmlDocument.AppendChild(xmlWhitespace);

      // ルートノードの情報を初期化
      NodeInfo ni = new NodeInfo();

      TreeNode baseNode = new TreeNode();
      // BeseNodeの探索
      foreach (TreeNode tn in this.treeView.Nodes)
      {
        if (TreeViewManager.IsChildNode(tn, this.treeView.SelectedNode)) baseNode = tn;
      }
      if (baseNode.Tag is NodeInfo) ni = (NodeInfo)baseNode.Tag;
      else
      {
        ni.Type = "root";
        ni.Title = baseNode.Text;
        ni.expand = true;
        baseNode.Tag = ni;
      }
      XmlElement xe = exportXmlDocument.CreateElement(ni.Type);
      // 「title」属性書き出し
      if (ni.Tooltip != String.Empty) xe.SetAttribute("tooltip", ni.Tooltip);
      if (ni.ForeColor != String.Empty) xe.SetAttribute("forecolor", ni.ForeColor);
      if (ni.NodeFont != String.Empty) xe.SetAttribute("nodefont", ni.NodeFont);
      if (ni.NodeChecked != String.Empty) xe.SetAttribute("nodechecked", ni.NodeChecked);
      if (ni.Title != String.Empty) xe.SetAttribute("title", ni.Title);
      if (ni.PathBase != String.Empty) xe.SetAttribute("base", ni.PathBase);
      if (ni.Action != String.Empty) xe.SetAttribute("action", ni.Action);
      if (ni.Command != String.Empty) xe.SetAttribute("command", ni.Command);
      if (ni.Path != String.Empty)
      {
        xe.SetAttribute("path", ni.Path);
        if (File.Exists(Path.Combine(projectDir, ni.Path)))
        {
          xe.SetAttribute("path", Path.Combine(projectDir, ni.Path));
        }
      }
      if (ni.Icon != String.Empty) xe.SetAttribute("icon", ni.Icon);
      if (ni.Args != String.Empty) xe.SetAttribute("args", ni.Args);
      if (ni.Option != String.Empty) xe.SetAttribute("option", ni.Option);
      // 「expand」属性書き出し
      // 「ni.Expand」プロパティを参照しても良いが、確実性は低い
      if (baseNode.IsExpanded)
      {
        xe.SetAttribute("expand", "true");
      }
      // ルートノードをXmlDocumentに追加
      exportXmlDocument.AppendChild(xe);
      // 再帰的にツリーノードを読み込み、XmlDocument構築
      RecursiveBuildToXml(baseNode, xe);
      //MessageBox.Show(exportXmlDocument.OuterXml);
      // ファイルに出力
      exportXmlDocument.Save(xmlTreeMenuPath);
      //this.filepath = path;
    }

    /// <summary>
    /// Option #1: Recursive approach:
    /// </summary>
    /// https://stackoverflow.com/questions/6239544/populate-treeview-with-file-system-directory-structure
    /// <param name="directoryInfo"></param>
    /// <returns></returns>
    public TreeNode RecursiveCreateDirectoryNode(DirectoryInfo directoryInfo)
    {
      NodeInfo ni = new NodeInfo();
      ni.Path = directoryInfo.FullName;
      ni.Type = "folder";
      if (String.IsNullOrEmpty(ni.Title)) ni.Title = Path.GetFileName(directoryInfo.Name);
      // FIXME
      int imageIndex = this.GetIconImageIndex(@"C:\windows"); //this.getImageIndexFromNodeInfo_safe(ni);
      //MessageBox.Show(imageIndex.ToString());
      TreeNode directoryNode = new TreeNode(ni.Title, imageIndex, imageIndex);
      directoryNode.Name = ni.Title;
      // ノードに属性を設定
      directoryNode.Tag = ni;
      directoryNode.ToolTipText = ni.Path;

      foreach (var directory in directoryInfo.GetDirectories())
        directoryNode.Nodes.Add(RecursiveCreateDirectoryNode(directory));
      foreach (var file in directoryInfo.GetFiles())
      {
        ni = new NodeInfo();
        ni.Type = "record";
        ni.Path = file.FullName;
        ni.Title = Path.GetFileName(file.Name);
        //MessageBox.Show(ni.Path);
        imageIndex = this.GetIconImageIndex(ni.Path); //this.getImageIndexFromNodeInfo_safe(ni);
        TreeNode fileNode = new TreeNode(ni.Title, imageIndex, imageIndex);
        fileNode.Name = ni.Title;
        fileNode.Tag = ni;
        fileNode.ToolTipText = ni.Path;

        //directoryNode.Nodes.Add(new TreeNode(file.Name));
        directoryNode.Nodes.Add(fileNode);
      }
      return directoryNode;
    }

    //追加変更 ni_arg Time-stamp: <2019-10-22 12:49:25 kahata>
    //public NodeInfo SetNodeinfo(XmlNode xmlNode, String path)
    public NodeInfo SetNodeinfo(XmlNode xmlNode, String path, NodeInfo ni_arg = null)
    {
      NodeInfo nodeInfo = new NodeInfo();
      nodeInfo.Type = ((XmlElement)xmlNode).Name;

      nodeInfo.Title = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("title"));
      if (nodeInfo.Title == String.Empty) nodeInfo.Title = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("name"));
      if (nodeInfo.Title == String.Empty) nodeInfo.Title = Path.GetFileName(path);
      if (xmlNode.Name == "ASX")
      {
        XmlNode titleNode = ((XmlElement)xmlNode).GetElementsByTagName("TITLE")[0];
        nodeInfo.Title = titleNode.InnerText;
      }
      if (xmlNode.Name == "link")
      {
        nodeInfo.Title = Path.GetFileName(path);
      }

      nodeInfo.PathBase = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("base"));
      nodeInfo.Action = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("action"));
      nodeInfo.Command = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("command"));
      //nodeInfo.Path = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("path"));
      //nodeInfo.Path = path;
      nodeInfo.Args = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("args"));
      nodeInfo.Option = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("option"));
      //nodeInfo.XmlNode = xmlNode;
      nodeInfo.Icon = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("icon"));

      // 追加 Time-stamp: <2019-10-22 13:12:51 kahata>
      if (ni_arg != null)
      {
        try
        {
          //if (!String.IsNullOrEmpty(ni_arg.Title)) nodeInfo.Title = ni_arg.Title;
          if (!String.IsNullOrEmpty(ni_arg.PathBase)) nodeInfo.pathbase = ni_arg.PathBase;
          if (!String.IsNullOrEmpty(ni_arg.Action)) nodeInfo.Action = ni_arg.Action;
          if (!String.IsNullOrEmpty(ni_arg.Command)) nodeInfo.Command = ni_arg.Command;
          //if (!String.IsNullOrEmpty(ni_arg.Path)) nodeInfo.Path = ni_arg.Path;
          if (!String.IsNullOrEmpty(ni_arg.Args)) nodeInfo.Args = ni_arg.Args;
          if (!String.IsNullOrEmpty(ni_arg.Option)) nodeInfo.Option = ni_arg.Option;
          if (!String.IsNullOrEmpty(ni_arg.Icon)) nodeInfo.Icon = ni_arg.Icon;
        }
        catch { }
      }
      nodeInfo.XmlNode = xmlNode;
      nodeInfo.Path = path;

      if (((XmlElement)xmlNode).GetAttribute("expand") == "true")
      {
        nodeInfo.Expand = true;
      }
      return nodeInfo;
    }
 
    public TreeNode BuildTreeNode(NodeInfo nodeInfo, String path)
    {
      this.currentTreeMenuFilepath = path;

      //Int32 imageIndex = getImageIndexFromNodeInfo(nodeInfo);
      Int32 imageIndex = getImageIndexFromNodeInfo_safe(nodeInfo);
      TreeNode treeNode;
      if (!String.IsNullOrEmpty(nodeInfo.Title))
      {
        treeNode = new TreeNode(this.ProcessVariable(nodeInfo.Title), imageIndex, imageIndex);
        treeNode.Name = this.ProcessVariable(nodeInfo.Title);
      }
      else
      {
        treeNode = new TreeNode(nodeInfo.xmlNode.Name, imageIndex, imageIndex);
        treeNode.Name = nodeInfo.xmlNode.Name;
      }
      treeNode.ToolTipText = path;
      treeNode.Tag = nodeInfo;
      return treeNode;
    }

    public string ProcessVariable(string strVar)
    {
      string arg = strVar;// string.Empty;
      try
      {
        arg = PluginBase.MainForm.ProcessArgString(strVar);
      }
      catch { arg = strVar; }
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
      }
      catch { }
      return arg;
    }

    /// <summary>
    /// Xml文書を再帰的に検査し、TreeNodeを構築
    /// </summary>
    /// https://msdn.microsoft.com/ja-jp/library/system.xml.xmlnode.name(v=vs.110).aspx
    /// 属性               属性の限定名。
    /// CDATA              #cdata-section
    /// コメント           #comment
    /// Document           #document
    /// DocumentFragment   #document-fragment
    /// DocumentType ドキュメントの種類の名前。
    /// Text               #text
    /// Whitespace         #whitespace
    /// SignificantWhitespace     #significant-whitespace
    /// XmlDeclaration    #xml-declaration
    /// <param name="Parentnode"></param>
    /// <param name="ParenttreeNode"></param>
    private void RecursiveBuildToTreeNode(XmlNode Parentnode, TreeNode ParenttreeNode, Boolean fullNode = false)
    {
      foreach (XmlNode childXmlNode in Parentnode.ChildNodes)
      {
        NodeInfo ni = new NodeInfo();
        if (childXmlNode.Name != "#text" && childXmlNode.Name != "#comment" && childXmlNode.Name != "#cdata-section")    // テキストノードへの処理
        {
          //for test やはりおかしい
          //ni = TreeViewManager.SetNodeInfoFromXmlNode(childXmlNode, fullNode);
          ni = this.SetNodeInfoFromXmlNode(childXmlNode, fullNode);

          #region backup
          /* 
          // ツリーノードの追加 タグ名を取得
          switch (childXmlNode.Name)
          {
            case "folder":
              ni.Type = "folder";
              break;
            case "record":
              ni.Type = "record";
              break;
            // kahata FIX 2018-02-10
            case "toolbar":
            case "menubar":
            case "launch":
            case "settings":
            case "property":
              ni.Type = "null";
              //this.ProcessNode(childXmlNode);
              break;
            // kahata FIX 2018-02-14
            case "load":
            case "loadfile":
            case "include":
            case "includefile":
              ni.Type = "include";
              break;
            default:
              if (fullNode == true) ni.Type = childXmlNode.Name;// "node";
              else ni.Type = "null";
              break;
          }
          // 属性を取得
          ni.Title = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("title"));
          //if (!String.IsNullOrEmpty(ni.Title)) nodeName = ni.Title;
          //if (String.IsNullOrEmpty(nodeName)) nodeName = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("name"));
          //if (String.IsNullOrEmpty(nodeName)) nodeName = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("id"));
          //if (String.IsNullOrEmpty(nodeName)) nodeName = ((XmlElement)childXmlNode).Name;

          ni.PathBase = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("base"));
          ni.Action = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("action"));
          if (childXmlNode.Name == "target") ni.Action = "Ant";
          if (childXmlNode.Name == "job") ni.Action = "Wsf";

          ni.Command = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("command"));
          ni.Path = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("path"));

          ni.Icon = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("icon"));
          ni.Args = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("args"));
          ni.Tooltip = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("tooltip"));

          ni.BackColor = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("backcolor"));
          ni.ForeColor = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("forecolor"));
          ni.NodeFont = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("nodefont"));
          ni.NodeChecked = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("nodechecked"));
          ni.Option = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("option"));
          if (fullNode == true && childXmlNode.InnerText != String.Empty)
          {
            ni.InnerText = childXmlNode.InnerText;
          }
          // 「Expand」属性を取得
          if (((XmlElement)childXmlNode).GetAttribute("expand") == "true")
          {
            ni.Expand = true;
          }
          ni.XmlNode = childXmlNode;
          */
          #endregion

          if (childXmlNode.Name == "toolbar" || childXmlNode.Name == "menubar" 
            || childXmlNode.Name == "launch" || childXmlNode.Name == "launch"
            || childXmlNode.Name == "toolbar" || childXmlNode.Name == "settings"
            || childXmlNode.Name == "property")
          {
            this.ProcessNode(childXmlNode);
          }

          String nodeName = String.Empty;
          if (!String.IsNullOrEmpty(ni.Title)) nodeName = ni.Title;
          if (String.IsNullOrEmpty(nodeName)) nodeName = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("name"));
          if (String.IsNullOrEmpty(nodeName)) nodeName = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("id"));
          if (String.IsNullOrEmpty(nodeName)) nodeName = ((XmlElement)childXmlNode).Name;

          // TreeNodeを新規作成
          //TreeNode tn = new TreeNode(ni.Title);
          TreeNode tn = new TreeNode(nodeName);
          tn.Name = nodeName;
          if (fullNode == true) tn.ToolTipText = GetHeadTagFromXmlNode(childXmlNode);

          if (ni.Tooltip != string.Empty) tn.ToolTipText = ni.Tooltip;
          /*
          tn.BackColor;
          tn.Checked;
          tn.Expand;
          tn.ForeColor;
          tn.IsVisible;
          tn.NodeFont;
          tn.TreeView; //現在割り当てられている親TreeViewを取得
          tn.IsVisible;
          tn.FullPath;
          tn.ContextMenu;
          */
          if (ni.BackColor != string.Empty) tn.BackColor = Color.FromName(ni.BackColor);
          if (ni.ForeColor != string.Empty) tn.ForeColor = Color.FromName(ni.ForeColor);


          if (ni.NodeFont != string.Empty)
          {
            //this.Font = new Font("Meiryo UI", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 128);
            var cvt = new FontConverter();
            //string s = cvt.ConvertToString(this.Font);
            //MessageBox.Show(s);
            Font f = cvt.ConvertFromString(ni.NodeFont) as Font;
            tn.NodeFont = f;            
          }

          if (ni.NodeChecked != string.Empty)
          {
            if (ni.NodeChecked == "true") tn.Checked = true;
            else tn.Checked = false;
          }
            ///////////////////////////////////////////////////////////////////////////////////////////
            // icon設定
            // アイコン設定(このサンプルにはリソースファイルを入れてないので実は機能していませんが)
            tn.ImageIndex = tn.SelectedImageIndex = this.getImageIndexFromNodeInfo_safe(ni);
          // ノードに属性を設定
          tn.Tag = ni;
          // ノードを追加

          ////////////////////////////////////////////////////////////////////////
          if (ni.Type == "include")
          {
            Int32 imageIndex = this.getImageIndexFromNodeInfo_safe(ni);
            try
            {
              TreeNode inctn = loadfile(ni, imageIndex);
              if (!String.IsNullOrEmpty(ni.Title)) inctn.Text = ni.Title;
              if (!String.IsNullOrEmpty(ni.icon))
              {
                inctn.ImageIndex = GetIconImageIndexFromIconPath(ni.icon);
              }
              else inctn.ImageIndex = 38;
              if (!String.IsNullOrEmpty(ni.Tooltip)) inctn.ToolTipText = ni.Tooltip;
              if (ni.BackColor != string.Empty) inctn.BackColor = Color.FromName(ni.BackColor);
              if (ni.ForeColor != string.Empty) inctn.ForeColor = Color.FromName(ni.ForeColor);
              if (ni.NodeFont != string.Empty)
              {
                //this.Font = new Font("Meiryo UI", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 128);
                var cvt = new FontConverter();
                //string s = cvt.ConvertToString(this.Font);
                //MessageBox.Show(s);
                Font f = cvt.ConvertFromString(ni.NodeFont) as Font;
                inctn.NodeFont = f;
              }

              if (ni.NodeChecked != string.Empty)
              {
                if (ni.NodeChecked == "true") inctn.Checked = true;
                else inctn.Checked = false;
              }
              //if (!String.IsNullOrEmpty(ni.Tooltip)) inctn.ToolTipText = ni.Tooltip;
              ParenttreeNode.Nodes.Add(inctn);
            }
            catch (Exception exc)
            {
              String exMsg = exc.Message.ToString();
              // FIXME: 2018-03-25
              // Snippets のfiltering で二重にincludeしてエラーが発生すす問題 原因特定できず
              // XMLTreeMenu.dllが原因か? うるさいのでひとまず errMsgをコメントアウト
              // MessageBox.Show(exc.Message.ToString(),"TreeMenu:RecursiveBuildToTreeNode:902");
            }
          }
          else if (ni.Type != "null")
          {
            ParenttreeNode.Nodes.Add(tn);
          }
          // 再帰呼び出し
          RecursiveBuildToTreeNode(childXmlNode, tn, fullNode);
        }
      }
    }

    public NodeInfo SetNodeInfoFromXmlNode(XmlNode xmlNode, Boolean fullNode = false)
    {
      NodeInfo ni = new NodeInfo();
      // ツリーノードの追加 タグ名を取得
      switch (xmlNode.Name)
      {
        case "folder":
          ni.Type = "folder";
          break;
        case "record":
          ni.Type = "record";
          break;
        // kahata FIX 2018-02-10
        case "toolbar":
        case "menubar":
        case "launch":
        case "settings":
        case "property":
          ni.Type = "null";
          //this.ProcessNode(childXmlNode);
          break;
        // kahata FIX 2018-02-14
        case "load":
        case "loadfile":
        case "include":
        case "includefile":
          ni.Type = "include";
          break;
        default:
          if (fullNode == true) ni.Type = xmlNode.Name;// "node";
          else ni.Type = "null";
          break;
      }
      // 属性を取得
      ni.Title = ProcessVariable(((XmlElement)xmlNode).GetAttribute("title"));
      ni.PathBase = ProcessVariable(((XmlElement)xmlNode).GetAttribute("base"));
      ni.Action = ProcessVariable(((XmlElement)xmlNode).GetAttribute("action"));
      if (xmlNode.Name == "target") ni.Action = "Ant";
      if (xmlNode.Name == "job") ni.Action = "Wsf";

      ni.Command = ProcessVariable(((XmlElement)xmlNode).GetAttribute("command"));
      ni.Path = ProcessVariable(((XmlElement)xmlNode).GetAttribute("path"));

      ni.Icon = ProcessVariable(((XmlElement)xmlNode).GetAttribute("icon"));
      ni.Args = ProcessVariable(((XmlElement)xmlNode).GetAttribute("args"));
      ni.Tooltip = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("tooltip"));

      ni.BackColor = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("backcolor"));
      ni.ForeColor = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("forecolor"));
      ni.NodeFont = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("nodefont"));
      ni.NodeChecked = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("nodechecked"));
      ni.Option = ProcessVariable(((XmlElement)xmlNode).GetAttribute("option"));
      if (fullNode == true && xmlNode.InnerText != String.Empty)
      {
        ni.InnerText = xmlNode.InnerText;
      }
      // 「Expand」属性を取得
      if (((XmlElement)xmlNode).GetAttribute("expand") == "true")
      {
        ni.Expand = true;
      }
      ni.XmlNode = xmlNode;
      return ni;
    }

    /// <summary>
    /// XMLをツリーノードに変換する
    /// </summary>
    /// http://hiros-dot.net/CS2005/Control/TreeView/TreeView11.htm
    /// <param name="xmlParent">変化するXMLデータ</param>
    /// <param name="trvParent">変換されたツリーノード</param>
    public TreeNode MakeXmlTreeMode(System.Xml.XmlElement xmlParent, TreeNode trvParent)
    {
      if (xmlParent.HasChildNodes)
      {
        for (int i = 0; i < xmlParent.ChildNodes.Count; i++)
        {
          if (xmlParent.ChildNodes[i].GetType().ToString().IndexOf("XmlElement") >= 0)
          {
            System.Xml.XmlElement xmlChild = (System.Xml.XmlElement)xmlParent.ChildNodes[i];
            //Path 設定
            NodeInfo nodeInfo = this.SetNodeinfo(xmlChild,"");
            TreeNode trvChild = new TreeNode();
            trvChild.Tag = nodeInfo;
            trvChild.Text = xmlChild.Name;
            trvChild.ToolTipText = this.GetHeadTagFromXmlNode(xmlChild);
            
            // 子ノードがまだあるか？
            if (xmlChild.HasChildNodes)
              this.MakeXmlTreeMode(xmlChild, trvChild);
            trvParent.Nodes.Add(trvChild);
          }
          else if (xmlParent.ChildNodes[i].GetType().ToString().IndexOf("XmlText") >= 0)
          {
            TreeNode trvChild = new TreeNode();
            NodeInfo nodeInfo = this.SetNodeinfo(xmlParent, "");
            trvChild.Tag = nodeInfo;
            trvChild.Text = xmlParent.ChildNodes[i].InnerText;
            trvParent.Nodes.Add(trvChild);
          }
        }
      }
      //親ノードの設定
      //trvParent.Text = xmlParent.Name;
      return trvParent;
    }
 
    public String GetHeadTagFromXmlNode(XmlNode node)
    {
      string title = String.Empty;
      title = node.OuterXml;
      if (node.InnerXml != String.Empty) title = title.Replace(node.InnerXml, "");
      title = title.Replace("></", ">\n</");
      String[] tmp = title.Split('\n');
      title = tmp[0];
      if (node.InnerText != String.Empty) title = title + " : " + node.InnerText;
      return title;
    }

    private Int32 getImageIndexFromNodeInfo(NodeInfo ni)
    {
      Int32 imageIndex = 38;
      ///////////////////////////////////////////////////////////////////////////////////////////
      // icon設定
      // アイコン設定(このサンプルにはリソースファイルを入れてないので実は機能していませんが)
      if (ni.Icon != String.Empty)
      {
        imageIndex = GetIconImageIndexFromIconPath(ni.Icon);
      }
      else
      {
        if (ni.Type == "folder")
        {
          //tn.ImageIndex = GetIconImageIndex(@"F:\cpp");
          //tn.SelectedImageIndex = GetIconImageIndex(@"F:\cpp");
          imageIndex = GetIconImageIndex_old(@"C:\windows");
        }
        else
        {
          String path = "";
          if (ni.PathBase != String.Empty) path = Path.Combine(ni.PathBase, ni.Path);
          else path = ni.Path;

          if (Lib.IsWebSite(path))
          {
            //tn.ImageIndex = tn.SelectedImageIndex = this.GetIconImageIndexFromIconPath(path);
            imageIndex = this.GetIconImageIndexFromIconPath(path);
          }

          else if (File.Exists(path))
          {
            if (ni.Type == "root")
            {
              imageIndex = 38;
            }
            else if (ni.Action.ToLower() == "ant" || ni.Action.ToLower() == "wsf")
            {
              //tn.ImageIndex = tn.SelectedImageIndex = 14;
              imageIndex = 14;
            }
            
            /*
            // TODO:Gradleの実装
            else if (Path.GetExtension(ni.Path.ToLower()) == ".gradle"
            || Path.GetFileName(ni.Path.ToLower()) == "pom.xml")
            {
              tn.ImageIndex = tn.SelectedImageIndex = 14;

              if (((XmlElement)childXmlNode).GetAttribute("default") == "true")
              {
                tn.ForeColor = Color.DarkGreen;

                tn.NodeFont = new Font(
                  this.treeView.Font.Name,
                  this.treeView.Font.Size,
                  FontStyle.Bold);

              }
              else if (((XmlElement)childXmlNode).GetAttribute("titlecolor") != null)
              {
                tn.ForeColor = Color.FromName(((XmlElement)childXmlNode).GetAttribute("titlecolor"));
                tn.NodeFont = new Font(
                    this.treeView.Font.Name,
                    this.treeView.Font.Size,
                    FontStyle.Bold);
              }
              if (((XmlElement)childXmlNode).GetAttribute("tooltip") != null)
              {
                this.treeView.ShowNodeToolTips = true;
                tn.ToolTipText = (((XmlElement)childXmlNode).GetAttribute("tooltip")).ToString();
              }
            }
            */
            else
            {
               imageIndex = this.GetIconImageIndex_old(ni.Path);
            }
          }
          else
          {
            if (ni.Command != String.Empty && ni.Command != null)
            {
              //tn.ImageIndex = tn.SelectedImageIndex = 9;
              imageIndex = 9;
            }
            else
            {
              // TODO:指定iconの場合
              imageIndex = 8;
            }
          }
        }
      }
      return imageIndex;// 38;
    }

    private Int32 getImageIndexFromNodeInfo_safe(NodeInfo ni)
    {
      Int32 imageIndex = 38;
      ///////////////////////////////////////////////////////////////////////////////////////////
      // icon設定
      // アイコン設定(このサンプルにはリソースファイルを入れてないので実は機能していませんが)
      if (ni.Icon != String.Empty) imageIndex = GetIconImageIndexFromIconPath(ni.Icon);
      else
      {
        //if (ni.Type == "folder") imageIndex = GetIconImageIndex(@"F:\cpp");
        if (ni.Type == "folder") imageIndex = GetIconImageIndex(@"C:\windows");
        else
        {
          String path = "";
          if (ni.PathBase != String.Empty) path = Path.Combine(ni.PathBase, ni.Path);
          else path = ni.Path;

          if (Lib.IsWebSite(path)) imageIndex = this.GetIconImageIndexFromIconPath(path);
          else if (File.Exists(path))
          {
            if (ni.Type == "root") imageIndex = 38;
            else if (ni.Action.ToLower() == "ant" || ni.Action.ToLower() == "wsf") imageIndex = 14;
            else imageIndex = this.GetIconImageIndex(ni.Path);
          }
          else
          {
            if (ni.Command != String.Empty && ni.Command != null) imageIndex = 9;
            else imageIndex = 8;
          }
        }
      }
      return imageIndex; ;
    }

    private XmlDocument exportXmlDocument = new XmlDocument();

    private void RecursiveBuildToXml(TreeNode Parentnode, XmlNode ParentXmlNode)
    {
      foreach (TreeNode treeNode in Parentnode.Nodes)
      {
        NodeInfo nodeInfo = new NodeInfo();
        nodeInfo = (NodeInfo)treeNode.Tag;
        XmlElement xmlElement = this.exportXmlDocument.CreateElement(nodeInfo.Type);
        if (nodeInfo.Title != string.Empty)
        {
          xmlElement.SetAttribute("title", nodeInfo.Title);
        }
        if (nodeInfo.PathBase != string.Empty && this.ProcessVariable(((XmlElement)ParentXmlNode).GetAttribute("base")) != nodeInfo.PathBase)
        {
          xmlElement.SetAttribute("base", nodeInfo.PathBase);
        }
        if (nodeInfo.Action != string.Empty)
        {
          xmlElement.SetAttribute("action", nodeInfo.Action);
        }
        if (nodeInfo.Command != string.Empty)
        {
          xmlElement.SetAttribute("command", nodeInfo.Command);
        }
        if (nodeInfo.Path != string.Empty)
        {
          xmlElement.SetAttribute("path", nodeInfo.Path);
        }
        if (nodeInfo.Icon != string.Empty)
        {
          xmlElement.SetAttribute("icon", nodeInfo.Icon);
        }
        if (nodeInfo.Args != string.Empty)
        {
          xmlElement.SetAttribute("args", nodeInfo.Args);
        }
        if (nodeInfo.Option != string.Empty)
        {
          xmlElement.SetAttribute("option", nodeInfo.Option);
        }
        if (nodeInfo.Type == "record" && nodeInfo.InnerText != string.Empty && nodeInfo.InnerText != null)
        {
          xmlElement.InnerText = nodeInfo.InnerText;

          if ((nodeInfo.Type == "root" || nodeInfo.Type == "folder") && treeNode.IsExpanded)
          {
            xmlElement.SetAttribute("expand", "true");

          }
          ParentXmlNode.AppendChild(xmlElement);
          this.RecursiveBuildToXml(treeNode, xmlElement);
        }
      }
    }

    private void RecursiveBuildToXml2(TreeNode Parentnode, XmlNode ParentXmlNode, XmlDocument doc)
    {
      foreach (TreeNode childTreeNode in Parentnode.Nodes)
      {
        if (childTreeNode.Tag is NodeInfo)
        {
          NodeInfo ni = childTreeNode.Tag as NodeInfo;
          XmlElement xe = doc.CreateElement(ni.Type);
          TreeViewManager.SetAttributeByNodeInfo(xe, ni);
          if (ni.Type == "record" && !String.IsNullOrEmpty(ni.InnerText)) xe.InnerText = ni.InnerText;
          //public string Comment
          if (ni.Type == "root" || ni.Type == "folder")
          {
            if (childTreeNode.IsExpanded == true) xe.SetAttribute("expand", "true");
            ParentXmlNode.AppendChild(xe);
            RecursiveBuildToXml2(childTreeNode, xe, doc);
          }
          else
          {
            ParentXmlNode.AppendChild(xe);
          }
        }
        else
        {
          XmlElement xe = doc.CreateElement(childTreeNode.Text);
          if (childTreeNode.Tag is String) xe.SetAttribute("path", childTreeNode.Tag as String);
          else if (childTreeNode.Tag != null) xe.SetAttribute("object", childTreeNode.Tag.ToString());
          ParentXmlNode.AppendChild(xe);
          RecursiveBuildToXml2(childTreeNode, xe, doc);
        }
      }
    }

    private void ProcessNode(XmlNode xmlNode)
    {
      String name = xmlNode.Name;

      switch (name)
      {
        case "toolbar":
          this.ToolBarSettings(xmlNode);
          break;
        case "menubar":
          //MessageBox.Show("menubar");
          // 未完成
          this.MenuBarSettings(xmlNode);
          break;
        case "launch":
          this.AutoRunSetting(xmlNode);
          break;
        case "settings":
          //this.ProjectSettings(xmlNode);
          break;
        case "property":
          //this.AntPropertySettings(xmlNode);
          break;
      }

    }

    public static List<String> ToolBarSettingsFiles = new List<string>();
    public static List<ToolStrip> toolStripList  = new List<ToolStrip>();
    public static ToolStrip dynamicToolStrip = new ToolStrip();

    public void ToolBarSettings(XmlNode xmlNode)
    {
      //MessageBox.Show(this.currentTreeMenuFilepath);
      try
      {
        if (!ToolBarSettingsFiles.Contains(this.currentTreeMenuFilepath))
        {
          //一秒間（1000ミリ秒）停止する
          //System.Threading.Thread.Sleep(1000);
          ToolStrip toolStrip = StripBarManager.GetToolStripFromString(xmlNode.OuterXml);
          ToolStripManager.Merge(toolStrip, PluginBase.MainForm.ToolStrip);
          ToolBarSettingsFiles.Add(this.currentTreeMenuFilepath);
        }
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.Message.ToString(),"TreeMenu:ToolBarSettings:1184");
      }
    }

    public static List<String> MenuBarSettingsFiles = new List<string>();
    public static List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();
    public void MenuBarSettings(XmlNode xmlNode)
    {
      try
      {
        if (!MenuBarSettingsFiles.Contains(this.currentTreeMenuFilepath))
        {
          //一秒間（1000ミリ秒）停止する
          //System.Threading.Thread.Sleep(1000);
          MenuStrip menuStrip = StripBarManager.GetMenuStripFromString(xmlNode.OuterXml);
          menuItems = new List<ToolStripMenuItem>();
          foreach (ToolStripMenuItem item in menuStrip.Items)
          {
            menuItems.Add(item);
          }

          foreach (ToolStripMenuItem item in menuItems)
          {
            PluginBase.MainForm.MenuStrip.Items.Add(item);
          }
          MenuBarSettingsFiles.Add(this.currentTreeMenuFilepath);
        }
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.Message.ToString(),"TreeMenu:MenuBarSettings:1214");
      }
    }

    public static List<String> AutoRunSettingFiles = new List<string>();
    public void AutoRunSetting(XmlNode parentNode)
    {
      try
      {
        if (!AutoRunSettingFiles.Contains(this.currentTreeMenuFilepath))
        {
          //MessageBox.Show(parentNode.ChildNodes.Count.ToString());
          AutoRunSettingFiles.Add(this.currentTreeMenuFilepath);
          for (int i = 0; i < parentNode.ChildNodes.Count; i++)
          {
            if (parentNode.ChildNodes[i].Name == "record")
            {
              XmlNode node = parentNode.ChildNodes[i];
              NodeInfo nodeInfo = new NodeInfo();
              nodeInfo.Type = ((XmlElement)node).Name; //MessageBox.Show(nodeInfo.Type);
              nodeInfo.Title = this.ProcessVariable(((XmlElement)node).GetAttribute("title")); //MessageBox.Show(nodeInfo.Type);
              nodeInfo.PathBase = this.ProcessVariable(((XmlElement)node).GetAttribute("base")); //MessageBox.Show(nodeInfo.PathBase);
              nodeInfo.Action = this.ProcessVariable(((XmlElement)node).GetAttribute("action")); //MessageBox.Show(nodeInfo.Action);
              nodeInfo.Command = this.ProcessVariable(((XmlElement)node).GetAttribute("command")); //MessageBox.Show(nodeInfo.Action);
              nodeInfo.Path = this.ProcessVariable(((XmlElement)node).GetAttribute("path")); //MessageBox.Show(nodeInfo.Command);
              nodeInfo.Args = this.ProcessVariable(((XmlElement)node).GetAttribute("args")); //MessageBox.Show(nodeInfo.Args);
              nodeInfo.Option = this.ProcessVariable(((XmlElement)node).GetAttribute("option")); //MessageBox.Show(nodeInfo.Option);
              ActionManager.NodeAction(nodeInfo);
            }
          }
        }
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.Message.ToString(),"TreeMenu:AutoRunSetting:1338");
      }
    }

    public static List<String> ProjectSettingsFiles = new List<string>();
    public void ProjectSettings(XmlNode parentNode)
    {
      /*
      try
      {
        if (!ProjectSettingsFiles.Contains(this.currentTreeMenuFilepath))
        {
          ProjectSettingsFiles.Add(this.currentTreeMenuFilepath);
          for (int i = 0; i < parentNode.ChildNodes.Count; i++)
          {
            switch (parentNode.ChildNodes[i].Name)
            {
              case "documentroot": this.settings.DocumentRoot = parentNode.ChildNodes[i].InnerXml; break;
              case "serverroot": this.settings.ServerRoot = parentNode.ChildNodes[i].InnerXml; break;
            }
          }
          DialogResult result = MessageBox.Show("DocumentRoot: " + this.settings.DocumentRoot
            + "\nServerRoot: " + this.settings.ServerRoot,
              "設定変更",
              MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.Message.ToString());
        //this.settings.DocumentRoot = @"C:\Apache2.2\htdocs";
        //this.settings.ServerRoot = "http://localhost";
      }
    */
    }

    public Dictionary<String, String> AntProperties = new Dictionary<string, string>();
    public static List<String> AntPropertiesFiles = new List<string>();
    public void AntPropertySettings(XmlNode parentNode)
    {
      if (Path.GetFileName(this.currentTreeMenuFilepath) != "FDTreeMenu.xml") return;
      //MessageBox.Show(Path.GetFileName(this.currentTreeMenuFilepath));
      //MessageBox.Show(string.Format("Key : {0} / Value : {1}",
      //  ((XmlElement)parentNode).GetAttribute("name"), ((XmlElement)parentNode).GetAttribute("value")));
      if (!AntPropertiesFiles.Contains(this.currentTreeMenuFilepath))
      {
        AntPropertiesFiles.Add(this.currentTreeMenuFilepath);
        try
        {
          if (parentNode.HasChildNodes)
          {
            AntProperties[this.ProcessVariable(((XmlElement)parentNode).GetAttribute("name"))] = parentNode.InnerText;
          }
          else
          {
            AntProperties[this.ProcessVariable(((XmlElement)parentNode).GetAttribute("name"))]
              = this.ProcessVariable(((XmlElement)parentNode).GetAttribute("value"));
          }
        }
        catch (Exception ex1)
        {
          MessageBox.Show(ex1.Message.ToString(),"TreeMenu:AntPropertySettings:1309");
        }
      }
    }

    #region treeView Doulbe Click Handler and Functions

    public void treeView_DoubleClick(object sender, EventArgs e)
    {
      // fixed 2018-2-23
      //TreeNode treeNode = treeView.SelectedNode;
      TreeView tree = sender as TreeView;
      TreeNode treeNode = tree.SelectedNode;

      /// XmlTreeView カプセル化のため追加 2018-02-23
      ToolStripMenuItem button = new ToolStripMenuItem();
      try
      {
        if (treeNode.Tag.GetType().Name != "NodeInfo") return;
        /*
        if (treeNode is AntTreeNode)
        {
          //this.pluginUI.RunTarget(sender,null);
          XmlNode xmlNode = treeNode.Tag as XmlNode;
          XmlNode tag = treeNode.Tag as XmlNode;
          XmlAttribute descrAttr = tag.Attributes["description"];
          if (xmlNode.Name == "project" || xmlNode.Name == "package")
          {
            PluginBase.MainForm.OpenEditableDocument(((AntTreeNode)treeNode).File, false);
          }
          else
          {
            MessageBox.Show(((AntTreeNode)treeNode).File, ((AntTreeNode)treeNode).Target);
            this.pluginMain.RunTarget(((AntTreeNode)treeNode).File, ((AntTreeNode)treeNode).Target);
          }
          return;
        }
        */
        //if (this.treeView.SelectedNode.Parent == null)
        if (treeNode.Parent == null)
        {
          //PluginBase.MainForm.OpenEditableDocument(((NodeInfo)this.treeView.SelectedNode.Tag).Path);
          PluginBase.MainForm.OpenEditableDocument(((NodeInfo)treeNode.Tag).Path);
          return;
        }
      }
      catch (Exception ex2)
      {
        String errorMsg = ex2.Message.ToString();
        MessageBox.Show(errorMsg, "treeNode.Tag.GetType().Name != NodeInfo");
        return;
      }
      NodeInfo ni = new NodeInfo();

      // fixed 2018-02-23
      //ni = this.treeView.SelectedNode.Tag as NodeInfo;
      ni = treeNode.Tag as NodeInfo;
      ActionManager.NodeAction(ni);

    }

    public NodeInfo SelectedNodeInfo()
    {
      return (NodeInfo)this.treeView.SelectedNode.Tag;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 未完成
    /// </summary>
    /// <param name="argstring"></param>
    public void Menu(String argstring)
    {
      String command = null;
      String args = null;
      String path = null;
      String option = null;
      String dir = String.Empty;

      if (argstring == "") return;
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
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()),"TreeMenu:Menu:1519");
        return;
      }
      if (path != String.Empty)
      {
        if (System.IO.Directory.Exists(path)) dir = path;
        else if (System.IO.File.Exists(path)) dir = Path.GetDirectoryName(path);
        if (dir != String.Empty) System.IO.Directory.SetCurrentDirectory(dir);
      }
      if (command == "OpenTreeDataDialog") this.pluginMain.pluginUI.addButton_Click(null, null);
      else if (File.Exists(path))
      {
        //this.treeMenuFile.Clear();
        //this.treeView.Nodes.Clear();
        //this.LoadFile(path);
        //this.currentTreeMenuFilepath = path;
        //string[] files = { path };
        //this.pluginMain.AddBuildFiles(files);
        treeView.Nodes.Clear();
        treeView.Nodes.Add(this.getXmlTreeNode(path, true));
      }
      else return;
    }
    #endregion

    #region XmlTreeMenu コンテキストメニュー Click Handler
    private void 検索FToolStripMenuItem_Click(object sender, EventArgs e)
    {
      String msg = String.Empty;
      //NodeInfo nodeInfo = this.SelectedNodeInfo();
      TreeNode treeNode = this.treeView.SelectedNode;

      string value = "zipfile";
      if (Lib.InputBox("Node 検索", "検索するノード名:", ref value) == DialogResult.OK)
      {
        TreeNode[] nodes = treeNode.Nodes.Find(value, true);
        if (nodes.Length > 0)
        {
          this.pluginUI.treeView.CheckBoxes = true;
          this.treeView.SelectedNode = nodes[0];
          for (int i = 0; i < nodes.Length; i++)
          {
            msg += nodes[i].FullPath + "\n";
            nodes[i].Checked = true;
          }
          //msg = nodes.Length.ToString() + " 件が検索されました\n" + msg;
          MessageBox.Show(msg, "zipfile 検索結果 ; " + nodes.Length.ToString() + " 件");

        }
        else this.pluginUI.treeView.CheckBoxes = false;
      }
      else this.pluginUI.treeView.CheckBoxes = false;
    }

    private void 再読み込みToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.pluginUI.RefreshData();
    }

    private void コマンドプロンプトCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (Directory.Exists(nodeInfo.Path))
      {
        Directory.SetCurrentDirectory(nodeInfo.Path);
        Process.Start(@"C:\windows\system32\cmd.exe");
        return;
      }
      if (Directory.Exists(Path.GetDirectoryName(nodeInfo.Path)))
      {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(nodeInfo.Path));
        Process.Start(@"C:\windows\system32\cmd.exe");
      }

    }

    private void エクスプローラで開くXToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path))
      {
        Process.Start(@"C:\windows\explorer.exe", Path.GetDirectoryName(nodeInfo.Path));
      }
    }

    private void コンテキストメニューToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.treeView.SelectedNode == null)
      {
        MessageBox.Show("ノードを選択してください");
        return;
      }
      FileInfo[] array = new FileInfo[1];
      string path = ((NodeInfo)this.treeView.SelectedNode.Tag).Path;
      if (!File.Exists(path))
      {
        return;
      }
      ShellContextMenu shellContextMenu = new ShellContextMenu();
      Point point = new Point(this.contextMenuStrip1.Bounds.Left, this.contextMenuStrip1.Bounds.Top);
      array[0] = new FileInfo(path);
      this.contextMenuStrip1.Hide();
      shellContextMenu.ShowContextMenu(array, point);
    }

    private void 実行EToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path) || WebHandler.SiteExists(nodeInfo.Path))
      {
        Process.Start(nodeInfo.Path);
      }
    }

    private void sakuraMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path)) Process.Start(this.pluginUI.settings.SakuraPath, nodeInfo.Path);
    }

    private void OpenDocumentMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path))
      {
        //PluginBase.MainForm.OpenEditableDocument(nodeInfo.Path, false);
        this.CallMenuCommand("OpenDocument", nodeInfo.Path);
      }
    }

    private void psPadMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path))
      {
        Process.Start(this.pluginUI.settings.PspadPath, nodeInfo.Path);
      }
    }

    private void explorerMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path))
      {
        Process.Start(@"C:\windows\explorer.exe", Path.GetDirectoryName(nodeInfo.Path));
      }
    }

    private void CmdPromptMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (Directory.Exists(nodeInfo.Path))
      {
        Directory.SetCurrentDirectory(nodeInfo.Path);
        Process.Start(@"C:\windows\system32\cmd.exe");
        return;
      }
      if (Directory.Exists(Path.GetDirectoryName(nodeInfo.Path)))
      {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(nodeInfo.Path));
        Process.Start(@"C:\windows\system32\cmd.exe");
      }
    }

    private void chromeCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      String path = nodeInfo.Path.Replace("\\", "/");
      String url = Lib.Path2Url(path);
      if (url == String.Empty || !WebHandler.SiteExists(url)) return;

      DialogResult result = MessageBox.Show(url, "url を chromeで開きますか？",
          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

      //何が選択されたか調べる
      if (result == DialogResult.Yes)
      {
        //「はい」が選択された時
        Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", url);
      }
      else if (result == DialogResult.No)
      {
        //「いいえ」が選択された時
        Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", path);
      }
      else if (result == DialogResult.Cancel)
      {
        //「キャンセル」が選択された時
        MessageBox.Show("キャンセル");
      }
    }

    private void ファイル名を指定して実行_Click(object sender, EventArgs e)
    {

    }

    private void browseMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      String url = Lib.Path2Url(nodeInfo.Path);
      PluginBase.MainForm.CallCommand("Browse", url);
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (nodeInfo.Type == "root")
      {
        this.pluginUI.MenuRemoveClick(sender, e);
      }
      else
      {
        MessageBox.Show(nodeInfo.Type);
      }
    }

    private void runTargetToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void showInEditorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TreeNode node = treeView.SelectedNode;
      String headTag = GetHeadTagFromXmlNode(((NodeInfo)node.Tag).XmlNode);
      string[] del = { "\n" };
      String keyWord = headTag.Replace("\n","|").Split('|')[0].Trim();
      MessageBox.Show(headTag, "検索に成功しました");
      MessageBox.Show(keyWord, "検索に成功しました");


      while (node.Parent != null)
      {
        if (((NodeInfo)node.Tag).Type == "root" || ((NodeInfo)node.Tag).Type == "include") break;
        node = node.Parent;
      }
      NodeInfo ni = node.Tag as NodeInfo;
      if(File.Exists(ni.Path))
      {
        PluginBase.MainForm.OpenEditableDocument(((NodeInfo)node.Tag).Path, false);
        ScintillaControl sci = PluginBase.MainForm.CurrentDocument.SciControl;
        String text = sci.Text;
        MessageBox.Show(keyWord, "検索に成功しました");
        sci.GotoPos(0);
        if (text.IndexOf(keyWord) > -1)
        {
           sci.SetSel(text.IndexOf(keyWord), keyWord.Length);
        }
      }
    }

    private void FindNext(String text, Boolean refreshHighlights)
    {
      //FlashDevelop.Controls.QuickFind.FindNext(String text, Boolean refreshHighlights)

      if (text == "") return;
      ScintillaControl sci = PluginBase.MainForm.CurrentDocument.SciControl;
      List<SearchMatch> matches = this.GetResults(sci, text);
      sci.GotoPos(0);
      sci.SetSel(matches[0].Index, matches[0].Index + matches[0].Length);
    }

    private List<SearchMatch> GetResults(ScintillaControl sci, String text)
    {
      String pattern = text;
      FRSearch search = new FRSearch(pattern);
      search.Filter = SearchFilter.None;
      search.SourceFile = sci.FileName;
      return search.Matches(sci.Text);
    }

    private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void visualStudioToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path)) Process.Start(this.pluginUI.settings.Devenv17Path, "/edit " + "\"" + nodeInfo.Path + "\"");
    }

    private void mediaPlayerMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path))
      {
        Process.Start(this.pluginUI.settings.MediaPlayerPath, "\"" + nodeInfo.Path + "\"");
      }
    }

    private void vlcVToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path))
      {
        Process.Start(this.pluginUI.settings.VlcPath, "\"" + nodeInfo.Path + "\"");
      }
    }

    private void richTextEditorMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void azukiEditorMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void traverseNodeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      String projectDir = Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath);
      TreeNode treeNode = this.treeView.SelectedNode;
      String savePath = Path.Combine(Path.Combine(projectDir, "obj"), treeNode.Text + "_xPathList.txt");
      TraverseCallback cb = ShowTitle;

      TreeViewManager.treexPathList.Clear();
      TreeViewManager.treeNodeInfoList.Clear();
      TreeViewManager.TraverseTree(treeNode.Nodes, cb, "");
      //MessageBox.Show(String.Join("\n",TreeViewManager.xPathList.ToArray()));

      if (Lib.confirmDestructionText("保存確認", String.Join("\n", TreeViewManager.treexPathList.ToArray())))
      {
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "XPathListファイル (*.txt)|*.txt|" + "All files (*.*)|*.*";
        dialog.FileName = treeNode.Text + "_XPathList.txt";
        if (!String.IsNullOrEmpty(projectDir))
        {
          if (Directory.Exists(Path.Combine(projectDir, "obj")))
            dialog.InitialDirectory = Path.Combine(projectDir, "obj");
          else dialog.InitialDirectory = projectDir;
        }
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          File.WriteAllLines(savePath, TreeViewManager.treexPathList.ToArray(), Encoding.UTF8);
        }
      }
    }

    void ShowTitle(TreeNode tn)
    {
      //String title = tn.FullPath;
      MessageBox.Show("FullPath = " + tn.FullPath.Replace("\\", "/") + "\nXPath    = " + tn.Name);
    }

    private void xML生成ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      String projectDir = Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath);
      TreeNode treeNode = this.treeView.SelectedNode;
      NodeInfo ni = treeNode.Tag as NodeInfo;
      XmlNode node = ni.XmlNode;
      String output = String.Empty;
      if (!String.IsNullOrEmpty(node.OuterXml))
      {
        output = TreeViewManager.GetFormattedXmlText(node.OuterXml);
        if (output.Length > 1000) output = output.Substring(0, 1000) + "\n......";
      }
      else
      {
        XmlDocument doc = new XmlDocument();
        System.Xml.XmlDeclaration xmlDecl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        //作成したXML宣言をDOMドキュメントに追加します
        doc.AppendChild(xmlDecl);
        XmlElement xe = doc.CreateElement("root");
        RecursiveBuildToXml2(treeNode, xe, doc);
        doc.AppendChild(xe);
        output = TreeViewManager.GetFormattedXmlText(xe.OuterXml);
        if (output.Length > 1000) output = output.Substring(0, 1000) + "\n......";
        /*
        // https://www.ipentec.com/document/csharp-xml-xmlnode-xmlelement-diff
        XmlElementはXmlNodeクラスを継承している(XmlElementのほうが機能が多い)
        XmlElementにはSetAttributes SetAttributeNodeの属性編集用メソッドが提供されている
        XmlElementにはGetAttributes GetAttributeNodeの属性取得用メソッドが提供されている
        XmlElementにはRemoveAllAttributes RemoveAttribute RemoveAttributeAt RemoveAttributeNode の属性削除メソッドが提供されている
        XmlElementには要素のタグが短い形式(< tag />)である(要素の中が空である)ことを判定するIsEmptyプロパティが用意されている
        */
      }
      if (Lib.confirmDestructionText("保存確認", output))
      {
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "XmlTreeファイル (*.xml)|*.xml|" + "All files (*.*)|*.*";
        dialog.FileName = treeNode.Text + "_TreeMenu.xml";
        if (!String.IsNullOrEmpty(projectDir))
        {
          if (Directory.Exists(Path.Combine(projectDir, "obj")))
            dialog.InitialDirectory = Path.Combine(projectDir, "obj");
          else dialog.InitialDirectory = projectDir;
        }
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          StreamWriter sw = new StreamWriter(dialog.FileName, false, Encoding.UTF8);
          sw.Write(TreeViewManager.GetFormattedXmlText(node.OuterXml));
          sw.Close();
        }
      }
    }

    private void xPathList生成ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      String projectDir = Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath);
      TreeNode treeNode = this.treeView.SelectedNode;
      NodeInfo ni = treeNode.Tag as NodeInfo;
      XmlNode node = ni.XmlNode;
      MessageBox.Show(TreeViewManager.GetFormattedXmlText(node.OuterXml), "変換するXML");

      XmlDocument doc = new XmlDocument();
      doc.LoadXml(node.OuterXml);

      TreeViewManager.XPathList.Clear();
      //node = doc.SelectSingleNode("/folder");
      node = doc.DocumentElement;
      TreeViewManager.TraverseNode(node);
      String output = String.Join("\n", TreeViewManager.XPathList.ToArray());
      if (output.Length > 1000) output = output.Substring(0, 1000) + "\n......";

      if (Lib.confirmDestructionText("保存確認", output))
      {
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "XPathList (*.txt)|*.txt|" + "All files (*.*)|*.*";
        dialog.FileName = treeNode.Text + "_xPathList.txt";
        if (!String.IsNullOrEmpty(projectDir))
        {
          if (Directory.Exists(Path.Combine(projectDir, "obj")))
            dialog.InitialDirectory = Path.Combine(projectDir, "obj");
          else dialog.InitialDirectory = projectDir;
        }
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          File.WriteAllLines(dialog.FileName, TreeViewManager.XPathList.ToArray(), Encoding.UTF8);
        }
      }
    }

    private void 挿入IToolStripMenuItem_Click(object sender, EventArgs e)
    {
      String projectDir = Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath);
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Filter = "BuildFiles (*.xml)|*.XML|" + "WshFiles (*.wsf)|*.wsf|"
         + "XPathList (*.txt)|*.txt|" + "All files (*.*)|*.*";
      dialog.Multiselect = true;
      if (!String.IsNullOrEmpty(projectDir))
      {
        if (Directory.Exists(Path.Combine(projectDir, "obj")))
          dialog.InitialDirectory = Path.Combine(projectDir, "obj");
        else dialog.InitialDirectory = projectDir;
      }
      if (dialog.ShowDialog() == DialogResult.OK)
      {
        TreeViewManager.GetTreeNodeFromFiles(this.treeView.SelectedNode, dialog.FileNames);
      }
    }

    private void 切り取りToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TreeViewManager.RemoveNode(this.treeView.SelectedNode);
    }

    private void コピーCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TreeViewManager.CopyNode(this.treeView.SelectedNode);
    }

    private void 貼り付けPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TreeViewManager.PasteNode(this.treeView.SelectedNode);
    }

    /// <summary>
    /// Addrecord
    /// </summary>
    ///https://www.atmarkit.co.jp/fdotnet/dotnettips/259treeviewadd/treeviewadd.html
    private int counter = 1;
    private void 新規項目MenuItem_Click(object sender, EventArgs e)
    {
      if (this.treeView.SelectedNode == null)
      {
        MessageBox.Show("ノードを選択してください");
        return;
      }
      if (!(this.treeView.SelectedNode.Tag is NodeInfo) || ((NodeInfo)this.treeView.SelectedNode.Tag).Type == "record")//|| PluginUI.GetTagType(this.treeView1.SelectedNode) == null)
      {
        MessageBox.Show("グループを追加できるのはルート又はグループの下だけです。");
        return;
      }
      TreeNode treeNodeNew = new TreeNode("新規項目" + counter.ToString());
      treeNodeNew.ImageIndex = treeNodeNew.SelectedImageIndex = 10;
      treeNodeNew.Tag = new NodeInfo
      {
        Type = "record",
        Title = "新規項目" + counter.ToString(),
        PathBase = string.Empty,
        Action = string.Empty,
        Command = string.Empty,
        Path = string.Empty,
        Icon = string.Empty,
        Args = string.Empty,
        Option = string.Empty,
        InnerText = string.Empty,
        Comment = string.Empty
      };
      this.treeView.SelectedNode.Nodes.Add(treeNodeNew);
      this.treeView.SelectedNode = treeNodeNew;
      this.treeView.LabelEdit = true;
      this.treeView.SelectedNode.BeginEdit();
      this.treeView.SelectedNode.EndEdit(true);
      this.counter++;
    }

    /// <summary>
    /// Addfolder
    /// </summary>
    private int counter2 = 1;
    private void 新規フォルダMenuItem_Click(object sender, EventArgs e)
    {
      if (this.treeView.SelectedNode == null)
      {
        MessageBox.Show("ノードを選択してください");
        return;
      }
      if (!(this.treeView.SelectedNode.Tag is NodeInfo) || ((NodeInfo)this.treeView.SelectedNode.Tag).Type == "record")
      {
        MessageBox.Show("グループを追加できるのはルート又はグループの下だけです。");
        return;
      }
      TreeNode treeNode = new TreeNode("新規グループ" + counter2.ToString());
      treeNode.ImageIndex = 11;
      treeNode.SelectedImageIndex = 12;
      treeNode.Tag = new NodeInfo
      {
        Type = "folder",
        Title = "新規グループ",
        PathBase = string.Empty,
        Action = string.Empty,
        Command = string.Empty,
        Path = string.Empty,
        Icon = string.Empty,
        Args = string.Empty,
        Option = string.Empty,
        InnerText = string.Empty,
        Comment = string.Empty
      };
      this.treeView.SelectedNode.Nodes.Add(treeNode);
      this.treeView.SelectedNode = treeNode;
      this.treeView.LabelEdit = true;
      this.treeView.SelectedNode.BeginEdit();
      this.treeView.SelectedNode.EndEdit(true);
      this.counter2++;
    }

    /// <summary>
    /// Renames current file or directory
    /// </summary>
    private void 名前の変更MToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.treeView.LabelEdit = true;
      this.treeView.SelectedNode.BeginEdit();
      this.pluginUI.propertyGrid1_PropertyValueChanged(null, null);
      //this.treeView.LabelEdit = false;
    }

    private void 削除MenuItem_Click(object sender, EventArgs e)
    {
      if (this.treeView.SelectedNode != null)
      {
        if (this.treeView.SelectedNode.Parent == null)
        {
          MessageBox.Show("ルートノードを削除することはできません");
          return;
        }
        if (MessageBox.Show("選択したノードを削除してもよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          this.treeView.Nodes.Remove(this.treeView.SelectedNode);
          return;
        }
      }
      else
      {
        MessageBox.Show("ノードを選択してください");
      }
    }

    private void 隠した項目を表示SToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void 隠すHToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //this.treeView.SelectedNode.IsVisible = !this.treeView.SelectedNode.IsVisible;
    }

    private void targetToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Custom Document

    public void InitializeCustomControlsInterface(IMDIForm control)
    {
      control.MainForm = new ParentFormClass
      {
        Instance = (Form)PluginBase.MainForm,
        //containerDockContent = control as DockContent,
        toolStrip = PluginBase.MainForm.ToolStrip,
        menuStrip = PluginBase.MainForm.MenuStrip,
        statusStrip = PluginBase.MainForm.StatusStrip,
        //xmlTreeMenu_pluginUI = this,
        //settings = this.pluginMain.Settings,
        callPluginCommand = this.CallPluginCommand
      };
    }
   
    public DockContent CreateCustomDocument(string name, string file, string option= "")
    {
      DockContent result;
      try
      {
        Control control = this.CreateCustomDockControl(name, file,option);
        control.Dock = DockStyle.Fill;
        DockContent dockContent = PluginBase.MainForm.CreateCustomDocument(control);
        dockContent.Name = Path.GetFileNameWithoutExtension(name);
        dockContent.Tag = control;
        dockContent.AccessibleDescription = control.GetType().FullName;
        string[] array = file.Split(new char[] { '!' });
        if (array.Length > 1)
        {
          dockContent.TabText = "[出力]" + Path.GetFileName(array[0]);
        }
        else
        {
          dockContent.TabText = Path.GetFileName(array[0]);
        }
        string data = control.GetType().FullName + "!" + file;
        //this.AddPreviousCustomDocuments(data);
        result = dockContent;
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()),"TreeMenu:DockContent CreateCustomDocument:2083");
        result = null;
      }
      return result;
    }

    public void CustomDocument_FormClosing(object sender, EventArgs e)
    {
      DockContent dockContent = sender as DockContent;
      string fullName = dockContent.Controls[0].GetType().FullName;
      switch (dockContent.Name)
      {
        case "AzukiEditor":
          //IMDIForm control = dockContent.Tag as IMDIForm;
          //this.settings.PreviousAzukiEditorDocuments = ((AzukiEditor)dockContent.Tag).PreviousDocuments;
          break;
        case "Browser":
          //this.settings.PreviousHTMLEditorDocuments = ((Browser)dockContent.Tag).PreviousDocuments;
          break;
        case "HTMLEditor":
          //this.settings.PreviousHTMLEditorDocuments = ((HTMLEditor)dockContent.Tag).PreviousDocuments;
          break;
        case "OpenGLPanel":
          //this.settings.PreviousOpenGLPanelDocuments = ((OpenGLPanel)dockContent.Tag).PreviousDocuments;
          break;
        case "PicturePanel":
          //this.settings.PreviousPicturePanelDocuments = ((PicturePanel)dockContent.Tag).PreviousDocuments;
          break;
        case "PlayerPanel":
          //this.settings.PreviousPlayerPanelDocuments = ((PlayerPanel)dockContent.Tag).PreviousDocuments;
          break;
        case "RichTextEditor":
          //this.settings.PreviousRichTextEditorDocuments = ((RichTextEditor)dockContent.Tag).PreviousDocuments;
          break;
        /*
      case "SpreadSheet":
        //this.settings.PreviousSpreadSheetDocuments = ((SpreadSheet)dockContent.Tag).PreviousDocuments;
        break;
      */
        case "SimplePanel":
          //this.settings.PreviousSimplePanelDocuments = ((SimplePanel)dockContent.Tag).PreviousDocuments;
          break;
        case "TreeGridView":
          //this.settings.PrevioustreeGridViewDocuments = ((TreeGridViewPanel)dockContent.Tag).PreviousDocuments;
          break;
        default:
          break;
      }

      //FIXME dictionary 使い方 ???
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      try
      {
        string[] array2 = ((Control)dockContent.Tag).AccessibleDescription.Split(new char[] { ';' });
        for (int i = 0; i < array2.Length; i++)
        {
          string text = array2[i];
          string[] array3 = text.Split(new char[] { '=' });
          dictionary.Add(array3[0], array3[1]);
        }
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
        return;
      }
      //string key;
      //switch (key = fullName)
      //{
      //}
      string text2 = dockContent.Text;
      string item = string.Concat(new string[] { text2, "|", fullName, "|", String.Empty });
      /*
      try
      {
        //FIXME
        if (this.settings.CustomSessionData.Contains(item))
        {
          this.settings.CustomSessionData.Remove(item);
        }
      }
      catch (Exception ex2)
      {
        MessageBox.Show(ex2.Message.ToString());
      }
      */
    }

    private string MakeQueryString(string name)
    {
      /*
      if (String.IsNullOrEmpty(name)) return String.Empty;
      switch (name)
      {
        case "PicturePanel":
          string text = "recentdocuments=" + string.Join("|", this.settings.PreviousPicturePanelDocuments.ToArray());
          string text2 = "menubarvisible=" + (this.settings.PicturePanelMenuBarVisible ? "true" : "false");
          string text3 = "toolbarvisible=" + (this.settings.PicturePanelToolBarVisible ? "true" : "false");
          string text4 = "statusbarvisible=" + (this.settings.PicturePanelStatusBarVisible ? "true" : "false");
          string text5 = "scribblemode=" + (this.settings.PicturePanelScribbleMode ? "true" : "false");
          string text6 = "rubberbandmode=" + (this.settings.PicturePanelRubberBandMode ? "true" : "false");
          return string.Concat(new string[] { text, ";", text2, ";", text3, ";", text4, ";", text5, ";", text6 });
          //break;
      }
      */
      return string.Empty;
    }

    public Control CreateCustomDockControl(string path, string file,string option="")
    {
      Control result;
      Assembly assembly = null;
      String dlldir = Path.Combine(PathHelper.BaseDir, "DockableControls");
      String dllpath = Application.ExecutablePath;//String.Empty;
      String classname = String.Empty;
      Type type = null;
      try
      {
        Control control = null;
        switch (Path.GetFileNameWithoutExtension(path))
        {
          // PicturePanelは動的dllロードで実装
          //case "PlayerPanel":
          //control = new PlayerPanel() as Control;
          //PlayerPanel playerPanel = new PlayerPanel();
          //playerPanel.PreviousDocuments = this.settings.PreviousPlayerPanelDocuments;
          //playerPanel.Instance.PreviousDocuments = this.settings.PreviousPlayerPanelDocuments;
          //control = playerPanel as Control;
          //break;
          case "PicturePanel":
            PicturePanel picturePanel = new PicturePanel();
            control = picturePanel as Control;
            classname = control.GetType().FullName;
            //picturePanel.PreviousDocuments = this.settings.PreviousPicturePanelDocuments;
            //picturePanel.Instance.PreviousDocuments = this.settings.PreviousPicturePanelDocuments;
            //control = picturePanel as Control;
            break;
          //case "OpenGLPanel":
          //control = new OpenGLPanel() as Control;
          //OpenGLPanel openGLPanel = new OpenGLPanel();
          //openGLPanel.PreviousDocuments = this.settings.PreviousOpenGLPanelDocuments;
          //openGLPanel.Instance.PreviousDocuments = this.settings.PreviousOpenGLPanelDocuments;
          //control = openGLPanel as Control;
          //break;
          //case "AzukiEditor":
          //control = new AzukiEditor() as Control;
          //AzukiEditor azukiEditor = new AzukiEditor();
          //azukiEditor.PreviousDocuments = this.settings.PreviousAzukiEditorDocuments;
          //azukiEditor.Instance.PreviousDocuments = this.settings.PreviousAzukiEditorDocuments; ;
          //control = azukiEditor as Control;
          //break;
          case "RichTextEditor":
            //control = new RichTextEditor() as Control;
            RichTextEditor richTextEditor = new RichTextEditor();
            control = richTextEditor as Control;
            classname = control.GetType().FullName;


            //richTextEditor.PreviousDocuments = this.settings.PreviousRichTextEditorDocuments;
            //richTextEditor.Instance.PreviousDocuments = this.settings.PreviousRichTextEditorDocuments;
            control = richTextEditor as Control;
            break;
            //case "ReoGridPanel":
            //control = new ReoGridPanel() as Control;
            //ReoGridPanel reoGridPanel = new ReoGridPanel();
            //reoGridPanel.PreviousDocuments = this.settings.PreviousSpreadSheetDocuments;
            //reoGridPanel.Instance.PreviousDocuments = this.settings.PreviousSpreadSheetDocuments;
            //control = reoGridPanel as Control;
            //control = new ReoGridPanel() as Control;
            //SpreadSheet spreadSheet = new SpreadSheet();
            //spreadSheet.PreviousDocuments = this.settings.PreviousSpreadSheetDocuments;
            //spreadSheet.Instance.PreviousDocuments = this.settings.PreviousSpreadSheetDocuments;
            //control = spreadSheet as Control;
            //break;
            //case "HTMLEditor":
            //HTMLEditor htmlEditor = new HTMLEditor();
            //htmlEditor.PreviousDocuments = this.settings.PreviousHTMLEditorDocuments;
            //htmlEditor.Instance.PreviousDocuments = this.settings.PreviousHTMLEditorDocuments;
            //control = htmlEditor as Control;
            //MessageBox.Show(name);
            //break;
            //case "TreeGridView":
            //TreeGridViewPanel treeGridView = new TreeGridViewPanel();
            //treeGridViewPanel.PreviousDocuments = this.settings.PreviousTreeGridViewDocuments;
            //treeGridView.Instance.PreviousDocuments = this.settings.PreviousTreeGridViewPanelDocuments;
            //control = treeGridView as Control;
            //break;
          //case "JsonViewer":
          //JsonView jsonViewer = new JsonView();
          //treeGridViewPanel.PreviousDocuments = this.settings.PreviousTreeGridViewDocuments;
          //treeGridView.Instance.PreviousDocuments = this.settings.PreviousTreeGridViewPanelDocuments;
          //control = jsonViewer as Control;
          //break;
          default:
            try
            {
              if (path.IndexOf("@") > -1)
              {
                classname = path.Split('@')[0];
                dllpath = path.Split('@')[1];
              }
              else
              {
                dllpath = Path.Combine(dlldir, Path.GetFileNameWithoutExtension(path) + ".dll");
                try
                {
                  if (path.IndexOf("@") > -1)
                  {
                    classname = path.Split('@')[0];
                    dllpath = path.Split('@')[1];
                  }
                  else
                  {
                    dllpath = Path.Combine(dlldir, Path.GetFileNameWithoutExtension(path) + ".dll");
                    //classname = "CommonControl." + Path.GetFileNameWithoutExtension(path);
                    classname = "XMLTreeMenu.Controls." + Path.GetFileNameWithoutExtension(path);
                  }
                  assembly = Assembly.LoadFrom(dllpath);
                  type = assembly.GetType(classname);
                  control = (Control)Activator.CreateInstance(type);
                }
                catch (Exception exc)
                {
                  MessageBox.Show(Lib.OutputError(exc.Message.ToString()),
                    MethodBase.GetCurrentMethod().Name);
                }
              }
              assembly = Assembly.LoadFrom(dllpath);
              type = assembly.GetType(classname);
              control = (Control)Activator.CreateInstance(type);
            }
            catch (Exception exc)
            {
              MessageBox.Show(Lib.OutputError(exc.Message.ToString()),
                MethodBase.GetCurrentMethod().Name);
            }
            break;
        }
        control.Name = Path.GetFileNameWithoutExtension(path);
        control.Dock = DockStyle.Fill;
        
        try
        {
          // 例外発生
          if (control is IMDIForm)
          {
            this.InitializeCustomControlsInterface((IMDIForm)control);
            //Console.WriteLine("実装してる！");
          }
          //MessageBox.Show("トライはここですよ");
        }
        catch (Exception ex)
        {
          string errmsg = ex.Message.ToString();
          MessageBox.Show(errmsg, "InitializeCustomControlsInterface((IMDIForm)control)エラー");
        }
        control.Name = classname + "@" + dllpath;
        //control.AccessibleDescription = option;
        control.AccessibleDescription = classname + "@" + dllpath;
        control.AccessibleName = file;
        control.AccessibleDefaultActionDescription = this.GetType().FullName + "@" + Application.ExecutablePath;
        ((Control)control.Tag).Tag = file;

        StatusStrip statusStrip = (StatusStrip)Lib.FindChildControlByType(control, "StatusStrip");
        if (statusStrip != null)
        {
          statusStrip.Tag = PluginBase.MainForm.StatusStrip;
        }
        MenuStrip menuStrip = (MenuStrip)Lib.FindChildControlByType(control, "MenuStrip");
        if (menuStrip != null)
        {
          menuStrip.Tag = PluginBase.MainForm.MenuStrip;
        }
        ToolStrip toolStrip = (ToolStrip)Lib.FindChildControlByType(control, "ToolStrip");
        if (toolStrip != null)
        {
          toolStrip.Tag = PluginBase.MainForm.ToolStrip;
        }
        result = control;
      }
      catch (Exception ex2)
      {
        MessageBox.Show(ex2.Message.ToString(),"TreeMenu:CreateCustomDockControl:2353");
        result = null;
      }
      return result;
    }


    public Control CreateCustomControl(string path, string file, string option = "")
    {
      Control result;
      Assembly assembly = null;
      String dlldir = Path.Combine(PathHelper.BaseDir, "DockableControls");
      String dllpath = Application.ExecutablePath;//String.Empty;
      String classname = String.Empty;
      Type type = null;
      try
      {
        Control control = null;
        //switch (Path.GetFileNameWithoutExtension(path).ToLower())
        switch (path.ToLower())
        {
          case "picturepanel":
            PicturePanel picturePanel = new PicturePanel();
            control = picturePanel as Control;
            classname = control.GetType().FullName;
            break;
          case "richtexteditor":
            control = new RichTextEditor() as Control;
            classname = control.GetType().FullName;
            break;
          //case "playerpanel":
            //control = new PlayerPanel() as Control;
            //classname = control.GetType().FullName;
            //break;
          case "browser":
            //control = new Browser();
            classname = control.GetType().FullName;
            break;
          case "browserex":
            //control = new BrowserEx();
            classname = control.GetType().FullName;
            break;
          case "simplepanel":
            control = new SimplePanel();
            classname = control.GetType().FullName;
            break;
          //case "reogridpanel":
          //reoGridPanel.PreviousDocuments = this.settings.PreviousSpreadSheetDocuments;
          //reoGridPanel.Instance.PreviousDocuments = this.settings.PreviousSpreadSheetDocuments;
          //break;
          //case "openglpanel":
          //control = new XMLTreeMenu.Controls.OpenGLPanel() as Control;
          //control = new OpenGLPanel() as Control;
          //break;
          //case "HTMLEditor":
          //HTMLEditor htmlEditor = new HTMLEditor();
          //htmlEditor.PreviousDocuments = this.settings.PreviousHTMLEditorDocuments;
          //htmlEditor.Instance.PreviousDocuments = this.settings.PreviousHTMLEditorDocuments;
          //control = htmlEditor as Control;
          //MessageBox.Show(name);
          //break;
          //case "TreeGridView":
          //TreeGridViewPanel treeGridView = new TreeGridViewPanel();
          //treeGridViewPanel.PreviousDocuments = this.settings.PreviousTreeGridViewDocuments;
          //treeGridView.Instance.PreviousDocuments = this.settings.PreviousTreeGridViewPanelDocuments;
          //control = treeGridView as Control;
          //break;
          //case "JsonViewer":
          //JsonView jsonViewer = new JsonView();
          //treeGridViewPanel.PreviousDocuments = this.settings.PreviousTreeGridViewDocuments;
          //treeGridView.Instance.PreviousDocuments = this.settings.PreviousTreeGridViewPanelDocuments;
          //control = jsonViewer as Control;
          //break;
          //case "reogridpanel":
          default:
            // UVIXでは戻る 追加 Time-stamp: <2019-04-07 13:09:41 kahata>
            try
            {
              if (path.IndexOf("@") > -1)
              {
                classname = path.Split('@')[0];
                dllpath = path.Split('@')[1];
              }
              else
              {
                dllpath = Path.Combine(dlldir, Path.GetFileNameWithoutExtension(path) + ".dll");
                classname = "CommonControl." + Path.GetFileNameWithoutExtension(path);
              }
              assembly = Assembly.LoadFrom(dllpath);
              //foreach (Type pluginType in assembly.GetTypes()) MessageBox.Show(pluginType.FullName);
              //MessageBox.Show(dllpath,classname);
              type = assembly.GetType(classname);
              control = (Control)Activator.CreateInstance(type);
            }
            catch (Exception exc)
            {
              MessageBox.Show(Lib.OutputError(exc.Message.ToString()),
                MethodBase.GetCurrentMethod().Name);
            }
            break;
        }
        control.Name = Path.GetFileName(file);
        control.Dock = DockStyle.Fill;
        try
        {
          // 例外発生
          if (control is IMDIForm)
          {
            //this.InitializeCustomControlsInterface((IMDIForm)control);
            //Console.WriteLine("実装してる！");
          }
        }
        catch (Exception ex)
        {
          string errmsg = Lib.OutputError(ex.Message.ToString());
          MessageBox.Show(errmsg, MethodBase.GetCurrentMethod().Name);
        }
        control.Name = classname + "@" + dllpath;
        //control.AccessibleDescription = option;
        control.AccessibleDescription = classname + "@" + dllpath;
        control.AccessibleName = file;
        control.AccessibleDefaultActionDescription = this.GetType().FullName + "@" + Application.ExecutablePath;
        ((Control)control.Tag).Tag = file;
        StatusStrip statusStrip = (StatusStrip)Lib.FindChildControlByType(control, "StatusStrip");
        if (statusStrip != null)
        {
          //statusStrip.Tag = this.StatusStrip;
        }
        MenuStrip menuStrip = (MenuStrip)Lib.FindChildControlByType(control, "MenuStrip");
        if (menuStrip != null)
        {
         // menuStrip.Tag = this.MenuStrip;
        }
        ToolStrip toolStrip = (ToolStrip)Lib.FindChildControlByType(control, "ToolStrip");
        if (toolStrip != null)
        {
          //toolStrip.Tag = this.ToolStrip;
        }
        result = control;
      }
      catch (Exception ex2)
      {
        String errMsg = Lib.OutputError(ex2.Message.ToString());
        MessageBox.Show(errMsg, MethodBase.GetCurrentMethod().Name);
        result = null;
      }
      return result;
    }



    public string CurrentDocumentPath()
    {
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
          MessageBox.Show(ex.Message.ToString(),"TreeMenu:CurrentDocumentPath:2378");
          return "";
        }
      }
      //this.currentDocumentPath = text;
      PluginBase.MainForm.StatusLabel.Text = text;
      return text;
    }

    public DockContent CreateCustomDocument(string argstring)
    {
      DockContent result;
      try
      {
        string[] array = argstring.Split(new char[] { '|' });
        string name = array[0];
        string path = (array.Length > 1) ? array[1] : null;
        path = this.ProcessVariable(path);
        if (string.IsNullOrEmpty(path) && File.Exists(this.CurrentDocumentPath()))
        {
          path = this.CurrentDocumentPath();
        }
        Control control = this.CreateCustomDockControl(name, path);
        if (control == null)
        {
          MessageBox.Show(name + " コントロールが作成できません");
        }
        control.Dock = DockStyle.Fill;
        DockContent dockContent = PluginBase.MainForm.CreateCustomDocument(control);
        dockContent.Name = name;
        dockContent.Text = ((path != null) ? Path.GetFileName(path) : name);
        dockContent.Tag = control;

        PluginBase.MainForm.WorkingDirectory = Path.GetDirectoryName(path);
        string data = control.GetType().FullName + "!" + path;
        //this.AddPreviousCustomDocuments(data);
        dockContent.FormClosing += new FormClosingEventHandler(this.CustomDocument_FormClosing);
        result = dockContent;
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()),"TreeMenu:CreateCustomDocument:2419");
        result = null;
      }
      return result;
    }

    // TODI no link
    public DockContent CreateEditableDocument(string file, string text, int codepage)
    {
      return null;
    }

    public void OpenDocument(string file)
    {
      if (Lib.IsImageFile(file))
      {
        ActionManager.Picture(file);
        return;
      }
      if (Lib.IsWebSite(file))
      {
        ActionManager.BrowseEx(file);
        return;
      }
      if (Lib.IsSoundFile(file) || Lib.IsVideoFile(file))
      {
        //this.Player(file);
        return;
      }
      if (!Lib.IsExecutableFile(file))
      {
        PluginBase.MainForm.OpenEditableDocument(file);
        return;
      }
      /*
      if (this.executeInPanelToolStripMenuItem.Checked)
      {
        //this.ExecuteInPlace(file);
        return;
      }
      */
      Process.Start(file);
    }

    #region Test

    /// <summary>
    /// Calls a MenuTree Action method
    /// </summary>
    public Boolean CallMenuCommand(String name, String tag)
    {
      //MessageBox.Show(tag,name);
      try
      {
        ActionManager action = new ActionManager();
        Type mfType = action.GetType();

        System.Reflection.MethodInfo method = mfType.GetMethod(name);
        if (method == null) throw new MethodAccessException();
        ToolStripMenuItem button = new ToolStripMenuItem();
        
        // Bugfix 2018-03-06
        button.Tag = tag;// new ItemData(null, tag, null); // Tag is used for args
        Object[] parameters = new Object[2];
        parameters[0] = button; parameters[1] = null;
        //string[] parameters = tag.Split('|');
        method.Invoke(this, parameters);
       
        return true;
      }
      catch (Exception ex)
      {
        ErrorManager.ShowError(ex);
        return false;
      }
    }
    
    public Boolean CallMenuCommand2(Object sender, System.EventArgs e)
    {
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String argstr = (string)button.Tag;
      try
      {
        String[] args = argstr.Split('|');
        this.CallMenuCommand(args[0],args[1]);
        return true;
      }
      catch(Exception exc)
      {
        String errmsg = Lib.OutputError(exc.Message.ToString());
        MessageBox.Show(errmsg, MethodBase.GetCurrentMethod().Name);
        return false;
      }
    }
    
    /// <summary>
    /// Calls a normal XmlMenuTree method
    /// </summary>
    public Boolean CallPluginCommand(String name, String tag)
    {
      try
      {
        Type mfType = this.GetType();

        //ActionManager action = new ActionManager();
        //Type mfType = action.GetType();

        System.Reflection.MethodInfo method = mfType.GetMethod(name);
        if (method == null) throw new MethodAccessException();
        ToolStripMenuItem button = new ToolStripMenuItem();

        // Bugfix 2018-03-06
        button.Tag = tag;// new ItemData(null, tag, null); // Tag is used for args
        Object[] parameters = new Object[2];
        parameters[0] = button; parameters[1] = null;
        //string[] parameters = tag.Split('|');
        method.Invoke(this, parameters);

        return true;
      }
      catch (Exception ex)
      {
        ErrorManager.ShowError(ex);
        return false;
      }
    }

    public void Hello(object sender, EventArgs e)
    {
      ToolStripMenuItem button = sender as ToolStripMenuItem;
      String msg = (string)button.Tag;
      //NodeInfo ni = MakeNodeInfo(sender);
      MessageBox.Show("AntMenu Hello関数からのご挨拶です "+ msg,"Hello");
    }
    
    #endregion

    #endregion

    #region Icon functions

    public Hashtable _systemIcons = new Hashtable();

    private Image getImageFromIconFile(String path)
    {
      Size smallIconSize = this.GetSmallIconSize();
      Icon icon = new Icon(path);
      Image image = ImageKonverter.ImageResize(icon.ToBitmap(), smallIconSize.Width, smallIconSize.Height);
      return image;
    }

    public int GetIconImageIndex_old(string path)
    {
      string key = string.Empty;
      Size smallIconSize = this.GetSmallIconSize();
      if (Directory.Exists(path))
      {
        key = "folder";
      }
      else if (File.Exists(path))
      {
        key = Path.GetExtension(path).ToLower();
      }

      else if (Lib.IsWebSite(path))
      {
        key = path;
      }

      if (!this._systemIcons.ContainsKey(key))
      {
        Icon iconIfNecessary = this.GetIconIfNecessary(path);
        if (Lib.IsWebSite(path))
        {
          iconIfNecessary = this.getIconFromUrl(path);
        }

        Image image = ImageKonverter.ImageResize(iconIfNecessary.ToBitmap(), smallIconSize.Width, smallIconSize.Height);
        this.imageList.Images.Add(image);
        iconIfNecessary.Dispose();
        image.Dispose();
        this._systemIcons.Add(key, this.imageList.Images.Count - 1);
      }
      return (int)this._systemIcons[key];
    }

    public int GetIconImageIndex(string path)
    {
      string key = string.Empty;
      Size smallIconSize = this.GetSmallIconSize();
      if (Directory.Exists(path)) key = "folder";
      else if (File.Exists(path)) key = Path.GetExtension(path).ToLower();
      else if (Lib.IsWebSite(path)) key = path;

      if (!this._systemIcons.ContainsKey(key))
      {
        Icon iconIfNecessary = this.GetIconIfNecessary(path, true);
        if (Directory.Exists(path)) iconIfNecessary = this.GetIconIfNecessary(path, false);

        if (Lib.IsWebSite(path)) iconIfNecessary = this.getIconFromUrl(path);
        Image image = ImageKonverter.ImageResize(iconIfNecessary.ToBitmap(), smallIconSize.Width, smallIconSize.Height);
        this.imageList.Images.Add(image);
        iconIfNecessary.Dispose();
        image.Dispose();
        this._systemIcons.Add(key, this.imageList.Images.Count - 1);
      }
      return (int)this._systemIcons[key];
    }

    private Size GetSmallIconSize()
    {
      Size smallIconSize = SystemInformation.SmallIconSize;
      if (smallIconSize.Width > 16)
      {
        return new Size(18, 18);
      }
      return smallIconSize;
    }

    private int iconcount;
    public int GetIconImageIndexFromIconPath(string path)
    {
      int result;
      try
      {
        string key = string.Format("icon_{0}", this.iconcount);
        Size smallIconSize = this.GetSmallIconSize();
        if (!this._systemIcons.ContainsKey(key))
        {
          Image image;
          Icon icon;
          if (path.StartsWith("imagelist:"))
          {
            image = PluginUI.imageList2.Images[int.Parse(path.Replace("imagelist:", ""))];
            icon = Icon.FromHandle(((Bitmap)image).GetHicon());
            image = ImageKonverter.ImageResize(icon.ToBitmap(), smallIconSize.Width, smallIconSize.Height);
          }
          else if (path.StartsWith("image:"))
          {
            image = PluginBase.MainForm.FindImage(path.Replace("image:", ""));
            icon = Icon.FromHandle(((Bitmap)image).GetHicon());
            image = ImageKonverter.ImageResize(icon.ToBitmap(), smallIconSize.Width, smallIconSize.Height);
          }
          else
          {
            icon = this.getIconFromPath(path);
            image = ImageKonverter.ImageResize(icon.ToBitmap(), smallIconSize.Width, smallIconSize.Height);
          }
          this.imageList.Images.Add(image);
          icon.Dispose();
          image.Dispose();
          this._systemIcons.Add(key, this.imageList.Images.Count - 1);
        }
        this.iconcount++;
        result = (int)this._systemIcons[key];
      }
      catch
      {
        result = 0;
      }
      return result;
    }

    private Icon GetIconIfNecessary(string path)
    {
      this.GetSmallIconSize();
      Icon result = null;
      if (File.Exists(path))
      {
        result = IconExtractor.GetFileIcon(path, false, true);
      }
      else if (Lib.IsWebSite(path))
      {
        //result = getIconFromUrl(path);
      }
      else
      {
        result = IconExtractor.GetFolderIcon(path, false, true);
      }
      return result;
    }

    private Icon GetIconIfNecessary(string path, bool isFile)
    {
      Icon icon = new Icon(@"F:\icons\google.ico"); ;
      try
      {
        Size size = ScaleHelper.Scale(new Size(16, 16));
        if (PluginCore.Win32.ShouldUseWin32())
        {
          if (isFile) icon = IconExtractor.GetFileIcon(path, false, true);
          else icon = IconExtractor.GetFolderIcon(path, false, true);
          return icon;
        }
      }
      catch { } // No errors please...
      return icon;
    }

    private Icon getIconFromPath(string path)
    {
      if (File.Exists(path) && Path.GetExtension(path).ToLower() == ".ico")
      {
        return new Icon(path);
      }
      //else if (File.Exists(path) && 
      //  (Path.GetExtension(path).ToLower() == ".bmp" 
      //  || Path.GetExtension(path).ToLower() == ".png"
      //  || Path.GetExtension(path).ToLower() == ".gif"
      //  || Path.GetExtension(path).ToLower() == ".jpeg"))

      else if (File.Exists(path) && Lib.IsImageFile(path))
      {
        // 画像ファイルを読み込んで、Imageオブジェクトを作成する
        //System.Drawing.Image img = System.Drawing.Image.FromFile(path);
        Bitmap bmp = (Bitmap)Image.FromFile(path);
        return ImageHander.BitmapToIcon(bmp);
      }
      else if (Lib.IsWebSite(path))
      {
        return getIconFromUrl(path);
      }
      return null;
    }

    /// <summary>
    ///  faviconがpng画像で取得
    /// </summary>
    /// Uri favicon = new Uri("http://www.google.com/s2/favicons?domain=" + domain, UriKind.Absolute);
    /// 利用方法
    /// http://www.google.com/s2/favicons?domain=ここにドメインを指定する
    ////たったこれだけで、faviconがpng画像で取得出来るので、imgタグのsrc要素に指定すればHTML上に表示できます。
    /// Load an image from a url into a picturebox.
    /// https://stackoverflow.com/questions/4071025/load-an-image-from-a-url-into-a-picturebox
    /// ImageからIconへ変換
    /// http://blogger.weblix.net/2010/12/image-to-icon.html
    /// <param name="url"></param>
    /// <returns></returns>
    private Icon getIconFromUrl(string url)
    {
      //if(url== "http://www.yahoo.co.jp/") MessageBox.Show(url);

      Icon icon;
      //String site = "http://www.gravatar.com/avatar/6810d91caff032b202c50701dd3af745?d=identicon&r=PG";
      String site = "http://www.google.com/s2/favicons?domain=" + url;
      var request = WebRequest.Create(site);

      using (var response = request.GetResponse())
      using (var stream = response.GetResponseStream())
      {
        icon = System.Drawing.Icon.FromHandle(((System.Drawing.Bitmap)Bitmap.FromStream(stream)).GetHicon());
      }
      return icon;
    }

    private void ClearImageList()
    {
      this.imageList.Images.Clear();
      this.imageList.Dispose();
      this.imageList = new ImageList();
      this.imageList.ImageSize = this.GetSmallIconSize();
      this.imageList.ColorDepth = ColorDepth.Depth32Bit;
    }

    #endregion



    /// <summary>
    /// AntPanel treeNode 右クリック コンテキストメニューの試験 EventHandler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private void 試験ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //NodeInfo nodeInfo = this.SelectedNodeInfo();
      //String outerXML = nodeInfo.XmlNode.OuterXml;
      //String innerXML = nodeInfo.XmlNode.InnerXml;
      //if (!String.IsNullOrEmpty(innerXML)) outerXML = outerXML.Replace(innerXML, "");
      /*
      string input = "This is   text with   far  too   much   " +
                     "whitespace.";
      string pattern = "\\s+";
      string replacement = " ";
      Regex rgx = new Regex(pattern);
      string result = rgx.Replace(input, replacement);
      */
      //MessageBox.Show(outerXML,"ContextMenu 試験");
      //MessageBox.Show(GetHeadTagFromXmlNode(nodeInfo.XmlNode));
      //MessageBox.Show(nodeInfo.XmlNode.InnerText);
      //if (File.Exists(nodeInfo.Path)) Process.Start(@"C:\Program Files (x86)\sakura\sakura.exe", nodeInfo.Path);

      //XmlElement elm; // initiate it to the node you want the path of.
      //string s = "";
      //XmlNode xmlNode = nodeInfo.XmlNode;

      TreeNode treeNode = this.treeView.SelectedNode;
      treeNode = GetTopTreeNode(treeNode);
      #region coment out      
      /*
      while (treeNode.Parent != null)
      {
        NodeInfo ni;
        if (treeNode.Tag is NodeInfo)
        {
          try
          {
            ni = treeNode.Tag as NodeInfo;
            XmlNode xmlNode = ni.XmlNode;
            if (xmlNode.Name == "root" || xmlNode.Name == "project") break;
          }
          catch
          {
            break;
          }
        }
        else
        {
          if (treeNode.Tag == null) break;
          if (treeNode.TreeView.Name == "outlineTreeView")
          {
            if (treeNode.Tag is NodeInfo)
            {
              ni = treeNode.Tag as NodeInfo;
              XmlNode xmlNode = ni.XmlNode;
              if (xmlNode.Name == "root" || xmlNode.Name == "project") break;
            }
          }
          else
          {
            AntTreeNode antNode = treeNode as AntTreeNode;
            if (antNode == null) break;
            XmlNode xmlNode = null;
            if (antNode.Tag is XmlNode)
            {
              xmlNode = antNode.Tag as XmlNode;
              if (xmlNode.Name == "project" || xmlNode.Name == "package") break;
            }
            // FIXME
            //else if (antNode.Tag is TaskInfo)
            //{
            //if (((TaskInfo)antNode.Tag).Name == "GradleBuildNode") break:
            //}
            else if (antNode.Tag is String) break;
          }
        }
        treeNode = treeNode.Parent;
      }
      */
      #endregion
      MessageBox.Show(treeNode.Name);
    }


    /// <summary>
    ///  treeNode の トップノードを取得する (Includeされた場合特に有効
    /// </summary>
    /// <param name="treeNode"></param>
    /// <returns></returns>
    public TreeNode GetTopTreeNode(TreeNode treeNode)
    {
      while (treeNode.Parent != null)
      {
        NodeInfo ni;
        if (treeNode.Tag is NodeInfo)
        {
          try
          {
            ni = treeNode.Tag as NodeInfo;
            XmlNode xmlNode = ni.XmlNode;
            if (xmlNode.Name == "root" || xmlNode.Name == "project") break;
          }
          catch { break; }
        }
        else
        {
          if (treeNode.Tag == null) break;
          if (treeNode.TreeView.Name == "outlineTreeView")
          {
            if (treeNode.Tag is NodeInfo)
            {
              try
              {
                ni = treeNode.Tag as NodeInfo;
                XmlNode xmlNode = ni.XmlNode;
                if (xmlNode.Name == "root" || xmlNode.Name == "project") break;
              } catch { break; }
            }
          }
          else
          {
            AntTreeNode antNode = treeNode as AntTreeNode;
            if (antNode == null) break;
            XmlNode xmlNode = null;
            if (antNode.Tag is XmlNode)
            {
              xmlNode = antNode.Tag as XmlNode;
              if (xmlNode.Name == "project" || xmlNode.Name == "package") break;
            }
            // FIXME
            //else if (antNode.Tag is TaskInfo)
            //{
            //if (((TaskInfo)antNode.Tag).Name == "GradleBuildNode") break:
            //}
            else if (antNode.Tag is String) break;
          }
        }
        treeNode = treeNode.Parent;
      }
      return treeNode;
    }





  }
}
