using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderPrint
{
    public partial class InvoiceForm : Form
    {
        public InvoiceForm(int page = 1)
        {
            InitializeComponent();

            //if secondary page hide unnecessary data
            if(page > 1)
            {
                BillToLabel1.Hide();
                BillToText.Hide();
                LocationLabel.Hide();
                CustomerPOLabel.Hide();
                AccountLabel.Hide();
                PaymentTermsLabel.Hide();
            }

            //set page number
            PageText.Text = page.ToString();
            ItemList.Controls.Clear();
            ItemList.RowCount = 0;
            ItemList.AutoSize = true;
        }

        /*------------------------------------------------------------------
         * @FUNCTION NAME:  public int addItem(<undefined class name> newItem)
         *
         * @BRIEF:          adds item to ItemList 
         * 
         * @PARAM:          <itemClass> newItem
         * 
         * @RETURNS:        int (number of items in list)
         * 
         * @NOTES:          none
         ------------------------------------------------------------------*/
        public int addItem(string location, string quantity, string partNum, string description, string customPart = "")
        {
            //add row for item
            ItemList.RowStyles.Add(new RowStyle() { SizeType = SizeType.Absolute, Height = 22});

            //set item location
            Label tempLabel = new Label() { TextAlign = ContentAlignment.MiddleLeft, AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(0) };
            ItemList.Controls.Add(tempLabel, 0, ItemList.RowCount);

            //



            return 0;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BillToText_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void PackListTable_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
