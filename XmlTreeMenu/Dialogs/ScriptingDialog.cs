using PluginCore;
using ScintillaNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XMLTreeMenu.Controls;

namespace AntPlugin.XMLTreeMenu.Dialogs
{
  public partial class ScriptingDialog : Form
  {
    
    
    
    // 仮
    //private RichTextEditor editor;

    public ScriptingDialog()
    {
      InitializeComponent();
      this.menuStrip1.Visible = false;
      this.toolStrip1.Visible = false;
      this.statusStrip1.Visible = false;
      
      
  
      
      
      
      
      // 仮
      //this.editor = new RichTextEditor();
      //this.editor.Dock = DockStyle.Fill;
      //this.editor.スクリプトを実行XToolStripMenuItem.Click += スクリプトを実行XToolStripMenuItem_Click;
      //this.Controls.Add(this.editor);
    }

    private void スクリプトを実行XToolStripMenuItem_Click(object sender, EventArgs e)
    {
      String currentfile = PluginBase.MainForm.CurrentDocument.FileName;
      MessageBox.Show(currentfile);
      //throw new NotImplementedException();
    }


    /// <summary>
    /// Adds a new scintilla control to the document
    /// </summary>
    public void AddEditorControls(String file, String text, Int32 codepage)
    {
      
      /*
      //this.editor = ScintillaManager.CreateControl(file, text, codepage);
      this.editor = new ScintillaControl();
      this.editor.Dock = DockStyle.Fill;
      this.Controls.Add(this.editor);
      
      this.editor2 = ScintillaManager.CreateControl(file, text, codepage);
      this.editor2.Dock = DockStyle.Fill;
      this.splitContainer = new SplitContainer();
      this.splitContainer.Name = "fdSplitView";
      this.splitContainer.SplitterWidth = ScaleHelper.Scale(this.splitContainer.SplitterWidth);
      this.splitContainer.Orientation = Orientation.Horizontal;
      this.splitContainer.BackColor = SystemColors.Control;
      this.splitContainer.Panel1.Controls.Add(this.editor);
      this.splitContainer.Panel2.Controls.Add(this.editor2);
      this.splitContainer.Dock = DockStyle.Fill;
      this.splitContainer.Panel2Collapsed = true;
      Int32 oldDoc = this.editor.DocPointer;
      this.editor2.DocPointer = oldDoc;
      this.editor.SavePointLeft += delegate
      {
        Globals.MainForm.OnDocumentModify(this);
      };
      this.editor.SavePointReached += delegate
      {
        this.editor.MarkerDeleteAll(2);
        this.IsModified = false;
      };
      this.editor.FocusChanged += new FocusHandler(this.EditorFocusChanged);
      this.editor2.FocusChanged += new FocusHandler(this.EditorFocusChanged);
      this.editor.UpdateSync += new UpdateSyncHandler(this.EditorUpdateSync);
      this.editor2.UpdateSync += new UpdateSyncHandler(this.EditorUpdateSync);
      this.Controls.Add(this.splitContainer);
      */
    }


  }

}
