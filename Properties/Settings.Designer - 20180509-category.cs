﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;

namespace AntPanelApplication.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AntPanel")]
        public string Name {
            get {
                return ((string)(this["Name"]));
            }
            set {
                this["Name"] = value;
            }
        }

        [Category("表示")]
        [DisplayName("MenuBarVisible")]
        [Description("MenuBarVisible")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool MenuBarVisible {
            get {
                return ((bool)(this["MenuBarVisible"]));
            }
            set {
                this["MenuBarVisible"] = value;
            }
        }

        [Category("表示")]
        [DisplayName("ToolBarVisible")]
        [Description("ToolBarVisible")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ToolBarVisible {
            get {
                return ((bool)(this["ToolBarVisible"]));
            }
            set {
                this["ToolBarVisible"] = value;
            }
        }

        [Category("表示")]
        [DisplayName("StatusBarVisible")]
        [Description("StatusBarVisible")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool StatusBarVisible {
            get {
                return ((bool)(this["StatusBarVisible"]));
            }
            set {
                this["StatusBarVisible"] = value;
            }
        }

        [Category("ファイル状態")]
        [DisplayName("BookMarks")]
        [Description("BookMarks")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>F:\My Music\２つのミサ曲\２つのミサ曲.asx</string>
  <string>C:\home\KingFM.asx</string>
  <string>F:\My Video\flv\misaki.flv</string>
  <string>F:\My Music\KingFM\070104\MARTIN-Mass.wax</string>
  <string>F:\home\xjpeg\ePXXXFd.wmv</string>
  <string>http://v6.player.abacast.net/3</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection BookMarks {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["BookMarks"]));
            }
            set {
                this["BookMarks"] = value;
            }
        }

        [Category("ファイル状態")]
        [DisplayName("PreviousDocuments")]
        [Description("PreviousDocuments")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>F:\My Music\KingFM\061122\MOZART-Horn_Concerto_No.2_in_E-flat_K.417.wax</string>
  <string>F:\My Music\２つのミサ曲\２つのミサ曲.asx</string>
  <string>C:\home\KingFM.asx</string>
  <string>F:\My Video\flv\misaki.flv</string>
  <string>F:\My Music\KingFM\070104\MARTIN-Mass.wax</string>
  <string>F:\My Music\KingFM\060804\MOZART-Piano_Concerto_No.13_in_C_K.415_387b.wax</string>
  <string>F:\My Music\KingFM\061030\MOZART-Piano_Concerto_No.12_in_A_K.414.wax</string>
  <string>F:\My Music\KingFM\060805\IBERT-Escales(Ports_of_Call).wax</string>
  <string>F:/My Music/KingFM/070108/MOZART-Piano_Concerto_No.1_in_F,_K.37.wax</string>
  <string>F:\home\xjpeg\ePXXXFd.wmv</string>
  <string>F:\My Music\KingFM\070107\IRELAND-Orchestral_Poem_in_A_minor.wax</string>
  <string>F:\My Music\KingFM\070112\KETCLBY-Bells_Across_the_Meadow.wax</string>
  <string>F:\My Music\KingFM\070115\HEINICHEN-Chamber_Sonata_in_A,_S.208.wax</string>
  <string>F:\My Music\KingFM\060810\GEMINIANI-Concerto_Grosso_in_B-flat_Op.5_2.wax</string>
  <string>F:\My Music\KingFM\070117\FALLA-Love,_the_Magician-Ritual_Fire_Dance.wax</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection PreviousDocuments {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["PreviousDocuments"]));
            }
            set {
                this["PreviousDocuments"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("ServerRoot")]
        [Description("ServerRoot")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost")]
        public string ServerRoot {
            get {
                return ((string)(this["ServerRoot"]));
            }
            set {
                this["ServerRoot"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("DocumentRoot")]
        [Description("DocumentRoot")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Apache2.2\\htdocs")]
        public string DocumentRoot {
            get {
                return ((string)(this["DocumentRoot"]));
            }
            set {
                this["DocumentRoot"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("SakuraPath")]
        [Description("SakuraPath")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files (x86)\\sakura\\sakura.exe")]
        public string SakuraPath {
            get {
                return ((string)(this["SakuraPath"]));
            }
            set {
                this["SakuraPath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("PSPadPath")]
        [Description("PSPadPath")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files (x86)\\PSPad editor\\PSPad.exe")]
        public string PspadPath {
            get {
                return ((string)(this["PspadPath"]));
            }
            set {
                this["PspadPath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("FlashdevelopPath")]
        [Description("AFlashdevelop 5.2.0 Path")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("F:\\VCSharp\\Flashdevelop5.1.1-LL\\FlashDevelop\\Bin\\Debug\\FlashDevelop.exe")]
        public string FlashdevelopPath {
            get {
                return ((string)(this["FlashdevelopPath"]));
            }
            set {
                this["FlashdevelopPath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("Devenv15Path")]
        [Description("Devenv15Path")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\IDE\\devenv.exe")]
        public string Devenv15Path {
            get {
                return ((string)(this["Devenv15Path"]));
            }
            set {
                this["Devenv15Path"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("Devenv17Path")]
        [Description("Devenv17Path")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\Common7\\IDE\\devenv." +
            "exe")]
        public string Devenv17Path {
            get {
                return ((string)(this["Devenv17Path"]));
            }
            set {
                this["Devenv17Path"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("MediaPlayerPath")]
        [Description("MediaPlayerPath")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files\\Windows Media Player\\wmplayer.exe")]
        public string MediaPlayerPath {
            get {
                return ((string)(this["MediaPlayerPath"]));
            }
            set {
                this["MediaPlayerPath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("VlcPath")]
        [Description("VlcPath")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files\\VideoLAN\\VLC\\vlc.exe")]
        public string VlcPath {
            get {
                return ((string)(this["VlcPath"]));
            }
            set {
                this["VlcPath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("AntExecutablePath")]
        [Description("AntExecutablePath")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("F:\\ant\\apache-ant-1.10.1\\bin\\ant.bat")]
        public string AntExecutablePath {
            get {
                return ((string)(this["AntExecutablePath"]));
            }
            set {
                this["AntExecutablePath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("GradlePath")]
        [Description("gradlePath")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("F:\\gradle-3.5\\bin\\gradle.bat")]
        public string GradlePath {
            get {
                return ((string)(this["GradlePath"]));
            }
            set {
                this["GradlePath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("ChromePath")]
        [Description("ChromePath")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe")]
        public string ChromePath {
            get {
                return ((string)(this["ChromePath"]));
            }
            set {
                this["ChromePath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("IePath")]
        [Description("IePath")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files\\Internet Explorer\\iexplore.exe")]
        public string IePath {
            get {
                return ((string)(this["IePath"]));
            }
            set {
                this["IePath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("CmdPath")]
        [Description("CmdPath")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Windows\\system32\\cmd.exe")]
        public string CmdPath {
            get {
                return ((string)(this["CmdPath"]));
            }
            set {
                this["CmdPath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("ExplorerPath")]
        [Description("ExplorerPath")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Windows\\explorer.exe")]
        public string ExplorerPath {
            get {
                return ((string)(this["ExplorerPath"]));
            }
            set {
                this["ExplorerPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ＭＳ ゴシック, 16.2pt")]
        public global::System.Drawing.Font DefaultFont {
            get {
                return ((global::System.Drawing.Font)(this["DefaultFont"]));
            }
            set {
                this["DefaultFont"] = value;
            }
        }

        [Category("表示")]
        [DisplayName("MenuStripItemEnabled")]
        [Description("メニューアイテムの表示状態")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>101000011011</string>
  <string>101000011011</string>
  <string>101000111111</string>
  <string>111111011011</string>
  <string>101000011011</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection MenuStripItemEnabled {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["MenuStripItemEnabled"]));
            }
            set {
                this["MenuStripItemEnabled"] = value;
            }
        }


    [Category("表示")]
    [DisplayName("FileMenuVisible")]
    [Description("FileMenuVisible")]
    [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>01111101000100110</string>
  <string>01110100000100110</string>
  <string>01111101000111110</string>
  <string>11111111111111110</string>
  <string>01111100000111110</string>
  <string>00000000000000000</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection FileMenuVisible {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["FileMenuVisible"]));
            }
            set {
                this["FileMenuVisible"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("AntPath")]
        [Description("Antのディレクトリ")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("F:\\ant\\apache-ant-1.10.1")]
        public string AntPath {
            get {
                return ((string)(this["AntPath"]));
            }
            set {
                this["AntPath"] = value;
            }
        }

        [Category("Options")]
        [DisplayName("StorageFileName")]
        [Description("StorageFileName AntTreeの保存データファイル")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("antPluginData.txt")]
        public string StorageFileName {
            get {
                return ((string)(this["StorageFileName"]));
            }
            set {
                this["StorageFileName"] = value;
            }
        }

        [Category("Options")]
        [DisplayName("HomeMenuPath")]
        [Description("HomeMenuのパスを設定します")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("F:\\VCSharp\\Flashdevelop5.1.1-LL\\FlashDevelop\\Bin\\Debug\\SettingData\\FDTreeMenu.xml" +
            "")]
        public string HomeMenuPath {
            get {
                return ((string)(this["HomeMenuPath"]));
            }
            set {
                this["HomeMenuPath"] = value;
            }
        }

        [Category("アプリケーションパス")]
        [DisplayName("GitPath")]
        [Description("Git.exeのパスを設定します")]
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files\\Git\\git-bash.exe")]
        public string GitPath {
            get {
                return ((string)(this["GitPath"]));
            }
            set {
                this["GitPath"] = value;
            }
        }
    }
}