using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;

namespace DomainModel
{
	public class Device
	{
		public string Name { get; set; }
		public IPAddress Ip { get; set; }
		public VersionCode VersionCode { get; set; }
		public int Port { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public int Timeout { get; set; }

		private List<DeviceItem> _items = new List<DeviceItem>();
		public List<DeviceItem> Items
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

		public void SyncUpdate()
		{
			var result = Messenger.Get(VersionCode,
				new IPEndPoint(Ip, Port),
				new OctetString("public"),
				Items
					.Select(i=> new Variable(i.Oid))
					.ToList(),
				Timeout);

			_items
				.Join(result, item => item.Oid, variable => variable.Id,
					(item, variable) => new{item, variable})
				.ToList()
				.ForEach(p => p.item.UpdateValue(p.variable.Data));
		}
	}
}
