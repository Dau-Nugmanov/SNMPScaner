using System.Collections.Generic;
using System.Linq;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;

namespace DAL.Repos
{
    public class EmailNotificationsRepository : BaseRepository<EmailNotification>, IEmailNotificationsRepo
    {
        private SnmpDbContext _context;
        public EmailNotificationsRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        //public void RemoveItemNotifications(long idItem)
        //{
        //    dbSet
        //        .Where(t => t.IdDeviceItemEntity == idItem)
        //        .ToList()
        //        .ForEach(t =>
        //        {
        //            dbSet.Remove(t);
        //        });
        //}

        public IEnumerable<EmailNotification> GetAllByNotifId(long id)
        {
            return dbSet
                .Where(t => t.IdNotification == id)
                .AsEnumerable<EmailNotification>();
        }
    }
}
