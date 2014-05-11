using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class CustomersController : Controller
    {
        public ActionResult GetAll()
        {
            var customersRepo = new CustomersRepository(new SnmpDbContext());
            return View(customersRepo.GetAll().ToList());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                ViewData["error_message"] = "Данные не корректны";
                return View(customer);
            }
            var customersRepo = new CustomersRepository(new SnmpDbContext());
            customersRepo.Add(customer.ToEfEntity());
            customersRepo.SaveChanges();
            ViewData["message"] = "Заказчик успешно добавлен в систему";
            return View(customer);
        }

        public ActionResult Remove(int id)
        {
            CustomersRepository customersRepo = new CustomersRepository(new SnmpDbContext());
            customersRepo.RemoveById(id);
            customersRepo.SaveChanges();
            return RedirectToAction("Index", "Settings");
        }

        public ActionResult Edit(int id)
        {
            var customersRepo = new CustomersRepository(new SnmpDbContext());
            var customer = customersRepo.GetById(id);
            if (customer == null)
                return HttpNotFound();
            return View(CustomerModel.ToModelEntity(customer));
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                ViewData["error_message"] = "Данные не корректны";
                return View(customer);
            }
            CustomersRepository customersRepo = new CustomersRepository(new SnmpDbContext());
            customersRepo.Edit(customer.ToEfEntity());
            customersRepo.SaveChanges();
            ViewData["message"] = "Данные успешно обновлены";
            return View(customer);
        }
    }
}
