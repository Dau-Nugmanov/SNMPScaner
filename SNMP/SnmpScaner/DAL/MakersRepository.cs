using DAL.EfModels;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MakersRepository : BaseRepository<Maker>, IMakersRepository
    {
        private SnmpDbContext _context;
        public MakersRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public void Edit(Maker entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            var oldEntity = dbSet.Find(entity.IdMaker);
            if (oldEntity == null)
                throw new InvalidOperationException("В базе на найдена сущность с id " + entity.IdMaker);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }
    }
}
