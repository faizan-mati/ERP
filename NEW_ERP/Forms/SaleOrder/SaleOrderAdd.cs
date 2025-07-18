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

        #endregion

        #region Constructor & Form Load
    
        public SaleOrderAdd()
        {
            InitializeComponent();
        }

        private void SaleOrderAdd_Load(object sender, EventArgs e)
        {
            FabricDataGridShow();
            ColorSizeDataGridShow();
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
            {
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

                    this.BeginInvoke((Action)(() => { box.DroppedDown = true; }));
                }
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

        // ===========================================  Handles edit button click - opens the edit form
     
        private void EditBtn_Click(object sender, EventArgs e)
        {
            SaleOrderEdit editForm = new SaleOrderEdit();
            editForm.Show();
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
    }
}