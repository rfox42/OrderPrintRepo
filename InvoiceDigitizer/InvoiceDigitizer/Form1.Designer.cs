namespace InvoiceDigitizer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ProcessButton = new System.Windows.Forms.Button();
            this.ReviewButton = new System.Windows.Forms.Button();
            this.LeftTable = new System.Windows.Forms.TableLayoutPanel();
            this.CalendarButton = new System.Windows.Forms.Button();
            this.ClearQueueButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ReviewCounterBox = new System.Windows.Forms.GroupBox();
            this.ReviewCounter = new System.Windows.Forms.Label();
            this.CountDisplay = new System.Windows.Forms.GroupBox();
            this.StubCounter = new System.Windows.Forms.Label();
            this.MainTable = new System.Windows.Forms.TableLayoutPanel();
            this.ReviewPanel = new System.Windows.Forms.Panel();
            this.MessagePanel = new System.Windows.Forms.Panel();
            this.MessageText = new System.Windows.Forms.Label();
            this.ReviewBrowser = new System.Windows.Forms.WebBrowser();
            this.ReviewControlsTable = new System.Windows.Forms.TableLayoutPanel();
            this.ReviewStopButton = new System.Windows.Forms.Button();
            this.ReviewDeleteButton = new System.Windows.Forms.Button();
            this.ReviewSubmitButton = new System.Windows.Forms.Button();
            this.ReviewBox = new System.Windows.Forms.GroupBox();
            this.ReviewInput = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.CalendarPanel = new System.Windows.Forms.Panel();
            this.Calendar = new System.Windows.Forms.MonthCalendar();
            this.LeftTable.SuspendLayout();
            this.ReviewCounterBox.SuspendLayout();
            this.CountDisplay.SuspendLayout();
            this.MainTable.SuspendLayout();
            this.ReviewPanel.SuspendLayout();
            this.MessagePanel.SuspendLayout();
            this.ReviewControlsTable.SuspendLayout();
            this.ReviewBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.CalendarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessButton
            // 
            this.ProcessButton.BackColor = System.Drawing.Color.PaleGreen;
            this.LeftTable.SetColumnSpan(this.ProcessButton, 2);
            this.ProcessButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessButton.Enabled = false;
            this.ProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessButton.Location = new System.Drawing.Point(0, 244);
            this.ProcessButton.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(200, 144);
            this.ProcessButton.TabIndex = 0;
            this.ProcessButton.Text = "Process Stubs";
            this.ProcessButton.UseVisualStyleBackColor = false;
            this.ProcessButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ReviewButton
            // 
            this.ReviewButton.BackColor = System.Drawing.Color.Khaki;
            this.LeftTable.SetColumnSpan(this.ReviewButton, 2);
            this.ReviewButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewButton.Enabled = false;
            this.ReviewButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ReviewButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReviewButton.Location = new System.Drawing.Point(0, 532);
            this.ReviewButton.Margin = new System.Windows.Forms.Padding(0);
            this.ReviewButton.Name = "ReviewButton";
            this.ReviewButton.Size = new System.Drawing.Size(200, 146);
            this.ReviewButton.TabIndex = 1;
            this.ReviewButton.Text = "Review Flagged Stubs";
            this.ReviewButton.UseVisualStyleBackColor = false;
            this.ReviewButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // LeftTable
            // 
            this.LeftTable.ColumnCount = 2;
            this.LeftTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LeftTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.LeftTable.Controls.Add(this.CalendarButton, 1, 0);
            this.LeftTable.Controls.Add(this.ClearQueueButton, 0, 1);
            this.LeftTable.Controls.Add(this.RefreshButton, 0, 0);
            this.LeftTable.Controls.Add(this.ReviewCounterBox, 0, 4);
            this.LeftTable.Controls.Add(this.CountDisplay, 0, 2);
            this.LeftTable.Controls.Add(this.ProcessButton, 0, 3);
            this.LeftTable.Controls.Add(this.ReviewButton, 0, 5);
            this.LeftTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftTable.Location = new System.Drawing.Point(0, 0);
            this.LeftTable.Margin = new System.Windows.Forms.Padding(0);
            this.LeftTable.Name = "LeftTable";
            this.LeftTable.RowCount = 6;
            this.MainTable.SetRowSpan(this.LeftTable, 2);
            this.LeftTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.LeftTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.LeftTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LeftTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LeftTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LeftTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LeftTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.LeftTable.Size = new System.Drawing.Size(200, 678);
            this.LeftTable.TabIndex = 2;
            // 
            // CalendarButton
            // 
            this.CalendarButton.BackColor = System.Drawing.Color.LightBlue;
            this.CalendarButton.BackgroundImage = global::InvoiceDigitizer.Properties.Resources.calendarimg;
            this.CalendarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CalendarButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalendarButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CalendarButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalendarButton.Location = new System.Drawing.Point(143, 0);
            this.CalendarButton.Margin = new System.Windows.Forms.Padding(0);
            this.CalendarButton.Name = "CalendarButton";
            this.CalendarButton.Size = new System.Drawing.Size(57, 50);
            this.CalendarButton.TabIndex = 7;
            this.CalendarButton.UseVisualStyleBackColor = false;
            this.CalendarButton.Click += new System.EventHandler(this.CalendarButton_Click);
            // 
            // ClearQueueButton
            // 
            this.ClearQueueButton.BackColor = System.Drawing.Color.LightCoral;
            this.LeftTable.SetColumnSpan(this.ClearQueueButton, 2);
            this.ClearQueueButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClearQueueButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearQueueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearQueueButton.Location = new System.Drawing.Point(0, 50);
            this.ClearQueueButton.Margin = new System.Windows.Forms.Padding(0);
            this.ClearQueueButton.Name = "ClearQueueButton";
            this.ClearQueueButton.Size = new System.Drawing.Size(200, 50);
            this.ClearQueueButton.TabIndex = 6;
            this.ClearQueueButton.Text = "Clear Queue";
            this.ClearQueueButton.UseVisualStyleBackColor = false;
            this.ClearQueueButton.Click += new System.EventHandler(this.ClearQueueButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.BackColor = System.Drawing.Color.LightBlue;
            this.RefreshButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Location = new System.Drawing.Point(0, 0);
            this.RefreshButton.Margin = new System.Windows.Forms.Padding(0);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(143, 50);
            this.RefreshButton.TabIndex = 5;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = false;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ReviewCounterBox
            // 
            this.ReviewCounterBox.BackColor = System.Drawing.SystemColors.Control;
            this.LeftTable.SetColumnSpan(this.ReviewCounterBox, 2);
            this.ReviewCounterBox.Controls.Add(this.ReviewCounter);
            this.ReviewCounterBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewCounterBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReviewCounterBox.Location = new System.Drawing.Point(3, 391);
            this.ReviewCounterBox.Name = "ReviewCounterBox";
            this.ReviewCounterBox.Size = new System.Drawing.Size(194, 138);
            this.ReviewCounterBox.TabIndex = 4;
            this.ReviewCounterBox.TabStop = false;
            this.ReviewCounterBox.Text = "Stubs to Review";
            // 
            // ReviewCounter
            // 
            this.ReviewCounter.BackColor = System.Drawing.Color.Transparent;
            this.ReviewCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReviewCounter.Location = new System.Drawing.Point(3, 22);
            this.ReviewCounter.Name = "ReviewCounter";
            this.ReviewCounter.Size = new System.Drawing.Size(188, 113);
            this.ReviewCounter.TabIndex = 0;
            this.ReviewCounter.Text = "0";
            this.ReviewCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CountDisplay
            // 
            this.CountDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.LeftTable.SetColumnSpan(this.CountDisplay, 2);
            this.CountDisplay.Controls.Add(this.StubCounter);
            this.CountDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CountDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountDisplay.Location = new System.Drawing.Point(3, 103);
            this.CountDisplay.Name = "CountDisplay";
            this.CountDisplay.Size = new System.Drawing.Size(194, 138);
            this.CountDisplay.TabIndex = 3;
            this.CountDisplay.TabStop = false;
            this.CountDisplay.Text = "Stubs in Queue";
            // 
            // StubCounter
            // 
            this.StubCounter.BackColor = System.Drawing.Color.Transparent;
            this.StubCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StubCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StubCounter.Location = new System.Drawing.Point(3, 22);
            this.StubCounter.Name = "StubCounter";
            this.StubCounter.Size = new System.Drawing.Size(188, 113);
            this.StubCounter.TabIndex = 0;
            this.StubCounter.Text = "0";
            this.StubCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainTable
            // 
            this.MainTable.BackColor = System.Drawing.Color.White;
            this.MainTable.ColumnCount = 2;
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTable.Controls.Add(this.LeftTable, 0, 0);
            this.MainTable.Controls.Add(this.ReviewPanel, 1, 0);
            this.MainTable.Controls.Add(this.ReviewControlsTable, 1, 1);
            this.MainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTable.Location = new System.Drawing.Point(0, 0);
            this.MainTable.Name = "MainTable";
            this.MainTable.RowCount = 2;
            this.MainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTable.Size = new System.Drawing.Size(761, 678);
            this.MainTable.TabIndex = 3;
            // 
            // ReviewPanel
            // 
            this.ReviewPanel.Controls.Add(this.MessagePanel);
            this.ReviewPanel.Controls.Add(this.ReviewBrowser);
            this.ReviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewPanel.Location = new System.Drawing.Point(200, 0);
            this.ReviewPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ReviewPanel.Name = "ReviewPanel";
            this.ReviewPanel.Size = new System.Drawing.Size(561, 578);
            this.ReviewPanel.TabIndex = 3;
            // 
            // MessagePanel
            // 
            this.MessagePanel.Controls.Add(this.MessageText);
            this.MessagePanel.Location = new System.Drawing.Point(127, 106);
            this.MessagePanel.Name = "MessagePanel";
            this.MessagePanel.Size = new System.Drawing.Size(328, 166);
            this.MessagePanel.TabIndex = 1;
            this.MessagePanel.Visible = false;
            // 
            // MessageText
            // 
            this.MessageText.BackColor = System.Drawing.SystemColors.Control;
            this.MessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageText.Location = new System.Drawing.Point(0, 0);
            this.MessageText.Name = "MessageText";
            this.MessageText.Size = new System.Drawing.Size(328, 166);
            this.MessageText.TabIndex = 0;
            this.MessageText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReviewBrowser
            // 
            this.ReviewBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewBrowser.Location = new System.Drawing.Point(0, 0);
            this.ReviewBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.ReviewBrowser.Name = "ReviewBrowser";
            this.ReviewBrowser.Size = new System.Drawing.Size(561, 578);
            this.ReviewBrowser.TabIndex = 0;
            // 
            // ReviewControlsTable
            // 
            this.ReviewControlsTable.BackColor = System.Drawing.SystemColors.Control;
            this.ReviewControlsTable.ColumnCount = 4;
            this.ReviewControlsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.ReviewControlsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.ReviewControlsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.ReviewControlsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.ReviewControlsTable.Controls.Add(this.ReviewStopButton, 2, 0);
            this.ReviewControlsTable.Controls.Add(this.ReviewDeleteButton, 3, 0);
            this.ReviewControlsTable.Controls.Add(this.ReviewSubmitButton, 1, 0);
            this.ReviewControlsTable.Controls.Add(this.ReviewBox, 0, 0);
            this.ReviewControlsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewControlsTable.Location = new System.Drawing.Point(200, 578);
            this.ReviewControlsTable.Margin = new System.Windows.Forms.Padding(0);
            this.ReviewControlsTable.Name = "ReviewControlsTable";
            this.ReviewControlsTable.RowCount = 1;
            this.ReviewControlsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ReviewControlsTable.Size = new System.Drawing.Size(561, 100);
            this.ReviewControlsTable.TabIndex = 6;
            // 
            // ReviewStopButton
            // 
            this.ReviewStopButton.BackColor = System.Drawing.Color.Khaki;
            this.ReviewStopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewStopButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ReviewStopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReviewStopButton.Location = new System.Drawing.Point(358, 0);
            this.ReviewStopButton.Margin = new System.Windows.Forms.Padding(0);
            this.ReviewStopButton.Name = "ReviewStopButton";
            this.ReviewStopButton.Size = new System.Drawing.Size(100, 100);
            this.ReviewStopButton.TabIndex = 8;
            this.ReviewStopButton.Text = "Stop\r\nReview";
            this.ReviewStopButton.UseVisualStyleBackColor = false;
            this.ReviewStopButton.Click += new System.EventHandler(this.ReviewStopButton_Click);
            // 
            // ReviewDeleteButton
            // 
            this.ReviewDeleteButton.BackColor = System.Drawing.Color.LightCoral;
            this.ReviewDeleteButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewDeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ReviewDeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReviewDeleteButton.Location = new System.Drawing.Point(458, 0);
            this.ReviewDeleteButton.Margin = new System.Windows.Forms.Padding(0);
            this.ReviewDeleteButton.Name = "ReviewDeleteButton";
            this.ReviewDeleteButton.Size = new System.Drawing.Size(103, 100);
            this.ReviewDeleteButton.TabIndex = 7;
            this.ReviewDeleteButton.Text = "Delete\r\nScan";
            this.ReviewDeleteButton.UseVisualStyleBackColor = false;
            this.ReviewDeleteButton.Click += new System.EventHandler(this.ReviewDeleteButton_Click);
            // 
            // ReviewSubmitButton
            // 
            this.ReviewSubmitButton.BackColor = System.Drawing.Color.PaleGreen;
            this.ReviewSubmitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewSubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ReviewSubmitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReviewSubmitButton.Location = new System.Drawing.Point(258, 0);
            this.ReviewSubmitButton.Margin = new System.Windows.Forms.Padding(0);
            this.ReviewSubmitButton.Name = "ReviewSubmitButton";
            this.ReviewSubmitButton.Size = new System.Drawing.Size(100, 100);
            this.ReviewSubmitButton.TabIndex = 6;
            this.ReviewSubmitButton.Text = "Submit\r\nStub";
            this.ReviewSubmitButton.UseVisualStyleBackColor = false;
            this.ReviewSubmitButton.Click += new System.EventHandler(this.ReviewSubmitButton_Click);
            // 
            // ReviewBox
            // 
            this.ReviewBox.Controls.Add(this.ReviewInput);
            this.ReviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReviewBox.Location = new System.Drawing.Point(5, 15);
            this.ReviewBox.Margin = new System.Windows.Forms.Padding(5, 15, 5, 20);
            this.ReviewBox.Name = "ReviewBox";
            this.ReviewBox.Size = new System.Drawing.Size(248, 65);
            this.ReviewBox.TabIndex = 5;
            this.ReviewBox.TabStop = false;
            this.ReviewBox.Text = "Please type invoice number:";
            // 
            // ReviewInput
            // 
            this.ReviewInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReviewInput.Location = new System.Drawing.Point(3, 22);
            this.ReviewInput.Name = "ReviewInput";
            this.ReviewInput.Size = new System.Drawing.Size(242, 26);
            this.ReviewInput.TabIndex = 0;
            this.ReviewInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ReviewInput_KeyPress);
            // 
            // CalendarPanel
            // 
            this.CalendarPanel.Controls.Add(this.Calendar);
            this.CalendarPanel.Location = new System.Drawing.Point(0, 50);
            this.CalendarPanel.Name = "CalendarPanel";
            this.CalendarPanel.Size = new System.Drawing.Size(227, 161);
            this.CalendarPanel.TabIndex = 4;
            this.CalendarPanel.Visible = false;
            // 
            // Calendar
            // 
            this.Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Calendar.Location = new System.Drawing.Point(0, 0);
            this.Calendar.Name = "Calendar";
            this.Calendar.TabIndex = 0;
            this.Calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.Calendar_DateSelected);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 678);
            this.Controls.Add(this.CalendarPanel);
            this.Controls.Add(this.MainTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Ranshu Invoice Digitizer";
            this.LeftTable.ResumeLayout(false);
            this.ReviewCounterBox.ResumeLayout(false);
            this.CountDisplay.ResumeLayout(false);
            this.MainTable.ResumeLayout(false);
            this.ReviewPanel.ResumeLayout(false);
            this.MessagePanel.ResumeLayout(false);
            this.ReviewControlsTable.ResumeLayout(false);
            this.ReviewBox.ResumeLayout(false);
            this.ReviewBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.CalendarPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.Button ReviewButton;
        private System.Windows.Forms.TableLayoutPanel LeftTable;
        private System.Windows.Forms.GroupBox CountDisplay;
        private System.Windows.Forms.Label StubCounter;
        private System.Windows.Forms.TableLayoutPanel MainTable;
        private System.Windows.Forms.Panel ReviewPanel;
        private System.Windows.Forms.GroupBox ReviewCounterBox;
        private System.Windows.Forms.Label ReviewCounter;
        private System.Windows.Forms.TableLayoutPanel ReviewControlsTable;
        private System.Windows.Forms.Button ReviewSubmitButton;
        private System.Windows.Forms.GroupBox ReviewBox;
        private System.Windows.Forms.TextBox ReviewInput;
        private System.Windows.Forms.Button ReviewStopButton;
        private System.Windows.Forms.Button ReviewDeleteButton;
        private System.Windows.Forms.WebBrowser ReviewBrowser;
        private System.Windows.Forms.Panel MessagePanel;
        private System.Windows.Forms.Label MessageText;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button ClearQueueButton;
        private System.Windows.Forms.Button CalendarButton;
        private System.Windows.Forms.Panel CalendarPanel;
        private System.Windows.Forms.MonthCalendar Calendar;
    }
}

