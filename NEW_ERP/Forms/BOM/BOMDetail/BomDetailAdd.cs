using NEW_ERP.GernalClasses;
using NEW_ERP.Template;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.BOM.BOMDetail
{
    public partial class BomDetailAdd : AddFormTemplate
    {
        #region Private Fields
        private readonly int _bomDetailId;     
        private readonly bool _isFromViewAll;     
        private bool _isEditMode = false;        
        #endregion

        #region Constructor

        public BomDetailAdd(int bomDetailId, bool isFromViewAll)
        {
            try
            {
                InitializeComponent();
                _bomDetailId = bomDetailId;
                _isFromViewAll = isFromViewAll;
            }
            catch (Exception ex)
            {
                LogError("Form initialization failed", ex);
                ShowErrorMessage("Failed to initialize form. Please contact support.");
                this.Close();
            }
        }
        #endregion

        #region Form Events

        private void BomDetailAdd_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBomMasterDropdown();
                ConfigureFormMode();

                if (_isFromViewAll && _bomDetailId > 0)
                {
                    LoadBomDetailData();
                }
            }
            catch (Exception ex)
            {
                LogError("Form load failed", ex);
                ShowErrorMessage("Failed to load form data. Please try again.");
            }
        }

        /// <summary>
        /// Handles the BOM ID dropdown event
        /// - Refreshes the BOM master dropdown data when opened
        /// </summary>
        private void BomIdBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                LoadBomMasterDropdown();
            }
            catch (Exception ex)
            {
                LogError("BOM ID dropdown load failed", ex);
                ShowErrorMessage("Failed to load BOM IDs. Please try again.");
            }
        }
        #endregion

        #region Form Mode Management
        /// <summary>
        /// Configures the form mode based on operation type
        /// - Insert mode for new records
        /// - View mode for existing records
        /// </summary>
        private void ConfigureFormMode()
        {
            try
            {
                if (!_isFromViewAll) 
                {
                    SetInsertMode();
                }
                else 
                {
                    SetViewMode();
                }
            }
            catch (Exception ex)
            {
                LogError("Form mode configuration failed", ex);
                throw;
            }
        }

        /// <summary>
        /// Sets the form to insert mode for adding new records
        /// - Enables all controls
        /// - Configures button states
        /// </summary>
        private void SetInsertMode()
        {
            try
            {
                EnableFormControls(true);
                SubmitBtn.Enabled = true;
                SubmitBtn.Text = "Submit";
                EditBtn.Enabled = false;
                DeleteBtn.Enabled = false;
                Text = "Add New BOM Detail";
            }
            catch (Exception ex)
            {
                LogError("Failed to set insert mode", ex);
                throw;
            }
        }

        /// <summary>
        /// Sets the form to view mode for existing records
        /// - Disables all controls
        /// - Configures button states
        /// </summary>
        private void SetViewMode()
        {
            try
            {
                EnableFormControls(false);
                SubmitBtn.Enabled = false;
                EditBtn.Enabled = true;
                EditBtn.Text = "Edit";
                DeleteBtn.Enabled = true;
                Text = "View/Edit BOM Detail";
            }
            catch (Exception ex)
            {
                LogError("Failed to set view mode", ex);
                throw;
            }
        }

        /// <summary>
        /// Sets the form to edit mode for modifying existing records
        /// - Enables all controls
        /// - Configures button states
        /// </summary>
        private void SetEditMode()
        {
            try
            {
                EnableFormControls(true);
                SubmitBtn.Enabled = false;
                EditBtn.Enabled = true;
                EditBtn.Text = "Save";
                DeleteBtn.Enabled = false;
                _isEditMode = true;
                Text = "Edit BOM Detail";
            }
            catch (Exception ex)
            {
                LogError("Failed to set edit mode", ex);
                throw;
            }
        }

        /// <summary>
        /// Enables or disables form controls based on current mode
        /// </summary>
        private void EnableFormControls(bool enabled)
        {
            try
            {
                BomIdBox.Enabled = enabled;
                txtItemName.Enabled = enabled;
                txtItemType.Enabled = enabled;
                txtUnit.Enabled = enabled;
                txtConPerPc.Enabled = enabled;
                txtWastagePercent.Enabled = enabled;
                txtRemarks.Enabled = enabled;
            }
            catch (Exception ex)
            {
                LogError("Failed to enable/disable form controls", ex);
                throw;
            }
        }
        #endregion

        #region Data Operations
        /// <summary>
        /// Loads active BOM Master IDs into the dropdown
        /// </summary>
        private void LoadBomMasterDropdown()
        {
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    const string query = "SELECT BOMID FROM BOMMaster WHERE StatusCode = 'ACT' ORDER BY BOMID DESC";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        var adapter = new SqlDataAdapter(cmd);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            throw new Exception("No active BOM Master records found");
                        }

                        BomIdBox.DataSource = dt;
                        BomIdBox.DisplayMember = "BOMID";
                        BomIdBox.ValueMember = "BOMID";
                        BomIdBox.SelectedIndex = -1;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                LogError("Database error loading BOM Master dropdown", sqlEx);
                throw new Exception("Failed to load BOM Master data. Please check database connection.", sqlEx);
            }
            catch (Exception ex)
            {
                LogError("Error loading BOM Master dropdown", ex);
                throw new Exception("Failed to load BOM Master data.", ex);
            }
        }

        /// <summary>
        /// Loads BOM Detail data for the specified ID
        /// </summary>
        private void LoadBomDetailData()
        {
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    const string query = @"SELECT bd.BOMID, bd.ItemName, bd.ItemType, bd.Unit, 
                                         bd.ConsumptionPerPiece, bd.WastagePercent, bd.Remarks
                                  FROM BOMDetails bd 
                                  WHERE bd.BOMDetailID = @BomDetailId";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BomDetailId", _bomDetailId);
                        conn.Open();

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                throw new Exception($"No BOM Detail found with ID: {_bomDetailId}");
                            }

                            if (reader.Read())
                            {
                                BomIdBox.SelectedValue = reader["BOMID"];
                                txtItemName.Text = reader["ItemName"].ToString();
                                txtItemType.Text = reader["ItemType"].ToString();
                                txtUnit.Text = reader["Unit"].ToString();
                                txtConPerPc.Text = reader["ConsumptionPerPiece"].ToString();
                                txtWastagePercent.Text = reader["WastagePercent"].ToString();
                                txtRemarks.Text = reader["Remarks"].ToString();
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                LogError("Database error loading BOM Detail data", sqlEx);
                throw new Exception("Failed to load BOM Detail data. Please check database connection.", sqlEx);
            }
            catch (Exception ex)
            {
                LogError("Error loading BOM Detail data", ex);
                throw new Exception("Failed to load BOM Detail data.", ex);
            }
        }

        /// <summary>
        /// Inserts a new BOM Detail record into the database
        /// </summary>
        private void InsertBomDetail()
        {
            SqlTransaction transaction = null;
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (var cmd = new SqlCommand("sp_InsertBOMDetail", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@BOMID", Convert.ToInt32(BomIdBox.SelectedValue));
                        cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItemType", txtItemType.Text.Trim());
                        cmd.Parameters.AddWithValue("@Unit", txtUnit.Text.Trim());

                        cmd.Parameters.AddWithValue("@ConsumptionPerPiece",
                            decimal.TryParse(txtConPerPc.Text.Trim(), out var conPerPc) ? conPerPc : 0);

                        cmd.Parameters.AddWithValue("@WastagePercent",
                            decimal.TryParse(txtWastagePercent.Text.Trim(), out var wastage) ? wastage : 0);

                        cmd.Parameters.AddWithValue("@Remarks",
                            string.IsNullOrWhiteSpace(txtRemarks.Text) ? DBNull.Value : (object)txtRemarks.Text.Trim());

                        cmd.Parameters.AddWithValue("@UserCode", "ADM");
                        cmd.Parameters.AddWithValue("@StatusCode", "ACT");

                        int result = cmd.ExecuteNonQuery();
                        transaction.Commit();

                        if (result > 0)
                        {
                            ShowSuccessMessage("BOM Detail inserted successfully!");
                            ResetFormControls();
                        }
                        else
                        {
                            ShowWarningMessage("No records were inserted.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                transaction?.Rollback();
                LogError("Database error inserting BOM Detail", sqlEx);
                throw new Exception("Failed to insert BOM Detail. Database error occurred.", sqlEx);
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                LogError("Error inserting BOM Detail", ex);
                throw new Exception("Failed to insert BOM Detail.", ex);
            }
        }

        /// <summary>
        /// Updates an existing BOM Detail record in the database
        /// </summary>
        private void UpdateBomDetail()
        {
            SqlTransaction transaction = null;
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (var cmd = new SqlCommand("sp_UpdateBOMDetail", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@BOMDetailID", _bomDetailId);
                        cmd.Parameters.AddWithValue("@BOMID", Convert.ToInt32(BomIdBox.SelectedValue));
                        cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItemType", txtItemType.Text.Trim());
                        cmd.Parameters.AddWithValue("@Unit", txtUnit.Text.Trim());

                        cmd.Parameters.AddWithValue("@ConsumptionPerPiece",
                            decimal.TryParse(txtConPerPc.Text.Trim(), out var conPerPc) ? conPerPc : 0);

                        cmd.Parameters.AddWithValue("@WastagePercent",
                            decimal.TryParse(txtWastagePercent.Text.Trim(), out var wastage) ? wastage : 0);

                        cmd.Parameters.AddWithValue("@Remarks",
                            string.IsNullOrWhiteSpace(txtRemarks.Text) ? DBNull.Value : (object)txtRemarks.Text.Trim());

                        int result = cmd.ExecuteNonQuery();
                        transaction.Commit();

                        if (result > 0)
                        {
                            ShowSuccessMessage("BOM Detail updated successfully!");
                            _isEditMode = false;
                            SetViewMode();
                        }
                        else
                        {
                            ShowWarningMessage("No records were updated.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                transaction?.Rollback();
                LogError("Database error updating BOM Detail", sqlEx);
                throw new Exception("Failed to update BOM Detail. Database error occurred.", sqlEx);
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                LogError("Error updating BOM Detail", ex);
                throw new Exception("Failed to update BOM Detail.", ex);
            }
        }

        /// <summary>
        /// Deletes a BOM Detail record from the database
        /// </summary>
        private void DeleteBomDetail()
        {
            SqlTransaction transaction = null;
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (var cmd = new SqlCommand("sp_DeleteBOMDetail", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BOMDetailID", _bomDetailId);

                        int result = cmd.ExecuteNonQuery();
                        transaction.Commit();

                        if (result > 0)
                        {
                            ShowSuccessMessage("BOM Detail deleted successfully!");
                            Close();
                            new BomDetailViewAll().Show();
                        }
                        else
                        {
                            ShowWarningMessage("No records were deleted.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                transaction?.Rollback();
                LogError("Database error deleting BOM Detail", sqlEx);
                throw new Exception("Failed to delete BOM Detail. Database error occurred.", sqlEx);
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                LogError("Error deleting BOM Detail", ex);
                throw new Exception("Failed to delete BOM Detail.", ex);
            }
        }
        #endregion

        #region Button Events
        /// <summary>
        /// Handles the Submit button click event
        /// - Validates form inputs
        /// - Inserts new BOM detail record
        /// </summary>
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm()) return;
                InsertBomDetail();
            }
            catch (Exception ex)
            {
                LogError("Submit operation failed", ex);
                ShowErrorMessage("Failed to save BOM Detail. Please try again.");
            }
        }

        /// <summary>
        /// Handles the Edit/Save button click event
        /// - Toggles between edit and save modes
        /// - Validates form inputs when saving
        /// - Updates existing BOM detail record
        /// </summary>
        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (EditBtn.Text == "Edit")
                {
                    SetEditMode();
                }
                else if (EditBtn.Text == "Save")
                {
                    if (ValidateForm())
                    {
                        UpdateBomDetail();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("Edit operation failed", ex);
                ShowErrorMessage("Failed to update BOM Detail. Please try again.");
            }
        }

        /// <summary>
        /// Handles the Delete button click event
        /// - Confirms deletion with user
        /// - Deletes the BOM detail record
        /// </summary>
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this BOM Detail record?\nThis action cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DeleteBomDetail();
                }
            }
            catch (Exception ex)
            {
                LogError("Delete operation failed", ex);
                ShowErrorMessage("Failed to delete BOM Detail. Please try again.");
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                LogError("Form close failed", ex);
                ShowErrorMessage("Failed to close form. Please try again.");
            }
        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                new BomDetailViewAll().Show();
            }
            catch (Exception ex)
            {
                LogError("Navigation to View All failed", ex);
                ShowErrorMessage("Failed to open BOM Detail list. Please try again.");
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Validates all form inputs before submission
        /// </summary>
        private bool ValidateForm()
        {
            try
            {
                if (BomIdBox.SelectedIndex == -1)
                {
                    ShowValidationError("Please select a BOM ID.", BomIdBox);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtItemName.Text))
                {
                    ShowValidationError("Please enter Item Name.", txtItemName);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtItemType.Text))
                {
                    ShowValidationError("Please enter Item Type.", txtItemType);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtUnit.Text))
                {
                    ShowValidationError("Please enter Unit.", txtUnit);
                    return false;
                }

                if (!decimal.TryParse(txtConPerPc.Text.Trim(), out _))
                {
                    ShowValidationError("Please enter a valid number for Consumption Per Piece.", txtConPerPc);
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(txtWastagePercent.Text) &&
                    !decimal.TryParse(txtWastagePercent.Text.Trim(), out _))
                {
                    ShowValidationError("Please enter a valid number for Wastage Percent.", txtWastagePercent);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogError("Form validation failed", ex);
                ShowErrorMessage("Validation error occurred. Please check your inputs.");
                return false;
            }
        }

        /// <summary>
        /// Resets all form controls to their default state
        /// </summary>
        private void ResetFormControls()
        {
            try
            {
                BomIdBox.SelectedIndex = -1;
                txtItemName.Clear();
                txtItemType.Clear();
                txtUnit.Clear();
                txtConPerPc.Clear();
                txtWastagePercent.Clear();
                txtRemarks.Clear();
                BomIdBox.Focus();
            }
            catch (Exception ex)
            {
                LogError("Failed to reset form controls", ex);
                throw;
            }
        }

        private void ShowValidationError(string message, Control control)
        {
            MessageBox.Show(message, "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            control.Focus();
        }

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LogError(string context, Exception ex)
        {
            Console.WriteLine($"{context}: {ex.Message}\n{ex.StackTrace}");
        }
        #endregion
    }
}