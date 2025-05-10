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

namespace NEW_ERP.Forms.AuthorityForm
{
    public partial class AuthorityForm : Form
    {
        public AuthorityForm()
        {
            InitializeComponent();
        }

        //======================================= SUBMIT BUTTON =======================================

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertAuthorityMaster", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@AuthorityCode", TxtAuthorityCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@AuthorityName", TxtAuthorityName.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserCode", "00123");
                        cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@StatusCode", TxtStatusCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Remarks", TxtAuthorityRemarks.Text.Trim());

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data inserted successfully.");
                            RestFormControler();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }


        //======================================= FOR VALIDATION =======================================

        private bool isValidation()
        {
            if (string.IsNullOrWhiteSpace(TxtAuthorityName.Text) ||
                string.IsNullOrWhiteSpace(TxtStatusCode.Text) ||
                string.IsNullOrWhiteSpace(TxtAuthorityCode.Text)

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
            TxtAuthorityCode.Clear();
            TxtAuthorityName.Clear();
            TxtStatusCode.Clear();
            TxtAuthorityRemarks.Clear();

        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            AuthorityViewAll NextForm = new AuthorityViewAll();
            NextForm.Show();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            AuthorityEdit NextForm = new AuthorityEdit();
            NextForm.Show();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            AuthorityDelete NextForm = new AuthorityDelete();
            NextForm.Show();
        }
    }
}
