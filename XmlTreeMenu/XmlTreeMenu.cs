using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Net;
using System.Diagnostics;
using System.Reflection;
using System.Collections;

using PluginCore;
using PluginCore.Helpers;
using PluginCore.Managers;
using WeifenLuo.WinFormsUI.Docking;

using PluginCore.Utilities;
using AntPlugin.CommonLibrary;
using AntPlugin.XmlTreeMenu.Managers;
using AntPlugin.XMLTreeMenu.Controls;
using AntPlugin.XMLTreeMenu.Dialogs;
using PluginCore.Controls;
using ScintillaNet;
using System.Text.RegularExpressions;
using PluginCore.FRService;
using MDIForm;

namespace AntPlugin.XmlTreeMenu
{

  public partial class XmlMenuTree : UserControl
  {
    public TreeView treeView = new TreeView();
    public ImageList imageList;
    public PluginMain pluginMain;
    public PluginUI pluginUI;
    public RunProcessDialog runProcessDialog;
    public OpenFileDialog openFileDialog;
    public String currentTreeMenuFilepath = String.Empty;

    private ContextMenuStrip buildFileMenu;
    private ContextMenuStrip targetMenu;

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

    private void InitializeXmlMenuTree()
    {
      ActionManager.menuTree = this;

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
          this.ToolStripMenuItemAddrecord.Visible = false;
          this.ToolStripMenuItemAddfolder.Visible = false;
          return;
        }
        this.ToolStripMenuItemAddrecord.Visible = true;
        this.ToolStripMenuItemAddfolder.Visible = true;
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
      this.RecursiveBuildToTreeNode(xmlNode, treeNode, fullNode);

      if (nodeInfo.Expand)
      {
        treeNode.Expand();
      }
       return treeNode;
    }

    public TreeNode loadfile(String file)
    {
      TreeNode treeNode = null;
      NodeInfo nodeInfo = this.SetNodeinfo(file);

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
      NodeInfo nodeInfo = this.SetNodeinfo(file);

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
            //MessageBox.Show(nodeInfo.Option);
            treeNode = this.getXmlTreeNode(nodeInfo.Path, true);
          }
          else
          {
            //MessageBox.Show(nodeInfo.Path);
            treeNode = this.getXmlTreeNode(nodeInfo.Path);
          }
          if(String.IsNullOrEmpty(treeNode.ToolTipText))treeNode.ToolTipText = nodeInfo.Path;
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
          treeNode = pluginUI.GetBuildFileNode(nodeInfo.Path);
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
      return treeNode;
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

        fileNode.Tag = ni;
        fileNode.ToolTipText = ni.Path;

        //directoryNode.Nodes.Add(new TreeNode(file.Name));
        directoryNode.Nodes.Add(fileNode);
      }
      return directoryNode;
    }

    /// <summary>
    /// Option #2: Non-recursive approach:
    /// </summary>
    /// https://stackoverflow.com/questions/6239544/populate-treeview-with-file-system-directory-structure
    /// <param name="treeView"></param>
    /// <param name="path"></param>
    private void ListDirectory(TreeView treeView, string path)
    {
      treeView.Nodes.Clear();

      var stack = new Stack<TreeNode>();
      var rootDirectory = new DirectoryInfo(path);
      var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
      stack.Push(node);

      while (stack.Count > 0)
      {
        var currentNode = stack.Pop();
        var directoryInfo = (DirectoryInfo)currentNode.Tag;
        foreach (var directory in directoryInfo.GetDirectories())
        {
          var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
          currentNode.Nodes.Add(childDirectoryNode);
          stack.Push(childDirectoryNode);
        }
        foreach (var file in directoryInfo.GetFiles())
          currentNode.Nodes.Add(new TreeNode(file.Name));
      }

      treeView.Nodes.Add(node);
    }

    private NodeInfo SetNodeinfo(String path)
    {
      NodeInfo nodeInfo = new NodeInfo();

      nodeInfo.Title = Path.GetFileName(path);
      nodeInfo.Type = "file";
      nodeInfo.Path = path;
      /*
      nodeInfo.PathBase = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("base"));
      nodeInfo.Action = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("action"));
      nodeInfo.Command = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("command"));
      //nodeInfo.Path = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("path"));
      nodeInfo.Path = path;
      nodeInfo.Args = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("args"));
      nodeInfo.Option = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("option"));
      nodeInfo.XmlNode = xmlNode;
      nodeInfo.Icon = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("icon"));
      if (((XmlElement)xmlNode).GetAttribute("expand") == "true")
      {
        nodeInfo.Expand = true;
      }
      */
      return nodeInfo;
    }

    private NodeInfo SetNodeinfo(XmlNode xmlNode, String path)
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
      nodeInfo.Path = path;
      nodeInfo.Args = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("args"));
      nodeInfo.Option = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("option"));
      nodeInfo.XmlNode = xmlNode;
      nodeInfo.Icon = this.ProcessVariable(((XmlElement)xmlNode).GetAttribute("icon"));
      if (((XmlElement)xmlNode).GetAttribute("expand") == "true")
      {
        nodeInfo.Expand = true;
      }
      return nodeInfo;
    }

    private TreeNode BuildTreeNode(NodeInfo nodeInfo, String path)
    {
      this.currentTreeMenuFilepath = path;

      //Int32 imageIndex = getImageIndexFromNodeInfo(nodeInfo);
      Int32 imageIndex = getImageIndexFromNodeInfo_safe(nodeInfo);

      TreeNode treeNode = new TreeNode(nodeInfo.Title, imageIndex, imageIndex);
      treeNode.Tag = nodeInfo;
      return treeNode;
    }

    public string ProcessVariable(string strVar)
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
              this.ProcessNode(childXmlNode);
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
          //ProcessVariable(((XmlElement)childXmlNode.ParentNode).GetAttribute("base"));

          String nodeName = String.Empty;

          ni.Title = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("title"));
          if (!String.IsNullOrEmpty(ni.Title)) nodeName = ni.Title;
          if (String.IsNullOrEmpty(nodeName)) nodeName = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("name"));
          if (String.IsNullOrEmpty(nodeName)) nodeName = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("id"));
          if (String.IsNullOrEmpty(nodeName)) nodeName = ((XmlElement)childXmlNode).Name;

          if (((XmlElement)childXmlNode.ParentNode).GetAttribute("base") != String.Empty
            && ((XmlElement)childXmlNode).GetAttribute("base") == String.Empty)
          {
            ni.PathBase = ProcessVariable(((XmlElement)childXmlNode.ParentNode).GetAttribute("base"));
          }
          else
          {
            ni.PathBase = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("base"));
          }
          ni.Action = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("action"));

          if (childXmlNode.Name == "target") ni.Action = "Ant";
          if (childXmlNode.Name == "job") ni.Action = "Wsf";

          ni.Command = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("command"));
          ni.Path = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("path"));

          //if (childXmlNode.Name == "target" || childXmlNode.Name == "job") ni.Path = this.currentTreeMenuFilepath;

          ni.Icon = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("icon"));
          ni.Args = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("args"));
          ni.Tooltip = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("tooltip"));

          ni.BackColor = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("backcolor"));
          ni.ForeColor = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("forecolor"));
          ni.NodeFont = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("nodefont"));
          ni.NodeChecked = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("nodechecked"));


          ni.Option = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("option"));

          //if (fullNode == true) ni.Tooltip = GetTitleFromXmlNode(childXmlNode);

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
          // TreeNodeを新規作成
          //TreeNode tn = new TreeNode(ni.Title);
          TreeNode tn = new TreeNode(nodeName);

          if (fullNode == true) tn.ToolTipText = GetTitleFromXmlNode(childXmlNode);

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


              if (!String.IsNullOrEmpty(ni.Title)) tn.Text = ni.Title;
              if (!String.IsNullOrEmpty(ni.icon)) tn.ImageIndex = GetIconImageIndexFromIconPath(ni.icon);
              if (!String.IsNullOrEmpty(ni.Tooltip)) tn.ToolTipText = ni.Tooltip;
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

    private String GetTitleFromXmlNode(XmlNode node)
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

    public void ToolBarSettings_try(XmlNode xmlNode)
    {
      //MessageBox.Show(this.currentTreeMenuFilepath);
      try
      {
        dynamicToolStrip = StripBarManager.GetToolStripFromString(xmlNode.OuterXml);
        if (!toolStripList.Contains(dynamicToolStrip))
        {
          ToolStripManager.Merge(dynamicToolStrip, PluginBase.MainForm.ToolStrip);
          toolStripList.Add(dynamicToolStrip);
        }
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.Message.ToString(),"TreeMenu:ToolBarSettings_try:1164");
      }
    }

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







    /*
     * 
     * 
     * 
     * 
     *


    public static List<String> ToolBarSettingsFiles = new List<string>();
    public void ToolBarSettings(XmlNode xmlNode)
    {
      //MessageBox.Show(this.currentTreeMenuFilepath);
      try
      {
        if (!ToolBarSettingsFiles.Contains(this.currentTreeMenuFilepath))
        {
          ToolStrip toolStrip = StripBarManager.GetToolStripFromString(xmlNode.OuterXml);
          ToolStripManager.Merge(toolStrip, PluginBase.MainForm.ToolStrip);
          ToolBarSettingsFiles.Add(this.currentTreeMenuFilepath);
        }
      }
      catch (Exception ex1)
      {
        MessageBox.Show(ex1.Message.ToString());
      }
    }

    public static List<String> ProjectSettingsFiles = new List<string>();
    public void ProjectSettings(XmlNode parentNode)
    {
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
        this.settings.DocumentRoot = @"C:\Apache2.2\htdocs";
        this.settings.ServerRoot = "http://localhost";
      }
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
          MessageBox.Show(ex1.Message.ToString());
        }
      }
    }










    
     * 
     * 
     * 
     * 
     */


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

    private void toolStripMenuItem2_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path)) Process.Start(@"C:\Program Files (x86)\sakura\sakura.exe", nodeInfo.Path);
    }

    private void toolStripMenuItem3_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();

      if (File.Exists(nodeInfo.Path))
      {
        //Globals.MainForm.OpenEditableDocument(((NodeInfo)treeView.SelectedNode.Tag).Path, false);
        PluginBase.MainForm.OpenEditableDocument(nodeInfo.Path, false);
      }
    }

    private void toolStripMenuItem4_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path))
      {
        Process.Start(@"C:\Program Files (x86)\PSPad editor\PSPad.exe", nodeInfo.Path);
      }
    }

    private void toolStripMenuItem7_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path))
      {
        Process.Start(@"C:\windows\explorer.exe", Path.GetDirectoryName(nodeInfo.Path));
      }
    }

    private void toolStripMenuItem8_Click(object sender, EventArgs e)
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

    private void toolStripMenuItem9_Click(object sender, EventArgs e)
    {

    }

    private void toolStripMenuItem10_Click(object sender, EventArgs e)
    {

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
      PluginBase.MainForm.OpenEditableDocument(((NodeInfo)node.Tag).Path, false);
      ScintillaControl sci = PluginBase.MainForm.CurrentDocument.SciControl;
      XmlNode xmlNode = ((NodeInfo)node.Tag).xmlNode;
      String outerXml = xmlNode.OuterXml;
      String text = sci.Text;
      Regex regexp = new Regex("<job[^>]+id\\s*=\\s*\"" + ((NodeInfo)node.Tag).Title + "\".*>", RegexOptions.CultureInvariant);
      Match match = regexp.Match(text);
      MessageBox.Show(match.Value);
      if (match != null)
      {
        this.FindNext(match.Value, false);
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
            //control = new PicturePanel() as Control;
            PicturePanel picturePanel = new PicturePanel();

            //picturePanel.PreviousDocuments = this.settings.PreviousPicturePanelDocuments;
            //picturePanel.Instance.PreviousDocuments = this.settings.PreviousPicturePanelDocuments;

            //koko
            //picturePanel.XmlTreeMenu = this.pluginMain;
            control = picturePanel as Control;
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
            control = new RichTextEditor() as Control;
            RichTextEditor richTextEditor = new RichTextEditor();
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
            //// 未完成
            String dlldir = PathHelper.BaseDir + @"\DockableControls";
            String dllpath = Path.Combine(dlldir, path + ".dll");
            Assembly assembly = null;
            Type type = null;
            if (Path.GetExtension(path) == ".dll")
            {
              if (File.Exists(path))
              {
                assembly = Assembly.LoadFrom(path);
                type = assembly.GetType("XMLTreeMenu.Controls." + Path.GetFileNameWithoutExtension(path));
                control = (UserControl)Activator.CreateInstance(type);
              }
              else if (File.Exists(Path.Combine(dlldir, path)))
              {
                assembly = Assembly.LoadFrom(Path.Combine(dlldir, path));
                type = assembly.GetType("XMLTreeMenu.Controls." + Path.GetFileNameWithoutExtension(path));
                control = (UserControl)Activator.CreateInstance(type);
              }
              else
              {
                MessageBox.Show("dllのパスが存在しません");
                return null;
              }
            }
            else
            {
              if (File.Exists(dllpath))
              {
                assembly = Assembly.LoadFrom(dllpath);
                type = assembly.GetType("XMLTreeMenu.Controls." + Path.GetFileNameWithoutExtension(path));
                control = (UserControl)Activator.CreateInstance(type);
              }
              else
              {
                MessageBox.Show("dllのパス [" + path + "] が存在しません");
                return null;
              }
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

        //control.AccessibleDescription = this.MakeQueryString(Path.GetFileNameWithoutExtension(path));
        control.AccessibleDescription = option;
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
        //Type mfType = this.GetType();

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
        String errmsg = exc.Message.ToString();
        MessageBox.Show(errmsg, "CallMenuCommand Handler");
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

    private void 試験ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      String outerXML = nodeInfo.XmlNode.OuterXml;
      String innerXML = nodeInfo.XmlNode.InnerXml;
      if (!String.IsNullOrEmpty(innerXML)) outerXML = outerXML.Replace(innerXML, "");

      /*
      string input = "This is   text with   far  too   much   " +
                     "whitespace.";
      string pattern = "\\s+";
      string replacement = " ";
      Regex rgx = new Regex(pattern);
      string result = rgx.Replace(input, replacement);
      */

      MessageBox.Show(outerXML);
      MessageBox.Show(nodeInfo.XmlNode.InnerText);
      //if (File.Exists(nodeInfo.Path)) Process.Start(@"C:\Program Files (x86)\sakura\sakura.exe", nodeInfo.Path);
      
    }
  }
}
