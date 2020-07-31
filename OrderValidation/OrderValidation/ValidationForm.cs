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
        LocationMenu locMenu;
        string invNot;
        string dateQualifier;
        string location;

        public ValidationForm(LocationMenu inMenu, string inLocation)
        {
            InitializeComponent();

            locMenu = inMenu;
            location = inLocation;
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
                pSqlConn.Open();

                //get unprocessed invoices from database
                using (OdbcCommand cmd = new OdbcCommand("{call getPickData (?, ?)}", pSqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(":invNum", invoiceNumber);
                    cmd.Parameters.AddWithValue(":inLoc", location);
                    OdbcDataReader invoiceReader = cmd.ExecuteReader();
                    if (invoiceReader.HasRows)
                    {
                        items = new List<Item>();
                        while (invoiceReader.Read())
                        {
                            Item item = new Item();
                            item.partCode = invoiceReader["part_code"].ToString().TrimEnd();
                            item.shipQuantity = Convert.ToInt32(invoiceReader["qty"].ToString());
                            item.quantity = Convert.ToInt32(invoiceReader["qty_pulled"].ToString());


                            using (OdbcCommand locationCmd = new OdbcCommand("{call getPartLocation (?, ?, ?)}", pSqlConn))
                            {
                                locationCmd.CommandType = CommandType.StoredProcedure;
                                locationCmd.Parameters.AddWithValue(":inPart", item.partCode);
                                locationCmd.Parameters.AddWithValue(":inLoc", location);
                                locationCmd.Parameters.AddWithValue(":qty", item.shipQuantity);
                                OdbcDataReader locationReader = locationCmd.ExecuteReader();
                                if (locationReader.HasRows)
                                {
                                    locationReader.Read();
                                    item.location = locationReader["INV_BIN"].ToString().TrimEnd();
                                    item.locationCode = locationReader["INV_TYPE"].ToString().TrimEnd();
                                    if (item.locationCode == "OS")
                                        item.location += "*";
                                }

                                locationReader.Close();
                            }

                            items.Add(item);
                        }
                    }

                    invoiceReader.Close();
                }


                if (items == null)
                {
                    //get unprocessed invoices from database
                    using (OdbcCommand cmd = new OdbcCommand("{call getAllParts (?)}", pSqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(":invNum", invoiceNumber);

                        OdbcDataReader invoiceReader = cmd.ExecuteReader();
                        if (invoiceReader.HasRows)
                        {
                            new MessageForm("No parts in queue for invoice: " + invoiceNumber + ". Displaying all parts for invoice possibly including backorder. Please scan invoice to populate queue and receive additional information.").Show();

                            items = new List<Item>();
                            while (invoiceReader.Read())
                            {
                                Item item = new Item();
                                item.partCode = invoiceReader["part"].ToString().TrimEnd();
                                item.shipQuantity = Convert.ToInt32(invoiceReader["qty"].ToString());
                                item.quantity = 0;


                                using (OdbcCommand locationCmd = new OdbcCommand("{call getPartLocation (?, ?, ?)}", pSqlConn))
                                {
                                    locationCmd.CommandType = CommandType.StoredProcedure;
                                    locationCmd.Parameters.AddWithValue(":inPart", item.partCode);
                                    locationCmd.Parameters.AddWithValue(":inLoc", location);
                                    locationCmd.Parameters.AddWithValue(":qty", item.shipQuantity);
                                    OdbcDataReader locationReader = locationCmd.ExecuteReader();
                                    if (locationReader.HasRows)
                                    {
                                        locationReader.Read();
                                        item.location = locationReader["INV_BIN"].ToString().TrimEnd();
                                        item.locationCode = locationReader["INV_TYPE"].ToString().TrimEnd();
                                        if (item.locationCode == "OS")
                                            item.location += "*";
                                    }

                                    locationReader.Close();
                                }

                                items.Add(item);
                            }
                        }

                        invoiceReader.Close();
                    }
                }

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
                pSqlConn.Open();
                string cmdString = "select h.bkar_inv_num, l.bkar_inv_sonum from bkarhinv h full outer join bkarinv l on h.bkar_inv_num = "+invoiceNum+" or h.bkar_inv_sonum = "+invoiceNum;

                using(OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn))
                {
                    OdbcDataReader reader = cmd.ExecuteReader();
                    
                    if(!reader.HasRows)
                    {
                        return order;
                    }
                }

                //get unprocessed invoices from database
                cmdString = "SELECT o.invoice_num, o.validated, o.packed_by, o.pulled_by, u.user_id, x.TRACKING_NUM " +
                                    "from wmsOrders o left join wmsUsers u on u.user_activity_notes like '%o.invoice_num%' and u.user_loc = '"+location+"' " +
                                    "left join TRACKING x on x.INVOICE_NUM = o.invoice_num " +
                                    "where o.invoice_num = "+ invoiceNum;

                using (OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn))
                {
                    OdbcDataReader invoiceReader = cmd.ExecuteReader();
                    if (invoiceReader.HasRows)
                    {
                        invoiceReader.Read();
                        order.validated = String.Format("{0:MM/dd/yyyy HH:mm}", invoiceReader["validated"].ToString());
                        if(invoiceReader["pulled_by"] != DBNull.Value)
                            order.pulled = ((string)invoiceReader["pulled_by"]).TrimEnd();
                        if (invoiceReader["packed_by"] != DBNull.Value)
                            order.packed = ((string)invoiceReader["packed_by"]).TrimEnd();
                        if (invoiceReader["user_id"] != DBNull.Value)
                            order.puller = ((string)invoiceReader["user_id"]).TrimEnd();
                        if (invoiceReader["tracking_num"] != DBNull.Value)
                            order.tracking = ((string)invoiceReader["tracking_num"]).TrimEnd();
                    }
                    else
                    {
                        DialogResult dialog = MessageBox.Show("Invoice: " + invoiceNum + " has not been printed. Print this invoice?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            string sqlStr = "update BKARHINV set BKAR_INV_MAX = 0 " +
                                "where bkar_inv_num = " + invoiceNum + " " +
                                "and bkar_inv_invdte > '"+DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd")+"'; " +
                                "update BKARINV set BKAR_INV_MAX = 0 " +
                                "where bkar_inv_sonum = " + invoiceNum;
                            using (OdbcCommand xCmd = new OdbcCommand(sqlStr, pSqlConn))
                            {
                                if(xCmd.ExecuteNonQuery() > 0)
                                    new MessageForm("Please give the print service a moment to process the invoice (approximately 10-15 seconds).").Show();
                                else
                                    new MessageForm("The invoice: "+invoiceNum+" is invalid. Please check the number or contact IT.").Show();
                            }

                        }

                        pSqlConn.Close();
                        return null;
                    }
                }
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
                                currentOrder.items[i].location,
                                currentOrder.items[i].shipQuantity.ToString(),
                                currentOrder.items[i].quantity.ToString()
                        },
                        currentOrder.items[i], color);
                    total += currentOrder.items[i].quantity;
                }
            }

            //InvoicePanel.Hide();
            ItemsPanel.Show();
            EnterButton.Text = "Edit Quantity";
            EnterField.Text = "";
            EnterButton.Enabled = false;
            InvoiceTypeButton.Enabled =
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
                        if(currentOrder == null)
                        {
                            EnterField.Text = "";
                            return;
                        }
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
            new ManagerMonitor(this, location).Show();
            Hide();
            /*
            string location = Interaction.InputBox("Location: ", "Reset Printer Status", currentItem.quantity.ToString());

            try
            {
                //establish database connection
                string strConnection = "DSN=Ranshu";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //get unprocessed invoices from database
                    string cmdString = "delete from wmsTrouble where trouble printer in (select printer_name from wmsPrinters where printer_location = '" + location + "')";

                    OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                    pSqlConn.Open();
                    cmd.ExecuteNonQuery();
                    pSqlConn.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
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
            bool clear = false;

            try
            {
                if (currentOrder == null)
                {
                    currentOrder = getOrderFromNum(Convert.ToInt32(EnterField.Text));
                    clear = true;
                }

                if (currentOrder != null)
                {
                    if (currentOrder.tracking != null)
                        if (MessageBox.Show("Invoice " + currentOrder.invoiceNumber + " has already been shipped with tracking: " + currentOrder.tracking + ". Are you sure you want to reprint it?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                            throw new Exception("Reprint cancelled.");

                    if (currentOrder.packed != null)
                        if (MessageBox.Show("Invoice " + currentOrder.invoiceNumber + " has already been processed by " + currentOrder.packed + ". Are you sure you want to reprint it?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                            throw new Exception("Reprint cancelled.");

                    if (currentOrder.pulled != null)
                        if (MessageBox.Show("Invoice "+currentOrder.invoiceNumber+" is currently being processed by "+currentOrder.pulled+". Are you sure you want to reprint it?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                            throw new Exception("Reprint cancelled.");

                    if (currentOrder.puller != null)
                        if (MessageBox.Show("Invoice " + currentOrder.invoiceNumber + " is currently being processed by " + currentOrder.puller + ". Are you sure you want to reprint it?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                            throw new Exception("Reprint cancelled.");


                    //establish database connection
                    string strConnection = "DSN=Ranshu";
                    OdbcConnection pSqlConn = null;
                    using (pSqlConn = new OdbcConnection(strConnection))
                    {
                        //get unprocessed invoices from database
                        string cmdString = "UPDATE BKARHINV " +
                            "SET BKAR_INV_MAX = 0 " +
                            "WHERE BKAR_INV_NUM = " + currentOrder.invoiceNumber;

                        OdbcCommand cmd = new OdbcCommand("{call reprintInvoice(?, ?)}", pSqlConn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(":invNum", currentOrder.invoiceNumber);
                        cmd.Parameters.AddWithValue(":loc", location);
                        pSqlConn.Open();
                        cmd.ExecuteNonQuery();
                        pSqlConn.Close();
                    }


                    using (pSqlConn = new OdbcConnection(strConnection))
                    {
                        //get unprocessed invoices from database
                        string cmdString = "UPDATE BKARINV " +
                            "SET BKAR_INV_MAX = 0 " +
                            "WHERE BKAR_INV_SONUM = " + currentOrder.invoiceNumber;

                        OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                        pSqlConn.Open();
                        cmd.ExecuteNonQuery();
                        pSqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if(clear)
            {
                currentOrder = null;
            }
        }

        private void InvoiceTypeButton_Click(object sender, EventArgs e)
        {
            bool clear = false;
            /*
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
            }*/
            try
            {
                if (currentOrder == null)
                {
                    currentOrder = getOrderFromNum(Convert.ToInt32(EnterField.Text));
                    clear = true;
                }

                if (currentOrder != null)
                {
                    string strConnection = "DSN=Ranshu";
                    OdbcConnection pSqlConn = null;
                    using (pSqlConn = new OdbcConnection(strConnection))
                    {
                        //get unprocessed invoices from database
                        string cmdString = "select printed, printer, packed_by, packed, pulled_by, pulled, notes from wmsOrders where invoice_num = " + currentOrder.invoiceNumber;

                        OdbcCommand cmd = new OdbcCommand(cmdString, pSqlConn);
                        pSqlConn.Open();
                        OdbcDataReader invoiceReader = cmd.ExecuteReader();
                        if (invoiceReader.HasRows)
                        {
                            while (invoiceReader.Read())
                            {
                                currentOrder.printed = invoiceReader["printed"].ToString();
                                currentOrder.printer = invoiceReader["printer"].ToString().TrimEnd();

                                try
                                {
                                    currentOrder.packed = Convert.ToDateTime(invoiceReader["packed"]).ToString("MM/dd/yyyy hh:mm tt") + " by " + invoiceReader["packed_by"].ToString().TrimEnd();
                                }
                                catch { }

                                try
                                {
                                    currentOrder.pulled = Convert.ToDateTime(invoiceReader["pulled"]).ToString("MM/dd/yyyy hh:mm tt") + " by " + invoiceReader["pulled_by"].ToString().TrimEnd();
                                }
                                catch { }
                                currentOrder.notes = invoiceReader["notes"].ToString().TrimEnd();
                            }
                        }

                        invoiceReader.Close();
                        pSqlConn.Close();
                    }

                    new MessageForm("Printed: " + currentOrder.printed + System.Environment.NewLine + "Printer: " + currentOrder.printer + System.Environment.NewLine + "Pulled: " + currentOrder.pulled + System.Environment.NewLine + "Packed: " + currentOrder.packed + System.Environment.NewLine + "\nNotes: " + System.Environment.NewLine + currentOrder.notes).Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace + ex.Message);
            }

            if(clear)
            {
                currentOrder = null;
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

        private void ValidationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            locMenu.Close();
        }
    }
}
