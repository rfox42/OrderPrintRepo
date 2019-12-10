namespace OrderValidation
{
    partial class ValidationForm
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
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.RightTable = new System.Windows.Forms.TableLayoutPanel();
            this.EnterField = new System.Windows.Forms.RichTextBox();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.LeftTable = new System.Windows.Forms.TableLayoutPanel();
            this.InvoicePanel = new System.Windows.Forms.Panel();
            this.InvoiceTable = new System.Windows.Forms.TableLayoutPanel();
            this.InvoiceHeaderLine = new System.Windows.Forms.Label();
            this.InvoiceHeaderTable = new System.Windows.Forms.TableLayoutPanel();
            this.InvoiceListPanel = new System.Windows.Forms.Panel();
            this.InvoiceList = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.InvoiceNumHeader = new System.Windows.Forms.Label();
            this.DateHeader = new System.Windows.Forms.Label();
            this.ItemsPanel = new System.Windows.Forms.Panel();
            this.ItemsTable = new System.Windows.Forms.TableLayoutPanel();
            this.ItemsHeaderLine = new System.Windows.Forms.Label();
            this.ItemsHeaderTable = new System.Windows.Forms.TableLayoutPanel();
            this.ItemDescriptionHeader = new System.Windows.Forms.Label();
            this.PartNumHeader = new System.Windows.Forms.Label();
            this.ItemListPanel = new System.Windows.Forms.Panel();
            this.ItemList = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CustomPartHeader = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EnterButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.InfoPanel.SuspendLayout();
            this.RightTable.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            this.LeftTable.SuspendLayout();
            this.InvoicePanel.SuspendLayout();
            this.InvoiceTable.SuspendLayout();
            this.InvoiceHeaderTable.SuspendLayout();
            this.InvoiceListPanel.SuspendLayout();
            this.InvoiceList.SuspendLayout();
            this.ItemsPanel.SuspendLayout();
            this.ItemsTable.SuspendLayout();
            this.ItemsHeaderTable.SuspendLayout();
            this.ItemListPanel.SuspendLayout();
            this.ItemList.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfoPanel
            // 
            this.InfoPanel.Controls.Add(this.RightTable);
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.InfoPanel.Location = new System.Drawing.Point(561, 0);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(239, 525);
            this.InfoPanel.TabIndex = 0;
            // 
            // RightTable
            // 
            this.RightTable.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.RightTable.ColumnCount = 1;
            this.RightTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RightTable.Controls.Add(this.EnterField, 0, 0);
            this.RightTable.Controls.Add(this.EnterButton, 0, 1);
            this.RightTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightTable.Location = new System.Drawing.Point(0, 0);
            this.RightTable.Name = "RightTable";
            this.RightTable.RowCount = 3;
            this.RightTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.RightTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.RightTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RightTable.Size = new System.Drawing.Size(239, 525);
            this.RightTable.TabIndex = 0;
            // 
            // EnterField
            // 
            this.EnterField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.EnterField.Location = new System.Drawing.Point(10, 10);
            this.EnterField.Margin = new System.Windows.Forms.Padding(10);
            this.EnterField.Name = "EnterField";
            this.EnterField.Size = new System.Drawing.Size(219, 32);
            this.EnterField.TabIndex = 0;
            this.EnterField.Text = "";
            this.EnterField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EnterField_KeyPress);
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.LeftTable);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(561, 525);
            this.LeftPanel.TabIndex = 1;
            // 
            // LeftTable
            // 
            this.LeftTable.ColumnCount = 1;
            this.LeftTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LeftTable.Controls.Add(this.ItemsPanel, 0, 1);
            this.LeftTable.Controls.Add(this.InvoicePanel, 0, 0);
            this.LeftTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftTable.Location = new System.Drawing.Point(0, 0);
            this.LeftTable.Name = "LeftTable";
            this.LeftTable.RowCount = 2;
            this.LeftTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LeftTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LeftTable.Size = new System.Drawing.Size(561, 525);
            this.LeftTable.TabIndex = 0;
            // 
            // InvoicePanel
            // 
            this.InvoicePanel.AutoScroll = true;
            this.InvoicePanel.Controls.Add(this.InvoiceTable);
            this.InvoicePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoicePanel.Location = new System.Drawing.Point(3, 3);
            this.InvoicePanel.Name = "InvoicePanel";
            this.InvoicePanel.Size = new System.Drawing.Size(555, 256);
            this.InvoicePanel.TabIndex = 3;
            // 
            // InvoiceTable
            // 
            this.InvoiceTable.ColumnCount = 1;
            this.InvoiceTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InvoiceTable.Controls.Add(this.InvoiceHeaderLine, 0, 1);
            this.InvoiceTable.Controls.Add(this.InvoiceHeaderTable, 0, 0);
            this.InvoiceTable.Controls.Add(this.InvoiceListPanel, 0, 2);
            this.InvoiceTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceTable.Location = new System.Drawing.Point(0, 0);
            this.InvoiceTable.Name = "InvoiceTable";
            this.InvoiceTable.RowCount = 3;
            this.InvoiceTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InvoiceTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.InvoiceTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InvoiceTable.Size = new System.Drawing.Size(555, 256);
            this.InvoiceTable.TabIndex = 6;
            // 
            // InvoiceHeaderLine
            // 
            this.InvoiceHeaderLine.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.InvoiceHeaderLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InvoiceHeaderLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.InvoiceHeaderLine.Location = new System.Drawing.Point(0, 37);
            this.InvoiceHeaderLine.Margin = new System.Windows.Forms.Padding(0);
            this.InvoiceHeaderLine.Name = "InvoiceHeaderLine";
            this.InvoiceHeaderLine.Size = new System.Drawing.Size(555, 3);
            this.InvoiceHeaderLine.TabIndex = 16;
            // 
            // InvoiceHeaderTable
            // 
            this.InvoiceHeaderTable.ColumnCount = 2;
            this.InvoiceHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.7883F));
            this.InvoiceHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.2117F));
            this.InvoiceHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InvoiceHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InvoiceHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InvoiceHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InvoiceHeaderTable.Controls.Add(this.DateHeader, 1, 0);
            this.InvoiceHeaderTable.Controls.Add(this.InvoiceNumHeader, 0, 0);
            this.InvoiceHeaderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceHeaderTable.Location = new System.Drawing.Point(0, 0);
            this.InvoiceHeaderTable.Margin = new System.Windows.Forms.Padding(0);
            this.InvoiceHeaderTable.Name = "InvoiceHeaderTable";
            this.InvoiceHeaderTable.RowCount = 1;
            this.InvoiceHeaderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InvoiceHeaderTable.Size = new System.Drawing.Size(555, 37);
            this.InvoiceHeaderTable.TabIndex = 3;
            // 
            // InvoiceListPanel
            // 
            this.InvoiceListPanel.AutoScroll = true;
            this.InvoiceListPanel.Controls.Add(this.InvoiceList);
            this.InvoiceListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceListPanel.Location = new System.Drawing.Point(0, 42);
            this.InvoiceListPanel.Margin = new System.Windows.Forms.Padding(0);
            this.InvoiceListPanel.Name = "InvoiceListPanel";
            this.InvoiceListPanel.Size = new System.Drawing.Size(555, 214);
            this.InvoiceListPanel.TabIndex = 17;
            // 
            // InvoiceList
            // 
            this.InvoiceList.AutoSize = true;
            this.InvoiceList.ColumnCount = 2;
            this.InvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 287F));
            this.InvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InvoiceList.Controls.Add(this.label6, 1, 0);
            this.InvoiceList.Controls.Add(this.label8, 0, 0);
            this.InvoiceList.Dock = System.Windows.Forms.DockStyle.Top;
            this.InvoiceList.Location = new System.Drawing.Point(0, 0);
            this.InvoiceList.Name = "InvoiceList";
            this.InvoiceList.RowCount = 1;
            this.InvoiceList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InvoiceList.Size = new System.Drawing.Size(555, 20);
            this.InvoiceList.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(269, 1);
            this.label6.Margin = new System.Windows.Forms.Padding(1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(285, 18);
            this.label6.TabIndex = 3;
            this.label6.Text = "08/20/2019";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1, 1);
            this.label8.Margin = new System.Windows.Forms.Padding(1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(266, 18);
            this.label8.TabIndex = 1;
            this.label8.Text = "1344925";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InvoiceNumHeader
            // 
            this.InvoiceNumHeader.AutoSize = true;
            this.InvoiceNumHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InvoiceNumHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvoiceNumHeader.Location = new System.Drawing.Point(1, 1);
            this.InvoiceNumHeader.Margin = new System.Windows.Forms.Padding(1);
            this.InvoiceNumHeader.Name = "InvoiceNumHeader";
            this.InvoiceNumHeader.Size = new System.Drawing.Size(268, 35);
            this.InvoiceNumHeader.TabIndex = 0;
            this.InvoiceNumHeader.Text = "Invoice #";
            this.InvoiceNumHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateHeader
            // 
            this.DateHeader.AutoSize = true;
            this.DateHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateHeader.Location = new System.Drawing.Point(271, 1);
            this.DateHeader.Margin = new System.Windows.Forms.Padding(1);
            this.DateHeader.Name = "DateHeader";
            this.DateHeader.Size = new System.Drawing.Size(283, 35);
            this.DateHeader.TabIndex = 1;
            this.DateHeader.Text = "Date";
            this.DateHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemsPanel
            // 
            this.ItemsPanel.AutoScroll = true;
            this.ItemsPanel.Controls.Add(this.ItemsTable);
            this.ItemsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemsPanel.Location = new System.Drawing.Point(3, 265);
            this.ItemsPanel.Name = "ItemsPanel";
            this.ItemsPanel.Size = new System.Drawing.Size(555, 257);
            this.ItemsPanel.TabIndex = 4;
            // 
            // ItemsTable
            // 
            this.ItemsTable.ColumnCount = 1;
            this.ItemsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ItemsTable.Controls.Add(this.ItemsHeaderLine, 0, 1);
            this.ItemsTable.Controls.Add(this.ItemsHeaderTable, 0, 0);
            this.ItemsTable.Controls.Add(this.ItemListPanel, 0, 2);
            this.ItemsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemsTable.Location = new System.Drawing.Point(0, 0);
            this.ItemsTable.Name = "ItemsTable";
            this.ItemsTable.RowCount = 3;
            this.ItemsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ItemsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.ItemsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ItemsTable.Size = new System.Drawing.Size(555, 257);
            this.ItemsTable.TabIndex = 6;
            // 
            // ItemsHeaderLine
            // 
            this.ItemsHeaderLine.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ItemsHeaderLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ItemsHeaderLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemsHeaderLine.Location = new System.Drawing.Point(0, 37);
            this.ItemsHeaderLine.Margin = new System.Windows.Forms.Padding(0);
            this.ItemsHeaderLine.Name = "ItemsHeaderLine";
            this.ItemsHeaderLine.Size = new System.Drawing.Size(555, 3);
            this.ItemsHeaderLine.TabIndex = 16;
            // 
            // ItemsHeaderTable
            // 
            this.ItemsHeaderTable.ColumnCount = 5;
            this.ItemsHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.95238F));
            this.ItemsHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.ItemsHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.77489F));
            this.ItemsHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ItemsHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.ItemsHeaderTable.Controls.Add(this.label3, 4, 0);
            this.ItemsHeaderTable.Controls.Add(this.label1, 3, 0);
            this.ItemsHeaderTable.Controls.Add(this.CustomPartHeader, 0, 0);
            this.ItemsHeaderTable.Controls.Add(this.ItemDescriptionHeader, 2, 0);
            this.ItemsHeaderTable.Controls.Add(this.PartNumHeader, 0, 0);
            this.ItemsHeaderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemsHeaderTable.Location = new System.Drawing.Point(0, 0);
            this.ItemsHeaderTable.Margin = new System.Windows.Forms.Padding(0);
            this.ItemsHeaderTable.Name = "ItemsHeaderTable";
            this.ItemsHeaderTable.RowCount = 1;
            this.ItemsHeaderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ItemsHeaderTable.Size = new System.Drawing.Size(555, 37);
            this.ItemsHeaderTable.TabIndex = 3;
            // 
            // ItemDescriptionHeader
            // 
            this.ItemDescriptionHeader.AutoSize = true;
            this.ItemDescriptionHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemDescriptionHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemDescriptionHeader.Location = new System.Drawing.Point(259, 1);
            this.ItemDescriptionHeader.Margin = new System.Windows.Forms.Padding(1);
            this.ItemDescriptionHeader.Name = "ItemDescriptionHeader";
            this.ItemDescriptionHeader.Size = new System.Drawing.Size(183, 35);
            this.ItemDescriptionHeader.TabIndex = 1;
            this.ItemDescriptionHeader.Text = "Description";
            this.ItemDescriptionHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PartNumHeader
            // 
            this.PartNumHeader.AutoSize = true;
            this.PartNumHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PartNumHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartNumHeader.Location = new System.Drawing.Point(1, 1);
            this.PartNumHeader.Margin = new System.Windows.Forms.Padding(1);
            this.PartNumHeader.Name = "PartNumHeader";
            this.PartNumHeader.Size = new System.Drawing.Size(135, 35);
            this.PartNumHeader.TabIndex = 0;
            this.PartNumHeader.Text = "Part #";
            this.PartNumHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemListPanel
            // 
            this.ItemListPanel.AutoScroll = true;
            this.ItemListPanel.Controls.Add(this.ItemList);
            this.ItemListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemListPanel.Location = new System.Drawing.Point(0, 42);
            this.ItemListPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ItemListPanel.Name = "ItemListPanel";
            this.ItemListPanel.Size = new System.Drawing.Size(555, 215);
            this.ItemListPanel.TabIndex = 17;
            // 
            // ItemList
            // 
            this.ItemList.AutoSize = true;
            this.ItemList.ColumnCount = 5;
            this.ItemList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.95238F));
            this.ItemList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.ItemList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.77489F));
            this.ItemList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.ItemList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.ItemList.Controls.Add(this.label9, 4, 0);
            this.ItemList.Controls.Add(this.label7, 3, 0);
            this.ItemList.Controls.Add(this.label2, 0, 0);
            this.ItemList.Controls.Add(this.label4, 2, 0);
            this.ItemList.Controls.Add(this.label5, 0, 0);
            this.ItemList.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemList.Location = new System.Drawing.Point(0, 0);
            this.ItemList.Name = "ItemList";
            this.ItemList.RowCount = 1;
            this.ItemList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ItemList.Size = new System.Drawing.Size(555, 38);
            this.ItemList.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(259, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 36);
            this.label4.TabIndex = 3;
            this.label4.Text = "SD 7H15 5.9 DIESEL 4682 8 GRV";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 1);
            this.label5.Margin = new System.Windows.Forms.Padding(1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 36);
            this.label5.TabIndex = 1;
            this.label5.Text = "14-SD4682NEW";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomPartHeader
            // 
            this.CustomPartHeader.AutoSize = true;
            this.CustomPartHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomPartHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomPartHeader.Location = new System.Drawing.Point(138, 1);
            this.CustomPartHeader.Margin = new System.Windows.Forms.Padding(1);
            this.CustomPartHeader.Name = "CustomPartHeader";
            this.CustomPartHeader.Size = new System.Drawing.Size(119, 35);
            this.CustomPartHeader.TabIndex = 2;
            this.CustomPartHeader.Text = "Cust Part #";
            this.CustomPartHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(138, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 36);
            this.label2.TabIndex = 4;
            this.label2.Text = "60-01534";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EnterButton
            // 
            this.EnterButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EnterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnterButton.Location = new System.Drawing.Point(0, 52);
            this.EnterButton.Margin = new System.Windows.Forms.Padding(0);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(239, 57);
            this.EnterButton.TabIndex = 1;
            this.EnterButton.Text = "Open Invoice <Enter>";
            this.EnterButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(444, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 35);
            this.label1.TabIndex = 3;
            this.label1.Text = "ShpQty";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(503, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 35);
            this.label3.TabIndex = 4;
            this.label3.Text = "Qty";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(444, 1);
            this.label7.Margin = new System.Windows.Forms.Padding(1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 36);
            this.label7.TabIndex = 5;
            this.label7.Text = "ShpQty";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(503, 1);
            this.label9.Margin = new System.Windows.Forms.Padding(1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 36);
            this.label9.TabIndex = 6;
            this.label9.Text = "Qty";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ValidationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.InfoPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ValidationForm";
            this.Text = "Validation Form";
            this.InfoPanel.ResumeLayout(false);
            this.RightTable.ResumeLayout(false);
            this.LeftPanel.ResumeLayout(false);
            this.LeftTable.ResumeLayout(false);
            this.InvoicePanel.ResumeLayout(false);
            this.InvoiceTable.ResumeLayout(false);
            this.InvoiceHeaderTable.ResumeLayout(false);
            this.InvoiceHeaderTable.PerformLayout();
            this.InvoiceListPanel.ResumeLayout(false);
            this.InvoiceListPanel.PerformLayout();
            this.InvoiceList.ResumeLayout(false);
            this.InvoiceList.PerformLayout();
            this.ItemsPanel.ResumeLayout(false);
            this.ItemsTable.ResumeLayout(false);
            this.ItemsHeaderTable.ResumeLayout(false);
            this.ItemsHeaderTable.PerformLayout();
            this.ItemListPanel.ResumeLayout(false);
            this.ItemListPanel.PerformLayout();
            this.ItemList.ResumeLayout(false);
            this.ItemList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.TableLayoutPanel RightTable;
        private System.Windows.Forms.RichTextBox EnterField;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.TableLayoutPanel LeftTable;
        private System.Windows.Forms.Panel ItemsPanel;
        private System.Windows.Forms.TableLayoutPanel ItemsTable;
        private System.Windows.Forms.Label ItemsHeaderLine;
        private System.Windows.Forms.TableLayoutPanel ItemsHeaderTable;
        private System.Windows.Forms.Label CustomPartHeader;
        private System.Windows.Forms.Label ItemDescriptionHeader;
        private System.Windows.Forms.Label PartNumHeader;
        private System.Windows.Forms.Panel ItemListPanel;
        private System.Windows.Forms.TableLayoutPanel ItemList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel InvoicePanel;
        private System.Windows.Forms.TableLayoutPanel InvoiceTable;
        private System.Windows.Forms.Label InvoiceHeaderLine;
        private System.Windows.Forms.TableLayoutPanel InvoiceHeaderTable;
        private System.Windows.Forms.Label DateHeader;
        private System.Windows.Forms.Label InvoiceNumHeader;
        private System.Windows.Forms.Panel InvoiceListPanel;
        private System.Windows.Forms.TableLayoutPanel InvoiceList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button EnterButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
    }
}

