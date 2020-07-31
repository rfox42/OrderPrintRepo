using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;

namespace MeridianFtpService
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
                new MeridianFtpService()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch(Exception ex)
            {
                writeToFile(ex.StackTrace + " " + ex.Message);
                SendEmail("ryan@ranshu.com", "URGENT FAILURE - FTP Service Error: " + ex.Message, ex.StackTrace);
            }
        }


        public static void writeToFile(string message)
        {
            //open/create log directory
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Year;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //open/create log file
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Year + "\\FTP_Service_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    //write message to file
                    sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + message);
                }
            }
            else
            {

                using (StreamWriter sw = File.AppendText(filePath))
                {
                    //write message to file
                    sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + message);
                }
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
        static void SendEmail(string recipient, string subject, string msgText, List<string> cc = null)
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
