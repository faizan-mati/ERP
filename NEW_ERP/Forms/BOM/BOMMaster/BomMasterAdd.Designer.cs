
namespace NEW_ERP.Forms.BOMMaster
{
    partial class BomMasterAdd
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
            this.txtVersionNo = new System.Windows.Forms.TextBox();
            this.ProductBox = new System.Windows.Forms.ComboBox();
            this.SaleOrderBox = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FormGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FormGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.FlatAppearance.BorderSize = 0;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // EditBtn
            // 
            this.EditBtn.FlatAppearance.BorderSize = 0;
            this.EditBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // ViewAllBtn
            // 
            this.ViewAllBtn.FlatAppearance.BorderSize = 0;
            // 
            // CloseBtn
            // 
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.FlatAppearance.BorderSize = 0;
            this.SubmitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // txtVersionNo
            // 
            this.txtVersionNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersionNo.Location = new System.Drawing.Point(138, 169);
            this.txtVersionNo.Name = "txtVersionNo";
            this.txtVersionNo.Size = new System.Drawing.Size(203, 26);
            this.txtVersionNo.TabIndex = 2178;
            // 
            // ProductBox
            // 
            this.ProductBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ProductBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ProductBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductBox.FormattingEnabled = true;
            this.ProductBox.Location = new System.Drawing.Point(138, 127);
            this.ProductBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProductBox.Name = "ProductBox";
            this.ProductBox.Size = new System.Drawing.Size(203, 24);
            this.ProductBox.TabIndex = 2177;
            // 
            // SaleOrderBox
            // 
            this.SaleOrderBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SaleOrderBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SaleOrderBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaleOrderBox.FormattingEnabled = true;
            this.SaleOrderBox.Location = new System.Drawing.Point(138, 82);
            this.SaleOrderBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SaleOrderBox.Name = "SaleOrderBox";
            this.SaleOrderBox.Size = new System.Drawing.Size(203, 24);
            this.SaleOrderBox.TabIndex = 2172;
            this.SaleOrderBox.SelectedIndexChanged += new System.EventHandler(this.SaleOrderBox_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label17.Location = new System.Drawing.Point(30, 84);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(105, 20);
            this.label17.TabIndex = 2176;
            this.label17.Text = "Sale Order :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label7.Location = new System.Drawing.Point(23, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 20);
            this.label7.TabIndex = 2175;
            this.label7.Text = "Version No :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(50, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 2174;
            this.label5.Text = "Product :";
            // 
            // FormGroupBox
            // 
            this.FormGroupBox.Controls.Add(this.label3);
            this.FormGroupBox.Controls.Add(this.txtVersionNo);
            this.FormGroupBox.Controls.Add(this.SaleOrderBox);
            this.FormGroupBox.Controls.Add(this.ProductBox);
            this.FormGroupBox.Controls.Add(this.label5);
            this.FormGroupBox.Controls.Add(this.label7);
            this.FormGroupBox.Controls.Add(this.label17);
            this.FormGroupBox.Location = new System.Drawing.Point(12, 90);
            this.FormGroupBox.Name = "FormGroupBox";
            this.FormGroupBox.Size = new System.Drawing.Size(403, 248);
            this.FormGroupBox.TabIndex = 2191;
            this.FormGroupBox.TabStop = false;
            this.FormGroupBox.Text = "FORM INPUTS";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(77, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 25);
            this.label3.TabIndex = 2192;
            this.label3.Text = "BOM MASTER FORM";
            // 
            // BomMasterAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 778);
            this.Controls.Add(this.FormGroupBox);
            this.Name = "BomMasterAdd";
            this.Text = "BomMasterAdd";
            this.Load += new System.EventHandler(this.BomMasterAdd_Load);
            this.Controls.SetChildIndex(this.FormGroupBox, 0);
            this.Controls.SetChildIndex(this.SubmitBtn, 0);
            this.Controls.SetChildIndex(this.CloseBtn, 0);
            this.Controls.SetChildIndex(this.ViewAllBtn, 0);
            this.Controls.SetChildIndex(this.EditBtn, 0);
            this.Controls.SetChildIndex(this.DeleteBtn, 0);
            this.FormGroupBox.ResumeLayout(false);
            this.FormGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtVersionNo;
        public System.Windows.Forms.ComboBox ProductBox;
        public System.Windows.Forms.ComboBox SaleOrderBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox FormGroupBox;
        private System.Windows.Forms.Label label3;
    }
}