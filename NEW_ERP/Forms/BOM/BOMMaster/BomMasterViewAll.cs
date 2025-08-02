using NEW_ERP.Forms.BOMMaster;
using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.BOM.BOMMaster
{
    public partial class BomMasterViewAll : Form
    {
        public BomMasterViewAll()
        {
            InitializeComponent();
        }

        //======================================= Form Events =======================================
        private void BomMasterViewAll_Load(object sender, EventArgs e)
        {
            LoadBomMasterIds();
            LoadBomData();
        }

        //======================================= Data Loading Methods =======================================

        private void LoadBomMasterIds()
        {
            const string query = @"SELECT DISTINCT BOMID FROM BOMMaster WHERE StatusCode='ACT'";

            using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
            using (var cmd = new SqlCommand(query, conn))
            {
                try
                {
                    var adapter = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    BomMasterIdBox.DataSource = dt;
                    BomMasterIdBox.DisplayMember = "BOMID";
                    BomMasterIdBox.ValueMember = "BOMID";
                    BomMasterIdBox.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    HandleError("Error loading BOM Master IDs", ex);
                }
            }
        }

        // Loads all active BOM data into the grid view
        private void LoadBomData()
        {
            const string query = @"
                SELECT  
                    bm.BOMID,
                    i.ProductDescription, 
                    so.SaleOrderNo,
                    bm.VersionNo,
                    bm.CreatedBy,
                    bm.SystemDate,
                    bm.CreatedDate
                FROM BOMMaster bm
                INNER JOIN ItemMaster i ON i.ProductCode = bm.ProductID
                INNER JOIN SaleOrderMaster so ON so.SaleOrderID = bm.SaleOrderID
                WHERE bm.StatusCode = 'ACT'
                ORDER BY bm.BOMID DESC;";

            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (var cmd = new SqlCommand(query, conn))
                {
                    var adapter = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    BomDataGridView.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading BOM data", ex);
            }
        }

        //======================================= Button Events =======================================

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (BomMasterIdBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a BOM Master ID.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SearchBomMaster(BomMasterIdBox.SelectedValue.ToString());
        }

        //======================================= GridView Events =======================================

        private void BomDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var selectedRow = BomDataGridView.Rows[e.RowIndex];
            var value = selectedRow.Cells["BOMID"].Value;

            if (value == null || !int.TryParse(value.ToString(), out int bomMasterId))
            {
                MessageBox.Show("Invalid BOM Master selected.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenBomMasterForEditing(bomMasterId);
        }

        //======================================= Helper Methods =======================================

        private void SearchBomMaster(string bomMasterId)
        {
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (var cmd = new SqlCommand("sp_SearchBOMMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BomMasterId", bomMasterId);

                    var adapter = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        BomDataGridView.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No records found.", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error retrieving BOM data", ex);
            }
        }

        // Opens the BomMasterAdd form in edit mode for the specified BOM Master ID
        private void OpenBomMasterForEditing(int bomMasterId)
        {
            try
            {
                this.Close();
                using (var bomMasterAddForm = new BomMasterAdd(bomMasterId, true))
                {
                    bomMasterAddForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error opening BOM Master for editing", ex);
            }
        }

        // Handles errors consistently throughout the application
        private void HandleError(string contextMessage, Exception ex)
        {
            MessageBox.Show($"{contextMessage}:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}