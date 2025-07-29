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
                LoadCountryData();
                CountryCodeShow();
            }
            catch (Exception ex)
            {
                ShowError("Error loading CityViewAll form", ex);
            }
        }

        /// <summary>
        /// Loads country and city data from database and binds it to the DataGridView
        /// </summary>
        private void LoadCountryData()
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
                            c.StatusCode,                    
                            c.SystemDate
                        FROM City c
                        INNER JOIN Country co ON c.CountryID = co.CountryID
                        WHERE c.StatusCode = 'ACT'
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

        /// <summary>
        /// Loads and displays country codes in the dropdown box
        /// </summary>
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
                        WHERE c.StatusCode = 'ACT'
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

        /// <summary>
        /// Handles search button click event to filter cities by selected country
        /// </summary>
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

        /// <summary>
        /// Searches cities by country ID using stored procedure
        /// </summary>
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

        /// <summary>
        /// Handles double-click event on DataGridView to open city for editing
        /// </summary>
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

        /// <summary>
        /// Opens the CityFormAdd form in edit mode for the specified city ID
        /// </summary>
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


        /// <summary>
        /// Displays an error message to the user
        /// </summary>

        private void ShowError(string context, Exception ex)
        {
            MessageBox.Show($"{context}:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}