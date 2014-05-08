using System.Collections.Generic;
using Lextm.SharpSnmpLib;

namespace DomainModel
{
	public class DeviceModel
	{
		public long Id { get; set; }
		public string Name { get; set; }

		public Maker Maker { get; set; }

		private List<DeviceModelItem> _items = new List<DeviceModelItem>();
		public List<DeviceModelItem> Items
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

		/// <summary>
		/// Список с типами, для которых вычисляется разность значений
		/// </summary>
		public static readonly List<SnmpType> ValueTypes = new List<SnmpType>
		{
			SnmpType.Counter32, SnmpType.Counter64, SnmpType.Gauge32,SnmpType.Integer32, SnmpType.TimeTicks
		};
	}
}
