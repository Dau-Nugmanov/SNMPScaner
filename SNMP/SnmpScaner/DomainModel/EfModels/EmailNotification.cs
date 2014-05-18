using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class EmailNotification
    {
        [Key]
        public int IdEmailNotification { get; set; }

        public long IdNotification { get; set; }
        public string IdEmailEntity { get; set; }

        [ForeignKey("IdNotification")]
        public Notification Notification { get; set; }

        [ForeignKey("IdEmailEntity")]
        public EmailEntity EmailEntity { get; set; }
    }
}
