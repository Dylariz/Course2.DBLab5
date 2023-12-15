namespace UI
{
    partial class DbViewer
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
            this.tableDataGrid = new System.Windows.Forms.DataGridView();
            this.tableNameLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.tableChoiseComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tableDataGrid
            // 
            this.tableDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDataGrid.Location = new System.Drawing.Point(12, 134);
            this.tableDataGrid.Name = "tableDataGrid";
            this.tableDataGrid.RowTemplate.Height = 33;
            this.tableDataGrid.Size = new System.Drawing.Size(1583, 891);
            this.tableDataGrid.TabIndex = 0;
            // 
            // tableNameLabel
            // 
            this.tableNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableNameLabel.Location = new System.Drawing.Point(41, 30);
            this.tableNameLabel.Name = "tableNameLabel";
            this.tableNameLabel.Size = new System.Drawing.Size(321, 75);
            this.tableNameLabel.TabIndex = 1;
            this.tableNameLabel.Text = "Таблица: ";
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectButton.Location = new System.Drawing.Point(1262, 26);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(315, 79);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Соеденить с БД";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // tableChoiseComboBox
            // 
            this.tableChoiseComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableChoiseComboBox.FormattingEnabled = true;
            this.tableChoiseComboBox.Location = new System.Drawing.Point(310, 35);
            this.tableChoiseComboBox.Name = "tableChoiseComboBox";
            this.tableChoiseComboBox.Size = new System.Drawing.Size(496, 59);
            this.tableChoiseComboBox.TabIndex = 3;
            // 
            // DbViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1620, 1037);
            this.Controls.Add(this.tableChoiseComboBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.tableNameLabel);
            this.Controls.Add(this.tableDataGrid);
            this.Name = "DbViewer";
            this.Text = "DbViewer";
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGrid)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox tableChoiseComboBox;

        private System.Windows.Forms.Button connectButton;

        private System.Windows.Forms.Label tableNameLabel;

        private System.Windows.Forms.DataGridView tableDataGrid;

        #endregion
    }
}