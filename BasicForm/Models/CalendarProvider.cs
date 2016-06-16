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
        public List<Order> orders;
        public List<Office> offices;
        public Provider provider;
        public Office currentOffice;

        public CalendarProvider(int providerID)
        {
            DBHandlerProvider hProvider = new DBHandlerProvider();
            provider = hProvider.getByID(providerID);

            DBHandlerOffice hOffice = new DBHandlerOffice();
            offices = hOffice.getByProviderIDActive(provider.ID);

            currentOffice = offices.Any() ? offices.ElementAt(0) : null;
            
        }




    }
}