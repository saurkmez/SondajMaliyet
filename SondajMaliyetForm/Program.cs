using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SondajMaliyetClass.DB;
using SondajMaliyetForm.View;

namespace SondajMaliyetForm
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CreateDb create = new CreateDb();
            create.CreateDB();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            splashFrm splash = new splashFrm();
            splash.Show();
            Form form = new Anasayfa(splash);
            Application.Run(form);    
        }
    }
}
