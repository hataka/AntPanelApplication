#define XMLTreeMenu

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMLTreeMenu.Controls
{
  public partial class RichTextEditor : UserControl
  {
    public RichTextEditor()
    {
      InitializeComponent();
      IntializeRichTextPanel();
    }
    public RichTextEditor(string[] args)
    {
      InitializeComponent();
      IntializeRichTextPanel();
    }
    public void IntializeRichTextPanel()
	  {
      System.ComponentModel.ComponentResourceManager resources
        = new System.ComponentModel.ComponentResourceManager(typeof(RichTextEditor));
      Bitmap bmp = ((System.Drawing.Bitmap)(resources.GetObject("toolStripButton2.Image")));
      this.imageList1.Images.AddStrip(bmp);
      this.toolStripDropDownButton2.Image = imageList1.Images[61 + 5];
      this.toolStripDropDownButton3.Image = imageList1.Images[15 + 5];
      this.toolStripDropDownButton4.Image = imageList1.Images[117 + 5];
    }
  }

}
