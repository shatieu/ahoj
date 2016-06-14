using BasicForm.Models.DBHandler;
using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace BasicForm.Models
{
    public class CalendarOrder
    {
        public Customer customer { get; set; }
        public Order newOrder { get; set; }
        public Office office { get; set; }
        public List<Procedure> procedures { get; set; }
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
                newOrder.DateAndTime.AddDays(Int32.Parse(splits[0])-1);
                newOrder.DateAndTime.AddMonths(Int32.Parse(splits[1])-1);
                newOrder.DateAndTime.AddYears(Int32.Parse(splits[2])-2000);
            }
        }
        [Required]
        [RegularExpression(@"^((([0-1]{0,1}[0-9])|([2][0-3]))[:][0-5][0-9])$", ErrorMessage = "Date has to be in format DD.MM.YYYY")]
        public string formHour { get { return ""; }
            set
            {
                String[] splits = value.Split(':');
                newOrder.DateAndTime.AddHours(Int32.Parse(splits[0]));
                newOrder.DateAndTime.AddMinutes(Int32.Parse(splits[1]));
            }
        }

        private CalendarOrder()
        {

        }

        public CalendarOrder(int officeID)
        {
            DBHandlerOffice hOffice = new DBHandlerOffice();
            office = hOffice.getByID(officeID);

            DBHandlerProcedure hProcedure = new DBHandlerProcedure();
            procedures = hProcedure.getByOfficeIDActive(officeID);

            newOrder = new Order();
            customer = new Customer(); 
        }

        

        
       

    }
}