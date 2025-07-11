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

        private void SaleOrderAdd_Load(object sender, EventArgs e)
        {
            Customer();
            Product();
            Category();
            ShipMode();
            Embelishment();
            PackingType();
            FoldType();
            SaleType();
            Agent();
            Tolerance();
            FabricDataGridShow();
            ColorSizeDataGridShow();
        }

        //======================================= FABRIC DATA GRID SHOW =======================================

        public void FabricDataGridShow()
        {
            if (FabricDataGrid.Columns.Count > 0)
                FabricDataGrid.Columns.RemoveAt(0);

            //DataGridViewComboBoxColumn fabricColumn = new DataGridViewComboBoxColumn();
            //fabricColumn.HeaderText = "FABRIC";
            //fabricColumn.Name = "FABRIC";
            //fabricColumn.DataSource = GetFabricNames();
            //fabricColumn.DisplayMember = "FabricName";
            //fabricColumn.ValueMember = "FabricName";

            //FabricDataGrid.Columns.Insert(0, fabricColumn);
            FabricDataGrid.Columns.Add("FABRIC", "FABRIC");
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
            //FabricDataGrid.EditingControlShowing += FabricTypeDataGrid_EditingControlShowing;
        }

        //======================================= COLOR AND SIZE DATA GRID SHOW =======================================

        public void ColorSizeDataGridShow()
        {
            if (ColorSizeDataGrid.Columns.Count > 0)
                ColorSizeDataGrid.Columns.RemoveAt(0);

            DataGridViewComboBoxColumn ColorColumn = new DataGridViewComboBoxColumn();
            ColorColumn.HeaderText = "COLOR";
            ColorColumn.Name = "COLOR";
            ColorColumn.DataSource = GetColorNames();
            ColorColumn.DisplayMember = "ColorName";
            ColorColumn.ValueMember = "ColorID";

            DataGridViewComboBoxColumn SizeColumn = new DataGridViewComboBoxColumn();
            SizeColumn.HeaderText = "SIZE";
            SizeColumn.Name = "SIZE";
            SizeColumn.DataSource = GetSizeNames();
            SizeColumn.DisplayMember = "SizeName";
            SizeColumn.ValueMember = "SizeID";

            ColorSizeDataGrid.Columns.Insert(0, ColorColumn);
            ColorSizeDataGrid.Columns.Insert(1, SizeColumn);
            ColorSizeDataGrid.Columns.Add("QUANTITY", "QUANTITY");
            ColorSizeDataGrid.Columns.Add("UNIT PRICE", "UNIT PRICE");
            ColorSizeDataGrid.Columns.Add("TOAL PRICE", "TOAL PRICE");


            ColorSizeDataGrid.DefaultCellStyle.Font = new Font("Arial", 11);
            ColorSizeDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            ColorSizeDataGrid.Columns[0].Width = 250;
            ColorSizeDataGrid.Columns[1].Width = 150;

            // Hook event
            ColorSizeDataGrid.EditingControlShowing += ColorSizeDataGrid_EditingControlShowing;
        }

        private void ColorSizeDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (ColorSizeDataGrid.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox comboBox)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }

        //======================================= SHOWING COLORS AND SIZE =======================================

        private DataTable GetSizeNames()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "select SizeID, SizeName   from SizeMaster";
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
                string query = "select ColorID, ColorName   from ColorMaster";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        //======================================= CUSTOMER NAME SHOW =======================================

        protected void Customer()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select CustomerID, CustomerName  from CustomerMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CustomerBox.DataSource = dt;
                    CustomerBox.DisplayMember = "CustomerName";
                    CustomerBox.ValueMember = "CustomerID";
                    CustomerBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= PRODUCT NAME SHOW =======================================

        protected void Product()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select ProductCode, ProductShortName from ItemMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ProductBox.DataSource = dt;
                    ProductBox.DisplayMember = "ProductShortName";
                    ProductBox.ValueMember = "ProductCode";
                    ProductBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= CATEGORY NAME SHOW =======================================

        protected void Category()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select CategoryID, CategoryName from CategoryMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CategoryBox.DataSource = dt;
                    CategoryBox.DisplayMember = "CategoryName";
                    CategoryBox.ValueMember = "CategoryID";
                    CategoryBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= SHIP MODE NAME SHOW =======================================

        protected void ShipMode()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select ShipModeID, ShipModeName from ShipmentModeMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ShipModeBox.DataSource = dt;
                    ShipModeBox.DisplayMember = "ShipModeName";
                    ShipModeBox.ValueMember = "ShipModeID";
                    ShipModeBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= EMBELISHMENT NAME SHOW =======================================

        protected void Embelishment()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select EmbellishmentID, EmbellishmentName from EmbellishmentMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    EmbelishmentBox.DataSource = dt;
                    EmbelishmentBox.DisplayMember = "EmbellishmentName";
                    EmbelishmentBox.ValueMember = "EmbellishmentID";
                    EmbelishmentBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= PACKING TYPE  NAME SHOW =======================================

        protected void PackingType()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select PackingTypeID, PackingType from PackingTypeMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    PackingTypeBox.DataSource = dt;
                    PackingTypeBox.DisplayMember = "PackingType";
                    PackingTypeBox.ValueMember = "PackingTypeID";
                    PackingTypeBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= FOLD TYPE  NAME SHOW =======================================

        protected void FoldType()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select FoldTypeID, FoldTypeName from FoldTypeMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    FoldTypeBox.DataSource = dt;
                    FoldTypeBox.DisplayMember = "FoldTypeName";
                    FoldTypeBox.ValueMember = "FoldTypeID";
                    FoldTypeBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= SALE TYPE  NAME SHOW =======================================

        protected void SaleType()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select SaleTypeID, SaleType from SaleTypeMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SaleTypeBox.DataSource = dt;
                    SaleTypeBox.DisplayMember = "SaleType";
                    SaleTypeBox.ValueMember = "SaleTypeID";
                    SaleTypeBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= AGENT NAME SHOW =======================================

        protected void Agent()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select AgentID, AgentName from AgentMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    AgentBox.DataSource = dt;
                    AgentBox.DisplayMember = "AgentName";
                    AgentBox.ValueMember = "AgentID";
                    AgentBox.SelectedIndex = -1;
                }
            }
        }


        //======================================= TOLERANCE NAME SHOW =======================================

        protected void Tolerance()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select ToleranceID, TolerancePercent  from ToleranceMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ToleranceBox.DataSource = dt;
                    ToleranceBox.DisplayMember = "TolerancePercent";
                    ToleranceBox.ValueMember = "ToleranceID";
                    ToleranceBox.SelectedIndex = -1;
                }
            }
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
    }
}
