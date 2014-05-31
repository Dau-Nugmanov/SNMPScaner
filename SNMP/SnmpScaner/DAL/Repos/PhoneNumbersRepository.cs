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
    public class PhoneNumbersRepository : BaseRepository<PhoneNumber>, IPhoneNumbersRepo
    {
        private SnmpDbContext _context;
        public PhoneNumbersRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public bool IsPhoneNumberExists(string number)
        {
            return dbSet.FirstOrDefault(t => t.Number == number) == null ? false : true;
        }
    }
}
