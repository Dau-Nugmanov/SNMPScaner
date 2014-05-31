using System;
using DomainModel.EfModels;

namespace DAL.Repos
{
    public class ReportParametersRepository : BaseRepository<ReportParameter>
    {
        private SnmpDbContext _context;
        public ReportParametersRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public void Edit(ReportParameter entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            var oldEntity = dbSet.Find(entity.IdReportParameter);
            if (oldEntity == null)
                throw new InvalidOperationException("В базе на найдена сущность с id " + entity.IdReportParameter);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }
    }
}
