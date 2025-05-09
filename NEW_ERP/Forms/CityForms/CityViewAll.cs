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
    public partial class CityViewAll : Form
    {
        public CityViewAll()
        {
            InitializeComponent();
        }

        private void CityViewAll_Load(object sender, EventArgs e)
        {
            LoadCountryData();
            CountryCodeShow();
            CityNameBox.SelectedIndex = -1;
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
                    c.CityName, 
                    co.CountryName, 
                    c.IsActive, 
                    c.SystemDate
                FROM City c
                INNER JOIN Country co ON c.CountryID = co.CountryID
                ORDER BY c.SystemDate DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CityDataGridView.DataSource = dt;
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
                string query = @"
            SELECT DISTINCT co.CountryID, co.CountryName 
            FROM City c
            INNER JOIN Country co ON c.CountryID = co.CountryID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CityNameBox.DataSource = dt;
                    CityNameBox.DisplayMember = "CountryName";  
                    CityNameBox.ValueMember = "CountryID";    
                    CityNameBox.SelectedIndex = 0;
                }
            }
        }


        //======================================= SEARCH BUTTON  =======================================

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            int selectedCountryId = Convert.ToInt32(CityNameBox.SelectedValue);

            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_GetCitiesByCountry", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CountryID", selectedCountryId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CityDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving cities:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
