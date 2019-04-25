using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AntPanelApplication.Controls
{
	public class RubberBandDialog : Form
	{
		public PicturePanel parent;
		public ColorDialog colorDialog1;
		public FontDialog fontDialog1;
		public Color use_color = default(Color);
		public PageFont page_font = default(PageFont);
		private IContainer components = null;
		private Button fontButton1;
		public TextBox stringTextBox;
		private Button drawButton;
		private Button colorButton;
		public RadioButton fillingRadioButton;
		public RadioButton drawingRadioButton;
		public NumericUpDown lineNumericUpDown;
		public NumericUpDown epyNumericUpDown;
		public NumericUpDown epxNumericUpDown;
		public NumericUpDown spyNumericUpDown;
		public NumericUpDown spxNumericUpDown;
		public ComboBox shapeComboBox;
		private Label currentFontLabel1;
		private Label fontLabel1;
		private Label stringLabel;
		private Label currentColorLabel;
		private Label colorLabel;
		private Label lineLabel;
		private Label drawLabel;
		private Label epLabel2;
		private Label epLabel1;
		private Label spLabel2;
		private Label spLabel1;
		private Label shapeLabel;
		private Button cancelButton;
		private Button setButton;
		public NumericUpDown angleUpDown1;
		private Label anglelabel1;
		public CheckBox xrect;
		public CheckBox yrect;
		public CheckBox aspect;
		private Label label1;

		public RubberBandDialog()
		{
			this.InitializeComponent();
			this.colorDialog1 = new ColorDialog();
			this.fontDialog1 = new FontDialog();
		}

		private void drawButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void setButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void shapeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = this.shapeComboBox.SelectedItem.ToString();
			if (text != null)
			{
				if (!(text == "直線"))
				{
					if (!(text == "四角形") && !(text == "楕円"))
					{
						if (text == "文字列")
						{
							this.epxNumericUpDown.Enabled = false;
							this.epyNumericUpDown.Enabled = false;
							this.drawingRadioButton.Enabled = false;
							this.fillingRadioButton.Checked = true;
							this.fillingRadioButton.Enabled = true;
							this.lineNumericUpDown.Enabled = false;
							this.stringTextBox.Enabled = true;
							this.fontButton1.Enabled = true;
						}
					}
					else
					{
						this.epxNumericUpDown.Enabled = true;
						this.epyNumericUpDown.Enabled = true;
						this.drawingRadioButton.Checked = true;
						this.drawingRadioButton.Enabled = true;
						this.fillingRadioButton.Enabled = true;
						this.lineNumericUpDown.Enabled = true;
						this.stringTextBox.Enabled = false;
						this.fontButton1.Enabled = false;
					}
				}
				else
				{
					this.epxNumericUpDown.Enabled = true;
					this.epyNumericUpDown.Enabled = true;
					this.drawingRadioButton.Checked = true;
					this.fillingRadioButton.Enabled = false;
					this.lineNumericUpDown.Enabled = true;
					this.stringTextBox.Enabled = false;
					this.fontButton1.Enabled = false;
				}
			}
		}

		private void colorButton_Click(object sender, EventArgs e)
		{
			if (this.colorDialog1.ShowDialog() == DialogResult.OK)
			{
				this.use_color = this.colorDialog1.Color;
				this.currentColorLabel.Text = this.use_color.Name;
				this.currentColorLabel.ForeColor = this.use_color;
			}
		}

		private void fontButton1_Click(object sender, EventArgs e)
		{
			this.fontDialog1.Font = this.page_font.font;
			if (this.fontDialog1.ShowDialog() == DialogResult.OK)
			{
				this.page_font.font = this.fontDialog1.Font;
				this.page_font.SetLabel();
			}
		}

		private void RubberBandDialog_Load(object sender, EventArgs e)
		{
			//FIXME
      this.page_font.Initialize(this.Font, this.currentFontLabel1);
			this.page_font.method = new GetLabelDelegate(FontLabels.GetLongLabel);
			this.page_font.SetLabel();
			this.shapeComboBox.SelectedIndex = 0;
			this.use_color = Color.Black;
			this.currentColorLabel.Text = this.use_color.Name;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			this.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.fontButton1 = new System.Windows.Forms.Button();
			this.stringTextBox = new System.Windows.Forms.TextBox();
			this.drawButton = new System.Windows.Forms.Button();
			this.colorButton = new System.Windows.Forms.Button();
			this.fillingRadioButton = new System.Windows.Forms.RadioButton();
			this.drawingRadioButton = new System.Windows.Forms.RadioButton();
			this.lineNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.epyNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.epxNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.spyNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.spxNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.shapeComboBox = new System.Windows.Forms.ComboBox();
			this.currentFontLabel1 = new System.Windows.Forms.Label();
			this.fontLabel1 = new System.Windows.Forms.Label();
			this.stringLabel = new System.Windows.Forms.Label();
			this.currentColorLabel = new System.Windows.Forms.Label();
			this.colorLabel = new System.Windows.Forms.Label();
			this.lineLabel = new System.Windows.Forms.Label();
			this.drawLabel = new System.Windows.Forms.Label();
			this.epLabel2 = new System.Windows.Forms.Label();
			this.epLabel1 = new System.Windows.Forms.Label();
			this.spLabel2 = new System.Windows.Forms.Label();
			this.spLabel1 = new System.Windows.Forms.Label();
			this.shapeLabel = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.setButton = new System.Windows.Forms.Button();
			this.angleUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.anglelabel1 = new System.Windows.Forms.Label();
			this.xrect = new System.Windows.Forms.CheckBox();
			this.yrect = new System.Windows.Forms.CheckBox();
			this.aspect = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.lineNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.epyNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.epxNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spyNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spxNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.angleUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// fontButton1
			// 
			this.fontButton1.Location = new System.Drawing.Point(242, 317);
			this.fontButton1.Name = "fontButton1";
			this.fontButton1.Size = new System.Drawing.Size(85, 23);
			this.fontButton1.TabIndex = 48;
			this.fontButton1.Text = "フォント";
			this.fontButton1.UseVisualStyleBackColor = true;
			this.fontButton1.Click += new System.EventHandler(this.fontButton1_Click);
			// 
			// stringTextBox
			// 
			this.stringTextBox.Location = new System.Drawing.Point(86, 251);
			this.stringTextBox.Multiline = true;
			this.stringTextBox.Name = "stringTextBox";
			this.stringTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.stringTextBox.Size = new System.Drawing.Size(200, 60);
			this.stringTextBox.TabIndex = 47;
			// 
			// drawButton
			// 
			this.drawButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.drawButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.drawButton.Location = new System.Drawing.Point(22, 364);
			this.drawButton.Name = "drawButton";
			this.drawButton.Size = new System.Drawing.Size(88, 32);
			this.drawButton.TabIndex = 46;
			this.drawButton.Text = "描　画";
			this.drawButton.UseVisualStyleBackColor = true;
			this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
			// 
			// colorButton
			// 
			this.colorButton.Location = new System.Drawing.Point(242, 214);
			this.colorButton.Name = "colorButton";
			this.colorButton.Size = new System.Drawing.Size(85, 23);
			this.colorButton.TabIndex = 45;
			this.colorButton.Text = "色の変更";
			this.colorButton.UseVisualStyleBackColor = true;
			this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
			// 
			// fillingRadioButton
			// 
			this.fillingRadioButton.AutoSize = true;
			this.fillingRadioButton.Location = new System.Drawing.Point(171, 124);
			this.fillingRadioButton.Name = "fillingRadioButton";
			this.fillingRadioButton.Size = new System.Drawing.Size(71, 16);
			this.fillingRadioButton.TabIndex = 44;
			this.fillingRadioButton.Text = "塗りつぶし";
			this.fillingRadioButton.UseVisualStyleBackColor = true;
			// 
			// drawingRadioButton
			// 
			this.drawingRadioButton.AutoSize = true;
			this.drawingRadioButton.Checked = true;
			this.drawingRadioButton.Location = new System.Drawing.Point(101, 124);
			this.drawingRadioButton.Name = "drawingRadioButton";
			this.drawingRadioButton.Size = new System.Drawing.Size(59, 16);
			this.drawingRadioButton.TabIndex = 43;
			this.drawingRadioButton.TabStop = true;
			this.drawingRadioButton.Text = "境界線";
			this.drawingRadioButton.UseVisualStyleBackColor = true;
			// 
			// lineNumericUpDown
			// 
			this.lineNumericUpDown.Location = new System.Drawing.Point(91, 149);
			this.lineNumericUpDown.Name = "lineNumericUpDown";
			this.lineNumericUpDown.Size = new System.Drawing.Size(40, 19);
			this.lineNumericUpDown.TabIndex = 42;
			// 
			// epyNumericUpDown
			// 
			this.epyNumericUpDown.Location = new System.Drawing.Point(156, 89);
			this.epyNumericUpDown.Name = "epyNumericUpDown";
			this.epyNumericUpDown.Size = new System.Drawing.Size(40, 19);
			this.epyNumericUpDown.TabIndex = 41;
			// 
			// epxNumericUpDown
			// 
			this.epxNumericUpDown.Location = new System.Drawing.Point(86, 89);
			this.epxNumericUpDown.Name = "epxNumericUpDown";
			this.epxNumericUpDown.Size = new System.Drawing.Size(40, 19);
			this.epxNumericUpDown.TabIndex = 40;
			// 
			// spyNumericUpDown
			// 
			this.spyNumericUpDown.Location = new System.Drawing.Point(156, 61);
			this.spyNumericUpDown.Name = "spyNumericUpDown";
			this.spyNumericUpDown.Size = new System.Drawing.Size(40, 19);
			this.spyNumericUpDown.TabIndex = 39;
			// 
			// spxNumericUpDown
			// 
			this.spxNumericUpDown.Location = new System.Drawing.Point(86, 61);
			this.spxNumericUpDown.Name = "spxNumericUpDown";
			this.spxNumericUpDown.Size = new System.Drawing.Size(40, 19);
			this.spxNumericUpDown.TabIndex = 38;
			// 
			// shapeComboBox
			// 
			this.shapeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.shapeComboBox.FormattingEnabled = true;
			this.shapeComboBox.Items.AddRange(new object[] {
            "編集",
            "位置セット",
            "直線",
            "四角形",
            "楕円",
            "文字列",
            "全削除"});
			this.shapeComboBox.Location = new System.Drawing.Point(71, 25);
			this.shapeComboBox.Name = "shapeComboBox";
			this.shapeComboBox.Size = new System.Drawing.Size(121, 20);
			this.shapeComboBox.TabIndex = 37;
			this.shapeComboBox.SelectedIndexChanged += new System.EventHandler(this.shapeComboBox_SelectedIndexChanged);
			// 
			// currentFontLabel1
			// 
			this.currentFontLabel1.AutoSize = true;
			this.currentFontLabel1.Location = new System.Drawing.Point(81, 322);
			this.currentFontLabel1.Name = "currentFontLabel1";
			this.currentFontLabel1.Size = new System.Drawing.Size(66, 12);
			this.currentFontLabel1.TabIndex = 36;
			this.currentFontLabel1.Text = "CurrentFont";
			// 
			// fontLabel1
			// 
			this.fontLabel1.AutoSize = true;
			this.fontLabel1.Location = new System.Drawing.Point(31, 322);
			this.fontLabel1.Name = "fontLabel1";
			this.fontLabel1.Size = new System.Drawing.Size(44, 12);
			this.fontLabel1.TabIndex = 35;
			this.fontLabel1.Text = "フォント：";
			// 
			// stringLabel
			// 
			this.stringLabel.AutoSize = true;
			this.stringLabel.Location = new System.Drawing.Point(31, 254);
			this.stringLabel.Name = "stringLabel";
			this.stringLabel.Size = new System.Drawing.Size(47, 12);
			this.stringLabel.TabIndex = 34;
			this.stringLabel.Text = "文字列：";
			// 
			// currentColorLabel
			// 
			this.currentColorLabel.AutoSize = true;
			this.currentColorLabel.Location = new System.Drawing.Point(71, 219);
			this.currentColorLabel.Name = "currentColorLabel";
			this.currentColorLabel.Size = new System.Drawing.Size(70, 12);
			this.currentColorLabel.TabIndex = 33;
			this.currentColorLabel.Text = "CurrentColor";
			// 
			// colorLabel
			// 
			this.colorLabel.AutoSize = true;
			this.colorLabel.Location = new System.Drawing.Point(31, 219);
			this.colorLabel.Name = "colorLabel";
			this.colorLabel.Size = new System.Drawing.Size(23, 12);
			this.colorLabel.TabIndex = 32;
			this.colorLabel.Text = "色：";
			// 
			// lineLabel
			// 
			this.lineLabel.AutoSize = true;
			this.lineLabel.Location = new System.Drawing.Point(31, 152);
			this.lineLabel.Name = "lineLabel";
			this.lineLabel.Size = new System.Drawing.Size(53, 12);
			this.lineLabel.TabIndex = 31;
			this.lineLabel.Text = "線の太さ：";
			// 
			// drawLabel
			// 
			this.drawLabel.AutoSize = true;
			this.drawLabel.Location = new System.Drawing.Point(31, 126);
			this.drawLabel.Name = "drawLabel";
			this.drawLabel.Size = new System.Drawing.Size(59, 12);
			this.drawLabel.TabIndex = 30;
			this.drawLabel.Text = "描画方法：";
			// 
			// epLabel2
			// 
			this.epLabel2.AutoSize = true;
			this.epLabel2.Location = new System.Drawing.Point(136, 92);
			this.epLabel2.Name = "epLabel2";
			this.epLabel2.Size = new System.Drawing.Size(12, 12);
			this.epLabel2.TabIndex = 29;
			this.epLabel2.Text = "Y";
			// 
			// epLabel1
			// 
			this.epLabel1.AutoSize = true;
			this.epLabel1.Location = new System.Drawing.Point(31, 92);
			this.epLabel1.Name = "epLabel1";
			this.epLabel1.Size = new System.Drawing.Size(46, 12);
			this.epLabel1.TabIndex = 28;
			this.epLabel1.Text = "終点： X";
			// 
			// spLabel2
			// 
			this.spLabel2.AutoSize = true;
			this.spLabel2.Location = new System.Drawing.Point(136, 64);
			this.spLabel2.Name = "spLabel2";
			this.spLabel2.Size = new System.Drawing.Size(12, 12);
			this.spLabel2.TabIndex = 27;
			this.spLabel2.Text = "Y";
			// 
			// spLabel1
			// 
			this.spLabel1.AutoSize = true;
			this.spLabel1.Location = new System.Drawing.Point(31, 64);
			this.spLabel1.Name = "spLabel1";
			this.spLabel1.Size = new System.Drawing.Size(46, 12);
			this.spLabel1.TabIndex = 26;
			this.spLabel1.Text = "始点： X";
			// 
			// shapeLabel
			// 
			this.shapeLabel.AutoSize = true;
			this.shapeLabel.Location = new System.Drawing.Point(31, 29);
			this.shapeLabel.Name = "shapeLabel";
			this.shapeLabel.Size = new System.Drawing.Size(35, 12);
			this.shapeLabel.TabIndex = 25;
			this.shapeLabel.Text = "図形：";
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cancelButton.Location = new System.Drawing.Point(239, 364);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(88, 32);
			this.cancelButton.TabIndex = 49;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// setButton
			// 
			this.setButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.setButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.setButton.Location = new System.Drawing.Point(128, 364);
			this.setButton.Name = "setButton";
			this.setButton.Size = new System.Drawing.Size(88, 32);
			this.setButton.TabIndex = 50;
			this.setButton.Text = "設　定";
			this.setButton.UseVisualStyleBackColor = true;
			this.setButton.Click += new System.EventHandler(this.setButton_Click);
			// 
			// angleUpDown1
			// 
			this.angleUpDown1.Location = new System.Drawing.Point(250, 149);
			this.angleUpDown1.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
			this.angleUpDown1.Name = "angleUpDown1";
			this.angleUpDown1.Size = new System.Drawing.Size(40, 19);
			this.angleUpDown1.TabIndex = 52;
			// 
			// anglelabel1
			// 
			this.anglelabel1.AutoSize = true;
			this.anglelabel1.Location = new System.Drawing.Point(169, 152);
			this.anglelabel1.Name = "anglelabel1";
			this.anglelabel1.Size = new System.Drawing.Size(67, 12);
			this.anglelabel1.TabIndex = 51;
			this.anglelabel1.Text = "回転角(度)：";
			// 
			// xrect
			// 
			this.xrect.AutoSize = true;
			this.xrect.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.xrect.Location = new System.Drawing.Point(91, 184);
			this.xrect.Name = "xrect";
			this.xrect.Size = new System.Drawing.Size(36, 16);
			this.xrect.TabIndex = 53;
			this.xrect.Text = "横";
			this.xrect.UseVisualStyleBackColor = true;
			// 
			// yrect
			// 
			this.yrect.AutoSize = true;
			this.yrect.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.yrect.Location = new System.Drawing.Point(133, 184);
			this.yrect.Name = "yrect";
			this.yrect.Size = new System.Drawing.Size(36, 16);
			this.yrect.TabIndex = 54;
			this.yrect.Text = "縦";
			this.yrect.UseVisualStyleBackColor = true;
			// 
			// aspect
			// 
			this.aspect.AutoSize = true;
			this.aspect.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.aspect.Location = new System.Drawing.Point(242, 185);
			this.aspect.Name = "aspect";
			this.aspect.Size = new System.Drawing.Size(84, 16);
			this.aspect.TabIndex = 55;
			this.aspect.Text = "縦横比固定";
			this.aspect.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(31, 185);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 12);
			this.label1.TabIndex = 56;
			this.label1.Text = "正方選択：";
			// 
			// RubberBandDialog
			// 
			this.ClientSize = new System.Drawing.Size(352, 422);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.aspect);
			this.Controls.Add(this.yrect);
			this.Controls.Add(this.xrect);
			this.Controls.Add(this.angleUpDown1);
			this.Controls.Add(this.anglelabel1);
			this.Controls.Add(this.setButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.fontButton1);
			this.Controls.Add(this.stringTextBox);
			this.Controls.Add(this.drawButton);
			this.Controls.Add(this.colorButton);
			this.Controls.Add(this.fillingRadioButton);
			this.Controls.Add(this.drawingRadioButton);
			this.Controls.Add(this.lineNumericUpDown);
			this.Controls.Add(this.epyNumericUpDown);
			this.Controls.Add(this.epxNumericUpDown);
			this.Controls.Add(this.spyNumericUpDown);
			this.Controls.Add(this.spxNumericUpDown);
			this.Controls.Add(this.shapeComboBox);
			this.Controls.Add(this.currentFontLabel1);
			this.Controls.Add(this.fontLabel1);
			this.Controls.Add(this.stringLabel);
			this.Controls.Add(this.currentColorLabel);
			this.Controls.Add(this.colorLabel);
			this.Controls.Add(this.lineLabel);
			this.Controls.Add(this.drawLabel);
			this.Controls.Add(this.epLabel2);
			this.Controls.Add(this.epLabel1);
			this.Controls.Add(this.spLabel2);
			this.Controls.Add(this.spLabel1);
			this.Controls.Add(this.shapeLabel);
			this.Name = "RubberBandDialog";
			this.Text = "RubberBandDialog";
			this.Load += new System.EventHandler(this.RubberBandDialog_Load);
			((System.ComponentModel.ISupportInitialize)(this.lineNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.epyNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.epxNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spyNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spxNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.angleUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
