﻿using NEW_ERP.Forms;
using NEW_ERP.Forms.AuthorityForm;
using NEW_ERP.Forms.CityForms;
using NEW_ERP.Forms.CountryForms;
using NEW_ERP.Forms.CustomerMaster;
using NEW_ERP.Forms.Dashboard;
using NEW_ERP.Forms.ItemMaster;
using NEW_ERP.Forms.ItemMasterForms;
using NEW_ERP.Forms.SupplierMaster;
using NEW_ERP.Forms.SupplierType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEW_ERP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainDashboard());
        }
    }
}
