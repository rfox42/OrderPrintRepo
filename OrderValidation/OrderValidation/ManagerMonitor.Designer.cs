namespace OrderValidation
{
    partial class ManagerMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerMonitor));
            this.ShippingTab = new System.Windows.Forms.TabPage();
            this.InvoicePanel = new System.Windows.Forms.Panel();
            this.InvoiceTable = new System.Windows.Forms.TableLayoutPanel();
            this.EnterButton = new System.Windows.Forms.Button();
            this.InvoiceHeaderLine = new System.Windows.Forms.Label();
            this.InvoiceHeaderTable = new System.Windows.Forms.TableLayoutPanel();
            this.UserHeader = new System.Windows.Forms.Label();
            this.TimeHeader = new System.Windows.Forms.Label();
            this.InvoiceNumHeader = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ActivityHeader = new System.Windows.Forms.Label();
            this.InvoiceListPanel = new System.Windows.Forms.Panel();
            this.InvoiceList = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.Receiving = new System.Windows.Forms.TabPage();
            this.UsersPanel = new System.Windows.Forms.Panel();
            this.UsersTable = new System.Windows.Forms.TableLayoutPanel();
            this.NewUserButton = new System.Windows.Forms.Button();
            this.UsersLine = new System.Windows.Forms.Label();
            this.UserHeaderTable = new System.Windows.Forms.TableLayoutPanel();
            this.NotesLabel = new System.Windows.Forms.Label();
            this.DeviceLabel = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.ActivityLabel = new System.Windows.Forms.Label();
            this.UserListPanel = new System.Windows.Forms.Panel();
            this.UserList = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ShippingTab.SuspendLayout();
            this.InvoicePanel.SuspendLayout();
            this.InvoiceTable.SuspendLayout();
            this.InvoiceHeaderTable.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.InvoiceListPanel.SuspendLayout();
            this.InvoiceList.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.Receiving.SuspendLayout();
            this.UsersPanel.SuspendLayout();
            this.UsersTable.SuspendLayout();
            this.UserHeaderTable.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.UserListPanel.SuspendLayout();
            this.UserList.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShippingTab
            // 
            this.ShippingTab.Controls.Add(this.InvoicePanel);
            this.ShippingTab.Location = new System.Drawing.Point(4, 22);
            this.ShippingTab.Name = "ShippingTab";
            this.ShippingTab.Size = new System.Drawing.Size(792, 424);
            this.ShippingTab.TabIndex = 1;
            this.ShippingTab.Text = "Orders";
            this.ShippingTab.UseVisualStyleBackColor = true;
            // 
            // InvoicePanel
            // 
            this.InvoicePanel.AutoScroll = true;
            this.InvoicePanel.Controls.Add(this.InvoiceTable);
            this.InvoicePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoicePanel.Location = new System.Drawing.Point(0, 0);
            this.InvoicePanel.Margin = new System.Windows.Forms.Padding(0);
            this.InvoicePanel.Name = "InvoicePanel";
            this.InvoicePanel.Size = new System.Drawing.Size(792, 424);
            this.InvoicePanel.TabIndex = 7;
            // 
            // InvoiceTable
            // 
            this.InvoiceTable.ColumnCount = 1;
            this.InvoiceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InvoiceTable.Controls.Add(this.EnterButton, 0, 0);
            this.InvoiceTable.Controls.Add(this.InvoiceHeaderLine, 0, 2);
            this.InvoiceTable.Controls.Add(this.InvoiceHeaderTable, 0, 1);
            this.InvoiceTable.Controls.Add(this.InvoiceListPanel, 0, 3);
            this.InvoiceTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceTable.Location = new System.Drawing.Point(0, 0);
            this.InvoiceTable.Name = "InvoiceTable";
            this.InvoiceTable.RowCount = 4;
            this.InvoiceTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.InvoiceTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InvoiceTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.InvoiceTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InvoiceTable.Size = new System.Drawing.Size(792, 424);
            this.InvoiceTable.TabIndex = 6;
            // 
            // EnterButton
            // 
            this.EnterButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.EnterButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.EnterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnterButton.Location = new System.Drawing.Point(0, 0);
            this.EnterButton.Margin = new System.Windows.Forms.Padding(0);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(196, 35);
            this.EnterButton.TabIndex = 18;
            this.EnterButton.Text = "Show Truck";
            this.EnterButton.UseVisualStyleBackColor = false;
            this.EnterButton.Click += new System.EventHandler(this.EnterButton_Click);
            // 
            // InvoiceHeaderLine
            // 
            this.InvoiceHeaderLine.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.InvoiceHeaderLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InvoiceHeaderLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.InvoiceHeaderLine.Location = new System.Drawing.Point(0, 72);
            this.InvoiceHeaderLine.Margin = new System.Windows.Forms.Padding(0);
            this.InvoiceHeaderLine.Name = "InvoiceHeaderLine";
            this.InvoiceHeaderLine.Size = new System.Drawing.Size(792, 3);
            this.InvoiceHeaderLine.TabIndex = 16;
            // 
            // InvoiceHeaderTable
            // 
            this.InvoiceHeaderTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.InvoiceHeaderTable.ColumnCount = 4;
            this.InvoiceHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InvoiceHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InvoiceHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InvoiceHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InvoiceHeaderTable.Controls.Add(this.UserHeader, 3, 0);
            this.InvoiceHeaderTable.Controls.Add(this.TimeHeader, 2, 0);
            this.InvoiceHeaderTable.Controls.Add(this.InvoiceNumHeader, 0, 0);
            this.InvoiceHeaderTable.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.InvoiceHeaderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceHeaderTable.Location = new System.Drawing.Point(0, 35);
            this.InvoiceHeaderTable.Margin = new System.Windows.Forms.Padding(0);
            this.InvoiceHeaderTable.Name = "InvoiceHeaderTable";
            this.InvoiceHeaderTable.Padding = new System.Windows.Forms.Padding(0, 0, 18, 0);
            this.InvoiceHeaderTable.RowCount = 1;
            this.InvoiceHeaderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InvoiceHeaderTable.Size = new System.Drawing.Size(792, 37);
            this.InvoiceHeaderTable.TabIndex = 3;
            // 
            // UserHeader
            // 
            this.UserHeader.AutoSize = true;
            this.UserHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserHeader.Location = new System.Drawing.Point(581, 2);
            this.UserHeader.Margin = new System.Windows.Forms.Padding(1);
            this.UserHeader.Name = "UserHeader";
            this.UserHeader.Size = new System.Drawing.Size(191, 33);
            this.UserHeader.TabIndex = 4;
            this.UserHeader.Text = "User";
            this.UserHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimeHeader
            // 
            this.TimeHeader.AutoSize = true;
            this.TimeHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimeHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeHeader.Location = new System.Drawing.Point(388, 2);
            this.TimeHeader.Margin = new System.Windows.Forms.Padding(1);
            this.TimeHeader.Name = "TimeHeader";
            this.TimeHeader.Size = new System.Drawing.Size(190, 33);
            this.TimeHeader.TabIndex = 3;
            this.TimeHeader.Text = "TimeStamp";
            this.TimeHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InvoiceNumHeader
            // 
            this.InvoiceNumHeader.AutoSize = true;
            this.InvoiceNumHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceNumHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvoiceNumHeader.Location = new System.Drawing.Point(2, 2);
            this.InvoiceNumHeader.Margin = new System.Windows.Forms.Padding(1);
            this.InvoiceNumHeader.Name = "InvoiceNumHeader";
            this.InvoiceNumHeader.Size = new System.Drawing.Size(190, 33);
            this.InvoiceNumHeader.TabIndex = 0;
            this.InvoiceNumHeader.Text = "Invoice #";
            this.InvoiceNumHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.ActivityHeader, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(194, 1);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(192, 35);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // ActivityHeader
            // 
            this.ActivityHeader.AutoSize = true;
            this.ActivityHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActivityHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivityHeader.Location = new System.Drawing.Point(0, 0);
            this.ActivityHeader.Margin = new System.Windows.Forms.Padding(0);
            this.ActivityHeader.Name = "ActivityHeader";
            this.ActivityHeader.Size = new System.Drawing.Size(192, 35);
            this.ActivityHeader.TabIndex = 2;
            this.ActivityHeader.Text = "Activity";
            this.ActivityHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InvoiceListPanel
            // 
            this.InvoiceListPanel.AutoScroll = true;
            this.InvoiceListPanel.Controls.Add(this.InvoiceList);
            this.InvoiceListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceListPanel.Location = new System.Drawing.Point(0, 77);
            this.InvoiceListPanel.Margin = new System.Windows.Forms.Padding(0);
            this.InvoiceListPanel.Name = "InvoiceListPanel";
            this.InvoiceListPanel.Size = new System.Drawing.Size(792, 347);
            this.InvoiceListPanel.TabIndex = 17;
            // 
            // InvoiceList
            // 
            this.InvoiceList.AutoSize = true;
            this.InvoiceList.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.InvoiceList.ColumnCount = 4;
            this.InvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InvoiceList.Controls.Add(this.label6, 1, 0);
            this.InvoiceList.Controls.Add(this.label8, 0, 0);
            this.InvoiceList.Dock = System.Windows.Forms.DockStyle.Top;
            this.InvoiceList.Location = new System.Drawing.Point(0, 0);
            this.InvoiceList.Name = "InvoiceList";
            this.InvoiceList.RowCount = 1;
            this.InvoiceList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InvoiceList.Size = new System.Drawing.Size(792, 22);
            this.InvoiceList.TabIndex = 18;
            this.InvoiceList.VisibleChanged += new System.EventHandler(this.InvoiceList_VisibleChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(199, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 18);
            this.label6.TabIndex = 3;
            this.label6.Text = "08/20/2019";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(194, 18);
            this.label8.TabIndex = 1;
            this.label8.Text = "1344925";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.ShippingTab);
            this.TabControl.Controls.Add(this.Receiving);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(800, 450);
            this.TabControl.TabIndex = 0;
            this.TabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabControl_Selecting);
            this.TabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl_Selected);
            this.TabControl.Deselected += new System.Windows.Forms.TabControlEventHandler(this.TabControl_Deselected);
            // 
            // Receiving
            // 
            this.Receiving.Controls.Add(this.UsersPanel);
            this.Receiving.Location = new System.Drawing.Point(4, 22);
            this.Receiving.Name = "Receiving";
            this.Receiving.Size = new System.Drawing.Size(792, 424);
            this.Receiving.TabIndex = 2;
            this.Receiving.Text = "Users";
            this.Receiving.UseVisualStyleBackColor = true;
            // 
            // UsersPanel
            // 
            this.UsersPanel.AutoScroll = true;
            this.UsersPanel.Controls.Add(this.UsersTable);
            this.UsersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersPanel.Location = new System.Drawing.Point(0, 0);
            this.UsersPanel.Name = "UsersPanel";
            this.UsersPanel.Size = new System.Drawing.Size(792, 424);
            this.UsersPanel.TabIndex = 8;
            // 
            // UsersTable
            // 
            this.UsersTable.ColumnCount = 1;
            this.UsersTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.UsersTable.Controls.Add(this.NewUserButton, 0, 0);
            this.UsersTable.Controls.Add(this.UsersLine, 0, 2);
            this.UsersTable.Controls.Add(this.UserHeaderTable, 0, 1);
            this.UsersTable.Controls.Add(this.UserListPanel, 0, 3);
            this.UsersTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersTable.Location = new System.Drawing.Point(0, 0);
            this.UsersTable.Name = "UsersTable";
            this.UsersTable.RowCount = 4;
            this.UsersTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.UsersTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.UsersTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.UsersTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.UsersTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.UsersTable.Size = new System.Drawing.Size(792, 424);
            this.UsersTable.TabIndex = 6;
            // 
            // NewUserButton
            // 
            this.NewUserButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.NewUserButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.NewUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewUserButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewUserButton.Location = new System.Drawing.Point(0, 0);
            this.NewUserButton.Margin = new System.Windows.Forms.Padding(0);
            this.NewUserButton.Name = "NewUserButton";
            this.NewUserButton.Size = new System.Drawing.Size(196, 40);
            this.NewUserButton.TabIndex = 19;
            this.NewUserButton.Text = "New User";
            this.NewUserButton.UseVisualStyleBackColor = false;
            this.NewUserButton.Click += new System.EventHandler(this.NewUserButton_Click);
            // 
            // UsersLine
            // 
            this.UsersLine.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.UsersLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.UsersLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.UsersLine.Location = new System.Drawing.Point(0, 77);
            this.UsersLine.Margin = new System.Windows.Forms.Padding(0);
            this.UsersLine.Name = "UsersLine";
            this.UsersLine.Size = new System.Drawing.Size(792, 3);
            this.UsersLine.TabIndex = 16;
            // 
            // UserHeaderTable
            // 
            this.UserHeaderTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.UserHeaderTable.ColumnCount = 4;
            this.UserHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.91203F));
            this.UserHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.89392F));
            this.UserHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.489F));
            this.UserHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.83441F));
            this.UserHeaderTable.Controls.Add(this.NotesLabel, 3, 0);
            this.UserHeaderTable.Controls.Add(this.DeviceLabel, 2, 0);
            this.UserHeaderTable.Controls.Add(this.UserLabel, 0, 0);
            this.UserHeaderTable.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.UserHeaderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserHeaderTable.Location = new System.Drawing.Point(0, 40);
            this.UserHeaderTable.Margin = new System.Windows.Forms.Padding(0);
            this.UserHeaderTable.Name = "UserHeaderTable";
            this.UserHeaderTable.Padding = new System.Windows.Forms.Padding(0, 0, 18, 0);
            this.UserHeaderTable.RowCount = 1;
            this.UserHeaderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.UserHeaderTable.Size = new System.Drawing.Size(792, 37);
            this.UserHeaderTable.TabIndex = 3;
            // 
            // NotesLabel
            // 
            this.NotesLabel.AutoSize = true;
            this.NotesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesLabel.Location = new System.Drawing.Point(498, 2);
            this.NotesLabel.Margin = new System.Windows.Forms.Padding(1);
            this.NotesLabel.Name = "NotesLabel";
            this.NotesLabel.Size = new System.Drawing.Size(274, 33);
            this.NotesLabel.TabIndex = 4;
            this.NotesLabel.Text = "Notes";
            this.NotesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeviceLabel
            // 
            this.DeviceLabel.AutoSize = true;
            this.DeviceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeviceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceLabel.Location = new System.Drawing.Point(386, 2);
            this.DeviceLabel.Margin = new System.Windows.Forms.Padding(1);
            this.DeviceLabel.Name = "DeviceLabel";
            this.DeviceLabel.Size = new System.Drawing.Size(109, 33);
            this.DeviceLabel.TabIndex = 3;
            this.DeviceLabel.Text = "Device";
            this.DeviceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserLabel.Location = new System.Drawing.Point(2, 2);
            this.UserLabel.Margin = new System.Windows.Forms.Padding(1);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(120, 33);
            this.UserLabel.TabIndex = 0;
            this.UserLabel.Text = "User";
            this.UserLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.ActivityLabel, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(124, 1);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(260, 35);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // ActivityLabel
            // 
            this.ActivityLabel.AutoSize = true;
            this.ActivityLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActivityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivityLabel.Location = new System.Drawing.Point(0, 0);
            this.ActivityLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ActivityLabel.Name = "ActivityLabel";
            this.ActivityLabel.Size = new System.Drawing.Size(260, 35);
            this.ActivityLabel.TabIndex = 2;
            this.ActivityLabel.Text = "Activity";
            this.ActivityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserListPanel
            // 
            this.UserListPanel.AutoScroll = true;
            this.UserListPanel.Controls.Add(this.UserList);
            this.UserListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserListPanel.Location = new System.Drawing.Point(0, 82);
            this.UserListPanel.Margin = new System.Windows.Forms.Padding(0);
            this.UserListPanel.Name = "UserListPanel";
            this.UserListPanel.Size = new System.Drawing.Size(792, 342);
            this.UserListPanel.TabIndex = 17;
            // 
            // UserList
            // 
            this.UserList.AutoSize = true;
            this.UserList.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.UserList.ColumnCount = 4;
            this.UserList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.54994F));
            this.UserList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.26043F));
            this.UserList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.66498F));
            this.UserList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.65107F));
            this.UserList.Controls.Add(this.label7, 1, 0);
            this.UserList.Controls.Add(this.label9, 0, 0);
            this.UserList.Dock = System.Windows.Forms.DockStyle.Top;
            this.UserList.Location = new System.Drawing.Point(0, 0);
            this.UserList.Name = "UserList";
            this.UserList.RowCount = 1;
            this.UserList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.UserList.Size = new System.Drawing.Size(792, 22);
            this.UserList.TabIndex = 18;
            this.UserList.VisibleChanged += new System.EventHandler(this.UserList_VisibleChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(125, 2);
            this.label7.Margin = new System.Windows.Forms.Padding(1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(267, 18);
            this.label7.TabIndex = 3;
            this.label7.Text = "RECEIVING 08/20/2019 10:25 AM";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(2, 2);
            this.label9.Margin = new System.Windows.Forms.Padding(1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 18);
            this.label9.TabIndex = 1;
            this.label9.Text = "Richard";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManagerMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManagerMonitor";
            this.Text = "ManagerMonitor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManagerMonitor_FormClosed);
            this.Load += new System.EventHandler(this.ManagerMonitor_Load);
            this.ResizeBegin += new System.EventHandler(this.ManagerMonitor_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.ManagerMonitor_ResizeEnd);
            this.ShippingTab.ResumeLayout(false);
            this.InvoicePanel.ResumeLayout(false);
            this.InvoiceTable.ResumeLayout(false);
            this.InvoiceHeaderTable.ResumeLayout(false);
            this.InvoiceHeaderTable.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.InvoiceListPanel.ResumeLayout(false);
            this.InvoiceListPanel.PerformLayout();
            this.InvoiceList.ResumeLayout(false);
            this.InvoiceList.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.Receiving.ResumeLayout(false);
            this.UsersPanel.ResumeLayout(false);
            this.UsersTable.ResumeLayout(false);
            this.UserHeaderTable.ResumeLayout(false);
            this.UserHeaderTable.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.UserListPanel.ResumeLayout(false);
            this.UserListPanel.PerformLayout();
            this.UserList.ResumeLayout(false);
            this.UserList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage ShippingTab;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage Receiving;
        private System.Windows.Forms.Panel UsersPanel;
        private System.Windows.Forms.TableLayoutPanel UsersTable;
        private System.Windows.Forms.Label UsersLine;
        private System.Windows.Forms.TableLayoutPanel UserHeaderTable;
        private System.Windows.Forms.Label NotesLabel;
        private System.Windows.Forms.Label DeviceLabel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label ActivityLabel;
        private System.Windows.Forms.Panel UserListPanel;
        private System.Windows.Forms.TableLayoutPanel UserList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel InvoicePanel;
        private System.Windows.Forms.TableLayoutPanel InvoiceTable;
        private System.Windows.Forms.Label InvoiceHeaderLine;
        private System.Windows.Forms.TableLayoutPanel InvoiceHeaderTable;
        private System.Windows.Forms.Label UserHeader;
        private System.Windows.Forms.Label TimeHeader;
        private System.Windows.Forms.Label InvoiceNumHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label ActivityHeader;
        private System.Windows.Forms.Panel InvoiceListPanel;
        private System.Windows.Forms.TableLayoutPanel InvoiceList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button EnterButton;
        private System.Windows.Forms.Button NewUserButton;
    }
}