using NEW_ERP.GernalClasses;
using NEW_ERP.Template;
using System;
using System.Data;
using System.Data.SqlClient;
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

            ShowSaleOrder();
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

      
    }
}
