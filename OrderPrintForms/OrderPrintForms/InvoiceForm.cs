using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderPrintForms
{
    public partial class InvoiceForm : Form
    {
        Bitmap image;
        PrintDocument printDocument = new PrintDocument();

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

            printDocument.PrintPage += new PrintPageEventHandler(printDocument_Print);

            //set page number
            PageText.Text = page.ToString();
            PageTextB.Text = page.ToString();
            ItemList.Controls.Clear();
            ItemList.RowCount = 0;
            ItemList.AutoSize = true;
        }

        public void print()
        {
            captureScreen();
            printDocument.Print();
            this.Close();
        }

        private void printDocument_Print(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(image, 0, 0);
        }

        /*
         * @FUNCTION NAME:  public int addItem(<undefined class name> newItem)
         *
         * @BRIEF:          adds item to ItemList 
         * 
         * @PARAM:          <itemClass> newItem
         * 
         * @RETURNS:        int (number of items in list)
         * 
         * @NOTES:          none
         */
        public int addItem(Item item)
        {
            //add row for item
            ItemList.RowCount++;
            ItemList.RowStyles.Add(new RowStyle() { SizeType = SizeType.Absolute, Height = 22});

            //set item location
            Label tempLabel = new Label() { Text = item.location, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(0) };
            ItemList.Controls.Add(tempLabel, 0, ItemList.RowCount);

            //set item quantity
            tempLabel = new Label() { Text = item.quantity.ToString(), TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(0) };
            ItemList.Controls.Add(tempLabel, 0, ItemList.RowCount);

            //set item part number
            tempLabel = new Label() { Text = item.partCode, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(0) };
            ItemList.Controls.Add(tempLabel, 0, ItemList.RowCount);

            //set item vend part number
            tempLabel = new Label() { Text = item.vendorPart, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(0) };
            ItemList.Controls.Add(tempLabel, 0, ItemList.RowCount);

            //set item description
            tempLabel = new Label() { Text = item.description, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular), AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(0) };
            ItemList.Controls.Add(tempLabel, 0, ItemList.RowCount);

            return ItemList.RowCount;
        }

        private void captureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            image = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics graphics = Graphics.FromImage(image);
            graphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
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
