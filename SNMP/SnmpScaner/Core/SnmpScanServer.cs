using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Antlr.Runtime.JavaExtensions;
using DomainModel;
using DomainModel.Interfaces;
using DomainModel.Models;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib.Security;
using StructureMap;
using Yarn;

namespace Core
{
	public class SnmpScanServer
	{
		private readonly Cache _cache = new Cache();

		public SnmpScanServer()
		{
			var repo = ObjectFactory.GetInstance<IConfigRepo>();
			_cache.Devices.AddRange(repo.GetAllDevices());
			_cache.Subscriptions.AddRange(repo.GetAllSubscriptions(_cache));
		}

		public void AddDevice(Device device)
		{
			if(_cache.Devices.All(d => d.Id != device.Id))
				_cache.Devices.Add(device);
		}

		public void AddDeviceItem(long deviceId, DeviceItem item)
		{
			var device = _cache.Devices.FirstOrDefault(d => d.Id == deviceId);
			if(device != null && device.Items.All(i=> i.Id != item.Id))
				device.Items.Add(item);
		}

		public void RemoveDevice(long deviceId)
		{
			_cache.Devices.RemoveAll(d => d.Id == deviceId);
		}

		public void RemoveDeviceItem(long deviceId, long itemId)
		{
			var device = _cache.Devices.FirstOrDefault(d => d.Id == deviceId);
			if (device != null) device.Items.RemoveAll(i => i.Id == itemId);
		}

		public DeviceItem[] GetAllValues()
		{
			return _cache.Devices.SelectMany(d => d.Items).ToArray();
		}

		public DeviceItem[] GetAllValues(long deviceId)
		{
			return _cache.Devices.Where(d=>d.Id == deviceId).SelectMany(d => d.Items).ToArray();
		}

		public Notification[] GetAllNotifications()
		{
			return _cache.Notifications.ToArray();
		}

		public Notification[] GetAllNotifications(long deviceId)
		{
			var device = _cache.Devices.FirstOrDefault(d => d.Id == deviceId);
			if (device != null)
				return device.Items.Join(_cache.Notifications, item => item.Id, n => n.ItemId, (d, n) => n).ToArray();
			return new Notification[0];
		}


		/// <summary>
		/// Нужно для теста. потом удалю
		/// </summary>
		public void Run()
		{
			var ipAddress = Dns.GetHostAddresses("demo.snmplabs.com");
			var result = Messenger.Get(VersionCode.V1,
						   new IPEndPoint(ipAddress[0], 161),
						   new OctetString("public"),
						   new List<Variable>
						   {
							   new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.1.0")),
							   new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.3.0")),
							   new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.5.0")),
						   },
						   6000);
		}
		/// <summary>
		/// Нужно для теста. потом удалю
		/// </summary>
		public void RunAsync()
		{
			var ipAddress = Dns.GetHostAddresses("demo.snmplabs.com");
			GetRequestMessage message = new GetRequestMessage(0,
				VersionCode.V1,
				new OctetString("public"),
				new List<Variable>
				{
					new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.1.0")),
					new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.3.0")),
					new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.5.0")),
				});
			var endpoint = new IPEndPoint(ipAddress[0], 161);
			message.BeginGetResponse(endpoint, new UserRegistry(), endpoint.GetSocket(), ar =>
			{
				var response = message.EndGetResponse(ar);
				Console.WriteLine(response);
			}, null);
			Thread.Sleep(6000);
		}
	}
}
