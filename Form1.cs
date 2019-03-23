using AntPanelApplication.Properties;
using AntPlugin.XMLTreeMenu.Controls;
using CommonLibrary;
using CommonLibrary.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AntPanelApplication
{
  public partial class Form1 : Form
  {
    #region Variables
    global::AntPanelApplication.Properties.Settings
      settings = new global::AntPanelApplication.Properties.Settings();
    global::AntPanelApplication.Properties.Resources 
      resources = new global::AntPanelApplication.Properties.Resources();

    public static OperatingSystem os = Environment.OSVersion;
    public static bool IsRunningOnMono = (Type.GetType("Mono.Runtime") != null);
    public static bool IsRunningUnix = ((Environment.OSVersion.ToString()).IndexOf("Unix") >= 0) ? true : false;
    public static bool IsRunningWindows = ((Environment.OSVersion.ToString()).IndexOf("Windows") >= 0) ? true : false;

    public AntPanel antPanel;
    public BrowserEx browser;
    public PicturePanel picturePanel;
    public RichTextEditor editor;
    public PlayerPanel player;
    public SimplePanel panel;

    public bool showIcon = true;
    /* Singleton */
    public static Boolean Silent;
    public static Boolean IsFirst;
    public static String[] Arguments;
    #endregion

    public Form1()
    {
      Globals.MainForm = this;
      this.antPanel = new AntPanel();
      if (Arguments.Length > 1 && !String.IsNullOrEmpty(Arguments[0]))
      {
        antPanel.AccessibleDescription = Arguments[0];
      }
      Globals.AntPanel = this.antPanel;
      InitializeComponent();
      InitializeForm();
    }

    private void InitializeForm()
    {
      this.splitContainer1.Panel1.Controls.Add(Globals.AntPanel);
      //this.splitContainer1.Panel2Collapsed = true;
      //this.splitContainer1.Panel1Collapsed = false;
      Globals.AntPanel.Dock = DockStyle.Fill;
      //Globals.AntPanel.Tag = this; //??


      LoadControls();

      this.Text = "AntPanel : " + Path.GetFileName(Globals.AntPanel.AccessibleDescription);
      this.Size = new Size(1200, 800);
      this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void LoadControls()
    {

      this.documentTabControl.TabPages.Clear();

      this.editor = new RichTextEditor();
      this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.editor.AccessibleDescription = "Editor";
      ((Control)this.editor.Tag).Tag = "無題";
      TabPageManager.AddTabPage(this.editor, this.documentTabControl);

      if (IsRunningUnix) return;   

      this.picturePanel = new PicturePanel();
      this.picturePanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.picturePanel.AccessibleDescription = "夏樹東京歓送会";
      ((Control)this.picturePanel.Tag).Tag = @"F:\VirtualBox\ShareFolder\Picture\DSCN0166.JPG";
      TabPageManager.AddTabPage(this.picturePanel, this.documentTabControl);
      // 
      // webBrowser
      // 
      this.browser = new BrowserEx();
      this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.browser.AccessibleDescription = "pukiwiki2016";
      ((Control)this.browser.Tag).Tag = "http://192.168.0.13/pukiwiki2016/index.php";
      TabPageManager.AddTabPage(this.browser, this.documentTabControl);
      this.player = new AntPlugin.XMLTreeMenu.Controls.PlayerPanel();
      this.player.Dock = System.Windows.Forms.DockStyle.Fill;
      this.player.AccessibleDescription = "二つのミサ曲";
      ((Control)this.player.Tag).Tag = @"F:\VirtualBox\ShareFolder\Music\03-Monteverdi.mp3";
      TabPageManager.AddTabPage(this.player, this.documentTabControl);

      this.panel = new SimplePanel();
      this.panel.Dock = DockStyle.Fill;
      this.panel.AccessibleDescription = "Lesson5";
      ((Control)this.panel.Tag).Tag = @"F:\c_program\OpenGL\NeHe_1200x900\Lesson05\lesson5.exe";
      TabPageManager.AddTabPage(this.panel, this.documentTabControl);

      this.tabPage6.Controls.Clear();
      this.documentTabControl.Controls.Remove(this.tabPage6);

      //RichTextBox textBox = new RichTextBox();
      //textBox.Dock = DockStyle.Fill;
      //textBox.Text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
      //textBox.AccessibleDescription = "無題";
      //TabPageManager.AddTabPage(textBox, this.documentTabControl);
    }

    private void ApplySettings()
    {
      this.menuStrip1.Visible = this.settings.MenuBarVisible;
      this.toolStrip1.Visible = this.settings.ToolBarVisible;
      this.statusStrip1.Visible = this.settings.StatusBarVisible;

      //this.gradleButton.Image = global::AntPanelApplication.Properties.Resources.gradle;
      //this.gradleButton.Image = Resources.gradle;
    }


    #region Event Handler
    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      TabControl tabControl = sender as TabControl;
      //MessageBox.Show(tabControl.Name);
    }
    #endregion

    #region Click Handler
    private int toggleIndex = 1;
    private void 表示ToolStripButton_Click(object sender, EventArgs e)
    {
      //this.xmlTreeView1.toggle_アイコン表示();
      int num = this.toggleIndex % 3;
      if (num == 0)
      {
        this.splitContainer1.Panel2Collapsed = true;
        this.splitContainer1.Panel1Collapsed = false;
        //this.propertyGrid1.HelpVisible = false;
        //this.propertyGrid1.ToolbarVisible = false;
      }
      else if (num == 1)
      {
        this.splitContainer1.Panel2Collapsed = false;
        this.splitContainer1.Panel1Collapsed = true;
        //this.propertyGrid1.HelpVisible = true;
        //this.propertyGrid1.ToolbarVisible = true;
      }
      else if (num == 2)
      {
        this.splitContainer1.Panel2Collapsed = false;
        this.splitContainer1.Panel1Collapsed = false;
        //this.propertyGrid1.HelpVisible = false;
        //this.propertyGrid1.ToolbarVisible = false;
      }
      this.Refresh();
      //this.Update();
      //this.Invalidate();
      this.toggleIndex++;
    }


    private void 画面切替TToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.表示ToolStripButton_Click(sender, e);
    }

    private void ツールバーTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.toolStrip1.Visible = this.ツールバーTToolStripMenuItem.Checked;
    }

    private void ステータスバーSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.statusStrip1.Visible = ステータスバーSToolStripMenuItem.Checked;
    }
    #endregion

    public void ActivatePlayer()
    {
      //this.splitContainer1.Panel2Collapsed = false;
      //this.splitContainer1.Panel1Collapsed = false;
      //this.t.tabControl1.SelectedIndex = 3;
    }

    #region Icon Management

    public void toggle_アイコン表示()
    {
      this.showIcon = !this.showIcon;
      if (this.showIcon == true)
      {
        //this.ImageList = this.imageList1;
      }
      //else this.ImageList = null;
    }

    #endregion

  }
}
