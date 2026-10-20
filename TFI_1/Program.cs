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

            // Los errores no controlados se muestran con el mismo formato que el
            // resto de la aplicación y se registran en %LocalAppData%\Peluqueria\errores.log.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => Mensajes.ErrorGlobal(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    Mensajes.ErrorGlobal(ex);
                }
            };

            try
            {
                Application.Run(new FormLog());
            }
            catch (Exception ex)
            {
                Mensajes.ErrorGlobal(ex);
            }
        }
    }
}
