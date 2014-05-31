using DomainModel.EfModels;

namespace DAL.Repos
{
    public class ReportParametersDataTypesRepository : BaseRepository<ReportParameterDataType>
    {
        private SnmpDbContext _context;
        public ReportParametersDataTypesRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }
    }
}
