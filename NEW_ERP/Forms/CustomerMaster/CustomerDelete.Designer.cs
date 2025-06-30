
namespace NEW_ERP.Forms.CustomerMaster
{
    partial class CustomerDelete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerDelete));
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomerCodeBox = new System.Windows.Forms.ComboBox();
            this.isCheckedcheckbox = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNTN = new System.Windows.Forms.TextBox();
            this.txtGSTNo = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtWhatsapp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CustomerTypeBox = new System.Windows.Forms.ComboBox();
            this.CityBox = new System.Windows.Forms.ComboBox();
            this.CountryBox = new System.Windows.Forms.ComboBox();
            this.BtnGroupBox = new System.Windows.Forms.GroupBox();
            this.FormGroupBox = new System.Windows.Forms.GroupBox();
            this.FormGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(134)))), ((int)(((byte)(193)))));
            this.DeleteBtn.FlatAppearance.BorderSize = 0;
            this.DeleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteBtn.ForeColor = System.Drawing.Color.White;
            this.DeleteBtn.Image = ((System.Drawing.Image)(resources.GetObject("DeleteBtn.Image")));
            this.DeleteBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeleteBtn.Location = new System.Drawing.Point(24, 29);
            this.DeleteBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(125, 31);
            this.DeleteBtn.TabIndex = 2152;
            this.DeleteBtn.Text = "DELETE";
            this.DeleteBtn.UseVisualStyleBackColor = false;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
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
            this.CloseBtn.Location = new System.Drawing.Point(151, 29);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(100, 31);
            this.CloseBtn.TabIndex = 2153;
            this.CloseBtn.Text = "EXIT";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label2.Location = new System.Drawing.Point(10, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 20);
            this.label2.TabIndex = 2234;
            this.label2.Text = "Customer Code :";
            // 
            // CustomerCodeBox
            // 
            this.CustomerCodeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CustomerCodeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CustomerCodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerCodeBox.FormattingEnabled = true;
            this.CustomerCodeBox.Location = new System.Drawing.Point(159, 77);
            this.CustomerCodeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CustomerCodeBox.Name = "CustomerCodeBox";
            this.CustomerCodeBox.Size = new System.Drawing.Size(259, 24);
            this.CustomerCodeBox.TabIndex = 2196;
            this.CustomerCodeBox.SelectedIndexChanged += new System.EventHandler(this.CustomerCodeBox_SelectedIndexChanged);
            // 
            // isCheckedcheckbox
            // 
            this.isCheckedcheckbox.AutoSize = true;
            this.isCheckedcheckbox.Checked = true;
            this.isCheckedcheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isCheckedcheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isCheckedcheckbox.Location = new System.Drawing.Point(159, 466);
            this.isCheckedcheckbox.Name = "isCheckedcheckbox";
            this.isCheckedcheckbox.Size = new System.Drawing.Size(107, 28);
            this.isCheckedcheckbox.TabIndex = 2233;
            this.isCheckedcheckbox.Text = "Is Active";
            this.isCheckedcheckbox.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label13.Location = new System.Drawing.Point(463, 223);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 20);
            this.label13.TabIndex = 2232;
            this.label13.Text = "State :";
            // 
            // txtNTN
            // 
            this.txtNTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNTN.Location = new System.Drawing.Point(533, 415);
            this.txtNTN.Name = "txtNTN";
            this.txtNTN.ReadOnly = true;
            this.txtNTN.Size = new System.Drawing.Size(259, 26);
            this.txtNTN.TabIndex = 2210;
            // 
            // txtGSTNo
            // 
            this.txtGSTNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGSTNo.Location = new System.Drawing.Point(533, 366);
            this.txtGSTNo.Name = "txtGSTNo";
            this.txtGSTNo.ReadOnly = true;
            this.txtGSTNo.Size = new System.Drawing.Size(259, 26);
            this.txtGSTNo.TabIndex = 2209;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label15.Location = new System.Drawing.Point(471, 418);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 20);
            this.label15.TabIndex = 2231;
            this.label15.Text = "NTN :";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(533, 317);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(259, 26);
            this.txtAddress.TabIndex = 2208;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label16.Location = new System.Drawing.Point(447, 368);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 20);
            this.label16.TabIndex = 2230;
            this.label16.Text = "GSTNo :";
            // 
            // txtZipCode
            // 
            this.txtZipCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZipCode.Location = new System.Drawing.Point(533, 268);
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.ReadOnly = true;
            this.txtZipCode.Size = new System.Drawing.Size(259, 26);
            this.txtZipCode.TabIndex = 2207;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label17.Location = new System.Drawing.Point(15, 177);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(139, 20);
            this.label17.TabIndex = 2229;
            this.label17.Text = "Customer Type :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label18.Location = new System.Drawing.Point(436, 271);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(91, 20);
            this.label18.TabIndex = 2228;
            this.label18.Text = "Zip Code :";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label19.Location = new System.Drawing.Point(445, 127);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(81, 20);
            this.label19.TabIndex = 2227;
            this.label19.Text = "Country :";
            // 
            // txtState
            // 
            this.txtState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.Location = new System.Drawing.Point(533, 221);
            this.txtState.Name = "txtState";
            this.txtState.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(259, 26);
            this.txtState.TabIndex = 2206;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label8.Location = new System.Drawing.Point(477, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 20);
            this.label8.TabIndex = 2226;
            this.label8.Text = "City :";
            // 
            // txtWhatsapp
            // 
            this.txtWhatsapp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWhatsapp.Location = new System.Drawing.Point(159, 415);
            this.txtWhatsapp.Name = "txtWhatsapp";
            this.txtWhatsapp.ReadOnly = true;
            this.txtWhatsapp.Size = new System.Drawing.Size(259, 26);
            this.txtWhatsapp.TabIndex = 2203;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label9.Location = new System.Drawing.Point(442, 320);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 20);
            this.label9.TabIndex = 2225;
            this.label9.Text = "Address :";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(159, 368);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(259, 26);
            this.txtEmail.TabIndex = 2202;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label10.Location = new System.Drawing.Point(88, 371);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 20);
            this.label10.TabIndex = 2224;
            this.label10.Text = "Email :";
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(159, 319);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.ReadOnly = true;
            this.txtMobileNo.Size = new System.Drawing.Size(259, 26);
            this.txtMobileNo.TabIndex = 2201;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label11.Location = new System.Drawing.Point(22, 419);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 20);
            this.label11.TabIndex = 2223;
            this.label11.Text = "WhatsApp No :";
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactPerson.Location = new System.Drawing.Point(159, 270);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.ReadOnly = true;
            this.txtContactPerson.Size = new System.Drawing.Size(259, 26);
            this.txtContactPerson.TabIndex = 2200;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label6.Location = new System.Drawing.Point(55, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 20);
            this.label6.TabIndex = 2222;
            this.label6.Text = "Mobile No :";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.Location = new System.Drawing.Point(159, 221);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(259, 26);
            this.txtCustomerName.TabIndex = 2199;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label7.Location = new System.Drawing.Point(15, 273);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 20);
            this.label7.TabIndex = 2221;
            this.label7.Text = "Contact Person:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(8, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 20);
            this.label5.TabIndex = 2220;
            this.label5.Text = "Customer Name :";
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerCode.Location = new System.Drawing.Point(159, 125);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.ReadOnly = true;
            this.txtCustomerCode.Size = new System.Drawing.Size(259, 26);
            this.txtCustomerCode.TabIndex = 2197;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label4.Location = new System.Drawing.Point(15, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 20);
            this.label4.TabIndex = 2219;
            this.label4.Text = "Customer Code :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(299, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(336, 25);
            this.label3.TabIndex = 2215;
            this.label3.Text = "CUSTOMER MASTER DELETE";
            // 
            // CustomerTypeBox
            // 
            this.CustomerTypeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CustomerTypeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CustomerTypeBox.Enabled = false;
            this.CustomerTypeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerTypeBox.FormattingEnabled = true;
            this.CustomerTypeBox.Location = new System.Drawing.Point(159, 176);
            this.CustomerTypeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CustomerTypeBox.Name = "CustomerTypeBox";
            this.CustomerTypeBox.Size = new System.Drawing.Size(259, 24);
            this.CustomerTypeBox.TabIndex = 2235;
            // 
            // CityBox
            // 
            this.CityBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CityBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CityBox.Enabled = false;
            this.CityBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CityBox.FormattingEnabled = true;
            this.CityBox.Location = new System.Drawing.Point(533, 177);
            this.CityBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CityBox.Name = "CityBox";
            this.CityBox.Size = new System.Drawing.Size(259, 24);
            this.CityBox.TabIndex = 2236;
            // 
            // CountryBox
            // 
            this.CountryBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CountryBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CountryBox.Enabled = false;
            this.CountryBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountryBox.FormattingEnabled = true;
            this.CountryBox.Location = new System.Drawing.Point(533, 127);
            this.CountryBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CountryBox.Name = "CountryBox";
            this.CountryBox.Size = new System.Drawing.Size(259, 24);
            this.CountryBox.TabIndex = 2237;
            // 
            // BtnGroupBox
            // 
            this.BtnGroupBox.Location = new System.Drawing.Point(16, 12);
            this.BtnGroupBox.Name = "BtnGroupBox";
            this.BtnGroupBox.Size = new System.Drawing.Size(248, 58);
            this.BtnGroupBox.TabIndex = 2238;
            this.BtnGroupBox.TabStop = false;
            this.BtnGroupBox.Text = "BUTTONS";
            // 
            // FormGroupBox
            // 
            this.FormGroupBox.Controls.Add(this.label3);
            this.FormGroupBox.Controls.Add(this.CountryBox);
            this.FormGroupBox.Controls.Add(this.CustomerCodeBox);
            this.FormGroupBox.Controls.Add(this.CityBox);
            this.FormGroupBox.Controls.Add(this.label4);
            this.FormGroupBox.Controls.Add(this.CustomerTypeBox);
            this.FormGroupBox.Controls.Add(this.txtCustomerCode);
            this.FormGroupBox.Controls.Add(this.label2);
            this.FormGroupBox.Controls.Add(this.label5);
            this.FormGroupBox.Controls.Add(this.label7);
            this.FormGroupBox.Controls.Add(this.isCheckedcheckbox);
            this.FormGroupBox.Controls.Add(this.txtCustomerName);
            this.FormGroupBox.Controls.Add(this.label13);
            this.FormGroupBox.Controls.Add(this.label6);
            this.FormGroupBox.Controls.Add(this.txtNTN);
            this.FormGroupBox.Controls.Add(this.txtContactPerson);
            this.FormGroupBox.Controls.Add(this.txtGSTNo);
            this.FormGroupBox.Controls.Add(this.label11);
            this.FormGroupBox.Controls.Add(this.label15);
            this.FormGroupBox.Controls.Add(this.txtMobileNo);
            this.FormGroupBox.Controls.Add(this.txtAddress);
            this.FormGroupBox.Controls.Add(this.label10);
            this.FormGroupBox.Controls.Add(this.label16);
            this.FormGroupBox.Controls.Add(this.txtEmail);
            this.FormGroupBox.Controls.Add(this.txtZipCode);
            this.FormGroupBox.Controls.Add(this.label9);
            this.FormGroupBox.Controls.Add(this.label17);
            this.FormGroupBox.Controls.Add(this.txtWhatsapp);
            this.FormGroupBox.Controls.Add(this.label18);
            this.FormGroupBox.Controls.Add(this.label8);
            this.FormGroupBox.Controls.Add(this.label19);
            this.FormGroupBox.Controls.Add(this.txtState);
            this.FormGroupBox.Location = new System.Drawing.Point(12, 76);
            this.FormGroupBox.Name = "FormGroupBox";
            this.FormGroupBox.Size = new System.Drawing.Size(863, 506);
            this.FormGroupBox.TabIndex = 2239;
            this.FormGroupBox.TabStop = false;
            this.FormGroupBox.Text = "FORM INPUTS";
            // 
            // CustomerDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(935, 626);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.BtnGroupBox);
            this.Controls.Add(this.FormGroupBox);
            this.Name = "CustomerDelete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerDelete";
            this.Load += new System.EventHandler(this.CustomerDelete_Load);
            this.FormGroupBox.ResumeLayout(false);
            this.FormGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button DeleteBtn;
        public System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox CustomerCodeBox;
        private System.Windows.Forms.CheckBox isCheckedcheckbox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNTN;
        private System.Windows.Forms.TextBox txtGSTNo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWhatsapp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtContactPerson;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox CustomerTypeBox;
        public System.Windows.Forms.ComboBox CityBox;
        public System.Windows.Forms.ComboBox CountryBox;
        private System.Windows.Forms.GroupBox BtnGroupBox;
        private System.Windows.Forms.GroupBox FormGroupBox;
    }
}