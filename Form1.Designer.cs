namespace AntPanelApplication
{
  partial class Form1
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.documentTabControl = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
      this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
      this.tabPage6 = new System.Windows.Forms.TabPage();
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.bottomSplitContainer = new System.Windows.Forms.SplitContainer();
      this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
      this.rightTabControl = new System.Windows.Forms.TabControl();
      this.tabPage9 = new System.Windows.Forms.TabPage();
      this.tabPage10 = new System.Windows.Forms.TabPage();
      this.bottomTabControl = new System.Windows.Forms.TabControl();
      this.tabPage7 = new System.Windows.Forms.TabPage();
      this.tabPage8 = new System.Windows.Forms.TabPage();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.documentTabControl.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.tabPage5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bottomSplitContainer)).BeginInit();
      this.bottomSplitContainer.Panel1.SuspendLayout();
      this.bottomSplitContainer.Panel2.SuspendLayout();
      this.bottomSplitContainer.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
      this.rightSplitContainer.Panel1.SuspendLayout();
      this.rightSplitContainer.Panel2.SuspendLayout();
      this.rightSplitContainer.SuspendLayout();
      this.rightTabControl.SuspendLayout();
      this.bottomTabControl.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.documentTabControl);
      this.splitContainer1.Size = new System.Drawing.Size(951, 572);
      this.splitContainer1.SplitterDistance = 233;
      this.splitContainer1.SplitterWidth = 7;
      this.splitContainer1.TabIndex = 3;
      // 
      // documentTabControl
      // 
      this.documentTabControl.Controls.Add(this.tabPage1);
      this.documentTabControl.Controls.Add(this.tabPage2);
      this.documentTabControl.Controls.Add(this.tabPage3);
      this.documentTabControl.Controls.Add(this.tabPage4);
      this.documentTabControl.Controls.Add(this.tabPage5);
      this.documentTabControl.Controls.Add(this.tabPage6);
      this.documentTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.documentTabControl.Location = new System.Drawing.Point(0, 0);
      this.documentTabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.documentTabControl.Name = "documentTabControl";
      this.documentTabControl.SelectedIndex = 0;
      this.documentTabControl.Size = new System.Drawing.Size(711, 572);
      this.documentTabControl.TabIndex = 0;
      this.documentTabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
      this.documentTabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.documentTabControl_MouseClick);
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.richTextBox1);
      this.tabPage1.Location = new System.Drawing.Point(4, 30);
      this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage1.Size = new System.Drawing.Size(703, 538);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "tabPage1";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // richTextBox1
      // 
      this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.richTextBox1.Location = new System.Drawing.Point(3, 4);
      this.richTextBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(697, 530);
      this.richTextBox1.TabIndex = 8;
      this.richTextBox1.Text = "こんにちわ\nリッチテキストです\n";
      this.richTextBox1.UseWaitCursor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.pictureBox1);
      this.tabPage2.Location = new System.Drawing.Point(4, 25);
      this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tabPage2.Size = new System.Drawing.Size(703, 543);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "tabPage2";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackColor = System.Drawing.Color.White;
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Location = new System.Drawing.Point(3, 4);
      this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(697, 535);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 7;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.UseWaitCursor = true;
      // 
      // tabPage3
      // 
      this.tabPage3.Location = new System.Drawing.Point(4, 25);
      this.tabPage3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new System.Drawing.Size(703, 543);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "tabPage3";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Location = new System.Drawing.Point(4, 25);
      this.tabPage4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Size = new System.Drawing.Size(703, 543);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "tabPage4";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // tabPage5
      // 
      this.tabPage5.Controls.Add(this.axWindowsMediaPlayer1);
      this.tabPage5.Controls.Add(this.propertyGrid1);
      this.tabPage5.Location = new System.Drawing.Point(4, 25);
      this.tabPage5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Size = new System.Drawing.Size(703, 543);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "tabPage5";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // axWindowsMediaPlayer1
      // 
      this.axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.axWindowsMediaPlayer1.Enabled = true;
      this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
      this.axWindowsMediaPlayer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
      this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
      this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(703, 543);
      this.axWindowsMediaPlayer1.TabIndex = 1;
      // 
      // propertyGrid1
      // 
      this.propertyGrid1.Location = new System.Drawing.Point(499, 233);
      this.propertyGrid1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new System.Drawing.Size(7, 7);
      this.propertyGrid1.TabIndex = 0;
      // 
      // tabPage6
      // 
      this.tabPage6.Location = new System.Drawing.Point(4, 25);
      this.tabPage6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage6.Name = "tabPage6";
      this.tabPage6.Size = new System.Drawing.Size(703, 543);
      this.tabPage6.TabIndex = 5;
      this.tabPage6.Text = "tabPage6";
      this.tabPage6.UseVisualStyleBackColor = true;
      // 
      // webBrowser1
      // 
      this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.webBrowser1.Location = new System.Drawing.Point(0, 0);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.Size = new System.Drawing.Size(973, 474);
      this.webBrowser1.TabIndex = 0;
      // 
      // imageList1
      // 
      this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // bottomSplitContainer
      // 
      this.bottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.bottomSplitContainer.Location = new System.Drawing.Point(0, 0);
      this.bottomSplitContainer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.bottomSplitContainer.Name = "bottomSplitContainer";
      this.bottomSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // bottomSplitContainer.Panel1
      // 
      this.bottomSplitContainer.Panel1.Controls.Add(this.rightSplitContainer);
      // 
      // bottomSplitContainer.Panel2
      // 
      this.bottomSplitContainer.Panel2.Controls.Add(this.bottomTabControl);
      this.bottomSplitContainer.Size = new System.Drawing.Size(986, 603);
      this.bottomSplitContainer.SplitterDistance = 572;
      this.bottomSplitContainer.SplitterWidth = 3;
      this.bottomSplitContainer.TabIndex = 4;
      // 
      // rightSplitContainer
      // 
      this.rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rightSplitContainer.Location = new System.Drawing.Point(0, 0);
      this.rightSplitContainer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.rightSplitContainer.Name = "rightSplitContainer";
      // 
      // rightSplitContainer.Panel1
      // 
      this.rightSplitContainer.Panel1.Controls.Add(this.splitContainer1);
      // 
      // rightSplitContainer.Panel2
      // 
      this.rightSplitContainer.Panel2.Controls.Add(this.rightTabControl);
      this.rightSplitContainer.Size = new System.Drawing.Size(986, 572);
      this.rightSplitContainer.SplitterDistance = 951;
      this.rightSplitContainer.SplitterWidth = 3;
      this.rightSplitContainer.TabIndex = 0;
      // 
      // rightTabControl
      // 
      this.rightTabControl.Alignment = System.Windows.Forms.TabAlignment.Right;
      this.rightTabControl.Controls.Add(this.tabPage9);
      this.rightTabControl.Controls.Add(this.tabPage10);
      this.rightTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rightTabControl.Location = new System.Drawing.Point(0, 0);
      this.rightTabControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.rightTabControl.Multiline = true;
      this.rightTabControl.Name = "rightTabControl";
      this.rightTabControl.SelectedIndex = 0;
      this.rightTabControl.Size = new System.Drawing.Size(32, 572);
      this.rightTabControl.TabIndex = 0;
      this.rightTabControl.DoubleClick += new System.EventHandler(this.rightTabControl_DoubleClick);
      this.rightTabControl.Enter += new System.EventHandler(this.rightTabControl_Enter);
      this.rightTabControl.Leave += new System.EventHandler(this.rightTabControl_Leave);
      this.rightTabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rightTabControl_MouseClick);
      // 
      // tabPage9
      // 
      this.tabPage9.Location = new System.Drawing.Point(4, 4);
      this.tabPage9.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage9.Name = "tabPage9";
      this.tabPage9.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage9.Size = new System.Drawing.Size(0, 564);
      this.tabPage9.TabIndex = 0;
      this.tabPage9.Text = "tabPage9";
      this.tabPage9.UseVisualStyleBackColor = true;
      // 
      // tabPage10
      // 
      this.tabPage10.Location = new System.Drawing.Point(4, 4);
      this.tabPage10.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage10.Name = "tabPage10";
      this.tabPage10.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage10.Size = new System.Drawing.Size(0, 564);
      this.tabPage10.TabIndex = 1;
      this.tabPage10.Text = "tabPage10";
      this.tabPage10.UseVisualStyleBackColor = true;
      // 
      // bottomTabControl
      // 
      this.bottomTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
      this.bottomTabControl.Controls.Add(this.tabPage7);
      this.bottomTabControl.Controls.Add(this.tabPage8);
      this.bottomTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.bottomTabControl.Location = new System.Drawing.Point(0, 0);
      this.bottomTabControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.bottomTabControl.Name = "bottomTabControl";
      this.bottomTabControl.SelectedIndex = 0;
      this.bottomTabControl.Size = new System.Drawing.Size(986, 28);
      this.bottomTabControl.TabIndex = 0;
      this.bottomTabControl.DoubleClick += new System.EventHandler(this.bottomTabControl_DoubleClick);
      this.bottomTabControl.Enter += new System.EventHandler(this.bottomTabControl_Enter);
      this.bottomTabControl.Leave += new System.EventHandler(this.bottomTabControl_Leave);
      this.bottomTabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.bottomTabControl_MouseClick);
      // 
      // tabPage7
      // 
      this.tabPage7.Location = new System.Drawing.Point(4, 4);
      this.tabPage7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage7.Name = "tabPage7";
      this.tabPage7.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage7.Size = new System.Drawing.Size(978, 0);
      this.tabPage7.TabIndex = 0;
      this.tabPage7.Text = "tabPage7";
      this.tabPage7.UseVisualStyleBackColor = true;
      // 
      // tabPage8
      // 
      this.tabPage8.Location = new System.Drawing.Point(4, 4);
      this.tabPage8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage8.Name = "tabPage8";
      this.tabPage8.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tabPage8.Size = new System.Drawing.Size(978, 0);
      this.tabPage8.TabIndex = 1;
      this.tabPage8.Text = "tabPage8";
      this.tabPage8.UseVisualStyleBackColor = true;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(986, 603);
      this.Controls.Add(this.bottomSplitContainer);
      this.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "Form1";
      this.Text = "Form1";
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.documentTabControl.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.tabPage5.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
      this.bottomSplitContainer.Panel1.ResumeLayout(false);
      this.bottomSplitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.bottomSplitContainer)).EndInit();
      this.bottomSplitContainer.ResumeLayout(false);
      this.rightSplitContainer.Panel1.ResumeLayout(false);
      this.rightSplitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
      this.rightSplitContainer.ResumeLayout(false);
      this.rightTabControl.ResumeLayout(false);
      this.bottomTabControl.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.SplitContainer splitContainer1;
    public System.Windows.Forms.TabControl documentTabControl;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.TabPage tabPage6;
    public System.Windows.Forms.RichTextBox richTextBox1;
    public System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.WebBrowser webBrowser1;
    public AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    public System.Windows.Forms.PropertyGrid propertyGrid1;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.SplitContainer bottomSplitContainer;
    private System.Windows.Forms.SplitContainer rightSplitContainer;
    private System.Windows.Forms.TabControl rightTabControl;
    private System.Windows.Forms.TabPage tabPage9;
    private System.Windows.Forms.TabPage tabPage10;
    private System.Windows.Forms.TabControl bottomTabControl;
    private System.Windows.Forms.TabPage tabPage7;
    private System.Windows.Forms.TabPage tabPage8;
  }
}

