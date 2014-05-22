using DAL;
using DAL.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        public ActionResult Index()
        {
            var devRepo = new DevicesRepository(new SnmpDbContext());
            return View(devRepo.GetAll().ToList());
        }

        public ActionResult GetDeviceParameters(int idDevice)
        {
            var devParamsRepo = new DevicesItemsRepository(new SnmpDbContext());
            return View(devParamsRepo.GetByDeviceId(idDevice));
        }

		public ActionResult Notification(long idDevicesItems)
        {
            var notifRepo = new NotificationsRepository(new SnmpDbContext());
			var notif = notifRepo.GetByDeviceIdAndItemId(idDevicesItems);
            if (notif == null)
            {
				TempData["IdDevicesItems"] = idDevicesItems;
                return RedirectToAction("Add");
            }
            return RedirectToAction("Edit", new { id = notif.IdNotification });
        }

        public ActionResult Edit(int id)
        {
            var notifsRepo = new NotificationsRepository(new SnmpDbContext());
            var entity = notifsRepo.GetById(id);
            if (entity == null)
                return HttpNotFound();
            return View(NotificationModel.ToModelEntity(entity));
        }

        [HttpPost]
        public ActionResult Edit(NotificationModel notification)
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
                return View(notification);
            }
            var notifRepo = new NotificationsRepository(new SnmpDbContext());
            notifRepo.Edit(notification.ToEfEntity());
            notifRepo.SaveChanges();
            ViewData["message"] = "Изменения успешно сохранены";
            return View(notification);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(NotificationModel notification)
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
                return View(notification);
            }
            var notifRepo = new NotificationsRepository(new SnmpDbContext());
            var efEntity = notification.ToEfEntity();
            notifRepo.Add(efEntity);
            notifRepo.SaveChanges();
            return RedirectToAction("Edit", new { id = efEntity.IdNotification });
        }

        public ActionResult Remove(long id)
        {
            var notifRepo = new NotificationsRepository(new SnmpDbContext());
            notifRepo.RemoveById(id);
            notifRepo.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
