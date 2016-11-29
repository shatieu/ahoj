using BasicForm.Models.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace BasicForm.Models
{
    public class CalendarOrder
    {
        /// <summary>
        /// ID of office that calendar will be work with
        /// </summary>
        public int OfficeID { get; set; }
        /// <summary>
        /// Customer for order, taken from form
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// New order createdby customer
        /// </summary>
        public Order NewOrder { get; set; }
        /// <summary>
        /// Form of Date only to parse into DateTime object
        /// </summary>
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
                NewOrder.Begin = NewOrder.Begin.AddDays(Int32.Parse(splits[0]));
                NewOrder.Begin = NewOrder.Begin.AddMonths(Int32.Parse(splits[1]));
                NewOrder.Begin = NewOrder.Begin.AddYears(Int32.Parse(splits[2]));
            }
        }
        /// <summary>
        /// Form of time and minutes only to parse into DateTime object
        /// </summary>
        [Required]
        [RegularExpression(@"^((([0-1]{0,1}[0-9])|([2][0-3]))[:][0-5][0-9])$", ErrorMessage = "Date has to be in format HH:MM")]
        public string formHour { get { return ""; }
            set
            {
                String[] splits = value.Split(':');
                NewOrder.Begin = NewOrder.Begin.AddHours(Int32.Parse(splits[0]));
                NewOrder.Begin = NewOrder.Begin.AddMinutes(Int32.Parse(splits[1]));
            }
        }
        


        /// <summary>
        /// Basic constructor for HTTP post form
        /// </summary>
        public CalendarOrder()
        {
            NewOrder = new Order();
            Customer = new Customer();

        }
        
        /// <summary>
        /// Constructor for setting IfficeID
        /// </summary>
        /// <param name="officeID">ID of office in DB that calendar is created for</param>
        public CalendarOrder(int officeID):this()
        {
            OfficeID = officeID;
        }

        /// <summary>
        /// Gets from DB all procedures that has proper OfficeID and Active==true
        /// </summary>
        /// <returns>All procedures that has proper OfficeID and Active==true</returns>
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

        /// <summary>
        /// Returns all times that are in specified month and year for specific provider
        /// </summary>
        /// <param name="month">specify month that times will be taken from</param>
        /// <param name="year">specify year that times will be taken from</param>
        /// <returns>Set of strings where strings are in format DD_HH:MM</returns>
        public List<string> getTakenTimesList( int month = 6, int year = 2016)
        {
            return UtilityOrder.getTakenTimesByMonthYear(OfficeID, month, year); 
        }






    }
}