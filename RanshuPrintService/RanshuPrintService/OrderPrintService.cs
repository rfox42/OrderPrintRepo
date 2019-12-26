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
    public partial class OrderPrintService : ServiceBase
    {

        // Create Excel Application Instance for use by Program Forms
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
        static bool REPRINT;
        static Timer timer;
        static List<string> vendNames;
        static OdbcConnection pSqlConn;
        static List<string> heldPrinters;

        public OrderPrintService()
        {
            InitializeComponent();
        }

        /*
         * @FUNCTION:   protected override void OnStart()
         * @PURPOSE:    when the service is initialized
         *              set initial formatting for the excel pages
         *              add vendors to list
         *              write start to log file
         *              initialize timer
         *              
         * @PARAM:      string[] args
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        protected override void OnStart(string[] args)
        {
            heldPrinters = new List<string>();

            //set vendor names
            vendNames = new List<string>() 
            { 
                "MERIDIAN",
                "RADEXPRESS",
                "CUMMINGS",
                "800DEPOT",
                "THERADSTOR",
                "OMEGA",
                "MEI",
            };

            // Temporarily Prevent new instances of Excel from using this applications workbook
            xlApp.IgnoreRemoteRequests = true;
            // Hide this instance of excel
            xlApp.ScreenUpdating = false;
            xlApp.Visible = false;

            //Create excel template
            xlsheet = xlWorkBook.Worksheets["Sheet1"];
            xlsheet.get_Range("A1", "L1").ColumnWidth = 8.30;
            xlsheet.get_Range("A1", "L1").Font.Name = "Calibri";
            xlsheet.get_Range("A1", "L1").Font.Size = 11;
            xlsheet.get_Range("A1", "L49").NumberFormat = "@";

            xlsheet.PageSetup.TopMargin = xlApp.InchesToPoints(.25);
            xlsheet.PageSetup.BottomMargin = xlApp.InchesToPoints(.25);
            xlsheet.PageSetup.RightMargin = xlApp.InchesToPoints(.25);
            xlsheet.PageSetup.LeftMargin = xlApp.InchesToPoints(.25);

            xlsheet.Cells[1, 1].ColumnWidth = 8.00;
            xlsheet.Cells[1, 2].ColumnWidth = 10.00;
            xlsheet.Cells[1, 3].ColumnWidth = 8.29;
            xlsheet.Cells[1, 4].ColumnWidth = 5.57;
            xlsheet.Cells[1, 5].ColumnWidth = 5;
            xlsheet.Cells[1, 6].ColumnWidth = 7;
            xlsheet.Cells[1, 7].ColumnWidth = 10.00;
            xlsheet.Cells[1, 8].ColumnWidth = 5.57;
            xlsheet.Cells[1, 9].ColumnWidth = 8.29;
            xlsheet.Cells[1, 10].ColumnWidth = 8.29;
            xlsheet.Cells[1, 11].ColumnWidth = 8.29;
            xlsheet.Cells[1, 12].ColumnWidth = 8.00;

            // Header layout
            xlsheet.get_Range("A1", "H22").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("I4", "K9").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("H21", "K22").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("A17", "H22").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("K21", "K22").HorizontalAlignment = XlHAlign.xlHAlignLeft;

            xlsheet.get_Range("J15", "L15").HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlsheet.get_Range("J50", "L50").HorizontalAlignment = XlHAlign.xlHAlignRight;

            xlsheet.get_Range("A24", "L24").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("A24", "L24").Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d;

            // BARCODE SECTIONS
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

            // Item Details
            xlsheet.get_Range("A25", "A49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("B24", "B49").HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlsheet.get_Range("C25", "C49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("F25", "F49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("H25", "H49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("K24", "L49").HorizontalAlignment = XlHAlign.xlHAlignRight;

            // BOTTOM Header Section
            xlsheet.Cells[17, 1].Value = "Ship To:";

            xlsheet.Cells[17, 5].Value = "PickNote:";
            xlsheet.Cells[18, 5].Value = "Date:";
            xlsheet.Cells[19, 5].Value = "Entered By:";
            xlsheet.Cells[20, 5].Value = "Account:";

            xlsheet.Cells[21, 8].Value = "Location:";
            xlsheet.Cells[22, 8].Value = "Delivery Method:";

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

            timer = new System.Timers.Timer(10000);
            timer.Elapsed += new ElapsedEventHandler(timerProgram);
            timer.Enabled = true;
        }

        /*
         * @FUNCTION:   private static void releaseObject()
         * @PURPOSE:    forcibly releases objects from memory
         *              
         * @PARAM:      object obj
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
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

        /*
         * @FUNCTION:   private static void timerProgram()
         * @PURPOSE:    checks for new orders
         *              sends error reports when exceptions occur
         *              
         * @PARAM:      object sender
         *              ElapsedEventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private static void timerProgram(object sender, ElapsedEventArgs e)
        {
            //stop the timer
            timer.Stop();

            try
            {
                //check for new orders
                newOrders();
            }
            catch(Exception ex)
            {
                //report error to IT
                string error = new StackTrace(ex, true).GetFrame(0).GetFileLineNumber() + " " + ex.Message;
                writeToFile(error);
                if (currentOrder != null)
                {
                    SendEmail("Failure on invoice: " + currentOrder.invoiceNumber, error);
                    if(pSqlConn.State == ConnectionState.Open)
                        pSqlConn.Close();
                    string strConnection = "DSN=RANSHU";
                    pSqlConn = null;
                    using (pSqlConn = new OdbcConnection(strConnection))
                    {
                        //get all new invoices
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
                    SendEmail("URGENT: System Failure", error);
                }

                //stop cycle
                return;
            }

            GC.Collect();
            timer.Start();
        }

        /*
         * @FUNCTION:   public static void newOrders()
         * @PURPOSE:    retrieves new invoices from database
         *              prints each invoice to the correct location
         *              updates database accordingly
         *              
         * @PARAM:      none
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        static void newOrders()
        {
            int test = 0;
            flagActive = 1;

            string sqlUpdateList;
            string strCurrentDateTime;

            string strInvLoc;

            string[] arrNotes = new string[10];
            currentOrder = null;

            //connect to database
            string strConnection = "DSN=RANSHU";
            pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                OdbcCommand sqlCommandINV = new OdbcCommand("{call getOrders}", pSqlConn);
                sqlCommandINV.CommandType = CommandType.StoredProcedure;
                sqlCommandINV.CommandTimeout = 90;
                pSqlConn.Open();

                OdbcDataReader readerINV = sqlCommandINV.ExecuteReader();

                //if reader has rows
                if (readerINV.HasRows)
                {
                    //read row
                    while (readerINV.Read() && test < 10)
                    {
                        //variable declaration
                        bool invoice = false;
                        REPRINT = false;
                        currentOrder = null;

                        //increment print set
                        test++;

                        //create new notes for order
                        List<string> notes = new List<string>();
                        List<string> crdtNotes = new List<string>();

                        //create order with invoice number
                        Order order = new Order(Convert.ToInt32(readerINV["BKAR_INV_NUM"].ToString()));
                        order.date = String.Format("{0:MM/dd/yyyy}", readerINV["BKAR_INV_INVDTE"]);
                        order.customerCode = readerINV["BKAR_INV_CUSCOD"].ToString();

                        //populate shipping address
                        order.billAddress.name = readerINV["BKAR_INV_CUSNME"].ToString().TrimEnd();
                        order.billAddress.line1 = readerINV["BKAR_INV_CUSA1"].ToString();
                        order.billAddress.line2 = readerINV["BKAR_INV_CUSA2"].ToString();
                        order.billAddress.city = readerINV["BKAR_INV_CUSCTY"].ToString().TrimEnd();
                        order.billAddress.state = readerINV["BKAR_INV_CUSST"].ToString().TrimEnd();
                        order.billAddress.zipcode = readerINV["BKAR_INV_CUSZIP"].ToString().TrimEnd();
                        order.billAddress.country = readerINV["BKAR_INV_CUSCUN"].ToString().TrimEnd();

                        //populate billing address
                        order.shipAddress.name = readerINV["BKAR_INV_SHPNME"].ToString().TrimEnd();
                        order.shipAddress.line1 = readerINV["BKAR_INV_SHPA1"].ToString();
                        order.shipAddress.line2 = readerINV["BKAR_INV_SHPA2"].ToString();
                        order.shipAddress.city = readerINV["BKAR_INV_SHPCTY"].ToString().TrimEnd();
                        order.shipAddress.state = readerINV["BKAR_INV_SHPST"].ToString().TrimEnd();
                        order.shipAddress.zipcode = readerINV["BKAR_INV_SHPZIP"].ToString().TrimEnd();
                        order.shipAddress.country = readerINV["BKAR_INV_SHPCUN"].ToString().TrimEnd();

                        //populate details
                        order.location = readerINV["BKAR_INV_LOC"].ToString().TrimEnd();
                        order.customerPO = readerINV["BKAR_INV_CUSORD"].ToString().TrimEnd();
                        order.deliveryMethod = readerINV["BKAR_INV_SHPVIA"].ToString().TrimEnd();
                        order.paymentTerms = readerINV["BKAR_INV_TERMD"].ToString().TrimEnd();
                        order.enteredBy = readerINV["BKAR_INV_ENTBY"].ToString().TrimEnd();

                        //set currentOrder
                        currentOrder = order;


                        //if order in wmsOrders
                        if (readerINV["invoice_num"].ToString() == order.invoiceNumber.ToString())
                        {
                            //mark as reprint
                            REPRINT = true;
                        }


                        //if account is vendor
                        if (vendNames.Contains(order.customerCode))
                            //order is not invoice
                            invoice = false;
                        //if order is going to purchaser
                        else if (order.billAddress == order.shipAddress)
                            //order is invoice
                            invoice = true;

                        //get items for order
                        int flagPrint = 1;
                        using (OdbcCommand sqlCommandItems = new OdbcCommand("{call getItems (?)}", pSqlConn))
                        {
                            sqlCommandItems.CommandType = CommandType.StoredProcedure;
                            sqlCommandItems.Parameters.AddWithValue(":invNum", order.invoiceNumber);
                            OdbcDataReader readerItems = sqlCommandItems.ExecuteReader();
                            if (readerItems.HasRows)
                            {
                                while (readerItems.Read())
                                {
                                    //populate new item
                                    Item item = new Item();
                                    item.partCode = readerItems["BKAR_INVL_PCODE"].ToString();
                                    item.itemType = readerItems["BKAR_INVL_ITYPE"].ToString();
                                    item.description = readerItems["BKAR_INVL_PDESC"].ToString();
                                    item.message = readerItems["BKAR_INVL_MSG"].ToString().TrimEnd().TrimStart();
                                    item.locationCode = readerItems["BKAR_INVL_LOC"].ToString().TrimEnd();
                                    item.vendorPart = readerItems["BKIC_VND_PART"].ToString();
                                    item.price = readerItems["BKAR_INVL_PEXT"].ToString();


                                    //if item has bin
                                    if (readerItems["BIN_NAME"].ToString() != "")
                                    {
                                        item.location = "BIN " + readerItems["BIN_NAME"].ToString();
                                    }
                                    //else print standard location code
                                    else
                                    {
                                        item.location = "";
                                    }


                                    // Check for shipping Notes
                                    if (item.itemType == "X" && item.message.Length >= 1)
                                    {
                                        //if order note mark present
                                        if (item.message.Substring(0, 1) == "@")
                                        {
                                            //add message to top notes
                                            notes.Add(item.message.TrimStart('@'));
                                        }
                                        else
                                        {
                                            //add message to credit notes
                                            crdtNotes.Add(item.message);
                                        }

                                        //if message declares invoice
                                        if ((item.message.ToUpper() == "@INVOICE" || item.message.ToUpper() == "INVOICE") && !crdtNotes.Contains("NO INVOICE"))
                                            //order is invoice
                                            invoice = true;
                                        else if (item.message.ToUpper() == "NO INVOICE")
                                            invoice = false;
                                    }
                                    else
                                    {
                                        //if regular item set quantity
                                        item.quantity = Convert.ToInt32(Convert.ToDouble(readerItems["BKAR_INVL_PQTY"].ToString()));
                                    }

                                    //add item to order
                                    order.items.Add(item);
                                }
                            }
                            readerItems.Close();
                        }

                        Range price, priceHead = xlsheet.get_Range("K24", "L24");
                        priceHead.Merge();

                        if (invoice)
                        {
                            xlsheet.Cells[17, 10].Value = "Ranshu";
                            priceHead.Value = "Price";
                        }
                        else
                        {
                            priceHead.Value = "";
                            xlsheet.Cells[17, 10].Value = "Pack List";
                        }

                        //if order is cash on demand
                        if (order.paymentTerms.Contains("COD"))
                        {
                            //add to notes
                            notes.Add(order.paymentTerms);
                            order.paymentTerms = order.paymentTerms + " $" + readerINV["BKAR_INV_TOTAL"].ToString();
                        }

                        string creditNotes = "";

                        for (int i = 0; i < crdtNotes.Count; i++)
                        {
                            creditNotes += System.Environment.NewLine + crdtNotes[i];
                        }

                        //if order is paid by credit
                        if (order.paymentTerms == "VISA/MC" && readerINV["CRDT_INV_NUM"].ToString() != order.invoiceNumber.ToString())
                        {

                            //add to credit table with Processed flagged false
                            string creditString = "INSERT INTO CRDTINV (CRDT_INV_NUM, CRDT_INV_CUSCOD, CRDT_INV_DATE, CRDT_INV_TOTAL, CRDT_INV_PROCESSED, CRDT_INV_USER, CRDT_INV_NOTES, CRDT_INV_SHPVIA, CRDT_INV_SLSP, CRDT_INV_DECLINED) " +
                                "VALUES (" + order.invoiceNumber + ", '" + order.customerCode + "', '" + order.date + "', " + readerINV["BKAR_INV_TOTAL"].ToString() + ", 0, 'null', '" + creditNotes.Replace("'", "") + "', '" + order.deliveryMethod + "', " + Convert.ToInt32(readerINV["BKAR_INV_SLSP"].ToString()) + ", 0 )";

                            using (OdbcCommand creditCommand = new OdbcCommand(creditString, pSqlConn))
                            {
                                creditCommand.ExecuteNonQuery();
                            }
                        }
                        else if (!(order.deliveryMethod == "FAX" || order.deliveryMethod == "RMT"))
                        {
                            int numItems = 0;
                            int numPage = 1;

                            strInvLoc = order.location;

                            // Find first location code
                            /*
                            foreach (Item item in order.items)
                            {
                                if(item.locationCode != null && item.locationCode != "")
                                //if Type is not message line 'X' or non-stock 'N'
                                if (item.itemType != "X" && item.itemType != "N")
                                {
                                    //change location code
                                    strInvLoc = item.locationCode;
                                    break;
                                }

                            }*/


                            //update excel template
                            // TOP Header Section
                            xlBarcodeTop.Value = "*" + order.invoiceNumber + "*";

                            xlsheet.Cells[1, 1].Value = "Date:";
                            xlsheet.Cells[1, 2].value = order.date;

                            //xlsheet.Cells[1, 6].value = arrOrder[0, 21].ToString();

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
                            xlsheet.Cells[8, 11].value = order.deliveryMethod;
                            xlsheet.Cells[9, 9].Value = "Payment Terms:";
                            xlsheet.Cells[9, 11].value = order.paymentTerms;

                            xlsheet.Cells[13, 10].Value = "# of Boxes: __________";

                            xlsheet.Cells[10, 1].Value = "NOTES";
                            xlsheet.get_Range("A10", "G10").Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d;

                            //Print Notes if any
                            int currentNoteRow = 11;
                            int currentNoteCol = 1;
                            // IF CALIFORNIA SHIPTO THEN ADD PROP65 LABEL
                            if (order.shipAddress.state == "CA")
                            {
                                notes.Add("PROP 65 LABEL");
                            }

                            //for all notes in list
                            for (int n = 0; n < notes.Count; n++)
                            {
                                if (currentNoteRow > 15)
                                {
                                    currentNoteRow = 11;
                                    currentNoteCol = 5;
                                }

                                //add notes to printout
                                xlsheet.Cells[currentNoteRow, currentNoteCol].value = notes[n].ToString();
                                currentNoteRow++;
                            }

                            // BOTTOM Header Section
                            xlBarcodeBottom.Value = "*" + order.invoiceNumber + "*";

                            xlsheet.Cells[18, 1].value = order.shipAddress.name;
                            xlsheet.Cells[19, 1].value = order.shipAddress.line1;
                            xlsheet.Cells[20, 1].value = order.shipAddress.line2;
                            xlsheet.Cells[21, 1].value = order.shipAddress.city + ", " + order.shipAddress.state + "  " + order.shipAddress.zipcode;

                            xlsheet.Cells[17, 7].value = order.invoiceNumber;
                            xlsheet.Cells[18, 7].value = order.date;
                            xlsheet.Cells[19, 7].value = order.enteredBy;
                            xlsheet.Cells[20, 7].value = order.customerCode;

                            xlsheet.Cells[21, 11].value = strInvLoc;
                            xlsheet.Cells[22, 11].value = order.deliveryMethod;

                            // ASSIGN PRINTER BASED OFF OF WAREHOUSE CODE
                            flagPrint = selectPrinter(strInvLoc, (order.deliveryMethod == "DELIVERY" || order.deliveryMethod == "WILL CALL"));
                            writeToFile(installedPrinter);

                            // Dont print FAX

                            int numPageItems = 0;
                            int currentRow = 25;
                            double subCheck = 0.0f;
                            for (int x = 0; x < order.items.Count; x++)
                            {
                                Item item = order.items[x];

                                if (REPRINT)
                                {
                                    xlsheet.Cells[1, 6].Value = "REPRINT";
                                }
                                else
                                {
                                    xlsheet.Cells[1, 6].Value = "";
                                }

                                if ((numPageItems > 19) && (strInvLoc == item.locationCode))
                                {
                                    xlsheet.Cells[46, 4].Value = "Continued on Next Page...";

                                    // Print Out Current PackList
                                    if (flagPrint == 1)
                                    {
                                        xlsheet.PrintOutEx(misValue, misValue, 1, false);
                                    }

                                    numPage = numPage + 1;
                                    numPageItems = 0;
                                    currentRow = 25;

                                    // CLEAR OUT TOP Header Sections
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

                                    //Clear out notes
                                    xlsheet.get_Range("A11", "E15").Clear();
                                    xlsheet.get_Range("A11", "E15").HorizontalAlignment = XlHAlign.xlHAlignLeft;

                                    // Clear out previous line items
                                    xlsheet.get_Range("A25", "L49").Clear();
                                    xlsheet.get_Range("A25", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                    xlsheet.get_Range("B24", "B49").HorizontalAlignment = XlHAlign.xlHAlignCenter;

                                }
                                else if (strInvLoc != item.locationCode && (item.itemType != "X" && item.itemType != "N"))
                                {
                                    xlsheet.Cells[46, 4].Clear();
                                    xlsheet.Cells[47, 1].Value = "Total Qty";
                                    xlsheet.Cells[47, 2].Value = numItems.ToString();

                                    //if invoice
                                    if (invoice)
                                    {
                                        xlsheet.Cells[46, 4].Value = "Remit Payment to:";
                                        xlsheet.Cells[46, 7].Value = "Ranshu";
                                        xlsheet.Cells[47, 7].Value = "P.O. Box 913317";
                                        xlsheet.Cells[48, 7].Value = "Denver, CO 80291-3317";

                                        if (order.paymentTerms == "NET 5TH")
                                        {
                                            xlsheet.Cells[49, 5].Value = "Pay by the 5th and save $" + ((Convert.ToDouble(readerINV["BKAR_INV_SUBTOT"].ToString()) + Convert.ToDouble(readerINV["BKAR_INV_TAXAMT"].ToString())) * .05f).ToString("N2");
                                        }

                                        if (subCheck < Convert.ToDouble(readerINV["BKAR_INV_SUBTOT"].ToString()))
                                        {
                                            xlsheet.Cells[45, 8].Value = "Items shipped separately:";
                                            price = xlsheet.get_Range("K45", "L45");
                                            price.Merge();
                                            price.Value = "$" + (Convert.ToDouble(readerINV["BKAR_INV_SUBTOT"].ToString()) - subCheck).ToString("N2");
                                        }
                                        else
                                        {
                                            xlsheet.get_Range("H45", "L45").Clear();
                                        }
                                        xlsheet.Cells[46, 10].Value = "Subtotal:";
                                        price = xlsheet.get_Range("K46", "L46");
                                        price.Merge();
                                        price.Value = "$" + Convert.ToDouble(readerINV["BKAR_INV_SUBTOT"].ToString()).ToString("N2");
                                        xlsheet.Cells[47, 10].Value = "Tax:";
                                        price = xlsheet.get_Range("K47", "L47");
                                        price.Merge();
                                        price.Value = "$" + Convert.ToDouble(readerINV["BKAR_INV_TAXAMT"].ToString()).ToString("N2");
                                        xlsheet.Cells[48, 10].Value = "Freight:";
                                        price = xlsheet.get_Range("K48", "L48");
                                        price.Merge();
                                        price.Value = "$" + Convert.ToDouble(readerINV["BKAR_INV_FRGHT"].ToString()).ToString("N2");
                                        xlsheet.Cells[49, 10].Value = "Total:";
                                        price = xlsheet.get_Range("K49", "L49");
                                        price.Merge();
                                        price.Value = "$" + Convert.ToDouble(readerINV["BKAR_INV_TOTAL"].ToString()).ToString("N2");
                                    }

                                    // Print Out Current PackList
                                    if (flagPrint == 1)
                                    {
                                        xlsheet.PrintOutEx(misValue, misValue, 1, false);
                                    }

                                    //reset items
                                    numItems = 0;
                                    numPage = 1;
                                    numPageItems = 0;
                                    currentRow = 25;

                                    //set new location code
                                    strInvLoc = item.locationCode;

                                    // ASSIGN PRINTER BASED OFF OF WAREHOUSE CODE
                                    flagPrint = selectPrinter(strInvLoc, (order.deliveryMethod == "DELIVERY" || order.deliveryMethod == "WILL CALL"));

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
                                    xlsheet.Cells[8, 11].value = order.deliveryMethod;
                                    xlsheet.Cells[9, 9].Value = "Payment Terms:";
                                    xlsheet.Cells[9, 11].value = order.paymentTerms;

                                    xlsheet.Cells[13, 10].Value = "# of Boxes: __________";

                                    xlsheet.Cells[10, 1].Value = "NOTES";
                                    xlsheet.get_Range("A10", "G10").Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d;

                                    // Clear out previous line items
                                    xlsheet.get_Range("A25", "L49").Clear();
                                    xlsheet.get_Range("A25", "H49").HorizontalAlignment = XlHAlign.xlHAlignLeft;

                                }

                                xlsheet.Cells[15, 11].value = "Page";
                                xlsheet.Cells[15, 12].value = numPage.ToString();

                                xlsheet.Cells[49, 1].value = "Page";
                                xlsheet.Cells[49, 2].value = numPage.ToString();

                                price = xlsheet.get_Range("K" + currentRow, "L" + currentRow);
                                price.Merge();

                                if (item.itemType == "X")
                                {
                                    xlsheet.Cells[currentRow, 4].value = "  " + item.message;
                                }
                                else if(item.itemType == "N")
                                {
                                    if (!(item.partCode.ToUpper().Contains("PROP65") || item.partCode.ToUpper().Contains("FREIGHT")))
                                    {
                                        xlsheet.Cells[currentRow, 4].value = "  " + item.message;

                                        if (invoice)
                                        {
                                            price.Value = "$" + Convert.ToDouble(item.price).ToString("N2");
                                            subCheck += Convert.ToDouble(item.price);
                                        }
                                        else
                                        {
                                            price.Value = "";
                                        }
                                    }
                                }
                                else
                                {
                                    xlsheet.Cells[currentRow, 1].value = item.location;
                                    xlsheet.Cells[currentRow, 2].value = item.quantity;
                                    xlsheet.Cells[currentRow, 3].value = item.partCode;
                                    xlsheet.Cells[currentRow, 6].value = item.vendorPart;
                                    xlsheet.Cells[currentRow, 8].value = item.description;

                                        if (invoice)
                                        {
                                            price.Value = "$" + Convert.ToDouble(item.price).ToString("N2");
                                            subCheck += Convert.ToDouble(item.price);
                                        }
                                        else
                                        {
                                            price.Value = "";
                                        }
                                    numItems = numItems + item.quantity;

                                }

                                currentRow++;
                                numPageItems++;
                            }

                            xlsheet.Cells[46, 4].Clear();

                            xlsheet.Cells[47, 1].Value = "Total Qty";
                            xlsheet.Cells[47, 2].Value = numItems.ToString();

                            if (invoice)
                            {
                                xlsheet.Cells[46, 4].Value = "Remit Payment to:";
                                xlsheet.Cells[46, 7].Value = "Ranshu";
                                xlsheet.Cells[47, 7].Value = "P.O. Box 913317";
                                xlsheet.Cells[48, 7].Value = "Denver, CO 80291-3317";

                                if(order.paymentTerms == "NET 5TH")
                                {
                                    xlsheet.Cells[49, 5].Value = "Pay by the 5th and save $" + ((Convert.ToDouble(readerINV["BKAR_INV_SUBTOT"].ToString()) + Convert.ToDouble(readerINV["BKAR_INV_TAXAMT"].ToString())) * .05f).ToString("N2");
                                }

                                if(subCheck < Convert.ToDouble(readerINV["BKAR_INV_SUBTOT"].ToString()))
                                {
                                    xlsheet.Cells[45, 8].Value = "Items shipped separately:";
                                    price = xlsheet.get_Range("K45", "L45");
                                    price.Merge();
                                    price.Value = "$" + (Convert.ToDouble(readerINV["BKAR_INV_SUBTOT"].ToString()) - subCheck).ToString("N2");
                                }
                                else
                                {
                                    xlsheet.get_Range("H45", "L45").Clear();
                                }
                                xlsheet.Cells[46, 10].Value = "Subtotal:"; 
                                price = xlsheet.get_Range("K46", "L46");
                                price.Merge();
                                price.Value = "$" + Convert.ToDouble(readerINV["BKAR_INV_SUBTOT"].ToString()).ToString("N2");
                                xlsheet.Cells[47, 10].Value = "Tax:";
                                price = xlsheet.get_Range("K47", "L47");
                                price.Merge();
                                price.Value = "$" + Convert.ToDouble(readerINV["BKAR_INV_TAXAMT"].ToString()).ToString("N2");
                                xlsheet.Cells[48, 10].Value = "Freight:";
                                price = xlsheet.get_Range("K48", "L48");
                                price.Merge();
                                price.Value = "$" + Convert.ToDouble(readerINV["BKAR_INV_FRGHT"].ToString()).ToString("N2");
                                xlsheet.Cells[49, 10].Value = "Total:";
                                price = xlsheet.get_Range("K49", "L49");
                                price.Merge();
                                price.Value = "$" + Convert.ToDouble(readerINV["BKAR_INV_TOTAL"].ToString()).ToString("N2");
                            }

                            // Print Out Final Page of Packlist
                            if (flagPrint == 1)
                            {
                                xlsheet.PrintOutEx(misValue, misValue, 1, false);

                            }

                            //if not a reprint
                            if (!REPRINT)
                            {
                                //add order to wmsOrders
                                //update invoice as printed
                                strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                                sqlUpdateList = "INSERT INTO wmsOrders (invoice_num,printed,printer, notes) " +
                                                "VALUES (" + order.invoiceNumber + ", '" + strCurrentDateTime.ToString() +
                                                "', '" + installedPrinter + "', '" + creditNotes.Replace("'", "") +"'); " +
                                                "UPDATE BKARHINV " +
                                                "SET BKAR_INV_MAX = 1 " +
                                                "WHERE BKAR_INV_NUM = " + order.invoiceNumber;
                                using (OdbcCommand cmd = new OdbcCommand(sqlUpdateList, pSqlConn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                //update invoice on database
                                strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                                sqlUpdateList = "UPDATE wmsOrders " +
                                                "SET printed = '" + strCurrentDateTime.ToString() + "', " +
                                                "printer = '" + installedPrinter + "'" +
                                                "WHERE invoice_num = " + order.invoiceNumber + ";" +
                                                "UPDATE BKARHINV " +
                                                "SET BKAR_INV_MAX = 1 " +
                                                "WHERE BKAR_INV_NUM = " + order.invoiceNumber;
                                using (OdbcCommand cmd = new OdbcCommand(sqlUpdateList, pSqlConn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            //if retail invoice
                            if(order.deliveryMethod == "DELIVERY" || order.deliveryMethod == "WILL CALL")
                            {
                                //mark invoice as validated
                                strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                                sqlUpdateList = "UPDATE wmsOrders " +
                                                "SET validated = '" + strCurrentDateTime.ToString()+ "' " +
                                                "WHERE invoice_num = " + order.invoiceNumber;
                                using (OdbcCommand cmd = new OdbcCommand(sqlUpdateList, pSqlConn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }

                        }

                        //mark invoice as printed
                        string sqlMarkPrint = "UPDATE BKARHINV " +
                                    "SET BKAR_INV_MAX = 1 " +
                                    "WHERE BKAR_INV_NUM = " + order.invoiceNumber;
                        using (OdbcCommand cmd = new OdbcCommand(sqlMarkPrint, pSqlConn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // CLEAN UP LINE ITEMS AND EXTRA PAGES
                        xlsheet.get_Range("A25", "L49").Clear();

                        // CLEAN UP NOTES
                        xlsheet.get_Range("A11", "E15").Clear();

                        // ALIGN CELLS PROPERLY
                        xlsheet.get_Range("A25", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
                        xlsheet.get_Range("B24", "B49").HorizontalAlignment = XlHAlign.xlHAlignCenter;

                        writeToFile(order.invoiceNumber.ToString());
                    }
                }

                //close database connections
                readerINV.Close();
                pSqlConn.Close();
            }

            //denote tick
            writeToFile("END SET");
        }

        /*
         * @FUNCTION:   public static void writeToFile()
         * @PURPOSE:    writes message to log file
         *              
         * @PARAM:      string message
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        public static void writeToFile(string message)
        {
            //open/create log directory
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //open/create log file
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Service_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if(!File.Exists(filePath))
            {
                using(StreamWriter sw = File.CreateText(filePath))
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

        /*
         * @FUNCTION:   public static void condenseFile()
         * @PURPOSE:    removes "END SET" lines from the log file
         *              
         * @PARAM:      none
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        public static void condenseFile()
        {
            //open log directory or exit if not present
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                return;
            }

            //open log file or exit if not present
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Service_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filePath))
            {
                return;
            }
            else
            {
                //read file to list
                List<string> rawFile = File.ReadAllLines(filePath).ToList<string>();

                //for each log line
                for(int i = 0; i < rawFile.Count; i++)
                {
                    //if line contatins "END SET"
                    if(rawFile[i].Contains("END SET"))
                    {
                        //remove line
                        rawFile.RemoveAt(i);
                        i--;
                    }
                }

                //rewrite file
                File.WriteAllLines(filePath, rawFile);
            }
        }

        /*
         * @FUNCTION:   public static int selectPrinter()
         * @PURPOSE:    Select printer based on location
         *              mark as retail if retail
         *              find installed printer
         *              check selected printer for errors
         *              
         * @PARAM:      string locationCode
         *              bool retail
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        public static int selectPrinter(string locationcode, bool retail)
        {
            int flagPrinterFound = 0;
            string backupPrinter;

            // ASSIGN PRINTER BASED OFF OF WAREHOUSE CODE
            if(retail)
            {
                installedPrinter = "RETAIL " + locationcode;
                return 0;
            }
            else
            {
                if (locationcode == "RENO" || locationcode == "SPARKS" || locationcode == "SPARKS2")
                {
                    installedPrinter = "RICOHNV";
                }
                /*else if (locationcode == "SPARKS")
                {
                    installedPrinter = "RICOHNV";
                }
                else if (locationcode == "SPARKS2")
                {
                    installedPrinter = "RICOHNV";
                }
                else if (locationcode == "")
                {
                    installedPrinter = "RICOHNV";
                }*/
                else if (locationcode == "FORT WORTH")
                {
                    installedPrinter = "RICOHTX";
                }
                else if (locationcode == "TX CONSIGN")
                {
                    installedPrinter = "RICOHTX";
                }
                else if (locationcode == "PA")
                {
                    installedPrinter = "RICOHTX";
                }
                else if (locationcode == "FL")
                {
                    installedPrinter = "RICOHTX";
                }
                else if(locationcode == "MEI" || locationcode == "APAIR")
                {
                    installedPrinter = "RICOHTX";
                }
                else
                {
                    writeToFile("Printer at " + locationcode + " not installed");
                    installedPrinter = null;
                }
            }

            //if installed printer is valid
            if (installedPrinter != null && installedPrinter != "" && !installedPrinter.Contains("RETAIL"))
            {
                try
                {
                    // GRAB REGISTRY KEY NAME/VALUE PAIRS FOR ALL PRINTERS INSTALLED ON MACHINE
                    // SEARCH FOR NAME/VALUE PAIR WITH installedPrinter AS THE NAME AND EXTRACT THE PORT NAME FROM IT
                    // USE THE PORT NAME TO SPECIFY THE ACTIVE PRINTER IN THE EXCEL INSTANCE
                    subkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Devices", false);
                    foreach (string printerName in subkey.GetValueNames())
                    {
                        if (printerName == installedPrinter)
                        {
                            //checks for errors on the installed printer
                            PrintServer printServer = new PrintServer(installedPrinter);
                            PrintQueueCollection printQueues = printServer.GetPrintQueues();
                            foreach (PrintQueue pq in printQueues)
                            {
                                pq.Refresh();
                                PrintJobInfoCollection pCollection = pq.GetPrintJobInfoCollection();
                                if (!heldPrinters.Contains(pq.Name) && ((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error || (pq.QueueStatus & PrintQueueStatus.None) != PrintQueueStatus.None))
                                {
                                    if (pSqlConn.State == ConnectionState.Open)
                                        pSqlConn.Close();
                                    string strConnection = "DSN=RANSHU";
                                    pSqlConn = null;
                                    using (pSqlConn = new OdbcConnection(strConnection))
                                    {
                                        //get all new invoices
                                        string sqlInvoice = "insert into wmsTrouble(trouble_printer) " +
                                            "select distinct printer_name from wmsPrinters " +
                                            "where printer_name = '" + pq.Name + "' " +
                                            "and printer_name not in (select trouble_printer " +
                                            "from wmsTrouble where trouble_printer = printer_name)";

                                        OdbcCommand cmd = new OdbcCommand(sqlInvoice, pSqlConn);
                                        pSqlConn.Open();

                                        if (cmd.ExecuteNonQuery() > 0)
                                        {
                                            //emails error report to IT
                                            string text = "There appears to be a problem printing order to " + pq.Name + ". ERROR: " + pq.QueueStatus.ToString();
                                            text += System.Environment.NewLine + "Please check the printer.";
                                            SendEmail("Print Failure on " + pq.Name, text);
                                        }

                                        heldPrinters.Add(pq.Name);
                                        pSqlConn.Close();
                                    }
                                    return 0;
                                }
                                else if (heldPrinters.Contains(pq.Name) && !((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error || (pq.QueueStatus & PrintQueueStatus.None) != PrintQueueStatus.None))
                                {
                                    if (pSqlConn.State == ConnectionState.Open)
                                        pSqlConn.Close();
                                    string strConnection = "DSN=RANSHU";
                                    pSqlConn = null;
                                    using (pSqlConn = new OdbcConnection(strConnection))
                                    {
                                        //get all new invoices
                                        string sqlInvoice = "delete from wmsTrouble " +
                                            "where printer_name = '" + pq.Name + "'";

                                        OdbcCommand cmd = new OdbcCommand(sqlInvoice, pSqlConn);
                                        pSqlConn.Open();
                                        cmd.ExecuteNonQuery();
                                        heldPrinters.Remove(pq.Name);
                                        pSqlConn.Close();
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
                        writeToFile("There appears to be a problem printing to " + installedPrinter);
                    }
                }
                catch (Exception ex)
                {
                    //report error to IT
                    string error = new StackTrace(ex, true).GetFrame(0).GetFileLineNumber() + " " + ex.Message;
                    writeToFile(error);
                    return 0;
                }
            }

            return flagPrinterFound;
        }

        /*
         * @FUNCTION:   static void SendEmail()
         * @PURPOSE:    create and send error emails
         *              
         * @PARAM:      string subject
         *              string msgText
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        static void SendEmail(string subject, string msgText)
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
            //msgMail.CC.Add(new MailAddress("jeremy@ranshu.com"));
            msgMail.Subject = "Ranshu Print Service Error: " + subject;
            msgMail.Body = msgText;
            msgMail.IsBodyHtml = true;

            //send message
            mailClient.Send(msgMail);

            //garbage collect
            msgMail.Dispose();
        }

        /*
         * @FUNCTION:   protected override void OnStop()
         * @PURPOSE:    free floating excel memory
         *              condense log file
         *              stop the service
         *              
         * @PARAM:      none
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        protected override void OnStop()
        {
            // Close down program
            // Allow new instances of Excel from using this applications workbook
            OrderPrintService.xlApp.IgnoreRemoteRequests = false;

            OrderPrintService.xlWorkBook.Close(false, misValue, misValue);
            OrderPrintService.xlApp.Application.Quit();
            OrderPrintService.xlApp.Quit();

            releaseObject(xlsheet);
            releaseObject(OrderPrintService.xlWorkBook);
            releaseObject(OrderPrintService.xlApp);

            //log stop to file and condense file
            writeToFile("Service Stopped");
            condenseFile();
        }
    }
}
