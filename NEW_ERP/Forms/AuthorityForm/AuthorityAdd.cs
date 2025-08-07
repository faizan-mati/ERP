using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace NEW_ERP.Forms.AuthorityForm
{
    public partial class AuthorityAdd : Form
    {
        private readonly string _authorityCode;
        private readonly bool _isFromViewAll;
        private bool _isEditMode = false;
        private string _originalAuthorityCode = string.Empty;

        private const string DEFAULT_USER_CODE = "00123";

        public AuthorityAdd(string authorityCode, bool isFromViewAll)
        {
            InitializeComponent();
            _authorityCode = authorityCode;
            _isFromViewAll = isFromViewAll;
        }

        //======================================= Form Load Event =======================================
        private void AuthorityAdd_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigureFormMode();
                ShowStatusCode();

                if (_isFromViewAll && !string.IsNullOrEmpty(_authorityCode))
                {
                    LoadAuthorityData();
                }
                else if (!_isFromViewAll)
                {
                    // Generate auto code only in Add New mode
                    TxtAuthorityCode.Text = GenerateAutoAuthorityCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Generate Auto Authority Code =======================================
        private string GenerateAutoAuthorityCode()
        {
            const string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(AuthorityCode, 4, LEN(AuthorityCode)) AS INT)), 0) + 1 FROM AuthorityMaster WHERE AuthorityCode LIKE 'Ath%'";

            try
            {
                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int nextCode = (int)command.ExecuteScalar();
                    return $"ATH{nextCode:D3}"; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating auto code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "ATH001";
            }
        }


        //======================================= Configure Form Mode =======================================
        private void ConfigureFormMode()
        {
            if (_isFromViewAll && !string.IsNullOrEmpty(_authorityCode))
                ConfigureEditDeleteMode();
            else
                ConfigureInsertMode();
        }

        //======================================= Configure Edit/Delete Mode =======================================
        private void ConfigureEditDeleteMode()
        {
            SetFormReadOnlyMode(true);
            SubmitBtn.Enabled = false;
            EditBtn.Enabled = true;
            DeleteBtn.Enabled = true;
            EditBtn.Text = "Edit";
            this.Text = "Authority - View/Edit";
        }

        //======================================= Configure Insert Mode =======================================
        private void ConfigureInsertMode()
        {
            SetFormReadOnlyMode(false);
            SubmitBtn.Enabled = true;
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;
            SubmitBtn.Text = "Submit";
            this.Text = "Authority - Add New";
            //TxtAuthorityCode.ReadOnly = true; 
            TxtAuthorityCode.BackColor = SystemColors.Control;
        }

        //======================================= Set Form ReadOnly Mode =======================================
        private void SetFormReadOnlyMode(bool readOnly)
        {
            //TxtAuthorityCode.ReadOnly = readOnly || !_isEditMode;
            TxtAuthorityName.ReadOnly = readOnly;
            TxtAuthorityRemarks.ReadOnly = readOnly;
            StatusCodeBox.Enabled = false;

            Color backgroundColor = readOnly ? SystemColors.Control : SystemColors.Window;
            //TxtAuthorityCode.BackColor = TxtAuthorityCode.ReadOnly ? SystemColors.Control : backgroundColor;
            TxtAuthorityName.BackColor = backgroundColor;
            TxtAuthorityRemarks.BackColor = backgroundColor;
        }

        //======================================= Load Authority Data =======================================
        private void LoadAuthorityData()
        {
            const string query = @"
    SELECT 
        AM.AuthorityCode, 
        AM.AuthorityName, 
        AM.Remarks, 
        S.StatusCode    
    FROM AuthorityMaster AM
    LEFT JOIN Status S ON AM.StatusCode = S.StatusId
    WHERE AM.AuthorityCode = @AuthorityCode";


            try
            {
                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorityCode", _authorityCode);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _originalAuthorityCode = reader["AuthorityCode"].ToString();
                            TxtAuthorityCode.Text = _originalAuthorityCode;
                            TxtAuthorityName.Text = reader["AuthorityName"].ToString();
                            TxtAuthorityRemarks.Text = reader["Remarks"]?.ToString() ?? string.Empty;
                            StatusCodeBox.Text = reader["StatusCode"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Authority record not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading authority data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Submit Button Click =======================================
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateFormInput()) return;

            if (_isEditMode)
                UpdateAuthorityData();
            else
                InsertAuthorityData();
        }

        //======================================= Edit Button Click =======================================
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
                EnableEditMode();
            else
                UpdateAuthorityData();
        }

        //======================================= Delete Button Click =======================================
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to deactivate this authority record?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeactivateAuthorityRecord();
            }
        }

        //======================================= Enable Edit Mode =======================================
        private void EnableEditMode()
        {
            _isEditMode = true;
            SetFormReadOnlyMode(false);
            EditBtn.Text = "Save";
            SubmitBtn.Enabled = false;
            DeleteBtn.Enabled = false;
            StatusCodeBox.Enabled = true;
        }

        //======================================= Disable Edit Mode =======================================
        private void DisableEditMode()
        {
            _isEditMode = false;
            SetFormReadOnlyMode(true);
            EditBtn.Text = "Edit";
            SubmitBtn.Enabled = false;
            DeleteBtn.Enabled = true;
        }

        //======================================= Validate Form Input =======================================
        private bool ValidateFormInput()
        {
            if (string.IsNullOrWhiteSpace(TxtAuthorityCode.Text))
            {
                MessageBox.Show("Authority Code is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtAuthorityCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TxtAuthorityName.Text))
            {
                MessageBox.Show("Authority Name is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtAuthorityName.Focus();
                return false;
            }

            if (StatusCodeBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Status Code", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StatusCodeBox.Focus();
                return false;
            }


            return true;
        }

        //======================================= Check Duplicate Authority Code =======================================
        private bool CheckDuplicateAuthorityCode(string authorityCode)
        {
            const string query = "SELECT COUNT(*) FROM AuthorityMaster WHERE AuthorityCode = @AuthorityCode";

            using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AuthorityCode", authorityCode);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        //======================================= Insert Authority Data =======================================
        private void InsertAuthorityData()
        {
            string newAuthorityCode = TxtAuthorityCode.Text.Trim();

            if (CheckDuplicateAuthorityCode(newAuthorityCode))
            {
                MessageBox.Show("Authority Code already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            const string storedProcedure = "sp_InsertAuthorityMaster";

            try
            {
                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AuthorityCode", newAuthorityCode);
                    command.Parameters.AddWithValue("@AuthorityName", TxtAuthorityName.Text.Trim());
                    command.Parameters.AddWithValue("@Remarks", TxtAuthorityRemarks.Text.Trim());
                    command.Parameters.AddWithValue("@SystemDate", DateTime.Now);
                    command.Parameters.AddWithValue("@UserCode", DEFAULT_USER_CODE);
                    command.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Authority added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetFormControls();
                    // Generate new auto code after successful insert
                    TxtAuthorityCode.Text = GenerateAutoAuthorityCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting authority: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Update Authority Data =======================================
        private void UpdateAuthorityData()
        {
            string newAuthorityCode = TxtAuthorityCode.Text.Trim();

            // Check for duplicate only if code is being changed
            if (newAuthorityCode != _originalAuthorityCode && CheckDuplicateAuthorityCode(newAuthorityCode))
            {
                MessageBox.Show("Authority Code already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            const string storedProcedure = "sp_UpdateAuthorityMaster";

            try
            {
                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AuthorityCode", _originalAuthorityCode);
                    command.Parameters.AddWithValue("@AuthorityCode1", newAuthorityCode);
                    command.Parameters.AddWithValue("@AuthorityName", TxtAuthorityName.Text.Trim());
                    command.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));
                    command.Parameters.AddWithValue("@Remarks", TxtAuthorityRemarks.Text.Trim());

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Authority updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _originalAuthorityCode = newAuthorityCode;
                        DisableEditMode();
                    }
                    else
                    {
                        MessageBox.Show("No records updated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating authority: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Deactivate Authority Record =======================================
        private void DeactivateAuthorityRecord()
        {
            const string procedureName = "sp_DeleteAuthorityMaster";

            try
            {
                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AuthorityCode", _originalAuthorityCode);
                    command.Parameters.AddWithValue("@StatusCode", 3);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Authority deactivated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        new AuthorityViewAll().Show();
                    }
                    else
                    {
                        MessageBox.Show("No records deactivated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deactivating authority: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Reset Form Controls =======================================
        private void ResetFormControls()
        {
            TxtAuthorityName.Clear();
            TxtAuthorityRemarks.Clear();
            TxtAuthorityName.Focus();

            StatusCodeBox.SelectedIndex = -1;

        }

        //======================================= View All Button Click =======================================
        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            new AuthorityViewAll().Show();
        }

        //======================================= Close Button Click =======================================
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StatusCodeBox_DropDown(object sender, EventArgs e)
        {
            
        }


        protected void ShowStatusCode()
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

                    StatusCodeBox.DataSource = dtRoles;
                    StatusCodeBox.DisplayMember = "StatusCode";
                    StatusCodeBox.ValueMember = "StatusId";
                    StatusCodeBox.SelectedIndex = -1;

                    StatusCodeBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    StatusCodeBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                    StatusCodeBox.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
        }

        //======================================= Get Nullable Value – Combo =======================================
        private object GetNullableValue(ComboBox comboBox)
        {
            return comboBox.SelectedItem != null ? comboBox.SelectedValue : DBNull.Value;
        }


    }
}