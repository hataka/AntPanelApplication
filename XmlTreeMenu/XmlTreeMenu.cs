using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using AntPanelApplication;
using System.IO;
using System.Collections;
using AntPlugin.CommonLibrary;
using System.Net;
using CommonLibrary;
using AntPlugin.XmlTreeMenu.Managers;

namespace AntPlugin.XmlTreeMenu
{
  public partial class XmlMenuTree : UserControl
  {
    public AntPanel antPanel;
    public TreeView treeView;
    public ImageList imageList;
    public String currentTreeMenuFilepath = String.Empty;

    private ContextMenuStrip buildFileMenu;
    private ContextMenuStrip targetMenu;
    
    public XmlMenuTree()
    {
      InitializeComponent();
    }
    
    public XmlMenuTree(AntPanel ui)
    {
      this.antPanel = ui;
      this.treeView = ui.treeView;
      this.imageList = ui.imageList;
      InitializeComponent();
      CreateMenus();
      InitializeXmlMenuTree();
    }

    private void CreateMenus()
    {
      buildFileMenu = new ContextMenuStrip();
      buildFileMenu.Items.Add("Run default target", this.antPanel.runButton.Image, this.antPanel.MenuRunClick);
      buildFileMenu.Items.Add("Edit file", null, this.antPanel.MenuEditClick);
      buildFileMenu.Items.Add(new ToolStripSeparator());
      buildFileMenu.Items.Add("Remove",this.antPanel.removeButton.Image, this.antPanel.MenuRemoveClick);
      targetMenu = new ContextMenuStrip();
      targetMenu.Items.Add("Run target", this.antPanel.runButton.Image, this.antPanel.MenuRunClick);
      targetMenu.Items.Add("Show in Editor", null, this.antPanel.MenuEditClick);
      // 旧版 AntPlugin (session)からimport)
      targetMenu.Items.Add("Show OuterXml", null, this.antPanel.ShowOuterXmlClick);
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
      //this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
      //this.treeView.AfterExpand += new TreeViewEventHandler(this.treeView_AfterExpand);
      //this.treeView.AfterCollapse += new TreeViewEventHandler(this.treeView_AfterCollapse);
      //this.treeView.Click += new TreeViewEventHandler(this.treeView_Click);
      //this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
      //this.treeView.DoubleClick += new EventHandler(this.treeView_DoubleClick);
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
        DirectoryInfo rootDirectoryInfo = new DirectoryInfo(nodeInfo.Path);
        TreeNode tn = this.RecursiveCreateDirectoryNode(rootDirectoryInfo);
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
          if (!AntPanel.csOutlineTreePath.Contains(nodeInfo.Path))
          {
            AntPanel.csOutlineTreePath.Add(nodeInfo.Path);
            this.imageList.Tag = "Ant";
            treeNode = this.antPanel.buildTree.CsOutlineTreeNode(nodeInfo.Path, this.imageList, this.antPanel.MemberId);
            treeNode.Tag = nodeInfo;
          }
          break;
        case ".fdp":
        case ".wsf":
          treeNode = antPanel.GetBuildFileNode(nodeInfo.Path);
          break;
        case ".gradle":
          treeNode = antPanel.gradleTree.GetGradleOutlineTreeNode(nodeInfo.Path);
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
      return treeNode;
    }

    public TreeNode getXmlTreeNodeFromString(String xml, String file)
    {
      XmlNode xmlNode = null;
      XmlDocument xmldoc = new XmlDocument();
      xmldoc.LoadXml(xml);
      xmlNode = xmldoc.DocumentElement;
      NodeInfo nodeInfo = this.SetNodeinfo(xmlNode, file);
      TreeNode treeNode = this.BuildTreeNode(nodeInfo, file);
      this.RecursiveBuildToTreeNode(xmlNode, treeNode, false);
      treeNode.Tag = nodeInfo;
      treeNode.ToolTipText = file;
      return treeNode;
    }



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

    private TreeNode BuildTreeNode(NodeInfo nodeInfo, String path)
    {
      this.currentTreeMenuFilepath = path;

      // TODO アイコン設定
      Int32 imageIndex = getImageIndexFromNodeInfo_safe(nodeInfo);
      //Int32 imageIndex = 0;//仮
 
      TreeNode treeNode = new TreeNode(nodeInfo.Title, imageIndex, imageIndex);
      treeNode.Tag = nodeInfo;
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

      // TODO アイコン設定
      //int imageIndex = this.GetIconImageIndex(@"C:\windows");
      int imageIndex = 0; //仮

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

        // TODO アイコン設定
        //imageIndex = this.GetIconImageIndex(ni.Path);
        imageIndex = 0; //仮

        TreeNode fileNode = new TreeNode(ni.Title, imageIndex, imageIndex);

        fileNode.Tag = ni;
        fileNode.ToolTipText = ni.Path;

        directoryNode.Nodes.Add(fileNode);
      }
      return directoryNode;
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
          // TreeNodeを新規作成
          TreeNode tn = new TreeNode(nodeName);

          if (fullNode == true) tn.ToolTipText = GetTitleFromXmlNode(childXmlNode);
          if (ni.Tooltip != string.Empty) tn.ToolTipText = ni.Tooltip;
          if (ni.BackColor != string.Empty) tn.BackColor = Color.FromName(ni.BackColor);
          if (ni.ForeColor != string.Empty) tn.ForeColor = Color.FromName(ni.ForeColor);
          if (ni.NodeFont != string.Empty)
          {
            var cvt = new FontConverter();
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

          // TODO 実装
          tn.ImageIndex = tn.SelectedImageIndex = this.getImageIndexFromNodeInfo_safe(ni);
          
          // ノードに属性を設定
          tn.Tag = ni;
          // ノードを追加

          ////////////////////////////////////////////////////////////////////////
          if (ni.Type == "include")
          {
            // TODO アイコン設定
            Int32 imageIndex = this.getImageIndexFromNodeInfo_safe(ni);
            //Int32 imageIndex = 5;
            try
            {
              TreeNode inctn = loadfile(ni, imageIndex);


              if (!String.IsNullOrEmpty(ni.Title)) tn.Text = ni.Title;
              // TODO アイコン設定
              if (!String.IsNullOrEmpty(ni.icon)) tn.ImageIndex = GetIconImageIndexFromIconPath(ni.icon);
              //if (!String.IsNullOrEmpty(ni.icon)) tn.ImageIndex = 4;// GetIconImageIndexFromIconPath(ni.icon);


              if (!String.IsNullOrEmpty(ni.Tooltip)) tn.ToolTipText = ni.Tooltip;
              if (ni.BackColor != string.Empty) inctn.BackColor = Color.FromName(ni.BackColor);
              if (ni.ForeColor != string.Empty) inctn.ForeColor = Color.FromName(ni.ForeColor);
              if (ni.NodeFont != string.Empty)
              {
                var cvt = new FontConverter();
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
    public static List<ToolStrip> toolStripList = new List<ToolStrip>();
    public static ToolStrip dynamicToolStrip = new ToolStrip();

    public void ToolBarSettings_try(XmlNode xmlNode)
    {
      /*
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
        MessageBox.Show(ex1.Message.ToString(), "TreeMenu:ToolBarSettings_try:1164");
      }
      */
    }

    public void ToolBarSettings(XmlNode xmlNode)
    {
      /*
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
        MessageBox.Show(ex1.Message.ToString(), "TreeMenu:ToolBarSettings:1184");
      }
      */
    }

    public static List<String> MenuBarSettingsFiles = new List<string>();
    public static List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();
    public void MenuBarSettings(XmlNode xmlNode)
    {
      /*
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
        MessageBox.Show(ex1.Message.ToString(), "TreeMenu:MenuBarSettings:1214");
      }
      */
    }

    public static List<String> AutoRunSettingFiles = new List<string>();
    public void AutoRunSetting(XmlNode parentNode)
    {
      /*
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
        MessageBox.Show(ex1.Message.ToString(), "TreeMenu:AutoRunSetting:1338");
      }
      */
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
          MessageBox.Show(ex1.Message.ToString(), "TreeMenu:AntPropertySettings:1309");
        }
      }
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

    public string ProcessArgString(string strVar)
    {
      string arg = strVar;
      /*      
      case "Quote" : return "\"";
      case "CBI" : return GetCBI();
      case "STC" : return GetSTC();
      case "AppDir" : return GetAppDir();
      case "UserAppDir" : return GetUserAppDir();
      case "TemplateDir": return GetTemplateDir();
      case "BaseDir" : return GetBaseDir();
      case "SelText" : return GetSelText();
      case "CurFilename": return GetCurFilename();
      case "CurFilenameNoExt": return GetCurFilenameNoExt();
      case "CurFile" : return GetCurFile();
      case "CurDir" : return GetCurDir();
      case "CurWord" : return GetCurWord();
      case "CurSyntax": return GetCurSyntax();
      case "Timestamp" : return GetTimestamp();
      case "OpenFile" : return GetOpenFile();
      case "SaveFile" : return GetSaveFile();
      case "OpenDir" : return GetOpenDir();
      case "DesktopDir" : return GetDesktopDir();
      case "SystemDir" : return GetSystemDir();
      case "ProgramsDir" : return GetProgramsDir();
      case "PersonalDir" : return GetPersonalDir();
      case "WorkingDir" : return GetWorkingDir();
      case "Clipboard": return GetClipboard();
      case "Locale": return GetLocale();
      case "Dollar": return "$";
      */
      return arg;
      }

    public string ProcessVariable(string strVar)
    {
      string arg = strVar;
      try
      {
        arg = this.ProcessArgString(strVar);
      }
      catch { arg = strVar; }
      try
      {
        arg = arg.Replace("$(CurProjectDir)", Path.GetDirectoryName(this.antPanel.projectPath));
        arg = arg.Replace("$(CurProjectName)", Path.GetFileNameWithoutExtension(this.antPanel.projectPath));
        //arg = arg.Replace("$(CurProjectUrl)", Lib.Path2Url(Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath), "localhost"));
        arg = arg.Replace("$(Quote)", "\"");
        arg = arg.Replace("$(Dollar)", "$");
        //arg = arg.Replace("$(AppDir)", PathHelper.AppDir);
        //arg = arg.Replace("$(BaseDir)", PathHelper.BaseDir);
        //arg = arg.Replace("$(CurSciText)", PluginBase.MainForm.CurrentDocument.SciControl.Text);
        //arg = arg.Replace("$(CurFileUrl)", Lib.Path2Url(PluginBase.MainForm.CurrentDocument.FileName, this.settings.DocumentRoot, this.settings.ServerRoot));
        //arg = arg.Replace("$(CurFileUrl)", Lib.Path2Url(PluginBase.MainForm.CurrentDocument.FileName, "localhost"));
        //arg = arg.Replace("$(ControlCurFilePath)", this.controlCurrentFilePath);
        //arg = arg.Replace("$(ControlCurFileDir)", Path.GetDirectoryName(this.controlCurrentFilePath));
        //arg = arg.Replace("$(CurControlFilePath)", this.controlCurrentFilePath);
        //arg = arg.Replace("$(CurControlFileDir)", Path.GetDirectoryName(this.controlCurrentFilePath));
      }
      catch { }
      return arg;
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

    private Int32 getImageIndexFromNodeInfo_safe(NodeInfo ni)
    {
      Int32 imageIndex = 38;
      ///////////////////////////////////////////////////////////////////////////////////////////
      // icon設定
      // アイコン設定(このサンプルにはリソースファイルを入れてないので実は機能していませんが)
      if (ni.Icon != String.Empty) imageIndex = GetIconImageIndexFromIconPath(ni.Icon);
      else
      {
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
      return imageIndex;
    }

    #region Icon functions

    public bool showIcon = true;
    public void toggle_アイコン表示()
    {
      this.showIcon = !this.showIcon;
      if (this.showIcon == true)
      {
        this.treeView.ImageList = this.imageList;
      }
      else this.treeView.ImageList = null;
    }


    public Hashtable _systemIcons = new Hashtable();

    private Image getImageFromIconFile(String path)
    {
      Size smallIconSize = this.GetSmallIconSize();
      Icon icon = new Icon(path);
      Image image = ImageKonverter.ImageResize(icon.ToBitmap(), smallIconSize.Width, smallIconSize.Height);
      return image;
    }
    /*
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
    */
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
            image = antPanel.imageList2.Images[int.Parse(path.Replace("imagelist:", ""))];
            icon = Icon.FromHandle(((Bitmap)image).GetHicon());
            image = ImageKonverter.ImageResize(icon.ToBitmap(), smallIconSize.Width, smallIconSize.Height);
          }
          /*
          else if (path.StartsWith("image:"))
          {
            image = PluginBase.MainForm.FindImage(path.Replace("image:", ""));
            icon = Icon.FromHandle(((Bitmap)image).GetHicon());
            image = ImageKonverter.ImageResize(icon.ToBitmap(), smallIconSize.Width, smallIconSize.Height);
          }
          */
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
        if (Win32.ShouldUseWin32())
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

  }

}
