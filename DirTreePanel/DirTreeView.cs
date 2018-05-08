using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.IO;

using System.Runtime.InteropServices;
using System.Diagnostics;

public class DirTreeView : TreeView
{
	private IContainer components;
	public ImageList imageList1;
	public Hashtable _systemIcons = new Hashtable();
	public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	public System.Windows.Forms.ToolStripMenuItem サクラエディタToolStripMenuItem1;
	public System.Windows.Forms.ToolStripMenuItem pSPadToolStripMenuItem1;
	public System.Windows.Forms.ToolStripMenuItem azukiControlToolStripMenuItem3;
	public System.Windows.Forms.ToolStripMenuItem richTextEditorToolStripMenuItem1;
	public System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
	public System.Windows.Forms.ToolStripMenuItem エクスプローラToolStripMenuItem1;
	public System.Windows.Forms.ToolStripMenuItem コマンドプロンプトToolStripMenuItem1;
	public System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
	public System.Windows.Forms.ToolStripMenuItem viewerToolStripMenuItem;
	public System.Windows.Forms.ToolStripMenuItem システムプログラムToolStripMenuItem;
	public ToolStripMenuItem 開くtoolStripMenuItem1;
	public ToolStripMenuItem ブラウザで開くtoolStripMenuItem1;
	public ToolStripSeparator toolStripSeparator1;
	public ToolStripMenuItem コンテキストメニューtoolStripMenuItem1;
  //public MDIForm.MDIParent1 MainForm = MDIForm.MDIParent1.IMainForm;
  public TreeNode rootNode = new TreeNode();
  public ToolStripSeparator toolStripSeparator2;
  public ToolStripMenuItem ファイルエクスプローラと同期toolStripMenuItem;
  public ToolStripMenuItem Antツリーに追加toolStripMenuItem;
  private ToolStripMenuItem サクラエディタToolStripMenuItem2;
  private ToolStripMenuItem PSPadtoolStripMenuItem2;
  private ToolStripMenuItem AzukitoolStripMenuItem2;
  private ToolStripMenuItem RichTexttoolStripMenuItem2;
  private ToolStripMenuItem DocumenttoolStripMenuItem2;
  private ToolStripMenuItem chrometoolStripMenuItem2;
  private ToolStripMenuItem IEtoolStripMenuItem2;
  private ToolStripMenuItem BrowserExtoolStripMenuItem2;
  private ToolStripMenuItem BrowsertoolStripMenuItem2;
  public  ToolStripMenuItem openAntPanel;
  public String filepath;

	/// <summary>
	/// 使用中のリソースをすべてクリーンアップします。
	/// </summary>
	/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	public DirTreeView()
	{
		InitializeComponent();
		InitializeDirTreeView();
	}

	public DirTreeView(String folder)
	{
		InitializeComponent();
		InitializeDirTreeView(folder);
	}

  public static TreeNode DirNode(string dir, TreeNode parentNode=null)
  {
    MessageBox.Show(dir);
    if (parentNode == null)
    {
      parentNode = new TreeNode(Path.GetFileName(dir));
    }
    try
    {
      foreach (string f in Directory.GetFiles(dir))
      {
        TreeNode fileNode = new TreeNode(Path.GetFileName(f));
        //Console.WriteLine(f);
        parentNode.Nodes.Add(fileNode);
      }
      foreach (string d in Directory.GetDirectories(dir))
      {
        //Console.WriteLine(d);
        //DirSearch(d);
      }

    }
    catch (System.Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
    return parentNode;
    /*
    try
    {
      foreach (string f in Directory.GetFiles(dir))
        Console.WriteLine(f);
      foreach (string d in Directory.GetDirectories(dir))
      {
        Console.WriteLine(d);
        DirSearch(d);
      }

    }
    catch (System.Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
    */
  }

  private void InitializeComponent()
	{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirTreeView));
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.開くtoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.サクラエディタToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.PSPadtoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.AzukitoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.RichTexttoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.DocumenttoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.ブラウザで開くtoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.chrometoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.IEtoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.BrowserExtoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.BrowsertoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.サクラエディタToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.pSPadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.azukiControlToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
      this.richTextEditorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
      this.エクスプローラToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.コマンドプロンプトToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
      this.viewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.システムプログラムToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.コンテキストメニューtoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.ファイルエクスプローラと同期toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.Antツリーに追加toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openAntPanel = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "bold.bmp");
      this.imageList1.Images.SetKeyName(1, "");
      this.imageList1.Images.SetKeyName(2, "");
      this.imageList1.Images.SetKeyName(3, "");
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開くtoolStripMenuItem1,
            this.ブラウザで開くtoolStripMenuItem1,
            this.toolStripSeparator1,
            this.サクラエディタToolStripMenuItem1,
            this.pSPadToolStripMenuItem1,
            this.azukiControlToolStripMenuItem3,
            this.richTextEditorToolStripMenuItem1,
            this.toolStripSeparator19,
            this.エクスプローラToolStripMenuItem1,
            this.コマンドプロンプトToolStripMenuItem1,
            this.toolStripSeparator20,
            this.viewerToolStripMenuItem,
            this.システムプログラムToolStripMenuItem,
            this.コンテキストメニューtoolStripMenuItem1,
            this.toolStripSeparator2,
            this.ファイルエクスプローラと同期toolStripMenuItem,
            this.Antツリーに追加toolStripMenuItem,
            this.openAntPanel});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(240, 392);
      this.contextMenuStrip1.TabStop = true;
      // 
      // 開くtoolStripMenuItem1
      // 
      this.開くtoolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.サクラエディタToolStripMenuItem2,
            this.PSPadtoolStripMenuItem2,
            this.AzukitoolStripMenuItem2,
            this.RichTexttoolStripMenuItem2,
            this.DocumenttoolStripMenuItem2});
      this.開くtoolStripMenuItem1.Name = "開くtoolStripMenuItem1";
      this.開くtoolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
      this.開くtoolStripMenuItem1.Text = "開く";
      this.開くtoolStripMenuItem1.Click += new System.EventHandler(this.開くtoolStripMenuItem1_Click);
      // 
      // サクラエディタToolStripMenuItem2
      // 
      this.サクラエディタToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("サクラエディタToolStripMenuItem2.Image")));
      this.サクラエディタToolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Black;
      this.サクラエディタToolStripMenuItem2.Name = "サクラエディタToolStripMenuItem2";
      this.サクラエディタToolStripMenuItem2.Size = new System.Drawing.Size(189, 26);
      this.サクラエディタToolStripMenuItem2.Text = "サクラエディタ";
      this.サクラエディタToolStripMenuItem2.Click += new System.EventHandler(this.サクラエディタToolStripMenuItem1_Click);
      // 
      // PSPadtoolStripMenuItem2
      // 
      this.PSPadtoolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("PSPadtoolStripMenuItem2.Image")));
      this.PSPadtoolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Black;
      this.PSPadtoolStripMenuItem2.Name = "PSPadtoolStripMenuItem2";
      this.PSPadtoolStripMenuItem2.Size = new System.Drawing.Size(189, 26);
      this.PSPadtoolStripMenuItem2.Text = "PSPadEditor";
      this.PSPadtoolStripMenuItem2.Click += new System.EventHandler(this.pSPadToolStripMenuItem1_Click);
      // 
      // AzukitoolStripMenuItem2
      // 
      this.AzukitoolStripMenuItem2.Name = "AzukitoolStripMenuItem2";
      this.AzukitoolStripMenuItem2.Size = new System.Drawing.Size(189, 26);
      this.AzukitoolStripMenuItem2.Text = "AzukiEditor";
      this.AzukitoolStripMenuItem2.Click += new System.EventHandler(this.azukiControlToolStripMenuItem3_Click);
      // 
      // RichTexttoolStripMenuItem2
      // 
      this.RichTexttoolStripMenuItem2.Name = "RichTexttoolStripMenuItem2";
      this.RichTexttoolStripMenuItem2.Size = new System.Drawing.Size(189, 26);
      this.RichTexttoolStripMenuItem2.Text = "RichTextEditor";
      this.RichTexttoolStripMenuItem2.Click += new System.EventHandler(this.richTextEditorToolStripMenuItem1_Click);
      // 
      // DocumenttoolStripMenuItem2
      // 
      this.DocumenttoolStripMenuItem2.Name = "DocumenttoolStripMenuItem2";
      this.DocumenttoolStripMenuItem2.Size = new System.Drawing.Size(189, 26);
      this.DocumenttoolStripMenuItem2.Text = "Document";
      this.DocumenttoolStripMenuItem2.Click += new System.EventHandler(this.開くtoolStripMenuItem1_Click);
      // 
      // ブラウザで開くtoolStripMenuItem1
      // 
      this.ブラウザで開くtoolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chrometoolStripMenuItem2,
            this.IEtoolStripMenuItem2,
            this.BrowserExtoolStripMenuItem2,
            this.BrowsertoolStripMenuItem2});
      this.ブラウザで開くtoolStripMenuItem1.Name = "ブラウザで開くtoolStripMenuItem1";
      this.ブラウザで開くtoolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
      this.ブラウザで開くtoolStripMenuItem1.Text = "ブラウザで開く";
      this.ブラウザで開くtoolStripMenuItem1.Click += new System.EventHandler(this.ブラウザで開くtoolStripMenuItem1_Click);
      // 
      // chrometoolStripMenuItem2
      // 
      this.chrometoolStripMenuItem2.Name = "chrometoolStripMenuItem2";
      this.chrometoolStripMenuItem2.Size = new System.Drawing.Size(211, 26);
      this.chrometoolStripMenuItem2.Text = "Chrome";
      // 
      // IEtoolStripMenuItem2
      // 
      this.IEtoolStripMenuItem2.Name = "IEtoolStripMenuItem2";
      this.IEtoolStripMenuItem2.Size = new System.Drawing.Size(211, 26);
      this.IEtoolStripMenuItem2.Text = "InterNet Explorer";
      // 
      // BrowserExtoolStripMenuItem2
      // 
      this.BrowserExtoolStripMenuItem2.Name = "BrowserExtoolStripMenuItem2";
      this.BrowserExtoolStripMenuItem2.Size = new System.Drawing.Size(211, 26);
      this.BrowserExtoolStripMenuItem2.Text = "BrowserEx";
      // 
      // BrowsertoolStripMenuItem2
      // 
      this.BrowsertoolStripMenuItem2.Name = "BrowsertoolStripMenuItem2";
      this.BrowsertoolStripMenuItem2.Size = new System.Drawing.Size(211, 26);
      this.BrowsertoolStripMenuItem2.Text = "Browser";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
      // 
      // サクラエディタToolStripMenuItem1
      // 
      this.サクラエディタToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("サクラエディタToolStripMenuItem1.Image")));
      this.サクラエディタToolStripMenuItem1.Name = "サクラエディタToolStripMenuItem1";
      this.サクラエディタToolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
      this.サクラエディタToolStripMenuItem1.Text = "サクラエディタ";
      this.サクラエディタToolStripMenuItem1.Click += new System.EventHandler(this.サクラエディタToolStripMenuItem1_Click);
      // 
      // pSPadToolStripMenuItem1
      // 
      this.pSPadToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("pSPadToolStripMenuItem1.Image")));
      this.pSPadToolStripMenuItem1.Name = "pSPadToolStripMenuItem1";
      this.pSPadToolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
      this.pSPadToolStripMenuItem1.Text = "PSPad";
      this.pSPadToolStripMenuItem1.Click += new System.EventHandler(this.pSPadToolStripMenuItem1_Click);
      // 
      // azukiControlToolStripMenuItem3
      // 
      this.azukiControlToolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("azukiControlToolStripMenuItem3.Image")));
      this.azukiControlToolStripMenuItem3.Name = "azukiControlToolStripMenuItem3";
      this.azukiControlToolStripMenuItem3.Size = new System.Drawing.Size(239, 26);
      this.azukiControlToolStripMenuItem3.Text = "AzukiControl";
      this.azukiControlToolStripMenuItem3.Click += new System.EventHandler(this.azukiControlToolStripMenuItem3_Click);
      // 
      // richTextEditorToolStripMenuItem1
      // 
      this.richTextEditorToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("richTextEditorToolStripMenuItem1.Image")));
      this.richTextEditorToolStripMenuItem1.Name = "richTextEditorToolStripMenuItem1";
      this.richTextEditorToolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
      this.richTextEditorToolStripMenuItem1.Text = "RichTextEditor";
      this.richTextEditorToolStripMenuItem1.Click += new System.EventHandler(this.richTextEditorToolStripMenuItem1_Click);
      // 
      // toolStripSeparator19
      // 
      this.toolStripSeparator19.Name = "toolStripSeparator19";
      this.toolStripSeparator19.Size = new System.Drawing.Size(236, 6);
      // 
      // エクスプローラToolStripMenuItem1
      // 
      this.エクスプローラToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("エクスプローラToolStripMenuItem1.Image")));
      this.エクスプローラToolStripMenuItem1.Name = "エクスプローラToolStripMenuItem1";
      this.エクスプローラToolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
      this.エクスプローラToolStripMenuItem1.Text = "エクスプローラ";
      this.エクスプローラToolStripMenuItem1.Click += new System.EventHandler(this.エクスプローラToolStripMenuItem1_Click);
      // 
      // コマンドプロンプトToolStripMenuItem1
      // 
      this.コマンドプロンプトToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("コマンドプロンプトToolStripMenuItem1.Image")));
      this.コマンドプロンプトToolStripMenuItem1.Name = "コマンドプロンプトToolStripMenuItem1";
      this.コマンドプロンプトToolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
      this.コマンドプロンプトToolStripMenuItem1.Text = "コマンド・プロンプト";
      this.コマンドプロンプトToolStripMenuItem1.Click += new System.EventHandler(this.コマンドプロンプトToolStripMenuItem1_Click);
      // 
      // toolStripSeparator20
      // 
      this.toolStripSeparator20.Name = "toolStripSeparator20";
      this.toolStripSeparator20.Size = new System.Drawing.Size(236, 6);
      // 
      // viewerToolStripMenuItem
      // 
      this.viewerToolStripMenuItem.Name = "viewerToolStripMenuItem";
      this.viewerToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
      this.viewerToolStripMenuItem.Text = "Viewer";
      this.viewerToolStripMenuItem.Click += new System.EventHandler(this.viewerToolStripMenuItem_Click);
      // 
      // システムプログラムToolStripMenuItem
      // 
      this.システムプログラムToolStripMenuItem.Name = "システムプログラムToolStripMenuItem";
      this.システムプログラムToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
      this.システムプログラムToolStripMenuItem.Text = "システムプログラム";
      this.システムプログラムToolStripMenuItem.Click += new System.EventHandler(this.システムプログラムToolStripMenuItem_Click);
      // 
      // コンテキストメニューtoolStripMenuItem1
      // 
      this.コンテキストメニューtoolStripMenuItem1.Name = "コンテキストメニューtoolStripMenuItem1";
      this.コンテキストメニューtoolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
      this.コンテキストメニューtoolStripMenuItem1.Text = "コンテキストメニュー";
      this.コンテキストメニューtoolStripMenuItem1.Click += new System.EventHandler(this.コンテキストメニューtoolStripMenuItem1_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(236, 6);
      // 
      // ファイルエクスプローラと同期toolStripMenuItem
      // 
      this.ファイルエクスプローラと同期toolStripMenuItem.Name = "ファイルエクスプローラと同期toolStripMenuItem";
      this.ファイルエクスプローラと同期toolStripMenuItem.Size = new System.Drawing.Size(239, 26);
      this.ファイルエクスプローラと同期toolStripMenuItem.Text = "ファイルエクスプローラと同期";
      this.ファイルエクスプローラと同期toolStripMenuItem.Click += new System.EventHandler(this.ファイルエクスプローラと同期toolStripMenuItem_Click);
      // 
      // Antツリーに追加toolStripMenuItem
      // 
      this.Antツリーに追加toolStripMenuItem.Name = "Antツリーに追加toolStripMenuItem";
      this.Antツリーに追加toolStripMenuItem.Size = new System.Drawing.Size(239, 26);
      this.Antツリーに追加toolStripMenuItem.Text = "Antツリーに追加";
      this.Antツリーに追加toolStripMenuItem.Click += new System.EventHandler(this.Antツリーに追加toolStripMenuItem_Click);
      // 
      // openAntPanel
      // 
      this.openAntPanel.Name = "openAntPanel";
      this.openAntPanel.Size = new System.Drawing.Size(239, 26);
      this.openAntPanel.Text = "AntPanelを開く";
      // 
      // DirTreeView
      // 
      this.LineColor = System.Drawing.Color.Black;
      this.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.DirTreeView_BeforeExpand);
      this.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DirTreeView_AfterSelect);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

	}

	public void InitializeDirTreeView()
	{
		this.ImageList = imageList1;//使用するImageListを指定
		this.ContextMenuStrip = contextMenuStrip1;

    TreeNode favorate = new TreeNode(@"C:\Users\和彦\Links");
    this.Nodes.Add(favorate);
    
    //MessageBox.Show("イメージ数は" + imageList1.Images.Count + "です");
		string[] drives = Environment.GetLogicalDrives();//ドライブ取得
		foreach (string drive in drives)
		{
			TreeNode tn = new TreeNode(drive);

			if (drive == "A:\\") //ドライブに対しImageListの画像を指定
			{
				tn.ImageIndex = 0;
				tn.SelectedImageIndex = 0;
			}
			else
			{
				tn.ImageIndex = 1;
				tn.SelectedImageIndex = 1;
			}
			this.Nodes.Add(tn);
			tn.Nodes.Add("dummy");
			//Cドライブを初期展開する場合このコードを追加
			//if(tn.Text == "C:\\")
			//	tn.Expand();
		}
	}

	public void InitializeDirTreeView(String folder)
	{
    //try { this.rootNode.Nodes.Clear(); } catch { }
    this.ImageList = imageList1;//使用するImageListを指定
		this.ContextMenuStrip = contextMenuStrip1;
		if(System.IO.Directory.Exists(folder))
		{
			try
			{
				TreeNode tn = new TreeNode(folder);
				tn.ImageIndex = 2;			//閉じたフォルダアイコン指定
				tn.SelectedImageIndex = 3;	//開いたフォルダアイコン指定
				this.Nodes.Add(tn);
				tn.Nodes.Add("dummy");
				//Cドライブを初期展開する場合このコードを追加
				//if(tn.Text == "C:\\")
				//tn.Expand();
        this.rootNode = tn;
      }
			catch (Exception exc)
			{
				String s = exc.Message.ToString();
				//MessageBox.Show(s);
				MessageBox.Show(OutputError(s));
				//this.MainForm.toolStripStatusLabel.Text = Lib.OutputError(s);
				//this.MainForm.toolStripStatusLabel.Text = s;
			}
		}
	}
	
	private void DirTreeView_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
	{
		TreeNode selectedNode = e.Node;		//引数のイベント発生ノードに対し追加・削除・複製
		selectedNode.Nodes.Clear();			//ダミーノードをクリアするので必要

		DirectoryInfo selectedDir = new DirectoryInfo(selectedNode.FullPath);//using System.IO
		if (selectedDir.Exists) //ディレクトリが存在すればノードに追加
		{
			DirectoryInfo[] subDirInfo = selectedDir.GetDirectories();
			foreach (DirectoryInfo di in subDirInfo)
			{
				try
				{
					TreeNode nd = selectedNode.Nodes.Add(di.Name);//サブディレクトリをノードに追加
          nd.Tag = di;
          nd.ImageIndex = 2;			//閉じたフォルダアイコン指定
					nd.SelectedImageIndex = 3;	//開いたフォルダアイコン指定

					DirectoryInfo[] subSubInfo = di.GetDirectories();
					nd.Nodes.Add("dummy");	//＋を表示するためにダミーノード（フォルダ）追加
				}
				catch (Exception exc)
				{
					String s = exc.Message.ToString();
					//MessageBox.Show(s);
					//MessageBox.Show(Lib.OutputError(s));
					//this.MainForm.toolStripStatusLabel.Text = Lib.OutputError(s);
					//this.MainForm.toolStripStatusLabel.Text = s;
				}
			}

			foreach (FileInfo file in selectedDir.GetFiles())
			{
				try
				{
					TreeNode nd = selectedNode.Nodes.Add(file.Name);//サブディレクトリをノードに追加
					nd.ImageIndex = GetIconImageIndex(file.FullName);//-1;
					nd.SelectedImageIndex = GetIconImageIndex(file.FullName);//-1;
          nd.Tag = file;
        }
				catch (Exception exc)
				{
					String s = exc.Message.ToString();
					MessageBox.Show(OutputError(s));
					//this.ShowExceptionUI(s);
					//MessageBox.Show(s);
				}
			}
		}
		else
		{
			MessageBox.Show(selectedDir.Root.Name + "にディスクを挿入してください。",
				this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void DirTreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
	{
		DirectoryInfo di = new DirectoryInfo(e.Node.FullPath);

		//string viewer_url;
		if (e.Node.Text != "A:\\")//＋マークによるノード展開でデフォルトのA:\アクセス回避
		{
			if (di.Exists)
			{
				try
				{
					foreach (FileInfo file in di.GetFiles())
					{
						// 追加
						TreeNode nd = e.Node.Nodes.Add(file.Name);//サブディレクトリをノードに追加
						nd.ImageIndex = GetIconImageIndex(file.FullName); //-1;
						nd.SelectedImageIndex = GetIconImageIndex(file.FullName);//-1;
					}
				}
				catch (Exception exc)
				{
					String s = exc.Message.ToString();
					//MessageBox.Show(Lib.OutputError(s));
					//this.ShowExceptionUI(s);
					//MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}
    this.filepath = e.Node.FullPath.Replace("\\\\", "\\");
	}

	public int GetIconImageIndex(string path)
	{
		string extension = Path.GetExtension(path);

		if (_systemIcons.ContainsKey(extension) == false)
		{
			Icon icon = ShellIcon.GetSmallIcon(path);
			imageList1.Images.Add(icon);
			_systemIcons.Add(extension, imageList1.Images.Count - 1);
		}
		return (int)_systemIcons[Path.GetExtension(path)];
	}

	private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
	{
		//ContextMenuStripを表示しているコントロールを表示する
		//ContextMenuStrip menu = (ContextMenuStrip)sender;
		//MessageBox.Show(menu.SourceControl.Name);
	}

	// http://www.atmarkit.co.jp/bbs/phpBB/viewtopic.php?forum=7&topic=24220
	private void treeView1_MouseDown(object sender, MouseEventArgs e)
	{
		this.SelectedNode = this.GetNodeAt(e.X, e.Y);
	}

	private void treeView1_MouseUp(object sender, MouseEventArgs e)
	{
	}

	
	private void サクラエディタToolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void pSPadToolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void azukiControlToolStripMenuItem3_Click(object sender, EventArgs e)
	{
	}

	private void richTextEditorToolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void エクスプローラToolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void コマンドプロンプトToolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void viewerToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void システムプログラムToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}


	private void 開くtoolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void ブラウザで開くtoolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void コンテキストメニューtoolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}


  private void ファイルエクスプローラと同期toolStripMenuItem_Click(object sender, EventArgs e)
  {

  }

  private void Antツリーに追加toolStripMenuItem_Click(object sender, EventArgs e)
  {

  }

  private String OutputError(string Message)
	{
		StackFrame CallStack = new StackFrame(1, true);

		String SourceFile = CallStack.GetFileName();
		int SourceLine = CallStack.GetFileLineNumber();
		String errMsg = "Error: " + Message + " - File: " + SourceFile + " Line: " + SourceLine.ToString();
		return errMsg;
	}
 
}

/// <summary>
/// Summary description for ShellIcon.
/// </summary>
/// <summary>
/// Summary description for ShellIcon.  Get a small or large Icon with an easy C# function call
/// that returns a 32x32 or 16x16 System.Drawing.Icon depending on which function you call
/// either GetSmallIcon(string fileName) or GetLargeIcon(string fileName)
/// </summary>
public class ShellIcon
	{
		[StructLayout(LayoutKind.Sequential)]
			public struct SHFILEINFO 
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

		class Win32
		{
			public const uint SHGFI_ICON = 0x100;
			public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
			public const uint SHGFI_SMALLICON = 0x1; // 'Small icon
			[DllImport("shell32.dll")]
			public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
		}

		public ShellIcon()
		{
			//
			// Add constructor logic here
			//
		}

		public static Icon GetSmallIcon(string fileName)
		{
			IntPtr hImgSmall; //the handle to the system image list
			SHFILEINFO shinfo = new SHFILEINFO();
			//Use this to get the small Icon
			hImgSmall = Win32.SHGetFileInfo(fileName, 0, ref shinfo,(uint)Marshal.SizeOf(shinfo),Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
			//The icon is returned in the hIcon member of the shinfo struct
			return System.Drawing.Icon.FromHandle(shinfo.hIcon);                
		}

		public static Icon GetLargeIcon(string fileName)
		{
			IntPtr hImgLarge; //the handle to the system image list
			SHFILEINFO shinfo = new SHFILEINFO();
			//Use this to get the large Icon
			hImgLarge = Win32.SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
			//The icon is returned in the hIcon member of the shinfo struct
			return System.Drawing.Icon.FromHandle(shinfo.hIcon);                
		}
	}
