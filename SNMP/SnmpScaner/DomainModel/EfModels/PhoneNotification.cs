using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel.EfModels
{
    public class PhoneNotification
    {
        [Key]
        public int IdPhoneNotification { get; set; }

        public long IdNotification { get; set; }
        public string IdPhoneEntity { get; set; }

        [ForeignKey("IdNotification")]
        public Notification Notification { get; set; }

        [ForeignKey("IdPhoneEntity")]
        public PhoneNumber PhoneNumber { get; set; }
    }
}
