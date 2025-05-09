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

namespace NEW_ERP.Forms.CountryForms
{
    public partial class CountryEdit : Form
    {
        public CountryEdit()
        {
            InitializeComponent();
        }

        private void CountryEdit_Load(object sender, EventArgs e)
        {
            CountryCodeShow();
            CountryCodeBox.SelectedIndex = -1;
        }

        //======================================= COUNTRY CODE SHOW =======================================

        protected void CountryCodeShow()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select CountryCode from Country";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CountryCodeBox.DataSource = dt;
                    CountryCodeBox.DisplayMember = "CountryCode";
                    CountryCodeBox.ValueMember = "CountryCode";
                    CountryCodeBox.SelectedIndex = 0;
                }
            }
        }

        //======================================= COUNTRY BOX CHANGE =======================================


        private void CountryCodeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountryCodeBox.SelectedValue != null)
            {
                string selectedCode = CountryCodeBox.SelectedValue.ToString();
                LoadItemDetails(selectedCode);
            }
        }

        private void LoadItemDetails(string selectedCode)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT CountryCode, CountryName, IsActive, SystemDate FROM Country
                         WHERE CountryCode = @CountryCode";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CountryCode", selectedCode);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        TxtCountryCode.Text = reader["CountryCode"].ToString();
                        TxtCountryName.Text = reader["CountryName"].ToString();


                        bool isActive = Convert.ToBoolean(reader["IsActive"]);
                        isCheckedcheckbox.Checked = isActive;

                        TxtCountryCode.ReadOnly = false;
                        TxtCountryName.ReadOnly = false;
                    }
                    else
                    {
                        TxtCountryCode.Clear();
                        TxtCountryName.Clear();
                        isCheckedcheckbox.Checked = false;
                    }
                }
            }
        }

        //======================================= UPDATE BUTTON =======================================

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    try
                    {
                        string selectedCode = CountryCodeBox.SelectedValue.ToString();
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("sp_UpdateCountry", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CountryCode1", TxtCountryCode.Text.Trim()); 
                        cmd.Parameters.AddWithValue("@CountryCode", selectedCode);               
                        cmd.Parameters.AddWithValue("@CountryName", TxtCountryName.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Country updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestFormControler();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Please check if the country code exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.Contains("Another record with the same CountryCode already exists"))
                        {
                            MessageBox.Show("Update failed: A country with this code already exists.", "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Update failed:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }


        //======================================= FOR VALIDATION =======================================

        private bool isValidation()
        {
            if (string.IsNullOrWhiteSpace(TxtCountryName.Text) ||
                string.IsNullOrWhiteSpace(TxtCountryCode.Text)

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

            CountryCodeShow();
            CountryCodeBox.SelectedIndex = -1;

            TxtCountryName.Clear();
            TxtCountryCode.Clear();
            isCheckedcheckbox.Checked = true;

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
