using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PhoneNotificationsRepository : BaseRepository<PhoneNotification>
    {
        private SnmpDbContext _context;
        public PhoneNotificationsRepository(SnmpDbContext context)
            : base(context)
        {
            _context = context;
        }

        //public void RemoveByItemId(long idItem)
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
