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

namespace OrderValidation
{
    public partial class ValidationForm : Form
    {
        Order currentOrder;
        Item currentItem;

        public ValidationForm()
        {
            InitializeComponent();

            ItemsPanel.Hide();
            InvoicePanel.Show();

            populateInvoices();
        }

        private void populateInvoices()
        {
            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string cmdString = "SELECT BKAR_INV_NUM, BKAR_INV_INVDTE " +
                                    "from wmsOrders w inner join BKARHINV b on w.invoice_num = b.BKAR_INV_NUM " +
                                    "where w.validated is null " +
                                    "ORDER BY BKAR_INV_INVDTE";

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                OdbcDataReader invoiceReader = cmd.ExecuteReader();
                if (invoiceReader.HasRows)
                {
                    List<Order> orders = new List<Order>();
                    while (invoiceReader.Read())
                    {
                        Order order = new Order(Convert.ToInt32(invoiceReader["BKAR_INV_NUM"].ToString())); 
                        order.date = String.Format("{0:MM/dd/yyyy}", invoiceReader["BKAR_INV_INVDTE"]);

                        orders.Add(order);
                    }

                    refreshInvoices(orders);
                }

                invoiceReader.Close();
                pSqlConn.Close();
            }
        }

        private void refreshInvoices(List<Order> invoices)
        {
            InvoiceList.Controls.Clear();
            InvoiceList.RowCount = 0;

            //for each invoice in the list
            for (int i = 0; i < invoices.Count; i++)
            {
                //add invoice to table
                addToTable(InvoiceList, new List<string>() { invoices[i].invoiceNumber.ToString(), invoices[i].date }, invoices[i]);
            }
        }

        private void addToTable(TableLayoutPanel table, List<string> columns, Object tag)
        {
            //increment rowcount of given table and add new row
            table.RowCount++;
            table.RowStyles.Add(new RowStyle() { SizeType = SizeType.AutoSize, Height = 30 });

            for(int i = 0; i < columns.Count; i++)
            {
                Label tempLabel = new Label() { Text = columns[i], TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = tag, Parent = table };
                tempLabel.Click += new EventHandler(tempLabel_Click);
                table.Controls.Add(tempLabel, i, table.RowCount);
            }
        }

        private void openOrder(Order order)
        {
            //establish database connection
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                //get unprocessed invoices from database
                string cmdString = "SELECT BKAR_INVL_PCODE, BKAR_INVL_ITYPE, BKAR_INVL_PQTY, BKAR_INVL_PDESC, BKIC_VND_PART " +
                            "FROM BKARHINV INNER JOIN BKARHIVL ON BKAR_INV_NUM = BKAR_INVL_INVNM " +
                            "LEFT JOIN BKICCUST ON (BKAR_INVL_PCODE = BKIC_VND_PCODE AND BKAR_INV_CUSCOD = BKIC_VND_VENDOR)" +
                            "WHERE BKAR_INV_NUM = " + order.invoiceNumber + " AND  BKAR_INVL_ITYPE != 'X' AND  BKAR_INVL_ITYPE != 'N'";

                OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                pSqlConn.Open();
                OdbcDataReader invoiceReader = cmd.ExecuteReader();
                if (invoiceReader.HasRows)
                {
                    List<Item> items = new List<Item>();
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

                    order.items = items;
                }

                invoiceReader.Close();
                pSqlConn.Close();
            }
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
            if (InvoiceList.Visible)
            {
                //flag table
                row = InvoiceList.GetRow(clicked);
                table = InvoiceList;

                //set current invoice
                currentOrder = clicked.Tag as Order;
            }
            //else if invoice is in complete table
            else if (ItemList.Visible)
            {
                //flag table
                row = ItemList.GetRow(clicked);
                table = ItemList;

                //set current item
                currentItem = clicked.Tag as Item;
            }


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
                        table.GetControlFromPosition(j, row).BackColor = Color.Cyan;
                    }
                }
                else
                {
                    //iterate through row
                    for (int j = 0; j < table.ColumnCount; j++)
                    {
                        try
                        {
                            Item item = table.GetControlFromPosition(j, i).Tag as Item;
                            if (item.quantity < item.shipQuantity)
                            {
                                backColor = Color.LightYellow;
                            }
                            else
                            {
                                backColor = Color.FromArgb(255, 240, 240, 240);
                            }
                        }
                        catch
                        {
                            backColor = Color.FromArgb(255, 240, 240, 240);
                        }


                        //paint default color
                        table.GetControlFromPosition(j, i).BackColor = backColor;
                    }
                }
            }
        }

        private void EnterField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
            {
                if (InvoiceList.Visible)
                {
                    try
                    {
                        currentOrder = new Order(Convert.ToInt32(EnterField.Text));
                        openOrder(currentOrder);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        return;
                    }

                    for(int i = 0; i < currentOrder.items.Count; i++)
                    {
                        addToTable(ItemList, 
                            new List<string>() 
                            { 
                                currentOrder.items[i].partCode, 
                                currentOrder.items[i].vendorPart, 
                                currentOrder.items[i].description, 
                                currentOrder.items[i].shipQuantity.ToString(), 
                                currentOrder.items[i].quantity.ToString() 
                            }, 
                            currentOrder.items[i]);
                    }

                    InvoiceList.Hide();
                    ItemList.Show();
                }
                else if(ItemList.Visible)
                {
                    currentOrder.items.Find(x => x.partCode == EnterField.Text).quantity++;
                }
            }
        }
    }
}
