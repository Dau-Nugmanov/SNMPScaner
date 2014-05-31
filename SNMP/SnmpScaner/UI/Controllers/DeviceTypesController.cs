using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repos;
using UI.Models;

namespace UI.Controllers
{
    public class DeviceTypesController : Controller
    {
        public ActionResult GetAll()
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                DeviceTypesRepository devTypesRepo = new DeviceTypesRepository(context);
                return View(devTypesRepo.GetAll().ToList());
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(DeviceTypeModel deviceType)
        {
            if (!ModelState.IsValid)
            {
                ViewData["message_error"] = "Заполните форму корректными данными";
                return View(deviceType);
            }
            using (SnmpDbContext context = new SnmpDbContext())
            {
                DeviceTypesRepository devTypeRepo = new DeviceTypesRepository(context);
                devTypeRepo.Add(deviceType.ToEfEntity());
                devTypeRepo.SaveChanges();
                ViewData["message"] = "Новый тип устройства добавлен в систему";
                return View(deviceType);
            }
        }

        public ActionResult Remove(int idDeviceType)
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var deviceTypeRepo = new DeviceTypesRepository(context);
                deviceTypeRepo.RemoveById(idDeviceType);
                deviceTypeRepo.SaveChanges();
                return RedirectToAction("Index", "Settings");
            }
        }

        public ActionResult Edit(int idDeviceType)
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var deviceTypesRepo = new DeviceTypesRepository(context);
                var devType = deviceTypesRepo.GetById(idDeviceType);
                if (devType == null)
                    return HttpNotFound();
                return View(DeviceTypeModel.ToModelEntity(devType));
            }
        }

        [HttpPost]
        public ActionResult Edit(DeviceTypeModel deviceType)
        {
            if (!ModelState.IsValid)
            {
                ViewData["message_error"] = "Заполните форму валидными данными";
                return View(deviceType);
            }
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var deviceTypeRepo = new DeviceTypesRepository(context);
                deviceTypeRepo.Edit(deviceType.ToEfEntity());
                deviceTypeRepo.SaveChanges();
                ViewData["message"] = "Изменения успешно сохранены";
                return View(deviceType);
            }
        }
    }
}
