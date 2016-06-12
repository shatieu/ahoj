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

        public List<Order> getAll()
        {

            Order order = new Order();
            List<Order> orders = base.dBGetAll(order, Order.DBName).Cast<Order>().ToList();

            return orders;
            
        }

        public List<Order> getInMonthYear(int month, int year)
        {
            Order order = new Order(); 
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE MONTH([{3}]) = {1} AND YEAR([{3}]) = {2}", Order.DBName, month, year, "DateAndTime");
            List<Order> orders = base.dBGetAllWhere(sqlQuery, order).Cast<Order>().ToList();

            return orders;
        }


    }
}
