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
    public partial class CustomerEdit : Form
    {
        public CustomerEdit()
        {
            InitializeComponent();
        }

        private void CustomerEdit_Load(object sender, EventArgs e)
        {
            CustomerCodeShow();
        }

        //======================================= CUSTOMER CODE SHOW =======================================

        protected void CustomerCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select CustomerCode  from CustomerMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CustomerCodeBox.DataSource = dt;
                    CustomerCodeBox.DisplayMember = "CustomerCode";
                    CustomerCodeBox.ValueMember = "CustomerCode";
                    CustomerCodeBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= CUSTOMER TYPE SHOW =======================================

        private void LoadCustomerTypes()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "SELECT CustomerTypeID, CustomerTypeName FROM CustomerTypeMaster"; 
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                CustomerTypeBox.DataSource = dt;
                CustomerTypeBox.DisplayMember = "CustomerTypeName"; 
                CustomerTypeBox.ValueMember = "CustomerTypeID";     
                CustomerTypeBox.SelectedIndex = -1;
            }
        }

        //======================================= COUNTRY ID SHOW =======================================

        private void LoadCountryId()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "SELECT CountryID, CountryName FROM Country";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                CountryBox.DataSource = dt;
                CountryBox.DisplayMember = "CountryName";
                CountryBox.ValueMember = "CountryID";
                CountryBox.SelectedIndex = -1;
            }
        }

        //======================================= COUNTRY CHANGE SHOW =======================================

        private void CountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountryBox.SelectedValue != null && CountryBox.SelectedIndex != -1)
            {
                int selectedCountryId;
                if (int.TryParse(CountryBox.SelectedValue.ToString(), out selectedCountryId))
                {
                    LoadCitiesByCountry(selectedCountryId);
                }
            }
        }

        private void LoadCitiesByCountry(int countryId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT CityID, CityName FROM City WHERE CountryID = @CountryID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CountryID", countryId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                CityBox.DataSource = dt;
                CityBox.DisplayMember = "CityName";
                CityBox.ValueMember = "CityID";
                CityBox.SelectedIndex = -1;
            }
        }


        //================================= CUSTOMER CODE BOX INDEX CHANGE SHOW =======================================

        private void CustomerCideBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CustomerCodeBox.SelectedValue != null)
            {
                string selectedCode = CustomerCodeBox.SelectedValue.ToString();
                LoadItemDetails(selectedCode);
            }
        }

        private void LoadItemDetails(string CustomerCode)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT CustomerID ,CustomerCode ,CustomerName ,ContactPerson ,MobileNo ,WhatsAppNo ,Email ,Address ,City
        ,State ,Country ,ZipCode ,CustomerTypeID ,GSTNo ,NTN ,CreditLimit ,IsActive ,CreatedDate ,CreatedBy ,UpdatedDate ,UpdatedBy
        FROM [dbo].[CustomerMaster] where CustomerCode = @CustomerCode";


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerCode", CustomerCode);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int customerTypeId = Convert.ToInt32(reader["CustomerTypeID"]);
                        int countryId = Convert.ToInt32(reader["Country"]);
                        int cityId = Convert.ToInt32(reader["City"]);

                        LoadCustomerTypes();
                        LoadCountryId();

                        txtContactPerson.Text = reader["ContactPerson"].ToString();
                        txtCustomerName.Text = reader["CustomerName"].ToString();
                        txtCustomerCode.Text = reader["CustomerCode"].ToString();
                        txtWhatsapp.Text = reader["WhatsAppNo"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtMobileNo.Text = reader["MobileNo"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtZipCode.Text = reader["ZipCode"].ToString();
                        txtState.Text = reader["State"].ToString();
                        txtGSTNo.Text = reader["GSTNo"].ToString();
                        txtNTN.Text = reader["NTN"].ToString();
                        isCheckedcheckbox.Checked = Convert.ToBoolean(reader["IsActive"]);

                        CustomerTypeBox.SelectedValue = customerTypeId;
                        CountryBox.SelectedValue = countryId;

                        LoadCitiesByCountry(countryId);
                        CityBox.SelectedValue = cityId;

                        txtContactPerson.ReadOnly = false;
                        txtCustomerName.ReadOnly = false;
                        txtCustomerCode.ReadOnly = false;
                        txtWhatsapp.ReadOnly = false;
                        txtEmail.ReadOnly = false;
                        txtMobileNo.ReadOnly = false;
                        txtAddress.ReadOnly = false;
                        txtZipCode.ReadOnly = false;
                        txtState.ReadOnly = false;
                        txtGSTNo.ReadOnly = false;
                        txtNTN.ReadOnly = false;

                        CustomerTypeBox.Enabled = true;
                        CountryBox.Enabled = true;
                        CityBox.Enabled = true;
                        isCheckedcheckbox.Enabled = true;


                    }
                }
            }
        }

        //================================================= UPDATE BUTTON ===============================================

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (CustomerCodeBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a customer code.");
                return;
            }
            if (isValidation())
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {


                    string selectedCode = CustomerCodeBox.SelectedValue.ToString();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CustomerCode", txtCustomerCode.Text);
                        cmd.Parameters.AddWithValue("@CustomerCode1", selectedCode);

                        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                        cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                        cmd.Parameters.AddWithValue("@WhatsAppNo", txtWhatsapp.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@City", CityBox.SelectedValue ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@State", txtState.Text);
                        cmd.Parameters.AddWithValue("@Country", CountryBox.SelectedValue ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ZipCode", txtZipCode.Text);
                        cmd.Parameters.AddWithValue("@CustomerTypeID", CustomerTypeBox.SelectedValue ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GSTNo", txtGSTNo.Text);
                        cmd.Parameters.AddWithValue("@NTN", txtNTN.Text);
                        cmd.Parameters.AddWithValue("@CreditLimit", 0);
                        cmd.Parameters.AddWithValue("@IsActive", isCheckedcheckbox.Checked);
                        cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UpdatedBy", "Admin");

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Customer updated successfully.");
                            ResetFormControls();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error updating customer: " + ex.Message);
                        }
                    }
                }
            }
        }


        //======================================= FOR VALIDATION =======================================

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
            CustomerCodeBox.SelectedIndex = -1;
            CityBox.DataSource = null;

            isCheckedcheckbox.Checked = false;
        }

        //====================================== BUTTONS ===========================================
        
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
