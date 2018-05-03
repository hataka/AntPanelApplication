using AntPlugin.CommonLibrary;
using PluginCore;
using PluginCore.Helpers;
using PluginCore.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.Diagnostics;
using AntPlugin.Managers;
using PluginCore.Managers;
using WeifenLuo.WinFormsUI.Docking;
using AntPlugin.XmlTreeMenu.Controls;
using AntPlugin.XMLTreeMenu.Controls;
using System.Reflection;

namespace AntPlugin.XmlTreeMenu 
{
  public class XmlMenuTree : UserControl
  {

    public TreeView treeView= new TreeView();
    public ImageList imageList;
    public PluginMain pluginMain;
    public PluginUI pluginUI;
    public String currentTreeMenuFilepath = String.Empty;

    //public const int ICON_FILE = 0;

    #region Constructor

    public XmlMenuTree()
    {
    }

    public XmlMenuTree(TreeView tv, ImageList imgList)
    {
      this.treeView = tv;
      this.imageList = imgList;
      InitializeComponent();
    }

    public XmlMenuTree(PluginMain pm, TreeView tv, ImageList imgList)
    {
      this.pluginMain = pm;
      this.treeView = tv;
      this.imageList = imgList;
      InitializeComponent();
    }

    public XmlMenuTree(PluginMain pm, PluginUI ui, TreeView tv, ImageList imgList)
    {
      this.pluginMain = pm;
      this.pluginUI = ui;
      this.treeView = tv;
      this.imageList = imgList;
      InitializeComponent();
    }

    public void InitializeComponent()
    {
      //this.treeView.ItemDrag += new ItemDragEventHandler(this.treeView1_ItemDrag);
      //this.treeView.DragOver += new DragEventHandler(this.treeView1_DragOver);
      //this.treeView.DragDrop += new DragEventHandler(this.treeView1_DragDrop);
      //this.treeView.KeyUp += new KeyEventHandler(this.treeView1_KeyUp);
      //this.treeView.ItemDrag += new ItemDragEventHandler(this.treeView1_ItemDrag);
      //this.treeView.MouseDown += new MouseEventHandler(this.treeView1_MouseDown);
      //this.treeView.BeforeLabelEdit += new NodeLabelEditEventHandler(this.treeView1_BeforeLabelEdit);
      //this.treeView.AfterLabelEdit += new NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
      this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
      //this.treeView.AfterExpand += new TreeViewEventHandler(this.treeView1_AfterExpand);
      //this.treeView.AfterCollapse += new TreeViewEventHandler(this.treeView1_AfterCollapse);

      //PluginUIで処理しているので二重定義になる 外す
      //this.treeView.DoubleClick += new EventHandler(this.treeView_DoubleClick);
    }

    #endregion


    #region TreeView Ivent Handler
    private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
    {

      TreeNode selectedNode = this.treeView.SelectedNode;
      this.ShowNodeInfo(selectedNode);
    }

    private void ShowNodeInfo(TreeNode treeNode)
    {
      if (treeNode != null && treeNode.Tag.GetType().Name == "NodeInfo" && treeNode.Tag != null)
      {
        NodeInfo selectedObject = new NodeInfo();
        selectedObject = (NodeInfo)treeNode.Tag;
        this.pluginUI.propertyGrid1.SelectedObject = selectedObject;
      }
    }
 
    #endregion

    public TreeNode getXmlTreeNode(String file,Boolean fullNode=false)
    {
      XmlNode xmlNode = null;
      XmlDocument xmldoc = new XmlDocument();

      xmldoc.Load(file);
      xmlNode = xmldoc.DocumentElement;
      //MessageBox.Show(xmlNode.Name);
      if(xmlNode.Name=="project")
      {
        return pluginUI.GetBuildFileNode(file);
      }
 
      NodeInfo nodeInfo = this.SetNodeinfo(xmlNode, file);

      TreeNode treeNode = this.BuildTreeNode(nodeInfo, file);

      //this.RecursiveBuildToTreeNode(xmlNode, treeNode);
      this.RecursiveBuildToTreeNode(xmlNode, treeNode, fullNode);

      if (nodeInfo.Expand)
      {
        treeNode.Expand();
      }
      //this.treeMenuFile.Add(path);

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

    public TreeNode loadfile(String file,Int32 imageIndex)
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
        TreeNode tn = new TreeNode(ni.Title,imageIndex,imageIndex);
        tn.Tag = ni;
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
            treeNode = this.getXmlTreeNode(nodeInfo.Path,true);
          }
          else treeNode = this.getXmlTreeNode(nodeInfo.Path);
          treeNode.ToolTipText = nodeInfo.Path;
          break;
        case ".cs":
        case ".java":
          if (!PluginUI.csOutlineTreePath.Contains(nodeInfo.Path))
          {
            PluginUI.csOutlineTreePath.Add(nodeInfo.Path);
            this.imageList.Tag = "Ant";
            treeNode = this.pluginUI.tree.CsOutlineTreeNode(nodeInfo.Path, this.imageList, this.pluginUI.MemberId);
            treeNode.Tag = nodeInfo;
          }
          break;
        case ".fdp":
        case ".wsf":
          treeNode = pluginUI.GetBuildFileNode(nodeInfo.Path);
          break;
        default:
          NodeInfo ni1 = new NodeInfo();
          ni1.Path = nodeInfo.Path;
          if (String.IsNullOrEmpty(nodeInfo.Title)) nodeInfo.Title = Path.GetFileName(nodeInfo.Path);
          //treeNode = new TreeNode(nodeInfo.Title, 10, 10);
          treeNode = new TreeNode(nodeInfo.Title, imageIndex, imageIndex);
          treeNode.Tag = nodeInfo;
          treeNode.ToolTipText = nodeInfo.Path;
          break;
      }
      return treeNode;
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

      TreeNode treeNode = new TreeNode(nodeInfo.Title,imageIndex,imageIndex);
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
    private void RecursiveBuildToTreeNode(XmlNode Parentnode, TreeNode ParenttreeNode,Boolean fullNode=false)
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
          ProcessVariable(((XmlElement)childXmlNode.ParentNode).GetAttribute("base"));
          ni.Title = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("title"));
          if (ni.Title == String.Empty) ni.Title = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("name"));
          if (ni.Title == String.Empty) ni.Title = this.ProcessVariable(((XmlElement)childXmlNode).GetAttribute("id"));
          if (ni.Title == String.Empty) ni.Title = ((XmlElement)childXmlNode).Name;

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
          // TreeNodeを新規作成
          TreeNode tn = new TreeNode(ni.Title);

          if (ni.Tooltip != string.Empty)
          {
            //this.treeView1.ShowNodeToolTips = true;
            tn.ToolTipText = ni.Tooltip;
          }
          else if(fullNode == true)  tn.ToolTipText = ni.XmlNode.OuterXml;
          else { }
          
          ///////////////////////////////////////////////////////////////////////////////////////////
          // icon設定
          // アイコン設定(このサンプルにはリソースファイルを入れてないので実は機能していませんが)
          //tn.ImageIndex = tn.SelectedImageIndex = this.getImageIndexFromNodeInfo(ni);
          //if (fullNode == true) tn.ImageIndex = tn.SelectedImageIndex = 5;
          //else tn.ImageIndex = tn.SelectedImageIndex = this.getImageIndexFromNodeInfo_safe(ni);
          tn.ImageIndex = tn.SelectedImageIndex = this.getImageIndexFromNodeInfo_safe(ni);

          // ノードに属性を設定
          tn.Tag = ni;
          // ノードを追加

          ////////////////////////////////////////////////////////////////////////
          if (ni.Type == "include")
          {
            string includefile = ProcessVariable(((XmlElement)childXmlNode).GetAttribute("path")) as String;

            //Int32 imageIndex = this.getImageIndexFromNodeInfo(ni);
            Int32 imageIndex = this.getImageIndexFromNodeInfo_safe(ni);
            try
            {
              TreeNode inctn = loadfile(ni, imageIndex);

              ParenttreeNode.Nodes.Add(inctn);
            }
            catch (Exception exc)
            {
              MessageBox.Show(exc.Message.ToString());
            }
          }
          else if (ni.Type != "null")
          {
            ParenttreeNode.Nodes.Add(tn);
          }
          //if (ni.Option == "hide") tn.Collapse();
          // 再帰呼び出し
          RecursiveBuildToTreeNode(childXmlNode, tn, fullNode);
        }
      }
    }

    private Int32 getImageIndexFromNodeInfo(NodeInfo ni)
    {
      Int32 imageIndex = 38;
      ///////////////////////////////////////////////////////////////////////////////////////////
      // icon設定
      // アイコン設定(このサンプルにはリソースファイルを入れてないので実は機能していませんが)
      if (ni.Icon != String.Empty)
      {
        //tn.ImageIndex = GetIconImageIndexFromIconPath(ni.Icon);// 5;
        //tn.SelectedImageIndex = GetIconImageIndexFromIconPath(ni.Icon);// 5;//
        imageIndex = GetIconImageIndexFromIconPath(ni.Icon);
      }
      else
      {
        if (ni.Type == "folder")
        {
          //tn.ImageIndex = GetIconImageIndex(@"F:\cpp");
          //tn.SelectedImageIndex = GetIconImageIndex(@"F:\cpp");
          imageIndex = GetIconImageIndex_old(@"F:\cpp");
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
            if(ni.Type=="root")
            {
              imageIndex = 38;
            }
            else if (ni.Action.ToLower() == "ant" || ni.Action.ToLower() == "wsf")
            {
              //tn.ImageIndex = tn.SelectedImageIndex = 14;
              imageIndex = 14;
            }
            /*
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
              //tn.ImageIndex = tn.SelectedImageIndex = this.GetIconImageIndex(ni.Path);
              //tn.ImageIndex = tn.SelectedImageIndex = GetIconImageIndex(ni.Path);
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
              //tn.ImageIndex = tn.SelectedImageIndex = 8;//4
              imageIndex = 8;
            }
          }
        }
      }
      return imageIndex;// 38;
    }

    /*
    private int getImageIndexFromNodeInfo_safe(NodeInfo ni)
    {
      Int32 imageIndex = 10;
      try
      {
        imageIndex = this.getImageIndexFromNodeInfo(ni);
;     }
      catch { }
      return imageIndex;
    }
    */

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

    private XmlDocument exportXmlDocument;

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
        case "launch":
          //this.AutoRunSetting(xmlNode);
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
        MessageBox.Show(ex1.Message.ToString());
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
              //this.Execute_xmlTreeMenu(nodeInfo);
            }
          }
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
          MessageBox.Show(ex1.Message.ToString());
        }
      }
    }

    #region treeView Doulbe Click Handler and Functions

    public void treeView_DoubleClick(object sender, EventArgs e)
    {
      NodeInfo ni = new NodeInfo();
      ni = this.treeView.SelectedNode.Tag as NodeInfo;
      String pathbase = PluginBase.MainForm.ProcessArgString(ni.PathBase);
      String action = PluginBase.MainForm.ProcessArgString(ni.Action);
      String command = ProcessVariable(PluginBase.MainForm.ProcessArgString(ni.Command));
      String innerText = ProcessVariable(ni.InnerText);
      String path;
      if (ni.Path.IndexOf('|') < 0)
      {
        path = ProcessVariable(Path.Combine(PluginBase.MainForm.ProcessArgString(ni.pathbase),
                  PluginBase.MainForm.ProcessArgString(ni.Path)));
      }
      else
      {
        path = ni.Path;
      }
      String icon = ProcessVariable(PluginBase.MainForm.ProcessArgString(ni.Icon));
      String args = ProcessVariable(PluginBase.MainForm.ProcessArgString(ni.args));//String.Empty;
      String option = ProcessVariable(PluginBase.MainForm.ProcessArgString(ni.Option));
      //String output = String.Empty;
      String filebody = String.Empty;
      String result = String.Empty;
      String dir = String.Empty;
      String code = String.Empty;
      DialogResult res;

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
      //this.ApplyPropertySetting();

      PluginBase.MainForm.StatusStrip.Tag = ni.Action.ToLower() + "|" + command + "|" + args + "|" + path + "|" + option;

      switch (ni.Action.ToLower())
      {
        case "menu":
          this.Menu(command + "|" + args + "|" + path + "|" + option);
          break;
        case "browse":
          //this.Browse(path);
          this.BrowseEx(path);
          //PluginBase.MainForm.CallCommand("Browse", path);
          break;
        /*
        case "ant":
          //MessageBox.Show(this.filepath);
          this.RunTarget(ni.Path, ni.Title);
          break;
        case "wsf":
          this.RunCScript(ni.Path, ni.Title);
          break;
        */
        case "conexe":
          this.RunProcess(command + "|" + args + "|" + path + "|" + "Console");
          break;
        case "winexe":
        case "runprocess":
          this.RunProcess(command + "|" + args + "|" + path + "|" + option);
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
          //this.OpenFile(command + "|" + args + "|" + path + "|" + option);
          //this.OpenDocument(path);
          PluginBase.MainForm.OpenEditableDocument(path);
          break;
        case "picture":
          this.Picture(command + "|" + args + "|" + path + "|" + option);
          break;
        //case "player":
          //this.Player(path);
          //break;

        //case "opengl":
          //this.OpenGL_Action(command);
          //break;

        case "custom":
        case "customdocument":
        case "customdoc":
          if (args == String.Empty) args = path;
          this.CreateCustomDocument(command, args);
          break;
       /* 
        //  2013-02-27 追加
        case "spreadsheet":
          this.SpreadSheet(path);
          break;
        case "executescript":
          if (command.ToLower() == "javawin")
          {
            PluginUI.Run_JavaWin(path);
            return;
          }
          else if (command.ToLower() == "javacon")
          {
            this.Run_JavaCon(path);
            return;
          }
          else if (command.ToLower() == "applet")
          {
            PluginUI.Run_Applet(path);
            return;
          }
          else if (command.ToLower() == "development")
          {
            try
            {
              PluginBase.MainForm.CallCommand("ExecuteScript", "Development;" + args);
            }
            catch (Exception exc)
            {
              MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
            }
            return;
          }
          else if (command.ToLower() == "execute")
          {
            try
            {
              PluginUI.Develop_Script(args, path);
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.Message.ToString());
            }
            return;
          }
          else
          {
            try
            {
              PluginBase.MainForm.CallCommand("ExecuteScript", "Development;" + path);
              return;
            }
            catch (Exception ex2)
            {
              MessageBox.Show(Lib.OutputError(ex2.Message.ToString()));
              return;
            }
          }
        */
        case "plugincommand":
          try
          {
            PluginBase.MainForm.CallCommand("PluginCommand", command + ";" + path);
          }
          catch (Exception exc)
          {
            MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
          }
          break;
        case "callcommand":
          if (command == String.Empty) break;
          // その他 Menu の Click  
          if (path != String.Empty)
          {
            if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
            {
              System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
            }
          }
          try
          {
            PluginBase.MainForm.CallCommand(command, args);
          }
          catch (Exception exc)
          {
            MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
          }
          break;

        //case "script":
          //this.RunScript(treeView1.SelectedNode);
          //ScriptManager.RunScript(this.treeView1.SelectedNode);
          //break;

        default:
          if (File.Exists(path))
          {
            if (Lib.IsTextFile(path))
            {
              PluginBase.MainForm.OpenEditableDocument(path);
            }
            else if (Path.GetExtension(path) == ".url")
            {
              StringBuilder sb = new StringBuilder(1024);
              IniFileHandler.GetPrivateProfileString(
                "InternetShortcut", "URL", "default", sb, (uint)sb.Capacity, path);
              PluginBase.MainForm.CallCommand("Browse", sb.ToString());
            }
            else
            {
              if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
              {
                System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
                Process.Start(path, args);
              }
            }
          }

          else if (Directory.Exists(path))
          {
            //MessageBox.Show(path);
            PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.BrowseTo;" + path);
          }


          else if (Lib.IsWebSite(path))
          {
            //PluginBase.MainForm.CallCommand("Browse", path.Replace(this.settings.DocumentRoot, this.settings.ServerRoot).Replace("\\", "/"));
            PluginBase.MainForm.CallCommand("Browse", path);

          }
          break;
      }
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
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
        return;
      }
      if (path != String.Empty)
      {
        if (System.IO.Directory.Exists(path)) dir = path;
        else if (System.IO.File.Exists(path)) dir = Path.GetDirectoryName(path);
        if (dir != String.Empty) System.IO.Directory.SetCurrentDirectory(dir);
      }
      if (command == "OpenTreeDataDialog") this.pluginMain.pluginUI.addButton_Click(null,null);
      else if (File.Exists(path))
      {
        //this.treeMenuFile.Clear();
        //this.treeView.Nodes.Clear();
        //this.LoadFile(path);
        //this.currentTreeMenuFilepath = path;
        string[] files = { path };
        this.pluginMain.AddBuildFiles(files);
      }
      else return;
    }

    public void RunProcess(String argstring)
    {
      String command = String.Empty;
      String args = String.Empty;
      String path = String.Empty;
      String option = String.Empty;
      String dir = String.Empty;

      //MessageBox.Show(argstring);

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
      ///////////////////////////////////////////////////////////////
      String result = "";

      // command前処理					
      if (command == String.Empty) command = path;
      else if (args == String.Empty) args = path;

      PluginBase.MainForm.WorkingDirectory = Path.GetDirectoryName(path);

      //実行出力
      try
      {
        if (String.IsNullOrEmpty(option))
        {
          Process.Start(command, args);
        }
        else if (option.ToLower().IndexOf("inplace") >= 0 || option.ToLower().IndexOf("inpanel") >= 0)
        {
          this.ExecuteInPlace(argstring);
        }
        else
        {
          //if (option.ToLower().IndexOf("doc") >= 0 || this.settings.IsDocumentOutput)
          if (option.ToLower().IndexOf("doc") >= 0)
          {
            result = ProcessHandler.getStandardOutput(command, args);
            PluginBase.MainForm.CallCommand("New", "");
            PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
            PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(result);
          }
          //if (option.ToLower().IndexOf("trace") >= 0 || this.settings.IsPanelOutput)
          if (option.ToLower().IndexOf("trace") >= 0)
          {
            result = ProcessHandler.getStandardOutput(command, args);
            TraceManager.Add(result);
          }
          if (option.ToLower().IndexOf("silent") >= 0)
          {
            result = ProcessHandler.getStandardOutput(command, args);
            TraceManager.Add(result);
          }
          // テストステージ
          if (option.ToLower().IndexOf("textlog") >= 0 || option.ToLower().IndexOf("richtext") >= 0)
          {
            result = ProcessHandler.getStandardOutput(command, args);
            String title = Path.GetFileNameWithoutExtension(command);
            this.CreateCustomDocument("RichTextEditor", "[出力]" + command + "!" + result);
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



    public void Picture(String argstring)
    {
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

      //if (File.Exists(path))
      //{
      //	PluginBase.MainForm.CallCommand("PluginCommand",
      //		"FileExplorer.BrowseTo;" + Path.GetDirectoryName(path)); //OK
      //}

      System.Windows.Forms.PictureBox pictureBox2 = new System.Windows.Forms.PictureBox();
      pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

      //pictureBox2.BackgroundImageLayout = ImageLayout.Tile;
      //pictureBox2.BackgroundImage
      //    = System.Drawing.Image.FromFile(Path.Combine(PathHelper.BaseDir, @"SettingData\grid1.png"));

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
              pictureBox2.DoubleClick += new System.EventHandler(this.pictureBox2_DoubleClick);
            }
            else if (File.Exists(command))
            {
              pictureBox2.Image = System.Drawing.Image.FromFile(command);
              pictureBox2.Tag = command;
              pictureBox2.DoubleClick += new System.EventHandler(this.pictureBox2_DoubleClick);
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
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
        pictureBox2.Image = null;// System.Drawing.Image.FromFile(path);
        pictureBox2.Tag = null;// path;
      }
      DockContent document2 = PluginBase.MainForm.CreateCustomDocument(pictureBox2);
      document2.Text = Path.GetFileName((String)pictureBox2.Tag).Replace("qcgraph!", "");
      //String data = document2.Text + "|" + pictureBox2.GetType().FullName + "|" + pictureBox2.Tag.ToString();
      //String data = pictureBox2.Tag.ToString();
      //this.AddPreviousCustomDocuments(data);
      //document2.FormClosing += new FormClosingEventHandler(this.CustomDocument_FormClosing);
    }


    private void pictureBox2_DoubleClick(object sender, EventArgs e)
    {
      System.Windows.Forms.PictureBox item = (System.Windows.Forms.PictureBox)sender;
      String path = item.Tag as String;
      Process.Start(path);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    public void BrowseEx(String path)
    {
      String url = string.Empty;
      path = ProcessVariable(path);
      //MessageBox.Show(path);
      try
      {
        if (Path.GetExtension(path) == ".url")
        {
          StringBuilder sb = new StringBuilder(1024);
          IniFileHandler.GetPrivateProfileString("InternetShortcut", "URL", "default", sb, (uint)sb.Capacity, path);
          url = sb.ToString();
        }
        else
        {
          if (Path.GetExtension(path).ToLower() == ".php")
          {
            url = path.Replace(@"C:\Apache2.2\htdocs", "http://localhost").Replace("\\", "/");
          }
          else url = path;
        }
        Browser browser = new Browser();
        browser.Dock = DockStyle.Fill;
        DockContent document = PluginBase.MainForm.CreateCustomDocument(browser);
        //String data = document.Text + "|" + browser.GetType().FullName + "|" + path;
        //this.AddPreviousCustomDocuments(path);
        //document.FormClosing += new FormClosingEventHandler(this.CustomDocument_FormClosing);
        if (url.Trim() != "") browser.WebBrowser.Navigate(url);
        else browser.WebBrowser.GoHome();
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
      }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    public void ExecuteInPlace(String argstring)
    {
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
        SimplePanel simplePanel = new SimplePanel(this.pluginUI);
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
        document.FormClosing += new FormClosingEventHandler(this.CustomDocument_FormClosing);
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()));
      }
    }


    #endregion

    public NodeInfo SelectedNodeInfo()
    {
      return (NodeInfo)this.treeView.SelectedNode.Tag;
    }


    #region Custom Document

    /*
    public void InitializeCustomControlsInterface(IMDIForm control)
    {
      control.MainForm = new ParentFormClass
      {
        Instance = (Form)PluginBase.MainForm,
        //containerDockContent = control as DockContent,
        toolStrip = PluginBase.MainForm.ToolStrip,
        menuStrip = PluginBase.MainForm.MenuStrip,
        statusStrip = PluginBase.MainForm.StatusStrip,
        xmlTreeMenu_pluginUI = this,
        settings = this.pluginMain.Settings,
        callPluginCommand = this.CallPluginCommand
      };
    }
    */
    public DockContent CreateCustomDocument(string name, string file)
    {
      DockContent result;
      try
      {
        Control control = this.CreateCustomDockControl(name, file);
        //if (control == null) MessageBox.Show("ぬるですよ");
        control.Dock = DockStyle.Fill;
        DockContent dockContent = PluginBase.MainForm.CreateCustomDocument(control);
        dockContent.Name = Path.GetFileNameWithoutExtension(name);
        dockContent.Tag = control;
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
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()));
        result = null;
      }
      return result;
    }

    private void CustomDocument_FormClosing(object sender, EventArgs e)
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

    public Control CreateCustomDockControl(string path, string file)
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
          case "OpenGLPanel":
            //control = new OpenGLPanel() as Control;
            //OpenGLPanel openGLPanel = new OpenGLPanel();
            //openGLPanel.PreviousDocuments = this.settings.PreviousOpenGLPanelDocuments;
            //openGLPanel.Instance.PreviousDocuments = this.settings.PreviousOpenGLPanelDocuments;
            //control = openGLPanel as Control;
            break;
          case "AzukiEditor":
            //control = new AzukiEditor() as Control;
            //AzukiEditor azukiEditor = new AzukiEditor();
            //azukiEditor.PreviousDocuments = this.settings.PreviousAzukiEditorDocuments;
            //azukiEditor.Instance.PreviousDocuments = this.settings.PreviousAzukiEditorDocuments; ;
            //control = azukiEditor as Control;
            break;
          case "RichTextEditor":
            control = new RichTextEditor() as Control;
            RichTextEditor richTextEditor = new RichTextEditor();
            //richTextEditor.PreviousDocuments = this.settings.PreviousRichTextEditorDocuments;
            //richTextEditor.Instance.PreviousDocuments = this.settings.PreviousRichTextEditorDocuments;
            control = richTextEditor as Control;
            break;
          case "ReoGridPanel":
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
            break;
          case "HTMLEditor":
            //HTMLEditor htmlEditor = new HTMLEditor();
            //htmlEditor.PreviousDocuments = this.settings.PreviousHTMLEditorDocuments;
            //htmlEditor.Instance.PreviousDocuments = this.settings.PreviousHTMLEditorDocuments;
            //control = htmlEditor as Control;
            //MessageBox.Show(name);
            break;
          case "TreeGridView":
            //TreeGridViewPanel treeGridView = new TreeGridViewPanel();
            //treeGridViewPanel.PreviousDocuments = this.settings.PreviousTreeGridViewDocuments;
            //treeGridView.Instance.PreviousDocuments = this.settings.PreviousTreeGridViewPanelDocuments;
            //control = treeGridView as Control;
            break;
          case "JsonViewer":
            //JsonView jsonViewer = new JsonView();
            //treeGridViewPanel.PreviousDocuments = this.settings.PreviousTreeGridViewDocuments;
            //treeGridView.Instance.PreviousDocuments = this.settings.PreviousTreeGridViewPanelDocuments;
            //control = jsonViewer as Control;
            break;
          default:
            //// 未完成
            String dllpath = Path.Combine(PathHelper.BaseDir + @"\DockableControls", Path.GetFileNameWithoutExtension(path) + ".dll");
            Assembly assembly = null;
            if (Path.GetExtension(path) == ".dll" && File.Exists(path))
            {
              assembly = Assembly.LoadFrom(path);
            }
            else if(File.Exists(dllpath))
            {
              assembly = Assembly.LoadFrom(dllpath);
            }
            else
            {
              MessageBox.Show("dllのパスが存在しません");
              return null;
            }
            Type type = assembly.GetType("XMLTreeMenu.Controls." + Path.GetFileNameWithoutExtension(path));
  ;         control = (UserControl)Activator.CreateInstance(type);
             break;
        }
        control.Name = Path.GetFileNameWithoutExtension(path);
        control.Dock = DockStyle.Fill;
        /*
        try
        {
          this.InitializeCustomControlsInterface((IMDIForm)control);
        }
        catch (Exception ex)
        {
          ex.Message.ToString();
        }
        */
        control.AccessibleDescription = this.MakeQueryString(Path.GetFileNameWithoutExtension(path));
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
        MessageBox.Show(ex2.Message.ToString());
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
          MessageBox.Show(ex.Message.ToString());
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
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()));
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
        this.Picture(file);
        return;
      }
      if (Lib.IsWebSite(file))
      {
        this.BrowseEx(file);
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
      Icon result=null;
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
      else if (Lib.IsWebSite(path))
      {
        return getIconFromUrl(path);
      }
      return null;
    }

    /// <summary>
    /// BitmapImage img = new BitmapImage();
    /// Uri favicon = new Uri("http://www.google.com/s2/favicons?domain=" + domain, UriKind.Absolute);
    /// 利用方法
    /// http://www.google.com/s2/favicons?domain=ここにドメインを指定する
    ////たったこれだけで、faviconがpng画像で取得出来るので、imgタグのsrc要素に指定すればHTML上に表示できます。
    /// Load an image from a url into a picturebox.
    /// https://stackoverflow.com/questions/4071025/load-an-image-from-a-url-into-a-picturebox
    /// ImageからIconへ変換
    /// http://blogger.weblix.net/2010/12/image-to-icon.html
    /// </summary>
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

  }
}
