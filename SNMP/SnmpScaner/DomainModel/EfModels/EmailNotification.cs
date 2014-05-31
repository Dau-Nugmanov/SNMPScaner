using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel.EfModels
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
