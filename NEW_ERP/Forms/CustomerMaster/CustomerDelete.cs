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
    public partial class CustomerDelete : Form
    {
        public CustomerDelete()
        {
            InitializeComponent();
        }

        private void CustomerDelete_Load(object sender, EventArgs e)
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

        private void CustomerCodeBox_SelectedIndexChanged(object sender, EventArgs e)
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


                    }
                }
            }
        }

        //======================================= DELETE BUTTON =======================================

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (CustomerCodeBox.SelectedValue != null)
            {
                string customerCode = CustomerCodeBox.SelectedValue.ToString();

                DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?",
                                                      "Confirm Delete",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        try
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("sp_DeleteCustomer", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@CustomerCode", customerCode);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Customer deleted successfully.");
                                    CustomerCodeShow();
                                    ResetFormControls();


                                }
                                else
                                {
                                    MessageBox.Show("No customer found with this code.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Delete failed:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
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


    }
}
