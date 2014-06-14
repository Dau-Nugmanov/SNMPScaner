using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;
using DomainModel.Interfaces;
using DomainModel.Models;
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

			var dict = new Dictionary<string, List<Notification>>();
			foreach (var notification in notify.Where(n => n.EfNotification != null
				&& n.EfNotification.EmailNotifications != null))
			{
				foreach (var email in notification.EfNotification.EmailNotifications)
				{
					if (!dict.ContainsKey(email.IdEmailEntity))
						dict.Add(email.IdEmailEntity, new List<Notification>());
					dict[email.IdEmailEntity].Add(notification.Notification);
				}
			}
			EmailNotify(dict);


			var dictPhones = new Dictionary<string, List<Notification>>();
			foreach (var notification in notify.Where(n => n.EfNotification != null
				&& n.EfNotification.PhoneNotifications != null))
			{
				foreach (var phone in notification.EfNotification.PhoneNotifications)
				{
					if (!dictPhones.ContainsKey(phone.IdPhoneEntity))
						dictPhones.Add(phone.IdPhoneEntity, new List<Notification>());
					dictPhones[phone.IdPhoneEntity].Add(notification.Notification);
				}
			}
			PhoneNotify(dictPhones);
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

		private void PhoneNotify(Dictionary<string, List<Notification>> phones)
		{
			const int error = 1;
			var sms = new SMSC();
			var tempBalance = sms.get_balance().Replace('.', ',');
			if (string.IsNullOrEmpty(tempBalance)) return;
			
			var balance = double.Parse(tempBalance);
			foreach (var phone in phones)
			{
				var number = phone.Key;
				var body = GetNotifyMessage(phone.Value.Where(n => n.Level == NotificationLevel.Hi || n.Level == NotificationLevel.Lo), true);
				if(string.IsNullOrEmpty(body)) continue;

				var tempCost = sms.get_sms_cost(number.Replace(" ", ""), body, sender:"SNMP");
				if (tempCost[error].StartsWith("-")) continue;
				if (balance < double.Parse(tempCost[0].Replace('.', ','))) continue;

				var sendResult = sms.send_sms(number, body, sender: "SNMP");
				if (!sendResult[error].StartsWith("-"))
				{
					balance = double.Parse(sendResult[3].Replace('.', ','));
				}
			}
		}

		private string GetNotifyMessage(IEnumerable<Notification> notifications, bool makeShorter = false)
		{
			var builder = new StringBuilder();
			foreach (var notification in notifications)
			{
				if(makeShorter)
					builder.AppendFormat("{0}-{1} Ст:{2} Нов:{3}\r\n", notification.Level == NotificationLevel.Hi ? "Выс": "Низ", notification.ItemName, 
						notification.OldValue, notification.NewValue);
				else
					builder.AppendFormat("{0} - {1}	Старое значение:{2} Новое значение:{3}	({4})\r\n", notification.Level, notification.ItemName,
						notification.OldValue, notification.NewValue, notification.DataType);
			}

			return builder.ToString();
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
