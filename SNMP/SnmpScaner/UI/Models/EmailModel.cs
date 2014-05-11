using DAL.EfModels;
using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class EmailModel : IModelEntity<EmailEntity>
    {
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Email { get; set; }

        public int IdUser { get; set; }

        public EmailEntity ToEfEntity()
        {
            return new EmailEntity
            {
                Email = Email,
                IdUser = IdUser
            };
        }
    }
}