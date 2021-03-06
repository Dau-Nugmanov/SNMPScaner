﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DomainModel;
using DomainModel.EfModels;
using DomainModel.Interfaces;
using DomainModel.Models;

namespace DAL.Repos
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
			var repo = new NotificationsRepository(new SnmpDbContext());
			var notifications =  repo.GetAll().ToList();
			
			var mapped = Mapper.Map<IEnumerable<DomainModel.EfModels.Notification>, IEnumerable<SubscriptionItem>>(notifications).ToArray();
			foreach (var subscriptionItem in mapped)
			{
				var deviceItems = cache.Devices.SelectMany(d => d.Items);
				var notify = notifications.FirstOrDefault(n => n.IdNotification == subscriptionItem.Id);
				subscriptionItem.InitItem(deviceItems.FirstOrDefault(i=>i.Id == notify.IdDevicesItems));
			}
			return mapped;
		}
	}
}
