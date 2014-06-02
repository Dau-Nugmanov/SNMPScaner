using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;
using System.Data.Entity;

namespace DAL.Repos
{
    public class NotificationsRepository : BaseRepository<Notification>, INotificationsRepo
    {
        private SnmpDbContext _context;
        public NotificationsRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

		public Notification GetByDeviceIdAndItemId(long idDevicesItems)
        {
			return dbSet.FirstOrDefault(t => t.IdDevicesItems == idDevicesItems);
        }

        public void Edit(Notification notification)
        {
            if (notification == null)
                throw new ArgumentNullException("entity");
            var oldEntity = dbSet.Find(notification.IdNotification);
            if (oldEntity == null)
                throw new InvalidOperationException("В базе на найдена сущность с id " + notification.IdNotification);
            _context.Entry(oldEntity).CurrentValues.SetValues(notification);
            UpdateNotifications(notification);
        }

        private void UpdateNotifications(Notification notif)
        {
            var notifsRepo = new NotificationsRepository(_context);
            AddPhoneNotifications(notif.PhoneNotifications.ToList(), notif.IdNotification);
            AddEmailsNotifications(notif.EmailNotifications.ToList(), notif.IdNotification);
            RemovePhoneNotifications(notif.PhoneNotifications.ToList(), notif.IdNotification);
            RemoveEmailNotifications(notif.EmailNotifications.ToList(), notif.IdNotification);
        }

        private void RemoveEmailNotifications(List<EmailNotification> emailNotifications, long idNotification)
        {
            var emailNotifsRepo = new EmailNotificationsRepository(_context);
            var oldEmailNotifs = emailNotifsRepo.GetAllByNotifId(idNotification).ToList();
            foreach (var item in oldEmailNotifs)
            {
                if (!emailNotifications.Any(t => t.IdEmailEntity == item.IdEmailEntity))
                {
                    emailNotifsRepo.RemoveById(item.IdEmailNotification);
                }
            }
        }

        private void RemovePhoneNotifications(List<PhoneNotification> phoneNotifications, long idNotification)
        {
            var phoneNotifs = new PhoneNotificationsRepository(_context);
            var oldPhoneNotifs = phoneNotifs.GetAllByNotifId(idNotification).ToList();
            foreach (var item in oldPhoneNotifs)
            {
                if (!phoneNotifications.Any(t => t.IdPhoneEntity == item.IdPhoneEntity))
                {
                    phoneNotifs.RemoveById(item.IdPhoneNotification);
                }
            }
        }

        private void AddEmailsNotifications(List<EmailNotification> newEmailNotifications, long idNotification)
        {
            var emailNotifsRepo = new EmailNotificationsRepository(_context);
            var oldEmailNotifs = emailNotifsRepo.GetAllByNotifId(idNotification).ToList();
            foreach (var item in newEmailNotifications)
            {
                if (!oldEmailNotifs.Any(t => t.IdEmailEntity == item.IdEmailEntity))
                {
                    emailNotifsRepo.Add(item);
                }
            }
        }

        private void AddPhoneNotifications(List<PhoneNotification> newPhoneNotifs, long idNotification)
        {
            var phoneNotifsRepo = new PhoneNotificationsRepository(_context);
            var oldPhoneNotifs = phoneNotifsRepo.GetAllByNotifId(idNotification).ToList();
            foreach (var item in newPhoneNotifs)
            {
                if (!oldPhoneNotifs.Any(t => t.IdPhoneEntity == item.IdPhoneEntity))
                {
                    phoneNotifsRepo.Add(item);
                }
            }
        }

        public override void RemoveById(object id)
        {
            RemoveAllPhoneNotifications((long)id);
            RemoveAllEmailNotifications((long)id);
            base.RemoveById(id);
        }

        private void RemoveAllEmailNotifications(long idNotif)
        {
            var emailNotifsRepo = new EmailNotificationsRepository(_context);
            emailNotifsRepo
                .GetAllByNotifId(idNotif)
                .ToList()
                .ForEach(t =>
                {
                    emailNotifsRepo.RemoveById(t.IdEmailNotification);
                });
        }

        private void RemoveAllPhoneNotifications(long idNotif)
        {
            var phoneNotifsRepo = new PhoneNotificationsRepository(_context);
            phoneNotifsRepo
                .GetAllByNotifId((long)idNotif)
                .ToList()
                .ForEach(t =>
                {
                    phoneNotifsRepo.RemoveById(t.IdPhoneNotification);
                });
            
        }
	}
}
