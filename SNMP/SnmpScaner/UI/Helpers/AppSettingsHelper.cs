using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UI.Helpers
{
    public class AppSettingsHelper
    {
        public static string DefaultLoginForDevice
        {
            get
            {
                return new AppSettingsReader().GetValue("defaultLogin", typeof(String)).ToString();
            }
        }

        public static string DefaultPasswordForDevice
        {
            get
            {
                return new AppSettingsReader().GetValue("defaultPassword", typeof(String)).ToString();
            }
        }
    }
}