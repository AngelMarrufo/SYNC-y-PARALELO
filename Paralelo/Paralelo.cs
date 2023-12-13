using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Paralelo{
    internal class Paralelo{
        static void Main(string[] args){
            Console.WriteLine("Ingrese la ruta completa de un archivo para hacer los procesos en paralelo:");
            string filePath = Console.ReadLine();

            if (Directory.Exists(filePath)){

                FileInfo fileInfo = new FileInfo(filePath);

                Stopwatch stopwatch = Stopwatch.StartNew();

                long totalSizeInBytes = ObtenerPesoArchivosEnBytes(filePath);

                stopwatch.Stop();

                Console.WriteLine($"El peso total de los archivos es: {totalSizeInBytes} bytes");
                Console.WriteLine($"Proceso completado en {stopwatch.Elapsed.TotalSeconds} segundos.");
            }
            else{
                Console.WriteLine("No existe La carpeta");
            }

            Console.ReadLine();
        }

        static long ObtenerPesoArchivosEnBytes(string carpeta){

            long totalSizeInBytes = 0;

            try{

                string[] archivos = Directory.GetFiles(carpeta);

                Parallel.ForEach(archivos, archivo =>{
                    FileInfo fileInfo = new FileInfo(archivo);
                    Interlocked.Add(ref totalSizeInBytes, fileInfo.Length);
                });
            }
            catch (Exception ex){

                Console.WriteLine($"Error al obtener el peso de los archivos: {ex.Message}");
            }

            return totalSizeInBytes;
        }
    }
}

