using BasicForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicForm.Controllers
{
    
    public class HomeController : Controller
    {
        private DBCustomer DBcust = new DBCustomer();

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            
            
            CalendarCustomer calCus = new CalendarCustomer();
            
            int m = calCus.Month++;
            ModelState.Remove("Month");
            calCus.Month = m+1;
            return View(calCus);
        }

        public ActionResult Details(CalendarCustomer calCus)
        {
            return View(calCus);
        }

        
        public ActionResult Index(CalendarCustomer calCus)
        {
            
            if (ModelState.IsValid)
            {
                calCus.Cust.DoctorID = 1;
                calCus.Cust.OrderDate = new DateOrder("1999_02_12_15:53");
                calCus.Cust.OrderTime = 10;
               // DBcust.CustomerInsert(cust);

                List<Customer> customers = DBcust.CustomerGetAll();
                
                foreach (Customer customer in customers) {
                    calCus.Cust.Description += customer.ToString();
                }
                calCus.reSetValues();
                if(calCus.Month > 5)
                {
                    calCus.Month = 3;
                }else
                {
                    calCus.Month = 8;
                }


            }

            return View(calCus);
        }

        
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

    

    }
}
