using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Microsoft.Extensions.Configuration;
using System.IO;
using static System.Net.WebRequestMethods;

namespace ProyectoSFTP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConfiguracionSFTP configuracion = ConfiguracionSFTP.ObtenerInstancia();
                SftpClient client = new SftpClient(configuracion.Host, configuracion.Puerto, configuracion.Usuario, configuracion.Contrasenia);
                client.Connect();
                TransferenciaArchivos.SubirArchivo(configuracion, client);
                TransferenciaArchivos.DescargarArchivos(configuracion, client);
                client.Disconnect();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                ManejoErrores.LoguearError(ex, "Error al conectarse");
            }
        }
    }
}
