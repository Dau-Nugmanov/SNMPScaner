using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class WarningModel
    {
        public long IdDevice { get; set; }
        public string DeviceName { get; set; }
        public string DeviceMaker { get; set; }
        public string DeviceModel { get; set; }
        public long IdParameter { get; set; }
        public string Value { get; set; }
        public int Number { get; set; }
        public bool WasRead { get; set; }

    }
}