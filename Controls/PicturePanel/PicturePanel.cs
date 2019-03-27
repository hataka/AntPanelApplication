using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using AntPanelApplication.CommonLibrary;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace AntPanelApplication.Controls.PicturePanel
{
  public partial class PicturePanel : UserControl
  {
    //public PicturePanel()
    //{
      //InitializeComponent();
    //}

    #region PicturePanel Variables
    //public static String baseDir = @"C:\Documents and Settings\kazuhiko\Local Settings\Application Data\FlashDevelop";

    public List<string> previousDocuments = new List<string>();
    public string currentPath = string.Empty;
    public Point currentPoint = new Point(0, 0);
    //private PictureBox pictureBox1;
    public static ImageList imageList1;
#if Interface
		private PluginMain xmlTreeMenu;
	  private XMLTreeMenu.Settings settings;
#endif
    //private bool newDocumentFlag = false;
    public ColorDialog colorDialog1;
    public Bitmap bitmap1;
    public Graphics g = null;
    public string path = @"F:\My Pictures\博幹\20111112平安神宮七五三\DSCF0932.jpg";
    private bool scribbleMode = false;
    private ArrayList PointList;
    private Options opt;
    private int oldX = -1;
    private int oldY = -1;
    private bool mouseDown = false;
    private Color current_color = Color.Blue;
    private int current_width = 5;
    private bool rubberBandMode = false;
    private RubberBandDialog rub;
    public Color use_color = Color.Red;
    public int lineWidth = 3;
    public int angle = 0;
    public string shapeType = "直線";
    public bool drawingOption = true;
    public string stringText = string.Empty;
    public PageFont page_font;
    private DrawPoints mouse_point;
    private DrawPoints drag_point;
    public List<Bitmap> history = new List<Bitmap>();
    public Bitmap selectionBitmap = null;
    public bool xrect;
    public bool yrect;
    public bool aspect;
    #endregion

    #region Properties
#if Interface
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

    public PluginMain XmlTreeMenu
    {
      get
      {
        return this.xmlTreeMenu;
      }
      set
      {
        this.xmlTreeMenu = value;
      }
    }
#endif
    public bool ScribbleMode
    {
      get
      {
        return this.scribbleMode;
      }
      set
      {
        this.scribbleMode = value;
      }
    }

    public bool RubberBandMode
    {
      get
      {
        return this.rubberBandMode;
      }
      set
      {
        this.rubberBandMode = value;
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
    public PicturePanel()
    {
      //this.newDocumentFlag = false;
      this.aspect = false;
      this.InitializeComponent();
      this.InitializeInterface();
      this.InitializePicturePanel();
      this.Text = "Picture Panel Ver1.0";
    }

    public PicturePanel(string[] args)
    {
      this.aspect = false;
      InitializeComponent();
      this.InitializeInterface();
      this.InitializePicturePanel();
      this.Text = "Picture Panel Ver1.0";
      if (args[0] != String.Empty && File.Exists(args[0])
        && Lib.IsImageFile(args[0]))
      {
        this.currentPath = args[0];
      }
    }
    #endregion

    #region Initialization
    private void InitializePicturePanel()
    {
      InitializeImageList();
      this.statusStrip1.Visible = true;
      this.menuStrip1.Visible = true;
      this.toolStrip1.Visible = true;
      // Designer を有効にするため ここに移す
      this.AutoScaleMode = AutoScaleMode.Font;
    }

    public void InitializeImageList()
    {
#if Interface
      imageList1 = this.xmlTreeMenu.pluginUI.ImageList2;
#else      
      Bitmap bmp3 = (Bitmap)this.imageListButton.Image;
      // 
      // imageList1
      // 
      imageList1 = new ImageList();
      imageList1.Images.AddStrip(bmp3);
      imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      imageList1.ImageSize = new System.Drawing.Size(16, 16);
      //imageList1.TransparentColor = Color.FromArgb(233, 229, 215);
      imageList1.TransparentColor = System.Drawing.Color.Transparent;
#endif	  
    }

    /**
    * Handles the click event for the menu items.
    */
    public static void AddIcon(String name)
    {
#if Interface
			//String baseDir = PathHelper.BaseDir;
		  // http://dobon.net/vb/dotnet/graphics/imagefromfile.html
      String iconPath = Path.Combine(PathHelper.BaseDir, @"SettingData\icons\" + name); //"
      //public static String baseDir = @"C:\Documents and Settings\kazuhiko\Local Settings\Application Data\FlashDevelop";
      //String iconPath = Path.Combine(baseDir, @"Settings\icons\" + name); //"
		  System.Drawing.Icon ico = new System.Drawing.Icon(iconPath, 16, 16);
		  //Bitmapに変換する
		  System.Drawing.Bitmap bmp = ico.ToBitmap();
		  //変換したBitmapしか使わないならば、元のIconは解放できる
		  ico.Dispose();
		  //イメージを表示する
		  imageList1.Images.Add(bmp);
#endif
    }

    public void InitializeInterface()
    {
#if Interface
			string guid = "0538077E-8C37-4A2B-962B-8FB77DC9F325";
		  this.xmlTreeMenu = (PluginMain)PluginBase.MainForm.FindPlugin(guid);
      this.settings = this.xmlTreeMenu.Settings as XMLTreeMenu.Settings;
		  this.Instance = new ChildFormControlClass();
		  this.Instance.name = "PicturePanel";
		  this.Instance.toolStrip = this.toolStrip1;
		  this.Instance.menuStrip = this.menuStrip1;
		  this.Instance.statusStrip = this.statusStrip1;
		  this.Instance.スクリプトToolStripMenuItem = this.スクリプトCToolStripMenuItem;
		  this.Instance.toolStripStatusLabel = this.toolStripStatusLabel1;
      this.Instance.PreviousDocuments = this.PreviousDocuments;
#endif
    }

    public void IntializeSettings()
    {
#if Interface
			this.previousDocuments = this.settings.PreviousPicturePanelDocuments;
		  this.toolStrip1.Visible = this.settings.PicturePanelToolBarVisible;
		  this.statusStrip1.Visible = this.settings.PicturePanelStatusBarVisible;
		  this.statusStrip1.Visible = this.settings.PicturePanelStatusBarVisible;
#endif
      this.ツールバーTToolStripMenuItem.Checked = this.toolStrip1.Visible;
      this.ステータスバーSToolStripMenuItem.Checked = this.statusStrip1.Visible;
      this.PopulatePreviousDocumentsMenu();
      Dictionary<string, string> dictionary = StringHandler.Get_Values(base.AccessibleDescription, ';', '=');
    }

    public void ApplySettings(Dictionary<string, string> dict)
    {
      string text = "";
      if (dict.TryGetValue("recentdocuments", out text))
      {
        this.previousDocuments.Clear();
        this.previousDocuments = new List<string>(text.Split(new char[] { '|' }));
        this.PopulatePreviousDocumentsMenu();
      }
      if (dict.TryGetValue("toolbarvisible", out text))
      {
        this.toolStrip1.Visible = (text == "true");
        this.ツールバーTToolStripMenuItem.Checked = this.toolStrip1.Visible;
      }
      if (dict.TryGetValue("statusbarvisible", out text))
      {
        this.statusStrip1.Visible = (text == "true");
        this.ステータスバーSToolStripMenuItem.Checked = (this.ステータスバーSToolStripMenuItem1.Checked = this.statusStrip1.Visible);
      }
      if (dict.TryGetValue("scribblemode", out text))
      {
        this.scribbleMode = (text == "true");
        this.scribbleToolStripMenuItem.Checked = this.scribbleMode;
      }
      if (dict.TryGetValue("rubberbandmode", out text))
      {
        this.rubberBandMode = (text == "true");
        this.rubberBandToolStripMenuItem.Checked = this.rubberBandMode;
      }
    }

    #endregion

    #region Event Handlers
    private void PicturePanel_Load(object sender, EventArgs e)
    {
      //MessageBox.Show(this.pictureBox1.Tag.ToString());
      if ((string)this.pictureBox1.Tag != string.Empty)
      {
        this.path = (string)this.pictureBox1.Tag;
        //ここを通る
        //MessageBox.Show(this.path);
      }

      if (!string.IsNullOrEmpty(this.path))
      {
        if (File.Exists(this.path))
        {
          this.pictureBox1.Tag = this.path;
          this.pictureBox1.Image = new Bitmap(this.path);
          //((Form)base.Parent).Text = Path.GetFileNameWithoutExtension(this.path);
        }
        else
        {
          ToolStripMenuItem toolStripMenuItem = this.FindQcGraphMenuItem(this.path);
          if (!string.IsNullOrEmpty(this.path) && toolStripMenuItem != null)
          {
            toolStripMenuItem.PerformClick();
          }
        }
      }
      this.IntializeSettings();
      this.AddPreviousDocuments(this.path);
      //((Form)this.Parent).FormClosing += new FormClosingEventHandler(this.parentForm_Closing);
      //OK!
      //MessageBox.Show(this.AccessibleDescription);
      //MessageBox.Show(((PluginUI)this.MainForm.xmlTreeMenu_pluginUI).ImageList2.Images.Count.ToString());
    }

    private ToolStripMenuItem FindQcGraphMenuItem(string theme)
    {
      ToolStripMenuItem result;
      foreach (ToolStripMenuItem toolStripMenuItem in this.qcgraphToolStripMenuItem.DropDownItems)
      {
        foreach (ToolStripMenuItem toolStripMenuItem2 in toolStripMenuItem.DropDownItems)
        {
          if (toolStripMenuItem2.Tag.ToString() == theme)
          {
            result = toolStripMenuItem2;
            return result;
          }
        }
      }
      result = null;
      return result;
    }

    private void parentForm_Closing(object sender, EventArgs e)
    {
#if Interface
			this.settings.PreviousPicturePanelDocuments = this.previousDocuments;
	  	this.settings.PicturePanelToolBarVisible = this.toolStrip1.Visible;
		  this.settings.PicturePanelStatusBarVisible = this.statusStrip1.Visible;
		  this.settings.PicturePanelScribbleMode = this.scribbleMode;
		  this.settings.PicturePanelRubberBandMode = this.rubberBandMode;
		  this.UpdateAccessibleDescription();
#endif
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
    }

    public void AddPreviousDocuments(string data)
    {
      try
      {
        ToolStripMenuItem toolStripMenuItem = this.最近開いたファイルToolStripMenuItem;
        if (this.previousDocuments.Contains(data))
        {
          this.previousDocuments.Remove(data);
        }
        this.previousDocuments.Insert(0, data);
        this.UpdateAccessibleDescription();
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
        ToolStripMenuItem toolStripMenuItem = this.最近開いたファイルToolStripMenuItem;
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
          toolStripMenuItem.DropDownItems.Add(this.最近開いたファイルをクリアToolStripMenuItem);
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
      if (File.Exists(text) && Lib.IsImageFile(text))
      {
        try
        {
          if (!this.ScribbleMode && !this.RubberBandMode)
          {
            this.pictureBox1.Image = new Bitmap(text);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            ((Form)base.Parent).Text = Path.GetFileName(text);
            this.pictureBox1.Tag = text;
            this.AddPreviousDocuments(text);
            this.pictureBox1.Refresh();
          }
        }
        catch (Exception ex)
        {
          string text2 = ex.Message.ToString();
          MessageBox.Show(Lib.OutputError(text2));
        }
      }
      else if (text.StartsWith("qcgraph!"))
      {
        this.AddPreviousDocuments(text);
        this.DrawQcGraphItem(text.Replace("qcgraph!", ""));
      }
    }

    private void 最近開いたファイルをクリアToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.previousDocuments.Clear();
      this.PopulatePreviousDocumentsMenu();
      this.UpdateAccessibleDescription();
    }

    private void UpdateAccessibleDescription()
    {
      string text = "recentdocuments=" + string.Join("|", this.previousDocuments.ToArray());
      string text2 = "menubarvisible=" + (this.menuStrip1.Visible ? "true" : "false");
      string text3 = "toolbarvisible=" + (this.toolStrip1.Visible ? "true" : "false");
      string text4 = "statusbarvisible=" + (this.statusStrip1.Visible ? "true" : "false");
      string text5 = "scribblemode=" + (this.scribbleMode ? "true" : "false");
      string text6 = "rubberbandmode=" + (this.rubberBandMode ? "true" : "false");
      this.AccessibleDescription = string.Concat(new string[]{text,";",text2,";",text3,";",text4,";",
        text5,";",text6});
    }

    private void PicturePanel_Enter(object sender, EventArgs e)
    {
    }

    private void PicturePanel_Leave(object sender, EventArgs e)
    {
    }

    private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.ScribbleMode)
      {
        this.scribble_MouseDown(sender, e);
      }
      else if (this.RubberBandMode)
      {
        this.rubberband_MouseDown(sender, e);
      }
    }

    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
      if (this.ScribbleMode)
      {
        this.scribble_MouseUp(sender, e);
      }
      else if (this.RubberBandMode)
      {
        this.rubberband_MouseUp(sender, e);
      }
    }

    private void pictureBox1_SizeChanged(object sender, EventArgs e)
    {
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.ScribbleMode)
      {
        this.scribble_MouseMove(sender, e);
      }
      else if (this.RubberBandMode)
      {
        this.rubberband_MouseMove(sender, e);
      }
    }
    #endregion

    #region MenuBar Click Handler
    private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string text = "F:\\My Pictures\\";
      string text2 = "imageファイル(*.jpg;*.bmp;*.png;*.gif;*.ico;*.jpeg)|*.jpg;*.bmp;*.png;*.gif;*.ico;*.jpeg|すべてのファイル(*.*)|*.*";
      string text3 = "test.jpeg";
      string text4 = Lib.File_OpenDialog(text3, text, text2);
      try
      {
        if (File.Exists(text4) && Lib.IsImageFile(text4))
        {
          if (!this.ScribbleMode && !this.RubberBandMode)
          {
            this.pictureBox1.Image = new Bitmap(text4);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            ((Form)base.Parent).Text = Path.GetFileName(text4);
            this.pictureBox1.Tag = text4;
            this.AddPreviousDocuments(text4);
            this.pictureBox1.Refresh();
          }
        }
      }
      catch (Exception ex)
      {
        string text5 = ex.Message.ToString();
        MessageBox.Show(Lib.OutputError(text5));
      }
    }

    private void 新規作成NToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        this.全クリアToolStripMenuItem.PerformClick();
      }
    }

    private void 上書き保存SToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DateTime now = DateTime.Now;
      string text = this.pictureBox1.Tag.ToString();
      string str = string.Format("_{0:00}{1:00}{2:00}{3:00}{4:00}{5:00}", new object[]
        {
        now.Year,
        now.Month,
        now.Day,
        now.Hour,
        now.Minute,
        now.Second
        });
      if (!this.ScribbleMode && !this.RubberBandMode)
      {
        if (File.Exists(text))
        {
          string destFileName = Path.Combine(Path.GetDirectoryName(text), Path.GetFileNameWithoutExtension(text)) + str + Path.GetExtension(text);
          File.Copy(text, destFileName);
          this.pictureBox1.Image.Save(text);
        }
      }
    }

    private void 名前を付けて保存AToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void 印刷PToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PrintDocument printDocument = new PrintDocument();
      printDocument.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
      if (new PrintDialog
      {
        Document = printDocument
      }.ShowDialog() == DialogResult.OK)
      {
        printDocument.Print();
      }
    }

    private void pd_PrintPage(object sender, PrintPageEventArgs e)
    {
      e.Graphics.DrawImage(this.pictureBox1.Image, e.MarginBounds);
      e.HasMorePages = false;
    }

    private void 印刷プレビューVToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PrintDocument printDocument = new PrintDocument();
      printDocument.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
      new PrintPreviewDialog
      {
        Document = printDocument
      }.ShowDialog();

    }

    private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ((Form)this.Parent).Close();
    }

    private void 元に戻すUToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        if (this.history.Count - 2 > -1)
        {
          Bitmap image = this.history[this.history.Count - 2];
          this.g.Clear(Color.Transparent);
          this.g.DrawImage(image, 0, 0);
          this.pictureBox1.Refresh();
          Bitmap bitmap = (Bitmap)this.bitmap1.Clone();
          bitmap.MakeTransparent(Color.FromArgb(211, 211, 211));
          this.history.Add(bitmap);
        }
      }
    }

    private void やり直しRToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        Bitmap image = this.history[this.history.Count - 1];
        this.g.Clear(Color.Transparent);
        this.g.DrawImage(image, 0, 0);
        this.pictureBox1.Refresh();
      }
    }

    private void 切り取りTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        this.shapeType = "Cut";
        this.DrawRubberband();
        Rectangle rect = new Rectangle(this.mouse_point.start_point.X, this.mouse_point.start_point.Y, this.mouse_point.end_point.X - this.mouse_point.start_point.X, this.mouse_point.end_point.Y - this.mouse_point.start_point.Y);
        Bitmap image = this.bitmap1.Clone(rect, this.bitmap1.PixelFormat);
        Clipboard.SetImage(image);
        SolidBrush brush = new SolidBrush(Color.FromArgb(211, 211, 211));
        this.g.FillRectangle(brush, rect);
        this.pictureBox1.Refresh();
        this.再描画ToolStripMenuItem.PerformClick();
      }
    }

    private void コピーCToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        this.shapeType = "Copy";
        this.DrawRubberband();
        Bitmap image = this.bitmap1.Clone(this.mouse_point.GetRectangle(), this.bitmap1.PixelFormat);
        Clipboard.SetImage(image);
      }
    }

    private void 貼り付けPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        if (Clipboard.ContainsImage())
        {
          Image image = Clipboard.GetImage();
          if (image != null)
          {
            ((Bitmap)image).MakeTransparent(Color.FromArgb(211, 211, 211));
            this.g.DrawImage(image, this.mouse_point.start_point.X, this.mouse_point.start_point.Y);
          }
        }
        this.pictureBox1.Refresh();
        this.再描画ToolStripMenuItem.PerformClick();
      }
    }

    private void 再描画ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        Bitmap bitmap = (Bitmap)this.bitmap1.Clone();
        bitmap.MakeTransparent(Color.FromArgb(211, 211, 211));
        this.g.Clear(Color.Transparent);
        this.g.DrawImage(bitmap, 0, 0);
        this.pictureBox1.Refresh();
        bitmap.Dispose();
      }
    }

    private void すべて選択AToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.mouse_point.start_point.X = 0;
      this.mouse_point.start_point.Y = 0;
      this.mouse_point.end_point.X = this.pictureBox1.Size.Width;
      this.mouse_point.end_point.Y = this.pictureBox1.Size.Height;
    }

    private void ツールバーTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.toolStrip1.Visible = this.ツールバーTToolStripMenuItem.Checked;
    }

    private void ステータスバーSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.statusStrip1.Visible = this.ステータスバーSToolStripMenuItem.Checked;
    }

    private void 選択位置に挿入ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        string text = "F:\\My Pictures\\";
        string text2 = "imageファイル(*.jpg;*.bmp;*.png;*.gif;*.ico;*.jpeg)|*.jpg;*.bmp;*.png;*.gif;*.ico;*.jpeg|すべてのファイル(*.*)|*.*";
        string text3 = "test.jpeg";
        string text4 = Lib.File_OpenDialog(text3, text, text2);
        try
        {
          if (File.Exists(text4) && Lib.IsImageFile(text4))
          {
            if (this.ScribbleMode || this.RubberBandMode)
            {
              Bitmap bitmap = new Bitmap(text4);
              Color pixel = bitmap.GetPixel(0, 0);
              bitmap.MakeTransparent(pixel);
              this.g.DrawImage(bitmap, this.mouse_point.start_point.X, this.mouse_point.start_point.Y);
              this.pictureBox1.Refresh();
            }
          }
        }
        catch (Exception ex)
        {
          string text5 = ex.Message.ToString();
          MessageBox.Show(Lib.OutputError(text5));
        }
      }
    }

    private void 選択範囲に挿入ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        string text = "F:\\My Pictures\\";
        string text2 = "imageファイル(*.jpg;*.bmp;*.png;*.gif;*.ico;*.jpeg)|*.jpg;*.bmp;*.png;*.gif;*.ico;*.jpeg|すべてのファイル(*.*)|*.*";
        string text3 = "test.jpeg";
        string text4 = Lib.File_OpenDialog(text3, text, text2);
        try
        {
          if (File.Exists(text4) && Lib.IsImageFile(text4))
          {
            if (this.ScribbleMode || this.RubberBandMode)
            {
              Bitmap bitmap = new Bitmap(text4);
              if (this.aspect)
              {
                Bitmap bitmap2 = ImageHander.ResizeImage(bitmap, (double)this.mouse_point.GetRectangle().Width, (double)this.mouse_point.GetRectangle().Height);
                Color pixel = bitmap2.GetPixel(0, 0);
                bitmap2.MakeTransparent(pixel);
                int x = this.mouse_point.start_point.X + (this.mouse_point.GetRectangle().Width - bitmap2.Width) / 2;
                int y = this.mouse_point.start_point.Y + (this.mouse_point.GetRectangle().Height - bitmap2.Height) / 2;
                this.g.DrawImage(bitmap2, x, y);
              }
              else
              {
                Color pixel = bitmap.GetPixel(0, 0);
                bitmap.MakeTransparent(pixel);
                this.g.DrawImage(bitmap, this.mouse_point.GetRectangle());
              }
              this.pictureBox1.Refresh();
              this.再描画ToolStripMenuItem.PerformClick();
            }
          }
        }
        catch (Exception ex)
        {
          string text5 = ex.Message.ToString();
          MessageBox.Show(Lib.OutputError(text5));
        }
      }
    }

    private void 選択範囲縦横比保持ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        string text = "F:\\My Pictures\\";
        string text2 = "imageファイル(*.jpg;*.bmp;*.png;*.gif;*.ico;*.jpeg)|*.jpg;*.bmp;*.png;*.gif;*.ico;*.jpeg|すべてのファイル(*.*)|*.*";
        string text3 = "test.jpeg";
        string text4 = Lib.File_OpenDialog(text3, text, text2);
        try
        {
          if (File.Exists(text4) && Lib.IsImageFile(text4))
          {
            if (this.ScribbleMode || this.RubberBandMode)
            {
              Bitmap bitmap = new Bitmap(text4);
              Bitmap bitmap2 = ImageHander.ResizeImage(bitmap, (double)this.mouse_point.GetRectangle().Width, (double)this.mouse_point.GetRectangle().Height);
              Color pixel = bitmap2.GetPixel(0, 0);
              bitmap2.MakeTransparent(pixel);
              int x = this.mouse_point.start_point.X + (this.mouse_point.GetRectangle().Width - bitmap2.Width) / 2;
              int y = this.mouse_point.start_point.Y + (this.mouse_point.GetRectangle().Height - bitmap2.Height) / 2;
              this.g.DrawImage(bitmap2, x, y);
              this.pictureBox1.Refresh();
              this.再描画ToolStripMenuItem.PerformClick();
            }
          }
        }
        catch (Exception ex)
        {
          string text5 = ex.Message.ToString();
          MessageBox.Show(Lib.OutputError(text5));
        }
      }

    }

    private void 変形貼り付けToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        if (Clipboard.ContainsImage())
        {
          Image image = Clipboard.GetImage();
          if (image != null)
          {
            ((Bitmap)image).MakeTransparent(Color.FromArgb(211, 211, 211));
            this.g.DrawImage(image, this.mouse_point.GetRectangle());
          }
          this.pictureBox1.Refresh();
          this.再描画ToolStripMenuItem.PerformClick();
        }
      }
    }

    private void 回転ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        if (Clipboard.ContainsImage())
        {
          Image image = Clipboard.GetImage();
          if (image != null)
          {
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            this.g.DrawImage(image, this.mouse_point.start_point.X, this.mouse_point.start_point.Y);
            this.pictureBox1.Refresh();
            this.再描画ToolStripMenuItem.PerformClick();
          }
        }
      }
    }

    private void 角度を指定ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Image image = Clipboard.GetImage();
      if (image != null)
      {
        double num = (double)this.angle / 57.295779513082323;
        float num2 = (float)this.mouse_point.start_point.X;
        float num3 = (float)this.mouse_point.start_point.Y;
        float x = num2 + (float)image.Width * (float)Math.Cos(num);
        float y = num3 + (float)image.Width * (float)Math.Sin(num);
        float x2 = num2 - (float)image.Height * (float)Math.Sin(num);
        float y2 = num3 + (float)image.Height * (float)Math.Cos(num);
        PointF[] destPoints = new PointF[]
          {
          new PointF(num2, num3),
          new PointF(x, y),
          new PointF(x2, y2)
          };
        this.g.DrawImage(image, destPoints);
        this.pictureBox1.Refresh();
        this.再描画ToolStripMenuItem.PerformClick();
        image.Dispose();
      }
    }

    private void 反転貼り付けToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.ScribbleMode || this.RubberBandMode)
      {
        if (Clipboard.ContainsImage())
        {
          Image image = Clipboard.GetImage();
          if (image != null)
          {
            int width = image.Width;
            int height = image.Height;
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            for (int i = 0; i < height; i++)
            {
              for (int j = 0; j < width; j++)
              {
                int num = ((Bitmap)image).GetPixel(j, i).ToArgb();
                num ^= 16777215;
                bitmap.SetPixel(j, i, Color.FromArgb(num));
              }
            }
            bitmap.MakeTransparent(Color.FromArgb(44, 44, 44));
            this.g.DrawImage(bitmap, this.mouse_point.start_point.X, this.mouse_point.start_point.Y);
          }
          this.pictureBox1.Refresh();
          this.再描画ToolStripMenuItem.PerformClick();
        }
      }
    }

    private void 全クリアToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.g.Clear(Color.Transparent);
      this.pictureBox1.Refresh();
    }

    private void 背景色変更ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.colorDialog1.ShowDialog() == DialogResult.OK)
      {
        Color color = this.colorDialog1.Color;
        Clipboard.SetImage(this.bitmap1);
        this.g.Clear(color);
        Image image = Clipboard.GetImage();
        Color pixel = ((Bitmap)image).GetPixel(0, 0);
        ((Bitmap)image).MakeTransparent(pixel);
        this.g.DrawImage(image, 0, 0);
        this.pictureBox1.Refresh();
        this.再描画ToolStripMenuItem.PerformClick();
      }
    }

    private void スクリプトを編集EToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void スクリプトメニュー更新RToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void 試験ToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region ToolBar Click Handler
    private void 開くOToolStripButton_Click(object sender, EventArgs e)
    {
      this.開くOToolStripMenuItem.PerformClick();
    }

    private void メニューバーMToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.menuStrip1.Visible = this.メニューバーMToolStripMenuItem.Checked;
    }

    private void ステータスバーSToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.statusStrip1.Visible = this.ステータスバーSToolStripMenuItem1.Checked;
    }
    #endregion

    #region ScribbleMode

    private void scribbleToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
      this.scribbleToolStripMenuItem.Checked = !this.scribbleToolStripMenuItem.Checked;
      this.ScribbleMode = this.scribbleToolStripMenuItem.Checked;
      this.新規作成NToolStripMenuItem.Enabled = this.ScribbleMode;
      this.編集EToolStripMenuItem.Enabled = this.ScribbleMode;
      this.画像GToolStripMenuItem.Enabled = this.ScribbleMode;
      this.切り取りUToolStripButton.Enabled = this.ScribbleMode;
      this.コピーCToolStripButton.Enabled = this.ScribbleMode;
      this.貼り付けPToolStripButton.Enabled = this.ScribbleMode;
      this.開くOToolStripMenuItem.Enabled = !this.ScribbleMode;
      this.開くOToolStripButton.Enabled = !this.ScribbleMode;
      this.上書き保存SToolStripMenuItem.Enabled = !this.ScribbleMode;
      if (this.ScribbleMode)
      {
        if (this.RubberBandMode)
        {
          this.DeactivateRubberBandMode(null, null);
          this.RubberBandMode = (this.rubberBandToolStripMenuItem.Checked = false);
        }
        this.pictureBox1.Tag = "Scribble";

        //((Form)base.Parent).Text = "Scribble";
        this.ActivateScribbleMode(null, null);
      }
      else
      {
        this.DeactivateScribbleMode(null, null);
      }

    }

    private void scribbleToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.scribbleToolStripMenuItem.Checked = !this.scribbleToolStripMenuItem.Checked;
      this.ScribbleMode = this.scribbleToolStripMenuItem.Checked;
      this.新規作成NToolStripMenuItem.Enabled = this.ScribbleMode;
      this.編集EToolStripMenuItem.Enabled = this.ScribbleMode;
      this.画像GToolStripMenuItem.Enabled = this.ScribbleMode;
      this.切り取りUToolStripButton.Enabled = this.ScribbleMode;
      this.コピーCToolStripButton.Enabled = this.ScribbleMode;
      this.貼り付けPToolStripButton.Enabled = this.ScribbleMode;
      this.開くOToolStripMenuItem.Enabled = !this.ScribbleMode;
      this.開くOToolStripButton.Enabled = !this.ScribbleMode;
      this.上書き保存SToolStripMenuItem.Enabled = !this.ScribbleMode;
      if (this.ScribbleMode)
      {
        if (this.RubberBandMode)
        {
          //this.DeactivateRubberBandMode(null, null);
          this.RubberBandMode = (this.rubberBandToolStripMenuItem.Checked = false);
        }
        this.pictureBox1.Tag = "Scribble";
        ((Form)base.Parent).Text = "Scribble";
        this.ActivateScribbleMode(null, null);
      }
      else
      {
        try { this.DeactivateScribbleMode(null, null); } catch { }
      }
    }

    public void ActivateScribbleMode(object sender, EventArgs e)
    {
      this.PointList = new ArrayList();
      this.opt = new Options(this);
      if (this.bitmap1 == null)
      {
        this.bitmap1 = new Bitmap(this.pictureBox1.Size.Width, this.pictureBox1.Size.Height);
      }
      this.bitmap1.MakeTransparent(Color.FromArgb(211, 211, 211));
      this.pictureBox1.Image = this.bitmap1;
      this.g = Graphics.FromImage(this.pictureBox1.Image);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
    }

    public void DeactivateScribbleMode(object sender, EventArgs e)
    {
      string text = "F:\\My Pictures\\20081004住化高槻同期会\\住化高槻45年同期会_008.JPG";
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.Image = new Bitmap(this.pictureBox1.Size.Width, this.pictureBox1.Size.Height);
      this.g.Dispose();
      this.opt.Dispose();
      this.pictureBox1.Tag = text;
      this.pictureBox1.Image = new Bitmap(text);
      //((Form)base.Parent).Text = Path.GetFileNameWithoutExtension(text);
    }

    private void scribble_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        if (this.opt.ShowDialog(this) == DialogResult.OK)
        {
          this.current_color = this.opt.scribble_color;
          this.current_width = this.opt.scribble_width;
        }
      }
      else
      {
        if (this.oldX == -1 && this.oldY == -1)
        {
          this.oldX = e.X;
          this.oldY = e.Y;
        }
        this.mouseDown = true;
        this.g.DrawLine(new Pen(this.current_color, (float)this.current_width), new Point(this.oldX, this.oldY), new Point(e.X, e.Y));
        this.PointList.Add(new Rectangle(this.oldX, this.oldY, e.X, e.Y));
      }
    }

    private void scribble_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.mouseDown)
      {
        this.g.DrawLine(new Pen(this.current_color, (float)this.current_width), new Point(this.oldX, this.oldY), new Point(e.X, e.Y));
        this.PointList.Add(new Rectangle(this.oldX, this.oldY, e.X, e.Y));
        this.oldX = e.X;
        this.oldY = e.Y;
        this.pictureBox1.Refresh();
      }
    }

    private void scribble_MouseUp(object sender, MouseEventArgs e)
    {
      this.mouseDown = false;
      this.oldX = -1;
      this.oldY = -1;
      this.pictureBox1.Refresh();
    }

    #endregion

    #region RubberBandMode

    private void rubberBandToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.rubberBandToolStripMenuItem.Checked = !this.rubberBandToolStripMenuItem.Checked;
      this.RubberBandMode = this.rubberBandToolStripMenuItem.Checked;
      this.新規作成NToolStripMenuItem.Enabled = this.RubberBandMode;
      this.編集EToolStripMenuItem.Enabled = this.RubberBandMode;
      this.画像GToolStripMenuItem.Enabled = this.RubberBandMode;
      this.新規作成NToolStripButton.Enabled = this.RubberBandMode;
      this.切り取りUToolStripButton.Enabled = this.RubberBandMode;
      this.コピーCToolStripButton.Enabled = this.RubberBandMode;
      this.貼り付けPToolStripButton.Enabled = this.RubberBandMode;
      this.開くOToolStripMenuItem.Enabled = !this.RubberBandMode;
      this.開くOToolStripButton.Enabled = !this.RubberBandMode;
      this.上書き保存SToolStripMenuItem.Enabled = !this.RubberBandMode;
      if (this.RubberBandMode)
      {
        if (this.ScribbleMode)
        {
          this.DeactivateScribbleMode(null, null);
          this.ScribbleMode = (this.scribbleToolStripMenuItem.Checked = false);
        }
        this.pictureBox1.Tag = "RubberBand";
        //((Form)base.Parent).Text = "RubberBand";
        this.ActivateRubberBandMode(null, null);
      }
      else
      {
        this.DeactivateRubberBandMode(null, null);
      }
    }

    private void rubberband_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        this.SetRubberbandOption();
      }
      else
      {
        this.mouse_point.SetPoints(e.Location, e.Location, 0);
        Point point = this.pictureBox1.PointToScreen(e.Location);
        this.drag_point.SetPoints(point, point, 0);
        this.drag_point.flag = true;
      }
    }

    private void rubberband_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.drag_point.flag)
      {
        this.DrawRubberband();
        this.drag_point.SetPoints(this.pictureBox1.PointToScreen(e.Location));
        this.DrawRubberband();
      }
    }

    private void rubberband_MouseUp(object sender, MouseEventArgs e)
    {
      this.DrawRubberband();
      this.drag_point.flag = false;
      this.mouse_point.SetPoints(e.Location, 0);
      this.DrawShape(this.mouse_point.start_point, this.mouse_point.end_point);
    }

    public void ActivateRubberBandMode(object sender, EventArgs e)
    {
      this.rub = new RubberBandDialog();
      this.rub.parent = this;
      this.rub.spxNumericUpDown.Maximum = this.pictureBox1.Size.Width - 1;
      this.rub.epxNumericUpDown.Maximum = this.pictureBox1.Size.Width - 1;
      this.rub.spyNumericUpDown.Maximum = this.pictureBox1.Size.Height - 1;
      this.rub.epyNumericUpDown.Maximum = this.pictureBox1.Size.Height - 1;
      this.pictureBox1.Image = null;
      if (this.bitmap1 == null)
      {
        this.bitmap1 = new Bitmap(this.pictureBox1.Size.Width, this.pictureBox1.Size.Height);
      }
      this.bitmap1.MakeTransparent(Color.FromArgb(211, 211, 211));
      this.pictureBox1.Image = this.bitmap1;
      this.g = Graphics.FromImage(this.pictureBox1.Image);
      this.pictureBox1.Refresh();
    }

    public void DeactivateRubberBandMode(object sender, EventArgs e)
    {
      string text = "/media/sf_ShareFolder/Picture/DSCN0166.JPG";
      if (AntPanel.IsRunningWindows)
      {
        text = "F:\\My Pictures\\20081004住化高槻同期会\\住化高槻45年同期会_008.JPG";
      }
      else
      {
        text = "/media/sf_ShareFolder/Picture/DSCN0166.JPG";
      }
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.Image = new Bitmap(this.pictureBox1.Size.Width, this.pictureBox1.Size.Height);
      try { this.rub.Dispose(); } catch { }
      this.pictureBox1.Tag = text;
      this.pictureBox1.Image = new Bitmap(text);
      //((Form)base.Parent).Text = Path.GetFileNameWithoutExtension(text);
    }

    private void SetRubberbandOption()
    {
      //this.rub.ShowDialog(this);
      if (this.rub.ShowDialog(this) != DialogResult.Cancel)
      {
        this.use_color = this.rub.use_color;
        this.shapeType = this.rub.shapeComboBox.Text;
        //MessageBox.Show(this.shapeType);
        int x = (int)this.rub.spxNumericUpDown.Value;
        int y = (int)this.rub.spyNumericUpDown.Value;
        int x2 = (int)this.rub.epxNumericUpDown.Value;
        int y2 = (int)this.rub.epyNumericUpDown.Value;
        this.lineWidth = (int)this.rub.lineNumericUpDown.Value;
        this.angle = (int)this.rub.angleUpDown1.Value;
        this.drawingOption = this.rub.drawingRadioButton.Checked;
        this.stringText = this.rub.stringTextBox.Text;
        this.page_font = this.rub.page_font;
        this.xrect = this.rub.xrect.Checked;
        this.yrect = this.rub.yrect.Checked;
        this.aspect = this.rub.aspect.Checked;
        if (this.rub.DialogResult == DialogResult.OK)
        {
          this.DrawShape(new Point(x, y), new Point(x2, y2));
        }
      }
    }

    private void DrawRubberband()
    {
      string text = this.shapeType;
      if (text != null)
      {
        if (text == "直線")
        {
          ControlPaint.DrawReversibleLine(this.drag_point.start_point, this.drag_point.end_point, this.BackColor);
          return;
        }
        if (!(text == "四角形") && !(text == "楕円"))
        {
        }
      }
      ControlPaint.DrawReversibleFrame(this.drag_point.GetDragRectangle(), this.BackColor, FrameStyle.Dashed);
    }

    private void DrawShape(Point sp, Point ep)
    {
      Pen pen;
      SolidBrush brush;
      if (this.drawingOption)
      {
        pen = new Pen(this.use_color, (float)this.lineWidth);
        brush = new SolidBrush(Color.Transparent);
      }
      else
      {
        pen = new Pen(this.use_color, 0f);
        brush = new SolidBrush(this.use_color);
      }
      if (this.shapeType == "編集")
      {
        if (this.mouse_point.GetRectangle().Height != 0 && this.mouse_point.GetRectangle().Width != 0)
        {
          this.selectionBitmap = this.bitmap1.Clone(this.mouse_point.GetRectangle(), this.bitmap1.PixelFormat);
          this.DrawRubberband();
        }
      }
      else if (this.shapeType == "全削除")
      {
        this.g.Clear(Color.Transparent);
        this.pictureBox1.Refresh();
      }
      else
      {
        if (this.shapeType == "直線")
        {
          this.g.DrawLine(pen, sp, ep);
        }
        else if (this.shapeType.ToString() == "四角形")
        {
          int num = Math.Abs(sp.X - ep.X);
          if (sp.X > ep.X)
          {
            sp.X = ep.X;
          }
          int num2 = Math.Abs(sp.Y - ep.Y);
          if (sp.Y > ep.Y)
          {
            sp.Y = ep.Y;
          }
          if (this.xrect)
          {
            num2 = num;
          }
          if (this.yrect)
          {
            num = num2;
          }
          if (this.drawingOption)
          {
            this.g.DrawRectangle(pen, sp.X, sp.Y, num, num2);
          }
          else
          {
            this.g.FillRectangle(brush, sp.X, sp.Y, num, num2);
          }
        }
        else if (this.shapeType == "楕円")
        {
          int num = Math.Abs(sp.X - ep.X);
          if (sp.X > ep.X)
          {
            sp.X = ep.X;
          }
          int num2 = Math.Abs(sp.Y - ep.Y);
          if (sp.Y > ep.Y)
          {
            sp.Y = ep.Y;
          }
          if (this.xrect)
          {
            num2 = num;
          }
          if (this.yrect)
          {
            num = num2;
          }
          if (this.drawingOption)
          {
            this.g.DrawEllipse(pen, sp.X, sp.Y, num, num2);
          }
          else
          {
            this.g.FillEllipse(brush, sp.X, sp.Y, num, num2);
          }
        }
        else if (this.shapeType == "文字列")
        {
          this.g.DrawString(this.stringText, this.page_font.font, brush, sp);
        }
        Bitmap bitmap = (Bitmap)this.bitmap1.Clone();
        bitmap.MakeTransparent(Color.FromArgb(211, 211, 211));
        this.history.Add(bitmap);
        this.pictureBox1.Refresh();
      }
    }
    #endregion

    #region QcGrapth
    private void qcGraphMenuItem_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
      string theme = toolStripMenuItem.Tag as string;
      this.DrawQcGraphItem(theme);
    }

    private void DrawQcGraphItem(string theme)
    {
      GlibSample glibSample = new GlibSample();
      this.RubberBandMode = (this.ScribbleMode = false);
      this.scribbleToolStripMenuItem.Checked = (this.rubberBandToolStripMenuItem.Checked = false);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
      this.pictureBox1.Image = new Bitmap(640, 480);
      Graphics graphics = Graphics.FromImage(this.pictureBox1.Image);
      switch (theme)
      {
        case "直線1":
          glibSample.samp11(graphics);
          break;
        case "直線2":
          glibSample.samp12(graphics);
          break;
        case "直線3":
          glibSample.samp13(graphics);
          break;
        case "三角形1":
          glibSample.samp14(graphics);
          break;
        case "三角形2":
          glibSample.samp15(graphics);
          break;
        case "直線の型":
          glibSample.samp23(graphics);
          break;
        case "濃淡の表示":
          glibSample.samp24(graphics);
          break;
        case "放射状の線":
          glibSample.samp25(graphics);
          break;
        case "星の形":
          glibSample.samp26(graphics);
          break;
        case "楕円の変換":
          glibSample.samp27(graphics);
          break;
        case "サイクロイド":
          glibSample.samp31(graphics);
          break;
        case "サイクロイド(星型)":
          glibSample.samp32(graphics);
          break;
        case "四葉形":
          glibSample.ex31(graphics);
          break;
        case "正六角形による図形":
          glibSample.ex32(graphics);
          break;
        case "正弦曲線":
          glibSample.samp41(graphics);
          break;
        case "扇":
          glibSample.samp42(graphics);
          break;
        case "三葉形の回転":
          glibSample.ex41(graphics);
          break;
        case "アステロイドの合成":
          glibSample.ex42(graphics);
          break;
        case "放射状の線の回転運動":
          glibSample.samp51(graphics);
          break;
        case "外転サイクロイド":
          glibSample.samp52(graphics);
          break;
        case "花の動画":
          glibSample.ex51(graphics);
          break;
        case "カージオイド上の正方形の運動":
          glibSample.ex52(graphics);
          break;
        case "ジューコフスキーの扇形":
          glibSample.samp61(graphics);
          break;
        case "流線":
          glibSample.samp62(graphics);
          break;
        case "wzczの写像":
          glibSample.ex61(graphics);
          break;
        case "球":
          glibSample.samp81(graphics);
          break;
        case "手毬":
          glibSample.samp82(graphics);
          break;
        case "球の回転":
          glibSample.ex81(graphics);
          break;
        case "マンデグローブ":
          glibSample.mandegrobe(graphics);
          break;
        case "コッホ曲線による図形":
          glibSample.samp71(graphics);
          break;
        case "雪の結晶":
          glibSample.samp72(graphics);
          break;
        case "盆栽":
          glibSample.ex71(graphics);
          break;
        case "雲":
          glibSample.ex72(graphics);
          break;
      }
      this.pictureBox1.Refresh();
      this.pictureBox1.Tag = theme;
      this.AddPreviousDocuments("qcgraph!" + theme);

      //((Form)base.Parent).Text = Path.GetFileNameWithoutExtension(theme);
      //this.newDocumentFlag = false;
    }
    #endregion






  }
}
