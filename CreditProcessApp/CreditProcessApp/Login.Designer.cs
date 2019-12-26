namespace CreditProcessApp
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.LoginLayoutTable = new System.Windows.Forms.TableLayoutPanel();
            this.CompanyBannerPanel = new System.Windows.Forms.Panel();
            this.ButtonTable = new System.Windows.Forms.TableLayoutPanel();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SignInButton = new System.Windows.Forms.Button();
            this.LoginTable = new System.Windows.Forms.TableLayoutPanel();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.UserPassTable = new System.Windows.Forms.TableLayoutPanel();
            this.PasswordTextBox = new System.Windows.Forms.RichTextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.UsernameTextBox = new System.Windows.Forms.RichTextBox();
            this.LoginLayoutTable.SuspendLayout();
            this.ButtonTable.SuspendLayout();
            this.LoginTable.SuspendLayout();
            this.UserPassTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginLayoutTable
            // 
            this.LoginLayoutTable.ColumnCount = 1;
            this.LoginLayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LoginLayoutTable.Controls.Add(this.CompanyBannerPanel, 0, 0);
            this.LoginLayoutTable.Controls.Add(this.ButtonTable, 0, 2);
            this.LoginLayoutTable.Controls.Add(this.LoginTable, 0, 1);
            this.LoginLayoutTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginLayoutTable.Location = new System.Drawing.Point(0, 0);
            this.LoginLayoutTable.Name = "LoginLayoutTable";
            this.LoginLayoutTable.Padding = new System.Windows.Forms.Padding(3);
            this.LoginLayoutTable.RowCount = 3;
            this.LoginLayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.LoginLayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LoginLayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.LoginLayoutTable.Size = new System.Drawing.Size(460, 343);
            this.LoginLayoutTable.TabIndex = 0;
            // 
            // CompanyBannerPanel
            // 
            this.CompanyBannerPanel.BackgroundImage = global::CreditProcessApp.Properties.Resources.RanshuLogo;
            this.CompanyBannerPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CompanyBannerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompanyBannerPanel.Location = new System.Drawing.Point(13, 13);
            this.CompanyBannerPanel.Margin = new System.Windows.Forms.Padding(10);
            this.CompanyBannerPanel.Name = "CompanyBannerPanel";
            this.CompanyBannerPanel.Size = new System.Drawing.Size(434, 80);
            this.CompanyBannerPanel.TabIndex = 0;
            // 
            // ButtonTable
            // 
            this.ButtonTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.ButtonTable.ColumnCount = 2;
            this.ButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ButtonTable.Controls.Add(this.CancelButton, 1, 0);
            this.ButtonTable.Controls.Add(this.SignInButton, 0, 0);
            this.ButtonTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonTable.Location = new System.Drawing.Point(3, 271);
            this.ButtonTable.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonTable.Name = "ButtonTable";
            this.ButtonTable.RowCount = 1;
            this.ButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ButtonTable.Size = new System.Drawing.Size(454, 69);
            this.ButtonTable.TabIndex = 1;
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CancelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CancelButton.FlatAppearance.BorderSize = 0;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(227, 1);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(226, 67);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SignInButton
            // 
            this.SignInButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.SignInButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SignInButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.SignInButton.FlatAppearance.BorderSize = 0;
            this.SignInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignInButton.Location = new System.Drawing.Point(1, 1);
            this.SignInButton.Margin = new System.Windows.Forms.Padding(0);
            this.SignInButton.Name = "SignInButton";
            this.SignInButton.Size = new System.Drawing.Size(225, 67);
            this.SignInButton.TabIndex = 0;
            this.SignInButton.Text = "Sign In";
            this.SignInButton.UseVisualStyleBackColor = false;
            this.SignInButton.Click += new System.EventHandler(this.SignInButton_Click);
            // 
            // LoginTable
            // 
            this.LoginTable.ColumnCount = 1;
            this.LoginTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.47126F));
            this.LoginTable.Controls.Add(this.ErrorLabel, 0, 2);
            this.LoginTable.Controls.Add(this.LoginLabel, 0, 0);
            this.LoginTable.Controls.Add(this.UserPassTable, 0, 1);
            this.LoginTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginTable.Location = new System.Drawing.Point(6, 106);
            this.LoginTable.Name = "LoginTable";
            this.LoginTable.RowCount = 3;
            this.LoginTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.80952F));
            this.LoginTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.19048F));
            this.LoginTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.LoginTable.Size = new System.Drawing.Size(448, 162);
            this.LoginTable.TabIndex = 2;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(0, 119);
            this.ErrorLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(448, 43);
            this.ErrorLabel.TabIndex = 2;
            this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginLabel.Location = new System.Drawing.Point(0, 0);
            this.LoginLabel.Margin = new System.Windows.Forms.Padding(0);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(448, 28);
            this.LoginLabel.TabIndex = 0;
            this.LoginLabel.Text = "Login";
            this.LoginLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserPassTable
            // 
            this.UserPassTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UserPassTable.ColumnCount = 2;
            this.UserPassTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.68966F));
            this.UserPassTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.31035F));
            this.UserPassTable.Controls.Add(this.PasswordTextBox, 1, 1);
            this.UserPassTable.Controls.Add(this.PasswordLabel, 0, 1);
            this.UserPassTable.Controls.Add(this.UsernameLabel, 0, 0);
            this.UserPassTable.Controls.Add(this.UsernameTextBox, 1, 0);
            this.UserPassTable.Location = new System.Drawing.Point(79, 31);
            this.UserPassTable.Name = "UserPassTable";
            this.UserPassTable.RowCount = 2;
            this.UserPassTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UserPassTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UserPassTable.Size = new System.Drawing.Size(290, 85);
            this.UserPassTable.TabIndex = 1;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextBox.Location = new System.Drawing.Point(92, 49);
            this.PasswordTextBox.Multiline = false;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(195, 28);
            this.PasswordTextBox.TabIndex = 1;
            this.PasswordTextBox.Text = "";
            this.PasswordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PasswordTextBox_KeyPress);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(0, 42);
            this.PasswordLabel.Margin = new System.Windows.Forms.Padding(0);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(89, 43);
            this.PasswordLabel.TabIndex = 2;
            this.PasswordLabel.Text = "Password:";
            this.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLabel.Location = new System.Drawing.Point(0, 0);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(89, 42);
            this.UsernameLabel.TabIndex = 1;
            this.UsernameLabel.Text = "Username:";
            this.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.UsernameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameTextBox.Location = new System.Drawing.Point(92, 7);
            this.UsernameTextBox.Multiline = false;
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(195, 28);
            this.UsernameTextBox.TabIndex = 0;
            this.UsernameTextBox.Text = "";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 343);
            this.Controls.Add(this.LoginLayoutTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.Text = "CreditProcessApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.LoginLayoutTable.ResumeLayout(false);
            this.ButtonTable.ResumeLayout(false);
            this.LoginTable.ResumeLayout(false);
            this.LoginTable.PerformLayout();
            this.UserPassTable.ResumeLayout(false);
            this.UserPassTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel LoginLayoutTable;
        private System.Windows.Forms.Panel CompanyBannerPanel;
        private System.Windows.Forms.TableLayoutPanel ButtonTable;
        private System.Windows.Forms.TableLayoutPanel LoginTable;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.TableLayoutPanel UserPassTable;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.RichTextBox UsernameTextBox;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        private System.Windows.Forms.Button CancelButton;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        private System.Windows.Forms.Button SignInButton;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.RichTextBox PasswordTextBox;
    }
}