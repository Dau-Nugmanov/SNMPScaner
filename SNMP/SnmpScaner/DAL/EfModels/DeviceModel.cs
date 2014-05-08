using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class DeviceModel
    {
        [Key]
        public int IdDeviceModel { get; set; }

        [Required]
        [StringLength(50)]
        public string ModelName { get; set; }

        public int IdMaker { get; set; }

        public int IdDeviceType { get; set; }

        [ForeignKey("IdMaker")]
        public Maker Maker { get; set; }

        [ForeignKey("IdDeviceType")]
        public DeviceType DeviceType { get; set; }

        public virtual ICollection<DeviceEntity> Devices { get; set; }
        public virtual ICollection<DeviceItemEntity> Items { get; set; }

    }
}
