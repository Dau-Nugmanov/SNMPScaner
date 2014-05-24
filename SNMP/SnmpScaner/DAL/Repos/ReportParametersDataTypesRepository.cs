using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
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
