using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XMLTreeMenu;
using System.IO;
using AntPlugin.CommonLibrary;
using System.Diagnostics;
//using PluginCore;
using AntPlugin;
//using MDIform;
using MDIForm;

namespace AntPlugin.XMLTreeMenu.Controls
{
  public partial class PlayerPanel : UserControl//, IMDIForm
  {
      //public PluginMain xmlTreeMenu;
      //public XMLTreeMenu.Settings settings;
      public List<string> previousDocuments = new List<string>();
      public List<string> favorateDocuments = new List<string>();
      
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

      public List<string> FavorateDocuments
      {
        get
        {
          return this.favorateDocuments;
        }
        set
        {
          this.favorateDocuments = value;
        }
      }

      public PlayerPanel()
        {
            InitializeComponent();
            this.InitializeInterface();
            this.InitializePlayerPanel();
            this.Text = "Windows Media Player Ver1.0";
            //this.settings = this.xmlTreeMenu.Settings as XMLTreeMenu.Settings;
        }
      
      public void InitializeInterface()
      {
        string guid = "0538077E-8C37-4A2B-962B-8FB77DC9F325";
        //this.xmlTreeMenu = (PluginMain)PluginBase.MainForm.FindPlugin(guid);
        this.Instance = new ChildFormControlClass();
        this.Instance.name = "PlayerPanel";
        this.Instance.toolStrip = this.toolStrip1;
        this.Instance.menuStrip = this.menuStrip1;
        this.Instance.statusStrip = this.statusStrip1;
        this.Instance.スクリプトToolStripMenuItem = this.スクリプトSToolStripMenuItem;
        this.Instance.PreviousDocuments = this.PreviousDocuments;
      }

      private void InitializePlayerPanel()
      {
        this.statusStrip1.Visible = false;
        this.menuStrip1.Visible = true;
        this.toolStrip1.Visible = false;
        //this.MainForm.xmlTreeMenu_pluginUI.
       }

    private void PlayerPanel_Load(object sender, EventArgs e)
    {

        string path = (string)this.axWindowsMediaPlayer1.Tag;

        if (!string.IsNullOrEmpty(path) && File.Exists(path))
        {
          this.axWindowsMediaPlayer1.URL = (string)this.axWindowsMediaPlayer1.Tag;
          ((Form)this.Parent).Text = Path.GetFileNameWithoutExtension(path);

        }

     }
 
    private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
    {
      string path = (string)this.axWindowsMediaPlayer1.Tag;

      if (!string.IsNullOrEmpty(path) && File.Exists(path))
      {
          this.axWindowsMediaPlayer1.URL = (string)this.axWindowsMediaPlayer1.Tag;
          ((Form)this.Parent).Text = Path.GetFileNameWithoutExtension(path);
      }

      try
      {
          //this.toolStrip1.Visible = this.settings.PlayerPanelToolBarVisible;
          this.ツールバーTToolStripMenuItem.Checked = this.toolStrip1.Visible;
          //this.statusStrip1.Visible = this.settings.PlayerPanelStatusBarVisible;
          this.ステータスバーSToolStripMenuItem.Checked = this.statusStrip1.Visible;

          //this.previousDocuments = this.settings.PreviousPlayerPanelDocuments;
          this.PopulatePreviousDocumentsMenu();
          this.AddPreviousDocuments(path);
          
          ((Form)this.Parent).FormClosing += new FormClosingEventHandler(this.parentForm_Closing);


      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
      }
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

      private void parentForm_Closing(object sender, EventArgs e)
      {
        //this.settings.PreviousPlayerPanelDocuments = this.previousDocuments;
        //this.settings.PlayerPanelMenuBarVisible = this.menuStrip1.Visible;
        //this.settings.PlayerPanelToolBarVisible = this.toolStrip1.Visible;
        //this.settings.PlayerPanelStatusBarVisible = this.statusStrip1.Visible;
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
            toolStripMenuItem2.Text = text;
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
        if (File.Exists(text) && (Lib.IsSoundFile(text) || Lib.IsVideoFile(text)))
        {
          try
          {
            this.axWindowsMediaPlayer1.URL = text;
            this.axWindowsMediaPlayer1.Tag = text;
            ((Form)this.Parent).Text = Path.GetFileName(text);
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

      private void PlayerPanel_Leave(object sender, EventArgs e)
      {
        if (!this.toolStrip1.Visible)
        {
        }
      }

      private void PlayerPanel_Enter(object sender, EventArgs e)
      {
        if (!this.toolStrip1.Visible)
        {
        }
      }

      private void メニューバーMToolStripMenuItem_Click(object sender, EventArgs e)
      {
        this.menuStrip1.Visible = this.メニューバーMToolStripMenuItem.Checked;
      }

      private void ステータスバーSToolStripMenuItem1_Click(object sender, EventArgs e)
      {
        this.statusStrip1.Visible = this.ステータスバーSToolStripMenuItem1.Checked;
        this.ステータスバーSToolStripMenuItem.Checked = this.statusStrip1.Visible;
      }

      private void kngFMKToolStripMenuItem_Click(object sender, EventArgs e)
      {
        this.axWindowsMediaPlayer1.URL = "C:\\home\\KingFM.asx";
      }

      private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
      {
        string initialDirectory = "F:\\My Music\\";
        string filter = "soundファイル(*.asf;*.wax;*.asx;*.mp3;*.wma)|*.asf;*.wax;*.asx;*.mp3;*.wma|すべてのファイル(*.*)|*.*";
        string fileName = "test.wma";
        string text = Lib.File_OpenDialog(fileName, initialDirectory, filter);
        try
        {
          if (File.Exists(text) && (Lib.soundfile.Contains(Path.GetExtension(text)) || Lib.videofile.Contains(Path.GetExtension(text))))
          {
            this.axWindowsMediaPlayer1.URL = text;
            this.axWindowsMediaPlayer1.BringToFront();
            this.axWindowsMediaPlayer1.Tag = text;
            ((Form)this.Parent).Text = Path.GetFileNameWithoutExtension(text);
            this.AddPreviousDocuments(text);
          }
        }
        catch (Exception ex)
        {
          string message = ex.Message.ToString();
          MessageBox.Show(Lib.OutputError(message));
        }
      }

      private void 開くOToolStripButton_Click(object sender, EventArgs e)
      {
        this.開くOToolStripMenuItem.PerformClick();
      }

      private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
      {
        ((Form)this.Parent).Close();
      }

      private void バージョン情報AToolStripMenuItem_Click(object sender, EventArgs e)
      {
        //PluginBase.MainForm.CallCommand("About", "");
      }

      private void mediaPlayerToolStripMenuItem_Click(object sender, EventArgs e)
      {
        this.axWindowsMediaPlayer1.Ctlcontrols.stop();
        Process.Start(this.axWindowsMediaPlayer1.Tag.ToString());
      }

      private void ツールバーTToolStripMenuItem_Click(object sender, EventArgs e)
      {
        this.toolStrip1.Visible = ツールバーTToolStripMenuItem.Checked;
      }

      private void ステータスバーSToolStripMenuItem_Click(object sender, EventArgs e)
      {
        this.statusStrip1.Visible = ステータスバーSToolStripMenuItem.Checked;
        this.ステータスバーSToolStripMenuItem1.Checked = this.statusStrip1.Visible;
      }

      private void お気に入りに追加AToolStripMenuItem_Click(object sender, EventArgs e)
      {

      }

      private void お気に入りをクリアCToolStripMenuItem_Click(object sender, EventArgs e)
      {

      }

    private void callPluginCommandToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.MainForm.callPluginCommand("こんにちわ", "畑さん");
    }
  }
}
