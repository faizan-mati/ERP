using NEW_ERP.GernalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            LoadStatustCodes();
        }

        #region Data Loading Functions

        //======================================= Load Customer Data =======================================
        private void LoadCustomerData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT 
    cm.[CustomerID],
    cm.[CustomerCode],
    cm.[CustomerName],
    cm.[ContactPerson],
    cm.[MobileNo],
    cm.[WhatsAppNo],
    cm.[Email],
    cm.[Address],
    cm.[City],
    cm.[State],
    cm.[Country],
    cm.[ZipCode],
    cm.[CustomerTypeID],
    cm.[GSTNo],
    cm.[NTN],
    cm.[CreditLimit],
    cm.[IsActive],
    cm.[CreatedDate],
    cm.[CreatedBy],
    cm.[UpdatedDate],
    cm.[UpdatedBy],
    s.[StatusCode],        
    s.[StatusDescription]  
FROM CustomerMaster cm
LEFT JOIN Status s ON cm.StatusCode = s.StatusId 
WHERE cm.StatusCode = 2
ORDER BY cm.CreatedDate DESC;
";

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

        //======================================= Load Status Codes =======================================
        private void LoadStatustCodes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = "SELECT StatusId, StatusCode FROM Status";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DataTable dtRoles = new DataTable();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        dtRoles.Load(sdr);

                        StatsuCodeBox.DataSource = dtRoles;
                        StatsuCodeBox.DisplayMember = "StatusCode";
                        StatsuCodeBox.ValueMember = "StatusId";
                        StatsuCodeBox.SelectedIndex = -1;


                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading sale order dropdown", ex);
            }
        }


        #endregion

        #region Event Handlers

        //======================================= Search Button Click =======================================
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (StatsuCodeBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select a Status code", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand("sp_SearchCustomerByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatsuCodeBox));

                    var adapter = new SqlDataAdapter(command);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    CustomerDataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error searching for country", ex);
            }


        }

        //======================================= Get Nullable Value – Combo =======================================
        private object GetNullableValue(ComboBox comboBox)
        {
            return comboBox.SelectedItem != null ? comboBox.SelectedValue : DBNull.Value;
        }


        //======================================= Customer Data Grid View Cell Double Click =======================================
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

        //======================================= Open Customer For Editing =======================================
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

        //======================================= Show Error =======================================
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