using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XMLTreeMenu.Controls
{
	public class settingDialog : Form//, IMDIForm
	{
		private RichTextBox _textBox;

		private IContainer components = null;

		private Button OKButton;

		private Button ApplyButton;

		private Button cancelButton;

		private TabPage tabPage1;

		private TabControl tabControl1;

		private TextBox PreViewTextBox;

		private Label label1;

		private Button FontButton;

		private Button BgColorButton;

		private ColorDialog colorDialog1;

		private FontDialog fontDialog1;

		private Button ResetButton;
/*
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
*/
		public RichTextBox textBox
		{
			get
			{
				return this._textBox;
			}
			set
			{
				this._textBox = value;
			}
		}

		public settingDialog()
		{
			this.InitializeComponent();
			this.InitializeChildControl();
		}

		public settingDialog(RichTextBox txtBox)
		{
			this._textBox = txtBox;
			this.InitializeComponent();
			this.InitializeChildControl();
		}

		private void InitializeChildControl()
		{
		}

		private void settingDialog_Load(object sender, EventArgs e)
		{
			this.PreViewTextBox.ForeColor = this._textBox.ForeColor;
			this.PreViewTextBox.BackColor = this._textBox.BackColor;
			this.PreViewTextBox.Font = this._textBox.Font;
		}

		private void FontButton_Click(object sender, EventArgs e)
		{
			this.fontDialog1.Color = this.PreViewTextBox.ForeColor;
			this.fontDialog1.Font = this.PreViewTextBox.Font;
			this.fontDialog1.ShowDialog(this);
			this.PreViewTextBox.Font = this.fontDialog1.Font;
			this.PreViewTextBox.ForeColor = this.fontDialog1.Color;
		}

		private void BgColorButton_Click(object sender, EventArgs e)
		{
			this.colorDialog1.Color = this.PreViewTextBox.BackColor;
			this.colorDialog1.ShowDialog(this);
			this.PreViewTextBox.BackColor = this.colorDialog1.Color;
		}

		private void ApplyButton_Click(object sender, EventArgs e)
		{
			this.SaveSettings();
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			base.Close();
			base.Dispose();
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			this.SaveSettings();
			base.Close();
			base.Dispose();
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
		}

		private void SaveSettings()
		{
			this._textBox.Font = this.PreViewTextBox.Font;
			this._textBox.BackColor = this.PreViewTextBox.BackColor;
			this._textBox.ForeColor = this.PreViewTextBox.ForeColor;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		public void InitializeInterface()
		{
		}

		private void InitializeComponent()
		{
			this.OKButton = new Button();
			this.ApplyButton = new Button();
			this.cancelButton = new Button();
			this.tabPage1 = new TabPage();
			this.ResetButton = new Button();
			this.label1 = new Label();
			this.FontButton = new Button();
			this.BgColorButton = new Button();
			this.PreViewTextBox = new TextBox();
			this.tabControl1 = new TabControl();
			this.colorDialog1 = new ColorDialog();
			this.fontDialog1 = new FontDialog();
			this.tabPage1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			base.SuspendLayout();
			this.OKButton.Location = new Point(183, 259);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new Size(77, 26);
			this.OKButton.TabIndex = 1;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new EventHandler(this.OKButton_Click);
			this.ApplyButton.Location = new Point(266, 259);
			this.ApplyButton.Name = "ApplyButton";
			this.ApplyButton.Size = new Size(77, 26);
			this.ApplyButton.TabIndex = 2;
			this.ApplyButton.Text = "適用";
			this.ApplyButton.UseVisualStyleBackColor = true;
			this.ApplyButton.Click += new EventHandler(this.ApplyButton_Click);
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.cancelButton.Location = new Point(349, 259);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new Size(77, 26);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new EventHandler(this.CancelButton_Click);
			this.tabPage1.Controls.Add(this.ResetButton);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.FontButton);
			this.tabPage1.Controls.Add(this.BgColorButton);
			this.tabPage1.Controls.Add(this.PreViewTextBox);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3);
			this.tabPage1.Size = new Size(421, 215);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "色とフォント";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.ResetButton.Location = new Point(311, 133);
			this.ResetButton.Name = "ResetButton";
			this.ResetButton.Size = new Size(83, 25);
			this.ResetButton.TabIndex = 4;
			this.ResetButton.Text = "リセット(&R)";
			this.ResetButton.UseVisualStyleBackColor = true;
			this.ResetButton.Click += new EventHandler(this.ResetButton_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(18, 23);
			this.label1.Name = "label1";
			this.label1.Size = new Size(66, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "プレビュー(&P):";
			this.FontButton.Location = new Point(311, 91);
			this.FontButton.Name = "FontButton";
			this.FontButton.Size = new Size(84, 24);
			this.FontButton.TabIndex = 2;
			this.FontButton.Text = "フォント(&F)...";
			this.FontButton.UseVisualStyleBackColor = true;
			this.FontButton.Click += new EventHandler(this.FontButton_Click);
			this.BgColorButton.Location = new Point(311, 47);
			this.BgColorButton.Name = "BgColorButton";
			this.BgColorButton.Size = new Size(84, 24);
			this.BgColorButton.TabIndex = 1;
			this.BgColorButton.Text = "背景色(&B)...";
			this.BgColorButton.UseVisualStyleBackColor = true;
			this.BgColorButton.Click += new EventHandler(this.BgColorButton_Click);
			this.PreViewTextBox.Location = new Point(20, 47);
			this.PreViewTextBox.Multiline = true;
			this.PreViewTextBox.Name = "PreViewTextBox";
			this.PreViewTextBox.ReadOnly = true;
			this.PreViewTextBox.Size = new Size(276, 124);
			this.PreViewTextBox.TabIndex = 0;
			this.PreViewTextBox.Text = "12345\r\nabcde\r\nABCDE\r\nｱｲｳｴｵ\r\nアイウエオ\r\nあいうえお";
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new Point(2, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(429, 241);
			this.tabControl1.TabIndex = 0;
			this.fontDialog1.ShowColor = true;
			base.AcceptButton = this.OKButton;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(435, 294);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.ApplyButton);
			base.Controls.Add(this.OKButton);
			base.Controls.Add(this.tabControl1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "settingDialog";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "アブリレーションの設定";
			base.Load += new EventHandler(this.settingDialog_Load);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
