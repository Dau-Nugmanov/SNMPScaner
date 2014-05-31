using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.EfModels
{
    public class ReportParameterDataType
    {
        [Key]
        public int IdReportParameterDataType { get; set; }
        public string DataTypeName { get; set; }

        public virtual ICollection<ReportParameter> ReportParameters { get; set; }
    }
}
