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
