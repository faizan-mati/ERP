using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
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
            try
            {
                LoadCountryData();
                AuthorityCodeShow();
            }
            catch (Exception ex)
            {
                HandleError("Error loading form data", ex);
            }
        }

        /// <summary>
        /// Loads all authority codes from database and binds to AuthorityNameBox
        /// </summary>
        private void AuthorityCodeShow()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = @"SELECT AuthorityName FROM AuthorityMaster WHERE StatusCode='ACT'";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        AuthorityNameBox.DataSource = dt;
                        AuthorityNameBox.DisplayMember = "AuthorityName";
                        AuthorityNameBox.ValueMember = "AuthorityName";
                        AuthorityNameBox.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading authority codes", ex);
                throw; 
            }
        }

        /// <summary>
        /// Loads all active authority data from database and binds to grid view
        /// </summary>
        private void LoadCountryData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();

                    string query = @"SELECT AuthorityCode, AuthorityName, UserCode, 
                                   SystemDate, StatusCode, Remarks FROM AuthorityMaster 
                                   WHERE StatusCode='ACT'
                                   ORDER BY SystemDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        AuthorityDataGridView.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading authority data", ex);
                throw; 
            }
        }

        /// <summary>
        /// Searches for authority records based on selected authority name
        /// </summary>
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (AuthorityNameBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select an authority name first.", "Information",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string authorityName = AuthorityNameBox.Text.Trim();

                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_SearchAuthorityMaster", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AuthorityName", authorityName);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        AuthorityDataGridView.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error searching authority records", ex);
            }
        }

        /// <summary>
        /// Handles double click event on grid view to open selected record for editing
        /// </summary>
        private void AuthorityDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var selectedRow = AuthorityDataGridView.Rows[e.RowIndex];
                var value = selectedRow.Cells["AuthorityCode"].Value;

                if (value == null || !int.TryParse(value.ToString(), out int authorityId))
                {
                    MessageBox.Show("Invalid Authority Code selected.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                OpenAuthorityForEditing(authorityId);
            }
            catch (Exception ex)
            {
                HandleError("Error handling grid double click", ex);
            }
        }

        /// <summary>
        /// Opens the AuthorityAdd form in edit mode for the specified authority ID
        /// </summary>
        private void OpenAuthorityForEditing(int authorityId)
        {
            try
            {
                this.Close();
                using (var authorityAddForm = new AuthorityAdd(authorityId, true))
                {
                    authorityAddForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error opening authority for editing", ex);
            }
        }

        /// <summary>
        /// Handles and displays error messages to user
        /// </summary>
        /// 
        private void HandleError(string contextMessage, Exception ex)
        {
            MessageBox.Show($"{contextMessage}:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}