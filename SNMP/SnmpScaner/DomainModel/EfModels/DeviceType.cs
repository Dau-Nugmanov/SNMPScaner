using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class DeviceType
    {
        [Key]
        public int IdDeviceType { get; set; }

        [Required]
        [StringLength(50)]
        public string DeviceTypeName { get; set; }

        public virtual ICollection<DeviceModel> Models { get; set; }
    }
}
