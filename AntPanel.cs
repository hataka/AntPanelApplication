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
using AntPlugin.XmlTreeMenu;
using AntPlugin.CommonLibrary;
using CSParser.BuildTree;
using AntPlugin.Controls;
using CSParser.Model;
using AntPlugin.XmlTreeMenu.Managers;

namespace AntPanelApplication
{
	public partial class AntPanel : UserControl
	{
		public const int ICON_FILE = 0;
		public const int ICON_DEFAULT_TARGET = 1;
		public const int ICON_INTERNAL_TARGET = 2;
		public const int ICON_PUBLIC_TARGET = 3;
		public const int ICON_WSF_FILE = 36;
		public const int ICON_FDP_FILE = 37;
		public const int ICON_MENU_ROOT = 38;

		//public String projectPath = @"F:\codingground\java\Nashorn\Nashorn.fdp";
		//public String projectPath = @"F:\codingground\codingground.fdp";
		//public String projectPath = @"F:\codingground\java\swt-snippets\swt-snippets.fdp";
		//public String projectPath = @"F:\codingground\java\swt-snippets\Snippet001\Snippet1.fdp";
		public String projectPath = @"F:\GitHub\Flasdevelop\flashdevelop.5.3.1\FlashDevelop-531.fdp";
		public string itemPath = @"F:\GitHub\Flasdevelop\flashdevelop.5.3.1\FlashDevelop\MainForm.cs";// String.Empty;
		public string curSelText = String.Empty;
    public string targetPath = String.Empty;

    global::AntPanelApplication.Properties.Settings settings
      = new global::AntPanelApplication.Properties.Settings();

		private ContextMenuStrip buildFileMenu;
		private ContextMenuStrip targetMenu;
		private List<String> buildFilesList = new List<string>();
		
		public String addArgs;
		private int toggleIndex = 1;
		public ImageList imageList2;

		public BuildTree buildTree = new BuildTree();
		public GradleTree gradleTree = new GradleTree();
		public XmlMenuTree menuTree = new XmlMenuTree();

		public DirTreePanel dirTreePanel;
		//public FTPClientPanel ftpClientPanel;
		//public XmlTreePanel xmlTreePanel;

		private String defaultTarget;
		private ContextMenuStrip csOutlineMenu = null;
		public static List<string> csOutlineTreePath = new List<string>();
		public List<string> MemberId = new List<string>();

		public static List<string> csOutlinePanelTreePath = new List<string>();
		public List<string> OutLinePanelMemberId = new List<string>();

		private const String STORAGE_FILE_NAME = "antPluginData.txt";
		public String antPath = @"F:\ant\apache-ant-1.10.1"; // F:\ant\apache-ant-1.10.1\bin\ant
		public String sakuraPath = @"C:\Program Files (x86)\sakura\sakura.exe";
		public String devenv15Path = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe";
		public String devenv17Path = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe";

		public TreeNode currentNode = new TreeNode();//null;

		public List<string> BuildFilesList
		{
			get { return buildFilesList; }
		}

		public AntPanel()
		{
			InitializeComponent();
			InitializeAntPanel();
		}

		public AntPanel(string projpath)
		{
			projectPath = projpath;
			InitializeComponent();
			InitializeAntPanel();
		}

		public AntPanel(string[] args)
		{
			if (args.Length > 0 && !String.IsNullOrEmpty(args[0])) this.projectPath = args[0];
			if (args.Length > 1 && !String.IsNullOrEmpty(args[1])) this.itemPath = args[1];
			if (args.Length > 2 && !String.IsNullOrEmpty(args[2])) this.curSelText = args[2];
      if (args.Length > 3 && !String.IsNullOrEmpty(args[3])) this.targetPath = args[3];
      InitializeComponent();
			InitializeAntPanel();
		}

    #region Initialization
    private void InitializeAntPanel()
		{
			InitializeGraphics();

			InitializeGradleTree();
			IntializeXmlMenuTree();
			IntializeDirTreePanel();
			//IntializeFTPClientPanel();
			//InitializeXmlTreePanel();

			CreateMenus();

      this.homeStripButton.Click += new System.EventHandler(this.homeStripButton_Click);

      this.splitContainer1.Panel2Collapsed = true;
			this.splitContainer1.Panel1Collapsed = false;
			this.propertyGrid1.HelpVisible = false;
			this.propertyGrid1.ToolbarVisible = false;
			this.settings = new global::AntPanelApplication.Properties.Settings();
			this.propertyGrid3.SelectedObject = this.settings;

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
			this.tabPage5.ImageIndex = 93; //settings

		}

		private void InitializeInterface()
		{
			// TODO 実装 必要性検討 
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
			this.gradleTree = new GradleTree(this);
		}

		private void IntializeXmlMenuTree()
		{
			this.menuTree = new XmlMenuTree(this);
		}

		private void IntializeDirTreePanel()
		{
			this.dirTreePanel = new DirTreePanel(this,this.projectPath);
			dirTreePanel.Dock = DockStyle.Fill;
			this.tabPage2.Controls.Add(dirTreePanel);
		}

		private void IntializeFTPClientPanel()
		{
			/*
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
			*/
		}

		private void InitializeXmlTreePanel()
		{
			/*
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
			*/
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
    #endregion

    private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if(treeView.GetNodeAt(e.Location) is AntTreeNode)
				{
					AntTreeNode currentNode = treeView.GetNodeAt(e.Location) as AntTreeNode;
					treeView.SelectedNode = currentNode;
					if (currentNode.Parent == null)
						buildFileMenu.Show(treeView, e.Location);
					else
						targetMenu.Show(treeView, e.Location);
				}
				else
				{
					TreeNode currentNode = treeView.GetNodeAt(e.Location) as TreeNode;
					treeView.SelectedNode = currentNode;
					if (currentNode.Tag == null) return;
					if (currentNode.Tag.GetType().Name == "NodeInfo")
					{
						try
						{
							this.menuTree.contextMenuStrip1.Show(treeView, e.Location);
						}
						catch (Exception Exception)
						{
							MessageBox.Show(Exception.Message.ToString(), "TreeMenu:treeView_NodeMouseClick:125");
						}
					}
					else if(currentNode.Tag.GetType().Name == "MemberModel")
					{
						MessageBox.Show(((MemberModel)currentNode.Tag).Name);
					}
					else if (currentNode.Tag.GetType().Name == "string")
					{
						MessageBox.Show((string)currentNode.Tag);
					}
				}
			}
		}

		private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			// fixed 2018-2-23
			TreeView tree = sender as TreeView;
			TreeNode treeNode = tree.SelectedNode;
			/// XmlTreeView カプセル化のため追加 2018-02-23
			ToolStripMenuItem button = new ToolStripMenuItem();

			if (treeNode is AntTreeNode)
			{
				//RunTarget();
        AntTreeNode antNode = treeNode as AntTreeNode;
        if (antNode == null) return;
				if (antNode.Tag is String)
				{
					String path = antNode.Tag as String;
					if (path == "GradleTargetNode") this.RunTargetex(antNode.File, antNode.Target);
					if (File.Exists(path))
					{
						//PluginBase.MainForm.OpenEditableDocument(path, false);
						//Process.Start(this.devenv15Path, "/edit " + path);
            this.OpenDocument(path);
          }
					return;
				}
				else if (antNode.Tag is TaskInfo)
				{
					if (((TaskInfo)antNode.Tag).Name == "GradleBuildNode")
					{
						//Process.Start(this.devenv15Path, "/edit " + antNode.File);
            //PluginBase.MainForm.OpenEditableDocument(antNode.File, false);
            this.OpenDocument(antNode.File);
          }
					else this.RunTargetex(antNode.File, antNode.Target);
				}
				else if (antNode.Tag is NodeInfo)
				{
					try
					{
						if (treeNode.Parent == null)
						{
							//PluginBase.MainForm.OpenEditableDocument(((NodeInfo)treeNode.Tag).Path);
							//Process.Start(this.devenv15Path, "/edit " + ((NodeInfo)treeNode.Tag).Path);
              this.OpenDocument(((NodeInfo)treeNode.Tag).Path);
              return;
						}
						else
						{
							NodeInfo ni = new NodeInfo();
							ni = treeNode.Tag as NodeInfo;
							//MessageBox.Show(ni.Path, ni.Title);
							ActionManager.NodeAction(ni);
						}
					}
					catch (Exception ex2)
					{
						String errorMsg = ex2.Message.ToString();
						MessageBox.Show(errorMsg, "treeNode.Tag.GetType().Name != NodeInfo");
						return;
					}
					return;
				}
				else if (treeNode.Tag is XmlNode)
				{
					//MessageBox.Show(treeNode.Tag.GetType().Name);
					//Process.Start(this.devenv15Path, "/edit " + (String)treeNode.Tag);

					XmlNode xmlNode = antNode.Tag as XmlNode;
					XmlNode tag = antNode.Tag as XmlNode;
					XmlAttribute descrAttr = tag.Attributes["description"];
					String description = (descrAttr != null) ? descrAttr.InnerText : "";
					switch (xmlNode.Name)
					{
						case "target":
						case "job":
							//if (description.StartsWith("Click"))
							//{
								//this.RunCommand(description);
							//}
							//else if (description.StartsWith("Plugin"))
							//{
								//this.PluginCommand(xmlNode);
							//}
							//else
								this.RunTargetex(antNode.File, antNode.Target);
							return;
						case "property":
							//if (description.StartsWith("Click")) this.RunCommand(description);
							//else MessageBox.Show(xmlNode.OuterXml.Replace("\t", "  ").Replace("	", "  "), "property : " + xmlNode.Attributes["name"].InnerText);
							return;
						case "taskdef":
							MessageBox.Show(xmlNode.OuterXml.Replace("\t", "  ").Replace("    ", "  "), "taskdef : " + xmlNode.Attributes["resource"].InnerText);
							return;
						case "package":
						case "project":
							AntTreeNode rootnode = treeView.SelectedNode as AntTreeNode;
              this.OpenDocument(rootnode.File);
              //Process.Start(this.devenv15Path, "/edit " + rootnode.File);
							return;
						default:
							if (treeNode.Parent == null)
							{
								AntTreeNode node = treeView.SelectedNode as AntTreeNode;
								Process.Start(this.devenv15Path, "/edit " + node.File);
							}
							else
							{
								MessageBox.Show(xmlNode.OuterXml.Replace("\t", "  ").Replace("    ", "  "), "NodeName : " + xmlNode.Name);
							}
							return;
					}
				}
			}
			else if (treeNode is TreeNode)
			{
				if (treeNode.Tag is NodeInfo)
				{
					try
					{
						if (treeNode.Parent == null)
						{
              this.OpenDocument(((NodeInfo)treeNode.Tag).Path);
              //Process.Start(this.devenv15Path, "/edit " + ((NodeInfo)treeNode.Tag).Path);
							return;
						}
						else
						{
							NodeInfo ni = new NodeInfo();
							ni = treeNode.Tag as NodeInfo;
							//MessageBox.Show(ni.Path, ni.Title);
							ActionManager.NodeAction(ni);
						}
					}
					catch (Exception ex2)
					{
						String errorMsg = ex2.Message.ToString();
						MessageBox.Show(errorMsg, "treeNode.Tag.GetType().Name != NodeInfo");
						return;
					}
					return;
				}
				else if(treeNode.Tag is String)
				{
          //MessageBox.Show((String)treeNode.Tag);
          this.OpenDocument((String)treeNode.Tag);
         }
        else if (treeNode.Tag is MemberModel)
				{
					MessageBox.Show(((MemberModel)treeNode.Tag).Definition, ((MemberModel)treeNode.Tag).Name);
          // 次の例では、現在選択されているコード セクションで、
          //"somestring" という語を、大文字と小文字を区別して検索します。
          //Edit.Find somestring / sel /case  
          String arguments=((MemberModel)treeNode.Tag).SrcPath + " /Command \"Edit.Find "
           + ((MemberModel)treeNode.Tag).Name + " /doc\"";
          Process.Start(this.devenv15Path, arguments);
        }
        else
				{

				}
			}
		}

    public void RunTargetex(String file, String target)
		{
			// KAHATA 追加 2018-02-08
			if (Path.GetExtension(file).ToLower() == ".gradle")
			{
				this.RunGradle(file, target);
				return;
			}
			else if (Path.GetExtension(file).ToLower() == ".wsf")
			{
				this.RunJob(file, target);
				return;
			}
			String command = Environment.SystemDirectory + "\\cmd.exe";

			String arguments = "/c ";
			if (this.settings.AntPath.Length > 0)
				arguments += Path.Combine(settings.AntPath, "bin") + "\\ant";
			//else if (this.ant17ToolStripMenuItem.Checked == true) arguments += "ant17";
			else arguments += "ant";

			//if (!string.IsNullOrEmpty(settings.AddArgs)) arguments += " " + settings.AddArgs;
			arguments += " -buildfile \"" + file + "\" \"" + target + "\"";
			//arguments += " -buildfile \"" + file + "\" \"" + target + "\"";
			arguments += " -DprojectDir=\"" + Path.GetDirectoryName(this.projectPath) + "\"";
			//arguments += " -DcurDir=\"" + PluginBase.MainForm.ProcessArgString("$(CurDir)") + "\"";
			arguments += " -DcurDir=\"" + Path.GetDirectoryName(this.projectPath) + "\"";

			//kahata: Time-stamp: <2016-04-23 5:41:26 kahata>
			System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(file));

			//if (this.pluginUI.consoleToolStripMenuItem.Checked == true)
			//{
				Process.Start("cmd.exe", "/k " + command + " " + arguments);
			//}
			//if (this.pluginUI.documentToolStripMenuItem.Checked == true)
			//{
				//String result = this.getStandardOutput("cmd.exe", command + " " + arguments);
				//PluginBase.MainForm.CallCommand("New", "");
				//PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
				//PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(result);
				//String output = Lib.StabdardOutput()
				//Process.Start("cmd.exe", "/k " + command + " " + arguments);
			//}
			//if (this.pluginUI.traceToolStripMenuItem.Checked == true)
			//{
				//TraceManager.Add(command + " " + arguments);
				//PluginBase.MainForm.CallCommand("RunProcessCaptured", command + ";" + arguments);
			//}
		}

		public void RunJob(String file, String job)
		{
			//CScript //Job:MyFirstJob MyScripts.wsf
			String command = Environment.SystemDirectory + @"\Wscript.exe";
			//file = "\"" + file + "\"";
			String arguments = " //job:" + job;
			arguments += " \"" + file + "\"";
			//kahata: Time-stamp: <2017-06-23 08:27:26 kahata>
			try
			{
				System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(file));
			}
			catch { }
			//PluginBase.MainForm.CallCommand("RunProcessCaptured", command + ";" + arguments);
			Process.Start(command, arguments);
		}

		public void RunGradle(String file, String target)
		{
			String command = @"F:\gradle-3.5\bin\gradle.bat";
			String arguments = "/k " + command + " -q " + target + " -b ";
			arguments += " \"" + file + "\"";
			//kahata: Time-stamp: <2017-06-23 08:27:26 kahata>
			try
			{
				System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(file));
			}
			catch { }
			Process.Start("cmd.exe", arguments);
			//PluginBase.MainForm.CallCommand("RunProcessCaptured", command + ";" + arguments);
		}

		public string getStandardOutput(string command, string arguments)
		{
			Process process = Process.Start(new ProcessStartInfo
			{
				FileName = command,
				Arguments = arguments,
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardOutput = true
			});
			string text = process.StandardOutput.ReadToEnd();
			return text.Replace("\r\r\n", "\n");
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
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "BuildFiles (*.xml)|*.XML|" + "WshFiles (*.wsf)|*.wsf|" + "All files (*.*)|*.*";
			dialog.Multiselect = true;
			if (!String.IsNullOrEmpty(projectPath))dialog.InitialDirectory = Path.GetDirectoryName(projectPath);

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				AddBuildFiles(dialog.FileNames);
			}
		}

		public void runButton_Click(object sender, EventArgs e)
		{
			RunTarget();
		}
		
		public void RunTarget()
		{
			AntTreeNode node = treeView.SelectedNode as AntTreeNode;
			if (node == null) return;
			RunTarget(node.File, node.Target);
		}
		
		public void RunTarget(String file, String target)
		{
			String command = Environment.SystemDirectory + "\\cmd.exe";

			String arguments = "/k ";
			
			if (antPath.Length > 0)arguments += Path.Combine(antPath, "bin") + "\\ant";
			else
				arguments += "ant";
			if (!string.IsNullOrEmpty(addArgs)) arguments += " " + addArgs;
			arguments += " -buildfile \"" + file + "\" \"" + target + "\"";
			//TraceManager.Add(command + " " + arguments);
			//Globals.MainForm.CallCommand("RunProcessCaptured", command + ";" + arguments);
			Process.Start(command, arguments);
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
			String fullName = folder + "\\" + STORAGE_FILE_NAME;

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
			String fullName = folder + "\\" + STORAGE_FILE_NAME;
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
			// タブコントロールに組み込むと最初に加えたノードが表示されなくなる 間に合せのパッチ
			TreeNode dummy = new TreeNode("dummy");
			treeView.Nodes.Add(dummy);
			foreach (String file in BuildFilesList)
			{
				if (File.Exists(file))
				{
					if (Path.GetExtension(file) == ".cs" || Path.GetExtension(file) == ".java")
					{
						this.LoadIn(file);
					}
					else if (Path.GetExtension(file) == ".xml")
					{
						bool isMenu = (Path.GetFileNameWithoutExtension(file).ToLower() == "fdtreemenu"
							|| Path.GetFileNameWithoutExtension(file).ToLower() == "xmltreemenu") ? true : false;
						treeView.Nodes.Add(this.menuTree.getXmlTreeNode(file, isMenu));
					}
					else if (Path.GetExtension(file) == ".fdp" || Path.GetExtension(file) == ".wsf")
					{
						treeView.Nodes.Add(GetBuildFileNode(file));
						// 追加 2017-01-12
						XmlDocument doc = new XmlDocument();
						 doc.Load(file);
						this.propertyGrid1.SelectedObject = new XmlNodeWrapper(doc.DocumentElement);
					}
					else if (Path.GetExtension(file) == ".gradle")
					{
						AntTreeNode gradleNode = this.gradleTree.GetGradleOutlineTreeNode(file);
						treeView.Nodes.Add(gradleNode);
					}
					else
					{
						TreeNode linkNode = new TreeNode(Path.GetFileName(file), 1, 1);
						linkNode.Tag = file;
						linkNode.ToolTipText = file;
						treeView.Nodes.Add(linkNode);
					}
				}
			}
			treeView.EndUpdate();
		}

		// kokokoko
		public TreeNode GetBuildFileNode(string file)
		{
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


			if (Path.GetExtension(file).ToLower() == ".wsf") rootNode = new AntTreeNode(projectName, ICON_WSF_FILE);
			if (Path.GetExtension(file).ToLower() == ".fdp") rootNode = new AntTreeNode(projectName, ICON_FDP_FILE);
			rootNode.File = file;
			rootNode.Target = defaultTarget;
			rootNode.Tag = xml.DocumentElement;
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

					case "property":
					case "scriptdef":
					case "taskdef":
					case "macrodef":
						// skip private targets
						XmlAttribute propertyNameAttr = child.Attributes["name"];
						XmlAttribute propertyResourceAttr = child.Attributes["resource"];
						if (propertyNameAttr != null)
						{
							String propertyName = propertyNameAttr.InnerText;
							if (!String.IsNullOrEmpty(propertyName) && (propertyName[0] == '-')) continue;
						}
						antNode = GetBuildTargetNode(child, defaultTarget);
						antNode.File = file;
						//Kahata TimeStamp: 2016-04-23
						antNode.Tag = child;
						if (子ノード表示ToolStripMenuItem.Checked == true) this.populateChildNodes(child, antNode);
						if (プロパティ表示ToolStripMenuItem.Checked == true) rootNode.Nodes.Add(antNode);
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
			//try
			//{
			//  if (Path.GetDirectoryName(file) == Path.GetDirectoryName(PluginBase.CurrentProject.ProjectPath))
			//  {
			//    rootNode.Expand();
			//  }
			//}
			//catch { }
			return rootNode;
		}

		public TreeNode GetBuildFileNode_org(string file)
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

			rootNode.Tag = file;

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
			targetNode.Tag = node;
			return targetNode;
		}

		public void LoadIn(string path)
		{
			try
			{
				//path = PluginBase.MainForm.ProcessArgString(path);
				if (Path.GetExtension(path) == ".xml")
				{
					//string[] s1 = new string[1] { PluginBase.MainForm.CurrentDocument.FileName };
					//String[] filenames = new String[];
					//AddBuildFiles(s1);
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
						//this.AddPreviousCustomDocuments(path);
						treeView.EndUpdate();
					}
				}
			}
			catch (Exception ex1)
			{
				MessageBox.Show(ex1.Message.ToString(), "PluginUI:LoadIn:1375");
			}
		}

		private void 表示ToolStripButton_Click(object sender, EventArgs e)
		{
			int num = this.toggleIndex % 3;

			if (this.treeView.SelectedNode != null)
			{
				this.propertyGrid1.SelectedObject = null;
				if (this.treeView.SelectedNode.Tag is NodeInfo)
				{
					this.propertyGrid1.SelectedObject = this.treeView.SelectedNode.Tag as NodeInfo;
				}
				else if (this.treeView.SelectedNode.Tag is TaskInfo)
				{
					this.propertyGrid1.SelectedObject = this.treeView.SelectedNode.Tag as TaskInfo;
				}
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
					else if (Lib.IsWebSite(path))
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

		/// <summary>
		/// TODO: 外部TreeView使用の場合
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ShowOuterXmlClick(object sender, EventArgs e)
		{
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
		}

		private void アイコン表示MenuItem_Click(object sender, EventArgs e)
		{
			//this.i
		}

		private void カスタマイズToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//this.playerPToolStripMenuItem.Checked = false;
			//this.richTextRToolStripMenuItem.Checked = false;
			//this.propertyGridGToolStripMenuItem.Checked = true;
			//((Form)this.Parent).Text = Path.GetFileNameWithoutExtension(this.axWindowsMediaPlayer1.URL);
			//((DockContent)base.Parent).TabText = "環境設定";// Path.GetFileName(this.axWindowsMediaPlayer1.URL);
			//this.propertyGrid3.SelectedObject = this.settings;
			tabControl1.SelectedIndex = 4;
			//this.propertyGrid1.BringToFront();

		}

		private void 試験ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// 次の例では、現在選択されているコード セクションで、
			//"somestring" という語を、大文字と小文字を区別して検索します。
			//Edit.Find somestring / sel /case  
			//Process.Start(this.devenv15Path, "/Edit "+ this.itemPath+ " /Command \"Edit.Find IMainForm /doc\"");
			Process.Start(this.devenv15Path, "/Edit "+ this.itemPath+ " /Command \"Edit.Goto 200\"");
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
			this.settings.Save();
		}

		private void propertyGrid3_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			//MessageBox.Show("propertyGrid1_PropertyValueChanged");
			this.settings.Save();
			this.ApplySettings();
		}

    public void OpenDocument(String path)
    {
      if (Lib.IsEditable(path)) Process.Start(this.devenv15Path, "/edit " + "\"" + path + "\"");
      else Process.Start(path);
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

    private void gitButton_Click(object sender, EventArgs e)
    {
      Directory.SetCurrentDirectory(Path.GetDirectoryName(this.projectPath));
      Process.Start(this.settings.GitPath);
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

	public class AntTreeNode : TreeNode
	{
		public String File;
		public String Target;

		public AntTreeNode(string text, int imageIndex)
				: base(text, imageIndex, imageIndex)
		{
		}
	}

}
