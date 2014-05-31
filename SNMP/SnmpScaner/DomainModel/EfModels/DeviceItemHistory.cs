using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel.EfModels
{
    [Table("DeviceItemsHistory")]
    public class DeviceItemHistory
    {
        [Key]
        public int IdItemHistory { get; set; }

        [StringLength(100)]
        public string Value { get; set; }

        public DateTime Timestamp { get; set; }

		//public long IdDeviceEntity { get; set; }

		//public long IdDeviceItemEntity { get; set; }

		public long IdDevicesItems { get; set; }

        //[ForeignKey("IdDeviceEntity,IdDeviceItemEntity")]
		[ForeignKey("IdDevicesItems")]
        public DevicesItems DevicesItems { get; set; }


    }
}
