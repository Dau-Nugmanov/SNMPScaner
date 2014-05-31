using System.Collections.Generic;
using DomainModel.EfModels;

namespace DomainModel.DalInterfaces
{
    public interface IDeviceItemsRepo
    {
        IEnumerable<DeviceItemEntity> GetByIdModel(int idModel);
        void Edit(DeviceItemEntity entity);
        IEnumerable<DeviceItemEntity> GetItemsByModelId(int id);
    }
}
