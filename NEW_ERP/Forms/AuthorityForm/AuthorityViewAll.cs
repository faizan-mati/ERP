using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.AuthorityForm
{
    public partial class AuthorityViewAll : Form
    {
        public AuthorityViewAll()
        {
            InitializeComponent();
        }

        private void AuthorityViewAll_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                AuthorityCodeShow();
            }
            catch (Exception ex)
            {
                HandleError("Error loading form data", ex);
            }
        }

        //======================================= Load Authority Codes =======================================
        private void AuthorityCodeShow()
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

                        AuthorityBox.DataSource = dtRoles;
                        AuthorityBox.DisplayMember = "StatusCode";
                        AuthorityBox.ValueMember = "StatusId";
                        AuthorityBox.SelectedIndex = -1;

                     
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading sale order dropdown", ex);
            }
        }

        //======================================= Load Authority Data =======================================
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();

                    string query = @"SELECT 
    AM.AuthorityCode,
    AM.AuthorityName,
    AM.UserCode,
    AM.SystemDate,
    S.StatusCode,         
    S.StatusDescription,  
    AM.Remarks
FROM AuthorityMaster AM
LEFT JOIN Status S ON AM.StatusCode = S.StatusId 
WHERE AM.StatusCode = 2
ORDER BY AM.SystemDate DESC;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        AuthorityDataGridView.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading authority data", ex);
                throw;
            }
        }

        //======================================= Search Authority =======================================
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (AuthorityBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select an authority name first.", "Information",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                };

                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_SearchAuthorityMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StatusCode", GetNullableValue(AuthorityBox));

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        AuthorityDataGridView.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error searching authority records", ex);
            }
        }


        //======================================= Get Nullable Value – Combo =======================================
        private object GetNullableValue(ComboBox comboBox)
        {
            return comboBox.SelectedItem != null ? comboBox.SelectedValue : DBNull.Value;
        }

        //======================================= Grid Double Click Handler =======================================
        private void AuthorityDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var selectedRow = AuthorityDataGridView.Rows[e.RowIndex];

                var supplierCodeCell = selectedRow.Cells["AuthorityCode"]?.Value ??
                                     selectedRow.Cells["AuthorityCode"]?.Value;

                if (supplierCodeCell == null || string.IsNullOrWhiteSpace(supplierCodeCell.ToString()))
                {
                    MessageBox.Show("Please select a valid supplier type record.",
                                  "Selection Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }

                string supplierCode = supplierCodeCell.ToString().Trim();
                OpenSupplierForEditing(supplierCode);
            }
            catch (Exception ex)
            {
                ShowError("Error handling grid selection", ex);
            }
        }

        private void OpenSupplierForEditing(string authorityCode)
        {
            try
            {
                this.Hide();

                using (var supplierTypeAddForm = new AuthorityAdd(authorityCode, true))
                {
                    DialogResult result = supplierTypeAddForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        string currentFilter = AuthorityBox.SelectedValue?.ToString() ?? "ACT";
                        LoadSupplierData(currentFilter);
                    }
                }

                this.Show();
            }
            catch (Exception ex)
            {
                ShowError("Error opening supplier for editing", ex);
                this.Show();
            }
        }

        private void LoadSupplierData(string statusFilter = "ACT")
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query;
                    SqlCommand cmd;

                    if (string.IsNullOrEmpty(statusFilter) || statusFilter == "ALL")
                    {
                        query = @"";
                        cmd = new SqlCommand(query, conn);
                    }
                    else
                    {
                        query = @"
                                  WHERE StatusCode = @StatusCode
                                  ORDER BY SystemDate DESC";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@StatusCode", statusFilter);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    AuthorityDataGridView.DataSource = dt;

                    FormatDataGridView();

                }
                catch (Exception ex)
                {
                    ShowError("Error loading supplier data", ex);
                }
            }
        }

        private void FormatDataGridView()
        {
            try
            {
                if (AuthorityDataGridView.Columns.Count > 0)
                {
                    AuthorityDataGridView.Columns["Code"].Width = 100;
                    AuthorityDataGridView.Columns["Description"].Width = 200;
                    AuthorityDataGridView.Columns["Created Date"].Width = 150;
                    AuthorityDataGridView.Columns["Status"].Width = 80;
                    AuthorityDataGridView.Columns["Remarks"].Width = 250;

                    foreach (DataGridViewColumn column in AuthorityDataGridView.Columns)
                    {
                        column.ReadOnly = true;
                    }

                    AuthorityDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    AuthorityDataGridView.MultiSelect = false;

                    //SupplyTypeDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error formatting grid", ex);
            }
        }

        //======================================= Error Handler =======================================
        private void HandleError(string contextMessage, Exception ex)
        {
            MessageBox.Show($"{contextMessage}:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowError(string context, Exception ex)
        {
            string errorMessage = $"{context}:\n\n{ex.Message}";

            MessageBox.Show(errorMessage,
                          "Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
        }

    }
}