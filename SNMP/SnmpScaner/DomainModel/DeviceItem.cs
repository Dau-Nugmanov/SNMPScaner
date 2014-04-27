using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib;

namespace DomainModel
{
	public class DeviceItem
	{
		public ObjectIdentifier Oid { get; set; }
		public SnmpType Type { get; set; }

		public DateTime Timestamp { get; private set; }
		public object Value { get; private set; }

		public void UpdateValue(object value)
		{
			Value = value;
			Timestamp = DateTime.Now;
		}
	}
}
