using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfModels
{
    public class ReportParameter
    {
        [Key]
        public int IdReportParameter { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string PromptName { get; set; }
        public int IdReportParameterDataType { get; set; }
        public int IdReport { get; set; }

        [ForeignKey("IdReport")]
        public virtual Report Report { get; set; }

        [ForeignKey("IdReportParameterDataType")]
        public virtual ReportParameterDataType ReportParameterDataType { get; set; }
    }
}
