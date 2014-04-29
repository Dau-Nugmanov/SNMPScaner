using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Interfaces;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using StructureMap;

namespace DomainModel
{
	public class DeviceItem
	{
		public long Id { get; set; }
		public ObjectIdentifier Oid { get; set; }
		
		public DateTime Timestamp { get; private set; }
		public ISnmpData Value { get; private set; }

		public void UpdateValue(ISnmpData data)
		{
			if(Value != null && Value.Equals(data)) return;
			
			Value = data;
			Timestamp = DateTime.Now;
			ObjectFactory
				.GetInstance<IHistoryRepo>()
				.Save(Id, Value, Timestamp);
		}
	}
}
