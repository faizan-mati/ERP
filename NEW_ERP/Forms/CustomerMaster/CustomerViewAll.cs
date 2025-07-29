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
    public partial class CustomerViewAll : Form
    {
        public CustomerViewAll()
        {
            InitializeComponent();
        }

        private void CustomerViewAll_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        //======================================= LOAD FORM FUNCTION =======================================

        private void LoadCustomerData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT[CustomerID]
      ,[CustomerCode]
      ,[CustomerName]
      ,[ContactPerson]
      ,[MobileNo]
      ,[WhatsAppNo]
      ,[Email]
      ,[Address]
      ,[City]
      ,[State]
      ,[Country]
      ,[ZipCode]
      ,[CustomerTypeID]
      ,[GSTNo]
      ,[NTN]
      ,[CreditLimit]
      ,[IsActive]
      ,[CreatedDate]
      ,[CreatedBy]
      ,[UpdatedDate]
      ,[UpdatedBy]
      ,[StatusCode] FROM CustomerMaster ORDER BY CreatedDate DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CustomerDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //======================================= SEARCH BUTTON  =======================================

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_SearchCustomerByName", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        CustomerDataGridView.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Search failed:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void CustomerDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var selectedRow = CustomerDataGridView.Rows[e.RowIndex];
                var value = selectedRow.Cells["CustomerID"].Value;

                if (value == null || !int.TryParse(value.ToString(), out int customerID))
                {
                    MessageBox.Show("Invalid Country ID selected.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                OpenCountryForEditing(customerID);
            }
            catch (Exception ex)
            {
                ShowError("Error handling grid double click", ex);
            }
        }


        private void OpenCountryForEditing(int customerID)
        {
            try
            {
                this.Close();
                using (var CustomerFormAdd = new CustomerFormAdd(customerID, true))
                {
                    CustomerFormAdd.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowError("Error opening country for editing", ex);
                throw;
            }
        }


        private void ShowError(string context, Exception ex)
        {
            MessageBox.Show($"{context}:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



    }
}
