using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class MakersController : Controller
    {
        public ActionResult GetAll()
        {
            MakersRepository makersRepo = new MakersRepository(new SnmpDbContext());
            return View(makersRepo.GetAll().ToList());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(MakerModel maker)
        {
            if (!ModelState.IsValid)
            {
                ViewData["error_message"] = "Данные не корректны";
                return View(maker);
            }
            MakersRepository makersRepo = new MakersRepository(new SnmpDbContext());
            makersRepo.Add(maker.ToEfEntity());
            makersRepo.SaveChanges();
            ViewData["message"] = "Производитель успешно добавлен в систему";
            return View(maker);
        }

        public ActionResult Remove(int id)
        {
            MakersRepository makersRepo = new MakersRepository(new SnmpDbContext());
            makersRepo.RemoveById(id);
            makersRepo.SaveChanges();
            return RedirectToAction("Index", "Settings");
        }

        public ActionResult Edit(int id)
        {
            var makerRepo = new MakersRepository(new SnmpDbContext());
            var maker = makerRepo.GetById(id);
            if (maker == null)
                return HttpNotFound();
            return View(MakerModel.ToModel(maker));
        }

        [HttpPost]
        public ActionResult Edit(MakerModel maker)
        {
            if (!ModelState.IsValid)
            {
                ViewData["error_message"] = "Данные не корректны";
                return View(maker);
            }
            MakersRepository makersRepo = new MakersRepository(new SnmpDbContext());
            makersRepo.Edit(maker.ToEfEntity());
            makersRepo.SaveChanges();
            ViewData["message"] = "Данные успешно обновлены";
            return View(maker);
        }
    }
}
