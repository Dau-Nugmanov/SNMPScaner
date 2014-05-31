using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DomainModel.Interfaces;
using DomainModel.Models;
using StructureMap;

namespace DomainModel
{
	public class Cache
	{
		public const int MaxUpdateRate = 1000;

		private List<Device> _devices = new List<Device>();
		public List<Device> Devices
		{
			get { return _devices; }
			set
			{
				if (value == null)
					_devices.Clear();
				else
					_devices = value;
			}
		}

		private List<SubscriptionItem> _subscriptions = new List<SubscriptionItem>();
		public List<SubscriptionItem> Subscriptions
		{
			get { return _subscriptions; }
			set
			{
				if (value == null)
					_subscriptions.Clear();
				else
					_subscriptions = value;
			}
		}

		public Cache()
		{
			ThreadPool.QueueUserWorkItem(SyncUpdate);
			ThreadPool.QueueUserWorkItem(SyncUpdateSubscriptions);
		}

		public void SyncUpdate(object state)
		{
			var delay = 0;
			do
			{
				// sleep until next update.
				Thread.Sleep((delay > 0 && delay < MaxUpdateRate) ? MaxUpdateRate - delay : MaxUpdateRate);

				delay = Environment.TickCount;

				Devices.ForEach(d => d.SyncUpdate());

				delay = Environment.TickCount - delay;
			} while (true);
		}

		public void SyncUpdateSubscriptions(object state)
		{
			var delay = 0;
			var notificationExecutor = ObjectFactory.GetInstance<INotificationExecutor>();
			do
			{
				// sleep until next update.
				Thread.Sleep((delay > 0 && delay < MaxUpdateRate) ? MaxUpdateRate - delay : MaxUpdateRate);

				delay = Environment.TickCount;

				var res = Subscriptions
					.Select(s => s.Update())
					.Where(n => !n.Equals(Notification.Empty))
					.ToList();
				notificationExecutor.Execute(res);
				
				delay = Environment.TickCount - delay;
			} while (true);
		}
	}
}
