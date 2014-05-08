using DAL.EfModels;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomersRepository : BaseRepository<Customer>, ICustomersRepo
    {
        private SnmpDbContext _context;
        public CustomersRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public void Edit(Customer entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            var oldEntity = dbSet.Find(entity.IdCustomer);
            if (oldEntity == null)
                throw new InvalidOperationException("В базе на найдена сущность с id " + entity.IdCustomer);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }
    }
}
