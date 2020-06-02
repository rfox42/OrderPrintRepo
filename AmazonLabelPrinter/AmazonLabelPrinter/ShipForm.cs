using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Zebra.Sdk.Device;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace AmazonLabelPrinter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = null;

                int invoice = Convert.ToInt32(textBox1.Text);

                //PrintDialog pd = new PrintDialog();
                //pd.PrinterSettings = new PrinterSettings();

                //establish database connection
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //get unprocessed invoices from database
                    string cmdString = "select BKAR_INV_CUSORD from BKARHINV where BKAR_INV_SHPVIA like '%SFP%' and BKAR_INV_NUM = " + invoice;

                    OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                    pSqlConn.Open();
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        reader.Read();
                        fileName = reader["BKAR_INV_CUSORD"].ToString().TrimEnd();
                    }
                    pSqlConn.Close();
                }

                if(fileName == null || fileName == "")
                {
                    throw new Exception("Invoice is not an SFP order.");
                }

                string path = @"\\10.1.1.7\Reports\edi\MERIDIAN\SFPLabels\" + fileName.TrimEnd() + ".zpl";
                string archPath = @"\\10.1.1.7\Reports\edi\MERIDIAN\SFPLabels\Archive\"+ DateTime.Now.Year + "\\" + "" + fileName.TrimEnd() + ".zpl";

                if (File.Exists(path))
                {
                    if (RawPrinterHelper.SendFileToPrinter("AMZN3844z", path))
                    {
                        if (!File.Exists(archPath))
                        {
                            getTracking(path, fileName, invoice);
                            Directory.Move(path, archPath);
                        }
                        else
                        {
                            MessageBox.Show("Duplicate label detected. Please check tracking number. TRACKING NUM: " + getTrackNum(path));
                            SendEmail("Duplicate Label Detected", "A duplicate label has been detected for " + fileName + ". Please check labels for discrepancies.");
                        }
                    }
                }
                else if (File.Exists(archPath))
                {
                    DialogResult dialog = MessageBox.Show("The label for this invoice has already been printed. Print again?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialog == DialogResult.Yes)
                        RawPrinterHelper.SendFileToPrinter("AMZN3844z", archPath);
                }
            }
            catch(Exception ex)
            {
                textBox1.Text = "";
                textBox1.Focus();
                MessageBox.Show(ex.Message);
            }

            textBox1.Text = "";
            textBox1.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter && textBox1.Text != "")
            {
                this.button1_Click(this, new EventArgs());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select label to print";
            fdlg.InitialDirectory = @"\\10.1.1.7\Reports\edi\MERIDIAN\SFPLabels\";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                RawPrinterHelper.SendFileToPrinter("AMZN3844z", fdlg.FileName);
                if (!File.Exists(@"\\10.1.1.7\Reports\edi\MERIDIAN\SFPLabels\Archive\" + DateTime.Now.Year + "\\" + fdlg.SafeFileName))
                {
                    MessageBox.Show("Tracking details are not populated with manual selection. Please submit tracking number to checklist. TRACKING NUMBER: " + getTrackNum(fdlg.FileName));
                    Directory.Move(fdlg.FileName, fdlg.InitialDirectory + @"Archive\" + DateTime.Now.Year + "\\" + fdlg.SafeFileName);
                }
                else
                {
                    MessageBox.Show("Tracking details are not populated with manual selection. Please submit tracking number to checklist. TRACKING NUMBER: " + getTrackNum(fdlg.FileName));
                }

            }
        }

        private string getTrackNum(string filePath)
        {
            IEnumerable<string> lines = File.ReadAllLines(filePath);
            string trackingNum = lines.Where(x => x.StartsWith("^FD>:", StringComparison.CurrentCultureIgnoreCase)).ToList<string>()[0];
            trackingNum = trackingNum.Replace("^FD>:", "");
            trackingNum = trackingNum.Replace("^FS", "");
            return trackingNum;
        }

        private void getTracking(string filePath, string PONum, int invoiceNum)
        {
            string trackingNum = getTrackNum(filePath);

            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                string date = DateTime.Now.ToString("yyyyMMdd");
                //get unprocessed invoices from database
                string cmdString = "insert into TRACKING (INVOICE_NUM, TRACK_DATE, TRACKING_NUM, SERVICE_TYPE, REFERENCE_1, REFERENCE_2, MATCH, ENTBY, UPDATE_DATE) " +
                    "values ("+invoiceNum+", '"+date+"', '"+trackingNum+"', 'SFP', '"+PONum+"', '"+invoiceNum+"', "+invoiceNum+", 'AUTO', '"+date+"')";

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                cmd.ExecuteNonQuery();
                pSqlConn.Close();
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
        static void SendEmail(string subject, string msgText, List<string> cc = null)
        {
            //set email credentials
            SmtpClient mailClient = new SmtpClient("secure.emailsrvr.com");
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = new System.Net.NetworkCredential("orders@ranshu.com", "%Ranshu525252");
            mailClient.Port = 587;
            mailClient.EnableSsl = true;

            //create message
            MailMessage msgMail;
            msgMail = new MailMessage(new MailAddress("orders@ranshu.com"), new MailAddress("ryan@ranshu.com"));
            if (cc != null)
            {
                foreach (string address in cc)
                {
                    msgMail.CC.Add(new MailAddress(address));
                }
            }
            msgMail.Subject = "Ranshu Print Service Error: " + subject;
            msgMail.Body = msgText;
            msgMail.IsBodyHtml = true;

            //send message
            mailClient.Send(msgMail);

            //garbage collect
            msgMail.Dispose();
        }
    }


    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            fs.Close();
            return bSuccess;
        }
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }
}
