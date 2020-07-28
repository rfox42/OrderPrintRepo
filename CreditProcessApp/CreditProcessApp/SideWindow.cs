using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace CreditProcessApp
{
    public partial class SideWindow : Form
    {
        MainWindow main;

        public SideWindow()
        {
            InitializeComponent();

            getOpenOrders("");
        }

        private void getOpenOrders(string searchTerms)
        {
            List<Invoice> invoices = new List<Invoice>();
            ProcessInvoiceList.Controls.Clear();
            ProcessInvoiceList.RowCount = 0;

            //update control user in db
            string strConnection = "DSN=Ranshu";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                pSqlConn.Open();
                string sqlStr = "select crdt_inv_num, crdt_inv_cuscod, crdt_inv_slsp, BKAR_INVT_AMT, BKAR_INVT_AMTRM, crdt_inv_date " +
                    "from CRDTINV inner join BKARINVT " +
                    "on BKAR_INVT_NUM = CRDT_INV_NUM and BKAR_INVT_CODE = CRDT_INV_CUSCOD " +
                    "where BKAR_INVT_AMTRM > 0 " + searchTerms + " order by CRDT_INV_DATE";
                using (OdbcCommand cmd = new OdbcCommand(sqlStr, pSqlConn))
                {
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Invoice invoice = new Invoice();

                            invoice.invoiceNumber = Convert.ToInt32(reader["crdt_inv_num"].ToString());
                            invoice.account = reader["CRDT_INV_CUSCOD"].ToString().TrimEnd();
                            invoice.salesPerson = Convert.ToInt32(reader["crdt_inv_slsp"].ToString());
                            invoice.total = Convert.ToDouble(reader["BKAR_INVT_AMT"].ToString());
                            invoice.remainder = Convert.ToDouble(reader["bkar_invt_amtrm"].ToString());
                            invoice.date = reader["CRDT_INV_DATE"].ToString().TrimEnd();

                            invoices.Add(invoice);
                        }
                    }
                }
                pSqlConn.Close();
            }

            foreach (Invoice inv in invoices)
            {
                addToTable(ProcessInvoiceList, new List<string>() { inv.invoiceNumber.ToString(), inv.account, inv.salesPerson.ToString(), inv.total.ToString(), inv.remainder.ToString(), inv.date }, inv);
            }
        }

        private void addToTable(TableLayoutPanel table, List<string> data, Invoice invoice, Color? color = null)
        {
            //increment rowcount of given table and add new row
            table.RowCount++;
            table.RowStyles.Add(new RowStyle() { SizeType = SizeType.AutoSize });

            for (int i = 0; i < data.Count; i++)
            {
                Label tempLabel = new Label() { Text = data[i], TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular), Dock = DockStyle.Fill, Margin = new Padding(0, 1, 0, 1), Tag = invoice, Parent = table, ForeColor = color ?? SystemColors.ControlText, UseMnemonic = false };
                table.Controls.Add(tempLabel, i, table.RowCount);
            }
        }

        private void ReprocessButton_Click(object sender, EventArgs e)
        {
            if(SearchText.Text != "" && SearchBySelection.SelectedItem != null)
            {
                switch(SearchBySelection.SelectedIndex)
                {
                    case 0:
                        getOpenOrders("and crdt_inv_slsp = " + SearchText.Text);
                        break;

                    case 1:
                        getOpenOrders("and crdt_inv_cuscod = " + SearchText.Text);
                        break;

                    default:
                        getOpenOrders("");
                        break;
                }
            }
            else
            {
                getOpenOrders("");
            }
        }

        private void SearchBySelection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
