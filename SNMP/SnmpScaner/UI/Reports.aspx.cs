using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class Reports : System.Web.UI.Page
    {
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

            List<Microsoft.Reporting.WebForms.ReportParameter> parameters = new List<Microsoft.Reporting.WebForms.ReportParameter>();

            foreach (var item in reportParameters.Keys)
            {
                Microsoft.Reporting.WebForms.ReportParameter p = new Microsoft.Reporting.WebForms.ReportParameter();
                p.Name = item;
                p.Values.Add(reportParameters[item].ToString());
                parameters.Add(p);
            }

            ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://alborozd/ReportServer_SQL12");
            ReportViewer1.ServerReport.ReportPath = "/DiplomReports/" + report.ReportPath;//"/DiplomReports/BestReport";
            
            //Microsoft.Reporting.WebForms.ReportParameter parameter = new ReportParameter();
            //parameter.Name = "IdServer";
            //parameter.Values.Add("1");

            ReportViewer1.ServerReport.SetParameters(parameters);
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;

            //var notificationsRepository = new NotificationsRepository(new DAO.DeployAppEntities());

            //if (!IsPostBack)
            //{
            //    chklstUsers.DataSource = notificationsRepository.GetAll().Select(t => new { email = t.Email, name = t.Name });
            //    chklstUsers.DataBind();
            //}
        }

        public byte[] Export(ReportViewer viewer, string exportType)//, string reportsTitle)
        {
            //try
            //{
                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;
                // just gets the Report title... make your own method
                //ReportViewer needs a specific type (not always the same as the extension)

                filetype = exportType == "PDF" ? "PDF" : exportType;
                byte[] bytes = viewer.ServerReport.Render(filetype, null, // deviceinfo not needed for csv
                out mimeType, out encoding, out extension, out streamIds, out warnings);
                //System.Web.HttpContext.Current.Response.Buffer = true;
                //System.Web.HttpContext.Current.Response.Clear();
                //System.Web.HttpContext.Current.Response.ContentType = mimeType;
                //System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + reportsTitle + "." + exportType);
                //System.Web.HttpContext.Current.Response.BinaryWrite(bytes);
                
                //FileStream fs = new FileStream(Server.MapPath("~/GeneratedFiles/" + reportsTitle + "." + exportType), FileMode.OpenOrCreate);
                return bytes;
                //fs.Write(bytes, 0, bytes.Length);
                //fs.Close();

                // System.Web.HttpContext.Current.Response.Flush();
                // System.Web.HttpContext.Current.Response.End();
            //}
            //catch (Exception ex)
            //{
            //}
            //return true;
            
        }

       
     

    }
}