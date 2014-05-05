using Lextm.SharpSnmpLib;

namespace DomainModel
{
	public class SubscriptionItem
	{
		public SubscriptionItem(DeviceItem item)
		{
			Item = item;
			OldValue = item.Value;
		}
		
		private DeviceItem Item { get; set; }
		private ISnmpData OldValue { get; set; }

		public ISnmpData GetValue()
		{
			OldValue = Item.Value;
			return OldValue;
		}
		public bool WasChanged
		{
			get
			{
				return Item != null && Item.Value!= null && !Item.Value.Equals(OldValue);
			}
		}
	}
}
