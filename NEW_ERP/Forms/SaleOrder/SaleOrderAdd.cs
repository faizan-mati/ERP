using NEW_ERP.GernalClasses;
using NEW_ERP.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SaleOrder
{
    public partial class SaleOrderAdd : AddFormTemplate
    {
        public SaleOrderAdd()
        {
            InitializeComponent();
        }

        // ========================== FORM LOAD ==========================
        private void SaleOrderAdd_Load(object sender, EventArgs e)
        {
            FabricDataGridShow();
            ColorSizeDataGridShow();
        }

        // ========================== FABRIC GRID SETUP ==========================
        public void FabricDataGridShow()
        {
            FabricDataGrid.Columns.Clear();

            var fabricColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "FABRIC",
                Name = "FABRIC"
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

            FabricDataGrid.Columns[0].Width = 250;
            FabricDataGrid.Columns[1].Width = 150;

            FabricDataGrid.EditingControlShowing += FabricDataGrid_EditingControlShowing;
            FabricDataGrid.CellClick += FabricDataGrid_CellClick;
        }

        private void FabricDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (FabricDataGrid.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox comboBox)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }

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

        // ========================== COLOR + SIZE GRID SETUP ==========================
        public void ColorSizeDataGridShow()
        {
            ColorSizeDataGrid.Columns.Clear();

            var colorColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "COLOR",
                Name = "COLOR"
            };

            var sizeColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "SIZE",
                Name = "SIZE"
            };

            ColorSizeDataGrid.Columns.Add(colorColumn);
            ColorSizeDataGrid.Columns.Add(sizeColumn);
            ColorSizeDataGrid.Columns.Add("QUANTITY", "QUANTITY");
            ColorSizeDataGrid.Columns.Add("UNIT PRICE", "UNIT PRICE");
            ColorSizeDataGrid.Columns.Add("TOAL PRICE", "TOAL PRICE");

            ColorSizeDataGrid.DefaultCellStyle.Font = new Font("Arial", 11);
            ColorSizeDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            ColorSizeDataGrid.Columns[0].Width = 250;
            ColorSizeDataGrid.Columns[1].Width = 150;

            ColorSizeDataGrid.EditingControlShowing += ColorSizeDataGrid_EditingControlShowing;
            ColorSizeDataGrid.CellClick += ColorSizeDataGrid_CellClick;
        }

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

        private void ColorSizeDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0) // COLOR
                {
                    var comboCol = ColorSizeDataGrid.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
                    if (comboCol != null && comboCol.DataSource == null)
                    {
                        comboCol.DataSource = GetColorNames();
                        comboCol.DisplayMember = "ColorName";
                        comboCol.ValueMember = "ColorID";
                    }
                }
                else if (e.ColumnIndex == 1) // SIZE
                {
                    var comboCol = ColorSizeDataGrid.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
                    if (comboCol != null && comboCol.DataSource == null)
                    {
                        comboCol.DataSource = GetSizeNames();
                        comboCol.DisplayMember = "SizeName";
                        comboCol.ValueMember = "SizeID";
                    }
                }
            }
        }

        // ========================== LOAD FABRIC / COLOR / SIZE ==========================

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


        //======================================= LOAD DROP DOWN SHOW ALL =======================================

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

                    this.BeginInvoke((Action)(() =>
                    {
                        box.DroppedDown = true;
                    }));
                }
            }
        }

        //======================================= CUSTOMER NAME SHOW =======================================

        private bool isCustomerLoaded = false;

        private void CustomerBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(CustomerBox, ref isCustomerLoaded,
                "SELECT CustomerID, CustomerName FROM CustomerMaster",
                "CustomerName", "CustomerID");
        }

        //======================================= PRODUCT NAME SHOW =======================================

        private bool isProductLoaded = false;

        private void ProductBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(ProductBox, ref isProductLoaded,
                "SELECT ProductCode, ProductShortName FROM ItemMaster",
                "ProductShortName", "ProductCode");
        }

        //======================================= CATEGORY NAME SHOW =======================================

        private bool isCategoryLoaded = false;

        private void CategoryBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(CategoryBox, ref isCategoryLoaded,
               "select CategoryID, CategoryName from CategoryMaster",
               "CategoryName", "CategoryID");
        }

        //======================================= SHIP MODE NAME SHOW =======================================

        private bool isShipModeLoaded = false;

        private void ShipModeBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(ShipModeBox, ref isShipModeLoaded,
               "select ShipModeID, ShipModeName from ShipmentModeMaster",
               "ShipModeName", "ShipModeID");
        }

        //======================================= EMBELISHMENT NAME SHOW =======================================
      
        private bool isEmblishmenLoaded = false;

        private void EmbelishmentBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(EmbelishmentBox, ref isEmblishmenLoaded,
              "select EmbellishmentID, EmbellishmentName from EmbellishmentMaster",
              "EmbellishmentName", "EmbellishmentID");
        }

        //======================================= PACKING TYPE  NAME SHOW =======================================

        private bool isPackingTypeLoaded = false;

        private void PackingTypeBox_DragDrop(object sender, DragEventArgs e)
        {
            LoadDropdown(PackingTypeBox, ref isPackingTypeLoaded,
               "select PackingTypeID, PackingType from PackingTypeMaster",
               "PackingType", "PackingTypeID");
        }

        //======================================= FOLD TYPE  NAME SHOW =======================================

        private bool isFoldTypeLoaded = false;

        private void FoldTypeBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(FoldTypeBox, ref isFoldTypeLoaded,
               "select FoldTypeID, FoldTypeName from FoldTypeMaster",
               "FoldTypeName", "FoldTypeID");
        }

        //======================================= SALE TYPE  NAME SHOW =======================================

        private bool isSaleTypeLoaded = false;

        private void SaleTypeBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(SaleTypeBox, ref isSaleTypeLoaded,
               "select SaleTypeID, SaleType from SaleTypeMaster",
               "SaleType", "SaleTypeID");
        }

        //======================================= AGENT NAME SHOW =======================================

        private bool isAgentLoaded = false;

        private void AgentBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(AgentBox, ref isAgentLoaded,
               "select AgentID, AgentName from AgentMaster",
               "AgentName", "AgentID");
        }
       
        //======================================= TOLERANCE NAME SHOW =======================================

        private bool isToleranceLoaded = false;

        private void ToleranceBox_DropDown(object sender, EventArgs e)
        {
            LoadDropdown(ToleranceBox, ref isToleranceLoaded,
                 "select ToleranceID, TolerancePercent  from ToleranceMaster",
                 "TolerancePercent", "ToleranceID");
        }


        //======================================= SUBMIT BUTTON =======================================

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                SaleOrderMaster();
                SaleOrderDetail();
                SaleOrderFabric();

                RestFormControler();
            }
        }

        //======================================= SALE ORDER MASTER =======================================

        public void SaleOrderMaster()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    string SelectedCustomerId = CustomerBox.SelectedValue.ToString();
                    string SelectedProductId = ProductBox.SelectedValue.ToString();

                    string SelectedCategoryId = CategoryBox.SelectedValue.ToString();
                    string SelectedSaleTypeId = SaleTypeBox.SelectedValue.ToString();

                    string SelectedAgentId = AgentBox.SelectedValue.ToString();
                    string SelectedShipModeId = ShipModeBox.SelectedValue.ToString();

                    string SelectedEmbelishmentId = EmbelishmentBox.SelectedValue.ToString();
                    string SelectedPackingTypeId = PackingTypeBox.SelectedValue.ToString();

                    string SelectedFoldTypeId = FoldTypeBox.SelectedValue.ToString();
                    string SelectedToleranceId = ToleranceBox.SelectedValue.ToString();

                    using (SqlCommand cmd = new SqlCommand("", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CustomerId", Convert.ToInt32(SelectedCustomerId));
                        cmd.Parameters.AddWithValue("@ProductId", Convert.ToInt32(SelectedProductId));

                        cmd.Parameters.AddWithValue("@CategoryId", Convert.ToInt32(SelectedCategoryId));
                        cmd.Parameters.AddWithValue("@SaleTypeId", Convert.ToInt32(SelectedSaleTypeId));

                        cmd.Parameters.AddWithValue("@AgentId", Convert.ToInt32(SelectedAgentId));
                        cmd.Parameters.AddWithValue("@ShipModeId", Convert.ToInt32(SelectedShipModeId));

                        cmd.Parameters.AddWithValue("@EmbelishmentId", Convert.ToInt32(SelectedEmbelishmentId));
                        cmd.Parameters.AddWithValue("@PackingTypeId", Convert.ToInt32(SelectedPackingTypeId));

                        cmd.Parameters.AddWithValue("@FoldTypeId", Convert.ToInt32(SelectedFoldTypeId));
                        cmd.Parameters.AddWithValue("@ToleranceId", Convert.ToInt32(SelectedToleranceId));

                        cmd.Parameters.AddWithValue("@SaleOrder", txtSaleOrder.Text.Trim());
                        cmd.Parameters.AddWithValue("@Style", txtStyle.Text.Trim());

                        cmd.Parameters.AddWithValue("@Range", txtRange.Text.Trim());
                        cmd.Parameters.AddWithValue("@FactoryPrice", txtFactoryPrice.Text.Trim());

                        cmd.Parameters.AddWithValue("@Commission", txtCommission.Text.Trim());
                        cmd.Parameters.AddWithValue("@Total", txtTotal.Text.Trim());

                        cmd.Parameters.AddWithValue("@OrderDateTime", OrderDateTimePicker.Value);
                        cmd.Parameters.AddWithValue("@ExFactoryDateTime", ExFactoryDateTimePicker.Value);
                        cmd.Parameters.AddWithValue("@ETADateTime", ETADateTimePicker.Value);

                        conn.Open();

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Sale Order inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           
                        }
                        else
                        {
                            MessageBox.Show("Sale Order Insertion failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //======================================= SALE ORDER DETAIL =======================================

        public void SaleOrderDetail()
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
                            if (row.IsNewRow) continue;
                            if (row.Cells["COLOR"].Value == null) continue;

                            using (SqlCommand cmd = new SqlCommand("", conn, tran)) 
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                int ColorId = Convert.ToInt32(row.Cells["COLOR"].Value);
                                int SizeId = Convert.ToInt32(row.Cells["SIZE"].Value);
                                int Quantity = Convert.ToInt32(row.Cells["QUANTITY"].Value);
                                decimal UnitPrice = Convert.ToDecimal(row.Cells["UNIT PRICE"].Value); 
                                decimal TotalPrice = Convert.ToDecimal(row.Cells["TOAL PRICE"].Value);

                                cmd.Parameters.Add("@ColorId", SqlDbType.Int).Value = ColorId;
                                cmd.Parameters.Add("@SizeId", SqlDbType.Int).Value = SizeId;
                                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Quantity;
                                cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = UnitPrice;
                                cmd.Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = TotalPrice;

                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                        MessageBox.Show("Sale order details saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Failed to save sale order details:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //======================================= SALE ORDER FABRIC =======================================

        public void SaleOrderFabric()
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
                            if (row.IsNewRow) continue;
                            if (row.Cells["FABRIC"].Value == null) continue;

                            using (SqlCommand cmd = new SqlCommand("", conn, tran))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                // Read and convert values
                                int fabricId = Convert.ToInt32(row.Cells["FABRIC"].Value);
                                string type = Convert.ToString(row.Cells["TYPE"].Value ?? "");
                                int gsm = Convert.ToInt32(row.Cells["GSM"].Value ?? "0");
                                decimal width = Convert.ToDecimal(row.Cells["WIDTH"].Value ?? "0");
                                decimal dia = Convert.ToDecimal(row.Cells["DIA"].Value ?? "0");
                                string gauge = Convert.ToString(row.Cells["GAUGE"].Value ?? "");
                                int shirnk = Convert.ToInt32(row.Cells["SHIRNK"].Value ?? "0");
                                string stitchLength = Convert.ToString(row.Cells["STITCH LENGTH"].Value ?? "");

                                // Add parameters with correct SqlDbType
                                cmd.Parameters.Add("@FabricId", SqlDbType.Int).Value = fabricId;
                                cmd.Parameters.Add("@Type", SqlDbType.VarChar, 50).Value = type;
                                cmd.Parameters.Add("@GSM", SqlDbType.Int).Value = gsm;
                                cmd.Parameters.Add("@Width", SqlDbType.Decimal).Value = width;
                                cmd.Parameters.Add("@DIA", SqlDbType.Decimal).Value = dia;
                                cmd.Parameters.Add("@Gauge", SqlDbType.VarChar, 20).Value = gauge;
                                cmd.Parameters.Add("@Shirnk", SqlDbType.Int).Value = shirnk;
                                cmd.Parameters.Add("@StitchLength", SqlDbType.VarChar, 50).Value = stitchLength;

                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                        MessageBox.Show("Fabric sale order saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Failed to save fabric sale order:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //======================================= FOR VALIDATION =======================================

        public bool validation()
        {
            if (string.IsNullOrWhiteSpace(txtSaleOrder.Text) ||
                CustomerBox.SelectedIndex == -1 || ShipModeBox.SelectedIndex == -1 ||
                ProductBox.SelectedIndex == -1 || CategoryBox.SelectedIndex == -1 ||

                  (FabricDataGrid.Rows.Count > 0 &&
                 (FabricDataGrid.Rows[0].Cells["FABRIC"].Value == null ||
                  string.IsNullOrWhiteSpace(FabricDataGrid.Rows[0].Cells["FABRIC"].Value.ToString()))) ||

                (ColorSizeDataGrid.Rows.Count > 0 &&
                 (ColorSizeDataGrid.Rows[0].Cells["COLOR"].Value == null || ColorSizeDataGrid.Rows[1].Cells["SIZE"].Value == null ||
                  string.IsNullOrWhiteSpace(ColorSizeDataGrid.Rows[0].Cells["COLOR"].Value.ToString())))

                )
            {
                MessageBox.Show("Please Fill All the Fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //======================================= REST FORM =======================================

        public void RestFormControler()
        {
            // Clear TextBoxes
            txtSaleOrder.Clear();
            txtStyle.Clear();
            txtRange.Clear();
            txtFactoryPrice.Clear();
            txtCommission.Clear();
            txtTotal.Clear();

            // Reset ComboBoxes
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

            // Reset DateTimePickers
            OrderDateTimePicker.Value = DateTime.Now;
            ExFactoryDateTimePicker.Value = DateTime.Now;
            ETADateTimePicker.Value = DateTime.Now;

            // Clear DataGridViews
            FabricDataGrid.Rows.Clear();
            ColorSizeDataGrid.Rows.Clear();
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

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
