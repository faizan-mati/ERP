using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SaleOrder
{
    public partial class SaleOrderViewAll : Form
    {
        //======================================= Constructor and Form Events =======================================
        public SaleOrderViewAll()
        {
            InitializeComponent();
        }

        private void SaleOrderViewAll_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllSaleOrderData();
            }
            catch (Exception ex)
            {
                HandleError("Error loading sale orders", ex);
            }
        }

        //======================================= Data Loading Methods =======================================
        private void LoadAllSaleOrderData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("sp_GetAllSaleOrders", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }

                    SaleOrderDataGridView.DataSource = dt;
                    ConfigureDataGridView();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load all sale orders", ex);
            }
        }

        private void LoadSaleOrderDataById(int saleOrderId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("sp_SearchSaleOrderByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);
                    conn.Open();

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }

                    SaleOrderDataGridView.DataSource = dt;
                    ConfigureDataGridView();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load sale order with ID {saleOrderId}", ex);
            }
        }

        private void ConfigureDataGridView()
        {
            if (SaleOrderDataGridView.Columns.Contains("SaleOrderID"))
            {
                SaleOrderDataGridView.Columns["SaleOrderID"].Visible = false;
            }
        }

        //======================================= Dropdown Methods =======================================
        private void SaleOrderBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT StatusId, StatusCode FROM Status
                      ORDER BY StatusId DESC", con))
                {
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }

                    SaleOrderBox.DataSource = dt;
                    SaleOrderBox.DisplayMember = "StatusCode";
                    SaleOrderBox.ValueMember = "StatusId";
                    SaleOrderBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading sale order dropdown", ex);
            }
        }

        //======================================= Event Handlers =======================================
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaleOrderBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select a sale order from the dropdown.",
                        "Invalid Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(SaleOrderBox.SelectedValue.ToString(), out int orderId))
                {
                    MessageBox.Show("Invalid sale order ID selected.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                LoadSaleOrderDataById(orderId);
            }
            catch (Exception ex)
            {
                HandleError("Error searching for sale order", ex);
            }
        }

        private void SaleOrderDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow selectedRow = SaleOrderDataGridView.Rows[e.RowIndex];

                if (!selectedRow.Cells["SaleOrderID"].Value.IsValidInt(out int saleOrderId))
                {
                    MessageBox.Show("Invalid sale order selected.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                this.Close();

                using (SaleOrderAdd saleOrderAddForm = new SaleOrderAdd(saleOrderId, true))
                {
                    saleOrderAddForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error opening sale order for editing", ex);
            }
        }

        //======================================= Helper Methods =======================================
        private void HandleError(string message, Exception ex)
        {
            MessageBox.Show($"{message}:\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    //======================================= Extension Methods =======================================
    public static class Extensions
    {
        public static bool IsValidInt(this object value, out int result)
        {
            result = 0;
            if (value == null || value == DBNull.Value) return false;
            return int.TryParse(value.ToString(), out result);
        }
    }
}