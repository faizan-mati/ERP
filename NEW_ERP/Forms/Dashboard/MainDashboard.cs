using NEW_ERP.Forms.AuthorityForm;
using NEW_ERP.Forms.BOM.BOMDetail;
using NEW_ERP.Forms.BOMMaster;
using NEW_ERP.Forms.CityForms;
using NEW_ERP.Forms.CountryForms;
using NEW_ERP.Forms.CustomerMaster;
using NEW_ERP.Forms.ItemMaster;
using NEW_ERP.Forms.SaleOrder;
using NEW_ERP.Forms.SupplierMaster;
using NEW_ERP.Forms.SupplierType;
using NEW_ERP.Forms.YarnDemand;
using System;
using System.Windows.Forms;

namespace NEW_ERP.Forms.Dashboard
{
    public partial class MainDashboard : Form
    {
        public MainDashboard()
        {
            InitializeComponent();
        }

        //======================================= LOAD FORM FUNCTION =======================================
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

        //======================================= PANEL PAINT EVENT =======================================
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        //======================================= BUTTON CLICK EVENT =======================================
        private void button2_Click(object sender, EventArgs e)
        {

        }

        bool menuExpand = false;
        bool saleOrderMenuExpand = false;
        bool BomMenuExpend = false;

        int maxHeight = 335;
        int minHeight = 40;

        int saleOrderMaxHeight = 85;
        int saleOrderMinHeight = 40;

        int BomMaxHeight = 125;
        int BomMinHeight = 40;

        //======================================= MENU TRANSITION TIMER =======================================
        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand)
            {
                // Collapse
                StepUpMenuContainer.Height -= 335;
                if (StepUpMenuContainer.Height <= minHeight)
                {
                    StepUpMenuContainer.Height = minHeight;
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }
            else
            {
                // Expand
                StepUpMenuContainer.Height += 335;
                if (StepUpMenuContainer.Height >= maxHeight)
                {
                    StepUpMenuContainer.Height = maxHeight;
                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
        }

        //======================================= SALE ORDER MENU TRANSITION =======================================
        private void saleOrderTransition_Tick(object sender, EventArgs e)
        {
            if (saleOrderMenuExpand)
            {
                // Collapse
                SaleOrderMenuContainer.Height -= 85;
                if (SaleOrderMenuContainer.Height <= saleOrderMinHeight)
                {
                    SaleOrderMenuContainer.Height = saleOrderMinHeight;
                    saleOrderTransition.Stop();
                    saleOrderMenuExpand = false;
                }
            }
            else
            {
                // Expand
                SaleOrderMenuContainer.Height += 85;
                if (SaleOrderMenuContainer.Height >= saleOrderMaxHeight)
                {
                    SaleOrderMenuContainer.Height = saleOrderMaxHeight;
                    saleOrderTransition.Stop();
                    saleOrderMenuExpand = true;
                }
            }
        }

        //======================================= BOM MENU TRANSITION =======================================
        private void BomTransition_Tick(object sender, EventArgs e)
        {
            if (BomMenuExpend)
            {
                // Collapse
                BomMenuContainer.Height -= 125;
                if (BomMenuContainer.Height <= BomMinHeight)
                {
                    BomMenuContainer.Height = BomMinHeight;
                    BomTransition.Stop();
                    BomMenuExpend = false;
                }
            }
            else
            {
                // Expand
                BomMenuContainer.Height += 125;
                if (BomMenuContainer.Height >= BomMaxHeight)
                {
                    BomMenuContainer.Height = BomMaxHeight;
                    BomTransition.Stop();
                    BomMenuExpend = true;
                }
            }
        }

        bool YarnMenuExpend = false;
        int YarnMaxHeight = 85;
        int YarnMinHeight = 40;

        //======================================= YARN MENU TRANSITION =======================================
        private void YarnTransition_Tick(object sender, EventArgs e)
        {
            if (YarnMenuExpend)
            {
                // Collapse
                YarnMenuContainer.Height -= 85;
                if (YarnMenuContainer.Height <= YarnMinHeight)
                {
                    YarnMenuContainer.Height = YarnMinHeight;
                    YarnTransition.Stop();
                    YarnMenuExpend = false;
                }
            }
            else
            {
                // Expand
                YarnMenuContainer.Height += 85;
                if (YarnMenuContainer.Height >= YarnMaxHeight)
                {
                    YarnMenuContainer.Height = YarnMaxHeight;
                    YarnTransition.Stop();
                    YarnMenuExpend = true;
                }
            }
        }

        //======================================= BUTTON CLICK EVENTS =======================================
        private void SetUpBtn_Click_1(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        private void SaleOrderFormBtn_Click(object sender, EventArgs e)
        {
            saleOrderTransition.Start();
        }

        private void BomMenuBtn_Click(object sender, EventArgs e)
        {
            BomTransition.Start();
        }

        private void YarnMenuBtn_Click(object sender, EventArgs e)
        {
            YarnTransition.Start();
        }

        //======================================= FORM LOAD BUTTONS =======================================
        private void AuthorityBtn_Click(object sender, EventArgs e)
        {
            loadform(new AuthorityAdd("", false));
        }

        private void CountryBtn_Click(object sender, EventArgs e)
        {
            loadform(new CountryFormAdd(0, false));
        }

        private void CityBtn_Click(object sender, EventArgs e)
        {
            loadform(new CityFormAdd(0, false));
        }

        private void CustomerBtn_Click(object sender, EventArgs e)
        {
            loadform(new CustomerFormAdd(0, false));
        }

        private void SupplierMasterBtn_Click(object sender, EventArgs e)
        {
            loadform(new SupplierMasterAdd("", false));
        }

        private void SuppliertypeBtn_Click(object sender, EventArgs e)
        {
            loadform(new SupplierTypeAdd("", false));
        }

        private void ItemBtn_Click(object sender, EventArgs e)
        {
            loadform(new ItemMasterForm("", false));
        }

        private void SaleOrderBtn_Click(object sender, EventArgs e)
        {
            loadform(new SaleOrderAdd(0, false));
        }

        private void BOMBtn_Click(object sender, EventArgs e)
        {
            loadform(new BomMasterAdd(0, false));
        }

        private void BomDetailBtn_Click(object sender, EventArgs e)
        {
            loadform(new BomDetailAdd(0, false));
        }

        private void YarnBtn_Click(object sender, EventArgs e)
        {
            loadform(new YarnDemandAdd());
        }

        //======================================= OTHER EVENTS =======================================
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {

        }

        private void ReportBtn_Click(object sender, EventArgs e)
        {

        }

        private void ChlidPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}