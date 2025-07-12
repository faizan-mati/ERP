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
    public partial class BomDetailEdit : EditFormTemplate
    {
        public BomDetailEdit()
        {
            InitializeComponent();
        }

        private void BomDetailEdit_Load(object sender, EventArgs e)
        {
            LoadBomDetailIds();

            BomIdBox.Enabled = false;

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

                    txtItemName.ReadOnly = false;
                    txtItemType.ReadOnly = false;
                    txtUnit.ReadOnly = false;
                    txtConPerPc.ReadOnly = false;
                    txtWastagePercent.ReadOnly = false;
                    txtRemarks.ReadOnly = false;

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

                BomIdBox.Enabled = true;
                BomIdBox.SelectedValue = selectedBomId;
            }
        }

        //======================================= UPDATE BUTTON =======================================

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (BomDetailIdBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a BOM DETAIL ID to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedDetailId = BomDetailIdBox.SelectedValue.ToString();

            if (validation())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_UpdateBOMDetail", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
  
                            cmd.Parameters.AddWithValue("@BOMDetailID", Convert.ToInt32(selectedDetailId));
                            cmd.Parameters.AddWithValue("@BOMID", Convert.ToInt32(BomIdBox.SelectedValue));
                            cmd.Parameters.AddWithValue("@ItemType", txtItemType.Text.Trim());
                            cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Unit", txtUnit.Text.Trim());
                            cmd.Parameters.AddWithValue("@ConsumptionPerPiece", Convert.ToDecimal(txtConPerPc.Text.Trim()));
                            cmd.Parameters.AddWithValue("@WastagePercent", Convert.ToDecimal(txtWastagePercent.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim());

                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record updated successfully.");
                                RestFormControler(); 
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given BOM Detail ID.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //======================================= FOR VALIDATION =======================================

        public bool validation()
        {
            if (BomIdBox.SelectedIndex == 1 ||
                string.IsNullOrEmpty(txtItemName.Text) ||
                string.IsNullOrEmpty(txtItemType.Text) ||
                string.IsNullOrEmpty(txtUnit.Text) ||
                string.IsNullOrEmpty(txtConPerPc.Text))
            {
                MessageBox.Show("Please Fill All the Fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        //======================================= REST FORM =======================================

        public void RestFormControler()
        {
            BomIdBox.SelectedIndex = -1;
            BomDetailIdBox.SelectedIndex = -1;

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
