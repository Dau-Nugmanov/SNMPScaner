using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.EfModels;

namespace UI.Models
{
    public class DeviceItemWrap
    {
        public DevicesItems DeviceItem { get; set; }
        public bool IsCritical { get; set; }
    }
}