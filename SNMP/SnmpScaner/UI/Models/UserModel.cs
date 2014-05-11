using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.EfModels;

namespace UI.Models
{
    public class UserModel : IModelEntity<DAL.EfModels.User>
    {
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Поле не должно превышать 50 символов")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Поле не должно превышать 50 символов")]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Поле не должно превышать 50 символов")]
        [DisplayName("Логин для входа в систему")]
        //[Remote("IsLoginExists", "Users", ErrorMessage="Извините, такой логин уже зарегистрирован в системе")]
        public string Login { get; set; }

        //[Required]
        public bool IsAdmin { get; set; }

        public EmailModel[] Emails { get; set; }

        public PhoneNumberModel[] PhoneNumbers { get; set; }

        public DAL.EfModels.User ToEfEntity()
        {
            List<EmailEntity> emails = new List<EmailEntity>();
            List<PhoneNumber> numbers = new List<PhoneNumber>();

            if (Emails != null && Emails.Any())
                emails.AddRange(Emails.Select(t => new EmailEntity
                    {
                        Email = t.Email,
                        IdUser = t.IdUser
                    }));
            if (PhoneNumbers != null && PhoneNumbers.Any())
                numbers.AddRange(PhoneNumbers.Select(t => new PhoneNumber
                    {
                        Number = t.PhoneNumber,
                        IdUser = t.IdUser
                    }));
            return new User()
            {
                IdUser = IdUser,
                Name = Name,
                LastName = LastName,
                Login = Login,
                IsAdmin = IsAdmin,
                Emails = emails,
                PhoneNumbers = numbers
            };
        }

        public static UserModel ToModelEntity(DAL.EfModels.User user)
        {
            List<EmailModel> emails = new List<EmailModel>();
            List<PhoneNumberModel> phoneNumbers = new List<PhoneNumberModel>();
            if (user.Emails != null && user.Emails.Any())
                emails.AddRange(user.Emails.Select(t => new EmailModel 
                { 
                    Email = t.Email, 
                    IdUser = t.IdUser 
                }));
            if (user.PhoneNumbers != null && user.PhoneNumbers.Any())
                phoneNumbers.AddRange(user.PhoneNumbers.Select(t => new PhoneNumberModel
                    {
                        IdUser = t.IdUser,
                        PhoneNumber = t.Number
                    }));
            return new UserModel
            {
                Emails = emails.ToArray(),
                IdUser = user.IdUser,
                IsAdmin = user.IsAdmin,
                LastName = user.LastName,
                Login = user.Login,
                Name = user.Name,
                PhoneNumbers = phoneNumbers.ToArray()
            };
        }
    }
}