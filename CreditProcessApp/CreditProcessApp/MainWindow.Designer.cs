namespace CreditProcessApp
{
    partial class MainWindow
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
            this.TopPanel = new System.Windows.Forms.Panel();
            this.TopTable = new System.Windows.Forms.TableLayoutPanel();
            this.CompleteLabel = new System.Windows.Forms.Label();
            this.ProcessLabel = new System.Windows.Forms.Label();
            this.ProcessPanel = new System.Windows.Forms.Panel();
            this.ProcessTable = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessHeaderLine = new System.Windows.Forms.Label();
            this.ProcessHeaderTable = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessDateHeader = new System.Windows.Forms.Label();
            this.ProcessCusCodeHeader = new System.Windows.Forms.Label();
            this.ProcessInvNumHeader = new System.Windows.Forms.Label();
            this.ProcessInvoicePanel = new System.Windows.Forms.Panel();
            this.ProcessInvoiceList = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.BottomTable = new System.Windows.Forms.TableLayoutPanel();
            this.BottomButtonTable = new System.Windows.Forms.TableLayoutPanel();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.NotesTable = new System.Windows.Forms.TableLayoutPanel();
            this.NotesText = new System.Windows.Forms.TextBox();
            this.NotesLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessCheckLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ChargedByLabel = new System.Windows.Forms.Label();
            this.ChargedData = new System.Windows.Forms.Label();
            this.ChargeTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            this.TopTable.SuspendLayout();
            this.ProcessPanel.SuspendLayout();
            this.ProcessTable.SuspendLayout();
            this.ProcessHeaderTable.SuspendLayout();
            this.ProcessInvoicePanel.SuspendLayout();
            this.ProcessInvoiceList.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.BottomTable.SuspendLayout();
            this.BottomButtonTable.SuspendLayout();
            this.NotesTable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.TopTable);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(873, 415);
            this.TopPanel.TabIndex = 0;
            // 
            // TopTable
            // 
            this.TopTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.TopTable.ColumnCount = 2;
            this.TopTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopTable.Controls.Add(this.CompleteLabel, 1, 0);
            this.TopTable.Controls.Add(this.ProcessLabel, 0, 0);
            this.TopTable.Controls.Add(this.ProcessPanel, 0, 1);
            this.TopTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopTable.Location = new System.Drawing.Point(0, 0);
            this.TopTable.Name = "TopTable";
            this.TopTable.RowCount = 2;
            this.TopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.38554F));
            this.TopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.61446F));
            this.TopTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TopTable.Size = new System.Drawing.Size(873, 415);
            this.TopTable.TabIndex = 0;
            // 
            // CompleteLabel
            // 
            this.CompleteLabel.AutoSize = true;
            this.CompleteLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompleteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompleteLabel.Location = new System.Drawing.Point(437, 1);
            this.CompleteLabel.Margin = new System.Windows.Forms.Padding(0);
            this.CompleteLabel.Name = "CompleteLabel";
            this.CompleteLabel.Size = new System.Drawing.Size(435, 67);
            this.CompleteLabel.TabIndex = 1;
            this.CompleteLabel.Text = "Complete";
            this.CompleteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessLabel
            // 
            this.ProcessLabel.AutoSize = true;
            this.ProcessLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessLabel.Location = new System.Drawing.Point(1, 1);
            this.ProcessLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessLabel.Name = "ProcessLabel";
            this.ProcessLabel.Size = new System.Drawing.Size(435, 67);
            this.ProcessLabel.TabIndex = 0;
            this.ProcessLabel.Text = "Process";
            this.ProcessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessPanel
            // 
            this.ProcessPanel.AutoScroll = true;
            this.ProcessPanel.Controls.Add(this.ProcessTable);
            this.ProcessPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessPanel.Location = new System.Drawing.Point(4, 72);
            this.ProcessPanel.Name = "ProcessPanel";
            this.ProcessPanel.Size = new System.Drawing.Size(429, 339);
            this.ProcessPanel.TabIndex = 2;
            // 
            // ProcessTable
            // 
            this.ProcessTable.ColumnCount = 1;
            this.ProcessTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ProcessTable.Controls.Add(this.ProcessHeaderLine, 0, 1);
            this.ProcessTable.Controls.Add(this.ProcessHeaderTable, 0, 0);
            this.ProcessTable.Controls.Add(this.ProcessInvoicePanel, 0, 2);
            this.ProcessTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessTable.Location = new System.Drawing.Point(0, 0);
            this.ProcessTable.Name = "ProcessTable";
            this.ProcessTable.RowCount = 3;
            this.ProcessTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ProcessTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.ProcessTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ProcessTable.Size = new System.Drawing.Size(429, 339);
            this.ProcessTable.TabIndex = 6;
            // 
            // ProcessHeaderLine
            // 
            this.ProcessHeaderLine.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ProcessHeaderLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProcessHeaderLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProcessHeaderLine.Location = new System.Drawing.Point(0, 37);
            this.ProcessHeaderLine.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessHeaderLine.Name = "ProcessHeaderLine";
            this.ProcessHeaderLine.Size = new System.Drawing.Size(429, 3);
            this.ProcessHeaderLine.TabIndex = 16;
            // 
            // ProcessHeaderTable
            // 
            this.ProcessHeaderTable.ColumnCount = 3;
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ProcessHeaderTable.Controls.Add(this.ProcessDateHeader, 2, 0);
            this.ProcessHeaderTable.Controls.Add(this.ProcessCusCodeHeader, 1, 0);
            this.ProcessHeaderTable.Controls.Add(this.ProcessInvNumHeader, 0, 0);
            this.ProcessHeaderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessHeaderTable.Location = new System.Drawing.Point(0, 0);
            this.ProcessHeaderTable.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessHeaderTable.Name = "ProcessHeaderTable";
            this.ProcessHeaderTable.RowCount = 1;
            this.ProcessHeaderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ProcessHeaderTable.Size = new System.Drawing.Size(429, 37);
            this.ProcessHeaderTable.TabIndex = 3;
            // 
            // ProcessDateHeader
            // 
            this.ProcessDateHeader.AutoSize = true;
            this.ProcessDateHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessDateHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessDateHeader.Location = new System.Drawing.Point(286, 0);
            this.ProcessDateHeader.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessDateHeader.Name = "ProcessDateHeader";
            this.ProcessDateHeader.Size = new System.Drawing.Size(143, 37);
            this.ProcessDateHeader.TabIndex = 3;
            this.ProcessDateHeader.Text = "Date";
            this.ProcessDateHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessCusCodeHeader
            // 
            this.ProcessCusCodeHeader.AutoSize = true;
            this.ProcessCusCodeHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessCusCodeHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessCusCodeHeader.Location = new System.Drawing.Point(143, 0);
            this.ProcessCusCodeHeader.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessCusCodeHeader.Name = "ProcessCusCodeHeader";
            this.ProcessCusCodeHeader.Size = new System.Drawing.Size(143, 37);
            this.ProcessCusCodeHeader.TabIndex = 2;
            this.ProcessCusCodeHeader.Text = "Customer Code";
            this.ProcessCusCodeHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessInvNumHeader
            // 
            this.ProcessInvNumHeader.AutoSize = true;
            this.ProcessInvNumHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessInvNumHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessInvNumHeader.Location = new System.Drawing.Point(0, 0);
            this.ProcessInvNumHeader.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessInvNumHeader.Name = "ProcessInvNumHeader";
            this.ProcessInvNumHeader.Size = new System.Drawing.Size(143, 37);
            this.ProcessInvNumHeader.TabIndex = 1;
            this.ProcessInvNumHeader.Text = "Invoice Number";
            this.ProcessInvNumHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessInvoicePanel
            // 
            this.ProcessInvoicePanel.AutoScroll = true;
            this.ProcessInvoicePanel.Controls.Add(this.ProcessInvoiceList);
            this.ProcessInvoicePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessInvoicePanel.Location = new System.Drawing.Point(0, 42);
            this.ProcessInvoicePanel.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessInvoicePanel.Name = "ProcessInvoicePanel";
            this.ProcessInvoicePanel.Size = new System.Drawing.Size(429, 297);
            this.ProcessInvoicePanel.TabIndex = 17;
            // 
            // ProcessInvoiceList
            // 
            this.ProcessInvoiceList.AutoSize = true;
            this.ProcessInvoiceList.ColumnCount = 3;
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.ProcessInvoiceList.Controls.Add(this.label6, 2, 0);
            this.ProcessInvoiceList.Controls.Add(this.label7, 1, 0);
            this.ProcessInvoiceList.Controls.Add(this.label8, 0, 0);
            this.ProcessInvoiceList.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProcessInvoiceList.Location = new System.Drawing.Point(0, 0);
            this.ProcessInvoiceList.Name = "ProcessInvoiceList";
            this.ProcessInvoiceList.RowCount = 1;
            this.ProcessInvoiceList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ProcessInvoiceList.Size = new System.Drawing.Size(429, 20);
            this.ProcessInvoiceList.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(285, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 20);
            this.label6.TabIndex = 3;
            this.label6.Text = "08/20/2019";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(142, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "AUTOAIROL";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "1344925";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.BottomTable);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomPanel.Location = new System.Drawing.Point(0, 415);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(873, 198);
            this.BottomPanel.TabIndex = 1;
            // 
            // BottomTable
            // 
            this.BottomTable.ColumnCount = 3;
            this.BottomTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.94273F));
            this.BottomTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.57503F));
            this.BottomTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.3677F));
            this.BottomTable.Controls.Add(this.BottomButtonTable, 2, 0);
            this.BottomTable.Controls.Add(this.NotesTable, 1, 0);
            this.BottomTable.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.BottomTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomTable.Location = new System.Drawing.Point(0, 0);
            this.BottomTable.Name = "BottomTable";
            this.BottomTable.RowCount = 1;
            this.BottomTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.BottomTable.Size = new System.Drawing.Size(873, 198);
            this.BottomTable.TabIndex = 0;
            // 
            // BottomButtonTable
            // 
            this.BottomButtonTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.BottomButtonTable.ColumnCount = 1;
            this.BottomButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BottomButtonTable.Controls.Add(this.ExitButton, 0, 1);
            this.BottomButtonTable.Controls.Add(this.ProcessButton, 0, 0);
            this.BottomButtonTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomButtonTable.Location = new System.Drawing.Point(668, 0);
            this.BottomButtonTable.Margin = new System.Windows.Forms.Padding(0);
            this.BottomButtonTable.Name = "BottomButtonTable";
            this.BottomButtonTable.RowCount = 2;
            this.BottomButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BottomButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BottomButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.BottomButtonTable.Size = new System.Drawing.Size(205, 198);
            this.BottomButtonTable.TabIndex = 1;
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ExitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExitButton.FlatAppearance.BorderSize = 0;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.Location = new System.Drawing.Point(1, 99);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(0);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(203, 98);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "EXIT";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ProcessButton
            // 
            this.ProcessButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ProcessButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessButton.FlatAppearance.BorderSize = 0;
            this.ProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessButton.Location = new System.Drawing.Point(1, 1);
            this.ProcessButton.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(203, 97);
            this.ProcessButton.TabIndex = 0;
            this.ProcessButton.Text = "Process Invoice";
            this.ProcessButton.UseVisualStyleBackColor = false;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // NotesTable
            // 
            this.NotesTable.ColumnCount = 1;
            this.NotesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.NotesTable.Controls.Add(this.NotesText, 0, 1);
            this.NotesTable.Controls.Add(this.NotesLabel, 0, 0);
            this.NotesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotesTable.Location = new System.Drawing.Point(441, 5);
            this.NotesTable.Margin = new System.Windows.Forms.Padding(5);
            this.NotesTable.Name = "NotesTable";
            this.NotesTable.RowCount = 2;
            this.NotesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.1875F));
            this.NotesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.8125F));
            this.NotesTable.Size = new System.Drawing.Size(222, 188);
            this.NotesTable.TabIndex = 2;
            // 
            // NotesText
            // 
            this.NotesText.BackColor = System.Drawing.SystemColors.Control;
            this.NotesText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NotesText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotesText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesText.Location = new System.Drawing.Point(0, 32);
            this.NotesText.Margin = new System.Windows.Forms.Padding(0);
            this.NotesText.Multiline = true;
            this.NotesText.Name = "NotesText";
            this.NotesText.Size = new System.Drawing.Size(222, 156);
            this.NotesText.TabIndex = 3;
            this.NotesText.Text = "\r\n";
            // 
            // NotesLabel
            // 
            this.NotesLabel.AutoSize = true;
            this.NotesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NotesLabel.Location = new System.Drawing.Point(0, 0);
            this.NotesLabel.Margin = new System.Windows.Forms.Padding(0);
            this.NotesLabel.Name = "NotesLabel";
            this.NotesLabel.Size = new System.Drawing.Size(222, 32);
            this.NotesLabel.TabIndex = 1;
            this.NotesLabel.Text = "Notes";
            this.NotesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.91667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.08334F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(430, 192);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.16038F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.32076F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.64151F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.64151F));
            this.tableLayoutPanel3.Controls.Add(this.ProcessCheckLabel, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label12, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.ChargedByLabel, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.ChargedData, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.ChargeTimeLabel, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label1, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 43);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33445F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33444F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33111F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(430, 149);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // ProcessCheckLabel
            // 
            this.ProcessCheckLabel.AutoSize = true;
            this.ProcessCheckLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessCheckLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessCheckLabel.Location = new System.Drawing.Point(235, 99);
            this.ProcessCheckLabel.Margin = new System.Windows.Forms.Padding(1);
            this.ProcessCheckLabel.Name = "ProcessCheckLabel";
            this.ProcessCheckLabel.Size = new System.Drawing.Size(95, 49);
            this.ProcessCheckLabel.TabIndex = 14;
            this.ProcessCheckLabel.Text = "Processed?";
            this.ProcessCheckLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(332, 1);
            this.label12.Margin = new System.Windows.Forms.Padding(1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 47);
            this.label12.TabIndex = 13;
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChargedByLabel
            // 
            this.ChargedByLabel.AutoSize = true;
            this.ChargedByLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChargedByLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChargedByLabel.Location = new System.Drawing.Point(235, 1);
            this.ChargedByLabel.Margin = new System.Windows.Forms.Padding(1);
            this.ChargedByLabel.Name = "ChargedByLabel";
            this.ChargedByLabel.Size = new System.Drawing.Size(95, 47);
            this.ChargedByLabel.TabIndex = 12;
            this.ChargedByLabel.Text = "Charged By:";
            this.ChargedByLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChargedData
            // 
            this.ChargedData.AutoSize = true;
            this.ChargedData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChargedData.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChargedData.Location = new System.Drawing.Point(79, 99);
            this.ChargedData.Margin = new System.Windows.Forms.Padding(1);
            this.ChargedData.Name = "ChargedData";
            this.ChargedData.Size = new System.Drawing.Size(154, 49);
            this.ChargedData.TabIndex = 11;
            this.ChargedData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChargeTimeLabel
            // 
            this.ChargeTimeLabel.AutoSize = true;
            this.ChargeTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChargeTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChargeTimeLabel.Location = new System.Drawing.Point(1, 99);
            this.ChargeTimeLabel.Margin = new System.Windows.Forms.Padding(1);
            this.ChargeTimeLabel.Name = "ChargeTimeLabel";
            this.ChargeTimeLabel.Size = new System.Drawing.Size(76, 49);
            this.ChargeTimeLabel.TabIndex = 10;
            this.ChargeTimeLabel.Text = "Charged:";
            this.ChargeTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(332, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 47);
            this.label1.TabIndex = 9;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(79, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 47);
            this.label2.TabIndex = 8;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(79, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 47);
            this.label3.TabIndex = 7;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(235, 50);
            this.label5.Margin = new System.Windows.Forms.Padding(1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 47);
            this.label5.TabIndex = 5;
            this.label5.Text = "Total:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1, 1);
            this.label9.Margin = new System.Windows.Forms.Padding(1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 47);
            this.label9.TabIndex = 4;
            this.label9.Text = "Account:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1, 50);
            this.label10.Margin = new System.Windows.Forms.Padding(1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 47);
            this.label10.TabIndex = 3;
            this.label10.Text = "Date:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(430, 43);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(216, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 41);
            this.label4.TabIndex = 7;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1, 1);
            this.label11.Margin = new System.Windows.Forms.Padding(1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(213, 41);
            this.label11.TabIndex = 3;
            this.label11.Text = "Invoice Number:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 613);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.TopPanel.ResumeLayout(false);
            this.TopTable.ResumeLayout(false);
            this.TopTable.PerformLayout();
            this.ProcessPanel.ResumeLayout(false);
            this.ProcessTable.ResumeLayout(false);
            this.ProcessHeaderTable.ResumeLayout(false);
            this.ProcessHeaderTable.PerformLayout();
            this.ProcessInvoicePanel.ResumeLayout(false);
            this.ProcessInvoicePanel.PerformLayout();
            this.ProcessInvoiceList.ResumeLayout(false);
            this.ProcessInvoiceList.PerformLayout();
            this.BottomPanel.ResumeLayout(false);
            this.BottomTable.ResumeLayout(false);
            this.BottomButtonTable.ResumeLayout(false);
            this.NotesTable.ResumeLayout(false);
            this.NotesTable.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.TableLayoutPanel TopTable;
        private System.Windows.Forms.Label CompleteLabel;
        private System.Windows.Forms.Label ProcessLabel;
        private System.Windows.Forms.Panel ProcessPanel;
        private System.Windows.Forms.TableLayoutPanel ProcessTable;
        private System.Windows.Forms.Label ProcessHeaderLine;
        private System.Windows.Forms.TableLayoutPanel ProcessHeaderTable;
        private System.Windows.Forms.Label ProcessDateHeader;
        private System.Windows.Forms.Label ProcessCusCodeHeader;
        private System.Windows.Forms.Label ProcessInvNumHeader;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Panel ProcessInvoicePanel;
        private System.Windows.Forms.TableLayoutPanel ProcessInvoiceList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel BottomTable;
        private System.Windows.Forms.TableLayoutPanel BottomButtonTable;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.TableLayoutPanel NotesTable;
        private System.Windows.Forms.TextBox NotesText;
        private System.Windows.Forms.Label NotesLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label ProcessCheckLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label ChargedByLabel;
        private System.Windows.Forms.Label ChargedData;
        private System.Windows.Forms.Label ChargeTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
    }
}

