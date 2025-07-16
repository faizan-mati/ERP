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
    public partial class BomDetailAdd : AddFormTemplate
    {
        public BomDetailAdd()
        {
            InitializeComponent();
        }

        private void BomDetailAdd_Load(object sender, EventArgs e)
        {
           
        }

        //======================================= LOAD BOM MASTER ID =======================================

        private void BomIdBox_DropDown(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT BOMID  FROM BOMMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    BomIdBox.DataSource = dt;
                    BomIdBox.DisplayMember = "BOMID";
                    BomIdBox.ValueMember = "BOMID";
                    BomIdBox.SelectedIndex = -1;
                }
            }
        }
 
        //======================================= SUBMIT BUTTON =======================================

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    try
                    {
                        string selectedBomId = BomIdBox.SelectedValue.ToString();

                        using (SqlCommand cmd = new SqlCommand("sp_InsertBOMDetail", conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            // Basic BOM info
                            cmd.Parameters.AddWithValue("@BOMID", Convert.ToInt32(selectedBomId));
                            cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim());
                            cmd.Parameters.AddWithValue("@ItemType", txtItemType.Text.Trim());
                            cmd.Parameters.AddWithValue("@Unit", txtUnit.Text.Trim());

                            // Safe decimal parsing
                            decimal conPerPc = decimal.TryParse(txtConPerPc.Text.Trim(), out var val1) ? val1 : 0;
                            decimal wastagePercent = decimal.TryParse(txtWastagePercent.Text.Trim(), out var val2) ? val2 : 0;

                            cmd.Parameters.AddWithValue("@ConsumptionPerPiece", conPerPc);
                            cmd.Parameters.AddWithValue("@WastagePercent", wastagePercent);

                            // Remarks
                            string remarks = string.IsNullOrWhiteSpace(txtRemarks.Text) ? null : txtRemarks.Text.Trim();
                            cmd.Parameters.AddWithValue("@Remarks", (object)remarks ?? DBNull.Value);

                            // IsActive & StatusCode
                            bool isActive = isCheckedcheckbox.Checked;      
                            string statusCode = isActive ? "ACT" : "INA";

                            cmd.Parameters.AddWithValue("@UserCode", "ADM");   
                            cmd.Parameters.AddWithValue("@StatusCode", statusCode);

                            conn.Open();
                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("BOM detail inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RestFormControler();
                            }
                            else
                            {
                                MessageBox.Show("Insertion failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }




        //======================================= FOR VALIDATION =======================================

        public bool validation()
        {
            if(BomIdBox.SelectedIndex == -1 ||
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

            txtItemName.Clear();
            txtItemType.Clear();
            txtUnit.Clear();
            txtConPerPc.Clear();
            txtWastagePercent.Clear();
            txtRemarks.Clear();

        }

        //====================================== BUTTONS ===========================================

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            BomDetailEdit nextForm = new BomDetailEdit();
            nextForm.Show();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            BomDetailDelete nextForm = new BomDetailDelete();
            nextForm.Show();
        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            BomDetailViewAll nextForm = new BomDetailViewAll();
            nextForm.Show();
        }

       
    }
}
