using System.Linq;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;

namespace DAL.Repos
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
