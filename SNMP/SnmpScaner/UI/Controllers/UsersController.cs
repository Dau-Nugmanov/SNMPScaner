using DAL;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    [Authorize(Roles="Admin")]
    public class UsersController : Controller
    {
        //UsersRepository usersRepository;
        public UsersController()
        {
            //usersRepository = new UsersRepository(new SnmpDbContext());
        }

        public PartialViewResult GetAll()
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var usersRepository = new UsersRepository(context);
                return PartialView("GetAll", usersRepository.GetAll().ToList());
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserModel user)
        {
            if (!ModelState.IsValid)
                return View(user);
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var usersRepository = new UsersRepository(context);
                if (usersRepository.IsLoginExists(user.Login))
                {
                    ModelState.AddModelError("Login", "Выберите другой логин");
                    return View(user);
                }
                usersRepository.Add(user.ToEfEntity());
                usersRepository.SaveChanges();
                ViewData["message"] = "Новый пользователь успешно добавлен";
                return View();
            }
        }

        public JsonResult IsLoginExists(string Login)
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var usersRepository = new UsersRepository(context);
                return usersRepository.IsLoginExists(Login)
                     ? Json(false, JsonRequestBehavior.AllowGet)
                     : Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Remove(int idUser)
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var usersRepository = new UsersRepository(context);
                usersRepository.RemoveById(idUser);
                usersRepository.SaveChanges();
                return RedirectToAction("Index", "Settings");
            }
        }

        public ActionResult Details(int id)
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var usersRepository = new UsersRepository(context);
                var user = usersRepository.GetById(id);
                if (user == null)
                    return HttpNotFound();
                return View(user);
            }
        }

        public ActionResult RefreshPassword(int idUser)
        {
            using (SnmpDbContext context = new SnmpDbContext())
            {
                var usersRepository = new UsersRepository(context);
                usersRepository.SetPasswordToDefault(idUser);
                usersRepository.SaveChanges();
                return RedirectToAction("Index", "Settings");
            }
        }

        public ActionResult NotificationsUsers()
        {
            //using (SnmpDbContext context = new SnmpDbContext())
            //{
            SnmpDbContext context = new SnmpDbContext();
                var usersRepository = new UsersRepository(context);
                return View(usersRepository.GetAll().ToList());
            //}
        }
    }
}
