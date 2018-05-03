//#undef Interface
#define Interface

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Collections;
using System.Diagnostics;
//using CSScriptLibrary;
#if Interface
using AntPlugin.CommonLibrary;
using PluginCore;
using WeifenLuo.WinFormsUI.Docking;
using PluginCore.Helpers;
#else
using CommonLibrary;
#endif

namespace AntPlugin.XMLTreeMenu.Controls
{
#if Interface
  public partial class RichTextEditor : UserControl//, IMDIForm
#endif
	{
    #region RichTextEditor Valiables
    public findDialog findDlg = null;
    public jumpDialog jumpDlg = null;
    private string printingText = string.Empty;
    private int printingPosition = 0;
    private Font printFont = null;
    public string currentPath = string.Empty;
    public Point currentPoint = new Point(0, 0);
    public bool modifiedFlag = false;
    public List<string> previousDocuments = new List<string>();
    public static ImageList imageList1;
#if Interface
    private PluginMain pluginMain;
    private PluginUI pluginUI;
    private ToolStripMenuItem toolStripMenuItem2;
    //private XMLTreeMenu.Settings settings;
#endif
    #endregion

    #region Properties
#if Interface
/*
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
*/
#endif
    public RichTextBox RichTextBox
    {
      get
      {
        return this.richTextBox1;
      }
    }

    public ToolStrip ToolBar
    {
      get
      {
        return this.toolStrip1;
      }
    }

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
    public RichTextEditor()
    {
      InitializeComponent();
			this.InitializeInterface();
			IntializeRichTextPanel();
    }
    public RichTextEditor(string[] args)
    {
      InitializeComponent();
      IntializeRichTextPanel();
      if (!String.IsNullOrEmpty(args[0]) && File.Exists(args[0]))
      {
        this.LoadFile(args[0]);
      }
    }

    public RichTextEditor(PluginUI ui)
    {
      this.pluginUI = ui;
      this.pluginMain = ui.pluginMain;
      InitializeComponent();
      IntializeRichTextPanel();
      //i/f (!String.IsNullOrEmpty(args[0]) && File.Exists(args[0]))
      //{
        //this.LoadFile(args[0]);
     // }
    }

    #endregion

    #region Initialization
    public void InitializeInterface()
    {
#if Interface
/*
      string guid = "0538077E-8C37-4A2B-962B-8FB77DC9F325";
      this.xmlTreeMenu = (PluginMain)PluginBase.MainForm.FindPlugin(guid);
      this.settings = this.xmlTreeMenu.Settings as XMLTreeMenu.Settings;
      this.Instance = new ChildFormControlClass();
      this.Instance.name = "RtchTextEditor";
      this.Instance.toolStrip = this.toolStrip1;
      this.Instance.menuStrip = this.menuStrip1;
      this.Instance.statusStrip = this.statusStrip1;
      this.Instance.スクリプトToolStripMenuItem = this.スクリプトCToolStripMenuItem;
      this.Instance.toolStripStatusLabel = this.toolStripStatusLabel1;
      this.toolStrip1.Renderer = new DockPanelStripRenderer(true);
      this.Instance.PreviousDocuments = this.PreviousDocuments;
      //this.MainForm.xmlTreeMenu_pluginUI.
*/ 
#endif    
		}
   
    public void IntializeRichTextPanel()
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
      //this.toolStripDropDownButton1.Image = System.Drawing.Image.FromFile(resource + @"famfamicons\asterisk_orange.png");
      this.toolStripDropDownButton2.Image = imageList1.Images[61];
      this.toolStripDropDownButton3.Image = imageList1.Images[15];
      this.toolStripDropDownButton4.Image = imageList1.Images[117];
    }

    public void InitializeImageList()
    {
#if Interface      
			String path = Path.Combine(PathHelper.BaseDir, @"SettingData\PSPad.bmp");
#endif
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
       // = System.Drawing.Image.FromFile(Path.Combine(baseDir, @"Settings\icons\EmEditor_16x16.png"));
      //System.Drawing.Image img
      //  = System.Drawing.Image.FromFile(Path.Combine(PathHelper.BaseDir, @"SettingData\icons\EmEditor_16x16.png"));
      //imageList1.Images.Add(img); //151
      //AddIcon("EmEditor_16x16.png"); //151
      //AddIcon("W95Icon0254_yellow.ico"); //152
      //AddIcon("cmd.ico"); //153
    }
    /**
     * Handles the click event for the menu items.
     */
    public void AddIcon(String name)
    {
#if Interface
      String iconPath = Path.Combine(PathHelper.BaseDir, @"SettingData\icons\" + name); //"
#endif      
      System.Drawing.Icon ico = new System.Drawing.Icon(iconPath, 16, 16);
      //Bitmapに変換する
      System.Drawing.Bitmap bmp = ico.ToBitmap();
      //変換したBitmapしか使わないならば、元のIconは解放できる
      ico.Dispose();
      //イメージを表示する
      imageList1.Images.Add(bmp);
    }
    #endregion

    #region Event Handler
    private void RichTextEditor_Load(object sender, EventArgs e)
    {
      this.printingText = this.richTextBox1.Text;
      this.printingPosition = 0;
      this.printFont = new Font("ＭＳ Ｐゴシック", 10f);
      this.IntializeSettings();
      //MessageBox.Show(base.Parent.GetType().Name);
      try
      {
        //if (base.Parent.GetType().Name == "TabbedDocument")
        //{
        ((Form)base.Parent).FormClosing += new FormClosingEventHandler(this.parentForm_Closing);
        //}
      }
      catch { }
      this.richTextBox1.Modified = (this.modifiedFlag = false);

#if Interface
      string[] array =  null;
      if (this.currentPath != string.Empty)
      {
        array = this.currentPath.Split(new char[]{'!'});
        this.richTextBox1.Tag = this.currentPath;
      }
      else if ((string)this.richTextBox1.Tag != string.Empty)
      {
				array = PluginBase.MainForm.ProcessArgString((string)this.richTextBox1.Tag).Split(new char[]{'!'});
        this.currentPath = array[0];
        //MessageBox.Show(this.currentPath);
      }
      // KAHATA: 一応FIX?
      if (array != null && array.Length > 1)
      {
        string text = array[1];
        if (string.IsNullOrEmpty(this.currentPath))
        {
          this.currentPath = "TextLog";
        }
        if (text.Trim() != "")
        {
          this.richTextBox1.Text = text;
        }
        this.richTextBox1.Modified = (this.modifiedFlag = true);
				((DockContent)base.Parent).TabText = "[出力]" + Path.GetFileName(array[0]);
				((Form)this.Parent).Text = "[出力]" + Path.GetFileName(array[0]);
        
				this.AddPreviousDocuments((string)this.richTextBox1.Tag);
      }
      else if (File.Exists(this.currentPath) && Lib.IsTextFile(this.currentPath))
      {
        if (Path.GetExtension(this.currentPath) == ".rtf")
        {
          this.richTextBox1.LoadFile(this.currentPath);
        }
        else
        {
          this.richTextBox1.Text = Lib.File_ReadToEndDecode(this.currentPath);
        }
        this.richTextBox1.Modified = (this.modifiedFlag = false);
       
				((DockContent)base.Parent).TabText = Path.GetFileName(this.currentPath);

				((Form)this.Parent).Text = Path.GetFileName(this.currentPath);

				this.AddPreviousDocuments(this.currentPath);
      }
      this.UpdateStatusText(this.currentPath);
#endif


    }

    public void IntializeSettings()
    {
#if Interface      
			//this.previousDocuments = this.settings.PreviousRichTextEditorDocuments;
      //this.toolStrip1.Visible = this.settings.RichTextEditorToolBarVisible;
      //this.statusStrip1.Visible = this.settings.RichTextEditorStatusBarVisible;
      //this.richTextBox1.Font = this.settings.RichTextEditorDefaultFont;
#endif			
			this.ツールバーTToolStripMenuItem.Checked = this.toolStrip1.Visible;
			this.ステータスバーSToolStripMenuItem.Checked = this.statusStrip1.Visible;
      this.PopulatePreviousDocumentsMenu();
    }

    private void parentForm_Closing(object sender, CancelEventArgs e)
    {
      if (this.richTextBox1.Modified)
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
/*
      this.settings.PreviousRichTextEditorDocuments = this.previousDocuments;
      this.settings.RichTextEditorMenuBarVisible = this.menuStrip1.Visible;
      this.settings.RichTextEditorToolBarVisible = this.toolStrip1.Visible;
      this.settings.RichTextEditorStatusBarVisible = this.statusStrip1.Visible;
      this.settings.RichTextEditorDefaultFont = this.richTextBox1.Font
    */    ;
#endif
    }

    private void RichTextEditor_Leave(object sender, EventArgs e)
    {
      if (!this.toolStrip1.Visible)
      {
      }
    }

    private void RichTextEditor_Enter(object sender, EventArgs e)
    {
      if (!this.toolStrip1.Visible)
      {
      }
    }

    #endregion

    #region Click Handler
    private void 新規作成NToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.richTextBox1.Modified)
      {
      }
      this.richTextBox1.Clear();
      this.richTextBox1.Modified = (this.modifiedFlag = false);
#if Interface      
      ((Form)base.Parent).Text = "無題";
#endif    
    }

    private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string fileName = "default.txt";
      string filter = "All files(*.*)|*.*|Supported files|*.txt;*.log;*.ini;*.inf;*.tex;*.htm;*.html;*.css;*.js;*.xml;*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx;*.cs;*.java;*.py;*.rb;*.pl;*.vbs;*.bat|Text file(*.txt, *.log, *.tex, ...)|*.txt;*.log;*.ini;*.inf;*.tex|HTML file(*.htm, *.html)|*.htm;*.html|CSS file(*.css)|*.css|Javascript file(*.js)|*.js|XML file(*.xml)|*.xml|C/C++ source(*.c, *.h, ...)|*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx|C# source(*.cs)|*.cs|Java source(*.java)|*.java|Python script(*.py)|*.py|Ruby script(*.rb)|*.rb|Perl script(*.pl)|*.pl|VB script(*.vbs)|*.vbs|Batch file(*.bat)|*.bat";
#if Interface
      string workingDirectory = PluginBase.MainForm.WorkingDirectory;
      string text = Lib.File_OpenDialog(fileName, workingDirectory, filter);//"
#endif
      this.currentPath = text;
      try
      {
        if (Path.GetExtension(this.currentPath) == ".rtf")
        {
          this.richTextBox1.LoadFile(text);
        }
        else
        {
#if Interface         
          this.richTextBox1.Text = Lib.File_ReadToEndDecode(this.currentPath);
#endif        
        }
        this.richTextBox1.Tag = this.currentPath;
#if Interface        
        ((DockContent)base.Parent).TabText = Path.GetFileName(text);
#endif        
        this.AddPreviousDocuments(text);
        this.PopulatePreviousDocumentsMenu();
        this.UpdateStatusText(this.currentPath);
      }
      catch (Exception ex)
      {
        string message = ex.Message.ToString();
        //MessageBox.Show(Lib.OutputError(message));
        MessageBox.Show(ex.Message.ToString());
      }
    }

    public void LoadFile(string path)
    {
      string text = path;
      this.currentPath = text;
      try
      {
        if (Path.GetExtension(this.currentPath) == ".rtf")
        {
          this.richTextBox1.LoadFile(text);
        }
        else
        {
          this.richTextBox1.Text = File_ReadToEndDecode(this.currentPath);
        }
        this.richTextBox1.Tag = this.currentPath;
#if Interface        
        ((DockContent)base.Parent).TabText = Path.GetFileName(text);
#endif        
        this.AddPreviousDocuments(text);
        this.PopulatePreviousDocumentsMenu();
        this.UpdateStatusText(this.currentPath);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
      }
    }

    private void 上書き保存SToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string[] array = ((string)this.richTextBox1.Tag).Split(new char[]{'!'});
      if (array.Length > 1)
      {
        this.名前を付けて保存AToolStripMenuItem_Click(sender, e);
      }
      else
      {
#if Interface        
        Lib.File_BackUpCopy(this.currentPath);
#endif
        if (Path.GetExtension(this.currentPath) == ".rtf")
        {
          this.richTextBox1.SaveFile(this.currentPath, RichTextBoxStreamType.RichText);
        }
        else
        {
#if Interface        
          Lib.File_SaveEncode(this.currentPath, this.richTextBox1.Text, Lib.File_GetCode(this.currentPath));
#endif
        }
        this.richTextBox1.Modified = (this.modifiedFlag = false);
#if Interface        
        ((DockContent)base.Parent).TabText = Path.GetFileName(this.currentPath);
#endif
      }
    }

    private void 名前を付けて保存AToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string text = string.Empty;
      ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
      if (toolStripMenuItem.Name == "sHIFTJISで保存ToolStripMenuItem")
      {
        text = "SHIFT_JIS";
      }
      else if (toolStripMenuItem.Name == "uTF8で保存ToolStripMenuItem")
      {
        text = "UTF-8";
      }
      else if (toolStripMenuItem.Name == "名前を付けて保存AToolStripMenuItem")
      {
        text = "UTF-8";
      }
      else
      {
        text = "UTF-8";
      }
      string filter = "Textファイル(*.txt)|*.txt|c,cs関連ファイル(*.c;*.cs;.c*.cpp;*,h)|*.c;*.cs;*.cpp;*.h|HTMLファイル(*.html;*.htm)|*.html;*.htm|すべてのファイル(*.*)|*.*";
      string[] array = ((string)this.richTextBox1.Tag).Split(new char[]
			{
				'!'
			});
      string initialDirectory;
      string fileName;
      if (this.richTextBox1.Tag == null)
      {
#if Interface        
        initialDirectory = PluginBase.MainForm.WorkingDirectory;
#endif        
        fileName = "新しいファイル.txt";
      }
      else if (array.Length > 1)
      {
        if (File.Exists(array[0]))
        {
          initialDirectory = Path.GetDirectoryName(array[0]);
        }
        else
        {
#if Interface
          initialDirectory = PluginBase.MainForm.WorkingDirectory; //"
#endif        
        }
        fileName = "新しいファイル.txt";
      }
      else
      {
        try
        {
          initialDirectory = Path.GetDirectoryName(array[0]);
          fileName = Path.GetFileName(array[0]);
#if Interface
          text = Lib.File_GetCode(array[0]);
#endif
          if (text == string.Empty)
          {
            text = "UTF-8";
          }
        }
        catch (Exception ex)
        {
          string message = ex.Message.ToString();
          //Lib.OutputError(message);
          MessageBox.Show(message);
          initialDirectory = Path.GetDirectoryName(array[0]);
          fileName = Path.GetFileName(array[0]);
          text = "UTF-8";
        }
      }
      try
      {
#if Interface
        string text2 = Lib.File_SaveDialog(fileName, initialDirectory, filter);
#endif
        if (File.Exists(text2))
        {
#if Interface
          Lib.File_BackUpCopy(text2);
#endif
        }
        if (text2 != null)
        {
          if (Path.GetExtension(text2) == ".rtf")
          {
            this.richTextBox1.SaveFile(text2, RichTextBoxStreamType.RichText);
          }
          else
          {
#if Interface
            Lib.File_SaveEncode(text2, this.richTextBox1.Text, text);
#endif
          }
        }
        this.currentPath = text2;
        this.richTextBox1.Modified = (this.modifiedFlag = false);
        this.richTextBox1.Tag = this.currentPath;
#if Interface
        ((DockContent)base.Parent).TabText = Path.GetFileName(this.currentPath);
#endif
        this.AddPreviousDocuments(this.currentPath);
        this.PopulatePreviousDocumentsMenu();
        this.UpdateStatusText(this.currentPath);
      }
      catch (Exception ex)
      {
        string message2 = ex.Message.ToString();
        //MessageBox.Show(Lib.OutputError(message2));
        MessageBox.Show(message2);
      }
    }

    private void 閉じるCToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if Interface      
      PluginBase.MainForm.CurrentDocument.Close();
#endif    
    }

    private void 印刷PToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.printingText = this.richTextBox1.Text;
      this.printingPosition = 0;
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
      this.printingText = this.richTextBox1.Text;
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
      PluginBase.MainForm.CallCommand("Exit", "");
#endif    
    }

    private void 元に戻すUToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.richTextBox1.CanUndo)
      {
        this.richTextBox1.Undo();
        this.richTextBox1.ClearUndo();
      }
    }

    private void やり直しRToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.richTextBox1.CanRedo)
      {
        this.richTextBox1.Redo();
      }
    }

    private void 切り取りTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.richTextBox1.Cut();
    }

    private void コピーCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.richTextBox1.Copy();
    }

    private void 貼り付けPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.richTextBox1.Paste();
    }

    private void すべて選択AToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.richTextBox1.SelectAll();
    }

    private void ツールバーTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.toolStrip1.Visible = !this.toolStrip1.Visible;
    }

    private void ステータスバーSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.statusStrip1.Visible = !this.statusStrip1.Visible;
      ステータスバーSToolStripMenuItem1.Checked = ステータスバーSToolStripMenuItem.Checked;
    }

    private void 検索FToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.findDlg == null || this.findDlg.IsDisposed)
      {
        this.findDlg = new findDialog(dialogMode.Find, this.richTextBox1);
        this.findDlg.Show(this);
      }
    }

    private void 置換RToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.findDlg == null || this.findDlg.IsDisposed)
      {
        this.findDlg = new findDialog((((ToolStripMenuItem)sender).Name == "menuFind") ? dialogMode.Find : dialogMode.Replace, this.richTextBox1);
        this.findDlg.Show(this);
      }
    }

    private void 行へ移動GToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.jumpDlg == null || this.jumpDlg.IsDisposed)
      {
        this.jumpDlg = new jumpDialog(this.richTextBox1);
        this.jumpDlg.ShowDialog(this);
      }
    }

    private void タイムスタンプToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.richTextBox1.SelectedText = timestamp();
    }

    private void c見出しToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        string path = this.currentPath;
        string str = CHeading(path);
        this.richTextBox1.Text = str + this.richTextBox1.Text;
      }
      catch (Exception ex)
      {
        string text = ex.Message.ToString();
      }

    }

    private void 右端で折り返すToolStripMenuItem_Click(object sender, EventArgs e)
    {
      bool flag = !this.右端で折り返すToolStripMenuItem.Checked;
      this.右端で折り返すToolStripMenuItem.Checked = flag;
      this.行へ移動GToolStripMenuItem.Enabled = !flag;
      this.richTextBox1.WordWrap = flag;
      this.richTextBox1.Modified = false;
    }

    private void フォントと色ToolStripMenuItem_Click(object sender, EventArgs e)
    {
       settingDialog settingDialog = new settingDialog(this.richTextBox1);
      settingDialog.ShowDialog(this);
    }

    private void スクリプトを実行XToolStripMenuItem_Click(object sender, EventArgs e)
    {
      /*
      String code = this.richTextBox1.Text;
      //MessageBox.Show(code);
      Assembly assembly = CSScript.LoadCode(code);
      AsmHelper helper = new AsmHelper(assembly);
      //helper.CreateObject("Test");
      helper.Invoke("*.Main", new object[] { }); //sum == 3; 
      */
      /*
      Assembly assembly = CSScript.LoadCode(
            @"using System;
              public class Calculator
              {
                    static public int Add(int a, int b)
                    {
                       return a + b;
                    }
              }");
      AsmHelper calc = new AsmHelper(assembly);
      int sum = (int)calc.Invoke("Calculator.Add", 1, 2); //sum == 3; 
      MessageBox.Show(sum.ToString());
      */
    }

    private void スクリプトメニュー更新RToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void スクリプトを編集EToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void 新規作成NToolStripButton_Click(object sender, EventArgs e)
    {
      this.新規作成NToolStripMenuItem_Click(sender, e);
    }

    private void 開くOToolStripButton_Click(object sender, EventArgs e)
    {
      this.開くOToolStripMenuItem_Click(sender, e);
    }

    private void 上書き保存SToolStripButton_Click(object sender, EventArgs e)
    {
      this.上書き保存SToolStripMenuItem_Click(sender, e);
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      this.名前を付けて保存AToolStripMenuItem_Click(sender, e);
    }

    private void 印刷PToolStripButton_Click(object sender, EventArgs e)
    {

    }

    private void 切り取りUToolStripButton_Click(object sender, EventArgs e)
    {
      this.切り取りTToolStripMenuItem_Click(sender, e);
    }

    private void コピーCToolStripButton_Click(object sender, EventArgs e)
    {
      this.コピーCToolStripMenuItem_Click(sender, e);
    }

    private void 貼り付けPToolStripButton_Click(object sender, EventArgs e)
    {
      this.貼り付けPToolStripMenuItem_Click(sender, e);
    }

    private void メニューバーMToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.menuStrip1.Visible = !this.menuStrip1.Visible;
    }

    private void サクラエディタSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
        Process.Start(@"C:\Program Files (x86)\sakura\sakura.exe", this.currentPath);
      }
    }

    private void pSPadPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
        Process.Start(@"C:\Program Files (x86)\PSPad editor\PSPad.exe", this.currentPath);
      }
    }

    private void scintillaCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
#if Interface        
        PluginBase.MainForm.OpenEditableDocument(this.currentPath);
#endif      
      }
    }

    private void azukiEditorZToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if Interface      
      PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.CreateCustomDocument;AzukiEditor|" + this.currentPath);
#endif    
    }

    private void エクスプローラEToolStripMenuItem_Click(object sender, EventArgs e)
    {
      String path = this.currentPath;
      if (File.Exists(this.currentPath))
      {
        if (System.IO.Directory.Exists(path))
        {
          Process.Start(path);
        }
        else if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
        {
          Process.Start(Path.GetDirectoryName(path));
        }
        //ProcessHandler.Run_Explorer(this.currentPath);
      }
    }

    private void コマンドプロンプトCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      String path = this.currentPath;
      if (File.Exists(this.currentPath))
      {
        if (System.IO.Directory.Exists(path))
        {
          System.IO.Directory.SetCurrentDirectory(path);
          Process.Start(@"C:\windows\xyxtem32\cmd.exe");
        }
        else if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
        {
          System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
          Process.Start(@"C:\windows\xyxtem32\cmd.exe");
        }
        //ProcessHandler.Run_Cmd(this.currentPath);
      }
    }

    private void 現在のファイルをブラウザで開くWToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
#if Interface      
        PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseEx;" + this.currentPath);
#endif      
      }
    }

    private void microsoftWordで開くWToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
        Process.Start("C:\\Program Files\\Microsoft Office\\OFFICE11\\WINWORD.EXE", this.currentPath);
      }
    }

    private void ファイル名を指定して実行OToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if Interface      
      PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.RunProcessDialog");
#endif    
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      FontDialog fontDialog = new FontDialog();
      fontDialog.Font = this.richTextBox1.Font;
      fontDialog.Color = this.richTextBox1.ForeColor;
      fontDialog.MaxSize = 36;
      fontDialog.MinSize = 8;
      fontDialog.FontMustExist = true;
      fontDialog.AllowVerticalFonts = false;
      fontDialog.ShowColor = true;
      fontDialog.ShowEffects = true;
      fontDialog.FixedPitchOnly = false;
      fontDialog.AllowVectorFonts = true;
      if (fontDialog.ShowDialog() != DialogResult.Cancel)
      {
        this.richTextBox1.SelectionFont = fontDialog.Font;
        this.richTextBox1.SelectionColor = fontDialog.Color;
      }
    }

    private void toolStripButton3_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.richTextBox1.SelectionFont.Bold)
        {
          this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont, FontStyle.Regular);
          this.toolStripButton12.CheckState = CheckState.Unchecked;
        }
        else
        {
          this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont, FontStyle.Bold);
          this.toolStripButton12.CheckState = CheckState.Checked;
        }
      }
      catch (Exception ex)
      {
        string str = ex.Message.ToString();
        Lib.OutputError("文字の太字設定で例外が発生しました。\n選択範囲を再設定してください\n\n" + str);
      }
    }

    private void toolStripButton4_Click(object sender, EventArgs e)
    {
      if (this.richTextBox1.SelectionFont.Italic)
      {
        this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont, FontStyle.Regular);
        this.toolStripButton13.CheckState = CheckState.Unchecked;
      }
      else
      {
        this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont, FontStyle.Italic);
        this.toolStripButton13.CheckState = CheckState.Checked;
      }

    }

    private void toolStripButton5_Click(object sender, EventArgs e)
    {
      if (this.richTextBox1.SelectionFont.Underline)
      {
        this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont, FontStyle.Regular);
        this.toolStripButton14.CheckState = CheckState.Unchecked;
      }
      else
      {
        this.richTextBox1.SelectionFont = new Font(this.richTextBox1.SelectionFont, FontStyle.Underline);
        this.toolStripButton14.CheckState = CheckState.Checked;
      }
    }

    private void toolStripButton6_Click(object sender, EventArgs e)
    {
      ColorDialog colorDialog = new ColorDialog();
      colorDialog.CustomColors = new int[]
			{
				51,
				102,
				153,
				204,
				13056,
				13107,
				13158,
				13209,
				13260,
				26112,
				26163,
				26214,
				26265,
				26316,
				39168,
				39219
			};
      if (colorDialog.ShowDialog() == DialogResult.OK)
      {
        this.richTextBox1.SelectionColor = colorDialog.Color;
      }
    }

    private void toolStripButton7_Click(object sender, EventArgs e)
    {
      ColorDialog colorDialog = new ColorDialog();
      colorDialog.CustomColors = new int[]
			{
				51,
				102,
				153,
				204,
				13056,
				13107,
				13158,
				13209,
				13260,
				26112,
				26163,
				26214,
				26265,
				26316,
				39168,
				39219
			};
      if (colorDialog.ShowDialog() == DialogResult.OK)
      {
        this.richTextBox1.SelectionBackColor = colorDialog.Color;
      }
    }

    private void toolStripButton8_Click(object sender, EventArgs e)
    {
      this.richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
      this.toolStripButton8.CheckState = CheckState.Checked;
    }

    private void toolStripButton9_Click(object sender, EventArgs e)
    {
      this.richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
      this.toolStripButton9.CheckState = CheckState.Checked;
    }

    private void toolStripButton10_Click(object sender, EventArgs e)
    {
      this.richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
      this.toolStripButton10.CheckState = CheckState.Checked;
    }

    private void toolStripButton11_Click(object sender, EventArgs e)
    {

    }

    private void toolStripButton12_Click(object sender, EventArgs e)
    {
      string selectedText = this.richTextBox1.SelectedText;
      string[] array = selectedText.Split(new char[]{'\n'});
      for (int i = 0; i < array.Length; i++)
      {
        array[i] = string.Format("{0}", i + 1) + "." + array[i];
      }
      this.richTextBox1.SelectedText = string.Join("\n", array);
    }

    private void toolStripButton13_Click(object sender, EventArgs e)
    {
      this.richTextBox1.SelectionBullet = true;
    }

    private void toolStripButton14_Click(object sender, EventArgs e)
    {
      this.richTextBox1.Clear();
      this.richTextBox1.SelectionIndent = 20;
      this.richTextBox1.Font = new Font("Lucinda Console", 12f);
      this.richTextBox1.SelectedText = "All text is indented 20 pixels from the left edge of the RichTextBox.\n";
      this.richTextBox1.SelectedText = "You can use this property to provide proper indentation \nsuch as when writing a letter.";
      this.richTextBox1.SelectedText = "After this paragraph the indent is returned to normal spacing.\n\n";
      this.richTextBox1.SelectionIndent = 0;
      this.richTextBox1.SelectedText = "No indenation is applied to this paragraph. All text in the paragraph flows from each control edge.";
    }

    private void toolStripButton15_Click(object sender, EventArgs e)
    {

    }

    private void toolStripButton16_Click(object sender, EventArgs e)
    {
      Image image = null;
#if Interface
      string text = Lib.File_OpenDialog("menu.xml", PathHelper.DataDir, "TreeDataファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*");
#endif

      if (File.Exists(text))
      {
        try
        {
          image = Image.FromFile(text);
        }
        catch (Exception ex)
        {
          string message = ex.Message.ToString();
          MessageBox.Show(Lib.OutputError(message));
        }
        Clipboard.Clear();
        Clipboard.SetImage(image);
        this.richTextBox1.Paste();
      }
    }

    private void delegate試験ToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void queryString試験ToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void toolStripMenuItem2_Click(object sender, EventArgs e)
    {

    }

    private void ステータスバーSToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.statusStrip1.Visible = !this.statusStrip1.Visible;
      this.ステータスバーSToolStripMenuItem.Checked = this.ステータスバーSToolStripMenuItem1.Checked;
    }
    
    #endregion

    #region Previous Document
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
          toolStripMenuItem2.Tag = text;
          string[] array = text.Split(new char[]
					{
						'!'
					});
          string text2 = array[0];
          if (string.IsNullOrEmpty(text2))
          {
            text2 = "TextLog";
          }
          if (array.Length > 1)
          {
            if (!text2.StartsWith("[出力]"))
            {
              toolStripMenuItem2.Text = "[出力]" + text2;
            }
            else
            {
              toolStripMenuItem2.Text = text2;
            }
          }
          if (array.Length == 1)
          {
            toolStripMenuItem2.Text = text;
          }
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
      string[] array = text.Split(new char[]
			{
				'!'
			});
      if (array.Length == 1)
      {
        this.currentPath = text;
        //kari
#if Interface        
        if (File.Exists(this.currentPath) && Lib.IsTextFile(this.currentPath))
#else
        if (File.Exists(this.currentPath))// && Lib.IsTextFile(this.currentPath))
#endif        
        {
          try
          {
            this.richTextBox1.Tag = this.currentPath;
            if (Path.GetExtension(this.currentPath) == ".rtf")
            {
              this.richTextBox1.LoadFile(text);
            }
            else
            {
#if Interface
              this.richTextBox1.Text = Lib.File_ReadToEndDecode(text);
#else
              //this.richTextBox1.Text = Lib.File_ReadToEndDecode(text);
#endif            
            }
#if Interface
            ((DockContent)base.Parent).TabText = Path.GetFileName(text);
#else
            this.Text = Path.GetFileName(text);
#endif
            this.AddPreviousDocuments(text);
            this.UpdateStatusText(text);
          }
          catch (Exception ex)
          {
            string message = ex.Message.ToString();
            //MessageBox.Show(Lib.OutputError(message));
            MessageBox.Show(message);
          }
        }
      }
      else if (array.Length > 1)
      {
        string text2 = array[0];
        string text3 = array[1];
        if (string.IsNullOrEmpty(text2))
        {
          text2 = "TextLog";
        }
        if (text3.Trim() != "")
        {
          this.richTextBox1.Text = text3;
        }
        this.richTextBox1.Modified = true;
#if Interface
        ((DockContent)base.Parent).TabText = "[出力]" + Path.GetFileName(text2);
#else
        this.Text = "[出力]" + Path.GetFileName(text2);
#endif
        this.AddPreviousDocuments(text);
        this.UpdateStatusText(text);
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
      string[] array = data.Split(new char[]{'!'});
      if (array.Length > 1)
      {
        this.上書き保存SToolStripMenuItem.Enabled = (this.上書き保存SToolStripButton.Enabled = false);
      }
      else
      {
        this.上書き保存SToolStripMenuItem.Enabled = (this.上書き保存SToolStripButton.Enabled = true);
      }
#if Interface      
      string text = array[0];
      string text2 = Lib.File_GetEofCode(text);
      string text3 = Lib.File_GetCode(text);
      this.toolStripStatusLabel1.Text = string.Format(format, new object[]{y,x,text2,text3,text});
#endif    
    }
    #endregion

    #region Utilities
    public Control[] GetAllControls(Control top)
    {
      ArrayList buf = new ArrayList();
      foreach (Control c in top.Controls)
      {
        //MessageBox.Show(c.Name);
        buf.Add(c);
        buf.AddRange(this.GetAllControls(c));
      }
      return (Control[])buf.ToArray(typeof(Control));
    }

    //FIXME no Links
    private void viewButton_Click(object sender, EventArgs e)
    {
      this.menuStrip1.Visible = !this.menuStrip1.Visible;
    }


    public static List<ToolStripItem> Items = new List<ToolStripItem>();
    /// <summary>
    /// Finds the tool or menu strip item by name or text
    /// </summary>

    public void getMenuItem()
    {
      Items.Clear();

      for (Int32 i = 0; i < this.menuStrip1.Items.Count; i++)
      {
        ToolStripItem item = this.menuStrip1.Items[i];
        Items.Add(item);
        this.ProcessDropDown(item);
      }

      for (Int32 i = 0; i < this.toolStrip1.Items.Count; i++)
      {
        ToolStripItem item = this.toolStrip1.Items[i] as ToolStripItem;
        Items.Add(item);
        this.ProcessDropDown(item);
        if (this.toolStrip1.Items[i].GetType().Name == "ToolStripDropDownButton")
        {
          for (Int32 j = 0; j < ((ToolStripDropDownButton)this.toolStrip1.Items[i]).DropDownItems.Count; j++)
          {
            ToolStripItem item2 = ((ToolStripDropDownButton)this.toolStrip1.Items[i]).DropDownItems[j] as ToolStripItem;
            Items.Add(item2);
            this.ProcessDropDown(item2);
          }
        }
      }
    }

    public ToolStripItem FindMenuItem(String name)
    {
      this.getMenuItem();
      for (Int32 i = 0; i < Items.Count; i++)
      {
        ToolStripItem item = Items[i];
        if (item.Name == name) return item;
      }
      return null;
    }

    private void ProcessDropDown(ToolStripItem item)
    {
      //Type casting from ToolStripItem to ToolStripMenuItem
      ToolStripMenuItem menuItem = item as ToolStripMenuItem;
      if (menuItem == null) return;
      if (!menuItem.HasDropDownItems) return;
      else
      {
        foreach (ToolStripItem val in menuItem.DropDownItems)
        {
          ToolStripMenuItem menuTool = val as ToolStripMenuItem;
          if (menuTool == null) continue;
          if (menuTool.HasDropDownItems) ProcessDropDown(menuTool);
          Items.Add(menuTool);
        }
      }
    }

    public Int32 FindButtonIndex(String name)
    {
      for (Int32 i = 0; i < this.toolStrip1.Items.Count; i++)
      {
        //MessageBox.Show(toolstrip.Items[i].Name);
        if (this.toolStrip1.Items[i].Name == name) return i;
      }
      return -1;
    }

    public Int32 FindMenuItemIndex(String name)
    {
      for (Int32 i = 0; i < this.menuStrip1.Items.Count; i++)
      {
        //MessageBox.Show(toolstrip.Items[i].Name);
        if (this.menuStrip1.Items[i].Name == name) return i;
      }
      return -1;
    }
    #endregion

    #region CommonLibrary
    public static String File_OpenDialog(String FileName, String InitialDirectory, String Filter)
    {
      // http://dobon.net/vb/dotnet/form/openfiledialog.html
      //OpenFileDialogクラスのインスタンスを作成
      OpenFileDialog ofd = new OpenFileDialog();

      //はじめのファイル名を指定する
      //はじめに「ファイル名」で表示される文字列を指定する
      ofd.FileName = FileName;
      //はじめに表示されるフォルダを指定する
      //指定しない（空の文字列）の時は、現在のディレクトリが表示される
      ofd.InitialDirectory = InitialDirectory;
      //[ファイルの種類]に表示される選択肢を指定する
      //指定しないとすべてのファイルが表示される
      ofd.Filter = Filter;
      //	"XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
      //[ファイルの種類]ではじめに
      //「すべてのファイル」が選択されているようにする
      ofd.FilterIndex = 0;
      //タイトルを設定する
      ofd.Title = "開くファイルを選択してください";
      //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
      ofd.RestoreDirectory = true;
      //存在しないファイルの名前が指定されたとき警告を表示する
      //デフォルトでTrueなので指定する必要はない
      ofd.CheckFileExists = true;
      //存在しないパスが指定されたとき警告を表示する
      //デフォルトでTrueなので指定する必要はない
      ofd.CheckPathExists = true;

      //ダイアログを表示する
      if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        if (File.Exists(ofd.FileName))
        {
          return ofd.FileName;
        }
        else
        {
          return "";
        }
      }
      else return "";
    }

    public static String File_ReadToEndDecode(String filepath)
    {
      if (!System.IO.File.Exists(filepath)) return null;
      //テキストファイルを開く
      System.IO.FileStream fs = new System.IO.FileStream(
        filepath, System.IO.FileMode.Open,
        System.IO.FileAccess.Read);
      byte[] bs = new byte[fs.Length];
      //byte配列に読み込む
      fs.Read(bs, 0, bs.Length);
      fs.Close();

      //文字コードを取得する
      //System.Text.Encoding enc = StringHandler.GetCode(bs);
      System.Text.Encoding enc = GetCode(bs);
      //MessageBox.Show(String.Format("{0}",enc));
      //デコードして表示する
      return enc.GetString(bs);
    }

    public static void File_BackUpCopy(String path)
    {
      if (!File.Exists(path)) return;
      DateTime dt = DateTime.Now;
      String strtime = String.Format("_{0:00}{1:00}{2:00}{3:00}{4:00}{5:00}",
          dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
      String newfilepath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path))
        + strtime + Path.GetExtension(path);
      System.IO.File.Copy(path, newfilepath);
    }

    public static void File_SaveEncode(String path, String text, String enc)
    {
      if (!File.Exists(path)) return;
      //Lib.File_BackUpCopy(path);
      if (enc.ToLower() == "auto")
      {
        if (File.Exists(path)) enc = Lib.File_GetCode(path);
        else enc = "UTF-8";
      }
      // 文字エンコーディングの必要
      // http://dobon.net/vb/dotnet/file/writefile.html
      //書き込むファイルが既に存在している場合は、上書きする
      //String enc = Lib.File_GetCode(this.currentPath);
      System.IO.StreamWriter sw = new System.IO.StreamWriter(
          path, false, System.Text.Encoding.GetEncoding(enc));
      //TextBox1.Textの内容を書き込む
      sw.Write(text);
      //閉じる
      sw.Close();
    }

    public static String File_GetCode(String filepath)
    {

      if (!System.IO.File.Exists(filepath)) return null;

      //テキストファイルを開く
      System.IO.FileStream fs = new System.IO.FileStream(
        filepath, System.IO.FileMode.Open,
        System.IO.FileAccess.Read);
      byte[] bs = new byte[fs.Length];
      //byte配列に読み込む
      fs.Read(bs, 0, bs.Length);
      fs.Close();

      //文字コードを取得する
      System.Text.Encoding enc = GetCode(bs);
      //MessageBox.Show(String.Format("{0}",enc));
      //デコードして表示する
      try
      {
        return enc.WebName.ToUpper();
      }
      catch
      {
        return "";
      }
    }

    public static String File_SaveDialog(String FileName, String InitialDirectory, String Filter)
    {
      //[C#] http://dobon.net/vb/dotnet/form/savefiledialog.html
      //SaveFileDialogクラスのインスタンスを作成
      String savefilepath;
      SaveFileDialog sfd = new SaveFileDialog();

      //はじめのファイル名を指定する
      sfd.FileName = FileName;
      //はじめに表示されるフォルダを指定する
      //sfd.InitialDirectory = @"C:\";
      sfd.InitialDirectory = InitialDirectory;
      //[ファイルの種類]に表示される選択肢を指定する
      sfd.Filter = Filter;
      //[ファイルの種類]ではじめに
      //「すべてのファイル」が選択されているようにする
      sfd.FilterIndex = 2;
      //タイトルを設定する
      sfd.Title = "保存先のファイルを選択してください";
      //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
      sfd.RestoreDirectory = true;
      //既に存在するファイル名を指定したとき警告する
      //デフォルトでTrueなので指定する必要はない
      sfd.OverwritePrompt = true;
      //存在しないパスが指定されたとき警告を表示する
      //デフォルトでTrueなので指定する必要はない
      sfd.CheckPathExists = true;

      //ダイアログを表示する
      if (sfd.ShowDialog() == DialogResult.OK)
      {
        //OKボタンがクリックされたとき
        //選択されたファイル名を表示する
        //MessageBox.Show(sfd.FileName);
        //Shift JISで書き込む http://dobon.net/vb/dotnet/file/writefile.html
        //書き込むファイルが既に存在している場合は、上書きする
        //
        //	パラメーター http://msdn.microsoft.com/ja-jp/library/f5f5x7kt.aspx
        //	path  型 : System.String 書き込まれる完全なファイルパス。
        //	append 型 : System.Boolean データをファイルの末尾に追加するかどうかを判断します。
        //							ファイルが存在し、append が false の場合は、ファイルが上書きされます。
        //							ファイルが存在し、append が true の場合は、データがファイルの末尾に追加されます。
        //							それ以外の場合は、新しいファイルが作成されます。
        //	encoding 型 : System.Text.Encoding 使用する文字エンコーディング。
        savefilepath = sfd.FileName;
        /*
              System.IO.StreamWriter sw = new System.IO.StreamWriter(
                //@"c:\test.txt",
              sfd.FileName,
              false,
              System.Text.Encoding.GetEncoding("shift_jis"));
              //TextBox1.Textの内容を書き込む
              sw.Write(this.richTextBox1.Text);
              //閉じる
              sw.Close();
              //this.tabPage2.Text = Path.GetFileName(sfd.FileName);
              //this.richTextBox1.Modified = false;
              //this.tabPage2.Text = this.tabPage2.Text.Replace(" *", "");
              //this.toolStripStatusLabel1.Text = this.filepath = sfd.FileName;
              //this.dirtyFlag = false;
        */
        return savefilepath;
      }
      return "";
    }

    /// <summary>
    /// 文字コードを判別する
    /// </summary>
    /// <remarks>
    /// Jcode.pmのgetcodeメソッドを移植したものです。
    /// Jcode.pm(http://openlab.ring.gr.jp/Jcode/index-j.html)
    /// Jcode.pmのCopyright: Copyright 1999-2005 Dan Kogai
    /// </remarks>
    /// <param name="bytes">文字コードを調べるデータ</param>
    /// <returns>適当と思われるEncodingオブジェクト。
    /// 判断できなかった時はnull。</returns>
    //  http://dobon.net/vb/dotnet/string/detectcode.html
    public static System.Text.Encoding GetCode(byte[] bytes)
    {
      const byte bEscape = 0x1B;
      const byte bAt = 0x40;
      const byte bDollar = 0x24;
      const byte bAnd = 0x26;
      const byte bOpen = 0x28;	//'('
      const byte bB = 0x42;
      const byte bD = 0x44;
      const byte bJ = 0x4A;
      const byte bI = 0x49;

      int len = bytes.Length;
      byte b1, b2, b3, b4;

      //Encode::is_utf8 は無視

      bool isBinary = false;
      for (int i = 0; i < len; i++)
      {
        b1 = bytes[i];
        if (b1 <= 0x06 || b1 == 0x7F || b1 == 0xFF)
        {
          //'binary'
          isBinary = true;
          if (b1 == 0x00 && i < len - 1 && bytes[i + 1] <= 0x7F)
          {
            //smells like raw unicode
            return System.Text.Encoding.Unicode;
          }
        }
      }
      if (isBinary)
      {
        return null;
      }

      //not Japanese
      bool notJapanese = true;
      for (int i = 0; i < len; i++)
      {
        b1 = bytes[i];
        if (b1 == bEscape || 0x80 <= b1)
        {
          notJapanese = false;
          break;
        }
      }
      if (notJapanese)
      {
        return System.Text.Encoding.ASCII;
      }

      for (int i = 0; i < len - 2; i++)
      {
        b1 = bytes[i];
        b2 = bytes[i + 1];
        b3 = bytes[i + 2];

        if (b1 == bEscape)
        {
          if (b2 == bDollar && b3 == bAt)
          {
            //JIS_0208 1978
            //JIS
            return System.Text.Encoding.GetEncoding(50220);
          }
          else if (b2 == bDollar && b3 == bB)
          {
            //JIS_0208 1983
            //JIS
            return System.Text.Encoding.GetEncoding(50220);
          }
          else if (b2 == bOpen && (b3 == bB || b3 == bJ))
          {
            //JIS_ASC
            //JIS
            return System.Text.Encoding.GetEncoding(50220);
          }
          else if (b2 == bOpen && b3 == bI)
          {
            //JIS_KANA
            //JIS
            return System.Text.Encoding.GetEncoding(50220);
          }
          if (i < len - 3)
          {
            b4 = bytes[i + 3];
            if (b2 == bDollar && b3 == bOpen && b4 == bD)
            {
              //JIS_0212
              //JIS
              return System.Text.Encoding.GetEncoding(50220);
            }
            if (i < len - 5 &&
              b2 == bAnd && b3 == bAt && b4 == bEscape &&
              bytes[i + 4] == bDollar && bytes[i + 5] == bB)
            {
              //JIS_0208 1990
              //JIS
              return System.Text.Encoding.GetEncoding(50220);
            }
          }
        }
      }

      //should be euc|sjis|utf8
      //use of (?:) by Hiroki Ohzaki <ohzaki@iod.ricoh.co.jp>
      int sjis = 0;
      int euc = 0;
      int utf8 = 0;
      for (int i = 0; i < len - 1; i++)
      {
        b1 = bytes[i];
        b2 = bytes[i + 1];
        if (((0x81 <= b1 && b1 <= 0x9F) || (0xE0 <= b1 && b1 <= 0xFC)) &&
          ((0x40 <= b2 && b2 <= 0x7E) || (0x80 <= b2 && b2 <= 0xFC)))
        {
          //SJIS_C
          sjis += 2;
          i++;
        }
      }
      for (int i = 0; i < len - 1; i++)
      {
        b1 = bytes[i];
        b2 = bytes[i + 1];
        if (((0xA1 <= b1 && b1 <= 0xFE) && (0xA1 <= b2 && b2 <= 0xFE)) ||
          (b1 == 0x8E && (0xA1 <= b2 && b2 <= 0xDF)))
        {
          //EUC_C
          //EUC_KANA
          euc += 2;
          i++;
        }
        else if (i < len - 2)
        {
          b3 = bytes[i + 2];
          if (b1 == 0x8F && (0xA1 <= b2 && b2 <= 0xFE) &&
            (0xA1 <= b3 && b3 <= 0xFE))
          {
            //EUC_0212
            euc += 3;
            i += 2;
          }
        }
      }
      for (int i = 0; i < len - 1; i++)
      {
        b1 = bytes[i];
        b2 = bytes[i + 1];
        if ((0xC0 <= b1 && b1 <= 0xDF) && (0x80 <= b2 && b2 <= 0xBF))
        {
          //UTF8
          utf8 += 2;
          i++;
        }
        else if (i < len - 2)
        {
          b3 = bytes[i + 2];
          if ((0xE0 <= b1 && b1 <= 0xEF) && (0x80 <= b2 && b2 <= 0xBF) &&
            (0x80 <= b3 && b3 <= 0xBF))
          {
            //UTF8
            utf8 += 3;
            i += 2;
          }
        }
      }
      //M. Takahashi's suggestion
      //utf8 += utf8 / 2;
      System.Diagnostics.Debug.WriteLine(
        string.Format("sjis = {0}, euc = {1}, utf8 = {2}", sjis, euc, utf8));
      if (euc > sjis && euc > utf8)
      {
        //EUC
        return System.Text.Encoding.GetEncoding(51932);
      }
      else if (sjis > euc && sjis > utf8)
      {
        //SJIS
        return System.Text.Encoding.GetEncoding(932);
      }
      else if (utf8 > euc && utf8 > sjis)
      {
        //UTF8
        return System.Text.Encoding.UTF8;
      }
      return null;
    }

    public static String timestamp()
    {
      DateTime dt = DateTime.Now;
      return String.Format("Time-stamp: <{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00} kahata>",
        dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
    }

    // C Heading 挿入
    public static String CHeading(String path)
    {
      String heading = File_ReadToEnd("C:\\home\\hidemaru\\template\\c_heading_001.txt");
      if (System.IO.File.Exists(path))
      {
        try
        {
          String localurl = path.Replace("F:", "http://localhost").Replace("\\", "/");
          heading = heading.Replace("%%FILEEXT%%", Path.GetExtension(path)).Replace(".", "");
          heading = heading.Replace("%%FILENAME%%", Path.GetFileName(path));
          heading = heading.Replace("%%FILEPATH%%", path);
          heading = heading.Replace("%%LOCALURL%%", localurl);
          heading = heading.Replace("%%TIMESTAMP%%", timestamp());
        }
        catch (System.Exception exc)
        {
          String s = exc.Message.ToString();
          //MessageBox.Show(Lib.OutputError(s));
          MessageBox.Show(exc.Message.ToString());
          //this.ShowExceptionUI(s);
          return exc.Message.ToString();		//statusBarにエラーを表示
        }
      }
      return heading;
    }

    public static String File_ReadToEnd(String filepath)
    {
      if (!System.IO.File.Exists(filepath)) return "";
      //"C:\test.txt"をShift-JISコードとして開く
      System.IO.StreamReader sr = new System.IO.StreamReader(
        filepath,
        System.Text.Encoding.GetEncoding("shift_jis"));
      //内容をすべて読み込む
      String s = sr.ReadToEnd();
      //閉じる
      sr.Close();
      //結果を出力する
      //Console.WriteLine(s);
      return s;
    }

    #endregion

    #region Form Variables
    public MenuStrip menuStrip1;
    public ToolStripMenuItem ファイルFToolStripMenuItem;
    public ToolStripMenuItem 新規作成NToolStripMenuItem;
    public ToolStripMenuItem 開くOToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator;
    public ToolStripMenuItem 最近開いたファイルRToolStripMenuItem;
    public ToolStripMenuItem 最近開いたファイルをクリアCToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator12;
    public ToolStripMenuItem 上書き保存SToolStripMenuItem;
    public ToolStripMenuItem 名前を付けて保存AToolStripMenuItem;
    public ToolStripMenuItem 文字コードを指定して保存ToolStripMenuItem;
    public ToolStripMenuItem sHIFTJISで保存ToolStripMenuItem;
    public ToolStripMenuItem uTF8で保存ToolStripMenuItem;
    public ToolStripMenuItem 形式を指定して保存ToolStripMenuItem;
    public ToolStripMenuItem rtf形式で保存ToolStripMenuItem;
    public ToolStripMenuItem html形式で保存ToolStripMenuItem;
    public ToolStripMenuItem pdf形式で保存ToolStripMenuItem;
    public ToolStripMenuItem plainTextで保存ToolStripMenuItem;
    public ToolStripMenuItem xml形式で保存ToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator14;
    public ToolStripMenuItem 閉じるCToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator1;
    public ToolStripMenuItem 印刷PToolStripMenuItem;
    public ToolStripMenuItem 印刷プレビューVToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator2;
    public ToolStripMenuItem 終了XToolStripMenuItem;
    public ToolStripMenuItem 編集EToolStripMenuItem;
    public ToolStripMenuItem 元に戻すUToolStripMenuItem;
    public ToolStripMenuItem やり直しRToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator3;
    public ToolStripMenuItem 切り取りTToolStripMenuItem;
    public ToolStripMenuItem コピーCToolStripMenuItem;
    public ToolStripMenuItem 貼り付けPToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator4;
    public ToolStripMenuItem すべて選択AToolStripMenuItem;
    public ToolStripMenuItem 表示VToolStripMenuItem1;
    public ToolStripMenuItem ツールバーTToolStripMenuItem;
    public ToolStripMenuItem ステータスバーSToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator10;
    public ToolStripMenuItem tabPageModeToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator9;
    public ToolStripMenuItem richTextEditorToolStripMenuItem;
    public ToolStripMenuItem statusTextToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator11;
    public ToolStripMenuItem dockingTreeViewToolStripMenuItem;
    public ToolStripMenuItem 検索SToolStripMenuItem1;
    public ToolStripMenuItem 検索FToolStripMenuItem;
    public ToolStripMenuItem 置換RToolStripMenuItem;
    public ToolStripMenuItem 行へ移動GToolStripMenuItem;
    public ToolStripMenuItem 挿入IToolStripMenuItem;
    public ToolStripMenuItem タイムスタンプToolStripMenuItem;
    public ToolStripMenuItem c見出しToolStripMenuItem;
    public ToolStripMenuItem 書式OToolStripMenuItem;
    public ToolStripMenuItem 右端で折り返すToolStripMenuItem;
    public ToolStripMenuItem フォントと色ToolStripMenuItem;
    public ToolStripMenuItem スクリプトCToolStripMenuItem;
    public ToolStripMenuItem スクリプトを実行XToolStripMenuItem;
    public ToolStripMenuItem スクリプトを編集EToolStripMenuItem;
    public ToolStripMenuItem スクリプトメニュー更新RToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator28;
    public ToolStripMenuItem ツールTToolStripMenuItem;
    public ToolStripMenuItem カスタマイズCToolStripMenuItem;
    public ToolStripMenuItem delegate試験ToolStripMenuItem;
    public ToolStripMenuItem queryString試験ToolStripMenuItem;
    public ToolStripMenuItem オプションOToolStripMenuItem;
    public ToolStripMenuItem 固定タブで開くToolStripMenuItem;
    public ToolStripMenuItem ヘルプHToolStripMenuItem;
    public ToolStripMenuItem 内容CToolStripMenuItem;
    public ToolStripMenuItem インデックスIToolStripMenuItem;
    public ToolStripMenuItem 検索SToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator5;
    public ToolStripMenuItem バージョン情報AToolStripMenuItem;
    public ToolStrip toolStrip1;
    public ToolStripButton 新規作成NToolStripButton;
    public ToolStripButton 開くOToolStripButton;
    public ToolStripButton 上書き保存SToolStripButton;
    public ToolStripButton toolStripButton1;
    public ToolStripButton 印刷PToolStripButton;
    public ToolStripSeparator toolStripSeparator6;
    public ToolStripButton 切り取りUToolStripButton;
    public ToolStripButton コピーCToolStripButton;
    public ToolStripButton 貼り付けPToolStripButton;
    public ToolStripSeparator toolStripSeparator7;
    public ToolStripButton ヘルプLToolStripButton;
    public ToolStripDropDownButton toolStripDropDownButton1;
    public ToolStripMenuItem メニューバーMToolStripMenuItem;
    public ToolStripMenuItem ステータスバーSToolStripMenuItem1;
    public ToolStripDropDownButton toolStripDropDownButton4;
    public ToolStripDropDownButton toolStripDropDownButton2;
    public ToolStripDropDownButton toolStripDropDownButton3;
    public ToolStripMenuItem サクラエディタSToolStripMenuItem;
    public ToolStripMenuItem pSPadPToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator16;
    public ToolStripMenuItem scintillaCToolStripMenuItem;
    public ToolStripMenuItem azukiEditorZToolStripMenuItem;
    public ToolStripMenuItem richTextBoxToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator17;
    public ToolStripMenuItem エクスプローラEToolStripMenuItem;
    public ToolStripMenuItem コマンドプロンプトCToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator18;
    public ToolStripMenuItem 現在のファイルをブラウザで開くWToolStripMenuItem;
    public ToolStripMenuItem microsoftWordで開くWToolStripMenuItem;
    public ToolStripMenuItem ファイル名を指定して実行OToolStripMenuItem;
    public ToolStripMenuItem リンクを開くLToolStripMenuItem;
    public ToolStripSeparator toolStripSeparator8;
    public ToolStripButton toolStripButton2;
    public ToolStripButton toolStripButton3;
    public ToolStripButton toolStripButton4;
    public ToolStripButton toolStripButton5;
    public ToolStripButton toolStripButton6;
    public ToolStripButton toolStripButton7;
    public ToolStripButton toolStripButton8;
    public ToolStripButton toolStripButton9;
    public ToolStripButton toolStripButton10;
    public ToolStripButton toolStripButton11;
    public ToolStripButton toolStripButton12;
    public ToolStripButton toolStripButton13;
    public ToolStripButton toolStripButton14;
    public ToolStripButton toolStripButton15;
    public ToolStripButton toolStripButton16;
    public StatusStrip statusStrip1;
    public ToolStripStatusLabel toolStripStatusLabel1;
    public RichTextBox richTextBox1;
		private ToolStripButton imageListButton;
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichTextEditor));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.新規作成NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.最近開いたファイルRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.最近開いたファイルをクリアCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.上書き保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.名前を付けて保存AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.文字コードを指定して保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sHIFTJISで保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.uTF8で保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.形式を指定して保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.rtf形式で保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.html形式で保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pdf形式で保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.plainTextで保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.xml形式で保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
      this.閉じるCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
      this.表示VToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.ツールバーTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ステータスバーSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.tabPageModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.richTextEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.dockingTreeViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.検索SToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.検索FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.置換RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.行へ移動GToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.挿入IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.タイムスタンプToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.c見出しToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.書式OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.右端で折り返すToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.フォントと色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトを実行XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトを編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトメニュー更新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
      this.ツールTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.カスタマイズCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.delegate試験ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.queryString試験ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.オプションOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.固定タブで開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.印刷PToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.切り取りUToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.コピーCToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.貼り付けPToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.ヘルプLToolStripButton = new System.Windows.Forms.ToolStripButton();
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
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
      this.imageListButton = new System.Windows.Forms.ToolStripButton();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
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
            this.表示VToolStripMenuItem1,
            this.検索SToolStripMenuItem1,
            this.挿入IToolStripMenuItem,
            this.書式OToolStripMenuItem,
            this.スクリプトCToolStripMenuItem,
            this.ツールTToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
      this.menuStrip1.Size = new System.Drawing.Size(993, 27);
      this.menuStrip1.TabIndex = 3;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // ファイルFToolStripMenuItem
      // 
      this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成NToolStripMenuItem,
            this.開くOToolStripMenuItem,
            this.toolStripSeparator,
            this.最近開いたファイルRToolStripMenuItem,
            this.toolStripSeparator12,
            this.上書き保存SToolStripMenuItem,
            this.名前を付けて保存AToolStripMenuItem,
            this.文字コードを指定して保存ToolStripMenuItem,
            this.形式を指定して保存ToolStripMenuItem,
            this.toolStripSeparator14,
            this.閉じるCToolStripMenuItem,
            this.toolStripSeparator1,
            this.印刷PToolStripMenuItem,
            this.印刷プレビューVToolStripMenuItem,
            this.toolStripSeparator2,
            this.終了XToolStripMenuItem});
      this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
      this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
      this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
      // 
      // 新規作成NToolStripMenuItem
      // 
      this.新規作成NToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新規作成NToolStripMenuItem.Image")));
      this.新規作成NToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.新規作成NToolStripMenuItem.Name = "新規作成NToolStripMenuItem";
      this.新規作成NToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.新規作成NToolStripMenuItem.Text = "新規作成(&N)";
      this.新規作成NToolStripMenuItem.Click += new System.EventHandler(this.新規作成NToolStripMenuItem_Click);
      // 
      // 開くOToolStripMenuItem
      // 
      this.開くOToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripMenuItem.Image")));
      this.開くOToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
      this.開くOToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.開くOToolStripMenuItem.Text = "開く(&O)";
      this.開くOToolStripMenuItem.Click += new System.EventHandler(this.開くOToolStripMenuItem_Click);
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(238, 6);
      // 
      // 最近開いたファイルRToolStripMenuItem
      // 
      this.最近開いたファイルRToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最近開いたファイルをクリアCToolStripMenuItem});
      this.最近開いたファイルRToolStripMenuItem.Name = "最近開いたファイルRToolStripMenuItem";
      this.最近開いたファイルRToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.最近開いたファイルRToolStripMenuItem.Text = "最近開いたファイル(&R)";
      // 
      // 最近開いたファイルをクリアCToolStripMenuItem
      // 
      this.最近開いたファイルをクリアCToolStripMenuItem.Name = "最近開いたファイルをクリアCToolStripMenuItem";
      this.最近開いたファイルをクリアCToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
      this.最近開いたファイルをクリアCToolStripMenuItem.Text = "最近開いたファイルをクリア(&C)";
      this.最近開いたファイルをクリアCToolStripMenuItem.Click += new System.EventHandler(this.最近開いたファイルをクリアCToolStripMenuItem_Click);
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(238, 6);
      // 
      // 上書き保存SToolStripMenuItem
      // 
      this.上書き保存SToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("上書き保存SToolStripMenuItem.Image")));
      this.上書き保存SToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.上書き保存SToolStripMenuItem.Name = "上書き保存SToolStripMenuItem";
      this.上書き保存SToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.上書き保存SToolStripMenuItem.Text = "上書き保存(&S)";
      this.上書き保存SToolStripMenuItem.Click += new System.EventHandler(this.上書き保存SToolStripMenuItem_Click);
      // 
      // 名前を付けて保存AToolStripMenuItem
      // 
      this.名前を付けて保存AToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("名前を付けて保存AToolStripMenuItem.Image")));
      this.名前を付けて保存AToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.名前を付けて保存AToolStripMenuItem.Name = "名前を付けて保存AToolStripMenuItem";
      this.名前を付けて保存AToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.名前を付けて保存AToolStripMenuItem.Text = "名前を付けて保存(&A)";
      this.名前を付けて保存AToolStripMenuItem.Click += new System.EventHandler(this.名前を付けて保存AToolStripMenuItem_Click);
      // 
      // 文字コードを指定して保存ToolStripMenuItem
      // 
      this.文字コードを指定して保存ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sHIFTJISで保存ToolStripMenuItem,
            this.uTF8で保存ToolStripMenuItem});
      this.文字コードを指定して保存ToolStripMenuItem.Name = "文字コードを指定して保存ToolStripMenuItem";
      this.文字コードを指定して保存ToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.文字コードを指定して保存ToolStripMenuItem.Text = "文字コードを指定して保存";
      // 
      // sHIFTJISで保存ToolStripMenuItem
      // 
      this.sHIFTJISで保存ToolStripMenuItem.Name = "sHIFTJISで保存ToolStripMenuItem";
      this.sHIFTJISで保存ToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
      this.sHIFTJISで保存ToolStripMenuItem.Text = "SHIFT JIS　で保存";
      this.sHIFTJISで保存ToolStripMenuItem.Click += new System.EventHandler(this.名前を付けて保存AToolStripMenuItem_Click);
      // 
      // uTF8で保存ToolStripMenuItem
      // 
      this.uTF8で保存ToolStripMenuItem.Name = "uTF8で保存ToolStripMenuItem";
      this.uTF8で保存ToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
      this.uTF8で保存ToolStripMenuItem.Text = "UTF-8 で保存";
      this.uTF8で保存ToolStripMenuItem.Click += new System.EventHandler(this.名前を付けて保存AToolStripMenuItem_Click);
      // 
      // 形式を指定して保存ToolStripMenuItem
      // 
      this.形式を指定して保存ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtf形式で保存ToolStripMenuItem,
            this.html形式で保存ToolStripMenuItem,
            this.pdf形式で保存ToolStripMenuItem,
            this.plainTextで保存ToolStripMenuItem,
            this.xml形式で保存ToolStripMenuItem});
      this.形式を指定して保存ToolStripMenuItem.Name = "形式を指定して保存ToolStripMenuItem";
      this.形式を指定して保存ToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.形式を指定して保存ToolStripMenuItem.Text = "形式を指定して保存";
      // 
      // rtf形式で保存ToolStripMenuItem
      // 
      this.rtf形式で保存ToolStripMenuItem.Name = "rtf形式で保存ToolStripMenuItem";
      this.rtf形式で保存ToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
      this.rtf形式で保存ToolStripMenuItem.Text = "rtf 形式で保存";
      // 
      // html形式で保存ToolStripMenuItem
      // 
      this.html形式で保存ToolStripMenuItem.Name = "html形式で保存ToolStripMenuItem";
      this.html形式で保存ToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
      this.html形式で保存ToolStripMenuItem.Text = "html 形式で保存";
      // 
      // pdf形式で保存ToolStripMenuItem
      // 
      this.pdf形式で保存ToolStripMenuItem.Name = "pdf形式で保存ToolStripMenuItem";
      this.pdf形式で保存ToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
      this.pdf形式で保存ToolStripMenuItem.Text = "pdf 形式で保存";
      // 
      // plainTextで保存ToolStripMenuItem
      // 
      this.plainTextで保存ToolStripMenuItem.Name = "plainTextで保存ToolStripMenuItem";
      this.plainTextで保存ToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
      this.plainTextで保存ToolStripMenuItem.Text = "plain text で保存";
      // 
      // xml形式で保存ToolStripMenuItem
      // 
      this.xml形式で保存ToolStripMenuItem.Name = "xml形式で保存ToolStripMenuItem";
      this.xml形式で保存ToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
      this.xml形式で保存ToolStripMenuItem.Text = "xml 形式で保存";
      // 
      // toolStripSeparator14
      // 
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      this.toolStripSeparator14.Size = new System.Drawing.Size(238, 6);
      // 
      // 閉じるCToolStripMenuItem
      // 
      this.閉じるCToolStripMenuItem.Name = "閉じるCToolStripMenuItem";
      this.閉じるCToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.閉じるCToolStripMenuItem.Text = "閉じる(&C)";
      this.閉じるCToolStripMenuItem.Click += new System.EventHandler(this.閉じるCToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(238, 6);
      // 
      // 印刷PToolStripMenuItem
      // 
      this.印刷PToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripMenuItem.Image")));
      this.印刷PToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷PToolStripMenuItem.Name = "印刷PToolStripMenuItem";
      this.印刷PToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.印刷PToolStripMenuItem.Text = "印刷(&P)";
      this.印刷PToolStripMenuItem.Click += new System.EventHandler(this.印刷PToolStripMenuItem_Click);
      // 
      // 印刷プレビューVToolStripMenuItem
      // 
      this.印刷プレビューVToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("印刷プレビューVToolStripMenuItem.Image")));
      this.印刷プレビューVToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷プレビューVToolStripMenuItem.Name = "印刷プレビューVToolStripMenuItem";
      this.印刷プレビューVToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.印刷プレビューVToolStripMenuItem.Text = "印刷プレビュー(&V)";
      this.印刷プレビューVToolStripMenuItem.Click += new System.EventHandler(this.印刷プレビューVToolStripMenuItem_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(238, 6);
      // 
      // 終了XToolStripMenuItem
      // 
      this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
      this.終了XToolStripMenuItem.Size = new System.Drawing.Size(241, 26);
      this.終了XToolStripMenuItem.Text = "アプリケーションの終了(&X)";
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
      this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
      this.編集EToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
      this.編集EToolStripMenuItem.Text = "編集(&E)";
      // 
      // 元に戻すUToolStripMenuItem
      // 
      this.元に戻すUToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(199)))), ((int)(((byte)(198)))));
      this.元に戻すUToolStripMenuItem.Name = "元に戻すUToolStripMenuItem";
      this.元に戻すUToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.元に戻すUToolStripMenuItem.Text = "元に戻す(&U)";
      this.元に戻すUToolStripMenuItem.Click += new System.EventHandler(this.元に戻すUToolStripMenuItem_Click);
      // 
      // やり直しRToolStripMenuItem
      // 
      this.やり直しRToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(199)))), ((int)(((byte)(198)))));
      this.やり直しRToolStripMenuItem.Name = "やり直しRToolStripMenuItem";
      this.やり直しRToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.やり直しRToolStripMenuItem.Text = "やり直し(&R)";
      this.やり直しRToolStripMenuItem.Click += new System.EventHandler(this.やり直しRToolStripMenuItem_Click);
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
      this.切り取りTToolStripMenuItem.Click += new System.EventHandler(this.切り取りTToolStripMenuItem_Click);
      // 
      // コピーCToolStripMenuItem
      // 
      this.コピーCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripMenuItem.Image")));
      this.コピーCToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.コピーCToolStripMenuItem.Name = "コピーCToolStripMenuItem";
      this.コピーCToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.コピーCToolStripMenuItem.Text = "コピー(&C)";
      this.コピーCToolStripMenuItem.Click += new System.EventHandler(this.コピーCToolStripMenuItem_Click);
      // 
      // 貼り付けPToolStripMenuItem
      // 
      this.貼り付けPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripMenuItem.Image")));
      this.貼り付けPToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.貼り付けPToolStripMenuItem.Name = "貼り付けPToolStripMenuItem";
      this.貼り付けPToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.貼り付けPToolStripMenuItem.Text = "貼り付け(&P)";
      this.貼り付けPToolStripMenuItem.Click += new System.EventHandler(this.貼り付けPToolStripMenuItem_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(172, 6);
      // 
      // すべて選択AToolStripMenuItem
      // 
      this.すべて選択AToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(199)))), ((int)(((byte)(198)))));
      this.すべて選択AToolStripMenuItem.Name = "すべて選択AToolStripMenuItem";
      this.すべて選択AToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
      this.すべて選択AToolStripMenuItem.Text = "すべて選択(&A)";
      this.すべて選択AToolStripMenuItem.Click += new System.EventHandler(this.すべて選択AToolStripMenuItem_Click);
      // 
      // 表示VToolStripMenuItem1
      // 
      this.表示VToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ツールバーTToolStripMenuItem,
            this.ステータスバーSToolStripMenuItem,
            this.toolStripSeparator10,
            this.tabPageModeToolStripMenuItem,
            this.toolStripSeparator9,
            this.richTextEditorToolStripMenuItem,
            this.statusTextToolStripMenuItem,
            this.toolStripSeparator11,
            this.dockingTreeViewToolStripMenuItem});
      this.表示VToolStripMenuItem1.Name = "表示VToolStripMenuItem1";
      this.表示VToolStripMenuItem1.Size = new System.Drawing.Size(75, 24);
      this.表示VToolStripMenuItem1.Text = "表示(&V)";
      // 
      // ツールバーTToolStripMenuItem
      // 
      this.ツールバーTToolStripMenuItem.Checked = true;
      this.ツールバーTToolStripMenuItem.CheckOnClick = true;
      this.ツールバーTToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ツールバーTToolStripMenuItem.Name = "ツールバーTToolStripMenuItem";
      this.ツールバーTToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
      this.ツールバーTToolStripMenuItem.Text = "ツールバー(&T)";
      this.ツールバーTToolStripMenuItem.Click += new System.EventHandler(this.ツールバーTToolStripMenuItem_Click);
      // 
      // ステータスバーSToolStripMenuItem
      // 
      this.ステータスバーSToolStripMenuItem.Checked = true;
      this.ステータスバーSToolStripMenuItem.CheckOnClick = true;
      this.ステータスバーSToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ステータスバーSToolStripMenuItem.Name = "ステータスバーSToolStripMenuItem";
      this.ステータスバーSToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
      this.ステータスバーSToolStripMenuItem.Text = "ステータスバー(&S)";
      this.ステータスバーSToolStripMenuItem.Click += new System.EventHandler(this.ステータスバーSToolStripMenuItem_Click);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(217, 6);
      // 
      // tabPageModeToolStripMenuItem
      // 
      this.tabPageModeToolStripMenuItem.CheckOnClick = true;
      this.tabPageModeToolStripMenuItem.Enabled = false;
      this.tabPageModeToolStripMenuItem.Name = "tabPageModeToolStripMenuItem";
      this.tabPageModeToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
      this.tabPageModeToolStripMenuItem.Text = "TabPage Mode";
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(217, 6);
      // 
      // richTextEditorToolStripMenuItem
      // 
      this.richTextEditorToolStripMenuItem.Enabled = false;
      this.richTextEditorToolStripMenuItem.Name = "richTextEditorToolStripMenuItem";
      this.richTextEditorToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
      this.richTextEditorToolStripMenuItem.Text = "RichTextEditor";
      // 
      // statusTextToolStripMenuItem
      // 
      this.statusTextToolStripMenuItem.Enabled = false;
      this.statusTextToolStripMenuItem.Name = "statusTextToolStripMenuItem";
      this.statusTextToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
      this.statusTextToolStripMenuItem.Text = "StatusText";
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(217, 6);
      // 
      // dockingTreeViewToolStripMenuItem
      // 
      this.dockingTreeViewToolStripMenuItem.CheckOnClick = true;
      this.dockingTreeViewToolStripMenuItem.Enabled = false;
      this.dockingTreeViewToolStripMenuItem.Name = "dockingTreeViewToolStripMenuItem";
      this.dockingTreeViewToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
      this.dockingTreeViewToolStripMenuItem.Text = "Docking TreeView ";
      // 
      // 検索SToolStripMenuItem1
      // 
      this.検索SToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.検索FToolStripMenuItem,
            this.置換RToolStripMenuItem,
            this.行へ移動GToolStripMenuItem});
      this.検索SToolStripMenuItem1.Name = "検索SToolStripMenuItem1";
      this.検索SToolStripMenuItem1.Size = new System.Drawing.Size(75, 24);
      this.検索SToolStripMenuItem1.Text = "検索(&S)";
      // 
      // 検索FToolStripMenuItem
      // 
      this.検索FToolStripMenuItem.Name = "検索FToolStripMenuItem";
      this.検索FToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
      this.検索FToolStripMenuItem.Text = "検索(&F)";
      this.検索FToolStripMenuItem.Click += new System.EventHandler(this.検索FToolStripMenuItem_Click);
      // 
      // 置換RToolStripMenuItem
      // 
      this.置換RToolStripMenuItem.Name = "置換RToolStripMenuItem";
      this.置換RToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
      this.置換RToolStripMenuItem.Text = "置換(&R)";
      this.置換RToolStripMenuItem.Click += new System.EventHandler(this.置換RToolStripMenuItem_Click);
      // 
      // 行へ移動GToolStripMenuItem
      // 
      this.行へ移動GToolStripMenuItem.Name = "行へ移動GToolStripMenuItem";
      this.行へ移動GToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
      this.行へ移動GToolStripMenuItem.Text = "行へ移動(&G)...";
      this.行へ移動GToolStripMenuItem.Click += new System.EventHandler(this.行へ移動GToolStripMenuItem_Click);
      // 
      // 挿入IToolStripMenuItem
      // 
      this.挿入IToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.タイムスタンプToolStripMenuItem,
            this.c見出しToolStripMenuItem});
      this.挿入IToolStripMenuItem.Name = "挿入IToolStripMenuItem";
      this.挿入IToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
      this.挿入IToolStripMenuItem.Text = "挿入(&I)";
      // 
      // タイムスタンプToolStripMenuItem
      // 
      this.タイムスタンプToolStripMenuItem.Name = "タイムスタンプToolStripMenuItem";
      this.タイムスタンプToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
      this.タイムスタンプToolStripMenuItem.Text = "タイムスタンプ";
      this.タイムスタンプToolStripMenuItem.Click += new System.EventHandler(this.タイムスタンプToolStripMenuItem_Click);
      // 
      // c見出しToolStripMenuItem
      // 
      this.c見出しToolStripMenuItem.Name = "c見出しToolStripMenuItem";
      this.c見出しToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
      this.c見出しToolStripMenuItem.Text = "C見出し";
      this.c見出しToolStripMenuItem.Click += new System.EventHandler(this.c見出しToolStripMenuItem_Click);
      // 
      // 書式OToolStripMenuItem
      // 
      this.書式OToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.右端で折り返すToolStripMenuItem,
            this.フォントと色ToolStripMenuItem});
      this.書式OToolStripMenuItem.Name = "書式OToolStripMenuItem";
      this.書式OToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
      this.書式OToolStripMenuItem.Text = "書式(&O)";
      // 
      // 右端で折り返すToolStripMenuItem
      // 
      this.右端で折り返すToolStripMenuItem.Name = "右端で折り返すToolStripMenuItem";
      this.右端で折り返すToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
      this.右端で折り返すToolStripMenuItem.Text = "右端で折り返す";
      this.右端で折り返すToolStripMenuItem.Click += new System.EventHandler(this.右端で折り返すToolStripMenuItem_Click);
      // 
      // フォントと色ToolStripMenuItem
      // 
      this.フォントと色ToolStripMenuItem.Name = "フォントと色ToolStripMenuItem";
      this.フォントと色ToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
      this.フォントと色ToolStripMenuItem.Text = "フォントと色";
      this.フォントと色ToolStripMenuItem.Click += new System.EventHandler(this.フォントと色ToolStripMenuItem_Click);
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
      this.スクリプトCToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
      this.スクリプトCToolStripMenuItem.Text = "スクリプト(&C)";
      // 
      // スクリプトを実行XToolStripMenuItem
      // 
      this.スクリプトを実行XToolStripMenuItem.Name = "スクリプトを実行XToolStripMenuItem";
      this.スクリプトを実行XToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
      this.スクリプトを実行XToolStripMenuItem.Text = "スクリプトを実行(&X)";
      this.スクリプトを実行XToolStripMenuItem.Click += new System.EventHandler(this.スクリプトを実行XToolStripMenuItem_Click);
      // 
      // スクリプトを編集EToolStripMenuItem
      // 
      this.スクリプトを編集EToolStripMenuItem.Name = "スクリプトを編集EToolStripMenuItem";
      this.スクリプトを編集EToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
      this.スクリプトを編集EToolStripMenuItem.Text = "スクリプトを編集(&E)";
      this.スクリプトを編集EToolStripMenuItem.Click += new System.EventHandler(this.スクリプトを編集EToolStripMenuItem_Click);
      // 
      // スクリプトメニュー更新RToolStripMenuItem
      // 
      this.スクリプトメニュー更新RToolStripMenuItem.Name = "スクリプトメニュー更新RToolStripMenuItem";
      this.スクリプトメニュー更新RToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
      this.スクリプトメニュー更新RToolStripMenuItem.Text = "スクリプトメニュー更新(&R)";
      this.スクリプトメニュー更新RToolStripMenuItem.Click += new System.EventHandler(this.スクリプトメニュー更新RToolStripMenuItem_Click);
      // 
      // toolStripSeparator28
      // 
      this.toolStripSeparator28.Name = "toolStripSeparator28";
      this.toolStripSeparator28.Size = new System.Drawing.Size(230, 6);
      // 
      // ツールTToolStripMenuItem
      // 
      this.ツールTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.カスタマイズCToolStripMenuItem,
            this.オプションOToolStripMenuItem});
      this.ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem";
      this.ツールTToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
      this.ツールTToolStripMenuItem.Text = "ツール(&T)";
      // 
      // カスタマイズCToolStripMenuItem
      // 
      this.カスタマイズCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delegate試験ToolStripMenuItem,
            this.queryString試験ToolStripMenuItem,
            this.toolStripMenuItem2});
      this.カスタマイズCToolStripMenuItem.Name = "カスタマイズCToolStripMenuItem";
      this.カスタマイズCToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
      this.カスタマイズCToolStripMenuItem.Text = "カスタマイズ(&C)";
      // 
      // delegate試験ToolStripMenuItem
      // 
      this.delegate試験ToolStripMenuItem.Name = "delegate試験ToolStripMenuItem";
      this.delegate試験ToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
      this.delegate試験ToolStripMenuItem.Text = "delegate試験";
      this.delegate試験ToolStripMenuItem.Click += new System.EventHandler(this.delegate試験ToolStripMenuItem_Click);
      // 
      // queryString試験ToolStripMenuItem
      // 
      this.queryString試験ToolStripMenuItem.Name = "queryString試験ToolStripMenuItem";
      this.queryString試験ToolStripMenuItem.Size = new System.Drawing.Size(199, 26);
      this.queryString試験ToolStripMenuItem.Text = "queryString試験";
      this.queryString試験ToolStripMenuItem.Click += new System.EventHandler(this.queryString試験ToolStripMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(199, 26);
      this.toolStripMenuItem2.Text = "queryString試験";
      this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
      // 
      // オプションOToolStripMenuItem
      // 
      this.オプションOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.固定タブで開くToolStripMenuItem});
      this.オプションOToolStripMenuItem.Name = "オプションOToolStripMenuItem";
      this.オプションOToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
      this.オプションOToolStripMenuItem.Text = "オプション(&O)";
      // 
      // 固定タブで開くToolStripMenuItem
      // 
      this.固定タブで開くToolStripMenuItem.CheckOnClick = true;
      this.固定タブで開くToolStripMenuItem.Enabled = false;
      this.固定タブで開くToolStripMenuItem.Name = "固定タブで開くToolStripMenuItem";
      this.固定タブで開くToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
      this.固定タブで開くToolStripMenuItem.Text = "固定タブで開く";
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
      this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
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
            this.toolStripButton1,
            this.印刷PToolStripButton,
            this.toolStripSeparator6,
            this.切り取りUToolStripButton,
            this.コピーCToolStripButton,
            this.貼り付けPToolStripButton,
            this.toolStripSeparator7,
            this.ヘルプLToolStripButton,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton4,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton3,
            this.toolStripSeparator8,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripButton9,
            this.toolStripButton10,
            this.toolStripButton11,
            this.toolStripButton12,
            this.toolStripButton13,
            this.toolStripButton14,
            this.toolStripButton15,
            this.toolStripButton16,
            this.imageListButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 27);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(993, 27);
      this.toolStrip1.TabIndex = 5;
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
      this.新規作成NToolStripButton.Click += new System.EventHandler(this.新規作成NToolStripButton_Click);
      // 
      // 開くOToolStripButton
      // 
      this.開くOToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.開くOToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripButton.Image")));
      this.開くOToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.開くOToolStripButton.Name = "開くOToolStripButton";
      this.開くOToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.開くOToolStripButton.Text = "開く(&O)";
      this.開くOToolStripButton.Click += new System.EventHandler(this.開くOToolStripButton_Click);
      // 
      // 上書き保存SToolStripButton
      // 
      this.上書き保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.上書き保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("上書き保存SToolStripButton.Image")));
      this.上書き保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.上書き保存SToolStripButton.Name = "上書き保存SToolStripButton";
      this.上書き保存SToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.上書き保存SToolStripButton.Text = "上書き保存(&S)";
      this.上書き保存SToolStripButton.Click += new System.EventHandler(this.上書き保存SToolStripButton_Click);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Black;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton1.Text = "toolStripButton1";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // 印刷PToolStripButton
      // 
      this.印刷PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.印刷PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripButton.Image")));
      this.印刷PToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷PToolStripButton.Name = "印刷PToolStripButton";
      this.印刷PToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.印刷PToolStripButton.Text = "印刷(&P)";
      this.印刷PToolStripButton.Click += new System.EventHandler(this.印刷PToolStripButton_Click);
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
      this.切り取りUToolStripButton.Click += new System.EventHandler(this.切り取りUToolStripButton_Click);
      // 
      // コピーCToolStripButton
      // 
      this.コピーCToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.コピーCToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripButton.Image")));
      this.コピーCToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.コピーCToolStripButton.Name = "コピーCToolStripButton";
      this.コピーCToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.コピーCToolStripButton.Text = "コピー(&C)";
      this.コピーCToolStripButton.Click += new System.EventHandler(this.コピーCToolStripButton_Click);
      // 
      // 貼り付けPToolStripButton
      // 
      this.貼り付けPToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.貼り付けPToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripButton.Image")));
      this.貼り付けPToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.貼り付けPToolStripButton.Name = "貼り付けPToolStripButton";
      this.貼り付けPToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.貼り付けPToolStripButton.Text = "貼り付け(&P)";
      this.貼り付けPToolStripButton.Click += new System.EventHandler(this.貼り付けPToolStripButton_Click);
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
      // toolStripDropDownButton4
      // 
      this.toolStripDropDownButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
      this.toolStripDropDownButton4.Size = new System.Drawing.Size(14, 24);
      this.toolStripDropDownButton4.Text = "toolStripDropDownButton4";
      // 
      // toolStripDropDownButton2
      // 
      this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
      this.toolStripDropDownButton2.Size = new System.Drawing.Size(14, 24);
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
      this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
      this.toolStripDropDownButton3.Size = new System.Drawing.Size(14, 24);
      this.toolStripDropDownButton3.Text = "&R";
      this.toolStripDropDownButton3.ToolTipText = "現在開いてるファイルを外部プログラムで開きます";
      // 
      // サクラエディタSToolStripMenuItem
      // 
      this.サクラエディタSToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("サクラエディタSToolStripMenuItem.Image")));
      this.サクラエディタSToolStripMenuItem.Name = "サクラエディタSToolStripMenuItem";
      this.サクラエディタSToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.サクラエディタSToolStripMenuItem.Text = "サクラエディタ(&S)";
      this.サクラエディタSToolStripMenuItem.Click += new System.EventHandler(this.サクラエディタSToolStripMenuItem_Click);
      // 
      // pSPadPToolStripMenuItem
      // 
      this.pSPadPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pSPadPToolStripMenuItem.Image")));
      this.pSPadPToolStripMenuItem.Name = "pSPadPToolStripMenuItem";
      this.pSPadPToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.pSPadPToolStripMenuItem.Text = "PSPad(&P)";
      this.pSPadPToolStripMenuItem.Click += new System.EventHandler(this.pSPadPToolStripMenuItem_Click);
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
      this.scintillaCToolStripMenuItem.Click += new System.EventHandler(this.scintillaCToolStripMenuItem_Click);
      // 
      // azukiEditorZToolStripMenuItem
      // 
      this.azukiEditorZToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("azukiEditorZToolStripMenuItem.Image")));
      this.azukiEditorZToolStripMenuItem.Name = "azukiEditorZToolStripMenuItem";
      this.azukiEditorZToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.azukiEditorZToolStripMenuItem.Text = "Azuki Editor(&Z)";
      this.azukiEditorZToolStripMenuItem.Click += new System.EventHandler(this.azukiEditorZToolStripMenuItem_Click);
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
      this.現在のファイルをブラウザで開くWToolStripMenuItem.Click += new System.EventHandler(this.現在のファイルをブラウザで開くWToolStripMenuItem_Click);
      // 
      // microsoftWordで開くWToolStripMenuItem
      // 
      this.microsoftWordで開くWToolStripMenuItem.Name = "microsoftWordで開くWToolStripMenuItem";
      this.microsoftWordで開くWToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.microsoftWordで開くWToolStripMenuItem.Text = "Microsoft Word で開く(&W)";
      this.microsoftWordで開くWToolStripMenuItem.Click += new System.EventHandler(this.microsoftWordで開くWToolStripMenuItem_Click);
      // 
      // ファイル名を指定して実行OToolStripMenuItem
      // 
      this.ファイル名を指定して実行OToolStripMenuItem.Name = "ファイル名を指定して実行OToolStripMenuItem";
      this.ファイル名を指定して実行OToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.ファイル名を指定して実行OToolStripMenuItem.Text = "ファイル名を指定して実行(&O)";
      this.ファイル名を指定して実行OToolStripMenuItem.Click += new System.EventHandler(this.ファイル名を指定して実行OToolStripMenuItem_Click);
      // 
      // リンクを開くLToolStripMenuItem
      // 
      this.リンクを開くLToolStripMenuItem.Enabled = false;
      this.リンクを開くLToolStripMenuItem.Name = "リンクを開くLToolStripMenuItem";
      this.リンクを開くLToolStripMenuItem.Size = new System.Drawing.Size(287, 26);
      this.リンクを開くLToolStripMenuItem.Text = "リンクを開く(&L)";
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(6, 27);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Black;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton2.Text = "toolStripButton2";
      this.toolStripButton2.ToolTipText = "文字のフォントと色を変更します";
      this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
      // 
      // toolStripButton3
      // 
      this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
      this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Black;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton3.Text = "toolStripButton3";
      this.toolStripButton3.ToolTipText = "選択文字を太字にします";
      this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
      // 
      // toolStripButton4
      // 
      this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton4.Text = "toolStripButton4";
      this.toolStripButton4.ToolTipText = "選択文字をイタリック体にします";
      this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
      // 
      // toolStripButton5
      // 
      this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
      this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton5.Name = "toolStripButton5";
      this.toolStripButton5.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton5.Text = "toolStripButton5";
      this.toolStripButton5.ToolTipText = "選択文字にアンダーラインを引きます";
      this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
      // 
      // toolStripButton6
      // 
      this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
      this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Black;
      this.toolStripButton6.Name = "toolStripButton6";
      this.toolStripButton6.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton6.Text = "toolStripButton6";
      this.toolStripButton6.ToolTipText = "選択文字の文字色を変更します";
      this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
      // 
      // toolStripButton7
      // 
      this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
      this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton7.Name = "toolStripButton7";
      this.toolStripButton7.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton7.Text = "toolStripButton7";
      this.toolStripButton7.ToolTipText = "選択文字の背景色を変更します";
      this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
      // 
      // toolStripButton8
      // 
      this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
      this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton8.Name = "toolStripButton8";
      this.toolStripButton8.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton8.Text = "toolStripButton8";
      this.toolStripButton8.ToolTipText = "選択文字を左寄せにします";
      this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
      // 
      // toolStripButton9
      // 
      this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
      this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton9.Name = "toolStripButton9";
      this.toolStripButton9.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton9.Text = "toolStripButton9";
      this.toolStripButton9.ToolTipText = "選択文字を中央揃えにします";
      this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
      // 
      // toolStripButton10
      // 
      this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
      this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton10.Name = "toolStripButton10";
      this.toolStripButton10.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton10.Text = "toolStripButton10";
      this.toolStripButton10.ToolTipText = "選択文字を右寄せにします";
      this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
      // 
      // toolStripButton11
      // 
      this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
      this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton11.Name = "toolStripButton11";
      this.toolStripButton11.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton11.Text = "toolStripButton11";
      this.toolStripButton11.ToolTipText = "選択範囲を均等割り付けにします";
      this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // toolStripButton12
      // 
      this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton12.Image")));
      this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton12.Name = "toolStripButton12";
      this.toolStripButton12.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton12.Text = "toolStripButton12";
      this.toolStripButton12.ToolTipText = "選択範囲を番号付リストにします";
      this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
      // 
      // toolStripButton13
      // 
      this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton13.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton13.Image")));
      this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton13.Name = "toolStripButton13";
      this.toolStripButton13.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton13.Text = "toolStripButton13";
      this.toolStripButton13.ToolTipText = "選択範囲を番号なしリストにします";
      this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
      // 
      // toolStripButton14
      // 
      this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton14.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton14.Image")));
      this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton14.Name = "toolStripButton14";
      this.toolStripButton14.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton14.Text = "toolStripButton14";
      this.toolStripButton14.ToolTipText = "選択範囲をアウトデントします";
      this.toolStripButton14.Click += new System.EventHandler(this.toolStripButton14_Click);
      // 
      // toolStripButton15
      // 
      this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton15.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton15.Image")));
      this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton15.Name = "toolStripButton15";
      this.toolStripButton15.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton15.Text = "toolStripButton15";
      this.toolStripButton15.ToolTipText = "選択範囲をインデントします";
      this.toolStripButton15.Click += new System.EventHandler(this.toolStripButton15_Click);
      // 
      // toolStripButton16
      // 
      this.toolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton16.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton16.Image")));
      this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton16.Name = "toolStripButton16";
      this.toolStripButton16.Size = new System.Drawing.Size(24, 24);
      this.toolStripButton16.Text = "toolStripButton16";
      this.toolStripButton16.ToolTipText = "選択位置に図形を挿入します";
      this.toolStripButton16.Click += new System.EventHandler(this.toolStripButton16_Click);
      // 
      // imageListButton
      // 
      this.imageListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.imageListButton.Image = ((System.Drawing.Image)(resources.GetObject("imageListButton.Image")));
      this.imageListButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.imageListButton.Name = "imageListButton";
      this.imageListButton.Size = new System.Drawing.Size(24, 24);
      this.imageListButton.Text = "toolStripButton17";
      this.imageListButton.Visible = false;
      // 
      // statusStrip1
      // 
      this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
      this.statusStrip1.Location = new System.Drawing.Point(0, 434);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
      this.statusStrip1.Size = new System.Drawing.Size(993, 24);
      this.statusStrip1.TabIndex = 6;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(167, 19);
      this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
      // 
      // richTextBox1
      // 
      this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.richTextBox1.Location = new System.Drawing.Point(0, 54);
      this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(993, 380);
      this.richTextBox1.TabIndex = 7;
      this.richTextBox1.Tag = "";
      this.richTextBox1.Text = "";
      // 
      // RichTextEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.menuStrip1);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "RichTextEditor";
      this.Size = new System.Drawing.Size(993, 458);
      this.Tag = this.richTextBox1;
      this.Load += new System.EventHandler(this.RichTextEditor_Load);
      this.Enter += new System.EventHandler(this.RichTextEditor_Enter);
      this.Leave += new System.EventHandler(this.RichTextEditor_Leave);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }


    #endregion


 
  }
}