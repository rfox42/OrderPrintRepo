namespace OrderValidation
{
    partial class AddUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUserForm));
            this.ID_BOX = new System.Windows.Forms.TextBox();
            this.ID_Group = new System.Windows.Forms.GroupBox();
            this.NameGroup = new System.Windows.Forms.GroupBox();
            this.NameText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CancelButton = new System.Windows.Forms.Button();
            this.EnterButton = new System.Windows.Forms.Button();
            this.managerCheckBox = new System.Windows.Forms.CheckBox();
            this.ID_Group.SuspendLayout();
            this.NameGroup.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ID_BOX
            // 
            this.ID_BOX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ID_BOX.Location = new System.Drawing.Point(3, 22);
            this.ID_BOX.Name = "ID_BOX";
            this.ID_BOX.Size = new System.Drawing.Size(235, 26);
            this.ID_BOX.TabIndex = 2;
            // 
            // ID_Group
            // 
            this.ID_Group.Controls.Add(this.ID_BOX);
            this.ID_Group.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ID_Group.Location = new System.Drawing.Point(12, 78);
            this.ID_Group.Name = "ID_Group";
            this.ID_Group.Size = new System.Drawing.Size(241, 60);
            this.ID_Group.TabIndex = 25;
            this.ID_Group.TabStop = false;
            this.ID_Group.Text = "Badge ID";
            // 
            // NameGroup
            // 
            this.NameGroup.Controls.Add(this.NameText);
            this.NameGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameGroup.Location = new System.Drawing.Point(12, 12);
            this.NameGroup.Name = "NameGroup";
            this.NameGroup.Size = new System.Drawing.Size(238, 60);
            this.NameGroup.TabIndex = 22;
            this.NameGroup.TabStop = false;
            this.NameGroup.Text = "Name";
            // 
            // NameText
            // 
            this.NameText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameText.Location = new System.Drawing.Point(3, 22);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(232, 26);
            this.NameText.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.CancelButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.EnterButton, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 153);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(349, 46);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.CancelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(174, 0);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(175, 46);
            this.CancelButton.TabIndex = 20;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // EnterButton
            // 
            this.EnterButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.EnterButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EnterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnterButton.Location = new System.Drawing.Point(0, 0);
            this.EnterButton.Margin = new System.Windows.Forms.Padding(0);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(174, 46);
            this.EnterButton.TabIndex = 19;
            this.EnterButton.Text = "Add User";
            this.EnterButton.UseVisualStyleBackColor = false;
            this.EnterButton.Click += new System.EventHandler(this.EnterButton_Click);
            // 
            // managerCheckBox
            // 
            this.managerCheckBox.AutoSize = true;
            this.managerCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.managerCheckBox.Location = new System.Drawing.Point(256, 39);
            this.managerCheckBox.Name = "managerCheckBox";
            this.managerCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.managerCheckBox.Size = new System.Drawing.Size(78, 21);
            this.managerCheckBox.TabIndex = 2;
            this.managerCheckBox.Text = "Manager";
            this.managerCheckBox.UseCompatibleTextRendering = true;
            this.managerCheckBox.UseVisualStyleBackColor = true;
            // 
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 199);
            this.Controls.Add(this.managerCheckBox);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.NameGroup);
            this.Controls.Add(this.ID_Group);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddUserForm";
            this.Text = "New User";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddUserForm_FormClosed);
            this.Load += new System.EventHandler(this.AddUserForm_Load);
            this.ID_Group.ResumeLayout(false);
            this.ID_Group.PerformLayout();
            this.NameGroup.ResumeLayout(false);
            this.NameGroup.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ID_BOX;
        private System.Windows.Forms.GroupBox ID_Group;
        private System.Windows.Forms.GroupBox NameGroup;
        private System.Windows.Forms.TextBox NameText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button EnterButton;
        private System.Windows.Forms.CheckBox managerCheckBox;
    }
}