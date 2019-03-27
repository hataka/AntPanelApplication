using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AntPanelApplication.Managers;
using AntPanelApplication.CommonLibrary;
using System.IO;

namespace AntPanelApplication.Controls.SimplePanel
{
  public partial class SimplePanel : UserControl
  {
    #region Variables
    /*
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
    private Panel panel1;
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
    private ToolStripMenuItem アプリケーションを起動PToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator10;
    */
    private List<string> previousDocuments = new List<string>();
    public string command = string.Empty;
    public string args = string.Empty;
    public string path = string.Empty;
    public string option = string.Empty;
    public string argstring = string.Empty;
    public AntPanel antPanel;
    public ImageList imageList1= new ImageList();

    private List<Process> processList = new List<Process>();
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

    #region Constructor
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
    #endregion

    #region Initialization
    private void InitializeSimplePanel()
    {
      this.imageList1 = AntPanel.imageList2;
      this.imageList1.TransparentColor = Color.FromArgb(233, 229, 215);
      this.toolStripDropDownButton2.Image = this.imageList1.Images[64];
      this.toolStripDropDownButton3.Image = this.imageList1.Images[117];
      this.toolStripDropDownButton4.Image = this.imageList1.Images[15];
      this.toolStrip1.Visible = false;
      this.statusStrip1.Visible = false;
    }
    #endregion

    private void SimplePanel_Load(object sender, EventArgs e)
    {
      //this.OpenFile(command + "|" + args + "|" + path + "|" + option);
      string command = string.Empty;
      string args = string.Empty;
      string path = string.Empty;
      string option = string.Empty;
      string argstring = string.Empty;

      try
      {
        // 仮
        argstring = String.Empty;// this.panel1.Tag.ToString();
        string[] array = argstring.Split(new char[] { '|' });
        command = ActionManager.ProcessVariable(array[0]);
        args = ((array.Length > 1) ? ActionManager.ProcessVariable(array[1]) : string.Empty);
        path = ((array.Length > 2) ? ActionManager.ProcessVariable(array[2]) : string.Empty);
        option = ((array.Length > 3) ? ActionManager.ProcessVariable(array[3]) : string.Empty);
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
        Process process = CommonLibrary.Win32.MdiUtil.LoadProcessInControl(processStartInfo, this.panel1);
        CommonLibrary.Win32.ShowMaximized(process.MainWindowHandle);
        this.processList.Insert(0, process);
      }
      this.IntializeSettings();
      this.AddPreviousDocuments(argstring);
      //((Form)base.Parent).FormClosing += new FormClosingEventHandler(this.parentForm_Closing);
    }

    public void IntializeSettings()
    {
      this.ツールバーTToolStripMenuItem.Checked = this.toolStrip1.Visible;
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
          string text2 = ActionManager.ProcessVariable(array[0]);
          string text3 = (array.Length > 1) ? ActionManager.ProcessVariable(array[1]) : string.Empty;
          string text4 = (array.Length > 2) ? ActionManager.ProcessVariable(array[2]) : string.Empty;
          if (array.Length <= 3)
          {
            string arg_A1_0 = string.Empty;
          }
          else
          {
            ActionManager.ProcessVariable(array[3]);
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
      string[] array = text.Split(new char[]
      {
        '|'
      });
      string text2 = ActionManager.ProcessVariable(array[0]);
      string text3 = (array.Length > 1) ? ActionManager.ProcessVariable(array[1]) : string.Empty;
      string text4 = (array.Length > 2) ? ActionManager.ProcessVariable(array[2]) : string.Empty;
      if (array.Length <= 3)
      {
        string arg_8D_0 = string.Empty;
      }
      else
      {
        ActionManager.ProcessVariable(array[3]);
      }
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
          Process process = CommonLibrary.Win32.MdiUtil.LoadProcessInControl(processStartInfo, this.panel1);
          CommonLibrary.Win32.ShowMaximized(process.MainWindowHandle);
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
    }

    private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
    {
      /*
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
        Process process = AntPlugin.CommonLibrary.Win32.MdiUtil.LoadProcessInControl(this.openFileDialog1.FileName, this.panel1);
        AntPlugin.CommonLibrary.Win32.ShowMaximized(process.MainWindowHandle);
        this.processList.Insert(0, process);
        ((DockContent)base.Parent).TabText = Path.GetFileName(this.openFileDialog1.FileName);
        this.AddPreviousDocuments(this.openFileDialog1.FileName);
      }
      catch (Exception)
      {
        MessageBox.Show("ファイルを開く処理でエラーが発生しました。ファイルの種類を確認して下さい。", "MDI Sample", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      */
    }

    private void graph371ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      /*
      string text = Path.Combine(PathHelper.BaseDir, "DockableControls\\graph371.exe");
      Process process = AntPlugin.CommonLibrary.Win32.MdiUtil.LoadProcessInControl(text, this.panel1);
      new StringBuilder(100);
      AntPlugin.CommonLibrary.Win32.ShowMaximized(process.MainWindowHandle);
      this.processList.Insert(0, process);
      ((DockContent)base.Parent).TabText = "graph371.exe";
      this.AddPreviousDocuments(text);
      */
    }

    private void lesson5ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      /*
      string text = "F:\\c_program\\OpenGL\\NeHe\\Lesson05\\Lesson5.exe";
      Process process = AntPlugin.CommonLibrary.Win32.MdiUtil.LoadProcessInControl(text, this.panel1);
      StringBuilder stringBuilder = new StringBuilder(100);
      AntPlugin.CommonLibrary.Win32.ShowMaximized(process.MainWindowHandle);
      AntPlugin.CommonLibrary.Win32.GetWindowText(process.MainWindowHandle, stringBuilder, stringBuilder.Capacity);
      this.processList.Insert(0, process);
      ((DockContent)base.Parent).TabText = stringBuilder.ToString();
      this.AddPreviousDocuments(text);
      */
    }

    private void 試験ToolStripMenuItem1_Click(object sender, EventArgs e)
    {
    }

    private void プロセスPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      /*
      List<IntPtr> windowsInControl = AntPlugin.CommonLibrary.Win32.GetWindowsInControl(this.panel1.Handle);
      IntPtr window = AntPlugin.CommonLibrary.Win32.GetWindow(this.panel1.Handle, 5u);
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
      */
    }

    private void ProcessItem_Click(object sender, EventArgs e)
    {
      /*
      ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
      Process process = toolStripMenuItem.Tag as Process;
      foreach (ToolStripMenuItem toolStripMenuItem2 in this.プロセスPToolStripMenuItem.DropDownItems)
      {
        toolStripMenuItem2.Checked = false;
      }
      toolStripMenuItem.Checked = true;
      if (process != null)
      {
        AntPlugin.CommonLibrary.Win32.SetForegroundWindow(process.MainWindowHandle);
        ((DockContent)base.Parent).TabText = Path.GetFileName(process.StartInfo.FileName);
        this.AddPreviousDocuments(process.StartInfo.FileName);
      }
      */
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
      /*
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
      ((DockContent)base.Parent).TabText = "Execute In Place";
      */
    }

    private void デスクトップに移動ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      /*
      IntPtr window = AntPlugin.CommonLibrary.Win32.GetWindow(this.panel1.Handle, 5u);
      AntPlugin.CommonLibrary.Win32.SetParent(window, IntPtr.Zero);
      Process process = this.FindProcessByHandle(window);
      if (this.processList.Contains(process))
      {
        this.processList.Remove(process);
      }
      window = AntPlugin.CommonLibrary.Win32.GetWindow(this.panel1.Handle, 5u);
      if (window == IntPtr.Zero)
      {
        ((DockContent)base.Parent).TabText = "Execute In Place";
        return;
      }
      process = this.FindProcessByHandle(window);
      if (process == null)
      {
        ((DockContent)base.Parent).TabText = "Execute In Place";
        return;
      }
      if (File.Exists(process.StartInfo.Arguments))
      {
        ((DockContent)base.Parent).TabText = Path.GetFileName(process.StartInfo.Arguments);
        return;
      }
      if (File.Exists(process.StartInfo.FileName))
      {
        ((DockContent)base.Parent).TabText = Path.GetFileName(process.StartInfo.FileName);
        return;
      }
      ((DockContent)base.Parent).TabText = "Execute In Place";
      */
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
      /*
      string path = string.Empty;
      IntPtr window = AntPlugin.CommonLibrary.Win32.GetWindow(this.panel1.Handle, 5u);
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
      */
    }

    private void コマンドプロンプトCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      /*
      string path = string.Empty;
      IntPtr window = AntPlugin.CommonLibrary.Win32.GetWindow(this.panel1.Handle, 5u);
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
    */
    }

    public Process process = null;
    private void panel1_DragDrop(object sender, DragEventArgs e)
    {
      /*
      string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
      try
      {
        //Process process = MDIForm.CommonLibrary.Win32.MdiUtil.LoadProcessInControl(array[0], this.panel1);
        this.process = AntPlugin.CommonLibrary.Win32.MdiUtil.LoadProcessInControl(array[0], this.panel1);
        AntPlugin.CommonLibrary.Win32.ShowMaximized(process.MainWindowHandle);
        this.processList.Insert(0, process);
        ((DockContent)base.Parent).TabText = Path.GetFileName(array[0]);
        this.AddPreviousDocuments(array[0]);
      }
      catch (Exception)
      {
        MessageBox.Show("ファイルを開く処理でエラーが発生しました。ファイルの種類を確認して下さい。", "MDI Sample", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      */
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
      //MessageBox.Show(this.panel1.Tag.ToString());
      //MessageBox.Show(this.command);
      //MessageBox.Show(this.args);
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
      ((Form)this.Parent).Close();
    }





  }
}
