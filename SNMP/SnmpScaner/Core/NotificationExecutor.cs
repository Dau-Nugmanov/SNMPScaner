using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;
using DomainModel.Interfaces;
using StructureMap;
using Notification = DomainModel.Models.Notification;

namespace Core
{
	public class NotificationExecutor : INotificationExecutor
	{
		public void Execute(IEnumerable<Notification> notifications)
		{
			var notificationRepo = ObjectFactory.GetInstance<INotificationsRepo>();

			var notify = notifications
				.Select(n => new { EfNotification = notificationRepo.GetById(n.SubscriptionItemId), Notification = n })
				.ToList();

			if(notify.Count <= 0)
				return;
			

			//var phones = notify
			//	.Where(n=>n.EfNotification.PhoneNotifications != null)
			//	.Select(n =>
			//	new
			//	{
			//		Phones = n.
			//			EfNotification
			//			.PhoneNotifications
			//			.Select(p => p.PhoneNumber)
			//			.Distinct(new PhoneNumberComparer()),
			//		n.Notification
			//	})
			//	.ToList();
			
			//phones
			//	.ForEach(p=> PhoneNotify(p.Phones, p.Notification));

			var dict = new Dictionary<string, List<Notification>>();
			foreach (var notification in notify.Where(n=>n.EfNotification != null 
				&& n.EfNotification.EmailNotifications != null))
			{
				foreach (var email in notification.EfNotification.EmailNotifications)
				{
					if(!dict.ContainsKey(email.IdEmailEntity))
						dict.Add(email.IdEmailEntity, new List<Notification>());
					dict[email.IdEmailEntity].Add(notification.Notification);
				}
			}
			EmailNotify(dict);
		}

		private void EmailNotify(Dictionary<string, List<Notification>> emails)
		{
			foreach (var email in emails)
			{
				var fromAddress = new MailAddress("SNMPEmailTest@gmail.com", "From Name");
				var toAddress = new MailAddress(email.Key, "To Name");
				const string fromPassword = "123SNMP321";
				const string subject = "Уведомление";
				var body = GetNotifyMessage(email.Value);

				var smtp = new SmtpClient
				{
					Host = "smtp.gmail.com",
					Port = 587,
					EnableSsl = true,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
					Timeout = 20000
				};
				using (var message = new MailMessage(fromAddress, toAddress)
				{
					Subject = subject,
					Body = body
				})
				{
					smtp.Send(message);
				}
			}
		}

		private string GetNotifyMessage(IEnumerable<Notification> notifications)
		{
			var builder = new StringBuilder();
			foreach (var notification in notifications)
			{
				builder.AppendFormat("{0} - {1}	Старое значение:{2} Новое значение:{3}	({4})\r\n", notification.Level, notification.ItemName, 
					notification.OldValue, notification.NewValue, notification.DataType);
			}

			return builder.ToString();
		}

		private void PhoneNotify(IEnumerable<PhoneNumber> phones, Notification notification)
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
