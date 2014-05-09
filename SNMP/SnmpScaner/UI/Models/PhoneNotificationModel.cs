using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class PhoneNotificationModel
    {
        public int IdPhoneNotification { get; set; }

        [Required]
        public long IdNotification { get; set; }

        [Required]
        public string IdPhoneEntity { get; set; }
    }
}