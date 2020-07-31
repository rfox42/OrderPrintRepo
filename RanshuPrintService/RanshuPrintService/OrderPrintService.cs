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
using System.IO;
using Microsoft.Win32.SafeHandles;
using Zebra.Sdk.Device;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using System.Runtime.InteropServices;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Printing;
using System.Printing;
using Microsoft.Win32;
using Microsoft.Office.Interop.Excel;

namespace RanshuPrintService
{
    /// <summary>
    /// cycles across database looking for invoices to print to shipping
    /// </summary>
    public partial class OrderPrintService : ServiceBase
    {

        ///Create Excel Application Instance for use by Program Forms
        public static Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        public static Workbook xlWorkBook = OrderPrintService.xlApp.Workbooks.Add();
        public static Worksheet xlsheet;

        public static Range xlBarcodeTop;
        public static Range xlBarcodeBottom;

        public static object misValue = System.Reflection.Missing.Value;

        public static string strActivePrinter = null;
        public static string printerValue = null;
        public static string portName = null;
        public static RegistryKey subkey = null;

        public static string installedPrinter = null;

        public static int flagActive = 0;
        static Order currentOrder;
        static Timer timer;
        static Timer printTimer;
        static List<string> vendNames;
        static OdbcConnection pSqlConn;
        static List<string> heldPrinters;
        static List<string> renoCodes;
        static List<string> txCodes;

        public OrderPrintService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// when the service is initialized
        /// set initial formatting for the excel pages
        /// add vendors to list
        /// write start to log file
        /// initialize timer
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            heldPrinters = new List<string>();

            ///set vendor names
            vendNames = new List<string>() 
            { 
                "MERIDIAN",
                "RADEXPRESS",
                "CUMMINGS",
                "800DEPOT",
                "THERADSTOR",
                "OMEGA",
                "MEI"
            };

            ///set reno codes
            renoCodes = new List<string>()
            {
                "RENO",
                "SPARKS",
                "SPARKS2"
            };

            ///set texas codes
            txCodes = new List<string>()
            {
                "FORT WORTH",
                "TX CONSIGN"
            };

            ///Temporarily Prevent new instances of Excel from using this applications workbook
            xlApp.IgnoreRemoteRequests = true;
            ///Hide this instance of excel
            xlApp.ScreenUpdating = false;
            xlApp.Visible = false;

            ///Create excel template
            xlsheet = xlWorkBook.Worksheets["Sheet1"];
            xlsheet.get_Range("A1", "L1").ColumnWidth = 8.30;
            xlsheet.get_Range("A1", "L1").Font.Name = "Calibri";
            xlsheet.get_Range("A1", "L1").Font.Size = 11;
            xlsheet.get_Range("A1", "L49").NumberFormat = "@";
            xlsheet.get_Range("A25", "L49").Font.Size = 10;

            xlsheet.PageSetup.TopMargin = xlApp.InchesToPoints(.25);
            xlsheet.PageSetup.BottomMargin = xlApp.InchesToPoints(.25);
            xlsheet.PageSetup.RightMargin = xlApp.InchesToPoints(.25);
            xlsheet.PageSetup.LeftMargin = xlApp.InchesToPoints(.25);

            xlsheet.Cells[1, 1].ColumnWidth = 8.00;
            xlsheet.Cells[1, 2].ColumnWidth = 7;
            xlsheet.Cells[1, 3].ColumnWidth = 8.29;
            xlsheet.Cells[1, 4].ColumnWidth = 5.57;
            xlsheet.Cells[1, 5].ColumnWidth = 5;
            xlsheet.Cells[1, 6].ColumnWidth = 10;
            xlsheet.Cells[1, 7].ColumnWidth = 7;
            xlsheet.Cells[1, 8].ColumnWidth = 8;
            xlsheet.Cells[1, 9].ColumnWidth = 8.29;
            xlsheet.Cells[1, 10].ColumnWidth = 10;
            xlsheet.Cells[1, 11].ColumnWidth = 8.29;
            xlsheet.Cells[1, 12].ColumnWidth = 6;

            ///Header layout
            xlsheet.get_Range("A1", "H22").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("I4", "K9").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("H21", "K22").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("A17", "H22").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("K21", "K22").HorizontalAlignment = XlHAlign.xlHAlignLeft;

            xlsheet.get_Range("J15", "L15").HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlsheet.get_Range("J50", "L50").HorizontalAlignment = XlHAlign.xlHAlignRight;

            xlsheet.get_Range("A24", "L24").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("A24", "L24").Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d;

            ///BARCODE SECTIONS
            xlBarcodeTop = xlsheet.get_Range("I1", "L3");
            xlBarcodeTop.Merge();
            xlBarcodeTop.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlBarcodeTop.VerticalAlignment = XlVAlign.xlVAlignCenter;
            xlBarcodeTop.Font.Name = "Free 3 of 9";
            xlBarcodeTop.Font.Size = 40;

            xlBarcodeBottom = xlsheet.get_Range("I18", "L20");
            xlBarcodeBottom.Merge();
            xlBarcodeBottom.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlBarcodeBottom.VerticalAlignment = XlVAlign.xlVAlignCenter;
            xlBarcodeBottom.Font.Name = "Free 3 of 9";
            xlBarcodeBottom.Font.Size = 40;

            ///Item Details
            xlsheet.get_Range("A25", "A49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("B24", "B49").HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlsheet.get_Range("C25", "C49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("F25", "F49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("H25", "H49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("K24", "L49").HorizontalAlignment = XlHAlign.xlHAlignRight;

            ///BOTTOM Header Section
            xlsheet.Cells[17, 1].Value = "Ship To:";

            xlsheet.Cells[17, 5].Value = "PickNote:";
            xlsheet.Cells[18, 5].Value = "Date:";
            xlsheet.Cells[19, 5].Value = "Entered By:";
            xlsheet.Cells[20, 5].Value = "Account:";

            xlsheet.Cells[21, 9].Value = "CustomerPO:";
            xlsheet.Cells[22, 9].Value = "Location:";
            xlsheet.Cells[23, 9].Value = "Delivery Method:";

            xlsheet.Cells[17, 10].Value = "Pack List";
            xlsheet.Cells[17, 10].Font.Size = 28;
            xlsheet.Cells[17, 10].Font.Bold = true;
            xlsheet.get_Range("A17").RowHeight = 50;

            xlsheet.get_Range("A24", "L24").Font.Bold = true;
            xlsheet.Cells[24, 1].Value = "Location";
            xlsheet.Cells[24, 2].Value = "Qty";
            xlsheet.Cells[24, 3].Value = "Part No";
            xlsheet.Cells[24, 6].Value = "Cust Part No";
            xlsheet.Cells[24, 8].Value = "Description";

            writeToFile("Service Started");

            ///set tick timer
            timer = new System.Timers.Timer(15000);
            timer.Elapsed += new ElapsedEventHandler(timerProgram);
            timer.Enabled = true;

            ///set printer timer
            printTimer = new System.Timers.Timer(600000);
            printTimer.Elapsed += new ElapsedEventHandler(printCheckTick);
            printTimer.Enabled = true;
            printTimer.Stop();
        }

        /// <summary>
        /// clears trouble printers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printCheckTick(object sender, ElapsedEventArgs e)
        {
            ///stop timer
            printTimer.Stop();

            if(heldPrinters.Count > 0)
            {
                try
                { 
                    ///for each trouble printer
                    foreach (string printer in heldPrinters)
                    {
                        string strConnection = "DSN=RANSHU";
                        OdbcConnection sqlConn = null;
                        using (sqlConn = new OdbcConnection(strConnection))
                        {
                            ///remove printer from trouble table
                            string sqlInvoice = "delete from wmsTrouble " +
                            "where trouble_printer = '" + printer + "'";

                            using (OdbcCommand cmd = new OdbcCommand(sqlInvoice, sqlConn))
                            {
                                sqlConn.Open();
                                cmd.ExecuteNonQuery();
                                heldPrinters.Remove(printer);
                                sqlConn.Close();
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    ///report errors to IT
                    SendEmail("ryan@ranshu.com", "Ranshu print service error: Error correcting print failure.", ex.StackTrace + " " + ex.Message + heldPrinters[0]);
                }
            }
        }

        /// <summary>
        /// forcibly releases objects from memory
        /// </summary>
        /// <param name="obj">
        /// garbage to collect
        /// </param>
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                writeToFile("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// checks for new orders
        /// sends error reports when exceptions occur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerProgram(object sender, ElapsedEventArgs e)
        {
            ///stop the timer
            timer.Stop();


            try
            {
                ///check for new orders
                newOrders();
            }
            catch(Exception ex)
            {
                ///report error to IT
                string error = new StackTrace(ex, true).ToString() + " " + ex.Message;
                writeToFile(error);
                if (currentOrder != null)
                {
                    SendEmail("ryan@ranshu.com", "Ranshu print service error: Failure on invoice: " + currentOrder.invoiceNumber, error);

                    if (pSqlConn.State == ConnectionState.Open)
                        pSqlConn.Close();
                    string strConnection = "DSN=RANSHU";
                    pSqlConn = null;
                    using (pSqlConn = new OdbcConnection(strConnection))
                    {
                        ///get all new invoices
                        string sqlInvoice = "INSERT INTO wmsTrouble (trouble_invoice) VALUES (" + currentOrder.invoiceNumber + ")";
                        OdbcCommand cmd = new OdbcCommand(sqlInvoice, pSqlConn);
                        pSqlConn.Open();
                        cmd.ExecuteNonQuery();
                        pSqlConn.Close();
                    }

                    timer.Start();
                }
                else
                {
                    SendEmail("ryan@ranshu.com", "Ranshu print service error: URGENT: System Failure", error);
                }

                ///stop cycle
                return;
            }

            ///if there are printer errors
            if (heldPrinters.Count > 0)
            {
                ///start printer timer
                printTimer.Start();
            }

            ///collect garbage and start timer
            GC.Collect();
            timer.Start();
        }

        /// <summary>
        /// prints any amazon labels in the queue
        /// NOT IN USE
        /// </summary>
        static void printSFP()
        {
            ///create database connection
            string strConnection = "DSN=RANSHU";
            pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                ///get amazon orders marked for print
                string sqlGet = "select b.BKAR_INV_CUSORD, w.invoice_num, w.label_printer from wmsOrders w inner join BKARHINV b on w.invoice_num = b.BKAR_INV_NUM where w.print_flag = 1 and b.BKAR_INV_SHPVIA like '%SFP%'";
                OdbcCommand cmd = new OdbcCommand(sqlGet, pSqlConn);
                cmd.CommandTimeout = 90;
                ///open database connection
                pSqlConn.Open();
                OdbcDataReader sfpReader =  cmd.ExecuteReader();

                if(sfpReader.HasRows)
                {
                    while(sfpReader.Read())
                    {
                        ///read invoice details 
                        string labelName = sfpReader["BKAR_INV_CUSORD"].ToString().TrimEnd() + ".zpl";
                        string path = @"\\RANSHU\Reports\edi\MERIDIAN\SFPLabels\" + labelName;
                        string archivePath = @"\\RANSHU\Reports\edi\MERIDIAN\SFPLabels\Archive\" + DateTime.Now.Year + "\\" + labelName;
                        string printer = sfpReader["label_printer"].ToString().TrimEnd();

                        if (printer == null || printer == "")
                            printer = @"\\RANSHUPC\AMZN3844z";

                        try
                        {
                            if (File.Exists(path))
                            {
                                if (RawPrinterHelper.SendFileToPrinter(printer, path))
                                {
                                    ///report success to log
                                    writeToFile("Amazon label " + labelName + " printed");

                                    ///check archive for file
                                    if (!File.Exists(archivePath))
                                    {
                                        ///move file to archive
                                        File.Move(path, archivePath);
                                    }

                                    ///clear print flag
                                    using (OdbcCommand updateCmd = new OdbcCommand("update wmsOrders set print_flag = 0 where invoice_num = " + sfpReader["invoice_num"].ToString().TrimEnd(), pSqlConn))
                                    {
                                        updateCmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    throw new Exception("System failed to print " + labelName);
                                }
                            }
                            else if(File.Exists(archivePath))
                            {
                                ///clear print flag
                                using (OdbcCommand updateCmd = new OdbcCommand("update wmsOrders set print_flag = 0 where invoice_num = " + sfpReader["invoice_num"].ToString().TrimEnd(), pSqlConn))
                                {
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                throw new Exception("SFP Label " + labelName + " does not exist.");
                            }
                        }
                        catch(Exception ex)
                        {
                            ///clear print flag
                            using (OdbcCommand updateCmd = new OdbcCommand("update wmsOrders set print_flag = 0 where invoice_num = " + sfpReader["invoice_num"].ToString().TrimEnd(), pSqlConn))
                            {
                                updateCmd.ExecuteNonQuery();
                            }

                            ///report error to IT
                            string error = new StackTrace(ex, true).ToString() + " " + ex.Message;
                            writeToFile(error);

                            SendEmail("ryan@ranshu.com", "Ranshu print service error: Error printing SFP Label", error, new List<string> { "della@ranshu.com", "jeff@ranshu.com" });
                        }

                    }
                }

                pSqlConn.Close();
            }
        }
        
        /// <summary>
        /// retrieves new invoices from database
        /// prints each invoice to the correct location
        /// updates database accordingly
        /// </summary>
        static void newOrders()
        {
            int test = 0;
            flagActive = 1;

            string sqlUpdateList;
            string strCurrentDateTime;

            ///holds current location code
            string strInvLoc;

            string[] arrNotes = new string[10];
            currentOrder = null;

            ///list of orders to print
            List<Order> orders = new List<Order>();

            ///connect to database
            string strConnection = "DSN=RANSHU";
            pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                ///get new posted invoices
                OdbcCommand sqlCommandINV = new OdbcCommand("{call getOrders}", pSqlConn);
                sqlCommandINV.CommandType = CommandType.StoredProcedure;
                sqlCommandINV.CommandTimeout = 90;
                pSqlConn.Open();

                OdbcDataReader readerINV = sqlCommandINV.ExecuteReader();

                ///if reader has rows
                if (readerINV.HasRows)
                {
                    ///read row
                    while (readerINV.Read() && test < 10)
                    {
                        ///increment print set
                        test++;

                        ///create order with invoice number
                        Order order = new Order(Convert.ToInt32(readerINV["BKAR_INV_NUM"].ToString()));
                        order.date = String.Format("{0:MM/dd/yyyy}", readerINV["BKAR_INV_INVDTE"]);
                        order.customerCode = readerINV["BKAR_INV_CUSCOD"].ToString();

                        ///populate shipping address
                        order.billAddress.name = readerINV["BKAR_INV_CUSNME"].ToString().TrimEnd();
                        order.billAddress.line1 = readerINV["BKAR_INV_CUSA1"].ToString();
                        order.billAddress.line2 = readerINV["BKAR_INV_CUSA2"].ToString();
                        order.billAddress.city = readerINV["BKAR_INV_CUSCTY"].ToString().TrimEnd();
                        order.billAddress.state = readerINV["BKAR_INV_CUSST"].ToString().TrimEnd();
                        order.billAddress.zipcode = readerINV["BKAR_INV_CUSZIP"].ToString().TrimEnd();
                        order.billAddress.country = readerINV["BKAR_INV_CUSCUN"].ToString().TrimEnd();

                        ///populate billing address
                        order.shipAddress.name = readerINV["BKAR_INV_SHPNME"].ToString().TrimEnd();
                        order.shipAddress.line1 = readerINV["BKAR_INV_SHPA1"].ToString();
                        order.shipAddress.line2 = readerINV["BKAR_INV_SHPA2"].ToString();
                        order.shipAddress.city = readerINV["BKAR_INV_SHPCTY"].ToString().TrimEnd();
                        order.shipAddress.state = readerINV["BKAR_INV_SHPST"].ToString().TrimEnd();
                        order.shipAddress.zipcode = readerINV["BKAR_INV_SHPZIP"].ToString().TrimEnd();
                        order.shipAddress.country = readerINV["BKAR_INV_SHPCUN"].ToString().TrimEnd();

                        ///populate details
                        order.location = readerINV["BKAR_INV_LOC"].ToString().TrimEnd();
                        order.customerPO = readerINV["BKAR_INV_CUSORD"].ToString().TrimEnd();
                        order.deliveryMethod = readerINV["BKAR_INV_SHPVIA"].ToString().TrimEnd();
                        order.paymentTerms = readerINV["BKAR_INV_TERMD"].ToString().TrimEnd();
                        order.enteredBy = readerINV["BKAR_INV_ENTBY"].ToString().TrimEnd();
                        order.salesPerson = Convert.ToInt32(readerINV["BKAR_INV_SLSP"].ToString());
                        order.subTotal = Convert.ToDouble(readerINV["BKAR_INV_SUBTOT"].ToString());
                        order.total = Convert.ToDouble(readerINV["BKAR_INV_TOTAL"].ToString());
                        order.tax = Convert.ToDouble(readerINV["BKAR_INV_TAXAMT"].ToString());
                        order.freight = Convert.ToDouble(readerINV["BKAR_INV_FRGHT"].ToString());

                        ///checks if order is reprint
                        using (OdbcCommand reprintcmd = new OdbcCommand("call checkReprint(?)", pSqlConn))
                        {
                            reprintcmd.CommandType = CommandType.StoredProcedure;
                            reprintcmd.Parameters.AddWithValue(":invNum", order.invoiceNumber);

                            OdbcDataReader xReader = reprintcmd.ExecuteReader();
                            if(xReader.HasRows)
                            {
                                order.reprint = true;
                                if (xReader["label_printer"] != DBNull.Value && xReader["label_printer"] != null && (string)xReader["label_printer"] != "")
                                    order.reprintLoc = ((string)xReader["label_printer"]).TrimEnd();
                            }
                            else
                            {
                                order.reprint = false;
                            }
                        }

                        ///checks if order has been processed on the credit table
                        using (OdbcCommand creditcmd = new OdbcCommand("call checkPostCred(?)", pSqlConn))
                        {
                            creditcmd.CommandType = CommandType.StoredProcedure;
                            creditcmd.Parameters.AddWithValue(":invNum", order.invoiceNumber);
                            if (creditcmd.ExecuteScalar() != null)
                            {
                                order.postCred = true;
                            }
                            else
                            {
                                order.postCred = false;
                            }
                        }

                        ///order has been posted
                        order.posted = true;

                        ///add order to list
                        orders.Add(order);
                    }
                }

                ///close database connections
                readerINV.Close();

                ///get new truck orders
                using (OdbcCommand truckCmd = new OdbcCommand("{call getTruckOrders}", pSqlConn))
                {
                    truckCmd.CommandType = CommandType.StoredProcedure;
                    OdbcDataReader truckReader = truckCmd.ExecuteReader();

                    ///if reader has rows
                    if (truckReader.HasRows)
                    {
                        ///read row
                        while (truckReader.Read() && test < 10)
                        {
                            //increment print set
                            test++;

                            ///create order with invoice number
                            Order order = new Order(Convert.ToInt32(truckReader["BKAR_INV_NUM"].ToString()));
                            order.date = DateTime.Today.ToString("MM/dd/yyyy");
                            order.customerCode = truckReader["BKAR_INV_CUSCOD"].ToString();

                            ///populate shipping address
                            order.billAddress.name = truckReader["BKAR_INV_CUSNME"].ToString().TrimEnd();
                            order.billAddress.line1 = truckReader["BKAR_INV_CUSA1"].ToString();
                            order.billAddress.line2 = truckReader["BKAR_INV_CUSA2"].ToString();
                            order.billAddress.city = truckReader["BKAR_INV_CUSCTY"].ToString().TrimEnd();
                            order.billAddress.state = truckReader["BKAR_INV_CUSST"].ToString().TrimEnd();
                            order.billAddress.zipcode = truckReader["BKAR_INV_CUSZIP"].ToString().TrimEnd();
                            order.billAddress.country = truckReader["BKAR_INV_CUSCUN"].ToString().TrimEnd();

                            ///populate billing address
                            order.shipAddress.name = truckReader["BKAR_INV_SHPNME"].ToString().TrimEnd();
                            order.shipAddress.line1 = truckReader["BKAR_INV_SHPA1"].ToString();
                            order.shipAddress.line2 = truckReader["BKAR_INV_SHPA2"].ToString();
                            order.shipAddress.city = truckReader["BKAR_INV_SHPCTY"].ToString().TrimEnd();
                            order.shipAddress.state = truckReader["BKAR_INV_SHPST"].ToString().TrimEnd();
                            order.shipAddress.zipcode = truckReader["BKAR_INV_SHPZIP"].ToString().TrimEnd();
                            order.shipAddress.country = truckReader["BKAR_INV_SHPCUN"].ToString().TrimEnd();

                            ///populate details
                            order.location = truckReader["BKAR_INV_LOC"].ToString().TrimEnd();
                            order.customerPO = truckReader["BKAR_INV_CUSORD"].ToString().TrimEnd();
                            order.deliveryMethod = truckReader["BKAR_INV_SHPVIA"].ToString().TrimEnd();
                            order.paymentTerms = truckReader["BKAR_INV_TERMD"].ToString().TrimEnd();
                            order.enteredBy = truckReader["BKAR_INV_ENTBY"].ToString().TrimEnd();
                            order.salesPerson = Convert.ToInt32(truckReader["BKAR_INV_SLSP"].ToString());
                            order.subTotal = Convert.ToDouble(truckReader["BKAR_INV_SUBTOT"].ToString());
                            order.total = Convert.ToDouble(truckReader["BKAR_INV_TOTAL"].ToString());
                            order.tax = Convert.ToDouble(truckReader["BKAR_INV_TAXAMT"].ToString());
                            order.freight = Convert.ToDouble(truckReader["BKAR_INV_FRGHT"].ToString());
                            ///checks if order has been processed on the credit table
                            order.postCred = false;
                            ///checks if order is reprint
                            order.reprint = false;

                            ///add order to list
                            orders.Add(order);
                        }
                    }

                    ///close database connections
                    truckReader.Close();
                }

                foreach (Order order in orders)
                { 
                    ///format item list
                    xlsheet.get_Range("A25", "L49").Font.Size = 10;
                    xlsheet.get_Range("I21", "L22").Font.Size = 10;
                    xlsheet.get_Range("I4", "L9").Font.Size = 10;
                    xlsheet.get_Range("E17", "H20").Font.Size = 10;

                    ///variable declaration
                    bool invoice = false;


                    ///create new notes for order
                    List<string> notes = new List<string>();
                    List<string> crdtNotes = new List<string>();


                    ///check for amazon order
                    if (order.deliveryMethod.Contains("SFP"))
                    {
                        notes.Add("AMAZON PRIME SHIPMENT");
                        crdtNotes.Add("AMAZON PRIME SHIPMENT");
                    }

                    ///set currentOrder
                    currentOrder = order;

                    ///if account is vendor
                    if (vendNames.Contains(order.customerCode))
                        //order is not invoice
                        invoice = false;
                    ///if order is going to purchaser
                    else if (order.billAddress == order.shipAddress)
                        //order is invoice
                        invoice = true;

                    ///get items for order
                    int flagPrint = 1;
                    string cmdStr = "{call getItems (?)}";

                    ///check if truck order
                    if (!order.posted)
                        cmdStr = "{call getTruckItems (?)}";

                    using (OdbcCommand sqlCommandItems = new OdbcCommand(cmdStr, pSqlConn))
                    {
                        sqlCommandItems.CommandType = CommandType.StoredProcedure;
                        sqlCommandItems.Parameters.AddWithValue(":invNum", order.invoiceNumber);
                        OdbcDataReader readerItems = sqlCommandItems.ExecuteReader();
                        if (readerItems.HasRows)
                        {
                            while (readerItems.Read())
                            {
                                ///populate new item
                                Item item = new Item();
                                item.partCode = readerItems["BKAR_INVL_PCODE"].ToString();
                                item.itemType = readerItems["BKAR_INVL_ITYPE"].ToString();
                                item.description = readerItems["BKAR_INVL_PDESC"].ToString();
                                item.message = readerItems["BKAR_INVL_MSG"].ToString().TrimEnd().TrimStart();
                                item.locationCode = readerItems["BKAR_INVL_LOC"].ToString().TrimEnd();
                                item.vendorPart = readerItems["BKIC_VND_PART"].ToString();
                                item.price = readerItems["BKAR_INVL_PEXT"].ToString();

                                /// Check for shipping Notes
                                if (item.itemType == "X" && item.message.Length >= 1)
                                {
                                    ///if order note mark present
                                    if (item.message.Substring(0, 1) == "@")
                                    {
                                        ///add message to top notes
                                        notes.Add(item.message.TrimStart('@').ToUpper());
                                        crdtNotes.Add(item.message.TrimStart('@').ToUpper());
                                        item.message = "";
                                    }
                                    else if(item.message.Contains("ARS"))
                                    {
                                        notes.Add(item.message);
                                        crdtNotes.Add(item.message);
                                        item.message = "";
                                    }
                                    else
                                    {
                                        ///add message to credit notes
                                        crdtNotes.Add(item.message);
                                    }

                                    ///if message declares invoice
                                    if (((item.message.ToUpper() == "INVOICE") || notes.Contains("INVOICE")) && !(crdtNotes.Contains("NO INVOICE") || notes.Contains("NO INVOICE")))
                                        ///order is invoice
                                        invoice = true;
                                    else if (item.message.ToUpper().Contains("NO INVOICE") || item.message.ToUpper().Contains("BLIND") || crdtNotes.Contains("NO INVOICE") || notes.Contains("NO INVOICE"))
                                        invoice = false;
                                }
                                else if(item.itemType != "N" && item.itemType != "X" && item.partCode != "" && item.partCode != null)
                                {
                                    ///if item missing location code
                                    if(item.locationCode == "" || item.locationCode == null)
                                    {
                                        ///set location code to order location
                                        item.locationCode = order.location;
                                    }///check if invoice is multi-location
                                    else if ((item.locationCode != order.location) && !notes.Contains("ML"))
                                    {
                                        notes.Add("ML");
                                    }

                                    ///check for g-parts
                                    if (item.partCode.StartsWith("G"))
                                        notes.Add(item.partCode);

                                    ///if regular item set quantity
                                    item.quantity = Convert.ToInt32(Convert.ToDouble(readerItems["BKAR_INVL_PQTY"].ToString()));

                                    try
                                    {
                                        ///get part location
                                        using (OdbcCommand sqlCommandLocations = new OdbcCommand("{call getPartLocation(?, ?, ?)}", pSqlConn))
                                        {
                                            sqlCommandLocations.CommandType = CommandType.StoredProcedure;
                                            sqlCommandLocations.Parameters.AddWithValue(":inPart", item.partCode);
                                            sqlCommandLocations.Parameters.AddWithValue(":inLoc", item.locationCode);
                                            sqlCommandLocations.Parameters.AddWithValue(":qty", item.quantity);

                                            OdbcDataReader readerLocations = sqlCommandLocations.ExecuteReader();
                                            if (readerLocations.HasRows)
                                            {
                                                while (readerLocations.Read())
                                                {
                                                    item.location = new Location(readerLocations["INV_BIN"].ToString().TrimEnd(), "", 0);
                                                    if (readerLocations["INV_TYPE"].ToString().TrimEnd() == "OS")
                                                        item.location.name += "*";
                                                    else
                                                        item.location.name += " ";
                                                }

                                                if (item.location == null)
                                                {
                                                    item.location = new Location("", "", 0);
                                                }
                                            }
                                            else
                                            {
                                                item.location = new Location("", "", 0);
                                            }
                                            readerLocations.Close();
                                        }
                                    }
                                    catch
                                    {
                                        ///if error occurs do not print location
                                        item.location = new Location("", "", 0);
                                    }
                                }
                                else if (item.itemType == "N" || item.itemType == "X")
                                {
                                    item.locationCode = order.location;
                                }

                                ///check if invoice is for reno
                                if (renoCodes.Contains(item.locationCode))
                                    item.locationCode = "RENO";

                                ///if (txCodes.Contains(item.locationCode))
                                ///item.locationCode = "FORT WORTH";

                                ///add item to order
                                order.items.Add(item);
                            }
                        }
                        readerItems.Close();
                    }

                    ///print price information for local sales orders
                    if (order.deliveryMethod == "DELIVERY" || order.deliveryMethod == "WILL CALL")
                        invoice = true;

                    ///create price header label
                    Range price, priceHead = xlsheet.get_Range("K24", "L24");
                    priceHead.Merge();

                    ///if invoice
                    if (invoice && order.posted)
                    {
                        ///show header
                        xlsheet.Cells[17, 10].Value = "Ranshu";
                        priceHead.Value = "Price";
                    }
                    else
                    {
                        ///hide header
                        priceHead.Value = "";
                        xlsheet.Cells[17, 10].Value = "Pack List";
                    }

                    ///if order is cash on demand
                    if (order.paymentTerms.Contains("COD"))
                    {
                        double credit = 0.0f;

                        ///add to notes
                        order.paymentTerms = order.paymentTerms + " $" + order.total.ToString("N2");
                        notes.Add(order.paymentTerms);

                        ///check for account credits
                        string creditString = "SELECT SUM(BKAR_INVT_AMTRM) as CREDIT from BKARINVT where BKAR_INVT_CODE = '" + order.customerCode + "' and BKAR_INVT_TYPE = 'C'";
                        using (OdbcCommand creditCommand = new OdbcCommand(creditString, pSqlConn))
                        {
                            try
                            {
                                credit = Convert.ToDouble(creditCommand.ExecuteScalar().ToString());
                            }
                            catch
                            {

                            }
                        }

                        ///if account has credit
                        if(credit < 0.0f && currentOrder.total > 0.0f)
                        {
                            ///note to shipping
                            notes.Add("PLEASE APPLY CREDITS");
                            if (currentOrder.location == "RENO")
                            {
                                SendEmail("ar@ranshu.com", "APPLY CREDITS " + currentOrder.invoiceNumber, "Please apply credits to invoice " + currentOrder.invoiceNumber + " on account " + currentOrder.customerCode);
                            }
                        }
                    }

                    ///create and populate credit notes string
                    string creditNotes = "";
                    for (int i = 0; i < crdtNotes.Count; i++)
                    {
                        creditNotes += System.Environment.NewLine + crdtNotes[i];
                    }

                    ///if order is paid by credit
                    if (order.paymentTerms == "VISA/MC" && !order.postCred && order.posted)
                    {

                        ///add to credit table with Processed flagged false
                        using (OdbcCommand creditCommand = new OdbcCommand("{call addCRDT (?, ?, ?, ?, ?, ?, ?)}", pSqlConn))
                        {
                            creditCommand.CommandType = CommandType.StoredProcedure;
                            creditCommand.Parameters.AddWithValue(":invNum", order.invoiceNumber);
                            creditCommand.Parameters.AddWithValue(":cusCode", order.customerCode);
                            creditCommand.Parameters.AddWithValue(":invDate", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                            creditCommand.Parameters.AddWithValue(":total", order.total);
                            creditCommand.Parameters.AddWithValue(":notes", creditNotes);
                            creditCommand.Parameters.AddWithValue(":delivery", order.deliveryMethod);
                            creditCommand.Parameters.AddWithValue(":salesPerson", order.salesPerson);
                            creditCommand.ExecuteNonQuery();
                        }

                        writeToFile("CREDIT");
                    }///if not credit or fax
                    else if (!(order.deliveryMethod == "FAX" || order.deliveryMethod == "RMT"))
                    {
                        ///reset pages
                        int numItems = 0;
                        int numPage = 1;

                        ///set starting location
                        if(order.reprintLoc != null)
                            strInvLoc = order.reprintLoc;
                        else
                            strInvLoc = order.location;

                        ///update excel template
                        ///TOP Header Section
                        xlBarcodeTop.Value = "*" + order.invoiceNumber + "*";

                        xlsheet.Cells[1, 1].Value = "Date:";
                        xlsheet.Cells[1, 2].value = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + " PST";

                        xlsheet.Cells[3, 1].Value = "Ship To:";
                        xlsheet.Cells[5, 1].value = order.shipAddress.name;
                        xlsheet.Cells[6, 1].value = order.shipAddress.line1;
                        xlsheet.Cells[7, 1].value = order.shipAddress.line2;
                        xlsheet.Cells[8, 1].value = order.shipAddress.city + ", " + order.shipAddress.state + "  " + order.shipAddress.zipcode;

                        xlsheet.Cells[3, 5].Value = "Bill To:";
                        xlsheet.Cells[5, 5].value = order.billAddress.name;
                        xlsheet.Cells[6, 5].value = order.billAddress.line1;
                        xlsheet.Cells[7, 5].value = order.billAddress.line2;
                        xlsheet.Cells[8, 5].value = order.billAddress.city + ", " + order.billAddress.state + "  " + order.billAddress.zipcode;

                        xlsheet.Cells[4, 9].Value = "PickNote:";
                        xlsheet.Cells[4, 11].value = order.invoiceNumber;
                        xlsheet.Cells[5, 9].Value = "Location:";
                        xlsheet.Cells[5, 11].value = strInvLoc;
                        xlsheet.Cells[6, 9].Value = "Customer PO:";
                        xlsheet.Cells[6, 11].value = order.customerPO;
                        xlsheet.Cells[7, 9].Value = "Account:";
                        xlsheet.Cells[7, 11].value = order.customerCode;
                        xlsheet.Cells[8, 9].Value = "Delivery Method:";
                        xlsheet.Cells[8, 11].Font.Bold = true;
                        xlsheet.Cells[8, 11].value = order.deliveryMethod;
                        xlsheet.Cells[8, 11].Font.Bold = true;

                        xlsheet.Cells[9, 9].Value = "Payment Terms:";
                        xlsheet.Cells[9, 11].value = order.paymentTerms;

                        xlsheet.Cells[13, 10].Value = "# of Boxes: __________";

                        xlsheet.Cells[10, 1].Value = "NOTES";
                        xlsheet.get_Range("A10", "G10").Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d;

                        ///Print Notes if any
                        int currentNoteRow = 11;
                        int currentNoteCol = 1;
                        /// IF CALIFORNIA SHIPTO THEN ADD PROP65 LABEL
                        if (order.shipAddress.state == "CA")
                        {
                            notes.Add("PROP 65 LABEL");
                        }

                        ///for all notes in list
                        for (int n = 0; n < notes.Count; n++)
                        {
                            if (currentNoteRow > 15)
                            {
                                currentNoteRow = 11;
                                currentNoteCol = 5;
                            }

                            ///add notes to printout
                            xlsheet.Cells[currentNoteRow, currentNoteCol].value = notes[n].ToString();
                            currentNoteRow++;
                        }

                        /// BOTTOM Header Section
                        xlBarcodeBottom.Value = "*" + order.invoiceNumber + "*";

                        xlsheet.Cells[18, 1].value = order.shipAddress.name;
                        xlsheet.Cells[19, 1].value = order.shipAddress.line1;
                        xlsheet.Cells[20, 1].value = order.shipAddress.line2;
                        xlsheet.Cells[21, 1].value = order.shipAddress.city + ", " + order.shipAddress.state + "  " + order.shipAddress.zipcode;

                        xlsheet.Cells[17, 7].value = order.invoiceNumber;
                        xlsheet.Cells[18, 7].value = order.date;
                        xlsheet.Cells[19, 7].value = order.enteredBy;
                        xlsheet.Cells[20, 7].value = order.customerCode;

                        xlsheet.Cells[21, 11].value = order.customerPO;
                        xlsheet.Cells[22, 11].value = strInvLoc;
                        xlsheet.Cells[23, 11].value = order.deliveryMethod;

                        /// ASSIGN PRINTER BASED OFF OF WAREHOUSE CODE
                        flagPrint = selectPrinter(strInvLoc, (order.deliveryMethod == "DELIVERY" || order.deliveryMethod == "WILL CALL"), (order.deliveryMethod == "SEE INSTRUCTION" || order.deliveryMethod == "LIFTGATE"));

                        ///stop printing on printer error
                        if (flagPrint == 0 && heldPrinters.Contains(installedPrinter))
                            break;

                        ///report printer to log
                        writeToFile(installedPrinter);

                        ///Dont print FAX
                        if (flagPrint == 1)
                        {
                            int numPageItems = 0;
                            int currentRow = 25;
                            double subCheck = 0.0f;
                            for (int x = 0; x < order.items.Count; x++)
                            {
                                Item item = order.items[x];

                                ///if reprint
                                if (order.reprint)
                                {
                                    ///label reprint
                                    xlsheet.Cells[1, 6].Value = "REPRINT";
                                }
                                else
                                {
                                    ///label nothing
                                    xlsheet.Cells[1, 6].Value = "";
                                }

                                switch(order.deliveryMethod)
                                {
                                    case "GSO":
                                        xlsheet.Cells[2, 6].Interior.Color = ColorTranslator.ToOle(Color.Black);
                                        xlsheet.Cells[2, 7].Interior.Color = ColorTranslator.ToOle(Color.Black);
                                        break;

                                    case "CALIF OVERNIGHT":
                                        xlsheet.Cells[2, 6].Interior.Color = ColorTranslator.ToOle(Color.Black);
                                        xlsheet.Cells[2, 7].Interior.Color = ColorTranslator.ToOle(Color.Black);
                                        break;

                                    default:
                                        xlsheet.Cells[2, 6].Interior.Color = ColorTranslator.ToOle(Color.FromArgb(255, 255, 255));
                                        xlsheet.Cells[2, 7].Interior.Color = ColorTranslator.ToOle(Color.FromArgb(255, 255, 255));
                                        break;
                                }

                                ///if item count at maximum and location is the same
                                if ((numPageItems > 19) && (strInvLoc == item.locationCode))
                                {
                                    ///note multipage
                                    xlsheet.Cells[46, 4].Value = "Continued on Next Page...";

                                    /// Print Out Current PackList
                                    if (flagPrint == 1 && numPageItems > 0)
                                    {
                                        xlsheet.get_Range("I21", "L22").Font.Size = 10;
                                        xlsheet.get_Range("I4", "L9").Font.Size = 10;
                                        xlsheet.get_Range("E17", "H20").Font.Size = 10;
                                        xlsheet.PrintOutEx(misValue, misValue, 1, false);

                                        if(order.deliveryMethod == "DELIVERY" || order.deliveryMethod == "WILL CALL")
                                        {
                                            xlsheet.PrintOutEx(misValue, misValue, 1, false);
                                        }
                                    }

                                    numPage = numPage + 1;
                                    numPageItems = 0;
                                    currentRow = 25;

                                    ///CLEAR OUT TOP Header Sections
                                    xlsheet.Cells[3, 5].Value = "";
                                    xlsheet.Cells[5, 5].value = "";
                                    xlsheet.Cells[6, 5].value = "";
                                    xlsheet.Cells[7, 5].value = "";
                                    xlsheet.Cells[8, 5].value = "";

                                    xlsheet.Cells[5, 9].Value = "";
                                    xlsheet.Cells[5, 11].value = "";
                                    xlsheet.Cells[6, 9].Value = "";
                                    xlsheet.Cells[6, 11].value = "";
                                    xlsheet.Cells[7, 9].Value = "";
                                    xlsheet.Cells[7, 11].value = "";
                                    xlsheet.Cells[9, 9].Value = "";
                                    xlsheet.Cells[9, 11].value = "";

                                    ///Clear out notes
                                    xlsheet.get_Range("A11", "E15").Clear();
                                    xlsheet.get_Range("A11", "E15").HorizontalAlignment = XlHAlign.xlHAlignLeft;

                                    ///Clear out previous line items
                                    xlsheet.get_Range("A25", "L49").Clear();
                                    xlsheet.get_Range("A25", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                    xlsheet.get_Range("B24", "B49").HorizontalAlignment = XlHAlign.xlHAlignCenter;

                                }
                                else if (strInvLoc != item.locationCode && (item.itemType != "X" && item.itemType != "N"))
                                {
                                    xlsheet.Cells[46, 4].Clear();
                                    xlsheet.Cells[47, 1].Value = "Total Qty";
                                    xlsheet.Cells[47, 2].Value = numItems.ToString();

                                    ///if invoice
                                    if (invoice && order.posted)
                                    {
                                        ///print price/payment details
                                        xlsheet.Cells[46, 4].Value = "Remit Payment to:";
                                        xlsheet.Cells[46, 7].Value = "Ranshu";
                                        xlsheet.Cells[47, 7].Value = "P.O. Box 913317";
                                        xlsheet.Cells[48, 7].Value = "Denver, CO 80291-3317";

                                        if (order.paymentTerms == "NET 5TH")
                                        {
                                            xlsheet.Cells[49, 5].Value = "Pay by the 5th and save $" + ((order.subTotal + order.tax) * .05f).ToString("N2");
                                        }

                                        if (subCheck < order.subTotal)
                                        {
                                            price = xlsheet.get_Range("K45", "L45");
                                            price.Merge();
                                            xlsheet.Cells[45, 8].Value = "Items shipped separately:";
                                            price.Value = "$" + order.subTotal.ToString("N2");
                                        }
                                        else
                                        {
                                            xlsheet.get_Range("H45", "L45").Clear();
                                        }
                                        xlsheet.Cells[46, 10].Value = "Subtotal:";
                                        price = xlsheet.get_Range("K46", "L46");
                                        price.Merge();
                                        price.Value = "$" + order.subTotal.ToString("N2");
                                        xlsheet.Cells[47, 10].Value = "Tax:";
                                        price = xlsheet.get_Range("K47", "L47");
                                        price.Merge();
                                        price.Value = "$" + order.tax.ToString("N2");
                                        xlsheet.Cells[48, 10].Value = "Freight:";
                                        price = xlsheet.get_Range("K48", "L48");
                                        price.Merge();
                                        price.Value = "$" + order.freight.ToString("N2");
                                        xlsheet.Cells[49, 10].Value = "Total:";
                                        price = xlsheet.get_Range("K49", "L49");
                                        price.Merge();
                                        price.Value = "$" + order.total.ToString("N2");
                                    }

                                    ///Print Out Current PackList
                                    if (flagPrint == 1 && numPageItems > 0)
                                    {
                                        xlsheet.get_Range("I21", "L22").Font.Size = 10;
                                        xlsheet.get_Range("I4", "L9").Font.Size = 10;
                                        xlsheet.get_Range("E17", "H20").Font.Size = 10;
                                        xlsheet.PrintOutEx(misValue, misValue, 1, false);
                                    }

                                    ///reset items
                                    numItems = 0;
                                    numPage = 1;
                                    numPageItems = 0;
                                    currentRow = 25;

                                    ///set new location code
                                    strInvLoc = item.locationCode;
                                    xlsheet.Cells[22, 11].value = strInvLoc;

                                    ///ASSIGN PRINTER BASED OFF OF WAREHOUSE CODE
                                    if (order.reprintLoc == strInvLoc || order.reprintLoc == null)
                                        flagPrint = selectPrinter(strInvLoc, (order.deliveryMethod == "DELIVERY" || order.deliveryMethod == "WILL CALL"), (order.deliveryMethod == "SEE INSTRUCTION" || order.deliveryMethod == "LIFTGATE"));
                                    else
                                        flagPrint = 0;

                                    xlsheet.Cells[3, 5].Value = "Bill To:";
                                    xlsheet.Cells[5, 5].value = order.billAddress.name;
                                    xlsheet.Cells[6, 5].value = order.billAddress.line1;
                                    xlsheet.Cells[7, 5].value = order.billAddress.line2;
                                    xlsheet.Cells[8, 5].value = order.billAddress.city + ", " + order.billAddress.state + "  " + order.billAddress.zipcode;

                                    xlsheet.Cells[4, 9].Value = "PickNote:";
                                    xlsheet.Cells[4, 11].value = order.invoiceNumber;
                                    xlsheet.Cells[5, 9].Value = "Location:";
                                    xlsheet.Cells[5, 11].value = strInvLoc;
                                    xlsheet.Cells[6, 9].Value = "Customer PO:";
                                    xlsheet.Cells[6, 11].value = order.customerPO;
                                    xlsheet.Cells[7, 9].Value = "Account:";
                                    xlsheet.Cells[7, 11].value = order.customerCode;
                                    xlsheet.Cells[8, 9].Value = "Delivery Method:";
                                    xlsheet.Cells[8, 11].Font.Bold = true;
                                    xlsheet.Cells[8, 11].value = order.deliveryMethod;
                                    xlsheet.Cells[8, 11].Font.Bold = true;
                                    xlsheet.Cells[9, 9].Value = "Payment Terms:";
                                    xlsheet.Cells[9, 11].value = order.paymentTerms;

                                    xlsheet.Cells[13, 10].Value = "# of Boxes: __________";

                                    xlsheet.Cells[10, 1].Value = "NOTES";
                                    xlsheet.get_Range("A10", "G10").Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d;

                                    ///Clear out previous line items
                                    xlsheet.get_Range("A25", "L49").Clear();
                                    xlsheet.get_Range("A25", "H49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                    xlsheet.get_Range("B24", "B49").HorizontalAlignment = XlHAlign.xlHAlignCenter;

                                }

                                ///page numbers
                                xlsheet.Cells[15, 11].value = "Page";
                                xlsheet.Cells[15, 12].value = numPage.ToString();

                                xlsheet.Cells[49, 1].value = "Page";
                                xlsheet.Cells[49, 2].value = numPage.ToString();

                                ///create price cell
                                price = xlsheet.get_Range("K" + currentRow, "L" + currentRow);
                                price.Merge();

                                ///if item is message
                                if (item.itemType == "X")
                                {
                                    xlsheet.Cells[currentRow, 4].value = "  " + item.message;
                                }///if item is note
                                else if (item.itemType == "N")
                                {
                                    if (!(item.partCode.ToUpper().Contains("PROP65")))
                                    {
                                        xlsheet.Cells[currentRow, 4].value = "  " + item.message;

                                        if (invoice && order.posted)
                                        {
                                            price.Value = "$" + Convert.ToDouble(item.price).ToString("N2");
                                            subCheck += Convert.ToDouble(item.price);
                                        }
                                        else
                                            price.Value = "";
                                    }
                                }
                                else
                                {
                                    xlsheet.Cells[currentRow, 1].numberFormat = "@";
                                    xlsheet.Cells[currentRow, 1].value = @"" + item.location.name;
                                    xlsheet.Cells[currentRow, 2].value = item.quantity;
                                    xlsheet.Cells[currentRow, 3].value = item.partCode;
                                    xlsheet.Cells[currentRow, 6].value = item.vendorPart.ToString();
                                    xlsheet.Cells[currentRow, 8].value = item.description;

                                    if (invoice && order.posted)
                                    {
                                        price.Value = "$" + Convert.ToDouble(item.price).ToString("N2");
                                        subCheck += Convert.ToDouble(item.price);
                                    }
                                    else if (!order.posted)
                                    {
                                        price.Value = "______";
                                    }
                                    else
                                        price.Value = "";

                                    numItems = numItems + item.quantity;

                                }

                                currentRow++;
                                numPageItems++;
                            }

                            xlsheet.Cells[46, 4].Clear();

                            xlsheet.Cells[47, 1].Value = "Total Qty";
                            xlsheet.Cells[47, 2].Value = numItems.ToString();

                            ///display invoice details
                            if (invoice && order.posted)
                            {
                                xlsheet.Cells[46, 4].Value = "Remit Payment to:";
                                xlsheet.Cells[46, 7].Value = "Ranshu";
                                xlsheet.Cells[47, 7].Value = "P.O. Box 913317";
                                xlsheet.Cells[48, 7].Value = "Denver, CO 80291-3317";

                                ///if net 5th display discount notice
                                if (order.paymentTerms == "NET 5TH")
                                {
                                    xlsheet.Cells[49, 5].Value = "Pay by the 5th and save $" + ((order.subTotal + order.tax) * .05f).ToString("N2");
                                }

                                ///display multi-invoice details
                                if (subCheck < order.subTotal)
                                {
                                    xlsheet.Cells[45, 8].Value = "Items shipped separately:";
                                    price = xlsheet.get_Range("K45", "L45");
                                    price.Merge();
                                    price.Value = "$" + (order.subTotal - subCheck).ToString("N2");
                                }
                                else
                                {
                                    xlsheet.get_Range("H45", "L45").Clear();
                                }
                                xlsheet.Cells[46, 10].Value = "Subtotal:";
                                price = xlsheet.get_Range("K46", "L46");
                                price.Merge();
                                price.Value = "$" + order.subTotal.ToString("N2");
                                xlsheet.Cells[47, 10].Value = "Tax:";
                                price = xlsheet.get_Range("K47", "L47");
                                price.Merge();
                                price.Value = "$" + order.tax.ToString("N2");
                                xlsheet.Cells[48, 10].Value = "Freight:";
                                price = xlsheet.get_Range("K48", "L48");
                                price.Merge();
                                price.Value = "$" + order.freight.ToString("N2");
                                xlsheet.Cells[49, 10].Value = "Total:";
                                price = xlsheet.get_Range("K49", "L49");
                                price.Merge();
                                price.Value = "$" + order.total.ToString("N2");
                            }

                            ///Print Out Final Page of Packlist
                            if (flagPrint == 1 && numPageItems > 0)
                            {
                                xlsheet.get_Range("I21", "L22").Font.Size = 10;
                                xlsheet.get_Range("I4", "L9").Font.Size = 10;
                                xlsheet.get_Range("E17", "H20").Font.Size = 10;
                                xlsheet.PrintOutEx(misValue, misValue, 1, false);

                                if (order.deliveryMethod == "DELIVERY" || order.deliveryMethod == "WILL CALL")
                                    xlsheet.PrintOutEx(misValue, misValue, 1, false);
                            }
                        }

                        ///if not a reprint
                        if (!order.reprint)
                        {
                            ///add order to wmsOrders
                            ///update invoice as printed
                            strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                            sqlUpdateList = "INSERT INTO wmsOrders (invoice_num,printed,printer, notes) " +
                                            "VALUES (" + order.invoiceNumber + ", '" + strCurrentDateTime.ToString() +
                                            "', '" + installedPrinter + "', '" + creditNotes.Replace("'", "") +"'); " +
                                            "UPDATE BKARHINV " +
                                            "SET BKAR_INV_MAX = 1 " +
                                            "WHERE BKAR_INV_NUM = " + order.invoiceNumber;
                            using (OdbcCommand cmd = new OdbcCommand(sqlUpdateList, pSqlConn))
                            {
                                cmd.CommandTimeout = 10;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            ///update invoice print time
                            strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                            sqlUpdateList = "UPDATE wmsOrders " +
                                            "SET printed = '" + strCurrentDateTime.ToString() + "', " +
                                            "printer = '" + installedPrinter + "', label_printer = null " +
                                            "WHERE invoice_num = " + order.invoiceNumber + ";" +
                                            "UPDATE BKARHINV " +
                                            "SET BKAR_INV_MAX = 1 " +
                                            "WHERE BKAR_INV_NUM = " + order.invoiceNumber;
                            using (OdbcCommand cmd = new OdbcCommand(sqlUpdateList, pSqlConn))
                            {
                                cmd.CommandTimeout = 10;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        ///if retail invoice
                        if(order.deliveryMethod == "DELIVERY" || order.deliveryMethod == "WILL CALL")
                        {
                            ///mark invoice as validated
                            strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                            sqlUpdateList = "UPDATE wmsOrders " +
                                            "SET validated = '" + strCurrentDateTime.ToString()+ "' " +
                                            "WHERE invoice_num = " + order.invoiceNumber;
                            using (OdbcCommand cmd = new OdbcCommand(sqlUpdateList, pSqlConn))
                            {
                                cmd.CommandTimeout = 10;
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }

                    ///mark invoice as printed
                    string sqlMarkPrint = "UPDATE BKARHINV " +
                                "SET BKAR_INV_MAX = 1 " +
                                "WHERE BKAR_INV_NUM = " + order.invoiceNumber;
                    using (OdbcCommand cmd = new OdbcCommand(sqlMarkPrint, pSqlConn))
                    {
                        cmd.CommandTimeout = 10;
                        cmd.ExecuteNonQuery();
                    }

                    if((order.deliveryMethod == "SEE INSTRUCTION" || order.deliveryMethod == "LIFTGATE") && order.paymentTerms != "VISA/MC" && !order.posted)
                    {
                        ///mark invoice as printed
                        sqlMarkPrint = "UPDATE BKARINV " +
                                    "SET BKAR_INV_MAX = 1 " +
                                    "WHERE BKAR_INV_SONUM = " + order.invoiceNumber;
                        using (OdbcCommand cmd = new OdbcCommand(sqlMarkPrint, pSqlConn))
                        {
                            cmd.CommandTimeout = 10;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    ///CLEAN UP LINE ITEMS AND EXTRA PAGES
                    xlsheet.get_Range("A25", "L49").Clear();

                    ///CLEAN UP NOTES
                    xlsheet.get_Range("A11", "E15").Clear();

                    ///ALIGN CELLS PROPERLY
                    xlsheet.get_Range("A25", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    xlsheet.get_Range("B24", "B49").HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    writeToFile(order.invoiceNumber.ToString());
                }

                pSqlConn.Close();
            }

            ///denote tick
            writeToFile("END SET");
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
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\"+DateTime.Now.Year;
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            ///open/create log file
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Year+"\\RanshuPrintService_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if(!File.Exists(filePath))
            {
                using(StreamWriter sw = File.CreateText(filePath))
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
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.Year + "\\RanshuPrintService_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filePath))
            {
                return;
            }
            else
            {
                ///read file to list
                List<string> rawFile = File.ReadAllLines(filePath).ToList<string>();

                ///for each log line
                for(int i = 0; i < rawFile.Count; i++)
                {
                    ///if line contatins "END SET"
                    if(rawFile[i].Contains("END SET"))
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
        /// Select printer based on location
        /// mark as retail if retail
        /// find installed printer
        /// check selected printer for errors
        /// </summary>
        /// <param name="locationcode">
        /// warehouse location code
        /// </param>
        /// <param name="retail">
        /// retail flag
        /// </param>
        /// <returns>
        /// printerfound flag
        /// </returns>
        public static int selectPrinter(string locationcode, bool retail, bool ltl)
        {
            int flagPrinterFound = 0;
            string retailFlag = "";
            string backupPrinter;
            installedPrinter = null;

            ///ASSIGN PRINTER BASED OFF OF WAREHOUSE CODE
            if(retail)
            {
                installedPrinter = "RETAIL " + locationcode;
                retailFlag = "RETAIL";
            }

            if(ltl)
            {
                installedPrinter = "LTL " + locationcode;
                return 0;
            }
            else
            {
                string printerSql = "select printer_name from wmsPrinters where printer_location = '" + retailFlag + locationcode + "'";

                using (OdbcCommand cmd = new OdbcCommand(printerSql, pSqlConn))
                {
                    try
                    {
                        installedPrinter = cmd.ExecuteScalar().ToString().TrimEnd();
                    }
                    catch
                    {
                        writeToFile("Printer at " + locationcode + " not installed");
                    }
                }
            }

            ///if installed printer is valid
            if (installedPrinter != null && installedPrinter != "" && !installedPrinter.Contains("RETAIL"))
            {
                try
                {
                    ///GRAB REGISTRY KEY NAME/VALUE PAIRS FOR ALL PRINTERS INSTALLED ON MACHINE
                    ///SEARCH FOR NAME/VALUE PAIR WITH installedPrinter AS THE NAME AND EXTRACT THE PORT NAME FROM IT
                    ///USE THE PORT NAME TO SPECIFY THE ACTIVE PRINTER IN THE EXCEL INSTANCE
                    subkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Devices", false);
                    foreach (string printerName in subkey.GetValueNames())
                    {
                        if (printerName == installedPrinter)
                        {
                            ///checks for errors on the installed printer
                            PrintServer printServer = new PrintServer(installedPrinter);
                            PrintQueueCollection printQueues = printServer.GetPrintQueues();
                            foreach (PrintQueue pq in printQueues)
                            {
                                pq.Refresh();
                                PrintJobInfoCollection pCollection = pq.GetPrintJobInfoCollection();
                                if (!heldPrinters.Contains(pq.Name) && pq.Name != "PACKNV002" && ((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error || (pq.QueueStatus & PrintQueueStatus.None) != PrintQueueStatus.None))
                                {
                                    ///get all new invoices
                                    string sqlInvoice = "insert into wmsTrouble(trouble_printer) " +
                                        "select distinct printer_name from wmsPrinters " +
                                        "where printer_name = '" + pq.Name + "' " +
                                        "and printer_name not in (select trouble_printer " +
                                        "from wmsTrouble where trouble_printer = printer_name)";

                                    using (OdbcCommand cmd = new OdbcCommand(sqlInvoice, pSqlConn))
                                    {

                                        if (cmd.ExecuteNonQuery() > 0)
                                        {
                                            ///emails error report to IT
                                            string text = "There appears to be a problem printing order to " + pq.Name + ". ERROR: " + pq.QueueStatus.ToString();
                                            text += System.Environment.NewLine + "Please check the printer.";
                                            List<string> cc = new List<string>();
                                            switch(pq.Name)
                                            {
                                                case "RICOHNV":
                                                    cc.Add("della@ranshu.com");
                                                    cc.Add("jodi@ranshu.com");
                                                    cc.Add("tiffany@ranshu.com");
                                                    break;

                                                case "RICOHTX":
                                                    cc.Add("cheryn@ranshu.com");
                                                    cc.Add("stan@ranshu.com");
                                                    break;

                                                case "RICOHFL":
                                                    cc.Add("richie@ranshu.com");
                                                    break;

                                                case "LOCALSALESNV":
                                                    cc.Add("keith@ranshu.com");
                                                    cc.Add("amber@ranshu.com");
                                                    cc.Add("leeg@ranshu.com");
                                                    cc.Add("Jeremy@ranshu.com");
                                                    break;
                                            }

                                            SendEmail( "ryan@ranshu.com", "Ranshu print service error: Print Failure on " + pq.Name, text, cc);
                                        }

                                        heldPrinters.Add(pq.Name);
                                    }

                                    if(pq.Name == installedPrinter)
                                        return 0;
                                }
                                else if (heldPrinters.Contains(pq.Name) && !((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error || (pq.QueueStatus & PrintQueueStatus.Offline) == PrintQueueStatus.Offline))
                                {
                                    ///get all new invoices
                                    string sqlInvoice = "delete from wmsTrouble " +
                                        "where trouble_printer = '" + pq.Name + "'"; 
                                    
                                    using (OdbcCommand cmd = new OdbcCommand(sqlInvoice, pSqlConn))
                                    {
                                        cmd.ExecuteNonQuery();
                                        heldPrinters.Remove(pq.Name);
                                    }
                                }
                            }

                            printerValue = subkey.GetValue(printerName).ToString();
                            flagPrinterFound = 1;
                        }
                        else if (printerName == "RICOHNV")
                        {
                            backupPrinter = subkey.GetValue(printerName).ToString();
                        }
                    }

                    if (flagPrinterFound == 1)
                    {

                        string[] arrPrinterValue = printerValue.Split(',');
                        portName = arrPrinterValue[1];

                        xlApp.ActivePrinter = installedPrinter + " on " + portName;
                    }
                    else
                    {
                        writeToFile(installedPrinter + " not installed.");
                    }
                }
                catch (Exception ex)
                {
                    ///report error to IT
                    string error = new StackTrace(ex, true).ToString() + " " + ex.Message;
                    writeToFile(error);
                    SendEmail("ryan@ranshu.com", "Ranshu print service error: Error printing invoice: " + currentOrder, error);
                    return 0;
                }
            }

            return flagPrinterFound;
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
        static void SendEmail( string recipient, string subject, string msgText, List<string> cc = null)
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

        /// <summary>
        /// free floating excel memory
        /// condense log file
        /// stop the service
        /// </summary>
        protected override void OnStop()
        {
            ///Close down program
            ///Allow new instances of Excel from using this applications workbook
            OrderPrintService.xlApp.IgnoreRemoteRequests = false;

            OrderPrintService.xlWorkBook.Close(false, misValue, misValue);
            OrderPrintService.xlApp.Application.Quit();
            OrderPrintService.xlApp.Quit();

            releaseObject(xlsheet);
            releaseObject(OrderPrintService.xlWorkBook);
            releaseObject(OrderPrintService.xlApp);

            ///log stop to file and condense file
            writeToFile("Service Stopped");
            condenseFile();
        }
    }

    /// <summary>
    /// zebra printer helper class
    /// </summary>
    public class RawPrinterHelper
    {
        /// Structure and API declarions:
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

        /// SendBytesToPrinter()
        /// When the function is given a printer name and an unmanaged array
        /// of bytes, the function sends those bytes to the print queue.
        /// Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; ///Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";

            ///Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                ///Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    ///Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        ///Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            ///If you did not succeed, GetLastError may give more information
            ///about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        /// <summary>
        /// sends zpl file to desired printer
        /// </summary>
        /// <param name="szPrinterName">
        /// printer name
        /// </param>
        /// <param name="szFileName">
        /// file path
        /// </param>
        /// <returns>
        /// success boolean
        /// </returns>
        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            ///Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            ///Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            ///Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            ///Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            ///Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            ///Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            ///Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            ///Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            ///Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            fs.Close();
            return bSuccess;
        }

        /// <summary>
        /// senda command to zebra printer
        /// </summary>
        /// <param name="szPrinterName">
        /// label printer name
        /// </param>
        /// <param name="szString">
        /// command string
        /// </param>
        /// <returns>
        /// success flag
        /// </returns>
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            ///How many characters are in the string?
            dwCount = szString.Length;
            ///Assume that the printer is expecting ANSI text, and then convert
            ///the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            ///Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }
}
