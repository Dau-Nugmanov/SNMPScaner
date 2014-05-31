using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DomainModel.EfModels;

namespace UI.Models
{
    public class ReportModel : IModelEntity<Report>
    {

        public int IdReport { get; set; }
        
        [Required(ErrorMessage="*")]
        [StringLength(100, ErrorMessage = "Поле не может быть длиннее 100 символов")]
        [DisplayName("Название отчета")]
        public string ReportName { get; set; }

        [Required(ErrorMessage="*")]
        [StringLength(100, ErrorMessage="Поле не может быть длиннее 100 символов")]
        [DisplayName("Путь к отчету")]
        public string ReportPath { get; set; }

        public ReportParameterModel[] Parameters { get; set; }

        public Report ToEfEntity()
        {
            List<ReportParameter> reportParams = new List<ReportParameter>();
            if(Parameters != null && Parameters.Any())
                reportParams
                    .AddRange
                    (
                        Parameters
                        .Select(t => new ReportParameter
                        {
                            IdReport = t.IdReport,
                            IdReportParameter = t.IdReportParameter,
                            IdReportParameterDataType = t.IdReportParameterDataType,
                            Name = t.Name,
                            PromptName = t.PromptName,
                        })
                    );
            return new Report
            {
                IdReport = IdReport,
                ReportName = ReportName,
                ReportPath = ReportPath,
                ReportParameters = reportParams
            };
        }

        public static ReportModel ToModelEntity(Report report)
        {
            List<ReportParameterModel> paramsModel = new List<ReportParameterModel>();
            if(report.ReportParameters.Any())
                paramsModel.AddRange
                    (
                        report
                        .ReportParameters
                        .Select(t => new ReportParameterModel
                        {
                            IdReport = t.IdReport,
                            IdReportParameter = t.IdReportParameter,
                            IdReportParameterDataType = t.IdReportParameterDataType,
                            Name = t.Name,
                            PromptName = t.PromptName,
                            ReportParameterDataTypeName = t.ReportParameterDataType.DataTypeName
                        })
                    );
            return new ReportModel
            {
                IdReport = report.IdReport,
                ReportName = report.ReportName,
                ReportPath = report.ReportPath,
                Parameters = paramsModel.ToArray()
            };
        }
    }
}