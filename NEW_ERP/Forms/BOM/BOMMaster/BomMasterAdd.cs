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

        public BomMasterAdd(int bomMasterId, bool isFromViewAll)
        {
            InitializeComponent();
            _bomMasterId = bomMasterId;
            _isFromViewAll = isFromViewAll;
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the form load event. Loads initial data and sets appropriate form mode.
        /// </summary>
        private void BomMasterAdd_Load(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ShowError("Failed to initialize form", ex);
                Close();
            }
        }

        /// <summary>
        /// Handles the Sale Order dropdown selection change event. Loads products for selected order.
        /// </summary>
        private void SaleOrderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SaleOrderBox.SelectedValue != null &&
                    int.TryParse(SaleOrderBox.SelectedValue.ToString(), out int saleOrderId))
                {
                    LoadProducts(saleOrderId);
                }
            }
            catch (Exception ex)
            {
                ShowError("Failed to load products for selected sale order", ex);
            }
        }

        /// <summary>
        /// Handles the Sale Order dropdown opening event. Clears product selection for new entries.
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
        /// Handles the Submit button click event for new BOM records.
        /// </summary>
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    InsertBOMRecord();
                }
            }
            catch (Exception ex)
            {
                ShowError("Failed to submit BOM record", ex);
            }
        }

        /// <summary>
        /// Handles the Edit/Save button click event for existing BOM records.
        /// </summary>
        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ShowError("Failed to process edit/save operation", ex);
            }
        }

        /// <summary>
        /// Handles the Delete button click event for existing BOM records.
        /// </summary>
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfirmDelete())
                {
                    DeleteBOMRecord();
                }
            }
            catch (Exception ex)
            {
                ShowError("Failed to delete BOM record", ex);
            }
        }

        /// <summary>
        /// Handles the View All button click event to return to the BOM master list.
        /// </summary>
        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
                new BomMasterViewAll().Show();
            }
            catch (Exception ex)
            {
                ShowError("Failed to open BOM master view", ex);
            }
        }

        /// <summary>
        /// Handles the Close button click event to close the form.
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
            catch (SqlException sqlEx)
            {
                throw new Exception("Database error while loading sale orders", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load sale orders", ex);
            }
        }

        /// <summary>
        /// Loads BOM data for editing/viewing
        /// </summary>

        private void LoadBOMData()
        {
            if (_bomMasterId <= 0)
            {
                throw new ArgumentException("Invalid BOM ID");
            }

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
                        else
                        {
                            throw new Exception($"BOM record with ID {_bomMasterId} not found");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Database error while loading BOM data", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load BOM data", ex);
            }
        }

        /// <summary>
        /// Loads products for the selected sale order
        /// </summary>

        private void LoadProducts(int saleOrderId)
        {
            if (saleOrderId <= 0)
            {
                throw new ArgumentException("Invalid Sale Order ID");
            }

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

                    if (dt.Rows.Count == 0)
                    {
                        throw new Exception($"No products found for Sale Order ID {saleOrderId}");
                    }

                    ProductBox.DataSource = dt;
                    ProductBox.DisplayMember = "ProductDescription";
                    ProductBox.ValueMember = "ProductCode";

                    if (!_isFromViewAll)
                    {
                        ProductBox.SelectedIndex = -1;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Database error while loading products", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load products", ex);
            }
        }
        #endregion

        #region Form Mode Methods

        /// <summary>
        /// Sets the form to Add mode (enables editing controls for new records)
        /// </summary>
        private void SetAddMode()
        {
            SaleOrderBox.Enabled = true;
            ProductBox.Enabled = true;
            txtVersionNo.Enabled = true;

            SubmitBtn.Visible = true;
            SubmitBtn.Text = "Submit";
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;

            Text = "BOM Master - Add New";
        }

        /// <summary>
        /// Sets the form to View mode (disables editing controls for viewing records)
        /// </summary>
        private void SetViewMode()
        {
            SaleOrderBox.Enabled = false;
            ProductBox.Enabled = false;
            txtVersionNo.Enabled = false;

            SubmitBtn.Enabled = false;
            EditBtn.Enabled = true;
            EditBtn.Text = "Edit";
            DeleteBtn.Enabled = true;

            Text = "BOM Master - View";
        }

        /// <summary>
        /// Sets the form to Edit mode (enables editing controls for existing records)
        /// </summary>
        private void SetEditMode()
        {
            SaleOrderBox.Enabled = true;
            ProductBox.Enabled = true;
            txtVersionNo.Enabled = true;

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
            if (SaleOrderBox.SelectedValue == null || ProductBox.SelectedValue == null)
            {
                ShowValidationError("Please select both Sale Order and Product");
                return;
            }

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
                        ShowWarning("No records were affected by the insert operation");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Database error while inserting BOM record", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert BOM record", ex);
            }
        }

        /// <summary>
        /// Updates an existing BOM record in the database
        /// </summary>

        private void UpdateBOMRecord()
        {
            if (SaleOrderBox.SelectedValue == null || ProductBox.SelectedValue == null)
            {
                ShowValidationError("Please select both Sale Order and Product");
                return;
            }

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
                        ShowWarning("No records were affected by the update operation");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Database error while updating BOM record", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update BOM record", ex);
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
                        ShowWarning("No records were affected by the delete operation");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Database error while deleting BOM record", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete BOM record", ex);
            }
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Validates the form input before submission
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtVersionNo.Text))
            {
                ShowValidationError("Version number is required");
                txtVersionNo.Focus();
                return false;
            }

            if (SaleOrderBox.SelectedIndex == -1)
            {
                ShowValidationError("Please select a Sale Order");
                SaleOrderBox.Focus();
                return false;
            }

            if (ProductBox.SelectedIndex == -1)
            {
                ShowValidationError("Please select a Product");
                ProductBox.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Shows a confirmation dialog for deletion
        /// </summary>
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

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}