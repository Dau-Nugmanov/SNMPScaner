using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI.Helpers;

namespace WebApplication
{
    public partial class Reports : System.Web.UI.Page
    {
        private string _reportName = "report";
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL.EfModels.Report report = null;
            Dictionary<string, object> reportParameters = null;

            if (Session["parameters"] == null)
                throw new InvalidOperationException("Список ппараметров пуст");
            reportParameters = (Dictionary<string, object>)Session["parameters"];

            if (Session["report"] == null)
                throw new InvalidOperationException("Отчет не задан");
            report = (DAL.EfModels.Report)Session["report"];
            _reportName = report.ReportName;
            List<Microsoft.Reporting.WebForms.ReportParameter> parameters = new List<Microsoft.Reporting.WebForms.ReportParameter>();

            foreach (var item in reportParameters.Keys)
            {
                Microsoft.Reporting.WebForms.ReportParameter p = new Microsoft.Reporting.WebForms.ReportParameter();
                p.Name = item;
                p.Values.Add(reportParameters[item].ToString());
                parameters.Add(p);
            }

            ReportViewer1.ServerReport.ReportServerUrl = new Uri(AppSettingsHelper.GetReportingServerUrl);
            ReportViewer1.ServerReport.ReportPath = AppSettingsHelper.GetReportsPath + "/" + report.ReportPath;//"/DiplomReports/BestReport";
            
            ReportViewer1.ServerReport.SetParameters(parameters);
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;
        }

        public byte[] Export(ReportViewer viewer, string exportType)//, string reportsTitle)
        {
                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;
                filetype = exportType == "PDF" ? "PDF" : exportType;
                byte[] bytes = viewer.ServerReport.Render(filetype, null, // deviceinfo not needed for csv
                out mimeType, out encoding, out extension, out streamIds, out warnings);
                return bytes; 
        }

        protected void btn_export_Click(object sender, EventArgs e)
        {
            string fileName = _reportName;
            switch (DropDownList1.SelectedValue)
            {
                case "PDF":
                    fileName += ".pdf";
                    break;
                case "Excel":
                    fileName += ".xls";
                    break;
                case "Word":
                    fileName += ".doc";
                    break;
            }

            Page.Response.Clear();
            Page.Response.ClearContent();
            Page.Response.ClearHeaders();
            Page.Response.HeaderEncoding = System.Text.Encoding.Default;
            Page.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
            Page.Response.ContentType = "application/octet-stream";
            Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1251");
            byte[] buf = Export(ReportViewer1, DropDownList1.SelectedValue);
            Page.Response.AddHeader("Content-Length", buf.Length.ToString());
            Page.Response.BinaryWrite(buf);
            Page.Response.End();
        }
    }
}