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
    public partial class ItemMasterEdit : Form
    {
        public ItemMasterEdit()
        {
            InitializeComponent();
        }

        private void ItemMasterEdit_Load(object sender, EventArgs e)
        {
            ProductCodeShow();
            ProductCodeBox.SelectedIndex = -1;
        }

        //======================================= PRODUCT CODE SHOW =======================================

        protected void ProductCodeShow()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"Select ProductCode from ItemMaster";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ProductCodeBox.DataSource = dt;
                    ProductCodeBox.DisplayMember = "ProductCode";
                    ProductCodeBox.ValueMember = "ProductCode";
                    ProductCodeBox.SelectedIndex = 0;
                }
            }
        }

        //======================================= PRODUCT BOX CHANGE =======================================

        private void ProductCodeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProductCodeBox.SelectedValue != null)
            {
                string selectedCode = ProductCodeBox.SelectedValue.ToString();
                LoadItemDetails(selectedCode);
            }
        }

        private void LoadItemDetails(string productCode)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT ProductDescription, ProductShortName, Remarks, ProductCode
                         FROM ItemMaster
                         WHERE ProductCode = @ProductCode";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductCode", productCode);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        TxtProductCode.Text = reader["ProductCode"].ToString();
                        TxtProductDes.Text = reader["ProductDescription"].ToString();
                        TxtProductShortName.Text = reader["ProductShortName"].ToString();
                        TxtProductRemarks.Text = reader["Remarks"].ToString();

                        TxtProductCode.ReadOnly = false;
                        TxtProductDes.ReadOnly = false;
                        TxtProductShortName.ReadOnly = false;
                        TxtProductRemarks.ReadOnly = false;
                    }
                    else
                    {
                        TxtProductDes.Clear();
                        TxtProductRemarks.Clear();
                        TxtProductRemarks.Clear();
                        TxtProductCode.Clear();
                    }
                }
            }
        }

        //======================================= UPDATE BUTTON =======================================

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (ProductCodeBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a product code to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("sp_UpdateItemMaster", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductCode", ProductCodeBox.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@ProductDescription", TxtProductDes.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductShortName", TxtProductShortName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Remarks", TxtProductRemarks.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductCode1", TxtProductCode.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestFormControler();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (string.IsNullOrWhiteSpace(TxtProductCode.Text) ||
                string.IsNullOrWhiteSpace(TxtProductShortName.Text) ||
                string.IsNullOrWhiteSpace(TxtProductDes.Text)

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
            ProductCodeShow();
            ProductCodeBox.SelectedIndex = -1;

            TxtProductCode.Clear();
            TxtProductDes.Clear();
            TxtProductShortName.Clear();
            TxtProductRemarks.Clear();


        }


        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
