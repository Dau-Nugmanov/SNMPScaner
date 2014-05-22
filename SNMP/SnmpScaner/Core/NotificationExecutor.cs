using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.EfModels;
using DAL.Interfaces;
using DomainModel.Interfaces;
using StructureMap;
using Notification = DomainModel.Notification;

namespace Core
{
	public class NotificationExecutor : INotificationExecutor
	{
		private Dictionary<User, List<Notification>> _users = new Dictionary<User, List<Notification>>();
		public void Execute(IEnumerable<Notification> notifications)
		{
			//var notificationRepo = ObjectFactory.GetInstance<INotificationsRepo>();
			
			//var notify = notifications
			//	.Select(n=>new { EfNotification = notificationRepo.GetById(n.SubscriptionItemId), Notification = n})
			//	.ToList();

			//var phones = notify.Select(n => 
			//	new 
			//	{
			//		Phones = n.
			//			EfNotification
			//			.PhoneNotifications
			//			.Select(p => p.PhoneNumber)
			//			.Distinct(new PhoneNumberComparer()), 
			//		n.Notification});


			//var emails = notify.Select(n =>
			//	new
			//	{
			//		Emails = n.
			//			EfNotification
			//			.EmailNotifications
			//			.Select(p => p.EmailEntity)
			//			.Distinct(new EmailEntityComparer()),
			//		n.Notification
			//	});
		}

		private void EmailNotify(IEnumerable<DAL.EfModels.EmailEntity> email, Notification notification)
		{
			
		}

		private void PhoneNotify(IEnumerable<DAL.EfModels.PhoneNumber> email, Notification notification)
		{

		}
	}

	class PhoneNumberComparer : IEqualityComparer<PhoneNumber>
	{
		// Products are equal if their names and product numbers are equal. 
		public bool Equals(PhoneNumber x, PhoneNumber y)
		{
			if (Object.ReferenceEquals(x, y)) return true;
			if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
				return false;

			return x.Number == y.Number;
		}

		// If Equals() returns true for a pair of objects  
		// then GetHashCode() must return the same value for these objects. 
		public int GetHashCode(PhoneNumber phoneNumber)
		{
			if (Object.ReferenceEquals(phoneNumber, null)) return 0;
			return phoneNumber.Number.GetHashCode();
		}

	}

	class EmailEntityComparer : IEqualityComparer<EmailEntity>
	{
		// Products are equal if their names and product numbers are equal. 
		public bool Equals(EmailEntity x, EmailEntity y)
		{
			if (Object.ReferenceEquals(x, y)) return true;
			if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
				return false;

			return x.Email == y.Email;
		}

		// If Equals() returns true for a pair of objects  
		// then GetHashCode() must return the same value for these objects. 
		public int GetHashCode(EmailEntity phoneNumber)
		{
			if (Object.ReferenceEquals(phoneNumber, null)) return 0;
			return phoneNumber.Email.GetHashCode();
		}

	}
}
