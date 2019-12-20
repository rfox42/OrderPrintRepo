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

    /* 
     * @CLASS:      static class Program
     * @PURPOSE:    starts and ends the windows service
     *              handles startup errors and critical errors
     * 
     * @NOTES: none
     */
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //default printer found property
            bool printerReady;

            try
            {
                //find/set default printer
                printerReady = printers.SetDefaultPrinter("RICOHNV");

                //if printer set
                if(printerReady)
                {
                    //start the service
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                    {
                        new OrderPrintService()
                    };
                    ServiceBase.Run(ServicesToRun);
                }
                else
                {
                    //throw error
                    throw new Exception("ERROR - Please install printers to use the Ranshu Print Service");
                }

            }
            catch(Exception ex)
            {
                //print exception to log
                writeToFile("EXCEPTION - " + ex.Message);
            }
        }

        /*
         * @FUNCTION:   public static void writeToFile()
         * @PURPOSE:    writes to log file
         *              
         * @PARAM:      string message
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        public static void writeToFile(string message)
        {
            //open/create "Logs"
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //open/create daily log file
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Service_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    //write message to log
                    sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    //write message to log
                    sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + message);
                }
            }
        }
    }
}
