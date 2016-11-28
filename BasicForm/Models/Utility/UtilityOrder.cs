using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicForm.Models.Utility
{
    public static class UtilityOrder
    {

        /// <summary>
        /// takes mounth and year and return list of taken times in this month
        /// </summary>
        /// <param name="mounth">to be found in</param>
        /// <param name="year">to be found in</param>
        /// <returns>Set of strings where strings are in format DD_HH:MM</returns>
        public static List<String> getTakenTimesByMonthYear(int officeID, int month, int year)
        {
            //old code - delete after proof that entity framework recreation works properly
            //DBHandlerProcedure hProcedure = new DBHandlerProcedure();
            //List<string> formatedOrders = new List<string>();
            //DateTime time;
            //StringBuilder sb = new StringBuilder();
            //DBHandlerOrder hOrder = new DBHandlerOrder();
            //UtilityProcedure uProcedure = new UtilityProcedure();

            //Dictionary<int, BasicForm.Models.DBRepresentations.Procedure> proceduresDic = uProcedure.getProceduresAsDictionary(hProcedure.getByOfficeIDAll(officeID));

            //List<BasicForm.Models.DBRepresentations.Order> ordersInDate = hOrder.getByOfficeIDInMonthYear(officeID, month, year);
            //try
            //{
            //    foreach (BasicForm.Models.DBRepresentations.Order order in ordersInDate)
            //    {
            //        for (int i = 0; i < proceduresDic[order.ProcedureID].Lasts; i = i + 10)
            //        {
            //            time = order.DateAndTime;
            //            time = time.AddMinutes(i);

            //            sb.Clear();
            //            sb.Append(time.Day).Append("_").Append(time.Hour).Append(":").Append(time.Minute);
            //            formatedOrders.Add(sb.ToString());
            //        }
            //    }
            //}catch(Exception e)
            //{
            //    ErrorSignal.FromCurrentContext().Raise(e);
            //    Console.WriteLine(e.ToString());
            //}
            //return formatedOrders;

            //entity framework

            List<string> _times = new List<string>();

            using (CalendarEntities db = new CalendarEntities())
            {

                //potencional request for cheange to faster the code....
                //<Date, ID of procedure> of Office is specified month and year
                Dictionary<DateTime, int> _ordersInDate = (from p in db.Orders
                                                           where (p.OfficeID.Equals(officeID) && p.Begin.Year.Equals(year) && p.Begin.Month.Equals(month))
                                                           select p).ToDictionary(p => p.Begin, p => p.ProcedureID);

                //<id of procedure, lasts> of Office with Active status
                Dictionary<int, int> proceduresActive = (from p in db.Procedures
                                                         where (p.OfficeID.Equals(officeID) && p.Active == true)
                                                         select p).ToDictionary(p => p.ID, p => p.Lasts);

                //to get faster adding in cycle
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                //parsing DateTime and Lasts into string
                foreach (KeyValuePair<DateTime, int> order in _ordersInDate)
                {
                    for (int i = 0; i < proceduresActive[order.Value] / 10; i++)
                    {
                        DateTime time;
                        time = order.Key;
                        time = time.AddMinutes(i);

                        sb.Clear();
                        sb.Append(time.Day).Append("_").Append(time.Hour).Append(":").Append(time.Minute);
                        _times.Add(sb.ToString());
                    }
                }
            }


            return _times;
        }

        /// <summary>
        /// takes day, mounth and year and return list of taken times in this day
        /// </summary>
        /// <param name="mounth">to be found in</param>
        /// <param name="year">to be found in</param>
        /// <param name="day">to be found in</param>
        /// <returns>Set of strings where strings are in format DD_HH:MM</returns>
        public static List<String> getTakenTimesByMonthYearDay(int officeID,int day, int month, int year)
        {
            List<string> _times = new List<string>();

            using (CalendarEntities db = new CalendarEntities())
            {

                //potencional request for cheange to faster the code....
                //<Date, ID of procedure> of Office is specified month and year
                Dictionary<DateTime, int> _ordersInDate = (from p in db.Orders
                                                           where (p.OfficeID.Equals(officeID) && p.Begin.Year.Equals(year) && p.Begin.Month.Equals(month) && p.Begin.Day.Equals(day))
                                                           select p).ToDictionary(p => p.Begin, p => p.ProcedureID);

                //<id of procedure, lasts> of Office with Active status
                Dictionary<int, int> proceduresActive = (from p in db.Procedures
                                                         where (p.OfficeID.Equals(officeID) && p.Active == true)
                                                         select p).ToDictionary(p => p.ID, p => p.Lasts);

                //to get faster adding in cycle
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                //parsing DateTime and Lasts into string
                foreach (KeyValuePair<DateTime, int> order in _ordersInDate)
                {
                    for (int i = 0; i < proceduresActive[order.Value] / 10; i++)
                    {
                        DateTime time;
                        time = order.Key;
                        time = time.AddMinutes(i);

                        sb.Clear();
                        sb.Append(time.Day).Append("_").Append(time.Hour).Append(":").Append(time.Minute);
                        _times.Add(sb.ToString());
                    }
                }
            }


            return _times;
        }
    }
}