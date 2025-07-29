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

namespace NEW_ERP.Forms.CustomerMaster
{
    public partial class CustomerFormAdd : Form
    {
        #region Private Fields
        private readonly int _customerId;
        private readonly bool _isFromViewAll;
        private bool _isEditMode = false; // Track if form is in edit mode
        #endregion

        #region Constructor
        public CustomerFormAdd(int customerId, bool isFromViewAll)
        {
            InitializeComponent();
            _customerId = customerId;
            _isFromViewAll = isFromViewAll;
        }
        #endregion

        #region Form Load Event
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            // Load dropdown data
            LoadCustomerTypes();
            LoadCountries();

            // Configure form based on mode
            ConfigureFormMode();

            // If editing existing customer, load data
            if (_customerId > 0)
            {
                LoadCustomerData();
            }
        }
        #endregion

        #region Form Configuration Methods
        /// <summary>
        /// Configure form controls based on whether it's add or edit mode
        /// </summary>
        private void ConfigureFormMode()
        {
            if (_customerId == 0 && !_isFromViewAll)
            {
                // Add new customer mode
                SetFormForAddMode();
            }
            else if (_customerId > 0 && _isFromViewAll)
            {
                // View/Edit existing customer mode
                SetFormForViewMode();
            }
        }

        /// <summary>
        /// Set form for adding new customer
        /// </summary>
        private void SetFormForAddMode()
        {
            SubmitBtn.Enabled = true;
            SubmitBtn.Text = "Submit";
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;
            SetFormControlsEnabled(true);
        }

        /// <summary>
        /// Set form for viewing existing customer (read-only)
        /// </summary>
        private void SetFormForViewMode()
        {
            SubmitBtn.Enabled = false;
            EditBtn.Enabled = true;
            EditBtn.Text = "Edit";
            DeleteBtn.Enabled = true;
            SetFormControlsEnabled(false);
            _isEditMode = false;
        }

        /// <summary>
        /// Set form for editing existing customer
        /// </summary>
        private void SetFormForEditMode()
        {
            SubmitBtn.Enabled = false;
            EditBtn.Text = "Save";
            DeleteBtn.Enabled = false;
            SetFormControlsEnabled(true);
            _isEditMode = true;
        }

        /// <summary>
        /// Enable or disable form controls
        /// </summary>
        /// <param name="enabled">True to enable controls, false to disable</param>
        private void SetFormControlsEnabled(bool enabled)
        {
            // Text boxes
            txtCustomerCode.Enabled = enabled;
            txtCustomerName.Enabled = enabled;
            txtContactPerson.Enabled = enabled;
            txtMobileNo.Enabled = enabled;
            txtEmail.Enabled = enabled;
            txtWhatsapp.Enabled = enabled;
            txtState.Enabled = enabled;
            txtZipCode.Enabled = enabled;
            txtAddress.Enabled = enabled;
            txtGSTNo.Enabled = enabled;
            txtNTN.Enabled = enabled;

            // Dropdown boxes
            CustomerTypeBox.Enabled = enabled;
            CountryBox.Enabled = enabled;
            CityBox.Enabled = enabled;

            // Checkbox
            isCheckedcheckbox.Enabled = enabled;
        }
        #endregion

        #region Data Loading Methods
        /// <summary>
        /// Load customer types into dropdown
        /// </summary>
        private void LoadCustomerTypes()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT CustomertypeId, CustomerTypeName FROM CustomerTypeMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CustomerTypeBox.DataSource = dt;
                    CustomerTypeBox.DisplayMember = "CustomerTypeName";
                    CustomerTypeBox.ValueMember = "CustomertypeId";
                    CustomerTypeBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Load countries into dropdown
        /// </summary>
        private void LoadCountries()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT CountryID, CountryName FROM Country";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CountryBox.DataSource = dt;
                    CountryBox.DisplayMember = "CountryName";
                    CountryBox.ValueMember = "CountryID";
                    CountryBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Load cities based on selected country
        /// </summary>
        /// <param name="countryId">Selected country ID</param>
        private void LoadCities(int countryId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT CityID, CityName FROM City WHERE CountryID = @CountryID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CountryID", countryId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CityBox.DataSource = dt;
                    CityBox.DisplayMember = "CityName";
                    CityBox.ValueMember = "CityID";
                    CityBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Load existing customer data for editing
        /// </summary>
        private void LoadCustomerData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT c.CustomerCode, c.CustomerName, c.ContactPerson, c.MobileNo, 
                                       c.Email, c.WhatsAppNo, c.State, c.ZipCode, c.Address, 
                                       c.GSTNo, c.NTN, c.IsActive, c.CustomerTypeId, c.Country, c.City
                                FROM CustomerMaster c 
                                WHERE c.CustomerId = @CustomerId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", _customerId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate form controls with data
                            txtCustomerCode.Text = reader["CustomerCode"].ToString();
                            txtCustomerName.Text = reader["CustomerName"].ToString();
                            txtContactPerson.Text = reader["ContactPerson"].ToString();
                            txtMobileNo.Text = reader["MobileNo"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            txtWhatsapp.Text = reader["WhatsAppNo"].ToString();
                            txtState.Text = reader["State"].ToString();
                            txtZipCode.Text = reader["ZipCode"].ToString();
                            txtAddress.Text = reader["Address"].ToString();
                            txtGSTNo.Text = reader["GSTNo"].ToString();
                            txtNTN.Text = reader["NTN"].ToString();
                            isCheckedcheckbox.Checked = Convert.ToBoolean(reader["IsActive"]);

                            // Set dropdown selections
                            CustomerTypeBox.SelectedValue = reader["CustomerTypeId"];
                            CountryBox.SelectedValue = reader["Country"];

                            // Load cities for selected country and set city
                            int countryId = Convert.ToInt32(reader["Country"]);
                            LoadCities(countryId);
                            CityBox.SelectedValue = reader["City"];
                        }
                    }
                }
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handle country selection change to load cities
        /// </summary>
        private void CountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountryBox.SelectedValue == null || CountryBox.SelectedIndex == -1)
                return;

            if (int.TryParse(CountryBox.SelectedValue.ToString(), out int selectedCountryId))
            {
                LoadCities(selectedCountryId);
            }
        }

        /// <summary>
        /// Handle submit button click for inserting new customer
        /// </summary>
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (IsValidationPassed())
            {
                InsertCustomer();
            }
        }

        /// <summary>
        /// Handle edit button click - toggle between edit and save modes
        /// </summary>
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                // Switch to edit mode
                SetFormForEditMode();
            }
            else
            {
                // Save changes
                if (IsValidationPassed())
                {
                    UpdateCustomer();
                }
            }
        }

        /// <summary>
        /// Handle delete button click
        /// </summary>
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this customer?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DeleteCustomer();
            }
        }

        /// <summary>
        /// Handle close button click
        /// </summary>
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handle view all button click
        /// </summary>
        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            CustomerViewAll NextForm = new CustomerViewAll();
            NextForm.Show();
        }
        #endregion

        #region Database Operations
        /// <summary>
        /// Insert new customer record
        /// </summary>
        private void InsertCustomer()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertCustomerMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@CustomerCode", txtCustomerCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@WhatsAppNo", txtWhatsapp.Text.Trim());
                        cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
                        cmd.Parameters.AddWithValue("@ZipCode", txtZipCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@GSTNo", txtGSTNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@NTN", txtNTN.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
                        cmd.Parameters.AddWithValue("@CustomerTypeId", Convert.ToInt32(CustomerTypeBox.SelectedValue));
                        cmd.Parameters.AddWithValue("@Country", Convert.ToInt32(CountryBox.SelectedValue));
                        cmd.Parameters.AddWithValue("@City", Convert.ToInt32(CityBox.SelectedValue));
                        cmd.Parameters.AddWithValue("@CreditLimit", 0);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
                        cmd.Parameters.AddWithValue("@StatusCode", "ACT");

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Customer inserted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetFormControls();
                        }
                        else
                        {
                            MessageBox.Show("Insertion failed.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database Error:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Update existing customer record
        /// </summary>
        private void UpdateCustomer()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add customer ID parameter for update
                        cmd.Parameters.AddWithValue("@CustomerId", _customerId);

                        // Add other parameters
                        AddCustomerParameters(cmd);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Customer updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Switch back to view mode
                            SetFormForViewMode();
                        }
                        else
                        {
                            MessageBox.Show("Update failed.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database Error:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Delete customer record
        /// </summary>
        private void DeleteCustomer()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_DeleteCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerId", _customerId);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Customer deleted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Close(); // Close form after successful deletion
                        }
                        else
                        {
                            MessageBox.Show("Deletion failed.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database Error:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Add common parameters for insert and update operations
        /// </summary>
        /// <param name="cmd">SQL command object</param>
        private void AddCustomerParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@CustomerCode", txtCustomerCode.Text.Trim());
            cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text.Trim());
            cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
            cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@WhatsAppNo", txtWhatsapp.Text.Trim());
            cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
            cmd.Parameters.AddWithValue("@ZipCode", txtZipCode.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@GSTNo", txtGSTNo.Text.Trim());
            cmd.Parameters.AddWithValue("@NTN", txtNTN.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
            cmd.Parameters.AddWithValue("@CustomerTypeId", Convert.ToInt32(CustomerTypeBox.SelectedValue));
            cmd.Parameters.AddWithValue("@Country", Convert.ToInt32(CountryBox.SelectedValue));
            cmd.Parameters.AddWithValue("@City", Convert.ToInt32(CityBox.SelectedValue));
            cmd.Parameters.AddWithValue("@CreditLimit", 0);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
        }
        #endregion

        #region Validation and Utility Methods
        /// <summary>
        /// Validate form inputs
        /// </summary>
        /// <returns>True if validation passes, false otherwise</returns>
        private bool IsValidationPassed()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerCode.Text) ||
                string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtNTN.Text) ||
                string.IsNullOrWhiteSpace(txtGSTNo.Text) ||
                CustomerTypeBox.SelectedIndex == -1 ||
                CountryBox.SelectedIndex == -1 ||
                CityBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reset all form controls to default state
        /// </summary>
        private void ResetFormControls()
        {
            // Clear text boxes
            txtCustomerCode.Clear();
            txtCustomerName.Clear();
            txtContactPerson.Clear();
            txtMobileNo.Clear();
            txtEmail.Clear();
            txtWhatsapp.Clear();
            txtState.Clear();
            txtZipCode.Clear();
            txtAddress.Clear();
            txtGSTNo.Clear();
            txtNTN.Clear();

            // Reset dropdowns
            CustomerTypeBox.SelectedIndex = -1;
            CountryBox.SelectedIndex = -1;
            CityBox.DataSource = null;

            // Reset checkbox
            isCheckedcheckbox.Checked = false;
        }
        #endregion

        #region Unused Event Handler
        private void label14_Click(object sender, EventArgs e)
        {
            // This event handler appears to be unused
        }
        #endregion
    }
}