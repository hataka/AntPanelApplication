using System;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using AntPlugin;
using AntPlugin.CommonLibrary;
using AntPlugin.XmlTreeMenu;
using System.Diagnostics;
using AntPanelApplication;
using CommonLibrary;

namespace AntPlugin.Controls
{
	public class DirTreePanel : UserControl
	{
		private ToolStrip toolStrip1;
		private ToolStripButton ヘルプLToolStripButton;
		private Panel panel1;
		//private PluginMain pluginMain;
		private AntPanel antPanel;
    global::AntPanelApplication.Properties.Settings settings;

    private ToolStripButton 新規作成NToolStripButton;
		private ToolStripButton 開くOToolStripButton;
		private ToolStripButton 上書き保存SToolStripButton;
		private ToolStripButton 印刷PToolStripButton;
		private ToolStripSeparator toolStripSeparator;
		private ToolStripButton 切り取りUToolStripButton;
		private ToolStripButton コピーCToolStripButton;
		private ToolStripButton 貼り付けPToolStripButton;
		private ToolStripSeparator toolStripSeparator2;
		public DirTreeView dirTreeView1;
		private SplitContainer splitContainer1;
		public PropertyGrid propertyGrid1;
		private ToolStripButton toolStripButton1;
		private TreeView treeView1;
		private Panel panel2;
		private ListView listView1;
		private Panel panel3;
		private TreeView treeView2;
		private ToolStripButton homeStripButton;
		private string currentProjectPath = String.Empty;
    private ToolStripButton upButton;
    private ToolStripButton downButton;
    private ToolStripButton projectButton;
    private ToolStripButton itemButton;
    private string currentRootDir = String.Empty;

    /*
		public DirTreePanel(PluginMain pluginMain)
		{
			this.pluginMain = pluginMain;
			this.InitializeComponent();
		}
		*/
    public DirTreePanel(AntPanel ui)
		{
			this.antPanel = ui;
			this.InitializeComponent();
      this.settings = new global::AntPanelApplication.Properties.Settings();
    }

		public DirTreePanel(AntPanel ui, String rootPath)
		{
			this.InitializeComponent();
			this.antPanel = ui;
			this.currentProjectPath = rootPath;
      this.currentRootDir = Path.GetDirectoryName(rootPath);
      InitializDirTreeView();
      this.settings = new global::AntPanelApplication.Properties.Settings();
    }

    /// <summary>
    /// Accessor to the RichTextBox
    /// </summary>
    /*    
		public RichTextBox Output
				{
						get { return this.richTextBox; }
				}
		*/

    #region Windows Forms Designer Generated Code

    /// <summary>
    /// This method is required for Windows Forms designer support.
    /// Do not change the method contents inside the source code editor. The Forms designer might
    /// not be able to load this method if it was changed manually.
    /// </summary>
    private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirTreePanel));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.新規作成NToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.開くOToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.上書き保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.印刷PToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.切り取りUToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.コピーCToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.貼り付けPToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.ヘルプLToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.homeStripButton = new System.Windows.Forms.ToolStripButton();
      this.panel1 = new System.Windows.Forms.Panel();
      this.treeView1 = new System.Windows.Forms.TreeView();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
      this.panel2 = new System.Windows.Forms.Panel();
      this.listView1 = new System.Windows.Forms.ListView();
      this.panel3 = new System.Windows.Forms.Panel();
      this.treeView2 = new System.Windows.Forms.TreeView();
      this.upButton = new System.Windows.Forms.ToolStripButton();
      this.downButton = new System.Windows.Forms.ToolStripButton();
      this.projectButton = new System.Windows.Forms.ToolStripButton();
      this.itemButton = new System.Windows.Forms.ToolStripButton();
      this.toolStrip1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成NToolStripButton,
            this.開くOToolStripButton,
            this.上書き保存SToolStripButton,
            this.印刷PToolStripButton,
            this.toolStripSeparator,
            this.切り取りUToolStripButton,
            this.コピーCToolStripButton,
            this.貼り付けPToolStripButton,
            this.toolStripSeparator2,
            this.toolStripButton1,
            this.ヘルプLToolStripButton,
            this.homeStripButton,
            this.upButton,
            this.downButton,
            this.projectButton,
            this.itemButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(787, 27);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // 新規作成NToolStripButton
      // 
      this.新規作成NToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.新規作成NToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("新規作成NToolStripButton.Image")));
      this.新規作成NToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.新規作成NToolStripButton.Name = "新規作成NToolStripButton";
      this.新規作成NToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.新規作成NToolStripButton.Text = "新規作成(&N)";
      // 
      // 開くOToolStripButton
      // 
      this.開くOToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.開くOToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripButton.Image")));
      this.開くOToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.開くOToolStripButton.Name = "開くOToolStripButton";
      this.開くOToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.開くOToolStripButton.Text = "開く(&O)";
      this.開くOToolStripButton.Click += new System.EventHandler(this.開くOToolStripButton_Click);
      // 
      // 上書き保存SToolStripButton
      // 
      this.上書き保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.上書き保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("上書き保存SToolStripButton.Image")));
      this.上書き保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.上書き保存SToolStripButton.Name = "上書き保存SToolStripButton";
      this.上書き保存SToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.上書き保存SToolStripButton.Text = "上書き保存(&S)";
      // 
      // 印刷PToolStripButton
      // 
      this.印刷PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.印刷PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripButton.Image")));
      this.印刷PToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.印刷PToolStripButton.Name = "印刷PToolStripButton";
      this.印刷PToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.印刷PToolStripButton.Text = "印刷(&P)";
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
      // 
      // 切り取りUToolStripButton
      // 
      this.切り取りUToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.切り取りUToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("切り取りUToolStripButton.Image")));
      this.切り取りUToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.切り取りUToolStripButton.Name = "切り取りUToolStripButton";
      this.切り取りUToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.切り取りUToolStripButton.Text = "切り取り(&U)";
      // 
      // コピーCToolStripButton
      // 
      this.コピーCToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.コピーCToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripButton.Image")));
      this.コピーCToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.コピーCToolStripButton.Name = "コピーCToolStripButton";
      this.コピーCToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.コピーCToolStripButton.Text = "コピー(&C)";
      // 
      // 貼り付けPToolStripButton
      // 
      this.貼り付けPToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.貼り付けPToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripButton.Image")));
      this.貼り付けPToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.貼り付けPToolStripButton.Name = "貼り付けPToolStripButton";
      this.貼り付けPToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.貼り付けPToolStripButton.Text = "貼り付け(&P)";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton1.Text = "toolStripButton1";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // ヘルプLToolStripButton
      // 
      this.ヘルプLToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ヘルプLToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ヘルプLToolStripButton.Image")));
      this.ヘルプLToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ヘルプLToolStripButton.Name = "ヘルプLToolStripButton";
      this.ヘルプLToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.ヘルプLToolStripButton.Text = "ヘルプ(&L)";
      this.ヘルプLToolStripButton.Click += new System.EventHandler(this.ヘルプLToolStripButton_Click);
      // 
      // homeStripButton
      // 
      this.homeStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.homeStripButton.Image = ((System.Drawing.Image)(resources.GetObject("homeStripButton.Image")));
      this.homeStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.homeStripButton.Name = "homeStripButton";
      this.homeStripButton.Size = new System.Drawing.Size(24, 24);
      this.homeStripButton.Text = "homeStripButton";
      this.homeStripButton.Click += new System.EventHandler(this.homeStripButton_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.treeView1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(787, 363);
      this.panel1.TabIndex = 1;
      // 
      // treeView1
      // 
      this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView1.Location = new System.Drawing.Point(0, 0);
      this.treeView1.Name = "treeView1";
      this.treeView1.Size = new System.Drawing.Size(787, 363);
      this.treeView1.TabIndex = 0;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 27);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
      this.splitContainer1.Size = new System.Drawing.Size(787, 612);
      this.splitContainer1.SplitterDistance = 363;
      this.splitContainer1.TabIndex = 2;
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertyGrid1.HelpVisible = false;
      this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(787, 245);
      this.propertyGrid1.TabIndex = 0;
      this.propertyGrid1.ToolbarVisible = false;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.listView1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 27);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(787, 612);
      this.panel2.TabIndex = 2;
      // 
      // listView1
      // 
      this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView1.Location = new System.Drawing.Point(0, 0);
      this.listView1.Name = "listView1";
      this.listView1.Size = new System.Drawing.Size(787, 612);
      this.listView1.TabIndex = 0;
      this.listView1.UseCompatibleStateImageBehavior = false;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.treeView2);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(0, 27);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(787, 612);
      this.panel3.TabIndex = 3;
      // 
      // treeView2
      // 
      this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView2.Location = new System.Drawing.Point(0, 0);
      this.treeView2.Name = "treeView2";
      this.treeView2.Size = new System.Drawing.Size(787, 612);
      this.treeView2.TabIndex = 0;
      // 
      // upButton
      // 
      this.upButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
      this.upButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.upButton.Name = "upButton";
      this.upButton.Size = new System.Drawing.Size(24, 24);
      this.upButton.Text = "upButton";
      this.upButton.ToolTipText = "上のディレクトリに移動します";
      this.upButton.Click += new System.EventHandler(this.upButton_Click);
      // 
      // downButton
      // 
      this.downButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.downButton.Image = ((System.Drawing.Image)(resources.GetObject("downButton.Image")));
      this.downButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.downButton.Name = "downButton";
      this.downButton.Size = new System.Drawing.Size(24, 24);
      this.downButton.Text = "downButton";
      this.downButton.ToolTipText = "選択項目のフォルダに移動します";
      this.downButton.Click += new System.EventHandler(this.downButton_Click);
      // 
      // projectButton
      // 
      this.projectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.projectButton.Image = ((System.Drawing.Image)(resources.GetObject("projectButton.Image")));
      this.projectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.projectButton.Name = "projectButton";
      this.projectButton.Size = new System.Drawing.Size(24, 24);
      this.projectButton.Text = "projectButton";
      this.projectButton.ToolTipText = "プロジェクトフォルダに移動します";
      this.projectButton.Click += new System.EventHandler(this.projectButton_Click);
      // 
      // itemButton
      // 
      this.itemButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.itemButton.Image = ((System.Drawing.Image)(resources.GetObject("itemButton.Image")));
      this.itemButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.itemButton.Name = "itemButton";
      this.itemButton.Size = new System.Drawing.Size(24, 24);
      this.itemButton.Text = "itemButton";
      this.itemButton.ToolTipText = "アイテムフォルダに移動します";
      this.itemButton.Click += new System.EventHandler(this.itemButton_Click);
      // 
      // DirTreePanel
      // 
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel3);
      this.Controls.Add(this.toolStrip1);
      this.Name = "DirTreePanel";
      this.Size = new System.Drawing.Size(787, 639);
      this.Load += new System.EventHandler(this.PluginUI_Load);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private void PluginUI_Load(object sender, EventArgs e)
		{
			InitializDirTreeView();
		}
		#region InitializDirTreeView

		public void InitializDirTreeView()
		{
      Boolean projectExists = (File.Exists(this.currentProjectPath));
      Enabled = projectExists;

      if (this.dirTreeView1 == null)
      {
        //Projectと同期廃止 親ディレクトリに移動できない FileExplorerを使用する
        if (projectExists)
        {
          this.dirTreeView1 = new DirTreeView(Path.GetDirectoryName(this.antPanel.projectPath));
        }
        else
        {
          this.dirTreeView1 = new DirTreeView();
        }
      }

      this.SuspendLayout();

			this.splitContainer1.Panel2Collapsed = true;
			this.splitContainer1.Panel1Collapsed = false;
			this.propertyGrid1.HelpVisible = false;
			this.propertyGrid1.ToolbarVisible = false;

			// 
			// dirTreeView1
			// 
			this.dirTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dirTreeView1.ImageIndex = 0;
			this.dirTreeView1.Location = new System.Drawing.Point(0, 2);
			this.dirTreeView1.Name = "dirTreeView1";
			this.dirTreeView1.SelectedImageIndex = 0;
			this.dirTreeView1.Size = new System.Drawing.Size(221, 361);
			this.dirTreeView1.TabIndex = 0;
			this.dirTreeView1.DoubleClick += new System.EventHandler(this.dirTreeView1_DoubleClick);
			this.dirTreeView1.BeforeExpand
				+= new System.Windows.Forms.TreeViewCancelEventHandler(this.dirTreeView1_BeforeExpand);
			this.dirTreeView1.AfterSelect
				+= new System.Windows.Forms.TreeViewEventHandler(this.dirTreeView1_AfterSelect);
			this.dirTreeView1.サクラエディタToolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_サクラエディタToolStripMenuItem1_Click);
			this.dirTreeView1.pSPadToolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_pSPadToolStripMenuItem1_Click);
			this.dirTreeView1.azukiControlToolStripMenuItem3.Click
				+= new System.EventHandler(this.dirTreeView1_azukiControlToolStripMenuItem3_Click);
			this.dirTreeView1.richTextEditorToolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_richTextEditorToolStripMenuItem1_Click);
			this.dirTreeView1.エクスプローラToolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_エクスプローラToolStripMenuItem1_Click);
			this.dirTreeView1.コマンドプロンプトToolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_コマンドプロンプトToolStripMenuItem1_Click);
			//this.viewerToolStripMenuItem.Click
			//	+= new System.EventHandler(this.viewerToolStripMenuItem_Click);
			this.dirTreeView1.viewerToolStripMenuItem.Click
				+= new System.EventHandler(this.dirTreeView1_viewerToolStripMenuItem_Click);
			this.dirTreeView1.システムプログラムToolStripMenuItem.Click
				+= new System.EventHandler(this.dirTreeView1_システムプログラムToolStripMenuItem_Click);
			this.dirTreeView1.開くtoolStripMenuItem1.Click
			 += new System.EventHandler(this.dirTreeView1_開くtoolStripMenuItem1_Click);
			this.dirTreeView1.ブラウザで開くtoolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_ブラウザで開くtoolStripMenuItem1_Click);
			this.dirTreeView1.コンテキストメニューtoolStripMenuItem1.Click
				+= new System.EventHandler(this.dirTreeView1_コンテキストメニューtoolStripMenuItem1_Click);
			this.dirTreeView1.ファイルエクスプローラと同期toolStripMenuItem.Click += new System.EventHandler(this.ファイルエクスプローラと同期toolStripMenuItem_Click);
			this.dirTreeView1.Antツリーに追加toolStripMenuItem.Click += new System.EventHandler(this.Antツリーに追加toolStripMenuItem_Click);

			this.panel1.Controls.Add(dirTreeView1);
			this.dirTreeView1.BringToFront();
			this.ResumeLayout(false);
		}

		// 一応完成 
		private void dirTreeView1_DoubleClick(object sender, System.EventArgs e)
		{
			String path = this.dirTreeView1.filepath;
			if (System.IO.Directory.Exists(path)) return;
			DirectoryInfo selectedPath = new DirectoryInfo(path);//using System.IO
			try
			{
        // TODO 実装
        //PluginBase.MainForm.OpenEditableDocument(path);
        antPanel.OpenDocument(path);
      }
			catch (Exception exc)
			{
				String errMsg = exc.Message.ToString();
				//MessageBox.Show(exc.Message.ToString());
			}
		}

		private void dirTreeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			TreeNode selectedNode = e.Node;   //引数のイベント発生ノードに対し追加・削除・複製
			//DirectoryInfo selectedDir = new DirectoryInfo(selectedNode.FullPath);//using System.IO
			//this.dirTreeView1.Nodes.Clear();
			//FolderTreeBox.dirTreeView1.InitializeDirTreeView(selectedNode.FullPath);
		}

		private void dirTreeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			String path = this.dirTreeView1.filepath;

			this.ShowNodeInfo(path);
			//ListViewにファイル一覧を表示
			DirectoryInfo di = new DirectoryInfo(e.Node.FullPath);
			if ((di.Attributes & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
			{
				//this.listView1.Clear();//これがないとファイル表示が累積
			}
		}

		// dirTreeView1 
		private void dirTreeView1_サクラエディタToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ProcessHandler.Run_Sakura(this.dirTreeView1.filepath);
		}

		// dirTreeView1 
		private void dirTreeView1_pSPadToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ProcessHandler.Run_PSPad(this.dirTreeView1.filepath);
		}

		// dirTreeView1 
		private void dirTreeView1_viewerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//MainForm.Run_PHPViewer(DirTreeBox.dirTreeView1.filepath);
		}
		// dirTreeView1 
		private void dirTreeView1_コマンドプロンプトToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ProcessHandler.Run_Cmd(this.dirTreeView1.filepath);
		}
		// dirTreeView1 
		private void dirTreeView1_richTextEditorToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			String path = this.dirTreeView1.filepath;
			if (System.IO.Directory.Exists(path)) return;
			try
			{
				// HACK - dirTreeView1_richTextEditorToolStripMenuItem1_Click
				//this.OpenDocument(path);
				//TODO 実装
				//PluginBase.MainForm.CallCommand("PluginCommand",
				//  "XMLTreeMenu.CreateCustomDocument;RichTextEditor|" + path);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message.ToString());
			}


		}
		// dirTreeView1 
		private void dirTreeView1_azukiControlToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			//MainForm.Run_AzukiControl(DirTreeBox.dirTreeView1.filepath);
			String path = this.dirTreeView1.filepath;
			if (System.IO.Directory.Exists(path)) return;
			try
			{
				// HACK - dirTreeView1_azukiControlToolStripMenuItem3_Click
				//this.OpenDocument(path);

				// TODO 実装
				//PluginBase.MainForm.CallCommand("PluginCommand",
				//  "XMLTreeMenu.CreateCustomDocument;AzukiEditor|" + path);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message.ToString());
			}
		}
		// dirTreeView1 
		private void dirTreeView1_エクスプローラToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			//ProcessHandler.Run_Explorer(this.dirTreeView1.filepath);
			Process.Start(@"C:\windows\explorer.exe", "/select," + this.dirTreeView1.filepath);
		}
		// dirTreeView1 
		private void dirTreeView1_システムプログラムToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProcessHandler.Run_SystemProcess(this.dirTreeView1.filepath);
		}


		private void dirTreeView1_開くtoolStripMenuItem1_Click(object sender, EventArgs e)
		{
			String path = this.dirTreeView1.filepath;
			if (System.IO.Directory.Exists(path)) return;
			try
			{
        // TODO 実装
        //PluginBase.MainForm.OpenEditableDocument(path);
        antPanel.OpenDocument(path);
      }
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message.ToString());
			}
		}

		private void dirTreeView1_ブラウザで開くtoolStripMenuItem1_Click(object sender, EventArgs e)
		{
			String path = this.dirTreeView1.filepath.ToLower();
			String url = path.Replace(@"f:\", "http://localhost/f/");
			url = url.Replace(@"c:\apache2.2\htdocs", "http://localhost");
			url = url.Replace("c:", "file:///c");
			url = url.Replace("\\", "/");
      // TODO 実装
      //PluginBase.MainForm.CallCommand("Browse", url);
      Process.Start(settings.ChromePath, url);
    }

		private void dirTreeView1_コンテキストメニューtoolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				FileInfo[] selectedPathsAndFiles = new FileInfo[1];
				// 選択ノードのPathを取得
				String path = this.dirTreeView1.filepath;
				if (!File.Exists(path)) return;
				ShellContextMenu scm = new ShellContextMenu();
				Point location = new Point(this.dirTreeView1.contextMenuStrip1.Bounds.Left,
																		this.dirTreeView1.contextMenuStrip1.Bounds.Top);
				selectedPathsAndFiles[0] = new FileInfo(path);
				this.dirTreeView1.contextMenuStrip1.Hide(); // Hide default menu
				scm.ShowContextMenu(selectedPathsAndFiles, location);
			}
			catch
			{
				MessageBox.Show("ノードを選択してください");
			}
		}


		private void ファイルエクスプローラと同期toolStripMenuItem_Click(object sender, EventArgs e)
		{
			String path = Path.GetDirectoryName(this.dirTreeView1.filepath);
			if (System.IO.Directory.Exists(this.dirTreeView1.filepath)) path = this.dirTreeView1.filepath;
			try
			{
				//PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.BrowseTo;" + path);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message.ToString());
			}

		}

		private void Antツリーに追加toolStripMenuItem_Click(object sender, EventArgs e)
		{
			string[] files = new String[1] { this.dirTreeView1.filepath };
			try
			{
				this.antPanel.AddBuildFiles(files);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message.ToString());

			}
		}

    private void ヘルプLToolStripButton_Click(object sender, EventArgs e)
		{
			String path = "http://localhost/f/VCSharp/Flashdevelop5.1.1-LL/External/Plugins/DirTreePanel/doxygen/html/index.html";
      //PluginBase.MainForm.CallCommand("Browse", path);
      Process.Start(settings.ChromePath, path);
        
    }

		public TreeNode RecursiveCreateDirectoryNode(DirectoryInfo directoryInfo)
		{
			NodeInfo ni = new NodeInfo();
			ni.Path = directoryInfo.FullName;
			ni.Type = "folder";
			if (String.IsNullOrEmpty(ni.Title)) ni.Title = Path.GetFileName(directoryInfo.Name);
			// FIXME
			int imageIndex = this.antPanel.menuTree.GetIconImageIndex(@"C:\windows");
			//this.getImageIndexFromNodeInfo_safe(ni);
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
				imageIndex = this.antPanel.menuTree.GetIconImageIndex(ni.Path);
				TreeNode fileNode = new TreeNode(ni.Title, imageIndex, imageIndex);

				fileNode.Tag = ni;
				fileNode.ToolTipText = ni.Path;

				//directoryNode.Nodes.Add(new TreeNode(file.Name));
				directoryNode.Nodes.Add(fileNode);
			}
			return directoryNode;
		}
		//////////////////////////////////////////////////	
		#endregion

		private void ShowNodeInfo(String path)
		{
			if (Directory.Exists(path))
			{
				DirectoryInfo di = new DirectoryInfo(path);
				this.propertyGrid1.SelectedObject = di;
			}
			else if (File.Exists(path))
			{
				FileInfo fi = new FileInfo(path);
				this.propertyGrid1.SelectedObject = fi;
			}
		}

    // no refer
		private void ShowNodeInfo(TreeNode treeNode)
		{
			if (treeNode != null && treeNode.Tag != null)
			{
					NodeInfo selectedObject = new NodeInfo();
				selectedObject = (NodeInfo)treeNode.Tag;
				this.propertyGrid1.SelectedObject = selectedObject;
			}
		}

		private int toggleIndex = 1;
		private void toolStripButton1_Click(object sender, EventArgs e)
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

		private void 開くOToolStripButton_Click(object sender, EventArgs e)
		{
      String filepath = @"F:\codingground\java\swt.snippets8\Snippet005\Snippet5.java";
      //FolderBrowserDialogクラスのインスタンスを作成
      FolderBrowserDialog fbd = new FolderBrowserDialog();

      //上部に表示する説明テキストを指定する
      fbd.Description = "フォルダを指定してください。";
      //ルートフォルダを指定する
      //デフォルトでDesktop
      fbd.RootFolder = Environment.SpecialFolder.Desktop;
      //最初に選択するフォルダを指定する
      //RootFolder以下にあるフォルダである必要がある
      fbd.SelectedPath = @"F:\";
      //ユーザーが新しいフォルダを作成できるようにする
      //デフォルトでTrue
      fbd.ShowNewFolderButton = true;

      //ダイアログを表示する
      if (fbd.ShowDialog(this) == DialogResult.OK)
      {
        //選択されたフォルダを表示する
        Console.WriteLine(fbd.SelectedPath);
        filepath = fbd.SelectedPath;
      }

      if (Directory.Exists(filepath)) this.currentRootDir = filepath;
      //else if(File.Exists(filepath)) this.currentRootDir = Path.GetDirectoryName(antPanel.projectPath);

      this.Controls.Remove(this.dirTreeView1);
      this.dirTreeView1 = new DirTreeView(this.currentRootDir);
      InitializDirTreeView();
		}

		private void homeStripButton_Click(object sender, EventArgs e)
		{
			this.Controls.Remove(this.dirTreeView1);
			this.dirTreeView1.Dispose();
			this.dirTreeView1 = new DirTreeView();
      InitializDirTreeView();
		}

    private void upButton_Click(object sender, EventArgs e)
    {
      this.Controls.Remove(this.dirTreeView1);
      this.dirTreeView1.Dispose();
      this.currentRootDir = Path.GetDirectoryName(this.currentRootDir);
      this.dirTreeView1 = new DirTreeView(this.currentRootDir);
      InitializDirTreeView();
    }

    private void downButton_Click(object sender, EventArgs e)
    {
      this.Controls.Remove(this.dirTreeView1);
      this.dirTreeView1.Dispose();
      if(Directory.Exists(this.dirTreeView1.filepath))
      {
        this.currentRootDir = this.dirTreeView1.filepath;
      }
      else if(File.Exists(this.dirTreeView1.filepath))
      {
        this.currentRootDir = Path.GetDirectoryName(this.dirTreeView1.filepath);
      }
      this.dirTreeView1 = new DirTreeView(this.currentRootDir);
      InitializDirTreeView();
    }

    private void itemButton_Click(object sender, EventArgs e)
    {
      this.Controls.Remove(this.dirTreeView1);
      this.dirTreeView1.Dispose();
      if (Directory.Exists(antPanel.itemPath))
      {
        this.currentRootDir = antPanel.itemPath;
      }
      else if (File.Exists(antPanel.itemPath))
      {
        this.currentRootDir = Path.GetDirectoryName(antPanel.itemPath);
      }
      this.dirTreeView1 = new DirTreeView(this.currentRootDir);
      InitializDirTreeView();
    }

    private void projectButton_Click(object sender, EventArgs e)
    {
      this.Controls.Remove(this.dirTreeView1);
      this.dirTreeView1.Dispose();
      if (File.Exists(antPanel.projectPath))
      {
        this.currentRootDir = Path.GetDirectoryName(antPanel.projectPath);
      }
      this.dirTreeView1 = new DirTreeView(this.currentRootDir);
      InitializDirTreeView();

    }
  }
}
