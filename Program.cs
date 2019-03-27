using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntPanelApplication
{
  static class Program
  {
    /// <summary>
    /// アプリケーションのメイン エントリ ポイントです。
    /// </summary>
    [STAThread]
    static void Main(String[] args)
    {
      MainForm mainForm = new MainForm();
      Application.EnableVisualStyles();

      // 例外発生
      // https://dobon.net/vb/dotnet/control/setcompatibletextrenderingdefault.html
      // 特定のコントロールで定義された UseCompatibleTextRendering プロパティに
      //アプリケーション全体で有効な既定値を設定します。
      //Application.SetCompatibleTextRenderingDefault(false);
      MainForm.Arguments = args;
      Application.Run(mainForm);
     }
  }
}
