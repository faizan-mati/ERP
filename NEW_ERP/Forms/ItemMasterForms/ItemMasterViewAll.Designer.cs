
namespace NEW_ERP.Forms.ItemMasterForms
{
    partial class ItemMasterViewAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemMasterViewAll));
            this.SearchBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ItemMasterDataGridView = new System.Windows.Forms.DataGridView();
            this.ProductCodeBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ItemMasterDataGridView)).BeginInit();
            this.SuspendLayout();
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
            this.SearchBtn.Location = new System.Drawing.Point(381, 108);
            this.SearchBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(129, 32);
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
            this.label2.Location = new System.Drawing.Point(12, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 20);
            this.label2.TabIndex = 2081;
            this.label2.Text = "Search by Short Name :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(132, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 25);
            this.label3.TabIndex = 2085;
            this.label3.Text = "ITEM MASTER VIEW ALL";
            // 
            // ItemMasterDataGridView
            // 
            this.ItemMasterDataGridView.AllowUserToAddRows = false;
            this.ItemMasterDataGridView.AllowUserToDeleteRows = false;
            this.ItemMasterDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemMasterDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ItemMasterDataGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.ItemMasterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemMasterDataGridView.Location = new System.Drawing.Point(3, 160);
            this.ItemMasterDataGridView.Name = "ItemMasterDataGridView";
            this.ItemMasterDataGridView.ReadOnly = true;
            this.ItemMasterDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ItemMasterDataGridView.Size = new System.Drawing.Size(644, 335);
            this.ItemMasterDataGridView.TabIndex = 2089;
            // 
            // ProductCodeBox
            // 
            this.ProductCodeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ProductCodeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ProductCodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductCodeBox.FormattingEnabled = true;
            this.ProductCodeBox.Location = new System.Drawing.Point(218, 113);
            this.ProductCodeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProductCodeBox.Name = "ProductCodeBox";
            this.ProductCodeBox.Size = new System.Drawing.Size(155, 24);
            this.ProductCodeBox.TabIndex = 1;
            // 
            // ItemMasterViewAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(659, 507);
            this.Controls.Add(this.ProductCodeBox);
            this.Controls.Add(this.ItemMasterDataGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.label2);
            this.Name = "ItemMasterViewAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ItemMasterViewAll";
            this.Load += new System.EventHandler(this.ItemMasterViewAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ItemMasterDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView ItemMasterDataGridView;
        public System.Windows.Forms.ComboBox ProductCodeBox;
    }
}