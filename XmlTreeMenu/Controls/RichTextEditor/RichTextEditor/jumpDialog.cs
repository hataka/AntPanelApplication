using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XMLTreeMenu.Controls
{
	public class jumpDialog : Form
	{
		private RichTextBox _textBox;

		private IContainer components = null;

		private Label label1;

		private Button okButton;

		private TextBox lineNumTextBox;

		private Button cancelButton;

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

		public jumpDialog()
		{
			this.InitializeComponent();
		}

		public jumpDialog(RichTextBox txtBox)
		{
			this.InitializeComponent();
			this._textBox = txtBox;
		}

		private void jumpDialog_Load(object sender, EventArgs e)
		{
			this.lineNumTextBox.Text = this.GetCurrntLineNumber().ToString();
			this.lineNumTextBox.SelectAll();
			this.lineNumTextBox.Focus();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			base.Close();
			base.Dispose();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			string[] array = this._textBox.Text.Split(new char[]
			{
				'\n'
			});
			int i = int.Parse(this.lineNumTextBox.Text) - 1;
			int num = array.Length;
			int num2 = 0;
			if (num < i)
			{
				MessageBox.Show("行番号が範囲外です。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num3 = 0;
				while (i >= num3)
				{
					num2 = array[num3].Length;
					stringBuilder.Append(array[num3]);
					num3++;
				}
				this._textBox.SelectionStart = stringBuilder.ToString().Length - (num2 - i);
				this._textBox.Focus();
				base.Close();
				base.Dispose();
			}
		}

		private int GetCurrntLineNumber()
		{
			int selectionStart = this._textBox.SelectionStart;
			string text = this._textBox.Text.Substring(0, selectionStart);
			return text.Split(new char[]
			{
				'\n'
			}).Length;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.okButton = new Button();
			this.lineNumTextBox = new TextBox();
			this.cancelButton = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 25);
			this.label1.Name = "label1";
			this.label1.Size = new Size(57, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "行番号(&L):";
			this.okButton.Location = new Point(14, 66);
			this.okButton.Name = "okButton";
			this.okButton.Size = new Size(84, 20);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new EventHandler(this.okButton_Click);
			this.lineNumTextBox.ImeMode = ImeMode.Alpha;
			this.lineNumTextBox.Location = new Point(75, 25);
			this.lineNumTextBox.Name = "lineNumTextBox";
			this.lineNumTextBox.Size = new Size(123, 19);
			this.lineNumTextBox.TabIndex = 2;
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.cancelButton.Location = new Point(130, 66);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new Size(84, 20);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
			base.AcceptButton = this.okButton;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.ClientSize = new Size(250, 100);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.lineNumTextBox);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "jumpDialog";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "行へ移動";
			base.Load += new EventHandler(this.jumpDialog_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
