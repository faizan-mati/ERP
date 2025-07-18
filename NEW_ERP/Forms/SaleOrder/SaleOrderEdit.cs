using NEW_ERP.GernalClasses;
using NEW_ERP.Template;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SaleOrder
{
    public partial class SaleOrderEdit : EditFormTemplate
    {
        public SaleOrderEdit()
        {
            InitializeComponent();
        }

        //======================================= FORM LOAD EVENT =======================================
        private void SaleOrderEdit_Load(object sender, EventArgs e)
        {
            SetFormEditable(false);
        }

        //======================================= CONTROL ENABLE/DISABLE =======================================
        private void SetFormEditable(bool enable)
        {
            CustomerBox.Enabled = enable;
            ProductBox.Enabled = enable;
            txtStyle.ReadOnly = !enable;
            CategoryBox.Enabled = enable;
            txtRange.ReadOnly = !enable;
            SaleTypeBox.Enabled = enable;
            AgentBox.Enabled = enable;
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

        //======================================= COMMON DROPDOWN LOADER =======================================
        private void LoadDropdown(ComboBox box, ref bool isLoaded, string query, string displayMember, string valueMember)
        {
            if (isLoaded) return;

            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                box.DataSource = dt;
                box.DisplayMember = displayMember;
                box.ValueMember = valueMember;
                box.SelectedIndex = -1;

                isLoaded = true;
            }
        }

        //======================================= SALE ORDER BOX =======================================

        private bool isSoidLoaded = false;

        private void SaleOrderIdBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(SaleOrderIdBox, ref isSoidLoaded,
                "SELECT SaleOrderNo, SaleOrderID FROM SaleOrderMaster",
                "SaleOrderNo", "SaleOrderID");
        }

        private void SaleOrderIdBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaleOrderIdBox.SelectedValue == null || SaleOrderIdBox.SelectedValue is DataRowView)
                return;

            FabricDataGridShow();
            ColorSizeDataGridShow();


            ShowSaleOrder();
            ShowSaleOrderFabric();
            ShowSaleOrderDetail();
        }

        //======================================= SHOW SALE ORDER MASTER DATA =======================================
        private void ShowSaleOrder()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
              SELECT [CustomerID], [ProductID], [Style], [CategoryID], [Range], [SaleTypeID], [AgentID], 
                       [ShipModeID], [OrderDate], [ExFactoryDate], [ETADate], [EmbellishmentID], 
                       [PackingTypeID], [FoldTypeID], [FactoryPrice], [Commission], [Total], 
                       [CustomerTolerance], [SaleOrderNo], [PoNo], [SaleOrderPlan], [CtnQty]
                FROM [SaleOrderMaster]
                WHERE [SaleOrderID] = @SaleOrderId", con))
            {
                cmd.Parameters.AddWithValue("@SaleOrderId", Convert.ToInt32(SaleOrderIdBox.SelectedValue));

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //================== LOAD DROPDOWNS ==================
                        LoadCustomerDropdown();
                        LoadProductDropdown();
                        LoadCategoryDropdown();
                        LoadEmbDropdown();
                        LoadShipModeDropdown();
                        LoadPackingTypeDropdown();
                        LoadFoldTypeDropdown();
                        LoadSaleTypeDropdown();
                        LoadToleranceDropdown();
                        LoadAgentDropdown();

                        //================== SET VALUES ==================
                        CustomerBox.SelectedValue = reader["CustomerID"];
                        ProductBox.SelectedValue = reader["ProductID"];
                        txtStyle.Text = reader["Style"].ToString();
                        CategoryBox.SelectedValue = reader["CategoryID"];
                        txtRange.Text = reader["Range"].ToString();
                        SaleTypeBox.SelectedValue = reader["SaleTypeID"];
                        AgentBox.SelectedValue = reader["AgentID"];
                        ShipModeBox.SelectedValue = reader["ShipModeID"];
                        txtPoNo.Text = reader["PoNo"].ToString();
                        txtPlan.Text = reader["SaleOrderPlan"].ToString();
                        txtCtnQty.Text = reader["CtnQty"].ToString();

                        OrderDateTimePicker.Value = reader["OrderDate"] != DBNull.Value ? Convert.ToDateTime(reader["OrderDate"]) : DateTime.Now;
                        ExFactoryDateTimePicker.Value = reader["ExFactoryDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExFactoryDate"]) : DateTime.Now;
                        ETADateTimePicker.Value = reader["ETADate"] != DBNull.Value ? Convert.ToDateTime(reader["ETADate"]) : DateTime.Now;

                        EmbelishmentBox.SelectedValue = reader["EmbellishmentID"];
                        PackingTypeBox.SelectedValue = reader["PackingTypeID"];
                        FoldTypeBox.SelectedValue = reader["FoldTypeID"];
                        txtFactoryPrice.Text = reader["FactoryPrice"].ToString();
                        txtCommission.Text = reader["Commission"].ToString();
                        txtTotal.Text = reader["Total"].ToString();
                        ToleranceBox.Text = reader["CustomerTolerance"].ToString();
                        txtSaleOrder.Text = reader["SaleOrderNo"].ToString();

                        SetFormEditable(true);
                    }
                    else
                    {
                        MessageBox.Show("No data found for the selected sale order.");
                    }
                }
            }
        }

        // ========================== LOAD FABRIC DATA INTO GRID ==========================
     
        private void ShowSaleOrderFabric()
        {
            if (SaleOrderIdBox.SelectedValue == null || SaleOrderIdBox.SelectedValue is DataRowView)
                return;

            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                int selectedSaleOrderId = Convert.ToInt32(SaleOrderIdBox.SelectedValue);

                string query = @"SELECT [FabricID], [Type], [GSM], [Width], [Dia], [Gauge], 
                         [ShrinkPercent], [StitchLength] 
                         FROM [SaleOrderFabric] 
                         WHERE [SaleOrderID] = @SaleOrder";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SaleOrder", selectedSaleOrderId);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        FabricDataGrid.Rows.Clear();
                        DataTable fabricList = GetFabricNames();

                        while (reader.Read())
                        {
                            int fabricId = Convert.ToInt32(reader["FabricID"]);

                            // Validate fabric exists in combo source
                            if (!fabricList.AsEnumerable().Any(row => row.Field<int>("FabricID") == fabricId))
                                continue; // Skip invalid

                            FabricDataGrid.Rows.Add(
                                fabricId,                            // ComboBox (FABRIC)
                                fabricId,                            // OLD FABRIC
                                reader["Type"].ToString(),
                                reader["GSM"].ToString(),
                                reader["Width"].ToString(),
                                reader["Dia"].ToString(),
                                reader["Gauge"].ToString(),
                                reader["ShrinkPercent"].ToString(),
                                reader["StitchLength"].ToString()
                            );
                        }
                    }
                }
            }
        }


        // =========================================== Configures the Fabric DataGrid with columns and event handlers

        public void FabricDataGridShow()
        {
            FabricDataGrid.Columns.Clear();

            // Load fabric list before adding rows
            DataTable fabricList = GetFabricNames();

            var fabricColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "FABRIC",
                Name = "FABRIC",
                Width = 220,
                DataSource = fabricList,
                DisplayMember = "FabricName",
                ValueMember = "FabricID",
                FlatStyle = FlatStyle.Flat
            };

            FabricDataGrid.Columns.Add(fabricColumn);

            int oldFabricColumnIndex = FabricDataGrid.Columns.Add("OLD FABRIC", "OLD FABRIC");
            FabricDataGrid.Columns[oldFabricColumnIndex].Visible = false;

            FabricDataGrid.Columns.Add("TYPE", "TYPE");
            FabricDataGrid.Columns.Add("GSM", "GSM");
            FabricDataGrid.Columns.Add("WIDTH", "WIDTH");
            FabricDataGrid.Columns.Add("DIA", "DIA");
            FabricDataGrid.Columns.Add("GAUGE", "GAUGE");
            FabricDataGrid.Columns.Add("SHIRNK", "SHIRNK");
            FabricDataGrid.Columns.Add("STITCH LENGTH", "STITCH LENGTH");

            // Grid styling
            FabricDataGrid.DefaultCellStyle.Font = new Font("Arial", 11);
            FabricDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            // Events
            FabricDataGrid.EditingControlShowing += FabricDataGrid_EditingControlShowing;
            FabricDataGrid.DataError += FabricDataGrid_DataError;
        }

        // ========================== DATA ERROR HANDLER ==========================
        private void FabricDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show("Invalid fabric value. Please select from the dropdown.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // ===========================================  Handles editing control showing event for Fabric DataGrid

        private void FabricDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (FabricDataGrid.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox comboBox)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }


        // ===========================================  Handles cell click event for Fabric DataGrid - loads fabric data when needed

        private void FabricDataGrid_CellClick_1(object sender, DataGridViewCellEventArgs e)
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

        // =========================================== Retrieves fabric names from the database

        private DataTable GetFabricNames()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "SELECT FabricID, FabricName FROM FabricMaster";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // ===========================================  Configures the Color/Size DataGrid with columns and event handlers

        private void ShowSaleOrderDetail()
        {
            if (SaleOrderIdBox.SelectedValue == null || SaleOrderIdBox.SelectedValue is DataRowView)
                return;

            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                int selectedSaleOrderId = Convert.ToInt32(SaleOrderIdBox.SelectedValue);
                string query = @"SELECT [ColorID], [SizeID], [Quantity], [UnitPrice], [TotalPrice] 
                         FROM [dbo].[SaleOrderDetails] 
                         WHERE [SaleOrderID] = @SaleOrder";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SaleOrder", selectedSaleOrderId);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ColorSizeDataGrid.Rows.Clear();
                        while (reader.Read())
                        {
                            ColorSizeDataGrid.Rows.Add(
                                reader["ColorID"],   // COLOR combo (ValueMember)
                                reader["SizeID"],    // SIZE combo (ValueMember)
                                reader["ColorID"],   // OLD COLOR (Hidden)
                                reader["SizeID"],    // OLD SIZE (Hidden)
                                reader["Quantity"],
                                reader["UnitPrice"],
                                reader["TotalPrice"]
                            );
                        }
                    }
                }
            }
        }

        // ========================== INITIALIZE DATAGRIDVIEW ==========================
        public void ColorSizeDataGridShow()
        {
            ColorSizeDataGrid.Columns.Clear();

            var colorColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "COLOR",
                Name = "COLOR",
                Width = 250,
                DataSource = GetColorNames(),
                DisplayMember = "ColorName",
                ValueMember = "ColorID",
                FlatStyle = FlatStyle.Flat
            };

            var sizeColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "SIZE",
                Name = "SIZE",
                Width = 150,
                DataSource = GetSizeNames(),
                DisplayMember = "SizeName",
                ValueMember = "SizeID",
                FlatStyle = FlatStyle.Flat
            };

            ColorSizeDataGrid.Columns.Add(colorColumn);
            ColorSizeDataGrid.Columns.Add(sizeColumn);

            // Hidden Columns
            var oldColorCol = new DataGridViewTextBoxColumn { Name = "OLD COLOR", Visible = false };
            var oldSizeCol = new DataGridViewTextBoxColumn { Name = "OLD SIZE", Visible = false };
            ColorSizeDataGrid.Columns.Add(oldColorCol);
            ColorSizeDataGrid.Columns.Add(oldSizeCol);

            ColorSizeDataGrid.Columns.Add("QUANTITY", "QUANTITY");
            ColorSizeDataGrid.Columns.Add("UNIT PRICE", "UNIT PRICE");
            ColorSizeDataGrid.Columns.Add("TOTAL PRICE", "TOTAL PRICE");

            ColorSizeDataGrid.DefaultCellStyle.Font = new Font("Arial", 11);
            ColorSizeDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            ColorSizeDataGrid.EditingControlShowing -= ColorSizeDataGrid_EditingControlShowing;
            ColorSizeDataGrid.EditingControlShowing += ColorSizeDataGrid_EditingControlShowing;

            ColorSizeDataGrid.CellClick -= ColorSizeDataGrid_CellClick;
            ColorSizeDataGrid.CellClick += ColorSizeDataGrid_CellClick;
        }

        // ========================== ENABLE AUTOCOMPLETE ON EDIT ==========================
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

        // ========================== LOAD COLOR/SIZE COMBO ON CLICK ==========================
        private void ColorSizeDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var comboCol = ColorSizeDataGrid.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
                if (comboCol != null && comboCol.DataSource == null)
                {
                    switch (e.ColumnIndex)
                    {
                        case 0: // COLOR
                            comboCol.DataSource = GetColorNames();
                            comboCol.DisplayMember = "ColorName";
                            comboCol.ValueMember = "ColorID";
                            break;
                        case 1: // SIZE
                            comboCol.DataSource = GetSizeNames();
                            comboCol.DisplayMember = "SizeName";
                            comboCol.ValueMember = "SizeID";
                            break;
                    }
                }
            }
        }

        // ========================== GET COLOR LIST FROM DB ==========================
        private DataTable GetColorNames()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "SELECT ColorID, ColorName FROM ColorMaster ORDER BY ColorName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    return dt;
                }
            }
        }

        // ========================== GET SIZE LIST FROM DB ==========================
        private DataTable GetSizeNames()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "SELECT SizeID, SizeName FROM SizeMaster ORDER BY SizeName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    return dt;
                }
            }
        }


        //======================================= LOAD ALL DROPDOWNS =======================================

        private bool isCustomerLoaded = false;
        private bool isProductLoaded = false;
        private bool isCategoryLoaded = false;
        private bool isShipModeLoaded = false;
        private bool isEmblishmenLoaded = false;
        private bool isPackingTypeLoaded = false;
        private bool isFoldTypeLoaded = false;
        private bool isSaleTypeLoaded = false;
        private bool isToleranceLoaded = false;
        private bool isAgentLoaded = false;

        private void LoadCustomerDropdown() => LoadDropdown(CustomerBox, ref isCustomerLoaded, "SELECT CustomerID, CustomerName FROM CustomerMaster", "CustomerName", "CustomerID");
        private void LoadProductDropdown() => LoadDropdown(ProductBox, ref isProductLoaded, "SELECT ProductCode, ProductShortName FROM ItemMaster", "ProductShortName", "ProductCode");
        private void LoadCategoryDropdown() => LoadDropdown(CategoryBox, ref isCategoryLoaded, "SELECT CategoryID, CategoryName FROM CategoryMaster", "CategoryName", "CategoryID");
        private void LoadEmbDropdown() => LoadDropdown(EmbelishmentBox, ref isEmblishmenLoaded, "SELECT EmbellishmentID, EmbellishmentName FROM EmbellishmentMaster", "EmbellishmentName", "EmbellishmentID");
        private void LoadShipModeDropdown() => LoadDropdown(ShipModeBox, ref isShipModeLoaded, "SELECT ShipModeID, ShipModeName FROM ShipmentModeMaster", "ShipModeName", "ShipModeID");
        private void LoadPackingTypeDropdown() => LoadDropdown(PackingTypeBox, ref isPackingTypeLoaded, "SELECT PackingTypeID, PackingType FROM PackingTypeMaster", "PackingType", "PackingTypeID");
        private void LoadFoldTypeDropdown() => LoadDropdown(FoldTypeBox, ref isFoldTypeLoaded, "SELECT FoldTypeID, FoldTypeName FROM FoldTypeMaster", "FoldTypeName", "FoldTypeID");
        private void LoadSaleTypeDropdown() => LoadDropdown(SaleTypeBox, ref isSaleTypeLoaded, "SELECT SaleTypeID, SaleType FROM SaleTypeMaster", "SaleType", "SaleTypeID");
        private void LoadAgentDropdown() => LoadDropdown(AgentBox, ref isAgentLoaded, "SELECT AgentID, AgentName FROM AgentMaster", "AgentName", "AgentID");
        private void LoadToleranceDropdown() => LoadDropdown(ToleranceBox, ref isToleranceLoaded, "SELECT ToleranceID, TolerancePercent FROM ToleranceMaster", "TolerancePercent", "ToleranceID");

        private void CustomerBox_DropDown_1(object sender, EventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
