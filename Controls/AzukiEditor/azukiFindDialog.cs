using Sgry.Azuki;
using Sgry.Azuki.WinForms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AntPanelApplication.Controls
{
 	public class azukiFindDialog : Form
	{
		private AzukiControl _textBox;
		private azukiDialogMode _mode;
		private int findStartIndex = 0;
		private int findCount = 0;
		private string dialogTitle = "";
		private int begin = 0;
		private int end = 0;
		private int SelectionLength = 0;
		private IContainer components = null;
		private Label label1;
		private TextBox findTextBox;
		private Button findButton;
		private Button cancelButton;
		private CheckBox LgSmCheckBox;
		private GroupBox findCourseGroupBox;
		private RadioButton currentPosRadio;
		private RadioButton topPosRadio;
		private Panel ReplacePanel;
		private Button replaceAllButton;
		private Button replaceNextButton;
		private TextBox ReplaceTextBox;
		private Label ReplaceLabel;
		private CheckBox checkBox1;

		public AzukiControl textBox
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

		public azukiDialogMode Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
				if (this._mode == azukiDialogMode.Find)
				{
					this.dialogTitle = "検索";
					this.ReplacePanel.Visible = false;
				}
				else
				{
					this.dialogTitle = "置換";
					this.ReplacePanel.Visible = true;
				}
				this.Text = this.dialogTitle;
			}
		}

		public azukiFindDialog()
		{
			this.InitializeComponent();
			this.findTextBox.Text = this._textBox.Document.GetTextInRange(ref this.begin, ref this.end);
		}

		public azukiFindDialog(AzukiControl txtBox)
		{
			this.InitializeComponent();
			this._textBox = txtBox;
			this.findTextBox.Text = this._textBox.Document.GetTextInRange(ref this.begin, ref this.end);
		}

		public azukiFindDialog(azukiDialogMode mode)
		{
			this.InitializeComponent();
			this.Mode = mode;
			this.findTextBox.Text = this._textBox.Document.GetTextInRange(ref this.begin, ref this.end);
		}

		public azukiFindDialog(azukiDialogMode mode, AzukiControl txtBox)
		{
			this.InitializeComponent();
			this._textBox = txtBox;
			this.Mode = mode;
			this.findTextBox.Text = this._textBox.Document.GetTextInRange(ref this.begin, ref this.end);
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			base.Close();
			base.Dispose();
		}

		private void findButton_Click(object sender, EventArgs e)
		{
			if (this.checkBox1.Checked)
			{
				this.replaceNextButton.Enabled = this.ExecRegExpFind2();
			}
			else
			{
				this.replaceNextButton.Enabled = this.ExecFind();
			}
		}

		private void replaceNextButton_Click(object sender, EventArgs e)
		{
			this._textBox.Document.GetSelection(out this.begin, out this.end);
			this.SelectionLength = this.end - this.begin;
			if (this.SelectionLength > 0)
			{
				this._textBox.Document.Replace(this.ReplaceTextBox.Text, this.begin, this.end);
			}
			this.replaceNextButton.Enabled = this.ExecFind();
		}

		private void replaceAllButton_Click(object sender, EventArgs e)
		{
			if (this.checkBox1.Checked)
			{
				while (this.ExecRegExpFind())
				{
					this._textBox.Document.GetSelection(out this.begin, out this.end);
					this.SelectionLength = this.end - this.begin;
					if (this.SelectionLength > 0)
					{
						this._textBox.Document.Replace(this.ReplaceTextBox.Text, this.begin, this.end);
					}
				}
			}
			else
			{
				while (this.ExecFind())
				{
					this._textBox.Document.GetSelection(out this.begin, out this.end);
					this.SelectionLength = this.end - this.begin;
					if (this.SelectionLength > 0)
					{
						this._textBox.Document.Replace(this.ReplaceTextBox.Text, this.begin, this.end);
					}
				}
			}
		}

		private void findPosition_Radio_CheckedChanged(object sender, EventArgs e)
		{
			this.topPosRadio.Checked = !this.currentPosRadio.Checked;
		}

		private bool ExecFind()
		{
			this._textBox.Document.GetSelection(out this.begin, out this.end);
			this.SelectionLength = this.end - this.begin;
			string text = this._textBox.Document.Text;
			string text2 = this.findTextBox.Text;
			int length = text2.Length;
			int num = this.topPosRadio.Checked ? 0 : this.begin;
			this.findStartIndex = ((this.findStartIndex == 0) ? num : this.findStartIndex);
			num = text.IndexOf(text2, this.findStartIndex, this.LgSmCheckBox.Checked ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);
			bool result;
			if (num == -1)
			{
				string text3 = (this._mode == azukiDialogMode.Find) ? "ドキュメントの最後まで検索しました。\nもう一度先頭から検索しますか?" : (this.findCount.ToString() + " 件置換しました");
				string text4 = "\"" + text2 + "\"は見つかりません。";
				if (this.findCount != 0)
				{
					if (MessageBox.Show(this, text3, this.dialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.ResetFindPosition();
					}
					else
					{
						base.Close();
						base.Dispose();
					}
				}
				else if (this.currentPosRadio.Checked)
				{
					string text5 = "現在位置からドキュメントの最後まで検索しました。\nもう一度先頭から検索しますか?";
					if (MessageBox.Show(this, text5, this.dialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.ResetFindPosition();
					}
					else
					{
						base.Close();
						base.Dispose();
					}
				}
				else
				{
					MessageBox.Show(this, text4, this.dialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				result = false;
			}
			else
			{
				this._textBox.Document.SetSelection(num, num + length);
				this._textBox.ScrollToCaret();
				this.findStartIndex = num + length;
				this.findCount++;
				this._textBox.Focus();
				result = true;
			}
			return result;
		}

		private bool ExecRegExpFind()
		{
			this._textBox.Document.GetSelection(out this.begin, out this.end);
			this.SelectionLength = this.end - this.begin;
			string text = this.findTextBox.Text;
			Regex regex = new Regex(text, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			int num = this.topPosRadio.Checked ? 0 : this.begin;
			this.findStartIndex = ((this.findStartIndex == 0) ? num : this.findStartIndex);
			SearchResult searchResult = this._textBox.Document.FindNext(regex, this.findStartIndex);
			if (searchResult != null)
			{
				num = searchResult.Begin;
			}
			else
			{
				num = -1;
			}
			bool result;
			if (num == -1)
			{
				string text2 = (this._mode == azukiDialogMode.Find) ? "ドキュメントの最後まで検索しました。\nもう一度先頭から検索しますか?" : (this.findCount.ToString() + " 件置換しました");
				string text3 = "\"" + text + "\"は見つかりません。";
				if (this.findCount != 0)
				{
					if (MessageBox.Show(this, text2, this.dialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.ResetFindPosition();
					}
					else
					{
						base.Close();
						base.Dispose();
					}
				}
				else if (this.currentPosRadio.Checked)
				{
					string text4 = "現在位置からドキュメントの最後まで検索しました。\nもう一度先頭から検索しますか?";
					if (MessageBox.Show(this, text4, this.dialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.ResetFindPosition();
					}
					else
					{
						base.Close();
						base.Dispose();
					}
				}
				else
				{
					MessageBox.Show(this, text3, this.dialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				result = false;
			}
			else
			{
				int num2 = searchResult.End - searchResult.Begin;
				this._textBox.Document.SetSelection(searchResult.Begin, searchResult.End);
				this._textBox.ScrollToCaret();
				this.findStartIndex = num + num2;
				this.findCount++;
				this._textBox.Focus();
				result = true;
			}
			return result;
		}

		private bool ExecRegExpFind2()
		{
			this._textBox.Document.GetSelection(out this.begin, out this.end);
			this.SelectionLength = this.end - this.begin;
			string text = this.findTextBox.Text;
			Regex regex = new Regex(text, RegexOptions.IgnoreCase | RegexOptions.Multiline);
			int num = this.topPosRadio.Checked ? 0 : this.begin;
			this.findStartIndex = ((this.findStartIndex == 0) ? num : this.findStartIndex);
			Match match = regex.Match(this._textBox.Document.Text, this.findStartIndex);
			if (match.Success)
			{
				num = match.Index;
			}
			else
			{
				num = -1;
			}
			bool result;
			if (num == -1)
			{
				string text2 = (this._mode == azukiDialogMode.Find) ? "ドキュメントの最後まで検索しました。\nもう一度先頭から検索しますか?" : (this.findCount.ToString() + " 件置換しました");
				string text3 = "\"" + text + "\"は見つかりません。";
				if (this.findCount != 0)
				{
					if (MessageBox.Show(this, text2, this.dialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.ResetFindPosition();
					}
					else
					{
						base.Close();
						base.Dispose();
					}
				}
				else if (this.currentPosRadio.Checked)
				{
					string text4 = "現在位置からドキュメントの最後まで検索しました。\nもう一度先頭から検索しますか?";
					if (MessageBox.Show(this, text4, this.dialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.ResetFindPosition();
					}
					else
					{
						base.Close();
						base.Dispose();
					}
				}
				else
				{
					MessageBox.Show(this, text3, this.dialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				result = false;
			}
			else
			{
				int length = match.Length;
				this._textBox.Document.SetSelection(match.Index, match.Index + match.Length);
				this._textBox.ScrollToCaret();
				this.findStartIndex = num + length + 1;
				this.findCount++;
				this._textBox.Focus();
				result = true;
			}
			return result;
		}

		private void ResetFindPosition()
		{
			this.findStartIndex = 0;
			this.findCount = 0;
			this._textBox.Document.SetSelection(0, 0);
			this._textBox.Focus();
		}

		private void replaceAllButton_Click_1(object sender, EventArgs e)
		{
			if (this.checkBox1.Checked)
			{
				while (this.ExecRegExpFind())
				{
					this._textBox.Document.GetSelection(out this.begin, out this.end);
					this.SelectionLength = this.end - this.begin;
					if (this.SelectionLength > 0)
					{
						this._textBox.Document.Replace(this.ReplaceTextBox.Text, this.begin, this.end);
					}
				}
			}
			else
			{
				while (this.ExecFind())
				{
					this._textBox.Document.GetSelection(out this.begin, out this.end);
					this.SelectionLength = this.end - this.begin;
					if (this.SelectionLength > 0)
					{
						this._textBox.Document.Replace(this.ReplaceTextBox.Text, this.begin, this.end);
					}
				}
			}
		}

		private void LgSmCheckBox_CheckedChanged(object sender, EventArgs e)
		{
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
			this.findTextBox = new TextBox();
			this.findButton = new Button();
			this.cancelButton = new Button();
			this.LgSmCheckBox = new CheckBox();
			this.findCourseGroupBox = new GroupBox();
			this.currentPosRadio = new RadioButton();
			this.topPosRadio = new RadioButton();
			this.ReplacePanel = new Panel();
			this.replaceAllButton = new Button();
			this.replaceNextButton = new Button();
			this.ReplaceTextBox = new TextBox();
			this.ReplaceLabel = new Label();
			this.checkBox1 = new CheckBox();
			this.findCourseGroupBox.SuspendLayout();
			this.ReplacePanel.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new Font("MS UI Gothic", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 128);
			this.label1.Location = new Point(7, 7);
			this.label1.Name = "label1";
			this.label1.Size = new Size(129, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "検索する文字列(&N):";
			this.findTextBox.Font = new Font("MS UI Gothic", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 128);
			this.findTextBox.Location = new Point(139, 7);
			this.findTextBox.Name = "findTextBox";
			this.findTextBox.Size = new Size(253, 20);
			this.findTextBox.TabIndex = 1;
			this.findButton.DialogResult = DialogResult.Cancel;
			this.findButton.Location = new Point(398, 4);
			this.findButton.Name = "findButton";
			this.findButton.Size = new Size(108, 26);
			this.findButton.TabIndex = 3;
			this.findButton.Text = "次を検索(&F)";
			this.findButton.UseVisualStyleBackColor = true;
			this.findButton.Click += new EventHandler(this.findButton_Click);
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.cancelButton.Location = new Point(398, 100);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new Size(108, 26);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
			this.LgSmCheckBox.AutoSize = true;
			this.LgSmCheckBox.Font = new Font("MS UI Gothic", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 128);
			this.LgSmCheckBox.Location = new Point(7, 100);
			this.LgSmCheckBox.Name = "LgSmCheckBox";
			this.LgSmCheckBox.Size = new Size(161, 17);
			this.LgSmCheckBox.TabIndex = 4;
			this.LgSmCheckBox.Text = "大文字小文字を区別する";
			this.LgSmCheckBox.UseVisualStyleBackColor = true;
			this.LgSmCheckBox.CheckedChanged += new EventHandler(this.LgSmCheckBox_CheckedChanged);
			this.findCourseGroupBox.Controls.Add(this.currentPosRadio);
			this.findCourseGroupBox.Controls.Add(this.topPosRadio);
			this.findCourseGroupBox.Font = new Font("MS UI Gothic", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 128);
			this.findCourseGroupBox.Location = new Point(196, 46);
			this.findCourseGroupBox.Name = "findCourseGroupBox";
			this.findCourseGroupBox.Size = new Size(225, 48);
			this.findCourseGroupBox.TabIndex = 5;
			this.findCourseGroupBox.TabStop = false;
			this.findCourseGroupBox.Text = "検索する方法";
			this.currentPosRadio.AutoSize = true;
			this.currentPosRadio.Location = new Point(109, 21);
			this.currentPosRadio.Name = "currentPosRadio";
			this.currentPosRadio.Size = new Size(113, 17);
			this.currentPosRadio.TabIndex = 6;
			this.currentPosRadio.Text = "現在位置から(&D)";
			this.currentPosRadio.UseVisualStyleBackColor = true;
			this.currentPosRadio.CheckedChanged += new EventHandler(this.findPosition_Radio_CheckedChanged);
			this.topPosRadio.AutoSize = true;
			this.topPosRadio.Checked = true;
			this.topPosRadio.Location = new Point(15, 21);
			this.topPosRadio.Name = "topPosRadio";
			this.topPosRadio.Size = new Size(87, 17);
			this.topPosRadio.TabIndex = 0;
			this.topPosRadio.TabStop = true;
			this.topPosRadio.Text = "先頭から(&T)";
			this.topPosRadio.UseVisualStyleBackColor = true;
			this.topPosRadio.CheckedChanged += new EventHandler(this.findPosition_Radio_CheckedChanged);
			this.ReplacePanel.Controls.Add(this.replaceAllButton);
			this.ReplacePanel.Controls.Add(this.replaceNextButton);
			this.ReplacePanel.Controls.Add(this.ReplaceTextBox);
			this.ReplacePanel.Controls.Add(this.ReplaceLabel);
			this.ReplacePanel.Location = new Point(0, 35);
			this.ReplacePanel.Name = "ReplacePanel";
			this.ReplacePanel.Size = new Size(507, 60);
			this.ReplacePanel.TabIndex = 7;
			this.replaceAllButton.Location = new Point(397, 34);
			this.replaceAllButton.Name = "replaceAllButton";
			this.replaceAllButton.Size = new Size(108, 26);
			this.replaceAllButton.TabIndex = 4;
			this.replaceAllButton.Text = "すべて置換(&A)";
			this.replaceAllButton.UseVisualStyleBackColor = true;
			this.replaceAllButton.Click += new EventHandler(this.replaceAllButton_Click_1);
			this.replaceNextButton.Enabled = false;
			this.replaceNextButton.Location = new Point(397, -1);
			this.replaceNextButton.Name = "replaceNextButton";
			this.replaceNextButton.Size = new Size(108, 26);
			this.replaceNextButton.TabIndex = 10;
			this.replaceNextButton.Text = "置換して次に(&R)";
			this.replaceNextButton.UseVisualStyleBackColor = true;
			this.replaceNextButton.Click += new EventHandler(this.replaceNextButton_Click);
			this.ReplaceTextBox.Font = new Font("MS UI Gothic", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 128);
			this.ReplaceTextBox.Location = new Point(141, 3);
			this.ReplaceTextBox.Name = "ReplaceTextBox";
			this.ReplaceTextBox.Size = new Size(251, 20);
			this.ReplaceTextBox.TabIndex = 2;
			this.ReplaceLabel.AutoSize = true;
			this.ReplaceLabel.Font = new Font("MS UI Gothic", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 128);
			this.ReplaceLabel.Location = new Point(3, 9);
			this.ReplaceLabel.Name = "ReplaceLabel";
			this.ReplaceLabel.Size = new Size(131, 15);
			this.ReplaceLabel.TabIndex = 12;
			this.ReplaceLabel.Text = "置換後の文字列(&P):";
			this.checkBox1.AutoSize = true;
			this.checkBox1.Font = new Font("MS UI Gothic", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 128);
			this.checkBox1.Location = new Point(184, 100);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(78, 17);
			this.checkBox1.TabIndex = 8;
			this.checkBox1.Text = "正規表現";
			this.checkBox1.UseVisualStyleBackColor = true;
			base.AcceptButton = this.findButton;
			base.AutoScaleMode = AutoScaleMode.None;
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.CancelButton = this.cancelButton;
			base.ClientSize = new Size(512, 131);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.ReplacePanel);
			base.Controls.Add(this.findCourseGroupBox);
			base.Controls.Add(this.LgSmCheckBox);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.findButton);
			base.Controls.Add(this.findTextBox);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "azukiFindDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.findCourseGroupBox.ResumeLayout(false);
			this.findCourseGroupBox.PerformLayout();
			this.ReplacePanel.ResumeLayout(false);
			this.ReplacePanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
