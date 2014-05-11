using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    [Authorize]
    public class MainPanelController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
