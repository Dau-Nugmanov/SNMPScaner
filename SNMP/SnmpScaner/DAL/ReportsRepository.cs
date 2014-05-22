using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL
{
    public class ReportsRepository : BaseRepository<Report>
    {
        private SnmpDbContext _context;
        public ReportsRepository(SnmpDbContext context)
            :base(context)
        {
            _context = context;
        }

        public void Edit(Report report)
        {
            if (report == null)
                throw new ArgumentNullException("report");
            var oldEntity = dbSet.Find(report.IdReport);
            if (oldEntity == null)
                throw new InvalidOperationException("В базе на найдена сущность с id " + report.IdReport);
            _context.Entry(oldEntity).CurrentValues.SetValues(report);
            UpdateReportParameters(report);
        }

        private void UpdateReportParameters(Report report)
        {
            var paramsRepo = new ReportParametersRepository(_context);
            var oldParams = paramsRepo.GetAll().ToList();
            UpdateParametersValues(report.ReportParameters.ToList());
            RemoveParameters(report.ReportParameters.ToList(), oldParams);
            AddParameters(report.ReportParameters.ToList());
        }

        private void UpdateParametersValues(List<ReportParameter> parameters)
        {
            var paramsRepo = new ReportParametersRepository(_context);
            parameters
                .Where(t => t.IdReportParameter != 0)
                .ToList()
                .ForEach(t => 
                {
                    paramsRepo.Edit(t);
                });
        }

        private void RemoveParameters(List<ReportParameter> newParameters, List<ReportParameter> oldParameters)
        {
            var paramsRepo = new ReportParametersRepository(_context);
            foreach (var oldParam in oldParameters)
            {
                if (!newParameters.Select(t => t.IdReportParameter).Contains(oldParam.IdReportParameter))
                {
                    paramsRepo.RemoveById(oldParam.IdReportParameter);
                }
            }
        }

        public void AddParameters(List<ReportParameter> parameters)
        {
            var paramsRepo = new ReportParametersRepository(_context);
            parameters
                .Where(t => t.IdReportParameter == 0)
                .ToList()
                .ForEach(t =>
                {
                    paramsRepo.Add(t);
                });
        }

        public override IEnumerable<Report> GetAll()
        {
            return dbSet
                .Include(t => t.ReportParameters)
                .Include(t => t.ReportParameters.Select(q => q.ReportParameterDataType));
        }

        public override Report GetById(object id)
        {
            return dbSet
                .Include(t => t.ReportParameters)
                .Include(t => t.ReportParameters.Select(q => q.ReportParameterDataType))
                .FirstOrDefault(t => t.IdReport == (int)id);
        }
    }
}
