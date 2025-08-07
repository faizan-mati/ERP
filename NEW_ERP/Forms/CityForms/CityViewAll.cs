using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
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
            try
            {
                LoadData();
                CountryCodeShow();
            }
            catch (Exception ex)
            {
                ShowError("Error loading CityViewAll form", ex);
            }
        }

        //======================================= Load Country Data =======================================
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
    c.CityID,
    c.CityName, 
    co.CountryName, 
    c.IsActive, 
    s.StatusCode,        
    s.StatusDescription,  
    c.SystemDate
FROM City c
INNER JOIN Country co ON c.CountryID = co.CountryID
LEFT JOIN Status s ON c.StatusCode = s.StatusId   
WHERE c.StatusCode in (2,1)
ORDER BY c.SystemDate DESC;
                    ";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CityDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to load country data from database", ex);
                }
            }
        }

        //======================================= Show Country Codes =======================================
        protected void CountryCodeShow()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    string query = @"
                        SELECT DISTINCT 
                            co.CountryID, 
                            co.CountryName
                        FROM City c
                        INNER JOIN Country co ON c.CountryID = co.CountryID
                        WHERE c.StatusCode in (2,1)
                        ORDER BY co.CountryName ASC;
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        CityNameBox.DataSource = dt;
                        CityNameBox.DisplayMember = "CountryName";
                        CityNameBox.ValueMember = "CountryID";
                        CityNameBox.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to load country codes", ex);
                }
            }
        }

        //======================================= Search Button Click =======================================
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (CityNameBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select a country to search", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int selectedCountryId = Convert.ToInt32(CityNameBox.SelectedValue);
                SearchCitiesByCountry(selectedCountryId);
            }
            catch (Exception ex)
            {
                ShowError("Error during city search", ex);
            }
        }

        //======================================= Search Cities By Country =======================================
        private void SearchCitiesByCountry(int countryId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_SearchCity", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CountryID", countryId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No cities found for the selected country", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    CityDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to search cities by country", ex);
                }
            }
        }

        //======================================= City Grid Double Click =======================================
        private void CityDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var selectedRow = CityDataGridView.Rows[e.RowIndex];
                var value = selectedRow.Cells["CityID"].Value;

                if (value == null || !int.TryParse(value.ToString(), out int cityId))
                {
                    MessageBox.Show("Invalid City ID selected.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                OpenCityForEditing(cityId);
            }
            catch (Exception ex)
            {
                ShowError("Error handling grid double click", ex);
            }
        }

        //======================================= Open City For Editing =======================================
        private void OpenCityForEditing(int cityId)
        {
            try
            {
                this.Close();
                using (var cityFormAdd = new CityFormAdd(cityId, true))
                {
                    cityFormAdd.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to open city editor for ID {cityId}", ex);
            }
        }

        //======================================= Show Error Message =======================================
        private void ShowError(string context, Exception ex)
        {
            MessageBox.Show($"{context}:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}