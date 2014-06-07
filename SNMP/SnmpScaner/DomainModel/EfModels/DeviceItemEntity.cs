using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel.EfModels
{
    public class DeviceItemEntity
    {
        [Key]
        public long IdDeviceItemEntity { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Oid { get; set; }

        public int IdModel { get; set; }

		[Required]
	    public DeviceItemEntityDataType DataType { get; set; }

	    [ForeignKey("IdModel")]
        public DeviceModel Model { get; set; }

        public virtual ICollection<DevicesItems> DevicesItems { get; set; }
    }
}
