using System;
using DomainModel.EfModels;
using DomainModel.Interfaces;
using Lextm.SharpSnmpLib;

namespace DAL.Repos
{
	public class HistoryRepo : IHistoryRepo
	{
		public void Save(long id, ISnmpData value, DateTime timestamp)
		{
			var context = new SnmpDbContext();
			var historyItem = context.ParametersHistory.Add(new DeviceItemHistory
			{
				IdDevicesItems = id,
				Timestamp = timestamp,
				Value = value.ToString()
			});
			context.SaveChanges();
		}
	}
}
