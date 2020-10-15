using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Services
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (Directory.Exists("C:\\services") == false)
            {
                Directory.CreateDirectory("C:\\services");
            }

            if (File.Exists("C:\\services\\servicesDB.s3db") == false)
            {
                new SQLite().createDataBase();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
