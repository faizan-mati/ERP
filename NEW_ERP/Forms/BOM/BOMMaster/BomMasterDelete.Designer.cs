
namespace NEW_ERP.Forms.BOM.BOMMaster
{
    partial class BomMasterDelete
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
            this.BomMasterIdBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVersionNo = new System.Windows.Forms.TextBox();
            this.SaleOrderBox = new System.Windows.Forms.ComboBox();
            this.ProductBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.FormGroupBox = new System.Windows.Forms.GroupBox();
            this.FormGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.FlatAppearance.BorderSize = 0;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            // 
            // BomMasterIdBox
            // 
            this.BomMasterIdBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.BomMasterIdBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.BomMasterIdBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BomMasterIdBox.FormattingEnabled = true;
            this.BomMasterIdBox.Location = new System.Drawing.Point(157, 88);
            this.BomMasterIdBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BomMasterIdBox.Name = "BomMasterIdBox";
            this.BomMasterIdBox.Size = new System.Drawing.Size(203, 24);
            this.BomMasterIdBox.TabIndex = 2193;
            this.BomMasterIdBox.SelectedIndexChanged += new System.EventHandler(this.BomMasterIdBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label1.Location = new System.Drawing.Point(13, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 2194;
            this.label1.Text = "Bom Master Id :";
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
            // txtVersionNo
            // 
            this.txtVersionNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersionNo.Location = new System.Drawing.Point(157, 223);
            this.txtVersionNo.Name = "txtVersionNo";
            this.txtVersionNo.ReadOnly = true;
            this.txtVersionNo.Size = new System.Drawing.Size(203, 26);
            this.txtVersionNo.TabIndex = 2178;
            // 
            // SaleOrderBox
            // 
            this.SaleOrderBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SaleOrderBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SaleOrderBox.Enabled = false;
            this.SaleOrderBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaleOrderBox.FormattingEnabled = true;
            this.SaleOrderBox.Location = new System.Drawing.Point(157, 136);
            this.SaleOrderBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SaleOrderBox.Name = "SaleOrderBox";
            this.SaleOrderBox.Size = new System.Drawing.Size(203, 24);
            this.SaleOrderBox.TabIndex = 2172;
            // 
            // ProductBox
            // 
            this.ProductBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ProductBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ProductBox.Enabled = false;
            this.ProductBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductBox.FormattingEnabled = true;
            this.ProductBox.Location = new System.Drawing.Point(157, 181);
            this.ProductBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProductBox.Name = "ProductBox";
            this.ProductBox.Size = new System.Drawing.Size(203, 24);
            this.ProductBox.TabIndex = 2177;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(65, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 2174;
            this.label5.Text = "Product :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label7.Location = new System.Drawing.Point(38, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 20);
            this.label7.TabIndex = 2175;
            this.label7.Text = "Version No :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label17.Location = new System.Drawing.Point(45, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(105, 20);
            this.label17.TabIndex = 2176;
            this.label17.Text = "Sale Order :";
            // 
            // FormGroupBox
            // 
            this.FormGroupBox.Controls.Add(this.BomMasterIdBox);
            this.FormGroupBox.Controls.Add(this.label1);
            this.FormGroupBox.Controls.Add(this.label3);
            this.FormGroupBox.Controls.Add(this.txtVersionNo);
            this.FormGroupBox.Controls.Add(this.SaleOrderBox);
            this.FormGroupBox.Controls.Add(this.ProductBox);
            this.FormGroupBox.Controls.Add(this.label5);
            this.FormGroupBox.Controls.Add(this.label7);
            this.FormGroupBox.Controls.Add(this.label17);
            this.FormGroupBox.Location = new System.Drawing.Point(12, 97);
            this.FormGroupBox.Name = "FormGroupBox";
            this.FormGroupBox.Size = new System.Drawing.Size(396, 310);
            this.FormGroupBox.TabIndex = 2242;
            this.FormGroupBox.TabStop = false;
            this.FormGroupBox.Text = "FORM INPUTS";
            // 
            // BomMasterDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 645);
            this.Controls.Add(this.FormGroupBox);
            this.Name = "BomMasterDelete";
            this.Text = "BomMasterDelete";
            this.Load += new System.EventHandler(this.BomMasterDelete_Load);
            this.Controls.SetChildIndex(this.CloseBtn, 0);
            this.Controls.SetChildIndex(this.DeleteBtn, 0);
            this.Controls.SetChildIndex(this.FormGroupBox, 0);
            this.FormGroupBox.ResumeLayout(false);
            this.FormGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox BomMasterIdBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVersionNo;
        public System.Windows.Forms.ComboBox SaleOrderBox;
        public System.Windows.Forms.ComboBox ProductBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox FormGroupBox;
    }
}