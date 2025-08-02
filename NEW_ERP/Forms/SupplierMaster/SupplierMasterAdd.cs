using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.SupplierMaster
{
    public partial class SupplierMasterAdd : Form
    {
        private readonly string _supplierCode;
        private readonly bool _isFromViewAll;
        private bool _isEditMode = false;

        public SupplierMasterAdd(string supplierCode, bool isFromViewAll)
        {
            InitializeComponent();
            _supplierCode = supplierCode;
            _isFromViewAll = isFromViewAll;
        }

        //======================================= FORM LOAD EVENT =======================================
        private void SupplierMasterForm_Load(object sender, EventArgs e)
        {
            SupplierTypeShow();

            ConfigureFormState();

            if (!string.IsNullOrEmpty(_supplierCode))
            {
                LoadSupplierData();
            }
            else
            {
                GenerateNextSupplierCode();
                txtSupplierCode.ReadOnly = true;
            }
        }

        //======================================= GENERATE NEXT SUPPLIER CODE =======================================
        private void GenerateNextSupplierCode()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT ISNULL(MAX(CAST(SUBSTRING(SupplierCode, 4, LEN(SupplierCode) - 3) AS INT)), 0) as MaxCode
                                FROM SupplierMaster 
                                WHERE SupplierCode LIKE 'SUP%' 
                                AND ISNUMERIC(SUBSTRING(SupplierCode, 4, LEN(SupplierCode) - 3)) = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        int maxCode = Convert.ToInt32(result);
                        int nextCode = maxCode + 1;

                        txtSupplierCode.Text = "SUP" + nextCode.ToString("D3");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error generating supplier code: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        txtSupplierCode.Text = "SUP" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    }
                }
            }
        }

        //======================================= CONFIGURE FORM STATE =======================================
        private void ConfigureFormState()
        {
            if (!string.IsNullOrEmpty(_supplierCode) && _isFromViewAll)
            {
                SetFormReadOnly(true);
                SubmitBtn.Enabled = false;
                EditBtn.Enabled = true;
                DeleteBtn.Enabled = true;
                EditBtn.Text = "Edit";
            }
            else
            {
                SetFormReadOnly(false);
                SubmitBtn.Enabled = true;
                EditBtn.Enabled = false;
                DeleteBtn.Enabled = false;
                EditBtn.Text = "Edit";
            }
        }

        //======================================= SET FORM READ ONLY STATE =======================================
        private void SetFormReadOnly(bool readOnly)
        {
            if (!string.IsNullOrEmpty(_supplierCode))
            {
                txtSupplierCode.ReadOnly = readOnly;
            }

            txtDescription.ReadOnly = readOnly;
            txtContact.ReadOnly = readOnly;
            txtShortName.ReadOnly = readOnly;
            txtAddress.ReadOnly = readOnly;
            txtShortAddress.ReadOnly = readOnly;
            txtPhoneNo.ReadOnly = readOnly;
            txtPhoneNo2.ReadOnly = readOnly;
            txtFaxNo.ReadOnly = readOnly;
            txtMobileNo.ReadOnly = readOnly;
            txtEmail.ReadOnly = readOnly;
            txtEmail2.ReadOnly = readOnly;
            txtURL.ReadOnly = readOnly;
            txtNtnNo.ReadOnly = readOnly;
            txtSaleTax.ReadOnly = readOnly;
            txtReference.ReadOnly = readOnly;
            txtRemarks.ReadOnly = readOnly;

            SupplierTypeBox.Enabled = !readOnly;
        }

        //======================================= LOAD SUPPLIER DATA =======================================
        private void LoadSupplierData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT s.SupplierCode, s.Description, s.SupplierTypeCode, s.ContactPerson, 
                                s.ShortName, s.Address, s.ShortAddress, s.PhoneNo, s.PhoneNo2, s.FaxNo, 
                                s.MobileNo, s.EMail, s.EMail2, s.Url, s.NtnNo, s.SaleTaxRegNo, 
                                s.Reference, s.StatusCode, s.Remarks,
                                st.Description as SupplierTypeDescription,
                                stat.StatusDescription
                                FROM SupplierMaster s
                                LEFT JOIN SupplierType st ON s.SupplierTypeCode = st.SupplierTypeCode
                                LEFT JOIN Status stat ON s.StatusCode = stat.StatusDescription
                                WHERE s.SupplierCode = @SupplierCode";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierCode", _supplierCode);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtSupplierCode.Text = reader["SupplierCode"].ToString();
                                txtDescription.Text = reader["Description"].ToString();
                                txtContact.Text = reader["ContactPerson"].ToString();
                                txtShortName.Text = reader["ShortName"].ToString();
                                txtAddress.Text = reader["Address"].ToString();
                                txtShortAddress.Text = reader["ShortAddress"].ToString();
                                txtPhoneNo.Text = reader["PhoneNo"].ToString();
                                txtPhoneNo2.Text = reader["PhoneNo2"].ToString();
                                txtFaxNo.Text = reader["FaxNo"].ToString();
                                txtMobileNo.Text = reader["MobileNo"].ToString();
                                txtEmail.Text = reader["EMail"].ToString();
                                txtEmail2.Text = reader["EMail2"].ToString();
                                txtURL.Text = reader["Url"].ToString();
                                txtNtnNo.Text = reader["NtnNo"].ToString();
                                txtSaleTax.Text = reader["SaleTaxRegNo"].ToString();
                                txtReference.Text = reader["Reference"].ToString();
                                txtRemarks.Text = reader["Remarks"].ToString();

                                SupplierTypeBox.SelectedValue = reader["SupplierTypeCode"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading supplier data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //======================================= SUPPLIER TYPE SHOW =======================================
        protected void SupplierTypeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT Description, SupplierTypeCode FROM SupplierType ORDER BY Description";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SupplierTypeBox.DataSource = dt;
                    SupplierTypeBox.DisplayMember = "Description";
                    SupplierTypeBox.ValueMember = "SupplierTypeCode";
                    SupplierTypeBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= SUBMIT BUTTON CLICK (INSERT) =======================================
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertSupplierMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SupplierCode", txtSupplierCode.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@SupplierTypeCode", SupplierTypeBox.SelectedValue ?? SupplierTypeBox.Text);
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContact.Text);
                        cmd.Parameters.AddWithValue("@ShortName", txtShortName.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@ShortAddress", txtShortAddress.Text);
                        cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                        cmd.Parameters.AddWithValue("@PhoneNo2", txtPhoneNo2.Text);
                        cmd.Parameters.AddWithValue("@FaxNo", txtFaxNo.Text);
                        cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                        cmd.Parameters.AddWithValue("@EMail", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@EMail2", txtEmail2.Text);
                        cmd.Parameters.AddWithValue("@Url", txtURL.Text);
                        cmd.Parameters.AddWithValue("@NtnNo", txtNtnNo.Text);
                        cmd.Parameters.AddWithValue("@SaleTaxRegNo", txtSaleTax.Text);
                        cmd.Parameters.AddWithValue("@Reference", txtReference.Text);
                        cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@StatusCode", "ACT");
                        cmd.Parameters.AddWithValue("@UserCode", "00123");
                        cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Supplier added successfully with code: " + txtSupplierCode.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestFormControler();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        //======================================= EDIT BUTTON CLICK =======================================
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                _isEditMode = true;
                SetFormReadOnly(false);
                EditBtn.Text = "Save";
                DeleteBtn.Enabled = false;
                SubmitBtn.Enabled = false;
            }
            else
            {
                if (isValidation())
                {
                    UpdateSupplierData();
                }
            }
        }

        //======================================= UPDATE SUPPLIER DATA =======================================
        private void UpdateSupplierData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateSupplierMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierCode", _supplierCode);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@SupplierTypeCode", SupplierTypeBox.SelectedValue ?? SupplierTypeBox.Text);
                    cmd.Parameters.AddWithValue("@ContactPerson", txtContact.Text);
                    cmd.Parameters.AddWithValue("@ShortName", txtShortName.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@ShortAddress", txtShortAddress.Text);
                    cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@PhoneNo2", txtPhoneNo2.Text);
                    cmd.Parameters.AddWithValue("@FaxNo", txtFaxNo.Text);
                    cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                    cmd.Parameters.AddWithValue("@EMail", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@EMail2", txtEmail2.Text);
                    cmd.Parameters.AddWithValue("@Url", txtURL.Text);
                    cmd.Parameters.AddWithValue("@NtnNo", txtNtnNo.Text);
                    cmd.Parameters.AddWithValue("@SaleTaxRegNo", txtSaleTax.Text);
                    cmd.Parameters.AddWithValue("@Reference", txtReference.Text);
                    cmd.Parameters.AddWithValue("@UserCode", "00123");
                    cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            _isEditMode = false;
                            SetFormReadOnly(true);
                            EditBtn.Text = "Edit";
                            DeleteBtn.Enabled = true;
                            SubmitBtn.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("No records were updated. Please check the supplier code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //======================================= DELETE BUTTON CLICK =======================================
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this supplier?",
                                                  "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteSupplierMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SupplierCode", _supplierCode);

                        try
                        {
                            conn.Open();

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string message = reader["Message"].ToString();
                                    int success = Convert.ToInt32(reader["Success"]);

                                    if (success == 1)
                                    {
                                        MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to delete supplier: " + message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No response from server. Please try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error deleting supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        //======================================= VALIDATION CHECK =======================================
        private bool isValidation()
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text) ||
                string.IsNullOrWhiteSpace(txtSupplierCode.Text))
            {
                MessageBox.Show("Please Fill All the Required Fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //======================================= RESET FORM CONTROLS =======================================
        private void RestFormControler()
        {
            txtDescription.Clear();
            txtContact.Clear();
            txtShortName.Clear();
            txtAddress.Clear();
            txtShortAddress.Clear();
            txtPhoneNo.Clear();
            txtPhoneNo2.Clear();
            txtFaxNo.Clear();
            txtMobileNo.Clear();
            txtEmail.Clear();
            txtEmail2.Clear();
            txtURL.Clear();
            txtNtnNo.Clear();
            txtSaleTax.Clear();
            txtReference.Clear();
            txtRemarks.Clear();

            SupplierTypeBox.SelectedIndex = -1;

            GenerateNextSupplierCode();

            txtDescription.Focus();
        }

        //======================================= CLOSE BUTTON CLICK =======================================
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //======================================= VIEW ALL BUTTON CLICK =======================================
        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            SupplierViewAll NextForm = new SupplierViewAll();
            NextForm.Show();
        }
    }
}