
namespace NEW_ERP.Forms.SaleOrder
{
    partial class SaleOrderEdit
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
            this.FabricDataGrid = new System.Windows.Forms.DataGridView();
            this.ColorSizeDataGrid = new System.Windows.Forms.DataGridView();
            this.ToleranceBox = new System.Windows.Forms.ComboBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtCommission = new System.Windows.Forms.TextBox();
            this.txtFactoryPrice = new System.Windows.Forms.TextBox();
            this.FoldTypeBox = new System.Windows.Forms.ComboBox();
            this.PackingTypeBox = new System.Windows.Forms.ComboBox();
            this.EmbelishmentBox = new System.Windows.Forms.ComboBox();
            this.ETADateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ExFactoryDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.OrderDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ShipModeBox = new System.Windows.Forms.ComboBox();
            this.AgentBox = new System.Windows.Forms.ComboBox();
            this.SaleTypeBox = new System.Windows.Forms.ComboBox();
            this.CategoryBox = new System.Windows.Forms.ComboBox();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.ProductBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CustomerBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SaleOrderIdBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPoNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPlan = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCtnQty = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtSaleOrder = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FabricDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorSizeDataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseBtn
            // 
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.FlatAppearance.BorderSize = 0;
            // 
            // FabricDataGrid
            // 
            this.FabricDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.FabricDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FabricDataGrid.Location = new System.Drawing.Point(24, 19);
            this.FabricDataGrid.Name = "FabricDataGrid";
            this.FabricDataGrid.Size = new System.Drawing.Size(966, 174);
            this.FabricDataGrid.TabIndex = 2234;
            this.FabricDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FabricDataGrid_CellClick_1);
            // 
            // ColorSizeDataGrid
            // 
            this.ColorSizeDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ColorSizeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ColorSizeDataGrid.Location = new System.Drawing.Point(22, 19);
            this.ColorSizeDataGrid.Name = "ColorSizeDataGrid";
            this.ColorSizeDataGrid.Size = new System.Drawing.Size(745, 260);
            this.ColorSizeDataGrid.TabIndex = 2233;
            // 
            // ToleranceBox
            // 
            this.ToleranceBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ToleranceBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ToleranceBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToleranceBox.FormattingEnabled = true;
            this.ToleranceBox.Location = new System.Drawing.Point(531, 251);
            this.ToleranceBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ToleranceBox.Name = "ToleranceBox";
            this.ToleranceBox.Size = new System.Drawing.Size(203, 24);
            this.ToleranceBox.TabIndex = 2232;
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(888, 359);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(203, 26);
            this.txtTotal.TabIndex = 2231;
            // 
            // txtCommission
            // 
            this.txtCommission.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommission.Location = new System.Drawing.Point(888, 321);
            this.txtCommission.Name = "txtCommission";
            this.txtCommission.Size = new System.Drawing.Size(203, 26);
            this.txtCommission.TabIndex = 2230;
            // 
            // txtFactoryPrice
            // 
            this.txtFactoryPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFactoryPrice.Location = new System.Drawing.Point(888, 284);
            this.txtFactoryPrice.Name = "txtFactoryPrice";
            this.txtFactoryPrice.Size = new System.Drawing.Size(203, 26);
            this.txtFactoryPrice.TabIndex = 2229;
            // 
            // FoldTypeBox
            // 
            this.FoldTypeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.FoldTypeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.FoldTypeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FoldTypeBox.FormattingEnabled = true;
            this.FoldTypeBox.Location = new System.Drawing.Point(531, 328);
            this.FoldTypeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FoldTypeBox.Name = "FoldTypeBox";
            this.FoldTypeBox.Size = new System.Drawing.Size(203, 24);
            this.FoldTypeBox.TabIndex = 2228;
            // 
            // PackingTypeBox
            // 
            this.PackingTypeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.PackingTypeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.PackingTypeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PackingTypeBox.FormattingEnabled = true;
            this.PackingTypeBox.Location = new System.Drawing.Point(531, 288);
            this.PackingTypeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PackingTypeBox.Name = "PackingTypeBox";
            this.PackingTypeBox.Size = new System.Drawing.Size(203, 24);
            this.PackingTypeBox.TabIndex = 2227;
            // 
            // EmbelishmentBox
            // 
            this.EmbelishmentBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.EmbelishmentBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.EmbelishmentBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmbelishmentBox.FormattingEnabled = true;
            this.EmbelishmentBox.Location = new System.Drawing.Point(531, 134);
            this.EmbelishmentBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.EmbelishmentBox.Name = "EmbelishmentBox";
            this.EmbelishmentBox.Size = new System.Drawing.Size(203, 24);
            this.EmbelishmentBox.TabIndex = 2226;
            // 
            // ETADateTimePicker
            // 
            this.ETADateTimePicker.Location = new System.Drawing.Point(888, 251);
            this.ETADateTimePicker.Name = "ETADateTimePicker";
            this.ETADateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.ETADateTimePicker.TabIndex = 2225;
            // 
            // ExFactoryDateTimePicker
            // 
            this.ExFactoryDateTimePicker.Location = new System.Drawing.Point(888, 211);
            this.ExFactoryDateTimePicker.Name = "ExFactoryDateTimePicker";
            this.ExFactoryDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.ExFactoryDateTimePicker.TabIndex = 2224;
            // 
            // OrderDateTimePicker
            // 
            this.OrderDateTimePicker.Location = new System.Drawing.Point(888, 170);
            this.OrderDateTimePicker.Name = "OrderDateTimePicker";
            this.OrderDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.OrderDateTimePicker.TabIndex = 2223;
            // 
            // ShipModeBox
            // 
            this.ShipModeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ShipModeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ShipModeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShipModeBox.FormattingEnabled = true;
            this.ShipModeBox.Location = new System.Drawing.Point(531, 212);
            this.ShipModeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ShipModeBox.Name = "ShipModeBox";
            this.ShipModeBox.Size = new System.Drawing.Size(203, 24);
            this.ShipModeBox.TabIndex = 2222;
            // 
            // AgentBox
            // 
            this.AgentBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.AgentBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.AgentBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AgentBox.FormattingEnabled = true;
            this.AgentBox.Location = new System.Drawing.Point(888, 128);
            this.AgentBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AgentBox.Name = "AgentBox";
            this.AgentBox.Size = new System.Drawing.Size(203, 24);
            this.AgentBox.TabIndex = 2221;
            // 
            // SaleTypeBox
            // 
            this.SaleTypeBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SaleTypeBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SaleTypeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaleTypeBox.FormattingEnabled = true;
            this.SaleTypeBox.Location = new System.Drawing.Point(531, 405);
            this.SaleTypeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SaleTypeBox.Name = "SaleTypeBox";
            this.SaleTypeBox.Size = new System.Drawing.Size(203, 24);
            this.SaleTypeBox.TabIndex = 2220;
            // 
            // CategoryBox
            // 
            this.CategoryBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CategoryBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CategoryBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryBox.FormattingEnabled = true;
            this.CategoryBox.Location = new System.Drawing.Point(169, 362);
            this.CategoryBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CategoryBox.Name = "CategoryBox";
            this.CategoryBox.Size = new System.Drawing.Size(203, 24);
            this.CategoryBox.TabIndex = 2219;
            // 
            // txtRange
            // 
            this.txtRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRange.Location = new System.Drawing.Point(169, 399);
            this.txtRange.Name = "txtRange";
            this.txtRange.Size = new System.Drawing.Size(203, 26);
            this.txtRange.TabIndex = 2218;
            // 
            // txtStyle
            // 
            this.txtStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStyle.Location = new System.Drawing.Point(169, 285);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.Size = new System.Drawing.Size(203, 26);
            this.txtStyle.TabIndex = 2217;
            // 
            // ProductBox
            // 
            this.ProductBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ProductBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ProductBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductBox.FormattingEnabled = true;
            this.ProductBox.Location = new System.Drawing.Point(169, 325);
            this.ProductBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProductBox.Name = "ProductBox";
            this.ProductBox.Size = new System.Drawing.Size(203, 24);
            this.ProductBox.TabIndex = 2216;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label1.Location = new System.Drawing.Point(767, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 20);
            this.label1.TabIndex = 2215;
            this.label1.Text = "Commission :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label14.Location = new System.Drawing.Point(822, 362);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 20);
            this.label14.TabIndex = 2214;
            this.label14.Text = "Total :";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label20.Location = new System.Drawing.Point(427, 330);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(97, 20);
            this.label20.TabIndex = 2213;
            this.label20.Text = "Fold Type :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label21.Location = new System.Drawing.Point(762, 287);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(124, 20);
            this.label21.TabIndex = 2212;
            this.label21.Text = "Factory Price :";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label22.Location = new System.Drawing.Point(426, 253);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(98, 20);
            this.label22.TabIndex = 2211;
            this.label22.Text = "Tolerance :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(412, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(338, 25);
            this.label3.TabIndex = 2210;
            this.label3.Text = "SALE ORDER  MASTER FORM";
            // 
            // CustomerBox
            // 
            this.CustomerBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CustomerBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CustomerBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerBox.FormattingEnabled = true;
            this.CustomerBox.Location = new System.Drawing.Point(169, 246);
            this.CustomerBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CustomerBox.Name = "CustomerBox";
            this.CustomerBox.Size = new System.Drawing.Size(203, 24);
            this.CustomerBox.TabIndex = 2202;
            this.CustomerBox.DropDown += new System.EventHandler(this.CustomerBox_DropDown_1);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label13.Location = new System.Drawing.Point(780, 170);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 20);
            this.label13.TabIndex = 2209;
            this.label13.Text = "Order Date :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label15.Location = new System.Drawing.Point(399, 290);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 20);
            this.label15.TabIndex = 2208;
            this.label15.Text = "Packing Type :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label16.Location = new System.Drawing.Point(393, 137);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(132, 20);
            this.label16.TabIndex = 2207;
            this.label16.Text = "Embelishment :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label18.Location = new System.Drawing.Point(742, 211);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(148, 20);
            this.label18.TabIndex = 2206;
            this.label18.Text = "Ex Factory Date :";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label19.Location = new System.Drawing.Point(821, 129);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 20);
            this.label19.TabIndex = 2205;
            this.label19.Text = "Agent :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label8.Location = new System.Drawing.Point(429, 215);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 20);
            this.label8.TabIndex = 2204;
            this.label8.Text = "Ship Mode :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label9.Location = new System.Drawing.Point(782, 250);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 20);
            this.label9.TabIndex = 2203;
            this.label9.Text = "ETA Date :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FabricDataGrid);
            this.groupBox1.Location = new System.Drawing.Point(17, 453);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1006, 205);
            this.groupBox1.TabIndex = 2235;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FABRIC DETAIL";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ColorSizeDataGrid);
            this.groupBox2.Location = new System.Drawing.Point(18, 664);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(779, 294);
            this.groupBox2.TabIndex = 2236;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "COLOR, SIZE AND QUANTITY";
            // 
            // SaleOrderIdBox
            // 
            this.SaleOrderIdBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.SaleOrderIdBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SaleOrderIdBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaleOrderIdBox.FormattingEnabled = true;
            this.SaleOrderIdBox.Location = new System.Drawing.Point(169, 132);
            this.SaleOrderIdBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SaleOrderIdBox.Name = "SaleOrderIdBox";
            this.SaleOrderIdBox.Size = new System.Drawing.Size(203, 24);
            this.SaleOrderIdBox.TabIndex = 2253;
            this.SaleOrderIdBox.DropDown += new System.EventHandler(this.SaleOrderIdBox_DropDown);
            this.SaleOrderIdBox.SelectedIndexChanged += new System.EventHandler(this.SaleOrderIdBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label2.Location = new System.Drawing.Point(34, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 2254;
            this.label2.Text = "Sale Order ID :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label17.Location = new System.Drawing.Point(65, 243);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 20);
            this.label17.TabIndex = 2252;
            this.label17.Text = "Customer :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label5.Location = new System.Drawing.Point(78, 325);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 2247;
            this.label5.Text = "Product :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label10.Location = new System.Drawing.Point(87, 400);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 20);
            this.label10.TabIndex = 2251;
            this.label10.Text = "Range :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label11.Location = new System.Drawing.Point(425, 402);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 20);
            this.label11.TabIndex = 2250;
            this.label11.Text = "Sale Type :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label7.Location = new System.Drawing.Point(100, 285);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 20);
            this.label7.TabIndex = 2248;
            this.label7.Text = "Style :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label6.Location = new System.Drawing.Point(68, 361);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 20);
            this.label6.TabIndex = 2249;
            this.label6.Text = "Category :";
            // 
            // txtPoNo
            // 
            this.txtPoNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPoNo.Location = new System.Drawing.Point(169, 207);
            this.txtPoNo.Name = "txtPoNo";
            this.txtPoNo.Size = new System.Drawing.Size(203, 26);
            this.txtPoNo.TabIndex = 2255;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label4.Location = new System.Drawing.Point(88, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 2256;
            this.label4.Text = "PO NO :";
            // 
            // txtPlan
            // 
            this.txtPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlan.Location = new System.Drawing.Point(531, 171);
            this.txtPlan.Name = "txtPlan";
            this.txtPlan.Size = new System.Drawing.Size(203, 26);
            this.txtPlan.TabIndex = 2257;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label12.Location = new System.Drawing.Point(454, 174);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 20);
            this.label12.TabIndex = 2258;
            this.label12.Text = "PLAN  :";
            // 
            // txtCtnQty
            // 
            this.txtCtnQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCtnQty.Location = new System.Drawing.Point(531, 365);
            this.txtCtnQty.Name = "txtCtnQty";
            this.txtCtnQty.Size = new System.Drawing.Size(203, 26);
            this.txtCtnQty.TabIndex = 2259;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label23.Location = new System.Drawing.Point(435, 368);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(85, 20);
            this.label23.TabIndex = 2260;
            this.label23.Text = "CTN Qty :";
            // 
            // txtSaleOrder
            // 
            this.txtSaleOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaleOrder.Location = new System.Drawing.Point(169, 168);
            this.txtSaleOrder.Name = "txtSaleOrder";
            this.txtSaleOrder.Size = new System.Drawing.Size(203, 26);
            this.txtSaleOrder.TabIndex = 2261;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.label24.Location = new System.Drawing.Point(54, 170);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(105, 20);
            this.label24.TabIndex = 2262;
            this.label24.Text = "Sale Order :";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(17, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1088, 365);
            this.groupBox3.TabIndex = 2236;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " INPUT FIELD ";
            // 
            // SaleOrderEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 972);
            this.Controls.Add(this.txtSaleOrder);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtCtnQty);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtPlan);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtPoNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SaleOrderIdBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ToleranceBox);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtCommission);
            this.Controls.Add(this.txtFactoryPrice);
            this.Controls.Add(this.FoldTypeBox);
            this.Controls.Add(this.PackingTypeBox);
            this.Controls.Add(this.EmbelishmentBox);
            this.Controls.Add(this.ETADateTimePicker);
            this.Controls.Add(this.ExFactoryDateTimePicker);
            this.Controls.Add(this.OrderDateTimePicker);
            this.Controls.Add(this.ShipModeBox);
            this.Controls.Add(this.AgentBox);
            this.Controls.Add(this.SaleTypeBox);
            this.Controls.Add(this.CategoryBox);
            this.Controls.Add(this.txtRange);
            this.Controls.Add(this.txtStyle);
            this.Controls.Add(this.ProductBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CustomerBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Name = "SaleOrderEdit";
            this.Text = "SaleOrderEdit";
            this.Load += new System.EventHandler(this.SaleOrderEdit_Load);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.CustomerBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label22, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ProductBox, 0);
            this.Controls.SetChildIndex(this.txtStyle, 0);
            this.Controls.SetChildIndex(this.txtRange, 0);
            this.Controls.SetChildIndex(this.CategoryBox, 0);
            this.Controls.SetChildIndex(this.SaleTypeBox, 0);
            this.Controls.SetChildIndex(this.AgentBox, 0);
            this.Controls.SetChildIndex(this.ShipModeBox, 0);
            this.Controls.SetChildIndex(this.OrderDateTimePicker, 0);
            this.Controls.SetChildIndex(this.ExFactoryDateTimePicker, 0);
            this.Controls.SetChildIndex(this.ETADateTimePicker, 0);
            this.Controls.SetChildIndex(this.EmbelishmentBox, 0);
            this.Controls.SetChildIndex(this.PackingTypeBox, 0);
            this.Controls.SetChildIndex(this.FoldTypeBox, 0);
            this.Controls.SetChildIndex(this.txtFactoryPrice, 0);
            this.Controls.SetChildIndex(this.txtCommission, 0);
            this.Controls.SetChildIndex(this.txtTotal, 0);
            this.Controls.SetChildIndex(this.ToleranceBox, 0);
            this.Controls.SetChildIndex(this.UpdateBtn, 0);
            this.Controls.SetChildIndex(this.CloseBtn, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.SaleOrderIdBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtPoNo, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.txtPlan, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.txtCtnQty, 0);
            this.Controls.SetChildIndex(this.label24, 0);
            this.Controls.SetChildIndex(this.txtSaleOrder, 0);
            ((System.ComponentModel.ISupportInitialize)(this.FabricDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorSizeDataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView FabricDataGrid;
        private System.Windows.Forms.DataGridView ColorSizeDataGrid;
        public System.Windows.Forms.ComboBox ToleranceBox;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtCommission;
        private System.Windows.Forms.TextBox txtFactoryPrice;
        public System.Windows.Forms.ComboBox FoldTypeBox;
        public System.Windows.Forms.ComboBox PackingTypeBox;
        public System.Windows.Forms.ComboBox EmbelishmentBox;
        private System.Windows.Forms.DateTimePicker ETADateTimePicker;
        private System.Windows.Forms.DateTimePicker ExFactoryDateTimePicker;
        private System.Windows.Forms.DateTimePicker OrderDateTimePicker;
        public System.Windows.Forms.ComboBox ShipModeBox;
        public System.Windows.Forms.ComboBox AgentBox;
        public System.Windows.Forms.ComboBox SaleTypeBox;
        public System.Windows.Forms.ComboBox CategoryBox;
        private System.Windows.Forms.TextBox txtRange;
        private System.Windows.Forms.TextBox txtStyle;
        public System.Windows.Forms.ComboBox ProductBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox CustomerBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ComboBox SaleOrderIdBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPoNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPlan;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCtnQty;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtSaleOrder;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}