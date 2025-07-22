using NEW_ERP.GernalClasses;
using NEW_ERP.Template;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SaleOrder
{
    /// <summary>
    /// Sale Order Add Form - Handles creation of new sale orders with fabric details and color/size specifications
    /// </summary>
    public partial class SaleOrderAdd : AddFormTemplate
    {
        #region Private Fields

        // Dropdown loading state flags
        private bool isCustomerLoaded = false;
        private bool isProductLoaded = false;
        private bool isCategoryLoaded = false;
        private bool isShipModeLoaded = false;
        private bool isEmblishmenLoaded = false;
        private bool isPackingTypeLoaded = false;
        private bool isFoldTypeLoaded = false;
        private bool isSaleTypeLoaded = false;
        private bool isAgentLoaded = false;
        private bool isToleranceLoaded = false;

        private int saleOrderId;
        private bool isFromViewAll;
        private Timer delayTimer;


        public SaleOrderAdd(int saleOrderId, bool isFromViewAll)
        {
            InitializeComponent();
            this.saleOrderId = saleOrderId;
            this.isFromViewAll = isFromViewAll;
        }

        private void SaleOrderAdd_Load(object sender, EventArgs e)
        {
            AttachDropdownEvents(); // attach lazy-loading

            if (isFromViewAll)
            {
                delayTimer = new Timer();
                delayTimer.Interval = 500;
                delayTimer.Tick += DelayTimer_Tick;
                delayTimer.Start();

                SetFormEditable(false);
                SubmitBtn.Enabled = false;

                FabricDataGridShowEdit();
                ColorSizeDataGridShowEdit();
            }
            else
            {
                EditBtn.Enabled = false;
                DeleteBtn.Enabled = false;

                FabricDataGridShow();
                ColorSizeDataGridShow();
            }
        }

        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            delayTimer.Stop();
            delayTimer.Tick -= DelayTimer_Tick;

            ShowSaleOrder(saleOrderId);

            // Load fabric and detail data after master data is loaded
            LoadSaleOrderFabricData(saleOrderId);
            LoadSaleOrderDetailData(saleOrderId);
        }


        private void AttachDropdownEvents()
        {
            CustomerBox.DropDown += (s, e) => LoadCustomerDropdown();
            ProductBox.DropDown += (s, e) => LoadProductDropdown();
            CategoryBox.DropDown += (s, e) => LoadCategoryDropdown();
            EmbelishmentBox.DropDown += (s, e) => LoadEmbDropdown();
            ShipModeBox.DropDown += (s, e) => LoadShipModeDropdown();
            PackingTypeBox.DropDown += (s, e) => LoadPackingTypeDropdown();
            FoldTypeBox.DropDown += (s, e) => LoadFoldTypeDropdown();
            SaleTypeBox.DropDown += (s, e) => LoadSaleTypeDropdown();
            AgentBox.DropDown += (s, e) => LoadAgentDropdown();
            ToleranceBox.DropDown += (s, e) => LoadToleranceDropdown();
        }



        #endregion

        #region DataGrid Setup Methods

        // =========================================== Configures the Fabric DataGrid with columns and event handlers

        public void FabricDataGridShow()
        {
            FabricDataGrid.Columns.Clear();

            // Add fabric combo box column
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

            // Set grid styling
            FabricDataGrid.DefaultCellStyle.Font = new Font("Arial", 11);
            FabricDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            FabricDataGrid.Columns[1].Width = 150;

            // Attach event handlers
            FabricDataGrid.EditingControlShowing += FabricDataGrid_EditingControlShowing;
            FabricDataGrid.CellClick += FabricDataGrid_CellClick;
        }

        // ===========================================  Configures the Color/Size DataGrid with columns and event handlers

        public void ColorSizeDataGridShow()
        {
            ColorSizeDataGrid.Columns.Clear();

            // Add color and size combo box columns
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

            // Set grid styling
            ColorSizeDataGrid.DefaultCellStyle.Font = new Font("Arial", 11);
            ColorSizeDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            // Attach event handlers
            ColorSizeDataGrid.EditingControlShowing += ColorSizeDataGrid_EditingControlShowing;
            ColorSizeDataGrid.CellClick += ColorSizeDataGrid_CellClick;
        }

        #endregion

        #region DataGrid Event Handlers

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

        // ===========================================  Handles editing control showing event for Color/Size DataGrid

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

        // ===========================================  Handles cell click event for Color/Size DataGrid - loads color/size data when needed

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

        #endregion

        #region Data Loading Methods

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

        // ===========================================  Retrieves color names from the database

        private DataTable GetColorNames()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "SELECT ColorID, ColorName FROM ColorMaster";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // ===========================================  Retrieves size names from the database

        private DataTable GetSizeNames()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "SELECT SizeID, SizeName FROM SizeMaster";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        #endregion

        #region ComboBox Dropdown Event Handlers

        // ===========================================  Generic method to load dropdown data

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

                isLoaded = true;
            }
        }


        // ===========================================  Loads customer data on dropdown

        private void CustomerBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(CustomerBox, ref isCustomerLoaded,
                "SELECT CustomerID, CustomerName FROM CustomerMaster",
                "CustomerName", "CustomerID");
        }

        // ===========================================  Loads product data on dropdown

        private void ProductBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(ProductBox, ref isProductLoaded,
                "SELECT ProductCode, ProductShortName FROM ItemMaster",
                "ProductShortName", "ProductCode");
        }

        // ===========================================  Loads category data on dropdown

        private void CategoryBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(CategoryBox, ref isCategoryLoaded,
               "SELECT CategoryID, CategoryName FROM CategoryMaster",
               "CategoryName", "CategoryID");
        }

        // ===========================================  Loads ship mode data on dropdown

        private void ShipModeBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(ShipModeBox, ref isShipModeLoaded,
               "SELECT ShipModeID, ShipModeName FROM ShipmentModeMaster",
               "ShipModeName", "ShipModeID");
        }

        // ===========================================  Loads embellishment data on dropdown

        private void EmbelishmentBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(EmbelishmentBox, ref isEmblishmenLoaded,
              "SELECT EmbellishmentID, EmbellishmentName FROM EmbellishmentMaster",
              "EmbellishmentName", "EmbellishmentID");
        }

        // ===========================================  Loads packing type data on dropdown

        private void PackingTypeBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(PackingTypeBox, ref isPackingTypeLoaded,
               "SELECT PackingTypeID, PackingType FROM PackingTypeMaster",
               "PackingType", "PackingTypeID");
        }

        // =========================================== Loads fold type data on dropdown

        private void FoldTypeBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(FoldTypeBox, ref isFoldTypeLoaded,
               "SELECT FoldTypeID, FoldTypeName FROM FoldTypeMaster",
               "FoldTypeName", "FoldTypeID");
        }

        // =========================================== Loads sale type data on dropdown

        private void SaleTypeBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(SaleTypeBox, ref isSaleTypeLoaded,
               "SELECT SaleTypeID, SaleType FROM SaleTypeMaster",
               "SaleType", "SaleTypeID");
        }

        // =========================================== Loads agent data on dropdown

        private void AgentBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(AgentBox, ref isAgentLoaded,
               "SELECT AgentID, AgentName FROM AgentMaster",
               "AgentName", "AgentID");
        }

        // ===========================================  Loads tolerance data on dropdown

        private void ToleranceBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(ToleranceBox, ref isToleranceLoaded,
                 "SELECT ToleranceID, TolerancePercent FROM ToleranceMaster",
                 "TolerancePercent", "ToleranceID");
        }

        #endregion

        #region Utility Methods

        // ===========================================  Returns ComboBox selected value or DBNull if no selection

        private object GetNullableValue(ComboBox comboBox)
        {
            return comboBox.SelectedItem != null ? comboBox.SelectedValue : DBNull.Value;
        }

        // ===========================================  Returns TextBox value with optional type conversion or DBNull if empty

        private object GetNullableText(TextBox textBox, bool convertToInt = false, bool convertToDecimal = false)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
                return DBNull.Value;

            try
            {
                if (convertToInt)
                    return Convert.ToInt32(textBox.Text);
                if (convertToDecimal)
                    return Convert.ToDecimal(textBox.Text);
                return textBox.Text.Trim();
            }
            catch
            {
                return DBNull.Value;
            }
        }

        // ===========================================  Returns DataGrid cell value with optional type conversion or DBNull if empty

        private object GetCellValue(DataGridViewRow row, string columnName, bool isDecimal = false)
        {
            var val = row.Cells[columnName].Value;
            if (val == null || string.IsNullOrWhiteSpace(val.ToString()))
                return DBNull.Value;

            try
            {
                return isDecimal ? Convert.ToDecimal(val) : Convert.ToInt32(val);
            }
            catch
            {
                return DBNull.Value;
            }
        }

        #endregion

        #region Data Insertion Methods

        // ===========================================  Inserts sale order master record and returns the new Sale Order ID

        public int InsertSaleOrderMaster()
        {
            int newSaleOrderId = 0;

            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertSaleOrderMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@SaleOrderNo", txtSaleOrder.Text.Trim());
                    cmd.Parameters.AddWithValue("@CustomerID", GetNullableValue(CustomerBox));
                    cmd.Parameters.AddWithValue("@ProductID", GetNullableValue(ProductBox));
                    cmd.Parameters.AddWithValue("@Style", GetNullableText(txtStyle));
                    cmd.Parameters.AddWithValue("@CategoryID", GetNullableValue(CategoryBox));
                    cmd.Parameters.AddWithValue("@Range", GetNullableText(txtRange));
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
                    cmd.Parameters.AddWithValue("@StatusCode", "ACT");
                    cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);

                    // Output parameter for new ID
                    SqlParameter outputIdParam = new SqlParameter("@SaleOrderID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputIdParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    newSaleOrderId = Convert.ToInt32(outputIdParam.Value);
                }
            }

            return newSaleOrderId;
        }

        // ===========================================  Inserts sale order detail records from ColorSizeDataGrid

        public void SaleOrderDetail(int saleOrderId)
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
                                cmd.Parameters.AddWithValue("@StatusCode", "ACT");
                                cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);

                                cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("SaleOrderDetail Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ===========================================  Inserts sale order fabric records from FabricDataGrid

        public void SaleOrderFabric(int saleOrderId)
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
                                cmd.Parameters.AddWithValue("@StatusCode", "ACT");
                                cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);

                                cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("SaleOrderFabric Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Validation Methods

        // ===========================================  Validates all required fields before saving

        public bool validation()
        {
            // Check required text fields
            if (string.IsNullOrWhiteSpace(txtSaleOrder.Text) ||
                string.IsNullOrWhiteSpace(txtPoNo.Text) ||
                string.IsNullOrWhiteSpace(txtPlan.Text))
            {
                MessageBox.Show("Please fill all required fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Check required combo boxes
            if (CustomerBox.SelectedIndex == -1 ||
                ShipModeBox.SelectedIndex == -1 ||
                ProductBox.SelectedIndex == -1 ||
                CategoryBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select all required dropdown values", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Check fabric grid has data
            if (FabricDataGrid.Rows.Count == 0 ||
                FabricDataGrid.Rows[0].Cells["FABRIC"].Value == null ||
                string.IsNullOrWhiteSpace(FabricDataGrid.Rows[0].Cells["FABRIC"].Value.ToString()))
            {
                MessageBox.Show("Please add at least one fabric entry", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Check color/size grid has data
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

        #endregion

        #region Form Control Methods

        // =========================================== Resets all form controls to default state

        public void RestFormControler()
        {
            // Clear text boxes
            txtSaleOrder.Clear();
            txtStyle.Clear();
            txtRange.Clear();
            txtFactoryPrice.Clear();
            txtCommission.Clear();
            txtTotal.Clear();
            txtPoNo.Clear();
            txtPlan.Clear();
            txtCtnQty.Clear();

            // Reset combo boxes
            CustomerBox.SelectedIndex = -1;
            ProductBox.SelectedIndex = -1;
            CategoryBox.SelectedIndex = -1;
            SaleTypeBox.SelectedIndex = -1;
            ShipModeBox.SelectedIndex = -1;
            AgentBox.SelectedIndex = -1;
            PackingTypeBox.SelectedIndex = -1;
            EmbelishmentBox.SelectedIndex = -1;
            FoldTypeBox.SelectedIndex = -1;
            ToleranceBox.SelectedIndex = -1;

            // Reset date time pickers
            OrderDateTimePicker.Value = DateTime.Now;
            ExFactoryDateTimePicker.Value = DateTime.Now;
            ETADateTimePicker.Value = DateTime.Now;

            // Clear data grids
            FabricDataGrid.Rows.Clear();
            ColorSizeDataGrid.Rows.Clear();

            // Reset loading flags
            isCustomerLoaded = false;
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

        #region Button Event Handlers

        // ===========================================  Handles submit button click - validates and saves the sale order

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (validation())
                {
                    int saleOrderId = InsertSaleOrderMaster();

                    if (saleOrderId > 0)
                    {
                        SaleOrderDetail(saleOrderId);
                        SaleOrderFabric(saleOrderId);
                        MessageBox.Show("Sale order saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RestFormControler();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===========================================  Handles close button click - closes the form

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCustomerCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void CustomerTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void PackingTypeBox_DragDrop(object sender, DragEventArgs e)
        {

        }

        #endregion

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            this.Close();

            SaleOrderViewAll editForm = new SaleOrderViewAll();
            editForm.Show();
        }



        //================================================= EDIT AND DELETE =====================================================


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

        private void ShowSaleOrder(int saleOrderId)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT [CustomerID], [ProductID], [Style], [CategoryID], [Range], [SaleTypeID], [AgentID], 
               [ShipModeID], [OrderDate], [ExFactoryDate], [ETADate], [EmbellishmentID], 
               [PackingTypeID], [FoldTypeID], [FactoryPrice], [Commission], [Total], 
               [CustomerTolerance], [SaleOrderNo], [PoNo], [SaleOrderPlan], [CtnQty]
        FROM [SaleOrderMaster]
        WHERE [SaleOrderID] = @SaleOrderId AND StatusCode = 'ACT'", con))
            {
                cmd.Parameters.AddWithValue("@SaleOrderId", saleOrderId);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Load only selected value for display
                        LoadSelectedItemOnly(CustomerBox, "SELECT CustomerID, CustomerName FROM CustomerMaster WHERE CustomerID = @ID", "CustomerName", "CustomerID", reader["CustomerID"]);
                        LoadSelectedItemOnly(ProductBox, "SELECT ProductCode, ProductShortName FROM ItemMaster WHERE ProductCode = @ID", "ProductShortName", "ProductCode", reader["ProductID"]);
                        LoadSelectedItemOnly(CategoryBox, "SELECT CategoryID, CategoryName FROM CategoryMaster WHERE CategoryID = @ID", "CategoryName", "CategoryID", reader["CategoryID"]);
                        LoadSelectedItemOnly(SaleTypeBox, "SELECT SaleTypeID, SaleType FROM SaleTypeMaster WHERE SaleTypeID = @ID", "SaleType", "SaleTypeID", reader["SaleTypeID"]);
                        LoadSelectedItemOnly(AgentBox, "SELECT AgentID, AgentName FROM AgentMaster WHERE AgentID = @ID", "AgentName", "AgentID", reader["AgentID"]);
                        LoadSelectedItemOnly(ShipModeBox, "SELECT ShipModeID, ShipModeName FROM ShipmentModeMaster WHERE ShipModeID = @ID", "ShipModeName", "ShipModeID", reader["ShipModeID"]);
                        LoadSelectedItemOnly(EmbelishmentBox, "SELECT EmbellishmentID, EmbellishmentName FROM EmbellishmentMaster WHERE EmbellishmentID = @ID", "EmbellishmentName", "EmbellishmentID", reader["EmbellishmentID"]);
                        LoadSelectedItemOnly(PackingTypeBox, "SELECT PackingTypeID, PackingType FROM PackingTypeMaster WHERE PackingTypeID = @ID", "PackingType", "PackingTypeID", reader["PackingTypeID"]);
                        LoadSelectedItemOnly(FoldTypeBox, "SELECT FoldTypeID, FoldTypeName FROM FoldTypeMaster WHERE FoldTypeID = @ID", "FoldTypeName", "FoldTypeID", reader["FoldTypeID"]);
                        LoadSelectedItemOnly(ToleranceBox, "SELECT ToleranceID, TolerancePercent FROM ToleranceMaster WHERE ToleranceID = @ID", "TolerancePercent", "ToleranceID", reader["CustomerTolerance"]);

                        txtStyle.Text = reader["Style"].ToString();
                        txtRange.Text = reader["Range"].ToString();
                        txtPoNo.Text = reader["PoNo"].ToString();
                        txtPlan.Text = reader["SaleOrderPlan"].ToString();
                        txtCtnQty.Text = reader["CtnQty"].ToString();
                        txtFactoryPrice.Text = reader["FactoryPrice"].ToString();
                        txtCommission.Text = reader["Commission"].ToString();
                        txtTotal.Text = reader["Total"].ToString();
                        txtSaleOrder.Text = reader["SaleOrderNo"].ToString();

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



        //======================================= COMMON DROPDOWN LOADER =======================================
        private void LoadSelectedItemOnly(ComboBox box, string query, string displayMember, string valueMember, object value)
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


        //======================================= LOAD ALL DROPDOWNS =======================================

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



        //======================================= LOAD FABRIC DATA FROM DATABASE =======================================


        public void FabricDataGridShowEdit()
        {
            FabricDataGrid.Columns.Clear();

            // Add fabric combo box column
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
            FabricDataGrid.Columns.Add("PRE FABRIC", "PRE FABRIC");

            FabricDataGrid.Columns["PRE FABRIC"].Visible = false;


            // Set grid styling
            FabricDataGrid.DefaultCellStyle.Font = new Font("Arial", 11);
            FabricDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            FabricDataGrid.Columns[1].Width = 150;

            // Attach event handlers
            FabricDataGrid.EditingControlShowing += FabricDataGrid_EditingControlShowing;
            FabricDataGrid.CellClick += FabricDataGrid_CellClick;
        }


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
            WHERE sof.SaleOrderID = @SaleOrderId AND sof.StatusCode = 'ACT'", con))
                {
                    cmd.Parameters.AddWithValue("@SaleOrderId", saleOrderId);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear existing rows
                        FabricDataGrid.Rows.Clear();

                        while (reader.Read())
                        {
                            int rowIndex = FabricDataGrid.Rows.Add();
                            DataGridViewRow row = FabricDataGrid.Rows[rowIndex];

                            // Load fabric dropdown for this row
                            var fabricColumn = FabricDataGrid.Columns["FABRIC"] as DataGridViewComboBoxColumn;
                            if (fabricColumn.DataSource == null)
                            {
                                fabricColumn.DataSource = GetFabricNames();
                                fabricColumn.DisplayMember = "FabricName";
                                fabricColumn.ValueMember = "FabricID";
                            }

                            // Set cell values
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

        //======================================= LOAD DETAIL DATA FROM DATABASE =======================================


        public void ColorSizeDataGridShowEdit()
        {
            ColorSizeDataGrid.Columns.Clear();

            // Add color and size combo box columns
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
            ColorSizeDataGrid.Columns.Add("PRE COLOR", "PRE COLOR");
            ColorSizeDataGrid.Columns.Add("PRE SIZE", "PRE SIZE");

            ColorSizeDataGrid.Columns["PRE COLOR"].Visible = false;
            ColorSizeDataGrid.Columns["PRE SIZE"].Visible = false;


            // Set grid styling
            ColorSizeDataGrid.DefaultCellStyle.Font = new Font("Arial", 11);
            ColorSizeDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            // Attach event handlers
            ColorSizeDataGrid.EditingControlShowing += ColorSizeDataGrid_EditingControlShowing;
            ColorSizeDataGrid.CellClick += ColorSizeDataGrid_CellClick;
        }



        private void LoadSaleOrderDetailData(int saleOrderId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(@"
            SELECT sod.ColorID, cm.ColorName, sod.SizeID, sm.SizeName, 
                   sod.Quantity, sod.UnitPrice, sod.TotalPrice
            FROM SaleOrderDetails sod
            INNER JOIN ColorMaster cm ON sod.ColorID = cm.ColorID
            INNER JOIN SizeMaster sm ON sod.SizeID = sm.SizeID
            WHERE sod.SaleOrderID = @SaleOrderId AND sod.StatusCode = 'ACT'", con))
                {
                    cmd.Parameters.AddWithValue("@SaleOrderId", saleOrderId);
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear existing rows
                        ColorSizeDataGrid.Rows.Clear();

                        while (reader.Read())
                        {
                            int rowIndex = ColorSizeDataGrid.Rows.Add();
                            DataGridViewRow row = ColorSizeDataGrid.Rows[rowIndex];

                            // Load color dropdown for this row
                            var colorColumn = ColorSizeDataGrid.Columns["COLOR"] as DataGridViewComboBoxColumn;
                            if (colorColumn.DataSource == null)
                            {
                                colorColumn.DataSource = GetColorNames();
                                colorColumn.DisplayMember = "ColorName";
                                colorColumn.ValueMember = "ColorID";
                            }

                            // Load size dropdown for this row
                            var sizeColumn = ColorSizeDataGrid.Columns["SIZE"] as DataGridViewComboBoxColumn;
                            if (sizeColumn.DataSource == null)
                            {
                                sizeColumn.DataSource = GetSizeNames();
                                sizeColumn.DisplayMember = "SizeName";
                                sizeColumn.ValueMember = "SizeID";
                            }

                            // Set cell values
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
        // ===========================================  Edit Button Click Event Handler
        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if we're entering edit mode or saving changes
                if (!SubmitBtn.Enabled)
                {
                    // Enable edit mode
                    SetFormEditable(true);
                    SubmitBtn.Enabled = true;
                    EditBtn.Text = "SAVE";
                    DeleteBtn.Enabled = false; // Disable delete while editing
                }
                else
                {
                    // Save changes
                    if (validation())
                    {
                        // Update master record
                        UpdateSaleOrderMaster(saleOrderId);

                        // Update detail records
                        UpdateSaleOrderDetail(saleOrderId);

                        // Update fabric records
                        UpdateSaleOrderFabric(saleOrderId);

                        MessageBox.Show("Sale order updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Disable edit mode
                        SetFormEditable(false);
                        SubmitBtn.Enabled = false;
                        EditBtn.Text = "Edit";
                        EditBtn.Enabled = true;
                        DeleteBtn.Enabled = true;

                        // Refresh data to show updated values
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

        // ===========================================  Updates sale order master record
        public void UpdateSaleOrderMaster(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateSaleOrderMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
                    cmd.Parameters.AddWithValue("@SaleOrderNo", txtSaleOrder.Text.Trim());
                    cmd.Parameters.AddWithValue("@CustomerID", GetNullableValue(CustomerBox));
                    cmd.Parameters.AddWithValue("@ProductID", GetNullableValue(ProductBox));
                    cmd.Parameters.AddWithValue("@Style", GetNullableText(txtStyle));
                    cmd.Parameters.AddWithValue("@CategoryID", GetNullableValue(CategoryBox));
                    cmd.Parameters.AddWithValue("@Range", GetNullableText(txtRange));
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

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ===========================================  Updates sale order detail records (UPDATE ONLY)
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

                            // Only process existing records that have PRE COLOR and PRE SIZE values
                            var preColorValue = row.Cells["PRE COLOR"].Value;
                            var preSizeValue = row.Cells["PRE SIZE"].Value;

                            if (preColorValue != null && preSizeValue != null)
                            {
                                // Existing record - Update only
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

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            // Skip rows without PRE COLOR or PRE SIZE (new rows should be handled by separate insert button)
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception("UpdateSaleOrderDetail Error: " + ex.Message);
                    }
                }
            }
        }

        // ===========================================  Updates sale order fabric records (UPDATE ONLY)
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

                            // Only process existing records that have PRE FABRIC value
                            var preFabricValue = row.Cells["PRE FABRIC"].Value;

                            if (preFabricValue != null)
                            {
                                // Existing record - Update only
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

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            // Skip rows without PRE FABRIC (new rows should be handled by separate insert button)
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw new Exception("UpdateSaleOrderFabric Error: " + ex.Message);
                    }
                }
            }
        }







        // ===========================================  Delete Button Click Event Handler
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this sale order? This action cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Delete detail records
                    DeleteSaleOrderDetail(saleOrderId);

                    // Delete fabric records
                    DeleteSaleOrderFabric(saleOrderId);

                    // Delete master record
                    DeleteSaleOrderMaster(saleOrderId);

                    MessageBox.Show("Sale order deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the form or navigate back
                    RestFormControler();
                    // Or you might want to close the form or navigate to another screen
                    // this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting sale order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===========================================  Deletes sale order master record
        public void DeleteSaleOrderMaster(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteSaleOrderMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ===========================================  Deletes sale order detail records
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

                            // Only process existing records that have PRE COLOR and PRE SIZE values
                            var preColorValue = row.Cells["PRE COLOR"].Value;
                            var preSizeValue = row.Cells["PRE SIZE"].Value;

                            if (preColorValue != null && preSizeValue != null)
                            {
                                // Delete existing record
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
                        throw new Exception("DeleteSaleOrderDetail Error: " + ex.Message);
                    }
                }
            }
        }

        // ===========================================  Deletes sale order fabric records
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

                            // Only process existing records that have PRE FABRIC value
                            var preFabricValue = row.Cells["PRE FABRIC"].Value;

                            if (preFabricValue != null)
                            {
                                // Delete existing record
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
                        throw new Exception("DeleteSaleOrderFabric Error: " + ex.Message);
                    }
                }
            }
        }



    }
}