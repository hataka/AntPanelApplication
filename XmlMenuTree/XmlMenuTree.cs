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
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using AntPanelApplication.CommonLibrary;
using AntPanelApplication.Managers;
using System.Collections;

namespace AntPanelApplication
{
  public partial class XmlMenuTree : UserControl
  {
    public TreeView treeView = new TreeView();
    public ImageList imageList;
    public AntPanel antPanel;
    //public RunProcessDialog runProcessDialog;
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

    public XmlMenuTree(AntPanel ui)
    {
      //this.pluginMain = ui.pluginMain;
      this.antPanel = ui;
      this.treeView = ui.treeView;
      this.imageList = ui.imageList;
      InitializeComponent();
      CreateMenus();
      InitializeXmlMenuTree();
    }

    private void InitializeXmlMenuTree()
    {
      ActionManager.menuTree = this;
      this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
      this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
      this.treeView.DoubleClick += new EventHandler(this.treeView_DoubleClick);
    }

    #region TreeView Event Handler

    private void CreateMenus()
    {
      buildFileMenu = new ContextMenuStrip();
      buildFileMenu.Items.Add("Run default target", this.antPanel.runButton.Image, this.antPanel.MenuRunClick);
      buildFileMenu.Items.Add("Edit file", null, this.antPanel.MenuEditClick);
      buildFileMenu.Items.Add(new ToolStripSeparator());


      // TODO fix
      //buildFileMenu.Items.Add("Remove",
      //PluginBase.MainForm.FindImage("153"), this.pluginUI.MenuRemoveClick);



      targetMenu = new ContextMenuStrip();
      targetMenu.Items.Add("Run target", this.antPanel.runButton.Image, this.antPanel.MenuRunClick);
      targetMenu.Items.Add("Show in Editor", null, this.antPanel.MenuEditClick);
      // 旧版 AntPlugin (session)からimport)
      targetMenu.Items.Add("Show OuterXml", null, this.antPanel.ShowOuterXmlClick);
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
      //TreeView tv = (TreeView)sender;
      TreeNode selectedNode = treeView.SelectedNode;
      this.ShowNodeInfo(selectedNode, treeView);
    }

    private void ShowNodeInfo(TreeNode treeNode)
    {
       if (treeNode != null && treeNode.Tag.GetType().Name == "NodeInfo" && treeNode.Tag != null)
      {
        NodeInfo selectedObject = new NodeInfo();
        selectedObject = (NodeInfo)treeNode.Tag;
        this.antPanel.propertyGrid1.SelectedObject = selectedObject;
      }
    }

    private void ShowNodeInfo(TreeNode treeNode, TreeView treeView)
    {
      MDIForm.ParentFormClass mainForm;
      if (treeNode != null && treeNode.Tag != null)
      {
        if (treeNode.Tag.GetType().Name == "NodeInfo")
        {
          NodeInfo selectedObject = new NodeInfo();
          selectedObject = (NodeInfo)treeNode.Tag;
          //mainForm = treeView.Tag as MDIForm.ParentFormClass;
          this.antPanel.propertyGrid1.SelectedObject = selectedObject;
        }
        else if (treeNode.Tag is TaskInfo)
        {
          TaskInfo selectedObject = new TaskInfo();
          selectedObject = (TaskInfo)treeNode.Tag;
          //mainForm = treeView.Tag as MDIForm.ParentFormClass;
          //mainForm.propertyGrid1.SelectedObject = selectedObject;
          this.antPanel.propertyGrid1.SelectedObject = selectedObject;
        }
        else if (treeNode.Tag is XmlElement)
        {
          //mainForm = treeView.Tag as MDIForm.ParentFormClass;
          //mainForm.propertyGrid1.SelectedObject = (XmlElement)treeNode.Tag;
          this.antPanel.propertyGrid1.SelectedObject = (XmlElement)treeNode.Tag;
        }
        //else if (treeNode.Tag is CSParser.Model.MemberModel)
        //{
        //mainForm = treeView.Tag as MDIForm.ParentFormClass;
        //mainForm.propertyGrid1.SelectedObject = (CSParser.Model.MemberModel)treeNode.Tag;
        //}
        else if (treeNode.Tag is String)
        {
          String path = treeNode.Tag as String;
          if (File.Exists(path))
          {
            //mainForm = treeView.Tag as MDIForm.ParentFormClass;
            //mainForm.propertyGrid1.SelectedObject = new FileInfo(path);
            this.antPanel.propertyGrid1.SelectedObject = new FileInfo(path);
          }
          else if (Directory.Exists(path))
          {
            //mainForm = treeView.Tag as MDIForm.ParentFormClass;
            //mainForm.propertyGrid1.SelectedObject = new DirectoryInfo(path);
            this.antPanel.propertyGrid1.SelectedObject = new DirectoryInfo(path);
          }
          else if (Lib.IsWebSite(path))
          {
            NodeInfo ni = new NodeInfo();
            ni.Path = path;
            //mainForm = treeView.Tag as MDIForm.ParentFormClass;
            //mainForm.propertyGrid1.SelectedObject = ni;
            this.antPanel.propertyGrid1.SelectedObject = ni;
          }
          else
          {
            NodeInfo ni2 = new NodeInfo();
            ni2.Command = path;
            //mainForm = treeView.Tag as MDIForm.ParentFormClass;
            //mainForm.propertyGrid1.SelectedObject = ni2;
            this.antPanel.propertyGrid1.SelectedObject = ni2;          }
        }
        else
        {
          //mainForm = treeView.Tag as MDIForm.ParentFormClass;
          //mainForm.propertyGrid1.SelectedObject = null;
          this.antPanel.propertyGrid1.SelectedObject = null;
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
            MessageBox.Show("ラベルには以下の文字は使用できません\n'@', ',', '!'", "TreeMenu:treeView_AfterLabelEdit:344");
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
        if (treeNode.Parent == null)
        {
          //koko
          //PluginBase.MainForm.OpenEditableDocument(((NodeInfo)treeNode.Tag).Path);
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
      ni = treeNode.Tag as NodeInfo;

      //OSの情報を取得する
      System.OperatingSystem os = System.Environment.OSVersion;
      //MessageBox.Show(os.ToString());
      if ((os.ToString()).IndexOf("Unix") >= 0)
      {
        UnixActionManager.NodeAction(ni);
        return;
      }
      else
      {
        ActionManager.NodeAction(ni);
      }
    }


    #endregion

    public TreeNode getXmlTreeNode(String file, Boolean fullNode = false)
    {
      XmlNode xmlNode = null;
      XmlDocument xmldoc = new XmlDocument();

      xmldoc.Load(file);
      xmlNode = xmldoc.DocumentElement;
      if (xmlNode.Name == "project" && fullNode == false)
      {
        return antPanel.GetBuildFileNode(file);
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
        tn.ImageIndex = tn.SelectedImageIndex = getImageIndexFromNodeInfo_safe(ni);
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

          // kokokoko 仮
          //treeNode = pluginUI.GetBuildFileNode(file);

          break;
        default:
          NodeInfo ni1 = new NodeInfo();
          ni1.Path = file;
          treeNode = new TreeNode(nodeInfo.Title, 10, 10);
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
          /// kokooko 仮
          //treeNode = pluginUI.GetBuildFileNode(file);
          break;
        default:
          NodeInfo ni1 = new NodeInfo();
          ni1.Path = file;
          treeNode = new TreeNode(nodeInfo.Title, 10, 10);
          treeNode.Tag = nodeInfo;
          treeNode.ToolTipText = file;
          break;
      }
      return treeNode;
    }

    public TreeNode loadfile(NodeInfo nodeInfo, Int32 imageIndex)
    {
      TreeNode treeNode = null;
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
      else if (Directory.Exists(nodeInfo.Path))
      {
        //treeView.Nodes.Clear();
        DirectoryInfo rootDirectoryInfo = new DirectoryInfo(nodeInfo.Path);
        TreeNode tn = this.RecursiveCreateDirectoryNode(rootDirectoryInfo);
        tn.Tag = nodeInfo;
        if (nodeInfo.Title != String.Empty) tn.Text = nodeInfo.Title;
        //if (!String.IsNullOrEmpty(nodeInfo.icon)) tn.ImageIndex = GetIconImageIndexFromIconPath(nodeInfo.icon);
        if (!String.IsNullOrEmpty(nodeInfo.Tooltip)) tn.ToolTipText = nodeInfo.Tooltip;
        if (nodeInfo.Expand == true) tn.Expand();
        if (nodeInfo.BackColor != string.Empty) tn.BackColor = Color.FromName(nodeInfo.BackColor);
        if (nodeInfo.ForeColor != string.Empty) tn.ForeColor = Color.FromName(nodeInfo.ForeColor);
        if (nodeInfo.NodeFont != string.Empty)
        {
          //this.Font = new Font("Meiryo UI", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 128);
          var cvt = new FontConverter();
          //string s = cvt.ConvertToString(this.Font);
          //MessageBox.Show(s);
          Font f = cvt.ConvertFromString(nodeInfo.NodeFont) as Font;
          tn.NodeFont = f;
        }
        if (nodeInfo.NodeChecked != string.Empty)
        {
          if (nodeInfo.NodeChecked == "true") tn.Checked = true;
          else tn.Checked = false;
        }
        return tn;
      }
      switch (Path.GetExtension(nodeInfo.Path.ToLower()))
      {
        case ".xml":
        case ".asx":
        case ".wax":
          if (nodeInfo.Option.ToLower() == "fullnode")
          {
            treeNode = this.getXmlTreeNode(nodeInfo.Path, true);
          }
          else
          {
            treeNode = this.getXmlTreeNode(nodeInfo.Path);
          }
          if (String.IsNullOrEmpty(treeNode.ToolTipText)) treeNode.ToolTipText = nodeInfo.Path;
          break;
        case ".cs":
        case ".java":
          /*
          if (!PluginUI.csOutlineTreePath.Contains(nodeInfo.Path))
          {
            PluginUI.csOutlineTreePath.Add(nodeInfo.Path);
            this.imageList.Tag = "Ant";
            treeNode = this.pluginUI.buildTree.CsOutlineTreeNode(nodeInfo.Path, this.imageList, this.pluginUI.MemberId);
            treeNode.Tag = nodeInfo;
          }
          */
          break;
        case ".fdp":
        case ".wsf":
          //treeNode = pluginUI.GetBuildFileNode(nodeInfo.Path);
          break;
        case ".gradle":
          //treeNode = pluginUI.gradleTree.GetGradleOutlineTreeNode(nodeInfo.Path);
          break;
        default:
          NodeInfo ni1 = new NodeInfo();
          ni1.Path = nodeInfo.Path;
          nodeInfo.Title = Path.GetFileName(nodeInfo.Path);
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
      int imageIndex = AntPanel.IsRunningWindows ? this.getImageIndexFromNodeInfo_safe(ni) : 39;
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
        imageIndex = AntPanel.IsRunningWindows ? this.getImageIndexFromNodeInfo_safe(ni) : 8;
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

    public NodeInfo SetNodeinfo(String path)
    {
      NodeInfo nodeInfo = new NodeInfo();

      nodeInfo.Title = Path.GetFileName(path);
      nodeInfo.Type = "file";
      nodeInfo.Path = path;
      return nodeInfo;
    }

    public NodeInfo SetNodeinfo(XmlNode xmlNode, String path)
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

    public TreeNode BuildTreeNode(NodeInfo nodeInfo, String path)
    {
      this.currentTreeMenuFilepath = path;

      Int32 imageIndex = getImageIndexFromNodeInfo_safe(nodeInfo);
      TreeNode treeNode;
      if (!String.IsNullOrEmpty(nodeInfo.Title))
      {
        treeNode = new TreeNode(this.ProcessVariable(nodeInfo.Title), imageIndex, imageIndex);
      }
      else
      {
        treeNode = new TreeNode(this.ProcessVariable(nodeInfo.xmlNode.Name), imageIndex, imageIndex);
      }
      treeNode.ToolTipText = path;
      treeNode.Tag = nodeInfo;
      return treeNode;
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
          // 「Expand」属性を取得
          if (((XmlElement)childXmlNode).GetAttribute("expand") == "true")
          {
            ni.Expand = true;
          }
          // 重要追加  kahata FIX 2019-01-25
          ni.innerText = childXmlNode.InnerXml;//"kokokokokokpko";
          ni.XmlNode = childXmlNode;
          // TreeNodeを新規作成
          TreeNode tn = new TreeNode(nodeName);


          //if (fullNode == true) tn.ToolTipText = GetHeadTagFromXmlNode(childXmlNode);



          if (ni.Tooltip != string.Empty) tn.ToolTipText = ni.Tooltip;
          if (ni.BackColor != string.Empty) tn.BackColor = Color.FromName(ni.BackColor);
          if (ni.ForeColor != string.Empty) tn.ForeColor = Color.FromName(ni.ForeColor);
          if (ni.NodeFont != string.Empty)
          {
            //this.Font = new Font("Meiryo UI", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 128);
            var cvt = new FontConverter();
            //string s = cvt.ConvertToString(this.Font);
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

              // kari
              if (!String.IsNullOrEmpty(ni.icon)) tn.ImageIndex = 38;// GetIconImageIndexFromIconPath(ni.icon);


              if (!String.IsNullOrEmpty(ni.Tooltip)) tn.ToolTipText = ni.Tooltip;
              if (ni.Expand == true) tn.Expand();
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
              if (!String.IsNullOrEmpty(ni.Tooltip)) inctn.ToolTipText = ni.Tooltip;
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

    public string ProcessVariable(string strVar)
    {
      return ActionManager.ProcessVariable(strVar);
      /*
      string arg = strVar;// string.Empty;
      try
      {
        // kokokokok セット必要
        //arg = PluginBase.MainForm.ProcessArgString(strVar);
      }
      catch { arg = strVar; }
      try
      {
        arg = arg.Replace("$(CurProjectDir)", AntPanel.projectDir);
        arg = arg.Replace("$(CurProjectName)", Path.GetFileNameWithoutExtension(AntPanel.projectPath));
        //arg = arg.Replace("$(CurProjectUrl)", Lib.Path2Url(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), "localhost"));
        arg = arg.Replace("$(Quote)", "\"");
        arg = arg.Replace("$(Dollar)", "$");
        arg = arg.Replace("$(AppDir)", Application.ExecutablePath);
        arg = arg.Replace("$(BaseDir)", Application.ExecutablePath);
        //arg = arg.Replace("$(CurSciText)", PluginBase.MainForm.CurrentDocument.SciControl.Text);
        //arg = arg.Replace("$(CurFileUrl)", Lib.Path2Url(PluginBase.MainForm.CurrentDocument.FileName, "localhost"));
      }
      catch { }
      return arg;
      */
    }

    private Int32 getImageIndexFromNodeInfo_safe(NodeInfo ni)
    {
      Int32 imageIndex = 38;
      //OSの情報を取得する
      System.OperatingSystem os = System.Environment.OSVersion;
      if (AntPanel.IsRunningUnix)
      {
        ///////////////////////////////////////////////////////////////////////////////////////////
        // icon設定
        // アイコン設定(このサンプルにはリソースファイルを入れてないので実は機能していませんが)
        if (ni.Icon != String.Empty) { } //imageIndex = GetIconImageIndexFromIconPath(ni.Icon);
        else
        {
          if (ni.Type == "folder") imageIndex = 39;
          else
          {
            String path = "";
            if (ni.PathBase != String.Empty) path = Path.Combine(ni.PathBase, ni.Path);
            else path = ni.Path;

            if (Lib.IsWebSite(path)) imageIndex = 41;// this.GetIconImageIndexFromIconPath(path);
            else if (File.Exists(path))
            {
              if (ni.Type == "root") imageIndex = 38;
              else if (ni.Action.ToLower() == "ant" || ni.Action.ToLower() == "wsf") imageIndex = 14;
              else imageIndex = 40;// this.GetIconImageIndex(ni.Path);
            }
            else
            {
              if (ni.Command != String.Empty && ni.Command != null) imageIndex = 42;
              else imageIndex = 8;
            }
          }
        }
        //MessageBox.Show(imageIndex.ToString());
        return imageIndex;
      }
      // windows
      else if(AntPanel.IsRunningWindows)
      {
        //Int32 imageIndex = 38;
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
      }
      return imageIndex; ;
    }

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
            image = AntPanel.imageList2.Images[int.Parse(path.Replace("imagelist:", ""))];
            icon = Icon.FromHandle(((Bitmap)image).GetHicon());
            image = ImageKonverter.ImageResize(icon.ToBitmap(), smallIconSize.Width, smallIconSize.Height);
          }
          else if (path.StartsWith("image:"))
          {
            //image = PluginBase.MainForm.FindImage(path.Replace("image:", ""));
            image = Image.FromFile(Path.Combine(@"F:\icons\FlashDevelop", path.Replace("image:", "") + ".png"));

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
        //Size size = ScaleHelper.Scale(new Size(16, 16));
        Size size = new Size(16, 16);
        //if (PluginCore.Win32.ShouldUseWin32())
        //{
          if (isFile) icon = IconExtractor.GetFileIcon(path, false, true);
          else icon = IconExtractor.GetFolderIcon(path, false, true);
          return icon;
        //}
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

    public Image FindImage(String data)
    {
      return FindImage(data, true);
    }

    public Image FindImage(String data, Boolean autoAdjusted)
    {
      try
      {
        lock (this) return ImageManager.GetComposedBitmap(data, autoAdjusted);
      }
      catch (Exception ex)
      {
        //ErrorManager.ShowError(ex);
        return null;
      }
    }



    #endregion

    #region ContextMenu Click Handler
    public NodeInfo SelectedNodeInfo()
    {
      return (NodeInfo)this.treeView.SelectedNode.Tag;
    }

    private void 再読み込みToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.antPanel.RefreshData();
    }

    private void コマンドプロンプトCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      String path = nodeInfo.Path;
      if (File.Exists(path))
      {
        if (System.IO.Directory.Exists(path))
        {
          if (AntPanel.IsRunningWindows)
          {
            System.IO.Directory.SetCurrentDirectory(path);
            Process.Start(@"C:\windows\system32\cmd.exe");
          }
          else if (AntPanel.IsRunningUnix)
          {
            //Process.Start("/usr/bin/nautilus", this.currentPath);
            String comm = "gnome-terminal";
            String arguments = "-e \"sh -c \'cd " + path + "; exec bash\'\"";
            Process.Start(comm, arguments);
          }
        }
        else if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
        {
          if (AntPanel.IsRunningWindows)
          {
            System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
            Process.Start(@"C:\windows\system32\cmd.exe");
          }
          else if (AntPanel.IsRunningUnix)
          {
            //Process.Start("/usr/bin/nautilus", this.currentPath);
            String comm = "gnome-terminal";
            String arguments = "-e \"sh -c \'cd " + Path.GetDirectoryName(path) + "; exec bash\'\"";
            Process.Start(comm, arguments);
          }
        }
      }
    }

    private void エクスプローラで開くXToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      //if (!File.Exists(nodeInfo.Path)) return;
      if (System.IO.Directory.Exists(nodeInfo.Path))
      {
        if (AntPanel.IsRunningWindows)
        {
          Process.Start(nodeInfo.Path);
        }
        else if (AntPanel.IsRunningUnix)
        {
          Process.Start("/usr/bin/nautilus", nodeInfo.Path);
        }
      }
      else if (System.IO.Directory.Exists(Path.GetDirectoryName(nodeInfo.Path)))
      {
        if (AntPanel.IsRunningWindows)
        {
          Process.Start(Path.GetDirectoryName(nodeInfo.Path));
        }
        else if (AntPanel.IsRunningUnix)
        {
          Process.Start("/usr/bin/nautilus", Path.GetDirectoryName(nodeInfo.Path));
        }
      }
    }

    private void コンテキストメニューToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows)
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
      else if (AntPanel.IsRunningUnix) { }
    }

    private void 実行EToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (File.Exists(nodeInfo.Path) || WebHandler.SiteExists(nodeInfo.Path))
      {
        Process.Start(nodeInfo.Path);
      }
      //if (AntPanel.IsRunningWindows) { }
      //else if (AntPanel.IsRunningUnix) { }
    }

    private void サクラエディタToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (!File.Exists(nodeInfo.Path)) return;
      if (AntPanel.IsRunningWindows)
      {
         Process.Start(@"C:\Program Files (x86)\sakura\sakura.exe", nodeInfo.Path);
      }
      else if (AntPanel.IsRunningUnix)
      {
        Process.Start("/home/kazuhiko/bin/sakura.sh", nodeInfo.Path);
      }
    }

    private void OpenDocumentToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (!File.Exists(nodeInfo.Path)) return;
      RichTextBox textBox = AntPanel.editor.Tag as RichTextBox;
      try
      {
        if (Path.GetExtension(nodeInfo.Path) == ".rtf")
        {
          AntPanel.editor.LoadFile(nodeInfo.Path);
        }
        else
        {
          textBox.Text = Lib.File_ReadToEndDecode(nodeInfo.Path);
        }
        textBox.Tag = nodeInfo.Path;
        AntPanel.editor.AddPreviousDocuments(nodeInfo.Path);
        AntPanel.editor.PopulatePreviousDocumentsMenu();
        AntPanel.editor.UpdateStatusText(nodeInfo.Path);
      }
      catch (Exception ex)
      {
        string message = ex.Message.ToString();
        MessageBox.Show(Lib.OutputError(message));
        //MessageBox.Show(ex.Message.ToString());
      }
      //if (AntPanel.IsRunningWindows) { }
      //else if (AntPanel.IsRunningUnix) { }
    }

    private void PSPadToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (!File.Exists(nodeInfo.Path)) return;
      if (AntPanel.IsRunningWindows)
      {
        Process.Start(@"C:\Program Files (x86)\PSPad editor\PSPad.exe", nodeInfo.Path);
      }
      else if (AntPanel.IsRunningUnix)
      {
        Process.Start("/home/kazuhiko/bin/pspad.sh", nodeInfo.Path);
      }
    }

    private void emacsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningUnix && File.Exists(nodeInfo.Path))
      {
        Process.Start("/usr/bin/emacs", nodeInfo.Path);
      }
    }

    private void geditToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningUnix && File.Exists(nodeInfo.Path))
      {
        Process.Start("/usr/bin/gedit", nodeInfo.Path);
      }
      //if (AntPanel.IsRunningWindows) { }
      //else if (AntPanel.IsRunningUnix) { }
    }

    private void エクスプローラToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //NodeInfo nodeInfo = this.SelectedNodeInfo();
      //if (AntPanel.IsRunningWindows) { }
      //else if (AntPanel.IsRunningUnix) { }
      this.エクスプローラで開くXToolStripMenuItem_Click(sender, e);
    }

    private void コマンドプロンプトToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows)
      {
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
      else if (AntPanel.IsRunningUnix) { }
    }

    private void chromeCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      //if (AntPanel.IsRunningWindows) { }
      //else if (AntPanel.IsRunningUnix) { }
      if (File.Exists(nodeInfo.Path))
      {
        if (AntPanel.IsRunningWindows)
        {
          Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", nodeInfo.Path);
        }
        else if (AntPanel.IsRunningUnix)
        {
          Process.Start("usr/bin/google-chrome", nodeInfo.Path);
        }
      }
    }

    private void ファイル名を指定して実行ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
    }

    private void リンクを開くToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
    }

    private void 切り取りToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
    }

    private void コピーCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
    }

    private void 貼り付けPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
    }

    private void 新規項目ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
      /*
      if (this.treeView1.SelectedNode == null)
      {
        MessageBox.Show("ノードを選択してください");
        return;
      }
      if (PluginUI.GetTagType(this.treeView1.SelectedNode) == "record" || PluginUI.GetTagType(this.treeView1.SelectedNode) == null)
      {
        MessageBox.Show("項目を追加できるのはルート又はフォルダーの下だけです。");
        return;
      }
      TreeNode treeNode = new TreeNode("新規アイテム");
      treeNode.ImageIndex = 1;
      treeNode.SelectedImageIndex = 1;
      treeNode.Tag = new NodeInfo
      {
        Type = "record",
        Title = "新規アイテム",
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
      this.treeView1.SelectedNode.Nodes.Add(treeNode);
      this.treeView1.SelectedNode = treeNode;
      this.treeView1.SelectedNode.BeginEdit();
      */
    }

    private void 新規フォルダToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }

      /*
      if (this.treeView1.SelectedNode == null)
      {
        MessageBox.Show("ノードを選択してください");
        return;
      }
      if (PluginUI.GetTagType(this.treeView1.SelectedNode) == "record" || PluginUI.GetTagType(this.treeView1.SelectedNode) == null)
      {
        MessageBox.Show("グループを追加できるのはルート又はグループの下だけです。");
        return;
      }
      TreeNode treeNode = new TreeNode("新規グループ");
      treeNode.ImageIndex = 2;
      treeNode.SelectedImageIndex = 3;
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
      this.treeView1.SelectedNode.Nodes.Add(treeNode);
      this.treeView1.SelectedNode = treeNode;
      this.treeView1.SelectedNode.BeginEdit();
      this.treeView1.SelectedNode.EndEdit(true);
      this.treeView1.LabelEdit = true;
      */
    }

    private void 名前の変更MToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //NodeInfo nodeInfo = this.SelectedNodeInfo();
      //if (AntPanel.IsRunningWindows) { }
      //else if (AntPanel.IsRunningUnix) { }
      MessageBox.Show(this.treeView.SelectedNode.Text);
    }

    private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }

      /*
      if (this.treeView1.SelectedNode != null)
      {
        if (this.treeView1.SelectedNode.Parent == null)
        {
          MessageBox.Show("ルートノードを削除することはできません");
          return;
        }
        if (MessageBox.Show("選択したノードを削除してもよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          this.treeView1.Nodes.Remove(this.treeView1.SelectedNode);
          return;
        }
      }
      else
      {
        MessageBox.Show("ノードを選択してください");
      }
      */
    }

    private void 隠した項目を表示SToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
    }

    private void 隠すHToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
    }

    private void targetToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
    }

    private void 試験ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NodeInfo nodeInfo = this.SelectedNodeInfo();
      if (AntPanel.IsRunningWindows) { }
      else if (AntPanel.IsRunningUnix) { }
    }
    #endregion
  }
}
