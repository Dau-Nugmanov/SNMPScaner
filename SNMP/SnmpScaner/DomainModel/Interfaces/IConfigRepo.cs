
namespace DomainModel.Interfaces
{
	public interface IConfigRepo
	{
		Device[] GetAllDevices();
		Subscription[] GetAllSubscriptions(Cache cache);
	}
}
