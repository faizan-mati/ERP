﻿using NEW_ERP.Forms.CountryForms;
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
                LoadCountryCodes();
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
                    @"SELECT CountryID, CountryCode, CountryName, IsActive, StatusCode, SystemDate 
                      FROM Country 
                      WHERE StatusCode='ACT' 
                      ORDER BY SystemDate DESC", connection))
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

        //======================================= Load Country Codes =======================================
        private void LoadCountryCodes()
        {
            try
            {
                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand(
                    @"SELECT CountryCode 
                      FROM Country 
                      WHERE StatusCode='ACT'", connection))
                {
                    var adapter = new SqlDataAdapter(command);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    CountryCodeBox.DataSource = dataTable;
                    CountryCodeBox.DisplayMember = "CountryCode";
                    CountryCodeBox.ValueMember = "CountryCode";
                    CountryCodeBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading country codes", ex);
                throw;
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

                string selectedCountryCode = CountryCodeBox.SelectedValue.ToString();

                using (var connection = new SqlConnection(AppConnection.GetConnectionString()))
                using (var command = new SqlCommand("sp_SearchCountryByCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CountryCode", selectedCountryCode);

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