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

        /// <summary>
        /// takes mounth and year and return list of taken times in this date
        /// </summary>
        /// <param name="mounth">to be found in</param>
        /// <param name="year">to be found in</param>
        /// <returns>Set of strings where strings are in format DD_HH:MM</returns>
        public List<String> getTakenTimesMonthYear(int month, int year)
        {
            Dictionary<int, Procedure> proceduresDic = getProcedures();
            List<string> formatedOrders = new List<string>();
            DateTime time;
            StringBuilder sb = new StringBuilder();
            DBHandlerOrder hOrder = new DBHandlerOrder();
            List<Order> ordersInDate = hOrder.getByOfficeIDInMonthYear(office.ID, month,year);

            foreach(Order order in ordersInDate)
            {
                for (int i = 0;  i < proceduresDic[order.ProcedureID].Lasts; i = i+10) {
                    time = order.DateAndTime;
                    time = time.AddMinutes(i);

                    sb.Clear();
                    sb.Append(time.Day).Append("_").Append(time.Hour).Append(":").Append(time.Minute);
                    formatedOrders.Add(sb.ToString());
                }
            }

            return formatedOrders;
        }

        private Dictionary<int, Procedure> getProcedures()
        {
            Dictionary<int, Procedure> proceduresDic = new Dictionary<int, Procedure>();
            foreach (Procedure proc in procedures)
            {
                proceduresDic.Add(proc.ID, proc);
            }

            return proceduresDic;
        }
       

    }
}