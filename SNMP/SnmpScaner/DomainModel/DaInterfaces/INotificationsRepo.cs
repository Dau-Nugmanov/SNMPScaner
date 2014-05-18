using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface INotificationsRepo
    {
        Notification GetByDeviceIdAndItemId(int idDevice, int idItem);
        void Edit(Notification notification);
    }
}
