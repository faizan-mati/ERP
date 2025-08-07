using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.CountryForms
{
    public partial class CountryFormAdd : Form
    {
        #region Constants
        #endregion

        #region Private Fields
        private readonly int _countryId;
        private readonly bool _isFromViewAll;
        private bool _isEditMode;
        #endregion

        #region Constructor
        public CountryFormAdd(int countryId, bool isFromViewAll)
        {
            InitializeComponent();
            _countryId = countryId;
            _isFromViewAll = isFromViewAll;
            _isEditMode = false;
        }
        #endregion

        #region Form Events

        //======================================= Form Load Event =======================================
        private void CountryForm_Load(object sender, EventArgs e)
        {
            try
            {
                ShowStatusCode();
                if (_isFromViewAll && _countryId > 0)
                {
                    LoadCountryForEditing();
                    SubmitBtn.Enabled = false;
                }
                else
                {
                    SetFormForInsertMode();
                    SubmitBtn.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                HandleUnexpectedError(ex);
            }
        }
        #endregion

        #region Button Events

        //======================================= Submit Button Click =======================================
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    InsertCountry();
                }
            }
            catch (Exception ex)
            {
                HandleUnexpectedError(ex);
            }
        }

        //======================================= Edit/Save Button Click =======================================
        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isEditMode)
                {
                    SetFormForEditMode();
                    SubmitBtn.Enabled = false;
                }
                else
                {
                    if (ValidateInputs())
                    {
                        UpdateCountry();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleUnexpectedError(ex);
            }
        }

        //======================================= Delete Button Click =======================================
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteCountry();
            }
            catch (Exception ex)
            {
                HandleUnexpectedError(ex);
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
            try
            {
                ShowCountryViewAllForm();
            }
            catch (Exception ex)
            {
                HandleUnexpectedError(ex);
            }
        }
        #endregion

        #region Form Mode Management

        //======================================= Set Form For Insert Mode =======================================
        private void SetFormForInsertMode()
        {
            _isEditMode = false;
            UpdateFormControls(
                readOnly: false,
                submitEnabled: true,
                editEnabled: false,
                deleteEnabled: false,
                 StatusCodeEnabled: true
            );
            isCheckedcheckbox.Checked = true;
            EditBtn.Text = "Edit";
        }

        //======================================= Set Form For View Mode =======================================
        private void SetFormForViewMode()
        {
            _isEditMode = false;
            UpdateFormControls(
                readOnly: true,
                submitEnabled: false,
                editEnabled: true,
                deleteEnabled: true,
                  StatusCodeEnabled: false
            );
            EditBtn.Text = "Edit";
        }

        //======================================= Set Form For Edit Mode =======================================
        private void SetFormForEditMode()
        {
            _isEditMode = true;
            UpdateFormControls(
                readOnly: false,
                submitEnabled: false,
                editEnabled: true,
                deleteEnabled: false,
                StatusCodeEnabled: true
            );
            EditBtn.Text = "Save";
        }

        //======================================= Update Form Controls =======================================
        private void UpdateFormControls(bool readOnly, bool submitEnabled, bool editEnabled, bool deleteEnabled, bool StatusCodeEnabled)
        {
            TxtCountryName.ReadOnly = readOnly;
            TxtCountryCode.ReadOnly = readOnly;
            isCheckedcheckbox.Enabled = !readOnly;

            SubmitBtn.Enabled = submitEnabled;
            EditBtn.Enabled = editEnabled;
            DeleteBtn.Enabled = deleteEnabled;


            StatusCodeBox.Enabled = StatusCodeEnabled;
        }
        #endregion

        #region Database Operations

        //======================================= Load Country For Editing =======================================
        private void LoadCountryForEditing()
        {
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    const string query = @"
                SELECT 
                    C.CountryName, 
                    C.CountryCode, 
                    C.IsActive, 
                    S.StatusCode  -- human-readable status
                FROM Country C
                LEFT JOIN Status S ON C.StatusCode = S.StatusId
                WHERE C.CountryId = @CountryId";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CountryId", _countryId);
                        conn.Open();

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TxtCountryName.Text = reader["CountryName"].ToString();
                                TxtCountryCode.Text = reader["CountryCode"].ToString();
                                isCheckedcheckbox.Checked = Convert.ToBoolean(reader["IsActive"]);
                                StatusCodeBox.Text = reader["StatusCode"].ToString();  
                                SetFormForViewMode();
                            }
                            else
                            {
                                ShowErrorMessage("Country data not found!", "Error");
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ShowErrorMessage($"Error loading country data:\n{ex.Message}", "Database Error");
                this.Close();
            }
        }

        //======================================= Insert Country =======================================
        private void InsertCountry()
        {
            using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
            using (var cmd = new SqlCommand("sp_InsertCountry", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                AddCommonParameters(cmd);
                cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();

                ShowSuccessMessage("Country added successfully!");
                ResetFormControls();
            }
        }

        //======================================= Update Country =======================================
        private void UpdateCountry()
        {
            using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
            using (var cmd = new SqlCommand("sp_UpdateCountry", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CountryId", _countryId);
                AddCommonParameters(cmd);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ShowSuccessMessage("Country updated successfully!");
                    SetFormForViewMode();
                }
                else
                {
                    ShowWarningMessage("No records were updated. Please try again.");
                }
            }
        }

        //======================================= Delete Country =======================================
        private void DeleteCountry()
        {
            if (ShowConfirmationDialog(
                "Are you sure you want to delete this country?",
                "Confirm Delete") != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                using (var cmd = new SqlCommand("sp_DeleteCountry", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CountryId", _countryId);
                    cmd.Parameters.AddWithValue("@StatusCode", 3);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ShowSuccessMessage("Country deleted successfully!");
                        CloseAndShowViewAllIfNeeded();
                    }
                    else
                    {
                        ShowWarningMessage("No records were deleted. Please try again.");
                    }
                }
            }
            catch (SqlException ex)
            {
                ShowErrorMessage($"Error deleting country:\n{ex.Message}", "Database Error");
            }
        }

        //======================================= Add Common Parameters =======================================
        private void AddCommonParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@CountryName", TxtCountryName.Text.Trim());
            cmd.Parameters.AddWithValue("@CountryCode", TxtCountryCode.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
            cmd.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));
        }
        #endregion

        #region Helper Methods

        //======================================= Validate Inputs =======================================
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(TxtCountryName.Text))
            {
                ShowErrorMessage("Please enter country name.", "Validation Error");
                TxtCountryName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(TxtCountryCode.Text))
            {
                ShowErrorMessage("Please enter country code.", "Validation Error");
                TxtCountryCode.Focus();
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

        //======================================= Reset Form Controls =======================================
        private void ResetFormControls()
        {
            TxtCountryName.Clear();
            TxtCountryCode.Clear();
            isCheckedcheckbox.Checked = true;
            TxtCountryName.Focus();

            StatusCodeBox.SelectedIndex = -1;
        }

        //======================================= Show Country View All Form =======================================
        private void ShowCountryViewAllForm()
        {
            this.Close();
            CountryViewAll viewAllForm = new CountryViewAll();
            viewAllForm.Show();
        }

        //======================================= Close And Show View All If Needed =======================================
        private void CloseAndShowViewAllIfNeeded()
        {
            this.Close();
            if (_isFromViewAll)
            {
                ShowCountryViewAllForm();
            }
        }
        #endregion

        #region Message Dialogs

        //======================================= Show Success Message =======================================
        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //======================================= Show Error Message =======================================
        private void ShowErrorMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //======================================= Show Warning Message =======================================
        private void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //======================================= Show Confirmation Dialog =======================================
        private DialogResult ShowConfirmationDialog(string message, string title)
        {
            return MessageBox.Show(message, title,
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
        }
        #endregion

        #region Error Handling

        //======================================= Handle Unexpected Error =======================================
        private void HandleUnexpectedError(Exception ex)
        {
            ShowErrorMessage($"An unexpected error occurred:\n{ex.Message}", "Error");
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


        #endregion
    }
}