using System;
using System.Xml.Serialization;
using DomainModel.Interfaces;
using Lextm.SharpSnmpLib;
using StructureMap;

namespace DomainModel.Models
{
	public class DeviceItem
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public ObjectIdentifier Oid { get; set; }
		public DateTime Timestamp { get; set; }
		public ISnmpData Value { get; set; }

		/// <summary>
		/// Флаг показывающий проблему при чтении с устройства
		/// </summary>
		[XmlIgnore]
		public SnmpType? ExceptionType { get; set; }

		/// <summary>
		/// Частота обновления в секундах - показывает как часто надо обновлять значение
		/// </summary>
		public long TimeDelta { get; set; }
		/// <summary>
		/// Разность в значении. Показывает какое изменение значения надо сохранять. Будет работать только для числовых типов
		/// </summary>
		public ISnmpData ValueDelta { get; set; }

		public void UpdateValue(ISnmpData data, DateTime timestamp)
		{
			if (!ItemHelper.NeedUpdate(Timestamp, timestamp, TimeDelta, Value, data, ValueDelta)) return;
			
			Value = data;
			Timestamp = timestamp;

			ObjectFactory
				.GetInstance<IHistoryRepo>()
				.Save(Id, Value, Timestamp);
		}
	}
}
