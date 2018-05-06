using AntPlugin.CommonLibrary;
using AntPlugin.XmlTreeMenu;
//using AntPlugin.XmlTreeMenu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace AntPanelApplication
{
  partial class AntPanel
  {
    private String[] dropFiles = null;
    private bool preventExpand = false;
    private DateTime lastMouseDown = DateTime.Now;


    /// <summary> 
    /// 必要なデザイナー変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// 使用中のリソースをすべてクリーンアップします。
    /// </summary>
    /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        this.SaveSettings();
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region コンポーネント デザイナーで生成されたコード

    /// <summary> 
    /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
    /// コード エディターで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AntPanel));
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.addButton = new System.Windows.Forms.ToolStripButton();
      this.refreshButton = new System.Windows.Forms.ToolStripButton();
      this.runButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
      this.内部ターゲット表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.プロパティ表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.子ノード表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.アイコン表示MenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
      this.カスタマイズToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItem21 = new System.Windows.Forms.ToolStripMenuItem();
      this.サクラエディタToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pSPadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.richTextBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.scintillaCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.azukiEditorZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.エクスプローラToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.コマンドプロンプトToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItem29 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem30 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem31 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem32 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem33 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.試験ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.imageListStripButton = new System.Windows.Forms.ToolStripButton();
      this.csOutlineButton1 = new System.Windows.Forms.ToolStripButton();
      this.syncronizeDodument = new System.Windows.Forms.ToolStripButton();
      this.syncronizeButton = new System.Windows.Forms.ToolStripButton();
      this.表示ToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.homeStripButton = new System.Windows.Forms.ToolStripButton();
      this.removeButton = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.treeView = new System.Windows.Forms.TreeView();
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.propertyGrid3 = new System.Windows.Forms.PropertyGrid();
      this.gitButton = new System.Windows.Forms.ToolStripButton();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.toolStrip.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabPage5.SuspendLayout();
      this.SuspendLayout();
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
      this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(1193, 611);
      this.tabControl1.TabIndex = 6;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.toolStrip);
      this.tabPage1.Controls.Add(this.splitContainer1);
      this.tabPage1.Location = new System.Drawing.Point(4, 4);
      this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage1.Size = new System.Drawing.Size(1185, 573);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Ant";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // toolStrip
      // 
      this.toolStrip.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.refreshButton,
            this.runButton,
            this.toolStripDropDownButton2,
            this.imageListStripButton,
            this.csOutlineButton1,
            this.gitButton,
            this.syncronizeDodument,
            this.syncronizeButton,
            this.表示ToolStripButton,
            this.homeStripButton,
            this.removeButton});
      this.toolStrip.Location = new System.Drawing.Point(4, 5);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
      this.toolStrip.Size = new System.Drawing.Size(1177, 32);
      this.toolStrip.TabIndex = 4;
      this.toolStrip.Text = "toolStrip1";
      // 
      // addButton
      // 
      this.addButton.Image = ((System.Drawing.Image)(resources.GetObject("addButton.Image")));
      this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.addButton.Name = "addButton";
      this.addButton.Size = new System.Drawing.Size(74, 29);
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
      this.refreshButton.Size = new System.Drawing.Size(24, 29);
      this.refreshButton.Text = "refreshButton";
      this.refreshButton.ToolTipText = "Refresh";
      this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
      // 
      // runButton
      // 
      this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
      this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.runButton.Name = "runButton";
      this.runButton.Size = new System.Drawing.Size(74, 29);
      this.runButton.Text = "Run";
      this.runButton.Click += new System.EventHandler(this.runButton_Click);
      // 
      // toolStripDropDownButton2
      // 
      this.toolStripDropDownButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.toolStripMenuItem7,
            this.toolStripSeparator1,
            this.toolStripMenuItem11,
            this.カスタマイズToolStripMenuItem,
            this.toolStripSeparator6,
            this.toolStripMenuItem21,
            this.toolStripMenuItem31,
            this.toolStripMenuItem32,
            this.toolStripSeparator7,
            this.試験ToolStripMenuItem});
      this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
      this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
      this.toolStripDropDownButton2.Size = new System.Drawing.Size(34, 29);
      this.toolStripDropDownButton2.Text = "ツール";
      this.toolStripDropDownButton2.ToolTipText = "ツール";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(295, 30);
      this.toolStripMenuItem1.Text = "最近開いたツリーファイル";
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(337, 30);
      this.toolStripMenuItem2.Text = "最近開いたツリーファイルをクリア";
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(295, 30);
      this.toolStripMenuItem3.Text = "ファイル状態の保存と復元";
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(266, 30);
      this.toolStripMenuItem4.Text = "前回セッションの復元";
      // 
      // toolStripMenuItem5
      // 
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      this.toolStripMenuItem5.Size = new System.Drawing.Size(266, 30);
      this.toolStripMenuItem5.Text = "復元ポイントの作成";
      // 
      // toolStripMenuItem6
      // 
      this.toolStripMenuItem6.Name = "toolStripMenuItem6";
      this.toolStripMenuItem6.Size = new System.Drawing.Size(266, 30);
      this.toolStripMenuItem6.Text = "全復元ポイントのクリア";
      // 
      // toolStripMenuItem7
      // 
      this.toolStripMenuItem7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10});
      this.toolStripMenuItem7.Name = "toolStripMenuItem7";
      this.toolStripMenuItem7.Size = new System.Drawing.Size(295, 30);
      this.toolStripMenuItem7.Text = "出力";
      // 
      // toolStripMenuItem8
      // 
      this.toolStripMenuItem8.Checked = true;
      this.toolStripMenuItem8.CheckOnClick = true;
      this.toolStripMenuItem8.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripMenuItem8.Name = "toolStripMenuItem8";
      this.toolStripMenuItem8.Size = new System.Drawing.Size(190, 30);
      this.toolStripMenuItem8.Text = "Console";
      // 
      // toolStripMenuItem9
      // 
      this.toolStripMenuItem9.CheckOnClick = true;
      this.toolStripMenuItem9.Name = "toolStripMenuItem9";
      this.toolStripMenuItem9.Size = new System.Drawing.Size(190, 30);
      this.toolStripMenuItem9.Text = "Document";
      // 
      // toolStripMenuItem10
      // 
      this.toolStripMenuItem10.CheckOnClick = true;
      this.toolStripMenuItem10.Name = "toolStripMenuItem10";
      this.toolStripMenuItem10.Size = new System.Drawing.Size(190, 30);
      this.toolStripMenuItem10.Text = "Trace";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(292, 6);
      // 
      // toolStripMenuItem11
      // 
      this.toolStripMenuItem11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.内部ターゲット表示ToolStripMenuItem,
            this.プロパティ表示ToolStripMenuItem,
            this.子ノード表示ToolStripMenuItem,
            this.アイコン表示MenuItem,
            this.toolStripMenuItem16,
            this.toolStripSeparator2,
            this.toolStripMenuItem20});
      this.toolStripMenuItem11.Name = "toolStripMenuItem11";
      this.toolStripMenuItem11.Size = new System.Drawing.Size(295, 30);
      this.toolStripMenuItem11.Text = "オプション";
      // 
      // 内部ターゲット表示ToolStripMenuItem
      // 
      this.内部ターゲット表示ToolStripMenuItem.Checked = true;
      this.内部ターゲット表示ToolStripMenuItem.CheckOnClick = true;
      this.内部ターゲット表示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.内部ターゲット表示ToolStripMenuItem.Name = "内部ターゲット表示ToolStripMenuItem";
      this.内部ターゲット表示ToolStripMenuItem.Size = new System.Drawing.Size(242, 30);
      this.内部ターゲット表示ToolStripMenuItem.Text = "内部ターゲット表示";
      // 
      // プロパティ表示ToolStripMenuItem
      // 
      this.プロパティ表示ToolStripMenuItem.CheckOnClick = true;
      this.プロパティ表示ToolStripMenuItem.Name = "プロパティ表示ToolStripMenuItem";
      this.プロパティ表示ToolStripMenuItem.Size = new System.Drawing.Size(242, 30);
      this.プロパティ表示ToolStripMenuItem.Text = "プロパティ表示";
      // 
      // 子ノード表示ToolStripMenuItem
      // 
      this.子ノード表示ToolStripMenuItem.Checked = true;
      this.子ノード表示ToolStripMenuItem.CheckOnClick = true;
      this.子ノード表示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.子ノード表示ToolStripMenuItem.Name = "子ノード表示ToolStripMenuItem";
      this.子ノード表示ToolStripMenuItem.Size = new System.Drawing.Size(242, 30);
      this.子ノード表示ToolStripMenuItem.Text = "子ノード表示";
      // 
      // アイコン表示MenuItem
      // 
      this.アイコン表示MenuItem.CheckOnClick = true;
      this.アイコン表示MenuItem.Name = "アイコン表示MenuItem";
      this.アイコン表示MenuItem.Size = new System.Drawing.Size(242, 30);
      this.アイコン表示MenuItem.Text = "アイコン表示";
      this.アイコン表示MenuItem.Click += new System.EventHandler(this.アイコン表示MenuItem_Click);
      // 
      // toolStripMenuItem16
      // 
      this.toolStripMenuItem16.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem17,
            this.toolStripMenuItem18,
            this.toolStripMenuItem19});
      this.toolStripMenuItem16.Name = "toolStripMenuItem16";
      this.toolStripMenuItem16.Size = new System.Drawing.Size(242, 30);
      this.toolStripMenuItem16.Text = "ツールボタン";
      // 
      // toolStripMenuItem17
      // 
      this.toolStripMenuItem17.CheckOnClick = true;
      this.toolStripMenuItem17.Name = "toolStripMenuItem17";
      this.toolStripMenuItem17.Size = new System.Drawing.Size(252, 30);
      this.toolStripMenuItem17.Text = "新規作成";
      // 
      // toolStripMenuItem18
      // 
      this.toolStripMenuItem18.Name = "toolStripMenuItem18";
      this.toolStripMenuItem18.Size = new System.Drawing.Size(252, 30);
      this.toolStripMenuItem18.Text = "名前を付けて保存";
      // 
      // toolStripMenuItem19
      // 
      this.toolStripMenuItem19.CheckOnClick = true;
      this.toolStripMenuItem19.Name = "toolStripMenuItem19";
      this.toolStripMenuItem19.Size = new System.Drawing.Size(252, 30);
      this.toolStripMenuItem19.Text = "ImageListButton";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(239, 6);
      // 
      // toolStripMenuItem20
      // 
      this.toolStripMenuItem20.Checked = true;
      this.toolStripMenuItem20.CheckOnClick = true;
      this.toolStripMenuItem20.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripMenuItem20.Name = "toolStripMenuItem20";
      this.toolStripMenuItem20.Size = new System.Drawing.Size(242, 30);
      this.toolStripMenuItem20.Text = "CSOutlinePanel";
      // 
      // カスタマイズToolStripMenuItem
      // 
      this.カスタマイズToolStripMenuItem.Name = "カスタマイズToolStripMenuItem";
      this.カスタマイズToolStripMenuItem.Size = new System.Drawing.Size(295, 30);
      this.カスタマイズToolStripMenuItem.Text = "カスタマイズ";
      this.カスタマイズToolStripMenuItem.Click += new System.EventHandler(this.カスタマイズToolStripMenuItem_Click);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(292, 6);
      // 
      // toolStripMenuItem21
      // 
      this.toolStripMenuItem21.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.サクラエディタToolStripMenuItem,
            this.pSPadToolStripMenuItem,
            this.toolStripSeparator3,
            this.richTextBoxToolStripMenuItem,
            this.scintillaCToolStripMenuItem,
            this.azukiEditorZToolStripMenuItem,
            this.toolStripSeparator4,
            this.エクスプローラToolStripMenuItem,
            this.コマンドプロンプトToolStripMenuItem,
            this.toolStripSeparator5,
            this.toolStripMenuItem29,
            this.toolStripMenuItem30});
      this.toolStripMenuItem21.Name = "toolStripMenuItem21";
      this.toolStripMenuItem21.Size = new System.Drawing.Size(295, 30);
      this.toolStripMenuItem21.Text = "開く(&O)";
      // 
      // サクラエディタToolStripMenuItem
      // 
      this.サクラエディタToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("サクラエディタToolStripMenuItem.Image")));
      this.サクラエディタToolStripMenuItem.Name = "サクラエディタToolStripMenuItem";
      this.サクラエディタToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.サクラエディタToolStripMenuItem.Text = "サクラエディタ";
      // 
      // pSPadToolStripMenuItem
      // 
      this.pSPadToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pSPadToolStripMenuItem.Image")));
      this.pSPadToolStripMenuItem.Name = "pSPadToolStripMenuItem";
      this.pSPadToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.pSPadToolStripMenuItem.Text = "PSPad";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(321, 6);
      // 
      // richTextBoxToolStripMenuItem
      // 
      this.richTextBoxToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("richTextBoxToolStripMenuItem.Image")));
      this.richTextBoxToolStripMenuItem.Name = "richTextBoxToolStripMenuItem";
      this.richTextBoxToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.richTextBoxToolStripMenuItem.Text = "RichTextEditor(&R)";
      // 
      // scintillaCToolStripMenuItem
      // 
      this.scintillaCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("scintillaCToolStripMenuItem.Image")));
      this.scintillaCToolStripMenuItem.Name = "scintillaCToolStripMenuItem";
      this.scintillaCToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.scintillaCToolStripMenuItem.Text = "Scintilla(&A)";
      // 
      // azukiEditorZToolStripMenuItem
      // 
      this.azukiEditorZToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("azukiEditorZToolStripMenuItem.Image")));
      this.azukiEditorZToolStripMenuItem.Name = "azukiEditorZToolStripMenuItem";
      this.azukiEditorZToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.azukiEditorZToolStripMenuItem.Text = "Azuki Editor(&Z)";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(321, 6);
      // 
      // エクスプローラToolStripMenuItem
      // 
      this.エクスプローラToolStripMenuItem.Enabled = false;
      this.エクスプローラToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("エクスプローラToolStripMenuItem.Image")));
      this.エクスプローラToolStripMenuItem.Name = "エクスプローラToolStripMenuItem";
      this.エクスプローラToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.エクスプローラToolStripMenuItem.Text = "エクスプローラ";
      // 
      // コマンドプロンプトToolStripMenuItem
      // 
      this.コマンドプロンプトToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("コマンドプロンプトToolStripMenuItem.Image")));
      this.コマンドプロンプトToolStripMenuItem.Name = "コマンドプロンプトToolStripMenuItem";
      this.コマンドプロンプトToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.コマンドプロンプトToolStripMenuItem.Text = "コマンドプロンプト";
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(321, 6);
      // 
      // toolStripMenuItem29
      // 
      this.toolStripMenuItem29.Name = "toolStripMenuItem29";
      this.toolStripMenuItem29.Size = new System.Drawing.Size(324, 30);
      this.toolStripMenuItem29.Text = "ファイル名を指定して実行(&O)";
      // 
      // toolStripMenuItem30
      // 
      this.toolStripMenuItem30.Name = "toolStripMenuItem30";
      this.toolStripMenuItem30.Size = new System.Drawing.Size(324, 30);
      this.toolStripMenuItem30.Text = "リンクを開く(&L)";
      // 
      // toolStripMenuItem31
      // 
      this.toolStripMenuItem31.Name = "toolStripMenuItem31";
      this.toolStripMenuItem31.Size = new System.Drawing.Size(295, 30);
      this.toolStripMenuItem31.Text = "アウトライン表示";
      this.toolStripMenuItem31.ToolTipText = "アウトライン表示";
      // 
      // toolStripMenuItem32
      // 
      this.toolStripMenuItem32.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem33});
      this.toolStripMenuItem32.Name = "toolStripMenuItem32";
      this.toolStripMenuItem32.Size = new System.Drawing.Size(295, 30);
      this.toolStripMenuItem32.Text = "Ant コマンド";
      // 
      // toolStripMenuItem33
      // 
      this.toolStripMenuItem33.CheckOnClick = true;
      this.toolStripMenuItem33.Name = "toolStripMenuItem33";
      this.toolStripMenuItem33.Size = new System.Drawing.Size(148, 30);
      this.toolStripMenuItem33.Text = "Ant17";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(292, 6);
      // 
      // 試験ToolStripMenuItem
      // 
      this.試験ToolStripMenuItem.Name = "試験ToolStripMenuItem";
      this.試験ToolStripMenuItem.Size = new System.Drawing.Size(295, 30);
      this.試験ToolStripMenuItem.Text = "試験";
      this.試験ToolStripMenuItem.Click += new System.EventHandler(this.試験ToolStripMenuItem_Click);
      // 
      // imageListStripButton
      // 
      this.imageListStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.imageListStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.imageListStripButton.Image = ((System.Drawing.Image)(resources.GetObject("imageListStripButton.Image")));
      this.imageListStripButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.imageListStripButton.Name = "imageListStripButton";
      this.imageListStripButton.Size = new System.Drawing.Size(24, 29);
      this.imageListStripButton.Text = "imageListStripButton";
      this.imageListStripButton.Visible = false;
      // 
      // csOutlineButton1
      // 
      this.csOutlineButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.csOutlineButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.csOutlineButton1.Image = ((System.Drawing.Image)(resources.GetObject("csOutlineButton1.Image")));
      this.csOutlineButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.csOutlineButton1.Name = "csOutlineButton1";
      this.csOutlineButton1.Size = new System.Drawing.Size(24, 29);
      this.csOutlineButton1.Text = "csOutlineButton1";
      this.csOutlineButton1.ToolTipText = "アウトラインツリーを表示します";
      // 
      // syncronizeDodument
      // 
      this.syncronizeDodument.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.syncronizeDodument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.syncronizeDodument.Image = ((System.Drawing.Image)(resources.GetObject("syncronizeDodument.Image")));
      this.syncronizeDodument.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.syncronizeDodument.Name = "syncronizeDodument";
      this.syncronizeDodument.Size = new System.Drawing.Size(24, 29);
      this.syncronizeDodument.Text = "syncronizeDodument";
      this.syncronizeDodument.ToolTipText = "ファイルエクスプローラとシンクロ";
      // 
      // syncronizeButton
      // 
      this.syncronizeButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.syncronizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.syncronizeButton.Image = ((System.Drawing.Image)(resources.GetObject("syncronizeButton.Image")));
      this.syncronizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.syncronizeButton.Name = "syncronizeButton";
      this.syncronizeButton.Size = new System.Drawing.Size(24, 29);
      this.syncronizeButton.Text = "syncronizeButton";
      this.syncronizeButton.ToolTipText = "現在のドキュメントをノードに追加";
      // 
      // 表示ToolStripButton
      // 
      this.表示ToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.表示ToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.表示ToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("表示ToolStripButton.Image")));
      this.表示ToolStripButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.表示ToolStripButton.Name = "表示ToolStripButton";
      this.表示ToolStripButton.Size = new System.Drawing.Size(24, 29);
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
      this.homeStripButton.Size = new System.Drawing.Size(24, 29);
      this.homeStripButton.Text = "homeStripButton";
      // 
      // removeButton
      // 
      this.removeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.removeButton.Image = ((System.Drawing.Image)(resources.GetObject("removeButton.Image")));
      this.removeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.removeButton.Name = "removeButton";
      this.removeButton.Size = new System.Drawing.Size(24, 29);
      this.removeButton.Text = "toolStripButton2";
      this.removeButton.Visible = false;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(4, 5);
      this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
      this.splitContainer1.Size = new System.Drawing.Size(1177, 563);
      this.splitContainer1.SplitterDistance = 347;
      this.splitContainer1.SplitterWidth = 7;
      this.splitContainer1.TabIndex = 3;
      // 
      // treeView
      // 
      this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView.Font = new System.Drawing.Font("Meiryo UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.treeView.HideSelection = false;
      this.treeView.ImageIndex = 0;
      this.treeView.ImageList = this.imageList;
      this.treeView.Location = new System.Drawing.Point(0, 0);
      this.treeView.Name = "treeView";
      this.treeView.SelectedImageIndex = 0;
      this.treeView.ShowNodeToolTips = true;
      this.treeView.Size = new System.Drawing.Size(1177, 347);
      this.treeView.TabIndex = 1;
      this.treeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeCollapse);
      this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
      this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
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
      // propertyGrid1
      // 
      this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
      this.propertyGrid1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(1177, 209);
      this.propertyGrid1.TabIndex = 1;
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 4);
      this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage2.Size = new System.Drawing.Size(1185, 573);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Dir";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // tabPage3
      // 
      this.tabPage3.Location = new System.Drawing.Point(4, 4);
      this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new System.Drawing.Size(1185, 573);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "FTP";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Location = new System.Drawing.Point(4, 4);
      this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Size = new System.Drawing.Size(1185, 573);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Link";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // tabPage5
      // 
      this.tabPage5.Controls.Add(this.propertyGrid3);
      this.tabPage5.Location = new System.Drawing.Point(4, 4);
      this.tabPage5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage5.Size = new System.Drawing.Size(1185, 573);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "Settings";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // propertyGrid3
      // 
      this.propertyGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertyGrid3.Location = new System.Drawing.Point(4, 5);
      this.propertyGrid3.Name = "propertyGrid3";
      this.propertyGrid3.Size = new System.Drawing.Size(1177, 563);
      this.propertyGrid3.TabIndex = 0;
      this.propertyGrid3.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid3_PropertyValueChanged);
      // 
      // gitButton
      // 
      this.gitButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.gitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.gitButton.Image = ((System.Drawing.Image)(resources.GetObject("gitButton.Image")));
      this.gitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.gitButton.Name = "gitButton";
      this.gitButton.Size = new System.Drawing.Size(24, 29);
      this.gitButton.Text = "gitButton";
      this.gitButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.gitButton.ToolTipText = "Git Bash を起動します";
      this.gitButton.Click += new System.EventHandler(this.gitButton_Click);
      // 
      // AntPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tabControl1);
      this.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "AntPanel";
      this.Size = new System.Drawing.Size(1193, 611);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.tabPage5.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    public System.Windows.Forms.ToolStrip toolStrip;
    public System.Windows.Forms.ToolStripButton addButton;
    public System.Windows.Forms.ToolStripButton refreshButton;
    public System.Windows.Forms.ToolStripButton runButton;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
    public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
    public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
    public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
    private System.Windows.Forms.ToolStripMenuItem 内部ターゲット表示ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem プロパティ表示ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem 子ノード表示ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem アイコン表示MenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem18;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem20;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem21;
    private System.Windows.Forms.ToolStripMenuItem サクラエディタToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pSPadToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem richTextBoxToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem scintillaCToolStripMenuItem;
    public System.Windows.Forms.ToolStripMenuItem azukiEditorZToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem エクスプローラToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem コマンドプロンプトToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem29;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem30;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem31;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem32;
    public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem33;
    private System.Windows.Forms.ToolStripButton imageListStripButton;
    private System.Windows.Forms.ToolStripButton csOutlineButton1;
    private System.Windows.Forms.ToolStripButton syncronizeDodument;
    private System.Windows.Forms.ToolStripButton syncronizeButton;
    private System.Windows.Forms.ToolStripButton 表示ToolStripButton;
    private System.Windows.Forms.ToolStripButton homeStripButton;
    private System.Windows.Forms.SplitContainer splitContainer1;
    public System.Windows.Forms.TreeView treeView;
    public System.Windows.Forms.PropertyGrid propertyGrid1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.TabPage tabPage5;
    public System.Windows.Forms.ImageList imageList;
    public System.Windows.Forms.ToolStripButton removeButton;

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
        this.AddBuildFiles(this.dropFiles);
      }
    }

    public void treeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      TreeView tv = (TreeView)sender;
      TreeNode selectedNode = tv.SelectedNode;
      this.ShowNodeInfo(selectedNode, tv);
    }

    private void ShowNodeInfo(TreeNode treeNode, TreeView treeView)
    {
      if (treeNode != null && treeNode.Tag != null)
      {
        if (treeNode.Tag is NodeInfo)
        {
          NodeInfo selectedObject = new NodeInfo();
          selectedObject = (NodeInfo)treeNode.Tag;
          this.propertyGrid1.SelectedObject = selectedObject;
        }
        else if (treeNode.Tag is TaskInfo)
        {
          TaskInfo selectedObject = new TaskInfo();
          selectedObject = (TaskInfo)treeNode.Tag;
          this.propertyGrid1.SelectedObject = selectedObject;
        }
        else if (treeNode.Tag is XmlElement)
        {
          this.propertyGrid1.SelectedObject = (XmlElement)treeNode.Tag;
        }
        else if (treeNode.Tag is CSParser.Model.MemberModel)
        {
           this.propertyGrid1.SelectedObject = (CSParser.Model.MemberModel)treeNode.Tag;
        }
        else if (treeNode.Tag is String)
        {
          String path = treeNode.Tag as String;
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
    }

    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem カスタマイズToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripMenuItem 試験ToolStripMenuItem;
    private PropertyGrid propertyGrid3;
    private ToolStripButton gitButton;
  }
}
