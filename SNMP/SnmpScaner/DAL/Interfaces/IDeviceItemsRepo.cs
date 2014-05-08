using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDeviceItemsRepo
    {
        IEnumerable<DeviceItemEntity> GetByIdModel(int idModel);
        void Edit(DeviceItemEntity entity);
        IEnumerable<DeviceItemEntity> GetItemsByModelId(int id);
    }
}
