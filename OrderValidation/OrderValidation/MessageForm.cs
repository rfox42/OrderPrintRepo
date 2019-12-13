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
    public partial class MessageForm : Form
    {
        public MessageForm(string message, string header = "")
        {
            InitializeComponent();

            Title.Text = header;
            Message.Text = message;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
