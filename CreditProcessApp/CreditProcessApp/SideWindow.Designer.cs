namespace CreditProcessApp
{
    partial class SideWindow
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessPanel = new System.Windows.Forms.Panel();
            this.ProcessTable = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessHeaderLine = new System.Windows.Forms.Label();
            this.ProcessHeaderTable = new System.Windows.Forms.TableLayoutPanel();
            this.ProcessDateButton = new System.Windows.Forms.Button();
            this.ProcessInvoiceNumButton = new System.Windows.Forms.Button();
            this.ProcessAccountButton = new System.Windows.Forms.Button();
            this.ProcessSalespersonButton = new System.Windows.Forms.Button();
            this.ProcessInvoicePanel = new System.Windows.Forms.Panel();
            this.ProcessInvoiceList = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SearchBox = new System.Windows.Forms.GroupBox();
            this.SearchByBox = new System.Windows.Forms.GroupBox();
            this.SearchText = new System.Windows.Forms.TextBox();
            this.SearchBySelection = new System.Windows.Forms.ComboBox();
            this.ReprocessButton = new System.Windows.Forms.Button();
            this.RemainderHeader = new System.Windows.Forms.Button();
            this.TotalHeader = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.ProcessPanel.SuspendLayout();
            this.ProcessTable.SuspendLayout();
            this.ProcessHeaderTable.SuspendLayout();
            this.ProcessInvoicePanel.SuspendLayout();
            this.ProcessInvoiceList.SuspendLayout();
            this.SearchBox.SuspendLayout();
            this.SearchByBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.36317F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.71355F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.17903F));
            this.tableLayoutPanel1.Controls.Add(this.SearchByBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ReprocessButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ProcessPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SearchBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.05153F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.94847F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(454, 621);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ProcessPanel
            // 
            this.ProcessPanel.AutoScroll = true;
            this.tableLayoutPanel1.SetColumnSpan(this.ProcessPanel, 3);
            this.ProcessPanel.Controls.Add(this.ProcessTable);
            this.ProcessPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessPanel.Location = new System.Drawing.Point(3, 53);
            this.ProcessPanel.Name = "ProcessPanel";
            this.ProcessPanel.Size = new System.Drawing.Size(448, 565);
            this.ProcessPanel.TabIndex = 4;
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
            this.ProcessTable.Size = new System.Drawing.Size(448, 565);
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
            this.ProcessHeaderLine.Size = new System.Drawing.Size(448, 3);
            this.ProcessHeaderLine.TabIndex = 16;
            // 
            // ProcessHeaderTable
            // 
            this.ProcessHeaderTable.ColumnCount = 6;
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.90032F));
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.13165F));
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.32493F));
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.43409F));
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.18649F));
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.ProcessHeaderTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ProcessHeaderTable.Controls.Add(this.TotalHeader, 3, 0);
            this.ProcessHeaderTable.Controls.Add(this.RemainderHeader, 4, 0);
            this.ProcessHeaderTable.Controls.Add(this.ProcessDateButton, 5, 0);
            this.ProcessHeaderTable.Controls.Add(this.ProcessInvoiceNumButton, 0, 0);
            this.ProcessHeaderTable.Controls.Add(this.ProcessAccountButton, 1, 0);
            this.ProcessHeaderTable.Controls.Add(this.ProcessSalespersonButton, 2, 0);
            this.ProcessHeaderTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessHeaderTable.Location = new System.Drawing.Point(0, 0);
            this.ProcessHeaderTable.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessHeaderTable.Name = "ProcessHeaderTable";
            this.ProcessHeaderTable.RowCount = 1;
            this.ProcessHeaderTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ProcessHeaderTable.Size = new System.Drawing.Size(448, 37);
            this.ProcessHeaderTable.TabIndex = 3;
            // 
            // ProcessDateButton
            // 
            this.ProcessDateButton.AutoSize = true;
            this.ProcessDateButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ProcessDateButton.BackColor = System.Drawing.SystemColors.Control;
            this.ProcessDateButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessDateButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ProcessDateButton.FlatAppearance.BorderSize = 0;
            this.ProcessDateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcessDateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessDateButton.Location = new System.Drawing.Point(358, 1);
            this.ProcessDateButton.Margin = new System.Windows.Forms.Padding(1);
            this.ProcessDateButton.Name = "ProcessDateButton";
            this.ProcessDateButton.Size = new System.Drawing.Size(89, 35);
            this.ProcessDateButton.TabIndex = 14;
            this.ProcessDateButton.Tag = "CRDT_INV_DATE";
            this.ProcessDateButton.Text = "Date";
            this.ProcessDateButton.UseVisualStyleBackColor = false;
            // 
            // ProcessInvoiceNumButton
            // 
            this.ProcessInvoiceNumButton.AccessibleDescription = "CRDT_INV_NUM";
            this.ProcessInvoiceNumButton.AutoSize = true;
            this.ProcessInvoiceNumButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ProcessInvoiceNumButton.BackColor = System.Drawing.SystemColors.Control;
            this.ProcessInvoiceNumButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessInvoiceNumButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ProcessInvoiceNumButton.FlatAppearance.BorderSize = 0;
            this.ProcessInvoiceNumButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcessInvoiceNumButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessInvoiceNumButton.Location = new System.Drawing.Point(1, 1);
            this.ProcessInvoiceNumButton.Margin = new System.Windows.Forms.Padding(1);
            this.ProcessInvoiceNumButton.Name = "ProcessInvoiceNumButton";
            this.ProcessInvoiceNumButton.Size = new System.Drawing.Size(73, 35);
            this.ProcessInvoiceNumButton.TabIndex = 13;
            this.ProcessInvoiceNumButton.Tag = "CRDT_INV_NUM";
            this.ProcessInvoiceNumButton.Text = "Invoice #";
            this.ProcessInvoiceNumButton.UseVisualStyleBackColor = false;
            // 
            // ProcessAccountButton
            // 
            this.ProcessAccountButton.AutoSize = true;
            this.ProcessAccountButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ProcessAccountButton.BackColor = System.Drawing.SystemColors.Control;
            this.ProcessAccountButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessAccountButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ProcessAccountButton.FlatAppearance.BorderSize = 0;
            this.ProcessAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcessAccountButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessAccountButton.Location = new System.Drawing.Point(76, 1);
            this.ProcessAccountButton.Margin = new System.Windows.Forms.Padding(1);
            this.ProcessAccountButton.Name = "ProcessAccountButton";
            this.ProcessAccountButton.Size = new System.Drawing.Size(102, 35);
            this.ProcessAccountButton.TabIndex = 12;
            this.ProcessAccountButton.Tag = "CRDT_INV_CUSCOD";
            this.ProcessAccountButton.Text = "Account";
            this.ProcessAccountButton.UseVisualStyleBackColor = false;
            // 
            // ProcessSalespersonButton
            // 
            this.ProcessSalespersonButton.AutoSize = true;
            this.ProcessSalespersonButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ProcessSalespersonButton.BackColor = System.Drawing.SystemColors.Control;
            this.ProcessSalespersonButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessSalespersonButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ProcessSalespersonButton.FlatAppearance.BorderSize = 0;
            this.ProcessSalespersonButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcessSalespersonButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessSalespersonButton.Location = new System.Drawing.Point(180, 1);
            this.ProcessSalespersonButton.Margin = new System.Windows.Forms.Padding(1);
            this.ProcessSalespersonButton.Name = "ProcessSalespersonButton";
            this.ProcessSalespersonButton.Size = new System.Drawing.Size(42, 35);
            this.ProcessSalespersonButton.TabIndex = 11;
            this.ProcessSalespersonButton.Tag = "CRDT_INV_SLSP";
            this.ProcessSalespersonButton.Text = "SLSP";
            this.ProcessSalespersonButton.UseVisualStyleBackColor = false;
            // 
            // ProcessInvoicePanel
            // 
            this.ProcessInvoicePanel.AutoScroll = true;
            this.ProcessInvoicePanel.Controls.Add(this.ProcessInvoiceList);
            this.ProcessInvoicePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessInvoicePanel.Location = new System.Drawing.Point(0, 42);
            this.ProcessInvoicePanel.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessInvoicePanel.Name = "ProcessInvoicePanel";
            this.ProcessInvoicePanel.Size = new System.Drawing.Size(448, 523);
            this.ProcessInvoicePanel.TabIndex = 17;
            // 
            // ProcessInvoiceList
            // 
            this.ProcessInvoiceList.AutoSize = true;
            this.ProcessInvoiceList.ColumnCount = 6;
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.34831F));
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.21348F));
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.07865F));
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.77465F));
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.69014F));
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.ProcessInvoiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ProcessInvoiceList.Controls.Add(this.label4, 2, 0);
            this.ProcessInvoiceList.Controls.Add(this.label6, 5, 0);
            this.ProcessInvoiceList.Controls.Add(this.label7, 1, 0);
            this.ProcessInvoiceList.Controls.Add(this.label8, 0, 0);
            this.ProcessInvoiceList.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProcessInvoiceList.Location = new System.Drawing.Point(0, 0);
            this.ProcessInvoiceList.Name = "ProcessInvoiceList";
            this.ProcessInvoiceList.RowCount = 1;
            this.ProcessInvoiceList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ProcessInvoiceList.Size = new System.Drawing.Size(448, 15);
            this.ProcessInvoiceList.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(181, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "SLSP";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(357, 1);
            this.label6.Margin = new System.Windows.Forms.Padding(1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "08/20/2019";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(77, 1);
            this.label7.Margin = new System.Windows.Forms.Padding(1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "AUTOAIROL";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1, 1);
            this.label8.Margin = new System.Windows.Forms.Padding(1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "1344925";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SearchBox
            // 
            this.SearchBox.Controls.Add(this.SearchText);
            this.SearchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchBox.Location = new System.Drawing.Point(3, 3);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(167, 44);
            this.SearchBox.TabIndex = 5;
            this.SearchBox.TabStop = false;
            this.SearchBox.Text = "Search";
            // 
            // SearchByBox
            // 
            this.SearchByBox.Controls.Add(this.SearchBySelection);
            this.SearchByBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchByBox.Location = new System.Drawing.Point(176, 3);
            this.SearchByBox.Name = "SearchByBox";
            this.SearchByBox.Size = new System.Drawing.Size(137, 44);
            this.SearchByBox.TabIndex = 6;
            this.SearchByBox.TabStop = false;
            this.SearchByBox.Text = "By";
            // 
            // SearchText
            // 
            this.SearchText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchText.Location = new System.Drawing.Point(3, 16);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(161, 20);
            this.SearchText.TabIndex = 0;
            // 
            // SearchBySelection
            // 
            this.SearchBySelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchBySelection.FormattingEnabled = true;
            this.SearchBySelection.Items.AddRange(new object[] {
            "SalesPerson",
            "Account"});
            this.SearchBySelection.Location = new System.Drawing.Point(3, 16);
            this.SearchBySelection.Name = "SearchBySelection";
            this.SearchBySelection.Size = new System.Drawing.Size(131, 21);
            this.SearchBySelection.TabIndex = 0;
            this.SearchBySelection.SelectedIndexChanged += new System.EventHandler(this.SearchBySelection_SelectedIndexChanged);
            // 
            // ReprocessButton
            // 
            this.ReprocessButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ReprocessButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReprocessButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.ReprocessButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReprocessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReprocessButton.Location = new System.Drawing.Point(316, 0);
            this.ReprocessButton.Margin = new System.Windows.Forms.Padding(0);
            this.ReprocessButton.Name = "ReprocessButton";
            this.ReprocessButton.Size = new System.Drawing.Size(138, 50);
            this.ReprocessButton.TabIndex = 21;
            this.ReprocessButton.Text = "Search";
            this.ReprocessButton.UseVisualStyleBackColor = false;
            this.ReprocessButton.Click += new System.EventHandler(this.ReprocessButton_Click);
            // 
            // RemainderHeader
            // 
            this.RemainderHeader.AutoSize = true;
            this.RemainderHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RemainderHeader.BackColor = System.Drawing.SystemColors.Control;
            this.RemainderHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RemainderHeader.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.RemainderHeader.FlatAppearance.BorderSize = 0;
            this.RemainderHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemainderHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemainderHeader.Location = new System.Drawing.Point(279, 1);
            this.RemainderHeader.Margin = new System.Windows.Forms.Padding(1);
            this.RemainderHeader.Name = "RemainderHeader";
            this.RemainderHeader.Size = new System.Drawing.Size(77, 35);
            this.RemainderHeader.TabIndex = 15;
            this.RemainderHeader.Text = "Remainder";
            this.RemainderHeader.UseVisualStyleBackColor = false;
            // 
            // TotalHeader
            // 
            this.TotalHeader.AutoSize = true;
            this.TotalHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TotalHeader.BackColor = System.Drawing.SystemColors.Control;
            this.TotalHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TotalHeader.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.TotalHeader.FlatAppearance.BorderSize = 0;
            this.TotalHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TotalHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalHeader.Location = new System.Drawing.Point(224, 1);
            this.TotalHeader.Margin = new System.Windows.Forms.Padding(1);
            this.TotalHeader.Name = "TotalHeader";
            this.TotalHeader.Size = new System.Drawing.Size(53, 35);
            this.TotalHeader.TabIndex = 16;
            this.TotalHeader.Text = "Total";
            this.TotalHeader.UseVisualStyleBackColor = false;
            // 
            // SideWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 621);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SideWindow";
            this.Text = "SideWindow";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ProcessPanel.ResumeLayout(false);
            this.ProcessTable.ResumeLayout(false);
            this.ProcessHeaderTable.ResumeLayout(false);
            this.ProcessHeaderTable.PerformLayout();
            this.ProcessInvoicePanel.ResumeLayout(false);
            this.ProcessInvoicePanel.PerformLayout();
            this.ProcessInvoiceList.ResumeLayout(false);
            this.ProcessInvoiceList.PerformLayout();
            this.SearchBox.ResumeLayout(false);
            this.SearchBox.PerformLayout();
            this.SearchByBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox SearchByBox;
        private System.Windows.Forms.ComboBox SearchBySelection;
        private System.Windows.Forms.Panel ProcessPanel;
        private System.Windows.Forms.TableLayoutPanel ProcessTable;
        private System.Windows.Forms.Label ProcessHeaderLine;
        private System.Windows.Forms.TableLayoutPanel ProcessHeaderTable;
        private System.Windows.Forms.Button ProcessDateButton;
        private System.Windows.Forms.Button ProcessInvoiceNumButton;
        private System.Windows.Forms.Button ProcessAccountButton;
        private System.Windows.Forms.Button ProcessSalespersonButton;
        private System.Windows.Forms.Panel ProcessInvoicePanel;
        private System.Windows.Forms.TableLayoutPanel ProcessInvoiceList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox SearchBox;
        private System.Windows.Forms.TextBox SearchText;
        private System.Windows.Forms.Button ReprocessButton;
        private System.Windows.Forms.Button TotalHeader;
        private System.Windows.Forms.Button RemainderHeader;
    }
}