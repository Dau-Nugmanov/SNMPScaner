using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.EfModels
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        public bool FirstEntry { get; set; }

        public virtual ICollection<EmailEntity> Emails { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
