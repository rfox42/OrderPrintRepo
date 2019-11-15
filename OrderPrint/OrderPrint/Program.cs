using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;
using System.IO;
using System.Data.Odbc;
using System.Data.SqlClient;

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

        static Timer timer;
        public static int flagActive;

        //List<InvoiceForm> invoice = new List<InvoiceForm>();

        static void Main(string[] args)
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


            timer = new Timer(30000);
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
        }

        static void newOrders()
        {
            flagActive = 1;

            string sqlUpdateList;
            string strCurrentDateTime;

            string strInvLoc;

            string[,] arrOrder = new string[501, 30];
            string[] arrNotes = new string[10];

            // Copy all invoices from Addsum not listed in wmsOrders table
            string strConnection = "DSN=Ranshu20190831";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                string sqlInvoice = "SELECT BKAR_INV_NUM, " +
                        " invoice_num" +
                        " FROM BKARHINV" +
                        " LEFT JOIN wmsOrders" +
                        " ON BKAR_INV_NUM = invoice_num" +
                        " WHERE BKAR_INV_INVDTE > '2019-11-10'" +
                        " AND invoice_num IS NULL";
                /*"SELECT BKAR_INV_NUM" +
                " FROM BKARHINV" +
                " WHERE BKAR_INV_INVDTE <= '0001-12-31 16:00:00.000'";*/
                OdbcCommand sqlCommandINV = new OdbcCommand(sqlInvoice, pSqlConn);
                sqlCommandINV.CommandTimeout = 90;
                pSqlConn.Open();
                OdbcDataReader readerINV = sqlCommandINV.ExecuteReader();
                if (readerINV.HasRows)
                {
                    while (readerINV.Read())
                    {

                        Array.Clear(arrOrder, 0, arrOrder.Length);

                        int numRow = 0;
                        int numNotes = 0;
                        int flagPrint = 1;
                        string sqlItems = "SELECT BKAR_INV_NUM, BKAR_INV_INVDTE, BKAR_INV_CUSCOD, BKAR_INV_CUSNME, BKAR_INV_CUSA1, BKAR_INV_CUSA2, BKAR_INV_CUSCTY, BKAR_INV_CUSST, BKAR_INV_CUSZIP, BKAR_INV_CUSCUN, BKAR_INV_SHPNME, BKAR_INV_SHPA1, BKAR_INV_SHPA2, BKAR_INV_SHPCTY, BKAR_INV_SHPST, BKAR_INV_SHPZIP, BKAR_INV_SHPCUN, BKAR_INV_LOC, BKAR_INV_CUSORD, BKAR_INV_SHPVIA, BKAR_INV_TERMD, BKAR_INV_ENTBY, BKAR_INVL_PCODE, BKAR_INVL_ITYPE, BKAR_INVL_PQTY, BKAR_INVL_PDESC, BKAR_INVL_LOC, BKAR_INVL_MSG, BKIC_VND_PART FROM BKARHINV INNER JOIN BKARHIVL ON BKAR_INV_NUM = BKAR_INVL_INVNM LEFT JOIN BKICCUST ON (BKAR_INVL_PCODE = BKIC_VND_PCODE AND BKAR_INV_CUSCOD = BKIC_VND_VENDOR) WHERE BKAR_INV_NUM = '" + readerINV["BKAR_INV_NUM"] + "'";
                        using (OdbcCommand sqlCommandItems = new OdbcCommand(sqlItems, pSqlConn))
                        {
                            OdbcDataReader readerItems = sqlCommandItems.ExecuteReader();
                            if (readerItems.HasRows)
                            {
                                while (readerItems.Read())
                                {

                                    arrOrder[numRow, 0] = readerItems["BKAR_INV_NUM"].ToString();
                                    arrOrder[numRow, 1] = String.Format("{0:MM/dd/yyyy}", readerItems["BKAR_INV_INVDTE"]);
                                    arrOrder[numRow, 2] = readerItems["BKAR_INV_CUSCOD"].ToString();
                                    arrOrder[numRow, 3] = readerItems["BKAR_INV_CUSNME"].ToString();
                                    arrOrder[numRow, 4] = readerItems["BKAR_INV_CUSA1"].ToString();
                                    arrOrder[numRow, 5] = readerItems["BKAR_INV_CUSA2"].ToString();
                                    arrOrder[numRow, 6] = readerItems["BKAR_INV_CUSCTY"].ToString();
                                    arrOrder[numRow, 7] = readerItems["BKAR_INV_CUSST"].ToString();
                                    arrOrder[numRow, 8] = readerItems["BKAR_INV_CUSZIP"].ToString();
                                    arrOrder[numRow, 9] = readerItems["BKAR_INV_CUSCUN"].ToString();
                                    arrOrder[numRow, 10] = readerItems["BKAR_INV_SHPNME"].ToString();
                                    arrOrder[numRow, 11] = readerItems["BKAR_INV_SHPA1"].ToString();
                                    arrOrder[numRow, 12] = readerItems["BKAR_INV_SHPA2"].ToString();
                                    arrOrder[numRow, 13] = readerItems["BKAR_INV_SHPCTY"].ToString();
                                    arrOrder[numRow, 14] = readerItems["BKAR_INV_SHPST"].ToString();
                                    arrOrder[numRow, 15] = readerItems["BKAR_INV_SHPZIP"].ToString();
                                    arrOrder[numRow, 16] = readerItems["BKAR_INV_SHPCUN"].ToString();
                                    arrOrder[numRow, 17] = readerItems["BKAR_INV_LOC"].ToString();
                                    arrOrder[numRow, 18] = readerItems["BKAR_INV_CUSORD"].ToString();
                                    arrOrder[numRow, 19] = readerItems["BKAR_INV_SHPVIA"].ToString();
                                    arrOrder[numRow, 20] = readerItems["BKAR_INV_TERMD"].ToString();
                                    arrOrder[numRow, 21] = readerItems["BKAR_INV_ENTBY"].ToString();
                                    arrOrder[numRow, 22] = readerItems["BKAR_INVL_PCODE"].ToString();
                                    arrOrder[numRow, 23] = readerItems["BKAR_INVL_ITYPE"].ToString();
                                    arrOrder[numRow, 24] = readerItems["BKAR_INVL_PQTY"].ToString();
                                    arrOrder[numRow, 25] = readerItems["BKAR_INVL_PDESC"].ToString();
                                    arrOrder[numRow, 26] = readerItems["BKAR_INVL_MSG"].ToString();
                                    arrOrder[numRow, 27] = readerItems["BKAR_INVL_LOC"].ToString();
                                    arrOrder[numRow, 28] = readerItems["BKIC_VND_PART"].ToString();
                                    arrOrder[numRow, 29] = "A12-08-05-2";

                                    // Check for shipping Notes
                                    if (arrOrder[numRow, 23].ToString() == "X")
                                    {
                                        if (arrOrder[numRow, 26].ToString().Substring(0, 1) == "@")
                                        {
                                            arrNotes[numNotes] = arrOrder[numRow, 26].ToString();
                                            numNotes++;
                                        }
                                    }

                                    

                                    numRow = numRow + 1;

                                }
                            }
                            readerItems.Close();
                        }

                        int numItems = 0;
                        int numPage = 1;

                        // Find first location code
                        strInvLoc = arrOrder[0, 27].ToString().TrimEnd();
                        for (int n = 0; n < arrOrder.Length; n++)
                        {
                            if(arrOrder[n,23].ToString() != "X")
                            {
                                strInvLoc = arrOrder[n, 27].ToString().TrimEnd();
                                break;
                            }
                            
                        }
                        
                        
                        //update excel template
                        // TOP Header Section
                        xlBarcodeTop.Value = "*" + arrOrder[0, 0].ToString() + "*";

                        xlsheet.Cells[1, 1].Value = "Date:";
                        xlsheet.Cells[1, 2].value = arrOrder[0, 1].ToString();

                        xlsheet.Cells[1, 6].value = arrOrder[0, 21].ToString();

                        xlsheet.Cells[3, 1].Value = "Ship To:";
                        xlsheet.Cells[5, 1].value = arrOrder[0, 10].ToString();
                        xlsheet.Cells[6, 1].value = arrOrder[0, 11].ToString();
                        xlsheet.Cells[7, 1].value = arrOrder[0, 12].ToString();
                        xlsheet.Cells[8, 1].value = arrOrder[0, 13].ToString().TrimEnd() + ", " + arrOrder[0, 14].ToString() + "  " + arrOrder[0, 15].ToString();

                        xlsheet.Cells[3, 5].Value = "Bill To:";
                        xlsheet.Cells[5, 5].value = arrOrder[0, 3].ToString();
                        xlsheet.Cells[6, 5].value = arrOrder[0, 4].ToString();
                        xlsheet.Cells[7, 5].value = arrOrder[0, 5].ToString();
                        xlsheet.Cells[8, 5].value = arrOrder[0, 6].ToString().TrimEnd() + ", " + arrOrder[0, 7].ToString() + "  " + arrOrder[0, 8].ToString();

                        xlsheet.Cells[4, 9].Value = "PickNote:";
                        xlsheet.Cells[4, 11].value = arrOrder[0, 0].ToString();
                        xlsheet.Cells[5, 9].Value = "Location:";
                        xlsheet.Cells[5, 11].value = strInvLoc;
                        xlsheet.Cells[6, 9].Value = "Customer PO:";
                        xlsheet.Cells[6, 11].value = arrOrder[0, 18].ToString();
                        xlsheet.Cells[7, 9].Value = "Account:";
                        xlsheet.Cells[7, 11].value = arrOrder[0, 2].ToString();
                        xlsheet.Cells[8, 9].Value = "Delivery Method:";
                        xlsheet.Cells[8, 11].value = arrOrder[0, 19].ToString();
                        xlsheet.Cells[9, 9].Value = "Payment Terms:";
                        xlsheet.Cells[9, 11].value = arrOrder[0, 20].ToString();

                        xlsheet.Cells[13, 10].Value = "# of Boxes: __________";

                        xlsheet.Cells[10, 1].Value = "NOTES";
                        xlsheet.get_Range("A10", "G10").Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d;

                        //Print Notes if any
                        int currentNoteRow = 11;
                        int currentNoteCol = 1;
                        // IF CALIFORNIA SHIPTO THEN ADD PROP65 LABEL
                        if (arrOrder[0, 14].ToString() == "CA")
                        {
                            arrNotes[numNotes] = "PROP 65 LABEL";
                            numNotes++;
                        }

                        for (int n = 0; n <= arrNotes.Length; n++)
                        {
                            if (arrNotes[n] == null)
                            {
                                break;
                            }
                            if (currentNoteRow > 15)
                            {
                                currentNoteRow = 11;
                                currentNoteCol = 5;
                            }

                            xlsheet.Cells[currentNoteRow, currentNoteCol].value = arrNotes[n].ToString();
                            currentNoteRow++;
                        }


                        // BOTTOM Header Section
                        xlBarcodeBottom.Value = "*" + arrOrder[0, 0].ToString() + "*";

                        xlsheet.Cells[18, 1].value = arrOrder[0, 10].ToString();
                        xlsheet.Cells[19, 1].value = arrOrder[0, 11].ToString();
                        xlsheet.Cells[20, 1].value = arrOrder[0, 12].ToString();
                        xlsheet.Cells[21, 1].value = arrOrder[0, 13].ToString().TrimEnd() + ", " + arrOrder[0, 14].ToString() + "  " + arrOrder[0, 15].ToString();

                        xlsheet.Cells[17, 7].value = arrOrder[0, 0].ToString();
                        xlsheet.Cells[18, 7].value = arrOrder[0, 1].ToString();
                        xlsheet.Cells[19, 7].value = arrOrder[0, 21].ToString();
                        xlsheet.Cells[20, 7].value = arrOrder[0, 2].ToString();

                        xlsheet.Cells[21, 11].value = strInvLoc;
                        xlsheet.Cells[22, 11].value = arrOrder[0, 19].ToString();

                        // ASSIGN PRINTER BASED OFF OF WAREHOUSE CODE
                        flagPrint = selectPrinter(strInvLoc);
                        Console.WriteLine(xlApp.ActivePrinter.ToString());

                        // Dont print FAX
                        if (arrOrder[0,19].ToString().TrimEnd() == "FAX")
                        {
                            flagPrint = 0;
                        }else if(arrOrder[0, 19].ToString().TrimEnd() == "RMT")
                        {
                            flagPrint = 0;
                        }

                        int numPageItems = 0;
                        int currentRow = 26;
                        for (int x = 0; x <= arrOrder.GetLength(0); x++)
                        {
                            if (arrOrder[x, 0] == null)
                            {
                                break;
                            }

                            if ((numPageItems > 21) && (strInvLoc == arrOrder[x,27].ToString().TrimEnd()))
                            {

                                xlsheet.Cells[49, 4].Value = "Continued on Next Page...";

                                // Print Out Current PackList
                                if(flagPrint == 1)
                                {
                                    //xlsheet.PrintOutEx(misValue, misValue, 1, false);
                                }

                                numPage = numPage + 1;
                                numPageItems = 0;
                                currentRow = 26;

                                // CLEAR OUT TOP Header Sections
                                xlBarcodeTop.Value = "*" + arrOrder[0, 0].ToString() + "*";

                                xlsheet.Cells[1, 1].Value = "Date:";
                                xlsheet.Cells[1, 2].value = arrOrder[0, 1].ToString();

                                xlsheet.Cells[1, 6].value = arrOrder[0, 21].ToString();

                                xlsheet.Cells[3, 1].Value = "Ship To:";
                                xlsheet.Cells[5, 1].value = arrOrder[0, 10].ToString();
                                xlsheet.Cells[6, 1].value = arrOrder[0, 11].ToString();
                                xlsheet.Cells[7, 1].value = arrOrder[0, 12].ToString();
                                xlsheet.Cells[8, 1].value = arrOrder[0, 13].ToString().TrimEnd() + ", " + arrOrder[0, 14].ToString() + "  " + arrOrder[0, 15].ToString();

                                xlsheet.Cells[3, 5].Value = "";
                                xlsheet.Cells[5, 5].value = "";
                                xlsheet.Cells[6, 5].value = "";
                                xlsheet.Cells[7, 5].value = "";
                                xlsheet.Cells[8, 5].value = "";

                                xlsheet.Cells[4, 9].Value = "PickNote:";
                                xlsheet.Cells[4, 11].Value = arrOrder[0, 0].ToString();
                                xlsheet.Cells[5, 9].Value = "";
                                xlsheet.Cells[5, 11].value = "";
                                xlsheet.Cells[6, 9].Value = "";
                                xlsheet.Cells[6, 11].value = "";
                                xlsheet.Cells[7, 9].Value = "";
                                xlsheet.Cells[7, 11].value = "";
                                xlsheet.Cells[8, 9].Value = "Delivery Method:";
                                xlsheet.Cells[8, 11].value = arrOrder[0, 19].ToString();
                                xlsheet.Cells[9, 9].Value = "";
                                xlsheet.Cells[9, 11].value = "";

                                xlsheet.Cells[13, 10].Value = "# of Boxes: __________";

                                //Clear out notes
                                xlsheet.get_Range("A11", "E15").Clear();
                                xlsheet.get_Range("A11", "E15").HorizontalAlignment = XlHAlign.xlHAlignLeft;

                                // Clear out previous line items
                                xlsheet.get_Range("A26", "I49").Clear();
                                xlsheet.get_Range("A26", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;

                            }else if ((strInvLoc != arrOrder[x,27].ToString().TrimEnd()) && (arrOrder[x,23].ToString() != "X") && (arrOrder[x,23].ToString() != "N"))
                            {
                                xlsheet.Cells[49, 4].Clear();

                                xlsheet.Cells[49, 2].Value = "Total Qty";
                                xlsheet.Cells[49, 3].Value = numItems.ToString();

                                // Print Out Current PackList
                                if (flagPrint == 1)
                                {
                                    //xlsheet.PrintOutEx(misValue, misValue, 1, false);
                                }

                                numItems = 0;
                                numPage = 1;
                                numPageItems = 0;
                                currentRow = 26;

                                strInvLoc = arrOrder[x, 27].ToString().TrimEnd();

                                // ASSIGN PRINTER BASED OFF OF WAREHOUSE CODE
                                flagPrint = selectPrinter(strInvLoc);

                                //update excel template
                                // TOP Header Section
                                xlBarcodeTop.Value = "*" + arrOrder[0, 0].ToString() + "*";

                                xlsheet.Cells[1, 1].Value = "Date:";
                                xlsheet.Cells[1, 2].value = arrOrder[0, 1].ToString();

                                xlsheet.Cells[1, 6].value = arrOrder[0, 21].ToString();

                                xlsheet.Cells[3, 1].Value = "Ship To:";
                                xlsheet.Cells[5, 1].value = arrOrder[0, 10].ToString();
                                xlsheet.Cells[6, 1].value = arrOrder[0, 11].ToString();
                                xlsheet.Cells[7, 1].value = arrOrder[0, 12].ToString();
                                xlsheet.Cells[8, 1].value = arrOrder[0, 13].ToString().TrimEnd() + ", " + arrOrder[0, 14].ToString() + "  " + arrOrder[0, 15].ToString();

                                xlsheet.Cells[3, 5].Value = "Bill To:";
                                xlsheet.Cells[5, 5].value = arrOrder[0, 3].ToString();
                                xlsheet.Cells[6, 5].value = arrOrder[0, 4].ToString();
                                xlsheet.Cells[7, 5].value = arrOrder[0, 5].ToString();
                                xlsheet.Cells[8, 5].value = arrOrder[0, 6].ToString().TrimEnd() + ", " + arrOrder[0, 7].ToString() + "  " + arrOrder[0, 8].ToString();

                                xlsheet.Cells[4, 9].Value = "PickNote:";
                                xlsheet.Cells[4, 11].value = arrOrder[0, 0].ToString();
                                xlsheet.Cells[5, 9].Value = "Location:";
                                xlsheet.Cells[5, 11].value = strInvLoc;
                                xlsheet.Cells[6, 9].Value = "Customer PO:";
                                xlsheet.Cells[6, 11].value = arrOrder[0, 18].ToString();
                                xlsheet.Cells[7, 9].Value = "Account:";
                                xlsheet.Cells[7, 11].value = arrOrder[0, 2].ToString();
                                xlsheet.Cells[8, 9].Value = "Delivery Method:";
                                xlsheet.Cells[8, 11].value = arrOrder[0, 19].ToString();
                                xlsheet.Cells[9, 9].Value = "Payment Terms:";
                                xlsheet.Cells[9, 11].value = arrOrder[0, 20].ToString();

                                xlsheet.Cells[13, 10].Value = "# of Boxes: __________";

                                xlsheet.Cells[10, 1].Value = "NOTES";
                                xlsheet.get_Range("A10", "G10").Borders[XlBordersIndex.xlEdgeBottom].Weight = 2d;

                                // BOTTOM Header Section
                                xlBarcodeBottom.Value = "*" + arrOrder[0, 0].ToString() + "*";

                                xlsheet.Cells[18, 1].value = arrOrder[0, 10].ToString();
                                xlsheet.Cells[19, 1].value = arrOrder[0, 11].ToString();
                                xlsheet.Cells[20, 1].value = arrOrder[0, 12].ToString();
                                xlsheet.Cells[21, 1].value = arrOrder[0, 13].ToString().TrimEnd() + ", " + arrOrder[0, 14].ToString() + "  " + arrOrder[0, 15].ToString();

                                xlsheet.Cells[17, 7].value = arrOrder[0, 0].ToString();
                                xlsheet.Cells[18, 7].value = arrOrder[0, 1].ToString();
                                xlsheet.Cells[19, 7].value = arrOrder[0, 21].ToString();
                                xlsheet.Cells[20, 7].value = arrOrder[0, 2].ToString();

                                xlsheet.Cells[21, 11].value = strInvLoc;
                                xlsheet.Cells[22, 11].value = arrOrder[0, 19].ToString();

                                // Clear out previous line items
                                xlsheet.get_Range("A26", "I49").Clear();
                                xlsheet.get_Range("A26", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;

                           }

                            xlsheet.Cells[15, 11].value = "Page";
                            xlsheet.Cells[15, 12].value = numPage.ToString();
                            
                            xlsheet.Cells[50, 11].value = "Page";
                            xlsheet.Cells[50, 12].value = numPage.ToString();
                            
                            if (arrOrder[x, 23].ToString() == "X")
                            {
                                xlsheet.Cells[currentRow, 4].value = "  " + arrOrder[x, 26].ToString();
                            }
                            else
                            {
                                xlsheet.Cells[currentRow, 1].value = arrOrder[x, 29].ToString();
                                xlsheet.Cells[currentRow, 3].value = arrOrder[x, 24].ToString();
                                xlsheet.Cells[currentRow, 4].value = arrOrder[x, 22].ToString();
                                xlsheet.Cells[currentRow, 7].value = arrOrder[x, 28].ToString();
                                xlsheet.Cells[currentRow, 9].value = arrOrder[x, 25].ToString();
                                numItems = numItems + Convert.ToInt32(arrOrder[x, 24]);

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
                           // xlsheet.PrintOutEx(misValue, misValue, 1, false);
                            
                        }

                        strCurrentDateTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm");
                        sqlUpdateList = "INSERT INTO wmsOrders (invoice_num,printed,printer) VALUES ('" + readerINV["BKAR_INV_NUM"] + "','" + strCurrentDateTime.ToString() + "','" + xlApp.ActivePrinter.ToString() + "')";
                        using (OdbcCommand cmd = new OdbcCommand(sqlUpdateList, pSqlConn))
                        {
                            cmd.ExecuteNonQuery();
                        }


                        /*using (OdbcCommand cmd = new OdbcCommand("order_printed", pSqlConn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue(":in_inv_num", readerINV["BKAR_INV_NUM"]);
                            cmd.Parameters.AddWithValue(":in_time", strCurrentDateTime.ToString());
                            cmd.Parameters.AddWithValue(":in_printer", xlApp.ActivePrinter.ToString());

                            cmd.ExecuteScalar();
                        }*/


                        // CLEAN UP LINE ITEMS AND EXTRA PAGES
                        xlsheet.get_Range("A26", "I49").Clear();
                        
                        // CLEAN UP NOTES
                        for (int n = 0; n < arrNotes.Length; n++)
                        {
                            arrNotes[n] = null;
                        }
                        xlsheet.get_Range("A11", "E15").Clear();

                        // ALIGN CELLS PROPERLY
                        xlsheet.get_Range("A26", "I49").HorizontalAlignment = XlHAlign.xlHAlignLeft;
                        
                        Console.WriteLine(readerINV["BKAR_INV_NUM"]);

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
                }
                if (flagPrinterFound == 1)
                {
                    string[] arrPrinterValue = printerValue.Split(',');
                    portName = arrPrinterValue[1];
                    xlApp.ActivePrinter = installedPrinter + " on " + portName;
                }
                else
                {
                    Console.WriteLine("The printer " + installedPrinter + " could not be found.");
                }
            }
            return flagPrinterFound;
        }
    }    


}