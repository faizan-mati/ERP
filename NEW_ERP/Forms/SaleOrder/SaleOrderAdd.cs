using NEW_ERP.GernalClasses;
using NEW_ERP.Template;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SaleOrder
{
    public partial class SaleOrderAdd : AddFormTemplate
    {
        #region Private Fields and Properties
        private bool isCustomerLoaded = false;
        private bool isRangeLoaded = false;
        private bool isCTNTypeLoaded = false;
        private bool isUnitNameLoaded = false;
        private bool isProductLoaded = false;
        private bool isCategoryLoaded = false;
        private bool isShipModeLoaded = false;
        private bool isEmblishmenLoaded = false;
        private bool isPackingTypeLoaded = false;
        private bool isFoldTypeLoaded = false;
        private bool isSaleTypeLoaded = false;
        private bool isAgentLoaded = false;
        private bool isStatusCodeLoaded = false;
        private bool isToleranceLoaded = false;

        private int saleOrderId;
        private bool isFromViewAll;
        private Timer delayTimer;
        #endregion

        #region Constructor and Form Events
        //======================================= Constructor =======================================
        public SaleOrderAdd(int saleOrderId, bool isFromViewAll)
        {
            InitializeComponent();
            this.saleOrderId = saleOrderId;
            this.isFromViewAll = isFromViewAll;
        }

        //======================================= Form Load – Add vs Edit =======================================
        private void SaleOrderAdd_Load(object sender, EventArgs e)
        {
            AttachDropdownEvents();
            if (isFromViewAll) InitializeEditMode();
            else InitializeAddMode();
        }
        #endregion

        #region Initialization Methods
        //======================================= Initialize EDIT Mode =======================================
        private void InitializeEditMode()
        {
            delayTimer = new Timer { Interval = 500 };
            delayTimer.Tick += DelayTimer_Tick;
            delayTimer.Start();
            SetFormEditable(false);
            SubmitBtn.Enabled = false;
            FabricDataGridShowEdit();
            ColorSizeDataGridShowEdit();
        }

        //======================================= Initialize ADD Mode =======================================
        private void InitializeAddMode()
        {
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;
            ShowNextSaleOrderId();
            FabricDataGridShow();
            ColorSizeDataGridShow();
        }

        //======================================= Attach Dropdown Events =======================================
        private void AttachDropdownEvents()
        {
            UnitNameBox.DropDown += (s, e) => LoadUnitNameDropdown();
            RangeBox.DropDown += (s, e) => LoadRangeDropdown();
            CTNTypeBox.DropDown += (s, e) => LoadCTNTypeDropdown();
            CustomerBox.DropDown += (s, e) => LoadCustomerDropdown();
            ProductBox.DropDown += (s, e) => LoadProductDropdown();
            CategoryBox.DropDown += (s, e) => LoadCategoryDropdown();
            EmbelishmentBox.DropDown += (s, e) => LoadEmbDropdown();
            ShipModeBox.DropDown += (s, e) => LoadShipModeDropdown();
            PackingTypeBox.DropDown += (s, e) => LoadPackingTypeDropdown();
            FoldTypeBox.DropDown += (s, e) => LoadFoldTypeDropdown();
            SaleTypeBox.DropDown += (s, e) => LoadSaleTypeDropdown();
            AgentBox.DropDown += (s, e) => LoadAgentDropdown();
            StatusCodeBox.DropDown += (s, e) => LoadStatusCodeDropdown();
            ToleranceBox.DropDown += (s, e) => LoadToleranceDropdown();
        }
        #endregion

        #region DataGrid Configuration Methods
        //======================================= Fabric Grid – ADD Mode =======================================
        public void FabricDataGridShow()
        {
            FabricDataGrid.Columns.Clear();
            var fabricColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "FABRIC",
                Name = "FABRIC",
                Width = 250,
                FlatStyle = FlatStyle.Flat
            };
            FabricDataGrid.Columns.Add(fabricColumn);
            FabricDataGrid.Columns.Add("TYPE", "TYPE");
            FabricDataGrid.Columns.Add("GSM", "GSM");
            FabricDataGrid.Columns.Add("WIDTH", "WIDTH");
            FabricDataGrid.Columns.Add("DIA", "DIA");
            FabricDataGrid.Columns.Add("GAUGE", "GAUGE");
            FabricDataGrid.Columns.Add("SHIRNK", "SHIRNK");
            FabricDataGrid.Columns.Add("STITCH LENGTH", "STITCH LENGTH");
            FabricDataGrid.DefaultCellStyle.Font = new Font("Arial", 11);
            FabricDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            FabricDataGrid.Columns[1].Width = 150;
            FabricDataGrid.EditingControlShowing += FabricDataGrid_EditingControlShowing;
            FabricDataGrid.CellClick += FabricDataGrid_CellClick;
        }

        //======================================= Fabric Grid – EDIT Mode =======================================
        public void FabricDataGridShowEdit()
        {
            FabricDataGridShow();
            FabricDataGrid.Columns.Add("PRE FABRIC", "PRE FABRIC");
            FabricDataGrid.Columns["PRE FABRIC"].Visible = false;
        }

        //======================================= Color/Size Grid – ADD Mode =======================================
        public void ColorSizeDataGridShow()
        {
            ColorSizeDataGrid.Columns.Clear();
            var colorColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "COLOR",
                Name = "COLOR",
                Width = 250,
                FlatStyle = FlatStyle.Flat
            };
            var sizeColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "SIZE",
                Name = "SIZE",
                Width = 150,
                FlatStyle = FlatStyle.Flat
            };
            ColorSizeDataGrid.Columns.Add(colorColumn);
            ColorSizeDataGrid.Columns.Add(sizeColumn);
            ColorSizeDataGrid.Columns.Add("QUANTITY", "QUANTITY");
            ColorSizeDataGrid.Columns.Add("UNIT PRICE", "UNIT PRICE");
            ColorSizeDataGrid.Columns.Add("TOAL PRICE", "TOAL PRICE");
            ColorSizeDataGrid.DefaultCellStyle.Font = new Font("Arial", 11);
            ColorSizeDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            ColorSizeDataGrid.EditingControlShowing += ColorSizeDataGrid_EditingControlShowing;
            ColorSizeDataGrid.CellClick += ColorSizeDataGrid_CellClick;
        }

        //======================================= Color/Size Grid – EDIT Mode =======================================
        public void ColorSizeDataGridShowEdit()
        {
            ColorSizeDataGridShow();
            ColorSizeDataGrid.Columns.Add("PRE COLOR", "PRE COLOR");
            ColorSizeDataGrid.Columns.Add("PRE SIZE", "PRE SIZE");
            ColorSizeDataGrid.Columns["PRE COLOR"].Visible = false;
            ColorSizeDataGrid.Columns["PRE SIZE"].Visible = false;
        }
        #endregion

        #region DataGrid Event Handlers
        //======================================= Fabric Grid – Auto-Complete =======================================
        private void FabricDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (FabricDataGrid.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox comboBox)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }

        //======================================= Fabric Grid – Load Fabric List =======================================
        private void FabricDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                var comboCol = FabricDataGrid.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
                if (comboCol != null && comboCol.DataSource == null)
                {
                    comboCol.DataSource = GetFabricNames();
                    comboCol.DisplayMember = "FabricName";
                    comboCol.ValueMember = "FabricID";
                }
            }
        }

        //======================================= Color/Size Grid – Auto-Complete =======================================
        private void ColorSizeDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((ColorSizeDataGrid.CurrentCell.ColumnIndex == 0 || ColorSizeDataGrid.CurrentCell.ColumnIndex == 1)
                && e.Control is ComboBox comboBox)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }

        //======================================= Color/Size Grid – Load Lists =======================================
        private void ColorSizeDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var comboCol = ColorSizeDataGrid.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
                if (comboCol != null && comboCol.DataSource == null)
                {
                    switch (e.ColumnIndex)
                    {
                        case 0:
                            comboCol.DataSource = GetColorNames();
                            comboCol.DisplayMember = "ColorName";
                            comboCol.ValueMember = "ColorID";
                            break;
                        case 1:
                            comboCol.DataSource = GetSizeNames();
                            comboCol.DisplayMember = "SizeName";
                            comboCol.ValueMember = "SizeID";
                            break;
                    }
                }
            }
        }
        #endregion

        #region Data Loading Methods
        //======================================= Generate Next SO-ID =======================================
        private void ShowNextSaleOrderId()
        {
            const string prefix = "AA-";
            int nextId = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    con.Open();

                    while (true)
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(CAST(SUBSTRING(SaleOrderNo,4,LEN(SaleOrderNo)) AS INT)),0)+1 FROM SaleOrderMaster WHERE SaleOrderNo LIKE @Prefix", con))
                        {
                            cmd.Parameters.AddWithValue("@Prefix", prefix + "%");
                            nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        string candidate = prefix + nextId.ToString("D5");

                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM SaleOrderMaster WHERE SaleOrderNo = @No", con))
                        {
                            cmd.Parameters.AddWithValue("@No", candidate);
                            if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                            {
                                txtSaleOrder.Text = candidate;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating SO-ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSaleOrder.Text = prefix + "00000";
            }
        }

        //======================================= Load Fabric Names =======================================
        private DataTable GetFabricNames()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("SELECT FabricID, FabricName FROM FabricMaster", con))
                {
                    con.Open();
                    new SqlDataAdapter(cmd).Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading fabrics: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        //======================================= Load Color Names =======================================
        private DataTable GetColorNames()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("SELECT ColorID, ColorName FROM ColorMaster", con))
                {
                    con.Open();
                    new SqlDataAdapter(cmd).Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading colors: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        //======================================= Load Size Names =======================================
        private DataTable GetSizeNames()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("SELECT SizeID, SizeName FROM SizeMaster", con))
                {
                    con.Open();
                    new SqlDataAdapter(cmd).Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sizes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        //======================================= Load Master Record =======================================
        private void ShowSaleOrder(int saleOrderId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(@"
    SELECT [unitNameID], [CustomerID], [ProductID], [Style], [CategoryID], [RangeID], [SaleTypeID], [AgentID], 
           [ShipModeID], [OrderDate], [ExFactoryDate], [ETADate], [EmbellishmentID], 
           [PackingTypeID], [FoldTypeID], [FactoryPrice], [Commission], [Total], [StatusCode],
           [CustomerTolerance], [SaleOrderNo], [PoNo], [SaleOrderPlan], [CTNTypeID], [CtnQty],
           [GrandTotal] 
    FROM [SaleOrderMaster]
    WHERE [SaleOrderID] = @SaleOrderId AND StatusCode IN (1, 2)", con))

                {
                    cmd.Parameters.AddWithValue("@SaleOrderId", saleOrderId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            LoadSelectedItemOnly(CustomerBox, "SELECT CustomerID, CustomerName FROM CustomerMaster WHERE CustomerID = @ID", "CustomerName", "CustomerID", reader["CustomerID"]);
                            LoadSelectedItemOnly(UnitNameBox, "SELECT UnitNameID, UnitName FROM UnitNameMaster WHERE UnitNameID = @ID", "UnitName", "UnitNameID", reader["UnitNameID"]);
                            LoadSelectedItemOnly(RangeBox, "SELECT RangeID, RangeName FROM RangeMaster WHERE RangeID = @ID", "RangeName", "RangeID", reader["RangeID"]);
                            LoadSelectedItemOnly(CTNTypeBox, "SELECT CTNTypeID, CTNTypeName FROM CTNTypeMaster WHERE CTNTypeID = @ID", "CTNTypeName", "CTNTypeID", reader["CTNTypeID"]);
                            LoadSelectedItemOnly(ProductBox, "SELECT ProductCode, ProductShortName FROM ItemMaster WHERE ProductCode = @ID", "ProductShortName", "ProductCode", reader["ProductID"]);
                            LoadSelectedItemOnly(CategoryBox, "SELECT CategoryID, CategoryName FROM CategoryMaster WHERE CategoryID = @ID", "CategoryName", "CategoryID", reader["CategoryID"]);
                            LoadSelectedItemOnly(SaleTypeBox, "SELECT SaleTypeID, SaleType FROM SaleTypeMaster WHERE SaleTypeID = @ID", "SaleType", "SaleTypeID", reader["SaleTypeID"]);
                            LoadSelectedItemOnly(AgentBox, "SELECT AgentID, AgentName FROM AgentMaster WHERE AgentID = @ID", "AgentName", "AgentID", reader["AgentID"]);
                            LoadSelectedItemOnly(ShipModeBox, "SELECT ShipModeID, ShipModeName FROM ShipmentModeMaster WHERE ShipModeID = @ID", "ShipModeName", "ShipModeID", reader["ShipModeID"]);
                            LoadSelectedItemOnly(EmbelishmentBox, "SELECT EmbellishmentID, EmbellishmentName FROM EmbellishmentMaster WHERE EmbellishmentID = @ID", "EmbellishmentName", "EmbellishmentID", reader["EmbellishmentID"]);
                            LoadSelectedItemOnly(PackingTypeBox, "SELECT PackingTypeID, PackingType FROM PackingTypeMaster WHERE PackingTypeID = @ID", "PackingType", "PackingTypeID", reader["PackingTypeID"]);
                            LoadSelectedItemOnly(FoldTypeBox, "SELECT FoldTypeID, FoldTypeName FROM FoldTypeMaster WHERE FoldTypeID = @ID", "FoldTypeName", "FoldTypeID", reader["FoldTypeID"]);
                            LoadSelectedItemOnly(ToleranceBox, "SELECT ToleranceID, TolerancePercent FROM ToleranceMaster WHERE ToleranceID = @ID", "TolerancePercent", "ToleranceID", reader["CustomerTolerance"]);
                            LoadSelectedItemOnly(StatusCodeBox, "SELECT StatusId, StatusCode FROM Status WHERE StatusId = @ID", "StatusCode", "StatusId", reader["StatusCode"]);

                            txtStyle.Text = reader["Style"].ToString();
                            txtPoNo.Text = reader["PoNo"].ToString();
                            txtPlan.Text = reader["SaleOrderPlan"].ToString();
                            txtCtnQty.Text = reader["CtnQty"].ToString();
                            txtFactoryPrice.Text = reader["FactoryPrice"].ToString();
                            txtCommission.Text = reader["Commission"].ToString();
                            txtTotal.Text = reader["Total"].ToString();
                            txtSaleOrder.Text = reader["SaleOrderNo"].ToString();
                             
                            TotalSumLabel.Text = reader["GrandTotal"].ToString();

                            OrderDateTimePicker.Value = reader["OrderDate"] != DBNull.Value ? Convert.ToDateTime(reader["OrderDate"]) : DateTime.Now;
                            ExFactoryDateTimePicker.Value = reader["ExFactoryDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExFactoryDate"]) : DateTime.Now;
                            ETADateTimePicker.Value = reader["ETADate"] != DBNull.Value ? Convert.ToDateTime(reader["ETADate"]) : DateTime.Now;
                        }
                        else
                        {
                            MessageBox.Show("No data found for the selected sale order.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sale order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Load Fabric Lines =======================================
        private void LoadSaleOrderFabricData(int saleOrderId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT sof.FabricID, fm.FabricName, sof.Type, sof.GSM, sof.Width, 
                           sof.Dia, sof.Gauge, sof.ShrinkPercent, sof.StitchLength
                    FROM SaleOrderFabric sof
                    INNER JOIN FabricMaster fm ON sof.FabricID = fm.FabricID
                    WHERE sof.SaleOrderID = @SaleOrderId AND sof.StatusCode IN (1, 2)", con))
                {
                    cmd.Parameters.AddWithValue("@SaleOrderId", saleOrderId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        FabricDataGrid.Rows.Clear();
                        while (reader.Read())
                        {
                            int rowIndex = FabricDataGrid.Rows.Add();
                            DataGridViewRow row = FabricDataGrid.Rows[rowIndex];

                            var fabricColumn = FabricDataGrid.Columns["FABRIC"] as DataGridViewComboBoxColumn;
                            if (fabricColumn.DataSource == null)
                            {
                                fabricColumn.DataSource = GetFabricNames();
                                fabricColumn.DisplayMember = "FabricName";
                                fabricColumn.ValueMember = "FabricID";
                            }

                            row.Cells["FABRIC"].Value = reader["FabricID"];
                            row.Cells["TYPE"].Value = reader["Type"].ToString();
                            row.Cells["GSM"].Value = reader["GSM"];
                            row.Cells["WIDTH"].Value = reader["Width"];
                            row.Cells["DIA"].Value = reader["Dia"];
                            row.Cells["GAUGE"].Value = reader["Gauge"].ToString();
                            row.Cells["SHIRNK"].Value = reader["ShrinkPercent"];
                            row.Cells["STITCH LENGTH"].Value = reader["StitchLength"].ToString();
                            row.Cells["PRE FABRIC"].Value = reader["FabricID"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading fabric data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Load Color/Size Lines =======================================
        private void LoadSaleOrderDetailData(int saleOrderId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(@"
                   SELECT 
                    sod.ColorID, cm.ColorName, 
                    sod.SizeID, sm.SizeName, 
                    sod.Quantity, sod.UnitPrice, sod.TotalPrice
                FROM SaleOrderDetails sod
                INNER JOIN ColorMaster cm ON sod.ColorID = cm.ColorID
                INNER JOIN SizeMaster sm ON sod.SizeID = sm.SizeID
                WHERE sod.SaleOrderID = @SaleOrderId 
                  AND sod.StatusCode IN (1, 2)
                ", con))
                {
                    cmd.Parameters.AddWithValue("@SaleOrderId", saleOrderId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ColorSizeDataGrid.Rows.Clear();
                        while (reader.Read())
                        {
                            int rowIndex = ColorSizeDataGrid.Rows.Add();
                            DataGridViewRow row = ColorSizeDataGrid.Rows[rowIndex];

                            var colorColumn = ColorSizeDataGrid.Columns["COLOR"] as DataGridViewComboBoxColumn;
                            if (colorColumn.DataSource == null)
                            {
                                colorColumn.DataSource = GetColorNames();
                                colorColumn.DisplayMember = "ColorName";
                                colorColumn.ValueMember = "ColorID";
                            }

                            var sizeColumn = ColorSizeDataGrid.Columns["SIZE"] as DataGridViewComboBoxColumn;
                            if (sizeColumn.DataSource == null)
                            {
                                sizeColumn.DataSource = GetSizeNames();
                                sizeColumn.DisplayMember = "SizeName";
                                sizeColumn.ValueMember = "SizeID";
                            }

                            row.Cells["COLOR"].Value = reader["ColorID"];
                            row.Cells["SIZE"].Value = reader["SizeID"];
                            row.Cells["QUANTITY"].Value = reader["Quantity"];
                            row.Cells["UNIT PRICE"].Value = reader["UnitPrice"];
                            row.Cells["TOAL PRICE"].Value = reader["TotalPrice"];
                            row.Cells["PRE COLOR"].Value = reader["ColorID"];
                            row.Cells["PRE SIZE"].Value = reader["SizeID"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading detail data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Delay Timer Tick =======================================
        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            delayTimer.Stop();
            delayTimer.Tick -= DelayTimer_Tick;
            ShowSaleOrder(saleOrderId);
            LoadSaleOrderFabricData(saleOrderId);
            LoadSaleOrderDetailData(saleOrderId);
        }
        #endregion

        #region Dropdown Loading Methods
        //======================================= Generic Load Dropdown =======================================
        private void LoadDropdown(ComboBox box, ref bool isLoaded, string query, string displayMember, string valueMember)
        {
            if (isLoaded) return;
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    box.DataSource = dt;
                    box.DisplayMember = displayMember;
                    box.ValueMember = valueMember;
                    isLoaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading {displayMember}: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Load Single Selected Item =======================================
        private void LoadSelectedItemOnly(ComboBox box, string query, string displayMember, string valueMember, object value)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", value);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    box.DataSource = dt;
                    box.DisplayMember = displayMember;
                    box.ValueMember = valueMember;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting {displayMember}: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Load UnitName =======================================
        private void LoadUnitNameDropdown() => LoadDropdown(UnitNameBox, ref isUnitNameLoaded, "select UnitName, UnitNameID from UnitNameMaster", "UnitName", "UnitNameID");
        //======================================= Load CTN Type =======================================
        private void LoadCTNTypeDropdown() => LoadDropdown(CTNTypeBox, ref isCTNTypeLoaded, "select CTNTypeName, CTNTypeID from CTNTypeMaster", "CTNTypeName", "CTNTypeID");
        //======================================= Load Range =======================================
        private void LoadRangeDropdown() => LoadDropdown(RangeBox, ref isRangeLoaded, "select RangeName, RangeID from RangeMaster", "RangeName", "RangeID");
        //======================================= Load Customer =======================================
        private void LoadCustomerDropdown() => LoadDropdown(CustomerBox, ref isCustomerLoaded, "SELECT CustomerID, CustomerName FROM CustomerMaster", "CustomerName", "CustomerID");
        //======================================= Load Product =======================================
        private void LoadProductDropdown() => LoadDropdown(ProductBox, ref isProductLoaded, "SELECT ProductCode, ProductShortName FROM ItemMaster", "ProductShortName", "ProductCode");
        //======================================= Load Category =======================================
        private void LoadCategoryDropdown() => LoadDropdown(CategoryBox, ref isCategoryLoaded, "SELECT CategoryID, CategoryName FROM CategoryMaster", "CategoryName", "CategoryID");
        //======================================= Load Embellishment =======================================
        private void LoadEmbDropdown() => LoadDropdown(EmbelishmentBox, ref isEmblishmenLoaded, "SELECT EmbellishmentID, EmbellishmentName FROM EmbellishmentMaster", "EmbellishmentName", "EmbellishmentID");
        //======================================= Load Ship Mode =======================================
        private void LoadShipModeDropdown() => LoadDropdown(ShipModeBox, ref isShipModeLoaded, "SELECT ShipModeID, ShipModeName FROM ShipmentModeMaster", "ShipModeName", "ShipModeID");
        //======================================= Load Packing Type =======================================
        private void LoadPackingTypeDropdown() => LoadDropdown(PackingTypeBox, ref isPackingTypeLoaded, "SELECT PackingTypeID, PackingType FROM PackingTypeMaster", "PackingType", "PackingTypeID");
        //======================================= Load Fold Type =======================================
        private void LoadFoldTypeDropdown() => LoadDropdown(FoldTypeBox, ref isFoldTypeLoaded, "SELECT FoldTypeID, FoldTypeName FROM FoldTypeMaster", "FoldTypeName", "FoldTypeID");
        //======================================= Load Sale Type =======================================
        private void LoadSaleTypeDropdown() => LoadDropdown(SaleTypeBox, ref isSaleTypeLoaded, "SELECT SaleTypeID, SaleType FROM SaleTypeMaster", "SaleType", "SaleTypeID");
        //======================================= Load Agent =======================================
        private void LoadAgentDropdown() => LoadDropdown(AgentBox, ref isAgentLoaded, "SELECT AgentID, AgentName FROM AgentMaster", "AgentName", "AgentID");
        //======================================= Status Code  =======================================
        private void LoadStatusCodeDropdown() => LoadDropdown(StatusCodeBox, ref isStatusCodeLoaded, "SELECT StatusId, StatusCode FROM Status", "StatusCode", "StatusId");
        //======================================= Load Tolerance =======================================
        private void LoadToleranceDropdown() => LoadDropdown(ToleranceBox, ref isToleranceLoaded, "SELECT ToleranceID, TolerancePercent FROM ToleranceMaster", "TolerancePercent", "ToleranceID");
        #endregion

        #region Utility Methods
        //======================================= Get Nullable Value – Combo =======================================
        private object GetNullableValue(ComboBox comboBox)
        {
            return comboBox.SelectedItem != null ? comboBox.SelectedValue : DBNull.Value;
        }

        //======================================= Get Nullable Value – Text =======================================
        private object GetNullableText(TextBox textBox, bool convertToInt = false, bool convertToDecimal = false)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text)) return DBNull.Value;
            try
            {
                if (convertToInt) return Convert.ToInt32(textBox.Text);
                if (convertToDecimal) return Convert.ToDecimal(textBox.Text);
                return textBox.Text.Trim();
            }
            catch { return DBNull.Value; }
        }

        //======================================= Get Nullable Value – Cell =======================================
        private object GetCellValue(DataGridViewRow row, string columnName, bool isDecimal = false)
        {
            var val = row.Cells[columnName].Value;
            if (val == null || string.IsNullOrWhiteSpace(val.ToString())) return DBNull.Value;
            try { return isDecimal ? Convert.ToDecimal(val) : Convert.ToInt32(val); }
            catch { return DBNull.Value; }
        }

        //======================================= Toggle Form Editable =======================================
        private void SetFormEditable(bool enable)
        {
            UnitNameBox.Enabled = enable;
            CustomerBox.Enabled = enable;
            RangeBox.Enabled = enable;
            CTNTypeBox.Enabled = enable;
            ProductBox.Enabled = enable;
            txtStyle.ReadOnly = !enable;
            CategoryBox.Enabled = enable;
            SaleTypeBox.Enabled = enable;
            AgentBox.Enabled = enable;
            StatusCodeBox.Enabled = enable;
            ShipModeBox.Enabled = enable;
            OrderDateTimePicker.Enabled = enable;
            ExFactoryDateTimePicker.Enabled = enable;
            ETADateTimePicker.Enabled = enable;
            EmbelishmentBox.Enabled = enable;
            PackingTypeBox.Enabled = enable;
            FoldTypeBox.Enabled = enable;
            txtFactoryPrice.ReadOnly = !enable;
            txtCommission.ReadOnly = !enable;
            txtTotal.ReadOnly = !enable;
            ToleranceBox.Enabled = enable;
            txtSaleOrder.ReadOnly = true;
            txtPlan.ReadOnly = !enable;
            txtPoNo.ReadOnly = !enable;
            txtCtnQty.ReadOnly = !enable;
        }

        //======================================= Validation =======================================
        public bool Validation()
        {
            if (string.IsNullOrWhiteSpace(txtSaleOrder.Text) ||
                string.IsNullOrWhiteSpace(txtPoNo.Text) ||
                string.IsNullOrWhiteSpace(txtPlan.Text))
            {
                MessageBox.Show("Please fill all required fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (UnitNameBox.SelectedIndex == -1 ||
                CustomerBox.SelectedIndex == -1 ||
                ShipModeBox.SelectedIndex == -1 ||
                ProductBox.SelectedIndex == -1 ||
                StatusCodeBox.SelectedIndex == -1 ||
                CategoryBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select all required dropdown values", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (FabricDataGrid.Rows.Count == 0 ||
                FabricDataGrid.Rows[0].Cells["FABRIC"].Value == null ||
                string.IsNullOrWhiteSpace(FabricDataGrid.Rows[0].Cells["FABRIC"].Value.ToString()))
            {
                MessageBox.Show("Please add at least one fabric entry", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (ColorSizeDataGrid.Rows.Count == 0 ||
                ColorSizeDataGrid.Rows[0].Cells["COLOR"].Value == null ||
                ColorSizeDataGrid.Rows[0].Cells["SIZE"].Value == null ||
                string.IsNullOrWhiteSpace(ColorSizeDataGrid.Rows[0].Cells["COLOR"].Value.ToString()))
            {
                MessageBox.Show("Please add at least one color/size entry", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        //======================================= Reset Form Controls =======================================
        public void ResetFormControls()
        {
            ShowNextSaleOrderId();
            txtStyle.Clear();
            txtFactoryPrice.Clear();
            txtCommission.Clear();
            txtTotal.Clear();
            txtPoNo.Clear();
            txtPlan.Clear();
            txtCtnQty.Clear();

            UnitNameBox.SelectedIndex = -1;
            CustomerBox.SelectedIndex = -1;
            RangeBox.SelectedIndex = -1;
            CTNTypeBox.SelectedIndex = -1;
            ProductBox.SelectedIndex = -1;
            CategoryBox.SelectedIndex = -1;
            SaleTypeBox.SelectedIndex = -1;
            ShipModeBox.SelectedIndex = -1;
            AgentBox.SelectedIndex = -1;
            StatusCodeBox.SelectedIndex = -1;
            PackingTypeBox.SelectedIndex = -1;
            EmbelishmentBox.SelectedIndex = -1;
            FoldTypeBox.SelectedIndex = -1;
            ToleranceBox.SelectedIndex = -1;

            OrderDateTimePicker.Value = DateTime.Now;
            ExFactoryDateTimePicker.Value = DateTime.Now;
            ETADateTimePicker.Value = DateTime.Now;

            FabricDataGrid.Rows.Clear();
            ColorSizeDataGrid.Rows.Clear();

            isUnitNameLoaded = false;
            isCustomerLoaded = false;
            isCTNTypeLoaded = false;
            isRangeLoaded = false;
            isProductLoaded = false;
            isCategoryLoaded = false;
            isShipModeLoaded = false;
            isEmblishmenLoaded = false;
            isPackingTypeLoaded = false;
            isFoldTypeLoaded = false;
            isSaleTypeLoaded = false;
            isAgentLoaded = false;
            isToleranceLoaded = false;
        }
        #endregion

        #region Database Operations
        //======================================= Insert Master Record =======================================
        public int InsertSaleOrderMaster()
        {
            int newSaleOrderId = 0;
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertSaleOrderMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SaleOrderNo", txtSaleOrder.Text.Trim());
                        cmd.Parameters.AddWithValue("@CustomerID", GetNullableValue(CustomerBox));
                        cmd.Parameters.AddWithValue("@RangeID", GetNullableValue(RangeBox));
                        cmd.Parameters.AddWithValue("@UnitNameID", GetNullableValue(UnitNameBox));
                        cmd.Parameters.AddWithValue("@CTNTypeID", GetNullableValue(CTNTypeBox));
                        cmd.Parameters.AddWithValue("@ProductID", GetNullableValue(ProductBox));
                        cmd.Parameters.AddWithValue("@Style", GetNullableText(txtStyle));
                        cmd.Parameters.AddWithValue("@CategoryID", GetNullableValue(CategoryBox));
                        cmd.Parameters.AddWithValue("@SaleTypeID", GetNullableValue(SaleTypeBox));
                        cmd.Parameters.AddWithValue("@AgentID", GetNullableValue(AgentBox));
                        cmd.Parameters.AddWithValue("@ShipModeID", GetNullableValue(ShipModeBox));
                        cmd.Parameters.AddWithValue("@OrderDate", OrderDateTimePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@ExFactoryDate", ExFactoryDateTimePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@ETADate", ETADateTimePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@EmbellishmentID", GetNullableValue(EmbelishmentBox));
                        cmd.Parameters.AddWithValue("@PackingTypeID", GetNullableValue(PackingTypeBox));
                        cmd.Parameters.AddWithValue("@FoldTypeID", GetNullableValue(FoldTypeBox));
                        cmd.Parameters.AddWithValue("@FactoryPrice", GetNullableText(txtFactoryPrice, convertToInt: true));
                        cmd.Parameters.AddWithValue("@Commission", GetNullableText(txtCommission, convertToInt: true));
                        cmd.Parameters.AddWithValue("@Total", GetNullableText(txtTotal, convertToInt: true));
                        cmd.Parameters.AddWithValue("@CustomerTolerance", GetNullableValue(ToleranceBox));
                        cmd.Parameters.AddWithValue("@Plan", txtPlan.Text.Trim());
                        cmd.Parameters.AddWithValue("@Po", txtPoNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@CtnQty", txtCtnQty.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserCode", "USR");
                        cmd.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));
                        cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@GrandTotal", Convert.ToDecimal(TotalSumLabel.Text));

                        SqlParameter outputIdParam = new SqlParameter("@SaleOrderID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(outputIdParam);

                        cmd.ExecuteNonQuery();
                        newSaleOrderId = Convert.ToInt32(outputIdParam.Value);
                    }
                }
                catch (SqlException sqlex) when (sqlex.Number == 2627 || sqlex.Number == 2601)
                {
                    MessageBox.Show("Sale Order No already exists. Regenerating...", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowNextSaleOrderId();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Insert master error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return newSaleOrderId;
        }

        //======================================= Insert Detail Lines =======================================
        public void InsertSaleOrderDetail(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataGridViewRow row in ColorSizeDataGrid.Rows)
                        {
                            if (row.IsNewRow || row.Cells["COLOR"].Value == null) continue;
                            using (SqlCommand cmd = new SqlCommand("sp_InsertSaleOrderDetail", conn, tran))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
                                cmd.Parameters.AddWithValue("@ProductID", GetNullableValue(ProductBox));
                                cmd.Parameters.AddWithValue("@ColorID", GetCellValue(row, "COLOR"));
                                cmd.Parameters.AddWithValue("@SizeID", GetCellValue(row, "SIZE"));
                                cmd.Parameters.AddWithValue("@Quantity", GetCellValue(row, "QUANTITY"));
                                cmd.Parameters.AddWithValue("@UnitPrice", GetCellValue(row, "UNIT PRICE", true));
                                cmd.Parameters.AddWithValue("@TotalPrice", GetCellValue(row, "TOAL PRICE", true));
                                cmd.Parameters.AddWithValue("@UserCode", "USR");
                                cmd.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));
                                cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Insert detail error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                }
            }
        }

        //======================================= Insert Fabric Lines =======================================
        public void InsertSaleOrderFabric(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataGridViewRow row in FabricDataGrid.Rows)
                        {
                            if (row.IsNewRow || row.Cells["FABRIC"].Value == null) continue;
                            using (SqlCommand cmd = new SqlCommand("sp_InsertSaleOrderFabric", conn, tran))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
                                cmd.Parameters.AddWithValue("@FabricID", GetCellValue(row, "FABRIC"));
                                cmd.Parameters.AddWithValue("@Type", row.Cells["TYPE"].Value?.ToString() ?? "");
                                cmd.Parameters.AddWithValue("@GSM", GetCellValue(row, "GSM"));
                                cmd.Parameters.AddWithValue("@Width", GetCellValue(row, "WIDTH", true));
                                cmd.Parameters.AddWithValue("@Dia", GetCellValue(row, "DIA", true));
                                cmd.Parameters.AddWithValue("@Gauge", row.Cells["GAUGE"].Value?.ToString() ?? "");
                                cmd.Parameters.AddWithValue("@ShrinkPercent", GetCellValue(row, "SHIRNK", true));
                                cmd.Parameters.AddWithValue("@StitchLength", row.Cells["STITCH LENGTH"].Value?.ToString() ?? "");
                                cmd.Parameters.AddWithValue("@UserCode", "USR");
                                cmd.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));
                                cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Insert fabric error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                }
            }
        }

        //======================================= Update Master Record =======================================
        public void UpdateSaleOrderMaster(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateSaleOrderMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
                        cmd.Parameters.AddWithValue("@SaleOrderNo", txtSaleOrder.Text.Trim());
                        cmd.Parameters.AddWithValue("@CustomerID", GetNullableValue(CustomerBox));
                        cmd.Parameters.AddWithValue("@RangeID", GetNullableValue(RangeBox));
                        cmd.Parameters.AddWithValue("@CTNTypeID", GetNullableValue(CTNTypeBox));
                        cmd.Parameters.AddWithValue("@UnitNameID", GetNullableValue(UnitNameBox));
                        cmd.Parameters.AddWithValue("@ProductID", GetNullableValue(ProductBox));
                        cmd.Parameters.AddWithValue("@Style", GetNullableText(txtStyle));
                        cmd.Parameters.AddWithValue("@CategoryID", GetNullableValue(CategoryBox));
                        cmd.Parameters.AddWithValue("@SaleTypeID", GetNullableValue(SaleTypeBox));
                        cmd.Parameters.AddWithValue("@AgentID", GetNullableValue(AgentBox));
                        cmd.Parameters.AddWithValue("@ShipModeID", GetNullableValue(ShipModeBox));
                        cmd.Parameters.AddWithValue("@OrderDate", OrderDateTimePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@ExFactoryDate", ExFactoryDateTimePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@ETADate", ETADateTimePicker.Value.Date);
                        cmd.Parameters.AddWithValue("@EmbellishmentID", GetNullableValue(EmbelishmentBox));
                        cmd.Parameters.AddWithValue("@PackingTypeID", GetNullableValue(PackingTypeBox));
                        cmd.Parameters.AddWithValue("@FoldTypeID", GetNullableValue(FoldTypeBox));
                        cmd.Parameters.AddWithValue("@FactoryPrice", GetNullableText(txtFactoryPrice, convertToInt: true));
                        cmd.Parameters.AddWithValue("@Commission", GetNullableText(txtCommission, convertToInt: true));
                        cmd.Parameters.AddWithValue("@Total", GetNullableText(txtTotal, convertToInt: true));
                        cmd.Parameters.AddWithValue("@CustomerTolerance", GetNullableValue(ToleranceBox));
                        cmd.Parameters.AddWithValue("@Plan", txtPlan.Text.Trim());
                        cmd.Parameters.AddWithValue("@Po", txtPoNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@CtnQty", txtCtnQty.Text.Trim());
                        cmd.Parameters.AddWithValue("@GrandTotal", Convert.ToDecimal(TotalSumLabel.Text));
                        cmd.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update master error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        //======================================= Update Detail Lines =======================================
        public void UpdateSaleOrderDetail(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataGridViewRow row in ColorSizeDataGrid.Rows)
                        {
                            if (row.IsNewRow || row.Cells["COLOR"].Value == null) continue;
                            var preColorValue = row.Cells["PRE COLOR"].Value;
                            var preSizeValue = row.Cells["PRE SIZE"].Value;
                            if (preColorValue != null && preSizeValue != null)
                            {
                                using (SqlCommand cmd = new SqlCommand("sp_UpdateSaleOrderDetail", conn, tran))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
                                    cmd.Parameters.AddWithValue("@PreColorID", preColorValue);
                                    cmd.Parameters.AddWithValue("@PreSizeID", preSizeValue);
                                    cmd.Parameters.AddWithValue("@ProductID", GetNullableValue(ProductBox));
                                    cmd.Parameters.AddWithValue("@ColorID", GetCellValue(row, "COLOR"));
                                    cmd.Parameters.AddWithValue("@SizeID", GetCellValue(row, "SIZE"));
                                    cmd.Parameters.AddWithValue("@Quantity", GetCellValue(row, "QUANTITY"));
                                    cmd.Parameters.AddWithValue("@UnitPrice", GetCellValue(row, "UNIT PRICE", true));
                                    cmd.Parameters.AddWithValue("@TotalPrice", GetCellValue(row, "TOAL PRICE", true));
                                    cmd.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Update detail error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                }
            }
        }

        //======================================= Update Fabric Lines =======================================
        public void UpdateSaleOrderFabric(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataGridViewRow row in FabricDataGrid.Rows)
                        {
                            if (row.IsNewRow || row.Cells["FABRIC"].Value == null) continue;
                            var preFabricValue = row.Cells["PRE FABRIC"].Value;
                            if (preFabricValue != null)
                            {
                                using (SqlCommand cmd = new SqlCommand("sp_UpdateSaleOrderFabric", conn, tran))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
                                    cmd.Parameters.AddWithValue("@PreFabricID", preFabricValue);
                                    cmd.Parameters.AddWithValue("@FabricID", GetCellValue(row, "FABRIC"));
                                    cmd.Parameters.AddWithValue("@Type", row.Cells["TYPE"].Value?.ToString() ?? "");
                                    cmd.Parameters.AddWithValue("@GSM", GetCellValue(row, "GSM"));
                                    cmd.Parameters.AddWithValue("@Width", GetCellValue(row, "WIDTH", true));
                                    cmd.Parameters.AddWithValue("@Dia", GetCellValue(row, "DIA", true));
                                    cmd.Parameters.AddWithValue("@Gauge", row.Cells["GAUGE"].Value?.ToString() ?? "");
                                    cmd.Parameters.AddWithValue("@ShrinkPercent", GetCellValue(row, "SHIRNK", true));
                                    cmd.Parameters.AddWithValue("@StitchLength", row.Cells["STITCH LENGTH"].Value?.ToString() ?? "");
                                    cmd.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Update fabric error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                }
            }
        }

        //======================================= Delete Master Record =======================================
        public void DeleteSaleOrderMaster(int saleOrderId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("sp_DeleteSaleOrderMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete master error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        //======================================= Delete Detail Lines =======================================
        public void DeleteSaleOrderDetail(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataGridViewRow row in ColorSizeDataGrid.Rows)
                        {
                            if (row.IsNewRow || row.Cells["COLOR"].Value == null) continue;
                            var preColorValue = row.Cells["PRE COLOR"].Value;
                            var preSizeValue = row.Cells["PRE SIZE"].Value;
                            if (preColorValue != null && preSizeValue != null)
                            {
                                using (SqlCommand cmd = new SqlCommand("sp_DeleteSaleOrderDetail", conn, tran))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
                                    cmd.Parameters.AddWithValue("@ColorID", preColorValue);
                                    cmd.Parameters.AddWithValue("@SizeID", preSizeValue);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Delete detail error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                }
            }
        }

        //======================================= Delete Fabric Lines =======================================
        public void DeleteSaleOrderFabric(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataGridViewRow row in FabricDataGrid.Rows)
                        {
                            if (row.IsNewRow || row.Cells["FABRIC"].Value == null) continue;
                            var preFabricValue = row.Cells["PRE FABRIC"].Value;
                            if (preFabricValue != null)
                            {
                                using (SqlCommand cmd = new SqlCommand("sp_DeleteSaleOrderFabric", conn, tran))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
                                    cmd.Parameters.AddWithValue("@FabricID", preFabricValue);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Delete fabric error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                }
            }
        }
        #endregion

        #region Button Event Handlers
        //======================================= SAVE / INSERT Button =======================================
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    int newId = InsertSaleOrderMaster();
                    if (newId > 0)
                    {
                        InsertSaleOrderDetail(newId);
                        InsertSaleOrderFabric(newId);
                        MessageBox.Show("Sale order saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetFormControls();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= EDIT / SAVE Toggle =======================================
        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SubmitBtn.Enabled)
                {
                    SetFormEditable(true);
                    SubmitBtn.Enabled = true;
                    EditBtn.Text = "SAVE";
                    DeleteBtn.Enabled = false;
                }
                else
                {
                    if (Validation())
                    {
                        UpdateSaleOrderMaster(saleOrderId);
                        UpdateSaleOrderDetail(saleOrderId);
                        UpdateSaleOrderFabric(saleOrderId);
                        MessageBox.Show("Sale order updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SetFormEditable(false);
                        SubmitBtn.Enabled = true;
                        EditBtn.Text = "Edit";
                        EditBtn.Enabled = true;
                        DeleteBtn.Enabled = true;
                        ShowSaleOrder(saleOrderId);
                        LoadSaleOrderFabricData(saleOrderId);
                        LoadSaleOrderDetailData(saleOrderId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating sale order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= DELETE Button =======================================
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this sale order? This action cannot be undone.",
                                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    DeleteSaleOrderDetail(saleOrderId);
                    DeleteSaleOrderFabric(saleOrderId);
                    DeleteSaleOrderMaster(saleOrderId);
                    MessageBox.Show("Sale order deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetFormControls();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting sale order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //======================================= COLOR SIZES TOTAL =======================================
        private void TotalSum()
        {
            int grandTotal = 0;

            if (!ColorSizeDataGrid.Columns.Contains("QUANTITY"))
                return;

            foreach (DataGridViewRow row in ColorSizeDataGrid.Rows)
            {
                if (row.Cells["QUANTITY"].Value != null)
                {
                    int value;
                    if (int.TryParse(row.Cells["QUANTITY"].Value.ToString(), out value))
                    {
                        grandTotal += value;
                    }
                }
            }

            TotalSumLabel.Text = grandTotal.ToString();
        }

        private void ColorSizeDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.ColumnIndex < ColorSizeDataGrid.Columns.Count - 1)
            {
                TotalSum();
            }
        }


        //======================================= CLOSE Button =======================================
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //======================================= VIEW-ALL Button =======================================
        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            new SaleOrderViewAll().Show();
        }
        #endregion

        #region Unused Event Handlers
        private void txtCustomerCode_TextChanged(object sender, EventArgs e) { }
        private void CustomerTypeBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void FormGroupBox_Enter(object sender, EventArgs e) { }
        private void PackingTypeBox_DragDrop(object sender, DragEventArgs e) { }
        private void groupBox3_Enter(object sender, EventArgs e) { }
        private void CustomerBox_DropDown(object sender, EventArgs e) { }
        private void ProductBox_DropDown(object sender, EventArgs e) { }
        private void CategoryBox_DropDown(object sender, EventArgs e) { }
        private void ShipModeBox_DropDown(object sender, EventArgs e) { }
        private void EmbelishmentBox_DropDown(object sender, EventArgs e) { }
        private void PackingTypeBox_DropDown(object sender, EventArgs e) { }
        private void FoldTypeBox_DropDown(object sender, EventArgs e) { }
        private void SaleTypeBox_DropDown(object sender, EventArgs e) { }
        private void AgentBox_DropDown(object sender, EventArgs e) { }
        private void ToleranceBox_DropDown(object sender, EventArgs e) { }
        private void StatusCodeBox_DropDown(object sender, EventArgs e) { }
        #endregion

     
    }
}