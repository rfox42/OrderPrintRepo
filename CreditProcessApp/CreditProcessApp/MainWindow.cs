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
     * @CLASS: public partial class MainWindow
     * @PURPOSE: display credit invoice data for user to process charges
     * 
     * @PARAM: none
     * 
     * @NOTES: none
     */
    public partial class MainWindow : Form
    {
        Invoice currentInvoice;
        DateTime selectedDate;
        Login login;

        /*
         * @FUNCTION:   public MainWindow()
         * @PURPOSE:    class constructor
         *              initializes variables and gets form into start position
         *              
         * @PARAM:      none
         * 
         * @RETURNS:    none
         * @NOTES:      will likely add login support in holding form
         */
        public MainWindow(Login in_login)
        {
            //create window
            InitializeComponent();

            login = in_login;

            //set start values
            selectedDate = DateTime.Now;

            //draw cell lines
            this.ProcessInvoiceList.CellPaint += tableLayoutPanel1_Paint;
            this.CompleteInvoiceList.CellPaint += tableLayoutPanel1_Paint;

            //fill tables with data
            populateTables();
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
            //create table lists
            List<Invoice> incomplete = new List<Invoice>();
            List<Invoice> complete = new List<Invoice>();

            //establish database connection
            string strConnection = "DSN=Ranshu20190831";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string creditCommand = "SELECT CRDT_INV_NUM, CRDT_INV_CUSCOD, CRDT_INV_DATE, CRDT_INV_TOTAL, CRDT_INV_NOTES FROM CRDTINV WHERE CRDT_INV_PROCESSED = 0 ORDER BY CRDT_INV_NUM";
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

                        //add invoice to list
                        incomplete.Add(invoice);
                    }
                }

                creditReader.Close();

                //get processed invoices for selected date (default is currentdate)
                creditCommand = "SELECT CRDT_INV_NUM, CRDT_INV_CUSCOD, CRDT_INV_DATE, CRDT_INV_TOTAL, CRDT_INV_CHARGETIME, CRDT_INV_USER, CRDT_INV_NOTES FROM CRDTINV WHERE CRDT_INV_PROCESSED = 1 AND (CRDT_INV_CHARGETIME >= '"+ selectedDate.ToString("yyyy-MM-dd") +"' AND CRDT_INV_CHARGETIME < '"+ selectedDate.AddDays(1).ToString("yyyy-MM-dd") +"') ORDER BY CRDT_INV_NUM";
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

                        //add invoice to list
                        complete.Add(invoice);
                    }
                }

                //close connections
                creditReader.Close();
                pSqlConn.Close();
            }

            //fill respective tables with list data
            refreshTable(ProcessInvoiceList, incomplete);
            refreshTable(CompleteInvoiceList, complete);
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
            table.Visible = false;
            table.Controls.Clear();
            table.RowCount = 0;

            //for each invoice in the list
            for (int i = 0; i < invoices.Count; i++)
            {
                //add invoice to table
                addToTable(table, invoices[i]);
            }

            //display table
            table.Visible = true;
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
            //increment rowcount of given table and add new row
            table.RowCount++;
            table.RowStyles.Add(new RowStyle() { SizeType = SizeType.Absolute, Height = 30 });

            //add invoice number to row
            //add click event
            Label tempLabel = new Label() { Text = invoice.invoiceNumber.ToString(), TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0,1,0,1), Tag = invoice, Parent = table };
            tempLabel.Click += new EventHandler(tempLabel_Click);
            table.Controls.Add(tempLabel, 0, table.RowCount);

            //add customer account to row
            //add click event
            tempLabel = new Label() { Text = invoice.account, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
            tempLabel.Click += new EventHandler(tempLabel_Click);
            table.Controls.Add(tempLabel, 1, table.RowCount);

            //if invoice has been processed
            if(invoice.chargeTime != null)
            {
                //show date processed
                //add click event
                tempLabel = new Label() { Text = Convert.ToDateTime( invoice.chargeTime).ToString("MM/dd/yyyy"), TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
                tempLabel.Click += new EventHandler(tempLabel_Click);
                table.Controls.Add(tempLabel, 2, table.RowCount);
            }
            else
            {
                //show date order was placed
                //add click event
                tempLabel = new Label() { Text = invoice.date, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table };
                tempLabel.Click += new EventHandler(tempLabel_Click);
                table.Controls.Add(tempLabel, 2, table.RowCount);
            }
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
            currentInvoice = clicked.Tag as Invoice;
            showData(currentInvoice);

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

            //for every row in flagged table
            for(int i = 1; i <= table.RowCount; i++)
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
         * @FUNCTION:   private void ProcessButton_Click()
         * @PURPOSE:    updates database to mark invoice as processed
         *              
         * @PARAM:      object sender
         *              EventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void ProcessButton_Click(object sender, EventArgs e)
        {
            //if current invoice is valid
            if (currentInvoice != null)
            {
                //establish database connection
                string strConnection = "DSN=Ranshu20190831";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //update invoice in database as processed, at current time, and by current user
                    string sqlProcess = "UPDATE CRDTINV " +
                        "SET CRDT_INV_PROCESSED = 1, " +
                        "CRDT_INV_CHARGETIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "', " +
                        "CRDT_INV_USER = '" + CurrentUser.user_code + "' " +
                        "WHERE CRDT_INV_NUM = " + currentInvoice.invoiceNumber;
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
         * @FUNCTION:   private void ExitButton_Click()
         * @PURPOSE:    closes the form
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
            Close();
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
            if(Calendar.Visible)
            {
                //hide calendar
                Calendar.Hide();

                CompleteInvoicePanel.VerticalScroll.Enabled = true;
            }
            else
            {
                //show calendar
                Calendar.Show();

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
            Calendar.Hide();


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

            login.Show();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            login.Close();
        }
    }
}
