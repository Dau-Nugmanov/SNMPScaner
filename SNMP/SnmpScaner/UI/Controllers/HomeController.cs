using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string login, string password, string returnurl)
        {
            UsersRepository usersRepo = new UsersRepository(new SnmpDbContext());
            var user = usersRepo.GetUserByLoginAndPassword(login, password);
            if (user == null)
            {
                ViewData["message"] = "Неверное имя пользователя или пароль";
                return View();
            }
            if (user.FirstEntry)
            {
                TempData["idUser"] = user.IdUser;
                return RedirectToAction("ChangePassword");
            }
            Session["user"] = user;
            //FormsAuthentication.SetAuthCookie(login, true);

            SetAuthCoockie(user.Login, user.IsAdmin);

            return RedirectToAction("Index", "MainPanel");
        }

        private void SetAuthCoockie(string userLogin, bool isAdmin)
        {
            string roles = "";
            if (isAdmin)
                roles += "Admin";
            else
                roles += "User";

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                  1,
                  userLogin,  //user id
                  DateTime.Now,
                  DateTime.Now.AddMinutes(20),  // expiry
                  false,  //do not remember
                  roles,
                  "/");
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                               FormsAuthentication.Encrypt(authTicket));
            Response.Cookies.Add(cookie);

        }

        public ActionResult ChangePassword()
        {
            int idUser;
            if (TempData["idUser"] != null)
            {
                if (!int.TryParse(TempData["idUser"].ToString(), out idUser))
                    return HttpNotFound();
                return View(idUser);
            }
            return HttpNotFound();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword, int IdUser)
        {
            UsersRepository usersRepository = new UsersRepository(new SnmpDbContext());
            var user = usersRepository.GetById(IdUser);
            if (user == null)
            {
                ViewData["message"] = "Неверный пользователь";
                return View();
            }
            if (user.Password != oldPassword)
            {
                ViewData["message"] = "Неверный пароль";
                return View();
            }
            if (newPassword != confirmPassword)
            {
                ViewData["message"] = "Пароли должны совпадать";
                return View();
            }
            user.Password = newPassword;
            user.FirstEntry = false;
            usersRepository.Edit(user);
            usersRepository.SaveChanges();
            Session["user"] = user;
            SetAuthCoockie(user.Login, user.IsAdmin);
            //FormsAuthentication.SetAuthCookie(user.Login, true);
            return RedirectToAction("Index", "MainPanel");
        }
    }
}
