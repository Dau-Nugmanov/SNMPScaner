using DAL;
using DAL.Repos;
using DomainModel;
using DomainModel.EfModels;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UI.Models;

namespace UI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public ActionResult Edit()
        {
            var usersRepository = new UsersRepository(new SnmpDbContext());
            if (Session["user"] == null)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            int idUser = (Session["user"] as User).IdUser;
            var user = usersRepository.GetById(idUser);
            if (user == null)
                return HttpNotFound();
            return View(UserModel.ToModelEntity(user));
        }

        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                ViewData["message"] = "Данные не верны";
                return View(user);
            }
            var usersRepo = new UsersRepository(new SnmpDbContext());
            usersRepo.Edit(user.ToEfEntity());
            usersRepo.SaveChanges();
            return View(user);
        }

        public JsonResult IsEmailExists(string email)
        {
            EmailsRepository emailsRepo = new EmailsRepository(new SnmpDbContext());
            return emailsRepo.IsEmailExists(email)
                 ? Json(true, JsonRequestBehavior.AllowGet)
                 : Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsPhoneNumberExists(string number)
        {
            PhoneNumbersRepository numbersRepo = new PhoneNumbersRepository(new SnmpDbContext());
            return numbersRepo.IsPhoneNumberExists(number)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
