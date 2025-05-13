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
            ProductCodeShow();
            ProductCodeBox.SelectedIndex = -1;
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


        //======================================= PRODUCT SHORT CODE SHOW =======================================

        protected void ProductCodeShow()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"Select ProductShortName from ItemMaster";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ProductCodeBox.DataSource = dt;
                    ProductCodeBox.DisplayMember = "ProductShortName";
                    ProductCodeBox.ValueMember = "ProductShortName";
                    ProductCodeBox.SelectedIndex = 0;
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
                    if (ProductCodeBox.SelectedValue != null)
                    {

                        string selectedCode = ProductCodeBox.SelectedValue.ToString();
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("sp_SearchItemMaster", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ShortName", selectedCode);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        ItemMasterDataGridView.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Select any value");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Search failed:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
