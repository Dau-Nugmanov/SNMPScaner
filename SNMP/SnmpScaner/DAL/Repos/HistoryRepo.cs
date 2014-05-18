using System;
using DAL.EfModels;
using DomainModel.Interfaces;
using Lextm.SharpSnmpLib;

namespace DAL.Repos
{
	public class HistoryRepo : IHistoryRepo
	{
		public void Save(long id, ISnmpData value, DateTime timestamp)
		{
			//var context = new SnmpDbContext();
			//var repo = new DevicesItemsRepository(context);
			//var deviceItem = repo.GetById(id);

			//var historyItem =  context.ParametersHistory.Add(new DeviceItemHistory
			//{
			//	IdDeviceEntity = deviceItem.IdDeviceEntity,
			//	IdDeviceItemEntity = deviceItem.IdDeviceItemEntity,
			//	Timestamp = timestamp,
			//	Value = value.ToString()
			//});
			//context.SaveChanges();
		}
	}
}
