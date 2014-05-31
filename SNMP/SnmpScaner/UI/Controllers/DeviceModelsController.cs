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
    public class DeviceModelsController : Controller
    {
        public ActionResult GetAll()
        {
            DeviceModelsRepository modelsRepo = new DeviceModelsRepository(new SnmpDbContext());
            return View(modelsRepo.GetAll().ToList());
        }

        public ActionResult Add()
        {
            SetDdls();
            return View();
        }

        private void SetDdls()
        {
            var makersRepo = new MakersRepository(new SnmpDbContext());
            var makersSelect = new SelectList(makersRepo.GetAll().ToList(), "IdMaker", "MakerName");
            ViewData["makers"] = makersSelect;

            var deviceTypesRepo = new DeviceTypesRepository(new SnmpDbContext());
            var devTypesSelect = new SelectList(deviceTypesRepo.GetAll().ToList(), "IdDeviceType", "DeviceTypeName");
            ViewData["types"] = devTypesSelect;
        }

        [HttpPost]
        public ActionResult Add(DeviceModelModel model)
        {
            SetDdls();
            if (!ModelState.IsValid)
            {
                ViewData["error_message"] = "Данные не корректны";
                return View(model);
            }
            DeviceModelsRepository modelsRepo = new DeviceModelsRepository(new SnmpDbContext());
            modelsRepo.Add(model.ToEfEntity());
            modelsRepo.SaveChanges();
            ViewData["message"] = "Модель успешно добавлена в систему";
            return View(model);
        }

        public ActionResult Remove(int id)
        {
            DeviceModelsRepository modelsRepo = new DeviceModelsRepository(new SnmpDbContext());
            modelsRepo.RemoveById(id);
            modelsRepo.SaveChanges();
            return RedirectToAction("Index", "Settings");
        }

        public void SetDdlsSelectValues(int idMaker, int idType)
        {
            var makersRepo = new MakersRepository(new SnmpDbContext());
            var makers = makersRepo.GetAll().ToList();
            var makersSelect = new SelectList(makers, "IdMaker", "MakerName"
                ,makers.FirstOrDefault(t => t.IdMaker == idMaker));
            ViewData["makers"] = makersSelect;

            var deviceTypesRepo = new DeviceTypesRepository(new SnmpDbContext());
            var types = deviceTypesRepo.GetAll().ToList();
            var devTypesSelect = new SelectList(types, "IdDeviceType", "DeviceTypeName"
                ,types.FirstOrDefault(t => t.IdDeviceType == idType));
            ViewData["types"] = devTypesSelect;
        }

        public ActionResult Edit(int id)
        {
            var modelsRepo = new DeviceModelsRepository(new SnmpDbContext());
            var model = modelsRepo.GetById(id);
            if (model == null)
                return HttpNotFound();
            SetDdlsSelectValues(model.IdMaker, model.IdDeviceType);
            var m = DeviceModelModel.ToModelEntity(model);
            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(DeviceModelModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["error_message"] = "Данные не корректны";
                return View(model);
            }
            DeviceModelsRepository modelsRepo = new DeviceModelsRepository(new SnmpDbContext());
            modelsRepo.Edit(model.ToEfEntity());
            modelsRepo.SaveChanges();
            ViewData["message"] = "Данные успешно обновлены";
            SetDdlsSelectValues(model.IdMaker, model.IdDeviceType);
            return View(model);
        }

        public ActionResult GetParametersByModel(int idModel)
        {
            var itemsRepo = new DeviceItemsRepository(new SnmpDbContext());
            return View(itemsRepo.GetItemsByModelId(idModel).ToList());
        }

        public ActionResult GetAllCheck()
        {
            var modelsRepo = new DeviceModelsRepository(new SnmpDbContext());
            return View(modelsRepo.GetAll().ToList());
        }
    }
}
