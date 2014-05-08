using DAL.EfModels;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
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
    }
}
