using System;
using System.Windows.Forms;
using TFI_1.Presentacion;

namespace TFI_1
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
            ApplicationConfiguration.Initialize();
            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show($"Error no controlado: {e.Exception.GetType().Name}\n{e.Exception.Message}\n\n{e.Exception.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.Run(new FormLog());
        }
    }
}