using BasicForm.Models.DBHandler;
using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models
{
    public class CalendarProvider
    {
        public List<BasicForm.Models.DBRepresentations.Order> orders;
        public List<BasicForm.Models.DBRepresentations.Office> offices;
        public BasicForm.Models.DBRepresentations.Provider provider;
        public BasicForm.Models.DBRepresentations.Office currentOffice;

        public CalendarProvider(int providerID)
        {
            DBHandlerProvider hProvider = new DBHandlerProvider();
            provider = hProvider.getByID(providerID);

            DBHandlerOffice hOffice = new DBHandlerOffice();
            offices = hOffice.getByProviderIDActive(provider.ID);

            currentOffice = offices.Any() ? offices.ElementAt(0) : null;

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