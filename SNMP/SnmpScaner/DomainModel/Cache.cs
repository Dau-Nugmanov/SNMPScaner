using System;
using System.Collections.Generic;
using System.Threading;

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

		public Cache()
		{
			ThreadPool.QueueUserWorkItem(SyncUpdate);
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
	}
}
