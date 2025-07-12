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

namespace NEW_ERP.Forms.BOM.BOMMaster
{
    public partial class BomMasterViewAll : Form
    {
        public BomMasterViewAll()
        {
            InitializeComponent();
        }

        private void BomMasterViewAll_Load(object sender, EventArgs e)
        {
            BomMasterIdShow();
            LoadCountryData();
        }

        //======================================= BOM MASTER ID COMBO =======================================

        public void BomMasterIdShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT BOMID FROM BOMMaster";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    BomMasterIdBox.DataSource = dt;
                    BomMasterIdBox.DisplayMember = "BOMID";
                    BomMasterIdBox.ValueMember = "BOMID";
                    BomMasterIdBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= LOAD DATA =======================================

        private void LoadCountryData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = @"
                SELECT  bm.BOMID,
    i.ProductDescription, 
	so.SaleOrderNo,
    bm.VersionNo,
    bm.CreatedBy,
    bm.CreatedDate
FROM BOMMaster bm
inner join ItemMaster as i  on i.ProductCode = bm.ProductID
inner join SaleOrderMaster as so on so.SaleOrderID =  bm.SaleOrderID
ORDER BY BOMID DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    BomDataGridView.DataSource = dt;
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
                    if (BomMasterIdBox.SelectedValue != null)
                    {
                        string BomMasterId = BomMasterIdBox.SelectedValue.ToString();

                        conn.Open();
                        SqlCommand cmd = new SqlCommand("sp_SearchBOMMaster", conn); 
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BomMasterId", BomMasterId);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            BomDataGridView.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("No records found.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a BOM Master ID.");
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
