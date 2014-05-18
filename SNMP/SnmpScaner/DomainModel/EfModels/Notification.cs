﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class Notification
    {
        [Key]
        public long IdNotification { get; set; }

        public long IdDeviceEntity { get; set; }

        public long IdDeviceItemEntity { get; set; }

        public long TimeDelta { get; set; }

        public long ValueDelta { get; set; }

        [ForeignKey("IdDeviceEntity,IdDeviceItemEntity")]
        public DevicesItems DevicesItems { get; set; }

        public virtual ICollection<EmailNotification> EmailNotifications { get; set; }

        public virtual ICollection<PhoneNotification> PhoneNotifications { get; set; }
    }
}