using System.Linq;
using DomainModel.DalInterfaces;
using DomainModel.EfModels;

namespace DAL.Repos
{
    public class EmailsRepository : BaseRepository<EmailEntity>, IEmailsRepo
    {
        private SnmpDbContext _context;
        public EmailsRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public bool IsEmailExists(string email)
        {
            return dbSet.FirstOrDefault(t => t.Email == email) == null ? false : true;
        }
    }
}
