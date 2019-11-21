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
    public partial class Login : Form
    {
        MainWindow main;

        public Login()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            try
            {
                //establish database connection
                string strConnection = "DSN=Ranshu20190831";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //get unprocessed invoices from database
                    string creditCommand = "SELECT BKSY_USER_CODE, BKSY_USER_SCTY FROM BKSYUSER WHERE BKSY_USER_CODE = '" + UsernameTextBox.Text + "' AND BKSY_USER_PSWD = '" + PasswordTextBox.Text + "'";
                    OdbcCommand cmd = new OdbcCommand(creditCommand, pSqlConn);
                    pSqlConn.Open();
                    OdbcDataReader userReader = cmd.ExecuteReader();
                    if (userReader.HasRows)
                    {
                        CurrentUser.user_code = userReader["BKSY_USER_CODE"].ToString();
                        CurrentUser.security_lvl = Convert.ToInt32(userReader["BKSY_USER_SCTY"].ToString());
                    }
                    else
                    {
                        throw new Exception("Invalid username or password");
                    }

                    if(CurrentUser.security_lvl > 2)
                    {
                        throw new Exception("You do not have the security level to access this application");
                    }

                    main = new MainWindow(this);
                    main.Show();
                    this.Hide();
                }
            }
            catch(Exception ex)
            {
                ErrorLabel.Text = "*" + ex.Message;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }


    static class CurrentUser
    {
        private static string _user_code = "";
        private static int _security_lvl = 6;

        public static string user_code
        {
            get { return _user_code; }
            set { _user_code = value; }
        }

        public static int security_lvl
        {
            get { return _security_lvl; }
            set { _security_lvl = value; }
        }
    }
}
