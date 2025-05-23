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
    public partial class SupplyTypeEdit : Form
    {
        public SupplyTypeEdit()
        {
            InitializeComponent();
        }

        private void SupplyTypeEdit_Load(object sender, EventArgs e)
        {
            SupplierCodeShow();
            SatutsCodeShow();
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


        protected void SatutsCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT DISTINCT StatusCode FROM Status;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SatusCodeBox.DataSource = dt;
                    SatusCodeBox.DisplayMember = "StatusCode";
                    SatusCodeBox.ValueMember = "StatusCode";
                    SatusCodeBox.SelectedIndex = -1;
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
                        SatusCodeBox.SelectedValue = reader["StatusCode"].ToString();

                        txtSupplierCode.ReadOnly = false;
                        txtSupplierDes.ReadOnly = false;
                        txtSupplierRemarks.ReadOnly = false;

                        SatusCodeBox.Enabled = true;

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

        //======================================= UPDATE BUTTON =======================================


        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (SupplierCodeBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a Supplier Code code to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    try
                    {
                        string selectedCode = SatusCodeBox.SelectedValue.ToString();
                        string selectedSupplierCode = SupplierCodeBox.SelectedValue.ToString();

                        conn.Open();

                        SqlCommand cmd = new SqlCommand("sp_UpdateSupplierType", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SupplierTypeCode1", txtSupplierCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@SupplierTypeCode", selectedSupplierCode);
                        cmd.Parameters.AddWithValue("@Description", txtSupplierDes.Text.Trim());
                        cmd.Parameters.AddWithValue("@StatusCode", selectedCode);
                        cmd.Parameters.AddWithValue("@Remarks", txtSupplierRemarks.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Supplu updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestFormControler();
                        }
                        else
                        {
                            MessageBox.Show("Supplu failed. No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
