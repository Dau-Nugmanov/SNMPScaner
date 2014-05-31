using System.Collections.Generic;
using DomainModel.EfModels;

namespace DomainModel.DalInterfaces
{
    public interface IDeviceModelsRepo
    {
        void Edit(DeviceModel entity);
        IEnumerable<DeviceModel> GetAllByMakerId(int id);
    }
}
