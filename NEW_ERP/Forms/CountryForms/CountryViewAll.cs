using NEW_ERP.Forms.CountryForms;
using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
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
            try
            {
                LoadCountryData();
                LoadStatustCodes();
                CountryCodeBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ShowError("Error initializing form", ex);
            }
        }

        #region Data Loading Methods

        //======================================= Load Country Data =======================================
        private void LoadCountryData()
        {
            try
            {
                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand(
                    @"SELECT 
    C.CountryID, 
    C.CountryCode, 
    C.CountryName, 
    C.IsActive, 
    S.StatusCode,        
    S.StatusDescription,  
    C.SystemDate 
FROM Country C
LEFT JOIN Status S ON C.StatusCode = S.StatusId
WHERE C.StatusCode = 2
ORDER BY C.SystemDate DESC;
", connection))
                {
                    connection.Open();

                    var adapter = new SqlDataAdapter(command);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    CountryDataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading country data", ex);
                throw;
            }
        }

        //======================================= Load Status Codes =======================================
        private void LoadStatustCodes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = "SELECT StatusId, StatusCode FROM Status";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DataTable dtRoles = new DataTable();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        dtRoles.Load(sdr);

                        CountryCodeBox.DataSource = dtRoles;
                        CountryCodeBox.DisplayMember = "StatusCode";
                        CountryCodeBox.ValueMember = "StatusId";
                        CountryCodeBox.SelectedIndex = -1;


                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading sale order dropdown", ex);
            }
        }

        #endregion

        #region Event Handlers

        //======================================= Search Button Click =======================================
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (CountryCodeBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select a country code", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand("sp_SearchCountryByCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StatusCode", GetNullableValue(CountryCodeBox));

                    var adapter = new SqlDataAdapter(command);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    CountryDataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error searching for country", ex);
            }
        }

        //======================================= Get Nullable Value – Combo =======================================
        private object GetNullableValue(ComboBox comboBox)
        {
            return comboBox.SelectedItem != null ? comboBox.SelectedValue : DBNull.Value;
        }

        //======================================= Data Grid Double Click =======================================
        private void CountryDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var selectedRow = CountryDataGridView.Rows[e.RowIndex];
                var value = selectedRow.Cells["CountryID"].Value;

                if (value == null || !int.TryParse(value.ToString(), out int countryId))
                {
                    MessageBox.Show("Invalid Country ID selected.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                OpenCountryForEditing(countryId);
            }
            catch (Exception ex)
            {
                ShowError("Error handling grid double click", ex);
            }
        }

        #endregion

        #region Helper Methods

        //======================================= Open Country For Editing =======================================
        private void OpenCountryForEditing(int countryId)
        {
            try
            {
                this.Close();
                using (var countryFormAdd = new CountryFormAdd(countryId, true))
                {
                    countryFormAdd.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowError("Error opening country for editing", ex);
                throw;
            }
        }

        //======================================= Show Error =======================================
        private void ShowError(string context, Exception ex)
        {
            MessageBox.Show($"{context}:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}