using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBHandler
{
    public class DBHandlerCustomer: DBHandlerGeneral
    {

        private string DBName = Customer.DBName;

        public List<Customer> getByEmail(string email)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = '{2}'", DBName, "Email", email);
            return executeQuery(sqlQuery);
        }

        public bool insert(Customer customer)
        {
            return base.dBInsertRepresentation(customer, DBName);
        }

        public Customer getByID(int ID)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "ID", ID);
            return executeQuery(sqlQuery).ElementAt(0);
        }

        public List<Customer> getAll()
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}]", DBName);
            return executeQuery(sqlQuery);
        }

        private List<Customer> executeQuery(string sqlQuery)
        {
            Customer customer = new Customer();
            List<Customer> customers = base.dBGetAllWhere(sqlQuery, customer).Cast<Customer>().ToList();

            return customers;
        }


    }
}