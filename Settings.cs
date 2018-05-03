using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Collections.Generic;
using System.Drawing;

namespace AntPlugin
{
	[Serializable]
	public class Settings
	{
		private const String DEFAULT_ANT_PATH = "";
		private const String DEFAULT_ADD_ARGS = "";
		
		private String antPath = DEFAULT_ANT_PATH;
		private String addArgs = DEFAULT_ADD_ARGS;

		private System.Drawing.Font antPanelDefaultFont = new Font("Meiryo UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 128);

		private bool restoreCuctomSession;
		private List<string> customSessionData = new List<string>();
		private List<string> previousCustomDocuments = new List<string>();
		private List<string> fileStates = new List<string>();

		private string documentRoot = @"C:\Apache2.2\htdocs";//"F:";
		private string serverRoot = "http://localhost";
		private string sakuraPath = @"C:\Program Files (x86)\sakura\sakura.exe";//@"C:\TiuDevTools\sakura\sakura.exe";
		private string pspadPath = @"C:\Program Files (x86)\PSPad editor\PSPad.exe";//F:\Programs\PSPad editor\PSPad.exe";
		private string chromePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";//@"C:\Documents and Settings\kazuhiko\Local Settings\Application Data\Google\Chrome\Application\chrome.exe";
		private string firefoxPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
		private string eclipsePath = @"F:\eclipse3.5.1EE\eclipse\eclipse.exe";
		private string iePath = @"C:\Program Files\Internet Explorer\iexplore.exe";
		private string cmdPath = @"C:\Windows\system32\cmd.exe";
		private string explorerPath = @"C:\Windows\explorer.exe";
		private string nextFTPPath = @"C:\Program Files\NextFTP\NEXTFTP.EXE";//@"C:\Program Files\NextFTP\NEXTFTP.EXE";
    //private string homeMenuPath = @"F:\VCSharp\Flashdevelop5.1.1-LL\FlashDevelop\Bin\Debug\SettingData\XmlTreeMenu.xml";
    private string homeMenuPath = @"F:\VCSharp\Flashdevelop5.1.1-LL\FlashDevelop\Bin\Debug\SettingData\FDTreeMenu.xml";
 
    private Font fTPClientDefaultFont = new Font("Meiryo UI", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 128);


    [DisplayName("Path to Ant")]
		[Description("Path to Ant installation dir")]
		[DefaultValue(DEFAULT_ANT_PATH)]
		[Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
		public String AntPath
		{
			get { return antPath; }
			set { antPath = value; }
		}

		[DisplayName("Additional Arguments")]
		[Description("More parameters to add to the Ant call (e.g. -inputhandler <classname>)")]
		[DefaultValue(DEFAULT_ANT_PATH)]
		public String AddArgs
		{
			get { return addArgs; }
			set { addArgs = value; }
		}

		[DisplayName("AntPanel Font")]
		[Description("AntPanel Default Font")]
		[DefaultValue(typeof(Font), "Meiryo UI, 12pt")]
		public Font AntPanelDefaultFont
		{
			get
			{
				return this.antPanelDefaultFont;
			}
			set
			{
				this.antPanelDefaultFont = value;
			}
		}

		[Category("�t�@�C�����"), DisplayName("�t�@�C����Ԃ̕ۑ��ƕ���"), Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public List<string> FileStates
		{
			get
			{
				return this.fileStates;
			}
			set
			{
				this.fileStates = value;
			}
		}

		[Category("�t�@�C�����"), DefaultValue(false), DisplayName("�J�X�^���Z�b�V�����̕���")]
		public bool RestoreCuctomSession
		{
			get
			{
				return this.restoreCuctomSession;
			}
			set
			{
				this.restoreCuctomSession = value;
			}
		}

		[Category("�t�@�C�����"), DisplayName("�J�X�^���Z�b�V�����f�[�^"), Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public List<string> CustomSessionData
		{
			get
			{
				return this.customSessionData;
			}
			set
			{
				this.customSessionData = value;
			}
		}

		[Category("�t�@�C�����"), DisplayName("�ŋߊJ�����J�X�^���h�L�������g"), Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public List<string> PreviousCustomDocuments
		{
			get
			{
				return this.previousCustomDocuments;
			}
			set
			{
				this.previousCustomDocuments = value;
			}
		}

		[Category("�A�v���P�[�V���� �p�X"), DefaultValue("http://localhost"), DisplayName("�T�[�o�[���[�g")]
		public string ServerRoot
		{
			get
			{
				return this.serverRoot;
			}
			set
			{
				this.serverRoot = value;
			}
		}

		[Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"C:\Apache2.2\htdocs"), DisplayName("�h�L�������g���[�g")]
		public string DocumentRoot
		{
			get
			{
				return this.documentRoot;
			}
			set
			{
				this.documentRoot = value;
			}
		}

		//private string sakuraPath = @"C:\TiuDevTools\sakura\sakura.exe";
		[Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"C:\TiuDevTools\sakura\sakura.exe"), DisplayName("�T�N���G�f�B�^")]
		public string SakuraPath
		{
			get
			{
				return this.sakuraPath;
			}
			set
			{
				this.sakuraPath = value;
			}
		}
		//private string papadPath = @"F:\Programs\PSPad editor\PSPad.exe";
		[Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"F:\Programs\PSPad editor\PSPad.exe"), DisplayName("PSPad Editor")]
		public string PspadPath
		{
			get
			{
				return this.pspadPath;
			}
			set
			{
				this.pspadPath = value;
			}
		}
		//private string chromePath = @"C:\Documents and Settings\kazuhiko\Local Settings\Application Data\Google\Chrome\Application\chrome.exe";
		[Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"C:\Documents and Settings\kazuhiko\Local Settings\Application Data\Google\Chrome\Application\chrome.exe"), DisplayName("Gooogle Chrome")]
		public string ChromePath
		{
			get
			{
				return this.chromePath;
			}
			set
			{
				this.chromePath = value;
			}
		}
		//private string firefoxPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
		[Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"C:\Program Files\Mozilla Firefox\firefox.exe"), DisplayName("Mozilla Firefox")]
		public string FirefoxPath
		{
			get
			{
				return this.firefoxPath;
			}
			set
			{
				this.firefoxPath = value;
			}
		}
		//private string eclipsePath = @"F:\eclipse3.5.1EE\eclipse\eclipse.exe";
		[Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"F:\eclipse3.5.1EE\eclipse\eclipse.exe"), DisplayName("Eclipse")]
		public string EclipsePath
		{
			get
			{
				return this.eclipsePath;
			}
			set
			{
				this.eclipsePath = value;
			}
		}
		//private string iePath = @"C:\Program Files\Internet Explorer\iexplore.exe";
		[Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"C:\Program Files\Internet Explorer\iexplore.exe"), DisplayName("IE")]
		public string IePath
		{
			get
			{
				return this.iePath;
			}
			set
			{
				this.iePath = value;
			}
		}
		//private string cmdPath = @"C:\Windows\system32\cmd.exe";
		[Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"C:\Windows\system32\cmd.exe"), DisplayName("�R�}���h�E�v�����v�g")]
		public string CmdPath
		{
			get
			{
				return this.cmdPath;
			}
			set
			{
				this.cmdPath = value;
			}
		}
		//private string explorerPath = @"C:\Windows\explorer.exe";
		[Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"C:\Windows\explorer.exe"), DisplayName("�G�N�X�v���[��")]
		public string ExplorerPath
		{
			get
			{
				return this.explorerPath;
			}
			set
			{
				this.explorerPath = value;
			}
		}
    //private string nextFTPPath = @"C:\Program Files\NextFTP\NEXTFTP.EXE";


    [Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"C:\Program Files\NextFTP\NEXTFTP.EXE"), DisplayName("NextFTP")]
		public string NextFTPPath
		{
			get
			{
				return this.nextFTPPath;
			}
			set
			{
				this.nextFTPPath = value;
			}
		}

    [Category("�A�v���P�[�V���� �p�X"), DefaultValue(@"F:\VCSharp\Flashdevelop5.1.1-LL\FlashDevelop\Bin\Debug\SettingData\FDTreeMenu.xml"), DisplayName("HomeMenuPath")]
    public string HomeMenuPath
    {
      get
      {
        return this.homeMenuPath;
      }
      set
      {
        this.homeMenuPath = value;
      }
    }

    [DisplayName("FTPClient Font")]
    [Description("FTPClient Default Font")]
    [DefaultValue(typeof(Font), "Meiryo UI, 12pt")]
    public Font FTPClientDefaultFont
    {
      get
      {
        return this.fTPClientDefaultFont;
      }
      set
      {
        this.fTPClientDefaultFont = value;
      }
    }



  }
}