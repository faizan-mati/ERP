using NEW_ERP.GernalClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NEW_ERP.Forms.BOM.BOMDetail
{
    public partial class BomDetailViewAll : Form
    {
        public BomDetailViewAll()
        {
            InitializeComponent();
        }
        //======================================= BomDetailViewAll_Load =======================================
        private void BomDetailViewAll_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBomDetailIds();
                LoadAllBomData();
            }
            catch (Exception ex)
            {
                HandleError("Error initializing form data", ex);
            }
        }

        //======================================= LoadBomDetailIds =======================================
        private void LoadBomDetailIds()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = @"SELECT BOMDetailID FROM BOMDetails 
                                   WHERE StatusCode='ACT' 
                                   ORDER BY BOMDetailID DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        BomDetailIdBox.DataSource = dt;
                        BomDetailIdBox.DisplayMember = "BOMDetailID";
                        BomDetailIdBox.ValueMember = "BOMDetailID";
                        BomDetailIdBox.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading BOM Detail IDs", ex);
                throw;
            }
        }

        //======================================= LoadAllBomData =======================================
        private void LoadAllBomData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    string query = @"
                                    SELECT [BOMDetailID]
                                          ,[BOMID]
                                          ,[ItemType]
                                          ,[ItemName]
                                          ,[Unit]
                                          ,[ConsumptionPerPiece]
                                          ,[WastagePercent]
                                          ,[TotalRequirement]
                                          ,[Remarks]
                                          ,[UserCode]
                                          ,[StatusCode]
                                          ,[SystemDate]
                                      FROM [dbo].[BOMDetails]
                                   WHERE StatusCode='ACT' 
                                   ORDER BY BOMDetailID DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        BomDetailDataGridView.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error loading BOM data", ex);
                throw;
            }
        }

        //======================================= SearchBtn_Click =======================================
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (BomDetailIdBox.SelectedValue == null)
                {
                    ShowInformationMessage("Please select a BOM Detail ID.");
                    return;
                }

                if (!int.TryParse(BomDetailIdBox.SelectedValue.ToString(), out int bomDetailId))
                {
                    ShowErrorMessage("Invalid BOM Detail ID selected.");
                    return;
                }

                SearchBomDetails(bomDetailId);
            }
            catch (Exception ex)
            {
                HandleError("Error searching BOM details", ex);
            }
        }

        //======================================= BomDetailDataGridView_CellDoubleClick =======================================
        private void BomDetailDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow selectedRow = BomDetailDataGridView.Rows[e.RowIndex];

                if (selectedRow.Cells["BOMDetailID"].Value == null ||
                    !int.TryParse(selectedRow.Cells["BOMDetailID"].Value.ToString(), out int bomDetailID))
                {
                    ShowErrorMessage("Invalid BOM detail selected.");
                    return;
                }

                this.Close();
                OpenBomDetailEditForm(bomDetailID);
            }
            catch (Exception ex)
            {
                HandleError("Error opening BOM detail for editing", ex);
            }
        }

        //======================================= SearchBomDetails =======================================
        private void SearchBomDetails(int bomDetailId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_SearchBOMDetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BomDetailId", bomDetailId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        BomDetailDataGridView.DataSource = dt;
                    }
                    else
                    {
                        ShowInformationMessage("No records found.");
                        BomDetailDataGridView.DataSource = null;
                    }
                }
            }
        }

        //======================================= OpenBomDetailEditForm =======================================
        private void OpenBomDetailEditForm(int bomDetailID)
        {
            using (BomDetailAdd bomDetailAddForm = new BomDetailAdd(bomDetailID, true))
            {
                bomDetailAddForm.ShowDialog();
            }
        }

        //======================================= ShowErrorMessage =======================================
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //======================================= ShowInformationMessage =======================================
        private void ShowInformationMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //======================================= HandleError =======================================
        private void HandleError(string message, Exception ex)
        {
            MessageBox.Show($"{message}:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}