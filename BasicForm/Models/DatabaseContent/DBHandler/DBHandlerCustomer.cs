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

        public Customer getByPersonaNumber(string PersonaNumber)
        {
            List<Customer> list;
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = '{2}'", DBName, "PersonalNumber", PersonaNumber);
            list = executeQuery(sqlQuery);

            if (!list.Any())
            {
                return null;
            }
            else
            {
                return list.ElementAt(0);
            }
        }

        public bool insert(Customer customer)
        {
            return base.insertRepresentation(customer);
        }

        public Customer getByID(int ID)
        {
            return (Customer) Convert.ChangeType( base.selectWhereID(ID, new Customer()), typeof(Customer));
        }

        public List<Customer> getAll()
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}]", DBName);
            return executeQuery(sqlQuery);
        }

        private List<Customer> executeQuery(string sqlQuery)
        {
            List<Customer> customers = base.selectByQuery(sqlQuery, new Customer()).Cast<Customer>().ToList();
            return customers;
        }


    }
}