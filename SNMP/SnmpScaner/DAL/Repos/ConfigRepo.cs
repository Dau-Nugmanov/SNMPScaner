using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.EfModels;
using DAL.Interfaces;
using DomainModel;
using DomainModel.Interfaces;

namespace DAL
{
	public class ConfigRepo : IConfigRepo
	{
		public Device[] GetAllDevices()
		{
			var devicesRepo = new DevicesRepository(new SnmpDbContext());
			var devices = devicesRepo.GetAll();
			var mapped = Mapper.Map<IEnumerable<DeviceEntity>, IEnumerable<Device>>(devices).ToArray();
			return mapped;
		}

		public SubscriptionItem[] GetAllSubscriptions(Cache cache)
		{
			throw new NotImplementedException();
		}
	}
}
