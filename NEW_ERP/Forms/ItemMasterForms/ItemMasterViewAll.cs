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

namespace NEW_ERP.Forms.ItemMasterForms
{
    public partial class ItemMasterViewAll : Form
    {
        public ItemMasterViewAll()
        {
            InitializeComponent();
        }

        //======================================= LOAD FORM =======================================

        private void ItemMasterViewAll_Load(object sender, EventArgs e)
        {
            LoadItemData();
        }

        //======================================= LOAD FORM FUNCTION =======================================
        
        private void LoadItemData()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_GetItemMaster", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ItemMasterDataGridView.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_SearchItemMaster", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ShortName", txtSearch.Text.Trim());

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ItemMasterDataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Search failed:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
