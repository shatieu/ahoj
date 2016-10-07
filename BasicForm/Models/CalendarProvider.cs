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
            
        }




    }
}