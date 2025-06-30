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
    public partial class MainDashboard : Form
    {
        public MainDashboard()
        {
            InitializeComponent();
        }

        public void loadform(object form)
        {

            if (this.ChlidPanel.Controls.Count > 0)
                this.ChlidPanel.Controls.RemoveAt(0);
            Form f = form as Form;

            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.ChlidPanel.Controls.Add(f);
            this.ChlidPanel.Tag = f;
            f.Show();

        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        bool menuExpand = false;

        int maxHeight = 300;
        int minHeight = 46;

        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand)
            {
                // Collapse
                StepUpMenuContainer.Height -= 300;
                if (StepUpMenuContainer.Height <= minHeight)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }
            else
            {
                // Expand
                StepUpMenuContainer.Height += 300;
                if (StepUpMenuContainer.Height >= maxHeight)
                {
                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
        }


        private void SetUpBtn_Click_1(object sender, EventArgs e)
        {
            menuTransition.Start();
        }


        private void AuthorityBtn_Click(object sender, EventArgs e)
        {
            loadform(new AuthorityAdd());
        }

        private void CountryBtn_Click(object sender, EventArgs e)
        {
            loadform(new CountryForm());
        }

        private void CityBtn_Click(object sender, EventArgs e)
        {
            loadform(new CityForm());
        }

        private void CustomerBtn_Click(object sender, EventArgs e)
        {
            loadform(new CustomerForm());
        }

        private void SupplierMasterBtn_Click(object sender, EventArgs e)
        {
            loadform(new SupplierMasterForm());
        }

        private void SuppliertypeBtn_Click(object sender, EventArgs e)
        {
            loadform(new SupplierTypeForm());
        }

        private void ItemBtn_Click(object sender, EventArgs e)
        {
            loadform(new ItemMasterForm());
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReportBtn_Click(object sender, EventArgs e)
        {

        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}


