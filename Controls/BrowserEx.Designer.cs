
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLibrary.Controls
{
  partial class BrowserEx
  {
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
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region コンポーネント デザイナーで生成されたコード

    /// <summary>     /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
    /// コード エディターで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserEx));
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.backButton = new System.Windows.Forms.ToolStripButton();
      this.forwardButton = new System.Windows.Forms.ToolStripButton();
      this.stopButton = new System.Windows.Forms.ToolStripButton();
      this.refreshButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.iEで開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.chromeで開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fireFoxで開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.お気に入りToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.googleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.お気に入りに追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.お気に入りのクリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.履歴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.履歴をクリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.デスクトップにショートカットを作成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mhtで保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.htmlで保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.textで保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.jpegで保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.印刷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.印刷プレビューVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.linkPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.サクラエディタSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pSPadPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.エクスプローラEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.コマンドプロンプトCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.scintillaCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.richTextBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.ファイルエクスプローラを同期するXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ソースの表示VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.codeの表示DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.ファイル名を指定して実行OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.リンクを開くLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.searchButton = new System.Windows.Forms.ToolStripButton();
      this.GoHomeButton = new System.Windows.Forms.ToolStripButton();
      this.printButton = new System.Windows.Forms.ToolStripButton();
      this.goButton = new System.Windows.Forms.ToolStripButton();
      this.addressComboBox = new ToolStripSpringComboBox2();
      this.toolStrip = new System.Windows.Forms.ToolStrip();
      this.toolStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // webBrowser1
      // 
      this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.webBrowser1.Location = new System.Drawing.Point(0, 30);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.Size = new System.Drawing.Size(764, 429);
      this.webBrowser1.TabIndex = 5;
      this.webBrowser1.CanGoBackChanged += new System.EventHandler(this.WebBrowserPropertyUpdated);
      this.webBrowser1.CanGoForwardChanged += new System.EventHandler(this.WebBrowserPropertyUpdated);
      this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
      this.webBrowser1.DocumentTitleChanged += new System.EventHandler(this.WebBrowserDocumentTitleChanged);
      this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.WebBrowserNavigated);
      this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.WebBrowserNewWindow);
      // 
      // backButton
      // 
      this.backButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.backButton.Enabled = false;
      this.backButton.Image = ((System.Drawing.Image)(resources.GetObject("backButton.Image")));
      this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.backButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
      this.backButton.Name = "backButton";
      this.backButton.Size = new System.Drawing.Size(24, 25);
      this.backButton.Text = "Back";
      this.backButton.Click += new System.EventHandler(this.BackButtonClick);
      // 
      // forwardButton
      // 
      this.forwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.forwardButton.Enabled = false;
      this.forwardButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardButton.Image")));
      this.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.forwardButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
      this.forwardButton.Name = "forwardButton";
      this.forwardButton.Size = new System.Drawing.Size(24, 25);
      this.forwardButton.Text = "Forward";
      this.forwardButton.Click += new System.EventHandler(this.ForwardButtonClick);
      // 
      // stopButton
      // 
      this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
      this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.stopButton.Name = "stopButton";
      this.stopButton.Size = new System.Drawing.Size(24, 24);
      this.stopButton.Text = "StopButton";
      this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
      // 
      // refreshButton
      // 
      this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
      this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.refreshButton.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
      this.refreshButton.Name = "refreshButton";
      this.refreshButton.Size = new System.Drawing.Size(24, 25);
      this.refreshButton.Text = "Refresh";
      this.refreshButton.Click += new System.EventHandler(this.RefreshButtonClick);
      // 
      // addressComboBox
      // 
      this.addressComboBox.Name = "addressComboBox";
      this.addressComboBox.Padding = new Padding(0, 0, 1, 0);
      this.addressComboBox.Size = new Size(363, 23);
      //this.addressComboBox.SelectedIndexChanged += new EventHandler(this.AddressComboBoxSelectedIndexChanged);
      this.addressComboBox.KeyPress += new KeyPressEventHandler(this.AddressComboBoxKeyPress);

      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iEで開くToolStripMenuItem,
            this.chromeで開くToolStripMenuItem,
            this.fireFoxで開くToolStripMenuItem,
            this.toolStripSeparator2,
            this.お気に入りToolStripMenuItem,
            this.履歴ToolStripMenuItem,
            this.toolStripSeparator8,
            this.保存SToolStripMenuItem,
            this.toolStripSeparator5,
            this.印刷ToolStripMenuItem,
            this.印刷プレビューVToolStripMenuItem,
            this.toolStripSeparator1,
            this.linkPathToolStripMenuItem,
            this.ソースの表示VToolStripMenuItem,
            this.codeの表示DToolStripMenuItem,
            this.toolStripSeparator4,
            this.ファイル名を指定して実行OToolStripMenuItem,
            this.リンクを開くLToolStripMenuItem});
      this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(34, 24);
      this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
      // 
      // iEで開くToolStripMenuItem
      // 
      this.iEで開くToolStripMenuItem.Name = "iEで開くToolStripMenuItem";
      this.iEで開くToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.iEで開くToolStripMenuItem.Text = "IEで開く";
      this.iEで開くToolStripMenuItem.Click += new System.EventHandler(this.iEで開くToolStripMenuItem_Click);
      // 
      // chromeで開くToolStripMenuItem
      // 
      this.chromeで開くToolStripMenuItem.Name = "chromeで開くToolStripMenuItem";
      this.chromeで開くToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.chromeで開くToolStripMenuItem.Text = "Chromeで開く";
      this.chromeで開くToolStripMenuItem.Click += new System.EventHandler(this.chromeで開くToolStripMenuItem_Click);
      // 
      // fireFoxで開くToolStripMenuItem
      // 
      this.fireFoxで開くToolStripMenuItem.Name = "fireFoxで開くToolStripMenuItem";
      this.fireFoxで開くToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.fireFoxで開くToolStripMenuItem.Text = "FireFoxで開く";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(257, 6);
      // 
      // お気に入りToolStripMenuItem
      // 
      this.お気に入りToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.googleToolStripMenuItem,
            this.toolStripSeparator9,
            this.お気に入りに追加ToolStripMenuItem,
            this.お気に入りのクリアToolStripMenuItem});
      this.お気に入りToolStripMenuItem.Name = "お気に入りToolStripMenuItem";
      this.お気に入りToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.お気に入りToolStripMenuItem.Text = "お気に入り";
      // 
      // googleToolStripMenuItem
      // 
      this.googleToolStripMenuItem.Name = "googleToolStripMenuItem";
      this.googleToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.googleToolStripMenuItem.Tag = "http://www.google.co.jp";
      this.googleToolStripMenuItem.Text = "Google";
      this.googleToolStripMenuItem.Click += new System.EventHandler(this.favoriteMenuItem_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(186, 6);
      // 
      // お気に入りに追加ToolStripMenuItem
      // 
      this.お気に入りに追加ToolStripMenuItem.Name = "お気に入りに追加ToolStripMenuItem";
      this.お気に入りに追加ToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.お気に入りに追加ToolStripMenuItem.Text = "お気に入りに追加";
      this.お気に入りに追加ToolStripMenuItem.Click += new System.EventHandler(this.お気に入りに追加ToolStripMenuItem_Click);
      // 
      // お気に入りのクリアToolStripMenuItem
      // 
      this.お気に入りのクリアToolStripMenuItem.Name = "お気に入りのクリアToolStripMenuItem";
      this.お気に入りのクリアToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.お気に入りのクリアToolStripMenuItem.Text = "お気に入りのクリア";
      // 
      // 履歴ToolStripMenuItem
      // 
      this.履歴ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.履歴をクリアToolStripMenuItem});
      this.履歴ToolStripMenuItem.Name = "履歴ToolStripMenuItem";
      this.履歴ToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.履歴ToolStripMenuItem.Text = "履歴";
      // 
      // 履歴をクリアToolStripMenuItem
      // 
      this.履歴をクリアToolStripMenuItem.Name = "履歴をクリアToolStripMenuItem";
      this.履歴をクリアToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
      this.履歴をクリアToolStripMenuItem.Text = "履歴をクリア";
      this.履歴をクリアToolStripMenuItem.Click += new System.EventHandler(this.履歴をクリアToolStripMenuItem_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(257, 6);
      // 
      // 保存SToolStripMenuItem
      // 
      this.保存SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.デスクトップにショートカットを作成ToolStripMenuItem,
            this.mhtで保存ToolStripMenuItem,
            this.htmlで保存ToolStripMenuItem,
            this.textで保存ToolStripMenuItem,
            this.jpegで保存ToolStripMenuItem});
      this.保存SToolStripMenuItem.Name = "保存SToolStripMenuItem";
      this.保存SToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.保存SToolStripMenuItem.Text = "保存(&S)";
      // 
      // デスクトップにショートカットを作成ToolStripMenuItem
      // 
      this.デスクトップにショートカットを作成ToolStripMenuItem.Name = "デスクトップにショートカットを作成ToolStripMenuItem";
      this.デスクトップにショートカットを作成ToolStripMenuItem.Size = new System.Drawing.Size(275, 26);
      this.デスクトップにショートカットを作成ToolStripMenuItem.Text = "デスクトップにショートカットを作成";
      this.デスクトップにショートカットを作成ToolStripMenuItem.Click += new System.EventHandler(this.デスクトップにショートカットを作成ToolStripMenuItem_Click);
      // 
      // mhtで保存ToolStripMenuItem
      // 
      this.mhtで保存ToolStripMenuItem.Enabled = false;
      this.mhtで保存ToolStripMenuItem.Name = "mhtで保存ToolStripMenuItem";
      this.mhtで保存ToolStripMenuItem.Size = new System.Drawing.Size(275, 26);
      this.mhtで保存ToolStripMenuItem.Text = "mhtで保存";
      // 
      // htmlで保存ToolStripMenuItem
      // 
      this.htmlで保存ToolStripMenuItem.Name = "htmlで保存ToolStripMenuItem";
      this.htmlで保存ToolStripMenuItem.Size = new System.Drawing.Size(275, 26);
      this.htmlで保存ToolStripMenuItem.Text = "htmlで保存";
      this.htmlで保存ToolStripMenuItem.Click += new System.EventHandler(this.htmlで保存ToolStripMenuItem_Click);
      // 
      // textで保存ToolStripMenuItem
      // 
      this.textで保存ToolStripMenuItem.Name = "textで保存ToolStripMenuItem";
      this.textで保存ToolStripMenuItem.Size = new System.Drawing.Size(275, 26);
      this.textで保存ToolStripMenuItem.Text = "textで保存";
      this.textで保存ToolStripMenuItem.Click += new System.EventHandler(this.textで保存ToolStripMenuItem_Click);
      // 
      // jpegで保存ToolStripMenuItem
      // 
      this.jpegで保存ToolStripMenuItem.Name = "jpegで保存ToolStripMenuItem";
      this.jpegで保存ToolStripMenuItem.Size = new System.Drawing.Size(275, 26);
      this.jpegで保存ToolStripMenuItem.Text = "jpegで保存";
      this.jpegで保存ToolStripMenuItem.Click += new System.EventHandler(this.jpegで保存ToolStripMenuItem_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(257, 6);
      // 
      // 印刷ToolStripMenuItem
      // 
      this.印刷ToolStripMenuItem.Name = "印刷ToolStripMenuItem";
      this.印刷ToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.印刷ToolStripMenuItem.Text = "印刷(&P)";
      this.印刷ToolStripMenuItem.Click += new System.EventHandler(this.印刷ToolStripMenuItem_Click);
      // 
      // 印刷プレビューVToolStripMenuItem
      // 
      this.印刷プレビューVToolStripMenuItem.Name = "印刷プレビューVToolStripMenuItem";
      this.印刷プレビューVToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.印刷プレビューVToolStripMenuItem.Text = "印刷プレビュー(&V)";
      this.印刷プレビューVToolStripMenuItem.Click += new System.EventHandler(this.印刷プレビューVToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(257, 6);
      // 
      // linkPathToolStripMenuItem
      // 
      this.linkPathToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.サクラエディタSToolStripMenuItem,
            this.pSPadPToolStripMenuItem,
            this.エクスプローラEToolStripMenuItem,
            this.コマンドプロンプトCToolStripMenuItem,
            this.toolStripSeparator3,
            this.scintillaCToolStripMenuItem,
            this.richTextBoxToolStripMenuItem,
            this.toolStripSeparator6,
            this.ファイルエクスプローラを同期するXToolStripMenuItem});
      this.linkPathToolStripMenuItem.Name = "linkPathToolStripMenuItem";
      this.linkPathToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.linkPathToolStripMenuItem.Text = "LinkPath";
      // 
      // サクラエディタSToolStripMenuItem
      // 
      this.サクラエディタSToolStripMenuItem.Name = "サクラエディタSToolStripMenuItem";
      this.サクラエディタSToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
      this.サクラエディタSToolStripMenuItem.Text = "サクラエディタ(&S)";
      this.サクラエディタSToolStripMenuItem.Click += new System.EventHandler(this.サクラエディタSToolStripMenuItem_Click);
      // 
      // pSPadPToolStripMenuItem
      // 
      this.pSPadPToolStripMenuItem.Name = "pSPadPToolStripMenuItem";
      this.pSPadPToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
      this.pSPadPToolStripMenuItem.Text = "PSPad(&P)";
      this.pSPadPToolStripMenuItem.Click += new System.EventHandler(this.pSPadPToolStripMenuItem_Click);
      // 
      // エクスプローラEToolStripMenuItem
      // 
      this.エクスプローラEToolStripMenuItem.Name = "エクスプローラEToolStripMenuItem";
      this.エクスプローラEToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
      this.エクスプローラEToolStripMenuItem.Text = "エクスプローラ(&E)";
      this.エクスプローラEToolStripMenuItem.Click += new System.EventHandler(this.エクスプローラEToolStripMenuItem_Click);
      // 
      // コマンドプロンプトCToolStripMenuItem
      // 
      this.コマンドプロンプトCToolStripMenuItem.Name = "コマンドプロンプトCToolStripMenuItem";
      this.コマンドプロンプトCToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
      this.コマンドプロンプトCToolStripMenuItem.Text = "コマンド・プロンプト(&C)";
      this.コマンドプロンプトCToolStripMenuItem.Click += new System.EventHandler(this.コマンドプロンプトCToolStripMenuItem_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(288, 6);
      // 
      // scintillaCToolStripMenuItem
      // 
      this.scintillaCToolStripMenuItem.Name = "scintillaCToolStripMenuItem";
      this.scintillaCToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
      this.scintillaCToolStripMenuItem.Text = "Scintilla(&A)";
      this.scintillaCToolStripMenuItem.Click += new System.EventHandler(this.scintillaCToolStripMenuItem_Click);
      // 
      // richTextBoxToolStripMenuItem
      // 
      this.richTextBoxToolStripMenuItem.Name = "richTextBoxToolStripMenuItem";
      this.richTextBoxToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
      this.richTextBoxToolStripMenuItem.Text = "RichTextEditor(&R)";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(288, 6);
      // 
      // ファイルエクスプローラを同期するXToolStripMenuItem
      // 
      this.ファイルエクスプローラを同期するXToolStripMenuItem.Name = "ファイルエクスプローラを同期するXToolStripMenuItem";
      this.ファイルエクスプローラを同期するXToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
      this.ファイルエクスプローラを同期するXToolStripMenuItem.Text = "ファイルエクスプローラを同期する(&X)";
      this.ファイルエクスプローラを同期するXToolStripMenuItem.Click += new System.EventHandler(this.ファイルエクスプローラを同期するXToolStripMenuItem_Click);
      // 
      // ソースの表示VToolStripMenuItem
      // 
      this.ソースの表示VToolStripMenuItem.Name = "ソースの表示VToolStripMenuItem";
      this.ソースの表示VToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.ソースの表示VToolStripMenuItem.Text = "ソースの表示(&V)";
      this.ソースの表示VToolStripMenuItem.Click += new System.EventHandler(this.ソースの表示VToolStripMenuItem_Click);
      // 
      // codeの表示DToolStripMenuItem
      // 
      this.codeの表示DToolStripMenuItem.Name = "codeの表示DToolStripMenuItem";
      this.codeの表示DToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.codeの表示DToolStripMenuItem.Text = "Codeの表示(&D)";
      this.codeの表示DToolStripMenuItem.Click += new System.EventHandler(this.codeの表示DToolStripMenuItem_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(257, 6);
      // 
      // ファイル名を指定して実行OToolStripMenuItem
      // 
      this.ファイル名を指定して実行OToolStripMenuItem.Name = "ファイル名を指定して実行OToolStripMenuItem";
      this.ファイル名を指定して実行OToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.ファイル名を指定して実行OToolStripMenuItem.Text = "ファイル名を指定して実行(&O)";
      this.ファイル名を指定して実行OToolStripMenuItem.Click += new System.EventHandler(this.ファイル名を指定して実行OToolStripMenuItem_Click);
      // 
      // リンクを開くLToolStripMenuItem
      // 
      this.リンクを開くLToolStripMenuItem.Name = "リンクを開くLToolStripMenuItem";
      this.リンクを開くLToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.リンクを開くLToolStripMenuItem.Text = "リンクを開く(&L)";
      this.リンクを開くLToolStripMenuItem.Click += new System.EventHandler(this.リンクを開くLToolStripMenuItem_Click);
      // 
      // searchButton
      // 
      this.searchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.searchButton.Image = ((System.Drawing.Image)(resources.GetObject("searchButton.Image")));
      this.searchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.searchButton.Name = "searchButton";
      this.searchButton.Size = new System.Drawing.Size(24, 24);
      this.searchButton.Text = "GoogleSearch";
      this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
      // 
      // GoHomeButton
      // 
      this.GoHomeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.GoHomeButton.Image = ((System.Drawing.Image)(resources.GetObject("GoHomeButton.Image")));
      this.GoHomeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.GoHomeButton.Name = "GoHomeButton";
      this.GoHomeButton.Size = new System.Drawing.Size(24, 24);
      this.GoHomeButton.Text = "GoHome";
      this.GoHomeButton.Click += new System.EventHandler(this.GoHomeButton_Click);
      // 
      // printButton
      // 
      this.printButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.printButton.Image = ((System.Drawing.Image)(resources.GetObject("printButton.Image")));
      this.printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.printButton.Name = "printButton";
      this.printButton.Size = new System.Drawing.Size(24, 24);
      this.printButton.Text = "print";
      this.printButton.Click += new System.EventHandler(this.printButton_Click);
      // 
      // goButton
      // 
      this.goButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.goButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.goButton.Image = ((System.Drawing.Image)(resources.GetObject("goButton.Image")));
      this.goButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.goButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
      this.goButton.Name = "goButton";
      this.goButton.Size = new System.Drawing.Size(24, 25);
      this.goButton.Text = "Go";
      this.goButton.Click += new System.EventHandler(this.BrowseButtonClick);
      // 
      // toolStrip
      // 
      this.toolStrip.CanOverflow = false;
      this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.forwardButton,
            this.stopButton,
            this.refreshButton,
            this.toolStripSeparator7,
            this.toolStripDropDownButton1,
            this.searchButton,
            this.GoHomeButton,
            this.printButton,
            this.addressComboBox,
            this.goButton});
      this.toolStrip.Location = new System.Drawing.Point(0, 0);
      this.toolStrip.Name = "toolStrip";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(2, 1, 2, 2);
      this.toolStrip.Size = new System.Drawing.Size(764, 30);
      this.toolStrip.TabIndex = 4;
      // 
      // BrowserEx
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.webBrowser1);
      this.Controls.Add(this.toolStrip);
      this.Name = "BrowserEx";
      this.Size = new System.Drawing.Size(764, 459);
      this.Load += new System.EventHandler(this.Browser_Load);
      this.toolStrip.ResumeLayout(false);
      this.toolStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.ToolStripSpringComboBox2 addressComboBox;
    public System.Windows.Forms.WebBrowser webBrowser1;
    private System.Windows.Forms.ToolStripButton backButton;
    private System.Windows.Forms.ToolStripButton forwardButton;
    private System.Windows.Forms.ToolStripButton stopButton;
    private System.Windows.Forms.ToolStripButton refreshButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem iEで開くToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem chromeで開くToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem fireFoxで開くToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem お気に入りToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem googleToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripMenuItem お気に入りに追加ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem お気に入りのクリアToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem 履歴ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem 履歴をクリアToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripMenuItem 保存SToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem デスクトップにショートカットを作成ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mhtで保存ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem htmlで保存ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem textで保存ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem jpegで保存ToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem 印刷ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem 印刷プレビューVToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem linkPathToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem サクラエディタSToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pSPadPToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem エクスプローラEToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem コマンドプロンプトCToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem scintillaCToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem richTextBoxToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripMenuItem ファイルエクスプローラを同期するXToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ソースの表示VToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem codeの表示DToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem ファイル名を指定して実行OToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem リンクを開くLToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton searchButton;
    private System.Windows.Forms.ToolStripButton GoHomeButton;
    private System.Windows.Forms.ToolStripButton printButton;
    private System.Windows.Forms.ToolStripButton goButton;
    private System.Windows.Forms.ToolStrip toolStrip;
  }
}
