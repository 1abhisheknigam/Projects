using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//Author: Abhishek Nigam
namespace ProjectGemini
{
    static class Controller
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        private static CPU CPU;
        private static GUI GUI;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CPU = new CPU();
            GUI = new GUI(CPU);
            Application.Run(GUI);
            
        }
    }
}
