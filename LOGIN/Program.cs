<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


=======
>>>>>>> 7d0825fb45b5e008fd08699a04400a342044acee
namespace LOGIN
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
<<<<<<< HEAD
            //ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forget_Pass());
=======
            ApplicationConfiguration.Initialize();
            Application.Run(new Reset_Pass());
>>>>>>> 7d0825fb45b5e008fd08699a04400a342044acee
        }
    }
}