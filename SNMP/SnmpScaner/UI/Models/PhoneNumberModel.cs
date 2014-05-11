using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DAL.EfModels;

namespace UI.Models
{
    public class PhoneNumberModel : IModelEntity<DAL.EfModels.PhoneNumber>
    {
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string PhoneNumber { get; set; }

        public int IdUser { get; set; }

        public DAL.EfModels.PhoneNumber ToEfEntity()
        {
            return new DAL.EfModels.PhoneNumber
            {
                IdUser = IdUser,
                Number = PhoneNumber
            };
        }
    }
}