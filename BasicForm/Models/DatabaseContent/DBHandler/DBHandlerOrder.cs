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
        private string DBName = Order.DBName;

        public bool insert(Order order)
        {
            return base.dBInsertRepresentation(order, DBName);
        }

        public Order getByID(int ID)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "ID", ID);
            return executeQuery(sqlQuery).ElementAt(0);
        }

        public List<Order> getAll()
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}]", DBName);
            return executeQuery(sqlQuery);
        }

        private List<Order> executeQuery(string sqlQuery)
        {
            Order order = new Order();
            List<Order> orders = base.dBGetAllWhere(sqlQuery, order).Cast<Order>().ToList();

            return orders;
        }

        private List<Order> getInMonthYear(int month, int year)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE MONTH([{3}]) = {1} AND YEAR([{3}]) = {2}", Order.DBName, month, year, "DateAndTime");
            return executeQuery(sqlQuery);
        }

        public List<Order> getByOfficeIDInMonthYear(int officeID, int month, int year)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{4}] = {5} AND MONTH([{3}]) = {1} AND YEAR([{3}]) = {2}", Order.DBName, month, year, "DateAndTime", "OfficeID", officeID);
            return executeQuery(sqlQuery);
        }

        

    }
}
