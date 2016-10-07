using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicForm.Controllers
{
    public class SanboxesController : Controller
    {
        // GET: Sanboxes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ondra()
        {
            return View();
        }

        public ActionResult Jenda()
        {
            return View();
        }

        public ActionResult Jirka()
        {
            return View();
        }
    }
}