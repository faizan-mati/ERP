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
        private readonly int _authorityId;
        private readonly bool _isFromViewAll;
        private bool _isEditMode = false;
        private string _originalAuthorityCode = string.Empty;

        private const string ACTIVE_STATUS = "ACT";
        private const string INACTIVE_STATUS = "INA";
        private const string DEFAULT_USER_CODE = "00123";

        public AuthorityAdd(int authorityId, bool isFromViewAll)
        {
            InitializeComponent();
            _authorityId = authorityId;
            _isFromViewAll = isFromViewAll;
        }

        private void AuthorityAdd_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigureFormMode();

                if (_isFromViewAll && _authorityId > 0)
                {
                    LoadAuthorityData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureFormMode()
        {
            if (_isFromViewAll && _authorityId > 0)
                ConfigureEditDeleteMode();
            else
                ConfigureInsertMode();
        }

        private void ConfigureEditDeleteMode()
        {
            SetFormReadOnlyMode(true);
            SubmitBtn.Enabled = false;
            EditBtn.Enabled = true;
            DeleteBtn.Enabled = true;
            EditBtn.Text = "Edit";
            this.Text = "Authority - View/Edit";
        }

        private void ConfigureInsertMode()
        {
            SetFormReadOnlyMode(false);
            SubmitBtn.Enabled = true;
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;
            SubmitBtn.Text = "Submit";
            this.Text = "Authority - Add New";
        }

        private void SetFormReadOnlyMode(bool readOnly)
        {
            TxtAuthorityCode.ReadOnly = readOnly;
            TxtAuthorityName.ReadOnly = readOnly;
            TxtAuthorityRemarks.ReadOnly = readOnly;

            Color backgroundColor = readOnly ? SystemColors.Control : SystemColors.Window;
            TxtAuthorityCode.BackColor = backgroundColor;
            TxtAuthorityName.BackColor = backgroundColor;
            TxtAuthorityRemarks.BackColor = backgroundColor;
        }

        private void LoadAuthorityData()
        {
            const string query = @"SELECT AuthorityCode, AuthorityName, Remarks, StatusCode
                                 FROM AuthorityMaster 
                                 WHERE AuthorityCode = @AuthorityCode";

            try
            {
                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorityCode", _authorityId.ToString());
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _originalAuthorityCode = reader["AuthorityCode"].ToString();
                            TxtAuthorityCode.Text = _originalAuthorityCode;
                            TxtAuthorityName.Text = reader["AuthorityName"].ToString();
                            TxtAuthorityRemarks.Text = reader["Remarks"]?.ToString() ?? string.Empty;
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

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateFormInput()) return;

            if (_isEditMode)
                UpdateAuthorityData();
            else
                InsertAuthorityData();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
                EnableEditMode();
            else
                UpdateAuthorityData();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to deactivate this authority record?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeactivateAuthorityRecord();
            }
        }

        private void EnableEditMode()
        {
            _isEditMode = true;
            SetFormReadOnlyMode(false);
            EditBtn.Text = "Save";
            SubmitBtn.Enabled = true;
            SubmitBtn.Text = "Save";
            DeleteBtn.Enabled = false;
        }

        private void DisableEditMode()
        {
            _isEditMode = false;
            SetFormReadOnlyMode(true);
            EditBtn.Text = "Edit";
            SubmitBtn.Enabled = false;
            DeleteBtn.Enabled = true;
        }

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

            return true;
        }

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
                    command.Parameters.AddWithValue("@StatusCode", ACTIVE_STATUS);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Authority added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetFormControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting authority: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                    command.Parameters.AddWithValue("@StatusCode", ACTIVE_STATUS);
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

        private void DeactivateAuthorityRecord()
        {
            const string query = "UPDATE AuthorityMaster SET StatusCode = @StatusCode WHERE AuthorityCode = @AuthorityCode";

            try
            {
                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorityCode", _originalAuthorityCode);
                    command.Parameters.AddWithValue("@StatusCode", INACTIVE_STATUS);

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

        private void ResetFormControls()
        {
            TxtAuthorityCode.Clear();
            TxtAuthorityName.Clear();
            TxtAuthorityRemarks.Clear();
            TxtAuthorityCode.Focus();
        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            new AuthorityViewAll().Show();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}