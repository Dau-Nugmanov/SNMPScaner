using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repos;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;

namespace DAL
{
    public class PhoneNotificationsRepository : BaseRepository<PhoneNotification>, IPhoneNotificationsRepo
    {
        private SnmpDbContext _context;
        public PhoneNotificationsRepository(SnmpDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<PhoneNotification> GetAllByNotifId(long id)
        {
            return dbSet
                .Where(t => t.IdNotification == id)
                .AsEnumerable<PhoneNotification>();
        }
    }
}
