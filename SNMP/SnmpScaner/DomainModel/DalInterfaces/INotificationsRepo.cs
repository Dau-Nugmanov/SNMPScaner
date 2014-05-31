namespace DomainModel.DalInterfaces
{
    public interface INotificationsRepo
    {
		EfModels.Notification GetByDeviceIdAndItemId(long idDevicesItems);
        void Edit(EfModels.Notification notification);
	    EfModels.Notification GetById(object id);
    }
}
