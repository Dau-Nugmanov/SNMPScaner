using System.Collections.Generic;
using DomainModel.EfModels;

namespace DomainModel.DalInterfaces
{
    public interface IDevicesItemsRepo
    {
        void Edit(DevicesItems entity);
        IEnumerable<DevicesItems> GetByDeviceId(long id);

    }
}
