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
    public partial class MainWindow : Form
    {
        Invoice currentInvoice;

        public MainWindow()
        {
            InitializeComponent();


            ProcessInvoiceList.Controls.Clear();
            ProcessInvoiceList.RowCount = 0;
            this.ProcessInvoiceList.CellPaint += tableLayoutPanel1_Paint;

            populateTables();
        }

        private void populateTables()
        {
            List<Invoice> incomplete = new List<Invoice>();
            List<Invoice> complete = new List<Invoice>();
            string strConnection = "DSN=Ranshu20190831";
            OdbcConnection pSqlConn = null;
            using (pSqlConn = new OdbcConnection(strConnection))
            {
                string creditCommand = "SELECT CRDT_INV_NUM, CRDT_INV_CUSCOD, CRDT_INV_DATE, CRDT_INV_TOTAL, CRDT_INV_NOTES FROM CRDTINV WHERE CRDT_INV_PROCESSED = 0";
                OdbcCommand cmd = new OdbcCommand(creditCommand, pSqlConn);
                pSqlConn.Open();
                OdbcDataReader creditReader = cmd.ExecuteReader();
                if(creditReader.HasRows)
                {
                    while(creditReader.Read())
                    {
                        Invoice invoice = new Invoice();

                        invoice.invoiceNumber = Convert.ToInt32(creditReader["CRDT_INV_NUM"].ToString());
                        invoice.account = creditReader["CRDT_INV_CUSCOD"].ToString().TrimEnd();
                        invoice.date = creditReader["CRDT_INV_DATE"].ToString().TrimEnd();
                        invoice.total = Convert.ToDouble(creditReader["CRDT_INV_TOTAL"].ToString());
                        invoice.notes = creditReader["CRDT_INV_NOTES"].ToString();

                        incomplete.Add(invoice);
                    }
                }

                creditReader.Close();

                creditCommand = "SELECT CRDT_INV_NUM, CRDT_INV_CUSCOD, CRDT_INV_DATE, CRDT_INV_TOTAL, CRDT_INV_CHARGETIME, CRDT_INV_USER, CRDT_INV_NOTES FROM CRDTINV WHERE CRDT_INV_PROCESSED = 1";
                cmd = new OdbcCommand(creditCommand, pSqlConn);
                creditReader = cmd.ExecuteReader();
                if (creditReader.HasRows)
                {
                    while (creditReader.Read())
                    {
                        Invoice invoice = new Invoice();

                        invoice.invoiceNumber = Convert.ToInt32(creditReader["CRDT_INV_NUM"].ToString());
                        invoice.account = creditReader["CRDT_INV_CUSCOD"].ToString().TrimEnd();
                        invoice.date = creditReader["CRDT_INV_DATE"].ToString().TrimEnd();
                        invoice.total = Convert.ToDouble(creditReader["CRDT_INV_TOTAL"].ToString());
                        invoice.chargeTime = creditReader["CRDT_INV_CHARGETIME"].ToString();
                        invoice.user = creditReader["CRDT_INV_USER"].ToString();
                        invoice.notes = creditReader["CRDT_INV_NOTES"].ToString();

                        complete.Add(invoice);
                    }
                }

                creditReader.Close();
                pSqlConn.Close();
            }

            for(int i = 0; i < incomplete.Count; i++)
            {
                addToTable(ProcessInvoiceList, incomplete[i]);
            }
        }

        private void addToTable(TableLayoutPanel table, Invoice invoice)
        {
            table.RowCount++;
            table.RowStyles.Add(new RowStyle() { SizeType = SizeType.Absolute, Height = 30 });

            Label tempLabel = new Label() { Text = invoice.invoiceNumber.ToString(), TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Margin = new Padding(1), Tag = invoice };
            tempLabel.Click += new EventHandler(tempLabel_Click);
            table.Controls.Add(tempLabel, 0, table.RowCount);

            tempLabel = new Label() { Text = invoice.account, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Margin = new Padding(1), Tag = invoice };
            tempLabel.Click += new EventHandler(tempLabel_Click);
            table.Controls.Add(tempLabel, 1, table.RowCount);

            tempLabel = new Label() { Text = invoice.date, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Margin = new Padding(1), Tag = invoice };
            tempLabel.Click += new EventHandler(tempLabel_Click);
            table.Controls.Add(tempLabel, 2, table.RowCount);
        }

        private void tableLayoutPanel1_Paint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, new Point(e.CellBounds.Left, e.CellBounds.Bottom), new Point(e.CellBounds.Right, e.CellBounds.Bottom));
        }

        private void tempLabel_Click(object sender, EventArgs e)
        {
            Label clicked = (Label)sender;
            currentInvoice = clicked.Tag as Invoice;
            int row = ProcessInvoiceList.GetRow(clicked);

            for(int i = 0; i <= ProcessInvoiceList.RowCount; i++)
            {
                if(i == row)
                {
                    for (int j = 0; j < ProcessInvoiceList.ColumnCount; j++)
                    {
                        ProcessInvoiceList.GetControlFromPosition(j, row).BackColor = Color.LightGray;
                    }
                }
                else
                {
                    for (int j = 0; j < ProcessInvoiceList.ColumnCount; j++)
                    {
                        ProcessInvoiceList.GetControlFromPosition(j, row).BackColor = Color.FromName("Control");
                    }
                }
            }

        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit the application?", "EXIT ProcessTool", MessageBoxButtons.YesNo);

            if(dialogResult == DialogResult.Yes)
            {
                Close();
            }
            else
            {

            }
        }
    }
}
