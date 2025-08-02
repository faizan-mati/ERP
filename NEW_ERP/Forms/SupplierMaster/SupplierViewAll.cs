using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SupplierMaster
{
    public partial class SupplierViewAll : Form
    {
        public SupplierViewAll()
        {
            InitializeComponent();
        }

        private void SupplierViewAll_Load(object sender, EventArgs e)
        {
            StatusCodeShow();
            LoadSupplierData();
        }

        //======================================= STATUS CODE SHOW =======================================
        protected void StatusCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT DISTINCT StatusCode FROM SupplierMaster;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    StatusCodeBox.DataSource = dt;
                    StatusCodeBox.DisplayMember = "StatusCode";
                    StatusCodeBox.ValueMember = "StatusCode";
                    StatusCodeBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= LOAD SUPPLIER DATA =======================================
        private void LoadSupplierData(string statusFilter = null)
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
                                    SupplierCode as 'Code', 
                                    Description, 
                                    SupplierTypeCode as 'Type',
                                    ContactPerson as 'Contact Person',
                                    ShortName as 'Short Name',
                                    Address,
                                    ShortAddress as 'Short Address',
                                    PhoneNo as 'Phone',
                                    PhoneNo2 as 'Phone 2',
                                    FaxNo as 'Fax',
                                    MobileNo as 'Mobile',
                                    EMail as 'Email',
                                    EMail2 as 'Email 2',
                                    Url as 'Website',
                                    NtnNo as 'NTN',
                                    SaleTaxRegNo as 'Sales Tax No',
                                    Reference,
                                    SystemDate as 'Created Date', 
                                    StatusCode as 'Status', 
                                    UserCode as 'User',
                                    Remarks 
                                  FROM SupplierMaster 
                                  ORDER BY SystemDate DESC";
                        cmd = new SqlCommand(query, conn);
                    }
                    else
                    {
                        query = @"SELECT 
                                    SupplierCode as 'Code', 
                                    Description, 
                                    SupplierTypeCode as 'Type',
                                    ContactPerson as 'Contact Person',
                                    ShortName as 'Short Name',
                                    Address,
                                    ShortAddress as 'Short Address',
                                    PhoneNo as 'Phone',
                                    PhoneNo2 as 'Phone 2',
                                    FaxNo as 'Fax',
                                    MobileNo as 'Mobile',
                                    EMail as 'Email',
                                    EMail2 as 'Email 2',
                                    Url as 'Website',
                                    NtnNo as 'NTN',
                                    SaleTaxRegNo as 'Sales Tax No',
                                    Reference,
                                    SystemDate as 'Created Date', 
                                    StatusCode as 'Status', 
                                    UserCode as 'User',
                                    Remarks 
                                  FROM SupplierMaster 
                                  WHERE StatusCode = @StatusCode
                                  ORDER BY SystemDate DESC";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@StatusCode", statusFilter);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SpplierMasterDataGridView.DataSource = dt;
                    FormatDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //======================================= SEARCH BUTTON CLICK =======================================
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (StatusCodeBox.SelectedIndex == -1)
            {
                LoadSupplierData();
                return;
            }

            LoadSupplierData(StatusCodeBox.SelectedValue?.ToString());
        }

        //======================================= FORMAT DATA GRID VIEW =======================================
        private void FormatDataGridView()
        {
            try
            {
                if (SpplierMasterDataGridView.Columns.Count > 0)
                {
                    SpplierMasterDataGridView.Columns["Code"].Width = 80;
                    SpplierMasterDataGridView.Columns["Description"].Width = 150;
                    SpplierMasterDataGridView.Columns["Type"].Width = 60;
                    SpplierMasterDataGridView.Columns["Contact Person"].Width = 120;
                    SpplierMasterDataGridView.Columns["Short Name"].Width = 80;
                    SpplierMasterDataGridView.Columns["Address"].Width = 200;
                    SpplierMasterDataGridView.Columns["Short Address"].Width = 120;
                    SpplierMasterDataGridView.Columns["Phone"].Width = 100;
                    SpplierMasterDataGridView.Columns["Phone 2"].Width = 100;
                    SpplierMasterDataGridView.Columns["Fax"].Width = 100;
                    SpplierMasterDataGridView.Columns["Mobile"].Width = 100;
                    SpplierMasterDataGridView.Columns["Email"].Width = 150;
                    SpplierMasterDataGridView.Columns["Email 2"].Width = 150;
                    SpplierMasterDataGridView.Columns["Website"].Width = 150;
                    SpplierMasterDataGridView.Columns["NTN"].Width = 100;
                    SpplierMasterDataGridView.Columns["Sales Tax No"].Width = 100;
                    SpplierMasterDataGridView.Columns["Reference"].Width = 100;
                    SpplierMasterDataGridView.Columns["Created Date"].Width = 120;
                    SpplierMasterDataGridView.Columns["Status"].Width = 60;
                    SpplierMasterDataGridView.Columns["User"].Width = 80;
                    SpplierMasterDataGridView.Columns["Remarks"].Width = 200;

                    foreach (DataGridViewColumn column in SpplierMasterDataGridView.Columns)
                    {
                        column.ReadOnly = true;
                    }

                    SpplierMasterDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    SpplierMasterDataGridView.MultiSelect = false;
                    SpplierMasterDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error formatting grid", ex);
            }
        }

        //======================================= CELL DOUBLE CLICK EVENT =======================================
        private void SpplierMasterDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var selectedRow = SpplierMasterDataGridView.Rows[e.RowIndex];
                var supplierCodeCell = selectedRow.Cells["Code"]?.Value;

                if (supplierCodeCell == null || string.IsNullOrWhiteSpace(supplierCodeCell.ToString()))
                {
                    MessageBox.Show("Please select a valid supplier record.",
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

        //======================================= OPEN SUPPLIER FOR EDITING =======================================
        private void OpenSupplierForEditing(string supplierCode)
        {
            try
            {
                this.Hide();

                using (var SupplierMasterAdd = new SupplierMasterAdd(supplierCode, true))
                {
                    DialogResult result = SupplierMasterAdd.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        string currentFilter = StatusCodeBox.SelectedValue?.ToString();
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

        //======================================= SHOW ERROR =======================================
        private void ShowError(string context, Exception ex)
        {
            MessageBox.Show($"{context}:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}