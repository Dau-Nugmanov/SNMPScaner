using DAL.EfModels;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
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
