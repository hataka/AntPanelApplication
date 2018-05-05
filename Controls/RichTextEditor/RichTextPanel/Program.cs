using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
      /// <summary>
      /// アプリケーションのメイン エントリ ポイントです。
      /// </summary>
      [STAThread]
      static void Main(string[] args)
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        RichTextPanel form;
       if (args.Length == 0) form = new RichTextPanel();
        else form = new RichTextPanel(args);
      	//form.ClientSize = new Size(800, 600);
        //form.StartPosition = FormStartPosition.CenterScreen;
        //Application.Run(form);
        
       
        form.Dock = DockStyle.Fill;
        Form app = new Form();
        app.Controls.Add(form);
        app.ClientSize = new Size(800, 600);
        app.StartPosition = FormStartPosition.CenterScreen;
        Application.Run(app);
     
      
      }
      
      
  }
}
