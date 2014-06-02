using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel.EfModels
{
    public class Notification
    {
        [Key]
        public long IdNotification { get; set; }

        public long TimeDelta { get; set; }

        public long ValueDelta { get; set; }

		public long Lo { get; set; }

		public long Hi { get; set; }

		public long IdDevicesItems { get; set; }

        //[ForeignKey("IdDeviceEntity,IdDeviceItemEntity")]
		[ForeignKey("IdDevicesItems")]
        public DevicesItems DevicesItems { get; set; }

        public virtual ICollection<EmailNotification> EmailNotifications { get; set; }

        public virtual ICollection<PhoneNotification> PhoneNotifications { get; set; }
    }
}
