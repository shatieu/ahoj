using BasicForm.Models;
using BasicForm.Models.DBHandler;
using BasicForm.Models.DBRepresentations;
using BasicForm.Models.Logger;
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

        [HandleError()]
        public ActionResult SomeError()
        {
            throw new Exception("test");
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index(int officeID = 1)
        {

            //TEST - implicit json to test AJAX
            string jsonTimes;
            UtilityOrder uOrder = new UtilityOrder();
            try
            {
                List<string> times = uOrder.getTakenTimesMonthYear(officeID, 7, 2016);
                /*
                List<JsonTimes> jTimes = new List<JsonTimes>();

                foreach(string time in times)
                {
                    jTimes.Add(new JsonTimes(time));
                }*/

                var jsonSerialiser = new JavaScriptSerializer();
                jsonTimes = jsonSerialiser.Serialize(times);
            }
            catch (ArgumentException e)
            {
                jsonTimes = "[{\"erorr\":\"" + e.ToString() + "\"}]";
            }
            
            //TEST - implicit json to test AJAX
            ViewBag.JsonRaw = jsonTimes.ToString();
            ViewBag.Json = Json(jsonTimes, JsonRequestBehavior.AllowGet);

            //init new object to view
            CalendarOrder calendar = new CalendarOrder(officeID);
            
            return View(calendar);
        }

        public ActionResult Details(CalendarOrder model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CalendarOrder model)
        {
            CalendarOrder _model = new CalendarOrder(model.office.ID);
            DBHandlerCustomer dbHandlerCustomer = new DBHandlerCustomer();
            DBHandlerOrder dbHandlerOrder = new DBHandlerOrder();
            List<Customer> listCustomers = dbHandlerCustomer.getByPersonaNumber(model.customer.PersonalNumber);

            if (ModelState.IsValid)
            {
                

                //taking data from post and complete with data from database
                _model.customer = model.customer;
                _model.newOrder = model.newOrder;

                _model.newOrder.OfficeID = model.office.ID;
                if (!listCustomers.Any())
                {
                    dbHandlerCustomer.insert(model.customer);
                }
                listCustomers = dbHandlerCustomer.getByPersonaNumber(model.customer.PersonalNumber);
                int IDCustomer = listCustomers.ElementAt(0).ID;
                _model.newOrder.CustomerID = IDCustomer;
                dbHandlerOrder.insert(_model.newOrder);

            }



            return View("Details",_model);
        }

       

        [HttpGet]
        public JsonResult getTakenTimes(int officeID = 1, int month = 6, int year = 2016)
        {
            
            string jsonTimes;
            UtilityOrder uOrder = new UtilityOrder();
            try
            {
                List<string> times = uOrder.getTakenTimesMonthYear(officeID, month, year);
                /*
                List<JsonTimes> jTimes = new List<JsonTimes>();

                foreach(string time in times)
                {
                    jTimes.Add(new JsonTimes(time));
                }*/

                var jsonSerialiser = new JavaScriptSerializer();
                jsonTimes = jsonSerialiser.Serialize(times);
            }catch(ArgumentException e)
            {
                jsonTimes = "[{\"erorr\":\""+e.ToString()+"\"}]";
            }
            
            return Json(jsonTimes,JsonRequestBehavior.AllowGet);
            
        }
        

    }
}
