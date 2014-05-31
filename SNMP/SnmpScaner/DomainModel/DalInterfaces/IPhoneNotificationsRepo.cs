using System.Collections.Generic;
using DomainModel.EfModels;

namespace DomainModel.DalInterfaces
{
    public interface IPhoneNotificationsRepo
    {
        IEnumerable<PhoneNotification> GetAllByNotifId(long id);
    }
}
