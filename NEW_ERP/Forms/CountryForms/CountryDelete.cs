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
    public partial class CountryDelete : Form
    {
        public CountryDelete()
        {
            InitializeComponent();
        }

        private void CountryDelete_Load(object sender, EventArgs e)
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

        //======================================= DELETE BUTTON =======================================


        private void DeleteBtn_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                if (CountryCodeBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select a product code to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                if (isValidation())
                {

                    using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        try
                        {
                            conn.Open();

                            SqlCommand cmd = new SqlCommand("sp_DeleteCountry", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@CountryCode", CountryCodeBox.SelectedValue.ToString());

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Country deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RestFormControler();
                            }
                            else
                            {
                                MessageBox.Show("Delete failed. No record found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
