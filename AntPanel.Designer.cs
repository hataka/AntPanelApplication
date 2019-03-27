using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.新規作成NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.上書き保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.名前を付けて保存AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.印刷PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.印刷プレビューVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.元に戻すUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.やり直しRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.切り取りTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.コピーCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.貼り付けPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.すべて選択AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.表示VToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.ツールバーTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ステータスバーSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.tabPageModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.richTextEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
      this.dockingTreeViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトを実行XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトを編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトメニュー更新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
      this.ツールTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.カスタマイズCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.オプションOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.試験ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.内容CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.インデックスIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.検索SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.バージョン情報AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.新規作成NToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.開くOToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.上書き保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.印刷PToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.切り取りUToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.コピーCToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.貼り付けPToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.ヘルプLToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.メニューバーMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ステータスバーSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.fdImageButton = new System.Windows.Forms.ToolStripButton();
      this.fdImage32Button = new System.Windows.Forms.ToolStripButton();
      this.mainForm_toggleButton = new System.Windows.Forms.ToolStripButton();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
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
      this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
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
      this.imageListStripButton = new System.Windows.Forms.ToolStripButton();
      this.csOutlineButton1 = new System.Windows.Forms.ToolStripButton();
      this.syncronizeDodument = new System.Windows.Forms.ToolStripButton();
      this.syncronizeButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
      this.homeStripButton = new System.Windows.Forms.ToolStripButton();
      this.removeButton = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.treeView = new System.Windows.Forms.TreeView();
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
      this.miniToolStrip = new System.Windows.Forms.ToolStrip();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.propertyGrid3 = new System.Windows.Forms.PropertyGrid();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.toolStrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage5.SuspendLayout();
      this.SuspendLayout();
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
      // menuStrip1
      // 
      this.menuStrip1.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.表示VToolStripMenuItem1,
            this.スクリプトCToolStripMenuItem,
            this.ツールTToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(1193, 33);
      this.menuStrip1.TabIndex = 7;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // ファイルFToolStripMenuItem
      // 
      this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成NToolStripMenuItem,
            this.開くOToolStripMenuItem,
            this.toolStripSeparator,
            this.上書き保存SToolStripMenuItem,
            this.名前を付けて保存AToolStripMenuItem,
            this.toolStripSeparator1,
            this.印刷PToolStripMenuItem,
            this.印刷プレビューVToolStripMenuItem,
            this.toolStripSeparator6,
            this.終了XToolStripMenuItem});
      this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
      this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(109, 29);
      this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
      // 
      // 新規作成NToolStripMenuItem
      // 
      this.新規作成NToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新規作成NToolStripMenuItem.Image")));
      this.新規作成NToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.新規作成NToolStripMenuItem.Name = "新規作成NToolStripMenuItem";
      this.新規作成NToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.新規作成NToolStripMenuItem.Size = new System.Drawing.Size(291, 30);
      this.新規作成NToolStripMenuItem.Text = "新規作成(&N)";
      // 
      // 開くOToolStripMenuItem
      // 
      this.開くOToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripMenuItem.Image")));
      this.開くOToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
      this.開くOToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.開くOToolStripMenuItem.Size = new System.Drawing.Size(291, 30);
      this.開くOToolStripMenuItem.Text = "開く(&O)";
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(288, 6);
      // 
      // 上書き保存SToolStripMenuItem
      // 
      this.上書き保存SToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("上書き保存SToolStripMenuItem.Image")));
      this.上書き保存SToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.上書き保存SToolStripMenuItem.Name = "上書き保存SToolStripMenuItem";
      this.上書き保存SToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.上書き保存SToolStripMenuItem.Size = new System.Drawing.Size(291, 30);
      this.上書き保存SToolStripMenuItem.Text = "上書き保存(&S)";
      // 
      // 名前を付けて保存AToolStripMenuItem
      // 
      this.名前を付けて保存AToolStripMenuItem.Name = "名前を付けて保存AToolStripMenuItem";
      this.名前を付けて保存AToolStripMenuItem.Size = new System.Drawing.Size(291, 30);
      this.名前を付けて保存AToolStripMenuItem.Text = "名前を付けて保存(&A)";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(288, 6);
      // 
      // 印刷PToolStripMenuItem
      // 
      this.印刷PToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripMenuItem.Image")));
      this.印刷PToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.印刷PToolStripMenuItem.Name = "印刷PToolStripMenuItem";
      this.印刷PToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
      this.印刷PToolStripMenuItem.Size = new System.Drawing.Size(291, 30);
      this.印刷PToolStripMenuItem.Text = "印刷(&P)";
      // 
      // 印刷プレビューVToolStripMenuItem
      // 
      this.印刷プレビューVToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("印刷プレビューVToolStripMenuItem.Image")));
      this.印刷プレビューVToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.印刷プレビューVToolStripMenuItem.Name = "印刷プレビューVToolStripMenuItem";
      this.印刷プレビューVToolStripMenuItem.Size = new System.Drawing.Size(291, 30);
      this.印刷プレビューVToolStripMenuItem.Text = "印刷プレビュー(&V)";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(288, 6);
      // 
      // 終了XToolStripMenuItem
      // 
      this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
      this.終了XToolStripMenuItem.Size = new System.Drawing.Size(291, 30);
      this.終了XToolStripMenuItem.Text = "終了(&X)";
      // 
      // 編集EToolStripMenuItem
      // 
      this.編集EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.元に戻すUToolStripMenuItem,
            this.やり直しRToolStripMenuItem,
            this.toolStripSeparator7,
            this.切り取りTToolStripMenuItem,
            this.コピーCToolStripMenuItem,
            this.貼り付けPToolStripMenuItem,
            this.toolStripSeparator8,
            this.すべて選択AToolStripMenuItem});
      this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
      this.編集EToolStripMenuItem.Size = new System.Drawing.Size(94, 29);
      this.編集EToolStripMenuItem.Text = "編集(&E)";
      // 
      // 元に戻すUToolStripMenuItem
      // 
      this.元に戻すUToolStripMenuItem.Name = "元に戻すUToolStripMenuItem";
      this.元に戻すUToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
      this.元に戻すUToolStripMenuItem.Size = new System.Drawing.Size(271, 30);
      this.元に戻すUToolStripMenuItem.Text = "元に戻す(&U)";
      // 
      // やり直しRToolStripMenuItem
      // 
      this.やり直しRToolStripMenuItem.Name = "やり直しRToolStripMenuItem";
      this.やり直しRToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
      this.やり直しRToolStripMenuItem.Size = new System.Drawing.Size(271, 30);
      this.やり直しRToolStripMenuItem.Text = "やり直し(&R)";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(268, 6);
      // 
      // 切り取りTToolStripMenuItem
      // 
      this.切り取りTToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("切り取りTToolStripMenuItem.Image")));
      this.切り取りTToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.切り取りTToolStripMenuItem.Name = "切り取りTToolStripMenuItem";
      this.切り取りTToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
      this.切り取りTToolStripMenuItem.Size = new System.Drawing.Size(271, 30);
      this.切り取りTToolStripMenuItem.Text = "切り取り(&T)";
      // 
      // コピーCToolStripMenuItem
      // 
      this.コピーCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripMenuItem.Image")));
      this.コピーCToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.コピーCToolStripMenuItem.Name = "コピーCToolStripMenuItem";
      this.コピーCToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.コピーCToolStripMenuItem.Size = new System.Drawing.Size(271, 30);
      this.コピーCToolStripMenuItem.Text = "コピー(&C)";
      // 
      // 貼り付けPToolStripMenuItem
      // 
      this.貼り付けPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripMenuItem.Image")));
      this.貼り付けPToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.貼り付けPToolStripMenuItem.Name = "貼り付けPToolStripMenuItem";
      this.貼り付けPToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
      this.貼り付けPToolStripMenuItem.Size = new System.Drawing.Size(271, 30);
      this.貼り付けPToolStripMenuItem.Text = "貼り付け(&P)";
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(268, 6);
      // 
      // すべて選択AToolStripMenuItem
      // 
      this.すべて選択AToolStripMenuItem.Name = "すべて選択AToolStripMenuItem";
      this.すべて選択AToolStripMenuItem.Size = new System.Drawing.Size(271, 30);
      this.すべて選択AToolStripMenuItem.Text = "すべて選択(&A)";
      // 
      // 表示VToolStripMenuItem1
      // 
      this.表示VToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ツールバーTToolStripMenuItem,
            this.ステータスバーSToolStripMenuItem,
            this.toolStripSeparator12,
            this.tabPageModeToolStripMenuItem,
            this.toolStripSeparator13,
            this.richTextEditorToolStripMenuItem,
            this.statusTextToolStripMenuItem,
            this.toolStripSeparator14,
            this.dockingTreeViewToolStripMenuItem});
      this.表示VToolStripMenuItem1.Name = "表示VToolStripMenuItem1";
      this.表示VToolStripMenuItem1.Size = new System.Drawing.Size(96, 29);
      this.表示VToolStripMenuItem1.Text = "表示(&V)";
      // 
      // ツールバーTToolStripMenuItem
      // 
      this.ツールバーTToolStripMenuItem.Checked = true;
      this.ツールバーTToolStripMenuItem.CheckOnClick = true;
      this.ツールバーTToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ツールバーTToolStripMenuItem.Name = "ツールバーTToolStripMenuItem";
      this.ツールバーTToolStripMenuItem.Size = new System.Drawing.Size(272, 30);
      this.ツールバーTToolStripMenuItem.Text = "ツールバー(&T)";
      this.ツールバーTToolStripMenuItem.Click += new System.EventHandler(this.ツールバーTToolStripMenuItem_Click);
      // 
      // ステータスバーSToolStripMenuItem
      // 
      this.ステータスバーSToolStripMenuItem.Checked = true;
      this.ステータスバーSToolStripMenuItem.CheckOnClick = true;
      this.ステータスバーSToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ステータスバーSToolStripMenuItem.Name = "ステータスバーSToolStripMenuItem";
      this.ステータスバーSToolStripMenuItem.Size = new System.Drawing.Size(272, 30);
      this.ステータスバーSToolStripMenuItem.Text = "ステータスバー(&S)";
      this.ステータスバーSToolStripMenuItem.Click += new System.EventHandler(this.ステータスバーSToolStripMenuItem_Click);
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(269, 6);
      // 
      // tabPageModeToolStripMenuItem
      // 
      this.tabPageModeToolStripMenuItem.CheckOnClick = true;
      this.tabPageModeToolStripMenuItem.Enabled = false;
      this.tabPageModeToolStripMenuItem.Name = "tabPageModeToolStripMenuItem";
      this.tabPageModeToolStripMenuItem.Size = new System.Drawing.Size(272, 30);
      this.tabPageModeToolStripMenuItem.Text = "TabPage Mode";
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new System.Drawing.Size(269, 6);
      // 
      // richTextEditorToolStripMenuItem
      // 
      this.richTextEditorToolStripMenuItem.Enabled = false;
      this.richTextEditorToolStripMenuItem.Name = "richTextEditorToolStripMenuItem";
      this.richTextEditorToolStripMenuItem.Size = new System.Drawing.Size(272, 30);
      this.richTextEditorToolStripMenuItem.Text = "RichTextEditor";
      // 
      // statusTextToolStripMenuItem
      // 
      this.statusTextToolStripMenuItem.Enabled = false;
      this.statusTextToolStripMenuItem.Name = "statusTextToolStripMenuItem";
      this.statusTextToolStripMenuItem.Size = new System.Drawing.Size(272, 30);
      this.statusTextToolStripMenuItem.Text = "StatusText";
      // 
      // toolStripSeparator14
      // 
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      this.toolStripSeparator14.Size = new System.Drawing.Size(269, 6);
      // 
      // dockingTreeViewToolStripMenuItem
      // 
      this.dockingTreeViewToolStripMenuItem.CheckOnClick = true;
      this.dockingTreeViewToolStripMenuItem.Enabled = false;
      this.dockingTreeViewToolStripMenuItem.Name = "dockingTreeViewToolStripMenuItem";
      this.dockingTreeViewToolStripMenuItem.Size = new System.Drawing.Size(272, 30);
      this.dockingTreeViewToolStripMenuItem.Text = "Docking TreeView ";
      // 
      // スクリプトCToolStripMenuItem
      // 
      this.スクリプトCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.スクリプトを実行XToolStripMenuItem,
            this.スクリプトを編集EToolStripMenuItem,
            this.スクリプトメニュー更新RToolStripMenuItem,
            this.toolStripSeparator28});
      this.スクリプトCToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.スクリプトCToolStripMenuItem.Name = "スクリプトCToolStripMenuItem";
      this.スクリプトCToolStripMenuItem.Size = new System.Drawing.Size(125, 29);
      this.スクリプトCToolStripMenuItem.Text = "スクリプト(&C)";
      // 
      // スクリプトを実行XToolStripMenuItem
      // 
      this.スクリプトを実行XToolStripMenuItem.Name = "スクリプトを実行XToolStripMenuItem";
      this.スクリプトを実行XToolStripMenuItem.Size = new System.Drawing.Size(290, 30);
      this.スクリプトを実行XToolStripMenuItem.Text = "スクリプトを実行(&X)";
      // 
      // スクリプトを編集EToolStripMenuItem
      // 
      this.スクリプトを編集EToolStripMenuItem.Name = "スクリプトを編集EToolStripMenuItem";
      this.スクリプトを編集EToolStripMenuItem.Size = new System.Drawing.Size(290, 30);
      this.スクリプトを編集EToolStripMenuItem.Text = "スクリプトを編集(&E)";
      // 
      // スクリプトメニュー更新RToolStripMenuItem
      // 
      this.スクリプトメニュー更新RToolStripMenuItem.Name = "スクリプトメニュー更新RToolStripMenuItem";
      this.スクリプトメニュー更新RToolStripMenuItem.Size = new System.Drawing.Size(290, 30);
      this.スクリプトメニュー更新RToolStripMenuItem.Text = "スクリプトメニュー更新(&R)";
      // 
      // toolStripSeparator28
      // 
      this.toolStripSeparator28.Name = "toolStripSeparator28";
      this.toolStripSeparator28.Size = new System.Drawing.Size(287, 6);
      // 
      // ツールTToolStripMenuItem
      // 
      this.ツールTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.カスタマイズCToolStripMenuItem,
            this.オプションOToolStripMenuItem,
            this.試験ToolStripMenuItem});
      this.ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem";
      this.ツールTToolStripMenuItem.Size = new System.Drawing.Size(102, 29);
      this.ツールTToolStripMenuItem.Text = "ツール(&T)";
      // 
      // カスタマイズCToolStripMenuItem
      // 
      this.カスタマイズCToolStripMenuItem.Name = "カスタマイズCToolStripMenuItem";
      this.カスタマイズCToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
      this.カスタマイズCToolStripMenuItem.Text = "カスタマイズ(&C)";
      // 
      // オプションOToolStripMenuItem
      // 
      this.オプションOToolStripMenuItem.Name = "オプションOToolStripMenuItem";
      this.オプションOToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
      this.オプションOToolStripMenuItem.Text = "オプション(&O)";
      // 
      // 試験ToolStripMenuItem
      // 
      this.試験ToolStripMenuItem.Name = "試験ToolStripMenuItem";
      this.試験ToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
      this.試験ToolStripMenuItem.Text = "試験";
      this.試験ToolStripMenuItem.Click += new System.EventHandler(this.試験ToolStripMenuItem_Click);
      // 
      // ヘルプHToolStripMenuItem
      // 
      this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.内容CToolStripMenuItem,
            this.インデックスIToolStripMenuItem,
            this.検索SToolStripMenuItem,
            this.toolStripSeparator9,
            this.バージョン情報AToolStripMenuItem});
      this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
      this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(104, 29);
      this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
      // 
      // 内容CToolStripMenuItem
      // 
      this.内容CToolStripMenuItem.Name = "内容CToolStripMenuItem";
      this.内容CToolStripMenuItem.Size = new System.Drawing.Size(258, 30);
      this.内容CToolStripMenuItem.Text = "内容(&C)";
      // 
      // インデックスIToolStripMenuItem
      // 
      this.インデックスIToolStripMenuItem.Name = "インデックスIToolStripMenuItem";
      this.インデックスIToolStripMenuItem.Size = new System.Drawing.Size(258, 30);
      this.インデックスIToolStripMenuItem.Text = "インデックス(&I)";
      // 
      // 検索SToolStripMenuItem
      // 
      this.検索SToolStripMenuItem.Name = "検索SToolStripMenuItem";
      this.検索SToolStripMenuItem.Size = new System.Drawing.Size(258, 30);
      this.検索SToolStripMenuItem.Text = "検索(&S)";
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(255, 6);
      // 
      // バージョン情報AToolStripMenuItem
      // 
      this.バージョン情報AToolStripMenuItem.Name = "バージョン情報AToolStripMenuItem";
      this.バージョン情報AToolStripMenuItem.Size = new System.Drawing.Size(258, 30);
      this.バージョン情報AToolStripMenuItem.Text = "バージョン情報(&A)...";
      // 
      // toolStrip1
      // 
      this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成NToolStripButton,
            this.開くOToolStripButton,
            this.上書き保存SToolStripButton,
            this.印刷PToolStripButton,
            this.toolStripSeparator10,
            this.切り取りUToolStripButton,
            this.コピーCToolStripButton,
            this.貼り付けPToolStripButton,
            this.toolStripSeparator11,
            this.ヘルプLToolStripButton,
            this.toolStripDropDownButton1,
            this.fdImageButton,
            this.fdImage32Button,
            this.mainForm_toggleButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 33);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(1193, 27);
      this.toolStrip1.TabIndex = 8;
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
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(6, 27);
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
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(6, 27);
      // 
      // ヘルプLToolStripButton
      // 
      this.ヘルプLToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ヘルプLToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ヘルプLToolStripButton.Image")));
      this.ヘルプLToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ヘルプLToolStripButton.Name = "ヘルプLToolStripButton";
      this.ヘルプLToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.ヘルプLToolStripButton.Text = "ヘルプ(&L)";
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.メニューバーMToolStripMenuItem,
            this.ステータスバーSToolStripMenuItem1});
      this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(34, 24);
      this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
      // 
      // メニューバーMToolStripMenuItem
      // 
      this.メニューバーMToolStripMenuItem.Checked = true;
      this.メニューバーMToolStripMenuItem.CheckOnClick = true;
      this.メニューバーMToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.メニューバーMToolStripMenuItem.Name = "メニューバーMToolStripMenuItem";
      this.メニューバーMToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
      this.メニューバーMToolStripMenuItem.Text = "メニューバー(&M)";
      this.メニューバーMToolStripMenuItem.Click += new System.EventHandler(this.メニューバーMToolStripMenuItem_Click);
      // 
      // ステータスバーSToolStripMenuItem1
      // 
      this.ステータスバーSToolStripMenuItem1.Checked = true;
      this.ステータスバーSToolStripMenuItem1.CheckOnClick = true;
      this.ステータスバーSToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ステータスバーSToolStripMenuItem1.Name = "ステータスバーSToolStripMenuItem1";
      this.ステータスバーSToolStripMenuItem1.Size = new System.Drawing.Size(188, 26);
      this.ステータスバーSToolStripMenuItem1.Text = "ステータスバー(&S)";
      this.ステータスバーSToolStripMenuItem1.Click += new System.EventHandler(this.ステータスバーSToolStripMenuItem1_Click);
      // 
      // fdImageButton
      // 
      this.fdImageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fdImageButton.Image = ((System.Drawing.Image)(resources.GetObject("fdImageButton.Image")));
      this.fdImageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fdImageButton.Name = "fdImageButton";
      this.fdImageButton.Size = new System.Drawing.Size(24, 24);
      this.fdImageButton.Text = "fdImageButton";
      this.fdImageButton.Visible = false;
      // 
      // fdImage32Button
      // 
      this.fdImage32Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.fdImage32Button.Image = ((System.Drawing.Image)(resources.GetObject("fdImage32Button.Image")));
      this.fdImage32Button.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.fdImage32Button.Name = "fdImage32Button";
      this.fdImage32Button.Size = new System.Drawing.Size(24, 24);
      this.fdImage32Button.Text = "fdImage32Button";
      this.fdImage32Button.Visible = false;
      // 
      // mainForm_toggleButton
      // 
      this.mainForm_toggleButton.Checked = true;
      this.mainForm_toggleButton.CheckOnClick = true;
      this.mainForm_toggleButton.CheckState = System.Windows.Forms.CheckState.Checked;
      this.mainForm_toggleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.mainForm_toggleButton.Image = ((System.Drawing.Image)(resources.GetObject("mainForm_toggleButton.Image")));
      this.mainForm_toggleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mainForm_toggleButton.Name = "mainForm_toggleButton";
      this.mainForm_toggleButton.Size = new System.Drawing.Size(24, 24);
      this.mainForm_toggleButton.Text = "toolStripButton1";
      this.mainForm_toggleButton.Click += new System.EventHandler(this.mainForm_toggle_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.statusStrip1.Location = new System.Drawing.Point(0, 589);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(1193, 22);
      this.statusStrip1.TabIndex = 9;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // tabPage4
      // 
      this.tabPage4.Location = new System.Drawing.Point(36, 4);
      this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Size = new System.Drawing.Size(1153, 521);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Link";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // tabPage3
      // 
      this.tabPage3.Location = new System.Drawing.Point(36, 4);
      this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new System.Drawing.Size(1153, 521);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "FTP";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(36, 4);
      this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage2.Size = new System.Drawing.Size(1153, 521);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Dir";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.toolStrip);
      this.tabPage1.Controls.Add(this.splitContainer1);
      this.tabPage1.Location = new System.Drawing.Point(36, 4);
      this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPage1.Size = new System.Drawing.Size(1153, 521);
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
            this.syncronizeDodument,
            this.syncronizeButton,
            this.toolStripButton8,
            this.homeStripButton,
            this.removeButton});
      this.toolStrip.Location = new System.Drawing.Point(4, 5);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
      this.toolStrip.Size = new System.Drawing.Size(1145, 32);
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
      this.addButton.Click += new System.EventHandler(this.toolStripButton1_Click);
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
      this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click_1);
      // 
      // runButton
      // 
      this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
      this.runButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.runButton.Name = "runButton";
      this.runButton.Size = new System.Drawing.Size(74, 29);
      this.runButton.Text = "Run";
      this.runButton.Click += new System.EventHandler(this.runButton_Click_1);
      // 
      // toolStripDropDownButton2
      // 
      this.toolStripDropDownButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.toolStripMenuItem7,
            this.toolStripMenuItem11,
            this.toolStripMenuItem21,
            this.toolStripMenuItem31,
            this.toolStripMenuItem32});
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
      // toolStripMenuItem11
      // 
      this.toolStripMenuItem11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem12,
            this.toolStripMenuItem13,
            this.toolStripMenuItem14,
            this.toolStripMenuItem15,
            this.toolStripMenuItem16,
            this.toolStripSeparator2,
            this.toolStripMenuItem20});
      this.toolStripMenuItem11.Name = "toolStripMenuItem11";
      this.toolStripMenuItem11.Size = new System.Drawing.Size(295, 30);
      this.toolStripMenuItem11.Text = "オプション";
      // 
      // toolStripMenuItem12
      // 
      this.toolStripMenuItem12.Checked = true;
      this.toolStripMenuItem12.CheckOnClick = true;
      this.toolStripMenuItem12.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripMenuItem12.Name = "toolStripMenuItem12";
      this.toolStripMenuItem12.Size = new System.Drawing.Size(242, 30);
      this.toolStripMenuItem12.Text = "内部ターゲット表示";
      // 
      // toolStripMenuItem13
      // 
      this.toolStripMenuItem13.CheckOnClick = true;
      this.toolStripMenuItem13.Name = "toolStripMenuItem13";
      this.toolStripMenuItem13.Size = new System.Drawing.Size(242, 30);
      this.toolStripMenuItem13.Text = "プロパティ表示";
      // 
      // toolStripMenuItem14
      // 
      this.toolStripMenuItem14.Checked = true;
      this.toolStripMenuItem14.CheckOnClick = true;
      this.toolStripMenuItem14.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripMenuItem14.Name = "toolStripMenuItem14";
      this.toolStripMenuItem14.Size = new System.Drawing.Size(242, 30);
      this.toolStripMenuItem14.Text = "子ノード表示";
      // 
      // toolStripMenuItem15
      // 
      this.toolStripMenuItem15.Checked = true;
      this.toolStripMenuItem15.CheckOnClick = true;
      this.toolStripMenuItem15.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripMenuItem15.Name = "toolStripMenuItem15";
      this.toolStripMenuItem15.Size = new System.Drawing.Size(242, 30);
      this.toolStripMenuItem15.Text = "アイコン表示";
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
      // toolStripButton8
      // 
      this.toolStripButton8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
      this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.toolStripButton8.Name = "toolStripButton8";
      this.toolStripButton8.Size = new System.Drawing.Size(24, 29);
      this.toolStripButton8.Text = "Property表示切替";
      this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
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
      this.splitContainer1.Size = new System.Drawing.Size(1145, 511);
      this.splitContainer1.SplitterDistance = 312;
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
      this.treeView.Size = new System.Drawing.Size(1145, 312);
      this.treeView.TabIndex = 1;
      this.treeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeCollapse);
      this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
      this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
      this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
      this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
      this.propertyGrid1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(1145, 192);
      this.propertyGrid1.TabIndex = 1;
      // 
      // miniToolStrip
      // 
      this.miniToolStrip.AccessibleName = "新しい項目の選択";
      this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
      this.miniToolStrip.AutoSize = false;
      this.miniToolStrip.CanOverflow = false;
      this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
      this.miniToolStrip.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.miniToolStrip.Location = new System.Drawing.Point(172, 4);
      this.miniToolStrip.Name = "miniToolStrip";
      this.miniToolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
      this.miniToolStrip.Size = new System.Drawing.Size(1177, 32);
      this.miniToolStrip.TabIndex = 4;
      // 
      // tabControl1
      // 
      this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Controls.Add(this.tabPage5);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 60);
      this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabControl1.Multiline = true;
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(1193, 529);
      this.tabControl1.TabIndex = 6;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
      // 
      // tabPage5
      // 
      this.tabPage5.Controls.Add(this.propertyGrid3);
      this.tabPage5.Location = new System.Drawing.Point(36, 4);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Size = new System.Drawing.Size(1153, 521);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "Settings";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // propertyGrid3
      // 
      this.propertyGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertyGrid3.Location = new System.Drawing.Point(0, 0);
      this.propertyGrid3.Name = "propertyGrid3";
      this.propertyGrid3.Size = new System.Drawing.Size(1153, 521);
      this.propertyGrid3.TabIndex = 0;
      this.propertyGrid3.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid3_PropertyValueChanged);
      // 
      // AntPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.menuStrip1);
      this.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "AntPanel";
      this.Size = new System.Drawing.Size(1193, 611);
      this.Load += new System.EventHandler(this.AntPanel_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage5.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
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
        this.AddBuildFiles(this.dropFiles);
      }
    }

    private MenuStrip menuStrip1;
    private ToolStripMenuItem ファイルFToolStripMenuItem;
    private ToolStripMenuItem 新規作成NToolStripMenuItem;
    private ToolStripMenuItem 開くOToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripMenuItem 上書き保存SToolStripMenuItem;
    private ToolStripMenuItem 名前を付けて保存AToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem 印刷PToolStripMenuItem;
    private ToolStripMenuItem 印刷プレビューVToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripMenuItem 終了XToolStripMenuItem;
    private ToolStripMenuItem 編集EToolStripMenuItem;
    private ToolStripMenuItem 元に戻すUToolStripMenuItem;
    private ToolStripMenuItem やり直しRToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripMenuItem 切り取りTToolStripMenuItem;
    private ToolStripMenuItem コピーCToolStripMenuItem;
    private ToolStripMenuItem 貼り付けPToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripMenuItem すべて選択AToolStripMenuItem;
    private ToolStripMenuItem ツールTToolStripMenuItem;
    private ToolStripMenuItem カスタマイズCToolStripMenuItem;
    private ToolStripMenuItem オプションOToolStripMenuItem;
    private ToolStripMenuItem ヘルプHToolStripMenuItem;
    private ToolStripMenuItem 内容CToolStripMenuItem;
    private ToolStripMenuItem インデックスIToolStripMenuItem;
    private ToolStripMenuItem 検索SToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripMenuItem バージョン情報AToolStripMenuItem;
    private ToolStrip toolStrip1;
    private ToolStripButton 新規作成NToolStripButton;
    private ToolStripButton 開くOToolStripButton;
    private ToolStripButton 上書き保存SToolStripButton;
    private ToolStripButton 印刷PToolStripButton;
    private ToolStripSeparator toolStripSeparator10;
    private ToolStripButton 切り取りUToolStripButton;
    private ToolStripButton コピーCToolStripButton;
    private ToolStripButton 貼り付けPToolStripButton;
    private ToolStripSeparator toolStripSeparator11;
    private ToolStripButton ヘルプLToolStripButton;
    private StatusStrip statusStrip1;
    public  static RichTextBox richTextBox1;
    public  static PictureBox pictureBox1;
    public static WebBrowser webBrowser1;
    public  static  AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    private ToolStripMenuItem 試験ToolStripMenuItem;
    public ToolStripMenuItem 表示VToolStripMenuItem1;
    public ToolStripMenuItem ツールバーTToolStripMenuItem;
    public ToolStripMenuItem ステータスバーSToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator12;
    public ToolStripMenuItem tabPageModeToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator13;
    public ToolStripMenuItem richTextEditorToolStripMenuItem;
    public ToolStripMenuItem statusTextToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator14;
    public ToolStripMenuItem dockingTreeViewToolStripMenuItem;
    public ToolStripMenuItem スクリプトCToolStripMenuItem;
    public ToolStripMenuItem スクリプトを実行XToolStripMenuItem;
    public ToolStripMenuItem スクリプトを編集EToolStripMenuItem;
    public ToolStripMenuItem スクリプトメニュー更新RToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator28;
    public ToolStripDropDownButton toolStripDropDownButton1;
    public ToolStripMenuItem メニューバーMToolStripMenuItem;
    public ToolStripMenuItem ステータスバーSToolStripMenuItem1;
    private ToolStripButton fdImageButton;
    private ToolStripButton fdImage32Button;
    private ToolStripButton mainForm_toggleButton;
    private TabPage tabPage4;
    private TabPage tabPage3;
    private TabPage tabPage2;
    private TabPage tabPage1;
    public ToolStrip toolStrip;
    public ToolStripButton addButton;
    public ToolStripButton refreshButton;
    public ToolStripButton runButton;
    private ToolStripDropDownButton toolStripDropDownButton2;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem toolStripMenuItem4;
    private ToolStripMenuItem toolStripMenuItem5;
    private ToolStripMenuItem toolStripMenuItem6;
    private ToolStripMenuItem toolStripMenuItem7;
    public ToolStripMenuItem toolStripMenuItem8;
    public ToolStripMenuItem toolStripMenuItem9;
    public ToolStripMenuItem toolStripMenuItem10;
    private ToolStripMenuItem toolStripMenuItem11;
    private ToolStripMenuItem toolStripMenuItem12;
    private ToolStripMenuItem toolStripMenuItem13;
    private ToolStripMenuItem toolStripMenuItem14;
    private ToolStripMenuItem toolStripMenuItem15;
    private ToolStripMenuItem toolStripMenuItem16;
    private ToolStripMenuItem toolStripMenuItem17;
    private ToolStripMenuItem toolStripMenuItem18;
    private ToolStripMenuItem toolStripMenuItem19;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem toolStripMenuItem20;
    private ToolStripMenuItem toolStripMenuItem21;
    private ToolStripMenuItem サクラエディタToolStripMenuItem;
    private ToolStripMenuItem pSPadToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem richTextBoxToolStripMenuItem;
    private ToolStripMenuItem scintillaCToolStripMenuItem;
    public ToolStripMenuItem azukiEditorZToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem エクスプローラToolStripMenuItem;
    private ToolStripMenuItem コマンドプロンプトToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem toolStripMenuItem29;
    private ToolStripMenuItem toolStripMenuItem30;
    private ToolStripMenuItem toolStripMenuItem31;
    private ToolStripMenuItem toolStripMenuItem32;
    public ToolStripMenuItem toolStripMenuItem33;
    private ToolStripButton imageListStripButton;
    private ToolStripButton csOutlineButton1;
    private ToolStripButton syncronizeDodument;
    private ToolStripButton syncronizeButton;
    private ToolStripButton toolStripButton8;
    private ToolStripButton homeStripButton;
    private ToolStripButton removeButton;
    private SplitContainer splitContainer1;
    public TreeView treeView;
    public PropertyGrid propertyGrid1;
    private ToolStrip miniToolStrip;
    private TabControl tabControl1;
    private TabPage tabPage5;
    public PropertyGrid propertyGrid3;
  }
}
