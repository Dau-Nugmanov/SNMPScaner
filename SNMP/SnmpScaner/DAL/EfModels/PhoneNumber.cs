using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class PhoneNumber
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        [StringLength(50)]
        public string Number { get; set; }

        public int IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }
    }
}
