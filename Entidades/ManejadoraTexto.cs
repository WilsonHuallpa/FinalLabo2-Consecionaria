using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class ManejadoraTexto
    {
        
        public static bool EscribirArchivo(List<Auto> lista)
        {
            string ruta = "Autos.log";
            try
            {
                using (StreamWriter sw = new StreamWriter(ruta, true))
                {
                    sw.WriteLine($"Color {lista[0].Color}");
                    foreach (Auto c in lista)
                    {
                        sw.WriteLine(c.ToString());
                    }
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Leer(string archivo, out string datos)
        {
            try
            {
                datos = String.Empty;

                if (File.Exists(archivo))
                {
                    using (StreamReader sr = new StreamReader(archivo))
                    {
                        datos = sr.ReadToEnd();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw new Exception("Error al intentar leer archivo de texto");
            }
        }
    }
}
