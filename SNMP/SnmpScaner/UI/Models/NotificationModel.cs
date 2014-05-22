using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class NotificationModel : IModelEntity<Notification>
    {
        public long IdNotification { get; set; }
		public long IdDevicesItems { get; set; }
        [Range(1, int.MaxValue, ErrorMessage="Только целое число")]
        [Required(ErrorMessage="*")]
        public long DeltaTime { get; set; }

        [Required(ErrorMessage="*")]
        public long DeltaValue { get; set; }

        public EmailNotificationModel[] EmailNotifications { get; set; }

        public PhoneNotificationModel[] PhoneNotifications { get; set; }

        public Notification ToEfEntity()
        {
            List<EmailNotification> emails = new List<EmailNotification>();
            List<PhoneNotification> phones = new List<PhoneNotification>();
            if (EmailNotifications != null)
            {
                emails.AddRange(EmailNotifications.Select(t => new EmailNotification
                    {
                        IdEmailEntity = t.IdEmailEntity,
                        IdNotification = t.IdNotification,
                        IdEmailNotification = t.IdEmailNotification
                    }));
            }
            if (PhoneNotifications != null)
            {
                phones.AddRange(PhoneNotifications.Select(t => new PhoneNotification
                    {
                        IdNotification = t.IdNotification,
                        IdPhoneEntity = t.IdPhoneEntity,
                        IdPhoneNotification = t.IdPhoneNotification
                    }));
            }
            return new Notification
            {
                EmailNotifications = emails,
				IdDevicesItems = IdDevicesItems,
                IdNotification = IdNotification,
                PhoneNotifications = phones,
                TimeDelta = DeltaTime,
                ValueDelta = DeltaValue
            };
        }

        public static NotificationModel ToModelEntity(Notification entity)
        {
            List<EmailNotificationModel> emails = new List<EmailNotificationModel>();
            List<PhoneNotificationModel> phones = new List<PhoneNotificationModel>();
            if (entity.EmailNotifications != null)
                emails.AddRange(entity.EmailNotifications.Select(t => new EmailNotificationModel
                    {
                        IdEmailEntity = t.IdEmailEntity,
                        IdEmailNotification = t.IdEmailNotification,
                        IdNotification = t.IdNotification
                    }));
            if (entity.PhoneNotifications != null)
                phones.AddRange(entity.PhoneNotifications.Select(t => new PhoneNotificationModel
                    {
                        IdNotification = t.IdNotification,
                        IdPhoneEntity = t.IdPhoneEntity,
                        IdPhoneNotification = t.IdPhoneNotification
                    }));
            return new NotificationModel
            {
                DeltaTime = entity.TimeDelta,
                DeltaValue = entity.ValueDelta,
                EmailNotifications = emails.ToArray(),
				IdDevicesItems = entity.IdDevicesItems,
                IdNotification = entity.IdNotification,
                PhoneNotifications = phones.ToArray()
            };
        }
    }
}