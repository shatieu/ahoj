using BasicForm.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models
{
    public class CalendarProvider
    {
        public List<Order> orders;
        public List<Office> offices;
        public Provider provider;
        public Office currentOffice;

        public CalendarProvider(int providerID = 2)
        {
            //DBHandlerProvider hProvider = new DBHandlerProvider();
            //provider = hProvider.getByID(providerID);

            //DBHandlerOffice hOffice = new DBHandlerOffice();
            //offices = hOffice.getByProviderIDActive(2);

            //currentOffice = offices.Any() ? offices.ElementAt(0) : null;

            //using(CalendarEntities cal = new CalendarEntities())
            //{
            //    Provider provider = cal.Providers.Where(x => x.ID.Equals(providerID)).Select(x => x).SingleOrDefault();

            //                      /*  (from prov in cal.Providers
            //                        where (prov.ID.Equals(providerID))
            //                        select prov).SingleOrDefault();*/

            //}
            

            using (CalendarEntities db = new CalendarEntities())
            {
                //TADY KURVA JE POTREBA PREPSAT DVOJKA NA PROVIDERID
                provider = db.Providers.Where(x => x.ID.Equals(providerID)).SingleOrDefault();
                offices = db.Offices.Where(x => x.ProviderID.Equals(provider.ID)).ToList();
                currentOffice = offices.FirstOrDefault();
                if(currentOffice == null)
                {
                    Console.WriteLine("PICWWWWWWWEEEE");
                }
                currentOffice = offices.FirstOrDefault();
                orders =  db.Orders.Include("Customer").Include("Procedure").Where(x => x.OfficeID.Equals(currentOffice.ID)).ToList();
            }

            Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error());
           // Elmah.ErrorLog.
        }

        /// <summary>
        /// Returns all times that are in specified day, month and year for specific provider
        /// </summary>
        /// <param name="month">specify month that times will be taken from</param>
        /// <param name="year">specify year that times will be taken from</param>
        /// <param name="day">specify day that times will be taken from</param>
        /// <returns>Set of strings where strings are in format DD_HH:MM</returns>
        public List<string> getTakenTimesListDay(int day = 1, int month = 6, int year = 2016)
        {
            return UtilityOrder.getTakenTimesByMonthYearDay(currentOffice.ID, month, year, day);
        }


    }
}