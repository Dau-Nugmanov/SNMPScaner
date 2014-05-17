using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Helpers;
using UI.Models;

namespace UI.Code
{
    public static class WarningsStorage
    {
        private static SortedList<int, WarningModel> _warningNotifications = new SortedList<int, WarningModel>();
        private static int _maxWarningsCount = AppSettingsHelper.GetMaxWarningsCount;
        
        public static void AddWarning(WarningModel warning)
        {
            if (_warningNotifications.Count >= _maxWarningsCount)
                _warningNotifications.Clear();
            _warningNotifications.Add(_warningNotifications.Count, warning);
        }

        public static IEnumerable<WarningModel> GetWarnings()
        {
            return _warningNotifications.Values.AsEnumerable<WarningModel>();
        }

        public static int GetCount()
        {
            return _warningNotifications.Count;
        }
    }
}