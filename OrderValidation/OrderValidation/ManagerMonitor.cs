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

namespace OrderValidation
{
    public partial class ManagerMonitor : Form
    {
        Timer timer;
        ValidationForm vForm;
        List<Order> lastOrders = new List<Order>();
        string printer;
        string location;
        string orderRule;
        public bool hold = false;

        public ManagerMonitor(ValidationForm vFormIn, string inlocation)
        {
            InitializeComponent();

            location = inlocation;
            orderRule = "bin_date desc";
            printer = getPrinter(); ;
            vForm = vFormIn;
            ShippingTab.Tag = InvoiceList;
            Receiving.Tag = UserList;
            refreshInvoiceList(InvoiceList);
            refreshUserList(UserList);
            TransactionList.Controls.Clear();
            TransactionList.RowCount = 0;


            timer = new Timer();
            timer.Interval = 90000;
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }

        string getPrinter()
        {
            string prntr;

            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string cmdString = "select printer_name from wmsPrinters where printer_location = '"+location+ "' and retail = 0";

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                prntr = cmd.ExecuteScalar().ToString().TrimEnd();
                pSqlConn.Close();
            }

            return prntr;
        }

        private void timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (!hold)
            {
                if (InvoiceList.Visible)
                    refreshInvoiceList(InvoiceList);
                else
                    refreshUserList(UserList);
            }
            timer.Start();
        }

        void refreshUserList(TableLayoutPanel table)
        {
            List<User> users = null;
            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string cmdString = "select USER_ID, USER_NAME, USER_LAST_DEVICE, USER_LAST_ACTIVITY, USER_ACTIVITY_NOTES from wmsUsers where USER_LOC = '"+location+"' and USER_ACTIVE = 1";

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                OdbcDataReader invoiceReader = cmd.ExecuteReader();
                if (invoiceReader.HasRows)
                {
                    users = new List<User>();
                    while (invoiceReader.Read())
                    {
                        User user = new User();
                        user.id = invoiceReader["USER_ID"].ToString().TrimEnd();
                        user.name = invoiceReader["USER_NAME"].ToString().TrimEnd();
                        user.device = invoiceReader["USER_LAST_DEVICE"].ToString().TrimEnd();
                        user.notes = invoiceReader["USER_ACTIVITY_NOTES"].ToString().Replace(" ", "");
                        user.activity = invoiceReader["USER_LAST_ACTIVITY"].ToString().TrimEnd();

                        users.Add(user);
                    }
                }

                table.Hide();
                refreshUsers(table, users);
                table.Show();

                invoiceReader.Close();
                pSqlConn.Close();
            }
        }

        void refreshUsers(TableLayoutPanel userTable, List<User> users)
        {
            userTable.Controls.Clear();
            userTable.RowCount = 0;

            if (users != null)
            {
                foreach (User user in users)
                {
                    addToTable(userTable, new List<string>() { user.name, user.activity, user.device, user.notes }, user);
                }
            }
        }

        void refreshInvoiceList(TableLayoutPanel table)
        {

            List<Order> orders = null;
            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");

                if (printer.Contains("LTL"))
                    date = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                //get unprocessed invoices from database
                string cmdString = "select invoice_num, printed, notes from wmsOrders where validated is null and printer = '"+printer+"' and (printed >= '" + date + "' or exists (select * from wmsUsers where LOCATE(invoice_num,USER_ACTIVITY_NOTES) > 0 and USER_ACTIVE = 1 and USER_LOC = '"+location+"'))";

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                OdbcDataReader invoiceReader = cmd.ExecuteReader();
                if (invoiceReader.HasRows)
                {
                    orders = new List<Order>();
                    while (invoiceReader.Read())
                    {
                        Order order = new Order(Convert.ToInt32(invoiceReader["invoice_num"].ToString()));

                        order.notes = invoiceReader["notes"].ToString().TrimEnd();

                        cmd = new OdbcCommand("select USER_NAME, USER_LAST_ACTIVITY from wmsUsers where USER_ACTIVITY_NOTES LIKE '%" + order.invoiceNumber + "%' and USER_ACTIVE = 1 and USER_LOC = '" + location + "'", pSqlConn);
                        OdbcDataReader activityReader = cmd.ExecuteReader();
                        if (activityReader.HasRows)
                        {
                            activityReader.Read();
                            order.date = activityReader["USER_LAST_ACTIVITY"].ToString().TrimEnd();
                            order.deliveryMethod = order.date.Split(new char[] { ' ' })[0];
                            order.date = order.date.Split(new char[] { ' ' })[1] + " " + order.date.Split(new char[] { ' ' })[2] + " " + order.date.Split(new char[] { ' ' })[3];
                            order.customerCode = activityReader["USER_NAME"].ToString().TrimEnd();
                        }
                        else
                        {
                            if (invoiceReader["notes"].ToString().Contains("PULLED"))
                            {
                                order.deliveryMethod = "PULLED";
                            }
                            else
                            {
                                order.deliveryMethod = "PRINTED";
                            }
                            order.date = invoiceReader["printed"].ToString();
                        }

                        orders.Add(order);
                    }
                }

                lastOrders = orders;

                table.Hide();
                refreshInvoices(table, orders);
                table.Show();

                invoiceReader.Close();
                pSqlConn.Close();
            }
        }

        void refreshInvoices(TableLayoutPanel invoiceList, List<Order> invoices)
        {
            invoiceList.Controls.Clear();
            invoiceList.RowCount = 0;

            //for each invoice in the list
            if (invoices != null)
            {
                for (int i = 0; i < invoices.Count; i++)
                {
                    Color color = Color.Transparent;

                    if (invoices[i].notes.Contains("AMAZON"))
                        color = Color.SkyBlue;

                    //add invoice to table
                    addToTable(invoiceList, new List<string>() { invoices[i].invoiceNumber.ToString(), invoices[i].deliveryMethod, invoices[i].date, invoices[i].customerCode }, invoices[i], new EventHandler(tempLabel_Click), color);
                }
            }
        }

        void addToTable(TableLayoutPanel table, List<string> columns, Object tag, EventHandler clickEvent = null, Color? color = null)
        {
            //increment rowcount of given table and add new row
            table.RowCount++;
            table.RowStyles.Add(new RowStyle() { SizeType = SizeType.AutoSize, Height = 30 });

            for (int i = 0; i < columns.Count; i++)
            {
                Label tempLabel = new Label() { Text = columns[i], TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 0), Tag = tag, Parent = table, BackColor = color ?? Color.Transparent/*.FromArgb(255, 240, 240, 240)*/ };
                tempLabel.Click += clickEvent;
                table.Controls.Add(tempLabel, i, table.RowCount);
            }
        }

        private void tempLabel_Click(object sender, EventArgs e)
        {
            Order order = (sender as Label).Tag as Order;

            if(order != null)
            {

                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //get unprocessed invoices from database
                    string cmdString = "select printed, printer, notes from wmsOrders where invoice_num = " + order.invoiceNumber;

                    OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                    pSqlConn.Open();
                    OdbcDataReader invoiceReader = cmd.ExecuteReader();
                    if (invoiceReader.HasRows)
                    {
                        while (invoiceReader.Read())
                        {
                            order.printer = invoiceReader["printer"].ToString().TrimEnd();
                            order.notes = invoiceReader["notes"].ToString().TrimEnd();
                        }
                    }

                    invoiceReader.Close();
                    pSqlConn.Close();
                }

                DialogResult dialog = MessageBox.Show("FORCE VALIDATE?\nInvoice: "+order.invoiceNumber+"\nPrinted: " + order.date + "\nPrinter: " + order.printer + "\nValidated: " + order.validated + "\nNotes: \n " + order.notes, "FORCE VALIDATE?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if(dialog == DialogResult.Yes)
                {

                    using (pSqlConn = new OdbcConnection(strConnection))
                    {
                        //get unprocessed invoices from database
                        string cmdString = "update wmsOrders set validated = '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm")+"', notes = ('MANAGER VALIDATED %' + notes) where invoice_num = "+order.invoiceNumber+"";

                        OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                        pSqlConn.Open();
                        cmd.ExecuteNonQuery();
                        pSqlConn.Close();
                    }
                }
            }
        }

        private void ManagerMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            vForm.Show();
        }

        private void ManagerMonitor_ResizeBegin(object sender, EventArgs e)
        {
            TabControl.Hide();
            timer.Stop();
        }

        private void ManagerMonitor_ResizeEnd(object sender, EventArgs e)
        {
            try
            {
                refreshInvoiceList(InvoiceList);
            }
            finally
            {
                TabControl.Show();
            }
            timer.Start();
        }

        private void ManagerMonitor_Load(object sender, EventArgs e)
        {

        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            //(e.TabPage.Tag as TableLayoutPanel).Visible = true;
        }

        private void UserList_VisibleChanged(object sender, EventArgs e)
        {
            /*
            TableLayoutPanel table = (sender as TableLayoutPanel);
            if (!table.Visible)
            {
                TabControl.SelectedTab.Tag = table;
                refreshUserList(table);
            }
            else
            {
                table.Controls.Clear();
                table.RowCount = 0;
            }*/
        }

        private void InvoiceList_VisibleChanged(object sender, EventArgs e)
        {
            /*
            TableLayoutPanel table = (sender as TableLayoutPanel);
            if(!table.Visible)
            {
                TabControl.SelectedTab.Tag = table;
                refreshInvoiceList(table);
            }
            else
            {
                table.Controls.Clear();
                table.RowCount = 0;
            }*/
        }

        private void TabControl_Deselected(object sender, TabControlEventArgs e)
        {
            //(e.TabPage.Tag as TableLayoutPanel).Visible = false;
        }

        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if(printer.Contains("LTL"))
            {
                printer = getPrinter();
                EnterButton.Text = "Truck Orders";
            }
            else
            {
                printer = "LTL " + location;
                EnterButton.Text = "Hide Truck";
            }

            refreshInvoiceList(InvoiceList);
        }

        private void NewUserButton_Click(object sender, EventArgs e)
        {
            new AddUserForm(this, location).Show();
            this.Enabled = false;
            hold = true;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Transaction> transactions = new List<Transaction>();

                //establish database connection
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    string cmdString = "select bin_name, bin_part, bin_trans, bin_user, bin_date, user_name from wmsTrans left join wmsUsers on user_id = bin_user and user_loc = bin_loc where bin_loc = '"+location+"' ";

                    if(PartField.Text != "")
                    {
                        cmdString += "and bin_part = '"+PartField.Text.ToUpper()+"' ";
                    }

                    if(LocationField.Text != "")
                    {
                        cmdString += "and bin_name = '" + LocationField.Text.ToUpper() + "' ";
                    }

                    if(LocationField.Text == "" && PartField.Text == "")
                    {
                        throw new Exception("Please specify part, location, or both");
                    }

                    cmdString += "order by " + orderRule;

                    OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                    pSqlConn.Open();
                    OdbcDataReader invoiceReader = cmd.ExecuteReader();
                    if (invoiceReader.HasRows)
                    {
                        while (invoiceReader.Read())
                        {
                            Transaction trans = new Transaction();
                            trans.bin= invoiceReader["bin_name"].ToString().TrimEnd();
                            trans.part = invoiceReader["bin_part"].ToString().TrimEnd();
                            trans.qty = Convert.ToInt32(invoiceReader["bin_trans"].ToString());
                            
                            
                            if((trans.user = invoiceReader["user_name"].ToString().TrimEnd()) == "")
                            {
                                trans.user = invoiceReader["bin_user"].ToString().TrimEnd();
                            }

                            trans.date = invoiceReader["bin_date"].ToString();

                            transactions.Add(trans);
                        }
                    }

                    TransactionListPanel.Hide();
                    addTransactions(transactions);
                    TransactionListPanel.Show();

                    invoiceReader.Close();
                    pSqlConn.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addTransactions(List<Transaction> transactions)
        {
            TransactionList.Controls.Clear();
            TransactionList.RowCount = 0;

            foreach(Transaction transaction in transactions)
            {
                addToTable(
                    TransactionList,
                    new List<string>()
                    {
                        transaction.bin,
                        transaction.part,
                        transaction.qty.ToString(),
                        transaction.user,
                        transaction.date
                    },
                    transaction);
            }
        }

        private void LocationHeader_Click(object sender, EventArgs e)
        {
            string column = (sender as Button).Tag.ToString();

            if(orderRule.Contains(column) && orderRule.Contains("desc"))
            {
                orderRule = column;
            }
            else if(orderRule.Contains(column))
            {
                orderRule = column + " desc";
            }
            else
            {
                orderRule = column;
            }

            this.SearchButton_Click(this, new EventArgs());
        }

        private void LocationField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
            {
                this.SearchButton_Click(this, new EventArgs());
            }
        }
    }
}
