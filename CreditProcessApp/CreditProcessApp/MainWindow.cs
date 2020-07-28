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
using System.Windows.Forms;
using System.Net.Mail;

namespace CreditProcessApp
{
    /// <summary>
    /// display credit invoice data for user to process charges
    /// </summary>
    public partial class MainWindow : Form
    {
        Invoice currentInvoice;
        DateTime selectedDate;
        int deliverySelection, declined = 0;
        string[] selectedDelivery = new string[3] {"ALL", "(CRDT_INV_SHPVIA = 'DELIVERY' OR CRDT_INV_SHPVIA = 'WILL CALL')", "(CRDT_INV_SHPVIA != 'DELIVERY' AND CRDT_INV_SHPVIA != 'WILL CALL')" };
        string held;
        string processOrderBy;
        string completeOrderBy;
        string location;
        Account account;
        Login login;
        Timer refreshTimer, idleTimer;
        

        /// <summary>
        /// class constructor
        /// initializes variables and gets form into start position
        /// </summary>
        /// <param name="in_login">
        /// reference to login form
        /// </param>
        public MainWindow(Login in_login, string in_location)
        {
            //create window
            InitializeComponent();

            //set location and login form reference
            location = in_location;
            login = in_login;

            //set initial order flag
            completeOrderBy = processOrderBy = "CRDT_INV_NUM";

            //set start values
            selectedDate = DateTime.Now;

            //draw cell lines
            this.ProcessInvoiceList.CellPaint += tableLayoutPanel1_Paint;
            this.CompleteInvoiceList.CellPaint += tableLayoutPanel1_Paint;

            //set and start timers
            refreshTimer = new Timer();
            refreshTimer.Tick += new EventHandler(refreshTimer_Complete);
            refreshTimer.Interval = 90000;
            refreshTimer.Enabled = true;
            refreshTimer.Start();

            //initialize idletimer
            idleTimer = new Timer();
            idleTimer.Tick += new EventHandler(idleTimer_Tick);
            idleTimer.Interval = 300000;
            idleTimer.Enabled = true;
            idleTimer.Stop();

            //set held flag
            held = "";

            //fill tables with invoice data
            populateTables();

            if(CurrentUser.security_lvl <=10)
            {
                HeldViewButton.Show();
            }
            else
            {
                ProcessLabel.Margin = new Padding(6, 0, 0, 0);
            }

            if(location != "")
            {
                ExitButton.BackColor = Color.Gold;
                ExitButton.Text = "Card on File";
                RemoveButton.Hide();
            }
            else
            {
                button1.Show();
            }

        }

        /// <summary>
        /// 5 minute idle timer to refresh invoices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void idleTimer_Tick(object sender, EventArgs e)
        {
            idleTimer.Stop();
            releaseInvoice();
            this.refreshTimer_Complete(this, new EventArgs());
        }

        /// <summary>
        /// queries database for unprocessed and processed invoices
        /// sends lists of invoices to be displayed
        /// </summary>
        private void populateTables()
        {
            HeldViewButton.BackColor = SystemColors.GradientInactiveCaption;
            RemoveButton.Enabled =
            ReprocessButton.Enabled = 
            ProcessButton.Enabled = 
            ExitButton.Enabled = 
            OtherOrdersButton.Enabled = false;
            releaseInvoice();
            clearData();
            refreshTimer.Stop();

            //create table lists
            List<Invoice> incomplete = new List<Invoice>();
            List<Invoice> complete = new List<Invoice>();

            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string creditCommand = "SELECT CRDT_INV_NUM, CRDT_INV_CUSCOD, CRDT_INV_DATE, CRDT_INV_TOTAL, CRDT_INV_NOTES, CRDT_INV_SHPVIA, CRDT_INV_SLSP, BKAR_INV_LOC " +
                    "FROM CRDTINV LEFT JOIN BKARHINV ON BKAR_INV_NUM = CRDT_INV_NUM " +
                    "WHERE CRDT_INV_PROCESSED = 0 and CRDT_INV_USER "+ held +"= 'null'";
                if(selectedDelivery[deliverySelection % 3] != "ALL")
                {
                    creditCommand += " AND " + selectedDelivery[deliverySelection % 3];
                }

                if (location != "")
                {
                    creditCommand += " AND BKAR_INV_LOC = '"+location+"' ";
                }

                creditCommand += " ORDER BY " + processOrderBy;
                OdbcCommand cmd = new OdbcCommand(creditCommand, pSqlConn);
                pSqlConn.Open();
                OdbcDataReader creditReader = cmd.ExecuteReader();
                if(creditReader.HasRows)
                {
                    while(creditReader.Read())
                    {
                        //create and fill invoice
                        Invoice invoice = new Invoice();

                        invoice.invoiceNumber = Convert.ToInt32(creditReader["CRDT_INV_NUM"].ToString());
                        invoice.account = creditReader["CRDT_INV_CUSCOD"].ToString().TrimEnd();
                        invoice.date = creditReader["CRDT_INV_DATE"].ToString().TrimEnd();
                        invoice.total = Convert.ToDouble(creditReader["CRDT_INV_TOTAL"].ToString());
                        invoice.notes = creditReader["CRDT_INV_NOTES"].ToString().Trim();
                        invoice.deliveryMethod = creditReader["CRDT_INV_SHPVIA"].ToString().TrimEnd();
                        invoice.salesPerson = Convert.ToInt32(creditReader["CRDT_INV_SLSP"].ToString());
                        invoice.setLocation(creditReader["BKAR_INV_LOC"].ToString().TrimEnd());
                        if (invoice.deliveryMethod == "DELIVERY" || invoice.deliveryMethod == "WILL CALL")
                            invoice.retail = 'R';
                        else
                            invoice.retail = 'N';

                        //add invoice to list
                        if((invoice.retail == 'R' && !invoice.notes.Contains("USE CARD ON FILE")) || location == "")
                            incomplete.Add(invoice);
                    }
                }

                creditReader.Close();

                if(held != "!")
                {
                    creditCommand = "SELECT COUNT(*) FROM CRDTINV WHERE CRDT_INV_PROCESSED = 0 and CRDT_INV_USER != 'null'";
                    cmd = new OdbcCommand(creditCommand, pSqlConn);
                    if (Convert.ToInt32(cmd.ExecuteScalar().ToString()) > 0)
                        HeldViewButton.BackColor = Color.Salmon;
                }

                //get processed invoices for selected date (default is currentdate)
                creditCommand = "SELECT CRDT_INV_NUM, CRDT_INV_CUSCOD, CRDT_INV_DATE, CRDT_INV_TOTAL, CRDT_INV_CHARGETIME, CRDT_INV_USER, CRDT_INV_NOTES, CRDT_INV_SHPVIA, CRDT_INV_SLSP " +
                    "FROM CRDTINV WHERE CRDT_INV_PROCESSED = 1 " +
                    "AND CRDT_INV_DECLINED = " + declined + " ";
                if(declined == 1)
                {
                    creditCommand += "AND (CRDT_INV_CHARGETIME >= '" + selectedDate.AddDays(-31).ToString("yyyy-MM-dd") + "' ";
                }
                else
                {
                    creditCommand += "AND (CRDT_INV_CHARGETIME >= '" + selectedDate.ToString("yyyy-MM-dd") + "' ";
                }

                    creditCommand += "AND CRDT_INV_CHARGETIME < '"+ selectedDate.AddDays(1).ToString("yyyy-MM-dd") +"') " +
                    "ORDER BY " + completeOrderBy;
                cmd = new OdbcCommand(creditCommand, pSqlConn);
                creditReader = cmd.ExecuteReader();
                if (creditReader.HasRows)
                {
                    while (creditReader.Read())
                    {
                        //create and fill invoice
                        Invoice invoice = new Invoice();

                        invoice.invoiceNumber = Convert.ToInt32(creditReader["CRDT_INV_NUM"].ToString());
                        invoice.account = creditReader["CRDT_INV_CUSCOD"].ToString().TrimEnd();
                        invoice.date = creditReader["CRDT_INV_DATE"].ToString().TrimEnd();
                        invoice.total = Convert.ToDouble(creditReader["CRDT_INV_TOTAL"].ToString());
                        invoice.chargeTime = creditReader["CRDT_INV_CHARGETIME"].ToString();
                        invoice.user = creditReader["CRDT_INV_USER"].ToString().TrimEnd();
                        invoice.notes = creditReader["CRDT_INV_NOTES"].ToString().Trim();
                        invoice.deliveryMethod = creditReader["CRDT_INV_SHPVIA"].ToString().TrimEnd();
                        invoice.salesPerson = Convert.ToInt32(creditReader["CRDT_INV_SLSP"].ToString());
                        if (invoice.deliveryMethod == "DELIVERY" || invoice.deliveryMethod == "WILL CALL")
                            invoice.retail = 'R';
                        else
                            invoice.retail = 'N';

                        //add invoice to list
                        if((invoice.retail == 'R' && !invoice.notes.Contains("USE CARD ON FILE")) || location == "")
                            complete.Add(invoice);
                    }
                }

                //close connections
                creditReader.Close();
                pSqlConn.Close();
            }

            

            ProcessInvoicePanel.Hide();
            CompleteInvoicePanel.Hide();

            try
            {

                //fill respective tables with list data
                refreshTable(ProcessInvoiceList, incomplete);
                refreshTable(CompleteInvoiceList, complete);

            }
            finally
            {
                ProcessInvoicePanel.Show();
                CompleteInvoicePanel.Show();
            }

            idleTimer.Stop();
            refreshTimer.Start();
        }

        /// <summary>
        /// empties given table and refills with given invoice list
        /// </summary>
        /// <param name="table">
        /// table to be edited
        /// </param>
        /// <param name="invoices">
        /// invoices to poluate table
        /// </param>
        private void refreshTable(TableLayoutPanel table, List<Invoice> invoices)
        {

            //clear and hide table during refresh
            table.Controls.Clear();
            table.RowCount = 0;
            Color textColor;

            switch (table.Name)
            {
                case "ProcessInvoiceList":
                    //for each invoice in the list
                    for (int i = 0; i < invoices.Count; i++)
                    {
                        if (invoices[i].total <= 0)
                            textColor = Color.Red;
                        else
                            textColor = SystemColors.ControlText;

                        //add invoice to table
                        addToTable(table,
                            new List<string>()
                            {
                            invoices[i].invoiceNumber.ToString(),
                            invoices[i].account,
                            invoices[i].location,
                            invoices[i].salesPerson.ToString(),
                            invoices[i].retail.ToString(),
                            invoices[i].date
                            },
                            invoices[i],
                            textColor);
                    }
                    break;

                case "CompleteInvoiceList":
                    //for each invoice in the list
                    for (int i = 0; i < invoices.Count; i++)
                    {
                        if (invoices[i].total <= 0)
                            textColor = Color.Red;
                        else
                            textColor = SystemColors.ControlText;

                        //add invoice to table
                        addToTable(table,
                            new List<string>()
                            {
                            invoices[i].invoiceNumber.ToString(),
                            invoices[i].account,
                            invoices[i].salesPerson.ToString(),
                            invoices[i].retail.ToString(),
                            Convert.ToDateTime(invoices[i].chargeTime).ToString("MM/dd/yyyy HH:mm")
                            },
                            invoices[i],
                            textColor);
                    }
                    break;

                case "OtherInvoicesList":
                    //for each invoice in the list
                    for (int i = 0; i < invoices.Count; i++)
                    {

                        //add invoice to table
                        addToTable(table,
                            new List<string>()
                            {
                            invoices[i].invoiceNumber.ToString(),
                            invoices[i].location,
                            invoices[i].salesPerson.ToString(),
                            invoices[i].retail.ToString(),
                            "$" + invoices[i].total.ToString(),
                            invoices[i].date
                            },
                            invoices[i]);
                    }
                    break;
            }

        }


        /// <summary>
        /// creates new row
        /// populates new row with invoice data 
        /// establishes table relationships
        /// </summary>
        /// <param name="table">
        /// reference to table
        /// </param>
        /// <param name="data">
        /// list of strings to add 
        /// </param>
        /// <param name="invoice"></param>
        /// <param name="color"></param>
        private void addToTable(TableLayoutPanel table, List<string> data, Invoice invoice, Color? color = null)
        {
            //increment rowcount of given table and add new row
            table.RowCount++;
            table.RowStyles.Add(new RowStyle() { SizeType = SizeType.AutoSize});

            for (int i = 0; i < data.Count; i++)
            {
                Label tempLabel = new Label() { Text = data[i], TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table, ForeColor = color ?? SystemColors.ControlText, UseMnemonic = false };
                tempLabel.Click += new EventHandler(tempLabel_Click);
                table.Controls.Add(tempLabel, i, table.RowCount);
            }

            /*
            if(table.Name != "OtherInvoicesList")
            {
                //add account to row
                //add click event
                tempLabel = new Label() { Text = invoice.account, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
                tempLabel.Click += new EventHandler(tempLabel_Click);
                table.Controls.Add(tempLabel, column, table.RowCount);
                column++;
            }

            if(invoice.chargeTime == null)
            {
                //add location to row
                //add click event
                tempLabel = new Label() { Text = invoice.location, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
                tempLabel.Click += new EventHandler(tempLabel_Click);
                table.Controls.Add(tempLabel, column, table.RowCount);
                column++;
            }

            //add salesperson to row
            //add click event
            tempLabel = new Label() { Text = invoice.salesPerson.ToString(), TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
            tempLabel.Click += new EventHandler(tempLabel_Click);
            table.Controls.Add(tempLabel, column, table.RowCount);
            column++;


            //add retail character to row
            if (invoice.deliveryMethod == "DELIVERY" || invoice.deliveryMethod == "WILL CALL")
            {
                tempLabel = new Label() { Text = "R", TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
            }
            else
            {
                tempLabel = new Label() { Text = "N", TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
            }

            //add click event
            tempLabel.Click += new EventHandler(tempLabel_Click);
            table.Controls.Add(tempLabel, column, table.RowCount);
            column++;

            if(table.Name == "OtherInvoicesList")
            {
                //add total to row
                //add click event
                tempLabel = new Label() { Text = "$" + invoice.total.ToString(), TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
                tempLabel.Click += new EventHandler(tempLabel_Click);
                table.Controls.Add(tempLabel, column, table.RowCount);
                column++;
            }


            //if invoice has been processed
            if (invoice.chargeTime != null)
            {
                //show date processed
                tempLabel = new Label() { Text = Convert.ToDateTime( invoice.chargeTime).ToString("MM/dd/yyyy"), TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };

            }
            else
            {
                //show date order was placed
                tempLabel = new Label() { Text = invoice.date, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
            }

            //add click event
            tempLabel.Click += new EventHandler(tempLabel_Click);
            table.Controls.Add(tempLabel, column, table.RowCount);
            column++;*/
        }

        /// <summary>
        /// adds lines between table entries
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableLayoutPanel1_Paint(object sender, TableLayoutCellPaintEventArgs e)
        {
            //add line between table entries
            e.Graphics.DrawLine(Pens.Black, new Point(e.CellBounds.Left, e.CellBounds.Bottom), new Point(e.CellBounds.Right, e.CellBounds.Bottom));
        }

        /// <summary>
        /// refreshes the tables every thrity seconds if no invoice is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshTimer_Complete(object sender, EventArgs e)
        {
            refreshTimer.Stop();
            if(currentInvoice == null)
            {
                populateTables();
            }
            else
            {
                idleTimer.Start();
                refreshTimer.Start();
            }
        }

        /// <summary>
        /// shows selected invoice's data
        /// shades selected invoice in table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tempLabel_Click(object sender, EventArgs e)
        {
            idleTimer.Stop();
            RemoveButton.Enabled =
                ExitButton.Enabled =
                ProcessButton.Enabled =
                ReprocessButton.Enabled = false;

            //get invoice and display data
            Label clicked = (Label)sender;

            //initialize table references
            TableLayoutPanel table = null;
            TableLayoutPanel offTable = null;
            int row = 0;

            //if invoice is in process table
            if (ProcessInvoiceList == (sender as Label).Parent)
            {
                //flag table
                RemoveButton.Enabled =
                    ExitButton.Enabled =
                    ProcessButton.Enabled = true;

                row = ProcessInvoiceList.GetRow(clicked);
                table = ProcessInvoiceList;
                offTable = CompleteInvoiceList;

                //update control user in db
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    pSqlConn.Open();
                    string creditCommand = "UPDATE CRDTINV SET CRDT_INV_USER = '" + CurrentUser.user_code + "' " +
                        "WHERE CRDT_INV_NUM = " + ((Invoice)clicked.Tag).invoiceNumber + " " +
                        "AND (CRDT_INV_USER "+ held +"= 'null' OR CRDT_INV_USER = '" + CurrentUser.user_code + "')";
                    using (OdbcCommand cmd = new OdbcCommand(creditCommand, pSqlConn))
                    {
                        //if invoice is being processed by another user
                        if (cmd.ExecuteNonQuery() <= 0)
                        {
                            //alert user
                            MessageBox.Show("Invoice being processed by another user please select another invoice.", "Error");

                            //refresh tables and break from function
                            populateTables();
                            return;
                        }
                    }
                    pSqlConn.Close();
                }
            }
            //else if invoice is in complete table
            else if(CompleteInvoiceList == (sender as Label).Parent)
            {
                //ExitButton.Enabled = ProcessButton.Enabled = false;
                //flag table
                row = CompleteInvoiceList.GetRow(clicked);
                table = CompleteInvoiceList;
                offTable = ProcessInvoiceList;

                RemoveButton.Enabled = true;
                ReprocessButton.Enabled = true;
            }

            if(OtherInvoicesPanel.Visible)
            {
                OtherInvoicesPanel.Hide();
                BottomPanel.Show();
            }
            OtherOrdersButton.Enabled = true;

            //release previous invoice
            releaseInvoice();

            //set current invoice
            currentInvoice = clicked.Tag as Invoice;
            showData(currentInvoice);
            if(AccountInfoTable.Visible)
            {
                AccountInfoTable.Hide();
                DataTable.Show();
            }
            AccountDataButton.Enabled = true;

            if(table != null)
            {//for every row in flagged table
                for (int i = 1; i <= table.RowCount; i++)
                {
                    //if row matches invoice row
                    if (i == row)
                    {
                        //iterate through row
                        for (int j = 0; j < table.ColumnCount; j++)
                        {
                            //paint "selected" color
                            table.GetControlFromPosition(j, row).BackColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        //iterate through row
                        for (int j = 0; j < table.ColumnCount; j++)
                        {
                            //paint default color
                            table.GetControlFromPosition(j, i).BackColor = Color.FromArgb(255, 240, 240, 240);
                        }
                    }
                }

                //for all items in unflagged table
                for (int i = 1; i <= offTable.RowCount; i++)
                {
                    for (int j = 0; j < offTable.ColumnCount; j++)
                    {
                        //paint default color
                        offTable.GetControlFromPosition(j, i).BackColor = Color.FromArgb(255, 240, 240, 240);
                    }
                }
            }

            Clipboard.SetText(currentInvoice.invoiceNumber.ToString());
        }

        /// <summary>
        /// shows invoice information to user
        /// </summary>
        /// <param name="invoice"></param>
        private void showData(Invoice invoice)
        {
            //display given invoice data in data table
            InvoiceData.Text = invoice.invoiceNumber.ToString();
            AccountData.Text = invoice.account;
            DateData.Text = invoice.date;
            ChargedData.Text = invoice.chargeTime;
            ChargedByData.Text = invoice.user;
            TotalData.Text = "$" + String.Format("{0:0.00}", invoice.total);
            NotesText.Text = invoice.notes;
        }

        /// <summary>
        /// clears data fields
        /// </summary>
        private void clearData()
        {

            //clear data fields
            InvoiceData.Text = "";
            AccountData.Text = "";
            DateData.Text = "";
            ChargedData.Text = "";
            ChargedByData.Text = "";
            TotalData.Text = "";
            NotesText.Text = "";
        }

        /// <summary>
        /// releases control of current invoice in db
        /// sets current invoice to null
        /// </summary>
        private void releaseInvoice()
        {

            //if invoice is selected
            if (currentInvoice != null)
            {
                //update invoice control in db
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSQLConn = null;
                using (pSQLConn = new OdbcConnection(strConnection))
                {
                    string creditCommand = "UPDATE CRDTINV SET CRDT_INV_USER = 'null' WHERE CRDT_INV_NUM = " + currentInvoice.invoiceNumber + "AND CRDT_INV_USER != 'null' AND CRDT_INV_PROCESSED = 0";
                    OdbcCommand cmd = new OdbcCommand(creditCommand, pSQLConn);
                    pSQLConn.Open();
                    cmd.ExecuteNonQuery();
                    pSQLConn.Close();
                }

                //clear current invoice
                currentInvoice = null;
                AccountDataButton.Enabled = false;
            }
        }

        /// <summary>
        /// updates database to mark invoice as processed
        /// sets print flag to print
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessButton_Click(object sender, EventArgs e)
        {
            if(location == "")
                processInvoice(0);
            else if(MessageBox.Show("This will print the invoice and mark it as charged. Continue?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                processInvoice(0);
        }

        /// <summary>
        /// updates the CRDT table
        /// leaves print flag false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (currentInvoice != null)
            {
                if (location == "")
                {
                    if (currentInvoice.total > 0)
                    {
                        string email = null;
                        switch (currentInvoice.location)
                        {
                            case "R":
                                email = "shippingNV@ranshu.com";
                                break;

                            case "S1":
                                email = "shippingNV@ranshu.com";
                                break;

                            case "S2":
                                email = "shippingNV@ranshu.com";
                                break;

                            case "FW":
                                email = "shippingTX@ranshu.com";
                                break;

                            case "TXC":
                                email = "shippingTX@ranshu.com";
                                break;

                            case "FL":
                                email = "shippingFL@ranshu.com";
                                break;

                            default:
                                switch (currentInvoice.salesPerson)
                                {
                                    case 3:
                                        email = ("leeg@ranshu.com");
                                        break;

                                    case 5:
                                        email = ("scott@ranshu.com");
                                        break;

                                    case 9:
                                        email = ("rachael@ranshu.com");
                                        break;

                                    case 19:
                                        email = ("buddy@ranshu.com");
                                        break;

                                    case 35:
                                        email = ("stan@ranshu.com");
                                        break;

                                    case 36:
                                        email = ("cheryn@ranshu.com");
                                        break;

                                    case 38:
                                        email = ("aaron@ranshu.com");
                                        break;

                                    case 45:
                                        email = ("martin@ranshu.com");
                                        break;

                                    case 47:
                                        email = ("ramon@ranshu.com");
                                        break;

                                    case 55:
                                        email = ("ken@ranshu.com");
                                        break;

                                    case 60:
                                        email = ("wesley@ranshu.com");
                                        break;

                                    case 63:
                                        email = ("kyle@ranshu.com");
                                        break;

                                    case 64:
                                        email = ("louis@ranshu.com");
                                        break;

                                    case 66:
                                        email = ("elio@ranshu.com");
                                        break;

                                    case 67:
                                        email = ("jarrod@ranshu.com");
                                        break;

                                    case 71:
                                        email = ("bill@ranshu.com");
                                        break;

                                    case 72:
                                        email = ("edith@ranshu.com");
                                        break;

                                    case 73:
                                        email = ("tania@ranshu.com");
                                        break;

                                    case 77:
                                        email = ("alex@ranshu.com");
                                        break;
                                }
                                break;
                        }

                        ///check for email and that user is in accounting dashboard
                        if (email != null && location == "")
                        {
                            List<string> cc = new List<string>() { "AR@ranshu.com", "melanie@ranshu.com", "danielle@ranshu.com" };

                            string subject;
                            if (email.Contains("shipping"))
                            {
                                switch (currentInvoice.salesPerson)
                                {
                                    case 3:
                                        cc.Add("leeg@ranshu.com");
                                        break;

                                    case 5:
                                        cc.Add("scott@ranshu.com");
                                        break;

                                    case 9:
                                        cc.Add("rachael@ranshu.com");
                                        break;

                                    case 19:
                                        cc.Add("buddy@ranshu.com");
                                        break;

                                    case 35:
                                        cc.Add("stan@ranshu.com");
                                        break;

                                    case 36:
                                        cc.Add("cheryn@ranshu.com");
                                        break;

                                    case 38:
                                        cc.Add("aaron@ranshu.com");
                                        break;

                                    case 45:
                                        cc.Add("martin@ranshu.com");
                                        break;

                                    case 47:
                                        cc.Add("ramon@ranshu.com");
                                        break;

                                    case 55:
                                        cc.Add("ken@ranshu.com");
                                        break;

                                    case 60:
                                        cc.Add("wesley@ranshu.com");
                                        break;

                                    case 63:
                                        cc.Add("kyle@ranshu.com");
                                        break;

                                    case 64:
                                        cc.Add("louis@ranshu.com");
                                        break;

                                    case 66:
                                        cc.Add("elio@ranshu.com");
                                        break;

                                    case 67:
                                        cc.Add("jarrod@ranshu.com");
                                        break;

                                    case 71:
                                        cc.Add("bill@ranshu.com");
                                        break;

                                    case 72:
                                        cc.Add("edith@ranshu.com");
                                        break;

                                    case 73:
                                        cc.Add("tania@ranshu.com");
                                        break;

                                    case 77:
                                        email = ("alex@ranshu.com");
                                        break;
                                }


                                subject = currentInvoice.account + " + " + currentInvoice.invoiceNumber + " DO NOT SHIP";
                            }
                            else
                            {

                                subject = currentInvoice.account + " + " + currentInvoice.invoiceNumber + " - " + currentInvoice.vendLoc + " VENDOR PO ON HOLD, DO NOT LET SHIP!";
                            }

                            SendEmail(email, subject, currentInvoice.account + " " + currentInvoice.invoiceNumber + " DECLINED/HELD by " + CurrentUser.user_code + ". \n" + NotesText.Text, cc);
                        }
                    }

                    processInvoice(1);
                }
                else if (MessageBox.Show("This will send the invoice to accounting and then print a copy. Continue?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    printInvoice();
            }
            else
                MessageBox.Show("Please select an invoice before processing.");
        }

        private void printInvoice()
        {
                if (currentInvoice.retail == 'R' && currentInvoice.location == location)
                {
                    string conn = "DSN=Ranshu";
                    OdbcConnection sqlConn = null;
                    using (sqlConn = new OdbcConnection(conn))
                    {
                        //update invoice in database as processed, at current time, and by current user
                        OdbcCommand cmd = new OdbcCommand("{call printCredit(?)}", sqlConn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(":invNum", currentInvoice.invoiceNumber);
                        sqlConn.Open();
                        cmd.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                }
        }

        /// <summary>
        /// updates the CRDTINV and BKARHINV tables
        /// </summary>
        /// <param name="printFlag">
        /// 
        /// </param>
        private void processInvoice(int printFlag)
        {
            //if current invoice is valid
            if (currentInvoice != null)
            {
                if(currentInvoice.notes != NotesText.Text)
                {
                    DialogResult dialog = MessageBox.Show("Overwrite database notes with changes?", "Query", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if(dialog == DialogResult.Yes)
                    {
                        string conn = "DSN=Ranshu";
                        OdbcConnection sqlConn = null;
                        using (sqlConn = new OdbcConnection(conn))
                        {
                            //update invoice in database as processed, at current time, and by current user
                            OdbcCommand cmd = new OdbcCommand("{call editCRDT(?, ?)}", sqlConn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue(":invNum", currentInvoice.invoiceNumber);
                            cmd.Parameters.AddWithValue(":notes", NotesText.Text);
                            sqlConn.Open();
                            cmd.ExecuteNonQuery();
                            sqlConn.Close();
                        }
                    }
                    else if(dialog == DialogResult.No)
                    {
                        NotesText.Text = currentInvoice.notes;
                    }
                    else if(dialog == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                //establish database connection
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //update invoice in database as processed, at current time, and by current user
                    string sqlProcess = "UPDATE CRDTINV " +
                        "SET CRDT_INV_PROCESSED = 1, " +
                        "CRDT_INV_CHARGETIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "', " +
                        "CRDT_INV_USER = '" + CurrentUser.user_code + "', " +
                        "CRDT_INV_DECLINED = " + printFlag + " " +
                        "WHERE CRDT_INV_NUM = " + currentInvoice.invoiceNumber + "; " +
                        "UPDATE BKARHINV " +
                        "SET BKAR_INV_MAX = "+ printFlag +
                        "WHERE BKAR_INV_NUM = " + currentInvoice.invoiceNumber + ";";
                    OdbcCommand cmd = new OdbcCommand(sqlProcess, pSqlConn);
                    pSqlConn.Open();
                    cmd.ExecuteNonQuery();
                    pSqlConn.Close();
                }
            }

            //refresh tables
            populateTables();
        }

        /// <summary>
        /// fills account table with payment details
        /// </summary>
        /// <param name="payment">
        /// current selected payment
        /// </param>
        private void populatePaymentTable(Payment payment)
        {
            AccountNameText.Text = currentInvoice.account;
            CardNumText.Text = payment.cardNum;
            ExpText.Text = payment.expiration;
            PaymentNotesText.Text = payment.cardNotes;
        }

        /// <summary>
        /// toggles calendar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarButton_Click(object sender, EventArgs e)
        {
            //if calender active
            if(CalendarPanel.Visible)
            {
                //hide calendar
                CalendarPanel.Hide();

                CompleteInvoicePanel.VerticalScroll.Enabled = true;
            }
            else
            {
                //show calendar
                CalendarPanel.Show();

                CompleteInvoicePanel.VerticalScroll.Value = 0;
                CompleteInvoicePanel.VerticalScroll.Enabled = false;
            }
        }

        /// <summary>
        /// changes selected date
        /// repopulates data tables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            //refreshes data with new selected date
            selectedDate = Calendar.SelectionStart;
            populateTables();

            //hide calendar
            CalendarPanel.Hide();


            CompleteInvoicePanel.VerticalScroll.Enabled = true;
        }

        /// <summary>
        /// call just before form closure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //query user
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit the application?", "EXIT ProcessTool", MessageBoxButtons.YesNo);

            //if user selects "no", stop closing
            e.Cancel = (dialogResult == DialogResult.No);
            if (dialogResult == DialogResult.Yes && currentInvoice != null)
            {
                //clear selected invoice
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSQLConn = null;
                using(pSQLConn = new OdbcConnection(strConnection))
                {
                    string creditCommand = "UPDATE CRDTINV SET CRDT_INV_USER = 'null' WHERE CRDT_INV_NUM = " + currentInvoice.invoiceNumber + "AND CRDT_INV_USER != 'null' AND CRDT_INV_PROCESSED = 0";
                    OdbcCommand cmd = new OdbcCommand(creditCommand, pSQLConn);
                    pSQLConn.Open();
                    cmd.ExecuteNonQuery();
                    pSQLConn.Close();
                }
            }
        }

        /// <summary>
        /// close login form at main form closure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            login.Close();
        }

        /// <summary>
        /// depreciated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeliveryMethodBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            populateTables();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            populateTables();
        }

        private void DeliveryMethodLabel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// toggle viewing of held invoices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeldViewButton_Click(object sender, EventArgs e)
        {
            //toggles held invoices
            if(held == "")
            {
                held = "!";
                HeldViewButton.Text = "View\nOpen\nOrders";
            }
            else
            {
                held = "";
                HeldViewButton.Text = "View\nHeld\nOrders";
            }
            populateTables();
        }

        /// <summary>
        /// toggle other orders view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherOrdersButton_Click(object sender, EventArgs e)
        {
            if(currentInvoice != null)
            {
                BottomPanel.Hide();
                OtherOrdersTitle.Text = "ACCOUNT: " + currentInvoice.account;
                List<Invoice> incomplete = new List<Invoice>();

                //establish database connection
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //get unprocessed invoices from database
                    OdbcCommand cmd = new OdbcCommand("{call getOtherOrders (?, ?)}", pSqlConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(":account", currentInvoice.account);
                    cmd.Parameters.AddWithValue(":invNum", currentInvoice.invoiceNumber);
                    pSqlConn.Open();
                    OdbcDataReader creditReader = cmd.ExecuteReader();
                    if (creditReader.HasRows)
                    {
                        while (creditReader.Read())
                        {
                            //create and fill invoice
                            Invoice invoice = new Invoice();

                            invoice.invoiceNumber = Convert.ToInt32(creditReader["BKAR_INV_NUM"].ToString());
                            invoice.account = creditReader["BKAR_INV_CUSCOD"].ToString().TrimEnd();
                            invoice.date = creditReader["BKAR_INV_INVDTE"].ToString().TrimEnd();
                            invoice.total = Convert.ToDouble(creditReader["BKAR_INV_TOTAL"].ToString());
                            invoice.deliveryMethod = creditReader["BKAR_INV_SHPVIA"].ToString().TrimEnd();
                            invoice.salesPerson = Convert.ToInt32(creditReader["BKAR_INV_SLSP"].ToString());
                            invoice.setLocation(creditReader["BKAR_INV_LOC"].ToString());
                            invoice.notes = creditReader["notes"].ToString().Trim();

                            //add invoice to list
                            incomplete.Add(invoice);
                        }
                    }

                    creditReader.Close();
                    pSqlConn.Close();
                }

                //hide panel
                OtherInvoicesPanel.Hide();

                //refresh list
                try
                {
                    refreshTable(OtherInvoicesList, incomplete);
                }
                finally
                {
                    OtherInvoicesPanel.Show();
                }
            }
        }

        /// <summary>
        /// show account data for selected invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountDataButton_Click(object sender, EventArgs e)
        {
            //check for active invoice
            if(currentInvoice != null)
            {
                //establish database connection
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //get unprocessed invoices from database
                    OdbcCommand cmd = new OdbcCommand("{call getCCRD (?)}", pSqlConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(":account", currentInvoice.account);
                    pSqlConn.Open();

                    OdbcDataReader creditReader = cmd.ExecuteReader();
                    if (creditReader.HasRows)
                    {
                        List<Payment> payments = new List<Payment>();
                        while (creditReader.Read())
                        {
                            Payment payment = new Payment(creditReader["BKAR_CCRD_NUM"].ToString().TrimEnd(), creditReader["BKAR_CCRD_EXP"].ToString().TrimEnd(), creditReader["BKAR_CCRD_NAME"].ToString().TrimEnd());
                            payments.Add(payment);
                        }
                        account = new Account(payments);
                    }
                    else
                    {
                        MessageBox.Show("Account does not have any stored payment methods.", "ATTENTION");
                        return;
                    }
                    pSqlConn.Close();
                }

                populatePaymentTable(account.getPayment());
                PreviousPaymentButton.Enabled = false;

                if (account.count > 1)
                {
                    NextPaymentButton.Enabled = true;
                }
                else
                {
                    NextPaymentButton.Enabled = false;
                }

                DataTable.Hide();
                AccountInfoTable.Show();
            }
        }

        private void OtherInvoicesTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OtherOrdersCloseButton_Click(object sender, EventArgs e)
        {
            OtherInvoicesPanel.Hide();
            BottomPanel.Show();
        }

        private void ProcessOrderChanged(object sender, EventArgs e)
        {
            string order = (sender as Button).Tag.ToString();

            if (processOrderBy == order)
            {
                order += " DESC";
            }

            processOrderBy = order;
            populateTables();
        }

        private void CompleteOrderChanged(object sender, EventArgs e)
        {
            string order = (sender as Button).Tag.ToString();

            if (completeOrderBy == order)
            {
                order += " DESC";
            }

            completeOrderBy = order;
            populateTables();
        }

        private void ProcessRetailButton_Click(object sender, EventArgs e)
        {
            deliverySelection++;
            populateTables();
            switch(deliverySelection % 3)
            {
                case 0:
                    (sender as Button).BackColor = Color.FromArgb(255, 240, 240, 240);
                    break;

                case 1:
                    (sender as Button).BackColor = Color.FromArgb(255, 152, 251, 152);
                    break;

                case 2:
                    (sender as Button).BackColor = Color.FromArgb(255, 250, 128, 114);
                    break;
            }
        }

        private void AccountDataCloseButton_Click(object sender, EventArgs e)
        {
            AccountInfoTable.Hide();
            DataTable.Show();
        }

        private void PreviousPaymentButton_Click(object sender, EventArgs e)
        {
            populatePaymentTable(account.previousPayment());
            NextPaymentButton.Enabled = true;
            if (account.paymentIndex > 0)
                PreviousPaymentButton.Enabled = true;
            else
                PreviousPaymentButton.Enabled = false;
        }

        private void NextPaymentButton_Click(object sender, EventArgs e)
        {
            populatePaymentTable(account.nextPayment());
            PreviousPaymentButton.Enabled = true;
            if (account.paymentIndex + 1 < account.count)
                NextPaymentButton.Enabled = true;
            else
                NextPaymentButton.Enabled = false;
        }

        private void OtherInvoicesPanel_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void ReprocessButton_Click(object sender, EventArgs e)
        {
            if(currentInvoice != null)
            {
                if(currentInvoice.user != "" || currentInvoice.user != null)
                {
                    string strConnection = "DSN=Ranshu";
                    OdbcConnection pSQLConn = null;
                    using (pSQLConn = new OdbcConnection(strConnection))
                    {
                        string creditCommand = "UPDATE CRDTINV SET CRDT_INV_USER = 'null', CRDT_INV_PROCESSED = 0, CRDT_INV_CHARGETIME = null, CRDT_INV_DECLINED = 0 WHERE CRDT_INV_NUM = " + currentInvoice.invoiceNumber + " AND CRDT_INV_PROCESSED = 1";
                        OdbcCommand cmd = new OdbcCommand(creditCommand, pSQLConn);
                        pSQLConn.Open();
                        cmd.ExecuteNonQuery();
                        pSQLConn.Close();
                    }

                    populateTables();
                }
            }
        }

        private void AccountData_Click(object sender, EventArgs e)
        {
            Clipboard.SetText((sender as Label).Text);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            //if invoice is valid
            if (currentInvoice != null)
            {
                //warn user of removal
                DialogResult dialogResult = MessageBox.Show("WARNING: Removing invoice " + currentInvoice.invoiceNumber + " will delete it from the credit process app and will not print a pick ticket. " +
                    "Retrieval after this will require direct access to the database. " +
                    "Are you sure you wish to continue?", "Remove Invoice " + currentInvoice.invoiceNumber, MessageBoxButtons.YesNo);

                //if confirmed
                if (dialogResult == DialogResult.Yes && currentInvoice != null)
                {
                    //remove invoice from db
                    string strConnection = "DSN=Ranshu";
                    OdbcConnection pSQLConn = null;
                    using (pSQLConn = new OdbcConnection(strConnection))
                    {
                        string creditCommand = "DELETE FROM CRDTINV WHERE CRDT_INV_NUM = " + currentInvoice.invoiceNumber;
                        OdbcCommand cmd = new OdbcCommand(creditCommand, pSQLConn);
                        pSQLConn.Open();
                        cmd.ExecuteNonQuery();
                        pSQLConn.Close();
                    }

                    //repopulate
                    populateTables();
                }
            }
            else
            {
                //disable remove button
                RemoveButton.Enabled = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new SideWindow().Show();
        }

        private void MainWindow_Validated(object sender, EventArgs e)
        {
        }

        private void BottomButtonTable_Paint(object sender, PaintEventArgs e)
        {
        }

        private void SwitchCompletedButton_Click(object sender, EventArgs e)
        {
            if(declined == 1)
            {
                declined = 0;
                populateTables();
                SwitchCompletedButton.Text = "View Holds";
            }
            else
            {
                declined = 1;
                populateTables();
                SwitchCompletedButton.Text = "View Charged";
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
        static void SendEmail(string recipient, string subject, string msgText, List<string> cc = null)
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
            mailClient.Send(msgMail);

            ///garbage collect
            msgMail.Dispose();
        }
    }
}
