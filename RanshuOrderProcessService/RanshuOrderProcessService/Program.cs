using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.IO;
using System.Threading.Tasks;

namespace RanshuOrderProcessService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            try
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new ServiceMain()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch(Exception ex)
            {
                writeToFile(ex.StackTrace + ex.Message);
                SendEmail("ryan@ranshu.com", "URGENT FAILURE: PO Process Service. ERROR: " + ex.Message, ex.StackTrace + " " + ex.Message);
            }
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
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Year;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            ///open/create log file
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Year + "\\RanshuOrderService_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
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


        /// <summary>
        /// removes "END SET" lines from the log file
        /// </summary>
        public static void condenseFile()
        {
            ///open log directory or exit if not present
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Year;
            if (!Directory.Exists(path))
            {
                return;
            }

            ///open log file or exit if not present
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Year + "\\RanshuOrderService_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filePath))
            {
                return;
            }
            else
            {
                ///read file to list
                List<string> rawFile = File.ReadAllLines(filePath).ToList<string>();

                ///for each log line
                for (int i = 0; i < rawFile.Count; i++)
                {
                    ///if line contatins "END SET"
                    if (rawFile[i].Contains("END SET"))
                    {
                        ///remove line
                        rawFile.RemoveAt(i);
                        i--;
                    }
                }

                ///rewrite file
                File.WriteAllLines(filePath, rawFile);
            }
        }

        /// <summary>
        /// create and send error emails
        /// </summary>
        /// <param name="subject">
        /// email subject
        /// </param>
        /// <param name="msgText">
        /// email text
        /// </param>
        /// <param name="cc">
        /// email cc list
        /// </param>
        public static void SendEmail(string recipient, string subject, string msgText, List<string> cc = null)
        {
            ///set email credentials
            SmtpClient mailClient = new SmtpClient("secure.emailsrvr.com");
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = new System.Net.NetworkCredential("orders@ranshu.com", "%Ranshu525252");
            mailClient.Port = 587;
            mailClient.EnableSsl = true;

            ///create message
            MailMessage msgMail;
            msgMail = new MailMessage(new MailAddress("orders@ranshu.com"), new MailAddress(recipient));
            if (cc != null)
            {
                foreach (string address in cc)
                {
                    msgMail.CC.Add(new MailAddress(address));
                }
            }
            msgMail.Subject = subject;
            msgMail.Body = msgText;
            msgMail.IsBodyHtml = true;

            ///send message
            try
            {
                mailClient.Send(msgMail);
            }
            catch
            {
                mailClient.Port = 465;
                mailClient.Send(msgMail);
            }

            ///garbage collect
            msgMail.Dispose();
        }
    }
}
