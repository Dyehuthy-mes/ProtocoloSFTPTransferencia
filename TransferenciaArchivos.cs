using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSFTP
{
    internal class TransferenciaArchivos
    {
        const string ExtensionArchivoSubida = "*.xml";   //Solamente indicar que extension de archivo se desea transferir
        const string ExtensionArchivoDescarga = ".xml";

        public static void SubirArchivo(ConfiguracionSFTP configuracion, SftpClient client)
        {
            string[] archivos = new string[] { };

            try
            {
                archivos = Directory.GetFiles(configuracion.CarpetaLocalSubida, ExtensionArchivoSubida);
                client.ChangeDirectory(configuracion.CarpetaRemotaSubida);
            }
            catch (Exception ex)
            {
                ManejoErrores.LoguearError(ex, "Carpeta No Encontrada");
            }

            foreach (string archivo in archivos)
            {
                try
                {

                    using (var fileStream = new FileStream(archivo, FileMode.Open))
                    {
                        client.UploadFile(fileStream, Path.GetFileName(archivo));
                        Console.WriteLine($"Archivo subido: {archivo}");
                    }
                }
                catch (Exception ex)
                {
                    ManejoErrores.LoguearError(ex, $"Archivo perdido: {archivo}");
                }
            }
        }


        public static void DescargarArchivos(ConfiguracionSFTP configuracion, SftpClient client)
        {
            List<SftpFile> archivos = new List<SftpFile>();

            try
            {
                client.ChangeDirectory(configuracion.CarpetaRemotaDescarga);
                archivos = client.ListDirectory(configuracion.CarpetaRemotaDescarga)
                                     .Where(f => !f.IsDirectory && f.Name.EndsWith(ExtensionArchivoDescarga))
                                    .ToList();
            }
            catch (Exception ex)
            {
                ManejoErrores.LoguearError(ex, "Carpeta No Encontrada");
            }

            foreach (var archivoRemoto in archivos)
            {
                try
                {
                    string nombreArchivo = archivoRemoto.Name;
                    string rutaArchivoLocal = Path.Combine(configuracion.CarpetaLocalDescarga, nombreArchivo);

                    using (var fileStream = System.IO.File.Create(rutaArchivoLocal))
                    {
                        client.DownloadFile(archivoRemoto.Name, fileStream);
                        Console.WriteLine($"Archivo descargado: {archivoRemoto}");
                    }
                }
                catch (Exception ex)
                {
                    ManejoErrores.LoguearError(ex, $"Archivo perdido: {archivoRemoto}");
                }
            }
        }
    }
}
