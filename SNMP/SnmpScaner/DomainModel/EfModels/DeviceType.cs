using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.EfModels
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
