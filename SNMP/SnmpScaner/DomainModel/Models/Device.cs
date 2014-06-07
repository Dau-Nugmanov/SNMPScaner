using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;

namespace DomainModel.Models
{
	public class Device
	{
		/// <summary>
		/// Id в БД
		/// </summary>
		public long Id { get; set; }
		
		public IPAddress Ip { get; set; }
		public VersionCode VersionCode { get; set; }
		public int Port { get; set; }
		
		private int _timeout = 6000;
		public int Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}

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

		private DateTime _nextBadItemsUpdateTry = DateTime.MinValue;
		private const long BadItemsUpdateFrequency = 600;

		public void SyncUpdate()
		{
			SyncUpdate(Items.Where(i => !i.ExceptionType.HasValue));
			
			if (DateTime.Now <= _nextBadItemsUpdateTry) return;
			SyncUpdate(Items.Where(i => i.ExceptionType.HasValue));
			_nextBadItemsUpdateTry = DateTime.Now.AddSeconds(BadItemsUpdateFrequency);
		}

		private void SyncUpdate(IEnumerable<DeviceItem> items)
		{
			try
			{
				var result = Messenger.Get(VersionCode,
					new IPEndPoint(Ip, Port),
					new OctetString("public"),
					items
						.Select(i => new Variable(i.Oid))
						.ToList(),
					Timeout);

				var timestamp = DateTime.Now;

				_items
					.Join(result, item => item.Oid, variable => variable.Id,
						(item, variable) => new { item, variable })
					.ToList()
					.ForEach(p =>
					{
						p.item.UpdateValue(p.variable.Data, timestamp);
						p.item.ExceptionType = null;
					});
			}
			catch (ErrorException e)
			{

				var timestamp = DateTime.Now;
				//Список значений с ошибками
				var invalidItems = e.Body.Scope.Pdu.Variables
					.Where(v => v.Data.TypeCode == SnmpType.NoSuchObject
						|| v.Data.TypeCode == SnmpType.NoSuchInstance
						|| v.Data.TypeCode == SnmpType.EndOfMibView
						|| v.Data.TypeCode == SnmpType.Null)
					.ToList();

				//Помечаем DeviceItem как ошибочные чтобы их больше не читать
				_items
					.Join(invalidItems, item => item.Oid, variable => variable.Id,
						(item, variable) => new { item, variable })
					.ToList()
					.ForEach(p => p.item.ExceptionType = p.variable.Data.TypeCode);

				//Пытаемся сохранить то что осталось
				_items
					.Join(e.Body.Scope.Pdu.Variables.Except(invalidItems), item => item.Oid, variable => variable.Id,
						(item, variable) => new { item, variable })
					.ToList()
					.ForEach(p =>
					{
						p.item.UpdateValue(p.variable.Data, timestamp);
						p.item.ExceptionType = null;
					});
			}
		}
	}
}
