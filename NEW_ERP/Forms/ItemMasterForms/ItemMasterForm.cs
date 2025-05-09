using NEW_ERP.Forms.ItemMasterForms;
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

namespace NEW_ERP.Forms.ItemMaster
{
    public partial class ItemMasterForm : Form
    {
        public ItemMasterForm()
        {
            InitializeComponent();
        }
        //========================================= FORM LOAD EVENT =================================================

        private void ItemMasterForm_Load(object sender, EventArgs e)
        {

        }

        //========================================= SUBMIT BUTTON =================================================

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (isValidation())
            {
                using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("sp_InsertItemMaster", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductCode", TxtProductCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductDescription", TxtProductDes.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductShortName", TxtProductShortName.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserCode", "000123");
                        cmd.Parameters.AddWithValue("@Remarks", TxtProductRemarks.Text.Trim());

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Item inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RestFormControler();
                        }
                        else
                        {
                            MessageBox.Show("Insertion failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            TxtProductCode.Clear();
            TxtProductDes.Clear();
            TxtProductShortName.Clear();
            TxtProductRemarks.Clear();

        }

        //========================================= CHECK IF ALREADY EXIT =================================================

        private bool ProductCodeExists(string productCode)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = "SELECT COUNT(*) FROM ItemMaster WHERE ProductCode = @ProductCode";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductCode", productCode);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        //========================================= BUTTONS =================================================

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            ItemMasterViewAll NextForm = new ItemMasterViewAll();
            NextForm.Show();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            ItemMasterEdit NextForm = new ItemMasterEdit();
            NextForm.Show();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            ItemMasterDelete NextForm = new ItemMasterDelete();
            NextForm.Show();
        }
    }
}
