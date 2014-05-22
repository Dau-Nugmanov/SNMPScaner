using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UI.Helpers;
using UI.Models;

namespace UI.Controllers
{
    public class DevicesController : Controller
    {
        public ActionResult GetAll()
        {
            DevicesRepository devicesRepo = new DevicesRepository(new SnmpDbContext());
            return View(devicesRepo.GetAll().ToList());
        }

        public ActionResult Add()
        {
            SetDdlsNotSelected();
            var defLogin = AppSettingsHelper.DefaultLoginForDevice;
            var defPas = AppSettingsHelper.DefaultPasswordForDevice;
            ViewData["defaultLogin"] = defLogin;
            ViewData["defaultPassword"] = defPas;
            return View();
        }

        [HttpPost]
        public ActionResult Add(DeviceModel device)
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
                ViewData["message_error"] = "Пожалуйста, заполните форму корректными данными: "+ errors.ToString();
                SetDdlsNotSelected();
                return View(device);
            }
            DevicesRepository devRepo = new DevicesRepository(new SnmpDbContext());
            devRepo.Add(device.ToEfEntity());
            devRepo.SaveChanges();
            ViewData["message"] = "Новое устройство добавлено в систему";
            SetDdlsNotSelected();
            return RedirectToAction("Index", "Settings");
        }

        public ActionResult Edit(int idDevice)
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var deviceRepo = new DevicesRepository(context);
                var device = deviceRepo.GetById(idDevice);
                if (device == null)
                    return HttpNotFound();
                SetDdlsSelectValues(device.IdCustomer, device.DeviceModel.IdMaker);
                return View(DeviceModel.ToModelEntity(device));
            }
        }

        [HttpPost]
        public ActionResult Edit(DeviceModel device, int Makers)
        {
            SetDdlsSelectValues(device.IdCustomer, Makers);
            if (!ModelState.IsValid)
            {
                ViewData["message_error"] = "Заполните форму корректными данными";
                return View(device);
            }
            var devRepo = new DevicesRepository(new SnmpDbContext());
            devRepo.Edit(device.ToEfEntity());
            devRepo.SaveChanges();
            ViewData["message"] = "Изменения успешно сохранены";
            return View(device);
        }

        public void SetDdlsNotSelected()
        {
            var makersRepo = new MakersRepository(new SnmpDbContext());
            SelectList makers = new SelectList(makersRepo.GetAll().ToList(), "IdMaker", "MakerName");
            ViewData["makers"] = makers;

            var customersRepo = new CustomersRepository(new SnmpDbContext());
            SelectList customers = new SelectList(customersRepo.GetAll().ToList(), "IdCustomer", "CustomerName");
            ViewData["customers"] = customers;
        }

        public void SetDdlsSelectValues(int idCustomer, int idMaker)
        {

            var customersRepo = new CustomersRepository(new SnmpDbContext());
            var customers = customersRepo.GetAll().ToList();
            var customersSelectList = new SelectList(customers, "IdCustomer", "CustomerName"
                , customers.FirstOrDefault(t => t.IdCustomer == idCustomer));
            ViewData["customers"] = customersSelectList;

            var makersRepo = new MakersRepository(new SnmpDbContext());
            var makers = makersRepo.GetAll().ToList();
            SelectList makersSelect = new SelectList(makers, "IdMaker", "MakerName"
                , makers.FirstOrDefault(t => t.IdMaker == idMaker));
            ViewData["makers"] = makersSelect;
        }
       
        [HttpPost]
        public JsonResult GetModels(int idMaker)
        {
            var modelsRepo = new DeviceModelsRepository(new SnmpDbContext());
            var models = modelsRepo.GetAllByMakerId(idMaker)
                .Select(t => new { IdDeviceModel = t.IdDeviceModel, ModelName = t.ModelName })
                .ToList();
            return Json(models);
        }

        public ActionResult Remove(int id)
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var devRepo = new DevicesRepository(context);
                devRepo.RemoveById(id);
                devRepo.SaveChanges();
                return RedirectToAction("Index", "Settings");
            }
        }

        public ActionResult GetAllCheck()
        {
            var devRepo = new DevicesRepository(new SnmpDbContext());
            return View(devRepo.GetAll().ToList());
        }

        public ActionResult GetAllCheckParameters()
        {
            var paramsRepo = new DevicesItemsRepository(new SnmpDbContext());
            return View(paramsRepo.GetAll().ToList());
        }
    }
}
