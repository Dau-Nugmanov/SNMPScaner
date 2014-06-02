using Lextm.SharpSnmpLib;

namespace DomainModel.Models
{
	public class Notification
	{
		public Notification(long subscriptionItemId, ISnmpData newValue, ISnmpData oldValue, NotificationLevel level)
		{
			SubscriptionItemId = subscriptionItemId;
			NewValue = newValue == null ? null : newValue.ToString();
			OldValue = oldValue == null ? null : oldValue.ToString();
			Level = level;
		}
		public long SubscriptionItemId { get; private set; }

		public string NewValue { get; private set; }
		public string OldValue { get; private set; }

		public NotificationLevel Level { get; private set; }

		private static readonly Notification _empty = new Notification(0 , null, null, NotificationLevel.Undefined);
		public static Notification Empty
		{
			get { return _empty; }
		}
	}

	public enum NotificationLevel
	{
		Undefined = 0,
		Hi,
		Lo
	}
}
