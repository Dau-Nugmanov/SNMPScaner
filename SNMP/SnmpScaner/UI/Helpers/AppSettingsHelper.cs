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

        public static Int32 GetMaxWarningsCount
        {
            get
            {
                Int32 maxWarningCount = 0;
                if(!Int32.TryParse(new AppSettingsReader().GetValue("maxWarningsCount", typeof(string)).ToString(), out maxWarningCount))
                {
                    throw new InvalidOperationException("Неверное значение количества оповещений");
                }
                return maxWarningCount;
            }
        }

        public static string GetReportingServerUrl
        {
            get
            {
                return new AppSettingsReader().GetValue("reportServerUrl", typeof(string)).ToString();
            }
        }

        public static string GetReportsPath
        {
            get
            {
                return new AppSettingsReader().GetValue("reportPath", typeof(string)).ToString();
            }
        }
    }
}