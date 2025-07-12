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

namespace NEW_ERP.Forms.BOM.BOMDetail
{
    public partial class BomDetailViewAll : Form
    {
        public BomDetailViewAll()
        {
            InitializeComponent();
        }

        private void BomDetailViewAll_Load(object sender, EventArgs e)
        {
            BomDtailIdShow();
            LoadBomData();
        }

        //======================================= BOM MASTER ID COMBO =======================================

        public void BomDtailIdShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT BOMDetailID FROM BOMDetails";
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

        //======================================= LOAD DATA =======================================

        private void LoadBomData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT BOMDetailID, BOMID, ItemName, ItemType, Unit, ConsumptionPerPiece, WastagePercent,
TotalRequirement, Remarks  FROM BOMDetails ORDER BY BOMID DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    BomDetailDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //======================================= BUTTON CLICK =======================================

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    if (BomDetailIdBox.SelectedValue != null && BomDetailIdBox.SelectedValue is int)
                    {
                        int BomDetailId = Convert.ToInt32(BomDetailIdBox.SelectedValue);

                        SqlCommand cmd = new SqlCommand("sp_SearchBOMDetail", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BomDetailId", BomDetailId);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            BomDetailDataGridView.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("No records found.");
                            BomDetailDataGridView.DataSource = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a BOM Detail ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving BOM data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



    }
}
