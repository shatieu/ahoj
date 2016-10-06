using BasicForm.Models.DBHandler;
using BasicForm.Models.DBRepresentations;
using BasicForm.Models.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace BasicForm.Models
{
    public class CalendarOrder
    {
        public BasicForm.Models.DBRepresentations.Customer customer { get; set; }
        public BasicForm.Models.DBRepresentations.Order newOrder { get; set; }
        public BasicForm.Models.DBRepresentations.Office office { get; set; }
        public List<BasicForm.Models.DBRepresentations.Procedure> procedures { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{1,2}[.][0-9]{1,2}[.][0-9]{4})$", ErrorMessage = "Date has to be in format DD.MM.YYYY")]
        public string formDate {
            get
            {
                return "";
            }
            set
            {
                String[] splits = value.Split('.');
                newOrder.DateAndTime = newOrder.DateAndTime.AddDays(Int32.Parse(splits[0])-1);
                newOrder.DateAndTime = newOrder.DateAndTime.AddMonths(Int32.Parse(splits[1])-1);
                newOrder.DateAndTime = newOrder.DateAndTime.AddYears(Int32.Parse(splits[2])-2000);
            }
        }
        [Required]
        [RegularExpression(@"^((([0-1]{0,1}[0-9])|([2][0-3]))[:][0-5][0-9])$", ErrorMessage = "Date has to be in format HH:MM")]
        public string formHour { get { return ""; }
            set
            {
                String[] splits = value.Split(':');
                newOrder.DateAndTime = newOrder.DateAndTime.AddHours(Int32.Parse(splits[0]));
                newOrder.DateAndTime = newOrder.DateAndTime.AddMinutes(Int32.Parse(splits[1]));
            }
        }
        
        public int OfficeID { get; set; }
        /*
        public Order NewOrder { get; set; }
        public Customer NewCustomer { get; set; }
        */

        public CalendarOrder()
        {
            procedures = new List<DBRepresentations.Procedure>();
            newOrder = new BasicForm.Models.DBRepresentations.Order();
            customer = new BasicForm.Models.DBRepresentations.Customer();
            /*
            NewOrder = new Order();
            NewCustomer = new Customer();
            */
        }


        public CalendarOrder(int officeID)
        {
            DBHandlerOffice hOffice = new DBHandlerOffice();
            office = hOffice.getByID(officeID);
            OfficeID = officeID;

            DBHandlerProcedure hProcedure = new DBHandlerProcedure();
            procedures = hProcedure.getByOfficeIDActive(officeID);

            newOrder = new BasicForm.Models.DBRepresentations.Order();
            customer = new BasicForm.Models.DBRepresentations.Customer(); 
        }

        public List<Procedure> getActiveProcedures()
        {
            List<Procedure> proceduresActive;

            using (CalendarEntities entities = new CalendarEntities())
            {
                proceduresActive = (from p in entities.Procedures
                                    where (p.OfficeID.Equals(OfficeID) && p.Active == true)
                                    select p).ToList();
            }

            return proceduresActive;
        }

        public List<string> getTakenTimesList(int officeID = 1, int month = 6, int year = 2016)
        {


            UtilityOrder uOrder = new UtilityOrder();
            List<string> times = uOrder.getTakenTimesMonthYear(officeID, month, year);
            

            return times;

        }






    }
}