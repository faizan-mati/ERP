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
    public partial class AuthorityDelete : Form
    {
        public AuthorityDelete()
        {
            InitializeComponent();
        }

        private void AuthorityDelete_Load(object sender, EventArgs e)
        {
            AuthorityCodeShow();
            AuthorityCodeBox.SelectedIndex = -1;
        }

        //======================================= AUTHORITY CODE SHOW =======================================

        protected void AuthorityCodeShow()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select AuthorityCode from AuthorityMaster";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    AuthorityCodeBox.DataSource = dt;
                    AuthorityCodeBox.DisplayMember = "AuthorityCode";
                    AuthorityCodeBox.ValueMember = "AuthorityCode";
                    AuthorityCodeBox.SelectedIndex = 0;
                }
            }
        }


        //======================================= AUTHORITY BOX CHANGE =======================================


        private void AuthorityCodeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AuthorityCodeBox.SelectedValue != null)
            {
                string selectedCode = AuthorityCodeBox.SelectedValue.ToString();
                LoadItemDetails(selectedCode);
            }
        }

        private void LoadItemDetails(string AuthorityCode)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select AuthorityCode, AuthorityName, StatusCode, Remarks from AuthorityMaster
                         WHERE AuthorityCode = @AuthorityCode";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@AuthorityCode", AuthorityCode);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        TxtAuthorityCode.Text = reader["AuthorityCode"].ToString();
                        TxtAuthorityName.Text = reader["AuthorityName"].ToString();
                        TxtAuthorityRemarks.Text = reader["Remarks"].ToString();
                        TxtAuthorityStatusCode.Text = reader["StatusCode"].ToString();

                    }
                    else
                    {
                        TxtAuthorityCode.Clear();
                        TxtAuthorityName.Clear();
                        TxtAuthorityStatusCode.Clear();
                        TxtAuthorityRemarks.Clear();
                    }
                }
            }
        }

        //======================================= DELETE BUTTON =======================================

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                if (isValidation())
                {
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_DeleteAuthorityMaster", con))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@AuthorityCode", TxtAuthorityCode.Text.Trim());

                            try
                            {
                                con.Open();
                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Record deleted successfully.");
                                    RestFormControler();
                                }
                                else
                                    MessageBox.Show("No record found with that Authority Code.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }


        //======================================= FOR VALIDATION =======================================

        private bool isValidation()
        {
            if (string.IsNullOrWhiteSpace(TxtAuthorityName.Text) ||
                string.IsNullOrWhiteSpace(TxtAuthorityStatusCode.Text) ||
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
            AuthorityCodeShow();
            AuthorityCodeBox.SelectedIndex = -1;

            TxtAuthorityCode.Clear();
            TxtAuthorityName.Clear();
            TxtAuthorityStatusCode.Clear();
            TxtAuthorityRemarks.Clear();

        }



    }
}
