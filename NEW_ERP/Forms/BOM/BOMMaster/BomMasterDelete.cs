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

namespace NEW_ERP.Forms.BOM.BOMMaster
{
    public partial class BomMasterDelete : DeleteFormTemplate
    {
        public BomMasterDelete()
        {
            InitializeComponent();
        }

        private void BomMasterDelete_Load(object sender, EventArgs e)
        {
            BomMasterIdShow();
            SaleOrderBox.DataSource = null;
            ProductBox.DataSource = null;

        }


        //======================================= LOAD ALL SALE ORDERS =======================================

        protected void LoadSaleOrders()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT SaleOrderID, SaleOrderNo FROM SaleOrderMaster";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SaleOrderBox.DataSource = dt;
                    SaleOrderBox.DisplayMember = "SaleOrderNo";
                    SaleOrderBox.ValueMember = "SaleOrderID";
                    SaleOrderBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= LOAD PRODUCTS BASED ON SALE ORDER =======================================

        protected void LoadProducts(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"
        SELECT P.ProductCode, P.ProductDescription
        FROM SaleOrderMaster SM
        INNER JOIN ItemMaster P ON SM.ProductID = P.ProductCode
        WHERE SM.SaleOrderID = @SaleOrderID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ProductBox.DataSource = dt;
                    ProductBox.DisplayMember = "ProductDescription";
                    ProductBox.ValueMember = "ProductCode";
                    ProductBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= BOM MASTER ID COMBO =======================================

        public void BomMasterIdShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT BOMID FROM BOMMaster";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    BomMasterIdBox.DataSource = dt;
                    BomMasterIdBox.DisplayMember = "BOMID";
                    BomMasterIdBox.ValueMember = "BOMID";
                    BomMasterIdBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= BOM ID SELECTED =======================================

        private void BomMasterIdBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BomMasterIdBox.SelectedIndex != -1 && BomMasterIdBox.SelectedValue != null)
            {
                string bomId = BomMasterIdBox.SelectedValue.ToString();

                if (int.TryParse(bomId, out _))
                {
                    LoadBomDetails(bomId);
                }
            }
        }


        //======================================= LOAD BOM DETAILS (SALE ORDER + PRODUCT) =======================================

        public void LoadBomDetails(string bomId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT ProductID, SaleOrderID, VersionNo FROM BOMMaster WHERE BOMID = @BomId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!int.TryParse(bomId, out int bomIdInt))
                    {
                        MessageBox.Show("Invalid BOM ID format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    cmd.Parameters.AddWithValue("@BomId", bomIdInt);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtVersionNo.Text = reader["VersionNo"].ToString();

                        int saleOrderId = Convert.ToInt32(reader["SaleOrderID"]);
                        int productId = Convert.ToInt32(reader["ProductID"]);

                        LoadSaleOrders();

                        SaleOrderBox.SelectedValue = saleOrderId;

                        LoadProducts(saleOrderId);
                        ProductBox.SelectedValue = productId;
                    }
                    else
                    {
                        txtVersionNo.Clear();
                        SaleOrderBox.DataSource = null;
                        ProductBox.DataSource = null;
                    }
                }
            }
        }

        //======================================= DELETE BUTTON =======================================

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (BomMasterIdBox.SelectedValue == null || BomMasterIdBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a BOM ID to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirm deletion
            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this BOM?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog != DialogResult.Yes)
                return;

            try
            {
                int bomId = Convert.ToInt32(BomMasterIdBox.SelectedValue);

                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteBOMMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BOMID", bomId);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("BOM deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestFormControler();
                            BomMasterIdShow();    
                        }
                        else
                        {
                            MessageBox.Show("No BOM found with that ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= REST FORM =======================================

        public void RestFormControler()
        {
            txtVersionNo.Clear();

            SaleOrderBox.SelectedIndex = -1;
            ProductBox.SelectedIndex = -1;
            BomMasterIdBox.SelectedIndex = -1;

        }



    }
}
