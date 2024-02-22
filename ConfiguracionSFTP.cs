using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace ProyectoSFTP
{
    public class ConfiguracionSFTP
    {
        public string CarpetaLocalSubida { get; }
        public string CarpetaRemotaSubida { get; }
        public string CarpetaLocalDescarga { get; }
        public string CarpetaRemotaDescarga { get; }
        public string Host { get; }
        public int Puerto { get; }
        public string Usuario { get; }
        public string Contrasenia { get; }

        private static ConfiguracionSFTP instancia;

        public ConfiguracionSFTP(string carpetaLocalSubida, string carpetaRemotaSubida, string carpetaLocalDescarga, string carpetaRemotaDescarga, string host, int puerto, string usuario, string contrasenia)
        {
            CarpetaLocalSubida = carpetaLocalSubida;
            CarpetaRemotaSubida = carpetaRemotaSubida;
            CarpetaLocalDescarga = carpetaLocalDescarga;
            CarpetaRemotaDescarga = carpetaRemotaDescarga;
            Host = host;
            Puerto = puerto;
            Usuario = usuario;
            Contrasenia = contrasenia;
        }

        public static ConfiguracionSFTP ObtenerInstancia()
        {
            if (instancia == null)
            {
                    string carpetaLocalSubida = ConfigurationManager.AppSettings["CarpetaLocalSubida"];
                    string carpetaRemotaSubida = ConfigurationManager.AppSettings["CarpetaRemotaSubida"];
                    string carpetaLocalDescarga = ConfigurationManager.AppSettings["CarpetaLocalDescarga"];
                    string carpetaRemotaDescarga = ConfigurationManager.AppSettings["CarpetaRemotaDescarga"];
                    string host = ConfigurationManager.AppSettings["Host"];
                    int puerto = int.Parse(ConfigurationManager.AppSettings["Puerto"]);
                    string usuario = ConfigurationManager.AppSettings["Usuario"];
                    string contrasenia = ConfigurationManager.AppSettings["Contrasenia"];

                    instancia = new ConfiguracionSFTP(carpetaLocalSubida, carpetaRemotaSubida, carpetaLocalDescarga, carpetaRemotaDescarga, host, puerto, usuario, contrasenia);    
            }

            return instancia;
        }
    }
}
