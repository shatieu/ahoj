using BasicForm.Models.DBHandler;
using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicForm.Models.DBHandler
{
    class DBHandlerOrder : DBHandlerGeneral
    {
        private string DBName = BasicForm.Models.DBRepresentations.Order.DBName;

        public bool insert(BasicForm.Models.DBRepresentations.Order order)
        {
            return base.insertRepresentation(order);
        }

        public BasicForm.Models.DBRepresentations.Order getByID(int ID)
        {
            return (BasicForm.Models.DBRepresentations.Order)Convert.ChangeType(base.selectWhereID(ID, new BasicForm.Models.DBRepresentations.Order()), typeof(BasicForm.Models.DBRepresentations.Order));
        }

        public List<BasicForm.Models.DBRepresentations.Order> getAll()
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}]", DBName);
            return executeQuery(sqlQuery);
        }

        private List<BasicForm.Models.DBRepresentations.Order> executeQuery(string sqlQuery)
        {
            BasicForm.Models.DBRepresentations.Order order = new BasicForm.Models.DBRepresentations.Order();
            List<BasicForm.Models.DBRepresentations.Order> orders = base.selectByQuery(sqlQuery, order).Cast<BasicForm.Models.DBRepresentations.Order>().ToList();

            return orders;
        }

        private List<BasicForm.Models.DBRepresentations.Order> getInMonthYear(int month, int year)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE MONTH([{3}]) = {1} AND YEAR([{3}]) = {2}", DBName, month, year, "DateAndTime");
            return executeQuery(sqlQuery);
        }

        public List<BasicForm.Models.DBRepresentations.Order> getByOfficeIDInMonthYear(int officeID, int month, int year)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{4}] = {5} AND MONTH([{3}]) = {1} AND YEAR([{3}]) = {2}", DBName, month, year, "DateAndTime", "OfficeID", officeID);
            return executeQuery(sqlQuery);
        }

        public List<BasicForm.Models.DBRepresentations.Order> getByOfficeIDAll(int officeID)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2} ", DBName, "OfficeID", officeID);
            return executeQuery(sqlQuery);
        }

    }
}
