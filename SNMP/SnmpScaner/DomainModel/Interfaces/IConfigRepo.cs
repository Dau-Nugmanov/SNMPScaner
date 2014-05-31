
using DomainModel.Models;

namespace DomainModel.Interfaces
{
	public interface IConfigRepo
	{
		Device[] GetAllDevices();
		SubscriptionItem[] GetAllSubscriptions(Cache cache);
	}
}
