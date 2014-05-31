using System.Collections.Generic;
using DomainModel.EfModels;

namespace DomainModel.DalInterfaces
{
    public interface IEmailNotificationsRepo
    {
        IEnumerable<EmailNotification> GetAllByNotifId(long id);
    }
}
