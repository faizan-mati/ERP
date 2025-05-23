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
    public partial class SupplierMasterDelete : Form
    {
        public SupplierMasterDelete()
        {
            InitializeComponent();
        }

        private void SupplierMasterDelete_Load(object sender, EventArgs e)
        {
            SupplierCodeShow();
            SatutsCodeShow();
            SupplierTypeCodeShow();
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


                    }
                    else
                    {
                        txtSupplierCode.Clear();

                    }
                }
            }
        }

        //======================================= DELETE BUTTON =======================================

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSupplierCode.Text))
            {
                MessageBox.Show("Please select a supplier to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteSupplierMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierCode", txtSupplierCode.Text.Trim());

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplier deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestFormControler();
                        }
                        else
                        {
                            MessageBox.Show("No supplier found with the specified code.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occurred while deleting supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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




    }
}
