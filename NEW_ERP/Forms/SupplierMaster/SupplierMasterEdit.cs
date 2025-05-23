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

namespace NEW_ERP.Forms.SupplierMaster
{
    public partial class SupplierMasterEdit : Form
    {
        public SupplierMasterEdit()
        {
            InitializeComponent();
        }

        private void SupplierMasterEdit_Load(object sender, EventArgs e)
        {
            SupplierCodeShow();
            SatutsCodeShow();
            SupplierTypeCodeShow();
        }


        //======================================= SUPPLIER CODE SHOW =======================================

        protected void SupplierCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT DISTINCT SupplierCode FROM SupplierMaster;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SupplierCodeBox.DataSource = dt;
                    SupplierCodeBox.DisplayMember = "SupplierCode";
                    SupplierCodeBox.ValueMember = "SupplierCode";
                    SupplierCodeBox.SelectedIndex = -1;
                }
            }
        }


        //======================================= STATUS CODE SHOW =======================================

        protected void SatutsCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT StatusCode, StatusDescription FROM Status";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    StatusCodeBox.DataSource = dt;
                    StatusCodeBox.DisplayMember = "StatusDescription"; 
                    StatusCodeBox.ValueMember = "StatusCode";          
                    StatusCodeBox.SelectedIndex = -1;
                }
            }
        }


        //======================================= SUPPLIER TYPE CODE SHOW =======================================

        protected void SupplierTypeCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT SupplierTypeCode, Description FROM SupplierType";

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


        //======================================= SUPPLIER CODE CHANGE SHOW =======================================

        private void SupplierCodeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SupplierCodeBox.SelectedValue != null)
            {
                string selectedCode = SupplierCodeBox.SelectedValue.ToString();
                LoadDetails(selectedCode);
            }
        }

        private void LoadDetails(string selectedCode)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"
SELECT SupplierCode , Description, SupplierTypeCode, ContactPerson, ShortName, Address, ShortAddress, PhoneNo, PhoneNo2
 , FaxNo, MobileNo, EMail, EMail2, Url, NtnNo, SaleTaxRegNo, Reference, SystemDate, StatusCode, UserCode, Remarks
  FROM SupplierMaster
    WHERE SupplierCode = @SupplyCode";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplyCode", selectedCode);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtSupplierCode.Text = reader["SupplierCode"].ToString();
                        txtShortName.Text = reader["ShortName"].ToString();
                        txtDescription.Text = reader["Description"].ToString();

                        txtContact.Text = reader["ContactPerson"].ToString();
                        txtPhoneNo.Text = reader["PhoneNo"].ToString();
                        txtPhoneNo2.Text = reader["PhoneNo2"].ToString();

                        txtAddress.Text = reader["Address"].ToString();
                        txtShortAddress.Text = reader["ShortAddress"].ToString();
                        txtNtnNo.Text = reader["NtnNo"].ToString();

                        txtFaxNo.Text = reader["FaxNo"].ToString();
                        txtSaleTax.Text = reader["SaleTaxRegNo"].ToString();
                        txtURL.Text = reader["Url"].ToString();

                        txtRemarks.Text = reader["Remarks"].ToString();
                        txtEmail.Text = reader["EMail"].ToString();
                        txtEmail2.Text = reader["EMail2"].ToString();
                        txtMobileNo.Text = reader["MobileNo"].ToString();

                        StatusCodeBox.SelectedValue = reader["StatusCode"].ToString();
                        SupplierTypeBox.SelectedValue = reader["SupplierTypeCode"].ToString();

                        txtSupplierCode.ReadOnly = false;
                        txtShortName.ReadOnly = true;
                        txtDescription.ReadOnly = true;
                        txtContact.ReadOnly = true;
                        txtPhoneNo.ReadOnly = true;
                        txtPhoneNo2.ReadOnly = true;
                        txtAddress.ReadOnly = true;
                        txtShortAddress.ReadOnly = true;
                        txtNtnNo.ReadOnly = true;
                        txtFaxNo.ReadOnly = true;
                        txtSaleTax.ReadOnly = true;
                        txtURL.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtEmail.ReadOnly = true;
                        txtEmail2.ReadOnly = true;
                        txtMobileNo.ReadOnly = true;

                        SupplierTypeBox.Enabled = true;
                        StatusCodeBox.Enabled = true;


                    }
                    else
                    {

                        txtSupplierCode.Clear();
                        txtShortName.Clear();
                        txtDescription.Clear();
                        txtContact.Clear();
                        txtPhoneNo.Clear();
                        txtPhoneNo2.Clear();
                        txtAddress.Clear();
                        txtShortAddress.Clear();
                        txtNtnNo.Clear();
                        txtFaxNo.Clear();
                        txtSaleTax.Clear();
                        txtURL.Clear();
                        txtRemarks.Clear();
                        txtEmail.Clear();
                        txtEmail2.Clear();
                        txtMobileNo.Clear();

                        SupplierTypeBox.SelectedIndex = -1;
                        StatusCodeBox.SelectedIndex = -1;

                    }
                }
            }
        }


        //======================================= UPDATE BUTTON =======================================

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (isValidation())
            {

                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateSupplierMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierCode1", txtSupplierCode.Text.Trim());

                    cmd.Parameters.AddWithValue("@SupplierCode", SupplierCodeBox.SelectedValue?.ToString() ?? "");

                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@SupplierTypeCode", SupplierTypeBox.SelectedValue?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@ContactPerson", txtContact.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShortName", txtShortName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@ShortAddress", txtShortAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhoneNo2", txtPhoneNo2.Text.Trim());
                    cmd.Parameters.AddWithValue("@FaxNo", txtFaxNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMail", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMail2", txtEmail2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Url", txtURL.Text.Trim());
                    cmd.Parameters.AddWithValue("@NtnNo", txtNtnNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@SaleTaxRegNo", txtSaleTax.Text.Trim());
                    cmd.Parameters.AddWithValue("@Reference", DBNull.Value);
                    cmd.Parameters.AddWithValue("@StatusCode", StatusCodeBox.SelectedValue?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim());

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier information updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestFormControler();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Supplier not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //======================================= FOR VALIDATION =======================================

        private bool isValidation()
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text) ||
                string.IsNullOrWhiteSpace(txtSupplierCode.Text) ||
                string.IsNullOrWhiteSpace(StatusCodeBox.Text)
            )
            {
                MessageBox.Show("Please Fill All the Fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //======================================= REST FORM =======================================

        private void RestFormControler()
        {
            SupplierCodeShow();

            txtSupplierCode.Clear();
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
            StatusCodeBox.SelectedIndex = -1;

            txtSupplierCode.Focus();

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
