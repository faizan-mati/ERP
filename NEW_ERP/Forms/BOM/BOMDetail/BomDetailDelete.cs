using NEW_ERP.GernalClasses;
using NEW_ERP.Template;
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
    public partial class BomDetailDelete : DeleteFormTemplate
    {
        public BomDetailDelete()
        {
            InitializeComponent();
        }

        private void BomDetailDelete_Load(object sender, EventArgs e)
        {
            LoadBomDetailIds();
        }

        //======================================= BOM DETAIL ID COMBO =======================================

        private void LoadBomDetailIds()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "SELECT BOMDetailID FROM BOMDetails";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                BomDetailIdBox.DataSource = dt;
                BomDetailIdBox.DisplayMember = "BOMDetailID";
                BomDetailIdBox.ValueMember = "BOMDetailID";
                BomDetailIdBox.SelectedIndex = -1;
            }
        }

        //======================================= BOM DETAIL BOX CHANGE =======================================

        private void BomDetailIdBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BomDetailIdBox.SelectedValue != null && BomDetailIdBox.SelectedValue is int)
            {
                string selectedDetailId = BomDetailIdBox.SelectedValue.ToString();
                LoadItemDetails(selectedDetailId);
            }
        }

        private void LoadItemDetails(string bomDetailId)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"
            SELECT BOMID, ItemType, ItemName, Unit, ConsumptionPerPiece, WastagePercent, Remarks 
            FROM BOMDetails 
            WHERE BOMDetailID = @BOMDetailID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BOMDetailID", bomDetailId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtItemName.Text = reader["ItemName"].ToString();
                    txtItemType.Text = reader["ItemType"].ToString();
                    txtUnit.Text = reader["Unit"].ToString();
                    txtConPerPc.Text = reader["ConsumptionPerPiece"].ToString();
                    txtWastagePercent.Text = reader["WastagePercent"].ToString();
                    txtRemarks.Text = reader["Remarks"].ToString();

                    int bomId = Convert.ToInt32(reader["BOMID"]);
                    LoadBomMasterDropdown(bomId);
                }
                else
                {
                    txtItemName.Clear();
                    txtItemType.Clear();
                    txtUnit.Clear();
                    txtConPerPc.Clear();
                    txtWastagePercent.Clear();
                    txtRemarks.Clear();
                    BomIdBox.DataSource = null;
                    BomIdBox.Enabled = false;
                }

                reader.Close();
            }
        }

        //======================================= LOAD MASTER BOM ID =======================================

        private void LoadBomMasterDropdown(int selectedBomId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "SELECT BOMID FROM BOMMaster";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                BomIdBox.DataSource = dt;
                BomIdBox.DisplayMember = "BOMID";
                BomIdBox.ValueMember = "BOMID";

                BomIdBox.SelectedValue = selectedBomId;
            }
        }

        //======================================= DELETE BUTTON =======================================

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (BomDetailIdBox.SelectedValue == null || BomDetailIdBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a BOM DETAIL ID to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this BOM Detail?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog != DialogResult.Yes)
                return;

            try
            {
                int bomDetailId = Convert.ToInt32(BomDetailIdBox.SelectedValue);

                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteBOMDetail", conn)) 
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BOMDetailID", bomDetailId);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("BOM Detail deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestFormControler();
                            LoadBomDetailIds();
                        }
                        else
                        {
                            MessageBox.Show("No BOM Detail found with that ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //======================================= REST FORM =======================================

        public void RestFormControler()
        {
            BomDetailIdBox.SelectedIndex = -1;
            BomIdBox.SelectedIndex = -1;

            txtItemName.Clear();
            txtItemType.Clear();
            txtUnit.Clear();
            txtConPerPc.Clear();
            txtWastagePercent.Clear();
            txtRemarks.Clear();

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
