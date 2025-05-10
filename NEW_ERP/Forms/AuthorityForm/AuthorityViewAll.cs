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

namespace NEW_ERP.Forms.AuthorityForm
{
    public partial class AuthorityViewAll : Form
    {
        public AuthorityViewAll()
        {
            InitializeComponent();
        }

        private void AuthorityViewAll_Load(object sender, EventArgs e)
        {
            LoadCountryData();

            AuthorityCodeShow();
            AuthorityNameBox.SelectedIndex = -1;
        }

        //======================================= AUTHORITY CODE SHOW =======================================

        protected void AuthorityCodeShow()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select AuthorityName from AuthorityMaster";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    AuthorityNameBox.DataSource = dt;
                    AuthorityNameBox.DisplayMember = "AuthorityName";
                    AuthorityNameBox.ValueMember = "AuthorityName";
                    AuthorityNameBox.SelectedIndex = 0;
                }
            }
        }

        //======================================= LOAD FORM FUNCTION =======================================

        private void LoadCountryData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = @"
                SELECT 
                   *
                FROM AuthorityMaster 
                ORDER BY SystemDate DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    AuthorityDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string AuthorityName = AuthorityNameBox.Text.Trim();

            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_SearchAuthorityMaster", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AuthorityName", AuthorityName);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    AuthorityDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving cities:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



    }
}
