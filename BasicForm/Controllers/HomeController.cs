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
            times.Add("10_15:30");
            times.Add("12_15:30");
            times.Add("9_09:30");
            times.Add("10_12:30");
            times.Add("12_16:40");
            times.Add("10_12:00");
            times.Add("8_15:30");
            times.Add("7_15:30");
            times.Add("8_09:30");
            times.Add("9_12:30");
            times.Add("13_16:40");
            times.Add("14_12:00");

            for (int i = 0; i < 10; i++)
            {
                jt = new JsonTimes(times.ElementAt(i));
                jTimes.Add(jt);
            }

            System.Text.StringBuilder mujJson = new System.Text.StringBuilder("[");
            for (int i = 0; i < 10; i++)
            {
                mujJson.Append("{\"hour\":\"").Append(times.ElementAt(i)).Append("\"},");
            }
            mujJson.Remove(mujJson.Length-2,1);
            mujJson.Append("]");

            var jsonSerialiser = new JavaScriptSerializer();
            var jsonTimes = jsonSerialiser.Serialize(jTimes);
            ViewBag.JsonRaw = jsonTimes.ToString();
            ViewBag.MujJson = mujJson.ToString();

            ViewBag.Json = Json(jTimes, JsonRequestBehavior.AllowGet);

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
