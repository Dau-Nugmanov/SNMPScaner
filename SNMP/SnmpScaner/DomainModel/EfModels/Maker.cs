using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.EfModels
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
