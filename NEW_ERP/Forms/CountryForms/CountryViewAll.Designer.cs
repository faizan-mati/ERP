
namespace NEW_ERP
{
    partial class CountryViewAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountryViewAll));
            this.label3 = new System.Windows.Forms.Label();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CountryDataGridView = new System.Windows.Forms.DataGridView();
            this.CountryCodeBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.CountryDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(181, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(235, 25);
            this.label3.TabIndex = 2094;
            this.label3.Text = "COUNTRY VIEW ALL";
            // 
            // SearchBtn
            // 
            this.SearchBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(134)))), ((int)(((byte)(193)))));
            this.SearchBtn.FlatAppearance.BorderSize = 0;
            this.SearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBtn.ForeColor = System.Drawing.Color.White;
            this.SearchBtn.Image = ((System.Drawing.Image)(resources.GetObject("SearchBtn.Image")));
            this.SearchBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SearchBtn.Location = new System.Drawing.Point(388, 103);
            this.SearchBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(125, 32);
            this.SearchBtn.TabIndex = 2;
            this.SearchBtn.Text = "SEARCH";
            this.SearchBtn.UseVisualStyleBackColor = false;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label2.Location = new System.Drawing.Point(11, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 20);
            this.label2.TabIndex = 2090;
            this.label2.Text = "Search by Country Code :";
            // 
            // CountryDataGridView
            // 
            this.CountryDataGridView.AllowUserToAddRows = false;
            this.CountryDataGridView.AllowUserToDeleteRows = false;
            this.CountryDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CountryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CountryDataGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.CountryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CountryDataGridView.Location = new System.Drawing.Point(7, 158);
            this.CountryDataGridView.Name = "CountryDataGridView";
            this.CountryDataGridView.ReadOnly = true;
            this.CountryDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CountryDataGridView.Size = new System.Drawing.Size(792, 429);
            this.CountryDataGridView.TabIndex = 2098;
            this.CountryDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CountryDataGridView_CellDoubleClick);
            // 
            // CountryCodeBox
            // 
            this.CountryCodeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CountryCodeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CountryCodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountryCodeBox.FormattingEnabled = true;
            this.CountryCodeBox.Location = new System.Drawing.Point(226, 108);
            this.CountryCodeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CountryCodeBox.Name = "CountryCodeBox";
            this.CountryCodeBox.Size = new System.Drawing.Size(155, 24);
            this.CountryCodeBox.TabIndex = 1;
            // 
            // CountryViewAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(807, 590);
            this.Controls.Add(this.CountryCodeBox);
            this.Controls.Add(this.CountryDataGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.label2);
            this.Name = "CountryViewAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CountryViewAll";
            this.Load += new System.EventHandler(this.CountryViewAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CountryDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView CountryDataGridView;
        public System.Windows.Forms.ComboBox CountryCodeBox;
    }
}