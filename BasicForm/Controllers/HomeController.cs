using BasicForm.Models;
using BasicForm.Models.DBHandler;
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
       // private DBCustomer DBcust = new DBCustomer();

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
           // DBCustomer dbCustomer = new DBCustomer();
            List<string> times = new List<string>();//dbCustomer.getTakenTimes(2, 1999);
            List<JsonTimes> jTimes = new List<JsonTimes>();

            JsonTimes jt;
            
            //DD_HH:MM
            times.Add("101530");
            times.Add("121530");
            times.Add("90930");
            times.Add("101230");
            times.Add("121640");
            times.Add("101200");
            times.Add("81530");
            times.Add("71530");
            times.Add("80930");
            times.Add("91230");
            times.Add("131640");
            times.Add("141200");

            for (int i = 0; i < 10; i++)
            {
                jt = new JsonTimes(times.ElementAt(i));
                jTimes.Add(jt);
            }

            var jsonSerialiser = new JavaScriptSerializer();
            var jsonTimes = jsonSerialiser.Serialize(jTimes);


            ViewBag.Json = Json(jsonTimes, JsonRequestBehavior.AllowGet).Data;

       

            CalendarOrder calendar = new CalendarOrder(2);

            // int m = calCus.Month++;
            // ModelState.Remove("Month");
            // calCus.Month = m+1;
            return View(calendar);
        }

        public ActionResult Details(CalendarCustomer calCus)
        {
            return View(calCus);
        }

        
        public ActionResult Index(CalendarCustomer calCus)
        {
            /*
            if (ModelState.IsValid)
            {
                calCus.Cust.DoctorID = 2;
               // calCus.Cust.OrderDate = new ODateOrder("1999_02_12_15:53");
               // DBcust.CustomerInsert(cust);

               // List<OCustomer> customers = DBcust.CustomerGetAll();
                
               // foreach (OCustomer customer in customers) {
               //     calCus.Cust.Description += customer.ToString();
              //  }
                calCus.reSetValues();
                


            }*/

            return View(calCus);
        }

        /*
        public ActionResult increaseMonth(CalendarCustomer calCus)
        {

            if (ModelState.IsValid)
            {
                
            }
            int m = calCus.Month;
            ModelState.Remove("month");
            calCus.Month = m + 3;
            calCus.reSetValues();
            calCus.Cust.Surname = "vrbka" + m;


            return RedirectToAction("Index", new { cal = calCus });//View("Index","Index",calCus);
        }

    */
        [HttpGet]
        public ActionResult getTakenTimes(int officeID, int month, int year)
        {
            UtilityOrder uOrder = new UtilityOrder();

            List<string> times = uOrder.getTakenTimesMonthYear(officeID, month, year);
            
                    
            var jsonSerialiser = new JavaScriptSerializer();
            var jsonTimes = jsonSerialiser.Serialize(times);

            
            return Json(jsonTimes,JsonRequestBehavior.AllowGet);
            
        }
        

    }
}
