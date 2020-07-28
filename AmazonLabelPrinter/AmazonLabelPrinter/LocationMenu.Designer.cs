namespace AmazonLabelPrinter
{
    partial class LocationMenu
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
            this.ButtonTable = new System.Windows.Forms.TableLayoutPanel();
            this.NV = new System.Windows.Forms.Button();
            this.TX = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.Label();
            this.ButtonTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonTable
            // 
            this.ButtonTable.ColumnCount = 1;
            this.ButtonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ButtonTable.Controls.Add(this.TX, 0, 2);
            this.ButtonTable.Controls.Add(this.NV, 0, 1);
            this.ButtonTable.Controls.Add(this.Header, 0, 0);
            this.ButtonTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonTable.Location = new System.Drawing.Point(0, 0);
            this.ButtonTable.Name = "ButtonTable";
            this.ButtonTable.RowCount = 3;
            this.ButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.ButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ButtonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ButtonTable.Size = new System.Drawing.Size(245, 247);
            this.ButtonTable.TabIndex = 0;
            // 
            // NV
            // 
            this.NV.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.NV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NV.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NV.Location = new System.Drawing.Point(0, 50);
            this.NV.Margin = new System.Windows.Forms.Padding(0);
            this.NV.Name = "NV";
            this.NV.Size = new System.Drawing.Size(245, 98);
            this.NV.TabIndex = 0;
            this.NV.Text = "Reno";
            this.NV.UseVisualStyleBackColor = false;
            // 
            // TX
            // 
            this.TX.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.TX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TX.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TX.Location = new System.Drawing.Point(0, 148);
            this.TX.Margin = new System.Windows.Forms.Padding(0);
            this.TX.Name = "TX";
            this.TX.Size = new System.Drawing.Size(245, 99);
            this.TX.TabIndex = 1;
            this.TX.Text = "Fort Worth";
            this.TX.UseVisualStyleBackColor = false;
            this.TX.Click += new System.EventHandler(this.TX_Click);
            // 
            // Header
            // 
            this.Header.AutoSize = true;
            this.Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header.Location = new System.Drawing.Point(3, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(239, 50);
            this.Header.TabIndex = 2;
            this.Header.Text = "Select Warehouse";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LocationMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 247);
            this.Controls.Add(this.ButtonTable);
            this.Name = "LocationMenu";
            this.Text = "LocationMenu";
            this.ButtonTable.ResumeLayout(false);
            this.ButtonTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ButtonTable;
        private System.Windows.Forms.Button TX;
        private System.Windows.Forms.Button NV;
        private System.Windows.Forms.Label Header;
    }
}