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
    public partial class supplierTypeDelete : Form
    {
        public supplierTypeDelete()
        {
            InitializeComponent();
        }

        private void supplierTypeDelete_Load(object sender, EventArgs e)
        {
            SupplierCodeShow();
        }

        //======================================= SUPPLIER CODE SHOW =======================================

        protected void SupplierCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT SupplierTypeCode FROM SupplierType;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SupplierCodeBox.DataSource = dt;
                    SupplierCodeBox.DisplayMember = "SupplierTypeCode";
                    SupplierCodeBox.ValueMember = "SupplierTypeCode";
                    SupplierCodeBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= SUPPLIER BOX CHANGE =======================================

        private void SupplierCodeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SupplierCodeBox.SelectedValue != null)
            {
                string selectedCode = SupplierCodeBox.SelectedValue.ToString();
                LoadItemDetails(selectedCode);
            }
        }

        private void LoadItemDetails(string selectedCode)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @" SELECT 
        SupplierTypeCode,
        Description,
        SystemDate,
        StatusCode,
        UserCode,
        Remarks
    FROM SupplierType
    WHERE SupplierTypeCode = @SupplyTypeCode";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplyTypeCode", selectedCode);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtSupplierCode.Text = reader["SupplierTypeCode"].ToString();
                        txtSupplierDes.Text = reader["Description"].ToString();
                        txtSupplierRemarks.Text = reader["Remarks"].ToString();
                        SatusCodeBox.Text = reader["StatusCode"].ToString();

                    }
                    else
                    {
                        txtSupplierCode.Clear();
                        txtSupplierDes.Clear();
                        txtSupplierRemarks.Clear();
                    }
                }
            }
        }


        //======================================= DELETE BUTTON =======================================

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (SupplierCodeBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select a product code to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isValidation())
                {

                    using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        try
                        {
                            conn.Open();

                            SqlCommand cmd = new SqlCommand("sp_DeleteSupplierType", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@SupplierTypeCode", SupplierCodeBox.SelectedValue.ToString());

                            int rowsAffected = cmd.ExecuteNonQuery();

                            RestFormControler();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Item deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                              
                            }
                            else
                            {
                                MessageBox.Show("Delete failed. No record found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            SupplierCodeShow();

            txtSupplierDes.Clear();
            txtSupplierRemarks.Clear();
            txtSupplierCode.Clear();

            SatusCodeBox.SelectedIndex = -1;


        }




    }
}
