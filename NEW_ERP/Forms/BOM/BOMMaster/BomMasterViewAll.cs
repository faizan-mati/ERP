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
        /// <summary>
        /// Initializes a new instance of the BomMasterViewAll form
        /// </summary>
        public BomMasterViewAll()
        {
            InitializeComponent();
        }

        #region Form Events

        /// <summary>
        /// Handles the form load event to initialize data
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event arguments</param>
        private void BomMasterViewAll_Load(object sender, EventArgs e)
        {
            LoadBomMasterIds();
            LoadBomData();
        }
        #endregion

        #region Data Loading Methods

        /// <summary>
        /// Loads all active BOM Master IDs into the combo box
        /// </summary>
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

        /// <summary>
        /// Loads all active BOM data into the grid view
        /// </summary>
        private void LoadBomData()
        {
            const string query = @"
                SELECT  
                    bm.BOMID,
                    i.ProductDescription, 
                    so.SaleOrderNo,
                    bm.VersionNo,
                    bm.CreatedBy,
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
        #endregion

        #region Button Events

        /// <summary>
        /// Handles the search button click event to filter BOM data
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event arguments</param>
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
        #endregion

        #region GridView Events

        /// <summary>
        /// Handles the double-click event on a grid view row to open the BOM for editing
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event arguments containing row and column information</param>
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
        #endregion

        #region Helper Methods

        /// <summary>
        /// Searches for a specific BOM Master record using the stored procedure
        /// </summary>
        /// <param name="bomMasterId">The BOM Master ID to search for</param>
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

        /// <summary>
        /// Opens the BomMasterAdd form in edit mode for the specified BOM Master ID
        /// </summary>
        /// <param name="bomMasterId">The BOM Master ID to edit</param>
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

        /// <summary>
        /// Handles errors consistently throughout the application
        /// </summary>
        /// <param name="contextMessage">Descriptive message about where the error occurred</param>
        /// <param name="ex">The exception that was thrown</param>
        private void HandleError(string contextMessage, Exception ex)
        {
            MessageBox.Show($"{contextMessage}:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Uncomment if you have logging set up
            // Logger.LogError(ex, contextMessage);
        }
        #endregion
    }
}