using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using TKFP.IO;
using TKFP.Net;

using System.Xml.Serialization;
using System.Collections.Generic;
using AntPlugin;
using AntPanelApplication;

namespace AntPlugin.Controls
{
  public class FTPClientPanel : UserControl
  {
    #region FtpClient Variables
    public AntPanel antPanel;
    public FtpClient FtpClient;
    public TKFP.IO.DirectoryInfo CurrentDirectory;
    public string currentFilePath;

    public string serverName = string.Empty;
    public string userID = string.Empty;
    public string serverUrl = string.Empty;
    public string password = string.Empty;
    public string serverAddress = string.Empty;
    public string serverDirectory = string.Empty;
    public string serverDocumentRoot = string.Empty;
    public string currentDownLoadedFile = string.Empty;
    public ImageList imageList2;
    public OpenFileDialog openFileDialog1;
    public System.Windows.Forms.ToolStripButton syncronizeButton;
    public SaveFileDialog saveFileDialog1;
    public ServersModel serversModel;
    global::AntPanelApplication.Properties.Settings settings;
    public String BaseDir = @"F:\VCSharp\Flashdevelop5.1.1-LL\FlashDevelop\Bin\Debug";
    public string nextFTPPath = @"C:\Program Files\NextFTP\NEXTFTP.EXE";
    public string sakuraPath = @"C:\Program Files (x86)\sakura\sakura.exe";
    public string pspadPath = @"C:\Program Files (x86)\PSPad editor\PSPad.exe";
    public string chromePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";

    //繰り返す項目は以下の様にSystem.Collections.Generic.Listとして宣言、
    [XmlElement("server")]
    public List<ServerModel> Servers = null;
    private ToolStripMenuItem 同期ファイルを開くToolStripMenuItem1;
    private ToolStripMenuItem ドキュメントToolStripMenuItem;
    private ToolStripMenuItem さくらエディタToolStripMenuItem;
    private ToolStripMenuItem システムToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator13;
    private ToolStripMenuItem ダウンロードボタンToolStripMenuItem;
    private ToolStripMenuItem アップロードボタンToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator14;
    private ToolStripMenuItem ファイルエクスプローラとシンクロボタンToolStripMenuItem;
    private ToolStripMenuItem nextFTPToolStripMenuItem;
    private SplitContainer splitContainer1;
    private PropertyGrid propertyGrid1;

    //XML属性(id)は、System.Xml.Serialization.XmlAttributeで属性を与えのことで属性を取り込めます
    [XmlAttribute("id")]
    public String ID = null;
    #endregion

    #region Constructor
    /*
    public FTPClientPanel(PluginMain pluginMain)
    {
      this.InitializeComponent();
      this.pluginMain = pluginMain;
      this.settings = ((Settings)pluginMain.Settings);

      this.InitializeFTPClient();
    }
    */
    public FTPClientPanel(AntPanel ui)
    {
      this.antPanel = ui;
      this.settings = new AntPanelApplication.Properties.Settings();
      this.InitializeComponent();
      this.InitializeFTPClient();
    }
    #endregion

    #region public Classes

    public class DirectoryItem : ListViewItem
    {
      public readonly TKFP.IO.DirectoryInfo Directory;
      public DirectoryItem(TKFP.IO.DirectoryInfo Directory)
      {
        this.Directory = Directory;
        this.Text = Directory.Name;
        this.SubItems.Add("<dir>");
        this.SubItems.Add(Directory.Permission.PermissionCode);
        this.SubItems.Add(Directory.LastWriteTime.ToString("yy-MM-dd HH:mm"));
        this.ImageIndex = 0;
      }
    }

    private class FileItem : ListViewItem
    {
      public readonly TKFP.IO.FileInfo File;
      public FileItem(TKFP.IO.FileInfo File)
      {
        this.File = File;
        this.Text = File.Name;
        this.SubItems.Add(File.Length.ToString("#,##0"));
        this.SubItems.Add(File.Permission.PermissionCode);
        this.SubItems.Add(File.LastWriteTime.ToString("yy-MM-dd HH:mm"));
        this.ImageIndex = 1;
      }
    }

    [XmlRoot("servers")]
    public class ServersModel
    {
      /// <summary>
      /// 人
      /// </summary>
      [XmlElement("server")]
      public List<ServerModel> Servers { get; set; }
    }

    public class ServerModel
    {
      /// <summary>
      /// ID
      /// </summary>
      [XmlAttribute("id")]
      public String ID { get; set; }
      /// <summary>
      /// 標題
      /// </summary>
      [XmlElement("name")]
      public String Name { get; set; }
      /// <summary>
      /// FTPサーバ名
      /// </summary>
      [XmlElement("serverAddress")]
      public String ServerAddress { get; set; }
      /// <summary>
      /// URL
      /// </summary>
      [XmlElement("serverUrl")]
      public String ServerUrl { get; set; }
      /// <summary>
      /// 転送先開始ディレクトリ
      /// </summary>
      [XmlElement("serverDirectory")]
      public String ServerDirectory { get; set; }
      /// <summary>
      /// FTPアカウント
      /// </summary>
      [XmlElement("userID")]
      public String UserID { get; set; }
      /// <summary>
      /// FTPパスワード
      /// </summary>
      [XmlElement("password")]
      public String Password { get; set; }
      /// <summary>
      /// ローカルディスクのドキュメントルート
      /// </summary>
      [XmlElement("documentRoot")]
      public String DocumentRoot { get; set; }
      /// <summary>
      /// サーバーの説明
      /// </summary>
      [XmlElement("description")]
      public String Description { get; set; }
    }
    #endregion

    #region Initialization

    private void InitializeFTPClient()
    {
      this.ClientSize = new Size(342, 560);
      this.InitializeImage();
      this.InitializeDialog();
      this.InitializeSettings();

      //this.niftyToolStripMenuItem.Visible = false;
      //this.lviFolder.Visible = false;
      //this.toolStripSeparator1.Visible = false;
      //this.表示ToolStripButton.Visible = false;
      //アイテムごとにツールヒントが表示されるようにする
      this.lviFolder.ShowItemToolTips = true;

      this.connectDropDownButton.DropDownItems.Clear();
      this.connectDropDownButton.DropDownItems.Add(this.nexrFTPToolStripMenuItem);
      this.connectDropDownButton.DropDownItems.Add(this.toolStripSeparator1);
      this.loadServerXML(System.IO.Path.Combine(BaseDir, @"SettingData\server.xml"));

      this.splitContainer1.Panel2Collapsed = true;
      this.splitContainer1.Panel1Collapsed = false;
      this.propertyGrid1.HelpVisible = false;
      this.propertyGrid1.ToolbarVisible = false;
    }

    private void InitializeImage()
    {
      Bitmap value = ((System.Drawing.Bitmap)(this.imageListButton.Image));
      this.imageList2 = new ImageList();
      this.imageList2.Images.AddStrip(value);
      this.imageList2.TransparentColor = Color.FromArgb(233, 229, 215);
      //this.connectDropDownButton.Image = this.imageList2.Images[71];
      //this.disconnectButton.Image = this.imageList2.Images[45];
      //this.parentDirButton.Image = this.imageList2.Images[97];
      //this.downLoadButton.Image = this.imageList2.Images[112];
      //this.upLoadButton.Image = this.imageList2.Images[111];
      //this.ツールDropDownButton.Image = PluginBase.MainForm.FindImage("263");
      //this.syncronizeButton.Image = PluginBase.MainForm.FindImage("203|9|-3|-3");
    }

    private void InitializeDialog()
    {
      this.openFileDialog1 = new OpenFileDialog();
      this.openFileDialog1.RestoreDirectory = true;
      this.openFileDialog1.CheckFileExists = true;
      this.openFileDialog1.CheckPathExists = true;
      this.openFileDialog1.DereferenceLinks = true;
      this.openFileDialog1.AddExtension = true;
      this.saveFileDialog1 = new SaveFileDialog();
    }

    private void InitializeSettings()
    {
      if (this.pspadPath != this.settings.win_PspadPath) this.pspadPath = this.settings.win_PspadPath;
      if (this.sakuraPath != this.settings.win_SakuraPath) this.sakuraPath = this.settings.win_SakuraPath;
      if (this.nextFTPPath != this.settings.win_NextFTPPath) this.nextFTPPath = this.settings.win_NextFTPPath;
      if (this.chromePath != this.settings.win_ChromePath) this.chromePath = this.settings.win_ChromePath;
      this.toolStrip1.Font = this.settings.FTPClientDefaultFont;
      this.lviFolder.Font = this.settings.FTPClientDefaultFont;
    }

    #endregion


    #region FTPClient Functions

    private void loadServerXML(String path)
    {
      System.IO.FileStream fs
        = new System.IO.FileStream(path, FileMode.Open);
      XmlSerializer serializer = new XmlSerializer(typeof(ServersModel));
      this.serversModel = (ServersModel)serializer.Deserialize(fs);
      // bug-fixed 2018-03-21
      fs.Close();
      fs.Dispose();
      foreach (ServerModel server in this.serversModel.Servers)
      {
        ToolStripMenuItem serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        serverToolStripMenuItem.Name = server.Name;
        serverToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
        serverToolStripMenuItem.Tag = server;
        serverToolStripMenuItem.Text = server.Name;
        serverToolStripMenuItem.Click += new System.EventHandler(this.serverToolStripMenuItem_Click);
        this.connectDropDownButton.DropDownItems.Add(serverToolStripMenuItem);
      }
    }

    private void FTPConnect()
    {
      if (this.FtpClient != null)
      {
        return;
      }
      BasicFtpLogon ftpLogon = new BasicFtpLogon(this.userID, this.password);
      this.FtpClient = new FtpClient(ftpLogon, this.serverAddress, 21);
      this.FtpClient.ConnectionMode = ConnectionModes.Passive;
      this.FtpClient.ListType = ListType.LIST;
      this.FtpClient.ListCacheValidityInterval = 60;
      this.FtpClient.FileSystemCacheValidityInterval = 60;
      this.FtpClient.ListDataLoader = new UnixListDataLoader();
      this.FtpClient.MessageReceive += new MessageReceiveHandler(this.FtpClient_MessageReceive);
      this.FtpClient.MessageSend += new MessageSendHandler(this.FtpClient_MessageSend);
      if (!this.FtpClient.Connect())
      {
        ((Form1)this.antPanel.Tag).Text = "接続失敗";
        this.FtpClient.Close();
        this.FtpClient = null;
        return;
      }
      ((Form1)this.antPanel.Tag).Text = "接続成功";
      if (this.FtpClient.IsEncrypted)
      {
        // TODO :
        //ToolStripStatusLabel expr_FE = PluginBase.MainForm.StatusLabel;
        //expr_FE.Text += " (・・ｻ)";
      }
      this.CurrentDirectory = new TKFP.IO.DirectoryInfo(this.FtpClient, this.serverDirectory);
      this.ShowFolder();

      this.syncronizeFileExplorer(false);
    }

    private void ShowFolder()
    {
      this.toolStripStatusLabel1.Text = this.CurrentDirectory.FullName;
      if (!this.CurrentDirectory.Parent.FullName.StartsWith(this.serverDirectory))
      {
        //this.toolStripButton2.Enabled = false;
      }
      else
      {
        this.parentDirButton.Enabled = true;
      }
      this.lviFolder.Items.Clear();
      TKFP.IO.DirectoryInfo[] directories = this.CurrentDirectory.GetDirectories();
      for (int i = 0; i < directories.Length; i++)
      {
        TKFP.IO.DirectoryInfo directory = directories[i];
        DirectoryItem item = new FTPClientPanel.DirectoryItem(directory);
        item.ToolTipText = directory.FullName;
        //this.lviFolder.Items.Add(new FTPClientPanel.DirectoryItem(directory));
        this.lviFolder.Items.Add(item);
      }

      TKFP.IO.FileInfo[] files = this.CurrentDirectory.GetFiles();
      for (int j = 0; j < files.Length; j++)
      {
        TKFP.IO.FileInfo file = files[j];
        FileItem item = new FileItem(file);
        item.ToolTipText = file.FullName;
        //this.lviFolder.Items.Add(new FTPClientPanel.FileItem(file));
        this.lviFolder.Items.Add(item);
      }
      if (this.ファイルエクスプローラとシンクロToolStripMenuItem.Checked == true)
      {
        this.syncronizeFileExplorer(false);
      }
    }

    private Size GetSmallIconSize()
    {
      Size smallIconSize = SystemInformation.SmallIconSize;
      if (smallIconSize.Width > 16)
      {
        return new Size(18, 18);
      }
      return smallIconSize;
    }

    private void syncronizeFileExplorer(bool confirm)
    {
      //MessageBox.Show(this.serverDirectory);
      //MessageBox.Show(this.serverDocumentRoot);
      //string documentDir = this.serverDocumentRoot
      //    + this.CurrentDirectory.FullName.Replace(this.serverDirectory, "");

      string documentDir = ServerPath2DocumentPath(this.CurrentDirectory.FullName);

      //MessageBox.Show(documentDir);
      if (confirm)
      {
        //文字を右揃えにしてメッセージボックスを表示する
        DialogResult result = MessageBox.Show(documentDir,
          "ファイル エクスプローラ",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Exclamation,
          MessageBoxDefaultButton.Button1);
        /*
        if (result == DialogResult.Yes)
        {
          // TODO
          if (System.IO.Directory.Exists(documentDir))
          {
            PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.BrowseTo;" + documentDir);
          }
          else
          {
            MessageBox.Show(documentDir + " が存在しません");
            return;
          }
        }
        else return;
      */
      }
      else
      {
        /*
        if (System.IO.Directory.Exists(documentDir))
        {
          PluginBase.MainForm.CallCommand("PluginCommand", "FileExplorer.BrowseTo;" + documentDir);
          //Globals.MainForm.CallCommand("PluginCommand", "ResultsPanel.ShowResults");
        }
        else
        {
          //MessageBox.Show(documentDir + " が存在しません");
          return;
        }
        */
      }
    }

    public string ServerPath2DocumentPath(string serverPath)
    {
      string documentPath = this.serverDocumentRoot + serverPath.Replace(this.serverDirectory, "");
      return documentPath;
    }

    public string DocumentPath2ServerPath(string documentPath)
    {
      string serverPath = this.serverDirectory + documentPath.Replace("\\", "/").Replace(this.serverDocumentRoot, "");
      return serverPath;
    }

    #endregion

    private void serverToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ServerModel sender_server = ((ToolStripMenuItem)sender).Tag as ServerModel;
      this.userID = sender_server.UserID;
      this.password = sender_server.Password;
      this.serverAddress = sender_server.ServerAddress;
      this.serverName = sender_server.Name;
      this.serverUrl = sender_server.ServerUrl;
      this.serverDirectory = sender_server.ServerDirectory;
      this.serverDocumentRoot = sender_server.DocumentRoot;
      this.FTPConnect();
    }

    private void lacoocanToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.userID = "kahata.travel.coocan.jp";
      this.password = "QE5Xq7pT";
      this.serverAddress = "ftp.travel.coocan.jp";
      this.serverUrl = "http://kahata.travel.coocan.jp";
      this.serverDirectory = "kahata.travel.coocan.jp/homepage/";
      this.FTPConnect();
    }

    private void niftyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.userID = "ha745445";
      this.password = "T45KGZC2";
      this.serverAddress = "ftp15.nifty.com";
      this.serverUrl = "http://homepage2.nifty.com/kahata";
      this.serverDirectory = "/";
      this.FTPConnect();
    }

    private void nextFTPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //Process.Start("C:\\Program Files\\NextFTP\\NEXTFTP.EXE");
      Process.Start(this.nextFTPPath);
    }

    public void disconnectButton_Click(object sender, EventArgs e)
    {
      if (this.FtpClient == null)
      {
        return;
      }
      this.FtpClient.Close();
      this.FtpClient = null;
      this.lviFolder.Items.Clear();
      this.userID = String.Empty;
      this.password = String.Empty;
      this.serverAddress = String.Empty;
      this.serverUrl = String.Empty;
      this.serverDirectory = String.Empty;
      this.currentFilePath = String.Empty;

     // FIXME
      // ((Form1)this.antPanel.Tag).toolStripStatusLabel1.Text = "切断";

    }

    private void parentDirButton_Click(object sender, EventArgs e)
    {
      //MessageBox.Show("FTPClient null");
      if (this.FtpClient == null)
      {
        return;
      }
      this.CurrentDirectory = this.CurrentDirectory.Parent;
      this.ShowFolder();
    }

    private void downLoadButton_Click(object sender, EventArgs e)
    {
      if (this.FtpClient == null || this.lviFolder.SelectedItems.Count == 0) return;
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      if (listViewItem is FTPClientPanel.FileItem)
      {
        TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
        //kahata.travel.coocan,jp/homepage/picture/Car/car006.jpg
        string documentDir = this.serverDocumentRoot
            + this.CurrentDirectory.FullName.Replace(this.serverDirectory, "");
        this.saveFileDialog1.FileName = file.Name;
        this.saveFileDialog1.InitialDirectory = documentDir;
        DialogResult dialogResult = this.saveFileDialog1.ShowDialog();
        if (dialogResult == DialogResult.OK)
        {
          file.CopyTo(this.saveFileDialog1.FileName, true);
          this.currentDownLoadedFile = this.saveFileDialog1.FileName;
          DialogResult dialogResult2 = MessageBox.Show("ダウンロードしたファイルを開きますか？",
            "ファイルを開く", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
          //TODO: ファイルを開く処理
          if (dialogResult2 == DialogResult.Yes)
          {
            this.antPanel.OpenDocument(this.currentDownLoadedFile);
          }
        }
      }
    }

    private void upLoadButton_Click(object sender, EventArgs e)
    {
      if (this.FtpClient == null) return;
      String serverPath = this.GetSelectedServerPath();
      //MessageBox.Show(serverPath, "serverPath");
      String documentPath = this.serverDocumentRoot + serverPath.Replace(this.serverDirectory, "");

      //if (File.Exists(this.currentDownLoadedFile))
      if (File.Exists(documentPath))
      {
        this.openFileDialog1.FileName = System.IO.Path.GetFileName(documentPath);
        this.openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(documentPath);
      }
      else
      {
        this.openFileDialog1.InitialDirectory = this.serverDocumentRoot;// System.IO.Path.Combine(PathHelper.BaseDir, "DownLoadedFiles");
      }
      DialogResult dialogResult = this.openFileDialog1.ShowDialog();
      if (dialogResult != DialogResult.OK) return;
      System.IO.FileInfo fileInfo = new System.IO.FileInfo(this.openFileDialog1.FileName);
      string fileName = this.CurrentDirectory.FullName + fileInfo.Name;
      TKFP.IO.FileInfo fileInfo2 = new TKFP.IO.FileInfo(this.CurrentDirectory.FtpClient, fileName);
      //MessageBox.Show(fileInfo2.FullName);
      //MessageBox.Show(DocumentPath2ServerPath(this.openFileDialog1.FileName));
      DialogResult dialogResult2 = MessageBox.Show("from: " + this.openFileDialog1.FileName + "\n"
        + "To:   " + fileInfo2.FullName,
        "アップロード", MessageBoxButtons.OKCancel,
        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
      if (dialogResult2 == DialogResult.OK)
      {
        fileInfo2.ReadFrom(fileInfo.FullName);
      }
      this.CurrentDirectory.Refresh();
      this.ShowFolder();
    }

    private void ヘルプLToolStripButton_Click(object sender, EventArgs e)
    {
      string str = "http://uwa.potetihouse.com/library/tkfpdll.html";
     //PluginBase.MainForm.CallCommand("Browse", str);
      this.antPanel.OpenDocument(str);
    }

    private void lviFolder_DoubleClick(object sender, EventArgs e)
    {
      if (this.lviFolder.SelectedItems.Count == 0)
      {
        return;
      }
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      if (listViewItem is FTPClientPanel.DirectoryItem)
      {
        this.CurrentDirectory = ((FTPClientPanel.DirectoryItem)listViewItem).Directory;
        this.ShowFolder();
      }
      else if (listViewItem is FTPClientPanel.FileItem)
      {
        TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
        this.toolStripStatusLabel1.Text = file.FullName;
        this.currentFilePath = file.FullName;
        String url = this.serverUrl + file.FullName.Replace(this.serverDirectory, "");
        //文字を右揃えにしてメッセージボックスを表示する
        DialogResult result = MessageBox.Show(url + "\nWebページを開きますか？",
            "質問",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Exclamation,
            MessageBoxDefaultButton.Button1);
        if (result == DialogResult.Yes)
        {
          //PluginBase.MainForm.CallCommand("Browse", url);
          this.antPanel.OpenDocument(url);
        }
      }
    }

    private void FtpClient_MessageReceive(object sender, MessageArgs e)
    {
      //TODO
      //TraceManager.Add(e.Message, -1);
    }

    private void FtpClient_MessageSend(object sender, MessageArgs e)
    {
      // TODO
      //TraceManager.Add(e.Message, 3);
    }

    private void FtpClient_CertificateValidation(object sender, CertificateValidationArgs e)
    {
      e.Cancel = false;
    }

    private void txtDirectory_TextChanged(object sender, EventArgs e)
    {
    }

    private void lviFolder_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.FtpClient == null)
      {
        return;
      }
      if (this.lviFolder.SelectedItems.Count == 0)
      {
        return;
      }
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      if (listViewItem is FTPClientPanel.FileItem)
      {
        TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
        this.toolStripStatusLabel1.Text = file.FullName;
        this.currentFilePath = file.FullName;
        //MessageBox.Show(file.FullName);
      }
    }

    private void lviFolder_Click(object sender, EventArgs e)
    {
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      if (listViewItem is FTPClientPanel.DirectoryItem)
      {
        TKFP.IO.DirectoryInfo dir = ((FTPClientPanel.DirectoryItem)listViewItem).Directory;
        this.propertyGrid1.SelectedObject = dir;
      }
      else if (listViewItem is FTPClientPanel.FileItem)
      {
        TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
        this.toolStripStatusLabel1.Text = file.FullName;
        this.currentFilePath = file.FullName;
        String url = this.serverUrl + file.FullName.Replace(this.serverDirectory, "");
        this.propertyGrid1.SelectedObject = file;
      }
    }

    private void syncronizeButton_Click(object sender, EventArgs e)
    {
      this.syncronizeFileExplorer(true);
    }

    private void reloadButton_Click(object sender, EventArgs e)
    {
      this.ShowFolder();
    }

    private void 表示切替ボタンToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.表示ToolStripButton.Visible = 表示切替ボタンToolStripMenuItem.Checked;
    }

    private void ダウンロードボタンToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.downLoadButton.Visible = ダウンロードボタンToolStripMenuItem.Checked;

    }

    private void アップロードボタンToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.upLoadButton.Visible = アップロードボタンToolStripMenuItem.Checked;

    }

    private void imageListButtonToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    #region ContextMenu Click Handler
    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      if (listViewItem is FTPClientPanel.DirectoryItem)
      {
        TKFP.IO.DirectoryInfo dir = ((FTPClientPanel.DirectoryItem)listViewItem).Directory;
        //MessageBox.Show(((FTPClientPanel.DirectoryItem)listViewItem).Directory.FullName);
        this.ダウンロードToolStripMenuItem.Enabled = false;
        this.アップロードToolStripMenuItem.Enabled = false;
        this.開くOToolStripMenuItem.Enabled = false;
        this.webViewToolStripMenuItem.Enabled = false;
        this.SimpleViewerToolStripMenuItem.Enabled = false;
        this.editAreaToolStripMenuItem.Enabled = false;
      }
      else if (listViewItem is FTPClientPanel.FileItem)
      {
        TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
        //MessageBox.Show(file.FullName);
        //MessageBox.Show(file.LastWriteTime.ToString());
        this.ダウンロードToolStripMenuItem.Enabled = true;
        this.アップロードToolStripMenuItem.Enabled = true;
        this.開くOToolStripMenuItem.Enabled = true;
        this.webViewToolStripMenuItem.Enabled = true;
        this.SimpleViewerToolStripMenuItem.Enabled = this.editAreaToolStripMenuItem.Enabled
          = this.serverName == "旅行空間" ? true : false;
      }
    }

    private void コマンドプロンプトCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      if (listViewItem is FTPClientPanel.DirectoryItem)
      {
        MessageBox.Show(((FTPClientPanel.DirectoryItem)listViewItem).Directory.FullName);
        //this.CurrentDirectory = ((FTPClientPanel.DirectoryItem)listViewItem).Directory;
        //this.ShowFolder();
      }
      else if (listViewItem is FTPClientPanel.FileItem)
      {
        TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
        this.toolStripStatusLabel1.Text = file.FullName;
        this.currentFilePath = file.FullName;
        String url = this.serverUrl + file.FullName.Replace(this.serverDirectory, "");
        MessageBox.Show(file.FullName);
        MessageBox.Show(file.LastWriteTime.ToString());

        //文字を右揃えにしてメッセージボックスを表示する
        /*
        DialogResult result = MessageBox.Show(url + "\nWebページを開きますか？",
          "質問",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Exclamation,
          MessageBoxDefaultButton.Button1);
        if (result == DialogResult.Yes)
        {
          //PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseEx;" + url);
        }
      */
      }
    }

    private void 再読み込みToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.reloadButton.PerformClick();
    }

    private void ファイルエクスプローラとシンクロToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.syncronizeButton.PerformClick();
    }

    private void webViewToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      if (listViewItem is FTPClientPanel.FileItem)
      {
        TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
        this.toolStripStatusLabel1.Text = file.FullName;
        this.currentFilePath = file.FullName;
        String url = this.serverUrl + file.FullName.Replace(this.serverDirectory, "");
        //文字を右揃えにしてメッセージボックスを表示する
        /*
        DialogResult result = MessageBox.Show(url + "\nWebページを開きますか？",
            "質問",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Exclamation,
            MessageBoxDefaultButton.Button1);
        */
        //PluginBase.MainForm.CallCommand("Browse", url);
        this.antPanel.OpenDocument(url);
      }

    }

    private void SimpleViewerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
      //this.toolStripStatusLabel1.Text = file.FullName;
      //this.currentFilePath = file.FullName;
      String url2 = this.serverUrl + file.FullName.Replace(this.serverDirectory, "");
      string url1 = "http://kahata.travel.coocan.jp/FrameWork/Dirlister/SimpleViewer.class.php?path=";
      string url = url1 + url2 + "&main = true";
      //http://kahata.travel.coocan.jp/FrameWork/Dirlister/SimpleViewer.class.php?path=http://kahata.travel.coocan.jp//WisdomSoft/programming/cs/cs011/cs011_4/main.cs&main=true
      MessageBox.Show(url);
      //PluginBase.MainForm.CallCommand("PluginCommand", "XMLTreeMenu.BrowseEx;" + url);
      //PluginBase.MainForm.CallCommand("Browse", url);
      this.antPanel.OpenDocument(url);
    }

    private void editAreaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
      string url1 = "http://kahata.travel.coocan.jp/FrameWork/editarea_0_8_2/exemples/editor.php?path=";
      string url2 = "/homepage" + file.FullName.Replace(this.serverDirectory, "");
      string url = url1 + url2;
      //http://kahata.travel.coocan.jp/FrameWork/editarea_0_8_2/exemples/editor.php?path=/homepage/WisdomSoft/programming/cs/cs011/cs011_4/main.cs
      MessageBox.Show(url);
      //PluginBase.MainForm.CallCommand("Browse", url);
      this.antPanel.OpenDocument(url);
    }








    #endregion

    public string GetSelectedServerPath()
    {
      string filefullname = string.Empty;
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      if (listViewItem is FTPClientPanel.FileItem)
      {
        TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
        filefullname = file.FullName;
      }
      return filefullname;
    }

    public string GetSelectedServerUrl()
    {
      String serverPath = this.GetSelectedServerPath();
      return this.serverUrl + serverPath.Replace(this.serverDirectory, "");
    }

    public string GetSelectedDocumentPath()
    {
      String serverPath = this.GetSelectedServerPath();
      return this.serverDocumentRoot + serverPath.Replace(this.serverDirectory, "");
    }

    private void ドキュメントToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      if (listViewItem is FTPClientPanel.FileItem)
      {
        TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
        this.toolStripStatusLabel1.Text = file.FullName;
        this.currentFilePath = file.FullName;
        String url = this.serverUrl + file.FullName.Replace(this.serverDirectory, "");
        String path = this.serverDocumentRoot + file.FullName.Replace(this.serverDirectory, "");
        //PluginBase.MainForm.OpenEditableDocument(path);
        this.antPanel.OpenDocument(path);
      }
    }

    private void さくらエディタToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string path = this.GetSelectedDocumentPath();
      if (File.Exists(path))
      {
        Process.Start(this.settings.win_SakuraPath, path);
      }
    }

    private void システムToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string path = this.GetSelectedDocumentPath();
      if (File.Exists(path))
      {
        Process.Start(path);
      }
    }

    private void 同期フォルダーからToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.FtpClient == null) return;

      String serverPath = this.GetSelectedServerPath();
      String documentPath = this.serverDocumentRoot + serverPath.Replace(this.serverDirectory, "");
      System.IO.FileInfo fileInfo = new System.IO.FileInfo(documentPath);
      TKFP.IO.FileInfo fileInfo2 = new TKFP.IO.FileInfo(this.CurrentDirectory.FtpClient, serverPath);
      DialogResult dialogResult2 = MessageBox.Show("from: " + documentPath + "\n"
         + "To:   " + serverPath,
         "アップロード", MessageBoxButtons.OKCancel,
         MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
      if (dialogResult2 == DialogResult.OK)
      {
        fileInfo2.ReadFrom(fileInfo.FullName);
      }
      this.CurrentDirectory.Refresh();
      this.ShowFolder();
    }

    private void アップロードFROMYToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.upLoadButton_Click(sender, e);
    }

    private void 名前を変えてアップロードToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void ファイルエクスプローラとシンクロボタンToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.syncronizeButton.Visible = this.ファイルエクスプローラとシンクロボタンToolStripMenuItem.Checked;
    }

    private void 編集ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //PluginBase.MainForm.OpenEditableDocument(System.IO.Path.Combine(PathHelper.BaseDir, @"SettingData\server.xml"));
      this.antPanel.OpenDocument(System.IO.Path.Combine(BaseDir, @"SettingData\server.xml"));
    }

    private void 再読み込みToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.InitializeFTPClient();
    }



    private int toggleIndex = 1;
    private void 表示ToolStripButton_Click(object sender, EventArgs e)
    {
      int num = this.toggleIndex % 3;
      if (num == 0)
      {
        this.splitContainer1.Panel2Collapsed = true;
        this.splitContainer1.Panel1Collapsed = false;
        this.propertyGrid1.HelpVisible = false;
        this.propertyGrid1.ToolbarVisible = false;
      }
      else if (num == 1)
      {
        this.splitContainer1.Panel2Collapsed = false;
        this.splitContainer1.Panel1Collapsed = true;
        this.propertyGrid1.HelpVisible = true;
        this.propertyGrid1.ToolbarVisible = true;
      }
      else if (num == 2)
      {
        this.splitContainer1.Panel2Collapsed = false;
        this.splitContainer1.Panel1Collapsed = false;
        this.propertyGrid1.HelpVisible = false;
        this.propertyGrid1.ToolbarVisible = false;
      }
      this.toggleIndex++;
    }

    private void 同期フォルダーへToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.FtpClient == null || this.lviFolder.SelectedItems.Count == 0) return;
      ListViewItem listViewItem = this.lviFolder.SelectedItems[0];
      if (listViewItem is FTPClientPanel.FileItem)
      {
        TKFP.IO.FileInfo file = ((FTPClientPanel.FileItem)listViewItem).File;
        //kahata.travel.coocan,jp/homepage/picture/Car/car006.jpg
        string documentDir = this.serverDocumentRoot
            + this.CurrentDirectory.FullName.Replace(this.serverDirectory, "");

         String msg = "ダウンロード元:\n " + file.FullName + "\n"
          + "ダウンロード先:\n " + this.GetSelectedDocumentPath() + "\n"
          + "ダウンロードしますか？";
        DialogResult dialogResult = MessageBox.Show(msg,"ダウンロード", MessageBoxButtons.YesNo,
          MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        //TODO: ファイルを開く処理
        if (dialogResult == DialogResult.Yes)
        {
          file.CopyTo(GetSelectedDocumentPath(), true);
        }
        else return;

        DialogResult dialogResult2 = MessageBox.Show("ダウンロードしたファイルを開きますか？",
          "ファイルを開く", MessageBoxButtons.YesNo,
          MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        //TODO: ファイルを開く処理
        if (dialogResult2 == DialogResult.Yes)
        {
          this.antPanel.OpenDocument(this.GetSelectedDocumentPath());
        }
      }
    }

    private void ダウンロードTOWToolStripMenuItem_Click(object sender, EventArgs e)
    {
      downLoadButton_Click(sender, e);
    }

    private void richTextEditorToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void サクラエディタToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
    {

    }

    private void nextFTPToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      //MessageBox.Show(this.serverName); //旅行空間
      //"C:\Program Files\NextFTP\NEXTFTP.EXE" $Host14 
      // - dir = "/kahata.travel.coocan.jp/homepage/home/diary"
      // - local = "C:\Apache2.2\htdocs\home\diary"
      // - auto - options = 019953101000000100100 - timedef = 0
      // - upload = "diary2018" - minimize - quit
     String connectionKey = "$Host14";
      if(this.serverName == "旅行空間") connectionKey = "$Host14";
      else if (this.serverName == "ジオシティーズ") connectionKey = "$Host15";

      // 以下を指定すると自動アップロードが開始されるので外す
      //String connectionString = connectionKey 
      //+"\" - local = \"" + System.IO.Path.GetDirectoryName(this.GetSelectedDocumentPath()) + "\"";
     Process.Start(this.settings.win_NextFTPPath, connectionKey);
    }




    #region Form Variable
    public ToolStrip toolStrip1;
    public ToolStripDropDownButton connectDropDownButton;
    public ToolStripButton disconnectButton;
    public ToolStripButton parentDirButton;
    public ToolStripButton downLoadButton;
    public ToolStripButton upLoadButton;
    public ToolStripSeparator toolStripSeparator8;
    public ToolStripButton ヘルプLToolStripButton;
    public StatusStrip statusStrip1;
    public ToolStripStatusLabel toolStripStatusLabel1;
    public ListView lviFolder;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    public ToolStripMenuItem niftyToolStripMenuItem;
    public ToolStripMenuItem lacoocanToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    public ToolStripMenuItem nexrFTPToolStripMenuItem;
    public ImageList imageList1;
    public ImageList imageList3;
    private IContainer components;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem 編集ToolStripMenuItem;
    private ToolStripMenuItem 再読み込みToolStripMenuItem1;
    private ToolStripMenuItem オプションToolStripMenuItem;
    private ToolStripMenuItem ファイルエクスプローラとシンクロToolStripMenuItem;
    private ToolStripMenuItem アイコン表示ToolStripMenuItem;
    private ToolStripMenuItem ツールボタンToolStripMenuItem;
    private ToolStripMenuItem 表示切替ボタンToolStripMenuItem;
    private ToolStripMenuItem imageListButtonToolStripMenuItem;

    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem 再読み込みToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem 開くOToolStripMenuItem;
    private ToolStripMenuItem サクラエディタToolStripMenuItem;
    private ToolStripMenuItem pSPadToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem 名前の変更MToolStripMenuItem;
    private ToolStripMenuItem ToolStripMenuItemDelete;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripMenuItem SimpleViewerToolStripMenuItem;
    private ToolStripButton reloadButton;
    private ToolStripButton 表示ToolStripButton;
    private ToolStripDropDownButton ツールDropDownButton;
    private ToolStripButton imageListButton;
    private ToolStripMenuItem ファイルエクスプローラとシンクロToolStripMenuItem1;
    private ToolStripMenuItem webViewToolStripMenuItem;
    private ToolStripMenuItem editAreaToolStripMenuItem;
    private ToolStripMenuItem ダウンロードToolStripMenuItem;
    private ToolStripMenuItem 同期フォルダーへToolStripMenuItem;
    private ToolStripMenuItem ダウンロードTOWToolStripMenuItem;
    private ToolStripMenuItem 名前を変えてダウンロードToolStripMenuItem;
    private ToolStripMenuItem アップロードToolStripMenuItem;
    private ToolStripMenuItem 同期フォルダーからToolStripMenuItem;
    private ToolStripMenuItem アップロードFROMYToolStripMenuItem;
    private ToolStripMenuItem 名前を変えてアップロードToolStripMenuItem;
    private ToolStripMenuItem 新規ドキュメントToolStripMenuItem;
    private ToolStripMenuItem richTextEditorToolStripMenuItem;
    private ToolStripMenuItem azukiEditorToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripSeparator toolStripSeparator10;
    private ToolStripMenuItem コマンドプロンプトToolStripMenuItem;
    private ToolStripMenuItem エクスプローラToolStripMenuItem;
    private ToolStripMenuItem 新規フォルダー作成ToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator11;
    private ToolStripMenuItem 移動ToolStripMenuItem;
    private ToolStripMenuItem 容量の計算ToolStripMenuItem;
    private ToolStripMenuItem 検索ToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator12;
    private ToolStripMenuItem パーミッションの変更ToolStripMenuItem;


    #endregion

    #region Windows Forms Designer Generated Code

    /// <summary>
    /// This method is required for Windows Forms designer support.
    /// Do not change the method contents inside the source code editor. The Forms designer might
    /// not be able to load this method if it was changed manually.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPClientPanel));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.connectDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
      this.niftyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.lacoocanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.nexrFTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.disconnectButton = new System.Windows.Forms.ToolStripButton();
      this.parentDirButton = new System.Windows.Forms.ToolStripButton();
      this.downLoadButton = new System.Windows.Forms.ToolStripButton();
      this.upLoadButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.reloadButton = new System.Windows.Forms.ToolStripButton();
      this.表示ToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.syncronizeButton = new System.Windows.Forms.ToolStripButton();
      this.ツールDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.編集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.再読み込みToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.オプションToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ファイルエクスプローラとシンクロToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.アイコン表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ツールボタンToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.表示切替ボタンToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.ダウンロードボタンToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.アップロードボタンToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ファイルエクスプローラとシンクロボタンToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
      this.imageListButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ヘルプLToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.imageListButton = new System.Windows.Forms.ToolStripButton();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.lviFolder = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.再読み込みToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ファイルエクスプローラとシンクロToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.webViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.SimpleViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.ダウンロードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.同期フォルダーへToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ダウンロードTOWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.名前を変えてダウンロードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.アップロードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.同期フォルダーからToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.アップロードFROMYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.名前を変えてアップロードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.nextFTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.同期ファイルを開くToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.ドキュメントToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.さくらエディタToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.システムToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.新規ドキュメントToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.richTextEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.azukiEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.サクラエディタToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pSPadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.コマンドプロンプトToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.エクスプローラToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.新規フォルダー作成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.名前の変更MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.移動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.容量の計算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.検索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.パーミッションの変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.imageList3 = new System.Windows.Forms.ImageList(this.components);
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
      this.toolStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectDropDownButton,
            this.disconnectButton,
            this.parentDirButton,
            this.downLoadButton,
            this.upLoadButton,
            this.toolStripSeparator8,
            this.reloadButton,
            this.表示ToolStripButton,
            this.syncronizeButton,
            this.ツールDropDownButton,
            this.ヘルプLToolStripButton,
            this.imageListButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(379, 27);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // connectDropDownButton
      // 
      this.connectDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.connectDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.niftyToolStripMenuItem,
            this.lacoocanToolStripMenuItem,
            this.toolStripSeparator1,
            this.nexrFTPToolStripMenuItem});
      this.connectDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("connectDropDownButton.Image")));
      this.connectDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.connectDropDownButton.Name = "connectDropDownButton";
      this.connectDropDownButton.Size = new System.Drawing.Size(34, 24);
      this.connectDropDownButton.Text = "toolStripDropDownButton2";
      this.connectDropDownButton.ToolTipText = "接続";
      // 
      // niftyToolStripMenuItem
      // 
      this.niftyToolStripMenuItem.Name = "niftyToolStripMenuItem";
      this.niftyToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
      this.niftyToolStripMenuItem.Text = "@nifty";
      this.niftyToolStripMenuItem.Click += new System.EventHandler(this.niftyToolStripMenuItem_Click);
      // 
      // lacoocanToolStripMenuItem
      // 
      this.lacoocanToolStripMenuItem.Name = "lacoocanToolStripMenuItem";
      this.lacoocanToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
      this.lacoocanToolStripMenuItem.Text = "la.coocan";
      this.lacoocanToolStripMenuItem.Click += new System.EventHandler(this.lacoocanToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
      // 
      // nexrFTPToolStripMenuItem
      // 
      this.nexrFTPToolStripMenuItem.Name = "nexrFTPToolStripMenuItem";
      this.nexrFTPToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
      this.nexrFTPToolStripMenuItem.Text = "NexrFTP";
      this.nexrFTPToolStripMenuItem.Click += new System.EventHandler(this.nextFTPToolStripMenuItem_Click);
      // 
      // disconnectButton
      // 
      this.disconnectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.disconnectButton.Image = ((System.Drawing.Image)(resources.GetObject("disconnectButton.Image")));
      this.disconnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.disconnectButton.Name = "disconnectButton";
      this.disconnectButton.Size = new System.Drawing.Size(24, 24);
      this.disconnectButton.Text = "toolStripButton1";
      this.disconnectButton.ToolTipText = "切断";
      this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
      // 
      // parentDirButton
      // 
      this.parentDirButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.parentDirButton.Image = ((System.Drawing.Image)(resources.GetObject("parentDirButton.Image")));
      this.parentDirButton.Name = "parentDirButton";
      this.parentDirButton.Size = new System.Drawing.Size(24, 24);
      this.parentDirButton.Text = "toolStripButton2";
      this.parentDirButton.ToolTipText = "親ディレクトリ";
      this.parentDirButton.Click += new System.EventHandler(this.parentDirButton_Click);
      // 
      // downLoadButton
      // 
      this.downLoadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.downLoadButton.Image = ((System.Drawing.Image)(resources.GetObject("downLoadButton.Image")));
      this.downLoadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.downLoadButton.Name = "downLoadButton";
      this.downLoadButton.Size = new System.Drawing.Size(24, 24);
      this.downLoadButton.Text = "toolStripButton3";
      this.downLoadButton.ToolTipText = "ダウンロード";
      this.downLoadButton.Visible = false;
      this.downLoadButton.Click += new System.EventHandler(this.downLoadButton_Click);
      // 
      // upLoadButton
      // 
      this.upLoadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.upLoadButton.Image = ((System.Drawing.Image)(resources.GetObject("upLoadButton.Image")));
      this.upLoadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.upLoadButton.Name = "upLoadButton";
      this.upLoadButton.Size = new System.Drawing.Size(24, 24);
      this.upLoadButton.Text = "toolStripButton4";
      this.upLoadButton.ToolTipText = "アップロード";
      this.upLoadButton.Visible = false;
      this.upLoadButton.Click += new System.EventHandler(this.upLoadButton_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(6, 27);
      // 
      // reloadButton
      // 
      this.reloadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.reloadButton.Image = ((System.Drawing.Image)(resources.GetObject("reloadButton.Image")));
      this.reloadButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.reloadButton.Name = "reloadButton";
      this.reloadButton.Size = new System.Drawing.Size(24, 24);
      this.reloadButton.Text = "toolStripButton1";
      this.reloadButton.ToolTipText = "再読み込み";
      this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
      // 
      // 表示ToolStripButton
      // 
      this.表示ToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.表示ToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("表示ToolStripButton.Image")));
      this.表示ToolStripButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.表示ToolStripButton.Name = "表示ToolStripButton";
      this.表示ToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.表示ToolStripButton.Text = "表示切替";
      this.表示ToolStripButton.Click += new System.EventHandler(this.表示ToolStripButton_Click);
      // 
      // syncronizeButton
      // 
      this.syncronizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.syncronizeButton.Image = ((System.Drawing.Image)(resources.GetObject("syncronizeButton.Image")));
      this.syncronizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.syncronizeButton.Name = "syncronizeButton";
      this.syncronizeButton.Size = new System.Drawing.Size(24, 24);
      this.syncronizeButton.Text = "ファイルエクスプローラとシンクロ";
      this.syncronizeButton.Visible = false;
      this.syncronizeButton.Click += new System.EventHandler(this.syncronizeButton_Click);
      // 
      // ツールDropDownButton
      // 
      this.ツールDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ツールDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.オプションToolStripMenuItem});
      this.ツールDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("ツールDropDownButton.Image")));
      this.ツールDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ツールDropDownButton.Name = "ツールDropDownButton";
      this.ツールDropDownButton.Size = new System.Drawing.Size(34, 24);
      this.ツールDropDownButton.Text = "ツール";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.編集ToolStripMenuItem,
            this.再読み込みToolStripMenuItem1});
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 26);
      this.toolStripMenuItem1.Text = "サーバー設定";
      // 
      // 編集ToolStripMenuItem
      // 
      this.編集ToolStripMenuItem.Name = "編集ToolStripMenuItem";
      this.編集ToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
      this.編集ToolStripMenuItem.Text = "編集";
      this.編集ToolStripMenuItem.Click += new System.EventHandler(this.編集ToolStripMenuItem_Click);
      // 
      // 再読み込みToolStripMenuItem1
      // 
      this.再読み込みToolStripMenuItem1.Name = "再読み込みToolStripMenuItem1";
      this.再読み込みToolStripMenuItem1.Size = new System.Drawing.Size(155, 26);
      this.再読み込みToolStripMenuItem1.Text = "再読み込み";
      this.再読み込みToolStripMenuItem1.Click += new System.EventHandler(this.再読み込みToolStripMenuItem1_Click);
      // 
      // オプションToolStripMenuItem
      // 
      this.オプションToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルエクスプローラとシンクロToolStripMenuItem,
            this.アイコン表示ToolStripMenuItem,
            this.ツールボタンToolStripMenuItem});
      this.オプションToolStripMenuItem.Name = "オプションToolStripMenuItem";
      this.オプションToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
      this.オプションToolStripMenuItem.Text = "オプション";
      // 
      // ファイルエクスプローラとシンクロToolStripMenuItem
      // 
      this.ファイルエクスプローラとシンクロToolStripMenuItem.Checked = true;
      this.ファイルエクスプローラとシンクロToolStripMenuItem.CheckOnClick = true;
      this.ファイルエクスプローラとシンクロToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ファイルエクスプローラとシンクロToolStripMenuItem.Name = "ファイルエクスプローラとシンクロToolStripMenuItem";
      this.ファイルエクスプローラとシンクロToolStripMenuItem.RightToLeftAutoMirrorImage = true;
      this.ファイルエクスプローラとシンクロToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
      this.ファイルエクスプローラとシンクロToolStripMenuItem.Text = "ファイルエクスプローラとシンクロ";
      this.ファイルエクスプローラとシンクロToolStripMenuItem.ToolTipText = "ファイルエクスプローラとシンクロ";
      // 
      // アイコン表示ToolStripMenuItem
      // 
      this.アイコン表示ToolStripMenuItem.Checked = true;
      this.アイコン表示ToolStripMenuItem.CheckOnClick = true;
      this.アイコン表示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.アイコン表示ToolStripMenuItem.Name = "アイコン表示ToolStripMenuItem";
      this.アイコン表示ToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
      this.アイコン表示ToolStripMenuItem.Text = "アイコン表示";
      // 
      // ツールボタンToolStripMenuItem
      // 
      this.ツールボタンToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.表示切替ボタンToolStripMenuItem,
            this.toolStripSeparator13,
            this.ダウンロードボタンToolStripMenuItem,
            this.アップロードボタンToolStripMenuItem,
            this.ファイルエクスプローラとシンクロボタンToolStripMenuItem,
            this.toolStripSeparator14,
            this.imageListButtonToolStripMenuItem});
      this.ツールボタンToolStripMenuItem.Name = "ツールボタンToolStripMenuItem";
      this.ツールボタンToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
      this.ツールボタンToolStripMenuItem.Text = "ツールボタン";
      // 
      // 表示切替ボタンToolStripMenuItem
      // 
      this.表示切替ボタンToolStripMenuItem.Checked = true;
      this.表示切替ボタンToolStripMenuItem.CheckOnClick = true;
      this.表示切替ボタンToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.表示切替ボタンToolStripMenuItem.Name = "表示切替ボタンToolStripMenuItem";
      this.表示切替ボタンToolStripMenuItem.Size = new System.Drawing.Size(288, 26);
      this.表示切替ボタンToolStripMenuItem.Text = "表示切替ボタン";
      this.表示切替ボタンToolStripMenuItem.Click += new System.EventHandler(this.表示切替ボタンToolStripMenuItem_Click);
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new System.Drawing.Size(285, 6);
      // 
      // ダウンロードボタンToolStripMenuItem
      // 
      this.ダウンロードボタンToolStripMenuItem.CheckOnClick = true;
      this.ダウンロードボタンToolStripMenuItem.Name = "ダウンロードボタンToolStripMenuItem";
      this.ダウンロードボタンToolStripMenuItem.Size = new System.Drawing.Size(288, 26);
      this.ダウンロードボタンToolStripMenuItem.Text = "ダウンロードボタン";
      this.ダウンロードボタンToolStripMenuItem.Click += new System.EventHandler(this.ダウンロードボタンToolStripMenuItem_Click);
      // 
      // アップロードボタンToolStripMenuItem
      // 
      this.アップロードボタンToolStripMenuItem.CheckOnClick = true;
      this.アップロードボタンToolStripMenuItem.Name = "アップロードボタンToolStripMenuItem";
      this.アップロードボタンToolStripMenuItem.Size = new System.Drawing.Size(288, 26);
      this.アップロードボタンToolStripMenuItem.Text = "アップロードボタン";
      this.アップロードボタンToolStripMenuItem.Click += new System.EventHandler(this.アップロードボタンToolStripMenuItem_Click);
      // 
      // ファイルエクスプローラとシンクロボタンToolStripMenuItem
      // 
      this.ファイルエクスプローラとシンクロボタンToolStripMenuItem.CheckOnClick = true;
      this.ファイルエクスプローラとシンクロボタンToolStripMenuItem.Name = "ファイルエクスプローラとシンクロボタンToolStripMenuItem";
      this.ファイルエクスプローラとシンクロボタンToolStripMenuItem.Size = new System.Drawing.Size(288, 26);
      this.ファイルエクスプローラとシンクロボタンToolStripMenuItem.Text = "ファイルエクスプローラとシンクロボタン";
      this.ファイルエクスプローラとシンクロボタンToolStripMenuItem.Click += new System.EventHandler(this.ファイルエクスプローラとシンクロボタンToolStripMenuItem_Click);
      // 
      // toolStripSeparator14
      // 
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      this.toolStripSeparator14.Size = new System.Drawing.Size(285, 6);
      // 
      // imageListButtonToolStripMenuItem
      // 
      this.imageListButtonToolStripMenuItem.CheckOnClick = true;
      this.imageListButtonToolStripMenuItem.Name = "imageListButtonToolStripMenuItem";
      this.imageListButtonToolStripMenuItem.Size = new System.Drawing.Size(288, 26);
      this.imageListButtonToolStripMenuItem.Text = "ImageListButton";
      this.imageListButtonToolStripMenuItem.Click += new System.EventHandler(this.imageListButtonToolStripMenuItem_Click);
      // 
      // ヘルプLToolStripButton
      // 
      this.ヘルプLToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ヘルプLToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ヘルプLToolStripButton.Image")));
      this.ヘルプLToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ヘルプLToolStripButton.Name = "ヘルプLToolStripButton";
      this.ヘルプLToolStripButton.Size = new System.Drawing.Size(24, 24);
      this.ヘルプLToolStripButton.Text = "ヘルプ(&L)";
      this.ヘルプLToolStripButton.Click += new System.EventHandler(this.ヘルプLToolStripButton_Click);
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
      // statusStrip1
      // 
      this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
      this.statusStrip1.Location = new System.Drawing.Point(0, 367);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(379, 24);
      this.statusStrip1.TabIndex = 25;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(167, 19);
      this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
      // 
      // lviFolder
      // 
      this.lviFolder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
      this.lviFolder.ContextMenuStrip = this.contextMenuStrip1;
      this.lviFolder.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lviFolder.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.lviFolder.FullRowSelect = true;
      this.lviFolder.Location = new System.Drawing.Point(0, 0);
      this.lviFolder.MultiSelect = false;
      this.lviFolder.Name = "lviFolder";
      this.lviFolder.Size = new System.Drawing.Size(379, 210);
      this.lviFolder.SmallImageList = this.imageList3;
      this.lviFolder.TabIndex = 23;
      this.lviFolder.UseCompatibleStateImageBehavior = false;
      this.lviFolder.View = System.Windows.Forms.View.Details;
      this.lviFolder.SelectedIndexChanged += new System.EventHandler(this.lviFolder_SelectedIndexChanged);
      this.lviFolder.Click += new System.EventHandler(this.lviFolder_Click);
      this.lviFolder.DoubleClick += new System.EventHandler(this.lviFolder_DoubleClick);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "名前";
      this.columnHeader1.Width = 240;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "サイズ";
      this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "属性";
      this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "更新日時";
      this.columnHeader4.Width = 120;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.再読み込みToolStripMenuItem,
            this.ファイルエクスプローラとシンクロToolStripMenuItem1,
            this.toolStripSeparator2,
            this.webViewToolStripMenuItem,
            this.SimpleViewerToolStripMenuItem,
            this.editAreaToolStripMenuItem,
            this.toolStripSeparator3,
            this.ダウンロードToolStripMenuItem,
            this.アップロードToolStripMenuItem,
            this.toolStripSeparator4,
            this.nextFTPToolStripMenuItem,
            this.同期ファイルを開くToolStripMenuItem1,
            this.開くOToolStripMenuItem,
            this.toolStripSeparator5,
            this.新規フォルダー作成ToolStripMenuItem,
            this.toolStripSeparator11,
            this.ToolStripMenuItemDelete,
            this.名前の変更MToolStripMenuItem,
            this.移動ToolStripMenuItem,
            this.toolStripSeparator6,
            this.容量の計算ToolStripMenuItem,
            this.検索ToolStripMenuItem,
            this.toolStripSeparator12,
            this.パーミッションの変更ToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(274, 454);
      this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
      // 
      // 再読み込みToolStripMenuItem
      // 
      this.再読み込みToolStripMenuItem.Name = "再読み込みToolStripMenuItem";
      this.再読み込みToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.再読み込みToolStripMenuItem.Text = "再読み込み(&R)";
      this.再読み込みToolStripMenuItem.Click += new System.EventHandler(this.再読み込みToolStripMenuItem_Click);
      // 
      // ファイルエクスプローラとシンクロToolStripMenuItem1
      // 
      this.ファイルエクスプローラとシンクロToolStripMenuItem1.Name = "ファイルエクスプローラとシンクロToolStripMenuItem1";
      this.ファイルエクスプローラとシンクロToolStripMenuItem1.Size = new System.Drawing.Size(273, 24);
      this.ファイルエクスプローラとシンクロToolStripMenuItem1.Text = "ファイルエクスプローラとシンクロ(&S)";
      this.ファイルエクスプローラとシンクロToolStripMenuItem1.Click += new System.EventHandler(this.ファイルエクスプローラとシンクロToolStripMenuItem1_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(270, 6);
      // 
      // webViewToolStripMenuItem
      // 
      this.webViewToolStripMenuItem.Name = "webViewToolStripMenuItem";
      this.webViewToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.webViewToolStripMenuItem.Text = "Web View";
      this.webViewToolStripMenuItem.Click += new System.EventHandler(this.webViewToolStripMenuItem_Click);
      // 
      // SimpleViewerToolStripMenuItem
      // 
      this.SimpleViewerToolStripMenuItem.Name = "SimpleViewerToolStripMenuItem";
      this.SimpleViewerToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.SimpleViewerToolStripMenuItem.Text = "SimpleViewer";
      this.SimpleViewerToolStripMenuItem.Click += new System.EventHandler(this.SimpleViewerToolStripMenuItem_Click);
      // 
      // editAreaToolStripMenuItem
      // 
      this.editAreaToolStripMenuItem.Name = "editAreaToolStripMenuItem";
      this.editAreaToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.editAreaToolStripMenuItem.Text = "Edit Area";
      this.editAreaToolStripMenuItem.Click += new System.EventHandler(this.editAreaToolStripMenuItem_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(270, 6);
      // 
      // ダウンロードToolStripMenuItem
      // 
      this.ダウンロードToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.同期フォルダーへToolStripMenuItem,
            this.ダウンロードTOWToolStripMenuItem,
            this.名前を変えてダウンロードToolStripMenuItem});
      this.ダウンロードToolStripMenuItem.Name = "ダウンロードToolStripMenuItem";
      this.ダウンロードToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.ダウンロードToolStripMenuItem.Text = "ダウンロード...";
      // 
      // 同期フォルダーへToolStripMenuItem
      // 
      this.同期フォルダーへToolStripMenuItem.Name = "同期フォルダーへToolStripMenuItem";
      this.同期フォルダーへToolStripMenuItem.Size = new System.Drawing.Size(246, 26);
      this.同期フォルダーへToolStripMenuItem.Text = "同期フォルダーへ...";
      this.同期フォルダーへToolStripMenuItem.Click += new System.EventHandler(this.同期フォルダーへToolStripMenuItem_Click);
      // 
      // ダウンロードTOWToolStripMenuItem
      // 
      this.ダウンロードTOWToolStripMenuItem.Name = "ダウンロードTOWToolStripMenuItem";
      this.ダウンロードTOWToolStripMenuItem.Size = new System.Drawing.Size(246, 26);
      this.ダウンロードTOWToolStripMenuItem.Text = "ダウンロード TO(&W)";
      this.ダウンロードTOWToolStripMenuItem.Click += new System.EventHandler(this.ダウンロードTOWToolStripMenuItem_Click);
      // 
      // 名前を変えてダウンロードToolStripMenuItem
      // 
      this.名前を変えてダウンロードToolStripMenuItem.Enabled = false;
      this.名前を変えてダウンロードToolStripMenuItem.Name = "名前を変えてダウンロードToolStripMenuItem";
      this.名前を変えてダウンロードToolStripMenuItem.Size = new System.Drawing.Size(246, 26);
      this.名前を変えてダウンロードToolStripMenuItem.Text = "名前を変えてダウンロード...";
      // 
      // アップロードToolStripMenuItem
      // 
      this.アップロードToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.同期フォルダーからToolStripMenuItem,
            this.アップロードFROMYToolStripMenuItem,
            this.名前を変えてアップロードToolStripMenuItem});
      this.アップロードToolStripMenuItem.Name = "アップロードToolStripMenuItem";
      this.アップロードToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.アップロードToolStripMenuItem.Text = "アップロード...";
      // 
      // 同期フォルダーからToolStripMenuItem
      // 
      this.同期フォルダーからToolStripMenuItem.Name = "同期フォルダーからToolStripMenuItem";
      this.同期フォルダーからToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
      this.同期フォルダーからToolStripMenuItem.Text = "同期フォルダーから...";
      this.同期フォルダーからToolStripMenuItem.Click += new System.EventHandler(this.同期フォルダーからToolStripMenuItem_Click);
      // 
      // アップロードFROMYToolStripMenuItem
      // 
      this.アップロードFROMYToolStripMenuItem.Name = "アップロードFROMYToolStripMenuItem";
      this.アップロードFROMYToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
      this.アップロードFROMYToolStripMenuItem.Text = "アップロード FROM(&Y)";
      this.アップロードFROMYToolStripMenuItem.Click += new System.EventHandler(this.アップロードFROMYToolStripMenuItem_Click);
      // 
      // 名前を変えてアップロードToolStripMenuItem
      // 
      this.名前を変えてアップロードToolStripMenuItem.Enabled = false;
      this.名前を変えてアップロードToolStripMenuItem.Name = "名前を変えてアップロードToolStripMenuItem";
      this.名前を変えてアップロードToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
      this.名前を変えてアップロードToolStripMenuItem.Text = "名前を変えてアップロード...";
      this.名前を変えてアップロードToolStripMenuItem.Click += new System.EventHandler(this.名前を変えてアップロードToolStripMenuItem_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(270, 6);
      // 
      // nextFTPToolStripMenuItem
      // 
      this.nextFTPToolStripMenuItem.Name = "nextFTPToolStripMenuItem";
      this.nextFTPToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.nextFTPToolStripMenuItem.Text = "NextFTP";
      this.nextFTPToolStripMenuItem.Click += new System.EventHandler(this.nextFTPToolStripMenuItem_Click_1);
      // 
      // 同期ファイルを開くToolStripMenuItem1
      // 
      this.同期ファイルを開くToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ドキュメントToolStripMenuItem,
            this.さくらエディタToolStripMenuItem,
            this.システムToolStripMenuItem});
      this.同期ファイルを開くToolStripMenuItem1.Name = "同期ファイルを開くToolStripMenuItem1";
      this.同期ファイルを開くToolStripMenuItem1.Size = new System.Drawing.Size(273, 24);
      this.同期ファイルを開くToolStripMenuItem1.Text = "同期ファイルを開く";
      // 
      // ドキュメントToolStripMenuItem
      // 
      this.ドキュメントToolStripMenuItem.Name = "ドキュメントToolStripMenuItem";
      this.ドキュメントToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
      this.ドキュメントToolStripMenuItem.Text = "ドキュメント";
      this.ドキュメントToolStripMenuItem.Click += new System.EventHandler(this.ドキュメントToolStripMenuItem_Click);
      // 
      // さくらエディタToolStripMenuItem
      // 
      this.さくらエディタToolStripMenuItem.Name = "さくらエディタToolStripMenuItem";
      this.さくらエディタToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
      this.さくらエディタToolStripMenuItem.Text = "さくらエディタ";
      this.さくらエディタToolStripMenuItem.Click += new System.EventHandler(this.さくらエディタToolStripMenuItem_Click);
      // 
      // システムToolStripMenuItem
      // 
      this.システムToolStripMenuItem.Name = "システムToolStripMenuItem";
      this.システムToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
      this.システムToolStripMenuItem.Text = "システム";
      this.システムToolStripMenuItem.Click += new System.EventHandler(this.システムToolStripMenuItem_Click);
      // 
      // 開くOToolStripMenuItem
      // 
      this.開くOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規ドキュメントToolStripMenuItem,
            this.toolStripSeparator7,
            this.richTextEditorToolStripMenuItem,
            this.azukiEditorToolStripMenuItem,
            this.toolStripSeparator9,
            this.サクラエディタToolStripMenuItem,
            this.pSPadToolStripMenuItem,
            this.toolStripSeparator10,
            this.コマンドプロンプトToolStripMenuItem,
            this.エクスプローラToolStripMenuItem});
      this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
      this.開くOToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.開くOToolStripMenuItem.Text = "ダウンロードして開く(&O)";
      // 
      // 新規ドキュメントToolStripMenuItem
      // 
      this.新規ドキュメントToolStripMenuItem.Name = "新規ドキュメントToolStripMenuItem";
      this.新規ドキュメントToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.新規ドキュメントToolStripMenuItem.Text = "新規ドキュメント";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(186, 6);
      // 
      // richTextEditorToolStripMenuItem
      // 
      this.richTextEditorToolStripMenuItem.Enabled = false;
      this.richTextEditorToolStripMenuItem.Name = "richTextEditorToolStripMenuItem";
      this.richTextEditorToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.richTextEditorToolStripMenuItem.Text = "RichTextEditor";
      this.richTextEditorToolStripMenuItem.Click += new System.EventHandler(this.richTextEditorToolStripMenuItem_Click);
      // 
      // azukiEditorToolStripMenuItem
      // 
      this.azukiEditorToolStripMenuItem.Enabled = false;
      this.azukiEditorToolStripMenuItem.Name = "azukiEditorToolStripMenuItem";
      this.azukiEditorToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.azukiEditorToolStripMenuItem.Text = "AzukiEditor";
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(186, 6);
      // 
      // サクラエディタToolStripMenuItem
      // 
      this.サクラエディタToolStripMenuItem.Name = "サクラエディタToolStripMenuItem";
      this.サクラエディタToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.サクラエディタToolStripMenuItem.Text = "サクラエディタ";
      this.サクラエディタToolStripMenuItem.Click += new System.EventHandler(this.サクラエディタToolStripMenuItem_Click);
      // 
      // pSPadToolStripMenuItem
      // 
      this.pSPadToolStripMenuItem.Name = "pSPadToolStripMenuItem";
      this.pSPadToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.pSPadToolStripMenuItem.Text = "PSPad";
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(186, 6);
      // 
      // コマンドプロンプトToolStripMenuItem
      // 
      this.コマンドプロンプトToolStripMenuItem.Enabled = false;
      this.コマンドプロンプトToolStripMenuItem.Name = "コマンドプロンプトToolStripMenuItem";
      this.コマンドプロンプトToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.コマンドプロンプトToolStripMenuItem.Text = "コマンドプロンプト";
      // 
      // エクスプローラToolStripMenuItem
      // 
      this.エクスプローラToolStripMenuItem.Enabled = false;
      this.エクスプローラToolStripMenuItem.Name = "エクスプローラToolStripMenuItem";
      this.エクスプローラToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
      this.エクスプローラToolStripMenuItem.Text = "エクスプローラ";
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(270, 6);
      // 
      // 新規フォルダー作成ToolStripMenuItem
      // 
      this.新規フォルダー作成ToolStripMenuItem.Name = "新規フォルダー作成ToolStripMenuItem";
      this.新規フォルダー作成ToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.新規フォルダー作成ToolStripMenuItem.Text = "新規フォルダー作成";
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(270, 6);
      // 
      // ToolStripMenuItemDelete
      // 
      this.ToolStripMenuItemDelete.Enabled = false;
      this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
      this.ToolStripMenuItemDelete.Size = new System.Drawing.Size(273, 24);
      this.ToolStripMenuItemDelete.Text = "削除(&D)";
      this.ToolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
      // 
      // 名前の変更MToolStripMenuItem
      // 
      this.名前の変更MToolStripMenuItem.Enabled = false;
      this.名前の変更MToolStripMenuItem.Name = "名前の変更MToolStripMenuItem";
      this.名前の変更MToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.名前の変更MToolStripMenuItem.Text = "名前の変更(&M)";
      // 
      // 移動ToolStripMenuItem
      // 
      this.移動ToolStripMenuItem.Enabled = false;
      this.移動ToolStripMenuItem.Name = "移動ToolStripMenuItem";
      this.移動ToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.移動ToolStripMenuItem.Text = "移動";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(270, 6);
      // 
      // 容量の計算ToolStripMenuItem
      // 
      this.容量の計算ToolStripMenuItem.Name = "容量の計算ToolStripMenuItem";
      this.容量の計算ToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.容量の計算ToolStripMenuItem.Text = "容量の計算";
      // 
      // 検索ToolStripMenuItem
      // 
      this.検索ToolStripMenuItem.Name = "検索ToolStripMenuItem";
      this.検索ToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.検索ToolStripMenuItem.Text = "検索...";
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(270, 6);
      // 
      // パーミッションの変更ToolStripMenuItem
      // 
      this.パーミッションの変更ToolStripMenuItem.Name = "パーミッションの変更ToolStripMenuItem";
      this.パーミッションの変更ToolStripMenuItem.Size = new System.Drawing.Size(273, 24);
      this.パーミッションの変更ToolStripMenuItem.Text = "パーミッションの変更";
      // 
      // imageList3
      // 
      this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
      this.imageList3.TransparentColor = System.Drawing.Color.Black;
      this.imageList3.Images.SetKeyName(0, "Image0");
      this.imageList3.Images.SetKeyName(1, "Image1");
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Black;
      this.imageList1.Images.SetKeyName(0, "Image0");
      this.imageList1.Images.SetKeyName(1, "Image1");
      this.imageList1.Images.SetKeyName(2, "Image2");
      this.imageList1.Images.SetKeyName(3, "Image3");
      this.imageList1.Images.SetKeyName(4, "Image4");
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 27);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.lviFolder);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
      this.splitContainer1.Size = new System.Drawing.Size(379, 340);
      this.splitContainer1.SplitterDistance = 210;
      this.splitContainer1.TabIndex = 26;
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.propertyGrid1.HelpVisible = false;
      this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(379, 126);
      this.propertyGrid1.TabIndex = 0;
      this.propertyGrid1.ToolbarVisible = false;
      // 
      // FTPClientPanel
      // 
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.toolStrip1);
      this.Name = "FTPClientPanel";
      this.Size = new System.Drawing.Size(379, 391);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion













  }
}
