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
using Microsoft.VisualBasic;

namespace OrderValidation
{
    public partial class ValidationForm : Form
    {
        int selectedRow;
        Order currentOrder;
        Item currentItem;
        string invNot;
        string dateQualifier;

        public ValidationForm()
        {
            InitializeComponent();

            ItemsPanel.Hide();
        }

        private void populateInvoices()
        {
            List<Order> orders = null;
            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string cmdString = "SELECT BKAR_INV_NUM, printed " +
                                    "from wmsOrders w inner join BKARHINV b on w.invoice_num = b.BKAR_INV_NUM " +
                                    "where w.validated is "+ invNot +" null " +
                                    dateQualifier+
                                    "ORDER BY printed";

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                OdbcDataReader invoiceReader = cmd.ExecuteReader();
                if (invoiceReader.HasRows)
                {
                    orders = new List<Order>();
                    while (invoiceReader.Read())
                    {
                        Order order = new Order(Convert.ToInt32(invoiceReader["BKAR_INV_NUM"].ToString())); 
                        order.date = String.Format("{0:MM/dd/yyyy HH:mm}", invoiceReader["printed"]);

                        orders.Add(order);
                    }
                }

                InvoiceListPanel.Hide();
                refreshInvoices(orders);
                InvoiceListPanel.Show();

                invoiceReader.Close();
                pSqlConn.Close();
            }
        }

        private void refreshInvoices(List<Order> invoices)
        {
            EnterButton.Enabled = false;
            InvoiceList.Controls.Clear();
            InvoiceList.RowCount = 0;

            //for each invoice in the list
            if(invoices != null)
            {
                for (int i = 0; i < invoices.Count; i++)
                {
                    //add invoice to table
                    addToTable(InvoiceList, new List<string>() { invoices[i].invoiceNumber.ToString(), invoices[i].date }, invoices[i]);
                }
            }

            FinalizeButton.Enabled = false;
            ReprintButton.Enabled = false;
        }

        private void addToTable(TableLayoutPanel table, List<string> columns, Object tag, Color? color = null)
        {
            //increment rowcount of given table and add new row
            table.RowCount++;
            table.RowStyles.Add(new RowStyle() { SizeType = SizeType.AutoSize, Height = 30 });

            for(int i = 0; i < columns.Count; i++)
            {
                Label tempLabel = new Label() { Text = columns[i], TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 0, 0, 0), Tag = tag, Parent = table, BackColor = color ?? Color.FromArgb(255, 240, 240, 240) };
                tempLabel.Click += new EventHandler(tempLabel_Click);
                table.Controls.Add(tempLabel, i, table.RowCount);
            }
        }

        private List<Item> getItems(int invoiceNumber)
        {
            List<Item> items = null;

            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string cmdString = "SELECT BKAR_INVL_PCODE, BKAR_INVL_ITYPE, BKAR_INVL_PQTY, BKAR_INVL_PDESC, BKIC_VND_PART " +
                            "FROM BKARHINV INNER JOIN BKARHIVL ON BKAR_INV_NUM = BKAR_INVL_INVNM " +
                            "LEFT JOIN BKICCUST ON (BKAR_INVL_PCODE = BKIC_VND_PCODE AND BKAR_INV_CUSCOD = BKIC_VND_VENDOR) " +
                            "WHERE BKAR_INV_NUM = " + invoiceNumber + " AND  BKAR_INVL_ITYPE != 'X' AND  BKAR_INVL_ITYPE != 'N'";

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                OdbcDataReader invoiceReader = cmd.ExecuteReader();
                if (invoiceReader.HasRows)
                {
                    items = new List<Item>();
                    while (invoiceReader.Read())
                    {
                        Item item = new Item();
                        item.partCode = invoiceReader["BKAR_INVL_PCODE"].ToString().TrimEnd();
                        item.description = invoiceReader["BKAR_INVL_PDESC"].ToString().TrimEnd(); ;
                        item.vendorPart = invoiceReader["BKIC_VND_PART"].ToString().TrimEnd();
                        item.shipQuantity = Convert.ToInt32(invoiceReader["BKAR_INVL_PQTY"].ToString());
                        item.quantity = 0;

                        items.Add(item);
                    }
                }

                invoiceReader.Close();
                pSqlConn.Close();
            }

            return items;
        }

        private Order getOrderFromNum(int invoiceNum)
        {
            Order order = new Order(invoiceNum);

            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string cmdString = "SELECT validated " +
                                    "from wmsOrders w inner join BKARHINV b on w.invoice_num = b.BKAR_INV_NUM " +
                                    "where b.BKAR_INV_NUM = "+ invoiceNum + "AND w.validated is not null";

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                order.validated = String.Format("{0:MM/dd/yyyy HH:mm}", cmd.ExecuteScalar());
                pSqlConn.Close();
            }

            order.items = getItems(invoiceNum);

            return order;
        }

        private void populateOrder()
        {
            bool orderComplete = true;
            int total = 0;
            Color color;
            ItemList.Controls.Clear();
            ItemList.RowCount = 0;
            if (currentOrder.validated != "" && currentOrder.validated != null) 
                currentOrder.items.Select(x => { x.quantity = x.shipQuantity; return x; }).ToList();

            if(currentOrder.items != null)
            {
                for (int i = 0; i < currentOrder.items.Count; i++)
                {
                    if (currentOrder.items[i].quantity != currentOrder.items[i].shipQuantity)
                    {
                        color = Color.Yellow;
                        orderComplete = false;
                    }
                    else color = Color.FromArgb(255, 240, 240, 240);

                    addToTable(ItemList,
                        new List<string>()
                        {
                                currentOrder.items[i].partCode,
                                currentOrder.items[i].vendorPart,
                                currentOrder.items[i].description,
                                currentOrder.items[i].shipQuantity.ToString(),
                                currentOrder.items[i].quantity.ToString()
                        },
                        currentOrder.items[i], color);
                    total += currentOrder.items[i].quantity;
                }
            }

            FinalizeButton.Enabled = orderComplete;

            if (currentOrder.validated != "" && currentOrder.validated != null) 
                FinalizeButton.Enabled = false;

            //InvoicePanel.Hide();
            ItemsPanel.Show();
            EnterButton.Text = "Edit Quantity";
            EnterField.Text = "";
            EnterButton.Enabled = false;
            CancelButton.Enabled = true;
            currentItem = null;
            TotalText.Text = total.ToString();
            this.ActiveControl = EnterField;
        }

        private void tempLabel_Click(object sender, EventArgs e)
        {
            //get invoice and display data
            Label clicked = (Label)sender;

            //initialize table references
            TableLayoutPanel table = null;
            int row = 0;
            Color backColor;

            //if invoice is in process table
            if (InvoicePanel.Visible)
            {
                //flag table
                selectedRow = row = InvoiceList.GetRow(clicked);
                table = InvoiceList;


                //set current invoice
                currentOrder = clicked.Tag as Order;
                //ErrorText.Text = (DateTime.Now - Convert.ToDateTime(currentOrder.date)).ToString();
                ReprintButton.Enabled = true;
                currentItem = null;
            }
            //else if invoice is in complete table
            else if (ItemsPanel.Visible)
            {
                //flag table
                selectedRow = row = ItemList.GetRow(clicked);
                table = ItemList;

                //set current item
                currentItem = clicked.Tag as Item;
            }

            EnterButton.Enabled = true;
            //this.ActiveControl = EnterButton;
            EnterButton.Focus();


            //for every row in flagged table
            for (int i = 1; i <= table.RowCount; i++)
            {
                //if row matches invoice row
                if (i == row)
                {
                    //iterate through row
                    for (int j = 0; j < table.ColumnCount; j++)
                    {
                        //paint "selected" color
                        table.GetControlFromPosition(j, row).BackColor = Color.RoyalBlue;
                    }
                }
                else
                {
                    //iterate through row
                    for (int j = 0; j < table.ColumnCount; j++)
                    {
                        if(currentItem != null)
                        {
                            Item item = table.GetControlFromPosition(j, i).Tag as Item;
                            if (item.quantity != item.shipQuantity)
                            {
                                backColor = Color.Yellow;
                            }
                            else
                            {
                                backColor = Color.FromArgb(255, 240, 240, 240);
                            }
                        }
                        else backColor = Color.FromArgb(255, 240, 240, 240);

                        //paint default color
                        table.GetControlFromPosition(j, i).BackColor = backColor;
                    }
                }
            }
        }

        private void EnterField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                try 
                {
                    if (ItemsPanel.Visible)
                    {
                        if (EnterField.Text == "FINALIZE")
                        {
                            if (FinalizeButton.Enabled)
                            {
                                InvokeOnClick(FinalizeButton, new EventArgs());
                            }
                            else if (currentOrder.validated != "" && currentOrder.validated != null)
                            {
                                throw new Exception("Cannot validate already validated invoice.");
                            }
                            else
                            {
                                throw new Exception("Please enter all items before validating order.");
                            }
                        }
                        else
                        {
                            try
                            {
                                currentOrder.items.Find(x => x.partCode == EnterField.Text).quantity++;
                                populateOrder();
                            }
                            catch
                            {
                                throw new Exception("Part " + EnterField.Text + " not found.");
                            }
                        }
                    }
                    else
                    {
                        currentOrder = getOrderFromNum(Convert.ToInt32(EnterField.Text));
                        if (currentOrder.items == null)
                        {
                            throw new Exception("Invoice number " + EnterField.Text + " is invalid.");
                        }

                        populateOrder();
                        ReprintButton.Enabled = true;
                        InvoiceNumText.Text = currentOrder.invoiceNumber.ToString();
                    }
                }
                catch(Exception ex)
                {
                    new MessageForm(ex.Message, "Error").ShowDialog();
                }

                EnterField.Text = "";
            }
        }

        private void FinalizeButton_Click(object sender, EventArgs e)
        {
            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string cmdString = "UPDATE wmsOrders " +
                    "SET validated = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' " +
                    "WHERE invoice_num = " + currentOrder.invoiceNumber;

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                cmd.ExecuteNonQuery();
                pSqlConn.Close();
            }

            InvokeOnClick(CancelButton, new EventArgs());
            FinalizeButton.Enabled = false;
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if(currentOrder != null)
            {
                if (InvoicePanel.Visible)
                {
                    currentOrder.items = getItems(currentOrder.invoiceNumber);

                    populateOrder();
                    InvoiceNumText.Text = currentOrder.invoiceNumber.ToString();
                }
                else if (currentItem != null && ItemsPanel.Visible)
                {
                    try
                    {
                        currentItem.quantity = Convert.ToInt32(Interaction.InputBox("Quantity:", "Edit item quantity", currentItem.quantity.ToString()));
                        ItemsPanel.Hide();
                        populateOrder();
                    }
                    catch(Exception ex)
                    {
                        new MessageForm(ex.Message, "Error").ShowDialog();
                    }
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //populateInvoices();
            EnterButton.Enabled = false;
            ReprintButton.Enabled = false;
            currentItem = null;
            currentOrder = null;
            ItemsPanel.Hide();
            //InvoicePanel.Show();
            CancelButton.Enabled = false;
            EnterField.Text = "";
            //EnterButton.Text = "Open Invoice <Enter>";
            this.ActiveControl = EnterField;
        }

        private void ValidationForm_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void EnterField_Enter(object sender, EventArgs e)
        {
            if(EnterField.Text != "")
            {
                EnterField.SelectAll();
            }
        }

        private void ReprintButton_Click(object sender, EventArgs e)
        {
            if(currentOrder != null)
            {
                //establish database connection
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //get unprocessed invoices from database
                    string cmdString = "UPDATE BKARHINV " +
                        "SET BKAR_INV_MAX = 0 " +
                        "WHERE BKAR_INV_NUM = " + currentOrder.invoiceNumber;

                    OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                    pSqlConn.Open();
                    cmd.ExecuteNonQuery();
                    pSqlConn.Close();
                }
            }
        }

        private void InvoiceTypeButton_Click(object sender, EventArgs e)
        {
            if(invNot == "")
            {
                invNot = "not";
                InvoiceTypeButton.Text = "View Current Invoices"; 
                dateQualifier = "and (w.printed >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and w.printed < '" + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + "') ";
                CalendarButton.Show();
                populateInvoices();
            }
            else
            {
                invNot = "";
                InvoiceTypeButton.Text = "View Previous Invoices";
                dateQualifier = "";
                CalendarButton.Hide();
                populateInvoices();
            }
        }

        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            dateQualifier = "and (w.printed >= '" + Calendar.SelectionStart.ToString("yyyy-MM-dd") + "' and w.printed < '" + Calendar.SelectionEnd.AddDays(1).ToString("yyyy-MM-dd") + "') ";
            CalendarPanel.Hide();
            populateInvoices();
        }

        private void CalendarButton_Click(object sender, EventArgs e)
        {
            CalendarPanel.Visible = !CalendarPanel.Visible;
        }
    }
}
