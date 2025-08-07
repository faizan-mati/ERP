using NEW_ERP.Forms.ItemMaster;
using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.ItemMasterForms
{
    public partial class ItemMasterViewAll : Form
    {
        public ItemMasterViewAll()
        {
            InitializeComponent();
        }

        private void ItemMasterViewAll_Load(object sender, EventArgs e)
        {
            try
            {
                LoadItemData();
                LoadStatustCodes();

            }
            catch (Exception ex)
            {
                ShowError("Error during form initialization", ex);
            }
        }

        //======================================= Load Item Data =======================================
        private void LoadItemData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetItemMaster", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ItemMasterDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    ShowError("Error loading item data", ex);
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

                        ProductCodeBox.DataSource = dtRoles;
                        ProductCodeBox.DisplayMember = "StatusCode";
                        ProductCodeBox.ValueMember = "StatusId";
                        ProductCodeBox.SelectedIndex = -1;


                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading sale order dropdown", ex);
            }
        }

        //======================================= Search Button Click =======================================
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (ProductCodeBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a product code");
                return;
            }

            try
            {
                string StatusCode = ProductCodeBox.SelectedValue.ToString();
                SearchItemsByCode(StatusCode);
            }
            catch (Exception ex)
            {
                ShowError("Search failed", ex);
            }
        }

        //======================================= Search Items By Code =======================================
        private void SearchItemsByCode(string StatusCode)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_SearchItemMaster", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StatusCode", StatusCode);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ItemMasterDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    ShowError("Error searching items", ex);
                }
            }
        }

        //======================================= Data Grid View Cell Double Click =======================================
        private void ItemMasterDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var selectedRow = ItemMasterDataGridView.Rows[e.RowIndex];
                var value = selectedRow.Cells["ProductCode"].Value;

                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    MessageBox.Show("Please select a valid product",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                OpenItemForEditing(value.ToString().Trim());
            }
            catch (Exception ex)
            {
                ShowError("Error handling grid selection", ex);
            }
        }

        //======================================= Open Item For Editing =======================================
        private void OpenItemForEditing(string productCode)
        {
            try
            {
                this.Close();
                using (var ItemMasterAdd = new ItemMasterForm(productCode, true))
                {
                    ItemMasterAdd.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowError("Error opening item for editing", ex);
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
    }
}