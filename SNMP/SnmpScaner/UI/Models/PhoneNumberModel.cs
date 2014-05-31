using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DomainModel.EfModels;

namespace UI.Models
{
    public class PhoneNumberModel : IModelEntity<PhoneNumber>
    {
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string PhoneNumber { get; set; }

        public int IdUser { get; set; }

        public PhoneNumber ToEfEntity()
        {
            return new PhoneNumber
            {
                IdUser = IdUser,
                Number = PhoneNumber
            };
        }
    }
}