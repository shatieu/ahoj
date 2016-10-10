using BasicForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicForm.Controllers
{
    public class ProviderController : Controller
    {
        // GET: Provider
        public ActionResult Index(int providerID = 1)
        {
            CalendarProvider calProvider = new CalendarProvider(providerID);

            return View(calProvider);
        }
        
        [HttpGet]
        public ActionResult EditOrder()
        {

            return View(new Order());
        }

        [HttpPost]
        public ActionResult EditOrder(Order order)
        {

            return View(order);
        }


        public ActionResult Settings()
        {
            return View();
        }
    }
}
