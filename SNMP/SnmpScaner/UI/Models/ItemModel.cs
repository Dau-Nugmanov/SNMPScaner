
using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DomainModel.EfModels;

namespace UI.Models
{
    public class ItemModel : IModelEntity<DeviceItemEntity>
    {
        public long IdItem { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Oid_s { get; set; }

        public int IdModel { get; set; }

        public string[] EmailNotifications { get; set; }
        public string[] PhoneNumbersNotifications { get; set; }

        public int EnumDataType { get; set; }
        public string EnumDataTypeName { get; set; }

        public DeviceItemEntity ToEfEntity()
        {
            List<EmailNotification> emailNotifs = new List<EmailNotification>();
            List<PhoneNotification> phoneNotis = new List<PhoneNotification>();
            if (EmailNotifications != null)
                emailNotifs.AddRange(EmailNotifications.Select(t => new EmailNotification { IdEmailEntity = t }));
            if (PhoneNumbersNotifications != null)
                phoneNotis.AddRange(PhoneNumbersNotifications.Select(t => new PhoneNotification { IdPhoneEntity = t }));
            return new DeviceItemEntity
            {
                IdDeviceItemEntity = IdItem,
                Name = Name,
                Oid = Oid_s,
                IdModel = IdModel,
				//EmailNotifications = emailNotifs,
				//PhoneNotifications = phoneNotis,
                DataType = (DeviceItemEntityDataType)EnumDataType
            };
        }

        public static ItemModel ToModelEntity(DeviceItemEntity item)
        {
            List<string> emails = new List<string>();
            List<string> phones = new List<string>();
			//if (item.PhoneNotifications != null)
			//	phones.AddRange(item.PhoneNotifications.Select(t => t.IdPhoneEntity));
			//if (item.EmailNotifications != null)
			//	emails.AddRange(item.EmailNotifications.Select(t => t.IdEmailEntity));
            return new ItemModel
            {
                IdItem = item.IdDeviceItemEntity,
                IdModel = item.IdModel,
                Name = item.Name,
                Oid_s = item.Oid,
                EmailNotifications = emails.ToArray(),
                PhoneNumbersNotifications = phones.ToArray(),
                EnumDataType = (int)item.DataType,
                EnumDataTypeName = item.DataType.ToString()
            };
        }
    }
}