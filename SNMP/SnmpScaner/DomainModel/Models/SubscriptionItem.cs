using System;
using Lextm.SharpSnmpLib;

namespace DomainModel.Models
{
	public class SubscriptionItem
	{
		public long Id { get; set; }
		/// <summary>
		/// Частота обновления в секундах - показывает как часто надо обновлять значение
		/// </summary>
		public long TimeDelta { get; set; }
		/// <summary>
		/// Разность в значении. Показывает какое изменение значения надо сохранять. Будет работать только для числовых типов
		/// </summary>
		public ISnmpData ValueDelta { get; set; }

		/// <summary>
		/// Максимальное значение. Если текуще значение больше, то генерируется уведомление с HiLevel. Будет работать только для числовых типов
		/// </summary>
		public ISnmpData HiValue { get; set; }

		/// <summary>
		/// Минимальное значение. Если текуще значение меньше, то генерируется уведомление с LoLevel. Будет работать только для числовых типов
		/// </summary>
		public ISnmpData LoValue { get; set; }

		private bool WasChanged
		{
			get
			{
				return Item != null 
					&& Item.Value != null
					&& ItemHelper.NeedUpdate(LastValue, Item.Value, ValueDelta);
			}
		}
		private DeviceItem Item { get; set; }
		private ISnmpData OldValue { get; set; }
		private ISnmpData LastValue { get; set; }
		
		public SubscriptionItem(DeviceItem item)
		{
			if (item == null) throw new ArgumentNullException("item");
			Item = item;
			LastValue = item.Value;
		}
		
		private DateTime _nextUpdateTime = DateTime.MinValue;

		public Notification Update()
		{
			int compare;
			if (DateTime.Now <= _nextUpdateTime 
				|| !WasChanged)
				return Notification.Empty;
				
			_nextUpdateTime = DateTime.Now.AddSeconds(TimeDelta);
			
			OldValue = LastValue;
			LastValue = Item.Value;

			if(ItemHelper.TryCompare(Item.Value, HiValue, out compare) && compare >= 0)
				return new Notification(Id, LastValue, OldValue, NotificationLevel.Hi);
			if (ItemHelper.TryCompare(Item.Value, LoValue, out compare) && compare <= 0)
				return new Notification(Id, LastValue, OldValue, NotificationLevel.Lo);
			return new Notification(Id, LastValue, OldValue, NotificationLevel.Undefined);
		}
	}
}
