
namespace NEW_ERP.Forms.ItemMasterForms
{
    partial class ItemMasterEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemMasterEdit));
            this.TxtProductDes = new System.Windows.Forms.TextBox();
            this.TxtProductShortName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.TxtProductRemarks = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtProductCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ProductCodeBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnGroupBox = new System.Windows.Forms.GroupBox();
            this.FormGroupBox = new System.Windows.Forms.GroupBox();
            this.BtnGroupBox.SuspendLayout();
            this.FormGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtProductDes
            // 
            this.TxtProductDes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProductDes.Location = new System.Drawing.Point(194, 230);
            this.TxtProductDes.Name = "TxtProductDes";
            this.TxtProductDes.ReadOnly = true;
            this.TxtProductDes.Size = new System.Drawing.Size(259, 26);
            this.TxtProductDes.TabIndex = 4;
            // 
            // TxtProductShortName
            // 
            this.TxtProductShortName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProductShortName.Location = new System.Drawing.Point(195, 178);
            this.TxtProductShortName.Name = "TxtProductShortName";
            this.TxtProductShortName.ReadOnly = true;
            this.TxtProductShortName.Size = new System.Drawing.Size(259, 26);
            this.TxtProductShortName.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label6.Location = new System.Drawing.Point(6, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 20);
            this.label6.TabIndex = 2098;
            this.label6.Text = "Product Short Name :";
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(134)))), ((int)(((byte)(193)))));
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.ForeColor = System.Drawing.Color.White;
            this.CloseBtn.Image = ((System.Drawing.Image)(resources.GetObject("CloseBtn.Image")));
            this.CloseBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CloseBtn.Location = new System.Drawing.Point(146, 19);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(100, 31);
            this.CloseBtn.TabIndex = 7;
            this.CloseBtn.Text = "EXIT";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(134)))), ((int)(((byte)(193)))));
            this.UpdateBtn.FlatAppearance.BorderSize = 0;
            this.UpdateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateBtn.ForeColor = System.Drawing.Color.White;
            this.UpdateBtn.Image = ((System.Drawing.Image)(resources.GetObject("UpdateBtn.Image")));
            this.UpdateBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UpdateBtn.Location = new System.Drawing.Point(15, 19);
            this.UpdateBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(127, 31);
            this.UpdateBtn.TabIndex = 6;
            this.UpdateBtn.Text = "UPDATE";
            this.UpdateBtn.UseVisualStyleBackColor = false;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // TxtProductRemarks
            // 
            this.TxtProductRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProductRemarks.Location = new System.Drawing.Point(195, 272);
            this.TxtProductRemarks.Multiline = true;
            this.TxtProductRemarks.Name = "TxtProductRemarks";
            this.TxtProductRemarks.ReadOnly = true;
            this.TxtProductRemarks.Size = new System.Drawing.Size(259, 79);
            this.TxtProductRemarks.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(96, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 2094;
            this.label5.Text = "Remarks : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label2.Location = new System.Drawing.Point(11, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 20);
            this.label2.TabIndex = 2092;
            this.label2.Text = "Product Description :";
            // 
            // TxtProductCode
            // 
            this.TxtProductCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProductCode.Location = new System.Drawing.Point(195, 133);
            this.TxtProductCode.Name = "TxtProductCode";
            this.TxtProductCode.ReadOnly = true;
            this.TxtProductCode.Size = new System.Drawing.Size(259, 26);
            this.TxtProductCode.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label4.Location = new System.Drawing.Point(61, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 2091;
            this.label4.Text = "Product Code :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(109, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(298, 25);
            this.label3.TabIndex = 2086;
            this.label3.Text = "ITEM MASTER EIDT FORM";
            // 
            // ProductCodeBox
            // 
            this.ProductCodeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ProductCodeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ProductCodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductCodeBox.FormattingEnabled = true;
            this.ProductCodeBox.Location = new System.Drawing.Point(195, 91);
            this.ProductCodeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProductCodeBox.Name = "ProductCodeBox";
            this.ProductCodeBox.Size = new System.Drawing.Size(259, 24);
            this.ProductCodeBox.TabIndex = 1;
            this.ProductCodeBox.SelectedIndexChanged += new System.EventHandler(this.ProductCodeBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label7.Location = new System.Drawing.Point(26, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 20);
            this.label7.TabIndex = 2101;
            this.label7.Text = "Edit Product Code :";
            // 
            // BtnGroupBox
            // 
            this.BtnGroupBox.Controls.Add(this.CloseBtn);
            this.BtnGroupBox.Controls.Add(this.UpdateBtn);
            this.BtnGroupBox.Location = new System.Drawing.Point(12, 12);
            this.BtnGroupBox.Name = "BtnGroupBox";
            this.BtnGroupBox.Size = new System.Drawing.Size(253, 58);
            this.BtnGroupBox.TabIndex = 2181;
            this.BtnGroupBox.TabStop = false;
            this.BtnGroupBox.Text = "BUTTONS";
            // 
            // FormGroupBox
            // 
            this.FormGroupBox.Controls.Add(this.TxtProductCode);
            this.FormGroupBox.Controls.Add(this.label3);
            this.FormGroupBox.Controls.Add(this.label7);
            this.FormGroupBox.Controls.Add(this.label4);
            this.FormGroupBox.Controls.Add(this.ProductCodeBox);
            this.FormGroupBox.Controls.Add(this.label2);
            this.FormGroupBox.Controls.Add(this.TxtProductDes);
            this.FormGroupBox.Controls.Add(this.label5);
            this.FormGroupBox.Controls.Add(this.TxtProductShortName);
            this.FormGroupBox.Controls.Add(this.TxtProductRemarks);
            this.FormGroupBox.Controls.Add(this.label6);
            this.FormGroupBox.Location = new System.Drawing.Point(14, 87);
            this.FormGroupBox.Name = "FormGroupBox";
            this.FormGroupBox.Size = new System.Drawing.Size(494, 386);
            this.FormGroupBox.TabIndex = 2182;
            this.FormGroupBox.TabStop = false;
            this.FormGroupBox.Text = "FORM INPUTS";
            // 
            // ItemMasterEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(659, 507);
            this.Controls.Add(this.BtnGroupBox);
            this.Controls.Add(this.FormGroupBox);
            this.Name = "ItemMasterEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ItemMasterEdit";
            this.Load += new System.EventHandler(this.ItemMasterEdit_Load);
            this.BtnGroupBox.ResumeLayout(false);
            this.FormGroupBox.ResumeLayout(false);
            this.FormGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TxtProductDes;
        private System.Windows.Forms.TextBox TxtProductShortName;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button CloseBtn;
        public System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.TextBox TxtProductRemarks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtProductCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox ProductCodeBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox BtnGroupBox;
        private System.Windows.Forms.GroupBox FormGroupBox;
    }
}