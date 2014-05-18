using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.EfModels;
using DomainModel;
using DomainModel.Interfaces;

namespace DAL
{
	public class ConfigRepo : IConfigRepo
	{
		public Device[] GetAllDevices()
		{
			var devicesRepo = new DevicesRepository(new SnmpDbContext());
			var devices = devicesRepo.GetAll().ToList();
			var mapped = Mapper.Map<IEnumerable<DeviceEntity>, IEnumerable<Device>>(devices).ToArray();
			return mapped;
		}

		public SubscriptionItem[] GetAllSubscriptions(Cache cache)
		{
			return new SubscriptionItem[0];
			//throw new NotImplementedException();
		}
	}
}
