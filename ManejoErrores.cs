using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSFTP
{
    internal class ManejoErrores
    {
        const string Path = "ErrorLog.txt";
        const string MensajeError = "Message: {0}<br />{1}StackTrace :{2}{1}Date :{3}{1}-----------------------------------------------------------------------------{1}";

        public static void LoguearError(Exception ex, string mensajeConsola)
        {
            var mensaje = string.Format(MensajeError, ex.Message, Environment.NewLine, ex.StackTrace, DateTime.Now.ToString());

            Console.WriteLine(mensajeConsola);
            using (StreamWriter sw = new StreamWriter(Path, true))
            {
                sw.Write(mensaje);
            }
        }

    }
}
