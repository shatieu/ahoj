using BasicForm.Models;
using BasicForm.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BasicForm.Controllers
{
    public class OrderController : Controller
    {
        [HandleError()]
        public ActionResult SomeError()
        {
            throw new Exception("test");
        }

        /// <summary>
        /// Creates new form for order
        /// </summary>
        /// <param name="id">ID of office for order</param>
        /// <returns>View with order form</returns>
        [HttpGet]
        public ActionResult Index(int id = 2)
        {

            ////TEST - implicit json to test AJAX
            //string jsonTimes;
            //UtilityOrder uOrder = new UtilityOrder();
            //try
            //{
            //    List<string> times = uOrder.getTakenTimesMonthYear(id, 7, 2016);
            //    /*
            //    List<JsonTimes> jTimes = new List<JsonTimes>();

            //    foreach(string time in times)
            //    {
            //        jTimes.Add(new JsonTimes(time));
            //    }*/

            //    var jsonSerialiser = new JavaScriptSerializer();
            //    jsonTimes = jsonSerialiser.Serialize(times);
            //}
            //catch (ArgumentException e)
            //{
            //    jsonTimes = "[{\"erorr\":\"" + e.ToString() + "\"}]";
            //}

            ////TEST - implicit json to test AJAX
            //ViewBag.JsonRaw = jsonTimes.ToString();
            //ViewBag.Json = Json(jsonTimes, JsonRequestBehavior.AllowGet);

            //init new object to view
            CalendarOrder calendar = new CalendarOrder(id);

            return View(calendar);
        }

        /// <summary>
        /// Controller to write out data about model
        /// </summary>
        /// <param name="model">Calendar with data</param>
        /// <returns>Detail page with saved values</returns>
        public ActionResult Details(CalendarOrder model)
        {
            return View(model);
        }

        /// <summary>
        /// Handle post method of form. 
        /// Create new instance of order. 
        /// Create new instance of customer if not created already
        /// </summary>
        /// <param name="model">Getting data from: OfficeID, Order, Customer</param>
        /// <returns>Detail page with saved values</returns>
        [HttpPost]
        public ActionResult Index(CalendarOrder model)
        {
            CalendarOrder calOrder = new CalendarOrder(model.OfficeID);

            if (ModelState.IsValid)
            {
                using (CalendarEntities db = new CalendarEntities())
                {
                    calOrder.NewOrder = model.NewOrder;

                    Customer _customer = db.Customers.Where(x => x.PersonalNumber.Equals(model.Customer.PersonalNumber)).SingleOrDefault();

                    //creating customer if not existing
                    if (_customer == null)
                    {
                        _customer = model.Customer;
                        db.Customers.Add(_customer);
                        // ID to _customer automatically added here
                        db.SaveChanges();
                    }

                    //To get values in detail page
                    calOrder.Customer = _customer;

                    //necessary values to insert
                    calOrder.NewOrder.CustomerID = _customer.ID;
                    calOrder.NewOrder.OfficeID = model.OfficeID;
                    db.Orders.Add(calOrder.NewOrder);

                    db.SaveChanges();
                }
            }
            
            return View("Details", calOrder);
        }


        /// <summary>
        /// Create JSON with times with orded
        /// </summary>
        /// <param name="officeID">to be search in</param>
        /// <param name="month">specify month that times will be taken from</param>
        /// <param name="year">specify year that times will be taken from</param>
        /// <returns>Json of taken times. Every time in json is formated DD_HH:MM</returns>
        [HttpGet]
        public JsonResult getTakenTimes(int officeID = 1, int month = 6, int year = 2016)
        {
            string jsonTimes;
            var jsonSerialiser = new JavaScriptSerializer();

            try
            {
                List<string> times = UtilityOrder.getTakenTimesByMonthYear(officeID, month, year);
                jsonSerialiser = new JavaScriptSerializer();
                jsonTimes = jsonSerialiser.Serialize(times);
            }
            catch (ArgumentException e)
            {
                jsonTimes = jsonSerialiser.Serialize(e);// "[{\"erorr\":\"" + e.ToString() + "\"}]";
            }

            return Json(jsonTimes, JsonRequestBehavior.AllowGet);

        }

        
    }
}

