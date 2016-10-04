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

        public ActionResult Index()
        {

            return View();
        }

        // private DBCustomer DBcust = new DBCustomer();

        //[HandleError()]
        //public ActionResult SomeError()
        //{
        //    throw new Exception("test");
        //}

        // GET: Home
        //[HttpGet]
        //public ActionResult Index(int id = 2)
        //{

        //    TEST - implicit json to test AJAX
        //    string jsonTimes;
        //    UtilityOrder uOrder = new UtilityOrder();
        //    try
        //    {
        //        List<string> times = uOrder.getTakenTimesMonthYear(id, 7, 2016);
        //        /*
        //        List<JsonTimes> jTimes = new List<JsonTimes>();

        //        foreach(string time in times)
        //        {
        //            jTimes.Add(new JsonTimes(time));
        //        }*/

        //        var jsonSerialiser = new JavaScriptSerializer();
        //        jsonTimes = jsonSerialiser.Serialize(times);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        jsonTimes = "[{\"erorr\":\"" + e.ToString() + "\"}]";
        //    }
            
        //    TEST - implicit json to test AJAX
        //    ViewBag.JsonRaw = jsonTimes.ToString();
        //    ViewBag.Json = Json(jsonTimes, JsonRequestBehavior.AllowGet);

        //    init new object to view
        //    CalendarOrder calendar = new CalendarOrder(id);
            
        //    return View(calendar);
        //}

        //public ActionResult Details(CalendarOrder model)
        //{
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Index(CalendarOrder model)
        //{
        //    CalendarOrder _model = new CalendarOrder(model.office.ID);

        //    if (ModelState.IsValid)
        //    {
                
        //        DBHandlerCustomer dbHandlerCustomer = new DBHandlerCustomer();
        //        DBHandlerOrder dbHandlerOrder = new DBHandlerOrder();

        //        BasicForm.Models.DBRepresentations.Customer customer = dbHandlerCustomer.getByPersonaNumber(model.customer.PersonalNumber);
        //        start
        //        Customer customer;
        //        using (CalendarEntities entities = new CalendarEntities())
        //        {
        //            customer = entities.Customers.Where(x => x.PersonalNumber)
        //        }
        //        end


        //        taking data from post and complete with data from database
        //        _model.customer = model.customer;
        //        _model.newOrder = model.newOrder;

        //        _model.newOrder.OfficeID = model.office.ID;
        //        if (customer == null)
        //        {
        //            dbHandlerCustomer.insert(model.customer);
        //            customer = dbHandlerCustomer.getByPersonaNumber(model.customer.PersonalNumber);
        //        }

        //        int IDCustomer = customer.ID;
        //        _model.newOrder.CustomerID = IDCustomer;
        //        dbHandlerOrder.insert(_model.newOrder);

        //    }



        //    return View("Details",_model);
        //}

       

        //[HttpGet]
        //public JsonResult getTakenTimes(int officeID = 1, int month = 6, int year = 2016)
        //{
            
        //    string jsonTimes;
        //    UtilityOrder uOrder = new UtilityOrder();
        //    try
        //    {
        //        List<string> times = uOrder.getTakenTimesMonthYear(officeID, month, year);
        //        /*
        //        List<JsonTimes> jTimes = new List<JsonTimes>();

        //        foreach(string time in times)
        //        {
        //            jTimes.Add(new JsonTimes(time));
        //        }*/

        //        var jsonSerialiser = new JavaScriptSerializer();
        //        jsonTimes = jsonSerialiser.Serialize(times);
        //    }catch(ArgumentException e)
        //    {
        //        jsonTimes = "[{\"erorr\":\""+e.ToString()+"\"}]";
        //    }
            
        //    return Json(jsonTimes,JsonRequestBehavior.AllowGet);
            
        //}
        

    }
}
