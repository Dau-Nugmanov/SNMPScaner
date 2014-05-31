using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DomainModel.EfModels;

namespace UI.Models
{
    public class ReportParameterModel : IModelEntity<ReportParameter>
    {
        public int IdReportParameter { get; set; }
        
        [Required(ErrorMessage="*")]
        [StringLength(100, ErrorMessage = "Поле не может быть длиннее 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Поле не может быть длиннее 100 символов")]
        public string PromptName { get; set; }
        
        public int IdReportParameterDataType { get; set; }

        public string ReportParameterDataTypeName { get; set; }

        public int IdReport { get; set; }

        public ReportParameter ToEfEntity()
        {
            return new ReportParameter
            {
                IdReportParameter = IdReportParameter,
                Name = Name,
                PromptName = PromptName,
                IdReportParameterDataType = IdReportParameterDataType,
                IdReport = IdReport
            };
        }

        public static ReportParameterModel ToModelEntity(ReportParameter param)
        {
            return new ReportParameterModel
            {
                IdReport = param.IdReport,
                IdReportParameter = param.IdReportParameter,
                IdReportParameterDataType = param.IdReportParameterDataType,
                Name = param.Name,
                PromptName = param.PromptName,
                ReportParameterDataTypeName = param.ReportParameterDataType.DataTypeName
            };
        }
    }
}