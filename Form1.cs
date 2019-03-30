using AntPanelApplication.Helpers;
using AntPanelApplication.Managers;
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
      //PluginBase.Initialize(this);
      this.antPanel = new AntPanel();
      if (Arguments.Length > 1 && !String.IsNullOrEmpty(Arguments[0]))
      {
        antPanel.AccessibleDescription = Arguments[0];
      }
      Globals.AntPanel = this.antPanel;
      //this.DoubleBuffered = true;
      //this.InitializeErrorLog();
      this.InitializeSettings();
      //this.InitializeLocalization();

      //if (this.InitializeFirstRun() != DialogResult.Abort)
      //{
      // Suspend layout!
      //this.InitializeConfig();
      //this.InitializeRendering();
      this.InitializeComponent();
      this.InitializeComponents();
      //this.InitializeProcessRunner();
      //this.InitializeSmartDialogs();
      this.InitializeMainForm();
      //this.InitializeGraphics();
      //Application.AddMessageFilter(this);
      //}
      //else this.Load += new EventHandler(this.MainFormLoaded);
    }

    #region Construct Components
    public void InitializeSettings() { }




    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private ToolStripPanel toolStripPanel;

    public void InitializeComponents()
    {
      //this.Controls.Add(this.splitContainer1);
      //this.Controls.Add(this.statusStrip1);
      //this.Controls.Add(this.toolStrip1);
      //this.Controls.Add(this.menuStrip1);

      this.Controls.Remove(this.menuStrip1);
      this.Controls.Remove(this.toolStrip1);
      this.Controls.Remove(this.statusStrip1);
      this.Controls.Remove(this.splitContainer1);


      this.toolStripPanel = new ToolStripPanel();
      this.toolStripPanel.Dock = DockStyle.Top;



      this.menuStrip = StripBarManager.GetMenuStrip(FileNameHelper.MainMenu);
      this.toolStrip = StripBarManager.GetToolStrip(FileNameHelper.ToolBar);

      this.menuStrip.Font = new Font("Meiryo UI", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
      //
      // toolstrip1
      //

      this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.toolStrip.Location = new System.Drawing.Point(0, 0);
      this.toolStrip.Name = "toolStrip1";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
      this.toolStrip.Size = new System.Drawing.Size(2007, 42);
      this.toolStrip.Font = new Font("Meiryo UI", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
      this.toolStrip.TabIndex = 1;
      this.toolStrip.Text = "toolStrip1";
      this.toolStrip.Visible = true;

      this.toolStrip2 = new ToolStrip();

      string customButtonFilePath = Path.Combine(PathHelper.SettingDataDir, "DropDownButtonToolBar.xml");
      if (File.Exists(customButtonFilePath))
      {
        try
        {
          this.toolStrip2 = StripBarManager.GetToolStrip(customButtonFilePath);
          ToolStripManager.Merge(this.toolStrip2, this.toolStrip);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message.ToString());
        }
      }


      if (Type.GetType("Mono.Runtime") != null)
      {
        this.toolStripPanel.Controls.Add(this.menuStrip);
        this.toolStripPanel.Controls.Add(this.toolStrip);
      }
      else
      {
        this.toolStripPanel.Controls.Add(this.toolStrip);
        this.toolStripPanel.Controls.Add(this.menuStrip);
      }
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.toolStripPanel);
      this.Controls.Add(this.statusStrip1);
      this.MainMenuStrip = this.menuStrip;




    }

    #endregion


    private void InitializeMainForm()
    {
      // https://dobon.net/vb/dotnet/control/tabpagehide.html
      //TabPageManagerオブジェクトの作成
      TabPageManager.AddTabControl(this.documentTabControl);

      this.settings = new global::AntPanelApplication.Properties.Settings();
      this.splitContainer1.Panel1.Controls.Add(Globals.AntPanel);
      //this.splitContainer1.Panel2Collapsed = true;
      //this.splitContainer1.Panel1Collapsed = false;
      Globals.AntPanel.Dock = DockStyle.Fill;
      //Globals.AntPanel.Tag = this; //??
      InitializeControls();

      this.Text = "AntPanel : " + Path.GetFileName(Globals.AntPanel.AccessibleDescription);
      this.Size = new Size(1200, 800);
      this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void InitializeControls()
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

    /// Handles the incoming events
    /// </summary>
    /// <summary>
    /// Handles the incoming events
    /// </summary>
    public void HandleEvent(Object sender, NotifyEvent e, HandlingPriority prority)
    {
      //MessageBox.Show("HandleEvent");
      switch (e.Type)
      {
        case EventType.FileOpen:
          TextEvent evnt2 = (TextEvent)e;
          if (File.Exists(evnt2.Value))
          {
            //this.pluginUI.AddPreviousCustomDocuments(evnt2.Value);
          }
          break;
        case EventType.FileSwitch:
          /*
          this.SetEnvironmentVariable();
          */


          //以下OK
          //string fileName = PluginBase.MainForm.CurrentDocument.FileName;
          //if (File.Exists(fileName))
          //{
          //this.pluginUI.AddPreviousCustomDocuments(fileName);
          //}


          break;
        case EventType.Command:
          DataEvent evnt = (DataEvent)e;
          //MessageBox.Show(evnt.Action);
          switch (evnt.Action)
          {
            case "XMLTreeMenu.Load":
              string path = Globals.MainForm.ProcessArgString(evnt.Data.ToString());

              //Globals.AntPanel.menuTree.LoadFile(path);
              //Globals.AntPanel.OpenPanel(null, null);
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.ImportXml":
              //this.pluginUI.ImportXml();
              //this.OpenPanel(null, null);
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.Test":
              //this.pluginUI.Test(evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.InsertTimeStamp":
              //this.pluginUI.InsertTimeStamp();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.InsertCHeading":
              //this.pluginUI.InsertCHeading();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.InsertLocalUrl":
              //this.pluginUI.InsertLocalUrl();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.CreateCustomDocument":

              //MessageBox.Show(evnt.Data.ToString(), "XMLTreeMenu.CreateCustomDocument");
              //this.CreateCustomDocument(evnt.Data.ToString());
              evnt.Handled = true;

              return;
            case "XMLTreeMenu.RunProcessDialog":
              //this.pluginUI.RunProcessDialog();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.Browse":
              //this.pluginUI.Browse(evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.BrowseEx":
              //this.pluginUI.BrowseEx(evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.BrowseExString":
              //MessageBox.Show(evnt.Data.ToString());
              //this.pluginUI.BrowseExString(evnt.Data.ToString().Replace("semicolon", ";"));
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.ExecuteInPlace":
              //this.pluginUI.ExecuteInPlace(evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.CloseOpen":
              //this.pluginUI.CloseOpen();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.CloseReOpen":
              //this.pluginUI.CloseReOpen();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.EncodeSave":
              //this.pluginUI.EncodeSave(evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.CurrentDocumentPath":
              //this.pluginUI.CurrentDocumentPath();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.SwitchToBrowseEx":
              //this.pluginUI.SwitchToBrowseEx();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.SwitchToMainFormBrowser":
              //this.pluginUI.SwitchToMainFormBrowser();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.Menu":
              //this.pluginUI.Menu(evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.RunProcess":
              //this.pluginUI.RunProcess(evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.SpreadSheet":
              //this.pluginUI.SpreadSheet(evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.Picture":
              try
              {
                //this.pluginUI.Picture(evnt.Data.ToString());
              }
              catch
              {
                //this.pluginUI.Picture("dummy");
              }
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.OpenFile":
            case "XMLTreeMenu.OpenDocument":
            case "XMLTreeMenu.OpenProject":
              //this.pluginUI.OpenDocument(evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.Open":
              //this.pluginUI.Open();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.Player":
              //this.pluginUI.Player(evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.PSPadTemplate":
              //this.pluginUI.PSPadTemplate(evnt.Data.ToString());
              evnt.Handled = true;
              break;
            case "XmlTreeMenu.CallCommand":
            case "XMLTreeMenu.CallCommand":
              string[] tmp = (evnt.Data.ToString()).Split('!');
              string name = tmp[0];
              string arg = tmp.Length > 1 ? tmp[1] : string.Empty;

              //MessageBox.Show(arg,name);

              //this.pluginUI.CallCommand(name, arg.Replace("semicolon", ";"));
              evnt.Handled = true;
              break;
            case "XMLTreeMenu.Exit":
              //this.pluginUI.Exit();
              evnt.Handled = true;
              break;
          }
          break;
      }
    }




    #endregion



    #region General Methods

    /// <summary>
    /// Finds the specified composed/ready image that is automatically adjusted according to the theme.
    /// <para/>
    /// If you make a copy of the image returned by this method, the copy will not be automatically adjusted.
    /// </summary>
    public Image FindImage(String data)
    {
      return FindImage(data, true);
    }

    /// <summary>
    /// Finds the specified composed/ready image.
    /// <para/>
    /// If you make a copy of the image returned by this method, the copy will not be automatically adjusted, even if <code>autoAdjusted</code> is <code>true</code>.
    /// </summary>
    public Image FindImage(String data, Boolean autoAdjusted)
    {
      try
      {
        lock (this) return ImageManager.GetComposedBitmap(data, autoAdjusted);
      }
      catch (Exception ex)
      {
        //ErrorManager.ShowError(ex);
        MessageBox.Show(ex.Message.ToString());
        return null;
      }
    }

    /// <summary>
    /// Finds the specified composed/ready image that is automatically adjusted according to the theme.
    /// The image size is always 16x16.
    /// <para/>
    /// If you make a copy of the image returned by this method, the copy will not be automatically adjusted.
    /// </summary>
    public Image FindImage16(String data)
    {
      return FindImage16(data, true);
    }

    /// <summary>
    /// Finds the specified composed/ready image. The image size is always 16x16.
    /// <para/>
    /// If you make a copy of the image returned by this method, the copy will not be automatically adjusted, even if <code>autoAdjusted</code> is <code>true</code>.
    /// </summary>
    public Image FindImage16(String data, Boolean autoAdjusted)
    {
      try
      {
        lock (this) return ImageManager.GetComposedBitmapSize16(data, autoAdjusted);
      }
      catch (Exception ex)
      {
        //ErrorManager.ShowError(ex);
        MessageBox.Show(ex.Message.ToString());
        return null;
      }
    }

    /// <summary>
    /// Finds the specified composed/ready image and returns a copy of the image that has its color adjusted.
    /// This method is typically used for populating a <see cref="ImageList"/> object.
    /// <para/>
    /// Equivalent to calling <code>ImageSetAdjust(FindImage(data, false))</code>.
    /// </summary>
    public Image FindImageAndSetAdjust(String data)
    {
      return ImageSetAdjust(FindImage(data, false));
    }

    /// <summary>
    /// Returns a copy of the specified image that has its color adjusted.
    /// </summary>
    public Image ImageSetAdjust(Image image)
    {
      return ImageManager.SetImageAdjustment(image);
    }

    /// <summary>
    /// Gets a copy of the image that gets automatically adjusted according to the theme.
    /// </summary>
    public Image GetAutoAdjustedImage(Image image)
    {
      return ImageManager.GetAutoAdjustedImage(image);
    }

    /// <summary>
    /// Adjusts all images for different themes.
    /// </summary>
    public void AdjustAllImages()
    {
      /*
      ImageManager.AdjustAllImages();
      ImageListManager.RefreshAll();

      for (int i = 0, length = LayoutManager.PluginPanels.Count; i < length; i++)
      {
        DockablePanel panel = LayoutManager.PluginPanels[i] as DockablePanel;
        if (panel != null) panel.RefreshIcon();
      }
      */
    }



    /// <summary>
    /// Processes the argument string variables
    /// </summary>
    public String ProcessArgString(String args, bool dispatch)
    {
      //return ArgsProcessor.ProcessString(args, dispatch);
      return Globals.AntPanel.menuTree.ProcessVariable(args);
    }
    public String ProcessArgString(String args)
    {
      //return ArgsProcessor.ProcessString(args, true);
      return ProcessArgString(args, true);
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



    #region Click Handlers
    public void SmartNew(object sender, EventArgs e)
    {
    }

    public void ChangeSyntax(object sender, EventArgs e)
    {
      try
      {
        //ScintillaControl sci = Globals.SciControl;
        ToolStripItem button = (ToolStripItem)sender;
        string language = ((ItemData)button.Tag).Tag;
        MessageBox.Show("こんにちわ " + language);
        //if (sci.ConfigurationLanguage.Equals(language)) return; // already using this syntax
        //ScintillaManager.ChangeSyntax(language, sci);
        //string extension = sci.GetFileExtension();
        //if (!string.IsNullOrEmpty(extension))
        //{
        //  string title = TextHelper.GetString("Title.RememberExtensionDialog");
        //  string message = TextHelper.GetString("Info.RememberExtensionDialog");
        //  if (MessageBox.Show(message, title, MessageBoxButtons.YesNo) == DialogResult.Yes)
        //  {
        //    sci.SaveExtensionToSyntaxConfig(extension);
        //  }
        //}
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message.ToString());
      }
    }

    public void Open(object sender, EventArgs e)
    {
      /*
      this.openFileDialog.Multiselect = true;
      this.openFileDialog.InitialDirectory = this.WorkingDirectory;
      if (this.openFileDialog.ShowDialog(this) == DialogResult.OK && this.openFileDialog.FileName.Length != 0)
      {
        Int32 count = this.openFileDialog.FileNames.Length;
        for (Int32 i = 0; i < count; i++)
        {
          this.OpenEditableDocument(openFileDialog.FileNames[i]);
        }
      }
      this.openFileDialog.Multiselect = false;
      */
    }

    public void Save(object sender, EventArgs e) { }
    public void SaveAll(object sender, EventArgs e) { }
    public void Print(object sender, EventArgs e) { }
    public void ToggleBookmark(object sender, EventArgs e) { }
    public void NextBookmark(object sender, EventArgs e) { }
    public void PrevBookmark(object sender, EventArgs e) { }
    public void ClearBookmarks(object sender, EventArgs e) { }

    public void PluginCommand(object sender, EventArgs e)
    {
      try
      {
        ToolStripItem button = (ToolStripItem)sender;
        String[] args = ((ItemData)button.Tag).Tag.Split(';');
        String action = args[0]; // Action of the command
        String data = (args.Length > 1) ? args[1] : null;
        DataEvent de = new DataEvent(EventType.Command, action, data);
        EventManager.DispatchEvent(this, de);
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()), "PluginCommand");
      }
    }

    public void EditSnippets(object sender, EventArgs e) { }
    public void NewFromTemplate(object sender, EventArgs e) { }
    public void Reload(object sender, EventArgs e) { }
    public void Duplicate(object sender, EventArgs e) { }
    public void Revert(object sender, EventArgs e) { }
    public void SaveAs(object sender, EventArgs e) { }
    public void OnEncodeSave(object sender, EventArgs e) { }

    public void Close(object sender, EventArgs e)
    {
      /*
      string msgboxString = this.tabControl1.SelectedTab.Text + " タブを削除します\nよろしいですか?";
      //if (Lib.confirmDestructionText("削除確認", msgboxString) == true)
      //{
      ..tabpageManager.CloseTabPage(sender, e);
      //}
      */
    }

    public void ReopenClosed(object sender, EventArgs e) { }

    public void CloseOthers(object sender, EventArgs e)
    {

    }

    public void CloseAll(object sender, EventArgs e)
    {

    }

    public void ChangeEncoding(object sender, EventArgs e) { }
    public void ToggleSaveBOM(object sender, EventArgs e) { }

    public void OpenCustomDocument(object sender, EventArgs e)
    {
      /*
      try
      {
        ToolStripItem button = (ToolStripItem)sender;
        String[] args = ((ItemData)button.Tag).Tag.Split(';');
        String action = args[0]; // Action of the command
        String data = (args.Length > 1) ? args[1] : null;
        OpenCustomDocument(action, data);
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()),
          "OpenCustomDocument(object sender, EventArgs e)");
      }
      */
    }

    public void OpenIn(object sender, EventArgs e) { }
    public void ConvertEOL(object sender, EventArgs e) { }
    public void PrintPreview(object sender, EventArgs e) { }
    public void Restart(object sender, EventArgs e) { }
    public void Exit(object sender, EventArgs e) { }
    public void ScintillaCommand(object sender, EventArgs e) { }
    public void SmartPaste(object sender, EventArgs e) { }
    public void SortLineGroups(object sender, EventArgs e) { }
    public void SortLines(object sender, EventArgs e) { }
    public void ToggleLineComment(object sender, EventArgs e) { }
    public void ToggleBlockComment(object sender, EventArgs e) { }
    public void SaveAsTemplate(object sender, EventArgs e) { }
    public void SaveAsSnippet(object sender, EventArgs e) { }
    public void ToggleBooleanSetting(object sender, EventArgs e) { }
    public void ToggleFold(object sender, EventArgs e) { }
    public void ToggleFullScreen(object sender, EventArgs e) { }
    public void ToggleSplitView(object sender, EventArgs e) { }
    public void QuickFind(object sender, EventArgs e) { }
    public void FindNext(object sender, EventArgs e) { }
    public void FindPrevious(object sender, EventArgs e) { }
    public void FindAndReplace(object sender, EventArgs e) { }
    public void FindAndReplaceInFiles(object sender, EventArgs e) { }

    public void GoTo(object sender, EventArgs e) { }
    public void GoToMatchingBrace(object sender, EventArgs e) { }
    public void InsertFile(object sender, EventArgs e) { }
    public void InsertFileDetails(object sender, EventArgs e) { }
    public void InsertTimestamp(object sender, EventArgs e) { }
    public void InsertSnippet(object sender, EventArgs e) { }
    public void InsertColor(object sender, EventArgs e) { }
    public void InsertGUID(object sender, EventArgs e) { }
    public void InsertHash(object sender, EventArgs e) { }
    public void KillProcess(object sender, EventArgs e) { }

    /// <summary>
    /// Calls a normal MainForm method
    /// </summary>
    public Boolean CallCommand(String name, String tag)
    {
      String classname = String.Empty;
      String methodname = String.Empty;
      String accessor = String.Empty;
      /*
      try
      {
        ToolStripMenuItem button = new ToolStripMenuItem();
        button.Tag = new ItemData(null, tag, null); // Tag is used for args
        Object[] parameters = new Object[2];
        parameters[0] = button; parameters[1] = null;
        if (name.LastIndexOf('.') > -1)
        {
          Int32 position = name.LastIndexOf('.'); // Position of the arguments
          methodname = name.Substring(position + 1);
          classname = name.Substring(0, position);
          if (methodname.IndexOf(':') > -1)
          {
            accessor = methodname.Split(':')[0];
            methodname = methodname.Split(':')[1];
          }
          Type type = Type.GetType(classname);
          object instance = Activator.CreateInstance(type);
          switch (accessor.ToLower())
          {
            case "private":
              MethodInfo method = type.GetMethod(methodname, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance);
              method.Invoke(instance, parameters);
              return true;
            case "static":
              MethodInfo method2
                   = type.GetMethod(methodname, BindingFlags.Static | BindingFlags.Public);
              method2.Invoke(null, parameters);
              return true;
            default:
              MethodInfo method3 = type.GetMethod(methodname);
              method3.Invoke(instance, parameters);
              return true;
          }

          //MessageBox.Show(methodname,clasname);
          //Type type = Type.GetType(classname);
          //object instance = Activator.CreateInstance(type);
          // privateメソッドを無理やり使用する方法
          // https://qiita.com/Aki_mintproject/items/f6a8a801d71312275655
          //MethodInfo method = type.GetMethod(methodname, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance);
          // publicメソッド
          //MethodInfo method = type.GetMethod(methodname);
          //method.Invoke(instance, parameters);
          //return true;
        }
        else
        {
          Type mfType = this.GetType();
          System.Reflection.MethodInfo method = mfType.GetMethod(name);
          if (method == null) throw new MethodAccessException();
          method.Invoke(this, parameters);
          return true;
        }
      }
      catch (Exception ex)
      {
        //ErrorManager.ShowError(ex);
        MessageBox.Show(Lib.OutputError(ex.Message.ToString())
          , "CallCommand(String name, String tag)");
        return false;
      }
      */
      return true;
    }

    public void CallCommand(object sender, EventArgs e)
    {
      String Arguments = String.Empty;
      String Name = String.Empty;
      try
      {
        ToolStripItem button = (ToolStripItem)sender;
        String args = Globals.AntPanel.menuTree.ProcessVariable(((ItemData)button.Tag).Tag);
        Int32 position = args.IndexOf(';'); // Position of the arguments
        Arguments = args.Substring(position + 1);
        Name = args.Substring(0, position);
        this.CallCommand(Name, Arguments);
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()),
          "CallCommand(object sender, EventArgs e)");
      }

    }

    public void RunProcess(object sender, EventArgs e)
    {
      /*
      try
      {
        ToolStripItem button = (ToolStripItem)sender;
        String args = Globals.AntPanel.menuTree.ProcessVariable(((ItemData)button.Tag).Tag);
        Int32 position = args.IndexOf(';'); // Position of the arguments
        //NotifyEvent ne = new NotifyEvent(EventType.ProcessStart);
        //EventManager.DispatchEvent(this, ne);
        if (position > -1)
        {
          //String message = TextHelper.GetString("Info.RunningProcess");
          //TraceManager.Add(message + " " + args.Substring(0, position) + " " + args.Substring(position + 1), (Int32)TraceType.ProcessStart);
          ProcessStartInfo psi = new ProcessStartInfo();
          psi.WorkingDirectory = AntPanel.projectDir;
          psi.Arguments = args.Substring(position + 1);
          psi.FileName = args.Substring(0, position);
          //ProcessHelper.StartAsync(psi);
          Process.Start(psi);
        }
        else
        {
          //String message = TextHelper.GetString("Info.RunningProcess");
          //TraceManager.Add(message + " " + args, (Int32)TraceType.ProcessStart);
          if (args.ToLower().EndsWith(".bat"))
          {
            Process bp = new Process();
            bp.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            bp.StartInfo.FileName = @args;
            bp.Start();
          }
          else
          {
            ProcessStartInfo psi = new ProcessStartInfo(args);
            psi.WorkingDirectory = this.WorkingDirectory;
            Process.Start(psi);
            //ProcessHelper.StartAsync(psi);
          }
        }
        //ButtonManager.UpdateFlaggedButtons();
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()),
          "RunProcess(object sender, EventArgs e)");
      }

  */
    }

    public void Browse(object sender, EventArgs e)
    {
      /*
      //Browser browser = new Browser();
      //browser.Dock = DockStyle.Fill;
      if (sender != null)
      {
        ToolStripItem button = (ToolStripItem)sender;
        String url = Globals.AntPanel.menuTree.ProcessVariable(((ItemData)button.Tag).Tag);
        this.OpenCustomDocument("Browser", url);
        //if (url.Trim() != "") browser.WebBrowser.Navigate(url);
        //else browser.WebBrowser.GoHome();
      }
      //else browser.WebBrowser.GoHome();
      */
    }

    private Int32 antPanel_toggleIndex = 0;
    public void ToggleAntPanelSplitter(object sender, EventArgs e)
    {
      int num = this.antPanel_toggleIndex % 3;
      if (num == 0)
      {
        Globals.MainForm.splitContainer1.Panel2Collapsed = true;
        Globals.MainForm.splitContainer1.Panel1Collapsed = false;
      }
      else if (num == 1)
      {
        Globals.MainForm.splitContainer1.Panel2Collapsed = false;
        Globals.MainForm.splitContainer1.Panel1Collapsed = true;
      }
      else if (num == 2)
      {
        Globals.MainForm.splitContainer1.Panel2Collapsed = false;
        Globals.MainForm.splitContainer1.Panel1Collapsed = false;
        //this.propertyGrid1.HelpVisible = false;
        //this.propertyGrid1.ToolbarVisible = false;
      }
      this.antPanel_toggleIndex++;
    }

    private Int32 bottom_toggleIndex = 0;
    public void ToggleBottomPanelSplitter(object sender, EventArgs e)
    {
      /*
      int num = this.bottom_toggleIndex % 3;
      if (num == 0)
      {
        this.bottomsplitContainer.Panel2Collapsed = true;
        this.bottomsplitContainer.Panel1Collapsed = false;
      }
      else if (num == 1)
      {
        this.bottomsplitContainer.Panel2Collapsed = false;
        this.bottomsplitContainer.Panel1Collapsed = true;
      }
      else if (num == 2)
      {
        this.bottomsplitContainer.Panel2Collapsed = false;
        this.bottomsplitContainer.Panel1Collapsed = false;
      }
      this.bottom_toggleIndex++;
      */
    }

    private Int32 rightPanel_toggleIndex = 0;
    public void ToggleRightPanelSplitter(object sender, EventArgs e)
    {
      /*
      int num = this.rightPanel_toggleIndex % 3;
      if (num == 0)
      {
        this.rightsplitContainer.Panel2Collapsed = true;
        this.rightsplitContainer.Panel1Collapsed = false;
      }
      else if (num == 1)
      {
        this.rightsplitContainer.Panel2Collapsed = false;
        this.rightsplitContainer.Panel1Collapsed = true;
      }
      else if (num == 2)
      {
        this.rightsplitContainer.Panel2Collapsed = false;
        this.rightsplitContainer.Panel1Collapsed = false;
      }
      this.rightPanel_toggleIndex++;
      */
    }

    public void EditSyntax(object sender, EventArgs e) { }
    public void SelectTheme(object sender, EventArgs e) { }
    public void EditShortcuts(object sender, EventArgs e) { }
    public void ShowArguments(object sender, EventArgs e) { }
    public void BackupSettings(object sender, EventArgs e) { }
    public void ShowSettings(object sender, EventArgs e) { }

    public void ExecuteScript(object sender, EventArgs e) { }

    public void ShowHelp(object sender, EventArgs e)
    {
      this.CallCommand("Browse", "http://www.flashdevelop.org/wikidocs/");
    }

    public void ShowHome(object sender, EventArgs e)
    {
      this.CallCommand("Browse", "http://www.flashdevelop.org/");
    }

    public void CheckUpdates(object sender, EventArgs e) { }

    public void About(object sender, EventArgs e)
    {
      //AboutDialog.Show();
    }

    public void Test(object sender, EventArgs e)
    {
      if (sender != null)
      {
        ToolStripItem button = (ToolStripItem)sender;
        String msg = Globals.AntPanel.menuTree.ProcessVariable(((ItemData)button.Tag).Tag);
        MessageBox.Show(msg, "Testからの送信です");
      }
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
