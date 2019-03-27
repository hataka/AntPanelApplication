using AntPanelApplication.Helpers;
using AntPanelApplication.Managers;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AntPanelApplication
{
  partial class MainForm
  {
    /// <summary>
    /// 必要なデザイナー変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 使用中のリソースをすべてクリーンアップします。
    /// </summary>
    /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows フォーム デザイナーで生成されたコード

    /// <summary>
    /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
    /// コード エディターで変更しないでください。
    /// </summary>
  

    private void InitializeComponent()
    {
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripPanel = new System.Windows.Forms.ToolStripPanel();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.bottomsplitContainer = new System.Windows.Forms.SplitContainer();
      this.rightsplitContainer = new System.Windows.Forms.SplitContainer();
      this.tabControl2 = new System.Windows.Forms.TabControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.tabControl3 = new System.Windows.Forms.TabControl();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.tabPage6 = new System.Windows.Forms.TabPage();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bottomsplitContainer)).BeginInit();
      this.bottomsplitContainer.Panel1.SuspendLayout();
      this.bottomsplitContainer.Panel2.SuspendLayout();
      this.bottomsplitContainer.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.rightsplitContainer)).BeginInit();
      this.rightsplitContainer.Panel1.SuspendLayout();
      this.rightsplitContainer.Panel2.SuspendLayout();
      this.rightsplitContainer.SuspendLayout();
      this.tabControl2.SuspendLayout();
      this.tabControl3.SuspendLayout();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.statusStrip1.Location = new System.Drawing.Point(0, 555);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
      this.statusStrip1.Size = new System.Drawing.Size(1080, 22);
      this.statusStrip1.TabIndex = 2;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripPanel
      // 
      this.toolStripPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.toolStripPanel.Location = new System.Drawing.Point(0, 0);
      this.toolStripPanel.Name = "toolStripPanel";
      this.toolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
      this.toolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.toolStripPanel.Size = new System.Drawing.Size(1080, 0);
      // 
      // menuStrip1
      // 
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(200, 24);
      this.menuStrip1.TabIndex = 0;
      // 
      // toolStrip1
      // 
      this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(100, 25);
      this.toolStrip1.TabIndex = 0;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
      this.splitContainer1.Size = new System.Drawing.Size(999, 526);
      this.splitContainer1.SplitterDistance = 222;
      this.splitContainer1.TabIndex = 3;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(773, 526);
      this.tabControl1.TabIndex = 0;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
      this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
      // 
      // tabPage1
      // 
      this.tabPage1.Location = new System.Drawing.Point(4, 34);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(765, 488);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "tabPage1";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 25);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(708, 518);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "tabPage2";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // splitContainer2
      // 
      this.bottomsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.bottomsplitContainer.Location = new System.Drawing.Point(0, 0);
      this.bottomsplitContainer.Name = "splitContainer2";
      this.bottomsplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.bottomsplitContainer.Panel1.Controls.Add(this.rightsplitContainer);
      // 
      // splitContainer2.Panel2
      // 
      this.bottomsplitContainer.Panel2.Controls.Add(this.tabControl2);
      this.bottomsplitContainer.Size = new System.Drawing.Size(1080, 555);
      this.bottomsplitContainer.SplitterDistance = 526;
      this.bottomsplitContainer.TabIndex = 5;
      // 
      // splitContainer3
      // 
      this.rightsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rightsplitContainer.Location = new System.Drawing.Point(0, 0);
      this.rightsplitContainer.Name = "splitContainer3";
      // 
      // splitContainer3.Panel1
      // 
      this.rightsplitContainer.Panel1.Controls.Add(this.splitContainer1);
      // 
      // splitContainer3.Panel2
      // 
      this.rightsplitContainer.Panel2.Controls.Add(this.tabControl3);
      this.rightsplitContainer.Size = new System.Drawing.Size(1080, 526);
      this.rightsplitContainer.SplitterDistance = 999;
      this.rightsplitContainer.TabIndex = 6;
      // 
      // tabControl2
      // 
      this.tabControl2.Alignment = System.Windows.Forms.TabAlignment.Bottom;
      this.tabControl2.Controls.Add(this.tabPage3);
      this.tabControl2.Controls.Add(this.tabPage4);
      this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl2.Location = new System.Drawing.Point(0, 0);
      this.tabControl2.Name = "tabControl2";
      this.tabControl2.SelectedIndex = 0;
      this.tabControl2.Size = new System.Drawing.Size(1080, 25);
      this.tabControl2.TabIndex = 0;
      // 
      // tabPage3
      // 
      this.tabPage3.Location = new System.Drawing.Point(4, 4);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(1072, 0);
      this.tabPage3.TabIndex = 0;
      this.tabPage3.Text = "tabPage3";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Location = new System.Drawing.Point(4, 4);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(192, 62);
      this.tabPage4.TabIndex = 1;
      this.tabPage4.Text = "tabPage4";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // tabControl3
      // 
      this.tabControl3.Alignment = System.Windows.Forms.TabAlignment.Right;
      this.tabControl3.Controls.Add(this.tabPage5);
      this.tabControl3.Controls.Add(this.tabPage6);
      this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl3.Location = new System.Drawing.Point(0, 0);
      this.tabControl3.Multiline = true;
      this.tabControl3.Name = "tabControl3";
      this.tabControl3.SelectedIndex = 0;
      this.tabControl3.Size = new System.Drawing.Size(77, 526);
      this.tabControl3.TabIndex = 0;
      // 
      // tabPage5
      // 
      this.tabPage5.Location = new System.Drawing.Point(4, 4);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage5.Size = new System.Drawing.Size(37, 518);
      this.tabPage5.TabIndex = 0;
      this.tabPage5.Text = "tabPage5";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // tabPage6
      // 
      this.tabPage6.Location = new System.Drawing.Point(4, 4);
      this.tabPage6.Name = "tabPage6";
      this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage6.Size = new System.Drawing.Size(128, 92);
      this.tabPage6.TabIndex = 1;
      this.tabPage6.Text = "tabPage6";
      this.tabPage6.UseVisualStyleBackColor = true;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1080, 577);
      this.Controls.Add(this.bottomsplitContainer);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.toolStripPanel);
      this.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "MainForm";
      this.Text = "MainForm";
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.bottomsplitContainer.Panel1.ResumeLayout(false);
      this.bottomsplitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.bottomsplitContainer)).EndInit();
      this.bottomsplitContainer.ResumeLayout(false);
      this.rightsplitContainer.Panel1.ResumeLayout(false);
      this.rightsplitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.rightsplitContainer)).EndInit();
      this.rightsplitContainer.ResumeLayout(false);
      this.tabControl2.ResumeLayout(false);
      this.tabControl3.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    #endregion

    private System.Windows.Forms.ToolStripPanel toolStripPanel;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    public System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private SplitContainer bottomsplitContainer;
    private SplitContainer rightsplitContainer;
    private TabControl tabControl3;
    private TabPage tabPage5;
    private TabPage tabPage6;
    private TabControl tabControl2;
    private TabPage tabPage3;
    private TabPage tabPage4;
    /* Context Menus */
  }
}

