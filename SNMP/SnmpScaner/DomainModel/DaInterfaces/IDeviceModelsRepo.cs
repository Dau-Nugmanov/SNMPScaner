using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDeviceModelsRepo
    {
        void Edit(DeviceModel entity);
        IEnumerable<DeviceModel> GetAllByMakerId(int id);
    }
}
