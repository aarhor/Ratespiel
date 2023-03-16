using System;
using System.Windows.Forms;

namespace Spielsücht
{
    internal sealed class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Ratespiel.Properties.Settings.Default.Hinweis_OK == false)
                Application.Run(new Hinweis());
            else
                Application.Run(new Auswahl());
        }
    }
}