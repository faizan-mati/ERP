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
    public partial class CountryForm : Form
    {
        public CountryForm()
        {
            InitializeComponent();
        }

        private void CountryForm_Load(object sender, EventArgs e)
        {

        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertCountry", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CountryName", TxtCountryName.Text.Trim());
                        cmd.Parameters.AddWithValue("@CountryCode", TxtCountryCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
                        cmd.Parameters.AddWithValue("@SystemDate", DateTime.Now);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data saved successfully.");
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
            TxtCountryName.Clear();
            TxtCountryCode.Clear();
            isCheckedcheckbox.Checked = true;
        }

        //========================================= BUTTONS =================================================

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            CountryViewAll NextForm = new CountryViewAll();
            NextForm.Show();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            CountryEdit NextForm = new CountryEdit();
            NextForm.Show();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            CountryDelete NextForm = new CountryDelete();
            NextForm.Show();
        }
    }
}
