﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace NEW_ERP.GernalClasses
{
    class AppConnection
    {

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ERP"].ConnectionString;
        }

    }
}
