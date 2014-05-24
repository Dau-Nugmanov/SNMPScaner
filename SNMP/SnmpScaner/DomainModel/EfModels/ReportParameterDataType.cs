using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class ReportParameterDataType
    {
        [Key]
        public int IdReportParameterDataType { get; set; }
        public string DataTypeName { get; set; }

        public virtual ICollection<ReportParameter> ReportParameters { get; set; }
    }
}
