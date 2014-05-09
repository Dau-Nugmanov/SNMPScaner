using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IEmailNotificationsRepo
    {
        IEnumerable<EmailNotification> GetAllByNotifId(long id);
    }
}
