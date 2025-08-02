
namespace NEW_ERP.Forms.SupplierMaster
{
    partial class SupplierViewAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SupplierViewAll));
            this.SpplierMasterDataGridView = new System.Windows.Forms.DataGridView();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StatusCodeBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.SpplierMasterDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SpplierMasterDataGridView
            // 
            this.SpplierMasterDataGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.SpplierMasterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SpplierMasterDataGridView.Location = new System.Drawing.Point(4, 179);
            this.SpplierMasterDataGridView.Name = "SpplierMasterDataGridView";
            this.SpplierMasterDataGridView.Size = new System.Drawing.Size(987, 515);
            this.SpplierMasterDataGridView.TabIndex = 2166;
            this.SpplierMasterDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SpplierMasterDataGridView_CellDoubleClick);
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
            this.SearchBtn.Location = new System.Drawing.Point(461, 130);
            this.SearchBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(139, 32);
            this.SearchBtn.TabIndex = 2163;
            this.SearchBtn.Text = "SEARCH";
            this.SearchBtn.UseVisualStyleBackColor = false;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label2.Location = new System.Drawing.Point(7, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 20);
            this.label2.TabIndex = 2164;
            this.label2.Text = "Search by Status Code :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(328, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(272, 25);
            this.label3.TabIndex = 2159;
            this.label3.Text = "SUPPLIER MASTER ALL";
            // 
            // StatusCodeBox
            // 
            this.StatusCodeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.StatusCodeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.StatusCodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusCodeBox.FormattingEnabled = true;
            this.StatusCodeBox.Location = new System.Drawing.Point(223, 135);
            this.StatusCodeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.StatusCodeBox.Name = "StatusCodeBox";
            this.StatusCodeBox.Size = new System.Drawing.Size(230, 24);
            this.StatusCodeBox.TabIndex = 2167;
            // 
            // SupplierViewAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(999, 697);
            this.Controls.Add(this.StatusCodeBox);
            this.Controls.Add(this.SpplierMasterDataGridView);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Name = "SupplierViewAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SupplierViewAll";
            this.Load += new System.EventHandler(this.SupplierViewAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SpplierMasterDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView SpplierMasterDataGridView;
        public System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox StatusCodeBox;
    }
}