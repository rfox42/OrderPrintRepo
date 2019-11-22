using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;
using System.IO;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Drawing.Printing;
using System.Printing;

using Microsoft.Win32;
using Microsoft.Office.Interop.Excel;

namespace OrderPrint
{
    class Program
    {

        // Create Excel Application Instance for use by Program Forms
        public static Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        public static Workbook xlWorkBook = Program.xlApp.Workbooks.Add();
        public static Worksheet xlsheet;

        public static Range xlBarcodeTop;
        public static Range xlBarcodeBottom;

        public static object misValue = System.Reflection.Missing.Value;

        public static string strActivePrinter = null;
        public static string printerValue = null;
        public static string portName = null;
        public static RegistryKey subkey = null;

        public static string installedPrinter = null;

        static System.Timers.Timer timer;
        public static int flagActive;
        static bool updateBinsFlag = false;
        static bool invoiceTest = false;
        static Order currentOrder;
        static bool REPRINT;

        //List<InvoiceForm> invoice = new List<InvoiceForm>();

        static void Main(string[] args)
        {
            //if update bins flag set
            if (updateBinsFlag)
            {
                //call update bins
                updateBins(@"C:\Users\rfox\Documents\GitHubRepo\OrderPrintRepo\OrderPrint\newCompressors.xlsx");
            }


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


            timer = new System.Timers.Timer(10000);
            timer.Elapsed += new ElapsedEventHandler(timerProgram);
            timer.Enabled = true;
            timer.Start();

            while (Console.Read() != 'q')
            {
                ;  // Do nothing until the user presses 'q' and then 'enter'  
            }

            // Close down program
            // Allow new instances of Excel from using this applications workbook
            Program.xlApp.IgnoreRemoteRequests = false;

            Program.xlWorkBook.Close(false, misValue, misValue);
            Program.xlApp.Application.Quit();
            Program.xlApp.Quit();

            releaseObject(xlsheet);
            releaseObject(Program.xlWorkBook);
            releaseObject(Program.xlApp);

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
                Console.WriteLine("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }


        static void timerProgram(object sender, ElapsedEventArgs e)
        {
            if (flagActive == 0)
            {
                newOrders();
            }


            PrintServer printServer = new PrintServer(installedPrinter);
            PrintQueueCollection printQueues = printServer.GetPrintQueues();
            foreach (PrintQueue pq in printQueues)
            {
                pq.Refresh();
                PrintJobInfoCollection pCollection = pq.GetPrintJobInfoCollection();
                if ((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error || (pq.QueueStatus & PrintQueueStatus.Offline) == PrintQueueStatus.Offline || (pq.QueueStatus & PrintQueueStatus.None) != PrintQueueStatus.None)
                {
                    string text = "There appears to be a problem printing order " + currentOrder.invoiceNumber + " to " + installedPrinter + ".";
                    text += System.Environment.NewLine + "Please check the printer.";
                    SendEmail(installedPrinter, text);
                    break;
                }
            }
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
            string strConnection = "DSN=Ranshu20190831";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get order information
                string sqlInvoice =
                "SELECT BKAR_INV_NUM, BKAR_INV_INVDTE, BKAR_INV_CUSCOD, BKAR_INV_CUSNME, BKAR_INV_CUSA1, BKAR_INV_CUSA2, BKAR_INV_CUSCTY, BKAR_INV_CUSST, BKAR_INV_CUSZIP, BKAR_INV_CUSCUN, BKAR_INV_SHPNME, BKAR_INV_SHPA1, BKAR_INV_SHPA2, BKAR_INV_SHPCTY, BKAR_INV_SHPST, BKAR_INV_SHPZIP, BKAR_INV_SHPCUN, BKAR_INV_LOC, BKAR_INV_CUSORD, BKAR_INV_SHPVIA, BKAR_INV_TERMD, BKAR_INV_ENTBY, BKAR_INV_TOTAL, invoice_num" +
                " FROM BKARHINV LEFT JOIN wmsOrders ON BKAR_INV_NUM = invoice_num " +
                " WHERE BKAR_INV_MAX = 0 ";
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

                        if(readerINV["invoice_num"].ToString() == order.invoiceNumber.ToString())
                        {
                            REPRINT = true;
                            xlsheet.Cells[1, 6].Value = "REPRINT";
                        }
                        else
                        {
                            xlsheet.Cells[1, 6].Value = "";
                        }

                        ///////////////////////////////////////////////
                        InvoiceForm invoiceForm = new InvoiceForm(1);

                        //get items for order
                        int flagPrint = 1;
                        string sqlItems = "SELECT BKAR_INVL_PCODE, BKAR_INVL_ITYPE, BKAR_INVL_PQTY, BKAR_INVL_PDESC, BKAR_INVL_LOC, BKAR_INVL_MSG, BKIC_VND_PART, BIN_NAME " +
                            "FROM BKARHINV INNER JOIN BKARHIVL ON BKAR_INV_NUM = BKAR_INVL_INVNM " +
                            "LEFT JOIN BKICCUST ON (BKAR_INVL_PCODE = BKIC_VND_PCODE AND BKAR_INV_CUSCOD = BKIC_VND_VENDOR)" +
                            "LEFT JOIN wmsLocations ON (BIN_PART = BKAR_INVL_PCODE AND BIN_LOC = BKAR_INVL_LOC) " +
                            "WHERE BKAR_INV_NUM = '" + readerINV["BKAR_INV_NUM"] + "'";
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
                                    item.quantity = Convert.ToInt32( Convert.ToDouble( readerItems["BKAR_INVL_PQTY"].ToString()));
                                    item.description = readerItems["BKAR_INVL_PDESC"].ToString();
                                    item.message = readerItems["BKAR_INVL_MSG"].ToString().TrimEnd();
                                    item.locationCode = readerItems["BKAR_INVL_LOC"].ToString().TrimEnd();
                                    item.vendorPart = readerItems["BKIC_VND_PART"].ToString();

                                    //if item has bin
                                    if(readerItems["BIN_NAME"].ToString() != "")
                                    {
                                        item.location = "BIN " + readerItems["BIN_NAME"].ToString();
                                    }
                                    //else print standard location code
                                    else
                                    {
                                        item.location = "A12-08-05-2";
                                    }

                                    
                                    // Check for shipping Notes
                                    if (item.itemType == "X")
                                    {
                                            notes.Add(item.message);
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
                        else if (order.paymentTerms == "VISA/MC" && !REPRINT)
                        {
                            string creditNotes = "";

                            for(int i = 0; i < notes.Count; i++)
                            {
                                creditNotes += System.Environment.NewLine + notes[i];
                            }

                            Console.Write(creditNotes);

                            //add to credit table with Processed flagged false
                            string creditString = "INSERT INTO CRDTINV (CRDT_INV_NUM, CRDT_INV_CUSCOD, CRDT_INV_DATE, CRDT_INV_TOTAL, CRDT_INV_PROCESSED, CRDT_INV_NOTES, CRDT_INV_SHPVIA) " +
                                "VALUES (" + order.invoiceNumber + ", '" + order.customerCode + "', '" + order.date + "', " + readerINV["BKAR_INV_TOTAL"].ToString() + ", 0, '"+ creditNotes +"', '"+ order.deliveryMethod +"')";

                            using (OdbcCommand creditCommand = new OdbcCommand(creditString, pSqlConn))
                            {
                                creditCommand.ExecuteNonQuery();
                            }
                        }

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
                        flagPrint = selectPrinter(strInvLoc);
                        Console.WriteLine(xlApp.ActivePrinter.ToString());

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

                            if ((numPageItems > 21) && (strInvLoc == item.locationCode))
                            {

                                xlsheet.Cells[49, 4].Value = "Continued on Next Page...";

                                // Print Out Current PackList
                                if(flagPrint == 1)
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

                            }else if ((strInvLoc != item.locationCode) && (item.itemType != "X") && (item.itemType != "N"))
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
                                flagPrint = selectPrinter(strInvLoc);

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
                            
                            if (item.itemType == "X" || item.itemType == "N")
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
                        if(invoiceTest)
                        {
                            invoiceForm.Show();
                            //invoiceForm.Location = new System.Drawing.Point(-30000, -30000);
                            invoiceForm.print();
                        }
                        else if (flagPrint == 1)
                        {
                           xlsheet.PrintOutEx(misValue, misValue, 1, false);
                            
                        }
                        

                        strCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                        sqlUpdateList = "INSERT INTO wmsOrders (invoice_num,printed,printer) " +
                                        "VALUES (" + order.invoiceNumber + ", '" + strCurrentDateTime.ToString() +
                                        "', '" + xlApp.ActivePrinter.ToString() + "');" +
                                        "UPDATE BKARHINV " +
                                        "SET BKAR_INV_MAX = 1 " +
                                        "WHERE BKAR_INV_NUM = " + order.invoiceNumber;
                        using (OdbcCommand cmd = new OdbcCommand(sqlUpdateList, pSqlConn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // CLEAN UP LINE ITEMS AND EXTRA PAGES
                        xlsheet.get_Range("A26", "I49").Clear();
                        
                        // CLEAN UP NOTES
                        xlsheet.get_Range("A11", "E15").Clear();

                        // ALIGN CELLS PROPERLY
                        xlsheet.get_Range("A26", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
                        
                        Console.WriteLine(readerINV["BKAR_INV_NUM"]);
                        invoiceForm.Close();
                    }
                }
                readerINV.Close();
                pSqlConn.Close();
            }

            Console.WriteLine("END SET");
            flagActive = 0;
        }

        public static int selectPrinter(string locationcode)
        {
            int flagPrinterFound = 0;
            string backupPrinter;

            // ASSIGN PRINTER BASED OFF OF WAREHOUSE CODE
            if (locationcode == "RENO")
            {
                installedPrinter = "RICOHNV";
            }
            else if (locationcode == "SPARKS")
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
            }
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
            }else
            {
                installedPrinter = null;
            }

            if (installedPrinter != null) { 
                // GRAB REGISTRY KEY NAME/VALUE PAIRS FOR ALL PRINTERS INSTALLED ON MACHINE
                // SEARCH FOR NAME/VALUE PAIR WITH z3844 AS THE NAME AND EXTRACT THE PORT NAME FROM IT
                // USE THE PORT NAME TO SPECIFY THE ACTIVE PRINTER IN THE EXCEL INSTANCE
                subkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Devices", false);
                foreach (string printerName in subkey.GetValueNames())
                {
                    if (printerName == installedPrinter)
                    {
                        printerValue = subkey.GetValue(printerName).ToString();
                        flagPrinterFound = 1;
                    }
                    else if(printerName == "RICOHNV")
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
                    string text = "There appears to be a problem printing order "+ currentOrder.invoiceNumber +" to " + installedPrinter + ".";
                    text += System.Environment.NewLine + "Please check the printer.";

                    if (installedPrinter != "RICOHNV")
                    {
                        text += System.Environment.NewLine + "The invoice will be printed to RICOHNV to compensate.";
                        flagPrinterFound = selectPrinter("RENO");
                    }

                    SendEmail(installedPrinter, text);
                }
            }
            return flagPrinterFound;
        }

        static void SendEmail(string subject, string msgText)
        {
            SmtpClient mailClient = new SmtpClient("secure.emailsrvr.com");
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = new System.Net.NetworkCredential("orders@ranshu.com", "%Ranshu525252");
            mailClient.Port = 587;
            mailClient.EnableSsl = true;

            MailMessage msgMail;


            msgMail = new MailMessage(new MailAddress("orders@ranshu.com"), new MailAddress("ryan@ranshu.com"));
            //msgMail.CC.Add(new MailAddress("jeremy@ranshu.com"));

            msgMail.Subject = "Print Error on " + subject;
            msgMail.Body = msgText;
            msgMail.IsBodyHtml = true;
            mailClient.Send(msgMail);
            msgMail.Dispose();
        }

        /*
         * @FUNCTION:   static void updateBins()
         * @PURPOSE:    updates database bin locations from given excel document
         *              
         * @PARAM:      string updateListAddress
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        static void updateBins(string updateListAddress)
        {
            //open bins excel sheet
            Workbook tempWorkbook = Program.xlApp.Workbooks.Open(updateListAddress);
            Worksheet tempSheet = tempWorkbook.Sheets[1];
            Range tempRange = tempSheet.UsedRange;

            //create item list
            List<Item> items = new List<Item>();

            //for each row in the sheet
            for (int i = 2; tempSheet.Cells[i, 1].Value != null; i++)
            {
                Item item = new Item();

                //get part code
                item.partCode = tempSheet.Cells[i, 1].Value;

                //if the partcode starts with 14/is a compressor
                if (item.partCode.Substring(0, 2) == "14")
                {
                    //add to list
                    item.location = "RENO";
                    item.quantity = Convert.ToInt32(tempSheet.Cells[i, 11].Value);
                    item.locationCode = "D";

                    items.Add(item);
                }
            }

            //connect to database
            string strConnection = "DSN=Ranshu20190831";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get all bin parts and locations
                string sqlInvoice = "SELECT BIN_PART, BIN_LOC FROM wmsLocations";
                OdbcCommand sqlCommandINV = new OdbcCommand(sqlInvoice, pSqlConn);
                sqlCommandINV.CommandTimeout = 90;
                pSqlConn.Open();
                OdbcDataReader binReader = sqlCommandINV.ExecuteReader();

                //if table has rows
                if (binReader.HasRows)
                {
                    while (binReader.Read())
                    {
                        //compare current list to database
                        for (int i = 0; i < items.Count; i++)
                        {
                            //if part and location match database entry
                            if (binReader["BIN_PART"].ToString() == items[i].partCode && binReader["BIN_LOC"].ToString() == items[i].location)
                            {
                                //update current entry with bin name and quantity
                                string updateString = "UPDATE wmsLocations " +
                                    "SET BIN_NAME = '" + items[i].locationCode + "', BIN_QTY = " + items[i].quantity + " " +
                                    "WHERE BIN_PART = '" + items[i].partCode + "' AND BIN_LOC = '" + items[i].location + "'";

                                using (OdbcCommand updateCommand = new OdbcCommand(updateString, pSqlConn))
                                {
                                    updateCommand.ExecuteNonQuery();
                                }

                                //remove part from list
                                items.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }

                //if the list still has parts
                for (int i = 0; i < items.Count; i++)
                {
                    //add them to the database
                    string addString = "INSERT INTO wmsLocations (BIN_NAME, BIN_LOC, BIN_PART, BIN_QTY) " +
                        "VALUES ('" + items[i].locationCode + "', '" + items[i].location + "', '" + items[i].partCode + "', " + items[i].quantity + ")";

                    using (OdbcCommand addCommand = new OdbcCommand(addString, pSqlConn))
                    {
                        addCommand.ExecuteNonQuery();
                    }

                }

                //close database connection
                binReader.Close();
                pSqlConn.Close();
            }

            //release xl assets
            releaseObject(tempSheet);
            releaseObject(tempWorkbook);
        }
    }    


}