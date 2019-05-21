using AntPlugin.CommonLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AntPlugin.XmlTreeMenu.Managers
{
  public delegate void TraverseCallback(TreeNode treeNode);

  public class TreeViewManager
  {

    #region Variables
    public static PluginUI pluginUI = null;//
    public static XmlMenuTree menuTree
    {
      get; internal set;
    }
    public static TreeView treeView;
    public static List<String> treexPathList = new List<string>();
    public static List<String> XPathList = new List<String>();
    public class TreeNodeInfo
    {
      public String nodePath;
      public TreeNode treeNode;
      public bool visible;
      public TreeView parentTree;

      public TreeNodeInfo(String path, TreeNode tn, bool v)
      {
        nodePath = path;
        treeNode = tn;
        visible = v;
      }
    }
    public static List<TreeNodeInfo> treeNodeInfoList = new List<TreeNodeInfo>();
    #endregion

    /// <summary>
    /// TraverseTree
    /// </summary>
    /// https://stackoverflow.com/questions/19691286/how-to-iterate-through-all-nodes-of-a-treeview-control-c-sharp
    /// <param name="nodes"></param>
    ///      And call it with: TraverseTree(MyTreeView.Nodes); 
    public static void TraverseTree(TreeNodeCollection nodes)
    {
      foreach (TreeNode child in nodes)
      {
        //DoSomethingWithNode(child);
        String xPath = GetFullXPath(child);
        TreeViewManager.treexPathList.Add(xPath);
        //TreeViewManager.treeNodeInfoList.Add(new TreeNodeInfo(xPath,child,true));
        //MessageBox.Show(xPath, GetTreeNodeFromXPath(xPath).Text);
        //MessageBox.Show(child.FullPath);
        TraverseTree(child.Nodes);
      }
    }

    public static void TraverseTree(TreeNodeCollection nodes, TraverseCallback callback, String xPath="")
    {
      foreach (TreeNode child in nodes)
      {
        if (child.Tag is NodeInfo)
        {
          NodeInfo ni = child.Tag as NodeInfo;
          string attr = TreeViewManager.XPathAttributeFromNodeInfo(ni);
          //xPath += "/" + ni.Type + "[" + child.Index.ToString() + "]" + attr;
          xPath += "/" + ni.Type  + attr;
        }
        else { xPath += "/" + child.Text; }
        child.Name = xPath;
        if (!TreeViewManager.treexPathList.Contains(xPath)) TreeViewManager.treexPathList.Add(xPath);
        TreeViewManager.treeNodeInfoList.Add(new TreeNodeInfo(xPath, child, true));
        //DoSomethingWithNode(child);
        callback(child);
        TraverseTree(child.Nodes, callback, xPath);
      }
    }

    public static string GetFullXPath(TreeNode tn)
    {
      String xPath = String.Empty;
      TreeNode parent = tn;
      List<String> parts = new List<String>();
      while (parent != null)
      {
        //xPath = parent.Index.ToString() + ":" + parent.Text + "/" + xPath;
        //xPath = parent.Text + "/" + xPath;
        //xPath = "[" + parent.Index.ToString() + "]/"+ parent.Text +"/" + xPath;
        if (parent.Tag is NodeInfo)
        {
          NodeInfo ni = parent.Tag as NodeInfo;
          string attr = TreeViewManager.XPathAttributeFromNodeInfo(ni);
          String part = ni.Type + "[" + parent.Index.ToString() + "]" + attr;
          //MessageBox.Show(attr);
          if (!parts.Contains(part)) parts.Add(part);
        }
        else { xPath = parent.Text + "[" + parent.Index.ToString() + "]/" + xPath; }
        parent = parent.Parent;
      }
      List<String> reversedList = Enumerable.Reverse(parts).ToList();
      return String.Join("/", reversedList.ToArray());
    }

    public static TreeNode GetTreeNodeByXPath(String xPath)
    {
      String[] tmp = xPath.Split('/');
      String text = tmp[0].Split('[')[0];
      Int32 index = Int32.Parse(tmp[0].Split('[')[1].TrimEnd(']'));
      TreeNode tn = new TreeNode();

      if (index >= treeView.Nodes.Count)
      {
        tn = new TreeNode(text);
        treeView.Nodes.Add(tn);
      }
      else tn = treeView.Nodes[index];
      for (int i = 1; i < tmp.Length; i++)
      {
        index = Int32.Parse(tmp[i].Split(':')[0]);
        text = tmp[i].Split(':')[1];
        if (index >= treeView.Nodes.Count)
        {
          TreeNode tn2 = new TreeNode(text);
          tn.Nodes.Add(tn2);
          tn = tn2;
        }
        else
        {
          //例外発生
          if (tn.Nodes[index].Text == text) tn = tn.Nodes[index];
          else
          {
            TreeNode tn3 = new TreeNode(text);
            tn.Nodes.Insert(index, tn3);
            tn = tn3;
          }
        }
      }
      return tn;
    }

    public static TreeNode GetTopNode(TreeNode tn, TreeView tv = null)
    {
      /*
      if (tv == null) tv = TreeViewManager.treeView;
      foreach (TreeNode treen in tv.Nodes)
      {
        String path = String.Empty;
        String path2 = String.Empty;
        if (tn == tv.SelectedNode)
        {
          MessageBox.Show(GetSelectedNodePath(), tn.Text);
          return tn;
        }
        else if (IsChildNode(tn, this.treeView.SelectedNode))
        {
          if (tn.Tag is NodeInfo) path = ((NodeInfo)tn.Tag).Path;
          else if (tn.Tag is String) path = (String)tn.Tag;
          else if (tn.Tag is FileInfo) path = ((FileInfo)tn.Tag).FullName;
          else if (tn is AntTreeNode) path = ((AntTreeNode)tn).File;
          //this.treeView.SelectedNode = tn;
          MessageBox.Show(this.GetSelectedNodePath(), tn.Text);
          return tn;
        }
        */
      return null;
    }

    // 第2引数のノードが第1引数の子ノードであるかチェック
    public static bool IsChildNode(TreeNode parent, TreeNode child)
    {
      if (child.Parent == parent)
        return true;
      else if (child.Parent != null)
        return IsChildNode(parent, child.Parent);
      else
        return false;
    }

    public static TreeNode GetTopTreeNode(TreeNode treeNode)
    {
      foreach (TreeNode tn in treeView.Nodes)
      {
        if (IsChildNode(tn, treeNode)) return tn;
      }
      return null;
    }

    /// <summary>
    /// FindTreeNodeByFullPath
    /// </summary
    /// https://stackoverflow.com/questions/2273577/how-to-go-from-treenode-fullpath-data-and-get-the-actual-treenode>
    /// <param name="collection"></param>
    /// <param name="fullPath"></param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    /*
    public static TreeNode FindTreeNodeByFullPath(this TreeNodeCollection collection, string fullPath, 
      StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
    {
      var foundNode = collection.Cast<TreeNode>().FirstOrDefault(tn => string.Equals(tn.FullPath, fullPath, comparison));
      if (null == foundNode)
      {
        foreach (var childNode in collection.Cast<TreeNode>())
        {
          var foundChildNode = FindTreeNodeByFullPath(childNode.Nodes, fullPath, comparison);
          if (null != foundChildNode)
          {
            return foundChildNode;
          }
        }
      }
      return foundNode;
    }
    */
    /// <summary>
    ///  GetNodeFromPath(TreeNode node, string path)
    /// </summary>
    /// http://c-sharpe.blogspot.com/2010/01/get-treenode-from-full-path.html
    /// <param name="node"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static TreeNode GetNodeFromPath(TreeNode node, string path)
    {
      TreeNode foundNode = null;
      foreach (TreeNode tn in node.Nodes)
      {
        if (tn.FullPath == path)
        {
          return tn;
        }
        else if (tn.Nodes.Count > 0)
        {
          foundNode = GetNodeFromPath(tn, path);
        }
        if (foundNode != null)
          return foundNode;
      }
      return null;
    }

    public static String XPathAttributeFromNodeInfo(NodeInfo ni)
    {
      String attr = String.Empty;
      PropertyInfo[] properties = ni.GetType().GetProperties();
      //MessageBox.Show(properties.Length.ToString());
      foreach (PropertyInfo info in properties)
      {
        //MessageBox.Show(info.GetValue(treeNode.Tag,null).ToString(), info.Name);
        if (info.GetValue(ni, null) is String)
        {
          String value = info.GetValue(ni, null) as String;
          if (!String.IsNullOrEmpty(value)) attr += "[@" + info.Name + "='" + value.Replace("/","%") + "']";
        }
      }
      //MessageBox.Show("[" + attr.TrimEnd('['));
      //Console.ReadLine();
      if (!String.IsNullOrEmpty(attr))
      {
        attr = attr.Replace("@InnerText", "text()");
        return attr;
      }
      return String.Empty;
    }

    public static XmlElement SetAttributeByNodeInfo(XmlElement xe,NodeInfo ni)
    {
      PropertyInfo[] properties = ni.GetType().GetProperties();
      //MessageBox.Show(properties.Length.ToString());
      foreach (PropertyInfo info in properties)
      {
        if (info.GetValue(ni, null) is String)
        {
          String value = info.GetValue(ni, null) as String;
          if (!String.IsNullOrEmpty(value)) xe.SetAttribute(info.Name.ToLower(), value);
        }
      }
      return xe;
    }

    public static TreeNode AppendTreeNodeByXPath(TreeNode rootNode, String xpath)
    {
      TreeNode treeNode = rootNode;
      XmlDocument doc = new XmlDocument();
      XmlNode node = createXPath(doc, xpath);
      //MessageBox.Show(GetFormattedXmlText(node.OuterXml));
      foreach (string part in xpath.Trim('/').Split('/'))
      {
        // root[1]/foo[2]/bar[2][@attr = 'value2'][text() = 'aaaa']
        string name=String.Empty;
        int index = 0;
        int start = 2;
        Dictionary<String, String> attributes = new Dictionary<string, string>();
        NodeInfo ni = new NodeInfo();
        if (part.Contains("["))
        {
          String[] tmp = part.Split('[');
          name = tmp[0];//.Replace("@","").Replace("[","").Replace("]","");
          //index = int.Parse(tmp[1].TrimEnd(']'));
          if (!int.TryParse(tmp[1].TrimEnd(']'), out index)) start = 1;
          if (index > 0) index--;
          for (int i = start; i < tmp.Length; i++)
          {
            String key = tmp[i].Replace("@", "").Replace("'", "").Replace("]", "").Split('=')[0].Trim();
            String value = tmp[i].Replace("@", "").Replace("'", "").Replace("]", "").Split('=')[1].Trim();
            attributes.Add(key, value);
          }
        }
        ni = MakeNodeInfo(attributes);
        if (name == "folder") ni.Type = "folder";
        if (name == "record") ni.Type = "record";
        if (name == "root") ni.Type = "root";
        if (String.IsNullOrEmpty(ni.Title)) ni.Title = name;
        ni.XmlNode = node;
        /////////////////////////////////////////////////////////////////////////////
        // treeNode 生成処理
        TreeNode child = new TreeNode(ni.Title);
        child.ImageIndex = child.SelectedImageIndex = getImageIndexFromNodeInfo(ni);
        child.Tag = ni;
        if (treeNode.Nodes.Count > index)
        {
          if (treeNode.Nodes[index].Text == ni.Title)
          {
            treeNode = treeNode.Nodes[index];
          }
          else
          {
            treeNode.Nodes.Insert(index, child);
            treeNode = child;
          }
        }
        else
        {
          treeNode.Nodes.Add(child);
          treeNode = child;
        }
      }
      return treeNode;
    }

    public static TreeNode GetTreeNodeFromFiles(TreeNode rootNode, String[] files)
    {
      foreach (String file in files) rootNode.Nodes.Add(GetTreeNodeFromFile(file));
      return rootNode;
    }

    public static TreeNode GetTreeNodeFromFile(String file)
    {
      TreeNode treeNode = new TreeNode();
      if (Path.GetExtension(file) == ".xml")
      {
        bool isMenu = (Path.GetFileNameWithoutExtension(file).ToLower() == "fdtreemenu"
          || Path.GetFileNameWithoutExtension(file).ToLower() == "xmltreemenu") ? true : false;
        treeNode = menuTree.getXmlTreeNode(file, isMenu);
      }
      else if (Path.GetExtension(file) == ".txt" && Path.GetFileName(file).ToLower().Contains("xpathlist"))
      {
        XmlDocument doc = new XmlDocument();
        StreamReader stream = new StreamReader(file);
        String line;
        while ((line = stream.ReadLine()) != null)
        {
          if (line.Length > 0 && !line.StartsWith("#")) Set(doc, line); //createXPath(doc, line); //AppendTreeNodeByXPath(rootNode, line);
        }
        stream.Close();
        String output = GetFormattedXmlText(doc.OuterXml.Replace("%", "/"));
        MessageBox.Show(output);
        treeNode = menuTree.getXmlTreeNodeFromString(doc.OuterXml.Replace("%", "/"), file);
      }
      else if (Path.GetExtension(file) == ".fdp" || Path.GetExtension(file) == ".wsf")
      {
        treeNode = pluginUI.GetBuildFileNode(file);
        // 追加 2017-01-12
        //XmlDocument doc = new XmlDocument();
        //doc.Load(file);
        //this.propertyGrid1.SelectedObject = new XmlNodeWrapper(doc.DocumentElement);
      }
      else if (Path.GetExtension(file) == ".gradle")
      {
        AntTreeNode gradleNode = pluginUI.gradleTree.GetGradleOutlineTreeNode(file);
        treeNode = (TreeNode)gradleNode;
      }
      else
      {
        NodeInfo ni = new NodeInfo();
        ni.Type = "record";
        ni.Title = Path.GetFileName(file);
        ni.Path = file;
        int imageindex = menuTree.GetIconImageIndex(file);
        TreeNode tn = new TreeNode(ni.Title, imageindex, imageindex);
        tn.Tag = ni;
        treeNode = tn;
        //Globals.AntPanel.FillTree()
      }
      return treeNode;
    }

    public static NodeInfo MakeNodeInfo(Dictionary<String,String> attr)
    {
      NodeInfo ni = new NodeInfo();
      foreach (KeyValuePair<string, string> kvp in attr)
      {
        if (kvp.Key.ToLower() == "type") ni.Type = kvp.Value;
        if (kvp.Key.ToLower() == "title") ni.Title = kvp.Value;
        if (kvp.Key.ToLower() == "tooltip") ni.Tooltip = kvp.Value;
        if (kvp.Key.ToLower() == "backcolor") ni.BackColor = kvp.Value;
        if (kvp.Key.ToLower() == "forecolor") ni.ForeColor = kvp.Value;
        if (kvp.Key.ToLower() == "nodefont") ni.NodeFont = kvp.Value;
        if (kvp.Key.ToLower() == "nodechecked") ni.NodeChecked = kvp.Value;
        if (kvp.Key.ToLower() == "pathbase") ni.PathBase = kvp.Value;
        if (kvp.Key.ToLower() == "action") ni.Action = kvp.Value;
        if (kvp.Key.ToLower() == "command") ni.Command = kvp.Value;
        if (kvp.Key.ToLower() == "path") ni.Path = kvp.Value;
        if (kvp.Key.ToLower() == "icon") ni.Icon = kvp.Value;
        if (kvp.Key.ToLower() == "args") ni.Args = kvp.Value;
        if (kvp.Key.ToLower() == "option") ni.Option = kvp.Value;
        if (kvp.Key.ToLower() == "text()") ni.InnerText = kvp.Value;
        if (kvp.Key.ToLower() == "comment()") ni.Comment = kvp.Value;
        if (kvp.Key.ToLower() == "expand")
        {
          if (kvp.Value.ToLower() == "true") ni.Expand = true;
          else ni.Expand = false;
        }
      }
      /*
      PropertyInfo[] properties = ni.GetType().GetProperties();
      foreach (PropertyInfo info in properties)
      {
        //MessageBox.Show(info.GetValue(treeNode.Tag,null).ToString(), info.Name);
        if (info.GetValue(ni, null) is String)
        {
          String value = info.GetValue(ni, null) as String;
          foreach (KeyValuePair<string, string> kvp in attr)
          {
            // プロパティ情報の取得
            var property = typeof(Student).GetProperty("Name");
            if (info.Name == kvp.Key) info.SetValue(ni,kvp.Value);
            //int id = kvp.Key;
            //string name = kvp.Value;
            //WriteLine($"{id}:{name}");
          }
        }
      }
      */
      return ni;  
    }

    ///////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///  XML 文字列にインデントつき整形を施す
    /// </summary>
    /// https://qiita.com/otagaisama-1/items/668e7c913b728b3218fc
    /// <param name="s"></param>
    /// <returns></returns>
    public static string GetFormattedXmlText(string s)
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

    static string FindXPath(XmlNode node)
    {
      StringBuilder builder = new StringBuilder();
      while (node != null)
      {
        switch (node.NodeType)
        {
          case XmlNodeType.Attribute:
            //builder.Insert(0, "/@" + node.Name);
            node = ((XmlAttribute)node).OwnerElement;
            break;
          case XmlNodeType.Element:
            int index = FindElementIndex((XmlElement)node);
            String part = "/" + node.Name + "[" + index + "]";

            foreach (XmlAttribute attr in node.Attributes)
            {
              part += "[@" + attr.Name + "=" + "'" + attr.Value.Replace("/","%") + "']";
            }
            try
            {
              if (node.ChildNodes[0].NodeType == XmlNodeType.Text || node.ChildNodes[0].NodeType == XmlNodeType.CDATA)
              {
                part += "[text()=" + "'" + node.InnerXml + "']";
              }
            }
            catch { }
            //builder.Insert(0, "/" + node.Name + "[" + index + "]");
            builder.Insert(0, part);
            node = node.ParentNode;
            break;
          case XmlNodeType.CDATA:
          case XmlNodeType.Text:
            node = node.ParentNode;
            break;
          case XmlNodeType.Document:
            return builder.ToString();
          default:
            MessageBox.Show(node.NodeType.ToString());
            break;
            //throw new ArgumentException("Only elements and attributes are supported");
        }
      }
      throw new ArgumentException("Node was not in a document");
    }

    static int FindElementIndex(XmlElement element)
    {
      XmlNode parentNode = element.ParentNode;
      if (parentNode is XmlDocument)
      {
        return 1;
      }
      XmlElement parent = (XmlElement)parentNode;
      int index = 1;
      foreach (XmlNode candidate in parent.ChildNodes)
      {
        if (candidate is XmlElement && candidate.Name == element.Name)
        {
          if (candidate == element)
          {
            return index;
          }
          index++;
        }
      }
      throw new ArgumentException("Couldn't find element within parent");
    }

    public static void TraverseNode(XmlNode xmlNode)
    {
      if (xmlNode.NodeType == XmlNodeType.Text) return;
      foreach (XmlNode child in xmlNode.ChildNodes)
      {
        //DoSomethingWithNode(child);
        if (!XPathList.Contains(FindXPath(child)))
        {
          //MessageBox.Show(FindXPath(child));
          XPathList.Add(FindXPath(child));
        }
        TraverseNode(child);
      }
    }

    public static void Set(XmlDocument doc, string xpath, string value = "")
    {
      if (doc == null) throw new ArgumentNullException("doc");
      if (string.IsNullOrEmpty(xpath)) throw new ArgumentNullException("xpath");

      XmlNodeList nodes = doc.SelectNodes(xpath);
      if (nodes.Count > 1)
      {
        // throw new ComponentException("Xpath '" + xpath + "' was not found multiple times!");
        MessageBox.Show("Xpath '" + xpath + "' was not found multiple times!");
        return;
      }
      else if (nodes.Count == 0)
      {
        XmlNode node = createXPath(doc, xpath);
        if (!String.IsNullOrEmpty(value))
        {
          node.InnerText = value;
        }
      }
      else if (!String.IsNullOrEmpty(value))
      {
        nodes[0].InnerText = value;
      }
    }
    //Set(doc, "/configuration/appSettings/add[@key='Server']/@value", "foobar");

    public static XmlNode createXPath(XmlDocument doc, string xpath)
    {
      XmlNode node = doc;
      foreach (string part in xpath.Trim('/').Split('/'))
      {
        XmlNodeList nodes = node.SelectNodes(part);
        if (nodes.Count > 1)
        {
          //Console.WriteLine("Xpath '" + xpath + "' was not found multiple times!");
          MessageBox.Show("Xpath '" + xpath + "' was not found multiple times!");
          return null;
        }
        else if (nodes.Count == 1)
        {
          node = nodes[0];
          continue;
        }
        if (part.StartsWith("@"))
        {
          var anode = doc.CreateAttribute(part.TrimStart('@'));
          node.Attributes.Append(anode);
          node = anode;
        }
        else
        {
          string elName, attrib = null;
          if (part.Contains("["))
          {
            String[] tmp = part.Split('[');
            elName = tmp[0];//.Replace("@","").Replace("[","").Replace("]","");
            int index;
            String s = tmp[1].TrimEnd(']');
            if (int.TryParse(s, out index))
            {
              for (int i = 2; i < tmp.Length; i++) attrib += tmp[i];
            }
            else
            {
              for (int i = 1; i < tmp.Length; i++) attrib += tmp[i];
            }
            /*
            int i = 0;
            string s = "108";
            bool result = int.TryParse(s, out i); //i now = 108  
            文字列が数値以外の文字、または指定した型で表すには大きすぎる(または小さすぎる) 数値の場合、
            TryParse は false を返し、out パラメーターを 0 に設定します。 
            それ以外の場合は true を返し、
            out パラメーターを文字列の数値に設定します。
            */
            //for (int i = 1; i < tmp.Length; i++) attrib += tmp[i];
            if (!String.IsNullOrEmpty(attrib)) attrib = attrib.TrimEnd(']');
          }
          else elName = part;

          XmlNode next = doc.CreateElement(elName);
          node.AppendChild(next);
          node = next;
          if (attrib != null)
          {
            Dictionary<string, string> attributes = StringHandler.Get_Values(attrib, ']', '=');
            foreach (KeyValuePair<string, string> item in attributes)
            {
              string name, value;
              name = item.Key.Replace("@", ""); ;
              value = item.Value.Trim('\'');
              if (name == "text()")
              {
                XmlNode textNode = doc.CreateTextNode(value);
                node.AppendChild(textNode);
              }
              else
              {
                var anode = doc.CreateAttribute(name);
                anode.Value = value;
                node.Attributes.Append(anode);
              }
            }
          }
        }
      }
      return node;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////
    // Click Handler Functions

    public static Stack<TreeNode> removeNodeStack = new Stack<TreeNode>();
    /// <summary>
    /// Cut the selected file to the clipboard
    /// </summary>
    public static  void RemoveNode(TreeNode treeNode)
    {
      removeNodeStack.Push(treeNode);
      treeNode.Parent.Nodes.Remove(treeNode);
    }

    public static void PasteNode(TreeNode treeNode)
    {
      if (removeNodeStack.Count > 0) treeNode.Parent.Nodes.Insert(treeNode.Index+1, removeNodeStack.Pop());
      /*
      // 以下 クリップボードからファイルを貼り付ける例 Flashdevelop FileExplorer
      String target = String.Empty;
      if (this.fileView.SelectedItems.Count == 0) target = this.selectedPath.Text;
      else target = this.fileView.SelectedItems[0].Tag.ToString();
      StringCollection items = Clipboard.GetFileDropList();
      for (Int32 i = 0; i < items.Count; i++)
      {
        if (File.Exists(items[i]))
        {
          String copy = Path.Combine(target, Path.GetFileName(items[i]));
          String file = FileHelper.EnsureUniquePath(copy);
          File.Copy(items[i], file, false);
        }
        else
        {
          String folder = FolderHelper.EnsureUniquePath(target);
          FolderHelper.CopyFolder(items[i], folder);
        }
      }
      */
    }

    public static void CopyNode(TreeNode treeNode)
    {
      // https://docs.microsoft.com/ja-jp/dotnet/api/system.windows.forms.treenode.clone?view=netframework-4.8
      TreeNode cloneNode = treeNode.Clone() as TreeNode;
      removeNodeStack.Push(cloneNode);
      /*
      // 以下 nodeのファイルパスをクリップボードにコピーする例 Flashdevelop FileExplorer Plugin
      StringCollection items = new StringCollection();
      items.Add(((NodeInfo)this.treeView.SelectedNode.Tag).Path);
      //for (Int32 i = 0; i < this.fileView.SelectedItems.Count; i++)
      //{
        //items.Add(fileView.SelectedItems[i].Tag.ToString());
      //}
      Clipboard.SetFileDropList(items);
      */
    }

    ///////////////////////////////////////////////////////////////////////////////////////////
    // icon設定
    public static Int32 getImageIndexFromNodeInfo(NodeInfo ni)
    {
      Int32 imageIndex = 38;
      String path = String.Empty;
      if (!String.IsNullOrEmpty(ni.Path)) path = ni.Path;
      if (!String.IsNullOrEmpty(ni.Icon)) imageIndex = menuTree.GetIconImageIndexFromIconPath(ni.Icon);
      else
      {
        if (ni.Type == "folder") imageIndex = menuTree.GetIconImageIndex(@"C:\windows");
        else if (ni.Type == "root") imageIndex = 38;
        else if (!String.IsNullOrEmpty(ni.Action) && (ni.Action.ToLower() == "ant" || ni.Action.ToLower() == "wsf")) imageIndex = 14;
        else if (!String.IsNullOrEmpty(ni.Command)) imageIndex = 9;
        else
        {
          //if (ni.PathBase != String.Empty) path = Path.Combine(ni.PathBase, ni.Path);
          if (Lib.IsWebSite(path)) imageIndex = menuTree.GetIconImageIndexFromIconPath(path);
          else if (File.Exists(path)) imageIndex = menuTree.GetIconImageIndex(path);
          else imageIndex = 8;
        }
      }
      return imageIndex;
    }

  }
}