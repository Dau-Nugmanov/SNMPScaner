using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class Maker
    {
        [Key]
        public int IdMaker { get; set; }

        [Required]
        [StringLength(50)]
        public string MakerName { get; set; }

        public virtual ICollection<DeviceModel> Models { get; set; }

        //public virtual ICollection<DeviceItemEntity> Items { get; set; }
    }
}
