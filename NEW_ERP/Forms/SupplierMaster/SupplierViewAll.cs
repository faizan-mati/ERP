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

namespace NEW_ERP.Forms.SupplierMaster
{
    public partial class SupplierViewAll : Form
    {
        public SupplierViewAll()
        {
            InitializeComponent();
        }

        private void SupplierViewAll_Load(object sender, EventArgs e)
        {
            StatusCodeShow();
            LoadSupplierData();
        }


        //======================================= STATUS CODE SHOW SHOW =======================================

        protected void StatusCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT DISTINCT StatusCode FROM SupplierMaster;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    StatusCodeBox.DataSource = dt;
                    StatusCodeBox.DisplayMember = "StatusCode";
                    StatusCodeBox.ValueMember = "StatusCode";
                    StatusCodeBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= LOAD FORM FUNCTION =======================================

        private void LoadSupplierData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = @"
SELECT SupplierCode , Description, SupplierTypeCode, ContactPerson, ShortName, Address, ShortAddress, PhoneNo, PhoneNo2
 , FaxNo, MobileNo, EMail, EMail2, Url, NtnNo, SaleTaxRegNo, Reference, SystemDate, StatusCode, UserCode, Remarks
  FROM SupplierMaster ORDER BY SystemDate DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SpplierMasterDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //======================================= SEARCH BUTTON =======================================


        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (StatusCodeBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Status Code to search.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("sp_SearchSupplierMaster", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatusCode", StatusCodeBox.SelectedValue);

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SpplierMasterDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




    }
}
