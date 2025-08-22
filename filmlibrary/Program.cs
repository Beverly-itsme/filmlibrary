using System;
using System.Windows.Forms;
using FilmLibrary.Forms; 

namespace FilmLibrary
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm()); 
        }
    }
}
