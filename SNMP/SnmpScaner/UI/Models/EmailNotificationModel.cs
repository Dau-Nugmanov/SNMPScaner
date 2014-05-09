using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class EmailNotificationModel
    {
        public int IdEmailNotification { get; set; }

        [Required]
        public long IdNotification { get; set; }

        [Required]
        public string IdEmailEntity { get; set; }
    }
}