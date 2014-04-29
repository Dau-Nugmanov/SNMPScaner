using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;

namespace DomainModel
{
	public class DeviceItem
	{
		public ObjectIdentifier Oid { get; set; }
		
		public DateTime Timestamp { get; private set; }
		public object Value { get; private set; }

		public void UpdateValue(ISnmpData data)
		{
			if (!ValueChanged(Value, data)) return;
			
			Value = data;
			Timestamp = DateTime.Now;
		}

		private static bool ValueChanged(object currentValue, ISnmpData newValue)
		{
			switch (newValue.TypeCode)
			{
				case SnmpType.Counter32:
					return currentValue as Counter32 != newValue as Counter32;
				case SnmpType.Counter64:
					return currentValue as Counter64 != newValue as Counter64;
				case SnmpType.EndMarker:
					return false;
				case SnmpType.EndOfMibView:
					return currentValue as EndOfMibView != newValue as EndOfMibView;
				case SnmpType.Gauge32:
					return currentValue as Gauge32 != newValue as Gauge32;
				case SnmpType.GetBulkRequestPdu:
					return currentValue as GetBulkRequestPdu != newValue as GetBulkRequestPdu;
				case SnmpType.GetNextRequestPdu:
					return currentValue as GetNextRequestPdu != newValue as GetNextRequestPdu;
				case SnmpType.GetRequestPdu:
					return currentValue as GetRequestPdu != newValue as GetRequestPdu;
				case SnmpType.IPAddress:
					return currentValue as IP != newValue as IP;
				case SnmpType.InformRequestPdu:
					return currentValue as InformRequestPdu != newValue as InformRequestPdu;
				case SnmpType.Integer32:
					return currentValue as Integer32 != newValue as Integer32;
				case SnmpType.NetAddress:
					return false;
				case SnmpType.NoSuchInstance:
					return currentValue as NoSuchInstance != newValue as NoSuchInstance;
				case SnmpType.NoSuchObject:
					return currentValue as NoSuchObject != newValue as NoSuchObject;
				case SnmpType.Null:
					return currentValue as Null != newValue as Null;
				case SnmpType.ObjectIdentifier:
					return currentValue as ObjectIdentifier != newValue as ObjectIdentifier;
				case SnmpType.OctetString:
					return currentValue as OctetString != newValue as OctetString;
				case SnmpType.Opaque:
					return currentValue as Opaque != newValue as Opaque;
				case SnmpType.ReportPdu:
					return currentValue as ReportPdu != newValue as ReportPdu;
				case SnmpType.ResponsePdu:
					return currentValue as ResponsePdu != newValue as ResponsePdu;
				case SnmpType.Sequence:
					return currentValue as Sequence != newValue as Sequence;
				case SnmpType.SetRequestPdu:
					return currentValue as SetRequestPdu != newValue as SetRequestPdu;
				case SnmpType.TimeTicks:
					return currentValue as TimeTicks != newValue as TimeTicks;
				case SnmpType.TrapV1Pdu:
					return currentValue as TrapV1Pdu != newValue as TrapV1Pdu;
				case SnmpType.TrapV2Pdu:
					return currentValue as TrapV2Pdu != newValue as TrapV2Pdu;
			}
			return false;
		}
	}
}
