using NEW_ERP.Forms.ItemMasterForms;
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

namespace NEW_ERP.Forms.ItemMaster
{
    public partial class ItemMasterForm : Form
    {
        #region Private Fields
        private readonly string _productCode;
        private readonly bool _isFromViewAll;
        private bool _isEditMode = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ItemMasterForm class
        /// </summary>
        /// <param name="productCode">The product code to load (empty for new item)</param>
        /// <param name="isFromViewAll">Indicates if form was opened from View All screen</param>
        public ItemMasterForm(string productCode, bool isFromViewAll)
        {
            try
            {
                InitializeComponent();
                _productCode = productCode;
                _isFromViewAll = isFromViewAll;
            }
            catch (Exception ex)
            {
                HandleError("Error initializing form", ex);
            }
        }
        #endregion

        #region Form Events
        /// <summary>
        /// Handles the form load event
        /// </summary>
        private void ItemMasterForm_Load(object sender, EventArgs e)
        {
            try
            {
                SetupFormMode();
            }
            catch (Exception ex)
            {
                HandleError("Error loading form", ex);
            }
        }
        #endregion

        #region Form Mode Management
        /// <summary>
        /// Configures the form based on current mode (Insert/Edit/View)
        /// </summary>
        private void SetupFormMode()
        {
            try
            {
                if (_isFromViewAll && !string.IsNullOrEmpty(_productCode))
                {
                    LoadExistingData(_productCode);
                    SetFormToViewMode();
                }
                else
                {
                    SetFormToInsertMode();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error setting up form mode", ex);
            }
        }

        /// <summary>
        /// Sets form to insert mode for new item entry
        /// </summary>
        private void SetFormToInsertMode()
        {
            try
            {
                SetTextBoxesEnabled(true);

                SubmitBtn.Enabled = true;
                SubmitBtn.Text = "Submit";
                EditBtn.Enabled = false;
                DeleteBtn.Enabled = false;
                ViewAllBtn.Enabled = true;
                CloseBtn.Enabled = true;

                _isEditMode = false;
            }
            catch (Exception ex)
            {
                HandleError("Error setting form to insert mode", ex);
            }
        }

        /// <summary>
        /// Sets form to view mode (read-only)
        /// </summary>
        private void SetFormToViewMode()
        {
            try
            {
                SetTextBoxesEnabled(false);

                SubmitBtn.Enabled = true;
                SubmitBtn.Text = "Submit";
                EditBtn.Enabled = true;
                EditBtn.Text = "Edit";
                DeleteBtn.Enabled = true;
                ViewAllBtn.Enabled = true;
                CloseBtn.Enabled = true;

                _isEditMode = false;
            }
            catch (Exception ex)
            {
                HandleError("Error setting form to view mode", ex);
            }
        }

        /// <summary>
        /// Sets form to edit mode for modifying existing item
        /// </summary>
        private void SetFormToEditMode()
        {
            try
            {
                SetTextBoxesEnabled(true);
                TxtProductCode.Enabled = false;

                SubmitBtn.Enabled = false;
                EditBtn.Text = "Save";
                DeleteBtn.Enabled = false;
                ViewAllBtn.Enabled = false;

                _isEditMode = true;
            }
            catch (Exception ex)
            {
                HandleError("Error setting form to edit mode", ex);
            }
        }

        /// <summary>
        /// Enables/disables all text boxes on the form
        /// </summary>
        /// <param name="enabled">True to enable controls, false to disable</param>
        private void SetTextBoxesEnabled(bool enabled)
        {
            TxtProductCode.Enabled = enabled;
            TxtProductDes.Enabled = enabled;
            TxtProductShortName.Enabled = enabled;
            TxtProductRemarks.Enabled = enabled;
        }
        #endregion

        #region Database Operations
        /// <summary>
        /// Loads existing product data from database
        /// </summary>
        /// <param name="productCode">Product code to load</param>
        private void LoadExistingData(string productCode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();

                    string query = @"SELECT ProductCode, ProductDescription, ProductShortName, Remarks 
                                   FROM ItemMaster 
                                   WHERE ProductCode = @ProductCode AND StatusCode = 'ACT'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductCode", productCode);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TxtProductCode.Text = reader["ProductCode"].ToString();
                                TxtProductDes.Text = reader["ProductDescription"].ToString();
                                TxtProductShortName.Text = reader["ProductShortName"].ToString();
                                TxtProductRemarks.Text = reader["Remarks"].ToString();
                            }
                            else
                            {
                                ShowMessage("Product not found!", "Error", MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleError("Database error loading product data", ex);
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError("Error loading product data", ex);
                this.Close();
            }
        }

        /// <summary>
        /// Inserts a new item into the ItemMaster table
        /// </summary>
        private void InsertItemMaster()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertItemMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductCode", TxtProductCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductDescription", TxtProductDes.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductShortName", TxtProductShortName.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserCode", "000123");
                        cmd.Parameters.AddWithValue("@Remarks", TxtProductRemarks.Text.Trim());

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            ShowMessage("Item inserted successfully!", "Success", MessageBoxIcon.Information);
                            ResetFormControls();
                        }
                        else
                        {
                            ShowMessage("Insertion failed.", "Warning", MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleError("Database error inserting item", ex);
            }
            catch (Exception ex)
            {
                HandleError("Error inserting item", ex);
            }
        }

        /// <summary>
        /// Updates an existing item in the ItemMaster table
        /// </summary>
        private void UpdateItemMaster()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateItemMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductCode", TxtProductCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductDescription", TxtProductDes.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductShortName", TxtProductShortName.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserCode", "000123");
                        cmd.Parameters.AddWithValue("@Remarks", TxtProductRemarks.Text.Trim());

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            ShowMessage("Item updated successfully!", "Success", MessageBoxIcon.Information);
                            SetFormToViewMode();
                        }
                        else
                        {
                            ShowMessage("Update failed.", "Warning", MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleError("Database error updating item", ex);
            }
            catch (Exception ex)
            {
                HandleError("Error updating item", ex);
            }
        }

        /// <summary>
        /// Performs a soft delete of an item in the ItemMaster table
        /// </summary>
        private void DeleteItemMaster()
        {
            try
            {
                DialogResult result = ShowMessage(
                    "Are you sure you want to delete this item?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand("sp_DeleteItemMaster", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@ProductCode", TxtProductCode.Text.Trim());
                            cmd.Parameters.AddWithValue("@UserCode", "000123");

                            int deleteResult = cmd.ExecuteNonQuery();

                            if (deleteResult > 0)
                            {
                                ShowMessage("Item deleted successfully!", "Success", MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                ShowMessage("Deletion failed.", "Warning", MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleError("Database error deleting item", ex);
            }
            catch (Exception ex)
            {
                HandleError("Error deleting item", ex);
            }
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates all form input fields
        /// </summary>
        /// <returns>True if validation passes, false otherwise</returns>
        private bool IsValidationPassed()
        {
            try
            {
                // Check required fields
                if (string.IsNullOrWhiteSpace(TxtProductCode.Text) ||
                    string.IsNullOrWhiteSpace(TxtProductShortName.Text) ||
                    string.IsNullOrWhiteSpace(TxtProductDes.Text))
                {
                    ShowMessage("Please fill all required fields (Product Code, Short Name, and Description)",
                               "Validation Error", MessageBoxIcon.Error);
                    return false;
                }

                // Validate field lengths
                if (TxtProductCode.Text.Trim().Length != 6)
                {
                    ShowMessage("Product Code must be exactly 6 characters long",
                               "Validation Error", MessageBoxIcon.Error);
                    TxtProductCode.Focus();
                    return false;
                }

                if (TxtProductShortName.Text.Trim().Length > 15)
                {
                    ShowMessage("Product Short Name cannot exceed 15 characters",
                               "Validation Error", MessageBoxIcon.Error);
                    TxtProductShortName.Focus();
                    return false;
                }

                if (TxtProductDes.Text.Trim().Length > 100)
                {
                    ShowMessage("Product Description cannot exceed 100 characters",
                               "Validation Error", MessageBoxIcon.Error);
                    TxtProductDes.Focus();
                    return false;
                }

                if (!string.IsNullOrEmpty(TxtProductRemarks.Text) && TxtProductRemarks.Text.Trim().Length > 200)
                {
                    ShowMessage("Remarks cannot exceed 200 characters",
                               "Validation Error", MessageBoxIcon.Error);
                    TxtProductRemarks.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                HandleError("Error during validation", ex);
                return false;
            }
        }
        #endregion

        #region Utility Methods
        /// <summary>
        /// Resets all form controls to their default state
        /// </summary>
        private void ResetFormControls()
        {
            try
            {
                TxtProductCode.Clear();
                TxtProductDes.Clear();
                TxtProductShortName.Clear();
                TxtProductRemarks.Clear();
            }
            catch (Exception ex)
            {
                HandleError("Error resetting form controls", ex);
            }
        }

        /// <summary>
        /// Displays a message box with standardized formatting
        /// </summary>
        private DialogResult ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        /// <summary>
        /// Displays a message box with standardized formatting and custom buttons
        /// </summary>
        private DialogResult ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(message, title, buttons, icon);
        }

        /// <summary>
        /// Handles and displays error messages consistently
        /// </summary>
        private void HandleError(string contextMessage, Exception ex)
        {
            string errorMessage = $"{contextMessage}:\n{ex.Message}";

            if (ex.InnerException != null)
            {
                errorMessage += $"\n\nInner Exception:\n{ex.InnerException.Message}";
            }

            ShowMessage(errorMessage, "Error", MessageBoxIcon.Error);

            // Log the error to a file or database if needed
            // ErrorLogger.LogError(errorMessage, ex);
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// Handles the Submit button click event
        /// </summary>
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isFromViewAll && IsValidationPassed())
                {
                    InsertItemMaster();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error in submit operation", ex);
            }
        }

        /// <summary>
        /// Handles the Close button click event
        /// </summary>
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError("Error closing form", ex);
            }
        }

        /// <summary>
        /// Handles the View All button click event
        /// </summary>
        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                ItemMasterViewAll nextForm = new ItemMasterViewAll();
                nextForm.Show();
            }
            catch (Exception ex)
            {
                HandleError("Error opening View All form", ex);
            }
        }

        /// <summary>
        /// Handles the Edit/Save button click event
        /// </summary>
        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isEditMode)
                {
                    if (IsValidationPassed())
                    {
                        UpdateItemMaster();
                    }
                }
                else
                {
                    SetFormToEditMode();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error in edit/save operation", ex);
            }
        }

        /// <summary>
        /// Handles the Delete button click event
        /// </summary>
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(_productCode))
                {
                    DeleteItemMaster();
                }
                else
                {
                    ShowMessage("No item selected for deletion.", "Warning", MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                HandleError("Error in delete operation", ex);
            }
        }
        #endregion
    }
}