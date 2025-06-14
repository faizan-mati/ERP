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

namespace NEW_ERP.Forms.CustomerMaster
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            CustomerCodeShow();
            CountryCodeShow();
        }

        //======================================= CUSTOMER NAME SHOW =======================================

        protected void CustomerCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select CustomertypeId, CustomerTypeName  from CustomerTypeMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CustomerTypeBox.DataSource = dt;
                    CustomerTypeBox.DisplayMember = "CustomerTypeName";
                    CustomerTypeBox.ValueMember = "CustomertypeId";
                    CustomerTypeBox.SelectedIndex = -1;
                }
            }
        }



        //======================================= COUNTRY NAME SHOW =======================================

        protected void CountryCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select CountryID, CountryName from Country";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CountryBox.DataSource = dt;
                    CountryBox.DisplayMember = "CountryName";
                    CountryBox.ValueMember = "CountryID";
                    CountryBox.SelectedIndex = -1;
                }
            }
        }

        //=================================== COUNTRY INDEX CHANGE TO SHOW CITY =======================================

        private void CountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountryBox.SelectedValue == null || CountryBox.SelectedIndex == -1)
                return;

            int selectedCountryId;
            if (!int.TryParse(CountryBox.SelectedValue.ToString(), out selectedCountryId))
                return;

            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select CityID, CityName from City WHERE CountryID = @CountryID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CountryID", selectedCountryId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CityBox.DataSource = dt;
                    CityBox.DisplayMember = "CityName";
                    CityBox.ValueMember = "CityID";
                    CityBox.SelectedIndex = -1;
                }
            }
        }

        //=================================== SUBMIT BUTTON =======================================

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    try
                    {

                        string selectedType = CustomerTypeBox.SelectedValue.ToString();
                        string selectedCountry = CountryBox.SelectedValue.ToString();
                        string selectedCity = CityBox.SelectedValue.ToString();

                        conn.Open();

                        SqlCommand cmd = new SqlCommand("sp_InsertCustomerMaster", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CustomerCode", txtCustomerCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());

                        cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@WhatsAppNo", txtWhatsapp.Text.Trim());

                        cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
                        cmd.Parameters.AddWithValue("@ZipCode", txtZipCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());

                        cmd.Parameters.AddWithValue("@GSTNo", txtGSTNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@NTN", txtNTN.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);

                        cmd.Parameters.AddWithValue("@CustomerTypeId", Convert.ToInt32(selectedType));
                        cmd.Parameters.AddWithValue("@Country", Convert.ToInt32(selectedCountry));
                        cmd.Parameters.AddWithValue("@City", Convert.ToInt32(selectedCity));

                        cmd.Parameters.AddWithValue("@CreditLimit", 0); 
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", "Admin"); 



                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Item inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetFormControls();
                        }
                        else
                        {
                            MessageBox.Show("Insertion failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //======================================= FORM VALIDATION =======================================

        private bool isValidation()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerCode.Text) ||
                string.IsNullOrWhiteSpace(txtCustomerName.Text) ||

                string.IsNullOrWhiteSpace(txtNTN.Text) ||
                string.IsNullOrWhiteSpace(txtGSTNo.Text) ||

                CustomerTypeBox.SelectedIndex == -1 ||
                CountryBox.SelectedIndex == -1 ||
                CityBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        //======================================= REST FORM =======================================

        private void ResetFormControls()
        {
            txtCustomerCode.Clear();
            txtCustomerName.Clear();
            txtContactPerson.Clear();
            txtMobileNo.Clear();
            txtEmail.Clear();
            txtWhatsapp.Clear();
            txtState.Clear();
            txtZipCode.Clear();
            txtAddress.Clear();
            txtGSTNo.Clear();
            txtNTN.Clear();

            CustomerTypeBox.SelectedIndex = -1;
            CountryBox.SelectedIndex = -1;
            CityBox.DataSource = null;

            isCheckedcheckbox.Checked = false;
        }


        //====================================== BUTTONS ===========================================

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            CustomerViewAll NextForm = new CustomerViewAll();
            NextForm.Show();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            CustomerEdit NextForm = new CustomerEdit();
            NextForm.Show();
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            CustomerDelete NextForm = new CustomerDelete();
            NextForm.Show();
        }


        private void label14_Click(object sender, EventArgs e)
        {

        }

      
    }
}
