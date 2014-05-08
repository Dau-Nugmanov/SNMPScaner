using Lextm.SharpSnmpLib;

namespace DomainModel
{
	public class DeviceModelItem
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public ObjectIdentifier Oid { get; set; }
	}
}
