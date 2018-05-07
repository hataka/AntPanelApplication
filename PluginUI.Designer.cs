using System.Windows.Forms;
using System;
using System.Collections.Generic;

namespace AntPlugin
{
	partial class PluginUI
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private String[] dropFiles = null;
		private bool preventExpand = false;
		private DateTime lastMouseDown = DateTime.Now;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginUI));
      this.treeView = new System.Windows.Forms.TreeView();
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.addButton = new System.Windows.Forms.ToolStripButton();
      this.refreshButton = new System.Windows.Forms.ToolStripButton();
      this.runButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.最近開いたカスタムドキュメントToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.最近開いたカスタムドキュメントをクリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ファイル状態の保存と復元ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.前回セッションの復元ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.復元ポイントの保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.全復元ポイントのクリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.出力ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.documentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.traceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.オプションToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.内部ターゲット表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.プロパティ表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.子ノード表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.アイコン表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ツールボタンToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.新規作成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.名前を付けて保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.imageListButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.CSOutlinePanelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.サクラエディタToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pSPadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.richTextBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.scintillaCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.azukiEditorZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.エクスプローラToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.コマンドプロンプトToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.ファイル名を指定して実行OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.リンクを開くLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.antコマンドToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ant17ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.imageListStripButton = new System.Windows.Forms.ToolStripButton();
      this.csOutlineButton1 = new System.Windows.Forms.ToolStripButton();
      this.syncronizeButton = new System.Windows.Forms.ToolStripButton();
      this.syncronizeDodument = new System.Windows.Forms.ToolStripButton();
      this.表示ToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.homeStripButton = new System.Windows.Forms.ToolStripButton();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.toolStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // treeView
      // 
      this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView.HideSelection = false;
      this.treeView.ImageIndex = 0;
      this.treeView.ImageList = this.imageList;
      this.treeView.Location = new System.Drawing.Point(0, 0);
      this.treeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.treeView.Name = "treeView";
      this.treeView.SelectedImageIndex = 0;
      this.treeView.ShowNodeToolTips = true;
      this.treeView.Size = new System.Drawing.Size(842, 349);
      this.treeView.TabIndex = 1;
      this.treeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeCollapse);
      this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
      this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
      this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
      this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
      // 
      // imageList
      // 
      this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
      this.imageList.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList.Images.SetKeyName(0, "ant_buildfile.png");
      this.imageList.Images.SetKeyName(1, "defaulttarget_obj.png");
      this.imageList.Images.SetKeyName(2, "targetinternal_obj.png");
      this.imageList.Images.SetKeyName(3, "targetpublic_obj.png");
      this.imageList.Images.SetKeyName(4, "CheckAS.png");
      this.imageList.Images.SetKeyName(5, "Class.png");
      this.imageList.Images.SetKeyName(6, "Const.png");
      this.imageList.Images.SetKeyName(7, "ConstPrivate.png");
      this.imageList.Images.SetKeyName(8, "ConstProtected.png");
      this.imageList.Images.SetKeyName(9, "Declaration.png");
      this.imageList.Images.SetKeyName(10, "FilePlain.png");
      this.imageList.Images.SetKeyName(11, "FolderClosed.png");
      this.imageList.Images.SetKeyName(12, "FolderOpen.png");
      this.imageList.Images.SetKeyName(13, "Interface.png");
      this.imageList.Images.SetKeyName(14, "Intrinsic.png");
      this.imageList.Images.SetKeyName(15, "Method.png");
      this.imageList.Images.SetKeyName(16, "MethodPrivate.png");
      this.imageList.Images.SetKeyName(17, "MethodProtected.png");
      this.imageList.Images.SetKeyName(18, "MethodStatic.png");
      this.imageList.Images.SetKeyName(19, "MethodStaticPrivate.png");
      this.imageList.Images.SetKeyName(20, "MethodStaticProtected.png");
      this.imageList.Images.SetKeyName(21, "Package.png");
      this.imageList.Images.SetKeyName(22, "Property.png");
      this.imageList.Images.SetKeyName(23, "PropertyPrivate.png");
      this.imageList.Images.SetKeyName(24, "PropertyProtected.png");
      this.imageList.Images.SetKeyName(25, "PropertyStatic.png");
      this.imageList.Images.SetKeyName(26, "PropertyStaticPrivate.png");
      this.imageList.Images.SetKeyName(27, "PropertyStaticProtected.png");
      this.imageList.Images.SetKeyName(28, "QuickBuild.png");
      this.imageList.Images.SetKeyName(29, "Template.png");
      this.imageList.Images.SetKeyName(30, "Variable.png");
      this.imageList.Images.SetKeyName(31, "VariablePrivate.png");
      this.imageList.Images.SetKeyName(32, "VariableProtected.png");
      this.imageList.Images.SetKeyName(33, "VariableStatic.png");
      this.imageList.Images.SetKeyName(34, "VariableStaticPrivate.png");
      this.imageList.Images.SetKeyName(35, "VariableStaticProtected.png");
      this.imageList.Images.SetKeyName(36, "Pspad15.png");
      this.imageList.Images.SetKeyName(37, "FlashDevelopIcon.ico");
      this.imageList.Images.SetKeyName(38, "53.png");
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(3, 3);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.treeView);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
      this.splitContainer1.Size = new System.Drawing.Size(842, 565);
      this.splitContainer1.SplitterDistance = 349;
      this.splitContainer1.TabIndex = 3;
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(842, 212);
      this.propertyGrid1.TabIndex = 1;
      // 
      // tabControl1
      // 
      this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Controls.Add(this.tabPage5);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(856, 600);
      this.tabControl1.TabIndex = 4;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.toolStrip);
      this.tabPage1.Controls.Add(this.splitContainer1);
      this.tabPage1.Location = new System.Drawing.Point(4, 4);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(848, 571);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Ant";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // toolStrip
      // 
      this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.refreshButton,
            this.runButton,
            this.toolStripDropDownButton1,
            this.imageListStripButton,
            this.csOutlineButton1,
            this.syncronizeButton,
            this.syncronizeDodument,
            this.表示ToolStripButton,
            this.homeStripButton});
      this.toolStrip.Location = new System.Drawing.Point(3, 3);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Size = new System.Drawing.Size(842, 27);
      this.toolStrip.TabIndex = 4;
      this.toolStrip.Text = "toolStrip1";
      // 
      // addButton
      // 
      this.addButton.Image = ((System.Drawing.Image)(resources.GetObject("addButton.Image")));
      this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.addButton.Name = "addButton";
      this.addButton.Size = new System.Drawing.Size(61, 24);
      this.addButton.Text = "Add";
      this.addButton.ToolTipText = "Add build file";
      this.addButton.Click += new System.EventHandler(this.addButton_Click);
      // 
      // refreshButton
      // 
      this.refreshButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
      this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.refreshButton.Name = "refreshButton";
      this.refreshButton.Size = new System.Drawing.Size(24, 24);
      this.refreshButton.Text = "toolStripButton2";
      this.refreshButton.ToolTipText = "Refresh";
      this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
      // 
      // runButton
      // 
      this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
      this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.runButton.Name = "runButton";
      this.runButton.Size = new System.Drawing.Size(61, 24);
      this.runButton.Text = "Run";
      this.runButton.Click += new System.EventHandler(this.runButton_Click);
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最近開いたカスタムドキュメントToolStripMenuItem,
            this.ファイル状態の保存と復元ToolStripMenuItem,
            this.出力ToolStripMenuItem,
            this.オプションToolStripMenuItem,
            this.開くOToolStripMenuItem,
            this.testToolStripMenuItem,
            this.antコマンドToolStripMenuItem});
      this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(34, 24);
      this.toolStripDropDownButton1.Text = "ツール";
      this.toolStripDropDownButton1.ToolTipText = "ツール";
      // 
      // 最近開いたカスタムドキュメントToolStripMenuItem
      // 
      this.最近開いたカスタムドキュメントToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最近開いたカスタムドキュメントをクリアToolStripMenuItem});
      this.最近開いたカスタムドキュメントToolStripMenuItem.Name = "最近開いたカスタムドキュメントToolStripMenuItem";
      this.最近開いたカスタムドキュメントToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
      this.最近開いたカスタムドキュメントToolStripMenuItem.Text = "最近開いたツリーファイル";
      // 
      // 最近開いたカスタムドキュメントをクリアToolStripMenuItem
      // 
      this.最近開いたカスタムドキュメントをクリアToolStripMenuItem.Name = "最近開いたカスタムドキュメントをクリアToolStripMenuItem";
      this.最近開いたカスタムドキュメントをクリアToolStripMenuItem.Size = new System.Drawing.Size(268, 26);
      this.最近開いたカスタムドキュメントをクリアToolStripMenuItem.Text = "最近開いたツリーファイルをクリア";
      this.最近開いたカスタムドキュメントをクリアToolStripMenuItem.Click += new System.EventHandler(this.最近開いたカスタムドキュメントをクリアToolStripMenuItem_Click_1);
      // 
      // ファイル状態の保存と復元ToolStripMenuItem
      // 
      this.ファイル状態の保存と復元ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.前回セッションの復元ToolStripMenuItem,
            this.復元ポイントの保存ToolStripMenuItem,
            this.全復元ポイントのクリアToolStripMenuItem});
      this.ファイル状態の保存と復元ToolStripMenuItem.Name = "ファイル状態の保存と復元ToolStripMenuItem";
      this.ファイル状態の保存と復元ToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
      this.ファイル状態の保存と復元ToolStripMenuItem.Text = "ファイル状態の保存と復元";
      // 
      // 前回セッションの復元ToolStripMenuItem
      // 
      this.前回セッションの復元ToolStripMenuItem.Name = "前回セッションの復元ToolStripMenuItem";
      this.前回セッションの復元ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
      this.前回セッションの復元ToolStripMenuItem.Text = "前回セッションの復元";
      this.前回セッションの復元ToolStripMenuItem.Click += new System.EventHandler(this.前回セッションの復元ToolStripMenuItem_Click);
      // 
      // 復元ポイントの保存ToolStripMenuItem
      // 
      this.復元ポイントの保存ToolStripMenuItem.Name = "復元ポイントの保存ToolStripMenuItem";
      this.復元ポイントの保存ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
      this.復元ポイントの保存ToolStripMenuItem.Text = "復元ポイントの作成";
      this.復元ポイントの保存ToolStripMenuItem.Click += new System.EventHandler(this.復元ポイントの保存ToolStripMenuItem_Click);
      // 
      // 全復元ポイントのクリアToolStripMenuItem
      // 
      this.全復元ポイントのクリアToolStripMenuItem.Name = "全復元ポイントのクリアToolStripMenuItem";
      this.全復元ポイントのクリアToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
      this.全復元ポイントのクリアToolStripMenuItem.Text = "全復元ポイントのクリア";
      this.全復元ポイントのクリアToolStripMenuItem.Click += new System.EventHandler(this.全復元ポイントのクリアToolStripMenuItem_Click);
      // 
      // 出力ToolStripMenuItem
      // 
      this.出力ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consoleToolStripMenuItem,
            this.documentToolStripMenuItem,
            this.traceToolStripMenuItem});
      this.出力ToolStripMenuItem.Name = "出力ToolStripMenuItem";
      this.出力ToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
      this.出力ToolStripMenuItem.Text = "出力";
      // 
      // consoleToolStripMenuItem
      // 
      this.consoleToolStripMenuItem.Checked = true;
      this.consoleToolStripMenuItem.CheckOnClick = true;
      this.consoleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
      this.consoleToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
      this.consoleToolStripMenuItem.Text = "Console";
      // 
      // documentToolStripMenuItem
      // 
      this.documentToolStripMenuItem.CheckOnClick = true;
      this.documentToolStripMenuItem.Name = "documentToolStripMenuItem";
      this.documentToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
      this.documentToolStripMenuItem.Text = "Document";
      // 
      // traceToolStripMenuItem
      // 
      this.traceToolStripMenuItem.CheckOnClick = true;
      this.traceToolStripMenuItem.Name = "traceToolStripMenuItem";
      this.traceToolStripMenuItem.Size = new System.Drawing.Size(159, 26);
      this.traceToolStripMenuItem.Text = "Trace";
      // 
      // オプションToolStripMenuItem
      // 
      this.オプションToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.内部ターゲット表示ToolStripMenuItem,
            this.プロパティ表示ToolStripMenuItem,
            this.子ノード表示ToolStripMenuItem,
            this.アイコン表示ToolStripMenuItem,
            this.ツールボタンToolStripMenuItem,
            this.toolStripSeparator1,
            this.CSOutlinePanelMenuItem});
      this.オプションToolStripMenuItem.Name = "オプションToolStripMenuItem";
      this.オプションToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
      this.オプションToolStripMenuItem.Text = "オプション";
      // 
      // 内部ターゲット表示ToolStripMenuItem
      // 
      this.内部ターゲット表示ToolStripMenuItem.Checked = true;
      this.内部ターゲット表示ToolStripMenuItem.CheckOnClick = true;
      this.内部ターゲット表示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.内部ターゲット表示ToolStripMenuItem.Name = "内部ターゲット表示ToolStripMenuItem";
      this.内部ターゲット表示ToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
      this.内部ターゲット表示ToolStripMenuItem.Text = "内部ターゲット表示";
      this.内部ターゲット表示ToolStripMenuItem.Click += new System.EventHandler(this.内部ターゲット表示ToolStripMenuItem_Click);
      // 
      // プロパティ表示ToolStripMenuItem
      // 
      this.プロパティ表示ToolStripMenuItem.CheckOnClick = true;
      this.プロパティ表示ToolStripMenuItem.Name = "プロパティ表示ToolStripMenuItem";
      this.プロパティ表示ToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
      this.プロパティ表示ToolStripMenuItem.Text = "プロパティ表示";
      this.プロパティ表示ToolStripMenuItem.Click += new System.EventHandler(this.プロパティ表示ToolStripMenuItem_Click);
      // 
      // 子ノード表示ToolStripMenuItem
      // 
      this.子ノード表示ToolStripMenuItem.Checked = true;
      this.子ノード表示ToolStripMenuItem.CheckOnClick = true;
      this.子ノード表示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.子ノード表示ToolStripMenuItem.Name = "子ノード表示ToolStripMenuItem";
      this.子ノード表示ToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
      this.子ノード表示ToolStripMenuItem.Text = "子ノード表示";
      this.子ノード表示ToolStripMenuItem.Click += new System.EventHandler(this.子ノード表示ToolStripMenuItem_Click);
      // 
      // アイコン表示ToolStripMenuItem
      // 
      this.アイコン表示ToolStripMenuItem.Checked = true;
      this.アイコン表示ToolStripMenuItem.CheckOnClick = true;
      this.アイコン表示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.アイコン表示ToolStripMenuItem.Name = "アイコン表示ToolStripMenuItem";
      this.アイコン表示ToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
      this.アイコン表示ToolStripMenuItem.Text = "アイコン表示";
      // 
      // ツールボタンToolStripMenuItem
      // 
      this.ツールボタンToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成ToolStripMenuItem,
            this.名前を付けて保存ToolStripMenuItem,
            this.imageListButtonToolStripMenuItem});
      this.ツールボタンToolStripMenuItem.Name = "ツールボタンToolStripMenuItem";
      this.ツールボタンToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
      this.ツールボタンToolStripMenuItem.Text = "ツールボタン";
      // 
      // 新規作成ToolStripMenuItem
      // 
      this.新規作成ToolStripMenuItem.CheckOnClick = true;
      this.新規作成ToolStripMenuItem.Name = "新規作成ToolStripMenuItem";
      this.新規作成ToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
      this.新規作成ToolStripMenuItem.Text = "新規作成";
      // 
      // 名前を付けて保存ToolStripMenuItem
      // 
      this.名前を付けて保存ToolStripMenuItem.Name = "名前を付けて保存ToolStripMenuItem";
      this.名前を付けて保存ToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
      this.名前を付けて保存ToolStripMenuItem.Text = "名前を付けて保存";
      // 
      // imageListButtonToolStripMenuItem
      // 
      this.imageListButtonToolStripMenuItem.CheckOnClick = true;
      this.imageListButtonToolStripMenuItem.Name = "imageListButtonToolStripMenuItem";
      this.imageListButtonToolStripMenuItem.Size = new System.Drawing.Size(205, 26);
      this.imageListButtonToolStripMenuItem.Text = "ImageListButton";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
      // 
      // CSOutlinePanelMenuItem
      // 
      this.CSOutlinePanelMenuItem.Checked = true;
      this.CSOutlinePanelMenuItem.CheckOnClick = true;
      this.CSOutlinePanelMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.CSOutlinePanelMenuItem.Name = "CSOutlinePanelMenuItem";
      this.CSOutlinePanelMenuItem.Size = new System.Drawing.Size(198, 26);
      this.CSOutlinePanelMenuItem.Text = "CSOutlinePanel";
      this.CSOutlinePanelMenuItem.Click += new System.EventHandler(this.CSOutlinePanelMenuItem_Click);
      // 
      // 開くOToolStripMenuItem
      // 
      this.開くOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.サクラエディタToolStripMenuItem,
            this.pSPadToolStripMenuItem,
            this.toolStripSeparator7,
            this.richTextBoxToolStripMenuItem,
            this.scintillaCToolStripMenuItem,
            this.azukiEditorZToolStripMenuItem,
            this.toolStripSeparator8,
            this.エクスプローラToolStripMenuItem,
            this.コマンドプロンプトToolStripMenuItem,
            this.toolStripSeparator9,
            this.ファイル名を指定して実行OToolStripMenuItem,
            this.リンクを開くLToolStripMenuItem});
      this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
      this.開くOToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
      this.開くOToolStripMenuItem.Text = "開く(&O)";
      // 
      // サクラエディタToolStripMenuItem
      // 
      this.サクラエディタToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("サクラエディタToolStripMenuItem.Image")));
      this.サクラエディタToolStripMenuItem.Name = "サクラエディタToolStripMenuItem";
      this.サクラエディタToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.サクラエディタToolStripMenuItem.Text = "サクラエディタ";
      this.サクラエディタToolStripMenuItem.Click += new System.EventHandler(this.サクラエディタToolStripMenuItem_Click);
      // 
      // pSPadToolStripMenuItem
      // 
      this.pSPadToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pSPadToolStripMenuItem.Image")));
      this.pSPadToolStripMenuItem.Name = "pSPadToolStripMenuItem";
      this.pSPadToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.pSPadToolStripMenuItem.Text = "PSPad";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(257, 6);
      // 
      // richTextBoxToolStripMenuItem
      // 
      this.richTextBoxToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("richTextBoxToolStripMenuItem.Image")));
      this.richTextBoxToolStripMenuItem.Name = "richTextBoxToolStripMenuItem";
      this.richTextBoxToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.richTextBoxToolStripMenuItem.Text = "RichTextEditor(&R)";
      // 
      // scintillaCToolStripMenuItem
      // 
      this.scintillaCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("scintillaCToolStripMenuItem.Image")));
      this.scintillaCToolStripMenuItem.Name = "scintillaCToolStripMenuItem";
      this.scintillaCToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.scintillaCToolStripMenuItem.Text = "Scintilla(&A)";
      // 
      // azukiEditorZToolStripMenuItem
      // 
      this.azukiEditorZToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("azukiEditorZToolStripMenuItem.Image")));
      this.azukiEditorZToolStripMenuItem.Name = "azukiEditorZToolStripMenuItem";
      this.azukiEditorZToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.azukiEditorZToolStripMenuItem.Text = "Azuki Editor(&Z)";
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(257, 6);
      // 
      // エクスプローラToolStripMenuItem
      // 
      this.エクスプローラToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("エクスプローラToolStripMenuItem.Image")));
      this.エクスプローラToolStripMenuItem.Name = "エクスプローラToolStripMenuItem";
      this.エクスプローラToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.エクスプローラToolStripMenuItem.Text = "エクスプローラ";
      this.エクスプローラToolStripMenuItem.Click += new System.EventHandler(this.エクスプローラToolStripMenuItem_Click);
      // 
      // コマンドプロンプトToolStripMenuItem
      // 
      this.コマンドプロンプトToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("コマンドプロンプトToolStripMenuItem.Image")));
      this.コマンドプロンプトToolStripMenuItem.Name = "コマンドプロンプトToolStripMenuItem";
      this.コマンドプロンプトToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.コマンドプロンプトToolStripMenuItem.Text = "コマンドプロンプト";
      this.コマンドプロンプトToolStripMenuItem.Click += new System.EventHandler(this.コマンドプロンプトToolStripMenuItem_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(257, 6);
      // 
      // ファイル名を指定して実行OToolStripMenuItem
      // 
      this.ファイル名を指定して実行OToolStripMenuItem.Name = "ファイル名を指定して実行OToolStripMenuItem";
      this.ファイル名を指定して実行OToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.ファイル名を指定して実行OToolStripMenuItem.Text = "ファイル名を指定して実行(&O)";
      // 
      // リンクを開くLToolStripMenuItem
      // 
      this.リンクを開くLToolStripMenuItem.Name = "リンクを開くLToolStripMenuItem";
      this.リンクを開くLToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.リンクを開くLToolStripMenuItem.Text = "リンクを開く(&L)";
      // 
      // testToolStripMenuItem
      // 
      this.testToolStripMenuItem.Name = "testToolStripMenuItem";
      this.testToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
      this.testToolStripMenuItem.Text = "アウトライン表示";
      this.testToolStripMenuItem.ToolTipText = "アウトライン表示";
      this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
      // 
      // antコマンドToolStripMenuItem
      // 
      this.antコマンドToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ant17ToolStripMenuItem});
      this.antコマンドToolStripMenuItem.Name = "antコマンドToolStripMenuItem";
      this.antコマンドToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
      this.antコマンドToolStripMenuItem.Text = "Ant コマンド";
      // 
      // ant17ToolStripMenuItem
      // 
      this.ant17ToolStripMenuItem.CheckOnClick = true;
      this.ant17ToolStripMenuItem.Name = "ant17ToolStripMenuItem";
      this.ant17ToolStripMenuItem.Size = new System.Drawing.Size(127, 26);
      this.ant17ToolStripMenuItem.Text = "Ant17";
      // 
      // imageListStripButton
      // 
      this.imageListStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.imageListStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.imageListStripButton.Image = ((System.Drawing.Image)(resources.GetObject("imageListStripButton.Image")));
      this.imageListStripButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.imageListStripButton.Name = "imageListStripButton";
      this.imageListStripButton.Size = new System.Drawing.Size(24, 24);
      this.imageListStripButton.Text = "toolStripButton2";
      this.imageListStripButton.Visible = false;
      this.imageListStripButton.Click += new System.EventHandler(this.imageListStripButton_Click);
      // 
      // csOutlineButton1
      // 
      this.csOutlineButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.csOutlineButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.csOutlineButton1.Image = ((System.Drawing.Image)(resources.GetObject("csOutlineButton1.Image")));
      this.csOutlineButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.csOutlineButton1.Name = "csOutlineButton1";
      this.csOutlineButton1.Size = new System.Drawing.Size(24, 24);
      this.csOutlineButton1.Text = "toolStripButton1";
      this.csOutlineButton1.ToolTipText = "アウトラインツリーを表示します";
      this.csOutlineButton1.Click += new System.EventHandler(this.csOutlineButton1_Click);
      // 
      // syncronizeButton
      // 
      this.syncronizeButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.syncronizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.syncronizeButton.Image = ((System.Drawing.Image)(resources.GetObject("syncronizeButton.Image")));
      this.syncronizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.syncronizeButton.Name = "syncronizeButton";
      this.syncronizeButton.Size = new System.Drawing.Size(24, 24);
      this.syncronizeButton.Text = "toolStripButton1";
      this.syncronizeButton.ToolTipText = "ファイルエクスプローラとシンクロ";
      this.syncronizeButton.Click += new System.EventHandler(this.syncronizeButton_Click);
      // 
      // syncronizeDodument
      // 
      this.syncronizeDodument.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.syncronizeDodument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.syncronizeDodument.Image = ((System.Drawing.Image)(resources.GetObject("syncronizeDodument.Image")));
      this.syncronizeDodument.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.syncronizeDodument.Name = "syncronizeDodument";
      this.syncronizeDodument.Size = new System.Drawing.Size(24, 24);
      this.syncronizeDodument.Text = "toolStripButton1";
      this.syncronizeDodument.ToolTipText = "現在のドキュメントをノードに追加";
      this.syncronizeDodument.Click += new System.EventHandler(this.syncronizeDodument_Click);
      // 
      // 表示ToolStripButton
      // 
      this.表示ToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.表示ToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.表示ToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("表示ToolStripButton.Image")));
      this.表示ToolStripButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.表示ToolStripButton.Name = "表示ToolStripButton";
      this.表示ToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.表示ToolStripButton.Text = "Property表示切替";
      this.表示ToolStripButton.Click += new System.EventHandler(this.表示ToolStripButton_Click);
      // 
      // homeStripButton
      // 
      this.homeStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.homeStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.homeStripButton.Image = ((System.Drawing.Image)(resources.GetObject("homeStripButton.Image")));
      this.homeStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.homeStripButton.Name = "homeStripButton";
      this.homeStripButton.Size = new System.Drawing.Size(24, 24);
      this.homeStripButton.Text = "toolStripButton1";
      this.homeStripButton.Click += new System.EventHandler(this.homeStripButton_Click);
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 4);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(848, 571);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Dir";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // tabPage3
      // 
      this.tabPage3.Location = new System.Drawing.Point(4, 4);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new System.Drawing.Size(848, 571);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "FTP";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Location = new System.Drawing.Point(4, 4);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Size = new System.Drawing.Size(848, 571);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Link";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // tabPage5
      // 
      this.tabPage5.Location = new System.Drawing.Point(4, 4);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage5.Size = new System.Drawing.Size(848, 571);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "tabPage5";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // PluginUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tabControl1);
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.Name = "PluginUI";
      this.Size = new System.Drawing.Size(856, 600);
      this.Load += new System.EventHandler(this.PluginUI_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion
		public System.Windows.Forms.TreeView treeView;
		public System.Windows.Forms.ImageList imageList;

		private void treeView_MouseDown(object sender, MouseEventArgs e)
		{
			int delta = (int)DateTime.Now.Subtract(lastMouseDown).TotalMilliseconds;
			preventExpand = (delta < SystemInformation.DoubleClickTime);
			lastMouseDown = DateTime.Now;
		}

		private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			e.Cancel = preventExpand;
			preventExpand = false;
		}

		private void treeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
		{
			e.Cancel = preventExpand;
			preventExpand = false;
		}

		internal void StartDragHandling()
		{
			this.treeView.AllowDrop = true;
			this.treeView.DragEnter += new DragEventHandler(treeView_DragEnter);
			this.treeView.DragDrop += new DragEventHandler(treeView_DragDrop);
			this.treeView.DragOver += new DragEventHandler(treeView_DragOver);
		}

		void treeView_DragEnter(object sender, DragEventArgs e)
		{
			String[] s = (String[])e.Data.GetData(DataFormats.FileDrop);
			List<String> xmls = new List<String>();
			for (Int32 i = 0; i < s.Length; i++)
			{
				if (s[i].EndsWith(".xml", true, null))
				{
					xmls.Add(s[i]);
				}
			}
			if (xmls.Count > 0)
			{
				e.Effect = DragDropEffects.Copy;
				this.dropFiles = xmls.ToArray();
			}
			else this.dropFiles = null;
		}

		void treeView_DragOver(object sender, DragEventArgs e)
		{
			if (this.dropFiles != null)
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		void treeView_DragDrop(object sender, DragEventArgs e)
		{
			if (this.dropFiles != null)
			{
				this.pluginMain.AddBuildFiles(this.dropFiles);
			}
		}
		private SplitContainer splitContainer1;
		public PropertyGrid propertyGrid1;
    private TabControl tabControl1;
    private TabPage tabPage1;
    public ToolStrip toolStrip;
    public ToolStripButton addButton;
    public ToolStripButton refreshButton;
    public ToolStripButton runButton;
    private ToolStripDropDownButton toolStripDropDownButton1;
    private ToolStripMenuItem 最近開いたカスタムドキュメントToolStripMenuItem;
    private ToolStripMenuItem 最近開いたカスタムドキュメントをクリアToolStripMenuItem;
    private ToolStripMenuItem ファイル状態の保存と復元ToolStripMenuItem;
    private ToolStripMenuItem 前回セッションの復元ToolStripMenuItem;
    private ToolStripMenuItem 復元ポイントの保存ToolStripMenuItem;
    private ToolStripMenuItem 全復元ポイントのクリアToolStripMenuItem;
    private ToolStripMenuItem 出力ToolStripMenuItem;
    public ToolStripMenuItem consoleToolStripMenuItem;
    public ToolStripMenuItem documentToolStripMenuItem;
    public ToolStripMenuItem traceToolStripMenuItem;
    private ToolStripMenuItem オプションToolStripMenuItem;
    private ToolStripMenuItem 内部ターゲット表示ToolStripMenuItem;
    private ToolStripMenuItem プロパティ表示ToolStripMenuItem;
    private ToolStripMenuItem 子ノード表示ToolStripMenuItem;
    private ToolStripMenuItem アイコン表示ToolStripMenuItem;
    private ToolStripMenuItem ツールボタンToolStripMenuItem;
    private ToolStripMenuItem 新規作成ToolStripMenuItem;
    private ToolStripMenuItem 名前を付けて保存ToolStripMenuItem;
    private ToolStripMenuItem imageListButtonToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem CSOutlinePanelMenuItem;
    private ToolStripMenuItem 開くOToolStripMenuItem;
    private ToolStripMenuItem サクラエディタToolStripMenuItem;
    private ToolStripMenuItem pSPadToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripMenuItem richTextBoxToolStripMenuItem;
    private ToolStripMenuItem scintillaCToolStripMenuItem;
    public ToolStripMenuItem azukiEditorZToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripMenuItem エクスプローラToolStripMenuItem;
    private ToolStripMenuItem コマンドプロンプトToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripMenuItem ファイル名を指定して実行OToolStripMenuItem;
    private ToolStripMenuItem リンクを開くLToolStripMenuItem;
    private ToolStripMenuItem testToolStripMenuItem;
    private ToolStripMenuItem antコマンドToolStripMenuItem;
    public ToolStripMenuItem ant17ToolStripMenuItem;
    private ToolStripButton imageListStripButton;
    private ToolStripButton csOutlineButton1;
    private ToolStripButton syncronizeButton;
    private ToolStripButton syncronizeDodument;
    private ToolStripButton 表示ToolStripButton;
    private ToolStripButton homeStripButton;
    private TabPage tabPage2;
    private TabPage tabPage3;
    private TabPage tabPage4;
    private TabPage tabPage5;
  }
}
