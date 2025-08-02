using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SupplierType
{
    public partial class SupplyTypeViewAll : Form
    {
        //======================================= Constructor =======================================
        public SupplyTypeViewAll()
        {
            InitializeComponent();
        }

        //======================================= Form Load Event =======================================
        private void SupplyTypeViewAll_Load(object sender, EventArgs e)
        {
            LoadStatusCodeDropdown();
            LoadSupplierData();
        }

        //======================================= Load Supplier Data =======================================
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
                        query = @"SELECT 
                                    SupplierTypeCode as 'Code', 
                                    Description, 
                                    SystemDate as 'Created Date', 
                                    StatusCode as 'Status', 
                                    Remarks 
                                  FROM SupplierType 
                                  ORDER BY SystemDate DESC";
                        cmd = new SqlCommand(query, conn);
                    }
                    else
                    {
                        query = @"SELECT 
                                    SupplierTypeCode as 'Code', 
                                    Description, 
                                    SystemDate as 'Created Date', 
                                    StatusCode as 'Status', 
                                    Remarks 
                                  FROM SupplierType 
                                  WHERE StatusCode = @StatusCode
                                  ORDER BY SystemDate DESC";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@StatusCode", statusFilter);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SupplyTypeDataGridView.DataSource = dt;

                    FormatDataGridView();
                }
                catch (Exception ex)
                {
                    ShowError("Error loading supplier data", ex);
                }
            }
        }

        //======================================= Format DataGridView =======================================
        private void FormatDataGridView()
        {
            try
            {
                if (SupplyTypeDataGridView.Columns.Count > 0)
                {
                    SupplyTypeDataGridView.Columns["Code"].Width = 100;
                    SupplyTypeDataGridView.Columns["Description"].Width = 200;
                    SupplyTypeDataGridView.Columns["Created Date"].Width = 150;
                    SupplyTypeDataGridView.Columns["Status"].Width = 80;
                    SupplyTypeDataGridView.Columns["Remarks"].Width = 250;

                    foreach (DataGridViewColumn column in SupplyTypeDataGridView.Columns)
                    {
                        column.ReadOnly = true;
                    }

                    SupplyTypeDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    SupplyTypeDataGridView.MultiSelect = false;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error formatting grid", ex);
            }
        }

        //======================================= Load Status Code Dropdown =======================================
        private void LoadStatusCodeDropdown()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    string query = @"SELECT DISTINCT StatusCode 
                                   FROM SupplierType 
                                   ORDER BY StatusCode";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        DataRow allRow = dt.NewRow();
                        allRow["StatusCode"] = "ALL";
                        dt.Rows.InsertAt(allRow, 0);

                        StatusCodeBox.DataSource = dt;
                        StatusCodeBox.DisplayMember = "StatusCode";
                        StatusCodeBox.ValueMember = "StatusCode";

                        StatusCodeBox.SelectedValue = "ACT";
                    }
                }
                catch (Exception ex)
                {
                    ShowError("Error loading status codes", ex);
                }
            }
        }

        //======================================= Search Button Click =======================================
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedStatusCode = StatusCodeBox.SelectedValue?.ToString();

                if (string.IsNullOrEmpty(selectedStatusCode))
                {
                    MessageBox.Show("Please select a status code to search.",
                                  "Search Validation",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }

                SearchSupplierTypes(selectedStatusCode);
            }
            catch (Exception ex)
            {
                ShowError("Error performing search", ex);
            }
        }

        //======================================= Search Supplier Types =======================================
        private void SearchSupplierTypes(string statusCode, string searchText = "")
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_SearchSupplierType", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@StatusCode", statusCode == "ALL" ? DBNull.Value : (object)statusCode);
                        cmd.Parameters.AddWithValue("@SearchText", string.IsNullOrEmpty(searchText) ? DBNull.Value : (object)searchText);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        SupplyTypeDataGridView.DataSource = dt;

                        FormatDataGridView();

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No records found matching the search criteria.",
                                          "Search Results",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowError("Error executing search", ex);
                }
            }
        }

        //======================================= Grid Cell Double Click =======================================
        private void SupplyTypeDataGridView_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var selectedRow = SupplyTypeDataGridView.Rows[e.RowIndex];

                var supplierCodeCell = selectedRow.Cells["Code"]?.Value ??
                                     selectedRow.Cells["SupplierTypeCode"]?.Value;

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

        //======================================= Open Supplier For Editing =======================================
        private void OpenSupplierForEditing(string supplierCode)
        {
            try
            {
                this.Hide();

                using (var supplierTypeAddForm = new SupplierTypeAdd(supplierCode, true))
                {
                    DialogResult result = supplierTypeAddForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        string currentFilter = StatusCodeBox.SelectedValue?.ToString() ?? "ACT";
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

        //======================================= Show Error =======================================
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