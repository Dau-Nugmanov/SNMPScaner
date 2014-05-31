using DomainModel.EfModels;

namespace DomainModel.DalInterfaces
{
    public interface IDevicesRepo
    {
        void Edit(DeviceEntity entity);
    }
}
