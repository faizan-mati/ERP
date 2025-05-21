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

namespace NEW_ERP.Forms.SupplierType
{
    public partial class SupplyTypeViewAll : Form
    {
        public SupplyTypeViewAll()
        {
            InitializeComponent();
        }

        private void SupplyTypeViewAll_Load(object sender, EventArgs e)
        {
            SatutsCodeShow();
            LoadCountryData();
        }

        //======================================= LOAD FORM FUNCTION =======================================

        private void LoadCountryData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM SupplierType ORDER BY SystemDate DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SupplyTypeDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        //======================================= COUNTRY NAME SHOW =======================================

        protected void SatutsCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT DISTINCT StatusCode FROM SupplierType;";

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

        //======================================= SEARCH BUTTON  =======================================

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    if (StatusCodeBox.SelectedValue != null)
                    {
                        string selectedStatusCode = StatusCodeBox.SelectedValue.ToString();
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("sp_SearchSupplierType", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StatusCode", selectedStatusCode);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        SupplyTypeDataGridView.DataSource = dt;
                    }

                    else
                    {
                        MessageBox.Show("Select any value");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Search failed:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
    }
}
