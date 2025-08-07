using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SupplierType
{
    public partial class SupplierTypeAdd : Form
    {
        #region Private Fields

        private readonly string _supplierCode;
        private readonly bool _isFromViewAll;
        private bool _isEditMode = false;
        #endregion

        #region Constructor

        public SupplierTypeAdd(string supplierCode = "", bool isFromViewAll = false)
        {
            InitializeComponent();
            _supplierCode = supplierCode;
            _isFromViewAll = isFromViewAll;
        }
        #endregion

        #region Form Load Event
        //======================================= Form Load Event =======================================
        private void SupplierTypeForm_Load(object sender, EventArgs e)
        {
            InitializeFormState();
        }
        #endregion

        #region Form State Management
        //======================================= Initialize Form State =======================================
        private void InitializeFormState()
        {
            if (_isFromViewAll && !string.IsNullOrEmpty(_supplierCode))
            {
                LoadSupplierData();
                SetFormToReadOnlyMode();
            }
            else
            {
                SetFormToInsertMode();
                GenerateSupplierCode();
            }
        }

        //======================================= Set Form To Insert Mode =======================================
        private void SetFormToInsertMode()
        {
            EnableFormControls(true);

            SubmitBtn.Enabled = true;
            SubmitBtn.Text = "Submit";
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;

            _isEditMode = false;
        }

        //======================================= Set Form To Read-Only Mode =======================================
        private void SetFormToReadOnlyMode()
        {
            EnableFormControls(false);

            SubmitBtn.Enabled = false;
            EditBtn.Enabled = true;
            EditBtn.Text = "Edit";
            DeleteBtn.Enabled = true;

            _isEditMode = false;
        }

        //======================================= Set Form To Edit Mode =======================================
        private void SetFormToEditMode()
        {
            EnableFormControls(true);

            SubmitBtn.Enabled = false;
            EditBtn.Enabled = true;
            EditBtn.Text = "Save";
            DeleteBtn.Enabled = false;

            txtSupplierCode.Enabled = false;

            _isEditMode = true;
        }

        //======================================= Enable/Disable Form Controls =======================================
        private void EnableFormControls(bool enabled)
        {
            txtSupplierCode.Enabled = enabled;
            txtSupplierDes.Enabled = enabled;
            txtSupplierRemarks.Enabled = enabled;
        }
        #endregion

        #region Data Operations
        //======================================= Load Supplier Data =======================================
        private void LoadSupplierData()
        {
            string query = @"
                SELECT SupplierTypeCode, Description, Remarks 
                FROM SupplierType 
                WHERE SupplierTypeCode = @SupplierTypeCode AND StatusCode = 'ACT'";

            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierTypeCode", _supplierCode);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtSupplierCode.Text = reader["SupplierTypeCode"].ToString();
                                txtSupplierDes.Text = reader["Description"].ToString();
                                txtSupplierRemarks.Text = reader["Remarks"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Supplier data not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
        }

        //======================================= Generate Supplier Code =======================================
        private void GenerateSupplierCode()
        {
            string query = @"
                SELECT ISNULL(MAX(CAST(SUBSTRING(SupplierTypeCode, 3, LEN(SupplierTypeCode)) AS INT)), 0) + 1 AS NextCode
                FROM SupplierType 
                WHERE SupplierTypeCode LIKE 'ST%' ";

            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        int nextCode = Convert.ToInt32(result);

                        txtSupplierCode.Text = "ST" + nextCode.ToString("000");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error generating supplier code: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSupplierCode.Text = "ST001";
                    }
                }
            }
        }
        #endregion

        #region Button Events
        //======================================= Submit Button Click =======================================
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (!IsValidInput())
                return;

            InsertSupplierType();
        }

        //======================================= Edit Button Click =======================================
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                SetFormToEditMode();
            }
            else
            {
                if (!IsValidInput())
                    return;

                UpdateSupplierType();
            }
        }

        //======================================= Delete Button Click =======================================
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this supplier type?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DeleteSupplierType();
            }
        }

        //======================================= Close Button Click =======================================
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //======================================= View All Button Click =======================================
        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            this.Close();

            //SupplyTypeViewAll nextForm = new SupplyTypeViewAll();
            //nextForm.Show();
        }
        #endregion

        #region Database Operations
        //======================================= Insert Supplier Type =======================================
        private void InsertSupplierType()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertSupplierType", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierTypeCode", txtSupplierCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtSupplierDes.Text.Trim());
                    cmd.Parameters.AddWithValue("@StatusCode", "ACT");
                    cmd.Parameters.AddWithValue("@UserCode", "00123");
                    cmd.Parameters.AddWithValue("@Remarks", txtSupplierRemarks.Text.Trim());

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Supplier type added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ResetFormControls();
                        GenerateSupplierCode();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inserting data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //======================================= Update Supplier Type =======================================
        private void UpdateSupplierType()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateSupplierType", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierTypeCode", _supplierCode);
                    cmd.Parameters.AddWithValue("@Description", txtSupplierDes.Text.Trim());
                    cmd.Parameters.AddWithValue("@UserCode", "00123");
                    cmd.Parameters.AddWithValue("@Remarks", txtSupplierRemarks.Text.Trim());

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier type updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            SetFormToReadOnlyMode();
                        }
                        else
                        {
                            MessageBox.Show("No records were updated. Please check if the record exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //======================================= Delete Supplier Type =======================================
        private void DeleteSupplierType()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteSupplierType", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierTypeCode", _supplierCode);
                    cmd.Parameters.AddWithValue("@UserCode", "00123");

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier type deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No records were deleted. Please check if the record exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region Validation and Utility Methods
        //======================================= Validate Input =======================================
        private bool IsValidInput()
        {
            if (string.IsNullOrWhiteSpace(txtSupplierDes.Text))
            {
                MessageBox.Show("Please enter supplier type description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierDes.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSupplierRemarks.Text))
            {
                MessageBox.Show("Please enter remarks.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierRemarks.Focus();
                return false;
            }

            if (!_isEditMode && !_isFromViewAll)
            {
                if (IsSupplierCodeExists(txtSupplierCode.Text.Trim()))
                {
                    MessageBox.Show("Supplier type code already exists. Please use a different code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSupplierCode.Focus();
                    return false;
                }
            }

            return true;
        }

        //======================================= Check Supplier Code Exists =======================================
        private bool IsSupplierCodeExists(string supplierCode)
        {
            string query = "SELECT COUNT(*) FROM SupplierType WHERE SupplierTypeCode = @SupplierTypeCode AND StatusCode = 'ACT'";

            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierTypeCode", supplierCode);

                    try
                    {
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error checking supplier code: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        //======================================= Reset Form Controls =======================================
        private void ResetFormControls()
        {
            txtSupplierCode.Clear();
            txtSupplierDes.Clear();
            txtSupplierRemarks.Clear();
        }
        #endregion
    }
}