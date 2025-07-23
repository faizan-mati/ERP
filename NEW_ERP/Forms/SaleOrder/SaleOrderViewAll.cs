using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SaleOrder
{
    /// <summary>
    /// Form for viewing and searching all sale orders
    /// </summary>
    public partial class SaleOrderViewAll : Form
    {
        #region Constructor and Form Events

        /// <summary>
        /// Initializes a new instance of the SaleOrderViewAll form
        /// </summary>
        public SaleOrderViewAll()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form load event handler
        /// </summary>
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

        #endregion

        #region Data Loading Methods

        /// <summary>
        /// Loads all sale order data into the DataGridView
        /// </summary>
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

        /// <summary>
        /// Loads sale order data for a specific ID into the DataGridView
        /// </summary>
        /// <param name="saleOrderId">The ID of the sale order to load</param>
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

        /// <summary>
        /// Configures the DataGridView appearance and behavior
        /// </summary>
        private void ConfigureDataGridView()
        {
            if (SaleOrderDataGridView.Columns.Contains("SaleOrderID"))
            {
                SaleOrderDataGridView.Columns["SaleOrderID"].Visible = false;
            }
        }

        #endregion

        #region Dropdown Methods

        /// <summary>
        /// Loads the sale order dropdown with active sale orders
        /// </summary>
        private void SaleOrderBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT SaleOrderNo, SaleOrderID 
                      FROM SaleOrderMaster 
                      WHERE StatusCode='ACT' 
                      ORDER BY SaleOrderID DESC", con))
                {
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }

                    SaleOrderBox.DataSource = dt;
                    SaleOrderBox.DisplayMember = "SaleOrderNo";
                    SaleOrderBox.ValueMember = "SaleOrderID";
                    SaleOrderBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading sale order dropdown", ex);
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Search button click event handler
        /// </summary>
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

        /// <summary>
        /// DataGridView cell double-click event handler - opens selected sale order for editing
        /// </summary>
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

        #endregion

        #region Helper Methods

        /// <summary>
        /// Handles errors consistently throughout the form
        /// </summary>
        /// <param name="message">The context message for the error</param>
        /// <param name="ex">The exception that occurred</param>
        private void HandleError(string message, Exception ex)
        {
            MessageBox.Show($"{message}:\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            // Log the error if needed
            // Logger.LogError(ex, message);
        }

        #endregion
    }

    /// <summary>
    /// Extension methods for common validation tasks
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Checks if an object can be converted to a valid integer
        /// </summary>
        public static bool IsValidInt(this object value, out int result)
        {
            result = 0;
            if (value == null || value == DBNull.Value) return false;
            return int.TryParse(value.ToString(), out result);
        }
    }
}