using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Interfaces;

namespace DAL
{
	public class ConfigRepo : IConfigRepo
	{
		public Device[] GetAllDevices()
		{
			throw new NotImplementedException();
		}

		public SubscriptionItem[] GetAllSubscriptions(Cache cache)
		{
			throw new NotImplementedException();
		}
	}
}
