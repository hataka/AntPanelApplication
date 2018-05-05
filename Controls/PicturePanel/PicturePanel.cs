#undef Interface
//#define Interface

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using CommonLibrary;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using AntPlugin.CommonLibrary;
using AntPlugin.XMLTreeMenu.Controls;

namespace AntPlugin.XMLTreeMenu.Controls
{
#if Interface
#else  
  public partial class PicturePanel : UserControl
#endif
	{
	  #region PicturePanel Variables
	  //public static String baseDir = @"C:\Documents and Settings\kazuhiko\Local Settings\Application Data\FlashDevelop";

	  public List<string> previousDocuments = new List<string>();
	  public string currentPath = string.Empty;
	  public Point currentPoint = new Point(0, 0);
	  public PictureBox pictureBox1;
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
    private ToolStripButton imageListButton;
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
		  if(args[0] != String.Empty && File.Exists(args[0]) 
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
        if(File.Exists(this.path))
		    { 
          this.pictureBox1.Tag = this.path;
          this.pictureBox1.Image = new Bitmap(this.path);
			    
          // FIXME
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

      //System.InvalidCastException: 型
      //  'System.Windows.Forms.TabPage' のオブジェクトを型 'System.Windows.Forms.Form' にキャストできません。
      
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
			((Form)base.Parent).Text = "Scribble";
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
			this.DeactivateScribbleMode(null, null);
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
		((Form)base.Parent).Text = Path.GetFileNameWithoutExtension(text);
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
			((Form)base.Parent).Text = "RubberBand";
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
		string text = "F:\\My Pictures\\20081004住化高槻同期会\\住化高槻45年同期会_008.JPG";
		this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
		this.pictureBox1.Image = new Bitmap(this.pictureBox1.Size.Width, this.pictureBox1.Size.Height);
		this.rub.Dispose();
		this.pictureBox1.Tag = text;
		this.pictureBox1.Image = new Bitmap(text);
		((Form)base.Parent).Text = Path.GetFileNameWithoutExtension(text);
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
		((Form)base.Parent).Text = Path.GetFileNameWithoutExtension(theme);
		//this.newDocumentFlag = false;
	}
	#endregion

	  #region Form Variables
	public StatusStrip statusStrip1;
	public ToolStripStatusLabel toolStripStatusLabel1;
	public MenuStrip menuStrip1;
	private ToolStripMenuItem ファイルFToolStripMenuItem;
	private ToolStripMenuItem 新規作成NToolStripMenuItem;
	private ToolStripMenuItem 開くOToolStripMenuItem;
	private ToolStripSeparator toolStripSeparator;
	private ToolStripMenuItem 最近開いたファイルToolStripMenuItem;
	private ToolStripMenuItem 最近開いたファイルをクリアToolStripMenuItem;
	private ToolStripSeparator toolStripSeparator13;
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
	private ToolStripMenuItem 再描画ToolStripMenuItem;
	private ToolStripMenuItem すべて選択AToolStripMenuItem;
	private ToolStripSeparator toolStripSeparator10;
	private ToolStripMenuItem 表示ToolStripMenuItem;
	private ToolStripMenuItem ツールバーTToolStripMenuItem;
	private ToolStripMenuItem ステータスバーSToolStripMenuItem;
	private ToolStripSeparator toolStripSeparator8;
	private ToolStripMenuItem tabPageModeToolStripMenuItem;
	private ToolStripMenuItem updateUIToolStripMenuItem;
	private ToolStripMenuItem 画像GToolStripMenuItem;
	private ToolStripMenuItem 挿入IToolStripMenuItem;
	private ToolStripMenuItem 選択位置に挿入ToolStripMenuItem;
	private ToolStripMenuItem 選択範囲に挿入ToolStripMenuItem;
	private ToolStripMenuItem 選択範囲縦横比保持ToolStripMenuItem;
	private ToolStripSeparator toolStripSeparator11;
	private ToolStripMenuItem 変形貼り付けToolStripMenuItem;
	private ToolStripMenuItem 回転貼り付けToolStripMenuItem;
	private ToolStripMenuItem 回転ToolStripMenuItem;
	private ToolStripMenuItem 角度を指定ToolStripMenuItem;
	private ToolStripMenuItem 反転貼り付けToolStripMenuItem;
	private ToolStripSeparator toolStripSeparator12;
	private ToolStripMenuItem 全クリアToolStripMenuItem;
	private ToolStripMenuItem 背景色変更ToolStripMenuItem;
	public ToolStripMenuItem スクリプトCToolStripMenuItem;
	private ToolStripMenuItem スクリプトを編集EToolStripMenuItem;
	private ToolStripMenuItem スクリプトを実行XToolStripMenuItem;
	private ToolStripMenuItem スクリプトメニュー更新RToolStripMenuItem;
	private ToolStripSeparator toolStripSeparator28;
	private ToolStripMenuItem ツールTToolStripMenuItem;
	private ToolStripMenuItem オプションOToolStripMenuItem;
	private ToolStripMenuItem scribbleToolStripMenuItem;
	private ToolStripMenuItem rubberBandToolStripMenuItem;
	private ToolStripMenuItem カスタマイズCToolStripMenuItem;
	private ToolStripSeparator toolStripSeparator14;
	private ToolStripMenuItem 試験ToolStripMenuItem;
	public ToolStripMenuItem qcgraphToolStripMenuItem;
	public ToolStripMenuItem 直線と三角形ToolStripMenuItem;
	public ToolStripMenuItem 直線1ToolStripMenuItem;
	public ToolStripMenuItem 直線2ToolStripMenuItem;
	public ToolStripMenuItem 直線3ToolStripMenuItem;
	public ToolStripMenuItem 三角形1ToolStripMenuItem;
	public ToolStripMenuItem 三角形2ToolStripMenuItem;
	public ToolStripMenuItem 図形の基礎ToolStripMenuItem;
	public ToolStripMenuItem 直線の型ToolStripMenuItem;
	public ToolStripMenuItem 濃淡の表示ToolStripMenuItem;
	public ToolStripMenuItem 放射状の線ToolStripMenuItem;
	public ToolStripMenuItem 星の形ToolStripMenuItem;
	public ToolStripMenuItem 楕円の変換ToolStripMenuItem;
	public ToolStripMenuItem 軌跡ToolStripMenuItem;
	public ToolStripMenuItem サイクロイドToolStripMenuItem;
	public ToolStripMenuItem サイクロイド星型ToolStripMenuItem;
	public ToolStripMenuItem 四葉形ToolStripMenuItem;
	public ToolStripMenuItem 正六角形による図ToolStripMenuItem;
	public ToolStripMenuItem 曲線ToolStripMenuItem;
	public ToolStripMenuItem 正弦曲線ToolStripMenuItem;
	public ToolStripMenuItem 扇ToolStripMenuItem;
	public ToolStripMenuItem 三葉形の回転ToolStripMenuItem;
	public ToolStripMenuItem アステロイドの合成ToolStripMenuItem;
	public ToolStripMenuItem 動画ToolStripMenuItem;
	public ToolStripMenuItem 放射状の線の回転運動ToolStripMenuItem;
	public ToolStripMenuItem 外転サイクロイドToolStripMenuItem;
	public ToolStripMenuItem 花の動画ToolStripMenuItem;
	public ToolStripMenuItem カージオイド上の正方形の運動ToolStripMenuItem;
	public ToolStripMenuItem 写像ToolStripMenuItem;
	public ToolStripMenuItem ジューコフスキーの扇形ToolStripMenuItem;
	public ToolStripMenuItem 流線ToolStripMenuItem;
	public ToolStripMenuItem wzczの写像ToolStripMenuItem;
	public ToolStripMenuItem フラクタルToolStripMenuItem;
	public ToolStripMenuItem コッホ曲線による図形ToolStripMenuItem;
	public ToolStripMenuItem 雪の結晶ToolStripMenuItem;
	public ToolStripMenuItem 盆栽ToolStripMenuItem;
	public ToolStripMenuItem 雲ToolStripMenuItem;
	public ToolStripMenuItem 球の描画ToolStripMenuItem;
	public ToolStripMenuItem 球ToolStripMenuItem;
	public ToolStripMenuItem 手毬ToolStripMenuItem;
	public ToolStripMenuItem 球の回転ToolStripMenuItem;
	public ToolStripMenuItem その他ToolStripMenuItem;
	public ToolStripMenuItem マンデグローブToolStripMenuItem;
	private ToolStripMenuItem お気に入りToolStripMenuItem;
	private ToolStripMenuItem お気に入りに追加ToolStripMenuItem;
	private ToolStripMenuItem ヘルプHToolStripMenuItem;
	private ToolStripMenuItem 内容CToolStripMenuItem;
	private ToolStripMenuItem インデックスIToolStripMenuItem;
	private ToolStripMenuItem 検索SToolStripMenuItem;
	private ToolStripSeparator toolStripSeparator5;
	private ToolStripMenuItem バージョン情報AToolStripMenuItem;
	public ToolStrip toolStrip1;
	private ToolStripButton 新規作成NToolStripButton;
	private ToolStripButton 開くOToolStripButton;
	private ToolStripButton 上書き保存SToolStripButton;
	private ToolStripButton 印刷PToolStripButton;
	private ToolStripSeparator toolStripSeparator6;
	private ToolStripButton 切り取りUToolStripButton;
	private ToolStripButton コピーCToolStripButton;
	private ToolStripButton 貼り付けPToolStripButton;
	private ToolStripButton ヘルプLToolStripButton;
	private ToolStripSeparator toolStripSeparator7;
	private ToolStripDropDownButton toolStripDropDownButton1;
	private ToolStripMenuItem メニューバーMToolStripMenuItem;
	private ToolStripMenuItem ステータスバーSToolStripMenuItem1;
	private ToolStripSeparator toolStripSeparator9;
	#endregion

  	#region Windows フォーム デザイナで生成されたコード
	/// <summary>
	/// 必要なデザイナ変数です。
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// 使用中のリソースをすべてクリーンアップします。
	/// </summary>
	/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}


	/// <summary>
	/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
	/// コード エディタで変更しないでください。
	/// </summary>
	private void InitializeComponent()
	{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PicturePanel));
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.新規作成NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.最近開いたファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.最近開いたファイルをクリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.上書き保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.名前を付けて保存AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.印刷PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.印刷プレビューVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.元に戻すUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.やり直しRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.切り取りTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.コピーCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.貼り付けPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.再描画ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.すべて選択AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ツールバーTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ステータスバーSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.tabPageModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.updateUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.画像GToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.挿入IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.選択位置に挿入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.選択範囲に挿入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.選択範囲縦横比保持ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.変形貼り付けToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.回転貼り付けToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.回転ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.角度を指定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.反転貼り付けToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.全クリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.背景色変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトを編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトを実行XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.スクリプトメニュー更新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
      this.ツールTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.オプションOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.scribbleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.rubberBandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.カスタマイズCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
      this.試験ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.qcgraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.直線と三角形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.直線1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.直線2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.直線3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.三角形1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.三角形2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.図形の基礎ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.直線の型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.濃淡の表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.放射状の線ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.星の形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.楕円の変換ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.軌跡ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.サイクロイドToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.サイクロイド星型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.四葉形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.正六角形による図ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.曲線ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.正弦曲線ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.扇ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.三葉形の回転ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.アステロイドの合成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.動画ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.放射状の線の回転運動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.外転サイクロイドToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.花の動画ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.カージオイド上の正方形の運動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.写像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ジューコフスキーの扇形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.流線ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.wzczの写像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.フラクタルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.コッホ曲線による図形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.雪の結晶ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.盆栽ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.雲ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.球の描画ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.球ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.手毬ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.球の回転ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.その他ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.マンデグローブToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.お気に入りToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.お気に入りに追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.内容CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.インデックスIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.検索SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.バージョン情報AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.新規作成NToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.開くOToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.上書き保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.印刷PToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.切り取りUToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.コピーCToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.貼り付けPToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.ヘルプLToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.メニューバーMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ステータスバーSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.imageListButton = new System.Windows.Forms.ToolStripButton();
      this.statusStrip1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
      this.statusStrip1.Location = new System.Drawing.Point(0, 369);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(766, 22);
      this.statusStrip1.TabIndex = 5;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(127, 17);
      this.toolStripStatusLabel1.Text = "こんにちわPictureBoxです";
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.表示ToolStripMenuItem,
            this.画像GToolStripMenuItem,
            this.スクリプトCToolStripMenuItem,
            this.ツールTToolStripMenuItem,
            this.qcgraphToolStripMenuItem,
            this.お気に入りToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(766, 24);
      this.menuStrip1.TabIndex = 6;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // ファイルFToolStripMenuItem
      // 
      this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成NToolStripMenuItem,
            this.開くOToolStripMenuItem,
            this.toolStripSeparator,
            this.最近開いたファイルToolStripMenuItem,
            this.toolStripSeparator13,
            this.上書き保存SToolStripMenuItem,
            this.名前を付けて保存AToolStripMenuItem,
            this.toolStripSeparator1,
            this.印刷PToolStripMenuItem,
            this.印刷プレビューVToolStripMenuItem,
            this.toolStripSeparator2,
            this.終了XToolStripMenuItem});
      this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
      this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
      this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
      // 
      // 新規作成NToolStripMenuItem
      // 
      this.新規作成NToolStripMenuItem.Enabled = false;
      this.新規作成NToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新規作成NToolStripMenuItem.Image")));
      this.新規作成NToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.新規作成NToolStripMenuItem.Name = "新規作成NToolStripMenuItem";
      this.新規作成NToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
      this.新規作成NToolStripMenuItem.Text = "新規作成(&N)";
      this.新規作成NToolStripMenuItem.Click += new System.EventHandler(this.新規作成NToolStripMenuItem_Click);
      // 
      // 開くOToolStripMenuItem
      // 
      this.開くOToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripMenuItem.Image")));
      this.開くOToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
      this.開くOToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
      this.開くOToolStripMenuItem.Text = "開く(&O)";
      this.開くOToolStripMenuItem.Click += new System.EventHandler(this.開くOToolStripMenuItem_Click);
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(172, 6);
      // 
      // 最近開いたファイルToolStripMenuItem
      // 
      this.最近開いたファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最近開いたファイルをクリアToolStripMenuItem});
      this.最近開いたファイルToolStripMenuItem.Name = "最近開いたファイルToolStripMenuItem";
      this.最近開いたファイルToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
      this.最近開いたファイルToolStripMenuItem.Text = "最近開いたファイル(&R)";
      // 
      // 最近開いたファイルをクリアToolStripMenuItem
      // 
      this.最近開いたファイルをクリアToolStripMenuItem.Name = "最近開いたファイルをクリアToolStripMenuItem";
      this.最近開いたファイルをクリアToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
      this.最近開いたファイルをクリアToolStripMenuItem.Text = "最近開いたファイルをクリア(&C)";
      this.最近開いたファイルをクリアToolStripMenuItem.Click += new System.EventHandler(this.最近開いたファイルをクリアToolStripMenuItem_Click);
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new System.Drawing.Size(172, 6);
      // 
      // 上書き保存SToolStripMenuItem
      // 
      this.上書き保存SToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("上書き保存SToolStripMenuItem.Image")));
      this.上書き保存SToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.上書き保存SToolStripMenuItem.Name = "上書き保存SToolStripMenuItem";
      this.上書き保存SToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
      this.上書き保存SToolStripMenuItem.Text = "上書き保存(&S)";
      this.上書き保存SToolStripMenuItem.Click += new System.EventHandler(this.上書き保存SToolStripMenuItem_Click);
      // 
      // 名前を付けて保存AToolStripMenuItem
      // 
      this.名前を付けて保存AToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("名前を付けて保存AToolStripMenuItem.Image")));
      this.名前を付けて保存AToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.名前を付けて保存AToolStripMenuItem.Name = "名前を付けて保存AToolStripMenuItem";
      this.名前を付けて保存AToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
      this.名前を付けて保存AToolStripMenuItem.Text = "名前を付けて保存(&A)";
      this.名前を付けて保存AToolStripMenuItem.Click += new System.EventHandler(this.名前を付けて保存AToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
      // 
      // 印刷PToolStripMenuItem
      // 
      this.印刷PToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripMenuItem.Image")));
      this.印刷PToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷PToolStripMenuItem.Name = "印刷PToolStripMenuItem";
      this.印刷PToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
      this.印刷PToolStripMenuItem.Text = "印刷(&P)";
      this.印刷PToolStripMenuItem.Click += new System.EventHandler(this.印刷PToolStripMenuItem_Click);
      // 
      // 印刷プレビューVToolStripMenuItem
      // 
      this.印刷プレビューVToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("印刷プレビューVToolStripMenuItem.Image")));
      this.印刷プレビューVToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷プレビューVToolStripMenuItem.Name = "印刷プレビューVToolStripMenuItem";
      this.印刷プレビューVToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
      this.印刷プレビューVToolStripMenuItem.Text = "印刷プレビュー(&V)";
      this.印刷プレビューVToolStripMenuItem.Click += new System.EventHandler(this.印刷プレビューVToolStripMenuItem_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(172, 6);
      // 
      // 終了XToolStripMenuItem
      // 
      this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
      this.終了XToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
      this.終了XToolStripMenuItem.Text = "終了(&X)";
      this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
      // 
      // 編集EToolStripMenuItem
      // 
      this.編集EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.元に戻すUToolStripMenuItem,
            this.やり直しRToolStripMenuItem,
            this.toolStripSeparator3,
            this.切り取りTToolStripMenuItem,
            this.コピーCToolStripMenuItem,
            this.貼り付けPToolStripMenuItem,
            this.toolStripSeparator4,
            this.再描画ToolStripMenuItem,
            this.すべて選択AToolStripMenuItem,
            this.toolStripSeparator10});
      this.編集EToolStripMenuItem.Enabled = false;
      this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
      this.編集EToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
      this.編集EToolStripMenuItem.Text = "編集(&E)";
      // 
      // 元に戻すUToolStripMenuItem
      // 
      this.元に戻すUToolStripMenuItem.Name = "元に戻すUToolStripMenuItem";
      this.元に戻すUToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.元に戻すUToolStripMenuItem.Text = "元に戻す(&U)";
      this.元に戻すUToolStripMenuItem.Click += new System.EventHandler(this.元に戻すUToolStripMenuItem_Click);
      // 
      // やり直しRToolStripMenuItem
      // 
      this.やり直しRToolStripMenuItem.Name = "やり直しRToolStripMenuItem";
      this.やり直しRToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.やり直しRToolStripMenuItem.Text = "やり直し(&R)";
      this.やり直しRToolStripMenuItem.Click += new System.EventHandler(this.やり直しRToolStripMenuItem_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
      // 
      // 切り取りTToolStripMenuItem
      // 
      this.切り取りTToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("切り取りTToolStripMenuItem.Image")));
      this.切り取りTToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.切り取りTToolStripMenuItem.Name = "切り取りTToolStripMenuItem";
      this.切り取りTToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.切り取りTToolStripMenuItem.Text = "切り取り(&T)";
      this.切り取りTToolStripMenuItem.Click += new System.EventHandler(this.切り取りTToolStripMenuItem_Click);
      // 
      // コピーCToolStripMenuItem
      // 
      this.コピーCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripMenuItem.Image")));
      this.コピーCToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.コピーCToolStripMenuItem.Name = "コピーCToolStripMenuItem";
      this.コピーCToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.コピーCToolStripMenuItem.Text = "コピー(&C)";
      this.コピーCToolStripMenuItem.Click += new System.EventHandler(this.コピーCToolStripMenuItem_Click);
      // 
      // 貼り付けPToolStripMenuItem
      // 
      this.貼り付けPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripMenuItem.Image")));
      this.貼り付けPToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
      this.貼り付けPToolStripMenuItem.Name = "貼り付けPToolStripMenuItem";
      this.貼り付けPToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.貼り付けPToolStripMenuItem.Text = "貼り付け(&P)";
      this.貼り付けPToolStripMenuItem.Click += new System.EventHandler(this.貼り付けPToolStripMenuItem_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
      // 
      // 再描画ToolStripMenuItem
      // 
      this.再描画ToolStripMenuItem.Name = "再描画ToolStripMenuItem";
      this.再描画ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.再描画ToolStripMenuItem.Text = "再描画";
      this.再描画ToolStripMenuItem.Click += new System.EventHandler(this.再描画ToolStripMenuItem_Click);
      // 
      // すべて選択AToolStripMenuItem
      // 
      this.すべて選択AToolStripMenuItem.Name = "すべて選択AToolStripMenuItem";
      this.すべて選択AToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.すべて選択AToolStripMenuItem.Text = "すべて選択(&A)";
      this.すべて選択AToolStripMenuItem.Click += new System.EventHandler(this.すべて選択AToolStripMenuItem_Click);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(149, 6);
      // 
      // 表示ToolStripMenuItem
      // 
      this.表示ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ツールバーTToolStripMenuItem,
            this.ステータスバーSToolStripMenuItem,
            this.toolStripSeparator8,
            this.tabPageModeToolStripMenuItem,
            this.updateUIToolStripMenuItem});
      this.表示ToolStripMenuItem.Name = "表示ToolStripMenuItem";
      this.表示ToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
      this.表示ToolStripMenuItem.Text = "表示(&V)";
      // 
      // ツールバーTToolStripMenuItem
      // 
      this.ツールバーTToolStripMenuItem.CheckOnClick = true;
      this.ツールバーTToolStripMenuItem.Name = "ツールバーTToolStripMenuItem";
      this.ツールバーTToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
      this.ツールバーTToolStripMenuItem.Text = "ツールバー(&T)";
      this.ツールバーTToolStripMenuItem.Click += new System.EventHandler(this.ツールバーTToolStripMenuItem_Click);
      // 
      // ステータスバーSToolStripMenuItem
      // 
      this.ステータスバーSToolStripMenuItem.CheckOnClick = true;
      this.ステータスバーSToolStripMenuItem.Name = "ステータスバーSToolStripMenuItem";
      this.ステータスバーSToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
      this.ステータスバーSToolStripMenuItem.Text = "ステータスバー(&S)";
      this.ステータスバーSToolStripMenuItem.Click += new System.EventHandler(this.ステータスバーSToolStripMenuItem_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(147, 6);
      // 
      // tabPageModeToolStripMenuItem
      // 
      this.tabPageModeToolStripMenuItem.CheckOnClick = true;
      this.tabPageModeToolStripMenuItem.Enabled = false;
      this.tabPageModeToolStripMenuItem.Name = "tabPageModeToolStripMenuItem";
      this.tabPageModeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
      this.tabPageModeToolStripMenuItem.Text = "TabPage Mode";
      // 
      // updateUIToolStripMenuItem
      // 
      this.updateUIToolStripMenuItem.Enabled = false;
      this.updateUIToolStripMenuItem.Name = "updateUIToolStripMenuItem";
      this.updateUIToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
      this.updateUIToolStripMenuItem.Text = "UpdateUI";
      // 
      // 画像GToolStripMenuItem
      // 
      this.画像GToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.挿入IToolStripMenuItem,
            this.toolStripSeparator11,
            this.変形貼り付けToolStripMenuItem,
            this.回転貼り付けToolStripMenuItem,
            this.反転貼り付けToolStripMenuItem,
            this.toolStripSeparator12,
            this.全クリアToolStripMenuItem,
            this.背景色変更ToolStripMenuItem});
      this.画像GToolStripMenuItem.Enabled = false;
      this.画像GToolStripMenuItem.Name = "画像GToolStripMenuItem";
      this.画像GToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
      this.画像GToolStripMenuItem.Text = "画像(&G)";
      // 
      // 挿入IToolStripMenuItem
      // 
      this.挿入IToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.選択位置に挿入ToolStripMenuItem,
            this.選択範囲に挿入ToolStripMenuItem,
            this.選択範囲縦横比保持ToolStripMenuItem});
      this.挿入IToolStripMenuItem.Name = "挿入IToolStripMenuItem";
      this.挿入IToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
      this.挿入IToolStripMenuItem.Text = "挿入...(&I)";
      // 
      // 選択位置に挿入ToolStripMenuItem
      // 
      this.選択位置に挿入ToolStripMenuItem.Name = "選択位置に挿入ToolStripMenuItem";
      this.選択位置に挿入ToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
      this.選択位置に挿入ToolStripMenuItem.Text = "選択位置に挿入";
      this.選択位置に挿入ToolStripMenuItem.Click += new System.EventHandler(this.選択位置に挿入ToolStripMenuItem_Click);
      // 
      // 選択範囲に挿入ToolStripMenuItem
      // 
      this.選択範囲に挿入ToolStripMenuItem.Name = "選択範囲に挿入ToolStripMenuItem";
      this.選択範囲に挿入ToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
      this.選択範囲に挿入ToolStripMenuItem.Text = "選択範囲に挿入";
      this.選択範囲に挿入ToolStripMenuItem.Click += new System.EventHandler(this.選択範囲に挿入ToolStripMenuItem_Click);
      // 
      // 選択範囲縦横比保持ToolStripMenuItem
      // 
      this.選択範囲縦横比保持ToolStripMenuItem.Name = "選択範囲縦横比保持ToolStripMenuItem";
      this.選択範囲縦横比保持ToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
      this.選択範囲縦横比保持ToolStripMenuItem.Text = "選択範囲(縦横比保持)";
      this.選択範囲縦横比保持ToolStripMenuItem.Click += new System.EventHandler(this.選択範囲縦横比保持ToolStripMenuItem_Click);
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(133, 6);
      // 
      // 変形貼り付けToolStripMenuItem
      // 
      this.変形貼り付けToolStripMenuItem.Name = "変形貼り付けToolStripMenuItem";
      this.変形貼り付けToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
      this.変形貼り付けToolStripMenuItem.Text = "変形貼り付け";
      this.変形貼り付けToolStripMenuItem.Click += new System.EventHandler(this.変形貼り付けToolStripMenuItem_Click);
      // 
      // 回転貼り付けToolStripMenuItem
      // 
      this.回転貼り付けToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.回転ToolStripMenuItem,
            this.角度を指定ToolStripMenuItem});
      this.回転貼り付けToolStripMenuItem.Name = "回転貼り付けToolStripMenuItem";
      this.回転貼り付けToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
      this.回転貼り付けToolStripMenuItem.Text = "回転貼り付け";
      // 
      // 回転ToolStripMenuItem
      // 
      this.回転ToolStripMenuItem.Name = "回転ToolStripMenuItem";
      this.回転ToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.回転ToolStripMenuItem.Text = "90°";
      this.回転ToolStripMenuItem.Click += new System.EventHandler(this.回転ToolStripMenuItem_Click);
      // 
      // 角度を指定ToolStripMenuItem
      // 
      this.角度を指定ToolStripMenuItem.Name = "角度を指定ToolStripMenuItem";
      this.角度を指定ToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.角度を指定ToolStripMenuItem.Text = "角度を指定";
      this.角度を指定ToolStripMenuItem.Click += new System.EventHandler(this.角度を指定ToolStripMenuItem_Click);
      // 
      // 反転貼り付けToolStripMenuItem
      // 
      this.反転貼り付けToolStripMenuItem.Name = "反転貼り付けToolStripMenuItem";
      this.反転貼り付けToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
      this.反転貼り付けToolStripMenuItem.Text = "反転貼り付け";
      this.反転貼り付けToolStripMenuItem.Click += new System.EventHandler(this.反転貼り付けToolStripMenuItem_Click);
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(133, 6);
      // 
      // 全クリアToolStripMenuItem
      // 
      this.全クリアToolStripMenuItem.Name = "全クリアToolStripMenuItem";
      this.全クリアToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
      this.全クリアToolStripMenuItem.Text = "全クリア";
      this.全クリアToolStripMenuItem.Click += new System.EventHandler(this.全クリアToolStripMenuItem_Click);
      // 
      // 背景色変更ToolStripMenuItem
      // 
      this.背景色変更ToolStripMenuItem.Name = "背景色変更ToolStripMenuItem";
      this.背景色変更ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
      this.背景色変更ToolStripMenuItem.Text = "背景色変更";
      this.背景色変更ToolStripMenuItem.Click += new System.EventHandler(this.背景色変更ToolStripMenuItem_Click);
      // 
      // スクリプトCToolStripMenuItem
      // 
      this.スクリプトCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.スクリプトを編集EToolStripMenuItem,
            this.スクリプトを実行XToolStripMenuItem,
            this.スクリプトメニュー更新RToolStripMenuItem,
            this.toolStripSeparator28});
      this.スクリプトCToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
      this.スクリプトCToolStripMenuItem.Name = "スクリプトCToolStripMenuItem";
      this.スクリプトCToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
      this.スクリプトCToolStripMenuItem.Text = "スクリプト(&C)";
      // 
      // スクリプトを編集EToolStripMenuItem
      // 
      this.スクリプトを編集EToolStripMenuItem.Name = "スクリプトを編集EToolStripMenuItem";
      this.スクリプトを編集EToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
      this.スクリプトを編集EToolStripMenuItem.Text = "スクリプトを編集(&E)";
      this.スクリプトを編集EToolStripMenuItem.Click += new System.EventHandler(this.スクリプトを編集EToolStripMenuItem_Click);
      // 
      // スクリプトを実行XToolStripMenuItem
      // 
      this.スクリプトを実行XToolStripMenuItem.Enabled = false;
      this.スクリプトを実行XToolStripMenuItem.Name = "スクリプトを実行XToolStripMenuItem";
      this.スクリプトを実行XToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
      this.スクリプトを実行XToolStripMenuItem.Text = "スクリプトを実行(&X)";
      // 
      // スクリプトメニュー更新RToolStripMenuItem
      // 
      this.スクリプトメニュー更新RToolStripMenuItem.Name = "スクリプトメニュー更新RToolStripMenuItem";
      this.スクリプトメニュー更新RToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
      this.スクリプトメニュー更新RToolStripMenuItem.Text = "スクリプトメニュー更新(&R)";
      this.スクリプトメニュー更新RToolStripMenuItem.Click += new System.EventHandler(this.スクリプトメニュー更新RToolStripMenuItem_Click);
      // 
      // toolStripSeparator28
      // 
      this.toolStripSeparator28.Name = "toolStripSeparator28";
      this.toolStripSeparator28.Size = new System.Drawing.Size(183, 6);
      // 
      // ツールTToolStripMenuItem
      // 
      this.ツールTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.オプションOToolStripMenuItem,
            this.カスタマイズCToolStripMenuItem,
            this.toolStripSeparator14,
            this.試験ToolStripMenuItem});
      this.ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem";
      this.ツールTToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
      this.ツールTToolStripMenuItem.Text = "ツール(&T)";
      // 
      // オプションOToolStripMenuItem
      // 
      this.オプションOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scribbleToolStripMenuItem,
            this.rubberBandToolStripMenuItem});
      this.オプションOToolStripMenuItem.Name = "オプションOToolStripMenuItem";
      this.オプションOToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
      this.オプションOToolStripMenuItem.Text = "オプション(&O)";
      // 
      // scribbleToolStripMenuItem
      // 
      this.scribbleToolStripMenuItem.Name = "scribbleToolStripMenuItem";
      this.scribbleToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
      this.scribbleToolStripMenuItem.Text = "Scribble";
      this.scribbleToolStripMenuItem.Click += new System.EventHandler(this.scribbleToolStripMenuItem_Click_1);
      // 
      // rubberBandToolStripMenuItem
      // 
      this.rubberBandToolStripMenuItem.Name = "rubberBandToolStripMenuItem";
      this.rubberBandToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
      this.rubberBandToolStripMenuItem.Text = "RubberBand";
      this.rubberBandToolStripMenuItem.Click += new System.EventHandler(this.rubberBandToolStripMenuItem_Click);
      // 
      // カスタマイズCToolStripMenuItem
      // 
      this.カスタマイズCToolStripMenuItem.Name = "カスタマイズCToolStripMenuItem";
      this.カスタマイズCToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
      this.カスタマイズCToolStripMenuItem.Text = "カスタマイズ(&C)";
      // 
      // toolStripSeparator14
      // 
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      this.toolStripSeparator14.Size = new System.Drawing.Size(137, 6);
      // 
      // 試験ToolStripMenuItem
      // 
      this.試験ToolStripMenuItem.Name = "試験ToolStripMenuItem";
      this.試験ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
      this.試験ToolStripMenuItem.Text = "試験";
      this.試験ToolStripMenuItem.Click += new System.EventHandler(this.試験ToolStripMenuItem_Click);
      // 
      // qcgraphToolStripMenuItem
      // 
      this.qcgraphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.直線と三角形ToolStripMenuItem,
            this.図形の基礎ToolStripMenuItem,
            this.軌跡ToolStripMenuItem,
            this.曲線ToolStripMenuItem,
            this.動画ToolStripMenuItem,
            this.写像ToolStripMenuItem,
            this.フラクタルToolStripMenuItem,
            this.球の描画ToolStripMenuItem,
            this.その他ToolStripMenuItem});
      this.qcgraphToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
      this.qcgraphToolStripMenuItem.Name = "qcgraphToolStripMenuItem";
      this.qcgraphToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
      this.qcgraphToolStripMenuItem.Text = "qcgraph";
      // 
      // 直線と三角形ToolStripMenuItem
      // 
      this.直線と三角形ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.直線1ToolStripMenuItem,
            this.直線2ToolStripMenuItem,
            this.直線3ToolStripMenuItem,
            this.三角形1ToolStripMenuItem,
            this.三角形2ToolStripMenuItem});
      this.直線と三角形ToolStripMenuItem.Name = "直線と三角形ToolStripMenuItem";
      this.直線と三角形ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.直線と三角形ToolStripMenuItem.Text = "直線と三角形";
      // 
      // 直線1ToolStripMenuItem
      // 
      this.直線1ToolStripMenuItem.Name = "直線1ToolStripMenuItem";
      this.直線1ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
      this.直線1ToolStripMenuItem.Tag = "直線1";
      this.直線1ToolStripMenuItem.Text = "直線1";
      this.直線1ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 直線2ToolStripMenuItem
      // 
      this.直線2ToolStripMenuItem.Name = "直線2ToolStripMenuItem";
      this.直線2ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
      this.直線2ToolStripMenuItem.Tag = "直線2";
      this.直線2ToolStripMenuItem.Text = "直線2";
      this.直線2ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 直線3ToolStripMenuItem
      // 
      this.直線3ToolStripMenuItem.Name = "直線3ToolStripMenuItem";
      this.直線3ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
      this.直線3ToolStripMenuItem.Tag = "直線3";
      this.直線3ToolStripMenuItem.Text = "直線3";
      this.直線3ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 三角形1ToolStripMenuItem
      // 
      this.三角形1ToolStripMenuItem.Name = "三角形1ToolStripMenuItem";
      this.三角形1ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
      this.三角形1ToolStripMenuItem.Tag = "三角形1";
      this.三角形1ToolStripMenuItem.Text = "三角形1";
      this.三角形1ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 三角形2ToolStripMenuItem
      // 
      this.三角形2ToolStripMenuItem.Name = "三角形2ToolStripMenuItem";
      this.三角形2ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
      this.三角形2ToolStripMenuItem.Tag = "三角形2";
      this.三角形2ToolStripMenuItem.Text = "三角形2";
      this.三角形2ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 図形の基礎ToolStripMenuItem
      // 
      this.図形の基礎ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.直線の型ToolStripMenuItem,
            this.濃淡の表示ToolStripMenuItem,
            this.放射状の線ToolStripMenuItem,
            this.星の形ToolStripMenuItem,
            this.楕円の変換ToolStripMenuItem});
      this.図形の基礎ToolStripMenuItem.Name = "図形の基礎ToolStripMenuItem";
      this.図形の基礎ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.図形の基礎ToolStripMenuItem.Text = "図形の基礎";
      // 
      // 直線の型ToolStripMenuItem
      // 
      this.直線の型ToolStripMenuItem.Name = "直線の型ToolStripMenuItem";
      this.直線の型ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
      this.直線の型ToolStripMenuItem.Tag = "直線の型";
      this.直線の型ToolStripMenuItem.Text = "直線の型";
      this.直線の型ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 濃淡の表示ToolStripMenuItem
      // 
      this.濃淡の表示ToolStripMenuItem.Name = "濃淡の表示ToolStripMenuItem";
      this.濃淡の表示ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
      this.濃淡の表示ToolStripMenuItem.Tag = "濃淡の表示";
      this.濃淡の表示ToolStripMenuItem.Text = "濃淡の表示";
      // 
      // 放射状の線ToolStripMenuItem
      // 
      this.放射状の線ToolStripMenuItem.Name = "放射状の線ToolStripMenuItem";
      this.放射状の線ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
      this.放射状の線ToolStripMenuItem.Tag = "放射状の線";
      this.放射状の線ToolStripMenuItem.Text = "放射状の線";
      this.放射状の線ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 星の形ToolStripMenuItem
      // 
      this.星の形ToolStripMenuItem.Name = "星の形ToolStripMenuItem";
      this.星の形ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
      this.星の形ToolStripMenuItem.Tag = "星の形";
      this.星の形ToolStripMenuItem.Text = "星の形";
      this.星の形ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 楕円の変換ToolStripMenuItem
      // 
      this.楕円の変換ToolStripMenuItem.Name = "楕円の変換ToolStripMenuItem";
      this.楕円の変換ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
      this.楕円の変換ToolStripMenuItem.Tag = "楕円の変換";
      this.楕円の変換ToolStripMenuItem.Text = "楕円の変換";
      this.楕円の変換ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 軌跡ToolStripMenuItem
      // 
      this.軌跡ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.サイクロイドToolStripMenuItem,
            this.サイクロイド星型ToolStripMenuItem,
            this.四葉形ToolStripMenuItem,
            this.正六角形による図ToolStripMenuItem});
      this.軌跡ToolStripMenuItem.Name = "軌跡ToolStripMenuItem";
      this.軌跡ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.軌跡ToolStripMenuItem.Text = "軌跡";
      // 
      // サイクロイドToolStripMenuItem
      // 
      this.サイクロイドToolStripMenuItem.Name = "サイクロイドToolStripMenuItem";
      this.サイクロイドToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
      this.サイクロイドToolStripMenuItem.Tag = "サイクロイド";
      this.サイクロイドToolStripMenuItem.Text = "サイクロイド";
      this.サイクロイドToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // サイクロイド星型ToolStripMenuItem
      // 
      this.サイクロイド星型ToolStripMenuItem.Name = "サイクロイド星型ToolStripMenuItem";
      this.サイクロイド星型ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
      this.サイクロイド星型ToolStripMenuItem.Tag = "サイクロイド(星型)";
      this.サイクロイド星型ToolStripMenuItem.Text = "サイクロイド(星型)";
      this.サイクロイド星型ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 四葉形ToolStripMenuItem
      // 
      this.四葉形ToolStripMenuItem.Name = "四葉形ToolStripMenuItem";
      this.四葉形ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
      this.四葉形ToolStripMenuItem.Tag = "四葉形";
      this.四葉形ToolStripMenuItem.Text = "四葉形";
      this.四葉形ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 正六角形による図ToolStripMenuItem
      // 
      this.正六角形による図ToolStripMenuItem.Name = "正六角形による図ToolStripMenuItem";
      this.正六角形による図ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
      this.正六角形による図ToolStripMenuItem.Tag = "正六角形による図形";
      this.正六角形による図ToolStripMenuItem.Text = "正六角形による図形";
      this.正六角形による図ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 曲線ToolStripMenuItem
      // 
      this.曲線ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.正弦曲線ToolStripMenuItem,
            this.扇ToolStripMenuItem,
            this.三葉形の回転ToolStripMenuItem,
            this.アステロイドの合成ToolStripMenuItem});
      this.曲線ToolStripMenuItem.Name = "曲線ToolStripMenuItem";
      this.曲線ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.曲線ToolStripMenuItem.Text = "曲線";
      // 
      // 正弦曲線ToolStripMenuItem
      // 
      this.正弦曲線ToolStripMenuItem.Name = "正弦曲線ToolStripMenuItem";
      this.正弦曲線ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
      this.正弦曲線ToolStripMenuItem.Tag = "正弦曲線";
      this.正弦曲線ToolStripMenuItem.Text = "正弦曲線";
      this.正弦曲線ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 扇ToolStripMenuItem
      // 
      this.扇ToolStripMenuItem.Name = "扇ToolStripMenuItem";
      this.扇ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
      this.扇ToolStripMenuItem.Tag = "扇";
      this.扇ToolStripMenuItem.Text = "扇";
      this.扇ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 三葉形の回転ToolStripMenuItem
      // 
      this.三葉形の回転ToolStripMenuItem.Name = "三葉形の回転ToolStripMenuItem";
      this.三葉形の回転ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
      this.三葉形の回転ToolStripMenuItem.Tag = "三葉形の回転";
      this.三葉形の回転ToolStripMenuItem.Text = "三葉形の回転";
      this.三葉形の回転ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // アステロイドの合成ToolStripMenuItem
      // 
      this.アステロイドの合成ToolStripMenuItem.Name = "アステロイドの合成ToolStripMenuItem";
      this.アステロイドの合成ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
      this.アステロイドの合成ToolStripMenuItem.Tag = "アステロイドの合成";
      this.アステロイドの合成ToolStripMenuItem.Text = "アステロイドの合成";
      this.アステロイドの合成ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 動画ToolStripMenuItem
      // 
      this.動画ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.放射状の線の回転運動ToolStripMenuItem,
            this.外転サイクロイドToolStripMenuItem,
            this.花の動画ToolStripMenuItem,
            this.カージオイド上の正方形の運動ToolStripMenuItem});
      this.動画ToolStripMenuItem.Name = "動画ToolStripMenuItem";
      this.動画ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.動画ToolStripMenuItem.Text = "動画";
      // 
      // 放射状の線の回転運動ToolStripMenuItem
      // 
      this.放射状の線の回転運動ToolStripMenuItem.Name = "放射状の線の回転運動ToolStripMenuItem";
      this.放射状の線の回転運動ToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
      this.放射状の線の回転運動ToolStripMenuItem.Tag = "放射状の線の回転運動";
      this.放射状の線の回転運動ToolStripMenuItem.Text = "放射状の線の回転運動";
      this.放射状の線の回転運動ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 外転サイクロイドToolStripMenuItem
      // 
      this.外転サイクロイドToolStripMenuItem.Name = "外転サイクロイドToolStripMenuItem";
      this.外転サイクロイドToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
      this.外転サイクロイドToolStripMenuItem.Tag = "外転サイクロイド";
      this.外転サイクロイドToolStripMenuItem.Text = "外転サイクロイド";
      this.外転サイクロイドToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 花の動画ToolStripMenuItem
      // 
      this.花の動画ToolStripMenuItem.Name = "花の動画ToolStripMenuItem";
      this.花の動画ToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
      this.花の動画ToolStripMenuItem.Tag = "花の動画";
      this.花の動画ToolStripMenuItem.Text = "花の動画";
      this.花の動画ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // カージオイド上の正方形の運動ToolStripMenuItem
      // 
      this.カージオイド上の正方形の運動ToolStripMenuItem.Name = "カージオイド上の正方形の運動ToolStripMenuItem";
      this.カージオイド上の正方形の運動ToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
      this.カージオイド上の正方形の運動ToolStripMenuItem.Tag = "カージオイド上の正方形の運動";
      this.カージオイド上の正方形の運動ToolStripMenuItem.Text = "カージオイド上の正方形の運動";
      this.カージオイド上の正方形の運動ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 写像ToolStripMenuItem
      // 
      this.写像ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ジューコフスキーの扇形ToolStripMenuItem,
            this.流線ToolStripMenuItem,
            this.wzczの写像ToolStripMenuItem});
      this.写像ToolStripMenuItem.Name = "写像ToolStripMenuItem";
      this.写像ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.写像ToolStripMenuItem.Text = "写像";
      // 
      // ジューコフスキーの扇形ToolStripMenuItem
      // 
      this.ジューコフスキーの扇形ToolStripMenuItem.Name = "ジューコフスキーの扇形ToolStripMenuItem";
      this.ジューコフスキーの扇形ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
      this.ジューコフスキーの扇形ToolStripMenuItem.Tag = "ジューコフスキーの扇形";
      this.ジューコフスキーの扇形ToolStripMenuItem.Text = "ジューコフスキーの扇形";
      this.ジューコフスキーの扇形ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 流線ToolStripMenuItem
      // 
      this.流線ToolStripMenuItem.Name = "流線ToolStripMenuItem";
      this.流線ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
      this.流線ToolStripMenuItem.Tag = "流線";
      this.流線ToolStripMenuItem.Text = "流線";
      this.流線ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // wzczの写像ToolStripMenuItem
      // 
      this.wzczの写像ToolStripMenuItem.Name = "wzczの写像ToolStripMenuItem";
      this.wzczの写像ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
      this.wzczの写像ToolStripMenuItem.Tag = "wzczの写像";
      this.wzczの写像ToolStripMenuItem.Text = "wzczの写像";
      this.wzczの写像ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // フラクタルToolStripMenuItem
      // 
      this.フラクタルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.コッホ曲線による図形ToolStripMenuItem,
            this.雪の結晶ToolStripMenuItem,
            this.盆栽ToolStripMenuItem,
            this.雲ToolStripMenuItem});
      this.フラクタルToolStripMenuItem.Name = "フラクタルToolStripMenuItem";
      this.フラクタルToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.フラクタルToolStripMenuItem.Text = "フラクタル";
      // 
      // コッホ曲線による図形ToolStripMenuItem
      // 
      this.コッホ曲線による図形ToolStripMenuItem.Name = "コッホ曲線による図形ToolStripMenuItem";
      this.コッホ曲線による図形ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.コッホ曲線による図形ToolStripMenuItem.Tag = "コッホ曲線による図形";
      this.コッホ曲線による図形ToolStripMenuItem.Text = "コッホ曲線による図形";
      this.コッホ曲線による図形ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 雪の結晶ToolStripMenuItem
      // 
      this.雪の結晶ToolStripMenuItem.Name = "雪の結晶ToolStripMenuItem";
      this.雪の結晶ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.雪の結晶ToolStripMenuItem.Tag = "雪の結晶";
      this.雪の結晶ToolStripMenuItem.Text = "雪の結晶";
      this.雪の結晶ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 盆栽ToolStripMenuItem
      // 
      this.盆栽ToolStripMenuItem.Name = "盆栽ToolStripMenuItem";
      this.盆栽ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.盆栽ToolStripMenuItem.Tag = "盆栽";
      this.盆栽ToolStripMenuItem.Text = "盆栽";
      this.盆栽ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 雲ToolStripMenuItem
      // 
      this.雲ToolStripMenuItem.Name = "雲ToolStripMenuItem";
      this.雲ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.雲ToolStripMenuItem.Tag = "雲";
      this.雲ToolStripMenuItem.Text = "雲";
      this.雲ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 球の描画ToolStripMenuItem
      // 
      this.球の描画ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.球ToolStripMenuItem,
            this.手毬ToolStripMenuItem,
            this.球の回転ToolStripMenuItem});
      this.球の描画ToolStripMenuItem.Name = "球の描画ToolStripMenuItem";
      this.球の描画ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.球の描画ToolStripMenuItem.Text = "球の描画";
      // 
      // 球ToolStripMenuItem
      // 
      this.球ToolStripMenuItem.Name = "球ToolStripMenuItem";
      this.球ToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
      this.球ToolStripMenuItem.Tag = "球";
      this.球ToolStripMenuItem.Text = "球";
      this.球ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 手毬ToolStripMenuItem
      // 
      this.手毬ToolStripMenuItem.Name = "手毬ToolStripMenuItem";
      this.手毬ToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
      this.手毬ToolStripMenuItem.Tag = "手毬";
      this.手毬ToolStripMenuItem.Text = "手毬";
      this.手毬ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // 球の回転ToolStripMenuItem
      // 
      this.球の回転ToolStripMenuItem.Name = "球の回転ToolStripMenuItem";
      this.球の回転ToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
      this.球の回転ToolStripMenuItem.Tag = "球の回転";
      this.球の回転ToolStripMenuItem.Text = "球の回転";
      this.球の回転ToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // その他ToolStripMenuItem
      // 
      this.その他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.マンデグローブToolStripMenuItem});
      this.その他ToolStripMenuItem.Name = "その他ToolStripMenuItem";
      this.その他ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.その他ToolStripMenuItem.Text = "その他";
      // 
      // マンデグローブToolStripMenuItem
      // 
      this.マンデグローブToolStripMenuItem.Name = "マンデグローブToolStripMenuItem";
      this.マンデグローブToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
      this.マンデグローブToolStripMenuItem.Tag = "マンデグローブ";
      this.マンデグローブToolStripMenuItem.Text = "マンデグローブ";
      this.マンデグローブToolStripMenuItem.Click += new System.EventHandler(this.qcGraphMenuItem_Click);
      // 
      // お気に入りToolStripMenuItem
      // 
      this.お気に入りToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.お気に入りに追加ToolStripMenuItem});
      this.お気に入りToolStripMenuItem.Name = "お気に入りToolStripMenuItem";
      this.お気に入りToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
      this.お気に入りToolStripMenuItem.Text = "お気に入り";
      // 
      // お気に入りに追加ToolStripMenuItem
      // 
      this.お気に入りに追加ToolStripMenuItem.Name = "お気に入りに追加ToolStripMenuItem";
      this.お気に入りに追加ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.お気に入りに追加ToolStripMenuItem.Text = "お気に入りに追加";
      // 
      // ヘルプHToolStripMenuItem
      // 
      this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.内容CToolStripMenuItem,
            this.インデックスIToolStripMenuItem,
            this.検索SToolStripMenuItem,
            this.toolStripSeparator5,
            this.バージョン情報AToolStripMenuItem});
      this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
      this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
      this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
      // 
      // 内容CToolStripMenuItem
      // 
      this.内容CToolStripMenuItem.Name = "内容CToolStripMenuItem";
      this.内容CToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
      this.内容CToolStripMenuItem.Text = "内容(&C)";
      // 
      // インデックスIToolStripMenuItem
      // 
      this.インデックスIToolStripMenuItem.Name = "インデックスIToolStripMenuItem";
      this.インデックスIToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
      this.インデックスIToolStripMenuItem.Text = "インデックス(&I)";
      // 
      // 検索SToolStripMenuItem
      // 
      this.検索SToolStripMenuItem.Name = "検索SToolStripMenuItem";
      this.検索SToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
      this.検索SToolStripMenuItem.Text = "検索(&S)";
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(158, 6);
      // 
      // バージョン情報AToolStripMenuItem
      // 
      this.バージョン情報AToolStripMenuItem.Name = "バージョン情報AToolStripMenuItem";
      this.バージョン情報AToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
      this.バージョン情報AToolStripMenuItem.Text = "バージョン情報(&A)...";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成NToolStripButton,
            this.開くOToolStripButton,
            this.上書き保存SToolStripButton,
            this.印刷PToolStripButton,
            this.toolStripSeparator6,
            this.切り取りUToolStripButton,
            this.コピーCToolStripButton,
            this.貼り付けPToolStripButton,
            this.ヘルプLToolStripButton,
            this.toolStripSeparator7,
            this.toolStripDropDownButton1,
            this.toolStripSeparator9,
            this.imageListButton});
      this.toolStrip1.Location = new System.Drawing.Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(766, 25);
      this.toolStrip1.TabIndex = 7;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // 新規作成NToolStripButton
      // 
      this.新規作成NToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.新規作成NToolStripButton.Enabled = false;
      this.新規作成NToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("新規作成NToolStripButton.Image")));
      this.新規作成NToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.新規作成NToolStripButton.Name = "新規作成NToolStripButton";
      this.新規作成NToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.新規作成NToolStripButton.Text = "新規作成(&N)";
      // 
      // 開くOToolStripButton
      // 
      this.開くOToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.開くOToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("開くOToolStripButton.Image")));
      this.開くOToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.開くOToolStripButton.Name = "開くOToolStripButton";
      this.開くOToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.開くOToolStripButton.Text = "開く(&O)";
      this.開くOToolStripButton.Click += new System.EventHandler(this.開くOToolStripButton_Click);
      // 
      // 上書き保存SToolStripButton
      // 
      this.上書き保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.上書き保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("上書き保存SToolStripButton.Image")));
      this.上書き保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.上書き保存SToolStripButton.Name = "上書き保存SToolStripButton";
      this.上書き保存SToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.上書き保存SToolStripButton.Text = "上書き保存(&S)";
      // 
      // 印刷PToolStripButton
      // 
      this.印刷PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.印刷PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("印刷PToolStripButton.Image")));
      this.印刷PToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.印刷PToolStripButton.Name = "印刷PToolStripButton";
      this.印刷PToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.印刷PToolStripButton.Text = "印刷(&P)";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
      // 
      // 切り取りUToolStripButton
      // 
      this.切り取りUToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.切り取りUToolStripButton.Enabled = false;
      this.切り取りUToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("切り取りUToolStripButton.Image")));
      this.切り取りUToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.切り取りUToolStripButton.Name = "切り取りUToolStripButton";
      this.切り取りUToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.切り取りUToolStripButton.Text = "切り取り(&U)";
      // 
      // コピーCToolStripButton
      // 
      this.コピーCToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.コピーCToolStripButton.Enabled = false;
      this.コピーCToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("コピーCToolStripButton.Image")));
      this.コピーCToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.コピーCToolStripButton.Name = "コピーCToolStripButton";
      this.コピーCToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.コピーCToolStripButton.Text = "コピー(&C)";
      // 
      // 貼り付けPToolStripButton
      // 
      this.貼り付けPToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.貼り付けPToolStripButton.Enabled = false;
      this.貼り付けPToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("貼り付けPToolStripButton.Image")));
      this.貼り付けPToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.貼り付けPToolStripButton.Name = "貼り付けPToolStripButton";
      this.貼り付けPToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.貼り付けPToolStripButton.Text = "貼り付け(&P)";
      // 
      // ヘルプLToolStripButton
      // 
      this.ヘルプLToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ヘルプLToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ヘルプLToolStripButton.Image")));
      this.ヘルプLToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
      this.ヘルプLToolStripButton.Name = "ヘルプLToolStripButton";
      this.ヘルプLToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.ヘルプLToolStripButton.Text = "ヘルプ(&L)";
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.メニューバーMToolStripMenuItem,
            this.ステータスバーSToolStripMenuItem1});
      this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
      this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
      // 
      // メニューバーMToolStripMenuItem
      // 
      this.メニューバーMToolStripMenuItem.Checked = true;
      this.メニューバーMToolStripMenuItem.CheckOnClick = true;
      this.メニューバーMToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.メニューバーMToolStripMenuItem.Name = "メニューバーMToolStripMenuItem";
      this.メニューバーMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.メニューバーMToolStripMenuItem.Text = "メニューバー(&M)";
      this.メニューバーMToolStripMenuItem.Click += new System.EventHandler(this.メニューバーMToolStripMenuItem_Click);
      // 
      // ステータスバーSToolStripMenuItem1
      // 
      this.ステータスバーSToolStripMenuItem1.CheckOnClick = true;
      this.ステータスバーSToolStripMenuItem1.Name = "ステータスバーSToolStripMenuItem1";
      this.ステータスバーSToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
      this.ステータスバーSToolStripMenuItem1.Text = "ステータスバー(&S)";
      this.ステータスバーSToolStripMenuItem1.Click += new System.EventHandler(this.ステータスバーSToolStripMenuItem1_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackColor = System.Drawing.Color.White;
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Location = new System.Drawing.Point(0, 49);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(766, 320);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 8;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Tag = "";
      this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
      this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
      this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
      this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
      this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
      // 
      // imageListButton
      // 
      this.imageListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.imageListButton.Image = ((System.Drawing.Image)(resources.GetObject("imageListButton.Image")));
      this.imageListButton.ImageTransparentColor = System.Drawing.Color.Transparent;
      this.imageListButton.Name = "imageListButton";
      this.imageListButton.Size = new System.Drawing.Size(23, 22);
      this.imageListButton.Text = "toolStripButton1";
      this.imageListButton.Visible = false;
      // 
      // PicturePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.menuStrip1);
      this.Controls.Add(this.statusStrip1);
      this.Name = "PicturePanel";
      this.Size = new System.Drawing.Size(766, 391);
      this.Tag = this.pictureBox1;
      this.Load += new System.EventHandler(this.PicturePanel_Load);
      this.Enter += new System.EventHandler(this.PicturePanel_Enter);
      this.Leave += new System.EventHandler(this.PicturePanel_Leave);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

	}

	#endregion

  	#region Main Comment OUT
	/*
	/// <summary>
	/// アプリケーションのメイン エントリ ポイントです。
	/// </summary>
	[STAThread]
	static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new PicturePanel());
	}
	*/
	#endregion

  }

}
