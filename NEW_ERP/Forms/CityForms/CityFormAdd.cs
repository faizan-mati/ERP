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

namespace NEW_ERP.Forms.CityForms
{
    public partial class CityFormAdd : Form
    {
        #region Private Fields
        private readonly int _cityId;
        private readonly bool _isFromViewAll;
        private bool _isEditMode = false;
        private bool _isFormEditable = false; 
        #endregion

        #region Constructor

        public CityFormAdd(int cityId, bool isFromViewAll)
        {
            try
            {
                InitializeComponent();
                _cityId = cityId;
                _isFromViewAll = isFromViewAll;
            }
            catch (Exception ex)
            {
                HandleError("Error initializing form", ex);
            }
        }
        #endregion

        #region Form Events

        private void CityForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCountryDropdown();

                if (_isFromViewAll && _cityId > 0)
                {
                    LoadCityData();
                    SetFormMode(FormMode.View);
                }
                else
                {
                    SetFormMode(FormMode.Insert);
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading form", ex);
            }
        }
        #endregion

        #region Form Mode Management
        /// <summary>
        /// Enum representing different form modes
        /// </summary>
        private enum FormMode
        {
            Insert,
            View,
            Edit
        }

        /// <summary>
        /// Sets the form mode and controls the state of buttons and form controls
        /// </summary>
        private void SetFormMode(FormMode mode)
        {
            try
            {
                switch (mode)
                {
                    case FormMode.Insert:
                        EnableFormControls(true);
                        _isFormEditable = true;

                        SubmitBtn.Enabled = (_cityId == 0 && !_isFromViewAll);
                        SubmitBtn.Text = "Submit";
                        EditBtn.Enabled = false;
                        EditBtn.Text = "Edit";
                        DeleteBtn.Enabled = false;

                        ClearForm();
                        _isEditMode = false;
                        break;

                    case FormMode.View:
                        EnableFormControls(false);
                        _isFormEditable = false;

                        SubmitBtn.Enabled = false;
                        EditBtn.Enabled = true;
                        EditBtn.Text = "Edit";
                        DeleteBtn.Enabled = true;

                        _isEditMode = false;
                        break;

                    case FormMode.Edit:
                        EnableFormControls(true);
                        _isFormEditable = true;

                        SubmitBtn.Enabled = false;
                        EditBtn.Enabled = true;
                        EditBtn.Text = "Save";
                        DeleteBtn.Enabled = false;

                        _isEditMode = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                HandleError("Error setting form mode", ex);
            }
        }

        /// <summary>
        /// Enables or disables form input controls
        /// </summary>
        private void EnableFormControls(bool enabled)
        {
            try
            {
                TxtCityName.Enabled = enabled;
                CountryCodeBox.Enabled = enabled;
                isCheckedcheckbox.Enabled = enabled;
            }
            catch (Exception ex)
            {
                HandleError("Error enabling/disabling form controls", ex);
            }
        }
        #endregion

        #region Data Loading Methods
        /// <summary>
        /// Loads country data into dropdown
        /// </summary>
        private void LoadCountryDropdown()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = "SELECT CountryID, CountryName FROM Country WHERE StatusCode='ACT' ORDER BY CountryName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        CountryCodeBox.DataSource = dt;
                        CountryCodeBox.DisplayMember = "CountryName";
                        CountryCodeBox.ValueMember = "CountryID";
                        CountryCodeBox.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading countries dropdown", ex);
            }
        }

        /// <summary>
        /// Loads existing city data for edit/view mode
        /// </summary>
        private void LoadCityData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = @"SELECT c.CityID, c.CityName, c.CountryID, c.IsActive, 
                                    co.CountryName 
                                    FROM City c 
                                    INNER JOIN Country co ON c.CountryID = co.CountryID 
                                    WHERE c.CityID = @CityID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CityID", _cityId);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TxtCityName.Text = reader["CityName"].ToString();
                                CountryCodeBox.SelectedValue = Convert.ToInt32(reader["CountryID"]);
                                isCheckedcheckbox.Checked = Convert.ToBoolean(reader["IsActive"]);
                            }
                            else
                            {
                                MessageBox.Show("City not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading city data", ex);
            }
        }
        #endregion

        #region Button Click Events
        /// <summary>
        /// Handles Submit button click - only works for new records (Insert mode)
        /// </summary>
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cityId == 0 && !_isFromViewAll)
                {
                    if (IsValidInput())
                    {
                        InsertCity();
                    }
                }
                else
                {
                    MessageBox.Show("Submit is only available for new records. Use Edit button for existing records.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                HandleError("Error in submit operation", ex);
            }
        }

        /// <summary>
        /// Handles Edit button click with dual functionality:
        /// 1st click: Makes form editable (Edit mode)
        /// 2nd click: Updates the record and returns to view mode
        /// </summary>
        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isFormEditable)
                {
                    SetFormMode(FormMode.Edit);
                }
                else
                {
                    if (IsValidInput())
                    {
                        UpdateCity();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error in edit operation", ex);
            }
        }

        /// <summary>
        /// Handles Delete button click with confirmation
        /// </summary>
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete the city '{TxtCityName.Text}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteCity();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error in delete operation", ex);
            }
        }

        /// <summary>
        /// Handles View All button click - navigates to city list
        /// </summary>
        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                CityViewAll nextForm = new CityViewAll();
                nextForm.Show();
            }
            catch (Exception ex)
            {
                HandleError("Error navigating to view all", ex);
            }
        }

        /// <summary>
        /// Handles Close button click - closes the form
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
        #endregion

        #region Database Operations
        /// <summary>
        /// Inserts new city record into database
        /// </summary>
        private void InsertCity()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertCity", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CityName", TxtCityName.Text.Trim());
                        cmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(CountryCodeBox.SelectedValue));
                        cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
                        cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@StatusCode", "ACT");

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("City inserted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearForm();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)
                {
                    MessageBox.Show(ex.Message, "Duplicate Entry",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    HandleError("Database error in insert operation", ex);
                }
            }
            catch (Exception ex)
            {
                HandleError("Error inserting city", ex);
            }
        }

        /// <summary>
        /// Updates existing city record in database
        /// </summary>
        private void UpdateCity()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateCity", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CityID", _cityId);
                        cmd.Parameters.AddWithValue("@CityName", TxtCityName.Text.Trim());
                        cmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(CountryCodeBox.SelectedValue));
                        cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
                        cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("City updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            SetFormMode(FormMode.View);
                        }
                        else
                        {
                            MessageBox.Show("No records were updated.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000) 
                {
                    MessageBox.Show(ex.Message, "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    HandleError("Database error in update operation", ex);
                }
            }
            catch (Exception ex)
            {
                HandleError("Error updating city", ex);
            }
        }

        /// <summary>
        /// Soft deletes city record by updating StatusCode to 'DEL'
        /// </summary>
        private void DeleteCity()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteCity", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CityID", _cityId);

                        // Execute the stored procedure
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("City deleted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Close();
                            CityViewAll viewAllForm = new CityViewAll();
                            viewAllForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("No records were deleted.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error deleting city", ex);
            }
        }
        #endregion

        #region Validation and Utility Methods
        /// <summary>
        /// Validates user input before database operations
        /// </summary>
        private bool IsValidInput()
        {
            try
            {
                if (CountryCodeBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select a country.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CountryCodeBox.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(TxtCityName.Text))
                {
                    MessageBox.Show("Please enter city name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtCityName.Focus();
                    return false;
                }

                if (TxtCityName.Text.Trim().Length < 2)
                {
                    MessageBox.Show("City name must be at least 2 characters long.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtCityName.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                HandleError("Error validating input", ex);
                return false;
            }
        }

        private void ClearForm()
        {
            try
            {
                TxtCityName.Clear();
                CountryCodeBox.SelectedIndex = -1;
                isCheckedcheckbox.Checked = true;
                TxtCityName.Focus();
            }
            catch (Exception ex)
            {
                HandleError("Error clearing form", ex);
            }
        }

        private void HandleError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}