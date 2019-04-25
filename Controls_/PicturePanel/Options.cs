using System;
using System.Drawing;
using System.Windows.Forms;

namespace AntPanelApplication.Controls
{
	public class Options : Form
	{
		private DomainUpDown domainUpDown1;

		public Label label1;

		public Button button1;

		private PicturePanel parent;

		public Color scribble_color;

		private Button OkButton;

		private Button cancelButton;

		private Label label2;

		public int scribble_width;

		public Options()
		{
			this.InitializeComponent();
		}

		public Options(PicturePanel F)
		{
			this.parent = F;
			this.InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.domainUpDown1 = new DomainUpDown();
			this.label1 = new Label();
			this.button1 = new Button();
			this.label2 = new Label();
			this.OkButton = new Button();
			this.cancelButton = new Button();
			base.SuspendLayout();
			this.domainUpDown1.Items.Add("1");
			this.domainUpDown1.Items.Add("2");
			this.domainUpDown1.Items.Add("3");
			this.domainUpDown1.Items.Add("4");
			this.domainUpDown1.Items.Add("5");
			this.domainUpDown1.Items.Add("6");
			this.domainUpDown1.Items.Add("7");
			this.domainUpDown1.Items.Add("8");
			this.domainUpDown1.Items.Add("9");
			this.domainUpDown1.Items.Add("10");
			this.domainUpDown1.Items.Add("11");
			this.domainUpDown1.Items.Add("12");
			this.domainUpDown1.Items.Add("13");
			this.domainUpDown1.Items.Add("14");
			this.domainUpDown1.Items.Add("15");
			this.domainUpDown1.Items.Add("16");
			this.domainUpDown1.Items.Add("17");
			this.domainUpDown1.Items.Add("18");
			this.domainUpDown1.Items.Add("19");
			this.domainUpDown1.Items.Add("20");
			this.domainUpDown1.Location = new Point(72, 7);
			this.domainUpDown1.Name = "domainUpDown1";
			this.domainUpDown1.Size = new Size(64, 19);
			this.domainUpDown1.TabIndex = 2;
			this.domainUpDown1.SelectedItemChanged += new EventHandler(this.domainUpDown1_SelectedItemChanged);
			this.label1.Location = new Point(8, 7);
			this.label1.Name = "label1";
			this.label1.Size = new Size(56, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "Pen Width";
			this.button1.BackColor = Color.FromArgb(128, 128, 255);
			this.button1.Location = new Point(72, 37);
			this.button1.Name = "button1";
			this.button1.Size = new Size(64, 22);
			this.button1.TabIndex = 6;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label2.Location = new Point(8, 37);
			this.label2.Name = "label2";
			this.label2.Size = new Size(56, 15);
			this.label2.TabIndex = 4;
			this.label2.Text = "Pen Color";
			this.label2.TextAlign = ContentAlignment.MiddleRight;
			this.OkButton.DialogResult = DialogResult.OK;
			this.OkButton.FlatStyle = FlatStyle.System;
			this.OkButton.Location = new Point(72, 78);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new Size(42, 23);
			this.OkButton.TabIndex = 39;
			this.OkButton.Text = "OK";
			this.OkButton.Click += new EventHandler(this.OkButton_Click);
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.cancelButton.Location = new Point(120, 78);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new Size(47, 23);
			this.cancelButton.TabIndex = 40;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
			this.AutoScaleBaseSize = new Size(5, 12);
			this.BackColor = Color.Silver;
			base.ClientSize = new Size(179, 104);
			base.ControlBox = false;
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.OkButton);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.domainUpDown1);
			base.Controls.Add(this.label2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Options";
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Options";
			base.Load += new EventHandler(this.Options_Load);
			base.Paint += new PaintEventHandler(this.Options_Paint);
			base.ResumeLayout(false);
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
		}

		private void Options_Load(object sender, EventArgs e)
		{
		}

		private void Options_Paint(object sender, PaintEventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				this.scribble_color = colorDialog.Color;
				this.button1.BackColor = this.scribble_color;
			}
		}

		private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
		{
			int selectedIndex = this.domainUpDown1.SelectedIndex;
			if (selectedIndex >= 0)
			{
				string s = (string)this.domainUpDown1.Items[selectedIndex];
				this.scribble_width = int.Parse(s);
			}
		}

		private void Options_Move(object sender, EventArgs e)
		{
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
