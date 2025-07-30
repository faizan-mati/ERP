using NEW_ERP.GernalClasses;
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

namespace NEW_ERP.Forms.CustomerMaster
{
    public partial class CustomerViewAll : Form
    {

        public CustomerViewAll()
        {
            InitializeComponent();
        }


        private void CustomerViewAll_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        #region Data Loading Functions

        private void LoadCustomerData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT [CustomerID]
                                          ,[CustomerCode]
                                          ,[CustomerName]
                                          ,[ContactPerson]
                                          ,[MobileNo]
                                          ,[WhatsAppNo]
                                          ,[Email]
                                          ,[Address]
                                          ,[City]
                                          ,[State]
                                          ,[Country]
                                          ,[ZipCode]
                                          ,[CustomerTypeID]
                                          ,[GSTNo]
                                          ,[NTN]
                                          ,[CreditLimit]
                                          ,[IsActive]
                                          ,[CreatedDate]
                                          ,[CreatedBy]
                                          ,[UpdatedDate]
                                          ,[UpdatedBy]
                                          ,[StatusCode] 
                                     FROM CustomerMaster where StatusCode='ACT' 
                                     ORDER BY CreatedDate DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CustomerDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data:\n" + ex.Message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Search button click event handler - Searches customers by name
        /// </summary>
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_SearchCustomerByName", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        CustomerDataGridView.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Search failed:\n" + ex.Message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// DataGridView cell double-click event handler - Opens selected customer for editing
        /// </summary>
        private void CustomerDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var selectedRow = CustomerDataGridView.Rows[e.RowIndex];
                var value = selectedRow.Cells["CustomerID"].Value;

                if (value == null || !int.TryParse(value.ToString(), out int customerID))
                {
                    MessageBox.Show("Invalid Country ID selected.",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                    return;
                }

                OpenCustomerForEditing(customerID);
            }
            catch (Exception ex)
            {
                ShowError("Error handling grid double click", ex);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Opens the CustomerFormAdd form in edit mode for the specified customer
        /// </summary>
        private void OpenCustomerForEditing(int customerID)
        {
            try
            {
                this.Close();
                using (var CustomerFormAdd = new CustomerFormAdd(customerID, true))
                {
                    CustomerFormAdd.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowError("Error opening customer for editing", ex);
                throw;
            }
        }

        /// <summary>
        /// Displays an error message to the user
        /// </summary>
        private void ShowError(string context, Exception ex)
        {
            MessageBox.Show($"{context}:\n{ex.Message}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        #endregion

    }
}