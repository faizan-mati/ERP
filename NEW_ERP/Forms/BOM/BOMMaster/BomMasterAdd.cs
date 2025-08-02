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

        //======================================= Form Load Event =======================================
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

        //======================================= Sale Order Selection Changed =======================================
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

        //======================================= Sale Order Dropdown Open =======================================
        private void SaleOrderBox_DropDown(object sender, EventArgs e)
        {
            if (!_isFromViewAll)
            {
                ProductBox.DataSource = null;
                ProductBox.Items.Clear();
            }
        }

        //======================================= Submit Button Click =======================================
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

        //======================================= Edit/Save Button Click =======================================
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

        //======================================= Delete Button Click =======================================
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

        //======================================= View All Button Click =======================================
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

        //======================================= Close Button Click =======================================
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Data Loading Methods

        //======================================= Load Sale Orders =======================================
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

        //======================================= Load BOM Data =======================================
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

        //======================================= Load Products =======================================
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

        //======================================= Set Add Mode =======================================
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

        //======================================= Set View Mode =======================================
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

        //======================================= Set Edit Mode =======================================
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

        //======================================= Insert BOM Record =======================================
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

        //======================================= Update BOM Record =======================================
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

        //======================================= Delete BOM Record =======================================
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

        //======================================= Validate Input =======================================
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

        //======================================= Confirm Delete =======================================
        private bool ConfirmDelete()
        {
            return MessageBox.Show(
                "Are you sure you want to delete this BOM record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;
        }

        //======================================= Clear Form =======================================
        private void ClearForm()
        {
            txtVersionNo.Clear();
            SaleOrderBox.SelectedIndex = -1;
            ProductBox.DataSource = null;
            ProductBox.Items.Clear();
        }

        //======================================= Show Error =======================================
        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //======================================= Show Success =======================================
        private void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //======================================= Show Warning =======================================
        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //======================================= Show Validation Error =======================================
        private void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}