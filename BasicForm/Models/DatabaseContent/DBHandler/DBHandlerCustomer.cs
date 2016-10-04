using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBHandler
{
    public class DBHandlerCustomer: DBHandlerGeneral
    {

        private string DBName = BasicForm.Models.DBRepresentations.Customer.DBName;

        public List<BasicForm.Models.DBRepresentations.Customer> getByEmail(string email)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = '{2}'", DBName, "Email", email);
            return executeQuery(sqlQuery);
        }

        public BasicForm.Models.DBRepresentations.Customer getByPersonaNumber(string PersonaNumber)
        {
            List<BasicForm.Models.DBRepresentations.Customer> list;
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

        public bool insert(BasicForm.Models.DBRepresentations.Customer customer)
        {
            return base.insertRepresentation(customer);
        }

        public BasicForm.Models.DBRepresentations.Customer getByID(int ID)
        {
            return (BasicForm.Models.DBRepresentations.Customer) Convert.ChangeType( base.selectWhereID(ID, new BasicForm.Models.DBRepresentations.Customer()), typeof(BasicForm.Models.DBRepresentations.Customer));
        }

        public List<BasicForm.Models.DBRepresentations.Customer> getAll()
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}]", DBName);
            return executeQuery(sqlQuery);
        }

        private List<BasicForm.Models.DBRepresentations.Customer> executeQuery(string sqlQuery)
        {
            List<BasicForm.Models.DBRepresentations.Customer> customers = base.selectByQuery(sqlQuery, new BasicForm.Models.DBRepresentations.Customer()).Cast<BasicForm.Models.DBRepresentations.Customer>().ToList();
            return customers;
        }


    }
}