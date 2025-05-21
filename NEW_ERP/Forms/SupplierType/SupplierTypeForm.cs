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

namespace NEW_ERP.Forms.SupplierType
{
    public partial class SupplierTypeForm : Form
    {
        public SupplierTypeForm()
        {
            InitializeComponent();
        }

        private void SupplierTypeForm_Load(object sender, EventArgs e)
        {
            SatutsCodeShow();
        }

        //======================================= SUPPLIER CODE SHOW =======================================

        protected void SatutsCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT SupplierTypeStatusId, StatusDescription FROM SupplierTypeStatus;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SatusCodeBox.DataSource = dt;
                    SatusCodeBox.DisplayMember = "StatusDescription";
                    SatusCodeBox.ValueMember = "StatusDescription";
                    SatusCodeBox.SelectedIndex = -1;
                }
            }
        }


        //========================================= SUBMIT BUTTON =================================================

        private void SubmitBtn_Click(object sender, EventArgs e)
        {

            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {

                    string selectedCode = SatusCodeBox.SelectedValue.ToString();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertSupplierType", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SupplierTypeCode", txtSupplierCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Description", txtSupplierDes.Text.Trim());
                        cmd.Parameters.AddWithValue("@StatusCode", selectedCode);
                        cmd.Parameters.AddWithValue("@UserCode", "00123");
                        cmd.Parameters.AddWithValue("@Remarks", txtSupplierRemarks.Text.Trim());

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data inserted successfully.");
                            RestFormControler();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }

        //======================================= FOR VALIDATION =======================================

        private bool isValidation()
        {
            if (string.IsNullOrWhiteSpace(SatusCodeBox.Text) ||
                string.IsNullOrWhiteSpace(txtSupplierDes.Text) ||
                string.IsNullOrWhiteSpace(txtSupplierRemarks.Text)
            )
            {
                MessageBox.Show("Please Fill All the Fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //======================================= REST FORM =======================================

        private void RestFormControler()
        {
            txtSupplierDes.Clear();
            txtSupplierRemarks.Clear();
            txtSupplierCode.Clear();

            SatusCodeBox.SelectedIndex = -1;

        }

        //====================================== BUTTONS ===========================================

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            SupplyTypeViewAll NextForm = new SupplyTypeViewAll();
            NextForm.Show();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            SupplyTypeEdit NextForm = new SupplyTypeEdit();
            NextForm.Show();
        }



    }
}
