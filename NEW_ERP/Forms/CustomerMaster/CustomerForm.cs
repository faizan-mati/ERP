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

namespace NEW_ERP.Forms.CustomerMaster
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            CustomerCodeShow();
            CustomerTypeBox.SelectedIndex = -1;
        }

        //======================================= CUSTOMER NAME SHOW =======================================

        protected void CustomerCodeShow()
        {
            using (SqlConnection conn = new SqlConnection(AppConnection.GetConnectionString()))
            {
                string query = @"select CustomertypeId, CustomerTypeName  from CustomerTypeMaster";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    CustomerTypeBox.DataSource = dt;
                    CustomerTypeBox.DisplayMember = "CustomerTypeName";
                    CustomerTypeBox.ValueMember = "CustomertypeId";
                    CustomerTypeBox.SelectedIndex = 0;
                }
            }
        }



        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
