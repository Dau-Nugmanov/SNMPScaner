using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DAL.Repos;
using UI.Models;

namespace UI.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        public ActionResult Index()
        {
            var repRepo = new ReportsRepository(new SnmpDbContext());
            return View(repRepo.GetAll().ToList());
        }

        public ActionResult ShowReport(int id)
        {
            var repRep = new ReportsRepository(new SnmpDbContext());
            var report = repRep.GetById(id);
            if (report == null)
                return HttpNotFound();
            return View(ReportModel.ToModelEntity(report));
        }

        [HttpPost]
        public ActionResult ShowReport(ReportModel report, FormCollection form)
        {
            var repsRepo = new ReportsRepository(new SnmpDbContext());
            var rep = repsRepo.GetById(report.IdReport);
            if (rep == null)
                return HttpNotFound();
            //List<ReportParametersWrap> paramsWrap = new List<ReportParametersWrap>();
            Dictionary<string, object> reportParameters = new Dictionary<string,object>();
            foreach (var item in rep.ReportParameters)
            {
                reportParameters.Add(item.Name, form[item.Name]);
                //paramsWrap.Add(new ReportParametersWrap { ParameterName = item.Name, ParameterValue = form[item.Name] });
            }
            Session["report"] = rep;
            Session["parameters"] = reportParameters;
            return Redirect("~/Reports.aspx");
        }

        [Authorize(Roles="Admin")]
        public ActionResult List()
        {
            var reportsRepo = new ReportsRepository(new SnmpDbContext());
            return View(reportsRepo.GetAll().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            FillDataTypesDdl();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(ReportModel report)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder errors = new StringBuilder();
                ModelState.Where(t => t.Value.Errors.Count > 0).ToList().ForEach(x =>
                {
                    foreach (var item in x.Value.Errors)
                    {
                        errors.Append(item.ErrorMessage);
                    }
                });
                ViewData["message_error"] = "Пожалуйста, заполните форму корректными данными: " + errors.ToString();
                return View(report);
            }
            var reportsRepository = new ReportsRepository(new SnmpDbContext());
            reportsRepository.Add(report.ToEfEntity());
            reportsRepository.SaveChanges();
            ViewData["message"] = "Отчет успешно добавлен";
            FillDataTypesDdl();
            return View(report);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var repRepository = new ReportsRepository(new SnmpDbContext());
            var report = repRepository.GetById(id);
            FillDataTypesDdl();
            return View(ReportModel.ToModelEntity(report));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(ReportModel report)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder errors = new StringBuilder();
                ModelState.Where(t => t.Value.Errors.Count > 0).ToList().ForEach(x =>
                {
                    foreach (var item in x.Value.Errors)
                    {
                        errors.Append(item.ErrorMessage);
                    }
                });
                ViewData["message_error"] = "Пожалуйста, заполните форму корректными данными: " + errors.ToString();
                return View(report);
            }
            var repRepository = new ReportsRepository(new SnmpDbContext());
            repRepository.Edit(report.ToEfEntity());
            repRepository.SaveChanges();
            ViewData["message"] = "Отчет успешно изменен";
            FillDataTypesDdl();
            return View(report);
        }

        public void FillDataTypesDdl()
        {
            var dtRepo = new ReportParametersDataTypesRepository(new SnmpDbContext());
            var selectDataTypes = new SelectList(dtRepo.GetAll().ToList(), "IdReportParameterDataType", "DataTypeName");
            ViewData["dataTypes"] = selectDataTypes;
        }
    }
}
