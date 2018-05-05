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
      /*
       public static void Main(string[] args)
      {
        for (int i = 0; i < args.Length; ++i)
          Console.Write("{0}番目のコマンドライン引数は{1}です。\n", i, args[i]);
      }
      */
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      if (args.Length>0)
      {
        //String projectPath = args[0];
        //Application.Run(new Form1(projectPath));
        Application.Run(new Form1(args));
      }
      else Application.Run(new Form1());
    }
  }
}
