using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Data.Odbc;
using System.Net.Mail;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Security.AccessControl;

namespace InvoiceDigitizer
{
    /// <summary>
    /// main form for project
    /// displays controls for processing and reviewing invoice stubs
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// holds list of stubs that have been successfully converted to pdfs
        /// </summary>
        List<Stub> success;

        bool dupeProblem;

        /// <summary>
        /// holds list of stubs being reviewed by the user
        /// </summary>
        List<Stub> review;

        /// <summary>
        /// holds stub currently being reviewed by the user
        /// </summary>
        Stub reviewStub;

        /// <summary>
        /// timers used for processing operations on separate threads
        /// </summary>
        Timer timer, reviewTimer;
        //string[] files;

        /// <summary>
        /// File paths for use is stub processing
        /// </summary>
        string queuePath = @"\\RANSHU\Reports\BOL\smallpackage\queue";
        string successPath = @"\\RANSHU\Reports\BOL\smallpackage";
        string reviewPath = @"\\RANSHU\Reports\BOL\smallpackage\review";
        string duplicatePath = @"\\RANSHU\Reports\BOL\smallpackage\duplicates";

        /// <summary>
        /// runs on form load
        /// initializes form and data
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            initData();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timerTick);
            timer.Enabled = true;
            timer.Stop();

            reviewTimer = new Timer();
            reviewTimer.Interval = 1000;
            reviewTimer.Tick += new EventHandler(reviewTick);
            reviewTimer.Enabled = true;
            reviewTimer.Stop();
        }

        /// <summary>
        /// No longer in use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reviewTick(object sender, EventArgs e)
        {
            reviewTimer.Stop();
            File.Move(reviewStub.file, reviewStub.newFile);
            review.Remove(reviewStub);
            nextStub();
        }

        /// <summary>
        /// no longer in use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTick(object sender, EventArgs e)
        {
            timer.Stop();
        }

        /// <summary>
        /// processes stubs in the queue
        /// clears processed image files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            bool delete = true;
            dupeProblem = false;

            ///report status to user
            MessageText.Text = "Processing Stubs.\nPlease Wait.";
            MessagePanel.Show();

            ///process stubs
            await Task.Run(processStubs);

            GC.Collect();

            while (delete)
            {
                try
                {
                    ///delete processed image files
                    foreach (Stub stub in success)
                    {
                        await Task.Run(stub.deleteFile);
                    }

                    delete = false;
                }
                catch
                {
                    GC.Collect();
                    delete = true;
                }
            }

            ///get number of processed stubs
            string stubTotal = StubCounter.Text;

            ///initialize data
            initData();

            ///report to process user
            MessagePanel.Show();
            MessageText.Text = stubTotal + " stub(s) processed. " + ReviewCounter.Text + " stub(s) marked for review.";

            if (dupeProblem)
                MessageBox.Show("One or more invoices has a duplicate file that is currently open. Please close any instances of the digitizer and any open BOL files before reviewing the flagged files.");
        }

        /// <summary>
        /// processes image files into pdfs for stubs
        /// </summary>
        private void processStubs()
        {
            ///get stub images
            string[] files = Directory.GetFiles(queuePath);
            success = new List<Stub>();

            ///for each image in file
            for(int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                ///if file is a jpg and is the first of 2 scans
                if (!file.EndsWith("_B.jpg") && file.EndsWith(".jpg"))
                {
                    ///create stub
                    Stub stub = new Stub();
                    stub.file = file;

                    ///initialize date field
                    string date = "";

                    ///create bitmap from image file
                    using (Bitmap bitmap = (Bitmap)Image.FromFile(file))
                    {
                        try
                        {
                            ///initialize barcode reader
                            IBarcodeReader reader = new BarcodeReader();
                            reader.Options.TryHarder = true;

                            ///attempt to find/read barcode in image
                            if (!file.EndsWith("_A.jpg"))
                                stub.invoiceNum = Convert.ToInt32((reader.Decode(bitmap)).ToString());
                            else
                            {
                                try
                                {
                                    stub.invoiceNum = Convert.ToInt32((reader.Decode(bitmap)).ToString());
                                }
                                catch
                                {
                                    stub.invoiceNum = Convert.ToInt32((reader.Decode((Bitmap)Image.FromFile(file.Replace("_A", "_B"))).ToString()));
                                }
                            }

                            ///dispose of reader
                            reader = null;

                            ///establish database connection
                            string strConnection = "DSN=Ranshu";
                            OdbcConnection pSqlConn = null;
                            using (pSqlConn = new OdbcConnection(strConnection))
                            {
                                ///check invoice num against database and confirm date
                                string cmdString = "select BKAR_INV_INVDTE, BKAR_INV_NUM from BKARHINV where BKAR_INV_NUM = " + stub.invoiceNum + " and BKAR_INV_INVDTE >= '" + Calendar.SelectionStart.ToString("yyyy-MM-dd") + "' " +
                                                   "and BKAR_INV_INVDTE < '" + Calendar.SelectionEnd.AddDays(1).ToString("yyyy-MM-dd") + "' ";

                                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                                pSqlConn.Open();
                                OdbcDataReader sqlReader = cmd.ExecuteReader();
                                if (!sqlReader.HasRows)
                                    throw new Exception();
                                else
                                {
                                    date = Convert.ToDateTime(sqlReader["BKAR_INV_INVDTE"]).ToString("yyyyMM");
                                }
                                pSqlConn.Close();
                            }
                        }
                        catch
                        {
                            ///catch any errors and set invoice num to 0
                            stub.invoiceNum = 0;
                        }

                        ///create jpeg encoder
                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                        System.Drawing.Imaging.Encoder myEncoder =
                            System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 10L);
                        myEncoderParameters.Param[0] = myEncoderParameter;

                        ///compress stub image for pdf
                        if (file.EndsWith("_A.jpg"))
                        {
                            bitmap.Save(file.Replace("_A", "Z_A"), jpgEncoder, myEncoderParameters);
                            Bitmap bitmapB = (Bitmap)Image.FromFile(file.Replace("_A", "_B"));
                            bitmapB.Save(file.Replace("_A", "Z_B"), jpgEncoder, myEncoderParameters);
                            bitmap.Dispose();
                            bitmapB.Dispose();
                            GC.Collect();
                            stub.compressedFile = file.Replace("_A", "Z_A");
                        }
                        else
                        {
                            bitmap.Save(file.Replace(".jpg", "Z.jpg"), jpgEncoder, myEncoderParameters);
                            bitmap.Dispose();
                            GC.Collect();
                            stub.compressedFile = file.Replace(".jpg", "Z.jpg");
                        }

                        ///initialize pdf document
                        PdfDocument document = new PdfDocument();
                        document.Options.FlateEncodeMode =
                            PdfFlateEncodeMode.BestCompression;
                        document.Options.UseFlateDecoderForJpegImages =
                            PdfUseFlateDecoderForJpegImages.Automatic;
                        document.Options.NoCompression = false;
                        document.Options.CompressContentStreams = true;

                        ///add page to pdf
                        PdfPage page = document.AddPage();
                        page.Size = PageSize.Letter;

                        ///draw stub image(s) to pdf
                        XImage image = XImage.FromFile(stub.compressedFile);
                        XGraphics gfx = XGraphics.FromPdfPage(page);
                        gfx.DrawImage(image, 0.0, 0.0);
                        if (stub.compressedFile.EndsWith("_A.jpg") && files.Contains(file.Replace("_A", "_B")))
                        {
                            image.Dispose();
                            image = XImage.FromFile(stub.compressedFile.Replace("_A", "_B"));
                            gfx.DrawImage(image, 0.0, 300);
                        }

                        ///clear potential file locks
                        bitmap.Dispose();
                        image.Dispose();
                        gfx.Dispose();
                        image = null;
                        gfx = null;

                        ///if stub has invoice number
                        if (stub.invoiceNum != 0)
                        {
                            ///check for valid directory
                            if (!Directory.Exists(successPath + "\\" + date))
                            {
                                ///create directory using invoice date
                                Directory.CreateDirectory(successPath + "\\" + date);
                            }

                            ///check path for duplicates and move files accordingly 
                            if (File.Exists(successPath + "\\" + date + "\\" + stub.invoiceNum + ".pdf"))
                            {
                                ///save duplicate pdf and move original pdf to review file
                                try
                                {
                                    File.Move(successPath + "\\" + date + "\\" + stub.invoiceNum + ".pdf", reviewPath + "\\" + stub.invoiceNum + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + "x.pdf");
                                }
                                catch
                                {
                                    dupeProblem = true;
                                }

                                stub.newFile = (reviewPath + "\\" + stub.invoiceNum + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + "y.pdf");
                            }
                            else if (File.Exists(successPath + "\\" + date + "\\" + stub.invoiceNum + "CXL.pdf"))
                            {
                                ///save duplicate pdf and move original pdf to review file
                                try
                                {
                                    File.Move(successPath + "\\" + date + "\\" + stub.invoiceNum + "CXL.pdf", reviewPath + "\\" + stub.invoiceNum + "CXL" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + "x.pdf");
                                }
                                catch
                                {
                                    dupeProblem = true;
                                }

                                stub.newFile = (reviewPath + "\\" + stub.invoiceNum + "CXL" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + "y.pdf");
                            }
                            else if (File.Exists(successPath + "\\" + date + "\\" + stub.invoiceNum + "R.pdf"))
                            {
                                ///save duplicate pdf to review file
                                stub.newFile = (reviewPath + "\\" + stub.invoiceNum + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + ".pdf");
                            }
                            else if (File.Exists(successPath + "\\" + date + "\\" + stub.invoiceNum + "RCXL.pdf"))
                            {
                                ///save duplicate pdf to review file
                                stub.newFile = (reviewPath + "\\" + stub.invoiceNum + "CXL" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + ".pdf");
                            }
                            else ///move to success file
                                stub.newFile = (successPath + "\\" + date + "\\" + stub.invoiceNum + ".pdf");
                        }
                        else
                        {
                            ///move files to review if no invoice number was determined
                            stub.newFile = (reviewPath + "\\" + i + " " + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + ".pdf");
                        }

                        document.Save(stub.newFile);

                        ///dispose of document to clear file locks
                        document.Dispose();
                        document = null;
                    }

                    ///collect garbage
                    GC.Collect();

                    ///add stub to success list
                    success.Add(stub);
                }
            }
        }

        /// <summary>
        /// creates encoder for compressing images
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }

        /// <summary>
        /// starts the review process and displays review controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button2_Click(object sender, EventArgs e)
        {
            ///initialize review list
            review = new List<Stub>();

            ///populate review list with stubs
            foreach (string file in Directory.GetFiles(reviewPath))
            {
                if (file.EndsWith(".pdf"))
                {
                    Stub stub = new Stub();
                    stub.file = file;

                    review.Add(stub);
                }
            }

            ///add duplicate stubs to review list
            foreach (string file in Directory.GetFiles(duplicatePath))
            {
                if (file.EndsWith(".pdf"))
                {
                    Stub stub = new Stub();
                    stub.file = file;

                    review.Add(stub);
                }
            }

            ///start review process if there are stubs to review
            if (review.Count > 0)
            {
                ///report process to user
                MessagePanel.Show();
                MessageText.Text = "Retrieving review pages.\nPlease Wait.";

                ///set up review controls
                LeftTable.Enabled = false;
                await Task.Run(nextStub);
                ReviewControlsTable.Show();
                MessagePanel.Hide();
                MessageText.Text = "";
                ReviewInput.Text = "";
                ReviewInput.Focus();
            }
            else///report to user
                MessageBox.Show("No stubs left for review.");
        }

        /// <summary>
        /// displays top stub in review list
        /// </summary>
        private void nextStub()
        {

            if (review.Count > 0)
            {
                ReviewCounter.Text = review.Count.ToString();
                reviewStub = review[0];
                ReviewBrowser.Navigate(reviewStub.file);
            }
            else
                endReview();
        }

        /// <summary>
        /// gets stub counts for application
        /// displays data to users
        /// </summary>
        private void initData()
        {
            ///count stubs in queue and in review folders
            StubCounter.Text = (Directory.GetFiles(queuePath).ToList().RemoveAll(x => x.EndsWith(".jpg")) / 2).ToString();
            ReviewCounter.Text = (Directory.GetFiles(reviewPath).ToList().RemoveAll(x => x.EndsWith(".pdf"))).ToString();

            ///set button enabled fields
            ProcessButton.Enabled = Convert.ToInt32(StubCounter.Text) > 0;
            ReviewButton.Enabled = Convert.ToInt32(ReviewCounter.Text) > 0;

            if(!(ProcessButton.Enabled || ReviewButton.Enabled))
            {
                ///display idle message
                MessagePanel.Show();
                MessageText.Text = "No stubs left to process.\nPlease scan additional stubs and hit refresh.";
            }
            else
                MessagePanel.Hide();

            ///hide review controls
            ReviewControlsTable.Hide();
        }

        /// <summary>
        /// DEPRECIATED
        /// Used for testing speed and success rate of various scanning methods
        /// </summary>
        private void trial()
        {
            string queuePath = @"\\RANSHUDC2\users_data\_Information Technology\Dev Csharp\Ranshu Projects\RFox\ScanTest";

            int brightness = 150;
            string contrast = "7";
            int sharpness = 3;
            int Rescans = 0;
            string datafile = queuePath + "\\Trial_B" + brightness + "_C" + contrast + "_S" + sharpness + "_R" + Rescans + ".txt";

            for (int j = 0; j < 5; j++)
            {
                int successCounter = 0;
                int counter = 0;
                var watch = System.Diagnostics.Stopwatch.StartNew();


                string[] files = Directory.GetFiles(queuePath);
                success = new List<Stub>();

                foreach (string file in files)
                {
                    if (!file.EndsWith("_B.jpg") && file.EndsWith(".jpg"))
                    {
                        Stub stub = new Stub();
                        stub.file = file;

                        using (Bitmap bitmap = (Bitmap)Image.FromFile(file))
                        {
                            for (int i = 0; i <= Rescans; i++)
                            {
                                try
                                {
                                    IBarcodeReader reader = new BarcodeReader();
                                    reader.Options.TryHarder = true;
                                    if (!file.EndsWith("_A.jpg"))
                                        stub.invoiceNum = Convert.ToInt32((reader.Decode(bitmap)).ToString());
                                    else
                                    {
                                        try
                                        {
                                            stub.invoiceNum = Convert.ToInt32((reader.Decode(bitmap)).ToString());
                                        }
                                        catch
                                        {
                                            stub.invoiceNum = Convert.ToInt32((reader.Decode((Bitmap)Image.FromFile(file.Replace("_A", "_B"))).ToString()));
                                        }
                                    }
                                    reader = null;

                                    //establish database connection
                                    string strConnection = "DSN=Ranshu";
                                    OdbcConnection pSqlConn = null;
                                    using (pSqlConn = new OdbcConnection(strConnection))
                                    {
                                        //get unprocessed invoices from database
                                        string cmdString = "select BKAR_INV_NUM from BKARHINV where BKAR_INV_NUM = " + stub.invoiceNum + " and BKAR_INV_INVDTE >= '2020-01-01'";

                                        OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                                        pSqlConn.Open();
                                        OdbcDataReader sqlReader = cmd.ExecuteReader();
                                        if (!sqlReader.HasRows)
                                            throw new Exception();
                                        pSqlConn.Close();
                                    }

                                    break;
                                }
                                catch
                                {
                                    stub.invoiceNum = 0;
                                }
                            }

                            PdfDocument document = new PdfDocument();
                            PdfPage page = document.AddPage();
                            page.Size = PageSize.Letter;

                            XImage image = XImage.FromFile(file);
                            XGraphics gfx = XGraphics.FromPdfPage(page);
                            gfx.DrawImage(image, 0.0, 0.0);
                            if (file.EndsWith("_A.jpg") && files.Contains(file.Replace("_A", "_B")))
                            {
                                image.Dispose();
                                image = XImage.FromFile(file.Replace("_A", "_B"));
                                gfx.DrawImage(image, 0.0, 300);
                            }

                            counter++;
                            if (stub.invoiceNum != 0)
                            {
                                successCounter++;
                                /*
                                if (!File.Exists(successPath + "\\" + stub.invoiceNum + ".pdf"))
                                {
                                    document.Save(stub.newFile = (successPath + "\\" + stub.invoiceNum + ".pdf"));
                                }
                                else
                                {
                                    document.Save(stub.newFile = (reviewPath + "\\" + stub.invoiceNum + "_duplicate_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + ".pdf"));
                                }*/
                            }
                            else
                            {
                                //document.Save(stub.newFile = (reviewPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + ".pdf"));
                            }

                            bitmap.Dispose();
                            image.Dispose();
                            document.Dispose();
                            gfx.Dispose();
                        }

                        GC.Collect();
                        success.Add(stub);
                    }
                }

                watch.Stop();

                if (!File.Exists(datafile))
                {
                    using (StreamWriter sw = File.CreateText(datafile))
                    {
                        ///write message to file
                        sw.WriteLine("Trial: Brightness: " + brightness + " Contrast: " + contrast + " Sharpness: " + sharpness + " Rescans: " + Rescans + "");
                        sw.WriteLine("Success, Total, Time");
                        sw.WriteLine(successCounter + "," + counter + "," + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds / 10));
                    }
                }
                else
                {

                    using (StreamWriter sw = File.AppendText(datafile))
                    {
                        ///write message to file
                        sw.WriteLine(successCounter + "," + counter + "," + string.Format("{0:00}:{1:00}:{2:00}.{3:00}", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds / 10));
                    }
                }
            }
        }

        /// <summary>
        /// handles review submission
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ReviewSubmitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You entered " + ReviewInput.Text + ". Is this correct?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ///clear browser and attempt to submit review number
                    await Task.Run(clearBrowser);
                    System.Threading.Thread.Sleep(1000);
                    GC.Collect();
                    await Task.Run(submitReview);

                    ///if a stub with matchin invoice number exists move it
                    if (reviewStub.reviewDupe != null)
                        await Task.Run(reviewMove);

                    ///if new file location specified move the file
                    if (reviewStub.newFile != null)
                        await Task.Run(moveFile);
                    else
                    {
                        ///delete duplicate stub
                        await Task.Run(reviewStub.deleteFile);
                        MessageBox.Show("Duplicate stub appended to " + reviewStub.invoiceNum + ".");
                    }

                    ///remove stub from list
                    review.Remove(reviewStub);
                }
                catch (Exception ex)
                {
                    ///report errors to user
                    if (ex.Message == "Invalid invoice number. Please try again.")
                        MessageBox.Show(ex.Message);
                    else
                        MessageBox.Show("An error occurred. Please try again. If error persists the BOL file for the invoice may be open by another user.");
                }

                ///get next stub
                nextStub();
            }

            ///move cursor
            ReviewInput.Text = "";
            ReviewInput.Focus();
        }

        private void reviewMove()
        {
            if (reviewStub.reviewDupe.Contains("CXL"))
                File.Move(reviewStub.reviewDupe, reviewPath + "\\" + reviewStub.invoiceNum + "CXL" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + ".pdf");
            else
                File.Move(reviewStub.reviewDupe, reviewPath + "\\" + reviewStub.invoiceNum + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.sss_tt") + ".pdf");

        }

        private void moveFile()
        {
            File.Move(reviewStub.file, reviewStub.newFile);
        }

        private void submitReview()
        {
            int input = Convert.ToInt32(ReviewInput.Text);

            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                string cmdString = "select BKAR_INV_INVDTE, BKAR_INV_NUM from BKARHINV where BKAR_INV_NUM = " + input + " and BKAR_INV_INVDTE >= '"+Calendar.SelectionStart.AddDays(-7).ToString("yyyy-MM-dd")+ "' " +
                    "and BKAR_INV_INVDTE < '"+Calendar.SelectionEnd.AddDays(1).ToString("yyyy-MM-dd")+"' ";

                string date = "";

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                OdbcDataReader sqlReader = cmd.ExecuteReader();
                if (!sqlReader.HasRows)
                    throw new Exception("Invalid invoice number. Please try again.");
                else
                {
                    date = Convert.ToDateTime(sqlReader["BKAR_INV_INVDTE"]).ToString("yyyyMM");

                    reviewStub.invoiceNum = input;

                    if (!Directory.Exists(successPath + "\\" + date))
                        Directory.CreateDirectory(successPath + "\\" + date);

                    if (File.Exists(successPath + "\\" + date + "\\" + reviewStub.invoiceNum + ".pdf") || File.Exists(successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "CXL.pdf") || (File.Exists(successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "R.pdf") || File.Exists(successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "RCXL.pdf")))
                    {
                        if (File.Exists(successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "R.pdf") || File.Exists(successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "RCXL.pdf"))
                        {
                            PdfDocument document;
                            if (File.Exists(successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "R.pdf"))
                            {
                                document = PdfReader.Open(successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "R.pdf", PdfDocumentOpenMode.Modify);
                            }
                            else
                            {
                                document = PdfReader.Open(successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "RCXL.pdf", PdfDocumentOpenMode.Modify);
                            }

                            document.Options.FlateEncodeMode =
                                PdfFlateEncodeMode.BestCompression;
                            document.Options.UseFlateDecoderForJpegImages =
                                PdfUseFlateDecoderForJpegImages.Automatic;
                            document.Options.NoCompression = false;
                            document.Options.CompressContentStreams = true;

                            foreach (PdfPage page in PdfReader.Open(reviewStub.file, PdfDocumentOpenMode.Import).Pages)
                                document.AddPage(page);

                            document.Save(document.FullPath);
                            SendEmail("shippingNV@ranshu.com",
                                "Duplicate stub detected for invoice: " + reviewStub.invoiceNum,
                                "A duplicate stub was appended to the file for invoice " + reviewStub.invoiceNum + ". A copy of the file is attached to this email.",
                                new Attachment(document.FullPath), new List<string>() { "ryan@ranshu.com", "jeremy@ranshu.com" });
                            document.Dispose();
                        }
                        else if(File.Exists(successPath + "\\" + date + "\\" + reviewStub.invoiceNum + ".pdf"))
                        {
                            reviewStub.reviewDupe = successPath + "\\" + date + "\\" + reviewStub.invoiceNum + ".pdf";
                            reviewStub.newFile = successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "R.pdf";
                        }
                        else
                        {
                            reviewStub.reviewDupe = successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "CXL.pdf";
                            reviewStub.newFile = successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "RCXL.pdf";
                        }
                    }
                    else
                    {
                        if (reviewStub.file.Contains("CXL"))
                        {
                            reviewStub.newFile = successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "RCXL.pdf";
                        }
                        else
                        {
                            reviewStub.newFile = successPath + "\\" + date + "\\" + reviewStub.invoiceNum + "R.pdf";
                        }
                    }
                }
                pSqlConn.Close();
            }
        }

        private async void ReviewDeleteButton_Click(object sender, EventArgs e)
        {
            await Task.Run(clearBrowser);
            GC.Collect();
            DialogResult dialog = MessageBox.Show("Are you sure? This will permanently delete the file being reviewed.", "", MessageBoxButtons.YesNo);

            if(dialog == DialogResult.Yes)
            {
                try
                {
                    await Task.Run(reviewStub.deleteFile);
                    review.Remove(reviewStub);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            nextStub();
        }

        private void clearBrowser()
        {
            ReviewBrowser.Navigate("");
        }

        private void ReviewStopButton_Click(object sender, EventArgs e)
        {
            endReview();
        }

        private void ReviewInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Keys.Enter == (Keys)e.KeyChar)
            {
                this.ReviewSubmitButton_Click(this, new EventArgs());
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            initData();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {

        }

        private void endReview()
        {
            review = null;
            LeftTable.Enabled = true;
            ReviewBrowser.Navigate("");
            ReviewInput.Text = "";
            initData();
        }

        private void ClearQueueButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Clear queue of all scans? This will permanently delete any unprocessed stubs and cannot be reversed.", "", MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                foreach (string file in Directory.GetFiles(queuePath))
                {
                    if (file.EndsWith(".jpg"))
                        File.Delete(file);
                }
            }
        }

        private void CalendarButton_Click(object sender, EventArgs e)
        {
            if (CalendarPanel.Visible)
                CalendarPanel.Hide();
            else
                CalendarPanel.Show();
        }

        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            CalendarPanel.Hide();
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
        public static void SendEmail( string recipient, string subject, string msgText, Attachment attachment = null, List<string> cc = null)
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

            msgMail.Attachments.Add(attachment);

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
