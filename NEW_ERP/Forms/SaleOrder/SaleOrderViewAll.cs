using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SaleOrder
{
    public partial class SaleOrderViewAll : Form
    {
        public SaleOrderViewAll()
        {
            InitializeComponent();
        }

        private void SaleOrderViewAll_Load(object sender, EventArgs e)
        {
            LoadAllSaleOrderData();
        }

        private void SaleOrderBox_DropDown(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT SaleOrderNo, SaleOrderID FROM SaleOrderMaster where StatusCode='ACT' ORDER BY SaleOrderID DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
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


        //======================================= LOAD FORM FUNCTION =======================================

        private void LoadAllSaleOrderData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_GetAllSaleOrders", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SaleOrderDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading all sale orders:\n" + ex.Message);
                }
            }
        }


        //=======================================  SEARCH FUNCTION =======================================

        private void LoadSaleOrderDataById(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_SearchSaleOrderByID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SaleOrderDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading sale order by ID:\n" + ex.Message);
                }
            }
        }

        //=======================================  SEARCH BUTTON FUNCTION =======================================

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (SaleOrderBox.SelectedValue != null && int.TryParse(SaleOrderBox.SelectedValue.ToString(), out int orderId))
            {
                LoadSaleOrderDataById(orderId);
            }
            else
            {
                MessageBox.Show("Please select a valid Sale Order from the dropdown.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SaleOrderDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedRow = SaleOrderDataGridView.Rows[e.RowIndex];
                int saleOrderId = Convert.ToInt32(selectedRow.Cells["SaleOrderID"].Value);

                this.Close();

                SaleOrderAdd saleOrderAddForm = new SaleOrderAdd(saleOrderId, true);
                saleOrderAddForm.Show();
            }
        }


    }
}
