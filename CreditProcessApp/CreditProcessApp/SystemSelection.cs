using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreditProcessApp
{
    public partial class SystemSelection : Form
    {
        public SystemSelection()
        {
            InitializeComponent();
        }

        private void AccountingButton_Click(object sender, EventArgs e)
        {
            new Login(this, "").Show();
            this.Hide();
        }

        private void LocalSalesButton_Click(object sender, EventArgs e)
        {
            LocationSelectionTable.Show();
        }

        private void LocationButton_Click(object sender, EventArgs e)
        {
            string location = (sender as Button).Text;
            new Login(this, location).Show();
            this.Hide();
        }
    }
}
