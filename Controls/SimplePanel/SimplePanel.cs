using AntPlugin.CommonLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AntPlugin;
using AntPanelApplication;
using CommonLibrary;
//using XMLTreeMenu.Properties;
//using XMLTreeMenu;

namespace AntPlugin.XMLTreeMenu.Controls
{
	public class SimplePanel : UserControl
  {
    #region Variables




    global::AntPanelApplication.Properties.Settings
      settings = new global::AntPanelApplication.Properties.Settings();

    public string command = string.Empty;
    public string args = string.Empty;
    public string path = string.Empty;
    public string option = string.Empty;
    public string argstring = string.Empty;
    //public PluginUI pluginUI;
    public AntPanel antPanel;
 

    private List<Process> processList = new List<Process>();
    #endregion

    #region Form Variables
    private IContainer components;

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
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem 終了XToolStripMenuItem;
    private ToolStripMenuItem 編集EToolStripMenuItem;
    private ToolStripMenuItem 元に戻すUToolStripMenuItem;
    private ToolStripMenuItem やり直しRToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem 切り取りTToolStripMenuItem;
    private ToolStripMenuItem コピーCToolStripMenuItem;
    private ToolStripMenuItem 貼り付けPToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem すべて選択AToolStripMenuItem;
    private ToolStripMenuItem ツールTToolStripMenuItem;
    private ToolStripMenuItem カスタマイズCToolStripMenuItem;
    private ToolStripMenuItem オプションOToolStripMenuItem;
    private ToolStripMenuItem ヘルプHToolStripMenuItem;
    private ToolStripMenuItem 内容CToolStripMenuItem;
    private ToolStripMenuItem インデックスIToolStripMenuItem;
    private ToolStripMenuItem 検索SToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem バージョン情報AToolStripMenuItem;
    private ToolStrip toolStrip1;
    private ToolStripButton 新規作成NToolStripButton;
    private ToolStripButton 開くOToolStripButton;
    private ToolStripButton 上書き保存SToolStripButton;
    private ToolStripButton 印刷PToolStripButton;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripButton 切り取りUToolStripButton;
    private ToolStripButton コピーCToolStripButton;
    private ToolStripButton 貼り付けPToolStripButton;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripButton ヘルプLToolStripButton;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
    public Panel panel1;
    private ToolStripMenuItem 表示VToolStripMenuItem;
    private ToolStripMenuItem プロセスPToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripMenuItem graph371ToolStripMenuItem;
    private ToolStripMenuItem lesson5ToolStripMenuItem;
    private ToolStripMenuItem 試験ToolStripMenuItem1;
    private OpenFileDialog openFileDialog1;
    private ToolStripMenuItem 全プロセスの停止ToolStripMenuItem;
    private ToolStripMenuItem ツールバーTToolStripMenuItem;
    private ToolStripMenuItem ステータスバーSToolStripMenuItem;
    public ToolStripMenuItem スクリプトCToolStripMenuItem;
    private ToolStripMenuItem スクリプトを編集EToolStripMenuItem;
    private ToolStripMenuItem スクリプトを実行XToolStripMenuItem;
    private ToolStripMenuItem スクリプトメニュー更新RToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator28;
    private ToolStripMenuItem お気に入りToolStripMenuItem;
    private ToolStripMenuItem お気に入りに追加ToolStripMenuItem;
    private ToolStripDropDownButton toolStripDropDownButton1;
    private ToolStripMenuItem メニューバーMToolStripMenuItem;
    private ToolStripMenuItem ステータスバーSToolStripMenuItem1;
    private ToolStripSeparator toolStripSeparator9;
    private ImageList imageList1;
    private ToolStripDropDownButton toolStripDropDownButton4;
    private ToolStripDropDownButton toolStripDropDownButton2;
    private ToolStripDropDownButton toolStripDropDownButton3;
    private ToolStripMenuItem サクラエディタSToolStripMenuItem;
    private ToolStripMenuItem pSPadPToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator16;
    private ToolStripMenuItem scintillaCToolStripMenuItem;
    private ToolStripMenuItem azukiEditorZToolStripMenuItem;
    private ToolStripMenuItem richTextBoxToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator17;
    private ToolStripMenuItem エクスプローラEToolStripMenuItem;
    private ToolStripMenuItem コマンドプロンプトCToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator18;
    private ToolStripMenuItem 現在のファイルをブラウザで開くWToolStripMenuItem;
    private ToolStripMenuItem microsoftWordで開くWToolStripMenuItem;
    private ToolStripMenuItem ファイル名を指定して実行OToolStripMenuItem;
    private ToolStripMenuItem リンクを開くLToolStripMenuItem;
    private ToolStripMenuItem デスクトップに移動ToolStripMenuItem;
    private ToolStripMenuItem 最近開いたファイルRToolStripMenuItem;
    private ToolStripMenuItem 最近開いたファイルをクリアCToolStripMenuItem;
    //private XMLTreeMenu.PluginMain xmlTreeMenu;
    //private XMLTreeMenu.Settings settings;
    private List<string> previousDocuments = new List<string>();
    private ToolStripMenuItem アプリケーションを起動PToolStripMenuItem;
    private ToolStripButton imageListButton;
    private ToolStripSeparator toolStripSeparator10;
    #endregion

    #region Properties
    public List<string> PreviousDocuments
    {
      get
      {
        return this.previousDocuments;
      }
      set
      {
        this.previousDocuments = value;
      }
    }
    #endregion

    protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimplePanel));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.新規作成NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.最近開いたファイルRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.最近開いたファイルをクリアCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.アプリケーションを起動PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.上書き保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.名前を付けて保存AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.印刷PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.印刷プレビューVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.元に戻すUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.やり直しRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.切り取りTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.コピーCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.貼り付けPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.すべて選択AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.表示VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ツールバーTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ステータスバーSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ツールTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.カスタマイズCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.オプションOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.デスクトップに移動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.graph371ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.lesson5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.試験ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.プロセスPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.全プロセスの停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトを編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトを実行XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトメニュー更新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
      this.お気に入りToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.お気に入りに追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.内容CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.インデックスIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.検索SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.バージョン情報AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.新規作成NToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.開くOToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.上書き保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.印刷PToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.切り取りUToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.コピーCToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.貼り付けPToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.ヘルプLToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.メニューバーMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ステータスバーSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
      this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
      this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
      this.サクラエディタSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pSPadPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
      this.scintillaCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.azukiEditorZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.richTextBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
      this.エクスプローラEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.コマンドプロンプトCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
      this.現在のファイルをブラウザで開くWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.microsoftWordで開くWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ファイル名を指定して実行OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.リンクを開くLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.imageListButton = new System.Windows.Forms.ToolStripButton();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.表示VToolStripMenuItem,
            this.ツールTToolStripMenuItem,
            this.プロセスPToolStripMenuItem,
            this.スクリプトCToolStripMenuItem,
            this.お気に入りToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(604, 27);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // ファイルFToolStripMenuItem
      // 
      this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成NToolStripMenuItem,
            this.開くOToolStripMenuItem,
            this.toolStripSeparator,
            this.最近開いたファイルRToolStripMenuItem,
            this.アプリケーションを起動PToolStripMenuItem,
            this.toolStripSeparator10,
            this.上書き保存SToolStripMenuItem,
            this.名前を付けて保存AToolStripMenuItem,
            this.toolStripSeparator1,
            this.印刷PToolStripMenuItem,
            this.印刷プレビューVToolStripMenuItem,
            this.toolStripSeparator2,
            this.終了XToolStripMenuItem});
      this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
      this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(86, 23);
      this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
      // 
      // 新規作成NToolStripMenuItem
      // 
      this.新規作成NToolStripMenuItem.Enabled = false;
      this.新規作成NToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新規作成NToolStripMenuItem.Image")));
      this.新規作成NToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.新規作成NToolStripMenuItem.Name = "新規作成NToolStripMenuItem";
      this.新規作成NToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
      this.新規作成NToolStripMenuItem.Text = "新規作成(&N)";
      // 
      // 開くOToolStripMenuItem
      // 
      this.開くOToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripMenuItem.Image")));
      this.開くOToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
      this.開くOToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
      this.開くOToolStripMenuItem.Text = "開く(&O)";
      this.開くOToolStripMenuItem.Click += new System.EventHandler(this.開くOToolStripMenuItem_Click);
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(231, 6);
      // 
      // 最近開いたファイルRToolStripMenuItem
      // 
      this.最近開いたファイルRToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最近開いたファイルをクリアCToolStripMenuItem});
      this.最近開いたファイルRToolStripMenuItem.Name = "最近開いたファイルRToolStripMenuItem";
      this.最近開いたファイルRToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
      this.最近開いたファイルRToolStripMenuItem.Text = "最近開いたファイル(&R)";
      // 
      // 最近開いたファイルをクリアCToolStripMenuItem
      // 
      this.最近開いたファイルをクリアCToolStripMenuItem.Name = "最近開いたファイルをクリアCToolStripMenuItem";
      this.最近開いたファイルをクリアCToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.最近開いたファイルをクリアCToolStripMenuItem.Text = "最近開いたファイルをクリア(&C)";
      this.最近開いたファイルをクリアCToolStripMenuItem.Click += new System.EventHandler(this.最近開いたファイルをクリアCToolStripMenuItem_Click);
      // 
      // アプリケーションを起動PToolStripMenuItem
      // 
      this.アプリケーションを起動PToolStripMenuItem.Name = "アプリケーションを起動PToolStripMenuItem";
      this.アプリケーションを起動PToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
      this.アプリケーションを起動PToolStripMenuItem.Text = "アプリケーションを起動(&P)";
      this.アプリケーションを起動PToolStripMenuItem.Click += new System.EventHandler(this.アプリケーションを起動PToolStripMenuItem_Click);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(231, 6);
      // 
      // 上書き保存SToolStripMenuItem
      // 
      this.上書き保存SToolStripMenuItem.Enabled = false;
      this.上書き保存SToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("上書き保存SToolStripMenuItem.Image")));
      this.上書き保存SToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.上書き保存SToolStripMenuItem.Name = "上書き保存SToolStripMenuItem";
      this.上書き保存SToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
      this.上書き保存SToolStripMenuItem.Text = "上書き保存(&S)";
      // 
      // 名前を付けて保存AToolStripMenuItem
      // 
      this.名前を付けて保存AToolStripMenuItem.Enabled = false;
      this.名前を付けて保存AToolStripMenuItem.Name = "名前を付けて保存AToolStripMenuItem";
      this.名前を付けて保存AToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
      this.名前を付けて保存AToolStripMenuItem.Text = "名前を付けて保存(&A)";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(231, 6);
      // 
      // 印刷PToolStripMenuItem
      // 
      this.印刷PToolStripMenuItem.Enabled = false;
      this.印刷PToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripMenuItem.Image")));
      this.印刷PToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷PToolStripMenuItem.Name = "印刷PToolStripMenuItem";
      this.印刷PToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
      this.印刷PToolStripMenuItem.Text = "印刷(&P)";
      // 
      // 印刷プレビューVToolStripMenuItem
      // 
      this.印刷プレビューVToolStripMenuItem.Enabled = false;
      this.印刷プレビューVToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("印刷プレビューVToolStripMenuItem.Image")));
      this.印刷プレビューVToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷プレビューVToolStripMenuItem.Name = "印刷プレビューVToolStripMenuItem";
      this.印刷プレビューVToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
      this.印刷プレビューVToolStripMenuItem.Text = "印刷プレビュー(&V)";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(231, 6);
      // 
      // 終了XToolStripMenuItem
      // 
      this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
      this.終了XToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
      this.終了XToolStripMenuItem.Text = "終了(&X)";
      this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
      // 
      // 編集EToolStripMenuItem
      // 
      this.編集EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.元に戻すUToolStripMenuItem,
            this.やり直しRToolStripMenuItem,
            this.toolStripSeparator3,
            this.切り取りTToolStripMenuItem,
            this.コピーCToolStripMenuItem,
            this.貼り付けPToolStripMenuItem,
            this.toolStripSeparator4,
            this.すべて選択AToolStripMenuItem});
      this.編集EToolStripMenuItem.Enabled = false;
      this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
      this.編集EToolStripMenuItem.Size = new System.Drawing.Size(74, 23);
      this.編集EToolStripMenuItem.Text = "編集(&E)";
      // 
      // 元に戻すUToolStripMenuItem
      // 
      this.元に戻すUToolStripMenuItem.Name = "元に戻すUToolStripMenuItem";
      this.元に戻すUToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.元に戻すUToolStripMenuItem.Text = "元に戻す(&U)";
      // 
      // やり直しRToolStripMenuItem
      // 
      this.やり直しRToolStripMenuItem.Name = "やり直しRToolStripMenuItem";
      this.やり直しRToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.やり直しRToolStripMenuItem.Text = "やり直し(&R)";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(172, 6);
      // 
      // 切り取りTToolStripMenuItem
      // 
      this.切り取りTToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("切り取りTToolStripMenuItem.Image")));
      this.切り取りTToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.切り取りTToolStripMenuItem.Name = "切り取りTToolStripMenuItem";
      this.切り取りTToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.切り取りTToolStripMenuItem.Text = "切り取り(&T)";
      // 
      // コピーCToolStripMenuItem
      // 
      this.コピーCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripMenuItem.Image")));
      this.コピーCToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.コピーCToolStripMenuItem.Name = "コピーCToolStripMenuItem";
      this.コピーCToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.コピーCToolStripMenuItem.Text = "コピー(&C)";
      // 
      // 貼り付けPToolStripMenuItem
      // 
      this.貼り付けPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripMenuItem.Image")));
      this.貼り付けPToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.貼り付けPToolStripMenuItem.Name = "貼り付けPToolStripMenuItem";
      this.貼り付けPToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.貼り付けPToolStripMenuItem.Text = "貼り付け(&P)";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(172, 6);
      // 
      // すべて選択AToolStripMenuItem
      // 
      this.すべて選択AToolStripMenuItem.Name = "すべて選択AToolStripMenuItem";
      this.すべて選択AToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.すべて選択AToolStripMenuItem.Text = "すべて選択(&A)";
      // 
      // 表示VToolStripMenuItem
      // 
      this.表示VToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ツールバーTToolStripMenuItem,
            this.ステータスバーSToolStripMenuItem});
      this.表示VToolStripMenuItem.Name = "表示VToolStripMenuItem";
      this.表示VToolStripMenuItem.Size = new System.Drawing.Size(75, 23);
      this.表示VToolStripMenuItem.Text = "表示(&V)";
      // 
      // ツールバーTToolStripMenuItem
      // 
      this.ツールバーTToolStripMenuItem.CheckOnClick = true;
      this.ツールバーTToolStripMenuItem.Name = "ツールバーTToolStripMenuItem";
      this.ツールバーTToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
      this.ツールバーTToolStripMenuItem.Text = "ツールバー(&T)";
      this.ツールバーTToolStripMenuItem.Click += new System.EventHandler(this.ツールバーTToolStripMenuItem_Click);
      // 
      // ステータスバーSToolStripMenuItem
      // 
      this.ステータスバーSToolStripMenuItem.CheckOnClick = true;
      this.ステータスバーSToolStripMenuItem.Name = "ステータスバーSToolStripMenuItem";
      this.ステータスバーSToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
      this.ステータスバーSToolStripMenuItem.Text = "ステータスバー(&S)";
      this.ステータスバーSToolStripMenuItem.Click += new System.EventHandler(this.ステータスバーSToolStripMenuItem_Click);
      // 
      // ツールTToolStripMenuItem
      // 
      this.ツールTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.カスタマイズCToolStripMenuItem,
            this.オプションOToolStripMenuItem,
            this.デスクトップに移動ToolStripMenuItem,
            this.toolStripSeparator8,
            this.graph371ToolStripMenuItem,
            this.lesson5ToolStripMenuItem,
            this.試験ToolStripMenuItem1});
      this.ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem";
      this.ツールTToolStripMenuItem.Size = new System.Drawing.Size(80, 23);
      this.ツールTToolStripMenuItem.Text = "ツール(&T)";
      // 
      // カスタマイズCToolStripMenuItem
      // 
      this.カスタマイズCToolStripMenuItem.Name = "カスタマイズCToolStripMenuItem";
      this.カスタマイズCToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
      this.カスタマイズCToolStripMenuItem.Text = "カスタマイズ(&C)";
      // 
      // オプションOToolStripMenuItem
      // 
      this.オプションOToolStripMenuItem.Name = "オプションOToolStripMenuItem";
      this.オプションOToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
      this.オプションOToolStripMenuItem.Text = "オプション(&O)";
      // 
      // デスクトップに移動ToolStripMenuItem
      // 
      this.デスクトップに移動ToolStripMenuItem.Name = "デスクトップに移動ToolStripMenuItem";
      this.デスクトップに移動ToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
      this.デスクトップに移動ToolStripMenuItem.Text = "デスクトップに移動";
      this.デスクトップに移動ToolStripMenuItem.Click += new System.EventHandler(this.デスクトップに移動ToolStripMenuItem_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(215, 6);
      // 
      // graph371ToolStripMenuItem
      // 
      this.graph371ToolStripMenuItem.Name = "graph371ToolStripMenuItem";
      this.graph371ToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
      this.graph371ToolStripMenuItem.Text = "graph371.exe";
      this.graph371ToolStripMenuItem.Click += new System.EventHandler(this.graph371ToolStripMenuItem_Click);
      // 
      // lesson5ToolStripMenuItem
      // 
      this.lesson5ToolStripMenuItem.Name = "lesson5ToolStripMenuItem";
      this.lesson5ToolStripMenuItem.Size = new System.Drawing.Size(218, 26);
      this.lesson5ToolStripMenuItem.Text = "NeHe Lesson5.exe";
      this.lesson5ToolStripMenuItem.Click += new System.EventHandler(this.lesson5ToolStripMenuItem_Click);
      // 
      // 試験ToolStripMenuItem1
      // 
      this.試験ToolStripMenuItem1.Name = "試験ToolStripMenuItem1";
      this.試験ToolStripMenuItem1.Size = new System.Drawing.Size(218, 26);
      this.試験ToolStripMenuItem1.Text = "試験";
      this.試験ToolStripMenuItem1.Click += new System.EventHandler(this.試験ToolStripMenuItem1_Click);
      // 
      // プロセスPToolStripMenuItem
      // 
      this.プロセスPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全プロセスの停止ToolStripMenuItem});
      this.プロセスPToolStripMenuItem.Name = "プロセスPToolStripMenuItem";
      this.プロセスPToolStripMenuItem.Size = new System.Drawing.Size(89, 23);
      this.プロセスPToolStripMenuItem.Text = "プロセス(&P)";
      this.プロセスPToolStripMenuItem.Click += new System.EventHandler(this.プロセスPToolStripMenuItem_Click);
      // 
      // 全プロセスの停止ToolStripMenuItem
      // 
      this.全プロセスの停止ToolStripMenuItem.Name = "全プロセスの停止ToolStripMenuItem";
      this.全プロセスの停止ToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
      this.全プロセスの停止ToolStripMenuItem.Text = "全プロセスの停止";
      this.全プロセスの停止ToolStripMenuItem.Click += new System.EventHandler(this.全プロセスの停止ToolStripMenuItem_Click);
      // 
      // スクリプトCToolStripMenuItem
      // 
      this.スクリプトCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.スクリプトを編集EToolStripMenuItem,
            this.スクリプトを実行XToolStripMenuItem,
            this.スクリプトメニュー更新RToolStripMenuItem,
            this.toolStripSeparator28});
      this.スクリプトCToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.スクリプトCToolStripMenuItem.Name = "スクリプトCToolStripMenuItem";
      this.スクリプトCToolStripMenuItem.Size = new System.Drawing.Size(97, 23);
      this.スクリプトCToolStripMenuItem.Text = "スクリプト(&C)";
      // 
      // スクリプトを編集EToolStripMenuItem
      // 
      this.スクリプトを編集EToolStripMenuItem.Name = "スクリプトを編集EToolStripMenuItem";
      this.スクリプトを編集EToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
      this.スクリプトを編集EToolStripMenuItem.Text = "スクリプトを編集(&E)";
      // 
      // スクリプトを実行XToolStripMenuItem
      // 
      this.スクリプトを実行XToolStripMenuItem.Enabled = false;
      this.スクリプトを実行XToolStripMenuItem.Name = "スクリプトを実行XToolStripMenuItem";
      this.スクリプトを実行XToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
      this.スクリプトを実行XToolStripMenuItem.Text = "スクリプトを実行(&X)";
      // 
      // スクリプトメニュー更新RToolStripMenuItem
      // 
      this.スクリプトメニュー更新RToolStripMenuItem.Name = "スクリプトメニュー更新RToolStripMenuItem";
      this.スクリプトメニュー更新RToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
      this.スクリプトメニュー更新RToolStripMenuItem.Text = "スクリプトメニュー更新(&R)";
      // 
      // toolStripSeparator28
      // 
      this.toolStripSeparator28.Name = "toolStripSeparator28";
      this.toolStripSeparator28.Size = new System.Drawing.Size(230, 6);
      // 
      // お気に入りToolStripMenuItem
      // 
      this.お気に入りToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.お気に入りに追加ToolStripMenuItem});
      this.お気に入りToolStripMenuItem.Name = "お気に入りToolStripMenuItem";
      this.お気に入りToolStripMenuItem.Size = new System.Drawing.Size(84, 23);
      this.お気に入りToolStripMenuItem.Text = "お気に入り";
      // 
      // お気に入りに追加ToolStripMenuItem
      // 
      this.お気に入りに追加ToolStripMenuItem.Name = "お気に入りに追加ToolStripMenuItem";
      this.お気に入りに追加ToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.お気に入りに追加ToolStripMenuItem.Text = "お気に入りに追加";
      // 
      // ヘルプHToolStripMenuItem
      // 
      this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.内容CToolStripMenuItem,
            this.インデックスIToolStripMenuItem,
            this.検索SToolStripMenuItem,
            this.toolStripSeparator5,
            this.バージョン情報AToolStripMenuItem});
      this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
      this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(81, 23);
      this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
      // 
      // 内容CToolStripMenuItem
      // 
      this.内容CToolStripMenuItem.Name = "内容CToolStripMenuItem";
      this.内容CToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
      this.内容CToolStripMenuItem.Text = "内容(&C)";
      // 
      // インデックスIToolStripMenuItem
      // 
      this.インデックスIToolStripMenuItem.Name = "インデックスIToolStripMenuItem";
      this.インデックスIToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
      this.インデックスIToolStripMenuItem.Text = "インデックス(&I)";
      // 
      // 検索SToolStripMenuItem
      // 
      this.検索SToolStripMenuItem.Name = "検索SToolStripMenuItem";
      this.検索SToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
      this.検索SToolStripMenuItem.Text = "検索(&S)";
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(206, 6);
      // 
      // バージョン情報AToolStripMenuItem
      // 
      this.バージョン情報AToolStripMenuItem.Name = "バージョン情報AToolStripMenuItem";
      this.バージョン情報AToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
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
            this.toolStripSeparator6,
            this.切り取りUToolStripButton,
            this.コピーCToolStripButton,
            this.貼り付けPToolStripButton,
            this.toolStripSeparator7,
            this.ヘルプLToolStripButton,
            this.toolStripSeparator9,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton4,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton3,
            this.imageListButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 27);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(604, 27);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // 新規作成NToolStripButton
      // 
      this.新規作成NToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.新規作成NToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("新規作成NToolStripButton.Image")));
      this.新規作成NToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.新規作成NToolStripButton.Name = "新規作成NToolStripButton";
      this.新規作成NToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.新規作成NToolStripButton.Text = "新規作成(&N)";
      // 
      // 開くOToolStripButton
      // 
      this.開くOToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.開くOToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripButton.Image")));
      this.開くOToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.開くOToolStripButton.Name = "開くOToolStripButton";
      this.開くOToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.開くOToolStripButton.Text = "開く(&O)";
      // 
      // 上書き保存SToolStripButton
      // 
      this.上書き保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.上書き保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("上書き保存SToolStripButton.Image")));
      this.上書き保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.上書き保存SToolStripButton.Name = "上書き保存SToolStripButton";
      this.上書き保存SToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.上書き保存SToolStripButton.Text = "上書き保存(&S)";
      // 
      // 印刷PToolStripButton
      // 
      this.印刷PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.印刷PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripButton.Image")));
      this.印刷PToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷PToolStripButton.Name = "印刷PToolStripButton";
      this.印刷PToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.印刷PToolStripButton.Text = "印刷(&P)";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
      // 
      // 切り取りUToolStripButton
      // 
      this.切り取りUToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.切り取りUToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("切り取りUToolStripButton.Image")));
      this.切り取りUToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.切り取りUToolStripButton.Name = "切り取りUToolStripButton";
      this.切り取りUToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.切り取りUToolStripButton.Text = "切り取り(&U)";
      // 
      // コピーCToolStripButton
      // 
      this.コピーCToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.コピーCToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripButton.Image")));
      this.コピーCToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.コピーCToolStripButton.Name = "コピーCToolStripButton";
      this.コピーCToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.コピーCToolStripButton.Text = "コピー(&C)";
      // 
      // 貼り付けPToolStripButton
      // 
      this.貼り付けPToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.貼り付けPToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripButton.Image")));
      this.貼り付けPToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.貼り付けPToolStripButton.Name = "貼り付けPToolStripButton";
      this.貼り付けPToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.貼り付けPToolStripButton.Text = "貼り付け(&P)";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
      // 
      // ヘルプLToolStripButton
      // 
      this.ヘルプLToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ヘルプLToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ヘルプLToolStripButton.Image")));
      this.ヘルプLToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.ヘルプLToolStripButton.Name = "ヘルプLToolStripButton";
      this.ヘルプLToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.ヘルプLToolStripButton.Text = "ヘルプ(&L)";
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(6, 27);
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.メニューバーMToolStripMenuItem,
            this.ステータスバーSToolStripMenuItem1});
      this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Black;
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
      this.ステータスバーSToolStripMenuItem1.CheckOnClick = true;
      this.ステータスバーSToolStripMenuItem1.Name = "ステータスバーSToolStripMenuItem1";
      this.ステータスバーSToolStripMenuItem1.Size = new System.Drawing.Size(188, 26);
      this.ステータスバーSToolStripMenuItem1.Text = "ステータスバー(&S)";
      this.ステータスバーSToolStripMenuItem1.Click += new System.EventHandler(this.ステータスバーSToolStripMenuItem1_Click);
      // 
      // toolStripDropDownButton4
      // 
      this.toolStripDropDownButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton4.Image")));
      this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Black;
      this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
      this.toolStripDropDownButton4.Size = new System.Drawing.Size(34, 24);
      this.toolStripDropDownButton4.Text = "toolStripDropDownButton4";
      // 
      // toolStripDropDownButton2
      // 
      this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
      this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Black;
      this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
      this.toolStripDropDownButton2.Size = new System.Drawing.Size(34, 24);
      this.toolStripDropDownButton2.Text = "toolStripDropDownButton2";
      // 
      // toolStripDropDownButton3
      // 
      this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.サクラエディタSToolStripMenuItem,
            this.pSPadPToolStripMenuItem,
            this.toolStripSeparator16,
            this.scintillaCToolStripMenuItem,
            this.azukiEditorZToolStripMenuItem,
            this.richTextBoxToolStripMenuItem,
            this.toolStripSeparator17,
            this.エクスプローラEToolStripMenuItem,
            this.コマンドプロンプトCToolStripMenuItem,
            this.toolStripSeparator18,
            this.現在のファイルをブラウザで開くWToolStripMenuItem,
            this.microsoftWordで開くWToolStripMenuItem,
            this.ファイル名を指定して実行OToolStripMenuItem,
            this.リンクを開くLToolStripMenuItem});
      this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
      this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Black;
      this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
      this.toolStripDropDownButton3.Size = new System.Drawing.Size(34, 24);
      this.toolStripDropDownButton3.Text = "&R";
      this.toolStripDropDownButton3.ToolTipText = "現在開いてるファイルを外部プログラムで開きます";
      // 
      // サクラエディタSToolStripMenuItem
      // 
      this.サクラエディタSToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("サクラエディタSToolStripMenuItem.Image")));
      this.サクラエディタSToolStripMenuItem.Name = "サクラエディタSToolStripMenuItem";
      this.サクラエディタSToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.サクラエディタSToolStripMenuItem.Text = "サクラエディタ(&S)";
      // 
      // pSPadPToolStripMenuItem
      // 
      this.pSPadPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pSPadPToolStripMenuItem.Image")));
      this.pSPadPToolStripMenuItem.Name = "pSPadPToolStripMenuItem";
      this.pSPadPToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.pSPadPToolStripMenuItem.Text = "PSPad(&P)";
      // 
      // toolStripSeparator16
      // 
      this.toolStripSeparator16.Name = "toolStripSeparator16";
      this.toolStripSeparator16.Size = new System.Drawing.Size(284, 6);
      // 
      // scintillaCToolStripMenuItem
      // 
      this.scintillaCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("scintillaCToolStripMenuItem.Image")));
      this.scintillaCToolStripMenuItem.Name = "scintillaCToolStripMenuItem";
      this.scintillaCToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.scintillaCToolStripMenuItem.Text = "Scintilla(&A)";
      // 
      // azukiEditorZToolStripMenuItem
      // 
      this.azukiEditorZToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("azukiEditorZToolStripMenuItem.Image")));
      this.azukiEditorZToolStripMenuItem.Name = "azukiEditorZToolStripMenuItem";
      this.azukiEditorZToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.azukiEditorZToolStripMenuItem.Text = "Azuki Editor(&Z)";
      // 
      // richTextBoxToolStripMenuItem
      // 
      this.richTextBoxToolStripMenuItem.Enabled = false;
      this.richTextBoxToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("richTextBoxToolStripMenuItem.Image")));
      this.richTextBoxToolStripMenuItem.Name = "richTextBoxToolStripMenuItem";
      this.richTextBoxToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.richTextBoxToolStripMenuItem.Text = "RichTextEditor(&R)";
      // 
      // toolStripSeparator17
      // 
      this.toolStripSeparator17.Name = "toolStripSeparator17";
      this.toolStripSeparator17.Size = new System.Drawing.Size(284, 6);
      // 
      // エクスプローラEToolStripMenuItem
      // 
      this.エクスプローラEToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("エクスプローラEToolStripMenuItem.Image")));
      this.エクスプローラEToolStripMenuItem.Name = "エクスプローラEToolStripMenuItem";
      this.エクスプローラEToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.エクスプローラEToolStripMenuItem.Text = "エクスプローラ(&E)";
      this.エクスプローラEToolStripMenuItem.Click += new System.EventHandler(this.エクスプローラEToolStripMenuItem_Click);
      // 
      // コマンドプロンプトCToolStripMenuItem
      // 
      this.コマンドプロンプトCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("コマンドプロンプトCToolStripMenuItem.Image")));
      this.コマンドプロンプトCToolStripMenuItem.Name = "コマンドプロンプトCToolStripMenuItem";
      this.コマンドプロンプトCToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.コマンドプロンプトCToolStripMenuItem.Text = "コマンド・プロンプト(&C)";
      this.コマンドプロンプトCToolStripMenuItem.Click += new System.EventHandler(this.コマンドプロンプトCToolStripMenuItem_Click);
      // 
      // toolStripSeparator18
      // 
      this.toolStripSeparator18.Name = "toolStripSeparator18";
      this.toolStripSeparator18.Size = new System.Drawing.Size(284, 6);
      // 
      // 現在のファイルをブラウザで開くWToolStripMenuItem
      // 
      this.現在のファイルをブラウザで開くWToolStripMenuItem.Name = "現在のファイルをブラウザで開くWToolStripMenuItem";
      this.現在のファイルをブラウザで開くWToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.現在のファイルをブラウザで開くWToolStripMenuItem.Text = "現在のファイルをブラウザで開く(&W)";
      // 
      // microsoftWordで開くWToolStripMenuItem
      // 
      this.microsoftWordで開くWToolStripMenuItem.Name = "microsoftWordで開くWToolStripMenuItem";
      this.microsoftWordで開くWToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.microsoftWordで開くWToolStripMenuItem.Text = "Microsoft Word で開く(&W)";
      // 
      // ファイル名を指定して実行OToolStripMenuItem
      // 
      this.ファイル名を指定して実行OToolStripMenuItem.Name = "ファイル名を指定して実行OToolStripMenuItem";
      this.ファイル名を指定して実行OToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.ファイル名を指定して実行OToolStripMenuItem.Text = "ファイル名を指定して実行(&O)";
      // 
      // リンクを開くLToolStripMenuItem
      // 
      this.リンクを開くLToolStripMenuItem.Enabled = false;
      this.リンクを開くLToolStripMenuItem.Name = "リンクを開くLToolStripMenuItem";
      this.リンクを開くLToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.リンクを開くLToolStripMenuItem.Text = "リンクを開く(&L)";
      // 
      // statusStrip1
      // 
      this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
      this.statusStrip1.Location = new System.Drawing.Point(0, 307);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(604, 24);
      this.statusStrip1.TabIndex = 2;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(167, 19);
      this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
      // 
      // panel1
      // 
      this.panel1.AllowDrop = true;
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 54);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(604, 253);
      this.panel1.Tag = @"F:\c_program\OpenGL\NeHe_1200x900\Lesson05\lesson5.exe";
      this.panel1.TabIndex = 3;
      this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
      this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // imageList1
      // 
      this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // imageListButton
      // 
      this.imageListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.imageListButton.Image = ((System.Drawing.Image)(resources.GetObject("imageListButton.Image")));
      this.imageListButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.imageListButton.Name = "imageListButton";
      this.imageListButton.Size = new System.Drawing.Size(24, 24);
      this.imageListButton.Text = "imageListButton";
      this.imageListButton.Visible = false;
      // 
      // SimplePanel
      // 
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.menuStrip1);
      this.Name = "SimplePanel";
      this.Size = new System.Drawing.Size(604, 331);
      this.Tag = this.panel1;
      this.Load += new System.EventHandler(this.SimplePanel_Load);
      this.Enter += new System.EventHandler(this.SimplePanel_Enter);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		public SimplePanel()
		{
			this.InitializeComponent();
			this.InitializeSimplePanel();
		}
    public SimplePanel(AntPanel ui)
    {
      this.antPanel = ui;
      this.InitializeComponent();
      this.InitializeSimplePanel();
    }

    private void InitializeSimplePanel()
		{
      this.imageList1.Images.Add((Bitmap)this.imageListButton.Image);
      this.imageList1.TransparentColor = Color.FromArgb(233, 229, 215);
			this.toolStrip1.Visible = false;
			this.statusStrip1.Visible = false;
		}

    private void SimplePanel_Load(object sender, EventArgs e)
		{

      if (this.panel1.Tag == null || String.IsNullOrEmpty((String)this.panel1.Tag)) return;
      //this.OpenFile(command + "|" + args + "|" + path + "|" + option);
      string command = string.Empty;
      string args = string.Empty;
			string path = string.Empty;
      string option = string.Empty;
			string argstring = string.Empty;
			try
			{
        if (!String.IsNullOrEmpty(this.AccessibleName))
        {
          argstring = this.AccessibleName;
          this.panel1.Tag = argstring;
        }
        else if (!String.IsNullOrEmpty(this.AccessibleDescription))
        {
          argstring = this.AccessibleDescription;
           this.panel1.Tag = argstring;
        }
        else if ((string)this.panel1.Tag != string.Empty)
        {
          argstring = this.panel1.Tag.ToString();
        }
        argstring = this.panel1.Tag.ToString();
				string[] array = argstring.Split('|');
				//command = this.antPanel.xmlTree.ProcessVariable(array[0]);
				//args = ((array.Length > 1) ? this.pluginUI.ProcessVariable(array[1]) : string.Empty);
				//path = ((array.Length > 2) ? this.pluginUI.ProcessVariable(array[2]) : string.Empty);
        //option = ((array.Length > 3) ? this.pluginUI.ProcessVariable(array[3]) : string.Empty);
        command = array[0];
        args = ((array.Length > 1) ? array[1] : string.Empty);
        path = ((array.Length > 2) ? array[2] : string.Empty);
        option = ((array.Length > 3) ? array[3] : string.Empty);
      }
      catch (Exception ex)
			{
				MessageBox.Show(Lib.OutputError(ex.Message.ToString()));
			}
			if (command == string.Empty) command = path;
			else if (args == string.Empty) args = path;
			if (File.Exists(command))
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				processStartInfo.FileName = command;
				processStartInfo.Arguments = args;
				if (File.Exists(args))
				{
					processStartInfo.WorkingDirectory = Path.GetDirectoryName(args);
				}
				else
				{
					processStartInfo.WorkingDirectory = Path.GetDirectoryName(command);
				}
        Process process = Win32.MdiUtil.LoadProcessInControl(processStartInfo, this.panel1);
				Win32.ShowMaximized(process.MainWindowHandle);
				this.processList.Insert(0, process);
      }
      if (!string.IsNullOrEmpty(command))
      {
        switch (this.Parent.GetType().Name)
        {
          case "DockContent":
            //try { ((DockContent)base.Parent).TabText = Path.GetFileName(this.currentPath); } catch { }
            //break;
          case "Form":
            try { ((Form)this.Parent).Text = Path.GetFileName(command); } catch { };
            break;
          case "PabPage":
            try { ((TabPage)this.Parent).Text = Path.GetFileName(command); } catch { }
            break;
        }
      }
      this.IntializeSettings();
			this.AddPreviousDocuments(argstring);
			//FIXME
      //((Form1)this.Tag).FormClosing += new FormClosingEventHandler(this.parentForm_Closing);
    }

		public void IntializeSettings()
		{
			//this.previousDocuments = this.settings.PreviousSimplePanelDocuments;
			//this.toolStrip1.Visible = this.settings.SimplePanelToolBarVisible;
			this.ツールバーTToolStripMenuItem.Checked = this.toolStrip1.Visible;
			//this.statusStrip1.Visible = this.settings.SimplePanelStatusBarVisible;
			this.ステータスバーSToolStripMenuItem.Checked = this.statusStrip1.Visible;
			this.PopulatePreviousDocumentsMenu();
		}

		public void AddPreviousDocuments(string data)
		{
			try
			{
				if (this.previousDocuments.Contains(data))
				{
					this.previousDocuments.Remove(data);
				}
				this.previousDocuments.Insert(0, data);
				this.PopulatePreviousDocumentsMenu();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		public void PopulatePreviousDocumentsMenu()
		{
			try
			{
				ToolStripMenuItem toolStripMenuItem = this.最近開いたファイルRToolStripMenuItem;
				toolStripMenuItem.DropDownItems.Clear();
				for (int i = 0; i < this.previousDocuments.Count; i++)
				{
					string text = this.previousDocuments[i];
					string[] array = text.Split(new char[]
					{
						'|'
					});
					string text2 = array[0];
					string text3 = (array.Length > 1) ? array[1] : string.Empty;
					string text4 = (array.Length > 2) ? array[2] : string.Empty;
					if (array.Length <= 3)
					{
						string arg_A1_0 = string.Empty;
					}
					else
					{
						//thhis.pluginUI.ProcessVariable(array[3]);
					}
					if (text2 == string.Empty)
					{
						text2 = text4;
					}
					else if (text3 == string.Empty)
					{
						text3 = text4;
					}
					ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
					toolStripMenuItem2.Click += new EventHandler(this.PreviousDocumentsMenuItem_Click);
					toolStripMenuItem2.Tag = text;
					if (text3 != string.Empty && File.Exists(text2))
					{
						toolStripMenuItem2.Text = Path.GetFileNameWithoutExtension(text2) + "!" + text3;
					}
					else if (text2 != string.Empty)
					{
						toolStripMenuItem2.Text = text2;
					}
					else
					{
						toolStripMenuItem2.Text = "最近のアイテム";
					}
					if (i < 15)
					{
						toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
					}
					else
					{
						this.previousDocuments.Remove(this.previousDocuments[i]);
					}
				}
				if (this.previousDocuments.Count > 0)
				{
					toolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
					toolStripMenuItem.DropDownItems.Add(this.最近開いたファイルをクリアCToolStripMenuItem);
					toolStripMenuItem.Enabled = true;
				}
				else
				{
					toolStripMenuItem.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
    }

		private void PreviousDocumentsMenuItem_Click(object sender, EventArgs e)
		{
      ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
			string text = toolStripMenuItem.Tag as string;
			string[] array = text.Split(new char[]{'|'});
			//string text2 = this.pluginUI.ProcessVariable(array[0]);
			//string text3 = (array.Length > 1) ? this.pluginUI.ProcessVariable(array[1]) : string.Empty;
			//string text4 = (array.Length > 2) ? this.pluginUI.ProcessVariable(array[2]) : string.Empty;
      string text2 = array[0];
      string text3 = (array.Length > 1) ? array[1] : string.Empty;
      string text4 = (array.Length > 2) ? array[2] : string.Empty;
      /*
      if (array.Length <= 3)
			{
				string arg_8D_0 = string.Empty;
			}
			else
			{
				this.pluginUI.ProcessVariable(array[3]);
			}
      */
      if (text2 == string.Empty)
			{
				text2 = text4;
			}
			else if (text3 == string.Empty)
			{
				text3 = text4;
			}
			if (File.Exists(text2))
			{
				try
				{
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					processStartInfo.FileName = text2;
					processStartInfo.Arguments = text3;
					if (File.Exists(text3))
					{
						processStartInfo.WorkingDirectory = Path.GetDirectoryName(text3);
					}
					else if (File.Exists(text2))
					{
						processStartInfo.WorkingDirectory = Path.GetDirectoryName(text2);
					}
					Process process = Win32.MdiUtil.LoadProcessInControl(processStartInfo, this.panel1);
					Win32.ShowMaximized(process.MainWindowHandle);
					this.processList.Insert(0, process);
					if (File.Exists(text3))
					{
						//((DockContent)base.Parent).TabText = Path.GetFileNameWithoutExtension(text2) + "!" + Path.GetFileName(text3);
					}
					else
					{
						//((DockContent)base.Parent).TabText = Path.GetFileName(text2);
					}
					this.AddPreviousDocuments(text);
				}
				catch (Exception ex)
				{
					string message = ex.Message.ToString();
					MessageBox.Show(Lib.OutputError(message));
				}
			}
    }

		private void 最近開いたファイルをクリアCToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.previousDocuments.Clear();
			this.PopulatePreviousDocumentsMenu();
		}

		private void SimplePanel_Enter(object sender, EventArgs e)
		{
		}

		private void parentForm_Closing(object sender, FormClosingEventArgs e)
		{
			foreach (Process current in this.processList)
			{
				if (current != null)
				{
					if (!current.CloseMainWindow())
					{
						current.Kill();
					}
					current.Close();
					current.Dispose();
				}
			}
			//this.settings.PreviousSimplePanelDocuments = this.previousDocuments;
			//this.settings.SimplePanelMenuBarVisible = this.menuStrip1.Visible;
			//this.settings.SimplePanelToolBarVisible = this.toolStrip1.Visible;
			//this.settings.SimplePanelStatusBarVisible = this.statusStrip1.Visible;
		}

		private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.openFileDialog1.Filter = "All files(*.*)|*.*|Supported files|*.txt;*.log;*.ini;*.inf;*.tex;*.htm;*.html;*.css;*.js;*.xml;*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx;*.cs;*.java;*.py;*.rb;*.pl;*.vbs;*.bat|Text file(*.txt, *.log, *.tex, ...)|*.txt;*.log;*.ini;*.inf;*.tex|HTML file(*.htm, *.html)|*.htm;*.html|CSS file(*.css)|*.css|Javascript file(*.js)|*.js|XML file(*.xml)|*.xml|C/C++ source(*.c, *.h, ...)|*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx|C# source(*.cs)|*.cs|Java source(*.java)|*.java|Python script(*.py)|*.py|Ruby script(*.rb)|*.rb|Perl script(*.pl)|*.pl|VB script(*.vbs)|*.vbs|Batch file(*.bat)|*.bat";
			this.openFileDialog1.FileName = "*.*";
			this.openFileDialog1.FilterIndex = 1;
			this.openFileDialog1.InitialDirectory = "F:\\";
			DialogResult dialogResult = this.openFileDialog1.ShowDialog();
			if (dialogResult != DialogResult.OK)
			{
				return;
			}
			Path.GetFileName(this.openFileDialog1.FileName);
			Path.GetFileNameWithoutExtension(this.openFileDialog1.FileName);
			try
			{
				Process process = Win32.MdiUtil.LoadProcessInControl(this.openFileDialog1.FileName, this.panel1);
        Win32.ShowMaximized(process.MainWindowHandle);
				this.processList.Insert(0, process);
				//((DockContent)base.Parent).TabText = Path.GetFileName(this.openFileDialog1.FileName);
				this.AddPreviousDocuments(this.openFileDialog1.FileName);
			}
			catch (Exception)
			{
				MessageBox.Show("ファイルを開く処理でエラーが発生しました。ファイルの種類を確認して下さい。", "MDI Sample", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void graph371ToolStripMenuItem_Click(object sender, EventArgs e)
		{
      /*
      string text = Path.Combine(PathHelper.BaseDir, "DockableControls\\graph371.exe");
			Process process = AntPlugin.CommonLibrary.Win32.MdiUtil.LoadProcessInControl(text, this.panel1);
			new StringBuilder(100);
			AntPlugin.CommonLibrary.Win32.ShowMaximized(process.MainWindowHandle);
			this.processList.Insert(0, process);
			((DockContent)base.Parent).TabText ="graph371.exe";
			this.AddPreviousDocuments(text);
      */
    }

		private void lesson5ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = "F:\\c_program\\OpenGL\\NeHe\\Lesson05\\Lesson5.exe";
			Process process = Win32.MdiUtil.LoadProcessInControl(text, this.panel1);
			StringBuilder stringBuilder = new StringBuilder(100);
			Win32.ShowMaximized(process.MainWindowHandle);
			Win32.GetWindowText(process.MainWindowHandle, stringBuilder, stringBuilder.Capacity);
			this.processList.Insert(0, process);
			//((DockContent)base.Parent).TabText = stringBuilder.ToString();
			this.AddPreviousDocuments(text);
		}

		private void 試験ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
		}

		private void プロセスPToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<IntPtr> windowsInControl = Win32.GetWindowsInControl(this.panel1.Handle);
			IntPtr window = Win32.GetWindow(this.panel1.Handle, 5u);
			this.プロセスPToolStripMenuItem.DropDownItems.Clear();
			foreach (IntPtr current in windowsInControl)
			{
				Process process = this.FindProcessByHandle(current);
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
				if (process.MainWindowHandle == window)
				{
					toolStripMenuItem.Checked = true;
				}
				toolStripMenuItem.Text = process.StartInfo.FileName;
				toolStripMenuItem.Tag = process;
				toolStripMenuItem.Click += new EventHandler(this.ProcessItem_Click);
				this.プロセスPToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
			}
			this.プロセスPToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
			this.プロセスPToolStripMenuItem.DropDownItems.Add(this.全プロセスの停止ToolStripMenuItem);
		}

		private void ProcessItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			Process process = toolStripMenuItem.Tag as Process;
			foreach (ToolStripMenuItem toolStripMenuItem2 in this.プロセスPToolStripMenuItem.DropDownItems)
			{
				toolStripMenuItem2.Checked = false;
			}
			toolStripMenuItem.Checked = true;
			if (process != null)
			{
				Win32.SetForegroundWindow(process.MainWindowHandle);
				//((DockContent)base.Parent).TabText = Path.GetFileName(process.StartInfo.FileName);
				this.AddPreviousDocuments(process.StartInfo.FileName);
			}
		}

		private Process FindProcessByHandle(IntPtr hwnd)
		{
			foreach (Process current in this.processList)
			{
				if (current.MainWindowHandle == hwnd)
				{
					return current;
				}
			}
			return null;
		}

		private void 全プロセスの停止ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (Process current in this.processList)
			{
				if (current != null)
				{
					if (!current.CloseMainWindow())
					{
						current.Kill();
					}
					current.Close();
					current.Dispose();
				}
			}
			this.プロセスPToolStripMenuItem.DropDownItems.Clear();
			this.プロセスPToolStripMenuItem.DropDownItems.Add(this.全プロセスの停止ToolStripMenuItem);
			this.processList.Clear();
			//((DockContent)base.Parent).TabText = "Execute In Place";
		}

		private void デスクトップに移動ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IntPtr window = Win32.GetWindow(this.panel1.Handle, 5u);
			Win32.SetParent(window, IntPtr.Zero);
			Process process = this.FindProcessByHandle(window);
			if (this.processList.Contains(process))
			{
				this.processList.Remove(process);
			}
			window = Win32.GetWindow(this.panel1.Handle, 5u);
			if (window == IntPtr.Zero)
			{
				//((DockContent)base.Parent).TabText = "Execute In Place";
				return;
			}
			process = this.FindProcessByHandle(window);
			if (process == null)
			{
				//((DockContent)base.Parent).TabText = "Execute In Place";
				return;
			}
			if (File.Exists(process.StartInfo.Arguments))
			{
				//((DockContent)base.Parent).TabText = Path.GetFileName(process.StartInfo.Arguments);
				return;
			}
			if (File.Exists(process.StartInfo.FileName))
			{
				//((DockContent)base.Parent).TabText = Path.GetFileName(process.StartInfo.FileName);
				return;
			}
			//((DockContent)base.Parent).TabText = "Execute In Place";
		}

		private void ツールバーTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.toolStrip1.Visible = this.ツールバーTToolStripMenuItem.Checked;
		}

		private void ステータスバーSToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.statusStrip1.Visible = this.ステータスバーSToolStripMenuItem.Checked;
			this.ステータスバーSToolStripMenuItem1.Checked = this.ステータスバーSToolStripMenuItem.Checked;
		}

		private void メニューバーMToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.menuStrip1.Visible = this.メニューバーMToolStripMenuItem.Checked;
		}

		private void ステータスバーSToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.statusStrip1.Visible = this.ステータスバーSToolStripMenuItem1.Checked;
			this.ステータスバーSToolStripMenuItem.Checked = this.ステータスバーSToolStripMenuItem1.Checked;
		}

		private void エクスプローラEToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string path = string.Empty;
			IntPtr window = Win32.GetWindow(this.panel1.Handle, 5u);
			Process process = this.FindProcessByHandle(window);
			if (File.Exists(process.StartInfo.Arguments))
			{
				path = process.StartInfo.Arguments;
			}
			else if (File.Exists(process.StartInfo.FileName))
			{
				path = process.StartInfo.FileName;
			}
			if (File.Exists(path))
			{
				ProcessHandler.Run_Explorer(path);
			}
		}

		private void コマンドプロンプトCToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string path = string.Empty;
			IntPtr window = Win32.GetWindow(this.panel1.Handle, 5u);
			Process process = this.FindProcessByHandle(window);
			if (File.Exists(process.StartInfo.Arguments))
			{
				path = process.StartInfo.Arguments;
			}
			else if (File.Exists(process.StartInfo.FileName))
			{
				path = process.StartInfo.FileName;
			}
			

        if (Directory.Exists(path))
        {
          Directory.SetCurrentDirectory(path);
          Process.Start(@"C:\windows\system32\cmd.exe");
          //return;
        }
        else if (Directory.Exists(Path.GetDirectoryName(path)))
        {
          Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
          Process.Start(@"C:\windows\system32\cmd.exe");
        }
      
		}

    public Process process = null;
    private void panel1_DragDrop(object sender, DragEventArgs e)
		{
			string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
			try
			{
        this.process = Win32.MdiUtil.LoadProcessInControl(array[0], this.panel1);
        Win32.ShowMaximized(process.MainWindowHandle);
				this.processList.Insert(0, process);
        //((DockContent)base.Parent).TabText = Path.GetFileName(array[0]);

        //FIXME
        //((Form1)this.Tag).Text = Path.GetFileName(array[0]);

        this.AddPreviousDocuments(array[0]);
			}
			catch (Exception)
			{
				MessageBox.Show("ファイルを開く処理でエラーが発生しました。ファイルの種類を確認して下さい。", "MDI Sample", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void panel1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string path = array2[i];
					if (!File.Exists(path))
					{
						return;
					}
				}
				e.Effect = DragDropEffects.Copy;
			}
		}

    private void アプリケーションを起動PToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        Process.Start(this.command, this.args);
        //this.process.Kill();
        //((Form)this.Parent).Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
      }
   }

    private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // FIXME
      //((Form)this.Parent).Close();
    }
  }
}
