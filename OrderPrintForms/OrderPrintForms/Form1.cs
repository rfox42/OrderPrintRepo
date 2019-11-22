using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderPrintForms
{
    public partial class Form1 : Form
    {
        InvoiceForm invoiceForm;
        Timer invoiceTimer;

        public Form1()
        {
            InitializeComponent();

            invoiceTimer = new Timer();
            invoiceTimer.Tick += new EventHandler(InvoiceForm_Print);
            invoiceTimer.Interval = 50;
            invoiceTimer.Enabled = true;

            invoiceForm = new InvoiceForm();
            invoiceForm.Show();
            Hide();
            invoiceTimer.Start();
        }

        private void InvoiceForm_Print(object sender, EventArgs e)
        {
            invoiceTimer.Stop();
            invoiceForm.print();
        }
    }
}
