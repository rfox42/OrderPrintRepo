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

        public OrderPrintService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
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
            xlsheet.get_Range("A26", "A49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("C26", "C49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("D26", "D49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("G26", "G49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlsheet.get_Range("I26", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;

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

            xlsheet.get_Range("A24", "L24").Font.Bold = true;
            xlsheet.Cells[24, 1].Value = "Location";
            xlsheet.Cells[24, 3].Value = "Qty";
            xlsheet.Cells[24, 4].Value = "Part No";
            xlsheet.Cells[24, 7].Value = "Cust Part No";
            xlsheet.Cells[24, 9].Value = "Description";

            writeToFile("Service Started");

            timer = new System.Timers.Timer(10000);
            timer.Elapsed += new ElapsedEventHandler(timerProgram);
            timer.Enabled = true;
        }

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

        private static void timerProgram(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            newOrders();

            try
            {
                //checks for errors on the installed printer
                PrintServer printServer = new PrintServer(installedPrinter);
                PrintQueueCollection printQueues = printServer.GetPrintQueues();
                foreach (PrintQueue pq in printQueues)
                {
                    pq.Refresh();
                    PrintJobInfoCollection pCollection = pq.GetPrintJobInfoCollection();
                    if ((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error || (pq.QueueStatus & PrintQueueStatus.None) != PrintQueueStatus.None)
                    {
                        //emails error report to IT
                        string text = "There appears to be a problem printing order " + currentOrder.invoiceNumber + " to " + installedPrinter + ".";
                        text += System.Environment.NewLine + "Please check the printer.";
                        SendEmail(installedPrinter, text);

                        //breaks loop once error found
                        break;
                    }
                }
            }
            catch
            {

            }

            GC.Collect();
            timer.Start();
        }


        static void newOrders()
        {
            int test = 0;
            flagActive = 1;

            string sqlUpdateList;
            string strCurrentDateTime;

            string strInvLoc;

            string[] arrNotes = new string[10];

            // Copy all invoices from Addsum not listed in wmsOrders table
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get order information
                string sqlInvoice =
                "SELECT BKAR_INV_NUM, BKAR_INV_INVDTE, BKAR_INV_CUSCOD, BKAR_INV_CUSNME, BKAR_INV_CUSA1, BKAR_INV_CUSA2, BKAR_INV_CUSCTY, BKAR_INV_CUSST, BKAR_INV_CUSZIP, BKAR_INV_CUSCUN, BKAR_INV_SHPNME, BKAR_INV_SHPA1, BKAR_INV_SHPA2, BKAR_INV_SHPCTY, BKAR_INV_SHPST, BKAR_INV_SHPZIP, BKAR_INV_SHPCUN, BKAR_INV_LOC, BKAR_INV_CUSORD, BKAR_INV_SHPVIA, BKAR_INV_TERMD, BKAR_INV_ENTBY, BKAR_INV_TOTAL, BKAR_INV_SLSP, invoice_num" +
                " FROM BKARHINV LEFT JOIN wmsOrders ON BKAR_INV_NUM = invoice_num " +
                " WHERE BKAR_INV_MAX = 0";
                OdbcCommand sqlCommandINV = new OdbcCommand(sqlInvoice, pSqlConn);
                sqlCommandINV.CommandTimeout = 90;
                pSqlConn.Open();

                OdbcDataReader readerINV = sqlCommandINV.ExecuteReader();

                if (readerINV.HasRows)
                {
                    while (readerINV.Read() && test < 10)
                    {

                        REPRINT = false;
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
                        order.location = readerINV["BKAR_INV_LOC"].ToString().TrimEnd(); //never used
                        order.customerPO = readerINV["BKAR_INV_CUSORD"].ToString().TrimEnd();
                        order.deliveryMethod = readerINV["BKAR_INV_SHPVIA"].ToString().TrimEnd();
                        order.paymentTerms = readerINV["BKAR_INV_TERMD"].ToString().TrimEnd();
                        order.enteredBy = readerINV["BKAR_INV_ENTBY"].ToString().TrimEnd();

                        currentOrder = order;

                        if (readerINV["invoice_num"].ToString() == order.invoiceNumber.ToString())
                        {
                            REPRINT = true;
                        }

                        //get items for order
                        int flagPrint = 1;
                        string sqlItems = "SELECT BKAR_INVL_PCODE, BKAR_INVL_ITYPE, BKAR_INVL_PQTY, BKAR_INVL_PDESC, BKAR_INVL_LOC, BKAR_INVL_MSG, BKIC_VND_PART, BIN_NAME " +
                            "FROM BKARHINV INNER JOIN BKARHIVL ON BKAR_INV_NUM = BKAR_INVL_INVNM " +
                            "LEFT JOIN BKICCUST ON (BKAR_INVL_PCODE = BKIC_VND_PCODE AND BKAR_INV_CUSCOD = BKIC_VND_VENDOR)" +
                            "LEFT JOIN wmsLocations ON (BIN_PART = BKAR_INVL_PCODE AND BIN_LOC = BKAR_INVL_LOC) " +
                            "WHERE BKAR_INV_NUM = '" + order.invoiceNumber + "'";
                        using (OdbcCommand sqlCommandItems = new OdbcCommand(sqlItems, pSqlConn))
                        {
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


                                    //if item has bin
                                    if (readerItems["BIN_NAME"].ToString() != "")
                                    {
                                        item.location = "BIN " + readerItems["BIN_NAME"].ToString();
                                    }
                                    //else print standard location code
                                    else
                                    {
                                        item.location = "A12-08-05-2";
                                    }


                                    // Check for shipping Notes
                                    if (item.itemType == "X" && item.message.Length >= 1)
                                    {
                                        if (item.message.Substring(0, 1) == "@")
                                        {
                                            notes.Add(item.message.TrimStart('@'));
                                        }
                                        else
                                        {
                                            crdtNotes.Add(item.message);
                                        }
                                    }
                                    else
                                    {
                                        item.quantity = Convert.ToInt32(Convert.ToDouble(readerItems["BKAR_INVL_PQTY"].ToString()));
                                    }

                                    //add item(s) to order
                                    order.items.Add(item);
                                }
                            }
                            readerItems.Close();
                        }

                        //if order is cash on demand
                        if (order.paymentTerms.Contains("COD"))
                        {
                            //add to notes
                            notes.Add(order.paymentTerms);
                        }

                        //if order is paid by credit
                        if (order.paymentTerms == "VISA/MC" && !REPRINT)
                        {
                            string creditNotes = "";

                            for (int i = 0; i < crdtNotes.Count; i++)
                            {
                                creditNotes += System.Environment.NewLine + crdtNotes[i];
                            }

                            //add to credit table with Processed flagged false
                            string creditString = "INSERT INTO CRDTINV (CRDT_INV_NUM, CRDT_INV_CUSCOD, CRDT_INV_DATE, CRDT_INV_TOTAL, CRDT_INV_PROCESSED, CRDT_INV_USER, CRDT_INV_NOTES, CRDT_INV_SHPVIA, CRDT_INV_SLSP) " +
                                "VALUES (" + order.invoiceNumber + ", '" + order.customerCode + "', '" + order.date + "', " + readerINV["BKAR_INV_TOTAL"].ToString() + ", 0, 'null', '" + creditNotes + "', '" + order.deliveryMethod + "', " + Convert.ToInt32(readerINV["BKAR_INV_SLSP"].ToString()) + ")";

                            using (OdbcCommand creditCommand = new OdbcCommand(creditString, pSqlConn))
                            {
                                creditCommand.ExecuteNonQuery();
                            }

                            if (order.deliveryMethod == "FAX" || order.deliveryMethod == "RMT")
                            {
                                flagPrint = 0;
                                string sqlMarkPrint = "UPDATE BKARHINV " +
                                            "SET BKAR_INV_MAX = 1 " +
                                            "WHERE BKAR_INV_NUM = " + order.invoiceNumber;
                                using (OdbcCommand cmd = new OdbcCommand(sqlMarkPrint, pSqlConn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                continue;
                            }
                        }
                        else if (!(order.deliveryMethod == "DELIVERY" || order.deliveryMethod == "WILL CALL"))
                        {
                            int numItems = 0;
                            int numPage = 1;

                            // Find first location code
                            strInvLoc = order.items[0].locationCode;
                            foreach (Item item in order.items)
                            {
                                //if Type is not message line 'X' or non-stock 'N'
                                if (item.itemType != "X" && item.itemType != "N")
                                {
                                    //change location code
                                    strInvLoc = item.locationCode;
                                    break;
                                }

                            }


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
                            writeToFile(xlApp.ActivePrinter.ToString());

                            // Dont print FAX
                            if (order.deliveryMethod == "FAX" || order.deliveryMethod == "RMT")
                            {
                                flagPrint = 0;
                            }

                            int numPageItems = 0;
                            int currentRow = 26;
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

                                if ((numPageItems > 21) && (strInvLoc == item.locationCode))
                                {

                                    xlsheet.Cells[49, 4].Value = "Continued on Next Page...";

                                    // Print Out Current PackList
                                    if (flagPrint == 1)
                                    {
                                        xlsheet.PrintOutEx(misValue, misValue, 1, false);
                                    }

                                    numPage = numPage + 1;
                                    numPageItems = 0;
                                    currentRow = 26;

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
                                    xlsheet.get_Range("A26", "I49").Clear();
                                    xlsheet.get_Range("A26", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;

                                }
                                else if ((strInvLoc != item.locationCode) && (item.itemType != "X") && (item.itemType != "N"))
                                {
                                    xlsheet.Cells[49, 4].Clear();

                                    xlsheet.Cells[49, 2].Value = "Total Qty";
                                    xlsheet.Cells[49, 3].Value = numItems.ToString();

                                    // Print Out Current PackList
                                    if (flagPrint == 1)
                                    {
                                        xlsheet.PrintOutEx(misValue, misValue, 1, false);
                                    }

                                    numItems = 0;
                                    numPage = 1;
                                    numPageItems = 0;
                                    currentRow = 26;

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
                                    xlsheet.get_Range("A26", "I49").Clear();
                                    xlsheet.get_Range("A26", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;

                                }

                                xlsheet.Cells[15, 11].value = "Page";
                                xlsheet.Cells[15, 12].value = numPage.ToString();

                                xlsheet.Cells[50, 11].value = "Page";
                                xlsheet.Cells[50, 12].value = numPage.ToString();

                                if (item.itemType == "X")
                                {
                                    xlsheet.Cells[currentRow, 4].value = "  " + item.message;
                                }
                                else
                                {
                                    xlsheet.Cells[currentRow, 1].value = item.location;
                                    xlsheet.Cells[currentRow, 3].value = item.quantity;
                                    xlsheet.Cells[currentRow, 4].value = item.partCode;
                                    xlsheet.Cells[currentRow, 7].value = item.vendorPart;
                                    xlsheet.Cells[currentRow, 9].value = item.description;
                                    numItems = numItems + item.quantity;

                                }

                                currentRow++;
                                numPageItems++;
                            }

                            xlsheet.Cells[49, 4].Clear();

                            xlsheet.Cells[49, 2].Value = "Total Qty";
                            xlsheet.Cells[49, 3].Value = numItems.ToString();

                            // Print Out Final Page of Packlist
                            if (flagPrint == 1)
                            {
                                xlsheet.PrintOutEx(misValue, misValue, 1, false);

                            }
                        }
                        else
                        {
                            installedPrinter = "RETAIL" + order.items[0].locationCode;
                        }

                        if (order.deliveryMethod == "FAX" || order.deliveryMethod == "RMT")
                        {
                            string sqlMarkPrint = "UPDATE BKARHINV " +
                                        "SET BKAR_INV_MAX = 1 " +
                                        "WHERE BKAR_INV_NUM = " + order.invoiceNumber;
                            using (OdbcCommand cmd = new OdbcCommand(sqlMarkPrint, pSqlConn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else if (!REPRINT)
                        {
                            //add order to wmsOrders
                            //update invoice as printed
                            strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                            sqlUpdateList = "INSERT INTO wmsOrders (invoice_num,printed,printer) " +
                                            "VALUES (" + order.invoiceNumber + ", '" + strCurrentDateTime.ToString() +
                                            "', '" + installedPrinter + "');" +
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
                            //update invoice as printed
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

                        // CLEAN UP LINE ITEMS AND EXTRA PAGES
                        xlsheet.get_Range("A26", "I49").Clear();

                        // CLEAN UP NOTES
                        xlsheet.get_Range("A11", "E15").Clear();

                        // ALIGN CELLS PROPERLY
                        xlsheet.get_Range("A26", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;

                        writeToFile(order.invoiceNumber.ToString());
                    }
                }
                readerINV.Close();
                pSqlConn.Close();
            }

            writeToFile("END SET - Test");
        }

        public static void writeToFile(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Service_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if(!File.Exists(filePath))
            {
                using(StreamWriter sw = File.CreateText(filePath))
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

        public static void condenseFile()
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                return;
            }

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Service_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if (!File.Exists(filePath))
            {
                return;
            }
            else
            {
                List<string> rawFile = File.ReadAllLines(filePath).ToList<string>();

                for(int i = 0; i < rawFile.Count; i++)
                {
                    if(rawFile[i].Contains("END SET"))
                    {
                        rawFile.RemoveAt(i);
                        i--;
                    }
                }

                File.WriteAllLines(filePath, rawFile);
            }
        }

        public static int selectPrinter(string locationcode, bool retail)
        {
            int flagPrinterFound = 0;
            string backupPrinter;

            // ASSIGN PRINTER BASED OFF OF WAREHOUSE CODE

            if (locationcode == "RENO" || locationcode == "SPARKS" || locationcode == "SPARKS2")
            {
                if (retail)
                {
                    installedPrinter = "RICOHNV";
                }
                else
                {
                    installedPrinter = "RICOHNV";
                }
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
                if (retail)
                {
                    installedPrinter = "RICOHTX";
                }
                else
                {
                    installedPrinter = "RICOHTX";
                }
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
            else
            {
                installedPrinter = null;
            }

            if (installedPrinter != null)
            {
                // GRAB REGISTRY KEY NAME/VALUE PAIRS FOR ALL PRINTERS INSTALLED ON MACHINE
                // SEARCH FOR NAME/VALUE PAIR WITH installedPrinter AS THE NAME AND EXTRACT THE PORT NAME FROM IT
                // USE THE PORT NAME TO SPECIFY THE ACTIVE PRINTER IN THE EXCEL INSTANCE

                //subkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Print\Printers", false);
                subkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Devices", false);
                foreach (string printerName in subkey.GetValueNames())
                {
                    if (printerName == installedPrinter)
                    {
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
            msgMail.Subject = "Print Error on " + subject;
            msgMail.Body = msgText;
            msgMail.IsBodyHtml = true;

            //send message
            mailClient.Send(msgMail);

            //garbage collect
            msgMail.Dispose();
        }

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

            writeToFile("Service Stopped");
            condenseFile();
        }
    }
}
