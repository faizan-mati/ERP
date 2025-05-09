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

namespace NEW_ERP.Forms.CityForms
{
    public partial class CityForm : Form
    {
        public CityForm()
        {
            InitializeComponent();
        }

        private void CityForm_Load(object sender, EventArgs e)
        {
            CountryCodeShow();
            CountryCodeBox.SelectedIndex = -1;
        }

        //======================================= COUNTRY NAME SHOW =======================================

        protected void CountryCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select CountryID, CountryName  from Country";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CountryCodeBox.DataSource = dt;
                    CountryCodeBox.DisplayMember = "CountryName";
                    CountryCodeBox.ValueMember = "CountryID";
                    CountryCodeBox.SelectedIndex = 0;
                }
            }
        }

        //======================================= SUBMIT BUTTON =======================================


        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    try
                    {
                        string selectedCode = CountryCodeBox.SelectedValue.ToString();
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("sp_InsertCity", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CityName", TxtCityName.Text.Trim());
                        cmd.Parameters.AddWithValue("@CountryID", Convert.ToInt32(selectedCode));
                        cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
                        cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("City inserted successfully.");
                        RestFormControler();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 50000)
                        {
                            MessageBox.Show(ex.Message, "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Database error: " + ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unexpected error: " + ex.Message);
                    }
                }
            }
        }



        //======================================= FOR VALIDATION =======================================

        private bool isValidation()
        {
            if (string.IsNullOrWhiteSpace(CountryCodeBox.Text) ||
                string.IsNullOrWhiteSpace(TxtCityName.Text)

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

            TxtCityName.Clear();
            isCheckedcheckbox.Checked = true;

        }

        //========================================= BUTTONS =================================================

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            CityViewAll NextForm = new CityViewAll();
            NextForm.Show();
        }
    }
}
