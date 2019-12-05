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

namespace CreditProcessApp
{
    /* 
     * @CLASS:      public partial class MainWindow
     * @PURPOSE:    display credit invoice data for user to process charges
     * 
     * @PARAM:      none
     * 
     * @NOTES:      none
     */
    public partial class MainWindow : Form
    {
        Invoice currentInvoice;
        DateTime selectedDate;
        int deliverySelection;
        string[] selectedDelivery = new string[3] {"ALL", "(CRDT_INV_SHPVIA = 'DELIVERY' OR CRDT_INV_SHPVIA = 'WILL CALL')", "(CRDT_INV_SHPVIA != 'DELIVERY' AND CRDT_INV_SHPVIA != 'WILL CALL')" };
        string held;
        string processOrderBy;
        string completeOrderBy;
        Login login;
        Timer refreshTimer;
        

        /*
         * @FUNCTION:   public MainWindow()
         * @PURPOSE:    class constructor
         *              initializes variables and gets form into start position
         *              
         * @PARAM:      none
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        public MainWindow(Login in_login)
        {
            //create window
            InitializeComponent();

            login = in_login;
            completeOrderBy = processOrderBy = "CRDT_INV_NUM";

            //set start values
            selectedDate = DateTime.Now;

            //draw cell lines
            this.ProcessInvoiceList.CellPaint += tableLayoutPanel1_Paint;
            this.CompleteInvoiceList.CellPaint += tableLayoutPanel1_Paint;

            //set and start timer
            refreshTimer = new Timer();
            refreshTimer.Tick += new EventHandler(refreshTimer_Complete);
            refreshTimer.Interval = 30000;
            refreshTimer.Enabled = true;
            refreshTimer.Start();

            held = "";

            populateTables();

            if(CurrentUser.security_lvl <=1)
            {
                HeldViewButton.Show();
            }
            else
            {
                ProcessLabel.Margin = new Padding(6, 0, 0, 0);
            }
        }

        /*
         * @FUNCTION:   private void populateTables()
         * @PURPOSE:    queries database for unprocessed and processed invoices
         *              sends lists of invoices to be displayed
         *              
         * @PARAM:      none
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void populateTables()
        {
            ProcessButton.Enabled = false;
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
                    "FROM CRDTINV LEFT JOIN BKARHINV ON CRDT_INV_NUM = BKAR_INV_NUM " +
                    "WHERE CRDT_INV_PROCESSED = 0 and CRDT_INV_USER "+ held +"= 'null'";
                if(selectedDelivery[deliverySelection % 3] != "ALL")
                {
                    creditCommand += " AND " + selectedDelivery[deliverySelection % 3];
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

                        //add invoice to list
                        incomplete.Add(invoice);
                    }
                }

                creditReader.Close();

                //get processed invoices for selected date (default is currentdate)
                creditCommand = "SELECT CRDT_INV_NUM, CRDT_INV_CUSCOD, CRDT_INV_DATE, CRDT_INV_TOTAL, CRDT_INV_CHARGETIME, CRDT_INV_USER, CRDT_INV_NOTES, CRDT_INV_SHPVIA, CRDT_INV_SLSP " +
                    "FROM CRDTINV WHERE CRDT_INV_PROCESSED = 1 " +
                    "AND (CRDT_INV_CHARGETIME >= '" + selectedDate.ToString("yyyy-MM-dd") +"' " +
                    "AND CRDT_INV_CHARGETIME < '"+ selectedDate.AddDays(1).ToString("yyyy-MM-dd") +"') " +
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

                        //add invoice to list
                        complete.Add(invoice);
                    }
                }

                //close connections
                creditReader.Close();
                pSqlConn.Close();
            }

            

            ProcessInvoiceList.Hide();
            CompleteInvoiceList.Hide();

            try
            {

                //fill respective tables with list data
                refreshTable(ProcessInvoiceList, incomplete);
                refreshTable(CompleteInvoiceList, complete);

            }
            finally
            {
                ProcessInvoiceList.Show();
                CompleteInvoiceList.Show();
            }

            refreshTimer.Start();
        }

        /*
         * @FUNCTION:   private void refreshTable()
         * @PURPOSE:    emptiesgiven table and refills with given invoice list
         *              
         * @PARAM:      TableLayoutPanel table
         *              List<Invoice> invoices
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void refreshTable(TableLayoutPanel table, List<Invoice> invoices)
        {
            //clear and hide table during refresh
            //table.Hide();
            try
            {
                table.Controls.Clear();
                table.RowCount = 0;

                //for each invoice in the list
                for (int i = 0; i < invoices.Count; i++)
                {
                    //add invoice to table
                    addToTable(table, invoices[i]);
                }
            }
            finally
            {
                //display table
                //table.Show();
            }

        }


        /*
         * @FUNCTION:   private void addToTable()
         * @PURPOSE:    creates new row
         *              populates new row with invoice data 
         *              establishes table relationships
         *              
         * @PARAM:      TableLayoutPanel table
         *              Invoice invoice
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void addToTable(TableLayoutPanel table, Invoice invoice)
        {
            int column = 0;

            //increment rowcount of given table and add new row
            table.RowCount++;
            table.RowStyles.Add(new RowStyle() { SizeType = SizeType.Absolute, Height = 30 });

            //add invoice number to row
            //add click event
            Label tempLabel = new Label() { Text = invoice.invoiceNumber.ToString(), TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0,1,0,1), Tag = invoice, Parent = table };
            tempLabel.Click += new EventHandler(tempLabel_Click);
            table.Controls.Add(tempLabel, column, table.RowCount);
            column++;

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
                tempLabel = new Label() { Text = invoice.total.ToString(), TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
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
            column++;
        }

        /*
         * @FUNCTION:   private void tableLayoutPanel1_Paint()
         * @PURPOSE:    adds lines between table entries
         *              
         * @PARAM:      object sender
         *              TableLayoutCellPaintEventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void tableLayoutPanel1_Paint(object sender, TableLayoutCellPaintEventArgs e)
        {
            //add line between table entries
            e.Graphics.DrawLine(Pens.Black, new Point(e.CellBounds.Left, e.CellBounds.Bottom), new Point(e.CellBounds.Right, e.CellBounds.Bottom));
        }

        /*
         * @FUNCTION:   private void refreshTimer_Complete()
         * @PURPOSE:    refreshes the tables if user has been idle for 30 seconds
         *              
         * @PARAM:      object sender
         *              EventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void refreshTimer_Complete(object sender, EventArgs e)
        {
            refreshTimer.Stop();
            if(currentInvoice == null)
            {
                populateTables();
            }
            else
            {
                refreshTimer.Start();
            }
        }

        /*
         * @FUNCTION:   private void tempLabel_Click()
         * @PURPOSE:    shows selected invoice's data
         *              shades selected invoice in table
         *              
         * @PARAM:      object sender
         *              EventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void tempLabel_Click(object sender, EventArgs e)
        {
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
                        "AND CRDT_INV_USER = 'null'";
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
                //flag table
                ProcessButton.Enabled = false;
                row = CompleteInvoiceList.GetRow(clicked);
                table = CompleteInvoiceList;
                offTable = ProcessInvoiceList;
            }

            OtherOrdersButton.Enabled = true;

            //release previous invoice
            releaseInvoice();

            //set current invoice
            currentInvoice = clicked.Tag as Invoice;
            showData(currentInvoice);

            //for every row in flagged table
            for (int i = 1; i <= table.RowCount; i++)
            {
                //if row matches invoice row
                if(i == row)
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
            for(int i = 1; i <= offTable.RowCount; i++)
            {
                for(int j = 0; j < offTable.ColumnCount; j++)
                {
                    //paint default color
                    offTable.GetControlFromPosition(j, i).BackColor = Color.FromArgb(255, 240, 240, 240);
                }
            }

            Clipboard.SetText(currentInvoice.invoiceNumber.ToString());
        }

        /*
         * @FUNCTION:   private void showData()
         * @PURPOSE:    display given invoice data in data table
         *              
         * @PARAM:      Invoice invoice
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
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

        /*
         * @FUNCTION:   private void clearData()
         * @PURPOSE:    clears data fields
         *              
         * @PARAM:      none
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
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

        /*
         * @FUNCTION:   private void releaseInvoice()
         * @PURPOSE:    releases control of current invoice in db
         *              sets current invoice to null
         *              
         * @PARAM:      none
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
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
            }
        }

        /*
         * @FUNCTION:   private void ProcessButton_Click()
         * @PURPOSE:    updates database to mark invoice as processed
         *              sets print flag to print
         *              
         * @PARAM:      object sender
         *              EventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void ProcessButton_Click(object sender, EventArgs e)
        {
            processInvoice(0);
        }

        /*
         * @FUNCTION:   private void ExitButton_Click()
         * @PURPOSE:    updates the CRDT table
         *              leaves print flag false
         *              
         * @PARAM:      object sender
         *              EventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void ExitButton_Click(object sender, EventArgs e)
        {
            //begin form close
            processInvoice(1);
        }

        /*
         * @FUNCTION:   private void processInvoice()
         * @PURPOSE:    updates the CRDTINV and BKARHINV tables
         *              
         * @PARAM:      int printFlag
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void processInvoice(int printFlag)
        {

            //if current invoice is valid
            if (currentInvoice != null)
            {
                //establish database connection
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //update invoice in database as processed, at current time, and by current user
                    string sqlProcess = "UPDATE CRDTINV " +
                        "SET CRDT_INV_PROCESSED = 1, " +
                        "CRDT_INV_CHARGETIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "', " +
                        "CRDT_INV_USER = '" + CurrentUser.user_code + "' " +
                        "WHERE CRDT_INV_NUM = " + currentInvoice.invoiceNumber + "; " +
                        "UPDATE BKARHINV " +
                        "SET BKAR_INV_MAX = "+ printFlag +
                        "WHERE BKAR_INV_NUM = " + currentInvoice.invoiceNumber + ";";
                    OdbcCommand cmd = new OdbcCommand(sqlProcess, pSqlConn);
                    pSqlConn.Open();
                    cmd.ExecuteNonQuery();
                    pSqlConn.Close();
                }

                //refresh tables
                populateTables();
            }

            //reset UI
            currentInvoice = null;
            ProcessButton.Enabled = false;
        }

        /*
         * @FUNCTION:   private void CalendarButton_Click()
         * @PURPOSE:    toggles calendar
         *              
         * @PARAM:      object sender
         *              EventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
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

        /*
         * @FUNCTION:   private void Calendar_DateSelected()
         * @PURPOSE:    changes selected date
         *              repopulates data tables
         *              
         * @PARAM:      object sender
         *              DateRangeEventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            //refreshes data with new selected date
            selectedDate = Calendar.SelectionStart;
            populateTables();

            //hide calendar
            CalendarPanel.Hide();


            CompleteInvoicePanel.VerticalScroll.Enabled = true;
        }

        /*
         * @FUNCTION:   private void MainWindow_FormClosing()
         * @PURPOSE:    give user a chance to opt out of closing program
         *              
         * @PARAM:      object sender
         *              DateRangeEventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //query user
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit the application?", "EXIT ProcessTool", MessageBoxButtons.YesNo);

            //if user selects "no", stop closing
            e.Cancel = (dialogResult == DialogResult.No);
            if (dialogResult == DialogResult.Yes && currentInvoice != null)
            {
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


        /*
         * @FUNCTION:   private void MainWindow_FormClosed()
         * @PURPOSE:    as the main window closes it closes the login page as well
         *              
         * @PARAM:      object sender
         *              EventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            login.Close();
        }


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

        private void HeldViewButton_Click(object sender, EventArgs e)
        {
            if(held == "")
            {
                held = "!";
            }
            else
            {
                held = "";
            }
            populateTables();
        }

        private void OtherOrdersButton_Click(object sender, EventArgs e)
        {
            BottomPanel.Hide();
            OtherInvoicesPanel.Show();
            List<Invoice> incomplete = new List<Invoice>();

            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string creditCommand = "SELECT BKAR_INV_NUM, BKAR_INV_CUSCOD, BKAR_INV_INVDTE, BKAR_INV_TOTAL, BKAR_INV_SHPVIA, BKAR_INV_SLSP, BKAR_INV_LOC, INVOICE_NUM, TRACKING_NUM " +
                    "FROM BKARHINV INNER JOIN TRACKING ON BKAR_INV_NUM = INVOICE_NUM " +
                    "WHERE TRACKING_NUM <= '' and BKAR_INV_CUSCOD = '"+ currentInvoice.account +"' " +
                    "ORDER BY " + (sender as Button).Tag.ToString();

                OdbcCommand cmd = new OdbcCommand(creditCommand, pSqlConn);
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
                        invoice.date = creditReader["BKAR_INV_DATE"].ToString().TrimEnd();
                        invoice.total = Convert.ToDouble(creditReader["BKAR_INV_TOTAL"].ToString());
                        invoice.deliveryMethod = creditReader["BKAR_INV_SHPVIA"].ToString().TrimEnd();
                        invoice.salesPerson = Convert.ToInt32(creditReader["BKAR_INV_SLSP"].ToString());
                        invoice.setLocation(creditReader["BKAR_INV_LOC"].ToString());

                        //add invoice to list
                        incomplete.Add(invoice);
                    }
                }

                creditReader.Close();
                pSqlConn.Close();
            }

            OtherInvoicesList.Hide();

            try
            {
                refreshTable(OtherInvoicesList, incomplete);
            }
            finally
            {
                OtherInvoicesList.Show();
            }
        }

        private void AccountDataButton_Click(object sender, EventArgs e)
        {

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
    }
}
