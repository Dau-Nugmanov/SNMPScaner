using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repos;
using UI.Code;

namespace UI.Controllers
{
    public class WarningsController : Controller
    {
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetLastTimeStamp()
        {
            return Json(WarningsStorage.GetCount());
        }

        [HttpPost]
        public JsonResult IsHavNewWarnings(int? currentNumber)
        {
            int curCount = WarningsStorage.GetCount();
            if ((currentNumber.HasValue && curCount > currentNumber)
                || (!currentNumber.HasValue && curCount > 0))
                return Json(new {state = true, count = curCount});
            return Json(false);
        }

        public ActionResult GetAll()
        {
            return View(WarningsStorage.GetWarnings());
        }

        public ActionResult Details(int id)
        {
            var devicesRepo = new DevicesRepository(new SnmpDbContext());
            var device = devicesRepo.GetById(id);
            return View(device);
        }
    }
}
