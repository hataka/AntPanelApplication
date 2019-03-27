#undef Interface
//#define Interface

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;

using Sgry.Azuki;
using Sgry.Azuki.Highlighter;
using Sgry.Azuki.WinForms;
using AntPanelApplication.CommonLibrary;
using AntPanelApplication.Controls;
#if Interface
//using AntPlugin;
using MDIForm;
//using PluginCore;
//using WeifenLuo.WinFormsUI.Docking;
//using PluginCore.Helpers;
#endif

namespace AntPanelApplication.Controls
{
#if Interface
	public partial class AzukiEditor : UserControl, IMDIForm
#else
	public partial class AzukiEditor : UserControl
#endif
	{
    //public static String baseDir = @"C:\Documents and Settings\kazuhiko\Local Settings\Application Data\FlashDevelop";
#if Interface
    private PluginMain xmlTreeMenu;
    private XMLTreeMenu.Settings settings;
#endif    
		private List<string> previousDocuments = new List<string>();
    public string currentPath = string.Empty;
    public Point currentPoint = new Point(0, 0);
    public azukiFindDialog azukiFindDlg = null;
    public azukiJumpDialog azukiJumpDlg = null;
    private int _UntitledFileCount = 1;
    private string printingText;
    private int printingPosition = 0;
    private Font printFont;
    public static ImageList imageList1;
    private ToolStripButton imageListButton;
    private SplitContainer splitContainer1;
    public AzukiControl azukiControl2;
    private ToolStripMenuItem コンソールSToolStripMenuItem;
    public AzukiControl azukiControl1;

    #region Properties
#if Interface
    public ParentFormClass MainForm
    {
      get;
      set;
    }

    public ChildFormControlClass Instance
    {
      get;
      set;
    }
#endif

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

    #region Constructor
    public AzukiEditor()
    {
      InitializeComponent();
      this.InitializeInterface();
      this.InitializeAzukiEditor();
      this.Text = "Azuki Editor Ver1.0";
    }

    public AzukiEditor(string[] args)
    {
      InitializeComponent();
      this.InitializeInterface();
      this.InitializeAzukiEditor();
      this.Text = "Azuki Editor Ver1.0";
      if (args[0] != String.Empty && File.Exists(args[0])
          && Lib.IsTextFile(args[0]))
      {
        this.currentPath = args[0];
      }
    }
    #endregion

    #region Initialization

    public void InitializeInterface()
    {
#if Interface
			string guid = "0538077E-8C37-4A2B-962B-8FB77DC9F325";
      this.xmlTreeMenu = (PluginMain)PluginBase.MainForm.FindPlugin(guid);
      this.settings = this.xmlTreeMenu.Settings  as XMLTreeMenu.Settings;
      this.Instance = new ChildFormControlClass();
      this.Instance.name = "AzukiEditor";
      
			this.Instance.toolStrip = this.toolStrip1;
      this.Instance.menuStrip = this.menuStrip1;
      this.Instance.statusStrip = this.statusStrip1;
      this.Instance.スクリプトToolStripMenuItem = this.スクリプトCToolStripMenuItem;
      this.Instance.toolStripStatusLabel = this.toolStripStatusLabel1;
      this.toolStrip1.Renderer = new DockPanelStripRenderer(true);
      this.Instance.PreviousDocuments = this.PreviousDocuments;
#endif    
		}
    
    private void InitializeAzukiEditor()
    {
      //String resource = Path.Combine(baseDir, @"CsMacro\CustomDocument\Resources\"); //"
      //String resource = Path.Combine(PathHelper.BaseDir, @"SettingData\\Resources\"); //"
      InitializeImageList();
      //this.新規作成NToolStripButton.Image = new Bitmap(resource + "new_Button.bmp");
      //this.開くOToolStripButton.Image = new Bitmap(resource + "open_Button.bmp");
      //this.上書き保存SToolStripButton.Image = new Bitmap(resource + "save_Button.bmp");
      //this.toolStripButton1.Image = new Bitmap(resource + "saveas_Button.bmp");
      //this.印刷PToolStripButton.Image = new Bitmap(resource + "print_Button.bmp");
      //this.切り取りUToolStripButton.Image = new Bitmap(resource + "cut_Button.bmp");
      //this.コピーCToolStripButton.Image = new Bitmap(resource + "copy_Button.bmp");
      //this.貼り付けPToolStripButton.Image = new Bitmap(resource + "paste_Button.bmp");
      //this.ヘルプLToolStripButton.Image = new Bitmap(resource + "help_Button.bmp");
      //this.新規作成NToolStripMenuItem.Image = new Bitmap(resource + "new_MenuItem.bmp");
      //this.開くOToolStripMenuItem.Image = new Bitmap(resource + "open_MenuItem.bmp");
      //this.印刷PToolStripMenuItem.Image = new Bitmap(resource + "print_MenuItem.bmp");
      //this.印刷プレビューVToolStripMenuItem.Image = new Bitmap(resource + "printpreview_MenuItem.bmp");
      //this.切り取りTToolStripMenuItem.Image = new Bitmap(resource + "cut_MenuItem.bmp");
      //this.コピーCToolStripMenuItem.Image = new Bitmap(resource + "copy_MenuItem.bmp");
      this.toolStripDropDownButton1.Image = imageList1.Images[15]; 
      //System.Drawing.Image.FromFile(resource + @"famfamicons\asterisk_orange.png");
      this.toolStripDropDownButton2.Image = imageList1.Images[64];
      this.toolStripDropDownButton3.Image = imageList1.Images[117];
      this.statusStrip1.Visible = true;
      this.menuStrip1.Visible = true;
      this.toolStrip1.Visible = true;
      this.imageListButton.Visible = false;
      // Designer を有効にするため ここに移す
      this.AutoScaleMode = AutoScaleMode.Font;
      this.azukiControl1.BorderStyle = BorderStyle.None;
    }

    public void InitializeImageList()
    {
      //String path = Path.Combine(baseDir, @"Settings\PSPad.bmp");
      //String path = Path.Combine(PathHelper.BaseDir, @"SettingData\PSPad.bmp");
      //Bitmap bmp3 = new Bitmap(path);
      Bitmap bmp3 = (Bitmap)this.imageListButton.Image;
      // 
      // imageList1
      // 
      imageList1 = new ImageList();
      imageList1.Images.AddStrip(bmp3);
      imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      imageList1.ImageSize = new System.Drawing.Size(16, 16);
      imageList1.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // imageList1
      // 
      //Icon型としてアイコンファイルを読み込み、16x16の画像を取得する
      //AddIcon("sakura_16x16.ico"); // 147
      //AddIcon("PSPad00.ico"); //148
      //AddIcon("Scintilla.ico"); //149
      //AddIcon("AnnCompact.ico"); //150
      //System.Drawing.Image img
      //  = System.Drawing.Image.FromFile(Path.Combine(baseDir, @"Settings\icons\EmEditor_16x16.png"));
      //System.Drawing.Image img
      //  = System.Drawing.Image.FromFile(Path.Combine(PathHelper.BaseDir, @"SettingData\icons\EmEditor_16x16.png"));
      //imageList1.Images.Add(img); //151
      //AddIcon("W95Icon0254_yellow.ico"); //152
      //AddIcon("cmd.ico"); //153
    }
    /**
     * Handles the click event for the menu items.
     */
    public void AddIcon(String name)
    {
      //String baseDir = PathHelper.BaseDir;
      // http://dobon.net/vb/dotnet/graphics/imagefromfile.html
      //String iconPath = Path.Combine(baseDir, @"Settings\icons\" + name); //"
      //String iconPath = Path.Combine(PathHelper.BaseDir, @"SettingData\icons\" + name); //"
      
      //System.Drawing.Icon ico = new System.Drawing.Icon(iconPath, 16, 16);
      //Bitmapに変換する
      //System.Drawing.Bitmap bmp = ico.ToBitmap();
      //変換したBitmapしか使わないならば、元のIconは解放できる
      //ico.Dispose();
      //イメージを表示する
      //imageList1.Images.Add(bmp);
    }

    public void IntializeSettings()
    {
#if Interface      
			this.previousDocuments = this.settings.PreviousAzukiEditorDocuments;
      this.toolStrip1.Visible = this.settings.AzukiEditorToolBarVisible;
      this.statusStrip1.Visible = this.settings.AzukiEditorStatusBarVisible;
      this.azukiControl1.Font = this.settings.AzukiEditorDefaultFont;
      //this.azukiControl1.Font = new System.Drawing.Font("ＭＳ ゴシック", 16F);
      Sgry.Azuki.FontInfo fontInfo7 = new Sgry.Azuki.FontInfo();
      fontInfo7.Name = this.settings.AzukiEditorDefaultFont.Name;
      fontInfo7.Size = (int)this.settings.AzukiEditorDefaultFont.Size;
      fontInfo7.Style = this.settings.AzukiEditorDefaultFont.Style;
      this.azukiControl1.FontInfo = fontInfo7;
#endif
      this.ツールバーTToolStripMenuItem.Checked = this.toolStrip1.Visible;
			this.ステータスバーUToolStripMenuItem.Checked = this.statusStrip1.Visible;
      this.PopulatePreviousDocumentsMenu();
    }
    #endregion

    #region Event Handler

    private void azukiControl1_DoubleClick(object sender, EventArgs e)
    {
      Document document = this.azukiControl1.Document;
      IMouseEventArgs mouseEventArgs = (IMouseEventArgs)e;
      if (mouseEventArgs.Index < document.Length && document.IsMarked(mouseEventArgs.Index, Marking.Uri))
      {
        string markedText = document.GetMarkedText(mouseEventArgs.Index, Marking.Uri);
        if (markedText != null)
        {
#if Interface          
          string chromePath = this.settings.ChromePath;
          PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseEx;" + markedText);
          //Process.Start(chromePath, markedText);
#else          
          string chromePath
          = @"C:\Documents and Settings\kazuhiko\Local Settings\Application Data\Google\Chrome\Application\chrome.exe";
          Process.Start(chromePath, markedText);
#endif
          mouseEventArgs.Handled = true;
        }
      }
    }

    private void azukiControl1_TextChanged(object sender, EventArgs e)
    {
      try
      {
        //#if Interface     
        //((DockContent)base.Parent).TabText = Path.GetFileName(this.currentPath) + "*";
        //#else 
      }
      catch
      {
        //((Form)this.Parent).Text = Path.GetFileName(this.currentPath) + "*";
        //#endif
      }
    }

    private void azukiControl1_CaretMoved(object sender, EventArgs e)
    {
      int num = 0;
      int num2 = 0;
      this.azukiControl1.Document.GetSelection(out num, out num2);
      string text = this.azukiControl1.Document.Text;
      int num3 = 1;
      int num4 = 0;
      int num5;
      while ((num5 = text.IndexOf('\n', num4)) < num && num5 > -1)
      {
        num4 = num5 + 1;
        num3++;
      }
      int x = num - num4 + 1;
      this.currentPoint.X = x;
      this.currentPoint.Y = num3;
      this.UpdateStatusText(this.currentPath);
    }

    private void AzukiEditor_Load(object sender, EventArgs e)
    {
      this.splitContainer1.Panel1Collapsed = false;
      this.splitContainer1.Panel2Collapsed = true;

      this.azukiControl1.Highlighter = Highlighters.Cpp;
      this.azukiControl1.TabWidth = 2;
      this.azukiControl1.ShowsHRuler = true;
      this.azukiControl1.Document.IsDirty = false;
      this.azukiControl1.MarksUri = true;
      this.printingText = this.azukiControl1.Text;
      this.printingPosition = 0;
      this.printFont = new Font("ＭＳ Ｐゴシック", 16f);
      if (!string.IsNullOrEmpty(this.azukiControl1.Tag.ToString())) 
        //&& File.Exists(this.azukiControl1.Tag.ToString()) 
        //&& Lib.IsTextFile(this.azukiControl1.Tag.ToString()))
      {
        this.currentPath = this.azukiControl1.Tag.ToString();
      }
      this.IntializeSettings();
      //((Form)this.Parent).FormClosing += new FormClosingEventHandler(this.parentForm_Closing);
      this.SetHighlight(this.currentPath);
      if (!string.IsNullOrEmpty(this.currentPath)
        && File.Exists(this.currentPath) && Lib.IsTextFile(this.currentPath))
      {
        this.azukiControl1.Document.Text = Lib.File_ReadToEndDecode(this.currentPath);
        this.azukiControl1.Document.IsDirty = false;
#if Interface        
        //((DockContent)base.Parent).TabText = Path.GetFileName(this.currentPath);
#else        
        //((Form)this.Parent).Text = Path.GetFileName(this.currentPath);
#endif
        this.AddPreviousDocuments(this.currentPath);
        this.UpdateStatusText(this.currentPath);
      }
    }

    public void SetHighlight(string path)
    {
      this.強調表示MenuItem_CheckClear();
      string extension = Path.GetExtension(path);
      switch (extension)
      {
        case ".c":
        case ".cpp":
        case ".h":
          this.azukiControl1.Highlighter = Highlighters.Cpp;
          this.cCCToolStripMenuItem.Checked = true;
          return;
        case ".cs":
          this.azukiControl1.Highlighter = Highlighters.CSharp;
          this.cToolStripMenuItem.Checked = true;
          return;
        case ".java":
          this.azukiControl1.Highlighter = Highlighters.Java;
          this.javaJToolStripMenuItem.Checked = true;
          return;
        case ".tex":
          this.azukiControl1.Highlighter = Highlighters.Latex;
          this.laTexLToolStripMenuItem.Checked = true;
          return;
        case ".xml":
          this.azukiControl1.Highlighter = Highlighters.Xml;
          this.xMLXToolStripMenuItem.Checked = true;
          return;
        case ".rb":
          this.azukiControl1.Highlighter = Highlighters.Ruby;
          this.rubyToolStripMenuItem.Checked = true;
          return;
        case ".txt":
          this.azukiControl1.Highlighter = null;
          this.textTToolStripMenuItem.Checked = true;
          return;
      }
      this.azukiControl1.Highlighter = Highlighters.Cpp;
      this.cCCToolStripMenuItem.Checked = true;
    }

    private void 強調表示MenuItem_CheckClear()
    {
      foreach (ToolStripMenuItem toolStripMenuItem in this.強調表示YToolStripMenuItem.DropDownItems)
      {
        toolStripMenuItem.Checked = false;
      }
    }

    private void AzukiEditor_Leave(object sender, EventArgs e)
    {
      if (!this.toolStrip1.Visible)
      {
      }
    }

    private void AzukiEditor_Enter(object sender, EventArgs e)
    {
      if (!this.toolStrip1.Visible)
      {
      }
    }

    private void parentForm_Closing(object sender, CancelEventArgs e)
    {
      if (this.azukiControl1.Document.IsDirty)
      {
        string text = this.currentPath + "\n ファイルが変更されています。 編集中のテキストを保存します。\n\nよろしいですか?";
        string caption = "ファイルを閉じる";
        DialogResult dialogResult = MessageBox.Show(text, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        if (dialogResult == DialogResult.Yes)
        {
          this.上書き保存SToolStripMenuItem_Click(sender, e);
        }
        else if (dialogResult != DialogResult.No)
        {
          if (dialogResult == DialogResult.Cancel)
          {
            e.Cancel = true;
          }
        }
      }
#if Interface      
			this.settings.PreviousAzukiEditorDocuments = this.previousDocuments;
      this.settings.AzukiEditorMenuBarVisible = this.menuStrip1.Visible;
      this.settings.AzukiEditorToolBarVisible = this.toolStrip1.Visible;
      this.settings.AzukiEditorStatusBarVisible = this.statusStrip1.Visible;
      //this.settings.AzukiEditorDefaultFont = this.azukiControl1.Font;
#endif
    }

    public void AddPreviousDocuments(string data)
    {
      try
      {
        ToolStripMenuItem toolStripMenuItem = this.最近開いたファイルRToolStripMenuItem;
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
          ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
          toolStripMenuItem2.Click += new EventHandler(this.PreviousDocumentsMenuItem_Click);
          toolStripMenuItem2.Tag = (toolStripMenuItem2.Text = text);
          if (i < 15)
          {
            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
          }
          else
          {
            this.previousDocuments.Remove(text);
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
      this.currentPath = text;
      if (File.Exists(this.currentPath))// && MDIForm.CommonLibrary.Lib.IsTextFile(this.currentPath))
      {
        try
        {
          this.azukiControl1.Document.Tag = this.currentPath;
          this.azukiControl1.Document.Text = Lib.File_ReadToEndDecode(text);
          this.SetHighlight(this.currentPath);
          try
          {
            //#if Interface          
            //((DockContent)base.Parent).TabText = Path.GetFileName(text);
          }
          catch
          {
            //#else          
            //((Form)this.Parent).Text = Path.GetFileName(text);
            //#endif          
          }
          this.AddPreviousDocuments(text);
          this.UpdateStatusText(text);
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

    private void UpdateStatusText(string data)
    {
      int x = this.currentPoint.X;
      int y = this.currentPoint.Y;
      string format = " 行: {0} | 列: {1} | 改行コード: ({2}) | 文字コード: {3} | {4}";
      string text = Lib.File_GetEofCode(data);
      string text2 = Lib.File_GetCode(data);
      this.toolStripStatusLabel1.Text = string.Format(format, new object[] { y, x, text, text2, data });
    }

    public void LoadFile(string path)
    {
      try
      {
        if (path != String.Empty)
        {
          this.currentPath = path;
          this.SetHighlight(path);
          this.azukiControl1.Document.Text = Lib.File_ReadToEndDecode(path);
          this.azukiControl1.Document.IsDirty = false;
          this.azukiControl1.Tag = path;
          //((Form)this.Parent).Text = Path.GetFileName(path);
          this.AddPreviousDocuments(path);
          this.PopulatePreviousDocumentsMenu();
          this.UpdateStatusText(this.currentPath);
        }
      }
      catch (Exception ex)
      {
        string message = ex.Message.ToString();
        MessageBox.Show(Lib.OutputError(message));
      }
    }

    #endregion

    #region Form Variables
    public MenuStrip menuStrip1;
    private ToolStripMenuItem ファイルFToolStripMenuItem;
    private ToolStripMenuItem 新規作成NToolStripMenuItem;
    private ToolStripMenuItem 開くOToolStripMenuItem;
    private ToolStripMenuItem 最近開いたファイルRToolStripMenuItem;
    private ToolStripMenuItem 最近開いたファイルをクリアCToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem 読み取り専用ToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripMenuItem 上書き保存SToolStripMenuItem;
    private ToolStripMenuItem 名前を付けて保存AToolStripMenuItem;
    private ToolStripMenuItem 閉じるCToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem 印刷PToolStripMenuItem;
    private ToolStripMenuItem 印刷プレビューVToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator8;
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
    private ToolStripMenuItem 表示VToolStripMenuItem;
    private ToolStripMenuItem ツールバーTToolStripMenuItem;
    private ToolStripMenuItem ステータスバーUToolStripMenuItem;
    private ToolStripMenuItem updateUIToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator29;
    private ToolStripMenuItem 行番号NToolStripMenuItem;
    private ToolStripMenuItem ルーラLToolStripMenuItem;
    private ToolStripMenuItem カーソル行SToolStripMenuItem;
    private ToolStripMenuItem 特殊文字CToolStripMenuItem;
    private ToolStripMenuItem ワードラップWToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripMenuItem タブパネルToolStripMenuItem;
    private ToolStripMenuItem azukiContol1ToolStripMenuItem;
    private ToolStripMenuItem webBrowserToolStripMenuItem;
    private ToolStripMenuItem extendedWebBrowserToolStripMenuItem;
    private ToolStripMenuItem 強調表示YToolStripMenuItem;
    private ToolStripMenuItem textTToolStripMenuItem;
    private ToolStripMenuItem laTexLToolStripMenuItem;
    private ToolStripMenuItem cCCToolStripMenuItem;
    private ToolStripMenuItem cToolStripMenuItem;
    private ToolStripMenuItem javaJToolStripMenuItem;
    private ToolStripMenuItem rubyToolStripMenuItem;
    private ToolStripMenuItem xMLXToolStripMenuItem;
    private ToolStripMenuItem 検索SToolStripMenuItem1;
    private ToolStripMenuItem 検索FToolStripMenuItem;
    private ToolStripMenuItem 置換RToolStripMenuItem;
    private ToolStripMenuItem 行へ移動GToolStripMenuItem;
    private ToolStripMenuItem ビルドBToolStripMenuItem;
    private ToolStripMenuItem buildbatBToolStripMenuItem;
    private ToolStripMenuItem makefileMToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator15;
    private ToolStripMenuItem cCCToolStripMenuItem1;
    private ToolStripMenuItem vC606ToolStripMenuItem;
    private ToolStripMenuItem vC20088ToolStripMenuItem;
    private ToolStripMenuItem vC20101ToolStripMenuItem;
    private ToolStripMenuItem cToolStripMenuItem1;
    private ToolStripMenuItem vCS20088ToolStripMenuItem;
    private ToolStripMenuItem vCS20101ToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator26;
    private ToolStripMenuItem javaJToolStripMenuItem1;
    private ToolStripMenuItem flex4FToolStripMenuItem1;
    private ToolStripSeparator toolStripSeparator24;
    private ToolStripMenuItem laTexLToolStripMenuItem2;
    private ToolStripMenuItem 実行XToolStripMenuItem;
    private ToolStripMenuItem consoleアプリケーションCToolStripMenuItem;
    private ToolStripMenuItem スクリプトSToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator25;
    private ToolStripMenuItem javaToolStripMenuItem;
    private ToolStripMenuItem アプリケーションAToolStripMenuItem;
    private ToolStripMenuItem アプレットPToolStripMenuItem;
    private ToolStripMenuItem flex4FToolStripMenuItem;
    private ToolStripMenuItem adobeFlashPlayer90FToolStripMenuItem;
    private ToolStripMenuItem webBrowser1WToolStripMenuItem;
    private ToolStripMenuItem googleChromeCToolStripMenuItem;
    private ToolStripMenuItem laTexLToolStripMenuItem1;
    private ToolStripMenuItem dviout表示DToolStripMenuItem;
    private ToolStripMenuItem pdf表示PToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator27;
    private ToolStripMenuItem runtimeCompilationRToolStripMenuItem;
    private ToolStripMenuItem cSharpCodeProviderToolStripMenuItem;
    private ToolStripMenuItem jScriptCodeProvideJToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator14;
    private ToolStripMenuItem ウエブサーバで実行WToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator13;
    private ToolStripMenuItem ファイル名を指定して実行OToolStripMenuItem1;
    public ToolStripMenuItem スクリプトCToolStripMenuItem;
    private ToolStripMenuItem スクリプトを実行XToolStripMenuItem;
    private ToolStripMenuItem スクリプトを編集EToolStripMenuItem;
    private ToolStripMenuItem スクリプトメニュー更新RToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator28;
    private ToolStripMenuItem ツールTToolStripMenuItem;
    private ToolStripMenuItem カスタマイズCToolStripMenuItem;
    private ToolStripMenuItem フォントと色ToolStripMenuItem;
    private ToolStripMenuItem 右端で折り返すToolStripMenuItem;
    private ToolStripMenuItem オプションOToolStripMenuItem;
    private ToolStripMenuItem azukiタブを固定ToolStripMenuItem;
    private ToolStripMenuItem browserタブを固定ToolStripMenuItem;
    private ToolStripMenuItem 挿入ToolStripMenuItem;
    private ToolStripMenuItem タイムスタンプToolStripMenuItem;
    private ToolStripMenuItem c見出しToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator12;
    private ToolStripMenuItem tODOリストToolStripMenuItem;
    private ToolStripMenuItem 新規作成ToolStripMenuItem;
    private ToolStripMenuItem 開くToolStripMenuItem;
    private ToolStripMenuItem 上書き保存ToolStripMenuItem;
    private ToolStripMenuItem 名前を付けて保存ToolStripMenuItem;
    private ToolStripMenuItem ヘルプHToolStripMenuItem;
    private ToolStripMenuItem azukiDocumentToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator10;
    private ToolStripMenuItem 内容CToolStripMenuItem;
    private ToolStripMenuItem インデックスIToolStripMenuItem;
    private ToolStripMenuItem 検索SToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem バージョン情報AToolStripMenuItem;
    public ToolStrip toolStrip1;
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
    private ToolStripDropDownButton toolStripDropDownButton4;
    private ToolStripMenuItem メニューバーMToolStripMenuItem;
    private ToolStripMenuItem ステータスバーSToolStripMenuItem1;
    private ToolStripSeparator toolStripSeparator11;
    private ToolStripDropDownButton toolStripDropDownButton1;
    private ToolStripMenuItem アプリケーションAToolStripMenuItem1;
    private ToolStripMenuItem スクリプトSToolStripMenuItem1;
    private ToolStripMenuItem jAVAToolStripMenuItem1;
    private ToolStripMenuItem アプリケーションAToolStripMenuItem2;
    private ToolStripMenuItem アプレットPToolStripMenuItem1;
    private ToolStripSeparator toolStripSeparator21;
    private ToolStripMenuItem ウエブサーバで実行WToolStripMenuItem1;
    private ToolStripDropDownButton toolStripDropDownButton2;
    private ToolStripMenuItem buildbatBToolStripMenuItem1;
    private ToolStripMenuItem makefileMToolStripMenuItem1;
    private ToolStripSeparator toolStripSeparator22;
    private ToolStripMenuItem cCCToolStripMenuItem2;
    private ToolStripMenuItem vC606ToolStripMenuItem1;
    private ToolStripMenuItem vC20088ToolStripMenuItem1;
    private ToolStripMenuItem vC20101ToolStripMenuItem1;
    private ToolStripMenuItem cToolStripMenuItem2;
    private ToolStripMenuItem vC20088ToolStripMenuItem2;
    private ToolStripMenuItem vC20101ToolStripMenuItem2;
    private ToolStripSeparator toolStripSeparator23;
    private ToolStripMenuItem jAVAJToolStripMenuItem2;
    private ToolStripDropDownButton toolStripDropDownButton3;
    private ToolStripMenuItem サクラエディタSToolStripMenuItem;
    private ToolStripMenuItem pSPadPToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator16;
    private ToolStripMenuItem scintillaCToolStripMenuItem;
    private ToolStripMenuItem richTextBoxToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator17;
    private ToolStripMenuItem エクスプローラEToolStripMenuItem;
    private ToolStripMenuItem コマンドプロンプトCToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator18;
    private ToolStripMenuItem ファイル名を指定して実行OToolStripMenuItem;
    private ToolStripMenuItem リンクを開くLToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator20;
    public StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
    #endregion

    #region Windows フォーム デザイナで生成されたコード
    /// <summary>
    /// 必要なデザイナ変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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


    /// <summary>
    /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
    /// コード エディタで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AzukiEditor));
      Sgry.Azuki.FontInfo fontInfo9 = new Sgry.Azuki.FontInfo();
      Sgry.Azuki.FontInfo fontInfo10 = new Sgry.Azuki.FontInfo();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.新規作成NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.最近開いたファイルRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.最近開いたファイルをクリアCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.読み取り専用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.上書き保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.名前を付けて保存AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.閉じるCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.印刷PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.印刷プレビューVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
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
      this.ステータスバーUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.updateUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
      this.行番号NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ルーラLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.カーソル行SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.特殊文字CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ワードラップWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.タブパネルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.azukiContol1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.webBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.extendedWebBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.強調表示YToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.textTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.laTexLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cCCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.javaJToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.rubyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.xMLXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.検索SToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.検索FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.置換RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.行へ移動GToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ビルドBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.buildbatBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.makefileMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
      this.cCCToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.vC606ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.vC20088ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.vC20101ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.vCS20088ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.vCS20101ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
      this.javaJToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.flex4FToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
      this.laTexLToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.実行XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.consoleアプリケーションCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
      this.javaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.アプリケーションAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.アプレットPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.flex4FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.adobeFlashPlayer90FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.webBrowser1WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.googleChromeCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.laTexLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.dviout表示DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pdf表示PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
      this.runtimeCompilationRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cSharpCodeProviderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.jScriptCodeProvideJToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
      this.ウエブサーバで実行WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.ファイル名を指定して実行OToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトを実行XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトを編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトメニュー更新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
      this.ツールTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.カスタマイズCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.フォントと色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.右端で折り返すToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.オプションOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.azukiタブを固定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.browserタブを固定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.挿入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.タイムスタンプToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.c見出しToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.tODOリストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.新規作成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.上書き保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.名前を付けて保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.azukiDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
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
      this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
      this.メニューバーMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ステータスバーSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.アプリケーションAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.jAVAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.アプリケーションAToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.アプレットPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
      this.ウエブサーバで実行WToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
      this.buildbatBToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.makefileMToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
      this.cCCToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.vC606ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.vC20088ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.vC20101ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.cToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.vC20088ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.vC20101ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
      this.jAVAJToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
      this.サクラエディタSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pSPadPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
      this.scintillaCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.richTextBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
      this.エクスプローラEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.コマンドプロンプトCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
      this.ファイル名を指定して実行OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.リンクを開くLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
      this.imageListButton = new System.Windows.Forms.ToolStripButton();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.azukiControl1 = new Sgry.Azuki.WinForms.AzukiControl();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.azukiControl2 = new Sgry.Azuki.WinForms.AzukiControl();
      this.コンソールSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.表示VToolStripMenuItem,
            this.強調表示YToolStripMenuItem,
            this.検索SToolStripMenuItem1,
            this.ビルドBToolStripMenuItem,
            this.実行XToolStripMenuItem,
            this.スクリプトCToolStripMenuItem,
            this.ツールTToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
      this.menuStrip1.Size = new System.Drawing.Size(1361, 33);
      this.menuStrip1.TabIndex = 2;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // ファイルFToolStripMenuItem
      // 
      this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成NToolStripMenuItem,
            this.開くOToolStripMenuItem,
            this.最近開いたファイルRToolStripMenuItem,
            this.toolStripSeparator2,
            this.読み取り専用ToolStripMenuItem,
            this.toolStripSeparator,
            this.上書き保存SToolStripMenuItem,
            this.名前を付けて保存AToolStripMenuItem,
            this.閉じるCToolStripMenuItem,
            this.toolStripSeparator1,
            this.印刷PToolStripMenuItem,
            this.印刷プレビューVToolStripMenuItem,
            this.toolStripSeparator8,
            this.終了XToolStripMenuItem});
      this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
      this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(109, 29);
      this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
      // 
      // 新規作成NToolStripMenuItem
      // 
      this.新規作成NToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新規作成NToolStripMenuItem.Image")));
      this.新規作成NToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.新規作成NToolStripMenuItem.Name = "新規作成NToolStripMenuItem";
      this.新規作成NToolStripMenuItem.Size = new System.Drawing.Size(270, 30);
      this.新規作成NToolStripMenuItem.Text = "新規作成(&N)";
      this.新規作成NToolStripMenuItem.Click += new System.EventHandler(this.新規作成NToolStripMenuItem_Click);
      // 
      // 開くOToolStripMenuItem
      // 
      this.開くOToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripMenuItem.Image")));
      this.開くOToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
      this.開くOToolStripMenuItem.Size = new System.Drawing.Size(270, 30);
      this.開くOToolStripMenuItem.Text = "開く(&O)";
      this.開くOToolStripMenuItem.Click += new System.EventHandler(this.開くOToolStripMenuItem_Click);
      // 
      // 最近開いたファイルRToolStripMenuItem
      // 
      this.最近開いたファイルRToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最近開いたファイルをクリアCToolStripMenuItem});
      this.最近開いたファイルRToolStripMenuItem.Name = "最近開いたファイルRToolStripMenuItem";
      this.最近開いたファイルRToolStripMenuItem.Size = new System.Drawing.Size(270, 30);
      this.最近開いたファイルRToolStripMenuItem.Text = "最近開いたファイル(&R)";
      // 
      // 最近開いたファイルをクリアCToolStripMenuItem
      // 
      this.最近開いたファイルをクリアCToolStripMenuItem.Name = "最近開いたファイルをクリアCToolStripMenuItem";
      this.最近開いたファイルをクリアCToolStripMenuItem.Size = new System.Drawing.Size(325, 30);
      this.最近開いたファイルをクリアCToolStripMenuItem.Text = "最近開いたファイルをクリア(&C)";
      this.最近開いたファイルをクリアCToolStripMenuItem.Click += new System.EventHandler(this.最近開いたファイルをクリアCToolStripMenuItem_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(267, 6);
      // 
      // 読み取り専用ToolStripMenuItem
      // 
      this.読み取り専用ToolStripMenuItem.CheckOnClick = true;
      this.読み取り専用ToolStripMenuItem.MergeIndex = 2;
      this.読み取り専用ToolStripMenuItem.Name = "読み取り専用ToolStripMenuItem";
      this.読み取り専用ToolStripMenuItem.Size = new System.Drawing.Size(270, 30);
      this.読み取り専用ToolStripMenuItem.Text = "読み取り専用";
      this.読み取り専用ToolStripMenuItem.Click += new System.EventHandler(this.読み取り専用ToolStripMenuItem_Click);
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(267, 6);
      // 
      // 上書き保存SToolStripMenuItem
      // 
      this.上書き保存SToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("上書き保存SToolStripMenuItem.Image")));
      this.上書き保存SToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.上書き保存SToolStripMenuItem.Name = "上書き保存SToolStripMenuItem";
      this.上書き保存SToolStripMenuItem.Size = new System.Drawing.Size(270, 30);
      this.上書き保存SToolStripMenuItem.Text = "上書き保存(&S)";
      this.上書き保存SToolStripMenuItem.Click += new System.EventHandler(this.上書き保存SToolStripMenuItem_Click);
      // 
      // 名前を付けて保存AToolStripMenuItem
      // 
      this.名前を付けて保存AToolStripMenuItem.Name = "名前を付けて保存AToolStripMenuItem";
      this.名前を付けて保存AToolStripMenuItem.Size = new System.Drawing.Size(270, 30);
      this.名前を付けて保存AToolStripMenuItem.Text = "名前を付けて保存(&A)";
      this.名前を付けて保存AToolStripMenuItem.Click += new System.EventHandler(this.名前を付けて保存AToolStripMenuItem_Click);
      // 
      // 閉じるCToolStripMenuItem
      // 
      this.閉じるCToolStripMenuItem.Name = "閉じるCToolStripMenuItem";
      this.閉じるCToolStripMenuItem.Size = new System.Drawing.Size(270, 30);
      this.閉じるCToolStripMenuItem.Text = "閉じる(&C)";
      this.閉じるCToolStripMenuItem.Click += new System.EventHandler(this.閉じるCToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(267, 6);
      // 
      // 印刷PToolStripMenuItem
      // 
      this.印刷PToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripMenuItem.Image")));
      this.印刷PToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷PToolStripMenuItem.Name = "印刷PToolStripMenuItem";
      this.印刷PToolStripMenuItem.Size = new System.Drawing.Size(270, 30);
      this.印刷PToolStripMenuItem.Text = "印刷(&P)";
      this.印刷PToolStripMenuItem.Click += new System.EventHandler(this.印刷PToolStripMenuItem_Click);
      // 
      // 印刷プレビューVToolStripMenuItem
      // 
      this.印刷プレビューVToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("印刷プレビューVToolStripMenuItem.Image")));
      this.印刷プレビューVToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷プレビューVToolStripMenuItem.Name = "印刷プレビューVToolStripMenuItem";
      this.印刷プレビューVToolStripMenuItem.Size = new System.Drawing.Size(270, 30);
      this.印刷プレビューVToolStripMenuItem.Text = "印刷プレビュー(&V)";
      this.印刷プレビューVToolStripMenuItem.Click += new System.EventHandler(this.印刷プレビューVToolStripMenuItem_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(267, 6);
      // 
      // 終了XToolStripMenuItem
      // 
      this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
      this.終了XToolStripMenuItem.Size = new System.Drawing.Size(270, 30);
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
      this.編集EToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
      this.編集EToolStripMenuItem.Size = new System.Drawing.Size(94, 29);
      this.編集EToolStripMenuItem.Text = "編集(&E)";
      // 
      // 元に戻すUToolStripMenuItem
      // 
      this.元に戻すUToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(199)))), ((int)(((byte)(198)))));
      this.元に戻すUToolStripMenuItem.Name = "元に戻すUToolStripMenuItem";
      this.元に戻すUToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
      this.元に戻すUToolStripMenuItem.Text = "元に戻す(&U)";
      this.元に戻すUToolStripMenuItem.Click += new System.EventHandler(this.元に戻すUToolStripMenuItem_Click);
      // 
      // やり直しRToolStripMenuItem
      // 
      this.やり直しRToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(199)))), ((int)(((byte)(198)))));
      this.やり直しRToolStripMenuItem.Name = "やり直しRToolStripMenuItem";
      this.やり直しRToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
      this.やり直しRToolStripMenuItem.Text = "やり直し(&R)";
      this.やり直しRToolStripMenuItem.Click += new System.EventHandler(this.やり直しRToolStripMenuItem_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(208, 6);
      // 
      // 切り取りTToolStripMenuItem
      // 
      this.切り取りTToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("切り取りTToolStripMenuItem.Image")));
      this.切り取りTToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.切り取りTToolStripMenuItem.Name = "切り取りTToolStripMenuItem";
      this.切り取りTToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
      this.切り取りTToolStripMenuItem.Text = "切り取り(&T)";
      this.切り取りTToolStripMenuItem.Click += new System.EventHandler(this.切り取りTToolStripMenuItem_Click);
      // 
      // コピーCToolStripMenuItem
      // 
      this.コピーCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripMenuItem.Image")));
      this.コピーCToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.コピーCToolStripMenuItem.Name = "コピーCToolStripMenuItem";
      this.コピーCToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
      this.コピーCToolStripMenuItem.Text = "コピー(&C)";
      this.コピーCToolStripMenuItem.Click += new System.EventHandler(this.コピーCToolStripMenuItem_Click);
      // 
      // 貼り付けPToolStripMenuItem
      // 
      this.貼り付けPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripMenuItem.Image")));
      this.貼り付けPToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.貼り付けPToolStripMenuItem.Name = "貼り付けPToolStripMenuItem";
      this.貼り付けPToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
      this.貼り付けPToolStripMenuItem.Text = "貼り付け(&P)";
      this.貼り付けPToolStripMenuItem.Click += new System.EventHandler(this.貼り付けPToolStripMenuItem_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(208, 6);
      // 
      // すべて選択AToolStripMenuItem
      // 
      this.すべて選択AToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(199)))), ((int)(((byte)(198)))));
      this.すべて選択AToolStripMenuItem.Name = "すべて選択AToolStripMenuItem";
      this.すべて選択AToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
      this.すべて選択AToolStripMenuItem.Text = "すべて選択(&A)";
      this.すべて選択AToolStripMenuItem.Click += new System.EventHandler(this.すべて選択AToolStripMenuItem_Click);
      // 
      // 表示VToolStripMenuItem
      // 
      this.表示VToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ツールバーTToolStripMenuItem,
            this.ステータスバーUToolStripMenuItem,
            this.updateUIToolStripMenuItem,
            this.toolStripSeparator29,
            this.行番号NToolStripMenuItem,
            this.ルーラLToolStripMenuItem,
            this.カーソル行SToolStripMenuItem,
            this.特殊文字CToolStripMenuItem,
            this.ワードラップWToolStripMenuItem,
            this.toolStripSeparator9,
            this.タブパネルToolStripMenuItem});
      this.表示VToolStripMenuItem.Name = "表示VToolStripMenuItem";
      this.表示VToolStripMenuItem.Size = new System.Drawing.Size(96, 29);
      this.表示VToolStripMenuItem.Text = "表示(&V)";
      // 
      // ツールバーTToolStripMenuItem
      // 
      this.ツールバーTToolStripMenuItem.CheckOnClick = true;
      this.ツールバーTToolStripMenuItem.Name = "ツールバーTToolStripMenuItem";
      this.ツールバーTToolStripMenuItem.Size = new System.Drawing.Size(231, 30);
      this.ツールバーTToolStripMenuItem.Text = "ツールバー(&T)";
      this.ツールバーTToolStripMenuItem.Click += new System.EventHandler(this.ツールバーTToolStripMenuItem_Click);
      // 
      // ステータスバーUToolStripMenuItem
      // 
      this.ステータスバーUToolStripMenuItem.CheckOnClick = true;
      this.ステータスバーUToolStripMenuItem.Name = "ステータスバーUToolStripMenuItem";
      this.ステータスバーUToolStripMenuItem.Size = new System.Drawing.Size(231, 30);
      this.ステータスバーUToolStripMenuItem.Text = "ステータスバー(&U)";
      this.ステータスバーUToolStripMenuItem.Click += new System.EventHandler(this.ステータスバーUToolStripMenuItem_Click);
      // 
      // updateUIToolStripMenuItem
      // 
      this.updateUIToolStripMenuItem.Enabled = false;
      this.updateUIToolStripMenuItem.Name = "updateUIToolStripMenuItem";
      this.updateUIToolStripMenuItem.Size = new System.Drawing.Size(231, 30);
      this.updateUIToolStripMenuItem.Text = "UpdateUI";
      // 
      // toolStripSeparator29
      // 
      this.toolStripSeparator29.Name = "toolStripSeparator29";
      this.toolStripSeparator29.Size = new System.Drawing.Size(228, 6);
      // 
      // 行番号NToolStripMenuItem
      // 
      this.行番号NToolStripMenuItem.Checked = true;
      this.行番号NToolStripMenuItem.CheckOnClick = true;
      this.行番号NToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.行番号NToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.行番号NToolStripMenuItem.MergeIndex = 0;
      this.行番号NToolStripMenuItem.Name = "行番号NToolStripMenuItem";
      this.行番号NToolStripMenuItem.Size = new System.Drawing.Size(231, 30);
      this.行番号NToolStripMenuItem.Text = "行番号(&N)";
      this.行番号NToolStripMenuItem.Click += new System.EventHandler(this.行番号NToolStripMenuItem_Click);
      // 
      // ルーラLToolStripMenuItem
      // 
      this.ルーラLToolStripMenuItem.Checked = true;
      this.ルーラLToolStripMenuItem.CheckOnClick = true;
      this.ルーラLToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ルーラLToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.ルーラLToolStripMenuItem.MergeIndex = 1;
      this.ルーラLToolStripMenuItem.Name = "ルーラLToolStripMenuItem";
      this.ルーラLToolStripMenuItem.Size = new System.Drawing.Size(231, 30);
      this.ルーラLToolStripMenuItem.Text = "ルーラ(&L)";
      this.ルーラLToolStripMenuItem.Click += new System.EventHandler(this.ルーラLToolStripMenuItem_Click);
      // 
      // カーソル行SToolStripMenuItem
      // 
      this.カーソル行SToolStripMenuItem.Checked = true;
      this.カーソル行SToolStripMenuItem.CheckOnClick = true;
      this.カーソル行SToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.カーソル行SToolStripMenuItem.Name = "カーソル行SToolStripMenuItem";
      this.カーソル行SToolStripMenuItem.Size = new System.Drawing.Size(231, 30);
      this.カーソル行SToolStripMenuItem.Text = "カーソル行(&S)";
      this.カーソル行SToolStripMenuItem.Click += new System.EventHandler(this.カーソル行SToolStripMenuItem_Click);
      // 
      // 特殊文字CToolStripMenuItem
      // 
      this.特殊文字CToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.特殊文字CToolStripMenuItem.MergeIndex = 2;
      this.特殊文字CToolStripMenuItem.Name = "特殊文字CToolStripMenuItem";
      this.特殊文字CToolStripMenuItem.Size = new System.Drawing.Size(231, 30);
      this.特殊文字CToolStripMenuItem.Text = "特殊文字(&C)";
      this.特殊文字CToolStripMenuItem.Click += new System.EventHandler(this.特殊文字CToolStripMenuItem_Click);
      // 
      // ワードラップWToolStripMenuItem
      // 
      this.ワードラップWToolStripMenuItem.Checked = true;
      this.ワードラップWToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ワードラップWToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.ワードラップWToolStripMenuItem.MergeIndex = 3;
      this.ワードラップWToolStripMenuItem.Name = "ワードラップWToolStripMenuItem";
      this.ワードラップWToolStripMenuItem.Size = new System.Drawing.Size(231, 30);
      this.ワードラップWToolStripMenuItem.Text = "ワード・ラップ(&W)";
      this.ワードラップWToolStripMenuItem.Click += new System.EventHandler(this.ワードラップWToolStripMenuItem_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.toolStripSeparator9.MergeIndex = 4;
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(228, 6);
      // 
      // タブパネルToolStripMenuItem
      // 
      this.タブパネルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.azukiContol1ToolStripMenuItem,
            this.webBrowserToolStripMenuItem,
            this.extendedWebBrowserToolStripMenuItem});
      this.タブパネルToolStripMenuItem.Enabled = false;
      this.タブパネルToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.タブパネルToolStripMenuItem.MergeIndex = 5;
      this.タブパネルToolStripMenuItem.Name = "タブパネルToolStripMenuItem";
      this.タブパネルToolStripMenuItem.Size = new System.Drawing.Size(231, 30);
      this.タブパネルToolStripMenuItem.Text = "タブパネル";
      // 
      // azukiContol1ToolStripMenuItem
      // 
      this.azukiContol1ToolStripMenuItem.Checked = true;
      this.azukiContol1ToolStripMenuItem.CheckOnClick = true;
      this.azukiContol1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.azukiContol1ToolStripMenuItem.Name = "azukiContol1ToolStripMenuItem";
      this.azukiContol1ToolStripMenuItem.Size = new System.Drawing.Size(304, 30);
      this.azukiContol1ToolStripMenuItem.Text = "azukiContol1";
      // 
      // webBrowserToolStripMenuItem
      // 
      this.webBrowserToolStripMenuItem.Checked = true;
      this.webBrowserToolStripMenuItem.CheckOnClick = true;
      this.webBrowserToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.webBrowserToolStripMenuItem.Name = "webBrowserToolStripMenuItem";
      this.webBrowserToolStripMenuItem.Size = new System.Drawing.Size(304, 30);
      this.webBrowserToolStripMenuItem.Text = "webBrowser";
      // 
      // extendedWebBrowserToolStripMenuItem
      // 
      this.extendedWebBrowserToolStripMenuItem.Checked = true;
      this.extendedWebBrowserToolStripMenuItem.CheckOnClick = true;
      this.extendedWebBrowserToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.extendedWebBrowserToolStripMenuItem.Name = "extendedWebBrowserToolStripMenuItem";
      this.extendedWebBrowserToolStripMenuItem.Size = new System.Drawing.Size(304, 30);
      this.extendedWebBrowserToolStripMenuItem.Text = "extendedWebBrowser";
      // 
      // 強調表示YToolStripMenuItem
      // 
      this.強調表示YToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textTToolStripMenuItem,
            this.laTexLToolStripMenuItem,
            this.cCCToolStripMenuItem,
            this.cToolStripMenuItem,
            this.javaJToolStripMenuItem,
            this.rubyToolStripMenuItem,
            this.xMLXToolStripMenuItem});
      this.強調表示YToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.強調表示YToolStripMenuItem.MergeIndex = 3;
      this.強調表示YToolStripMenuItem.Name = "強調表示YToolStripMenuItem";
      this.強調表示YToolStripMenuItem.Size = new System.Drawing.Size(135, 29);
      this.強調表示YToolStripMenuItem.Text = "強調表示(&Y)";
      // 
      // textTToolStripMenuItem
      // 
      this.textTToolStripMenuItem.Name = "textTToolStripMenuItem";
      this.textTToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
      this.textTToolStripMenuItem.Text = "Text(&T)";
      this.textTToolStripMenuItem.Click += new System.EventHandler(this.textTToolStripMenuItem_Click);
      // 
      // laTexLToolStripMenuItem
      // 
      this.laTexLToolStripMenuItem.Name = "laTexLToolStripMenuItem";
      this.laTexLToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
      this.laTexLToolStripMenuItem.Text = "LaTex(&L)";
      this.laTexLToolStripMenuItem.Click += new System.EventHandler(this.laTexLToolStripMenuItem_Click);
      // 
      // cCCToolStripMenuItem
      // 
      this.cCCToolStripMenuItem.Name = "cCCToolStripMenuItem";
      this.cCCToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
      this.cCCToolStripMenuItem.Text = "C/C++(&C)";
      this.cCCToolStripMenuItem.Click += new System.EventHandler(this.cCCToolStripMenuItem_Click);
      // 
      // cToolStripMenuItem
      // 
      this.cToolStripMenuItem.Name = "cToolStripMenuItem";
      this.cToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
      this.cToolStripMenuItem.Text = "C#(&#)";
      this.cToolStripMenuItem.Click += new System.EventHandler(this.cToolStripMenuItem_Click);
      // 
      // javaJToolStripMenuItem
      // 
      this.javaJToolStripMenuItem.Name = "javaJToolStripMenuItem";
      this.javaJToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
      this.javaJToolStripMenuItem.Text = "Java(&J)";
      this.javaJToolStripMenuItem.Click += new System.EventHandler(this.javaJToolStripMenuItem_Click);
      // 
      // rubyToolStripMenuItem
      // 
      this.rubyToolStripMenuItem.Name = "rubyToolStripMenuItem";
      this.rubyToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
      this.rubyToolStripMenuItem.Text = "Ruby(&R)";
      this.rubyToolStripMenuItem.Click += new System.EventHandler(this.rubyToolStripMenuItem_Click);
      // 
      // xMLXToolStripMenuItem
      // 
      this.xMLXToolStripMenuItem.Name = "xMLXToolStripMenuItem";
      this.xMLXToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
      this.xMLXToolStripMenuItem.Text = "XML(&X)";
      this.xMLXToolStripMenuItem.Click += new System.EventHandler(this.xMLXToolStripMenuItem_Click);
      // 
      // 検索SToolStripMenuItem1
      // 
      this.検索SToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.検索FToolStripMenuItem,
            this.置換RToolStripMenuItem,
            this.行へ移動GToolStripMenuItem});
      this.検索SToolStripMenuItem1.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.検索SToolStripMenuItem1.MergeIndex = 4;
      this.検索SToolStripMenuItem1.Name = "検索SToolStripMenuItem1";
      this.検索SToolStripMenuItem1.Size = new System.Drawing.Size(95, 29);
      this.検索SToolStripMenuItem1.Text = "検索(&S)";
      // 
      // 検索FToolStripMenuItem
      // 
      this.検索FToolStripMenuItem.Name = "検索FToolStripMenuItem";
      this.検索FToolStripMenuItem.Size = new System.Drawing.Size(220, 30);
      this.検索FToolStripMenuItem.Text = "検索(&F)...";
      this.検索FToolStripMenuItem.Click += new System.EventHandler(this.検索FToolStripMenuItem_Click);
      // 
      // 置換RToolStripMenuItem
      // 
      this.置換RToolStripMenuItem.Name = "置換RToolStripMenuItem";
      this.置換RToolStripMenuItem.Size = new System.Drawing.Size(220, 30);
      this.置換RToolStripMenuItem.Text = "置換(&R)...";
      this.置換RToolStripMenuItem.Click += new System.EventHandler(this.置換RToolStripMenuItem_Click);
      // 
      // 行へ移動GToolStripMenuItem
      // 
      this.行へ移動GToolStripMenuItem.Name = "行へ移動GToolStripMenuItem";
      this.行へ移動GToolStripMenuItem.Size = new System.Drawing.Size(220, 30);
      this.行へ移動GToolStripMenuItem.Text = "行へ移動(&G)...";
      this.行へ移動GToolStripMenuItem.Click += new System.EventHandler(this.行へ移動GToolStripMenuItem_Click);
      // 
      // ビルドBToolStripMenuItem
      // 
      this.ビルドBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildbatBToolStripMenuItem,
            this.makefileMToolStripMenuItem,
            this.toolStripSeparator15,
            this.cCCToolStripMenuItem1,
            this.cToolStripMenuItem1,
            this.toolStripSeparator26,
            this.javaJToolStripMenuItem1,
            this.flex4FToolStripMenuItem1,
            this.toolStripSeparator24,
            this.laTexLToolStripMenuItem2});
      this.ビルドBToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.ビルドBToolStripMenuItem.MergeIndex = 5;
      this.ビルドBToolStripMenuItem.Name = "ビルドBToolStripMenuItem";
      this.ビルドBToolStripMenuItem.Size = new System.Drawing.Size(99, 29);
      this.ビルドBToolStripMenuItem.Text = "ビルド(&B)";
      // 
      // buildbatBToolStripMenuItem
      // 
      this.buildbatBToolStripMenuItem.Name = "buildbatBToolStripMenuItem";
      this.buildbatBToolStripMenuItem.Size = new System.Drawing.Size(206, 30);
      this.buildbatBToolStripMenuItem.Text = "build.bat(&B)";
      // 
      // makefileMToolStripMenuItem
      // 
      this.makefileMToolStripMenuItem.Name = "makefileMToolStripMenuItem";
      this.makefileMToolStripMenuItem.Size = new System.Drawing.Size(206, 30);
      this.makefileMToolStripMenuItem.Text = "Makefile(&M)";
      // 
      // toolStripSeparator15
      // 
      this.toolStripSeparator15.Name = "toolStripSeparator15";
      this.toolStripSeparator15.Size = new System.Drawing.Size(203, 6);
      // 
      // cCCToolStripMenuItem1
      // 
      this.cCCToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vC606ToolStripMenuItem,
            this.vC20088ToolStripMenuItem,
            this.vC20101ToolStripMenuItem});
      this.cCCToolStripMenuItem1.Name = "cCCToolStripMenuItem1";
      this.cCCToolStripMenuItem1.Size = new System.Drawing.Size(206, 30);
      this.cCCToolStripMenuItem1.Text = "C/C++(&C)";
      // 
      // vC606ToolStripMenuItem
      // 
      this.vC606ToolStripMenuItem.Name = "vC606ToolStripMenuItem";
      this.vC606ToolStripMenuItem.Size = new System.Drawing.Size(195, 30);
      this.vC606ToolStripMenuItem.Text = "VC6,.0(&6)";
      // 
      // vC20088ToolStripMenuItem
      // 
      this.vC20088ToolStripMenuItem.Name = "vC20088ToolStripMenuItem";
      this.vC20088ToolStripMenuItem.Size = new System.Drawing.Size(195, 30);
      this.vC20088ToolStripMenuItem.Text = "VC2008(&8)";
      // 
      // vC20101ToolStripMenuItem
      // 
      this.vC20101ToolStripMenuItem.Name = "vC20101ToolStripMenuItem";
      this.vC20101ToolStripMenuItem.Size = new System.Drawing.Size(195, 30);
      this.vC20101ToolStripMenuItem.Text = "VC2010(&1)";
      // 
      // cToolStripMenuItem1
      // 
      this.cToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vCS20088ToolStripMenuItem,
            this.vCS20101ToolStripMenuItem});
      this.cToolStripMenuItem1.Name = "cToolStripMenuItem1";
      this.cToolStripMenuItem1.Size = new System.Drawing.Size(206, 30);
      this.cToolStripMenuItem1.Text = "C#(&#)";
      // 
      // vCS20088ToolStripMenuItem
      // 
      this.vCS20088ToolStripMenuItem.Name = "vCS20088ToolStripMenuItem";
      this.vCS20088ToolStripMenuItem.Size = new System.Drawing.Size(208, 30);
      this.vCS20088ToolStripMenuItem.Text = "VCS2008(&8)";
      // 
      // vCS20101ToolStripMenuItem
      // 
      this.vCS20101ToolStripMenuItem.Name = "vCS20101ToolStripMenuItem";
      this.vCS20101ToolStripMenuItem.Size = new System.Drawing.Size(208, 30);
      this.vCS20101ToolStripMenuItem.Text = "VCS2010(&1)";
      // 
      // toolStripSeparator26
      // 
      this.toolStripSeparator26.Name = "toolStripSeparator26";
      this.toolStripSeparator26.Size = new System.Drawing.Size(203, 6);
      // 
      // javaJToolStripMenuItem1
      // 
      this.javaJToolStripMenuItem1.Name = "javaJToolStripMenuItem1";
      this.javaJToolStripMenuItem1.Size = new System.Drawing.Size(206, 30);
      this.javaJToolStripMenuItem1.Text = "Java(&J)";
      // 
      // flex4FToolStripMenuItem1
      // 
      this.flex4FToolStripMenuItem1.Name = "flex4FToolStripMenuItem1";
      this.flex4FToolStripMenuItem1.Size = new System.Drawing.Size(206, 30);
      this.flex4FToolStripMenuItem1.Text = "Flex4(&F)";
      // 
      // toolStripSeparator24
      // 
      this.toolStripSeparator24.Name = "toolStripSeparator24";
      this.toolStripSeparator24.Size = new System.Drawing.Size(203, 6);
      // 
      // laTexLToolStripMenuItem2
      // 
      this.laTexLToolStripMenuItem2.Name = "laTexLToolStripMenuItem2";
      this.laTexLToolStripMenuItem2.Size = new System.Drawing.Size(206, 30);
      this.laTexLToolStripMenuItem2.Text = "LaTex(&L)";
      // 
      // 実行XToolStripMenuItem
      // 
      this.実行XToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consoleアプリケーションCToolStripMenuItem,
            this.スクリプトSToolStripMenuItem,
            this.toolStripSeparator25,
            this.javaToolStripMenuItem,
            this.flex4FToolStripMenuItem,
            this.laTexLToolStripMenuItem1,
            this.toolStripSeparator27,
            this.runtimeCompilationRToolStripMenuItem,
            this.toolStripSeparator14,
            this.ウエブサーバで実行WToolStripMenuItem,
            this.toolStripSeparator13,
            this.ファイル名を指定して実行OToolStripMenuItem1});
      this.実行XToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.実行XToolStripMenuItem.MergeIndex = 6;
      this.実行XToolStripMenuItem.Name = "実行XToolStripMenuItem";
      this.実行XToolStripMenuItem.Size = new System.Drawing.Size(95, 29);
      this.実行XToolStripMenuItem.Text = "実行(&X)";
      // 
      // consoleアプリケーションCToolStripMenuItem
      // 
      this.consoleアプリケーションCToolStripMenuItem.Name = "consoleアプリケーションCToolStripMenuItem";
      this.consoleアプリケーションCToolStripMenuItem.Size = new System.Drawing.Size(345, 30);
      this.consoleアプリケーションCToolStripMenuItem.Text = "アプリケーション(&A)";
      // 
      // スクリプトSToolStripMenuItem
      // 
      this.スクリプトSToolStripMenuItem.Name = "スクリプトSToolStripMenuItem";
      this.スクリプトSToolStripMenuItem.Size = new System.Drawing.Size(345, 30);
      this.スクリプトSToolStripMenuItem.Text = "スクリプト(&S)";
      this.スクリプトSToolStripMenuItem.Click += new System.EventHandler(this.スクリプトSToolStripMenuItem_Click);
      // 
      // toolStripSeparator25
      // 
      this.toolStripSeparator25.Name = "toolStripSeparator25";
      this.toolStripSeparator25.Size = new System.Drawing.Size(342, 6);
      // 
      // javaToolStripMenuItem
      // 
      this.javaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリケーションAToolStripMenuItem,
            this.アプレットPToolStripMenuItem});
      this.javaToolStripMenuItem.Name = "javaToolStripMenuItem";
      this.javaToolStripMenuItem.Size = new System.Drawing.Size(345, 30);
      this.javaToolStripMenuItem.Text = "Java(&J)";
      // 
      // アプリケーションAToolStripMenuItem
      // 
      this.アプリケーションAToolStripMenuItem.Name = "アプリケーションAToolStripMenuItem";
      this.アプリケーションAToolStripMenuItem.Size = new System.Drawing.Size(237, 30);
      this.アプリケーションAToolStripMenuItem.Text = "アプリケーション(&A)";
      // 
      // アプレットPToolStripMenuItem
      // 
      this.アプレットPToolStripMenuItem.Name = "アプレットPToolStripMenuItem";
      this.アプレットPToolStripMenuItem.Size = new System.Drawing.Size(237, 30);
      this.アプレットPToolStripMenuItem.Text = "アプレット(&P)";
      // 
      // flex4FToolStripMenuItem
      // 
      this.flex4FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adobeFlashPlayer90FToolStripMenuItem,
            this.webBrowser1WToolStripMenuItem,
            this.googleChromeCToolStripMenuItem});
      this.flex4FToolStripMenuItem.Name = "flex4FToolStripMenuItem";
      this.flex4FToolStripMenuItem.Size = new System.Drawing.Size(345, 30);
      this.flex4FToolStripMenuItem.Text = "Flex4(&F)";
      // 
      // adobeFlashPlayer90FToolStripMenuItem
      // 
      this.adobeFlashPlayer90FToolStripMenuItem.Name = "adobeFlashPlayer90FToolStripMenuItem";
      this.adobeFlashPlayer90FToolStripMenuItem.Size = new System.Drawing.Size(343, 30);
      this.adobeFlashPlayer90FToolStripMenuItem.Text = "Adobe Flash Player 9.0(&F)";
      // 
      // webBrowser1WToolStripMenuItem
      // 
      this.webBrowser1WToolStripMenuItem.Name = "webBrowser1WToolStripMenuItem";
      this.webBrowser1WToolStripMenuItem.Size = new System.Drawing.Size(343, 30);
      this.webBrowser1WToolStripMenuItem.Text = "WebBrowser1(&W)";
      // 
      // googleChromeCToolStripMenuItem
      // 
      this.googleChromeCToolStripMenuItem.Name = "googleChromeCToolStripMenuItem";
      this.googleChromeCToolStripMenuItem.Size = new System.Drawing.Size(343, 30);
      this.googleChromeCToolStripMenuItem.Text = "Google Chrome(&C)";
      // 
      // laTexLToolStripMenuItem1
      // 
      this.laTexLToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dviout表示DToolStripMenuItem,
            this.pdf表示PToolStripMenuItem});
      this.laTexLToolStripMenuItem1.Name = "laTexLToolStripMenuItem1";
      this.laTexLToolStripMenuItem1.Size = new System.Drawing.Size(345, 30);
      this.laTexLToolStripMenuItem1.Text = "LaTex(&L)";
      // 
      // dviout表示DToolStripMenuItem
      // 
      this.dviout表示DToolStripMenuItem.Name = "dviout表示DToolStripMenuItem";
      this.dviout表示DToolStripMenuItem.Size = new System.Drawing.Size(226, 30);
      this.dviout表示DToolStripMenuItem.Text = "Dviout表示(&D)";
      // 
      // pdf表示PToolStripMenuItem
      // 
      this.pdf表示PToolStripMenuItem.Name = "pdf表示PToolStripMenuItem";
      this.pdf表示PToolStripMenuItem.Size = new System.Drawing.Size(226, 30);
      this.pdf表示PToolStripMenuItem.Text = "Pdf表示(&P)";
      // 
      // toolStripSeparator27
      // 
      this.toolStripSeparator27.Name = "toolStripSeparator27";
      this.toolStripSeparator27.Size = new System.Drawing.Size(342, 6);
      // 
      // runtimeCompilationRToolStripMenuItem
      // 
      this.runtimeCompilationRToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cSharpCodeProviderToolStripMenuItem,
            this.jScriptCodeProvideJToolStripMenuItem});
      this.runtimeCompilationRToolStripMenuItem.Name = "runtimeCompilationRToolStripMenuItem";
      this.runtimeCompilationRToolStripMenuItem.Size = new System.Drawing.Size(345, 30);
      this.runtimeCompilationRToolStripMenuItem.Text = "Runtime Compilation(&R)";
      // 
      // cSharpCodeProviderToolStripMenuItem
      // 
      this.cSharpCodeProviderToolStripMenuItem.Name = "cSharpCodeProviderToolStripMenuItem";
      this.cSharpCodeProviderToolStripMenuItem.Size = new System.Drawing.Size(323, 30);
      this.cSharpCodeProviderToolStripMenuItem.Text = "CSharpCodeProvider(#)";
      // 
      // jScriptCodeProvideJToolStripMenuItem
      // 
      this.jScriptCodeProvideJToolStripMenuItem.Name = "jScriptCodeProvideJToolStripMenuItem";
      this.jScriptCodeProvideJToolStripMenuItem.Size = new System.Drawing.Size(323, 30);
      this.jScriptCodeProvideJToolStripMenuItem.Text = "JScriptCodeProvide(&J)";
      // 
      // toolStripSeparator14
      // 
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      this.toolStripSeparator14.Size = new System.Drawing.Size(342, 6);
      // 
      // ウエブサーバで実行WToolStripMenuItem
      // 
      this.ウエブサーバで実行WToolStripMenuItem.Name = "ウエブサーバで実行WToolStripMenuItem";
      this.ウエブサーバで実行WToolStripMenuItem.Size = new System.Drawing.Size(345, 30);
      this.ウエブサーバで実行WToolStripMenuItem.Text = "ウエブサーバで実行(&W)";
      this.ウエブサーバで実行WToolStripMenuItem.Click += new System.EventHandler(this.ウエブサーバで実行WToolStripMenuItem_Click);
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new System.Drawing.Size(342, 6);
      // 
      // ファイル名を指定して実行OToolStripMenuItem1
      // 
      this.ファイル名を指定して実行OToolStripMenuItem1.Name = "ファイル名を指定して実行OToolStripMenuItem1";
      this.ファイル名を指定して実行OToolStripMenuItem1.Size = new System.Drawing.Size(345, 30);
      this.ファイル名を指定して実行OToolStripMenuItem1.Text = "ファイル名を指定して実行(&O)...";
      this.ファイル名を指定して実行OToolStripMenuItem1.Click += new System.EventHandler(this.ファイル名を指定して実行OToolStripMenuItem1_Click);
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
      this.スクリプトを実行XToolStripMenuItem.Click += new System.EventHandler(this.スクリプトを実行XToolStripMenuItem_Click);
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
            this.挿入ToolStripMenuItem,
            this.toolStripSeparator12,
            this.tODOリストToolStripMenuItem});
      this.ツールTToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem";
      this.ツールTToolStripMenuItem.Size = new System.Drawing.Size(102, 29);
      this.ツールTToolStripMenuItem.Text = "ツール(&T)";
      // 
      // カスタマイズCToolStripMenuItem
      // 
      this.カスタマイズCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.フォントと色ToolStripMenuItem,
            this.右端で折り返すToolStripMenuItem});
      this.カスタマイズCToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.カスタマイズCToolStripMenuItem.Name = "カスタマイズCToolStripMenuItem";
      this.カスタマイズCToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
      this.カスタマイズCToolStripMenuItem.Text = "カスタマイズ(&C)";
      // 
      // フォントと色ToolStripMenuItem
      // 
      this.フォントと色ToolStripMenuItem.Name = "フォントと色ToolStripMenuItem";
      this.フォントと色ToolStripMenuItem.Size = new System.Drawing.Size(216, 30);
      this.フォントと色ToolStripMenuItem.Text = "フォントと色...";
      this.フォントと色ToolStripMenuItem.Click += new System.EventHandler(this.フォントと色ToolStripMenuItem_Click);
      // 
      // 右端で折り返すToolStripMenuItem
      // 
      this.右端で折り返すToolStripMenuItem.Name = "右端で折り返すToolStripMenuItem";
      this.右端で折り返すToolStripMenuItem.Size = new System.Drawing.Size(216, 30);
      this.右端で折り返すToolStripMenuItem.Text = "右端で折り返す";
      this.右端で折り返すToolStripMenuItem.Click += new System.EventHandler(this.右端で折り返すToolStripMenuItem_Click);
      // 
      // オプションOToolStripMenuItem
      // 
      this.オプションOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.azukiタブを固定ToolStripMenuItem,
            this.browserタブを固定ToolStripMenuItem});
      this.オプションOToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.オプションOToolStripMenuItem.Name = "オプションOToolStripMenuItem";
      this.オプションOToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
      this.オプションOToolStripMenuItem.Text = "オプション(&O)";
      // 
      // azukiタブを固定ToolStripMenuItem
      // 
      this.azukiタブを固定ToolStripMenuItem.CheckOnClick = true;
      this.azukiタブを固定ToolStripMenuItem.Enabled = false;
      this.azukiタブを固定ToolStripMenuItem.Name = "azukiタブを固定ToolStripMenuItem";
      this.azukiタブを固定ToolStripMenuItem.Size = new System.Drawing.Size(254, 30);
      this.azukiタブを固定ToolStripMenuItem.Text = "Azukiタブを固定";
      // 
      // browserタブを固定ToolStripMenuItem
      // 
      this.browserタブを固定ToolStripMenuItem.CheckOnClick = true;
      this.browserタブを固定ToolStripMenuItem.Enabled = false;
      this.browserタブを固定ToolStripMenuItem.Name = "browserタブを固定ToolStripMenuItem";
      this.browserタブを固定ToolStripMenuItem.Size = new System.Drawing.Size(254, 30);
      this.browserタブを固定ToolStripMenuItem.Text = "Browserタブを固定";
      // 
      // 挿入ToolStripMenuItem
      // 
      this.挿入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.タイムスタンプToolStripMenuItem,
            this.c見出しToolStripMenuItem});
      this.挿入ToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.挿入ToolStripMenuItem.MergeIndex = 2;
      this.挿入ToolStripMenuItem.Name = "挿入ToolStripMenuItem";
      this.挿入ToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
      this.挿入ToolStripMenuItem.Text = "挿入";
      // 
      // タイムスタンプToolStripMenuItem
      // 
      this.タイムスタンプToolStripMenuItem.Name = "タイムスタンプToolStripMenuItem";
      this.タイムスタンプToolStripMenuItem.Size = new System.Drawing.Size(193, 30);
      this.タイムスタンプToolStripMenuItem.Text = "タイムスタンプ";
      this.タイムスタンプToolStripMenuItem.Click += new System.EventHandler(this.タイムスタンプToolStripMenuItem_Click);
      // 
      // c見出しToolStripMenuItem
      // 
      this.c見出しToolStripMenuItem.Name = "c見出しToolStripMenuItem";
      this.c見出しToolStripMenuItem.Size = new System.Drawing.Size(193, 30);
      this.c見出しToolStripMenuItem.Text = "C見出し";
      this.c見出しToolStripMenuItem.Click += new System.EventHandler(this.c見出しToolStripMenuItem_Click);
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.toolStripSeparator12.MergeIndex = 3;
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(206, 6);
      // 
      // tODOリストToolStripMenuItem
      // 
      this.tODOリストToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成ToolStripMenuItem,
            this.開くToolStripMenuItem,
            this.上書き保存ToolStripMenuItem,
            this.名前を付けて保存ToolStripMenuItem});
      this.tODOリストToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Remove;
      this.tODOリストToolStripMenuItem.Name = "tODOリストToolStripMenuItem";
      this.tODOリストToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
      this.tODOリストToolStripMenuItem.Text = "TODOリスト";
      // 
      // 新規作成ToolStripMenuItem
      // 
      this.新規作成ToolStripMenuItem.Name = "新規作成ToolStripMenuItem";
      this.新規作成ToolStripMenuItem.Size = new System.Drawing.Size(237, 30);
      this.新規作成ToolStripMenuItem.Text = "新規作成";
      // 
      // 開くToolStripMenuItem
      // 
      this.開くToolStripMenuItem.Name = "開くToolStripMenuItem";
      this.開くToolStripMenuItem.Size = new System.Drawing.Size(237, 30);
      this.開くToolStripMenuItem.Text = "開く";
      // 
      // 上書き保存ToolStripMenuItem
      // 
      this.上書き保存ToolStripMenuItem.Name = "上書き保存ToolStripMenuItem";
      this.上書き保存ToolStripMenuItem.Size = new System.Drawing.Size(237, 30);
      this.上書き保存ToolStripMenuItem.Text = "上書き保存";
      // 
      // 名前を付けて保存ToolStripMenuItem
      // 
      this.名前を付けて保存ToolStripMenuItem.Name = "名前を付けて保存ToolStripMenuItem";
      this.名前を付けて保存ToolStripMenuItem.Size = new System.Drawing.Size(237, 30);
      this.名前を付けて保存ToolStripMenuItem.Text = "名前を付けて保存";
      // 
      // ヘルプHToolStripMenuItem
      // 
      this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.azukiDocumentToolStripMenuItem,
            this.toolStripSeparator10,
            this.内容CToolStripMenuItem,
            this.インデックスIToolStripMenuItem,
            this.検索SToolStripMenuItem,
            this.toolStripSeparator5,
            this.バージョン情報AToolStripMenuItem});
      this.ヘルプHToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
      this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(104, 29);
      this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
      // 
      // azukiDocumentToolStripMenuItem
      // 
      this.azukiDocumentToolStripMenuItem.Name = "azukiDocumentToolStripMenuItem";
      this.azukiDocumentToolStripMenuItem.Size = new System.Drawing.Size(258, 30);
      this.azukiDocumentToolStripMenuItem.Text = "AzukiDocument";
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(255, 6);
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
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(255, 6);
      // 
      // バージョン情報AToolStripMenuItem
      // 
      this.バージョン情報AToolStripMenuItem.Name = "バージョン情報AToolStripMenuItem";
      this.バージョン情報AToolStripMenuItem.Size = new System.Drawing.Size(258, 30);
      this.バージョン情報AToolStripMenuItem.Text = "バージョン情報(&A)...";
      this.バージョン情報AToolStripMenuItem.Click += new System.EventHandler(this.バージョン情報AToolStripMenuItem_Click);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
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
            this.toolStripDropDownButton4,
            this.toolStripSeparator11,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton3,
            this.toolStripSeparator20,
            this.imageListButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 33);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(1361, 32);
      this.toolStrip1.TabIndex = 3;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // 新規作成NToolStripButton
      // 
      this.新規作成NToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("新規作成NToolStripButton.Image")));
      this.新規作成NToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.新規作成NToolStripButton.Name = "新規作成NToolStripButton";
      this.新規作成NToolStripButton.Size = new System.Drawing.Size(149, 29);
      this.新規作成NToolStripButton.Tag = this.新規作成NToolStripMenuItem;
      this.新規作成NToolStripButton.Text = "新規作成(&N)";
      this.新規作成NToolStripButton.Click += new System.EventHandler(this.新規作成NToolStripButton_Click);
      // 
      // 開くOToolStripButton
      // 
      this.開くOToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.開くOToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripButton.Image")));
      this.開くOToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.開くOToolStripButton.Name = "開くOToolStripButton";
      this.開くOToolStripButton.Size = new System.Drawing.Size(24, 29);
      this.開くOToolStripButton.Tag = this.開くOToolStripMenuItem;
      this.開くOToolStripButton.Text = "開く(&O)";
      this.開くOToolStripButton.Click += new System.EventHandler(this.開くOToolStripButton_Click);
      // 
      // 上書き保存SToolStripButton
      // 
      this.上書き保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.上書き保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("上書き保存SToolStripButton.Image")));
      this.上書き保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.上書き保存SToolStripButton.Name = "上書き保存SToolStripButton";
      this.上書き保存SToolStripButton.Size = new System.Drawing.Size(24, 29);
      this.上書き保存SToolStripButton.Tag = this.上書き保存SToolStripMenuItem;
      this.上書き保存SToolStripButton.Text = "上書き保存(&S)";
      this.上書き保存SToolStripButton.Click += new System.EventHandler(this.上書き保存SToolStripButton_Click);
      // 
      // 印刷PToolStripButton
      // 
      this.印刷PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.印刷PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripButton.Image")));
      this.印刷PToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷PToolStripButton.Name = "印刷PToolStripButton";
      this.印刷PToolStripButton.Size = new System.Drawing.Size(24, 29);
      this.印刷PToolStripButton.Text = "印刷(&P)";
      this.印刷PToolStripButton.Click += new System.EventHandler(this.印刷PToolStripButton_Click);
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 32);
      // 
      // 切り取りUToolStripButton
      // 
      this.切り取りUToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.切り取りUToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("切り取りUToolStripButton.Image")));
      this.切り取りUToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.切り取りUToolStripButton.Name = "切り取りUToolStripButton";
      this.切り取りUToolStripButton.Size = new System.Drawing.Size(24, 29);
      this.切り取りUToolStripButton.Tag = this.切り取りTToolStripMenuItem;
      this.切り取りUToolStripButton.Text = "切り取り(&U)";
      this.切り取りUToolStripButton.Click += new System.EventHandler(this.切り取りUToolStripButton_Click);
      // 
      // コピーCToolStripButton
      // 
      this.コピーCToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.コピーCToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripButton.Image")));
      this.コピーCToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.コピーCToolStripButton.Name = "コピーCToolStripButton";
      this.コピーCToolStripButton.Size = new System.Drawing.Size(24, 29);
      this.コピーCToolStripButton.Tag = this.コピーCToolStripMenuItem;
      this.コピーCToolStripButton.Text = "コピー(&C)";
      this.コピーCToolStripButton.Click += new System.EventHandler(this.コピーCToolStripButton_Click);
      // 
      // 貼り付けPToolStripButton
      // 
      this.貼り付けPToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.貼り付けPToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripButton.Image")));
      this.貼り付けPToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.貼り付けPToolStripButton.Name = "貼り付けPToolStripButton";
      this.貼り付けPToolStripButton.Size = new System.Drawing.Size(24, 29);
      this.貼り付けPToolStripButton.Tag = this.貼り付けPToolStripMenuItem;
      this.貼り付けPToolStripButton.Text = "貼り付け(&P)";
      this.貼り付けPToolStripButton.Click += new System.EventHandler(this.貼り付けPToolStripButton_Click);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 32);
      // 
      // ヘルプLToolStripButton
      // 
      this.ヘルプLToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ヘルプLToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ヘルプLToolStripButton.Image")));
      this.ヘルプLToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.ヘルプLToolStripButton.Name = "ヘルプLToolStripButton";
      this.ヘルプLToolStripButton.Size = new System.Drawing.Size(24, 29);
      this.ヘルプLToolStripButton.Text = "ヘルプ(&L)";
      this.ヘルプLToolStripButton.Click += new System.EventHandler(this.ヘルプLToolStripButton_Click);
      // 
      // toolStripDropDownButton4
      // 
      this.toolStripDropDownButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.メニューバーMToolStripMenuItem,
            this.ステータスバーSToolStripMenuItem1,
            this.コンソールSToolStripMenuItem});
      this.toolStripDropDownButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton4.Image")));
      this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
      this.toolStripDropDownButton4.Size = new System.Drawing.Size(34, 29);
      this.toolStripDropDownButton4.Text = "toolStripDropDownButton1";
      // 
      // メニューバーMToolStripMenuItem
      // 
      this.メニューバーMToolStripMenuItem.Checked = true;
      this.メニューバーMToolStripMenuItem.CheckOnClick = true;
      this.メニューバーMToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.メニューバーMToolStripMenuItem.Name = "メニューバーMToolStripMenuItem";
      this.メニューバーMToolStripMenuItem.Size = new System.Drawing.Size(229, 30);
      this.メニューバーMToolStripMenuItem.Text = "メニューバー(&M)";
      this.メニューバーMToolStripMenuItem.Click += new System.EventHandler(this.メニューバーMToolStripMenuItem_Click);
      // 
      // ステータスバーSToolStripMenuItem1
      // 
      this.ステータスバーSToolStripMenuItem1.CheckOnClick = true;
      this.ステータスバーSToolStripMenuItem1.Name = "ステータスバーSToolStripMenuItem1";
      this.ステータスバーSToolStripMenuItem1.Size = new System.Drawing.Size(229, 30);
      this.ステータスバーSToolStripMenuItem1.Text = "ステータスバー(&S)";
      this.ステータスバーSToolStripMenuItem1.Click += new System.EventHandler(this.ステータスバーSToolStripMenuItem1_Click);
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(6, 32);
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリケーションAToolStripMenuItem1,
            this.スクリプトSToolStripMenuItem1,
            this.jAVAToolStripMenuItem1,
            this.toolStripSeparator21,
            this.ウエブサーバで実行WToolStripMenuItem1});
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(14, 29);
      this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.ToolTipText = "現在開いているファイルを実行します";
      // 
      // アプリケーションAToolStripMenuItem1
      // 
      this.アプリケーションAToolStripMenuItem1.Name = "アプリケーションAToolStripMenuItem1";
      this.アプリケーションAToolStripMenuItem1.Size = new System.Drawing.Size(278, 30);
      this.アプリケーションAToolStripMenuItem1.Tag = this.consoleアプリケーションCToolStripMenuItem;
      this.アプリケーションAToolStripMenuItem1.Text = "アプリケーション(&A)";
      this.アプリケーションAToolStripMenuItem1.Click += new System.EventHandler(this.アプリケーションAToolStripMenuItem1_Click);
      // 
      // スクリプトSToolStripMenuItem1
      // 
      this.スクリプトSToolStripMenuItem1.Name = "スクリプトSToolStripMenuItem1";
      this.スクリプトSToolStripMenuItem1.Size = new System.Drawing.Size(278, 30);
      this.スクリプトSToolStripMenuItem1.Tag = this.スクリプトSToolStripMenuItem;
      this.スクリプトSToolStripMenuItem1.Text = "スクリプト(&S)";
      // 
      // jAVAToolStripMenuItem1
      // 
      this.jAVAToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリケーションAToolStripMenuItem2,
            this.アプレットPToolStripMenuItem1});
      this.jAVAToolStripMenuItem1.Name = "jAVAToolStripMenuItem1";
      this.jAVAToolStripMenuItem1.Size = new System.Drawing.Size(278, 30);
      this.jAVAToolStripMenuItem1.Text = "JAVA(&J)";
      // 
      // アプリケーションAToolStripMenuItem2
      // 
      this.アプリケーションAToolStripMenuItem2.Name = "アプリケーションAToolStripMenuItem2";
      this.アプリケーションAToolStripMenuItem2.Size = new System.Drawing.Size(237, 30);
      this.アプリケーションAToolStripMenuItem2.Tag = this.アプリケーションAToolStripMenuItem;
      this.アプリケーションAToolStripMenuItem2.Text = "アプリケーション(&A)";
      // 
      // アプレットPToolStripMenuItem1
      // 
      this.アプレットPToolStripMenuItem1.Name = "アプレットPToolStripMenuItem1";
      this.アプレットPToolStripMenuItem1.Size = new System.Drawing.Size(237, 30);
      this.アプレットPToolStripMenuItem1.Tag = this.アプレットPToolStripMenuItem;
      this.アプレットPToolStripMenuItem1.Text = "アプレット(&P)";
      // 
      // toolStripSeparator21
      // 
      this.toolStripSeparator21.Name = "toolStripSeparator21";
      this.toolStripSeparator21.Size = new System.Drawing.Size(275, 6);
      // 
      // ウエブサーバで実行WToolStripMenuItem1
      // 
      this.ウエブサーバで実行WToolStripMenuItem1.Name = "ウエブサーバで実行WToolStripMenuItem1";
      this.ウエブサーバで実行WToolStripMenuItem1.Size = new System.Drawing.Size(278, 30);
      this.ウエブサーバで実行WToolStripMenuItem1.Tag = this.ウエブサーバで実行WToolStripMenuItem;
      this.ウエブサーバで実行WToolStripMenuItem1.Text = "ウエブサーバで実行(&W)";
      // 
      // toolStripDropDownButton2
      // 
      this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildbatBToolStripMenuItem1,
            this.makefileMToolStripMenuItem1,
            this.toolStripSeparator22,
            this.cCCToolStripMenuItem2,
            this.cToolStripMenuItem2,
            this.toolStripSeparator23,
            this.jAVAJToolStripMenuItem2});
      this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
      this.toolStripDropDownButton2.Size = new System.Drawing.Size(14, 29);
      this.toolStripDropDownButton2.Text = "toolStripDropDownButton2";
      this.toolStripDropDownButton2.ToolTipText = "現在開いているファイルをビルドします";
      // 
      // buildbatBToolStripMenuItem1
      // 
      this.buildbatBToolStripMenuItem1.Name = "buildbatBToolStripMenuItem1";
      this.buildbatBToolStripMenuItem1.Size = new System.Drawing.Size(207, 30);
      this.buildbatBToolStripMenuItem1.Tag = this.buildbatBToolStripMenuItem;
      this.buildbatBToolStripMenuItem1.Text = "Build.bat(&B)";
      // 
      // makefileMToolStripMenuItem1
      // 
      this.makefileMToolStripMenuItem1.Name = "makefileMToolStripMenuItem1";
      this.makefileMToolStripMenuItem1.Size = new System.Drawing.Size(207, 30);
      this.makefileMToolStripMenuItem1.Tag = this.makefileMToolStripMenuItem;
      this.makefileMToolStripMenuItem1.Text = "Makefile(&M)";
      // 
      // toolStripSeparator22
      // 
      this.toolStripSeparator22.Name = "toolStripSeparator22";
      this.toolStripSeparator22.Size = new System.Drawing.Size(204, 6);
      // 
      // cCCToolStripMenuItem2
      // 
      this.cCCToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vC606ToolStripMenuItem1,
            this.vC20088ToolStripMenuItem1,
            this.vC20101ToolStripMenuItem1});
      this.cCCToolStripMenuItem2.Name = "cCCToolStripMenuItem2";
      this.cCCToolStripMenuItem2.Size = new System.Drawing.Size(207, 30);
      this.cCCToolStripMenuItem2.Text = "C/C++(&C)";
      // 
      // vC606ToolStripMenuItem1
      // 
      this.vC606ToolStripMenuItem1.Name = "vC606ToolStripMenuItem1";
      this.vC606ToolStripMenuItem1.Size = new System.Drawing.Size(195, 30);
      this.vC606ToolStripMenuItem1.Tag = this.vC606ToolStripMenuItem;
      this.vC606ToolStripMenuItem1.Text = "VC6.0(&6)";
      // 
      // vC20088ToolStripMenuItem1
      // 
      this.vC20088ToolStripMenuItem1.Name = "vC20088ToolStripMenuItem1";
      this.vC20088ToolStripMenuItem1.Size = new System.Drawing.Size(195, 30);
      this.vC20088ToolStripMenuItem1.Tag = this.vC20088ToolStripMenuItem;
      this.vC20088ToolStripMenuItem1.Text = "VC2008(&8)";
      // 
      // vC20101ToolStripMenuItem1
      // 
      this.vC20101ToolStripMenuItem1.Name = "vC20101ToolStripMenuItem1";
      this.vC20101ToolStripMenuItem1.Size = new System.Drawing.Size(195, 30);
      this.vC20101ToolStripMenuItem1.Tag = this.vC20101ToolStripMenuItem;
      this.vC20101ToolStripMenuItem1.Text = "VC2010(&1)";
      // 
      // cToolStripMenuItem2
      // 
      this.cToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vC20088ToolStripMenuItem2,
            this.vC20101ToolStripMenuItem2});
      this.cToolStripMenuItem2.Name = "cToolStripMenuItem2";
      this.cToolStripMenuItem2.Size = new System.Drawing.Size(207, 30);
      this.cToolStripMenuItem2.Text = "C#(&#)";
      // 
      // vC20088ToolStripMenuItem2
      // 
      this.vC20088ToolStripMenuItem2.Name = "vC20088ToolStripMenuItem2";
      this.vC20088ToolStripMenuItem2.Size = new System.Drawing.Size(211, 30);
      this.vC20088ToolStripMenuItem2.Tag = this.vCS20088ToolStripMenuItem;
      this.vC20088ToolStripMenuItem2.Text = "VC#2008(&8)";
      // 
      // vC20101ToolStripMenuItem2
      // 
      this.vC20101ToolStripMenuItem2.Name = "vC20101ToolStripMenuItem2";
      this.vC20101ToolStripMenuItem2.Size = new System.Drawing.Size(211, 30);
      this.vC20101ToolStripMenuItem2.Tag = this.vCS20101ToolStripMenuItem;
      this.vC20101ToolStripMenuItem2.Text = "VC#2010(&1)";
      // 
      // toolStripSeparator23
      // 
      this.toolStripSeparator23.Name = "toolStripSeparator23";
      this.toolStripSeparator23.Size = new System.Drawing.Size(204, 6);
      // 
      // jAVAJToolStripMenuItem2
      // 
      this.jAVAJToolStripMenuItem2.Name = "jAVAJToolStripMenuItem2";
      this.jAVAJToolStripMenuItem2.Size = new System.Drawing.Size(207, 30);
      this.jAVAJToolStripMenuItem2.Tag = this.javaJToolStripMenuItem1;
      this.jAVAJToolStripMenuItem2.Text = "JAVA(&J)";
      // 
      // toolStripDropDownButton3
      // 
      this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.サクラエディタSToolStripMenuItem,
            this.pSPadPToolStripMenuItem,
            this.toolStripSeparator16,
            this.scintillaCToolStripMenuItem,
            this.richTextBoxToolStripMenuItem,
            this.toolStripSeparator17,
            this.エクスプローラEToolStripMenuItem,
            this.コマンドプロンプトCToolStripMenuItem,
            this.toolStripSeparator18,
            this.ファイル名を指定して実行OToolStripMenuItem,
            this.リンクを開くLToolStripMenuItem});
      this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
      this.toolStripDropDownButton3.Size = new System.Drawing.Size(14, 29);
      this.toolStripDropDownButton3.Text = "&R";
      this.toolStripDropDownButton3.ToolTipText = "現在開いてるファイルを外部プログラムで開きます";
      // 
      // サクラエディタSToolStripMenuItem
      // 
      this.サクラエディタSToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("サクラエディタSToolStripMenuItem.Image")));
      this.サクラエディタSToolStripMenuItem.Name = "サクラエディタSToolStripMenuItem";
      this.サクラエディタSToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.サクラエディタSToolStripMenuItem.Text = "サクラエディタ(&S)";
      this.サクラエディタSToolStripMenuItem.Click += new System.EventHandler(this.サクラエディタSToolStripMenuItem_Click);
      // 
      // pSPadPToolStripMenuItem
      // 
      this.pSPadPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pSPadPToolStripMenuItem.Image")));
      this.pSPadPToolStripMenuItem.Name = "pSPadPToolStripMenuItem";
      this.pSPadPToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.pSPadPToolStripMenuItem.Text = "PSPad(&P)";
      this.pSPadPToolStripMenuItem.Click += new System.EventHandler(this.pSPadPToolStripMenuItem_Click);
      // 
      // toolStripSeparator16
      // 
      this.toolStripSeparator16.Name = "toolStripSeparator16";
      this.toolStripSeparator16.Size = new System.Drawing.Size(321, 6);
      // 
      // scintillaCToolStripMenuItem
      // 
      this.scintillaCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("scintillaCToolStripMenuItem.Image")));
      this.scintillaCToolStripMenuItem.Name = "scintillaCToolStripMenuItem";
      this.scintillaCToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.scintillaCToolStripMenuItem.Text = "Scintilla(&A)";
      this.scintillaCToolStripMenuItem.Click += new System.EventHandler(this.scintillaCToolStripMenuItem_Click);
      // 
      // richTextBoxToolStripMenuItem
      // 
      this.richTextBoxToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("richTextBoxToolStripMenuItem.Image")));
      this.richTextBoxToolStripMenuItem.Name = "richTextBoxToolStripMenuItem";
      this.richTextBoxToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.richTextBoxToolStripMenuItem.Text = "RichTextEditor(&R)";
      this.richTextBoxToolStripMenuItem.Click += new System.EventHandler(this.richTextBoxToolStripMenuItem_Click);
      // 
      // toolStripSeparator17
      // 
      this.toolStripSeparator17.Name = "toolStripSeparator17";
      this.toolStripSeparator17.Size = new System.Drawing.Size(321, 6);
      // 
      // エクスプローラEToolStripMenuItem
      // 
      this.エクスプローラEToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("エクスプローラEToolStripMenuItem.Image")));
      this.エクスプローラEToolStripMenuItem.Name = "エクスプローラEToolStripMenuItem";
      this.エクスプローラEToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.エクスプローラEToolStripMenuItem.Text = "エクスプローラ(&E)";
      this.エクスプローラEToolStripMenuItem.Click += new System.EventHandler(this.エクスプローラEToolStripMenuItem_Click);
      // 
      // コマンドプロンプトCToolStripMenuItem
      // 
      this.コマンドプロンプトCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("コマンドプロンプトCToolStripMenuItem.Image")));
      this.コマンドプロンプトCToolStripMenuItem.Name = "コマンドプロンプトCToolStripMenuItem";
      this.コマンドプロンプトCToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.コマンドプロンプトCToolStripMenuItem.Text = "コマンド・プロンプト(&C)";
      this.コマンドプロンプトCToolStripMenuItem.Click += new System.EventHandler(this.コマンドプロンプトCToolStripMenuItem_Click);
      // 
      // toolStripSeparator18
      // 
      this.toolStripSeparator18.Name = "toolStripSeparator18";
      this.toolStripSeparator18.Size = new System.Drawing.Size(321, 6);
      // 
      // ファイル名を指定して実行OToolStripMenuItem
      // 
      this.ファイル名を指定して実行OToolStripMenuItem.Name = "ファイル名を指定して実行OToolStripMenuItem";
      this.ファイル名を指定して実行OToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.ファイル名を指定して実行OToolStripMenuItem.Text = "ファイル名を指定して実行(&O)";
      this.ファイル名を指定して実行OToolStripMenuItem.Click += new System.EventHandler(this.ファイル名を指定して実行OToolStripMenuItem_Click);
      // 
      // リンクを開くLToolStripMenuItem
      // 
      this.リンクを開くLToolStripMenuItem.Name = "リンクを開くLToolStripMenuItem";
      this.リンクを開くLToolStripMenuItem.Size = new System.Drawing.Size(324, 30);
      this.リンクを開くLToolStripMenuItem.Text = "リンクを開く(&L)";
      this.リンクを開くLToolStripMenuItem.Click += new System.EventHandler(this.リンクを開くLToolStripMenuItem_Click);
      // 
      // toolStripSeparator20
      // 
      this.toolStripSeparator20.Name = "toolStripSeparator20";
      this.toolStripSeparator20.Size = new System.Drawing.Size(6, 32);
      // 
      // imageListButton
      // 
      this.imageListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.imageListButton.Image = ((System.Drawing.Image)(resources.GetObject("imageListButton.Image")));
      this.imageListButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.imageListButton.Name = "imageListButton";
      this.imageListButton.Size = new System.Drawing.Size(24, 29);
      this.imageListButton.Text = "toolStripButton1";
      // 
      // statusStrip1
      // 
      this.statusStrip1.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
      this.statusStrip1.Location = new System.Drawing.Point(0, 614);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
      this.statusStrip1.Size = new System.Drawing.Size(1361, 30);
      this.statusStrip1.TabIndex = 4;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(222, 25);
      this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
      // 
      // azukiControl1
      // 
      this.azukiControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(240)))));
      this.azukiControl1.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.azukiControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.azukiControl1.DrawingOption = ((Sgry.Azuki.DrawingOption)(((((((Sgry.Azuki.DrawingOption.DrawsFullWidthSpace | Sgry.Azuki.DrawingOption.DrawsTab) 
            | Sgry.Azuki.DrawingOption.DrawsEol) 
            | Sgry.Azuki.DrawingOption.HighlightCurrentLine) 
            | Sgry.Azuki.DrawingOption.ShowsLineNumber) 
            | Sgry.Azuki.DrawingOption.ShowsHRuler) 
            | Sgry.Azuki.DrawingOption.ShowsDirtBar)));
      this.azukiControl1.FirstVisibleLine = 0;
      this.azukiControl1.Font = new System.Drawing.Font("ＭＳ ゴシック", 16F);
      fontInfo9.Name = "ＭＳ ゴシック";
      fontInfo9.Size = 16;
      fontInfo9.Style = System.Drawing.FontStyle.Regular;
      this.azukiControl1.FontInfo = fontInfo9;
      this.azukiControl1.ForeColor = System.Drawing.Color.Black;
      this.azukiControl1.Location = new System.Drawing.Point(0, 0);
      this.azukiControl1.Margin = new System.Windows.Forms.Padding(4);
      this.azukiControl1.MarksUri = true;
      this.azukiControl1.Name = "azukiControl1";
      this.azukiControl1.ShowsHRuler = true;
      this.azukiControl1.Size = new System.Drawing.Size(1361, 360);
      this.azukiControl1.TabIndex = 4;
      this.azukiControl1.TabWidth = 2;
      this.azukiControl1.Tag = "";
      this.azukiControl1.Text = "azukiControl1";
      this.azukiControl1.ViewWidth = 4151;
      this.azukiControl1.CaretMoved += new System.EventHandler(this.azukiControl1_CaretMoved);
      this.azukiControl1.TextChanged += new System.EventHandler(this.azukiControl1_TextChanged);
      this.azukiControl1.DoubleClick += new System.EventHandler(this.azukiControl1_DoubleClick);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 65);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.azukiControl1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.azukiControl2);
      this.splitContainer1.Size = new System.Drawing.Size(1361, 549);
      this.splitContainer1.SplitterDistance = 360;
      this.splitContainer1.TabIndex = 5;
      // 
      // azukiControl2
      // 
      this.azukiControl2.BackColor = System.Drawing.Color.Black;
      this.azukiControl2.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.azukiControl2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.azukiControl2.DrawingOption = ((Sgry.Azuki.DrawingOption)(((((((Sgry.Azuki.DrawingOption.DrawsFullWidthSpace | Sgry.Azuki.DrawingOption.DrawsTab) 
            | Sgry.Azuki.DrawingOption.DrawsEol) 
            | Sgry.Azuki.DrawingOption.HighlightCurrentLine) 
            | Sgry.Azuki.DrawingOption.ShowsLineNumber) 
            | Sgry.Azuki.DrawingOption.ShowsHRuler) 
            | Sgry.Azuki.DrawingOption.ShowsDirtBar)));
      this.azukiControl2.FirstVisibleLine = 0;
      this.azukiControl2.Font = new System.Drawing.Font("ＭＳ ゴシック", 16F);
      fontInfo10.Name = "ＭＳ ゴシック";
      fontInfo10.Size = 16;
      fontInfo10.Style = System.Drawing.FontStyle.Regular;
      this.azukiControl2.FontInfo = fontInfo10;
      this.azukiControl2.ForeColor = System.Drawing.Color.White;
      this.azukiControl2.Location = new System.Drawing.Point(0, 0);
      this.azukiControl2.Margin = new System.Windows.Forms.Padding(4);
      this.azukiControl2.MarksUri = true;
      this.azukiControl2.Name = "azukiControl2";
      this.azukiControl2.ShowsHRuler = true;
      this.azukiControl2.Size = new System.Drawing.Size(1361, 185);
      this.azukiControl2.TabIndex = 5;
      this.azukiControl2.TabWidth = 2;
      this.azukiControl2.Tag = "";
      this.azukiControl2.Text = "azukiControl2";
      this.azukiControl2.ViewWidth = 4151;
      // 
      // コンソールSToolStripMenuItem
      // 
      this.コンソールSToolStripMenuItem.Name = "コンソールSToolStripMenuItem";
      this.コンソールSToolStripMenuItem.Size = new System.Drawing.Size(229, 30);
      this.コンソールSToolStripMenuItem.Text = "コンソール(&S)";
      this.コンソールSToolStripMenuItem.Click += new System.EventHandler(this.コンソールSToolStripMenuItem_Click);
      // 
      // AzukiEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.menuStrip1);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "AzukiEditor";
      this.Size = new System.Drawing.Size(1361, 644);
      this.Tag = this.azukiControl1;
      this.Load += new System.EventHandler(this.AzukiEditor_Load);
      this.Enter += new System.EventHandler(this.AzukiEditor_Enter);
      this.Leave += new System.EventHandler(this.AzukiEditor_Leave);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    #region MenuBar Click Handler
    private void 新規作成NToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        this.azukiControl1.Document = new Document();
        this.azukiControl1.Document.IsDirty = false;
#if Interface
        //this.azukiControl1.Tag = (((DockContent)base.Parent).TabText = "無題 " + this._UntitledFileCount.ToString());
#else        
        //this.azukiControl1.Tag = (((Form)this.Parent).Text = "無題 " + this._UntitledFileCount.ToString());
#endif        
        this._UntitledFileCount++;
      }
      catch (Exception ex)
      {
        string message = ex.Message.ToString();
        MessageBox.Show(Lib.OutputError(message));
      }
    }

    private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if Interface
      string workingDirectory = PluginBase.MainForm.WorkingDirectory;
#else
      //string workingDirectory = @"F:\"; //"
#endif
      string fileName = "default.txt";
      string filter = "All files(*.*)|*.*|Supported files|*.txt;*.log;*.ini;*.inf;*.tex;*.htm;*.html;*.css;*.js;*.xml;*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx;*.cs;*.java;*.py;*.rb;*.pl;*.vbs;*.bat|Text file(*.txt, *.log, *.tex, ...)|*.txt;*.log;*.ini;*.inf;*.tex|HTML file(*.htm, *.html)|*.htm;*.html|CSS file(*.css)|*.css|Javascript file(*.js)|*.js|XML file(*.xml)|*.xml|C/C++ source(*.c, *.h, ...)|*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx|C# source(*.cs)|*.cs|Java source(*.java)|*.java|Python script(*.py)|*.py|Ruby script(*.rb)|*.rb|Perl script(*.pl)|*.pl|VB script(*.vbs)|*.vbs|Batch file(*.bat)|*.bat";
      try
      {
        //string path = MDIForm.CommonLibrary.Lib.File_OpenDialog(fileName, workingDirectory, filter);
        string path = Lib.File_OpenDialog(fileName, String.Empty, filter);
        if (!(path == ""))
        {
          this.currentPath = path;
          this.SetHighlight(path);
          this.azukiControl1.Document.Text = Lib.File_ReadToEndDecode(path);
          this.azukiControl1.Document.IsDirty = false;
          this.azukiControl1.Tag = path;
          //((Form)this.Parent).Text = Path.GetFileName(path);
          this.AddPreviousDocuments(path);
          this.PopulatePreviousDocumentsMenu();
          this.UpdateStatusText(this.currentPath);
        }
      }
      catch (Exception ex)
      {
        string message = ex.Message.ToString();
        MessageBox.Show(Lib.OutputError(message));
      }
    }

    private void 読み取り専用ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.azukiControl1.Document.IsReadOnly = this.読み取り専用ToolStripMenuItem.Checked;
    }

    private void 上書き保存SToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DateTime now = DateTime.Now;
      string text = string.Format("_{0:00}{1:00}{2:00}{3:00}{4:00}{5:00}", new object[]
			{
				now.Year,
				now.Month,
				now.Day,
				now.Hour,
				now.Minute,
				now.Second
			});
      string text2 = this.azukiControl1.Tag.ToString();
      if (text2 != "")
      {
        try
        {
          string destFileName = string.Concat(new string[]
					{
						Path.GetDirectoryName(text2),
						"\\",
						Path.GetFileNameWithoutExtension(text2),
						text,
						Path.GetExtension(text2)
					});
          File.Copy(text2, destFileName);
          File.WriteAllText(text2, this.azukiControl1.Document.Text, Encoding.Default);
          this.azukiControl1.Document.IsDirty = false;
#if Interface          
          ((DockContent)base.Parent).TabText = Path.GetFileName(this.currentPath);
#else          
          //((Form)this.Parent).Text = Path.GetFileName(this.currentPath);
#endif        
        }
        catch (Exception ex)
        {
          string message = ex.Message.ToString();
          MessageBox.Show(Lib.OutputError(message));
        }
      }
      else
      {
        this.名前を付けて保存AToolStripMenuItem.PerformClick();
      }
    }

    private void 名前を付けて保存AToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string filter = "All files(*.*)|*.*|Text file(*.txt, *.log, *.tex, ...)|*.txt;*.log;*.ini;*.inf;*.tex|HTML file(*.htm, *.html)|*.htm;*.html|CSS file(*.css)|*.css|Javascript file(*.js)|*.js|XML file(*.xml)|*.xml|C/C++ source(*.c, *.h, ...)|*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx|C# source(*.cs)|*.cs|Java source(*.java)|*.java|Python script(*.py)|*.py|Ruby script(*.rb)|*.rb|Perl script(*.pl)|*.pl|VB script(*.vbs)|*.vbs|Batch file(*.bat)|*.bat";
      string text = this.azukiControl1.Tag.ToString();
      string initialDirectory;
      string fileName;
      if (text.Length == 0)
      {
#if Interface
        initialDirectory = PluginBase.MainForm.WorkingDirectory;
#else
        initialDirectory = "F:\\";
#endif        
        fileName = "新しいファイル.txt";
      }
      else
      {
        initialDirectory = Path.GetDirectoryName(text);
        fileName = Path.GetFileName(text);
      }
      try
      {
        string text2 = Lib.File_SaveDialog(fileName, initialDirectory, filter);
        if (!(text2 == ""))
        {
          if (File.Exists(text2))
          {
            DateTime now = DateTime.Now;
            string text3 = string.Format("_{0:00}{1:00}{2:00}{3:00}{4:00}{5:00}", new object[]
						{
							now.Year,
							now.Month,
							now.Day,
							now.Hour,
							now.Minute,
							now.Second
						});
            string destFileName = string.Concat(new string[]
						{
							Path.GetDirectoryName(text2),
							"\\",
							Path.GetFileNameWithoutExtension(text2),
							text3,
							Path.GetExtension(text2)
						});
            if (File.Exists(text))
            {
              File.Copy(text, destFileName);
            }
          }
          File.WriteAllText(text2, this.azukiControl1.Document.Text, Encoding.Default);
          this.azukiControl1.Tag = text2;
          this.azukiControl1.Document.IsDirty = false;
          this.Text = Path.GetFileName(text2);
          //((Form)this.Parent).Text = Path.GetFileNameWithoutExtension(text2);
        }
      }
      catch (Exception ex)
      {
        string message = ex.Message.ToString();
        MessageBox.Show(Lib.OutputError(message));
      }
    }

    private void 閉じるCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!this.azukiControl1.Document.IsDirty || Lib.confirmDestructionText("ファイルを閉じる", this.azukiControl1.Tag.ToString() + "\r\nファイルは保存されていません。\n\n編集中のテキストは破棄されます。\n\nよろしいですか?"))
      {
#if Interface        
        //PluginBase.MainForm.CurrentDocument.Close();
#else        
        //((Form)this.Parent).Close();
#endif      
      }
    }

    private void 印刷PToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.printingText = this.azukiControl1.Text;
      this.printingPosition = 0;
      this.printFont = new Font("ＭＳ Ｐゴシック", 16f);
      PrintDocument printDocument = new PrintDocument();
      printDocument.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
      printDocument.Print();
    }

   // http://dobon.net/vb/dotnet/graphics/printtext.html
    private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      if (printingPosition == 0)
      {
        //改行記号を'\n'に統一する
        printingText = printingText.Replace("\r\n", "\n");
        printingText = printingText.Replace("\r", "\n");
      }
      //印刷する初期位置を決定
      int x = e.MarginBounds.Left;
      int y = e.MarginBounds.Top;
      //1ページに収まらなくなるか、印刷する文字がなくなるかまでループ
      while (e.MarginBounds.Bottom > y + printFont.Height &&
          printingPosition < printingText.Length)
      {
        string line = "";
        for (; ; )
        {
          //印刷する文字がなくなるか、
          //改行の時はループから抜けて印刷する
          if (printingPosition >= printingText.Length ||
              printingText[printingPosition] == '\n')
          {
            printingPosition++;
            break;
          }
          //一文字追加し、印刷幅を超えるか調べる
          line += printingText[printingPosition];
          if (e.Graphics.MeasureString(line, printFont).Width
              > e.MarginBounds.Width)
          {
            //印刷幅を超えたため、折り返す
            line = line.Substring(0, line.Length - 1);
            break;
          }
          //印刷文字位置を次へ
          printingPosition++;
        }
        //一行書き出す
        e.Graphics.DrawString(line, printFont, Brushes.Black, x, y);
        //次の行の印刷位置を計算
        y += (int)printFont.GetHeight(e.Graphics);
      }

      //次のページがあるか調べる
      if (printingPosition >= printingText.Length)
      {
        e.HasMorePages = false;
        //初期化する
        printingPosition = 0;
      }
      else
        e.HasMorePages = true;
    }

    private void 印刷プレビューVToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.printingText = this.azukiControl1.Text;
      this.printingPosition = 0;
      PrintDocument printDocument = new PrintDocument();
      printDocument.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
      new PrintPreviewDialog
      {
        Document = printDocument
      }.ShowDialog();
    }

    private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if Interface      
      //PluginBase.MainForm.CallCommand("Exit", "");
#else      
      //((Form)this.Parent).Close();
#endif    
    }

    private void 元に戻すUToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.azukiControl1.Undo();
    }

    private void やり直しRToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.azukiControl1.Redo();
    }

    private void 切り取りTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.azukiControl1.Cut();
    }

    private void コピーCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.azukiControl1.Copy();
    }

    private void 貼り付けPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.azukiControl1.Paste();
    }

    private void すべて選択AToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.azukiControl1.SelectAll();
    }

    private void ツールバーTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.toolStrip1.Visible = this.ツールバーTToolStripMenuItem.Checked;
    }

    private void ステータスバーUToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.statusStrip1.Visible = this.ステータスバーUToolStripMenuItem.Checked;
    }

    private void 行番号NToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.azukiControl1.ShowsLineNumber = this.行番号NToolStripMenuItem.Checked;
    }

    private void ルーラLToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.azukiControl1.ShowsHRuler = this.ルーラLToolStripMenuItem.Checked;
    }

    private void カーソル行SToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.azukiControl1.HighlightsCurrentLine = this.カーソル行SToolStripMenuItem.Checked;
    }

    private void 特殊文字CToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void ワードラップWToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.azukiControl1.ViewType == ViewType.Proportional)
      {
        this.azukiControl1.ViewType = ViewType.WrappedProportional;
        this.azukiControl1.ViewWidth = this.azukiControl1.ClientSize.Width - this.azukiControl1.View.HRulerUnitWidth * 2;
        this.ワードラップWToolStripMenuItem.Checked = true;
      }
      else
      {
        this.azukiControl1.ViewType = ViewType.Proportional;
        this.ワードラップWToolStripMenuItem.Checked = false;
      }
    }

    private void textTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.強調表示MenuItem_CheckClear();
      this.azukiControl1.Highlighter = null;
      ((ToolStripMenuItem)sender).Checked = true;
    }

    private void laTexLToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.強調表示MenuItem_CheckClear();
      this.azukiControl1.Highlighter = Highlighters.Latex;
      ((ToolStripMenuItem)sender).Checked = true;
    }

    private void cCCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.強調表示MenuItem_CheckClear();
      this.azukiControl1.Highlighter = Highlighters.Cpp;
      ((ToolStripMenuItem)sender).Checked = true;
    }

    private void javaJToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.強調表示MenuItem_CheckClear();
      this.azukiControl1.Highlighter = Highlighters.Java;
      ((ToolStripMenuItem)sender).Checked = true;
    }

    private void rubyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.強調表示MenuItem_CheckClear();
      this.azukiControl1.Highlighter = Highlighters.Java;
      ((ToolStripMenuItem)sender).Checked = true;
    }

    private void xMLXToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.強調表示MenuItem_CheckClear();
      this.azukiControl1.Highlighter = Highlighters.Xml;
      ((ToolStripMenuItem)sender).Checked = true;
    }

    private void cToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.強調表示MenuItem_CheckClear();
      this.azukiControl1.Highlighter = Highlighters.CSharp;
      ((ToolStripMenuItem)sender).Checked = true;
    }

    private void 検索FToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.azukiFindDlg == null || this.azukiFindDlg.IsDisposed)
      {
        this.azukiFindDlg = new azukiFindDialog(azukiDialogMode.Find, this.azukiControl1);
        this.azukiFindDlg.Show(this);
      }
    }

    private void 置換RToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.azukiFindDlg == null || this.azukiFindDlg.IsDisposed)
      {
        this.azukiFindDlg = new azukiFindDialog(azukiDialogMode.Replace, this.azukiControl1);
        this.azukiFindDlg.Show(this);
      }
    }

    private void 行へ移動GToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.azukiJumpDlg == null || this.azukiJumpDlg.IsDisposed)
      {
        this.azukiJumpDlg = new azukiJumpDialog(this.azukiControl1);
        this.azukiJumpDlg.ShowDialog(this);
      }
    }

    private void フォントと色ToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void 右端で折り返すToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void ウエブサーバで実行WToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
#if Interface        
        PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseEx;" + this.currentPath);
#else
        string chromePath = @"C:\Documents and Settings\kazuhiko\Local Settings\Application Data\Google\Chrome\Application\chrome.exe";
        Process.Start(chromePath, this.currentPath);
#endif      
      }
    }

    private void ファイル名を指定して実行OToolStripMenuItem1_Click(object sender, EventArgs e)
    {
#if Interface      
      PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.RunProcessDialog");
#endif    
    }

    private void タイムスタンプToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int begin = 0;
      int end = 0;
      this.azukiControl1.Document.GetSelection(out begin, out end);
      this.azukiControl1.Document.Replace(StringHandler.timestamp(), begin, end);
    }

    private void c見出しToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        string path = this.currentPath;
        string str = StringHandler.CHeading(path);
        this.azukiControl1.Document.Text = str + this.azukiControl1.Document.Text;
      }
      catch (Exception ex)
      {
        string text = ex.Message.ToString();
      }
    }

    private void バージョン情報AToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if Interface      
      PluginBase.MainForm.CallCommand("About", null);
#endif    
    }
    #endregion

    #region ToolBar Click Handler
    private void 新規作成NToolStripButton_Click(object sender, EventArgs e)
    {
      this.新規作成NToolStripMenuItem.PerformClick();
    }

    private void 開くOToolStripButton_Click(object sender, EventArgs e)
    {
      this.開くOToolStripMenuItem.PerformClick();
    }

    private void 上書き保存SToolStripButton_Click(object sender, EventArgs e)
    {
      this.上書き保存SToolStripMenuItem.PerformClick();
    }

    private void 印刷PToolStripButton_Click(object sender, EventArgs e)
    {
      this.印刷PToolStripMenuItem.PerformClick();
    }

    private void 切り取りUToolStripButton_Click(object sender, EventArgs e)
    {
      this.切り取りTToolStripMenuItem.PerformClick();
    }

    private void コピーCToolStripButton_Click(object sender, EventArgs e)
    {
      this.コピーCToolStripMenuItem.PerformClick();
    }

    private void 貼り付けPToolStripButton_Click(object sender, EventArgs e)
    {
      this.貼り付けPToolStripMenuItem.PerformClick();
    }

    private void ヘルプLToolStripButton_Click(object sender, EventArgs e)
    {
      this.バージョン情報AToolStripMenuItem.PerformClick();
    }

    private void メニューバーMToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.menuStrip1.Visible = this.メニューバーMToolStripMenuItem.Checked;
    }

    private void ステータスバーSToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.statusStrip1.Visible = this.ステータスバーSToolStripMenuItem1.Checked;
    }

    private void アプリケーションAToolStripMenuItem1_Click(object sender, EventArgs e)
    {

    }

    private void サクラエディタSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
#if Interface        
        Process.Start(this.settings.SakuraPath, this.currentPath);
#else
        Process.Start("C:\\TiuDevTools\\sakura\\sakura.exe", this.currentPath);
#endif
      }
    }

    private void pSPadPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
#if Interface        
        Process.Start(this.settings.PspadPath, this.currentPath);
#else
        Process.Start("F:\\Programs\\PSPad editor\\PSPad.exe", this.currentPath);
#endif      
      }
    }

    private void scintillaCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
#if Interface      
      PluginBase.MainForm.OpenEditableDocument(this.currentPath);
#else      
      Process.Start(@"F:\Programs\FlashDevelop\FlashDevelop.exe", "-reuse " + this.currentPath);
#endif    
      }
    }

    private void richTextBoxToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if Interface      
      PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.CreateCustomDocument;RichTextEditor|" + this.currentPath);
#endif    
    }

    private void エクスプローラEToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
          Process.Start(this.currentPath);
      }
      else  if (Directory.Exists(Path.GetDirectoryName(this.currentPath)))
      {
          Process.Start(Path.GetDirectoryName(this.currentPath));
      }
      //MDIForm.CommonLibrary.ProcessHandler.Run_Explorer(this.currentPath);
    }

    private void コマンドプロンプトCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (Directory.Exists(this.currentPath))
      {
        Directory.SetCurrentDirectory(this.currentPath);
        Process.Start("C:\\windows\\system32\\cmd.exe");
      }
      else if (Directory.Exists(Path.GetDirectoryName(this.currentPath)))
      {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(this.currentPath));
        Process.Start("C:\\windows\\system32\\cmd.exe");
      }
      //MDIForm.CommonLibrary.ProcessHandler.Run_Cmd(this.currentPath);
    }

    private void ファイル名を指定して実行OToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if Interface      
      PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.RunProcessDialog");
#endif    
    }

    private void リンクを開くLToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }
    #endregion

    private int toggleIndex=0;
    private void コンソールSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = this.toggleIndex % 3;
      if (num == 0)
      {
        this.splitContainer1.Panel2Collapsed = true;
        this.splitContainer1.Panel1Collapsed = false;
      }
      else if (num == 1)
      {
        this.splitContainer1.Panel2Collapsed = false;
        this.splitContainer1.Panel1Collapsed = true;
      }
      else if (num == 2)
      {
        this.splitContainer1.Panel2Collapsed = false;
        this.splitContainer1.Panel1Collapsed = false;
      }
      this.toggleIndex++;

    }

    private void スクリプトを実行XToolStripMenuItem_Click(object sender, EventArgs e)
    {

      if (Path.GetExtension(this.currentPath) == ".cs")
      {
        //PluginBase.MainForm.CallCommand("ExecuteScript", "Development;" + this.currentPath);
      }
      else this.RunScript();
      //this.currentPath = path;
      //this.SetHighlight(path);
      //this.azukiControl1.Document.Text = MDIForm.CommonLibrary.Lib.File_ReadToEndDecode(path);
      //this.azukiControl1.Document.IsDirty = false;
      //this.azukiControl1.Tag = path;
      //MessageBox.Show(this.settings.AzukiEditorDefaultFont.ToString());
    }

    public void RunScript()
    {
      /**
      * Entry point of the script.
      ProjectManager : pluginGuid =   "30018864-fadd-1122-b2a5-779832cbbf23";
      FileExplorer :   pluginGuid =   "f534a520-bcc7-4fe4-a4b9-6931948b2686";
      XmlTreemmenu :   pluginGuid =   "0538077E-8C37-4A2B-962B-8FB77DC9F325";
      */
      //String XMLTreeMenu_Guid = "0538077E-8C37-4A2B-962B-8FB77DC9F325";
      //XMLTreeMenu.PluginMain a = (XMLTreeMenu.PluginMain)PluginBase.MainForm.FindPlugin(XMLTreeMenu_Guid);
/*
      String argstring = PluginBase.MainForm.StatusStrip.Tag.ToString();
      String[] tmpstr = argstring.Split('|');
      String action = tmpstr[0];
      String command = tmpstr[1];
      String args = tmpstr[2];
      String path = tmpstr[3];
      String option = tmpstr[4];
*/
      String file = this.currentPath;// PluginBase.MainForm.CurrentDocument.FileName;
      //Globals.MainForm.CallCommand("RunProcess",@"C:\TiuDevTools\sakura\sakura.exe;" + path);
      String ext = System.IO.Path.GetExtension(file);
      //String title = Path.GetFileNameWithoutExtension(file);
      String title = Path.GetFileName(file);
      //MessageBox.Show(option);
      String cmd2 = "";

      switch (ext)
      {
        case ".php":
          cmd2 = @"C:\eclipse461\xampp\php\php.exe";
          break;
        case ".wsf":
        case ".js":
        case ".vbs":
          cmd2 = @"C:\windows\system32\cscript.exe";
          //cmd2 = @"C:\windows\system32\wscript.exe";
          break;
        case ".cs":
          cmd2 = @"C:\HDD_F\Programs\cs-script\cscs.exe";
          //MessageBox.Show(cmd2);
          //cmd2 = @"C:\HDD_F\Programs\csws.exe";
          break;
      }

      /// String output = StringHandler.ConvertEncoding(getStandardOutput(cmd2, file),
      /// System.Text.Encoding src = System.Text.Encoding.ASCII;
      /// System.Text.Encoding dest = System.Text.Encoding.GetEncoding("Shift_JIS");
      ///	System.Text.Encoding.GetEncoding("Shift_JIS"));
      String output = ProcessHandler.getStandardOutput(cmd2, file);
      this.azukiControl2.Document.Text = output;
      /*
      switch (option)
      {
        case "Console":
        case "console":
        case "con":
          Process.Start("cmd.exe", "/k " + cmd2 + " " + file);
          break;
        case "textlog":
        case "richtext":
          a.pluginUI.CreateCustomDocument("RichTextEditor", "[出力]" + cmd2 + "!" + output);
          break;
        case "trace":
          //TraceManager.Add(result);
          break;
        case "Document":
        case "document":
        case "doc":
        default:
          PluginBase.MainForm.CallCommand("New", "");
          PluginBase.MainForm.CurrentDocument.SciControl.DocumentStart();
          PluginBase.MainForm.CurrentDocument.SciControl.ReplaceSel(output);
          PluginBase.MainForm.CurrentDocument.Text = "[出力]" + title;
          break;
      }
      */
      
      
      
      //MessageBox.Show(output);

      //write_file_encoding(@"F:\temp\FDTreeMenu.xml", output, "utf-8");
      // プラグインのpublic property を取得する 以下OK
      //MessageBox.Show(Globals.MainForm.FindPlugin(XMLTreeMenu_Guid).ToString());

      //MDIForm.NodeInfo inf = a.pluginUI.treeView1.SelectedNode.Tag as MDIForm.NodeInfo;
      //MessageBox.Show(Globals.MainForm.StatusStrip.Tag.ToString());
      //a.pluginUI.LoadFile(@"F:\temp\FDTreeMenu.xml");

      /* ファイルエクスプローラの選択ファイルを取得する 以下OK
      String b = Globals.MainForm.FindPlugin(FileExplorer_Guid).Name;
      FileExplorer.PluginMain a 
      = (FileExplorer.PluginMain)Globals.MainForm.FindPlugin("f534a520-bcc7-4fe4-a4b9-6931948b2686");
      System.Windows.Forms.ListView fileView = a.FileView;

      String[] files = GetSelectedFiles(fileView);

      for(Int32 i = 0; i< fileView.SelectedItems.Count; i++)
      {
          MessageBox.Show(files[i]);
      }
      //MessageBox.Show(fileView.SelectedItems.Count.ToString()); // FileExplorer.PluginMain
      */
    }

    private void スクリプトSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.RunScript();
    }
  }

}
