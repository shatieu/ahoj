using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBHandler
{
    public class DBHandlerCustomer: DBHandlerGeneral
    {
        public List<Customer> getAll()
        {
            Customer cust = new Customer();
            List<Customer> custs = base.dBGetAll(cust, Customer.DBName).Cast<Customer>().ToList();

            return custs;
        }
    }
}