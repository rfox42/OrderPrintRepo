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
using Microsoft.Win32;
using Microsoft.Office.Interop.Excel;

namespace RanshuOrderProcessService
{
    public partial class ServiceMain : ServiceBase
    {
        Timer timer;
        OdbcConnection pSqlConn;
        Application xlApp;

        /// <summary>
        /// initializes service and excel application
        /// </summary>
        public ServiceMain()
        {
            InitializeComponent();

            xlApp = new Application();
        }

        /// <summary>
        /// executes operations at startup
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            Program.writeToFile("Service Started");

            timer = new Timer(20000);
            timer.Elapsed += new ElapsedEventHandler(Timer_Tick);
            timer.Enabled = true;
            timer.Start();
        }

        /// <summary>
        /// executes operations at timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                ///process any invoices in queue amd start timer
                processInvoices();
                timer.Start();
                Program.writeToFile("END SET");
            }
            catch(Exception ex)
            {
                Program.writeToFile(ex.StackTrace + ex.Message);
                Program.SendEmail("ryan@ranshu.com", ex.Message, ex.StackTrace);
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
                Program.writeToFile("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// parses and submits POs to database
        /// </summary>
        private void processInvoices()
        {
            ///create invoice list and set filepaths
            List<Invoice> meridian = new List<Invoice>();
            string queuePath = @"\\RANSHU\Reports\edi\MERIDIAN\Queue";
            string reviewPath = @"\\RANSHU\Reports\edi\MERIDIAN\Queue\Review";
            string labelPath = @"\\RANSHU\Reports\edi\MERIDIAN\SFPLabels";


            if (!Directory.Exists(@"C:\Temp"))
                Directory.CreateDirectory(@"C:\Temp");
            if (!Directory.Exists(@"C:\Windows\SysWOW64\config\systemprofile\Desktop"))
                Directory.CreateDirectory(@"C:\Windows\SysWOW64\config\systemprofile\Desktop");

            ///for each .csv file in the queue
            foreach (string file in Directory.GetFiles(queuePath))
            {
                if (file.EndsWith(".csv"))
                {
                    ///create invoice
                    Invoice invoice = new Invoice();

                    ///set account
                    invoice.account = "MERIDIAN";

                    ///open excel sheet
                    Workbook workbook = xlApp.Workbooks.Open(file);
                    Worksheet worksheet = workbook.Sheets[1];
                    Range range = worksheet.UsedRange;

                    ///get invoice details
                    invoice.fileName = file.Split('\\')[file.Split('\\').Length - 1];
                    invoice.PONum = range.Cells[1, 1].value;
                    invoice.deliveryMethod = range.Cells[1, 16].value;
                    invoice.attention = "";
                    invoice.terms = "NET 30";
                    invoice.termNum = 4;
                    invoice.taxYN = "N";
                    //TODO: CALC FREIGHT

                    ///get shipping address
                    invoice.shipAddress = new Address();
                    if (range.Cells[1, 6].value != null)
                        invoice.shipAddress.name = range.Cells[1, 6].value;
                    if (range.Cells[1, 8].value != null)
                        invoice.shipAddress.line1 = range.Cells[1, 8].value;
                    if (range.Cells[1, 9].value != null)
                        invoice.shipAddress.line2 = range.Cells[1, 9].value;
                    if (range.Cells[1, 10].value != null)
                        invoice.shipAddress.city = range.Cells[1, 10].value;
                    if (range.Cells[1, 11].value != null)
                        invoice.shipAddress.state = range.Cells[1, 11].value;
                    if(range.Cells[1, 12].value != null)
                        invoice.shipAddress.zipcode = range.Cells[1, 12].value.ToString();

                    ///set billing address
                    invoice.billAddress = new Address()
                    {
                        name = "MERIDIAN",
                        line1 = "6740 COBRA WAY SUITE 200",
                        line2 = "",
                        city = "SAN DIEGO",
                        state = "CA",
                        country = "",
                        zipcode = "92121"
                    };

                    ///define delivery method
                    switch (invoice.deliveryMethod)
                    {
                        case "UPS+GND":
                            invoice.deliveryMethod = "Ground";
                            break;

                        case "UPS+NDA":
                            invoice.deliveryMethod = "Next Day Air";
                            break;

                        case "UPS+3DS":
                            invoice.deliveryMethod = "3 Day Select";
                            break;

                        case "SFP+SFP01":
                            invoice.deliveryMethod = "SFP ONE DAY";
                            break;

                        case "SFP+SFP02":
                            invoice.deliveryMethod = "SFP TWO DAY";
                            break;

                        case "SFP+SFP03":
                            invoice.deliveryMethod = "SFP STANDARD";
                            break;

                        default:
                            throw new Exception(invoice.PONum + " Invalid ship method");
                            //TODO: Add other ship methods
                    }

                    ///create part list
                    List<Item> items = new List<Item>();

                    ///parse part list from file
                    for (int i = 1; i <= range.Rows.Count; i++)
                    {
                        Item item = new Item();

                        item.partCode = range.Cells[i, 4].value.ToString();
                        item.partCode = item.partCode.Trim();
                        item.partCode = item.partCode.TrimEnd('.');
                        if (items.Find(x => x.partCode == item.partCode) != null)
                            throw new Exception("Duplicate parts in order");

                        item.price = Convert.ToDouble(range.Cells[i, 18].value);
                        item.quantity = Convert.ToInt32(range.Cells[i, 3].value);
                        item.description = range.Cells[i, 5].value;
                        item.ext = item.quantity * item.price;

                        items.Add(item);
                    }

                    ///set item list
                    invoice.items = items;

                    ///add invoice to list
                    meridian.Add(invoice);

                    workbook.Close(false);
                    releaseObject(worksheet);
                    releaseObject(workbook);
                }
            }



            ///connect to database
            string strConnection = "DSN=RANSHU20190831";
            pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                foreach (Invoice invoice in meridian)
                {
                    ///open connection for invoice
                    pSqlConn.Open();

                    ///initialize location code
                    string location = null;
                    try
                    {
                        ///clear wmsLimbo of items
                        using (OdbcCommand cmd = new OdbcCommand("{call clearLimbo()}", pSqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }
                        

                        ///check for duplicate invoices
                        string cmdString = "select bkar_inv_num from bkarhinv where bkar_inv_cusord = '" + invoice.PONum + "' and bkar_inv_cuscod = '"+invoice.account+"'";

                        using (OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn))
                        {
                            OdbcDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                throw new Exception("PO already present in database");
                            }
                        }

                        ///check ship address
                        if (invoice.shipAddress.name.Length > 30 || invoice.shipAddress.line1.Length > 30 || invoice.shipAddress.line2.Length > 30 || invoice.shipAddress.city.Length > 15 || invoice.shipAddress.state.Length > 2 || invoice.shipAddress.zipcode.Length > 10)
                            throw new Exception("Invalid ship address");

                        ///confirm item prices
                        foreach (Item item in invoice.items)
                        {
                            cmdString = "select bkic_vnd_catcst from bkiccust " +
                                "where bkic_vnd_pcode = '" + item.partCode + "' " +
                                "and bkic_vnd_vendor = '"+invoice.account+"'";

                            using (OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn))
                            {
                                OdbcDataReader reader = cmd.ExecuteReader();

                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    if (Convert.ToDouble(reader["bkic_vnd_catcst"].ToString().TrimEnd()) != item.price)
                                    {
                                        throw new Exception("Incorrect item price");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Invalid part");
                                }
                            }
                        }

                        ///check for SFP label and set location accordingly
                        if (invoice.deliveryMethod.Contains("SFP"))
                        {
                            if (File.Exists(labelPath + "\\" + invoice.PONum + ".zpl"))
                            {
                                location = "RENO";
                            }
                            else if (File.Exists(labelPath + "\\" + invoice.PONum + ".NV.zpl"))
                            {
                                location = "RENO";
                            }
                            else if (File.Exists(labelPath + "\\" + invoice.PONum + ".TX.zpl"))
                            {
                                location = "FORT WORTH";
                            }
                            else
                            {
                                throw new Exception("NO SFP label");
                            }
                        }
                        else
                        {

                            ///get ideal locationcode by region by delivery state
                            cmdString = "select regionCode from regions where state = '" + invoice.shipAddress.state + "'";
                            using (OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn))
                            {
                                OdbcDataReader reader = cmd.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    reader.Read();

                                    switch (Convert.ToInt32(reader["regionCode"]))
                                    {
                                        case 1:
                                            location = "RENO";
                                            break;

                                        case 2:
                                            location = "FORT WORTH";
                                            break;

                                        case 4:
                                            location = "FL";
                                            break;

                                        default:
                                            location = "RENO";
                                            break;
                                    }
                                }
                                else
                                {
                                    throw new Exception("Delivery state invalid");
                                }
                            }
                        }

                        ///confirm location
                        if (location == null)
                            throw new Exception("No valid location found");

                        ///set taxkey
                        switch(location)
                        {
                            case "RENO":
                                invoice.taxKey = "NV";
                                break;

                            case "FORT WORTH":
                                invoice.taxKey = "TX";
                                break;

                            case "FL":
                                invoice.taxKey = "FL";
                                break;
                        }

                        ///get taxrate from taxKey
                        cmdString = "select bkic_tax_rate from bkictax where bkic_tax_state = '" + invoice.taxKey + "'";
                        using (OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn))
                        {
                            OdbcDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                reader.Read();
                                invoice.taxRate = Convert.ToDouble(reader["bkic_tax_rate"]);
                            }
                            else
                            {
                                throw new Exception("No valid tax rate");
                            }
                        }

                        ///calculate invoice totals
                        invoice.calcTotals();

                        ///check price limit automatic processing
                        if (invoice.total > 2000.00)
                        {
                            throw new Exception("Invoice too large for automatic processing");
                        }

                        foreach (Item item in invoice.items)
                        {
                            ///check units on hand for part and validate part code
                            cmdString = "select BKIC_LOC_CODE, BKIC_LOC_UOH from BKICLOC " +
                                "where bkic_loc_prod = '" + item.partCode + "' " +
                                "and bkic_loc_code = '" + location + "'";

                            using (OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn))
                            {
                                OdbcDataReader reader = cmd.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    if (item.quantity > Convert.ToInt32(reader["BKIC_LOC_UOH"].ToString()))
                                    {
                                        throw new Exception("Not enough units on hand to complete order");
                                    }

                                    item.location = location;
                                }
                                else
                                {
                                    throw new Exception("Invalid part");
                                }
                            }

                            ///add part to limbo
                            using(OdbcCommand cmd = new OdbcCommand("{call addPartToLimbo(?, ?, ?, ?, ?, ?)}", pSqlConn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue(":poNum", invoice.PONum);
                                cmd.Parameters.AddWithValue(":part", item.partCode);
                                cmd.Parameters.AddWithValue(":qty", item.quantity);
                                cmd.Parameters.AddWithValue(":price", item.price);
                                cmd.Parameters.AddWithValue(":pext", item.ext);
                                cmd.Parameters.AddWithValue(":pLoc", item.location);

                                cmd.ExecuteNonQuery();
                            }
                        }

                    }
                    catch(Exception ex)
                    {
                        ///catch flags and mark invoice for review
                        invoice.reviewMsg = ex.Message;
                        invoice.reviewFlag = true;
                    }

                    if(invoice.reviewFlag)
                    {
                        ///move invoices marked for review to the appropiate folders and report review messages to log file
                        if(File.Exists(reviewPath + "\\" + invoice.fileName) || invoice.reviewMsg == "PO already present in database")
                        {
                            File.Move(queuePath + "\\" + invoice.fileName, reviewPath + "\\" + invoice.fileName.Replace(".csv", "_Duplicate.csv"));
                            Program.writeToFile("Review " + invoice.PONum + "_Duplicate: " + invoice.reviewMsg);
                        }
                        else
                        {
                            File.Move(queuePath + "\\" + invoice.fileName, reviewPath + "\\" + invoice.fileName);
                            Program.writeToFile("Review: " + invoice.PONum + ": " + invoice.reviewMsg);
                        }
                    }
                    else
                    {
                        try
                        {
                            ///Insertion Transaction
                            ///Gets invoice number for PO and creates rows for parts/invoice
                            /// Any failure during transaction will result in the rollback of all changes
                            int invoiceNum = 0;
                            using (OdbcCommand cmd = new OdbcCommand("{call newInvoice(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)}", pSqlConn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue(":poNum", invoice.PONum);
                                cmd.Parameters.AddWithValue(":cusCode", invoice.account);
                                cmd.Parameters.AddWithValue(":billName", invoice.billAddress.name);
                                cmd.Parameters.AddWithValue(":billA1", invoice.billAddress.line1);
                                cmd.Parameters.AddWithValue(":billA2", invoice.billAddress.line2);
                                cmd.Parameters.AddWithValue(":billCity", invoice.billAddress.city);
                                cmd.Parameters.AddWithValue(":billState", invoice.billAddress.state);
                                cmd.Parameters.AddWithValue(":billZip", invoice.billAddress.zipcode);
                                cmd.Parameters.AddWithValue(":billCountry", invoice.billAddress.country);
                                cmd.Parameters.AddWithValue(":shipName", invoice.shipAddress.name);
                                cmd.Parameters.AddWithValue(":shipA1", invoice.shipAddress.line1);
                                cmd.Parameters.AddWithValue(":shipA2", invoice.shipAddress.line2);
                                cmd.Parameters.AddWithValue(":shipCity", invoice.shipAddress.city);
                                cmd.Parameters.AddWithValue(":shipState", invoice.shipAddress.state);
                                cmd.Parameters.AddWithValue(":shipZip", invoice.shipAddress.zipcode);
                                cmd.Parameters.AddWithValue(":shipCountry", invoice.shipAddress.country);
                                cmd.Parameters.AddWithValue(":shipATN", invoice.attention);
                                cmd.Parameters.AddWithValue(":shipVia", invoice.deliveryMethod);
                                cmd.Parameters.AddWithValue(":payTerms", invoice.terms);
                                cmd.Parameters.AddWithValue(":payNum", invoice.termNum);
                                cmd.Parameters.AddWithValue(":TAXYN", invoice.taxYN);
                                cmd.Parameters.AddWithValue(":subTotal", invoice.subTotal);
                                cmd.Parameters.AddWithValue(":TAXAMT", invoice.tax);
                                cmd.Parameters.AddWithValue(":total", invoice.total);
                                cmd.Parameters.AddWithValue(":taxRate", invoice.taxRate);
                                cmd.Parameters.AddWithValue(":freight", invoice.freight);
                                cmd.Parameters.AddWithValue(":location", location);
                                cmd.Parameters.AddWithValue(":taxKey", invoice.taxKey);

                                ///execute transaction and get invoice num
                                OdbcDataReader reader = cmd.ExecuteReader();
                                if(reader.HasRows)
                                {
                                    reader.Read();
                                    invoiceNum = Convert.ToInt32(reader["inv_num"]);
                                }
                            }

                            ///move file to appropriate folder and report success to log file
                            File.Move(queuePath + "\\" + invoice.fileName, queuePath + "\\Success\\" + invoice.fileName);
                            Program.writeToFile("Success: " + invoice.PONum + " added to database as " + invoiceNum + ".");
                        }
                        catch(Exception ex)
                        {
                            ///move file to review folder on exception
                            File.Move(queuePath + "\\" + invoice.fileName, reviewPath + "\\" + invoice.fileName);
                            Program.writeToFile("Review " + invoice.PONum + ": " + ex.Message);
                        }

                        ///clear limbo rows for next invoice
                        using (OdbcCommand cmd = new OdbcCommand("{call clearLimbo()}", pSqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    pSqlConn.Close();
                }

            }
        }

        /// <summary>
        /// handles stop execution
        /// </summary>
        protected override void OnStop()
        {
            ///releases excel objects
            xlApp.Application.Quit();
            xlApp.Quit();
            GC.Collect();
            releaseObject(xlApp);

            ///report stop to log file
            Program.writeToFile("Service Stopped");
            Program.condenseFile();
        }
    }
}
