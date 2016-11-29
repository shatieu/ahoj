using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models.Utility.Database
{
    public static partial class UDatabase
    {
        /// <summary>
        /// This class represents work with database and Order
        /// This is sinle instance class == Singleton implementation
        /// </summary>
        public static class UOrder
        {
           
            /// <summary>
            /// Get all orders which meets given conditions.
            /// Takes every order that has even part in this month.
            /// </summary>
            /// <param name="officeID">What officeID shoudl be in orders</param>
            /// <param name="year">In what year should be order</param>
            /// <param name="month">In what month should be order</param>
            /// <returns>List with null if no order. List with orders otherwise</returns>
            public static List<Order> getAllOrders(int officeID, int year, int month)
            {
                List<Order> orders;

                using (CalendarEntities db = new CalendarEntities())
                {
                    orders = (from order in db.Orders
                              where (order.OfficeID.Equals(officeID) && order.Begin.Year.Equals(year) && (order.Begin.Month.Equals(month) || order.End.Month.Equals(month)))
                              select order).ToList();

                }

                return orders;
            }


            /// <summary>
            /// Select orders in current year, month, day and OfficeID.
            /// Takes every order that has even part in this day.
            /// </summary>
            /// <param name="officeID">What officeID shoudl be in orders</param>
            /// <param name="year">In what year should be order</param>
            /// <param name="month">In what month should be order</param>
            /// <param name="day">In what day should be order</param>
            /// <returns>List with null if no order. List with orders otherwise</returns>
            public static List<Order> getAllOrders(int officeID, int year, int month, int day)
            {
                List<Order> orders;

                orders = getAllOrders(officeID, year, month);
                orders = orders.Where(order => order.Begin.Day.Equals(day) || order.End.Day.Equals(day)).ToList();

                return orders;
            }

            /// <summary>
            /// Code all orders in given time and office into pattern DD_HH:MM
            /// Takes Begin of every order and End of Order, do subdivision and code everything into pattern from begin to end after 10 minutes blocks 
            /// </summary>
            /// <param name="officeID">What officeID shoudl be in orders</param>
            /// <param name="year">In what year should be order</param>
            /// <param name="month">In what month should be order</param>
            /// <returns>List of coded times in pattern DD_HH:MM</returns>
            public static List<String> codeOrderIntoTimes(int officeID, int year, int month)
            {
                List<Order> orders;
                List<String> coded = new List<String>();

                orders = getAllOrders(officeID, year, month);

                //to get faster adding in cycle
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                //goes through every odred with condition
                foreach (Order order in orders)
                {
                    // Saves in 10 minutes blocks
                    for (DateTime changingDate = order.Begin; changingDate.CompareTo(order.End) < 0; changingDate.AddMinutes(10))
                    {
                        //there could be some orders that has start in previous month, this prevents to write them into this month
                        if (changingDate.Month == month)
                        {
                            //creating pattern and saving it
                            sb.Clear();
                            sb.Append(changingDate.Day).Append("_").Append(changingDate.Hour).Append(":").Append(changingDate.Minute);
                            coded.Add(sb.ToString());
                        }
                    }
                }


                return coded;
            }


        }
    }
}