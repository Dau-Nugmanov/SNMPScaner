using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class Customer
    {
        [Key]
        public int IdCustomer { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [StringLength(100)]
        public string ManagerName { get; set; }

        [StringLength(50)]
        public string ManagerEmail { get; set; }

        [StringLength(50)]
        public string ManagerPhone { get; set; }

        [StringLength(200)]
        public string Comment { get; set; }

        public virtual ICollection<DeviceEntity> Devices { get; set; }
    }
}
