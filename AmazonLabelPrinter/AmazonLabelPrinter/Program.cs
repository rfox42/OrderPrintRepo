using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AmazonLabelPrinter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        /// <summary>
        /// writes message to log file
        /// </summary>
        /// <param name="message">
        /// message to write to log
        /// </param>
        public static void writeToFile(string message)
        {
            ///open/create log directory
            string path = @"\\RANSHUDC2\users_data\_Information Technology\Dev Csharp\Ranshu Projects\RFox\App builds\AmazonLabelPrinting\Logs\" + DateTime.Now.Year;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            ///open/create log file
            string filePath = @"\\RANSHUDC2\users_data\_Information Technology\Dev Csharp\Ranshu Projects\RFox\App builds\AmazonLabelPrinting\Logs\" + DateTime.Now.Year + "\\AmazonPrintLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    ///write message to file
                    sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + message);
                }
            }
            else
            {

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    ///write message to file
                    sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + message);
                }
            }
        }
    }
}
