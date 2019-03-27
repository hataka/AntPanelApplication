using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLibrary;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Net;
using AntPanelApplication.CommonLibrary;
using AntPanelApplication;

namespace CommonLibrary.Controls
{
  public partial class BrowserEx : UserControl
  {
    public string linkFilePath = string.Empty;
    private string documentRoot;
    private string serverRoot;

    public WebBrowser Browser
    {
      get { return this.webBrowser1; }
      set { this.webBrowser1 = value; }
    }

    public BrowserEx()
    {
      //this.Font = PluginBase.MainForm.Settings.DefaultFont;
      this.InitializeComponent();
      this.InitializeLocalization();
      this.InitializeInterface();
      //this.GoHomeButton.Image = ImageManager.FindImage(224);
      //this.printButton.Image = ImageManager.FindImage(113);
      //this.stopButton.Image = ImageManager.FindImage(153);
      //string text = "0538077E-8C37-4A2B-962B-8FB77DC9F325";
      //this.xmlTreeMenu = (PluginMain)PluginBase.MainForm.FindPlugin(text);
      //this.settings = (XMLTreeMenu.Settings)this.xmlTreeMenu.Settings;
      //this.documentRoot = this.settings.DocumentRoot;
      //this.serverRoot = this.settings.ServerRoot;
    }

    private void InitializeLocalization()
    {
      this.goButton.Text = "移動";
      this.backButton.Text = "戻る";
      this.forwardButton.Text = "進む";
      this.refreshButton.Text = "再読込み";//
    }

    private void InitializeInterface()
    {
      // 未完成 例外発生 DockPanelStripRenderer(true)
      //this.toolStrip.Renderer = new DockPanelStripRenderer(true);
      //this.addressComboBox.FlatStyle = PluginBase.Settings.ComboBoxFlatStyle;
    }

    private void WebBrowserNewWindow(object sender, CancelEventArgs e)
    {
      //MessageBox.Show(this.browseEx1.StatusText)
      Globals.MainForm.OpenCustomDocument("BrowserEx", this.webBrowser1.StatusText);
      //PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseEx;" + this.webBrowser.StatusText);
      e.Cancel = true;
    }

    private void WebBrowserPropertyUpdated(object sender, EventArgs e)
    {
      this.backButton.Enabled = this.webBrowser1.CanGoBack;
      this.forwardButton.Enabled = this.webBrowser1.CanGoForward;
    }

    private void WebBrowserNavigated(object sender, WebBrowserNavigatedEventArgs e)
    {
      this.addressComboBox.Text = this.webBrowser1.Url.ToString();
      this.webBrowser1.Tag = this.webBrowser1.Url.ToString();
      //PluginBase.MainForm.StatusLabel.Text = this.webBrowser.Url.ToString();
      //this.AddHistoryMenuItem(this.webBrowser.Url.ToString());
      //this.xmlTreeMenu.pluginUI.AddPreviousCustomDocuments(this.webBrowser.Url.ToString());
    }

    private void WebBrowserDocumentTitleChanged(object sender, EventArgs e)
    {
      if (this.webBrowser1.DocumentTitle.Trim() == "")
      {
        string text = this.webBrowser1.Document.Domain.Trim();
        if (!string.IsNullOrEmpty(text))
        {
          base.Parent.Text = text;
        }
        else
        {
          //base.Parent.Text = TextHelper.GetString("Info.UntitledFileStart");
        }
      }
      else
      {
        base.Parent.Text = this.webBrowser1.DocumentTitle;
      }
      this.webBrowser1.Tag = this.webBrowser1.Url.ToString();
      //PluginBase1.MainForm.StatusLabel.Text = this.webBrowser1.Url.ToString();
    }

    private void AddressComboBoxSelectedIndexChanged(object sender, EventArgs e)
    {

      if (this.addressComboBox.SelectedItem != null)
      {
        string urlString = this.addressComboBox.SelectedItem.ToString();
        this.webBrowser1.Navigate(urlString);
      }
    }

    private void BackButtonClick(object sender, EventArgs e)
    {
      this.webBrowser1.GoBack();
    }

    private void ForwardButtonClick(object sender, EventArgs e)
    {
      this.webBrowser1.GoForward();
    }

    private void RefreshButtonClick(object sender, EventArgs e)
    {
      this.webBrowser1.Refresh();
    }

    private void BrowseButtonClick(object sender, EventArgs e)
    {
      string text = this.addressComboBox.Text;
      if (!this.addressComboBox.Items.Contains(text))
      {
        this.addressComboBox.Items.Insert(0, text);
      }
      this.webBrowser1.Navigate(text);
    }

    private void AddressComboBoxKeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        string text = this.addressComboBox.Text;
        if (!this.addressComboBox.Items.Contains(text))
        {
          this.addressComboBox.Items.Insert(0, text);
        }
        this.webBrowser1.Navigate(text);
      }
    }

    public void AddHistoryMenuItem(string file)
    {
      /*
      try
      {
        if (((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserHistory.Contains(file))
        {
          ((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserHistory.Remove(file);
        }
        ((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserHistory.Insert(0, file);
        this.PopulateHistoryMenu();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
      }
     */
    }

    public void PopulateHistoryMenu()
    {
      /*
      try
      {
        ToolStripMenuItem toolStripMenuItem = this.履歴ToolStripMenuItem;
        toolStripMenuItem.DropDownItems.Clear();
        for (int i = 0; i < ((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserHistory.Count; i++)
        {
          string text = ((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserHistory[i];
          ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
          toolStripMenuItem2.Click += new EventHandler(this.favoriteMenuItem_Click);
          toolStripMenuItem2.Tag = text;
          toolStripMenuItem2.Text = PathHelper.GetCompactPath(text);
          if (i < 15)
          {
            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
          }
          else
          {
            ((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserHistory.Remove(text);
          }
        }
        if (((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserHistory.Count > 0)
        {
          TextHelper.GetString("Label.ClearReopenList");
          toolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
          toolStripMenuItem.DropDownItems.Add(this.履歴をクリアToolStripMenuItem);
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
      */
    }

    public void AddBookMarksMenuItem(string label, string url)
    {
      /*
      string item = label + "|" + url;
      try
      {
        if (((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserBookMarks.Contains(item))
        {
          ((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserBookMarks.Remove(item);
        }
        ((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserBookMarks.Insert(0, item);
        this.PopulateBookMarksMenu();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
      }
    */
      }

    public void PopulateBookMarksMenu()
    {
      /*
      try
      {
        ToolStripMenuItem toolStripMenuItem = this.お気に入りToolStripMenuItem;
        toolStripMenuItem.DropDownItems.Clear();
        for (int i = 0; i < ((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserBookMarks.Count; i++)
        {
          string text = ((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserBookMarks[i];
          string[] array = text.Split(new char[]
          {
            '|'
          });
          string text2 = array[0];
          string tag = array[1];
          ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
          toolStripMenuItem2.Click += new EventHandler(this.favoriteMenuItem_Click);
          toolStripMenuItem2.Tag = tag;
          toolStripMenuItem2.Text = PathHelper.GetCompactPath(text2);
          if (i < 15)
          {
            toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
          }
          else
          {
            ((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserBookMarks.Remove(text);
          }
        }
        if (((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserBookMarks.Count > 0)
        {
          toolStripMenuItem.DropDownItems.Add(this.googleToolStripMenuItem);
          toolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
          toolStripMenuItem.DropDownItems.Add(this.お気に入りに追加ToolStripMenuItem);
          toolStripMenuItem.DropDownItems.Add(this.お気に入りのクリアToolStripMenuItem);
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
      */
    }

    private void Print()
    {
      this.webBrowser1.ShowPrintDialog();
    }

    private void PrintPreview()
    {
      this.webBrowser1.ShowPrintPreviewDialog();
    }

    public bool SaveWebPage(string format)
    {
      /*
      string str = string.Empty;
      if (format != null)
      {
        if (format == ".mht")
        {
          str = "mhtファイル(*.mht)|*.mht";
          goto IL_6C;
        }
        if (format == ".html")
        {
          str = "htmlファイル(*.html)|*.html";
          goto IL_6C;
        }
        if (format == ".txt")
        {
          str = "textファイル(*.txt)|*.txt";
          goto IL_6C;
        }
        if (format == ".jpeg")
        {
          str = "jpegファイル(*.jpeg)|*.jpeg";
          goto IL_6C;
        }
      }
      str = "mhtファイル(*.mht)|*.mht";
      IL_6C:
      string filter = str + "|すべてのファイル(*.*)|*.*";
      // FIXME
      //string initialDirectory = Path.Combine(PathHelper.BaseDir, "download");
      string fileName = "新しいファイル" + format;
      try
      {
        string text = Lib.File_SaveDialog(fileName, initialDirectory, filter);
        if (File.Exists(text))
        {
          DateTime now = DateTime.Now;
          string text2 = string.Format("_{0:00}{1:00}{2:00}{3:00}{4:00}{5:00}", new object[]
          {
            now.Year,
            now.Month,
            now.Day,
            now.Hour,
            now.Minute,
            now.Second
          });
          string.Concat(new string[]
          {
            Path.GetDirectoryName(text),
            "\\",
            Path.GetFileNameWithoutExtension(text),
            text2,
            Path.GetExtension(text)
          });
        }
        if (text != null)
        {
          if (format != null && !(format == ".mht"))
          {
            if (!(format == ".html"))
            {
              if (!(format == ".txt"))
              {
                if (format == ".jpeg")
                {
                  Bitmap bitmap = WebHandler.CaptureWebPage(this.webBrowser1.Url.ToString());
                  bitmap.Save(text, ImageFormat.Jpeg);
                  bitmap.Dispose();
                }
              }
              else
              {
                string encoding = this.webBrowser1.Document.Encoding;
                string innerText = this.webBrowser1.Document.Body.InnerText;
                StreamWriter streamWriter = new StreamWriter(text, false, Encoding.GetEncoding(encoding));
                streamWriter.Write(innerText);
                streamWriter.Close();
              }
            }
            else
            {
              string encoding2 = this.webBrowser1.Document.Encoding;
              string value = WebHandler.WebClientGet3(this.webBrowser1.Url.ToString(), encoding2);
              StreamWriter streamWriter2 = new StreamWriter(text, false, Encoding.GetEncoding(encoding2));
              streamWriter2.Write(value);
              streamWriter2.Close();
            }
          }
          MessageBox.Show(text + "\r\nに保存しました", "保存完了");
        }
      }
      catch (Exception ex)
      {
        string message = ex.Message.ToString();
        MessageBox.Show(Lib.OutputError(message), "保存失敗");
        return false;
      }
      */
      return true;


    }

    private void iEで開くToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("C:\\Program Files\\Internet Explorer\\iexplore.exe", this.webBrowser1.Url.ToString());
    }
    
    private void chromeで開くToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //MessageBox.Show(this.settings.ChromePath);
      Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", this.webBrowser1.Url.ToString());
    }
    /*
    private void fireFoxで開くToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //Process.Start("C:\\Program Files\\Mozilla Firefox\\firefox.exe", this.webBrowser.Url.ToString());
      Process.Start(this.settings.FirefoxPath, this.webBrowser1.Url.ToString());
    }
    */
    private void サクラエディタSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.linkFilePath))
      {
        //ProcessHandler.Run_Sakura(this.linkFilePath);
        Process.Start(@"C:\Program Files (x86)\sakura\sakura.exe", this.linkFilePath);
      }
    }

    private void pSPadPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.linkFilePath))
      {
        Process.Start(@"C:\Program Files (x86)\PSPad editor\PSPad.exe", this.linkFilePath);
      }
    }

    private void エクスプローラEToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.linkFilePath))
      {
        ProcessHandler.Run_Explorer(this.linkFilePath);
      }
    }

    private void コマンドプロンプトCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.linkFilePath))
      {
        ProcessHandler.Run_Cmd(this.linkFilePath);
      }
    }

    private void scintillaCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //PluginBase.MainForm.OpenEditableDocument(this.linkFilePath);
    }

    private void richTextBoxToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void ファイルエクスプローラを同期するXToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (File.Exists(this.linkFilePath))
      {
        //PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.BrowseTo;" + Path.GetDirectoryName(this.linkFilePath));
      }
    }

    private void ファイル名を指定して実行OToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void リンクを開くLToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void ソースの表示VToolStripMenuItem_Click(object sender, EventArgs e)
    {
      HtmlDocument document = this.webBrowser1.Document;
      string encoding = document.Encoding;
      WebHandler.WebClientGet3(this.webBrowser1.Url.ToString(), encoding);
    }

    private void codeの表示DToolStripMenuItem_Click(object sender, EventArgs e)
    {
      String url = this.webBrowser1.Url.ToString();
      if (File.Exists(Lib.Url2Path(this.webBrowser1.Url.ToString())))
      {
        //PluginBase.MainForm.OpenEditableDocument(Lib.Url2Path(this.webBrowser.Url.ToString()));
      }
      /*
      if (File.Exists(url.Replace(this.settings.ServerRoot, this.settings.DocumentRoot)))
      {
        PluginBase.MainForm.OpenEditableDocument(url.Replace(this.settings.ServerRoot, this.settings.DocumentRoot));
      }
    */
    }

    private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      HtmlDocument document = this.webBrowser1.Document;
      HtmlWindow window = document.Window;
      if (window.Frames.Count > 0)
      {
        if (window.Frames["MainPanel"].Document.GetElementById("fileurl").InnerText != string.Empty)
        {
          String fileurl = window.Frames["MainPanel"].Document.GetElementById("fileurl").InnerText;
          this.linkFilePath = Lib.Url2Path(window.Frames["MainPanel"].Document.GetElementById("fileurl").InnerText);
          //this.linkFilePath = fileurl.Replace(this.settings.ServerRoot, this.settings.DocumentRoot).Replace("/", "\\");
          return;
        }
      }
      else if (document.GetElementById("fileurl").InnerText != "")
      {
        string url = document.GetElementById("fileurl").InnerText;
        this.linkFilePath = Lib.Url2Path(document.GetElementById("fileurl").InnerText);
        //this.linkFilePath = url.Replace(this.settings.ServerRoot, this.settings.DocumentRoot).Replace("/", "\\");
      }
    }

    private void Browser_Load(object sender, EventArgs e)
    {
      String url = this.webBrowser1.Tag as String;
      if (!String.IsNullOrEmpty(url)) this.webBrowser1.Navigate(url);
    }

    private void mhtで保存ToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void htmlで保存ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SaveWebPage(".html");
    }

    private void textで保存ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SaveWebPage(".txt");
    }

    private void jpegで保存ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SaveWebPage(".jpeg");
    }

    private void 印刷ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Print();
    }

    private void 印刷プレビューVToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.PrintPreview();
    }

    private void searchButton_Click(object sender, EventArgs e)
    {
      this.webBrowser1.Navigate("http://www.google.co.jp/");
    }

    private void GoHomeButton_Click(object sender, EventArgs e)
    {
      this.webBrowser1.Navigate("http://kahata.travel.coocan.jp/pukiwiki2016/index.php");
    }

    private void printButton_Click(object sender, EventArgs e)
    {
      this.webBrowser1.Print();
    }

    private void stopButton_Click(object sender, EventArgs e)
    {
      this.webBrowser1.Stop();
    }

    private void favoriteMenuItem_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
      string text = toolStripMenuItem.Tag as string;
      if (text != string.Empty)
      {
        //PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseEx;" + text);
      }
    }

    private void デスクトップにショートカットを作成ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), PluginBase.MainForm.CurrentDocument.Text + ".url");
      //string str = ((Control)PluginBase.MainForm.CurrentDocument.Controls[0].Tag).Tag.ToString();
      //Encoding encoding = Encoding.GetEncoding("shift_jis");
      //StreamWriter streamWriter = new StreamWriter(path, false, encoding);
      //streamWriter.WriteLine("[InternetShortcut]");
      //streamWriter.WriteLine("URL=" + str);
      //streamWriter.Close();
    }

    private void お気に入りに追加ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //string url = ((Control)PluginBase.MainForm.CurrentDocument.Controls[0].Tag).Tag.ToString();
      //this.AddBookMarksMenuItem(PluginBase.MainForm.CurrentDocument.Text, url);
    }

    private void お気に入りのクリアToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserBookMarks.Clear();
      //this.PopulateBookMarksMenu();
    }

    private void 履歴をクリアToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //((XMLTreeMenu.Settings)this.xmlTreeMenu.Settings).BrowserHistory.Clear();
      //this.PopulateHistoryMenu();
    }

    private Icon GetIconFromUrl(string url)
    {
      WebBrowser webBrowser = new WebBrowser();
      webBrowser.ScrollBarsEnabled = false;
      webBrowser.ScriptErrorsSuppressed = true;
      webBrowser.Navigate(url);
      while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
      {
        Application.DoEvents();
      }
      Icon result = null;
      foreach (HtmlElement htmlElement in webBrowser.Document.GetElementsByTagName("link"))
      {
        string attribute = htmlElement.GetAttribute("rel");
        if (attribute == "shortcut icon" || attribute == "icon")
        {
          string attribute2 = htmlElement.GetAttribute("href");
          if (attribute2.StartsWith("http"))
          {
            result = this.getIconFromUrl(attribute2);
            break;
          }
          if (attribute2.StartsWith("/"))
          {
            result = this.getIconFromUrl("http://" + webBrowser.Url.Host + attribute2);
            break;
          }
          result = this.getIconFromUrl(webBrowser.Url.ToString() + attribute2);
          break;
        }
      }
      return result;
    }

    private Icon getIconFromUrl(string url)
    {
      Icon result;
      using (WebClient webClient = new WebClient())
      {
        using (MemoryStream memoryStream = new MemoryStream(webClient.DownloadData(url)))
        {
          result = new Icon(memoryStream);
        }
      }
      return result;
    }






    /*
      http://gurizuri0505.halfmoon.jp/20121022/50397
          NET FRAMEWORKのUserControlには終了処理に向いた，イベントが用意されてない，っぽい（汗
    スレッドの後始末をやろうとして，どん詰まった次第っちゅうか，
    いままで，UserControlに終了処理らしきものを書いていたけど，全然終了処理してなかった模様（爆
    微妙にリソースリークしまくり（猛汗
    結果，米国サイトで見つけた，
    OnHandleDestroyed()メソッドをオーバーライドするのが，一番良さそう？
    なんのハンドルの破棄時に呼び出されるのかが，イマイチ謎じゃが（爆
    なんとなく，ウインドウハンドルの破棄直前？
    ってことで，UI系の終了処理とかに用いると，バグる可能性もありそう．
    サンプルは載せておきますが，用法用量を守って，ご使用くださいませ♪
    いつものように，バグってたら，すまんこってす
    */

    //UserControlのOnHandleDestroyed()をオーバーライドしてみる
    protected override void OnHandleDestroyed(EventArgs e)
  {
    Dispose2();
    base.OnHandleDestroyed(e);
  }

   //——————————————
   //終了処理
   //
   //——————————————
   private void Dispose2()
    {
      /*
      m_ThreadLoopFL = false; //スレッドに終了指示
      for (int i = 0; i < 100; i++) //スレッド終了まで待機
      {
        if (m_Thread == null) //スレッドハンドルがNULLで全スレッドの破棄処理が完了
        {
          break;
        }

        Thread.Sleep(1); //スリープとメッセージポンプ回して，負荷上がり過ぎないように
        Application.DoEvents();
      }


      if (m_DBCtrl != null) //こちらは弊社で作ったデータベースエンジン．コヤツの破棄処理も
      {
        m_DBCtrl.Dispose();
        m_DBCtrl = null;
      }
    */  
    }

    private void BrowserEx_Enter(object sender, EventArgs e)
    {
      String url = this.webBrowser1.Tag as String;
      //if (!String.IsNullOrEmpty(url)) this.webBrowser1.Url = new Uri(url);
      if (!String.IsNullOrEmpty(url)) this.webBrowser1.Navigate(url);
    }
  }
}
