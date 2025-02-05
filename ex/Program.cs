using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ex.GUI;

namespace ex
{
    internal static class Program
    {
        public static int uid = 0;
        public static int LeaderID = 0;
        public static int adminID = 0;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI_LOGIN());
        }
    }
}
