using AntPanelApplication.Helpers;
//using AntPanelApplication.Helpers;
using AntPanelApplication.Managers;
using AntPanelApplication.Properties;
using AntPlugin.XMLTreeMenu.Controls;
using CommonInterface;
using CommonLibrary;
using CommonLibrary.Controls;
using MDIForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using XMLTreeMenu.Controls;

namespace AntPanelApplication
{
  public partial class Form1 : Form, CommonInterface.IMainForm//, IMessageFilter
  {
    #region Variables
    public global::AntPanelApplication.Properties.Settings
      settings = new global::AntPanelApplication.Properties.Settings();
    global::AntPanelApplication.Properties.Resources
      resources = new global::AntPanelApplication.Properties.Resources();

    //Settings.Default;

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
    #endregion

    #region Constructor
    public Form1()
    {
      Globals.MainForm = this;
      PluginBase.Initialize(this);
      //PluginBase.Initialize(this);
      this.antPanel = new AntPanel();
      if (Arguments.Length > 1 && !String.IsNullOrEmpty(Arguments[0]))
      {
        antPanel.AccessibleDescription = Arguments[0];
      }
      Globals.AntPanel = this.antPanel;
      this.DoubleBuffered = true;
      this.InitializeErrorLog();
      this.InitializeSettings();
      this.InitializeLocalization();

      //if (this.InitializeFirstRun() != DialogResult.Abort)
      //{
      // Suspend layout!
      this.InitializeConfig();
      this.InitializeRendering();
      this.InitializeComponent();
      this.InitializeComponents();
      this.InitializeProcessRunner();
      this.InitializeSmartDialogs();
      this.InitializeMainForm();
      this.InitializeGraphics();
      //Application.AddMessageFilter(this);
      //}
      //else this.Load += new EventHandler(this.MainFormLoaded);
    }
    private void InitializeErrorLog() { }
    private void OnUnhandledException(Object sender, UnhandledExceptionEventArgs e) { }
    private void MainFormLoaded(Object sender, EventArgs e) { }
    #endregion

    #region Private Properties

    // AppMan //
    private FileSystemWatcher amWatcher;

    // Components //
    //private QuickFind quickFind;
    private ToolStrip toolStrip;
    private MenuStrip menuStrip;
    private StatusStrip statusStrip;
    private ToolStripPanel toolStripPanel;
    private ToolStripProgressBar toolStripProgressBar;
    private ToolStripStatusLabel toolStripProgressLabel;
    private ToolStripStatusLabel toolStripStatusLabel;
    private ToolStripButton restartButton;
    //private ProcessRunner processRunner;

    // Dialogs //
    private PrintDialog printDialog;
    private ColorDialog colorDialog;
    private OpenFileDialog openFileDialog;
    private SaveFileDialog saveFileDialog;
    private PrintPreviewDialog printPreviewDialog;
    //private FRInFilesDialog frInFilesDialog;
    //private FRInDocDialog frInDocDialog;
    //private GoToDialog gotoDialog;

    // Settings //
    //private SettingObject appSettings;

    // Context Menus //
    private ContextMenuStrip tabMenu;
    private ContextMenuStrip editorMenu;
    
    /* Settings */
    //private SettingObject appSettings;

    // Working Dir //
    private String workingDirectory = String.Empty;

    // Form State //
    //private FormState formState;
    //private Hashtable fullScreenDocks;
    private Boolean isFullScreen = false;
    private Boolean panelIsActive = false;
    private Boolean savingMultiple = false;
    private Boolean notifyOpenFile = false;
    private Boolean reloadingDocument = false;
    private Boolean processingContents = false;
    private Boolean restoringContents = false;
    private Boolean closingForOpenFile = false;
    private Boolean closingEntirely = false;
    private Boolean closeAllCanceled = false;
    private Boolean restartRequested = false;
    private Boolean refreshConfig = false;
    private Boolean closingAll = false;

    // Singleton //
    public static Boolean Silent;
    public static Boolean IsFirst;
    public static String[] Arguments;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the menu strip
    /// </summary>
    public MenuStrip MenuStrip
    {
      get { return this.menuStrip; }
    }

    /// <summary>
    /// Gets the tool strip
    /// </summary>
    public ToolStrip ToolStrip
    {
      get { return this.toolStrip; }
    }

    /// <summary>
    /// Gets the tool strip panel
    /// </summary>
    public ToolStripPanel ToolStripPanel
    {
      get { return this.toolStripPanel; }
    }

    /// <summary>
    /// Gets the toolStripStatusLabel
    /// </summary>
    public ToolStripStatusLabel StatusLabel
    {
      get { return this.toolStripStatusLabel; }
    }

    /// <summary>
    /// Gets the toolStripProgressLabel
    /// </summary>
    public ToolStripStatusLabel ProgressLabel
    {
      get { return this.toolStripProgressLabel; }
    }

    /// <summary>
    /// Gets the toolStripProgressBar
    /// </summary>
    public ToolStripProgressBar ProgressBar
    {
      get { return this.toolStripProgressBar; }
    }

    /// <summary>
    /// Gets the TabMenu
    /// </summary>
    public ContextMenuStrip TabMenu
    {
      get { return this.tabMenu; }
    }

    /// <summary>
    /// Gets the EditorMenu
    /// </summary>
    public ContextMenuStrip EditorMenu
    {
      get { return this.editorMenu; }
    }

    /// <summary>
    /// Gets the StatusStrip
    /// </summary>
    public StatusStrip StatusStrip
    {
      get { return this.statusStrip; }
    }

    /// <summary>
    /// Gets the IgnoredKeys
    /// </summary>
    public List<Keys> IgnoredKeys
    {
      get { return ShortcutManager.AllShortcuts; }
    }

    /// <summary>
    /// Gets the Settings interface
    /// </summary>
    //public ISettings Settings
    //{
      //get { return this.appSettings; }
    //}

    /// <summary>
    /// Gets or sets the actual Settings
    /// </summary>
    //public SettingObject AppSettings
    //{
    //get { return this.appSettings; }
    //set { this.appSettings = value; }
    //}

    /// <summary>
    /// Gets the CurrentDocument
    /// </summary>
    public Control CurrentDocument
    {
      get
      {
        return ((PageInfo)this.documentTabControl.SelectedTab.Tag).Child;
      }
    }

    public String CurrentDocumentPath
    {
      get
      {
        return this.CurrentDocument.AccessibleName;
      }
    }


    /// <summary>
    /// Is FlashDevelop closing?
    /// </summary>
    public Boolean ClosingEntirely
    {
      get { return this.closingEntirely; }
    }

    /// <summary>
    /// Is this first MainForm instance?
    /// </summary>
    public Boolean IsFirstInstance
    {
      get { return IsFirst; }
    }

    /// <summary>
    /// Is FlashDevelop in multi instance mode?
    /// </summary>
    //public Boolean MultiInstanceMode
    //{
    //get { return Program.MultiInstanceMode; }
    //}

    /// <summary>
    /// Is FlashDevelop in standalone mode?
    /// </summary>
    public Boolean StandaloneMode
    {
      get
      {
        String file = Path.Combine(PathHelper.AppDir, ".local");
        return File.Exists(file);
      }
    }

    /// <summary>
    /// Gets the all available documents
    /// </summary> 
       // <summary>
    // Gets the all available documents
    // </summary> 
    public Control[] Documents
    {
      get
      {
        return TabPageManager.GetDocuments().ToArray();
      }
    }

    public Boolean IsEditable
    {
      get
      {
        if (this.CurrentDocument is RichTextEditor) return true;
        else return false;
      }
    }


    //public ITabbedDocument[] Documents
    //{
    //get
    //{
    //List<ITabbedDocument> documents = new List<ITabbedDocument>();
    //foreach (DockPane pane in DockPanel.Panes)
    //{
    //if (pane.DockState == DockState.Document)
    //{
    //foreach (IDockContent content in pane.Contents)
    //{
    //if (content is TabbedDocument)
    //{
    //documents.Add(content as TabbedDocument);
    //}
    //}
    //}
    //}
    //return documents.ToArray();
    //}
    //}

    /// <summary>
    /// Does FlashDevelop hold modified documents?
    /// </summary> 
    //public Boolean HasModifiedDocuments
    //{
    //get
    //{
    //foreach (ITabbedDocument document in this.Documents)
    //{
    //if (document.IsModified) return true;
    //}
    //return false;
    //}
    //}

    /// <summary>
    /// Gets or sets the WorkingDirectory
    /// </summary>
    public String WorkingDirectory
    {
      get
      {
        if (!Directory.Exists(this.workingDirectory))
        {
          this.workingDirectory = GetWorkingDirectory();
        }
        return this.workingDirectory;
      }
      set { this.workingDirectory = value; }
    }

    /// <summary>
    /// Gets or sets the ProcessIsRunning
    /// </summary>
    //public Boolean ProcessIsRunning
    //{
    //get { return this.processRunner.IsRunning; }
    //}

    /// <summary>
    /// Gets the panelIsActive
    /// </summary>
    public Boolean PanelIsActive
    {
      get { return this.panelIsActive; }
    }

    /// <summary>
    /// Gets the isFullScreen
    /// </summary>
    public Boolean IsFullScreen
    {
      get { return this.isFullScreen; }
    }

    /// <summary>
    /// Gets or sets the ReloadingDocument
    /// </summary>
    public Boolean ReloadingDocument
    {
      get { return this.reloadingDocument; }
      set { this.reloadingDocument = value; }
    }

    /// <summary>
    /// Gets or sets the CloseAllCanceled
    /// </summary>
    public Boolean CloseAllCanceled
    {
      get { return this.closeAllCanceled; }
      set { this.closeAllCanceled = value; }
    }

    /// <summary>
    /// Gets or sets the ProcessingContents
    /// </summary>
    public Boolean ProcessingContents
    {
      get { return this.processingContents; }
      set { this.processingContents = value; }
    }

    /// <summary>
    /// Gets or sets the RestoringContents
    /// </summary>
    public Boolean RestoringContents
    {
      get { return this.restoringContents; }
      set { this.restoringContents = value; }
    }

    /// <summary>
    /// Gets or sets the SavingMultiple
    /// </summary>
    public Boolean SavingMultiple
    {
      get { return this.savingMultiple; }
      set { this.savingMultiple = value; }
    }

    /// <summary>
    /// Gets or sets the RestartRequested
    /// </summary>
    public Boolean RestartRequested
    {
      get { return this.restartRequested; }
      set { this.restartRequested = value; }
    }

    /// <summary>
    /// Gets or sets the RefreshConfig
    /// </summary>
    public Boolean RefreshConfig
    {
      get { return this.refreshConfig; }
      set { this.refreshConfig = value; }
    }

    /// <summary>
    /// Gets the application start args
    /// </summary>
    public String[] StartArguments
    {
      get { return Arguments; }
    }

    /// <summary>
    /// Gets the application custom args
    /// </summary>
    //public List<Argument> CustomArguments
    //{
    // get { return ArgumentDialog.CustomArguments; }
    //}

    /// <summary>
    /// Gets the application's version
    /// </summary>
    public new String ProductVersion
    {
      get { return Application.ProductVersion; }
    }

    /// <summary>
    /// Gets the full human readable version string
    /// </summary>
    public new String ProductName
    {
      get { return Application.ProductName; }
    }

    /// <summary>
    /// Gets the command prompt executable (custom or cmd.exe by default).
    /// </summary>
    public string CommandPromptExecutable
    {
      get
      {
        //if (!String.IsNullOrEmpty(Settings.CustomCommandPrompt) && File.Exists(Settings.CustomCommandPrompt))
        //  return Settings.CustomCommandPrompt;
        return "cmd.exe";
      }
    }

    /// <summary>
    /// Gets the version of the operating system
    /// </summary>
    public Version OSVersion
    {
      get { return Environment.OSVersion.Version; }
    }

    #endregion

    #region Component Creation
    private void CreateTabPages()
    {
    }

    /// <summary>
    /// Creates a new custom document
    /// </summary>
    public void CreateCustomDocument(String tagStr)
    {
      //MessageBox.Show(tagStr);return;
      String name = String.Empty;
      String args = String.Empty;
      if (!String.IsNullOrEmpty(tagStr) )
      {
        name = tagStr.Split('|')[0].ToLower();
        args = tagStr.Split('|')[1];
        OpenCustomDocument(name, args);
      }
    }

    public Control OpenCustomDocument(String name, String argStr,Boolean singleton=true)
    {
      String alignment = "Top";
      String[] args = null;// argStr.Split('|');
      if (argStr.IndexOf('#')>-1)
      {
        args = argStr.Split('#')[0].Split('|');
        alignment = argStr.Split('#')[1];
      }
      else args = argStr.Split('|');

      if (IsRunningUnix)
      {
        for (int i = 0; i < args.Length; i++)
        {
          args[i] = args[i].Replace(@"F:\VirtualBox\ShareFolder\", "/media/sf_ShareFolder/").Replace("\\", "/");
        }
      }
      try
      {
        Control control = null;
        if (name.ToLower() == "browser")
        {
          if (IsRunningUnix)
          {
            for (int i = 0; i < args.Length; i++)
            {
              System.Diagnostics.Process.Start("/usr/bin/google-chrome", args[i]);
            }
          }
          else
          {
            for (int i = 0; i < args.Length; i++)
            {
              //MessageBox.Show(args[i]);
              BrowserEx browser = new BrowserEx();
              browser.AccessibleDescription = args[i];
              browser.Dock = DockStyle.Fill;
              browser.AccessibleDescription = args[i];
              //(String)((Control)browser.Tag).Tag  = args[i];
              String url = ((Control)browser.Tag).Tag as String;
              url = args[i];
              TabPageManager.AddTabPage((Control)browser, this.documentTabControl);
              WebBrowser br = browser.Tag as WebBrowser;
              br.Navigate(args[i]);
            }
          }
          return control;
        }
        if (IsRunningUnix)
        {
          for (int i = 0; i < args.Length; i++)
          {
            switch (name.ToLower())
            {
              case "playerpanel":
                if (File.Exists(args[i]) && (Lib.IsVideoFile(args[i]) || Lib.IsSoundFile(args[i])))
                {
                  Process.Start("/usr/bin/totem", args[i]);
                }
                return control;
              case "azukieditor":
                return control;
            }
          }
        }
        for (int i = 0; i < args.Length; i++)
        {
           control = this.CreateCustomControl(name, args[i]);
          if (control != null)
          {
            control.Dock = DockStyle.Fill;
            //Control tg = control.Tag as Control;
            //MessageBox.Show(tg.GetType().Name);
            ((Control)control.Tag).Tag = args[i];
            if (name.ToLower() == "browserex" && IsRunningUnix)
            {
              Process.Start("/usr/bin/google-chrome", args[i]);
              return control;
            }

            TabControl tbctrl = this.documentTabControl;
            if (alignment == "Bottom") tbctrl = this.bottomTabControl;
            else if (alignment == "Right") tbctrl = this.rightTabControl;
            else if (alignment == "Left") tbctrl = Globals.AntPanel.antPanelTabControl;

            control.AccessibleName = args[i];
            //TabPageManager.AddTabPage(control, this.documentTabControl,singleton);
            TabPageManager.AddTabPage(control, tbctrl, singleton);
          }
        }
        return control;
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()),
          "OpenCustomDocument(String name, String args)");
        return null;
      }
    }
    public Control CreateCustomControl(string path, string file, string option = "")
    {
      //MessageBox.Show(path);
      Control result;
      Assembly assembly = null;
      String dlldir = Path.Combine(PathHelper.BaseDir, "DockableControls");
      String dllpath = Application.ExecutablePath;//String.Empty;
      String classname = String.Empty;
      Type type = null;
      try
      {
        Control control = null;
        //switch (Path.GetFileNameWithoutExtension(path).ToLower())
        switch (path.ToLower())
        {
          case "picturepanel":
            PicturePanel picturePanel = new PicturePanel();
            control = picturePanel as Control;
            classname = control.GetType().FullName;
            break;
          case "richtexteditor":
            control = new RichTextEditor() as Control;
            classname = control.GetType().FullName;
            break;
          case "playerpanel":
            if (IsRunningUnix) return null;
            control = new PlayerPanel() as Control;
            classname = control.GetType().FullName;
            break;
          case "browserex":
            if (IsRunningUnix) return null;
            control = new BrowserEx();
            classname = control.GetType().FullName;
            break;
          case "simplepanel":
            if (IsRunningUnix) return null;
            control = new SimplePanel();
            classname = control.GetType().FullName;
            break;
          //case "reogridpanel":
            //reoGridPanel.PreviousDocuments = this.settings.PreviousSpreadSheetDocuments;
            //reoGridPanel.Instance.PreviousDocuments = this.settings.PreviousSpreadSheetDocuments;
            //break;
          //case "openglpanel":
          //control = new XMLTreeMenu.Controls.OpenGLPanel() as Control;
          //control = new OpenGLPanel() as Control;
          //break;
          //case "HTMLEditor":
          //HTMLEditor htmlEditor = new HTMLEditor();
          //htmlEditor.PreviousDocuments = this.settings.PreviousHTMLEditorDocuments;
          //htmlEditor.Instance.PreviousDocuments = this.settings.PreviousHTMLEditorDocuments;
          //control = htmlEditor as Control;
          //MessageBox.Show(name);
          //break;
          //case "TreeGridView":
          //TreeGridViewPanel treeGridView = new TreeGridViewPanel();
          //treeGridViewPanel.PreviousDocuments = this.settings.PreviousTreeGridViewDocuments;
          //treeGridView.Instance.PreviousDocuments = this.settings.PreviousTreeGridViewPanelDocuments;
          //control = treeGridView as Control;
          //break;
          //case "JsonViewer":
          //JsonView jsonViewer = new JsonView();
          //treeGridViewPanel.PreviousDocuments = this.settings.PreviousTreeGridViewDocuments;
          //treeGridView.Instance.PreviousDocuments = this.settings.PreviousTreeGridViewPanelDocuments;
          //control = jsonViewer as Control;
          //break;
          //case "reogridpanel":
          default:
            // UVIXでは戻る 追加 Time-stamp: <2019-04-07 13:09:41 kahata>
            if (IsRunningUnix) return null;
            try
            { 
              if(path.IndexOf("@")>-1)
              {
                classname = path.Split('@')[0];
                dllpath = path.Split('@')[1];
              }
              else
              {
                dllpath = Path.Combine(dlldir, Path.GetFileNameWithoutExtension(path) + ".dll");
                classname = "CommonControl." + Path.GetFileNameWithoutExtension(path);
              }
              assembly = Assembly.LoadFrom(dllpath);
              //foreach (Type pluginType in assembly.GetTypes()) MessageBox.Show(pluginType.FullName);
              type = assembly.GetType(classname);
              control = (Control)Activator.CreateInstance(type);
            }
            catch (Exception exc)
            {
              MessageBox.Show(Lib.OutputError(exc.Message.ToString()),
                MethodBase.GetCurrentMethod().Name);
            }
            break;
        }
        control.Name = Path.GetFileName(file);
        control.Dock = DockStyle.Fill;
        try
        {
          // 例外発生
          if (control is MDIForm.IMDIForm)
          {
            //this.InitializeCustomControlsInterface((IMDIForm)control);
            //Console.WriteLine("実装してる！");
          }
        }
        catch (Exception ex)
        {
          string errmsg = Lib.OutputError(ex.Message.ToString());
          MessageBox.Show(errmsg, MethodBase.GetCurrentMethod().Name);
        }
        control.Name = classname + "@" + dllpath;
        //control.AccessibleDescription = option;
        control.AccessibleDescription = classname + "@" + dllpath;
        control.AccessibleName = file;
        control.AccessibleDefaultActionDescription = this.GetType().FullName + "@" + Application.ExecutablePath;
        ((Control)control.Tag).Tag = file;
        StatusStrip statusStrip = (StatusStrip)Lib.FindChildControlByType(control, "StatusStrip");
        if (statusStrip != null)
        {
          statusStrip.Tag = this.StatusStrip;
        }
        MenuStrip menuStrip = (MenuStrip)Lib.FindChildControlByType(control, "MenuStrip");
        if (menuStrip != null)
        {
          menuStrip.Tag = this.MenuStrip;
        }
        ToolStrip toolStrip = (ToolStrip)Lib.FindChildControlByType(control, "ToolStrip");
        if (toolStrip != null)
        {
          toolStrip.Tag = this.ToolStrip;
        }
        result = control;
      }
      catch (Exception ex2)
      {
        String errMsg = Lib.OutputError(ex2.Message.ToString());
        MessageBox.Show(errMsg, MethodBase.GetCurrentMethod().Name);
        result = null;
      }
      return result;
    }

    public void OpenDocument(String tagStr,Boolean singleton=true)
    {
      String name = String.Empty;
      String[] args = null;
      if (tagStr.IndexOf(';') > -1)
      {
        name = tagStr.Split(';')[0];
        args = tagStr.Split(';')[1].Split('|');
      }
      else args = tagStr.Split('|');
      for (int i = 0; i < args.Length; i++)
      {
        EncodingFileInfo info = FileHelper.GetEncodingFileInfo(args[i]);
        FileStream fileStream = new FileStream(args[i], FileMode.Open, FileAccess.Read);
        byte[] array = new byte[fileStream.Length];
        fileStream.Read(array, 0, array.Length);
        fileStream.Close();
        Encoding encoding = StringHandler.GetCode(array);

        if(Path.GetExtension(args[i]) == ".fdp")
        {
          Globals.AntPanel.InitializeTreeView(args[i]);
          Globals.AntPanel.dirTreePanel.projectButton.PerformClick();
        }

        if (IsRunningUnix)
        {
          args[i] = args[i].Replace("\\", "/").Replace("F:/VortualBox/ShareFolder/", "/media/sf_ShareFolder/");
          if (Lib.IsImageFile(args[i])) this.OpenCustomDocument("/usr/bin/eog", args[i]);
          else if (Lib.IsWebSite(args[i])) this.OpenCustomDocument("/usr/bin/google-chrome", args[i]);
          else if (Lib.IsSoundFile(args[i]) || Lib.IsVideoFile(args[i]))
          {
            this.OpenCustomDocument("totem", args[i]);
          }
          else if (info.CodePage == -1)
          {
            Process.Start(args[i]);
          }
          else
          {
            this.OpenCustomDocument(this.settings.DefaultEditor, args[i],singleton);
          }
        }
        else
        {
          if (Lib.IsImageFile(args[i])) this.OpenCustomDocument("PicturePanel", args[i]);
          else if (Lib.IsWebSite(args[i])) this.OpenCustomDocument("BroeserEx", args[i]);
          else if (Lib.IsSoundFile(args[i]) || Lib.IsVideoFile(args[i]))
          {
            this.OpenCustomDocument("PlayerPanel", args[i]);
          }
          else if (info.CodePage == -1)
          {
            Process.Start(args[i]);
          }
          else
          {
            this.OpenCustomDocument(this.settings.DefaultEditor, args[i], singleton);
          }
        }
      }
    }

    /*
    /// <summary>
    /// Creates a new empty document
    /// </summary>
    public DockContent CreateEditableDocument(String file, String text, Int32 codepage)
    {
      try
      {
        this.notifyOpenFile = true;
        TabbedDocument tabbedDocument = new TabbedDocument();
        tabbedDocument.Closing += new System.ComponentModel.CancelEventHandler(this.OnDocumentClosing);
        tabbedDocument.Closed += new System.EventHandler(this.OnDocumentClosed);
        tabbedDocument.TabPageContextMenuStrip = this.tabMenu;
        tabbedDocument.ContextMenuStrip = this.editorMenu;
        tabbedDocument.Text = Path.GetFileName(file);
        tabbedDocument.AddEditorControls(file, text, codepage);
        tabbedDocument.Show();
        return tabbedDocument;
      }
      catch (Exception ex)
      {
        ErrorManager.ShowError(ex);
        return null;
      }
    }
    */
    /// <summary>
    /// Creates a floating panel for the plugin
    /// </summary>
    //public DockContent CreateDockablePanel(Control ctrl, String guid, Image image, DockState defaultDockState)
    //public Panel CreateDockablePanel(Control ctrl, String guid, Image image,
    //  System.Windows.Forms.DockStyle defaultDockState)
    public void CreateDockableTabPage(Control ctrl, String guid, Image image, String defaultDockState)
    {
      try
      {
        ctrl.Dock = DockStyle.Fill;
        ctrl.Name = guid;
        //((Control)control.Tag).Tag = args[i];
        TabControl tbctrl = this.documentTabControl;
        if (defaultDockState  == "DockBottom") tbctrl = this.bottomTabControl;
        else if (defaultDockState == "DockRight") tbctrl = this.rightTabControl;
        else if (defaultDockState == "DockLeft") tbctrl = Globals.AntPanel.antPanelTabControl;
        TabPageManager.AddTabPage(ctrl, tbctrl, true);
        /*
        Panel dockablePanel = new Panel();
        //DockablePanel dockablePanel = new DockablePanel(ctrl, guid);
        //dockablePanel.Image = image;
        dockablePanel.Name = guid;
        dockablePanel.Dock = defaultDockState;
        dockablePanel.Controls.Add(ctrl);
        //LayoutManager.PluginPanels.Add(dockablePanel);
        return dockablePanel;
        */

      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()), "CreateDockablePanel");
      }
    }

    /// <summary>
    /// Opens the specified file and creates a editable document
    /// </summary>
    public Control OpenEditableDocument(String org, Encoding encoding, Boolean restorePosition)
    {
      //DockContent createdDoc;
      Control createdDoc = null;
      EncodingFileInfo info;
      //FIXME
      String file = org;// PathHelper.GetPhysicalPathName(org);
      TextEvent te = new TextEvent(EventType.FileOpening, file);
      EventManager.DispatchEvent(this, te);
      if (te.Handled)
      {
        /*
        if (this.Documents.Length == 0)
        {
          this.SmartNew(null, null);
          return null;
        }
        else return null;
        */
      }
      else if (file.EndsWith(".delete.fdz"))
      {
        //this.CallCommand("RemoveZip", file);
        return null;
      }
      else if (file.EndsWith(".fdz"))
      {
        /*
        //this.CallCommand("ExtractZip", file);
        if (file.IndexOf("theme", StringComparison.OrdinalIgnoreCase) != -1)
        {
          
          String currentTheme = Path.Combine(PathHelper.ThemesDir, "CURRENT");
          if (File.Exists(currentTheme))
          {
            ThemeManager.LoadTheme(currentTheme);
            ThemeManager.WalkControls(this);
            this.RefreshSciConfig();
          }
        }
        return null;
        */
      }
      //OK
      //MessageBox.Show(this.Documents.Length.ToString());
      /*
       * TabPageManager AddYabPageで処理
      try
      {
        foreach (ITabbedDocument doc in this.Documents)
        {
          if (doc.IsEditable && doc.FileName.ToUpper() == file.ToUpper())
          {
            doc.Activate();
            return doc as DockContent;
          }
        }
      }
      catch { }
      */
      if (encoding == null)
      {
        info = FileHelper.GetEncodingFileInfo(file);
        if (info.CodePage == -1)
        {
          //MessageBox.Show(file);
          if (Lib.IsEditable(file)) return this.OpenCustomDocument(this.settings.DefaultEditor, file);
          else if (Lib.IsImageFile(file)) return this.OpenCustomDocument("PicturePanel", file);
          else if (Lib.IsWebSite(file)) return this.OpenCustomDocument("BroeserEx", file);
          else if (Lib.IsSoundFile(file) || Lib.IsVideoFile(file))
          {
            this.OpenCustomDocument("PlayerPanel", file);
          }
          else Process.Start(file);
          return null; // If the file is locked, stop.
        }
      }
      else
      {
        info = FileHelper.GetEncodingFileInfo(file);
        if (info.CodePage == -1) return null; // If the file is locked, stop.
        info.Contents = FileHelper.ReadFile(file, encoding);
        info.CodePage = encoding.CodePage;
      }
      DataEvent de = new DataEvent(EventType.FileDecode, file, null);
      EventManager.DispatchEvent(this, de); // Lets ask if a plugin wants to decode the data..

      if (de.Handled)
      {
        info.Contents = de.Data as String;
        info.CodePage = Encoding.UTF8.CodePage; // assume plugin always return UTF8
      }

      try
      {
        //MessageBox.Show(this.CurrentDocumentPath);
        //Boolean CurrentDocument_IsUntitled = File.Exists(this.CurrentDocumentPath));  
        /*
        if (this.CurrentDocument != null && this.CurrentDocument.IsUntitled && !this.CurrentDocument.IsModified && this.Documents.Length == 1)
        {
          this.closingForOpenFile = true;
          this.CurrentDocument.Close();
          this.closingForOpenFile = false;
          createdDoc = this.CreateEditableDocument(file, info.Contents, info.CodePage);
        }
        else createdDoc = this.CreateEditableDocument(file, info.Contents, info.CodePage);
        */
        //ButtonManager.AddNewReopenMenuItem(file);
        //MessageBox.Show(this.settings.DefaultEditor);
        //createdDoc = OpenCustomDocument(this.settings.DefaultEditor, file);
        createdDoc = OpenCustomDocument("RichTextEditor", file);
      }
      catch
      {
        //createdDoc = this.CreateEditableDocument(file, info.Contents, info.CodePage);
        //ButtonManager.AddNewReopenMenuItem(file);
      }
      /*
      TabbedDocument document = (TabbedDocument)createdDoc;
      document.SciControl.SaveBOM = info.ContainsBOM;
      document.SciControl.BeginInvoke((MethodInvoker)delegate
      {
        if (this.appSettings.RestoreFileStates)
        {
          FileStateManager.ApplyFileState(document, restorePosition);
        }
      });
      */
      //ButtonManager.UpdateFlaggedButtons();
      return createdDoc;
    }

    public Control OpenEditableDocument(String file, Boolean restorePosition)
    {
      return this.OpenEditableDocument(file, null, restorePosition);
    }
    //public DockContent OpenEditableDocument(String file)
    public Control OpenEditableDocument(String file)
    {
      return this.OpenEditableDocument(file, null, true);
    }
    /*
    /// <summary>
    /// Creates a new empty document
    /// </summary>
    public DockContent CreateEditableDocument(String file, String text, Int32 codepage)
    {
      try
      {
        this.notifyOpenFile = true;
        TabbedDocument tabbedDocument = new TabbedDocument();
        tabbedDocument.Closing += new System.ComponentModel.CancelEventHandler(this.OnDocumentClosing);
        tabbedDocument.Closed += new System.EventHandler(this.OnDocumentClosed);
        tabbedDocument.TabPageContextMenuStrip = this.tabMenu;
        tabbedDocument.ContextMenuStrip = this.editorMenu;
        tabbedDocument.Text = Path.GetFileName(file);
        tabbedDocument.AddEditorControls(file, text, codepage);
        tabbedDocument.Show();
        return tabbedDocument;
      }
      catch (Exception ex)
      {
        ErrorManager.ShowError(ex);
        return null;
      }
    }
    */
    #endregion

    #region Construct Components
    private void InitializeGraphics() { }
    private void InitializeConfig() { }
    private void AppManUpdate(Object sender, FileSystemEventArgs e) { }
    private void InitializeRestartButton() { }
    public void InitializeSmartDialogs() { }
    //private DialogResult InitializeFirstRun() { }
    private void InitializeRendering() { }
    private void InitializeSettings() { }
    private void InitializeLocalization() { }
    public void InitializeProcessRunner() { }
    public void CheckForUpdates() { }
    public void InitializeWindow() { }
    public void InitializeMainForm()
    {
      // https://dobon.net/vb/dotnet/control/tabpagehide.html
      //TabPageManagerオブジェクトの作成
      TabPageManager.AddTabControl(this.documentTabControl);
      TabPageManager.AddTabControl(this.rightTabControl);
      TabPageManager.AddTabControl(this.bottomTabControl);

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

      this.LoadRichTextEditor("無題");
      //if(IsRunningWindows) this.LoadPicturePanel(@"F:\VirtualBox\ShareFolder\Picture\DSCN0166.JPG");
      //else if(IsRunningUnix) this.LoadPicturePanel("/media/sf_ShareFolder/Picture/DSCN0166.JPG");
      if (IsRunningUnix) return;
      //this.LoadBrowserEx("http://192.168.0.13/pukiwiki2016/index.php");
      //this.LoadPlayerPanel(@"F:\VirtualBox\ShareFolder\Music\03-Monteverdi.mp3");
      this.LoadSimplePanel(@"F:\c_program\OpenGL\NeHe_1200x900\Lesson05\lesson5.exe");
      // だめ
      //////this.LoadSimplePanel(@"C:\Windows\System32\cmd.exe");

      this.tabPage6.Controls.Clear();
      this.documentTabControl.Controls.Remove(this.tabPage6);
    }

    private void LoadRichTextEditor(String path)
    {
      this.editor = new RichTextEditor();
      this.editor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.editor.AccessibleDescription = "Editor";
      this.editor.AccessibleDefaultActionDescription = this.GetType().FullName + ";" + Application.ExecutablePath;
      this.editor.AccessibleName = path;
      ((Control)this.editor.Tag).AccessibleName = path;
      ((Control)this.editor.Tag).Tag = path;
      TabPageManager.AddTabPage(this.editor, this.documentTabControl);
    }
    private void LoadPicturePanel(String path)
    {
      this.picturePanel = new PicturePanel();
      this.picturePanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.picturePanel.AccessibleDescription = "夏樹東京歓送会";
      ((Control)this.picturePanel.Tag).Tag = path;
      this.picturePanel.AccessibleDefaultActionDescription = this.GetType().FullName + ";" + Application.ExecutablePath;
      this.picturePanel.AccessibleName = path;
      TabPageManager.AddTabPage(this.picturePanel, this.documentTabControl);
    }
    private void LoadBrowserEx(String path)
    {
      // 
      // webBrowser
      // 
      this.browser = new BrowserEx();
      this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.browser.AccessibleDescription = "pukiwiki2016";
      this.browser.AccessibleDefaultActionDescription = this.GetType().FullName + ";" + Application.ExecutablePath;
      this.browser.AccessibleName = path;
      ((Control)this.browser.Tag).Tag = path;
      TabPageManager.AddTabPage(this.browser, this.documentTabControl);
    }
    private void LoadPlayerPanel(String path)
    {
      this.player = new AntPlugin.XMLTreeMenu.Controls.PlayerPanel();
      this.player.Dock = System.Windows.Forms.DockStyle.Fill;
      this.player.AccessibleDescription = "二つのミサ曲";
      this.player.AccessibleDefaultActionDescription = this.GetType().FullName + ";" + Application.ExecutablePath;
      this.player.AccessibleName = path;
      ((Control)this.player.Tag).Tag = path;
      TabPageManager.AddTabPage(this.player, this.documentTabControl);
    }
    private void LoadSimplePanel(String path)
    {
      this.panel = new SimplePanel();
      this.panel.Dock = DockStyle.Fill;
      this.panel.AccessibleDescription = "Lesson5";
      this.panel.AccessibleDefaultActionDescription = this.GetType().FullName + ";" + Application.ExecutablePath;
      this.panel.AccessibleName = path;
      ((Control)this.panel.Tag).Tag = path;
      TabPageManager.AddTabPage(this.panel, this.bottomTabControl);
    }

    private void ApplySettings()
    {
      this.menuStrip.Visible = this.settings.MenuBarVisible;
      this.toolStrip.Visible = this.settings.ToolBarVisible;
      this.statusStrip.Visible = this.settings.StatusBarVisible;

      //this.gradleButton.Image = global::AntPanelApplication.Properties.Resources.gradle;
      //this.gradleButton.Image = Resources.gradle;
    }

    private System.Windows.Forms.ToolStrip toolStrip2;
    //private System.Windows.Forms.StatusStrip statusStrip;
    //private ToolStripPanel toolStripPanel;
    public void InitializeComponents()
    {
      this.Controls.Clear();

      //this.quickFind = new QuickFind();
      //this.dockPanel = new DockPanel();
      this.statusStrip = new StatusStrip();
      this.toolStripPanel = new ToolStripPanel();
      this.menuStrip = StripBarManager.GetMenuStrip(FileNameHelper.MainMenu);
      this.toolStrip = StripBarManager.GetToolStrip(FileNameHelper.ToolBar);
      //
      // toolstrip1
      //
      this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.toolStrip.Location = new System.Drawing.Point(0, 0);
      this.toolStrip.Name = "toolStrip1";
      this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
      this.toolStrip.Size = new System.Drawing.Size(2007, 42);
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

      this.editorMenu = StripBarManager.GetContextMenu(FileNameHelper.ScintillaMenu);
      this.tabMenu = StripBarManager.GetContextMenu(FileNameHelper.TabMenu);
      this.tabMenu.Tag = this.documentTabControl;
      this.tabMenu.Name = "tabMenu";
      //this.tabMenu.Font = this.settings.DefaultFont; 
      this.toolStripStatusLabel = new ToolStripStatusLabel();
      this.toolStripProgressLabel = new ToolStripStatusLabel();
      this.toolStripProgressBar = new ToolStripProgressBar();
      this.printPreviewDialog = new PrintPreviewDialog();
      this.saveFileDialog = new SaveFileDialog();
      this.openFileDialog = new OpenFileDialog();
      this.colorDialog = new ColorDialog();
      this.printDialog = new PrintDialog();
      //
      // toolStripPanel
      //
      this.toolStripPanel.Dock = DockStyle.Top;
      if (Win32.IsRunningOnMono())
      {
        this.toolStripPanel.Controls.Add(this.menuStrip);
        this.toolStripPanel.Controls.Add(this.toolStrip);
      }
      else
      {
        this.toolStripPanel.Controls.Add(this.toolStrip);
        this.toolStripPanel.Controls.Add(this.menuStrip);
      }

      //this.tabMenu.Font = this.settings.DefaultFont;
      //this.toolStrip.Font = this.settings.DefaultFont;
      //this.menuStrip.Font = this.settings.DefaultFont;
      //this.editorMenu.Font = this.settings.DefaultFont;
      this.tabMenu.Font = new Font("Meiryo UI", 12.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
      this.toolStrip.Font = new Font("Meiryo UI", 12.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
      this.menuStrip.Font = new Font("Meiryo UI", 12.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
      this.editorMenu.Font = new Font("Meiryo UI", 12.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

      //this.tabMenu.Renderer = new DockPanelStripRenderer(false);
      //this.editorMenu.Renderer = new DockPanelStripRenderer(false);
      //this.menuStrip.Renderer = new DockPanelStripRenderer(false);
      //this.toolStrip.Renderer = new DockPanelStripRenderer(false);
      //this.toolStrip.Padding = new Padding(0, 1, 0, 0);
      this.toolStrip.Size = new Size(500, 26);
      this.toolStrip.Stretch = true;
      // 
      // openFileDialog
      //
      this.openFileDialog.Title = " " + TextHelper.GetString("Title.OpenFileDialog");
      this.openFileDialog.Filter = TextHelper.GetString("Info.FileDialogFilter") + "|*.*";
      this.openFileDialog.RestoreDirectory = true;
      //
      // colorDialog
      //
      this.colorDialog.FullOpen = true;
      this.colorDialog.ShowHelp = false;
      // 
      // printPreviewDialog
      //
      this.printPreviewDialog.Enabled = true;
      this.printPreviewDialog.Name = "printPreviewDialog";
      this.printPreviewDialog.StartPosition = FormStartPosition.CenterParent;
      this.printPreviewDialog.TransparencyKey = Color.Empty;
      this.printPreviewDialog.Visible = false;
      // 
      // saveFileDialog
      //
      this.saveFileDialog.Title = " " + TextHelper.GetString("Title.SaveFileDialog");
      this.saveFileDialog.Filter = TextHelper.GetString("Info.FileDialogFilter") + "|*.*";
      this.saveFileDialog.RestoreDirectory = true;
      // 
      // dockPanel
      //
      //this.dockPanel.TabIndex = 2;
      //this.dockPanel.DocumentStyle = DocumentStyle.DockingWindow;
      //this.dockPanel.DockWindows[DockState.Document].Controls.Add(this.quickFind);
      //this.dockPanel.Dock = DockStyle.Fill;
      //this.dockPanel.Name = "dockPanel";
      //
      // toolStripStatusLabel
      //
      this.toolStripStatusLabel.Name = "toolStripStatusLabel";
      this.toolStripStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
      this.toolStripStatusLabel.Spring = true;
      //
      // toolStripProgressLabel
      //
      this.toolStripProgressLabel.AutoSize = true;
      this.toolStripProgressLabel.Name = "toolStripProgressLabel";
      this.toolStripProgressLabel.TextAlign = ContentAlignment.MiddleRight;
      this.toolStripProgressLabel.Visible = false;
      //
      // toolStripProgressBar
      //
      this.toolStripProgressBar.Name = "toolStripProgressBar";
      this.toolStripProgressBar.ControlAlign = ContentAlignment.MiddleRight;
      this.toolStripProgressBar.ProgressBar.Width = 120;
      this.toolStripProgressBar.Visible = false;
      // 
      // statusStrip
      //
      this.statusStrip.TabIndex = 3;
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Items.Add(this.toolStripStatusLabel);
      this.statusStrip.Items.Add(this.toolStripProgressLabel);
      this.statusStrip.Items.Add(this.toolStripProgressBar);
      this.statusStrip.Font = this.settings.DefaultFont;
      //this.statusStrip.Renderer = new DockPanelStripRenderer(false);
      this.statusStrip.Stretch = true;
      // 
      // MainForm
      //
      this.AllowDrop = true;
      this.Name = "MainForm";
      this.Text = "AntPanelApplication";
      this.Controls.Add(this.bottomSplitContainer);
      this.Controls.Add(this.toolStripPanel);
      this.Controls.Add(this.statusStrip);
      this.MainMenuStrip = this.menuStrip;
      this.Size = this.settings.WindowSize;
      //this.Font = this.settings.DefaultFont;
      this.Font = new Font("Meiryo UI", 12.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Closing += new CancelEventHandler(this.OnMainFormClosing);
      this.FormClosed += new FormClosedEventHandler(this.OnMainFormClosed);
      this.Activated += new EventHandler(this.OnMainFormActivate);
      this.Shown += new EventHandler(this.OnMainFormShow);
      this.Load += new EventHandler(this.OnMainFormLoad);
      this.LocationChanged += new EventHandler(this.OnMainFormLocationChange);
      this.GotFocus += new EventHandler(this.OnMainFormGotFocus);
      this.Resize += new EventHandler(this.OnMainFormResize);
      //ScintillaManager.ConfigurationLoaded += this.ApplyAllSettings;
    }

    #endregion

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
              this.CreateCustomDocument(evnt.Data.ToString());
              evnt.Handled = true;

              return;
            case "XMLTreeMenu.RunProcessDialog":
              //this.pluginUI.RunProcessDialog();
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.Browse":
              this.OpenCustomDocument("BrowseEx",evnt.Data.ToString());
              evnt.Handled = true;
              return;
            case "XMLTreeMenu.BrowserEx":
              this.OpenCustomDocument("BrowserEx", evnt.Data.ToString());
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
              MessageBox.Show(evnt.Data.ToString(), evnt.Action);
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
      /*
      if (e.Type == EventType.Command)
      {
        string cmd = (e as DataEvent).Action;
        DataEvent evnt = (DataEvent)e;
        string path = String.Empty;
        if (cmd == "ProjectManager.Project")
        {
          if (PluginBase.CurrentProject != null)
            ReadBuildFiles();
          pluginUI.RefreshData();
        }
        // 2017-07-17
        else if (cmd == "Ant.CsOutlineParse")
        {
          pluginUI.CsOutlineParse();
        }
        else if (cmd == "Ant.LoadIn")
        {
          path = PluginBase.MainForm.ProcessArgString(evnt.Data.ToString());
          pluginUI.LoadIn(path);
        }
        else if (cmd == "Ant.LoadOut")
        {
          path = PluginBase.MainForm.ProcessArgString(evnt.Data.ToString());
          pluginUI.LoadOut(path);
        }
        else if (cmd == "Ant.CallCommand")
        {
          string[] tmp = (evnt.Data.ToString()).Split('!');
          string name = tmp[0];
          string arg = tmp.Length > 1 ? tmp[1] : string.Empty;
          this.pluginUI.CallCommand(name, arg);
          evnt.Handled = true;
        }
        else if (cmd == "Ant.MenuCommand")
        {
          string[] tmp2 = (evnt.Data.ToString()).Split('!');
          string name = tmp2[0];
          string arg = tmp2.Length > 1 ? tmp2[1] : string.Empty;
          this.pluginUI.menuTree.CallMenuCommand(name, arg);
          evnt.Handled = true;
        }
        else if (cmd == "Ant.NodeAction")
        {
          ActionManager.NodeAction(evnt.Data.ToString());
          evnt.Handled = true;
        }
        else if (cmd == "Ant.TreeCommand")
        {
          string[] tmp2 = (evnt.Data.ToString()).Split('!');
          string name = tmp2[0];
          string arg = tmp2.Length > 1 ? tmp2[1] : string.Empty;
          this.pluginUI.menuTree.CallPluginCommand(name, arg);
          evnt.Handled = true;
        }
        */

      }
    private void OnMainFormActivate(Object sender, System.EventArgs e) { }
    private void OnMainFormGotFocus(Object sender, System.EventArgs e) { }
    private void OnMainFormShow(Object sender, System.EventArgs e) { }
    private void OnMainFormResize(Object sender, System.EventArgs e)
    {
      try
      {
        this.bottomSplitContainer.SplitterDistance = this.Height - this.settings.DefaultBottomPanelHeight;
        this.rightSplitContainer.SplitterDistance = this.Width - this.settings.DefaultRightPanelWidth;
      }
      catch { }
      if (this.WindowState != FormWindowState.Maximized && this.WindowState != FormWindowState.Minimized)
      {
        // 外す Time-stamp: <2019-03-18 16:20:24 kahata>
        //this.settings.WindowSize = this.Size;
      }
    }
    private void OnMainFormLocationChange(Object sender, System.EventArgs e)
    {
      if (this.WindowState != FormWindowState.Maximized && this.WindowState != FormWindowState.Minimized)
      {
        //this.appSettings.WindowSize = this.Size;
        //this.appSettings.WindowPosition = this.Location;
      }
    }
    private void OnMainFormLoad(Object sender, System.EventArgs e)
    {
      //this.CreateTabPages();
      /**
			* DockPanel events
			*/
      /*
      this.dockPanel.ActivePaneChanged += new EventHandler(this.OnActivePaneChanged);
      this.dockPanel.ActiveContentChanged += new EventHandler(this.OnActiveContentChanged);
      this.dockPanel.ActiveDocumentChanged += new EventHandler(this.OnActiveDocumentChanged);
      this.dockPanel.ContentRemoved += new EventHandler<DockContentEventArgs>(this.OnContentRemoved);
      */
      /**
			* Populate menus and check buttons 
			*/
      //ButtonManager.PopulateReopenMenu();
      //ButtonManager.UpdateFlaggedButtons();
      /**
			* Set the initial directory for file dialogs
			*/
      //String path = this.appSettings.LatestDialogPath;
      //this.openFileDialog.InitialDirectory = path;
      //this.saveFileDialog.InitialDirectory = path;
      //this.workingDirectory = path;
      /**
			* Open document[s] in startup 
			*/
      if (Arguments != null && Arguments.Length != 0)
      {
        //this.ProcessParameters(Arguments);
        //Arguments = null;
        this.OpenDocument(Arguments[0]);
      }
      /*
      else if (this.appSettings.RestoreFileSession)
      {
        String file = FileNameHelper.SessionData;
        SessionManager.RestoreSession(file, SessionType.Startup);
      }
      if (this.Documents.Length == 0)
      {
        NotifyEvent ne = new NotifyEvent(EventType.FileEmpty);
        EventManager.DispatchEvent(this, ne);
        if (!ne.Handled) this.SmartNew(null, null);
      }
      */
      /**
			* Apply the default loaded theme
			*/
      //ThemeManager.WalkControls(this);
      /**
			* Notify plugins that the application is ready
			*/
      EventManager.DispatchEvent(this, new NotifyEvent(EventType.UIStarted));
      EventManager.DispatchEvent(this, new NotifyEvent(EventType.Completion));
      /**
			* Start polling for file changes outside of the editor
			*/
      //FilePollManager.InitializePolling();
      /**
			* Apply all settings to all documents
			*/
      //this.ApplyAllSettings();
      /**
			* Initialize window and continue layout
			*/
      this.InitializeWindow();
      /**
			* Initializes the restart button
			*/
      this.InitializeRestartButton();
      /**
			* Check for updates when needed
			*/
      this.CheckForUpdates();
    }
    public void OnMainFormClosing(Object sender, System.ComponentModel.CancelEventArgs e){ }
    /// <summary>
    /// When form is closed restart if requested.
    /// </summary>
    public void OnMainFormClosed(Object sender, FormClosedEventArgs e)
    {
			if (this.restartRequested)
			{
				this.restartRequested = false;
				Process.Start(Application.ExecutablePath);
				Process.GetCurrentProcess().Kill();
			}
    }

    /// <summary>
    /// When dock changes, applies the padding to documents
    /// </summary>
    private void OnActivePaneChanged(Object sender, EventArgs e)
    {
      //this.quickFind.ApplyFixedDocumentPadding();
    }

    /// <summary>
    /// When document is removed update tab texts
    /// </summary>
    /*
    public void OnContentRemoved(Object sender, DockContentEventArgs e)
    {
      //TabTextManager.UpdateTabTexts();
    }
    */
    /// <summary>
    /// Dispatch UIRefresh event and focus scintilla control
    /// </summary>
    private void OnActiveContentChanged(Object sender, System.EventArgs e)
    {
      /*
      if (this.dockPanel.ActiveContent != null)
			{
				if (this.dockPanel.ActiveContent.GetType() == typeof(TabbedDocument))
				{
					this.panelIsActive = false;
					TabbedDocument document = (TabbedDocument)this.dockPanel.ActiveContent;
					document.Activate();
				}
				else this.panelIsActive = true;
				NotifyEvent ne = new NotifyEvent(EventType.UIRefresh);
				EventManager.DispatchEvent(this, ne);
			}
      */
    }
    /*
    /// <summary>
    /// Updates the UI, tabbing, working directory and the button states. 
    /// Also notifies the plugins for the FileOpen and FileSwitch events.
    /// </summary>
    public void OnActiveDocumentChanged(Object sender, System.EventArgs e)
    {
			try
			{
				if (this.CurrentDocument == null) return;
				this.OnScintillaControlUpdateControl(this.CurrentDocument.SciControl);
				this.quickFind.CanSearch = this.CurrentDocument.IsEditable;
				/ **
				* Bring this newly active document to the top of the tab history
				* unless you're currently cycling through tabs with the keyboard
				* /
				TabbingManager.UpdateSequentialIndex(this.CurrentDocument);
				if (!TabbingManager.TabTimer.Enabled)
				{
					TabbingManager.TabHistory.Remove(this.CurrentDocument);
					TabbingManager.TabHistory.Insert(0, this.CurrentDocument);
				}
				if (this.CurrentDocument.IsEditable)
				{
					/ **
					* Apply correct extension when saving
					* /
      if (this.appSettings.ApplyFileExtension)
      {
        String extension = Path.GetExtension(this.CurrentDocument.FileName);
        if (extension != "") this.saveFileDialog.DefaultExt = extension;
      }
      / **
      * Set current working directory
      * /
      String path = Path.GetDirectoryName(this.CurrentDocument.FileName);
      if (!this.CurrentDocument.IsUntitled && Directory.Exists(path))
      {
        this.workingDirectory = path;
      }
      / **
      * Checks the file changes
      * /
      TabbedDocument document = (TabbedDocument)this.CurrentDocument;
      document.Activate();
      / **
      * Processes the opened file
      * /
      if (this.notifyOpenFile)
      {
        ScintillaManager.UpdateControlSyntax(this.CurrentDocument.SciControl);
        if (File.Exists(this.CurrentDocument.FileName))
        {
          TextEvent te = new TextEvent(EventType.FileOpen, this.CurrentDocument.FileName);
          EventManager.DispatchEvent(this, te);
        }
        this.notifyOpenFile = false;
      }
    }
    TabTextManager.UpdateTabTexts();
				NotifyEvent ne = new NotifyEvent(EventType.FileSwitch);
    EventManager.DispatchEvent(this, ne);
    NotifyEvent ce = new NotifyEvent(EventType.Completion);
    EventManager.DispatchEvent(this, ce);
    }
			catch (Exception ex)
			{
				ErrorManager.ShowError(ex);
			}
		}
}
/// <summary>
/// Checks that if the are any modified documents when closing.
/// </summary>
public void OnDocumentClosing(Object sender, System.ComponentModel.CancelEventArgs e)
{
  ITabbedDocument document = (ITabbedDocument)sender;
  if (this.closeAllCanceled && this.closingAll) e.Cancel = true;
  else if (document.IsModified)
  {
    String saveChanges = TextHelper.GetString("Info.SaveChanges");
    String saveChangesTitle = TextHelper.GetString("Title.SaveChanges");
    DialogResult result = MessageBox.Show(this, saveChanges, saveChangesTitle + " " + document.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
    if (result == DialogResult.Yes)
    {
      if (document.IsUntitled)
      {
        this.saveFileDialog.FileName = document.FileName;
        if (this.saveFileDialog.ShowDialog(this) == DialogResult.OK && this.saveFileDialog.FileName.Length != 0)
        {
          ButtonManager.AddNewReopenMenuItem(this.saveFileDialog.FileName);
          document.Save(this.saveFileDialog.FileName);
        }
        else
        {
          if (this.closingAll) this.closeAllCanceled = true;
          e.Cancel = true;
        }
      }
      else if (document.IsModified) document.Save();
    }
    else if (result == DialogResult.Cancel)
    {
      if (this.closingAll) this.closeAllCanceled = true;
      e.Cancel = true;
    }
    else if (result == DialogResult.No)
    {
      RecoveryManager.RemoveTemporaryFile(document.FileName);
    }
  }
  if (this.Documents.Length == 1 && document.IsUntitled && !document.IsModified && document.SciControl.Length == 0 && !e.Cancel && !this.closingForOpenFile && !this.restoringContents)
  {
    e.Cancel = true;
  }
  if (this.Documents.Length == 1 && !e.Cancel && !this.closingForOpenFile && !this.closingEntirely && !this.restoringContents)
  {
    NotifyEvent ne = new NotifyEvent(EventType.FileEmpty);
    EventManager.DispatchEvent(this, ne);
    if (!ne.Handled) this.SmartNew(null, null);
  }
}

/// <summary>
/// Activates the previous document when document is closed
/// </summary>
public void OnDocumentClosed(Object sender, System.EventArgs e)
{
  ITabbedDocument document = sender as ITabbedDocument;
  TabbingManager.TabHistory.Remove(document);
  TextEvent ne = new TextEvent(EventType.FileClose, document.FileName);
  EventManager.DispatchEvent(this, ne);
  if (this.appSettings.SequentialTabbing)
  {
    if (TabbingManager.SequentialIndex == 0) this.Documents[0].Activate();
    else TabbingManager.NavigateTabsSequentially(-1);
  }
  else TabbingManager.NavigateTabHistory(0);
  if (document.IsEditable && !document.IsUntitled)
  {
    if (this.appSettings.RestoreFileStates) FileStateManager.SaveFileState(document);
    RecoveryManager.RemoveTemporaryFile(document.FileName);
    OldTabsManager.SaveOldTabDocument(document.FileName);
  }
  ButtonManager.UpdateFlaggedButtons();
}

/// <summary>
/// Refreshes the statusbar display and updates the important edit buttons
/// </summary>
public void OnScintillaControlUpdateControl(ScintillaControl sci)
{
  if (this.InvokeRequired)
  {
    this.BeginInvoke((MethodInvoker)delegate { this.OnScintillaControlUpdateControl(sci); });
    return;
  }
  ITabbedDocument document = DocumentManager.FindDocument(sci);
  if (sci != null && document != null && document.IsEditable)
  {
    String statusText = " " + TextHelper.GetString("Info.StatusText");
    String line = sci.CurrentLine + 1 + " / " + sci.LineCount;
    String column = sci.Column(sci.CurrentPos) + 1 + " / " + (sci.Column(sci.LineEndPosition(sci.CurrentLine)) + 1);
    var oldOS = this.OSVersion.Major < 6; // Vista is 6.0 and ok...
    String file = oldOS ? PathHelper.GetCompactPath(sci.FileName) : sci.FileName;
    String eol = (sci.EOLMode == 0) ? "CR+LF" : ((sci.EOLMode == 1) ? "CR" : "LF");
    String encoding = ButtonManager.GetActiveEncodingName();
    this.toolStripStatusLabel.Text = String.Format(statusText, line, column, eol, encoding, file);
  }
  else this.toolStripStatusLabel.Text = " ";
  this.OnUpdateMainFormDialogTitle();
  ButtonManager.UpdateFlaggedButtons();
  NotifyEvent ne = new NotifyEvent(EventType.UIRefresh);
  EventManager.DispatchEvent(this, ne);
}

/// <summary>
/// Opens the selected files on drop
/// </summary>
public void OnScintillaControlDropFiles(ScintillaControl sci, String data)
{
  if (this.InvokeRequired)
  {
    this.BeginInvoke((MethodInvoker)delegate { this.OnScintillaControlDropFiles(null, data); });
    return;
  }
  String[] files = Regex.Split(data.Substring(1, data.Length - 2), "\" \"");
  foreach (String file in files)
  {
    if (File.Exists(file))
    {
      DockContent doc = this.OpenEditableDocument(file);
      if (doc == null || Control.ModifierKeys == Keys.Control) return;
      DockContent drop = DocumentManager.FindDocument(sci) as DockContent;
      if (drop != null && drop.Pane != null)
      {
        doc.DockTo(drop.Pane, DockStyle.Fill, -1);
        doc.Activate();
      }
    }
  }
}

/// <summary>
/// Notifies when the user is trying to modify a read only file
/// </summary>
public void OnScintillaControlModifyRO(ScintillaControl sci)
{
  if (!sci.Enabled || !File.Exists(sci.FileName)) return;
  TextEvent te = new TextEvent(EventType.FileModifyRO, sci.FileName);
  EventManager.DispatchEvent(this, te);
  if (te.Handled) return; // Let plugin handle this...
  String dlgTitle = TextHelper.GetString("Title.ConfirmDialog");
  String message = TextHelper.GetString("Info.MakeReadOnlyWritable");
  if (MessageBox.Show(this, message, dlgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
  {
    ScintillaManager.MakeFileWritable(sci);
  }
}

/// <summary>
/// Handles the modified event
/// </summary>
public void OnScintillaControlModified(ScintillaControl sender, Int32 pos, Int32 modType, String text, Int32 length, Int32 lAdded, Int32 line, Int32 fLevelNow, Int32 fLevelPrev)
{
  ITabbedDocument document = DocumentManager.FindDocument(sender);
  if (document != null && document.IsEditable)
  {
    this.OnDocumentModify(document);
    if (this.appSettings.ViewModifiedLines)
    {
      Int32 flags = sender.ModEventMask;
      sender.ModEventMask = flags & ~(Int32)ScintillaNet.Enums.ModificationFlags.ChangeMarker;
      Int32 modLine = sender.LineFromPosition(pos);
      sender.MarkerAdd(modLine, 2);
      for (Int32 i = 0; i <= lAdded; i++)
      {
        sender.MarkerAdd(modLine + i, 2);
      }
      sender.ModEventMask = flags;
    }
  }
}

/// <summary>
/// Provides a basic folding service and notifies the plugins for the MarginClick event
/// </summary>
public void OnScintillaControlMarginClick(ScintillaControl sci, Int32 modifiers, Int32 position, Int32 margin)
{
  if (margin == 2)
  {
    Int32 line = sci.LineFromPosition(position);
    if (Control.ModifierKeys == Keys.Control) MarkerManager.ToggleMarker(sci, 0, line);
    else sci.ToggleFold(line);
  }
}

/// <summary>
/// Handles the mouse wheel on hover
/// </summary>
public Boolean PreFilterMessage(ref Message m)
{
  if (Win32.ShouldUseWin32() && m.Msg == 0x20a) // WM_MOUSEWHEEL
  {
    Int32 x = unchecked((short)(long)m.LParam);
    Int32 y = unchecked((short)((long)m.LParam >> 16));
    IntPtr hWnd = Win32.WindowFromPoint(new Point(x, y));
    if (hWnd != IntPtr.Zero)
    {
      ITabbedDocument doc = Globals.CurrentDocument;
      if (Control.FromHandle(hWnd) != null)
      {
        Win32.SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
        return true;
      }
      else if (doc != null && doc.IsEditable && (hWnd == doc.SplitSci1.HandleSci || hWnd == doc.SplitSci2.HandleSci))
      {
        Win32.SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
        return true;
      }
    }
  }
 
  return false;
}

/// <summary>
/// Handles the application shortcuts
/// </summary>
protected override Boolean ProcessCmdKey(ref Message msg, Keys keyData)
{
  / **
  * Notify plugins. Don't notify ControlKey or ShiftKey as it polls a lot
  * /
  KeyEvent ke = new KeyEvent(EventType.Keys, keyData);
  Keys keyCode = keyData & Keys.KeyCode;
  if ((keyCode != Keys.ControlKey) && (keyCode != Keys.ShiftKey))
  {
    EventManager.DispatchEvent(this, ke);
  }
  if (!ke.Handled)
  {
    / **
    * Ignore basic control keys if sci doesn't have focus.
    * / 
    if (Globals.SciControl == null || !Globals.SciControl.IsFocus)
    {
      if (keyData == (Keys.Control | Keys.C)) return false;
      else if (keyData == (Keys.Control | Keys.V)) return false;
      else if (keyData == (Keys.Control | Keys.X)) return false;
      else if (keyData == (Keys.Control | Keys.A)) return false;
      else if (keyData == (Keys.Control | Keys.Z)) return false;
      else if (keyData == (Keys.Control | Keys.Y)) return false;
    }
    / **
    * Process special key combinations and allow "chaining" of 
    * Ctrl-Tab commands if you keep holding control down.
    * /
    if ((keyData & Keys.Control) != 0)
    {
      Boolean sequentialTabbing = this.appSettings.SequentialTabbing;
      if ((keyData == (Keys.Control | Keys.Next)) || (keyData == (Keys.Control | Keys.Tab)))
      {
        TabbingManager.TabTimer.Enabled = true;
        if (keyData == (Keys.Control | Keys.Next) || sequentialTabbing)
        {
          TabbingManager.NavigateTabsSequentially(1);
        }
        else TabbingManager.NavigateTabHistory(1);
        return true;
      }
      if ((keyData == (Keys.Control | Keys.Prior)) || (keyData == (Keys.Control | Keys.Shift | Keys.Tab)))
      {
        TabbingManager.TabTimer.Enabled = true;
        if (keyData == (Keys.Control | Keys.Prior) || sequentialTabbing)
        {
          TabbingManager.NavigateTabsSequentially(-1);
        }
        else TabbingManager.NavigateTabHistory(-1);
        return true;
      }
    }
    return base.ProcessCmdKey(ref msg, keyData);
  }
  return true;
}

/// <summary>
/// Notifies the plugins for the SyntaxChange event
/// </summary>
public void OnSyntaxChange(String language)
{
  TextEvent te = new TextEvent(EventType.SyntaxChange, language);
  EventManager.DispatchEvent(this, te);
}

/// <summary>
/// Updates the MainForm's title automatically
/// </summary>
public void OnUpdateMainFormDialogTitle()
{
  IProject project = PluginBase.CurrentProject;
  ITabbedDocument document = this.CurrentDocument;
  if (project != null) this.Text = project.Name + " - " + DistroConfig.DISTRIBUTION_NAME;
  else if (document != null && document.IsEditable)
  {
    String file = Path.GetFileName(document.FileName);
    this.Text = file + " - " + DistroConfig.DISTRIBUTION_NAME;
  }
  else this.Text = DistroConfig.DISTRIBUTION_NAME;
}

/// <summary>
/// Sets the current document unmodified and updates it
/// </summary>
public void OnDocumentReload(ITabbedDocument document)
{
  document.IsModified = false;
  this.reloadingDocument = false;
  this.OnUpdateMainFormDialogTitle();
  if (document.IsEditable) document.SciControl.MarkerDeleteAll(2);
  ButtonManager.UpdateFlaggedButtons();
}

/// <summary>
/// Sets the current document modified
/// </summary>
public void OnDocumentModify(ITabbedDocument document)
{
  if (document.IsEditable && !document.IsModified && !this.reloadingDocument && !this.processingContents)
  {
    document.IsModified = true;
    TextEvent te = new TextEvent(EventType.FileModify, document.FileName);
    EventManager.DispatchEvent(this, te);
  }
}

/// <summary>
/// Notifies the plugins for the FileSave event
/// </summary>
public void OnFileSave(ITabbedDocument document, String oldFile)
{

  if (oldFile != null)
  {
    String args = document.FileName + ";" + oldFile;
    TextEvent rename = new TextEvent(EventType.FileRename, args);
    EventManager.DispatchEvent(this, rename);
    TextEvent open = new TextEvent(EventType.FileOpen, document.FileName);
    EventManager.DispatchEvent(this, open);
  }
  this.OnUpdateMainFormDialogTitle();
  if (document.IsEditable) document.SciControl.MarkerDeleteAll(2);
  TextEvent save = new TextEvent(EventType.FileSave, document.FileName);
  EventManager.DispatchEvent(this, save);
  ButtonManager.UpdateFlaggedButtons();
  TabTextManager.UpdateTabTexts();
 
}
*/

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
    /// Resolves the working directory
    /// </summary>
    public String GetWorkingDirectory()
    {
      /*
      IProject project = PluginBase.CurrentProject;
      ITabbedDocument document = this.CurrentDocument;
      if (document != null && document.IsEditable && File.Exists(document.FileName))
      {
        return Path.GetDirectoryName(document.FileName);
      }
      else if (project != null && File.Exists(project.ProjectPath))
      {
        return Path.GetDirectoryName(project.ProjectPath);
      }
      else return PathHelper.AppDir;
      */
      return PathHelper.AppDir;
    }

    /// <summary>
    /// Finds the specified menu item by name
    /// </summary>
    public ToolStripItem FindMenuItem(String name)
    {
      return StripBarManager.FindMenuItem(name);
    }

    /// <summary>
    /// Finds the menu items that have the specified name
    /// </summary>
    public List<ToolStripItem> FindMenuItems(String name)
    {
      return StripBarManager.FindMenuItems(name);
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

    public String WindowsPath2UnixPath(String path)
    {
      return path.Replace("\\", "/").Replace("F/VirtualBox/ShareFolder", "/media/sf_ShareFolder");
    }

    /// <summary>
    /// Gets the amount instances running
    /// </summary>
    public Int32 GetInstanceCount()
    {
      Process current = Process.GetCurrentProcess();
      return Process.GetProcessesByName(current.ProcessName).Length;
    }

    /// <summary>
    /// Finds the specified plugin
    /// </summary>
    /*
    public IPlugin FindPlugin(String guid)
    {
			AvailablePlugin plugin = PluginServices.Find(guid);
			return plugin.Instance;
      return null;
    }
    */
    /// <summary>
    /// Themes the controls from the parent
    /// </summary>
    public void ThemeControls(Object parent)
    {
      //ThemeManager.WalkControls(parent);
    }

    /// <summary>
    /// Gets a theme property color
    /// </summary>
    public Color GetThemeColor(String id)
    {
      //return ThemeManager.GetThemeColor(id);
      return Color.Wheat;
    }

    /// <summary>
    /// Gets a theme property color with a fallback
    /// </summary>
    public Color GetThemeColor(String id, Color fallback)
    {
      /*
        Color color = ThemeManager.GetThemeColor(id);
        if (color != Color.Empty) return color;
        else return fallback;
      */
      return Color.White;
    }

    /// <summary>
    /// Gets a theme property value
    /// </summary>
    public String GetThemeValue(String id)
    {
      //return ThemeManager.GetThemeValue(id);
      return "";
    }

    /// <summary>
    /// Gets a theme property value with a fallback
    /// </summary>
    public String GetThemeValue(String id, String fallback)
    {
      /*
        String value = ThemeManager.GetThemeValue(id);
        if (!String.IsNullOrEmpty(value)) return value;
        else return fallback;
      */
      return "";
    }

    /// <summary>
    /// Gets a theme flag value.
    /// </summary>
    public Boolean GetThemeFlag(String id)
    {
      //return GetThemeFlag(id, false);
      return true;
		}

    /// <summary>
    /// Gets a theme flag value with a fallback.
    /// </summary>
    public Boolean GetThemeFlag(String id, Boolean fallback)
    {
      /*
			String value = ThemeManager.GetThemeValue(id);
			if (String.IsNullOrEmpty(value)) return fallback;
			switch (value.ToLower())
			{
				case "true": return true;
				case "false": return false;
				default: return fallback;
			}
		*/
      return true;
    }

    /// <summary>
    /// Lets you update menu items using the flag functionality
    /// </summary>
    public void AutoUpdateMenuItem(ToolStripItem item, String action)
    {
      /*
			Boolean value = ButtonManager.ValidateFlagAction(item, action);
			ButtonManager.ExecuteFlagAction(item, action, value);
			*/
    }

    /// <summary>
    /// Gets the specified item's shortcut keys.
    /// </summary>
    public Keys GetShortcutItemKeys(String id)
    {
      /*
			ShortcutItem item = ShortcutManager.GetRegisteredItem(id);
			return item == null ? Keys.None : item.Custom;
			*/
      return Keys.A;
    }

    /// <summary>
    /// Gets the specified item's id.
    /// </summary>
    public String GetShortcutItemId(Keys keys)
    {
      /*
        ShortcutItem item = ShortcutManager.GetRegisteredItem(keys);
        return item == null ? string.Empty : item.Id;
      */
      return "";
    }

    /// <summary>
    /// Registers a new menu item with the shortcut manager
    /// </summary>
    public void RegisterShortcutItem(String id, Keys keys)
    {
      //ShortcutManager.RegisterItem(id, keys);
    }

    /// <summary>
    /// Registers a new menu item with the shortcut manager
    /// </summary>
    public void RegisterShortcutItem(String id, ToolStripMenuItem item)
    {
      //ShortcutManager.RegisterItem(id, item);
    }

    /// <summary>
    /// Registers a new secondary menu item with the shortcut manager
    /// </summary>
    public void RegisterSecondaryItem(String id, ToolStripItem item)
    {
      //ShortcutManager.RegisterSecondaryItem(id, item);
    }

    /// <summary>
    /// Updates a registered secondary menu item in the shortcut manager
    /// - should be called when the tooltip changes.
    /// </summary>
    public void ApplySecondaryShortcut(ToolStripItem item)
    {
      //ShortcutManager.ApplySecondaryShortcut(item);
    }

    /// <summary>
    /// Shows the settings dialog
    /// </summary>
    public void ShowSettingsDialog(String itemName)
    {
      //SettingDialog.Show(itemName, "");
    }
    public void ShowSettingsDialog(String itemName, String filter)
    {
      //SettingDialog.Show(itemName, filter);
    }

    /// <summary>
    /// Shows the error dialog if the sender is ErrorManager
    /// </summary>
    public void ShowErrorDialog(Object sender, Exception ex)
    {
      /*
			if (sender.GetType().ToString() != "PluginCore.Managers.ErrorManager")
			{
				String message = TextHelper.GetString("Info.OnlyErrorManager");
				ErrorDialog.Show(new Exception(message));
			}
			else ErrorDialog.Show(ex);
		*/
    }

    /// <summary>
    /// Show a message to the user to restart FD
    /// </summary>
    public void RestartRequired()
    {
      /*
			if (this.restartButton != null) this.restartButton.Visible = true;
			String message = TextHelper.GetString("Info.RequiresRestart");
			TraceManager.Add(message);
		*/
    }

    /// <summary>
    /// Refreshes the main form
    /// </summary>
    public void RefreshUI()
    {
      /*
			if (this.CurrentDocument == null) return;
			ScintillaControl sci = this.CurrentDocument.SciControl;
			this.OnScintillaControlUpdateControl(sci);
		*/
    }

    /// <summary>
    /// Clears the temporary files from disk
    /// </summary>
    public void ClearTemporaryFiles(String file)
    {
      /*
			RecoveryManager.RemoveTemporaryFile(file);
			FileStateManager.RemoveStateFile(file);
		*/
    }

    /// <summary>
    /// Refreshes the scintilla configuration
    /// </summary>
    public void RefreshSciConfig()
    {
      //ScintillaManager.LoadConfiguration();
    }

    /// <summary>
    /// Processes the incoming arguments 
    /// </summary> 
    public void ProcessParameters(String[] args)
    {
      /*
			if (this.InvokeRequired)
			{
				this.BeginInvoke((MethodInvoker)delegate { this.ProcessParameters(args); });
				return;
			}
			this.Activate(); this.Focus();
			if (args != null && args.Length != 0)
			{
				Silent = Array.IndexOf(args, "-silent") != -1;
				for (Int32 i = 0; i < args.Length; i++)
				{
					OpenDocumentFromParameters(args[i]);
				}
			}
			if (Win32.ShouldUseWin32()) Win32.RestoreWindow(this.Handle);
			// Notify plugins about start arguments
			NotifyEvent ne = new NotifyEvent(EventType.StartArgs);
			EventManager.DispatchEvent(this, ne);
			*/
    }

    /// <summary>
    /// 
    /// </summary>
    private void OpenDocumentFromParameters(String file)
    {
      /*
			Match openParams = Regex.Match(file, "@([0-9]+)($|:([0-9]+)$)"); // path@line:col
			if (openParams.Success)
			{
				file = file.Substring(0, openParams.Index);
				file = PathHelper.GetLongPathName(file);
				if (File.Exists(file))
				{
					TabbedDocument doc = this.OpenEditableDocument(file, false) as TabbedDocument;
					if (doc != null) ApplyOpenParams(openParams, doc.SciControl);
					else if (CurrentDocument.FileName == file) ApplyOpenParams(openParams, CurrentDocument.SciControl);
				}
			}
			else if (File.Exists(file))
			{
				file = PathHelper.GetLongPathName(file);
				this.OpenEditableDocument(file);
			}
			*/
    }

    /// <summary>
    /// 
    /// </summary>		
    ///private void ApplyOpenParams(Match openParams, ScintillaControl sci)
    private void ApplyOpenParams(Match openParams, Control sci)
    {
      /*
			if (sci == null) return;
			Int32 col = 0;
			Int32 line = Math.Min(sci.LineCount - 1, Math.Max(0, Int32.Parse(openParams.Groups[1].Value) - 1));
			if (openParams.Groups.Count > 3 && openParams.Groups[3].Value.Length > 0)
			{
				col = Int32.Parse(openParams.Groups[3].Value);
			}
			Int32 position = sci.PositionFromLine(line) + col;
			sci.SetSel(position, position);
		*/
    }

    /// <summary>
    /// Closes all open documents with an option: exceptCurrent
    /// </summary>
    public void CloseAllDocuments(Boolean exceptCurrent)
    {
      CloseAllDocuments(exceptCurrent, false);
    }
    public void CloseAllDocuments(Boolean exceptCurrent, Boolean exceptOtherPanes)
    {
      /*
			ITabbedDocument current = this.CurrentDocument;
			DockPane currentPane = (current == null) ? null : current.DockHandler.PanelPane;
			this.closeAllCanceled = false; this.closingAll = true;
			var documents = new List<ITabbedDocument>(Documents);
			foreach (var document in documents)
			{
				Boolean close = true;
				if (exceptCurrent && document == current) close = false;
				if (exceptOtherPanes && document.DockHandler.PanelPane != currentPane) close = false;
				if (close) document.Close();
			}
			this.closingAll = false;
			*/
    }

    /// <summary>
    /// Updates all needed settings after modification
    /// </summary>
    public void ApplyAllSettings()
    {
      /*
			if (this.InvokeRequired)
			{
				this.BeginInvoke((MethodInvoker) this.ApplyAllSettings);
				return;
			}
			ShortcutManager.ApplyAllShortcuts();
			EventManager.DispatchEvent(this, new NotifyEvent(EventType.ApplySettings));
			for (Int32 i = 0; i < this.Documents.Length; i++)
			{
				ITabbedDocument document = this.Documents[i];
				if (document.IsEditable)
				{
					ScintillaManager.ApplySciSettings(document.SplitSci1, true);
					ScintillaManager.ApplySciSettings(document.SplitSci2, true);
				}
			}
			this.frInFilesDialog.UpdateSettings();
			this.statusStrip.Visible = this.appSettings.ViewStatusBar;
			this.toolStrip.Visible = this.isFullScreen ? false : this.appSettings.ViewToolBar;
			ButtonManager.UpdateFlaggedButtons();
			TabTextManager.UpdateTabTexts();
		*/
    }

    /// <summary>
    /// Saves all settings of the FlashDevelop
    /// </summary>
    public void SaveAllSettings()
    {
      /*
			try
			{
				this.appSettings.WindowState = this.WindowState;
				this.appSettings.LatestDialogPath = this.workingDirectory;
				if (this.WindowState != FormWindowState.Maximized && this.WindowState != FormWindowState.Minimized)
				{
					this.appSettings.WindowSize = this.Size;
					this.appSettings.WindowPosition = this.Location;
				}
				if (!File.Exists(FileNameHelper.SettingData))
				{
					String folder = Path.GetDirectoryName(FileNameHelper.SettingData);
					if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
				}
				ObjectSerializer.Serialize(FileNameHelper.SettingData, this.appSettings);
				try { this.dockPanel.SaveAsXml(FileNameHelper.LayoutData); }
				catch (Exception ex2)
				{
					// Ignore errors on multi instance full close...
					if (this.MultiInstanceMode && this.ClosingEntirely) return;
					else throw ex2;
				}
			}
			catch (Exception ex)
			{
				ErrorManager.ShowError(ex);
			}
		*/
    }

    /// <summary>
    /// Sets the text to find globally
    /// </summary>
    public void SetFindText(Object sender, String text)
    {
      /*
			if (sender != this.quickFind) this.quickFind.SetFindText(text);
			if (sender != this.frInDocDialog) this.frInDocDialog.SetFindText(text);
		*/
    }

    /// <summary>
    /// Sets the case setting to find globally
    /// </summary>
    public void SetMatchCase(Object sender, Boolean matchCase)
    {
      /*
			if (sender != this.quickFind) this.quickFind.SetMatchCase(matchCase);
			if (sender != this.frInDocDialog) this.frInDocDialog.SetMatchCase(matchCase);
		*/
    }

    /// <summary>
    /// Sets the whole word setting to find globally
    /// </summary>
    public void SetWholeWord(Object sender, Boolean wholeWord)
    {
      //if (sender != this.quickFind) this.quickFind.SetWholeWord(wholeWord);
      //if (sender != this.frInDocDialog) this.frInDocDialog.SetWholeWord(wholeWord);
    }
    #endregion

    #region MainForm Click Handler
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
      //this.toolStrip1.Visible = this.ツールバーTToolStripMenuItem.Checked;
    }

    private void ステータスバーSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //this.statusStrip1.Visible = ステータスバーSToolStripMenuItem.Checked;
    }

    private void documentTabControl_MouseClick(object sender, MouseEventArgs e)
    {
      TabControl tabcontrol = sender as TabControl;
      //MessageBox.Show(tabcontrol.Name);
      if (e.Button == MouseButtons.Right)
      {
        //http://note.phyllo.net/?eid=517117
        for (int i = 0; i < this.documentTabControl.TabCount; i++)
        {
          //タブとマウス位置を比較し、クリックしたタブを選択
          if (this.documentTabControl.GetTabRect(i).Contains(e.X, e.Y))
          {
            this.documentTabControl.SelectedTab = this.documentTabControl.TabPages[i];
            this.tabMenu.Show(this.documentTabControl, e.Location);
            break;
          }
        }
      }
    }

    #endregion

    #region BottomTabControl Click Handler
    private void bottomTabControl_MouseClick(object sender, MouseEventArgs e)
    {
      //this.bottomSplitContainer.Panel1Collapsed = true;//OK
    }

    private void bottomTabControl_Enter(object sender, EventArgs e)
    {
      this.bottomSplitContainer.SplitterDistance =
       this.Height - this.settings.DefaultBottomPanelHeight - 320;

    }
    private void bottomTabControl_Leave(object sender, EventArgs e)
    {
      this.bottomSplitContainer.SplitterDistance =
       this.Height - this.settings.DefaultBottomPanelHeight;
    }
    private void bottomTabControl_DoubleClick(object sender, EventArgs e)
    {
      this.bottomSplitContainer.Panel1Collapsed = true;
      this.bottomSplitContainer.Panel2Collapsed = false;
    }
    #endregion

    #region RightTabControl Click Handler
    private void rightTabControl_MouseClick(object sender, MouseEventArgs e)
    {

    }
    private void rightTabControl_Enter(object sender, EventArgs e)
    {
      this.rightSplitContainer.SplitterDistance =
       this.Width - this.settings.DefaultRightPanelWidth - 480;
    }

    private void rightTabControl_Leave(object sender, EventArgs e)
    {
      this.rightSplitContainer.SplitterDistance =
       this.Width - this.settings.DefaultRightPanelWidth;
    }
    private void rightTabControl_DoubleClick(object sender, EventArgs e)
    {
      this.rightSplitContainer.Panel1Collapsed = true;
      this.rightSplitContainer.Panel2Collapsed = false;
    }
    #endregion

    #region FlashDevelop Click Handlers
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
        ToolStripMenuItem syntaxMenu = Globals.MainForm.FindMenuItem("SyntaxMenu") as ToolStripMenuItem;
        for(int i=0; i<syntaxMenu.DropDownItems.Count; i++)
        {
          syntaxMenu.DropDownItems[i].BackColor = Color.White;
        }
        button.BackColor = Color.Beige;
        if (IsEditable)
        {
          RichTextEditor editor = this.CurrentDocument as RichTextEditor;
          RichTextBox rtb = editor.richTextBox1;

          // 未完成 FIXMI Focusが移動しない
          // MANDATORY - focuses a label before highlighting (avoids blinking)
          //this.editor.richTextBox2.Focus();
          SyntaxHighlighter.Highlight(rtb,language);
        }
        //if (sci.ConfigurationLanguage.Equals(language)) return; // already using this syntax
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
      this.openFileDialog.Multiselect = true;
      this.openFileDialog.InitialDirectory = this.WorkingDirectory;
      if (this.openFileDialog.ShowDialog(this) == DialogResult.OK && this.openFileDialog.FileName.Length != 0)
      {
        Int32 count = this.openFileDialog.FileNames.Length;
        for (Int32 i = 0; i < count; i++)
        {
          /*
          RichTextEditor editor = new RichTextEditor();
          editor.Dock = System.Windows.Forms.DockStyle.Fill;
          editor.AccessibleDescription = openFileDialog.FileNames[i];
           editor.AccessibleDefaultActionDescription = this.GetType().FullName +";"+ Application.ExecutablePath;
           */
          /*
          // [Windows アプリ] コントロールを文字列で操作する  
          // http://blog.hiros-dot.net/?p=4590
          // インデックスによる指定
             Controls[インデックス].プロパティ = 値
          // 文字列による指定
          Controls[コントロール名].プロパティ = 値
            this.Controls["label1"].Text = "HIRO";
          */
          /*
          //MessageBox.Show(editor.AccessibleDefaultActionDescription);
          //((Control)editor.Tag).Tag = (String)openFileDialog.FileNames[i];
          //TabPage tabPage = new TabPage();
          //tabPage.Name = "test";
          //tabPage.Text = "test";
          //tabPage.Controls.Add(editor);
          //this.documentTabControl.Controls.Add(tabPage);
          TabPageManager.AddTabPage(editor, this.documentTabControl);
          */

          this.OpenDocument(openFileDialog.FileNames[i]);
        }
      }
      this.openFileDialog.Multiselect = false;
    }
    public void Save(object sender, EventArgs e)
    {
      if(this.IsEditable && File.Exists(this.CurrentDocumentPath))
      {
        RichTextEditor editor = this.CurrentDocument as RichTextEditor;
        editor.上書き保存SToolStripMenuItem.PerformClick();
      }
    }
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
    public void Reload(object sender, EventArgs e)
    {
      if(this.IsEditable && File.Exists(this.CurrentDocumentPath))
      {
        RichTextEditor editor = this.CurrentDocument as RichTextEditor;
        editor.LoadFile(this.CurrentDocumentPath);
      }
    }
    public void Duplicate(object sender, EventArgs e)
    {
      this.OpenDocument(this.CurrentDocumentPath, false);
      //TabPageManager.AddTabPage(((PageInfo)this.documentTabControl.SelectedTab.Tag).Child, this.documentTabControl, false);
    }
    public void Revert(object sender, EventArgs e)
    {
      if (this.IsEditable)
      {
        RichTextEditor editor = this.CurrentDocument as RichTextEditor;
        editor.元に戻すUToolStripMenuItem.PerformClick();
      }
    }
    public void SaveAs(object sender, EventArgs e)
    {
      if (this.IsEditable)
      {
        RichTextEditor editor = this.CurrentDocument as RichTextEditor;
        editor.名前を付けて保存AToolStripMenuItem.PerformClick();
      }
    }
    public void OnEncodeSave(object sender, EventArgs e) { }
    public void Close(object sender, EventArgs e)
    {
      if (IsEditable)
      {
        bool result = ((RichTextEditor)this.CurrentDocument).parentForm_Closing(sender,e);
        if (result == false) return;
      }
      //string msgboxString = this.tabControl1.SelectedTab.Text + " タブを削除します\nよろしいですか?";
      //if (Lib.confirmDestructionText("削除確認", msgboxString) == true)
      //{
      //TabPageManager.CloseTabPage(sender, e);
      TabPageManager.CloseTabPage(this.documentTabControl.SelectedTab, this.documentTabControl);
      //}
    }
    public void ReopenClosed(object sender, EventArgs e) { }
    public void CloseOthers(object sender, EventArgs e)
    {
      TabPageManager.CloseOtherTabPages(this.documentTabControl.SelectedTab, this.documentTabControl);
    }
    public void CloseAll(object sender, EventArgs e)
    {
      TabPageManager.CloseAllTabPages(this.documentTabControl);
    }
    public void ChangeEncoding(object sender, EventArgs e) { }
    public void ToggleSaveBOM(object sender, EventArgs e) { }
    public void OpenCustomDocument(object sender, EventArgs e)
    {
      try
      {
        ToolStripItem button = (ToolStripItem)sender;
        String[] args = ((ItemData)button.Tag).Tag.Split(';');
        String flags = ((ItemData)button.Tag).Flags;
        String action = args[0]; // Action of the command
        String data = (args.Length > 1) ? args[1] : "";
        //MessageBox.Show(flags);
        if (!String.IsNullOrEmpty(flags)) data = data + "#" + flags;
        OpenCustomDocument(action, data);
      }
      catch (Exception ex)
      {
        MessageBox.Show(Lib.OutputError(ex.Message.ToString()),
          "OpenCustomDocument(object sender, EventArgs e)");
      }
    }
    public void OpenIn(object sender, EventArgs e) { }
    public void ConvertEOL(object sender, EventArgs e) { }
    public void PrintPreview(object sender, EventArgs e) { }
    public void Restart(object sender, EventArgs e)
    {
      if (this.GetInstanceCount() == 1)
      {
        this.restartRequested = true;
        this.Close();
      }
    }
    public void Exit(object sender, EventArgs e)
    {
      this.Close();
    }
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
      
      try
      {
        ToolStripMenuItem button = new ToolStripMenuItem();
        button.Tag = new ItemData(null, tag, null); // Tag is used for args
        Object[] parameters = new Object[2];
        parameters[0] = button; parameters[1] = null;

        if (name.IndexOf("@") > -1)
        {
          methodname = name.Split('@')[0];
          classname = name.Split('@')[1];
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
     
      return true;
    }
    public Boolean CallCommandFromDll(String name, String tag)
    {
      return this.CallCommand(name, tag);
      /*
      String classname = String.Empty;
      String methodname = String.Empty;
      String accessor = String.Empty;

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
      return true;
    */
    }
    public Boolean CallControlCommand(String name, String tag,Control control=null)
    {
      if (control == null) control = CurrentDocument;
      //MessageBox.Show(CurrentDocument.GetType().FullName);
      //MessageBox.Show(CurrentDocument.AccessibleName);
      //MessageBox.Show(CurrentDocument.AccessibleDescription);
      try
      {
        String classname = control.AccessibleDescription.Split('@')[0];
        String dllpath = control.AccessibleDescription.Split('@')[1];
        Assembly assembly = Assembly.LoadFrom(dllpath);
        Type type = assembly.GetType(classname);
        //control = (Control)Activator.CreateInstance(type);
        ToolStripMenuItem button = new ToolStripMenuItem();
        button.Tag = new ItemData(null, tag, null); // Tag is used for args
        button.AccessibleName = tag; // Tag is used for args
        Object[] parameters = new Object[2];
        parameters[0] = button; parameters[1] = null;
        object instance = Activator.CreateInstance(type);
        MethodInfo method3 = type.GetMethod(name);
        method3.Invoke(instance, parameters);
        return true;
      }
      catch (Exception exc)
      {
        MessageBox.Show(Lib.OutputError(exc.Message.ToString()), MethodBase.GetCurrentMethod().Name);
        return false;
      }
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
          psi.WorkingDirectory = Globals.AntPanel.projectDir;
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
    }
    public void Browse(object sender, EventArgs e)
    {
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
      int num = this.bottom_toggleIndex % 3;
      if (num == 0)
      {
        this.bottomSplitContainer.Panel2Collapsed = true;
        this.bottomSplitContainer.Panel1Collapsed = false;
      }
      else if (num == 1)
      {
        this.bottomSplitContainer.Panel2Collapsed = false;
        this.bottomSplitContainer.Panel1Collapsed = true;
      }
      else if (num == 2)
      {
        this.bottomSplitContainer.Panel2Collapsed = false;
        this.bottomSplitContainer.Panel1Collapsed = false;
      }
      this.bottom_toggleIndex++;
    }
    private Int32 rightPanel_toggleIndex = 0;
    public void ToggleRightPanelSplitter(object sender, EventArgs e)
    {
      int num = this.rightPanel_toggleIndex % 3;
      if (num == 0)
      {
        this.rightSplitContainer.Panel2Collapsed = true;
        this.rightSplitContainer.Panel1Collapsed = false;
      }
      else if (num == 1)
      {
        this.rightSplitContainer.Panel2Collapsed = false;
        this.rightSplitContainer.Panel1Collapsed = true;
      }
      else if (num == 2)
      {
        this.rightSplitContainer.Panel2Collapsed = false;
        this.rightSplitContainer.Panel1Collapsed = false;
      }
      this.rightPanel_toggleIndex++;
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
      AntPanelApplication.Dialogs.AboutDialog.Show();
    }
    
    public void OpenProject(object sender, EventArgs e)
    {
      try
      {
        ToolStripItem button = (ToolStripItem)sender;
        string name = ((ItemData)button.Tag).Tag;
        switch (name)
        {
          case "NewProject": //<button label = "新規プロジェクト(&amp;N)..."
            break;
          case "OpenProject": //<button label = "プロジェクトを開く(&amp;O)..."
            this.openFileDialog.Multiselect = false;
            this.openFileDialog.InitialDirectory = this.WorkingDirectory;
            if (this.openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
              Globals.AntPanel.InitializeTreeView(openFileDialog.FileName);
              Globals.AntPanel.dirTreePanel.projectButton.PerformClick();
              this.OpenDocument(openFileDialog.FileName);
            }
            break;
          case "ImportProject": break;//<button label = "プロジェクトをインポート(&amp;I)..." tag="ImportProject" image="315" />
          case "CloseProject":
            break;//< button label = "プロジェクトを閉じる(&amp;C)" click = "OpenProject" tag = "CloseProject" image = "315" />
          case "OpemResource": break; //<button label = "リソースを開く(&amp;R)..." click="OpenProject" shortcut="Control|R" tag="OpemResource" image="315" />
          case "TestProject": break; //<button  = "プロジェクトをテスト" click="OpenProject" shortcut="F5" tag="TestProject" image="315" />
          case "RunProject": break; //<button label = "プロジェクトを実行(&amp;R)" click="OpenProject" tag="RunProject" image="315" />
          case "BuildProject": break; //<button label = "プロジェクトをビルド(&amp;N)" click="OpenProject" shortcut=F8" tag="BuildProject" image="315" />
          case "CleanProject": break; //<button label = "プロジェクトのクリーニング(&amp;C)" click="OpenProject" shortcut="Shift|F8" tag="CleanProject" image="315" />
          case "AirApplicationSetting": break; //<button label = "エアアプリケーションの設定" click="OpenProject" tag="AirApplicationSetting" image="315" />
          case "ProjectSetting": break; //<button label = "プロジェクトの設定(&amp;P)..." click="OpenProject" tag="ProjectSetting" image="315" />
        }
      }
      catch (Exception ex)
      {
        String errMsg = Lib.OutputError(ex.Message.ToString());
        MessageBox.Show(errMsg, "CommandPromptHere(object sender, EventArgs e)");
      }
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

    public void MoveTabPage(object sender, EventArgs e)
    {
      ToolStripMenuItem item = sender as ToolStripMenuItem;
      //MessageBox.Show(item.Text);
      TabControl tabControl = this.documentTabControl;
      switch (item.Text)
      {
        case "Float":
          TabPageManager.ShowInDialog(this.documentTabControl.SelectedTab, tabControl);
          return;
        case "Top":
          break;
        case "Right":
          tabControl = this.rightTabControl;
          break;
        case "Bottom":
          tabControl = this.bottomTabControl;
          break;
        case "Left":
          tabControl = Globals.AntPanel.antPanelTabControl;
          break;
      }
      TabPageManager.MoveTabPage(this.documentTabControl.SelectedTab, tabControl);
    }

    public void CommandPromptHere(object sender, EventArgs e)
    {
      if(IsRunningWindows)
      {
        try
        {
          String dir = Path.GetDirectoryName(this.CurrentDocumentPath);
          Directory.SetCurrentDirectory(dir);
          Process.Start(@"C:\Windows\System32\cmd.exe");
        }
        catch(Exception ex)
        {
          String errMsg = Lib.OutputError(ex.Message.ToString());
          MessageBox.Show(errMsg, "CommandPromptHere(object sender, EventArgs e)");
        }
      }
      else if(IsRunningUnix)
      {
        try
        {
          String dir = Path.GetDirectoryName(WindowsPath2UnixPath(this.CurrentDocumentPath));
          Directory.SetCurrentDirectory(dir);
          Process.Start("/usr/bin/gnome-terminal");
        }
        catch (Exception ex)
        {
          String errMsg = Lib.OutputError(ex.Message.ToString());
          MessageBox.Show(errMsg, "CommandPromptHere(object sender, EventArgs e)");
        }
      }
    }

    public void ExplorerHere(object sender, EventArgs e)
    {
      if (IsRunningWindows)
      {
        try
        {
          String dir = Path.GetDirectoryName(this.CurrentDocumentPath);
          Directory.SetCurrentDirectory(dir);

          //C ドライブが展開され，TestApp が選択されている状態でウィンドウを表示する．
          //例 6 : EXPLORER.EXE / e,/root,C:\TestDir\TestApp.exe
          //http://www.geocities.jp/n_y_page/explorer.html
          //TestApp が選択されている状態でウィンドウを表示する．
         //例 5 : EXPLORER.EXE /select,C:\TestDir\TestApp.exe
          Process.Start(@"C:\Windows\EXPLORER.EXE", "/select," + this.CurrentDocumentPath);
          //Process.Start(@"C:\Windows\EXPLORER.EXE", dir);
        }
        catch (Exception ex)
        {
          String errMsg = Lib.OutputError(ex.Message.ToString());
          MessageBox.Show(errMsg, "CommandPromptHere(object sender, EventArgs e)");
        }
      }
      else if (IsRunningUnix)
      {
        try
        {
          String dir = Path.GetDirectoryName(WindowsPath2UnixPath(this.CurrentDocumentPath));
          Directory.SetCurrentDirectory(dir);
          Process.Start("/usr/bin/nautilus");
        }
        catch (Exception ex)
        {
          String errMsg = Lib.OutputError(ex.Message.ToString());
          MessageBox.Show(errMsg, "CommandPromptHere(object sender, EventArgs e)");
        }
      }
    }

    public void RunSystemCommandHere(object sender, EventArgs e)
    {
      if (IsRunningWindows)
      {
        try
        {
          String dir = Path.GetDirectoryName(this.CurrentDocumentPath);
          Directory.SetCurrentDirectory(dir);
          Process.Start(@"C:\Windows\EXPLORER.EXE", this.CurrentDocumentPath);
        }
        catch (Exception ex)
        {
          String errMsg = Lib.OutputError(ex.Message.ToString());
          MessageBox.Show(errMsg, "CommandPromptHere(object sender, EventArgs e)");
        }
      }
      else if (IsRunningUnix)
      {
        String path = WindowsPath2UnixPath(this.CurrentDocumentPath);
        try
        {
          if(Lib.IsEditable(path)) Process.Start("/usr/bin/emacs",path);
          else if (Lib.IsImageFile(path)) Process.Start("/usr/bin/eog", path);
          else if (Lib.IsSoundFile(path) || Lib.IsVideoFile(path)) Process.Start("/usr/bin/totem", path);
          else Process.Start(path);
        }
        catch (Exception ex)
        {
          String errMsg = Lib.OutputError(ex.Message.ToString());
          MessageBox.Show(errMsg, "CommandPromptHere(object sender, EventArgs e)");
        }
      }

    }

    public void InitializeTabPage(object sender, EventArgs e)
    {
      TabPageManager.CloseAllTabPages(this.documentTabControl);
      this.InitializeControls();
    }

    #endregion

    public void ActivatePlayer()
    {
      //this.splitContainer1.Panel2Collapsed = false;
      //this.splitContainer1.Panel1Collapsed = false;
      //this.t.tabControl1.SelectedIndex = 3;
    }

    public void Test1(object sender, EventArgs e)
    {
      /*
      if (sender != null)
      {
        ToolStripItem button = (ToolStripItem)sender;
        String msg = Globals.AntPanel.menuTree.ProcessVariable(((ItemData)button.Tag).Tag);
        MessageBox.Show(msg, "MainForm Testからの送信です");
      }
      */
      this.CallControlCommand("Test1", "MainForm Testからの送信です");
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
