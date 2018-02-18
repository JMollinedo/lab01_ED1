using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ListasArtesanales
{
    public class LogWriter
    {
        private string nombreLog;
        public LogWriter()
        {
            nombreLog = "log-LAB01_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
        }

        public void WriteLog(string mensaje, TimeSpan tiempo)
        {
            StreamWriter escritor = writerTipe();
            escritor.Write("\r\n");
            escritor.WriteLine(DateTime.Now.ToString("LOG yyyy/MM/dd HH:mm:ss"));
            escritor.WriteLine("Evento: " + mensaje);
            escritor.WriteLine("Duracion: " + decimal.Round(Convert.ToDecimal(tiempo.TotalMilliseconds), 3) + "ms");
            escritor.WriteLine("---- LOG END ----");
            escritor.Close();
        }

        public void WriteLog(string mensaje, TimeSpan tiempo, Exception e)
        {
            StreamWriter escritor = writerTipe();
            escritor.WriteLine(DateTime.Now.ToString("LOG yyyy/MM/dd HH:mm:ss"));
            escritor.WriteLine("Evento: " + mensaje);
            escritor.WriteLine("Excepcion: " + e.Message);
            escritor.WriteLine("Duracion: " + decimal.Round(Convert.ToDecimal(tiempo.TotalMilliseconds)) + "ms", 3);
            escritor.WriteLine("---- LOG END ----");
            escritor.Close();
        }

        private StreamWriter writerTipe()
        {
            try
            {
                return File.AppendText(HttpContext.Current.Server.MapPath("~\\log\\" + nombreLog));
            }
            catch
            {
                string ruta = "C:\\Users\\" + Environment.UserName + "\\" + nombreLog;
                return new StreamWriter(ruta);
            }
        }
    }
}
