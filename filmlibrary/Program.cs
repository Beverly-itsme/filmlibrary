using System;
using System.Windows.Forms;
using FilmLibrary.Forms; // namespace where LoginForm lives

namespace FilmLibrary
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm()); // instead of Form1
        }
    }
}
