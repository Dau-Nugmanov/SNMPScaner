using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class DeviceEntity
    {
        [Key]
        public long IdDeviceEntity { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Port { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsActive { get; set; }

        public int IdCustomer { get; set; }

        public int IdModel { get; set; }

        public string Ip { get; set; }

        [ForeignKey("IdCustomer")]
        public Customer Customer { get; set; }

        [ForeignKey("IdModel")]
        public DeviceModel DeviceModel { get; set; }

        public virtual ICollection<DevicesItems> DevicesItems { get; set; }
    }
}
