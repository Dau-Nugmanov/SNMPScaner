using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class Report
    {
        [Key]
        public int IdReport { get; set; }
        [StringLength(100)]
        public string ReportName { get; set; }
        [StringLength(100)]
        public string ReportPath { get; set; }

        public virtual ICollection<ReportParameter> ReportParameters { get; set; }
    }
}
