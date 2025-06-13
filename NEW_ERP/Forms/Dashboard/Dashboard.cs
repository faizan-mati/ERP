using NEW_ERP.Forms.AuthorityForm;
using NEW_ERP.Forms.CityForms;
using NEW_ERP.Forms.CountryForms;
using NEW_ERP.Forms.CustomerMaster;
using NEW_ERP.Forms.ItemMaster;
using NEW_ERP.Forms.SupplierMaster;
using NEW_ERP.Forms.SupplierType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEW_ERP.Forms.Dashboard
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        public void loadform(object form)
        {

            if (this.panel4.Controls.Count > 0)
                this.panel4.Controls.RemoveAt(0);
            Form f = form as Form;

            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panel4.Controls.Add(f);
            this.panel4.Tag = f;
            f.Show();

        }


        private void AuthorityMenu_Click(object sender, EventArgs e)
        {
            loadform(new AuthorityAdd());
        }

        private void CountryMenu_Click(object sender, EventArgs e)
        {
            loadform(new CountryForm());
        }

        private void CityMenu_Click(object sender, EventArgs e)
        {
            loadform(new CityForm());
        }

        private void CustomerMenu_Click(object sender, EventArgs e)
        {
            loadform(new CustomerForm());
        }

        private void SupplierMasterMenu_Click(object sender, EventArgs e)
        {
            loadform(new SupplierMasterForm());
        }

        private void SupplierTypeMenu_Click(object sender, EventArgs e)
        {
            loadform(new SupplierTypeForm());
        }

        private void ItemMenu_Click(object sender, EventArgs e)
        {
            loadform(new ItemMasterForm());
        }



    }
}
