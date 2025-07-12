using NEW_ERP.Forms.BOM.BOMMaster;
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

namespace NEW_ERP.Forms.BOMMaster
{
    public partial class BomMasterAdd : AddFormTemplate
    {
        public BomMasterAdd()
        {
            InitializeComponent();
        }

        private void BomMasterAdd_Load(object sender, EventArgs e)
        {
            SaleOderId();

            // Clear ProductBox on load
            ProductBox.DataSource = null;
            ProductBox.Items.Clear();
        }

        //======================================= LOAD SALE ORDER ID =======================================

        protected void SaleOderId()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"SELECT SaleOrderID, SaleOrderNo FROM SaleOrderMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    SaleOrderBox.DataSource = dt;
                    SaleOrderBox.DisplayMember = "SaleOrderNo";
                    SaleOrderBox.ValueMember = "SaleOrderID";
                    SaleOrderBox.SelectedIndex = -1;
                }
            }
        }

        //======================================= ON SALE ORDER CHANGE =======================================

        private void SaleOrderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SaleOrderBox.SelectedValue != null && SaleOrderBox.SelectedValue is int)
            {
                int saleOrderId = (int)SaleOrderBox.SelectedValue;
                //MessageBox.Show("Selected SaleOrderID: " + saleOrderId);
                LoadProducts(saleOrderId);
            }
        }


        //======================================= LOAD PRODUCTS BASED ON SALE ORDER =======================================

        protected void LoadProducts(int saleOrderId)
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"
    SELECT P.ProductCode, P.ProductDescription
    FROM SaleOrderMaster SM
    INNER JOIN ItemMaster P ON SM.ProductID = P.ProductCode
    WHERE SM.SaleOrderID = @SaleOrderID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleOrderID", saleOrderId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ProductBox.DataSource = dt;
                    ProductBox.DisplayMember = "ProductDescription";   
                    ProductBox.ValueMember = "ProductCode";      
                    ProductBox.SelectedIndex = -1;
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
                        string SelectedSaleOrderId = SaleOrderBox.SelectedValue.ToString();
                        string SelectedProductId = ProductBox.SelectedValue.ToString();

                        using (SqlCommand cmd = new SqlCommand("sp_InsertBOMMaster", conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@SaleOrderId", Convert.ToInt32(SelectedSaleOrderId));
                            cmd.Parameters.AddWithValue("@ProductId", Convert.ToInt32(SelectedProductId));
                            cmd.Parameters.AddWithValue("@VersionNo", txtVersionNo.Text.Trim());
                            cmd.Parameters.AddWithValue("@CreatedBy", Environment.UserName);

                            conn.Open(); 

                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("BOM inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if(string.IsNullOrWhiteSpace(txtVersionNo.Text) ||
                SaleOrderBox.SelectedIndex == -1 ||
                ProductBox.SelectedIndex == -1
                )
            {
                MessageBox.Show("Please Fill All the Fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //======================================= REST FORM =======================================

        public void RestFormControler()
        {
            txtVersionNo.Clear();

            SaleOrderBox.SelectedIndex = -1;
            ProductBox.SelectedIndex = -1;


        }

        //====================================== BUTTONS ===========================================


        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            BomMasterEdit NextForm = new BomMasterEdit();
            NextForm.Show();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            BomMasterDelete NextForm = new BomMasterDelete();
            NextForm.Show();
        }

        private void ViewAllBtn_Click(object sender, EventArgs e)
        {
            BomMasterViewAll NextForm = new BomMasterViewAll();
            NextForm.Show();
        }
    }
}
