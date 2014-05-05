using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DomainModel
{
	/// <summary>
	/// Подписка на уведомления
	/// </summary>
	public class Subscription
	{
		private List<SubscriptionItem> _items = new List<SubscriptionItem>();
		public List<SubscriptionItem> Items
		{
			get
			{
				return _items;
			}
			set
			{
				if (value == null)
					_items.Clear();
				else
					_items = value;
			}
		}
		public Notification Notification { get; set; }
		/// <summary>
		/// Частота обновления в секундах - показывает как часто надо обновлять значение
		/// </summary>
		public long TimeDelta { get; set; }
		private DateTime _nextUpdateTime = DateTime.MinValue;

		public List<SubscriptionItem> Update()
		{
			if (DateTime.Now > _nextUpdateTime)
			{
				_nextUpdateTime = DateTime.Now.AddSeconds(TimeDelta);
				return Items
					.Where(i => i.WasChanged)
					.ToList();
			}
			return new List<SubscriptionItem>();
		}

	}
}
