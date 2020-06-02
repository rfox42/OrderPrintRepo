using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderValidation
{
    public partial class LocationMenu : Form
    {
        public LocationMenu()
        {
            InitializeComponent();
        }

        private void Reno_Click(object sender, EventArgs e)
        {
            new ValidationForm(this, (sender as Button).Tag.ToString()).Show();
            Hide();
        }
    }
}
