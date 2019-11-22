﻿namespace CreditProcessApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.TopTable = new System.Windows.Forms.TableLayoutPanel();
            this.CompletePanel = new System.Windows.Forms.Panel();
            this.CompleteTable = new System.Windows.Forms.TableLayoutPanel();
            this.CompleteHeaderLine = new System.Windows.Forms.Label();
            this.CompleteHeaderTable = new System.Windows.Forms.TableLayoutPanel();
            this.CompleteDeliveryHeader = new System.Windows.Forms.Label();
            this.CompleteInvoiceHeader = new System.Windows.Forms.Label();
            this.CompleteInvoicePanel = new System.Windows.Forms.Panel();
            this.CompleteInvoiceList = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ProcessPanel = new System.Windows.Forms.Panel();
            this.ProcessTable = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessHeaderLine = new System.Windows.Forms.Label();
            this.ProcessHeaderTable = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessDateHeader = new System.Windows.Forms.Label();
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
            this.DataTable = new System.Windows.Forms.TableLayoutPanel();
            this.InvoiceDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessedData = new System.Windows.Forms.Label();
            this.ProcessCheckLabel = new System.Windows.Forms.Label();
            this.ChargedByData = new System.Windows.Forms.Label();
            this.ChargedByLabel = new System.Windows.Forms.Label();
            this.ChargedData = new System.Windows.Forms.Label();
            this.ChargeTimeLabel = new System.Windows.Forms.Label();
            this.TotalData = new System.Windows.Forms.Label();
            this.DateData = new System.Windows.Forms.Label();
            this.AccountData = new System.Windows.Forms.Label();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.AccountLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.InvoiceNumTable = new System.Windows.Forms.TableLayoutPanel();
            this.InvoiceData = new System.Windows.Forms.Label();
            this.InvoiceDataLabel = new System.Windows.Forms.Label();
            this.DeliverySelectionTable = new System.Windows.Forms.TableLayoutPanel();
            this.DeliveryMethodBox = new System.Windows.Forms.ComboBox();
            this.DeliveryMethodLabel = new System.Windows.Forms.Label();
            this.CompleteTitleTable = new System.Windows.Forms.TableLayoutPanel();
            this.CompleteLabel = new System.Windows.Forms.Label();
            this.TopButtonTable = new System.Windows.Forms.TableLayoutPanel();
            this.CalendarButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ProcessLabel = new System.Windows.Forms.Label();
            this.CompleteDateHeader = new System.Windows.Forms.Label();
            this.CalendarPanel = new System.Windows.Forms.Panel();
            this.Calendar = new System.Windows.Forms.MonthCalendar();
            this.TopPanel.SuspendLayout();
            this.TopTable.SuspendLayout();
            this.CompletePanel.SuspendLayout();
            this.CompleteTable.SuspendLayout();
            this.CompleteHeaderTable.SuspendLayout();
            this.CompleteInvoicePanel.SuspendLayout();
            this.CompleteInvoiceList.SuspendLayout();
            this.ProcessPanel.SuspendLayout();
            this.ProcessTable.SuspendLayout();
            this.ProcessHeaderTable.SuspendLayout();
            this.ProcessInvoicePanel.SuspendLayout();
            this.ProcessInvoiceList.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.BottomTable.SuspendLayout();
            this.BottomButtonTable.SuspendLayout();
            this.NotesTable.SuspendLayout();
            this.DataTable.SuspendLayout();
            this.InvoiceDataTable.SuspendLayout();
            this.InvoiceNumTable.SuspendLayout();
            this.DeliverySelectionTable.SuspendLayout();
            this.CompleteTitleTable.SuspendLayout();
            this.TopButtonTable.SuspendLayout();
            this.CalendarPanel.SuspendLayout();
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
            this.TopTable.Controls.Add(this.ProcessLabel, 0, 0);
            this.TopTable.Controls.Add(this.CompletePanel, 1, 1);
            this.TopTable.Controls.Add(this.ProcessPanel, 0, 1);
            this.TopTable.Controls.Add(this.CompleteTitleTable, 1, 0);
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
            // CompletePanel
            // 
            this.CompletePanel.AutoScroll = true;
            this.CompletePanel.Controls.Add(this.CompleteTable);
            this.CompletePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompletePanel.Location = new System.Drawing.Point(440, 72);
            this.CompletePanel.Name = "CompletePanel";
            this.CompletePanel.Size = new System.Drawing.Size(429, 339);
            this.CompletePanel.TabIndex = 3;
            // 
            // CompleteTable
            // 
            this.CompleteTable.ColumnCount = 1;
            this.CompleteTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CompleteTable.Controls.Add(this.CompleteHeaderLine, 0, 1);
            this.CompleteTable.Controls.Add(this.CompleteHeaderTable, 0, 0);
            this.CompleteTable.Controls.Add(this.CompleteInvoicePanel, 0, 2);
            this.CompleteTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompleteTable.Location = new System.Drawing.Point(0, 0);
            this.CompleteTable.Name = "CompleteTable";
            this.CompleteTable.RowCount = 3;
            this.CompleteTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CompleteTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.CompleteTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CompleteTable.Size = new System.Drawing.Size(429, 339);
            this.CompleteTable.TabIndex = 6;
            // 
            // CompleteHeaderLine
            // 
            this.CompleteHeaderLine.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CompleteHeaderLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CompleteHeaderLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.CompleteHeaderLine.Location = new System.Drawing.Point(0, 37);
            this.CompleteHeaderLine.Margin = new System.Windows.Forms.Padding(0);
            this.CompleteHeaderLine.Name = "CompleteHeaderLine";
            this.CompleteHeaderLine.Size = new System.Drawing.Size(429, 3);
            this.CompleteHeaderLine.TabIndex = 16;
            // 
            // CompleteHeaderTable
            // 
            this.CompleteHeaderTable.ColumnCount = 3;
            this.CompleteHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.CompleteHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.CompleteHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.CompleteHeaderTable.Controls.Add(this.CompleteDateHeader, 2, 0);
            this.CompleteHeaderTable.Controls.Add(this.CompleteDeliveryHeader, 1, 0);
            this.CompleteHeaderTable.Controls.Add(this.CompleteInvoiceHeader, 0, 0);
            this.CompleteHeaderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompleteHeaderTable.Location = new System.Drawing.Point(0, 0);
            this.CompleteHeaderTable.Margin = new System.Windows.Forms.Padding(0);
            this.CompleteHeaderTable.Name = "CompleteHeaderTable";
            this.CompleteHeaderTable.RowCount = 1;
            this.CompleteHeaderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CompleteHeaderTable.Size = new System.Drawing.Size(429, 37);
            this.CompleteHeaderTable.TabIndex = 3;
            // 
            // CompleteDeliveryHeader
            // 
            this.CompleteDeliveryHeader.AutoSize = true;
            this.CompleteDeliveryHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompleteDeliveryHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompleteDeliveryHeader.Location = new System.Drawing.Point(143, 0);
            this.CompleteDeliveryHeader.Margin = new System.Windows.Forms.Padding(0);
            this.CompleteDeliveryHeader.Name = "CompleteDeliveryHeader";
            this.CompleteDeliveryHeader.Size = new System.Drawing.Size(143, 37);
            this.CompleteDeliveryHeader.TabIndex = 2;
            this.CompleteDeliveryHeader.Text = "Delivery Method";
            this.CompleteDeliveryHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CompleteInvoiceHeader
            // 
            this.CompleteInvoiceHeader.AutoSize = true;
            this.CompleteInvoiceHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompleteInvoiceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompleteInvoiceHeader.Location = new System.Drawing.Point(0, 0);
            this.CompleteInvoiceHeader.Margin = new System.Windows.Forms.Padding(0);
            this.CompleteInvoiceHeader.Name = "CompleteInvoiceHeader";
            this.CompleteInvoiceHeader.Size = new System.Drawing.Size(143, 37);
            this.CompleteInvoiceHeader.TabIndex = 1;
            this.CompleteInvoiceHeader.Text = "Invoice Number";
            this.CompleteInvoiceHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CompleteInvoicePanel
            // 
            this.CompleteInvoicePanel.AutoScroll = true;
            this.CompleteInvoicePanel.Controls.Add(this.CompleteInvoiceList);
            this.CompleteInvoicePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompleteInvoicePanel.Location = new System.Drawing.Point(0, 42);
            this.CompleteInvoicePanel.Margin = new System.Windows.Forms.Padding(0);
            this.CompleteInvoicePanel.Name = "CompleteInvoicePanel";
            this.CompleteInvoicePanel.Size = new System.Drawing.Size(429, 297);
            this.CompleteInvoicePanel.TabIndex = 17;
            // 
            // CompleteInvoiceList
            // 
            this.CompleteInvoiceList.AutoSize = true;
            this.CompleteInvoiceList.ColumnCount = 3;
            this.CompleteInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.CompleteInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.CompleteInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.CompleteInvoiceList.Controls.Add(this.label11, 2, 0);
            this.CompleteInvoiceList.Controls.Add(this.label12, 1, 0);
            this.CompleteInvoiceList.Controls.Add(this.label13, 0, 0);
            this.CompleteInvoiceList.Dock = System.Windows.Forms.DockStyle.Top;
            this.CompleteInvoiceList.Location = new System.Drawing.Point(0, 0);
            this.CompleteInvoiceList.Name = "CompleteInvoiceList";
            this.CompleteInvoiceList.RowCount = 1;
            this.CompleteInvoiceList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.CompleteInvoiceList.Size = new System.Drawing.Size(429, 20);
            this.CompleteInvoiceList.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Control;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(285, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 20);
            this.label11.TabIndex = 3;
            this.label11.Text = "08/20/2019";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(142, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "AUTOAIROL";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 20);
            this.label13.TabIndex = 1;
            this.label13.Text = "1344925";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.82984F));
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.06993F));
            this.ProcessHeaderTable.Controls.Add(this.ProcessDateHeader, 2, 0);
            this.ProcessHeaderTable.Controls.Add(this.ProcessInvNumHeader, 0, 0);
            this.ProcessHeaderTable.Controls.Add(this.DeliverySelectionTable, 1, 0);
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
            this.ProcessDateHeader.Location = new System.Drawing.Point(299, 0);
            this.ProcessDateHeader.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessDateHeader.Name = "ProcessDateHeader";
            this.ProcessDateHeader.Size = new System.Drawing.Size(130, 37);
            this.ProcessDateHeader.TabIndex = 3;
            this.ProcessDateHeader.Text = "Date";
            this.ProcessDateHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessInvNumHeader
            // 
            this.ProcessInvNumHeader.AutoSize = true;
            this.ProcessInvNumHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessInvNumHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessInvNumHeader.Location = new System.Drawing.Point(0, 0);
            this.ProcessInvNumHeader.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessInvNumHeader.Name = "ProcessInvNumHeader";
            this.ProcessInvNumHeader.Size = new System.Drawing.Size(142, 37);
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
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.59674F));
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.30303F));
            this.ProcessInvoiceList.Controls.Add(this.label6, 2, 0);
            this.ProcessInvoiceList.Controls.Add(this.label7, 1, 0);
            this.ProcessInvoiceList.Controls.Add(this.label8, 0, 0);
            this.ProcessInvoiceList.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProcessInvoiceList.Location = new System.Drawing.Point(0, 0);
            this.ProcessInvoiceList.Name = "ProcessInvoiceList";
            this.ProcessInvoiceList.RowCount = 1;
            this.ProcessInvoiceList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ProcessInvoiceList.Size = new System.Drawing.Size(429, 22);
            this.ProcessInvoiceList.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(299, 1);
            this.label6.Margin = new System.Windows.Forms.Padding(1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
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
            this.label7.Location = new System.Drawing.Point(143, 1);
            this.label7.Margin = new System.Windows.Forms.Padding(1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "AUTOAIROL";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1, 1);
            this.label8.Margin = new System.Windows.Forms.Padding(1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 20);
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
            this.BottomTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.BottomTable.ColumnCount = 3;
            this.BottomTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BottomTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.49083F));
            this.BottomTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.3677F));
            this.BottomTable.Controls.Add(this.BottomButtonTable, 2, 0);
            this.BottomTable.Controls.Add(this.NotesTable, 1, 0);
            this.BottomTable.Controls.Add(this.DataTable, 0, 0);
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
            this.BottomButtonTable.Location = new System.Drawing.Point(668, 1);
            this.BottomButtonTable.Margin = new System.Windows.Forms.Padding(0);
            this.BottomButtonTable.Name = "BottomButtonTable";
            this.BottomButtonTable.RowCount = 2;
            this.BottomButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BottomButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BottomButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.BottomButtonTable.Size = new System.Drawing.Size(204, 196);
            this.BottomButtonTable.TabIndex = 1;
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ExitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExitButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ExitButton.FlatAppearance.BorderSize = 0;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.Location = new System.Drawing.Point(1, 98);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(0);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(202, 97);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.TabStop = false;
            this.ExitButton.Text = "EXIT";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ProcessButton
            // 
            this.ProcessButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ProcessButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessButton.Enabled = false;
            this.ProcessButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ProcessButton.FlatAppearance.BorderSize = 0;
            this.ProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessButton.Location = new System.Drawing.Point(1, 1);
            this.ProcessButton.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(202, 96);
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
            this.NotesTable.Location = new System.Drawing.Point(440, 4);
            this.NotesTable.Name = "NotesTable";
            this.NotesTable.RowCount = 2;
            this.NotesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.1875F));
            this.NotesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.8125F));
            this.NotesTable.Size = new System.Drawing.Size(224, 190);
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
            this.NotesText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NotesText.Size = new System.Drawing.Size(224, 158);
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
            this.NotesLabel.Size = new System.Drawing.Size(224, 32);
            this.NotesLabel.TabIndex = 1;
            this.NotesLabel.Text = "Notes";
            this.NotesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DataTable
            // 
            this.DataTable.ColumnCount = 1;
            this.DataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DataTable.Controls.Add(this.InvoiceDataTable, 0, 1);
            this.DataTable.Controls.Add(this.InvoiceNumTable, 0, 0);
            this.DataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataTable.Location = new System.Drawing.Point(4, 4);
            this.DataTable.Name = "DataTable";
            this.DataTable.RowCount = 2;
            this.DataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.91667F));
            this.DataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.08334F));
            this.DataTable.Size = new System.Drawing.Size(429, 190);
            this.DataTable.TabIndex = 3;
            // 
            // InvoiceDataTable
            // 
            this.InvoiceDataTable.ColumnCount = 4;
            this.InvoiceDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.71562F));
            this.InvoiceDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.16783F));
            this.InvoiceDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.InvoiceDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.64151F));
            this.InvoiceDataTable.Controls.Add(this.ProcessedData, 3, 2);
            this.InvoiceDataTable.Controls.Add(this.ProcessCheckLabel, 2, 2);
            this.InvoiceDataTable.Controls.Add(this.ChargedByData, 3, 0);
            this.InvoiceDataTable.Controls.Add(this.ChargedByLabel, 2, 0);
            this.InvoiceDataTable.Controls.Add(this.ChargedData, 1, 2);
            this.InvoiceDataTable.Controls.Add(this.ChargeTimeLabel, 0, 2);
            this.InvoiceDataTable.Controls.Add(this.TotalData, 3, 1);
            this.InvoiceDataTable.Controls.Add(this.DateData, 1, 1);
            this.InvoiceDataTable.Controls.Add(this.AccountData, 1, 0);
            this.InvoiceDataTable.Controls.Add(this.TotalLabel, 2, 1);
            this.InvoiceDataTable.Controls.Add(this.AccountLabel, 0, 0);
            this.InvoiceDataTable.Controls.Add(this.DateLabel, 0, 1);
            this.InvoiceDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceDataTable.Location = new System.Drawing.Point(0, 43);
            this.InvoiceDataTable.Margin = new System.Windows.Forms.Padding(0);
            this.InvoiceDataTable.Name = "InvoiceDataTable";
            this.InvoiceDataTable.RowCount = 3;
            this.InvoiceDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33445F));
            this.InvoiceDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33444F));
            this.InvoiceDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33111F));
            this.InvoiceDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InvoiceDataTable.Size = new System.Drawing.Size(429, 147);
            this.InvoiceDataTable.TabIndex = 5;
            // 
            // ProcessedData
            // 
            this.ProcessedData.AutoSize = true;
            this.ProcessedData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessedData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessedData.Location = new System.Drawing.Point(332, 99);
            this.ProcessedData.Margin = new System.Windows.Forms.Padding(1);
            this.ProcessedData.Name = "ProcessedData";
            this.ProcessedData.Size = new System.Drawing.Size(96, 47);
            this.ProcessedData.TabIndex = 15;
            this.ProcessedData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProcessCheckLabel
            // 
            this.ProcessCheckLabel.AutoSize = true;
            this.ProcessCheckLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessCheckLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessCheckLabel.Location = new System.Drawing.Point(215, 99);
            this.ProcessCheckLabel.Margin = new System.Windows.Forms.Padding(1);
            this.ProcessCheckLabel.Name = "ProcessCheckLabel";
            this.ProcessCheckLabel.Size = new System.Drawing.Size(115, 47);
            this.ProcessCheckLabel.TabIndex = 14;
            this.ProcessCheckLabel.Text = "Processed?";
            this.ProcessCheckLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChargedByData
            // 
            this.ChargedByData.AutoSize = true;
            this.ChargedByData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChargedByData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChargedByData.Location = new System.Drawing.Point(332, 1);
            this.ChargedByData.Margin = new System.Windows.Forms.Padding(1);
            this.ChargedByData.Name = "ChargedByData";
            this.ChargedByData.Size = new System.Drawing.Size(96, 47);
            this.ChargedByData.TabIndex = 13;
            this.ChargedByData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChargedByLabel
            // 
            this.ChargedByLabel.AutoSize = true;
            this.ChargedByLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChargedByLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChargedByLabel.Location = new System.Drawing.Point(215, 1);
            this.ChargedByLabel.Margin = new System.Windows.Forms.Padding(1);
            this.ChargedByLabel.Name = "ChargedByLabel";
            this.ChargedByLabel.Size = new System.Drawing.Size(115, 47);
            this.ChargedByLabel.TabIndex = 12;
            this.ChargedByLabel.Text = "Charged By:";
            this.ChargedByLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChargedData
            // 
            this.ChargedData.AutoSize = true;
            this.ChargedData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChargedData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChargedData.Location = new System.Drawing.Point(77, 99);
            this.ChargedData.Margin = new System.Windows.Forms.Padding(1);
            this.ChargedData.Name = "ChargedData";
            this.ChargedData.Size = new System.Drawing.Size(136, 47);
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
            this.ChargeTimeLabel.Size = new System.Drawing.Size(74, 47);
            this.ChargeTimeLabel.TabIndex = 10;
            this.ChargeTimeLabel.Text = "Charged:";
            this.ChargeTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TotalData
            // 
            this.TotalData.AutoSize = true;
            this.TotalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TotalData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalData.Location = new System.Drawing.Point(332, 50);
            this.TotalData.Margin = new System.Windows.Forms.Padding(1);
            this.TotalData.Name = "TotalData";
            this.TotalData.Size = new System.Drawing.Size(96, 47);
            this.TotalData.TabIndex = 9;
            this.TotalData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DateData
            // 
            this.DateData.AutoSize = true;
            this.DateData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateData.Location = new System.Drawing.Point(77, 50);
            this.DateData.Margin = new System.Windows.Forms.Padding(1);
            this.DateData.Name = "DateData";
            this.DateData.Size = new System.Drawing.Size(136, 47);
            this.DateData.TabIndex = 8;
            this.DateData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AccountData
            // 
            this.AccountData.AutoSize = true;
            this.AccountData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccountData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountData.Location = new System.Drawing.Point(77, 1);
            this.AccountData.Margin = new System.Windows.Forms.Padding(1);
            this.AccountData.Name = "AccountData";
            this.AccountData.Size = new System.Drawing.Size(136, 47);
            this.AccountData.TabIndex = 7;
            this.AccountData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TotalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalLabel.Location = new System.Drawing.Point(215, 50);
            this.TotalLabel.Margin = new System.Windows.Forms.Padding(1);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(115, 47);
            this.TotalLabel.TabIndex = 5;
            this.TotalLabel.Text = "Total:";
            this.TotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AccountLabel
            // 
            this.AccountLabel.AutoSize = true;
            this.AccountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountLabel.Location = new System.Drawing.Point(1, 1);
            this.AccountLabel.Margin = new System.Windows.Forms.Padding(1);
            this.AccountLabel.Name = "AccountLabel";
            this.AccountLabel.Size = new System.Drawing.Size(74, 47);
            this.AccountLabel.TabIndex = 4;
            this.AccountLabel.Text = "Account:";
            this.AccountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLabel.Location = new System.Drawing.Point(1, 50);
            this.DateLabel.Margin = new System.Windows.Forms.Padding(1);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(74, 47);
            this.DateLabel.TabIndex = 3;
            this.DateLabel.Text = "Date:";
            this.DateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InvoiceNumTable
            // 
            this.InvoiceNumTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.InvoiceNumTable.ColumnCount = 2;
            this.InvoiceNumTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.InvoiceNumTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.InvoiceNumTable.Controls.Add(this.InvoiceData, 0, 0);
            this.InvoiceNumTable.Controls.Add(this.InvoiceDataLabel, 0, 0);
            this.InvoiceNumTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceNumTable.Location = new System.Drawing.Point(0, 0);
            this.InvoiceNumTable.Margin = new System.Windows.Forms.Padding(0);
            this.InvoiceNumTable.Name = "InvoiceNumTable";
            this.InvoiceNumTable.RowCount = 1;
            this.InvoiceNumTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.InvoiceNumTable.Size = new System.Drawing.Size(429, 43);
            this.InvoiceNumTable.TabIndex = 6;
            // 
            // InvoiceData
            // 
            this.InvoiceData.AutoSize = true;
            this.InvoiceData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvoiceData.Location = new System.Drawing.Point(216, 2);
            this.InvoiceData.Margin = new System.Windows.Forms.Padding(1);
            this.InvoiceData.Name = "InvoiceData";
            this.InvoiceData.Size = new System.Drawing.Size(211, 39);
            this.InvoiceData.TabIndex = 7;
            this.InvoiceData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InvoiceDataLabel
            // 
            this.InvoiceDataLabel.AutoSize = true;
            this.InvoiceDataLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceDataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvoiceDataLabel.Location = new System.Drawing.Point(2, 2);
            this.InvoiceDataLabel.Margin = new System.Windows.Forms.Padding(1);
            this.InvoiceDataLabel.Name = "InvoiceDataLabel";
            this.InvoiceDataLabel.Size = new System.Drawing.Size(211, 39);
            this.InvoiceDataLabel.TabIndex = 3;
            this.InvoiceDataLabel.Text = "Invoice Number:";
            this.InvoiceDataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DeliverySelectionTable
            // 
            this.DeliverySelectionTable.ColumnCount = 2;
            this.DeliverySelectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DeliverySelectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DeliverySelectionTable.Controls.Add(this.DeliveryMethodLabel, 0, 0);
            this.DeliverySelectionTable.Controls.Add(this.DeliveryMethodBox, 1, 0);
            this.DeliverySelectionTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeliverySelectionTable.Location = new System.Drawing.Point(142, 0);
            this.DeliverySelectionTable.Margin = new System.Windows.Forms.Padding(0);
            this.DeliverySelectionTable.Name = "DeliverySelectionTable";
            this.DeliverySelectionTable.RowCount = 1;
            this.DeliverySelectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DeliverySelectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.DeliverySelectionTable.Size = new System.Drawing.Size(157, 37);
            this.DeliverySelectionTable.TabIndex = 4;
            // 
            // DeliveryMethodBox
            // 
            this.DeliveryMethodBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.DeliveryMethodBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeliveryMethodBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeliveryMethodBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeliveryMethodBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeliveryMethodBox.FormattingEnabled = true;
            this.DeliveryMethodBox.Items.AddRange(new object[] {
            "ALL",
            "RETAIL",
            "NON-R"});
            this.DeliveryMethodBox.Location = new System.Drawing.Point(81, 3);
            this.DeliveryMethodBox.Name = "DeliveryMethodBox";
            this.DeliveryMethodBox.Size = new System.Drawing.Size(73, 28);
            this.DeliveryMethodBox.TabIndex = 5;
            this.DeliveryMethodBox.SelectedIndexChanged += new System.EventHandler(this.DeliveryMethodBox_SelectedIndexChanged_1);
            // 
            // DeliveryMethodLabel
            // 
            this.DeliveryMethodLabel.AutoSize = true;
            this.DeliveryMethodLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeliveryMethodLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeliveryMethodLabel.Location = new System.Drawing.Point(0, 0);
            this.DeliveryMethodLabel.Margin = new System.Windows.Forms.Padding(0);
            this.DeliveryMethodLabel.Name = "DeliveryMethodLabel";
            this.DeliveryMethodLabel.Size = new System.Drawing.Size(78, 37);
            this.DeliveryMethodLabel.TabIndex = 6;
            this.DeliveryMethodLabel.Text = "Delivery";
            this.DeliveryMethodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CompleteTitleTable
            // 
            this.CompleteTitleTable.ColumnCount = 2;
            this.CompleteTitleTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.65517F));
            this.CompleteTitleTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.34483F));
            this.CompleteTitleTable.Controls.Add(this.CompleteLabel, 0, 0);
            this.CompleteTitleTable.Controls.Add(this.TopButtonTable, 1, 0);
            this.CompleteTitleTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompleteTitleTable.Location = new System.Drawing.Point(437, 1);
            this.CompleteTitleTable.Margin = new System.Windows.Forms.Padding(0);
            this.CompleteTitleTable.Name = "CompleteTitleTable";
            this.CompleteTitleTable.RowCount = 1;
            this.CompleteTitleTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CompleteTitleTable.Size = new System.Drawing.Size(435, 67);
            this.CompleteTitleTable.TabIndex = 4;
            // 
            // CompleteLabel
            // 
            this.CompleteLabel.AutoSize = true;
            this.CompleteLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompleteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompleteLabel.Location = new System.Drawing.Point(0, 0);
            this.CompleteLabel.Margin = new System.Windows.Forms.Padding(0);
            this.CompleteLabel.Name = "CompleteLabel";
            this.CompleteLabel.Size = new System.Drawing.Size(390, 67);
            this.CompleteLabel.TabIndex = 2;
            this.CompleteLabel.Text = "  Complete";
            this.CompleteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TopButtonTable
            // 
            this.TopButtonTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.TopButtonTable.ColumnCount = 1;
            this.TopButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonTable.Controls.Add(this.RefreshButton, 0, 0);
            this.TopButtonTable.Controls.Add(this.CalendarButton, 0, 1);
            this.TopButtonTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopButtonTable.Location = new System.Drawing.Point(390, 0);
            this.TopButtonTable.Margin = new System.Windows.Forms.Padding(0);
            this.TopButtonTable.Name = "TopButtonTable";
            this.TopButtonTable.RowCount = 2;
            this.TopButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TopButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TopButtonTable.Size = new System.Drawing.Size(45, 67);
            this.TopButtonTable.TabIndex = 3;
            // 
            // CalendarButton
            // 
            this.CalendarButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CalendarButton.BackgroundImage = global::CreditProcessApp.Properties.Resources.calendarimg;
            this.CalendarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CalendarButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalendarButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CalendarButton.FlatAppearance.BorderSize = 0;
            this.CalendarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CalendarButton.Location = new System.Drawing.Point(1, 34);
            this.CalendarButton.Margin = new System.Windows.Forms.Padding(0);
            this.CalendarButton.Name = "CalendarButton";
            this.CalendarButton.Size = new System.Drawing.Size(43, 32);
            this.CalendarButton.TabIndex = 6;
            this.CalendarButton.UseVisualStyleBackColor = false;
            this.CalendarButton.Click += new System.EventHandler(this.CalendarButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.RefreshButton.BackgroundImage = global::CreditProcessApp.Properties.Resources.Refresh_512;
            this.RefreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RefreshButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RefreshButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.RefreshButton.FlatAppearance.BorderSize = 0;
            this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Location = new System.Drawing.Point(1, 1);
            this.RefreshButton.Margin = new System.Windows.Forms.Padding(0);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(43, 32);
            this.RefreshButton.TabIndex = 7;
            this.RefreshButton.TabStop = false;
            this.RefreshButton.UseVisualStyleBackColor = false;
            this.RefreshButton.Click += new System.EventHandler(this.button1_Click);
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
            this.ProcessLabel.TabIndex = 5;
            this.ProcessLabel.Text = "Process";
            this.ProcessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CompleteDateHeader
            // 
            this.CompleteDateHeader.AutoSize = true;
            this.CompleteDateHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompleteDateHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompleteDateHeader.Location = new System.Drawing.Point(286, 0);
            this.CompleteDateHeader.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.CompleteDateHeader.Name = "CompleteDateHeader";
            this.CompleteDateHeader.Size = new System.Drawing.Size(138, 37);
            this.CompleteDateHeader.TabIndex = 5;
            this.CompleteDateHeader.Text = "Date";
            this.CompleteDateHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CalendarPanel
            // 
            this.CalendarPanel.AutoSize = true;
            this.CalendarPanel.Controls.Add(this.Calendar);
            this.CalendarPanel.Location = new System.Drawing.Point(642, 70);
            this.CalendarPanel.Name = "CalendarPanel";
            this.CalendarPanel.Size = new System.Drawing.Size(227, 163);
            this.CalendarPanel.TabIndex = 2;
            this.CalendarPanel.Visible = false;
            // 
            // Calendar
            // 
            this.Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Calendar.Location = new System.Drawing.Point(0, 0);
            this.Calendar.Name = "Calendar";
            this.Calendar.TabIndex = 3;
            this.Calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.Calendar_DateSelected);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 613);
            this.Controls.Add(this.CalendarPanel);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "CreditProcessApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.TopPanel.ResumeLayout(false);
            this.TopTable.ResumeLayout(false);
            this.TopTable.PerformLayout();
            this.CompletePanel.ResumeLayout(false);
            this.CompleteTable.ResumeLayout(false);
            this.CompleteHeaderTable.ResumeLayout(false);
            this.CompleteHeaderTable.PerformLayout();
            this.CompleteInvoicePanel.ResumeLayout(false);
            this.CompleteInvoicePanel.PerformLayout();
            this.CompleteInvoiceList.ResumeLayout(false);
            this.CompleteInvoiceList.PerformLayout();
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
            this.DataTable.ResumeLayout(false);
            this.InvoiceDataTable.ResumeLayout(false);
            this.InvoiceDataTable.PerformLayout();
            this.InvoiceNumTable.ResumeLayout(false);
            this.InvoiceNumTable.PerformLayout();
            this.DeliverySelectionTable.ResumeLayout(false);
            this.DeliverySelectionTable.PerformLayout();
            this.CompleteTitleTable.ResumeLayout(false);
            this.CompleteTitleTable.PerformLayout();
            this.TopButtonTable.ResumeLayout(false);
            this.CalendarPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.TableLayoutPanel TopTable;
        private System.Windows.Forms.Panel ProcessPanel;
        private System.Windows.Forms.TableLayoutPanel ProcessTable;
        private System.Windows.Forms.Label ProcessHeaderLine;
        private System.Windows.Forms.TableLayoutPanel ProcessHeaderTable;
        private System.Windows.Forms.Label ProcessDateHeader;
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
        private System.Windows.Forms.TableLayoutPanel DataTable;
        private System.Windows.Forms.TableLayoutPanel InvoiceDataTable;
        private System.Windows.Forms.Label ProcessCheckLabel;
        private System.Windows.Forms.Label ChargedByData;
        private System.Windows.Forms.Label ChargedByLabel;
        private System.Windows.Forms.Label ChargedData;
        private System.Windows.Forms.Label ChargeTimeLabel;
        private System.Windows.Forms.Label TotalData;
        private System.Windows.Forms.Label DateData;
        private System.Windows.Forms.Label AccountData;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.Label AccountLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.TableLayoutPanel InvoiceNumTable;
        private System.Windows.Forms.Label InvoiceData;
        private System.Windows.Forms.Label InvoiceDataLabel;
        private System.Windows.Forms.Panel CompletePanel;
        private System.Windows.Forms.TableLayoutPanel CompleteTable;
        private System.Windows.Forms.Label CompleteHeaderLine;
        private System.Windows.Forms.TableLayoutPanel CompleteHeaderTable;
        private System.Windows.Forms.Label CompleteDeliveryHeader;
        private System.Windows.Forms.Label CompleteInvoiceHeader;
        private System.Windows.Forms.Panel CompleteInvoicePanel;
        private System.Windows.Forms.TableLayoutPanel CompleteInvoiceList;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label ProcessedData;
        private System.Windows.Forms.TableLayoutPanel DeliverySelectionTable;
        private System.Windows.Forms.Label DeliveryMethodLabel;
        private System.Windows.Forms.ComboBox DeliveryMethodBox;
        private System.Windows.Forms.Label ProcessLabel;
        private System.Windows.Forms.Label CompleteDateHeader;
        private System.Windows.Forms.TableLayoutPanel CompleteTitleTable;
        private System.Windows.Forms.Label CompleteLabel;
        private System.Windows.Forms.TableLayoutPanel TopButtonTable;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button CalendarButton;
        private System.Windows.Forms.Panel CalendarPanel;
        private System.Windows.Forms.MonthCalendar Calendar;
    }
}

