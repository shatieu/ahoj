using BasicForm.Models;
using BasicForm.Models.DBHandler;
using BasicForm.Models.DBRepresentations;
using BasicForm.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BasicForm.Controllers
{
    
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }

    }
}
