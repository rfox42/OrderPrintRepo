using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace RanshuPrintService
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            bool printerReady;

            try
            {
                printerReady = printers.SetDefaultPrinter("RICOHNV");

                if(printerReady)
                {
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                    {
                new OrderPrintService()
                    };
                    ServiceBase.Run(ServicesToRun);
                }
                else
                {
                    throw new Exception("ERROR - Please install printers to use the Rnashu Print Service");
                }

            }
            catch(Exception ex)
            {
                writeToFile("EXCEPTION - " + ex.Message);
            }
        }


        public static void writeToFile(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Service_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + message);
                }
            }
            else
            {

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + message);
                }
            }
        }
    }
}
