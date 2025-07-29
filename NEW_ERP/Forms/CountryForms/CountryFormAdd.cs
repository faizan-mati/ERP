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
        private const string ACTIVE_STATUS = "ACT";
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

        private void CountryForm_Load(object sender, EventArgs e)
        {
            try
            {
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
        /// <summary>
        /// Handles the Submit button click event
        /// Only enabled when adding new countries (countryId = 0)
        /// </summary>
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

        /// <summary>
        /// Handles the Edit/Save button click event
        /// First click enables editing mode, second click saves changes
        /// </summary>
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

        /// <summary>
        /// Handles the Delete button click event
        /// Performs a soft delete of the country record
        /// </summary>
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

        /// <summary>
        /// Handles the Close button click event
        /// Closes the current form
        /// </summary>
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the View All button click event
        /// Closes current form and opens the CountryViewAll form
        /// </summary>
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
        /// <summary>
        /// Configures the form for inserting a new country
        /// Enables Submit button for new records only
        /// </summary>
        private void SetFormForInsertMode()
        {
            _isEditMode = false;
            UpdateFormControls(
                readOnly: false,
                submitEnabled: true, 
                editEnabled: false,
                deleteEnabled: false
            );
            isCheckedcheckbox.Checked = true;
            EditBtn.Text = "Edit";
        }

        /// <summary>
        /// Configures the form for viewing country details (read-only)
        /// Disables Submit button for existing records
        /// </summary>
        private void SetFormForViewMode()
        {
            _isEditMode = false;
            UpdateFormControls(
                readOnly: true,
                submitEnabled: false,  
                editEnabled: true,
                deleteEnabled: true
            );
            EditBtn.Text = "Edit";
        }

        /// <summary>
        /// Configures the form for editing country details
        /// Keeps Submit button disabled during edits
        /// </summary>
        private void SetFormForEditMode()
        {
            _isEditMode = true;
            UpdateFormControls(
                readOnly: false,
                submitEnabled: false, 
                editEnabled: true,
                deleteEnabled: false
            );
            EditBtn.Text = "Save"; 
        }

        /// <summary>
        /// Updates the enabled/read-only state of form controls
        /// </summary>

        private void UpdateFormControls(bool readOnly, bool submitEnabled, bool editEnabled, bool deleteEnabled)
        {
            TxtCountryName.ReadOnly = readOnly;
            TxtCountryCode.ReadOnly = readOnly;
            isCheckedcheckbox.Enabled = !readOnly;

            SubmitBtn.Enabled = submitEnabled;
            EditBtn.Enabled = editEnabled;
            DeleteBtn.Enabled = deleteEnabled;
        }
        #endregion

        #region Database Operations
        /// <summary>
        /// Loads country data for editing when form opened with existing country ID
        /// </summary>
        private void LoadCountryForEditing()
        {
            try
            {
                using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    const string query = @"SELECT CountryName, CountryCode, IsActive 
                                         FROM Country 
                                         WHERE CountryId = @CountryId";

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

        /// <summary>
        /// Inserts a new country record into the database
        /// </summary>
        private void InsertCountry()
        {
            using (var conn = new SqlConnection(AppConnection.GetConnectionString()))
            using (var cmd = new SqlCommand("sp_InsertCountry", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                AddCommonParameters(cmd);
                cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@StatusCode", ACTIVE_STATUS);

                conn.Open();
                cmd.ExecuteNonQuery();

                ShowSuccessMessage("Country added successfully!");
                ResetFormControls();
            }
        }

        /// <summary>
        /// Updates an existing country record in the database
        /// </summary>
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

        /// <summary>
        /// Performs a soft delete of the country record
        /// </summary>
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
                    cmd.Parameters.AddWithValue("@StatusCode", ACTIVE_STATUS);

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

        /// <summary>
        /// Adds common parameters to SQL commands for country operations
        /// </summary>
        private void AddCommonParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@CountryName", TxtCountryName.Text.Trim());
            cmd.Parameters.AddWithValue("@CountryCode", TxtCountryCode.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Validates form inputs before database operations
        /// </summary>
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

            return true;
        }

        /// <summary>
        /// Resets form controls to their default state
        /// </summary>
        private void ResetFormControls()
        {
            TxtCountryName.Clear();
            TxtCountryCode.Clear();
            isCheckedcheckbox.Checked = true;
            TxtCountryName.Focus();
        }

        /// <summary>
        /// Shows the CountryViewAll form and closes current form
        /// </summary>
        private void ShowCountryViewAllForm()
        {
            this.Close();
            CountryViewAll viewAllForm = new CountryViewAll();
            viewAllForm.Show();
        }

        /// <summary>
        /// Closes current form and shows ViewAll form if needed
        /// </summary>
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

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private DialogResult ShowConfirmationDialog(string message, string title)
        {
            return MessageBox.Show(message, title,
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
        }
        #endregion

        #region Error Handling
      
        private void HandleUnexpectedError(Exception ex)
        {
            ShowErrorMessage($"An unexpected error occurred:\n{ex.Message}", "Error");
        }
        #endregion
    }
}