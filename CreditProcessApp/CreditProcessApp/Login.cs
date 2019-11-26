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

    /* 
     * @CLASS:      public partial class Login
     * @PURPOSE:    query user for login details
     *              create MainWindow on successful login
     * 
     * @PARAM:      none
     * 
     * @NOTES:      none
     */
    public partial class Login : Form
    {
        MainWindow main;

        public Login()
        {
            InitializeComponent();
        }

        /*
         * @FUNCTION:   private void SignInButton_Click()
         * @PURPOSE:    checks current credentials against database
         *              creates new MainWindow if valid
         *              reports error to user otherwise
         *              
         * @PARAM:      object sender
         *              EventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void SignInButton_Click(object sender, EventArgs e)
        {
            
                //establish database connection
                string strConnection = "DSN=Ranshu20190831";
                OdbcConnection pSqlConn = null;
                using (pSqlConn = new OdbcConnection(strConnection))
                {
                    //get user code and security lvl from database with given username and password
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
                        //report failure to user
                        throw new Exception("Invalid username or password");
                    }

                    //if security level isn't high enough
                    if(CurrentUser.security_lvl > 2)
                    {
                        //report failure
                        throw new Exception("You do not have the security level to access this application");
                    }

                    //open main window with current credentials
                    main = new MainWindow(this);
                    main.Show();
                    this.Hide();
                }
            /*}
            catch(Exception ex)
            {
                //show error message
                ErrorLabel.Text = "*" + ex.Message;
            }*/
        }

        /*
         * @FUNCTION:   private void CancelButton_Click()
         * @PURPOSE:    closes the application
         *              
         * @PARAM:      object sender
         *              EventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //not used
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        /*
         * @FUNCTION:   private void PasswordTextBox_KeyPress()
         * @PURPOSE:    checks if keypressed was enter
         *              calls sign in function if it was
         *              
         * @PARAM:      object sender
         *              EventArgs e
         * 
         * @RETURNS:    none
         * @NOTES:      none
         */
        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                SignInButton_Click(sender, e);
            }
        }
    }

    /* 
     * @CLASS: static class CurrentUser
     * @PURPOSE: hold current user code and security lvl for all forms
     * 
     * @PARAM: none
     * 
     * @NOTES: none
     */
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
