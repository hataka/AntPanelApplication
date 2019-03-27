using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Collections;
using System.Diagnostics;
using AntPanelApplication.CommonLibrary;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AntPanelApplication.Controls
{
  public partial class RichTextEditor : UserControl
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
    public static AntPanel antPanel = null;
    #endregion

    #region Properties
    public RichTextBox RichTextBox
    {
      get
      {
        return this.richTextBox1;
      }
    }
    public MenuStrip MenuBar
    {
      get
      {
        return this.menuStrip1;
      }
      set
      {
        this.menuStrip1 = value;
      }
    }
    public ToolStrip ToolBar
    {
      get
      {
        return this.toolStrip1;
      }
    }
    public StatusStrip StatusBar
    {
      get
      {
        return this.statusStrip1;
      }
      set
      {
        this.statusStrip1 = value;
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
      InitializeInterface();
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

    public RichTextEditor(AntPanel ui)
    {
      RichTextEditor.antPanel = ui;
      InitializeComponent();
    }
    #endregion

    #region Initialization
    public void InitializeInterface()
    {
    }

    public void IntializeRichTextPanel()
    {
      InitializeImageList();
      this.toolStripDropDownButton2.Image = imageList1.Images[61];
      this.toolStripDropDownButton3.Image = imageList1.Images[15];
      this.toolStripDropDownButton4.Image = imageList1.Images[117];
    }

    public void InitializeImageList()
    {
      Bitmap bmp3 = (Bitmap)this.imageListButton.Image;
      imageList1 = new ImageList();
      imageList1.Images.AddStrip(bmp3);
      imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      imageList1.ImageSize = new System.Drawing.Size(16, 16);
      imageList1.TransparentColor = System.Drawing.Color.Transparent;
    }

    // Handles the click event for the menu items.
    public void AddIcon(String iconPath)
    {
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
      String path = this.richTextBox1.Tag as String;
      this.printingText = this.richTextBox1.Text;
      this.printingPosition = 0;
      this.printFont = new Font("ＭＳ Ｐゴシック", 10f);
      this.IntializeSettings();
      if (!String.IsNullOrEmpty(path) && File.Exists(path))
      {
        this.LoadFile(path);
      }


      try
      {
        //((Form)base.Parent).FormClosing += new FormClosingEventHandler(this.parentForm_Closing);
      }
      catch { }
      this.richTextBox1.Modified = (this.modifiedFlag = false);
    }

    public void IntializeSettings()
    {
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
    }

    private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string fileName = "default.txt";
      string filter = "All files(*.*)|*.*|Supported files|*.txt;*.log;*.ini;*.inf;*.tex;*.htm;*.html;*.css;*.js;*.xml;*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx;*.cs;*.java;*.py;*.rb;*.pl;*.vbs;*.bat|Text file(*.txt, *.log, *.tex, ...)|*.txt;*.log;*.ini;*.inf;*.tex|HTML file(*.htm, *.html)|*.htm;*.html|CSS file(*.css)|*.css|Javascript file(*.js)|*.js|XML file(*.xml)|*.xml|C/C++ source(*.c, *.h, ...)|*.c;*.cpp;*.cxx;*.h;*.hpp;*.hxx|C# source(*.cs)|*.cs|Java source(*.java)|*.java|Python script(*.py)|*.py|Ruby script(*.rb)|*.rb|Perl script(*.pl)|*.pl|VB script(*.vbs)|*.vbs|Batch file(*.bat)|*.bat";
      string workingDirectory = AntPanel.projectPath;
      string file = Lib.File_OpenDialog(fileName, workingDirectory, filter);
      this.currentPath = file;
      try
      {
        if (Path.GetExtension(this.currentPath) == ".rtf")
        {
          this.richTextBox1.LoadFile(file);
        }
        else
        {
          this.richTextBox1.Text = Lib.File_ReadToEndDecode(this.currentPath);
        }
        this.richTextBox1.Tag = this.currentPath;
        this.AddPreviousDocuments(file);
        this.PopulatePreviousDocumentsMenu();
        this.UpdateStatusText(this.currentPath);
      }
      catch (Exception ex)
      {
        string message = ex.Message.ToString();
        MessageBox.Show(Lib.OutputError(message));
        //MessageBox.Show(ex.Message.ToString());
      }
    }

    public void LoadFile(string path)
    {
      if (!File.Exists(path))
      {
        MessageBox.Show("パスが見つかりません", "RichTextEditor.LoadFile(string path)");
      }
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
        this.AddPreviousDocuments(text);
        this.PopulatePreviousDocumentsMenu();
        this.UpdateStatusText(this.currentPath);
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()), "RichTextEditor.LoadFile(string path)");
      }
    }

    private void 上書き保存SToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string[] array = ((string)this.richTextBox1.Tag).Split(new char[] { '!' });
      if (array.Length > 1)
      {
        this.名前を付けて保存AToolStripMenuItem_Click(sender, e);
      }
      else
      {
        if (Path.GetExtension(this.currentPath) == ".rtf")
        {
          this.richTextBox1.SaveFile(this.currentPath, RichTextBoxStreamType.RichText);
        }
        else
        {
          Lib.File_SaveEncode(this.currentPath, this.richTextBox1.Text, Lib.File_GetCode(this.currentPath));
        }
        this.richTextBox1.Modified = (this.modifiedFlag = false);
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
      string[] array = ((string)this.richTextBox1.Tag).Split(new char[] { '!' });
      string initialDirectory;
      string fileName;
      if (this.richTextBox1.Tag == null)
      {
        initialDirectory = AntPanel.projectPath;
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
          initialDirectory = AntPanel.projectPath;
        }
        fileName = "新しいファイル.txt";
      }
      else
      {
        try
        {
          initialDirectory = Path.GetDirectoryName(array[0]);
          fileName = Path.GetFileName(array[0]);
          text = Lib.File_GetCode(array[0]);
          if (text == string.Empty)
          {
            text = "UTF-8";
          }
        }
        catch (Exception ex)
        {
          string message = ex.Message.ToString();
          Lib.OutputError(message);
          //MessageBox.Show(message);
          initialDirectory = Path.GetDirectoryName(array[0]);
          fileName = Path.GetFileName(array[0]);
          text = "UTF-8";
        }
      }
      try
      {
        string text2 = Lib.File_SaveDialog(fileName, initialDirectory, filter);
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
        if (AntPanel.IsRunningWindows)
        {
          Process.Start(@"C:\Program Files (x86)\sakura\sakura.exe", this.currentPath);
        }
        else if (AntPanel.IsRunningUnix)
        {
          Process.Start("/home/kazuhiko/bin/sakura.sh", this.currentPath);
        }
      }
    }

    private void pSPadPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
        if (AntPanel.IsRunningWindows)
        {
          Process.Start(@"C:\Program Files (x86)\PSPad editor\PSPad.exe", this.currentPath);
        }
        else if (AntPanel.IsRunningUnix)
        {
          Process.Start("/home/kazuhiko/bin/pspad.sh", this.currentPath);
        }
      }
    }

    private void emacsEToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (AntPanel.IsRunningUnix && File.Exists(this.currentPath))
      {
        //PluginBase.MainForm.OpenEditableDocument(this.currentPath);
        Process.Start("/usr/bin/emacs", this.currentPath);
      }
    }

    private void geditGToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (AntPanel.IsRunningUnix)
      {
        Process.Start("/usr/bin/gedit", this.currentPath);
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
          if (AntPanel.IsRunningWindows)
          {
            Process.Start(path);
          }
          else if (AntPanel.IsRunningUnix)
          {
            Process.Start("/usr/bin/nautilus", this.currentPath);
          }
        }
        else if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
        {
          if (AntPanel.IsRunningWindows)
          {
            Process.Start(Path.GetDirectoryName(path));
          }
          else if (AntPanel.IsRunningUnix)
          {
            Process.Start("/usr/bin/nautilus", Path.GetDirectoryName(path));
          }
        }
      }
    }

    private void コマンドプロンプトCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      String path = this.currentPath;
      if (File.Exists(this.currentPath))
      {
        if (System.IO.Directory.Exists(path))
        {
          if (AntPanel.IsRunningWindows)
          {
            System.IO.Directory.SetCurrentDirectory(path);
            Process.Start(@"C:\windows\system32\cmd.exe");
          }
          else if (AntPanel.IsRunningUnix)
          {
            //Process.Start("/usr/bin/nautilus", this.currentPath);
            String comm = "gnome-terminal";
            String arguments = "-e \"sh -c \'cd " + path + "; exec bash\'\"";
            Process.Start(comm, arguments);
          }
        }
        else if (System.IO.Directory.Exists(Path.GetDirectoryName(path)))
        {
          if (AntPanel.IsRunningWindows)
          {
            System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(path));
            Process.Start(@"C:\windows\system32\cmd.exe");
          }
          else if (AntPanel.IsRunningUnix)
          {
            //Process.Start("/usr/bin/nautilus", this.currentPath);
            String comm = "gnome-terminal";
            String arguments = "-e \"sh -c \'cd " + Path.GetDirectoryName(path) + "; exec bash\'\"";
            Process.Start(comm, arguments);
          }
        }
      }
    }

    private void 現在のファイルをブラウザで開くWToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
        if (AntPanel.IsRunningWindows)
        {
          Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", this.currentPath);
        }
        else if (AntPanel.IsRunningUnix)
        {
          Process.Start("usr/bin/google-chrome", this.currentPath);
        }
      }
    }

    private void microsoftWordで開くWToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.currentPath))
      {
        if (AntPanel.IsRunningWindows)
        {
          Process.Start(@"C:\Program Files\LibreOffice\program\soffice.exe", this.currentPath);
        }
        else if (AntPanel.IsRunningUnix)
        {
          Process.Start("usr/bin/libreoffice", this.currentPath);
        }
      }
    }

    private void ファイル名を指定して実行OToolStripMenuItem_Click(object sender, EventArgs e)
    {
#if Interface
      //PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.RunProcessDialog");
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
      string[] array = selectedText.Split(new char[] { '\n' });
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
      string text = Lib.File_OpenDialog("menu.xml", AntPanel.projectDir,
        "TreeDataファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*");
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
              this.richTextBox1.Text = Lib.File_ReadToEndDecode(text);
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

    public void UpdateStatusText(string data)
    {
      //MessageBox.Show(data);
      int x = this.currentPoint.X;
      int y = this.currentPoint.Y;
      string format = " 行: {0} | 列: {1} | 改行コード: ({2}) | 文字コード: {3} | {4}";
      string[] array = data.Split(new char[] { '!' });
      if (array.Length > 1)
      {
        this.上書き保存SToolStripMenuItem.Enabled = (this.上書き保存SToolStripButton.Enabled = false);
      }
      else
      {
        this.上書き保存SToolStripMenuItem.Enabled = (this.上書き保存SToolStripButton.Enabled = true);
      }
      string text = array[0];
      string text2 = Lib.File_GetEofCode(text);
      string text3 = Lib.File_GetCode(text);
      this.toolStripStatusLabel1.Text = data;
      //this.toolStripStatusLabel1.Text = string.Format(format, new object[]{y,x,text2,text3,text});
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

    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {
      string tokens = "(auto|double|int|struct|break|else|long|switch|case|enum|register|typedef|char|extern|return|union|const|float|short|unsigned|continue|for|signed|void|default|goto|sizeof|volatile|do|if|static|while)";
      Regex rex = new Regex(tokens);
      MatchCollection mc = rex.Matches(richTextBox1.Text);
      int StartCursorPosition = richTextBox1.SelectionStart;
      foreach (Match m in mc)
      {
        int startIndex = m.Index;
        int StopIndex = m.Length;
        richTextBox1.Select(startIndex, StopIndex);
        richTextBox1.SelectionColor = Color.Blue;
        richTextBox1.SelectionStart = StartCursorPosition;
        richTextBox1.SelectionColor = Color.Black;
      }
    }
  }
}
