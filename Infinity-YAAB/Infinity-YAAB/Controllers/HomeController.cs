using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

using Infinity_YAAB.Models.Repositories.Abstract;

namespace Infinity_YAAB.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        protected IUnitDataRepository o_objUnitDataRepo { get; set; }

        public ActionResult Index()
        {
            //o_objUnitDataRepo.testFunction();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}