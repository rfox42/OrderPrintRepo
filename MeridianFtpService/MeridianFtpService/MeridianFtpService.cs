using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net;
using System.IO;
using System.Net.Mail;
using WinSCP;

namespace MeridianFtpService
{
    public partial class MeridianFtpService : ServiceBase
    {
        static bool toggle = false;
        static bool stock = true;
        static bool stockExp = true;
        static Timer timer;

        static WebClient ftpClient = null;

        static FtpWebRequest ftpRequest = null;
        static FtpWebResponse ftpResponse = null;

        static FtpWebRequest fileRequest = null;
        static FtpWebRequest moveRequest = null;
        static FtpWebResponse moveResponse = null;

        static int intFileCount = 0;

        static bool containsAll(string str, List<string> conditions)
        {
            foreach(string con in conditions)
            {
                if (!str.Contains(con))
                    return false;
            }
            return true;
        }

        static void moveStocks(string site, string login, string pass, string key, string path, List<string> conditions = null)
        {
            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = site,
                    UserName = login,
                    Password = pass,
                    SshHostKeyFingerprint = key
                };

                //create session
                using (Session session = new Session())
                {
                    //open session connection
                    session.Open(sessionOptions);

                    //get file names in path
                    List<string> files = Directory.GetFiles(path).ToList();

                    //for each file
                    foreach(string file in files)
                    {
                        //check for conditions
                        if (containsAll(file, conditions))//put file in sftp
                            writeToFile("EXP Stock-Move - "+ session.PutFiles(file, "/inbound/", true, null).IsSuccess);
                    }

                    //close session
                    session.Close();
                }
            }//catch exceptions
            catch(Exception ex)
            {
                //report errors
                writeToFile(ex.Message);
            }
        }

        static void moveFiles1800SFTP(string sftpSite, string sftpUser, string sftpPass, string destFolder)
        {


            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = sftpSite,
                    UserName = sftpUser,
                    Password = sftpPass,
                    SshHostKeyFingerprint = "ssh-ed25519 255 uLtkkJGqmFDqYoN20V5zYwT1FW8UctF6fy5FBQk90ck="
                };

                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);

                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    //check for stock upload
                    if (DateTime.Now > DateTime.Today.AddHours(19.10) && stock)
                    {
                        //move stocks
                        writeToFile("Invoice-Move - " + session.PutFiles(@"\\RANSHU\Reports\edi\outgoing\RADEXPRESS\invoice\*.inv", "/inbound/Invoice/", true, null).IsSuccess);
                        writeToFile("Stock-Move - " + session.PutFiles(@"\\Ranshu\Reports\edi\outgoing\RADEXPRESS\stock\*.csv", "/inbound/", true, null).IsSuccess);
                        writeToFile("ASN-Move - " + session.PutFiles(@"\\Ranshu\Reports\edi\outgoing\RADEXPRESS\asn\*.asn", "/inbound/ASN/", true, null).IsSuccess);

                        //toggle flag
                        stock = false;
                    }

                    //get file list from sftp
                    RemoteDirectoryInfo remoteDirectory = session.ListDirectory("/outbound/");
                    List<string> directories = new List<string>();

                    //check file names
                    foreach (RemoteFileInfo fileInfo in remoteDirectory.Files)
                    {
                        if(fileInfo.Name.Contains(".csv"))
                        {
                            //add file to list
                            directories.Add(fileInfo.Name);
                        }
                    }

                    //check files
                    foreach(string file in directories)
                    {
                        string path = "/outbound/" + file;
                        string dest = destFolder + file;

                        try
                        {
                            //download file
                            TransferEventArgs eventArgs = session.GetFileToDirectory(path, destFolder);
                        }
                        catch(Exception ex)
                        {
                            //report erros to log
                            writeToFile(ex.Message);
                        }
                        
                        //create timestamp
                        int intYear = DateTime.Now.Year;
                        string strYear = intYear.ToString();
                        int intMonth = DateTime.Now.Month;
                        string strMonth = intMonth.ToString();
                        if (strMonth.Length < 2)
                        {
                            strMonth = "0" + strMonth;
                        }
                        int intDay = DateTime.Now.Day;
                        string strDay = intDay.ToString();
                        if (strDay.Length < 2)
                        {
                            strDay = "0" + strDay;
                        }
                        int intHour = DateTime.Now.Hour;
                        string strHour = intHour.ToString();
                        if (strHour.Length < 2)
                        {
                            strHour = "0" + strHour;
                        }
                        int intMinute = DateTime.Now.Minute;
                        string strMinute = intMinute.ToString();
                        if (strMinute.Length < 2)
                        {
                            strMinute = "0" + strMinute;
                        }
                        int intSecond = DateTime.Now.Second;
                        string strSecond = intSecond.ToString();
                        if (strSecond.Length < 2)
                        {
                            strSecond = "0" + strSecond;
                        }
                        int intMillisecond = DateTime.Now.Millisecond;
                        string strMillisecond = intMillisecond.ToString();
                        if (strMillisecond.Length == 1)
                        {
                            strMillisecond = "00" + strMillisecond;
                        }
                        else if (strMillisecond.Length == 2)
                        {
                            strMillisecond = "0" + strMillisecond;
                        }
                        string strDateTimeStamp = strYear + strMonth + strDay + "_" + strHour + strMinute + "_" + strSecond + strMillisecond;
                        string strTimeStamp = "_" + strMinute + strSecond + strMillisecond;

                        //create movepath
                        string destPath = "/outbound/Archive/" + file;

                        //check path for duplicates
                        if (session.FileExists("/outbound/Archive/" + file))
                        {
                            //change path
                            destPath = "/outbound/Archive/" + file.Replace(".csv", strTimeStamp + ".csv");

                            // FILE ALREADY EXISTS
                            MailMessage mail = new MailMessage("orders@ranshu.com", "jeremy@ranshu.com");
                            SmtpClient client = new SmtpClient();
                            client.EnableSsl = true;
                            client.Port = 587;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new System.Net.NetworkCredential("orders@ranshu.com", "%Ranshu525252");
                            client.Host = "secure.emailsrvr.com";
                            mail.Subject = "Duplicate EDI file Received";
                            mail.Body = "Duplicate EDI file received from " + file.ToString();
                            client.Send(mail);
                        }

                        //move file to archive
                        session.MoveFile(path, destPath);

                        //report to log
                        using (System.IO.StreamWriter stream = new System.IO.StreamWriter(@"\\Ranshu\Reports\edi\1800RAD\logs\log.txt", true))
                        {
                            stream.WriteLine(strDateTimeStamp + "  file received - " + file);
                        }
                        writeToFile("File Received - " + file);
                    }

                    //close connection
                    session.Close();
                }
            }//catch exceptions
            catch (Exception e)
            {
                //report errors to log
                writeToFile(e.StackTrace + e.Message);
            }
        }

        public MeridianFtpService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            stock = true;

            timer = new Timer(45000);
            timer.Elapsed += new ElapsedEventHandler(timerElapsed);
            timer.Enabled = true;
            timer.Start();

            writeToFile("Service Started");
        }

        static void timerElapsed(object sender, ElapsedEventArgs e)
        {
            //stop timer
            timer.Stop();
            try
            {
                //check for stock push time
                if (DateTime.Now > DateTime.Today.AddHours(19.00) && stockExp)
                {
                    //move stocks
                    moveStocks("radsftp.radiator.com", "ranshuexp", "DRXmJ64L", "ssh-ed25519 255 uLtkkJGqmFDqYoN20V5zYwT1FW8UctF6fy5FBQk90ck=", @"\\Ranshu\Reports\edi\outgoing\RADEXPRESS\stock\", new List<string>() { "RanshuExp", ".csv" });
                    stockExp = false;
                }
                else if (toggle)
                {
                    //move meridian
                    moveFilesMERIDIAN("ftp://ranshu.net/%2F/orders/", "meridian", "%M3r1d1aN2018", @"\\Ranshu\Reports\edi\MERIDIAN\");
                    toggle = false;
                }
                else
                {
                    //move 1800
                    //moveFiles1800("ftp://radftp.radiator.com/Outbound/", "ranshu", "ac2018jp", @"\\Ranshu\Reports\edi\1800RAD\");
                    moveFiles1800SFTP("radsftp.radiator.com", "ranshu", "loko485", @"\\Ranshu\Reports\edi\1800RAD\");
                    toggle = true;
                }
            }//catch exceptions
            catch(Exception ex)
            {
                //report errors to log
                string error = ex.StackTrace + " " + ex.Message;
                writeToFile(error);

                // REPORT ERROR TO IT
                MailMessage mail = new MailMessage("orders@ranshu.com", "ryan@ranshu.com");
                SmtpClient client = new SmtpClient();
                client.EnableSsl = true;
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("orders@ranshu.com", "%Ranshu525252");
                client.Host = "secure.emailsrvr.com";
                mail.Subject = "FTP Service error";
                mail.Body = error;
                client.Send(mail);
            }
            timer.Start();
        }

        static void moveFilesMERIDIAN(string ftpSite, string ftpUser, string ftpPass, string destFolder)
        {
            ftpRequest = (FtpWebRequest)WebRequest.Create(ftpSite);
            ftpRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            ftpRequest.UseBinary = true;
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            StreamReader streamReader = new StreamReader(ftpResponse.GetResponseStream());
            List<string> directories = new List<string>();
            List<string> labels = new List<string>();

            string line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                // Currently only allowing .csv files
                if (line.IndexOf(".csv") != -1)
                {
                    directories.Add(line);
                }
                else if (line.IndexOf(".zpl") != -1)
                {
                    labels.Add(line);
                }
                line = streamReader.ReadLine();
            }

            streamReader.Close();
            streamReader = null;

            ftpResponse.Close();
            ftpResponse = null;

            ftpRequest.Abort();
            ftpRequest = null;

            using (ftpClient = new WebClient())
            {
                ftpClient.Credentials = new System.Net.NetworkCredential(ftpUser, ftpPass);

                //removed 'directories.Count - 1'
                //limit the download to 10 files
                intFileCount = 9;
                if (directories.Count <= 10)
                {
                    intFileCount = directories.Count - 1;
                }

                for (int i = 0; i <= intFileCount; i++)
                {
                    if (directories[i].Contains("."))
                    {
                        // Download file from FTP server to local folder
                        string path = ftpSite + directories[i].ToString();
                        string trnsfrpth = destFolder + directories[i].ToString();
                        try
                        {
                            ftpClient.DownloadFile(path, trnsfrpth);
                        }
                        catch (WebException ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        path = null;
                        trnsfrpth = null;

                        int flagExists = 0;

                        // See if file exists in archive folder
                        fileRequest = (FtpWebRequest)WebRequest.Create(ftpSite + "Archive/" + directories[i].ToString());
                        fileRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                        fileRequest.UseBinary = true;
                        fileRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                        try
                        {
                            FtpWebResponse response = (FtpWebResponse)fileRequest.GetResponse();
                            flagExists = 1;
                        }
                        catch (WebException)
                        {
                            flagExists = 0;
                        }

                        fileRequest.Abort();
                        fileRequest = null;


                        int intYear = DateTime.Now.Year;
                        string strYear = intYear.ToString();
                        int intMonth = DateTime.Now.Month;
                        string strMonth = intMonth.ToString();
                        if (strMonth.Length < 2)
                        {
                            strMonth = "0" + strMonth;
                        }
                        int intDay = DateTime.Now.Day;
                        string strDay = intDay.ToString();
                        if (strDay.Length < 2)
                        {
                            strDay = "0" + strDay;
                        }
                        int intHour = DateTime.Now.Hour;
                        string strHour = intHour.ToString();
                        if (strHour.Length < 2)
                        {
                            strHour = "0" + strHour;
                        }
                        int intMinute = DateTime.Now.Minute;
                        string strMinute = intMinute.ToString();
                        if (strMinute.Length < 2)
                        {
                            strMinute = "0" + strMinute;
                        }
                        int intSecond = DateTime.Now.Second;
                        string strSecond = intSecond.ToString();
                        if (strSecond.Length < 2)
                        {
                            strSecond = "0" + strSecond;
                        }
                        int intMillisecond = DateTime.Now.Millisecond;
                        string strMillisecond = intMillisecond.ToString();
                        if (strMillisecond.Length == 1)
                        {
                            strMillisecond = "00" + strMillisecond;
                        }
                        else if (strMillisecond.Length == 2)
                        {
                            strMillisecond = "0" + strMillisecond;
                        }
                        string strDateTimeStamp = strYear + strMonth + strDay + "_" + strHour + strMinute + "_" + strSecond + strMillisecond;
                        string strTimeStamp = "_" + strMinute + strSecond + strMillisecond;

                        if (flagExists == 1)
                        {
                            string newFileName = directories[i].ToString().Replace(".csv", strTimeStamp + ".csv");
                            // Move file from to Archive Folder with timestamp
                            moveRequest = (FtpWebRequest)WebRequest.Create(ftpSite + directories[i].ToString());
                            moveRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                            //moveRequest.UseBinary = true;
                            moveRequest.Method = WebRequestMethods.Ftp.Rename;
                            moveRequest.RenameTo = "Archive/" + newFileName;
                            using (moveResponse = (FtpWebResponse)moveRequest.GetResponse())
                            {
                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\\Ranshu\Reports\edi\MERIDIAN\logs\log.txt", true))
                                {
                                    file.WriteLine(strDateTimeStamp + "  file received (dup) - " + newFileName);
                                }
                            }
                            moveResponse.Close();
                            moveResponse = null;

                            moveRequest.Abort();
                            moveRequest = null;

                            // FILE ALREADY EXISTS
                            MailMessage mail = new MailMessage("orders@ranshu.com", "jeremy@ranshu.com");
                            SmtpClient client = new SmtpClient();
                            client.EnableSsl = true;
                            client.Port = 587;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new System.Net.NetworkCredential("orders@ranshu.com", "%Ranshu525252");
                            client.Host = "secure.emailsrvr.com";
                            mail.Subject = "Duplicate EDI file Received";
                            mail.Body = "Duplicate EDI file received from " + directories[i].ToString();
                            client.Send(mail);
                        }
                        else
                        {
                            // Move file from to Archive Folder
                            moveRequest = (FtpWebRequest)WebRequest.Create(ftpSite + directories[i].ToString());
                            moveRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                            moveRequest.Method = WebRequestMethods.Ftp.Rename;
                            moveRequest.RenameTo = "archive/" + directories[i].ToString();
                            using (moveResponse = (FtpWebResponse)moveRequest.GetResponse())
                            {
                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\\Ranshu\Reports\edi\MERIDIAN\logs\log.txt", true))
                                {
                                file.WriteLine(strDateTimeStamp + "  file received - " + directories[i].ToString());
                                }
                            }
                            moveResponse.Close();
                            moveResponse = null;

                            moveRequest.Abort();
                            moveRequest = null;
                        }

                    }
                }

                foreach(string label in labels)
                {

                    int flagExists = 0;

                    // See if file exists in archive folder
                    fileRequest = (FtpWebRequest)WebRequest.Create(ftpSite + "Archive/" + label);
                    fileRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                    fileRequest.UseBinary = true;
                    fileRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                    try
                    {
                        FtpWebResponse response = (FtpWebResponse)fileRequest.GetResponse();
                        flagExists = 1;
                    }
                    catch (WebException)
                    {
                        flagExists = 0;
                    }

                    fileRequest.Abort();
                    fileRequest = null;

                    int intYear = DateTime.Now.Year;
                    string strYear = intYear.ToString();
                    int intMonth = DateTime.Now.Month;
                    string strMonth = intMonth.ToString();
                    if (strMonth.Length < 2)
                    {
                        strMonth = "0" + strMonth;
                    }
                    int intDay = DateTime.Now.Day;
                    string strDay = intDay.ToString();
                    if (strDay.Length < 2)
                    {
                        strDay = "0" + strDay;
                    }
                    int intHour = DateTime.Now.Hour;
                    string strHour = intHour.ToString();
                    if (strHour.Length < 2)
                    {
                        strHour = "0" + strHour;
                    }
                    int intMinute = DateTime.Now.Minute;
                    string strMinute = intMinute.ToString();
                    if (strMinute.Length < 2)
                    {
                        strMinute = "0" + strMinute;
                    }
                    int intSecond = DateTime.Now.Second;
                    string strSecond = intSecond.ToString();
                    if (strSecond.Length < 2)
                    {
                        strSecond = "0" + strSecond;
                    }
                    int intMillisecond = DateTime.Now.Millisecond;
                    string strMillisecond = intMillisecond.ToString();
                    if (strMillisecond.Length == 1)
                    {
                        strMillisecond = "00" + strMillisecond;
                    }
                    else if (strMillisecond.Length == 2)
                    {
                        strMillisecond = "0" + strMillisecond;
                    }
                    string strDateTimeStamp = strYear + strMonth + strDay + "_" + strHour + strMinute + "_" + strSecond + strMillisecond;
                    string strTimeStamp = "_" + strMinute + strSecond + strMillisecond;

                    string newFileName;

                    if (flagExists == 1)
                    {
                        newFileName = strDateTimeStamp + "_" + label;

                        // FILE ALREADY EXISTS
                        MailMessage mail = new MailMessage("orders@ranshu.com", "ryan@ranshu.com");
                        SmtpClient client = new SmtpClient();
                        client.EnableSsl = true;
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("orders@ranshu.com", "%Ranshu525252");
                        client.Host = "secure.emailsrvr.com";
                        mail.Subject = "Duplicate SFP label Received";
                        mail.Body = "Duplicate SFP label received " + label;
                        client.Send(mail);
                    }
                    else
                    {
                        newFileName = label;
                    }

                    // Download file from FTP server to local folder
                    string path = ftpSite + label;
                    string trnsfrpth = @"\\Ranshu\Reports\edi\MERIDIAN\SFPLabels\" + newFileName;
                    try
                    {
                        ftpClient.DownloadFile(path, trnsfrpth);
                    }
                    catch (WebException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    path = null;
                    trnsfrpth = null;


                    // Move file from to Archive Folder
                    moveRequest = (FtpWebRequest)WebRequest.Create(ftpSite + label);
                    moveRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                    moveRequest.Method = WebRequestMethods.Ftp.Rename;
                    moveRequest.RenameTo = "archive/" + newFileName;
                    using (moveResponse = (FtpWebResponse)moveRequest.GetResponse())
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\\Ranshu\Reports\edi\MERIDIAN\logs\log.txt", true))
                        {
                            file.WriteLine(strDateTimeStamp + "  file received - " + newFileName);
                        }
                    }
                    moveResponse.Close();
                    moveResponse = null;

                    moveRequest.Abort();
                    moveRequest = null;
                }

            }

            ftpClient.Dispose();
            ftpClient = null;

            directories = null;
        }

        static void checkOrphans()
        {

        }

        static void moveFiles1800(string ftpSite, string ftpUser, string ftpPass, string destFolder)
        {
            ftpRequest = (FtpWebRequest)WebRequest.Create(ftpSite);
            ftpRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            ftpRequest.UseBinary = true;
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            StreamReader streamReader = new StreamReader(ftpResponse.GetResponseStream());
            List<string> directories = new List<string>();

            string line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                // Currently only allowing .csv files
                if (line.IndexOf(".csv") != -1)
                {
                    directories.Add(line);
                }
                line = streamReader.ReadLine();
            }

            streamReader.Close();
            streamReader = null;

            ftpResponse.Close();
            ftpResponse = null;

            ftpRequest.Abort();
            ftpRequest = null;

            using (ftpClient = new WebClient())
            {
                ftpClient.Credentials = new System.Net.NetworkCredential(ftpUser, ftpPass);

                //removed 'directories.Count - 1'
                //limit the download to 10 files
                intFileCount = 9;
                if (directories.Count <= 10)
                {
                    intFileCount = directories.Count - 1;
                }
                for (int i = 0; i <= intFileCount; i++)
                {
                    if (directories[i].Contains("."))
                    {
                        // Download file from FTP server to local folder
                        string path = ftpSite + directories[i].ToString();
                        string trnsfrpth = destFolder + directories[i].ToString();
                        try
                        {
                            ftpClient.DownloadFile(path, trnsfrpth);
                        }
                        catch (WebException)
                        {
                        }
                        path = null;
                        trnsfrpth = null;

                        int flagExists = 0;

                        // See if file exists in archive folder
                        fileRequest = (FtpWebRequest)WebRequest.Create(ftpSite + "Archive/" + directories[i].ToString());
                        fileRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                        fileRequest.UseBinary = true;
                        fileRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                        try
                        {
                            FtpWebResponse response = (FtpWebResponse)fileRequest.GetResponse();
                        }
                        catch (WebException)
                        {
                            flagExists = 0;
                        }

                        fileRequest.Abort();
                        fileRequest = null;


                        int intYear = DateTime.Now.Year;
                        string strYear = intYear.ToString();
                        int intMonth = DateTime.Now.Month;
                        string strMonth = intMonth.ToString();
                        if (strMonth.Length < 2)
                        {
                            strMonth = "0" + strMonth;
                        }
                        int intDay = DateTime.Now.Day;
                        string strDay = intDay.ToString();
                        if (strDay.Length < 2)
                        {
                            strDay = "0" + strDay;
                        }
                        int intHour = DateTime.Now.Hour;
                        string strHour = intHour.ToString();
                        if (strHour.Length < 2)
                        {
                            strHour = "0" + strHour;
                        }
                        int intMinute = DateTime.Now.Minute;
                        string strMinute = intMinute.ToString();
                        if (strMinute.Length < 2)
                        {
                            strMinute = "0" + strMinute;
                        }
                        int intSecond = DateTime.Now.Second;
                        string strSecond = intSecond.ToString();
                        if (strSecond.Length < 2)
                        {
                            strSecond = "0" + strSecond;
                        }
                        int intMillisecond = DateTime.Now.Millisecond;
                        string strMillisecond = intMillisecond.ToString();
                        if (strMillisecond.Length == 1)
                        {
                            strMillisecond = "00" + strMillisecond;
                        }
                        else if (strMillisecond.Length == 2)
                        {
                            strMillisecond = "0" + strMillisecond;
                        }
                        string strDateTimeStamp = strYear + strMonth + strDay + "_" + strHour + strMinute + "_" + strSecond + strMillisecond;
                        string strTimeStamp = "_" + strMinute + strSecond + strMillisecond;

                        if (flagExists == 1)
                        {
                            string newFileName = directories[i].ToString().Replace(".csv", strTimeStamp + ".csv");
                            // Move file from to Archive Folder with timestamp
                            moveRequest = (FtpWebRequest)WebRequest.Create(ftpSite + directories[i].ToString());
                            moveRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                            //moveRequest.UseBinary = true;
                            moveRequest.Method = WebRequestMethods.Ftp.Rename;
                            moveRequest.RenameTo = "Archive/" + newFileName;
                            using (moveResponse = (FtpWebResponse)moveRequest.GetResponse())
                            {
                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\\Ranshu\Reports\edi\1800RAD\logs\log.txt", true))
                                {
                                    file.WriteLine(strDateTimeStamp + "  file received (dup) - " + newFileName);
                                }
                            }
                            moveResponse.Close();
                            moveResponse = null;

                            moveRequest.Abort();
                            moveRequest = null;

                            // FILE ALREADY EXISTS
                            MailMessage mail = new MailMessage("orders@ranshu.com", "jeremy@ranshu.com");
                            SmtpClient client = new SmtpClient();
                            client.EnableSsl = true;
                            client.Port = 587;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new System.Net.NetworkCredential("orders@ranshu.com", "%Ranshu525252");
                            client.Host = "secure.emailsrvr.com";
                            mail.Subject = "Duplicate EDI file Received";
                            mail.Body = "Duplicate EDI file received from " + directories[i].ToString();
                            client.Send(mail);
                        }
                        else
                        {
                            // Move file from to Archive Folder
                            moveRequest = (FtpWebRequest)WebRequest.Create(ftpSite + directories[i].ToString());
                            moveRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                            moveRequest.Method = WebRequestMethods.Ftp.Rename;
                            moveRequest.RenameTo = "archive/" + directories[i].ToString();
                            using (moveResponse = (FtpWebResponse)moveRequest.GetResponse())
                            {
                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\\Ranshu\Reports\edi\1800RAD\logs\log.txt", true))
                                {
                                file.WriteLine(strDateTimeStamp + "  file received - " + directories[i].ToString());
                                }
                            }
                            moveResponse.Close();
                            moveResponse = null;

                            moveRequest.Abort();
                            moveRequest = null;
                        }

                    }
                }

            }

            ftpClient.Dispose();
            ftpClient = null;

            directories = null;

        }



        public static void writeToFile(string message)
        {
            //open/create log directory
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\"+DateTime.Now.Year;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //open/create log file
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Year+"\\FTP_Service_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
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

        protected override void OnStop()
        {
            writeToFile("Service Stopped");
        }
    }
}
