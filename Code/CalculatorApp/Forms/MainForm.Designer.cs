namespace CalculatorApp.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageCalculator;
        private System.Windows.Forms.TabPage tabPageCurrency;
        private System.Windows.Forms.TabPage tabPageHistory;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.GroupBox grpCurrency;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Button btnMode;
        private System.Windows.Forms.Button btnTheme;
        private System.Windows.Forms.ComboBox cmbCode;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.Label lblBest;
        private System.Windows.Forms.Button btnFetch;
        private System.Windows.Forms.Button btnConvertPln;
        private System.Windows.Forms.Button btnConvertToPln;
        private System.Windows.Forms.ListBox lstHistory;
        private System.Windows.Forms.ListBox lstCurrencyHistory;
        private System.Windows.Forms.Button btnClearHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tabControlMain = new TabControl();
            tabPageCalculator = new TabPage();
            panelGrid = new Panel();
            panelTop = new Panel();
            txtDisplay = new TextBox();
            btnMode = new Button();
            btnTheme = new Button();
            tabPageCurrency = new TabPage();
            grpCurrency = new GroupBox();
            cmbCode = new ComboBox();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            numAmount = new NumericUpDown();
            lblBest = new Label();
            btnFetch = new Button();
            btnConvertPln = new Button();
            btnConvertToPln = new Button();
            tabPageHistory = new TabPage();
            lstHistory = new ListBox();
            lstCurrencyHistory = new ListBox();
            btnClearHistory = new Button();
            tabControlMain.SuspendLayout();
            tabPageCalculator.SuspendLayout();
            panelTop.SuspendLayout();
            tabPageCurrency.SuspendLayout();
            grpCurrency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
            tabPageHistory.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabPageCalculator);
            tabControlMain.Controls.Add(tabPageCurrency);
            tabControlMain.Controls.Add(tabPageHistory);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(0, 0);
            tabControlMain.Margin = new Padding(4, 5, 4, 5);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1000, 700);
            tabControlMain.TabIndex = 0;
            // 
            // tabPageCalculator
            // 
            tabPageCalculator.Controls.Add(panelGrid);
            tabPageCalculator.Controls.Add(panelTop);
            tabPageCalculator.Location = new Point(4, 34);
            tabPageCalculator.Margin = new Padding(4, 5, 4, 5);
            tabPageCalculator.Name = "tabPageCalculator";
            tabPageCalculator.Size = new Size(992, 662);
            tabPageCalculator.TabIndex = 0;
            tabPageCalculator.Text = "Kalkulator";
            tabPageCalculator.UseVisualStyleBackColor = true;
            // 
            // panelGrid
            // 
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(0, 57);
            panelGrid.Margin = new Padding(0);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(992, 605);
            panelGrid.TabIndex = 1;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(txtDisplay);
            panelTop.Controls.Add(btnMode);
            panelTop.Controls.Add(btnTheme);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(992, 57);
            panelTop.TabIndex = 0;
            // 
            // txtDisplay
            // 
            txtDisplay.Font = new Font("Segoe UI", 9F);
            txtDisplay.Location = new Point(7, 8);
            txtDisplay.Margin = new Padding(4, 5, 4, 5);
            txtDisplay.Name = "txtDisplay";
            txtDisplay.ReadOnly = true;
            txtDisplay.Size = new Size(570, 31);
            txtDisplay.TabIndex = 0;
            txtDisplay.TextAlign = HorizontalAlignment.Right;
            // 
            // btnMode
            // 
            btnMode.Location = new Point(593, 8);
            btnMode.Margin = new Padding(4, 5, 4, 5);
            btnMode.Name = "btnMode";
            btnMode.Size = new Size(201, 38);
            btnMode.TabIndex = 1;
            btnMode.Text = "Tryb: Prosty";
            btnMode.UseVisualStyleBackColor = true;
            btnMode.Click += btnMode_Click;
            // 
            // btnTheme
            // 
            btnTheme.Location = new Point(829, 8);
            btnTheme.Margin = new Padding(4, 5, 4, 5);
            btnTheme.Name = "btnTheme";
            btnTheme.Size = new Size(129, 38);
            btnTheme.TabIndex = 2;
            btnTheme.Text = "Motyw";
            btnTheme.UseVisualStyleBackColor = true;
            btnTheme.Click += btnTheme_Click;
            // 
            // tabPageCurrency
            // 
            tabPageCurrency.Controls.Add(grpCurrency);
            tabPageCurrency.Location = new Point(4, 34);
            tabPageCurrency.Margin = new Padding(4, 5, 4, 5);
            tabPageCurrency.Name = "tabPageCurrency";
            tabPageCurrency.Size = new Size(992, 662);
            tabPageCurrency.TabIndex = 1;
            tabPageCurrency.Text = "Kalkulator walutowy";
            tabPageCurrency.UseVisualStyleBackColor = true;
            // 
            // grpCurrency
            // 
            grpCurrency.Controls.Add(cmbCode);
            grpCurrency.Controls.Add(dtpFrom);
            grpCurrency.Controls.Add(dtpTo);
            grpCurrency.Controls.Add(numAmount);
            grpCurrency.Controls.Add(lblBest);
            grpCurrency.Controls.Add(btnFetch);
            grpCurrency.Controls.Add(btnConvertPln);
            grpCurrency.Controls.Add(btnConvertToPln);
            grpCurrency.Dock = DockStyle.Fill;
            grpCurrency.Font = new Font("Segoe UI", 8F);
            grpCurrency.Location = new Point(0, 0);
            grpCurrency.Margin = new Padding(4, 5, 4, 5);
            grpCurrency.Name = "grpCurrency";
            grpCurrency.Padding = new Padding(4, 5, 4, 5);
            grpCurrency.Size = new Size(992, 662);
            grpCurrency.TabIndex = 0;
            grpCurrency.TabStop = false;
            grpCurrency.Text = "Kalkulator walutowy";
            // 
            // cmbCode
            // 
            cmbCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCode.Items.AddRange(new object[] { "USD", "EUR", "GBP", "CHF" });
            cmbCode.Location = new Point(14, 33);
            cmbCode.Margin = new Padding(4, 5, 4, 5);
            cmbCode.Name = "cmbCode";
            cmbCode.Size = new Size(84, 29);
            cmbCode.TabIndex = 0;
            // 
            // dtpFrom
            // 
            dtpFrom.CustomFormat = "dd-MM-yyyy";
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.Location = new Point(114, 33);
            dtpFrom.Margin = new Padding(4, 5, 4, 5);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(127, 29);
            dtpFrom.TabIndex = 1;
            // 
            // dtpTo
            // 
            dtpTo.CustomFormat = "dd-MM-yyyy";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.Location = new Point(257, 33);
            dtpTo.Margin = new Padding(4, 5, 4, 5);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(127, 29);
            dtpTo.TabIndex = 2;
            // 
            // numAmount
            // 
            numAmount.DecimalPlaces = 2;
            numAmount.Location = new Point(14, 83);
            numAmount.Margin = new Padding(4, 5, 4, 5);
            numAmount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numAmount.Name = "numAmount";
            numAmount.Size = new Size(114, 29);
            numAmount.TabIndex = 3;
            // 
            // lblBest
            // 
            lblBest.AutoSize = true;
            lblBest.Location = new Point(14, 133);
            lblBest.Margin = new Padding(4, 0, 4, 0);
            lblBest.Name = "lblBest";
            lblBest.Size = new Size(0, 21);
            lblBest.TabIndex = 4;
            // 
            // btnFetch
            // 
            btnFetch.Location = new Point(400, 33);
            btnFetch.Margin = new Padding(4, 5, 4, 5);
            btnFetch.Name = "btnFetch";
            btnFetch.Size = new Size(114, 37);
            btnFetch.TabIndex = 5;
            btnFetch.Text = "Pobierz";
            btnFetch.UseVisualStyleBackColor = true;
            btnFetch.Click += btnFetch_Click;
            // 
            // btnConvertPln
            // 
            btnConvertPln.Location = new Point(143, 83);
            btnConvertPln.Margin = new Padding(4, 5, 4, 5);
            btnConvertPln.Name = "btnConvertPln";
            btnConvertPln.Size = new Size(114, 37);
            btnConvertPln.TabIndex = 6;
            btnConvertPln.Text = "PLN->Wal";
            btnConvertPln.UseVisualStyleBackColor = true;
            btnConvertPln.Click += btnConvertPln_Click;
            // 
            // btnConvertToPln
            // 
            btnConvertToPln.Location = new Point(271, 83);
            btnConvertToPln.Margin = new Padding(4, 5, 4, 5);
            btnConvertToPln.Name = "btnConvertToPln";
            btnConvertToPln.Size = new Size(114, 37);
            btnConvertToPln.TabIndex = 7;
            btnConvertToPln.Text = "Wal->PLN";
            btnConvertToPln.UseVisualStyleBackColor = true;
            btnConvertToPln.Click += btnConvertToPln_Click;
            // 
            // tabPageHistory
            // 
            tabPageHistory.Controls.Add(lstHistory);
            tabPageHistory.Controls.Add(lstCurrencyHistory);
            tabPageHistory.Controls.Add(btnClearHistory);
            tabPageHistory.Location = new Point(4, 34);
            tabPageHistory.Margin = new Padding(4, 5, 4, 5);
            tabPageHistory.Name = "tabPageHistory";
            tabPageHistory.Size = new Size(992, 662);
            tabPageHistory.TabIndex = 2;
            tabPageHistory.Text = "Historia";
            tabPageHistory.UseVisualStyleBackColor = true;
            // 
            // lstHistory
            // 
            lstHistory.Font = new Font("Segoe UI", 8F);
            lstHistory.FormattingEnabled = true;
            lstHistory.ItemHeight = 21;
            lstHistory.Location = new Point(14, 17);
            lstHistory.Margin = new Padding(4, 5, 4, 5);
            lstHistory.Name = "lstHistory";
            lstHistory.Size = new Size(455, 466);
            lstHistory.TabIndex = 0;
            // 
            // lstCurrencyHistory
            // 
            lstCurrencyHistory.Font = new Font("Segoe UI", 8F);
            lstCurrencyHistory.FormattingEnabled = true;
            lstCurrencyHistory.ItemHeight = 21;
            lstCurrencyHistory.Location = new Point(486, 17);
            lstCurrencyHistory.Margin = new Padding(4, 5, 4, 5);
            lstCurrencyHistory.Name = "lstCurrencyHistory";
            lstCurrencyHistory.Size = new Size(455, 466);
            lstCurrencyHistory.TabIndex = 1;
            // 
            // btnClearHistory
            // 
            btnClearHistory.Location = new Point(14, 517);
            btnClearHistory.Margin = new Padding(4, 5, 4, 5);
            btnClearHistory.Name = "btnClearHistory";
            btnClearHistory.Size = new Size(171, 37);
            btnClearHistory.TabIndex = 2;
            btnClearHistory.Text = "Wyczyść historię";
            btnClearHistory.UseVisualStyleBackColor = true;
            btnClearHistory.Click += btnClearHistory_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 700);
            Controls.Add(tabControlMain);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            Text = "CalculatorApp";
            Load += MainForm_Load;
            tabControlMain.ResumeLayout(false);
            tabPageCalculator.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            tabPageCurrency.ResumeLayout(false);
            grpCurrency.ResumeLayout(false);
            grpCurrency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
            tabPageHistory.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}
