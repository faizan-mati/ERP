
namespace NEW_ERP.Forms.AuthorityForm
{
    partial class AuthorityViewAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorityViewAll));
            this.label3 = new System.Windows.Forms.Label();
            this.AuthorityDataGridView = new System.Windows.Forms.DataGridView();
            this.AuthorityNameBox = new System.Windows.Forms.ComboBox();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AuthorityDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(152, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(254, 25);
            this.label3.TabIndex = 2116;
            this.label3.Text = "AUTHORITY VIEW ALL";
            // 
            // AuthorityDataGridView
            // 
            this.AuthorityDataGridView.AllowUserToAddRows = false;
            this.AuthorityDataGridView.AllowUserToDeleteRows = false;
            this.AuthorityDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AuthorityDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AuthorityDataGridView.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.AuthorityDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AuthorityDataGridView.Location = new System.Drawing.Point(3, 158);
            this.AuthorityDataGridView.Name = "AuthorityDataGridView";
            this.AuthorityDataGridView.ReadOnly = true;
            this.AuthorityDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AuthorityDataGridView.Size = new System.Drawing.Size(807, 429);
            this.AuthorityDataGridView.TabIndex = 2121;
            this.AuthorityDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AuthorityDataGridView_CellDoubleClick);
            // 
            // AuthorityNameBox
            // 
            this.AuthorityNameBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.AuthorityNameBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.AuthorityNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthorityNameBox.FormattingEnabled = true;
            this.AuthorityNameBox.Location = new System.Drawing.Point(239, 110);
            this.AuthorityNameBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AuthorityNameBox.Name = "AuthorityNameBox";
            this.AuthorityNameBox.Size = new System.Drawing.Size(155, 24);
            this.AuthorityNameBox.TabIndex = 1;
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
            this.SearchBtn.Location = new System.Drawing.Point(401, 105);
            this.SearchBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(134, 32);
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
            this.label2.Location = new System.Drawing.Point(5, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 20);
            this.label2.TabIndex = 2122;
            this.label2.Text = "Search by Authority Name :";
            // 
            // AuthorityViewAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(822, 590);
            this.Controls.Add(this.AuthorityNameBox);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AuthorityDataGridView);
            this.Controls.Add(this.label3);
            this.Name = "AuthorityViewAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AuthorityViewAll";
            this.Load += new System.EventHandler(this.AuthorityViewAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AuthorityDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView AuthorityDataGridView;
        public System.Windows.Forms.ComboBox AuthorityNameBox;
        public System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Label label2;
    }
}