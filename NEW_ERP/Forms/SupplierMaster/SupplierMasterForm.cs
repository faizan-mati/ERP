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
    public partial class SupplierMasterForm : Form
    {
        public SupplierMasterForm()
        {
            InitializeComponent();
        }

        private void SupplierMasterForm_Load(object sender, EventArgs e)
        {
            SatutsCodeShow();
            SupplierTypeShow();
        }

        //======================================= SUPPLIER STATUS CODE SHOW =======================================

        protected void SatutsCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT StatusId, StatusDescription FROM Status;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    StatusCodeBox.DataSource = dt;
                    StatusCodeBox.DisplayMember = "StatusDescription";
                    StatusCodeBox.ValueMember = "StatusDescription";
                    StatusCodeBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= SUPPLIER TYPE SHOW =======================================

        protected void SupplierTypeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select Description, SupplierTypeCode from SupplierType";

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


        //========================================= SUBMIT BUTTON =================================================


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
                        cmd.Parameters.AddWithValue("@StatusCode", StatusCodeBox.SelectedValue ?? StatusCodeBox.Text);
                        cmd.Parameters.AddWithValue("@UserCode", "00123");
                        cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Clear all textboxes
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

            // Reset combo boxes
            SupplierTypeBox.SelectedIndex = -1;  // or 0 if you want to select the first item
            StatusCodeBox.SelectedIndex = -1;

            // Reset focus to the first control
            txtSupplierCode.Focus();

        }

        //========================================= ALL BUTTON =================================================


        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            SupplierViewAll NextForm = new SupplierViewAll();
            NextForm.Show();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            SupplierMasterEdit NextForm = new SupplierMasterEdit();
            NextForm.Show();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            SupplierMasterDelete NextForm = new SupplierMasterDelete();
            NextForm.Show();
        }
    }
}
