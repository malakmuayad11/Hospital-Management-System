using System;
using System.Configuration;

namespace Hospital_Data
{
    public class clsSettingData
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString;
    }
}
