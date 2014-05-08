using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
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
