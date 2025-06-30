
namespace NEW_ERP.Forms.CountryForms
{
    partial class CountryEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountryEdit));
            this.label7 = new System.Windows.Forms.Label();
            this.CountryCodeBox = new System.Windows.Forms.ComboBox();
            this.TxtCountryName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.TxtCountryCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.isCheckedcheckbox = new System.Windows.Forms.CheckBox();
            this.BtnGroupBox = new System.Windows.Forms.GroupBox();
            this.FormGroupBox = new System.Windows.Forms.GroupBox();
            this.FormGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label7.Location = new System.Drawing.Point(25, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 20);
            this.label7.TabIndex = 2119;
            this.label7.Text = "Country Code :";
            // 
            // CountryCodeBox
            // 
            this.CountryCodeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CountryCodeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CountryCodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountryCodeBox.FormattingEnabled = true;
            this.CountryCodeBox.Location = new System.Drawing.Point(161, 178);
            this.CountryCodeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CountryCodeBox.Name = "CountryCodeBox";
            this.CountryCodeBox.Size = new System.Drawing.Size(259, 24);
            this.CountryCodeBox.TabIndex = 1;
            this.CountryCodeBox.SelectedIndexChanged += new System.EventHandler(this.CountryCodeBox_SelectedIndexChanged);
            // 
            // TxtCountryName
            // 
            this.TxtCountryName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCountryName.Location = new System.Drawing.Point(161, 269);
            this.TxtCountryName.Name = "TxtCountryName";
            this.TxtCountryName.ReadOnly = true;
            this.TxtCountryName.Size = new System.Drawing.Size(259, 26);
            this.TxtCountryName.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label6.Location = new System.Drawing.Point(21, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 20);
            this.label6.TabIndex = 2116;
            this.label6.Text = "Country Name :";
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(134)))), ((int)(((byte)(193)))));
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.ForeColor = System.Drawing.Color.White;
            this.CloseBtn.Image = ((System.Drawing.Image)(resources.GetObject("CloseBtn.Image")));
            this.CloseBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CloseBtn.Location = new System.Drawing.Point(158, 32);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(100, 31);
            this.CloseBtn.TabIndex = 6;
            this.CloseBtn.Text = "EXIT";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(134)))), ((int)(((byte)(193)))));
            this.UpdateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateBtn.ForeColor = System.Drawing.Color.White;
            this.UpdateBtn.Image = ((System.Drawing.Image)(resources.GetObject("UpdateBtn.Image")));
            this.UpdateBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UpdateBtn.Location = new System.Drawing.Point(25, 32);
            this.UpdateBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(129, 31);
            this.UpdateBtn.TabIndex = 5;
            this.UpdateBtn.Text = "UPDATE";
            this.UpdateBtn.UseVisualStyleBackColor = false;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // TxtCountryCode
            // 
            this.TxtCountryCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCountryCode.Location = new System.Drawing.Point(161, 224);
            this.TxtCountryCode.Name = "TxtCountryCode";
            this.TxtCountryCode.ReadOnly = true;
            this.TxtCountryCode.Size = new System.Drawing.Size(259, 26);
            this.TxtCountryCode.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(78, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(254, 25);
            this.label3.TabIndex = 2104;
            this.label3.Text = "COUNTRY EIDT FORM";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label4.Location = new System.Drawing.Point(25, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 2120;
            this.label4.Text = "Country Code :";
            // 
            // isCheckedcheckbox
            // 
            this.isCheckedcheckbox.AutoSize = true;
            this.isCheckedcheckbox.Checked = true;
            this.isCheckedcheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isCheckedcheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isCheckedcheckbox.Location = new System.Drawing.Point(161, 324);
            this.isCheckedcheckbox.Name = "isCheckedcheckbox";
            this.isCheckedcheckbox.Size = new System.Drawing.Size(86, 20);
            this.isCheckedcheckbox.TabIndex = 4;
            this.isCheckedcheckbox.Text = "Is Active";
            this.isCheckedcheckbox.UseVisualStyleBackColor = true;
            // 
            // BtnGroupBox
            // 
            this.BtnGroupBox.Location = new System.Drawing.Point(12, 12);
            this.BtnGroupBox.Name = "BtnGroupBox";
            this.BtnGroupBox.Size = new System.Drawing.Size(253, 58);
            this.BtnGroupBox.TabIndex = 2180;
            this.BtnGroupBox.TabStop = false;
            this.BtnGroupBox.Text = "BUTTONS";
            // 
            // FormGroupBox
            // 
            this.FormGroupBox.Controls.Add(this.label3);
            this.FormGroupBox.Location = new System.Drawing.Point(12, 91);
            this.FormGroupBox.Name = "FormGroupBox";
            this.FormGroupBox.Size = new System.Drawing.Size(420, 263);
            this.FormGroupBox.TabIndex = 2181;
            this.FormGroupBox.TabStop = false;
            this.FormGroupBox.Text = "FORM INPUTS";
            // 
            // CountryEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(659, 507);
            this.Controls.Add(this.isCheckedcheckbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CountryCodeBox);
            this.Controls.Add(this.TxtCountryName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.UpdateBtn);
            this.Controls.Add(this.TxtCountryCode);
            this.Controls.Add(this.BtnGroupBox);
            this.Controls.Add(this.FormGroupBox);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.Name = "CountryEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CountryEdit";
            this.Load += new System.EventHandler(this.CountryEdit_Load);
            this.FormGroupBox.ResumeLayout(false);
            this.FormGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox CountryCodeBox;
        private System.Windows.Forms.TextBox TxtCountryName;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button CloseBtn;
        public System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.TextBox TxtCountryCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox isCheckedcheckbox;
        private System.Windows.Forms.GroupBox BtnGroupBox;
        private System.Windows.Forms.GroupBox FormGroupBox;
    }
}