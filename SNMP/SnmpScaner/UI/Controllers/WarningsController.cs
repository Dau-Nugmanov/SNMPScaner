using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repos;
using UI.Code;
using UI.Models;

namespace UI.Controllers
{
    [Authorize]
    public class WarningsController : Controller
    {
        public ActionResult GetAllDevices()
        {
            DevicesRepository devicesRepo = new DevicesRepository(new SnmpDbContext());
            return View(devicesRepo.GetAll().ToList());
        }

        public ActionResult DetailsDevice(int id)
        {
            var devicesRepo = new DevicesRepository(new SnmpDbContext());
            return View(devicesRepo.GetById(id));
        }

        public ActionResult GetParametersByDeviceId(long id)
        {
            var notifs = MvcApplication.SnmpServer.GetAllNotifications(id);
            TempData["notifs"] = notifs;
            return View(MvcApplication.SnmpServer.GetAllValues(id));
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult ParameterHistory(long id)
        {
            var paramsHistoryRepo = new DevicesItemsRepository(new SnmpDbContext());
            var item = paramsHistoryRepo.GetByItemId(id);
            return View(item);
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
            var notifs = MvcApplication.SnmpServer.GetAllNotifications();
            var devItemsRepo = new DevicesItemsRepository(new SnmpDbContext());
            var devItems = devItemsRepo.GetAll().Where(t => notifs.Select(q => q.ItemId).Contains(t.IdDeviceItemEntity)).ToList();
            var devsR = new DevicesRepository(new SnmpDbContext());
            var d = devsR.GetAll().Where(t => devItems.Select(q => q.IdDeviceEntity).Contains(t.IdDeviceEntity)).ToList();
            return View(d);
        }

        public ActionResult Details(int id)
        {
            var devicesRepo = new DevicesRepository(new SnmpDbContext());
            var device = devicesRepo.GetById(id);
            return View(device);
        }
    }
}
