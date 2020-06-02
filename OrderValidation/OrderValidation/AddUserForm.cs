using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Numerics;
using System.Windows.Forms;
using System.Drawing.Printing;

using Microsoft.Win32;
using Microsoft.Office.Interop.Word;

namespace OrderValidation
{
    public partial class AddUserForm : Form
    {
        string[] warehouses = new string[] {"RENO", "FORT WORTH", "FL", "PA", "SPARKS2", "TX CONSIGN" };

        bool badgePrinting;
        string id;
        string name;
        string location;

        ManagerMonitor managerMonitor;


        string strDefaultPrinter = null;
        public string printerValue = null;
        public string portName = null;
        public RegistryKey subkey = null;

        public object misValue = System.Reflection.Missing.Value;

        public string installedPrinter = null;

        public Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

        public AddUserForm(ManagerMonitor inManagerMonitor, string loc)
        {
            InitializeComponent();

            managerMonitor = inManagerMonitor;
            location = loc;

            NameText.Focus();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            int manager = 0;

            if(managerCheckBox.Checked)
            {
                manager = 1;
            }

            try
            {
                id = ID_BOX.Text.ToUpper();
                name = NameText.Text;

                if (id == "")
                    throw new Exception("Please enter valid ID");

                if(name == "")
                    throw new Exception("Please enter valid name");

                if (id.Contains('-'))
                {
                    location = id.Split('-')[1];

                    if (location == "TXFW")
                        location = "FORT WORTH";

                    if(location == "TXC")
                        location = "TX CONSIGN";

                    if (location == "SP2")
                        location = "SPARKS2";

                    if (!warehouses.Contains(location))
                        throw new Exception("INVALID LOCATION CODE");
                    id = id.Split('-')[0];
                }

                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //get unprocessed invoices from database
                    string cmdString = "insert into wmsUsers (user_id, user_name, user_loc, user_mgr) " +
                        "values ('"+id+"', '"+name+"', '"+location+"', "+manager+")";

                    OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                    pSqlConn.Open();
                    cmd.ExecuteNonQuery();
                    pSqlConn.Close();
                }

                if (badgePrinting)
                {
                    DialogResult dialog = MessageBox.Show("Print Badge Labels?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialog == DialogResult.Yes)
                    {
                        if (location == "FORT WORTH")
                            location = "TXFW";

                        if (location == "TX CONSIGN")
                            location = "TXC";

                        if (location == "SPARKS2")
                            location = "SP2";

                        labelPrintWord(id + "-"+location);
                        labelPrintWord(NameText.Text);
                    }
                }

                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private string selectPrinter(string labelPrinterName)
        {
            installedPrinter = labelPrinterName;

            string selectedPrinter;
            int flagPrinterFound = 0;

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
                // LocationLabels.Program.xlApp.ActivePrinter = installedPrinter + " on " + portName;
                selectedPrinter = installedPrinter + " on " + portName;
                return selectedPrinter;


            }
            else
            {
                MessageBox.Show("The printer " + labelPrinterName + " could not be found. Badge printing disabled.");
                return "";
            }
        }

        private void AddUserForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            strDefaultPrinter = null;

            printerValue = null;
            portName = null;
            subkey = null;

            winword.ActivePrinter = strDefaultPrinter;
            winword.Quit();
            releaseObject(winword);

            managerMonitor.Enabled = true;
            managerMonitor.hold = false;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void labelPrintWord(string strLocation)
        {

            winword.ActivePrinter = selectPrinter(variables.printerLabelLarge);

            //Create a missing variable for missing value
            object missing = System.Reflection.Missing.Value;
            //Create a new document
            Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            //adding text to document
            document.PageSetup.TopMargin = 10;
            document.PageSetup.BottomMargin = 10;
            document.PageSetup.LeftMargin = 10;
            document.PageSetup.RightMargin = 10;
            document.PageSetup.Orientation = WdOrientation.wdOrientPortrait;

            Paragraph pText = document.Content.Paragraphs.Add();
            pText.WordWrap = 0;
            pText.Range.Font.Size = 45;
            pText.Range.Font.Bold = 1;
            pText.Range.Font.Name = "Arial";
            pText.Range.Text = strLocation;
            pText.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            pText.Range.InsertParagraphAfter();

            Paragraph bText = document.Content.Paragraphs.Add();
            bText.Range.Font.Size = 60;
            bText.Range.Font.Scaling = 70;
            bText.Range.Font.Name = "Free 3 of 9";
            bText.Range.Text = "*" + strLocation + "*";
            bText.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

            document.Activate();
            //for(int i = 0; i < 30; i++)
            document.PrintOut();
            document.Close(WdSaveOptions.wdDoNotSaveChanges);

            winword.ActivePrinter = strDefaultPrinter;
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {

            winword.Visible = false;
            winword.ScreenUpdating = false;
            winword.DisplayAlerts = WdAlertLevel.wdAlertsNone;

            strDefaultPrinter = winword.ActivePrinter;


            variables.printerLabelLarge = "loc3844z";
            variables.printerLabelLargeTopMargin = Convert.ToDouble("0");
            variables.printerLabelLargeLeftMargin = Convert.ToDouble("0");

            badgePrinting = (selectPrinter(variables.printerLabelLarge) != "");

        }
    }

    class variables
    {
        public static string printerLabelLarge;
        public static double printerLabelLargeTopMargin;
        public static double printerLabelLargeLeftMargin;

    }
}
