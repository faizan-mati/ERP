using NEW_ERP.Forms.BOM.BOMMaster;
using NEW_ERP.GernalClasses;
using NEW_ERP.Template;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.BOMMaster
{
    public partial class BomMasterAdd : AddFormTemplate
    {
        #region Fields
        private readonly int _bomMasterId;
        private readonly bool _isFromViewAll;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the BomMasterAdd form
        /// </summary>
        /// <param name="bomMasterId">The ID of the BOM to edit/view</param>
        /// <param name="isFromViewAll">Flag indicating if coming from View All screen</param>
        public BomMasterAdd(int bomMasterId, bool isFromViewAll)
        {
            InitializeComponent();
            _bomMasterId = bomMasterId;
            _isFromViewAll = isFromViewAll;
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the form load event
        /// </summary>
        private void BomMasterAdd_Load(object sender, EventArgs e)
        {
            LoadSaleOrders();

            if (_isFromViewAll && _bomMasterId > 0)
            {
                LoadBOMData();
                SetViewMode();
            }
            else
            {
                SetAddMode();
            }
        }

        /// <summary>
        /// Handles the Sale Order dropdown selection change event
        /// </summary>
        private void SaleOrderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaleOrderBox.SelectedValue != null &&
                int.TryParse(SaleOrderBox.SelectedValue.ToString(), out int saleOrderId))
            {
                LoadProducts(saleOrderId);
            }
        }

        /// <summary>
        /// Handles the Sale Order dropdown opening event
        /// </summary>
        private void SaleOrderBox_DropDown(object sender, EventArgs e)
        {
            if (!_isFromViewAll)
            {
                ProductBox.DataSource = null;
                ProductBox.Items.Clear();
            }
        }

        /// <summary>
        /// Handles the Submit button click event
        /// </summary>
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                InsertBOMRecord();
            }
        }

        /// <summary>
        /// Handles the Edit/Save button click event
        /// </summary>
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EditBtn.Text == "Edit")
            {
                SetEditMode();
            }
            else if (EditBtn.Text == "Save" && ValidateInput())
            {
                UpdateBOMRecord();
            }
        }

        /// <summary>
        /// Handles the Delete button click event
        /// </summary>
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (ConfirmDelete())
            {
                DeleteBOMRecord();
            }
        }

        /// <summary>
        /// Handles the View All button click event
        /// </summary>
        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            Close();
            new BomMasterViewAll().Show();
        }

        /// <summary>
        /// Handles the Close button click event
        /// </summary>
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Data Loading Methods

        /// <summary>
        /// Loads all active sale orders into the dropdown
        /// </summary>
        private void LoadSaleOrders()
        {
            try
            {
                const string query = @"SELECT DISTINCT SaleOrderID, SaleOrderNo 
                                    FROM SaleOrderMaster 
                                    WHERE StatusCode='ACT' 
                                    ORDER BY SaleOrderID DESC";

                using (var adapter = new SqlDataAdapter(query, AppConnection.GetConnectionString()))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    SaleOrderBox.DataSource = dt;
                    SaleOrderBox.DisplayMember = "SaleOrderNo";
                    SaleOrderBox.ValueMember = "SaleOrderID";
                    SaleOrderBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading Sale Orders", ex);
            }
        }

        /// <summary>
        /// Loads BOM data for editing/viewing
        /// </summary>
        private void LoadBOMData()
        {
            try
            {
                const string query = @"SELECT bm.SaleOrderID, bm.ProductID, bm.VersionNo
                                    FROM BOMMaster bm 
                                    WHERE bm.BOMID = @BOMId";

                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BOMId", _bomMasterId);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int saleOrderId = Convert.ToInt32(reader["SaleOrderID"]);
                            string productId = reader["ProductID"].ToString();
                            string versionNo = reader["VersionNo"].ToString();

                            SaleOrderBox.SelectedValue = saleOrderId;
                            LoadProducts(saleOrderId);
                            ProductBox.SelectedValue = productId;
                            txtVersionNo.Text = versionNo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading BOM data", ex);
            }
        }

        /// <summary>
        /// Loads products for the selected sale order
        /// </summary>
        /// <param name="saleOrderId">The ID of the selected sale order</param>
        private void LoadProducts(int saleOrderId)
        {
            try
            {
                const string query = @"SELECT i.ProductCode, i.ProductDescription
                                    FROM SaleOrderMaster sm
                                    INNER JOIN ItemMaster i ON sm.ProductID = i.ProductCode
                                    WHERE sm.SaleOrderID = @SaleOrderID";

                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);

                    var adapter = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    ProductBox.DataSource = dt;
                    ProductBox.DisplayMember = "ProductDescription";
                    ProductBox.ValueMember = "ProductCode";

                    if (!_isFromViewAll)
                        ProductBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading Products", ex);
            }
        }
        #endregion

        #region Form Mode Methods

        /// <summary>
        /// Sets the form to Add mode (enables editing controls)
        /// </summary>
        private void SetAddMode()
        {
            // Enable controls
            SaleOrderBox.Enabled = true;
            ProductBox.Enabled = true;
            txtVersionNo.Enabled = true;

            // Configure buttons
            SubmitBtn.Visible = true;
            SubmitBtn.Text = "Submit";
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;

            Text = "BOM Master - Add New";
        }

        /// <summary>
        /// Sets the form to View mode (disables editing controls)
        /// </summary>
        private void SetViewMode()
        {
            // Disable controls
            SaleOrderBox.Enabled = false;
            ProductBox.Enabled = false;
            txtVersionNo.Enabled = false;

            // Configure buttons
            SubmitBtn.Enabled = false;
            EditBtn.Enabled = true;
            EditBtn.Text = "Edit";
            DeleteBtn.Enabled = true;

            Text = "BOM Master - View";
        }

        /// <summary>
        /// Sets the form to Edit mode (enables editing controls)
        /// </summary>
        private void SetEditMode()
        {
            // Enable controls
            SaleOrderBox.Enabled = true;
            ProductBox.Enabled = true;
            txtVersionNo.Enabled = true;

            // Configure buttons
            SubmitBtn.Enabled = false;
            EditBtn.Enabled = true;
            EditBtn.Text = "Save";
            DeleteBtn.Enabled = false;

            Text = "BOM Master - Edit";
        }
        #endregion

        #region Database Operations

        /// <summary>
        /// Inserts a new BOM record into the database
        /// </summary>
        private void InsertBOMRecord()
        {
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (var cmd = new SqlCommand("sp_InsertBOMMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SaleOrderId", Convert.ToInt32(SaleOrderBox.SelectedValue));
                    cmd.Parameters.AddWithValue("@ProductId", ProductBox.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@VersionNo", txtVersionNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@StatusCode", "ACT");
                    cmd.Parameters.AddWithValue("@CreatedBy", "Admin");

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        ShowSuccess("BOM inserted successfully!");
                        ClearForm();
                    }
                    else
                    {
                        ShowWarning("Insertion failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error inserting BOM", ex);
            }
        }

        /// <summary>
        /// Updates an existing BOM record in the database
        /// </summary>
        private void UpdateBOMRecord()
        {
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (var cmd = new SqlCommand("sp_UpdateBOMMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BOMId", _bomMasterId);
                    cmd.Parameters.AddWithValue("@SaleOrderId", Convert.ToInt32(SaleOrderBox.SelectedValue));
                    cmd.Parameters.AddWithValue("@ProductId", ProductBox.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@VersionNo", txtVersionNo.Text.Trim());

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        ShowSuccess("BOM updated successfully!");
                        SetViewMode();
                    }
                    else
                    {
                        ShowWarning("Update failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error updating BOM", ex);
            }
        }

        /// <summary>
        /// Deletes a BOM record from the database
        /// </summary>
        private void DeleteBOMRecord()
        {
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (var cmd = new SqlCommand("sp_DeleteBOMMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BOMId", _bomMasterId);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        ShowSuccess("BOM deleted successfully!");
                        Close();
                    }
                    else
                    {
                        ShowWarning("Delete failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error deleting BOM", ex);
            }
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Validates the form input
        /// </summary>
        /// <returns>True if input is valid, otherwise false</returns>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtVersionNo.Text) ||
                SaleOrderBox.SelectedIndex == -1 ||
                ProductBox.SelectedIndex == -1)
            {
                ShowValidationError("Please fill all the fields");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Shows a confirmation dialog for deletion
        /// </summary>
        /// <returns>True if user confirms deletion, otherwise false</returns>
        private bool ConfirmDelete()
        {
            return MessageBox.Show(
                "Are you sure you want to delete this BOM record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// Clears the form inputs
        /// </summary>
        private void ClearForm()
        {
            txtVersionNo.Clear();
            SaleOrderBox.SelectedIndex = -1;
            ProductBox.DataSource = null;
            ProductBox.Items.Clear();
        }

        /// <summary>
        /// Shows an error message
        /// </summary>
        /// <param name="message">The main error message</param>
        /// <param name="ex">The exception that occurred</param>
        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows a success message
        /// </summary>
        /// <param name="message">The success message to display</param>
        private void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows a warning message
        /// </summary>
        /// <param name="message">The warning message to display</param>
        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows a validation error message
        /// </summary>
        /// <param name="message">The validation message to display</param>
        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}