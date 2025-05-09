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

namespace NEW_ERP
{
    public partial class CountryViewAll : Form
    {
        public CountryViewAll()
        {
            InitializeComponent();
        }

        private void CountryViewAll_Load(object sender, EventArgs e)
        {
            LoadCountryData();
            CountryCodeShow();
            CountryCodeBox.SelectedIndex = -1;
        }

        //======================================= LOAD FORM FUNCTION =======================================

        private void LoadCountryData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM Country ORDER BY SystemDate DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CountryDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        //======================================= SEARCH BUTTON  =======================================

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    string selectedCountryCode = CountryCodeBox.SelectedValue.ToString();
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_SearchCountryByCode", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CountryCode", selectedCountryCode);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CountryDataGridView.DataSource = dt; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Search failed:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



    }
}
