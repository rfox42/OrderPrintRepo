namespace OrderValidation
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Florida = new System.Windows.Forms.Button();
            this.FortWorth = new System.Windows.Forms.Button();
            this.Pennsylvania = new System.Windows.Forms.Button();
            this.Reno = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Florida, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.FortWorth, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Pennsylvania, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Reno, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(255, 302);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Florida
            // 
            this.Florida.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Florida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Florida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Florida.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Florida.Location = new System.Drawing.Point(0, 150);
            this.Florida.Margin = new System.Windows.Forms.Padding(0);
            this.Florida.Name = "Florida";
            this.Florida.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.Florida.Size = new System.Drawing.Size(255, 75);
            this.Florida.TabIndex = 5;
            this.Florida.Tag = "FL";
            this.Florida.Text = "Florida";
            this.Florida.UseVisualStyleBackColor = false;
            this.Florida.Click += new System.EventHandler(this.Reno_Click);
            // 
            // FortWorth
            // 
            this.FortWorth.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.FortWorth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FortWorth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FortWorth.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FortWorth.Location = new System.Drawing.Point(0, 75);
            this.FortWorth.Margin = new System.Windows.Forms.Padding(0);
            this.FortWorth.Name = "FortWorth";
            this.FortWorth.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.FortWorth.Size = new System.Drawing.Size(255, 75);
            this.FortWorth.TabIndex = 4;
            this.FortWorth.Tag = "FORT WORTH";
            this.FortWorth.Text = "Fort Worth";
            this.FortWorth.UseVisualStyleBackColor = false;
            this.FortWorth.Click += new System.EventHandler(this.Reno_Click);
            // 
            // Pennsylvania
            // 
            this.Pennsylvania.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Pennsylvania.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pennsylvania.Enabled = false;
            this.Pennsylvania.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pennsylvania.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pennsylvania.Location = new System.Drawing.Point(0, 225);
            this.Pennsylvania.Margin = new System.Windows.Forms.Padding(0);
            this.Pennsylvania.Name = "Pennsylvania";
            this.Pennsylvania.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.Pennsylvania.Size = new System.Drawing.Size(255, 77);
            this.Pennsylvania.TabIndex = 3;
            this.Pennsylvania.Tag = "PA";
            this.Pennsylvania.Text = "Pennsylvania";
            this.Pennsylvania.UseVisualStyleBackColor = false;
            // 
            // Reno
            // 
            this.Reno.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Reno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Reno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reno.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reno.Location = new System.Drawing.Point(0, 0);
            this.Reno.Margin = new System.Windows.Forms.Padding(0);
            this.Reno.Name = "Reno";
            this.Reno.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.Reno.Size = new System.Drawing.Size(255, 75);
            this.Reno.TabIndex = 2;
            this.Reno.Tag = "RENO";
            this.Reno.Text = "Reno";
            this.Reno.UseVisualStyleBackColor = false;
            this.Reno.Click += new System.EventHandler(this.Reno_Click);
            // 
            // LocationMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 302);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LocationMenu";
            this.Text = "Select Location";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button Florida;
        private System.Windows.Forms.Button FortWorth;
        private System.Windows.Forms.Button Pennsylvania;
        private System.Windows.Forms.Button Reno;
    }
}