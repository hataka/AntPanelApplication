using AntPlugin.XMLTreeMenu.Controls;
using CommonLibrary.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AntPanelApplication
{
  public partial class Form1 : Form
  {
    global::AntPanelApplication.Properties.Settings
      settings = new global::AntPanelApplication.Properties.Settings();

    public bool showIcon = true;
    public BrowserEx browser;
    public PicturePanel picturePanel;
    public RichTextEditor editor;
    public AntPlugin.XMLTreeMenu.Controls.PlayerPanel player;

    public Form1()
    {
      InitializeComponent();
      AntPanel antPanel = new AntPanel();
      InitializeForm(antPanel);
    }

    public Form1(String path)
    {
      InitializeComponent();
      AntPanel antPanel = new AntPanel(path);
      antPanel.AccessibleDescription = path;
      InitializeForm(antPanel);
    }

    public Form1(String[] args)
    {
      InitializeComponent();
      AntPanel antPanel = new AntPanel(args);
      if (args.Length > 1 && !String.IsNullOrEmpty(args[0]))
      {
        antPanel.AccessibleDescription = args[0];
      }
      InitializeForm(antPanel);
    }

    private void InitializeForm(AntPanel antpanel)
    {
      LoadControls(antpanel);

      this.splitContainer1.Panel1.Controls.Add(antpanel);
      this.splitContainer1.Panel2Collapsed = true;
      this.splitContainer1.Panel1Collapsed = false;

      this.Text = "AntPanel : " + Path.GetFileName(antpanel.AccessibleDescription);
      this.Size = new Size(1200, 800);
      this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void LoadControls(AntPanel antpanel)
    {
      antpanel.Dock = DockStyle.Fill;
      antpanel.Tag = this;

      this.tabPage1.Controls.Clear();
      this.tabPage1.Text = "Editor";
      this.editor = new RichTextEditor();
      this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
      editor.Tag = this;
      this.tabPage1.Controls.Add(this.editor);
      //this.tabPage1.Controls.Add(this.richTextBox1);

      this.tabPage2.Controls.Clear();
      this.browser = new BrowserEx();
      // 
      // webBrowser
      // 
      this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.browser.Location = new System.Drawing.Point(0, 0);
      this.browser.MinimumSize = new System.Drawing.Size(20, 20);
      this.browser.Name = "Browser";
      this.browser.Size = new System.Drawing.Size(973, 474);
      this.browser.TabIndex = 0;
      this.browser.Tag = this;
      this.tabPage2.Text = "Browser";
      this.tabPage2.Controls.Add(this.browser);

      this.tabPage3.Controls.Clear();
      this.picturePanel = new PicturePanel();
      this.picturePanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.picturePanel.Tag = this;
      this.tabPage3.Text = "Picture";
      this.tabPage3.Controls.Add(this.picturePanel);

      this.tabPage4.Controls.Clear();
      this.player = new AntPlugin.XMLTreeMenu.Controls.PlayerPanel();
      this.player.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabPage4.Text = "Player";
      this.axWindowsMediaPlayer1.Tag = this;
      this.player.Tag = this;
      //this.tabPage4.Controls.Add(this.axWindowsMediaPlayer1);
      this.tabPage4.Controls.Add(this.player);

      this.tabPage5.Controls.Clear();
      //this.tabPage5.Controls.Add(this.axWindowsMediaPlayer1);

      this.tabPage6.Controls.Clear();
      //this.tabPage6.Controls.Add(this.axWindowsMediaPlayer1);

    }

    private void ApplySettings()
    {
      this.menuStrip1.Visible = this.settings.MenuBarVisible;
      this.toolStrip1.Visible = this.settings.ToolBarVisible;
      this.statusStrip1.Visible = this.settings.StatusBarVisible;
    }


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
