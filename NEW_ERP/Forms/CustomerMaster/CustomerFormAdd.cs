using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NEW_ERP.Forms.CustomerMaster
{
    public partial class CustomerFormAdd : Form
    {
        #region Private Fields
        private readonly int _customerId;
        private readonly bool _isFromViewAll;
        private bool _isEditMode = false;
        private bool _isEditing = false;
        #endregion

        #region Constructor

        public CustomerFormAdd(int customerId, bool isFromViewAll)
        {
            InitializeComponent();
            _customerId = customerId;
            _isFromViewAll = isFromViewAll;
        }
        #endregion

        #region Form Events

        //======================================= Load Form Data =======================================
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadCustomerTypes();
            LoadCountries();
            ShowStatusCode();

            if (_isFromViewAll && _customerId > 0)
            {
                SetEditMode();
                LoadCustomerData(_customerId);
            }
            else
            {
                SetInsertMode();
                GenerateCustomerCode();
            }
        }
        #endregion

        #region Form Mode Management
        //======================================= Set Form to Insert Mode =======================================
        private void SetInsertMode()
        {
            _isEditMode = false;
            _isEditing = false;

            EnableFormControls(true);

            SubmitBtn.Enabled = true;
            SubmitBtn.Text = "Submit";
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;
            ViewAllBtn.Enabled = true;
        }

        //======================================= Set Form to Edit Mode =======================================
        private void SetEditMode()
        {
            _isEditMode = true;
            _isEditing = false;

            EnableFormControls(false);

            SubmitBtn.Enabled = false;
            SubmitBtn.Text = "Submit";
            EditBtn.Enabled = true;
            EditBtn.Text = "Edit";
            DeleteBtn.Enabled = true;
            ViewAllBtn.Enabled = true;
        }

        //======================================= Enable/Disable Form Controls =======================================
        private void EnableFormControls(bool enabled)
        {
            txtCustomerCode.ReadOnly = !enabled;
            txtCustomerName.ReadOnly = !enabled;
            txtContactPerson.ReadOnly = !enabled;
            txtMobileNo.ReadOnly = !enabled;
            txtEmail.ReadOnly = !enabled;
            txtWhatsapp.ReadOnly = !enabled;
            txtState.ReadOnly = !enabled;
            txtZipCode.ReadOnly = !enabled;
            txtAddress.ReadOnly = !enabled;
            txtGSTNo.ReadOnly = !enabled;
            txtNTN.ReadOnly = !enabled;

            CustomerTypeBox.Enabled = enabled;
            CountryBox.Enabled = enabled;
            CityBox.Enabled = enabled;

            isCheckedcheckbox.Enabled = enabled;

            StatusCodeBox.Enabled = enabled;
        }
        #endregion

        #region Data Loading Methods
        //======================================= Load Customer Types =======================================
        private void LoadCustomerTypes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = "SELECT CustomertypeId, CustomerTypeName FROM CustomerTypeMaster";

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
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer types: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Load Countries =======================================
        private void LoadCountries()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = "SELECT CountryID, CountryName FROM Country WHERE StatusCode = 2";

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
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading countries: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Load Cities for Selected Country =======================================
        private void LoadCities(int countryId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = "SELECT CityID, CityName FROM City WHERE CountryID = @CountryID AND StatusCode = 2";

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
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cities: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Load Customer Data =======================================
        private void LoadCustomerData(int customerId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = @"SELECT 
    cm.CustomerCode, 
    cm.CustomerName, 
    cm.ContactPerson, 
    cm.MobileNo, 
    cm.Email, 
    cm.WhatsAppNo, 
    cm.State, 
    cm.ZipCode, 
    cm.Address, 
    cm.GSTNo, 
    cm.NTN, 
    cm.IsActive, 
    cm.CustomerTypeID, 
    cm.Country, 
    cm.City, 
    cm.CreditLimit, 
    s.StatusCode     
FROM CustomerMaster cm
LEFT JOIN Status s ON cm.StatusCode = s.StatusId 
WHERE cm.CustomerID = @CustomerID AND cm.StatusCode = 1
";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
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
                                StatusCodeBox.Text = reader["StatusCode"].ToString();

                                isCheckedcheckbox.Checked = Convert.ToBoolean(reader["IsActive"]);

                                CustomerTypeBox.SelectedValue = reader["CustomerTypeID"];
                                CountryBox.Text = reader["Country"].ToString();
                                CityBox.Text = reader["City"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Customer not found.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Generate Customer Code =======================================
        private void GenerateCustomerCode()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = @"SELECT ISNULL(MAX(
                            CASE 
                                WHEN CustomerCode LIKE 'CUST%' AND ISNUMERIC(SUBSTRING(CustomerCode, 5, LEN(CustomerCode))) = 1 
                                THEN CAST(SUBSTRING(CustomerCode, 5, LEN(CustomerCode)) AS INT)
                                ELSE 0
                            END), 0) + 1 
                        FROM CustomerMaster";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        int nextNumber = (int)cmd.ExecuteScalar();
                        txtCustomerCode.Text = "CUST" + nextNumber.ToString("D3");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating customer code: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Fallback to simple increment if there's an error
                txtCustomerCode.Text = "CUST001";
            }
        }

        #endregion

        #region Control Event Handlers
        //======================================= Country Selection Changed =======================================
        private void CountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountryBox.SelectedValue == null || CountryBox.SelectedIndex == -1)
                return;

            if (int.TryParse(CountryBox.SelectedValue.ToString(), out int selectedCountryId))
            {
                LoadCities(selectedCountryId);
            }
        }

        //======================================= Submit Button Click =======================================
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (!IsValidationPassed())
                return;

            if (!_isEditMode)
            {
                InsertCustomer();
            }
        }

        //======================================= Edit Button Click =======================================
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (!_isEditing)
            {
                EnableFormControls(true);
                _isEditing = true;
                SubmitBtn.Enabled = false;
                EditBtn.Text = "Save";
                DeleteBtn.Enabled = false;
            }
            else
            {
                if (IsValidationPassed())
                {
                    UpdateCustomer();
                }

                SetEditMode();
                LoadCustomerData(_customerId);
            }
        }

        //======================================= Delete Button Click =======================================
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

        //======================================= View All Button Click =======================================
        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            CustomerViewAll viewAllForm = new CustomerViewAll();
            viewAllForm.Show();
        }

        //======================================= Close Button Click =======================================
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Database Operations
        //======================================= Insert New Customer =======================================
        private void InsertCustomer()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertCustomerMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CustomerCode", txtCustomerCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@WhatsAppNo", txtWhatsapp.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@City", CityBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
                        cmd.Parameters.AddWithValue("@Country", CountryBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@ZipCode", txtZipCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@CustomerTypeID", Convert.ToInt32(CustomerTypeBox.SelectedValue));
                        cmd.Parameters.AddWithValue("@GSTNo", txtGSTNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@NTN", txtNTN.Text.Trim());
                        cmd.Parameters.AddWithValue("@CreditLimit", 0);
                        cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
                        cmd.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));
                        cmd.Parameters.AddWithValue("@CreatedBy", "Admin");

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Customer inserted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetFormControls();
                            GenerateCustomerCode();
                        }
                        else
                        {
                            MessageBox.Show("Customer insertion failed.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Update Customer =======================================
        private void UpdateCustomer()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CustomerID", _customerId);
                        cmd.Parameters.AddWithValue("@CustomerCode", txtCustomerCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@WhatsAppNo", txtWhatsapp.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@City", CityBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
                        cmd.Parameters.AddWithValue("@Country", CountryBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@ZipCode", txtZipCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@CustomerTypeID", Convert.ToInt32(CustomerTypeBox.SelectedValue));
                        cmd.Parameters.AddWithValue("@GSTNo", txtGSTNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@NTN", txtNTN.Text.Trim());
                        cmd.Parameters.AddWithValue("@CreditLimit", 0);
                        cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
                        cmd.Parameters.AddWithValue("@StatusCode", GetNullableValue(StatusCodeBox));
                        cmd.Parameters.AddWithValue("@Updatedby", "Admin");

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Customer updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetEditMode();
                        }
                        else
                        {
                            MessageBox.Show("Customer update failed.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //======================================= Delete Customer =======================================
        private void DeleteCustomer()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerID", _customerId);
                        cmd.Parameters.AddWithValue("@UpdatedBy", "Admin");

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Customer deleted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Customer deletion failed.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Validation Methods
        //======================================= Validate Form Inputs =======================================
        private bool IsValidationPassed()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerCode.Text))
            {
                MessageBox.Show("Please enter a Customer Code.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Please enter a Customer Name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerName.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGSTNo.Text))
            {
                MessageBox.Show("Please enter GST No.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGSTNo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNTN.Text))
            {
                MessageBox.Show("Please enter NTN.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNTN.Focus();
                return false;
            }

            if (CustomerTypeBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Customer Type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CustomerTypeBox.Focus();
                return false;
            }

            if (CountryBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Country.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CountryBox.Focus();
                return false;
            }

            if (CityBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a City.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CityBox.Focus();
                return false;
            }

            if (StatusCodeBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Status Code", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StatusCodeBox.Focus();
                return false;
            }

            if (!_isEditMode || _customerId == 0)
            {
                if (IsCustomerCodeExists(txtCustomerCode.Text.Trim()))
                {
                    MessageBox.Show("Customer Code already exists. Please enter a unique code.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerCode.Focus();
                    return false;
                }

                if (IsGSTNoExists(txtGSTNo.Text.Trim()))
                {
                    MessageBox.Show("GST No. already exists. Please enter a unique GST No.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGSTNo.Focus();
                    return false;
                }

                if (IsNTNExists(txtNTN.Text.Trim()))
                {
                    MessageBox.Show("NTN already exists. Please enter a unique NTN.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNTN.Focus();
                    return false;
                }
            }

            return true;
        }

        //======================================= Validate Email Format =======================================
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][_\w]*[0-9a-z]*\.)+[a-z0-9]{2,20}))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        //======================================= Check if Customer Code Exists =======================================
        private bool IsCustomerCodeExists(string customerCode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = "SELECT COUNT(1) FROM CustomerMaster WHERE CustomerCode = @CustomerCode AND StatusCode = 'ACT'";

                    if (_isEditMode && _customerId > 0)
                    {
                        query += " AND CustomerID != @CustomerID";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerCode", customerCode);

                        if (_isEditMode && _customerId > 0)
                        {
                            cmd.Parameters.AddWithValue("@CustomerID", _customerId);
                        }

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking customer code: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //======================================= Check if GST No Exists =======================================
        private bool IsGSTNoExists(string gstNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = "SELECT COUNT(1) FROM CustomerMaster WHERE GSTNo = @GSTNo AND StatusCode = 'ACT'";

                    if (_isEditMode && _customerId > 0)
                    {
                        query += " AND CustomerID != @CustomerID";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@GSTNo", gstNo);

                        if (_isEditMode && _customerId > 0)
                        {
                            cmd.Parameters.AddWithValue("@CustomerID", _customerId);
                        }

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking GST No: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //======================================= Check if NTN Exists =======================================
        private bool IsNTNExists(string ntn)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = "SELECT COUNT(1) FROM CustomerMaster WHERE NTN = @NTN AND StatusCode = 'ACT'";

                    if (_isEditMode && _customerId > 0)
                    {
                        query += " AND CustomerID != @CustomerID";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NTN", ntn);

                        if (_isEditMode && _customerId > 0)
                        {
                            cmd.Parameters.AddWithValue("@CustomerID", _customerId);
                        }

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking NTN: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region Utility Methods
        //======================================= Reset Form Controls =======================================
        private void ResetFormControls()
        {
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

            StatusCodeBox.SelectedIndex = -1;
            CustomerTypeBox.SelectedIndex = -1;
            CountryBox.SelectedIndex = -1;
            CityBox.DataSource = null;

            isCheckedcheckbox.Checked = false;
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