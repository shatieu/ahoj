using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BasicForm.Models
{
    public class DBCustomer : DBCalendar
    {
        private String DBName = "Customer";
        private String DBCusName = "Name";
        private String DBCusSurName = "Surname";
        private String DBCusEmail = "Email";
        private String DBCusDescription = "Description";
        private String DBCusBirthYear = "BirthYear";
        private String DBCusPhone = "Phone";
        private String DBCusOrderDate = "OrderDate";
        private String DBCusOrderTime = "OrderTime";
        private String DBCusID = "ID";
        private String DBCusDocID = "DoctorID";


        public DBCustomer() : base()
        {
        }


        /// <summary>
        /// Get all values in string and takes then into database query 
        /// </summary>
        /// <returns>String with query for taking all customers and its customers</returns>
        private String QueryGetAll()
        {
            return string.Format( "Select * FROM {0}", DBName);
        }
        

        /// <summary>
        /// Load all customers in database 
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customer> CustomerGetAll()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                
                using (SqlCommand sqlCommand = new SqlCommand(QueryGetAll(), conn))
                {
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        Customer customer = new Customer();
                        customer.BirthYear = sqlReader[DBCusBirthYear].ToString();
                        customer.Description = sqlReader[DBCusDescription].ToString();
                        customer.DoctorID = Int32.Parse( sqlReader[DBCusDocID].ToString());
                        customer.Email = sqlReader[DBCusEmail].ToString();
                        customer.ID = Int32.Parse(sqlReader[DBCusID].ToString());
                        customer.Name = sqlReader[DBCusName].ToString();
                        customer.OrderDate = new DateOrder( sqlReader[DBCusOrderDate].ToString());
                        customer.OrderTime = Int32.Parse(sqlReader[DBCusOrderTime].ToString());
                        customer.Phone = sqlReader[DBCusPhone].ToString();
                        customer.Surname = sqlReader[DBCusSurName].ToString();

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }
        
        /// <summary>
        /// Set customer into database
        /// 
        /// </summary>
        /// <param name="customer">to be inserted</param>
        /// <returns>True if inserted, False if fail</returns>
        public bool CustomerInsert(Customer customer)
        {

            int check;

            //check mandatory fields
            if(customer.OrderDate == null || customer.Email == null || customer.OrderTime == null || customer.OrderTime == 0)
            {
                return false;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                String queryInsert = string.Format("INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}) VALUES (@{1} ,@{2}, @{3}, @{4}, @{5}, @{6}, @{7}, @{8}, @{9})", DBName, DBCusName, DBCusSurName, DBCusPhone, DBCusEmail, DBCusDescription, DBCusBirthYear, DBCusOrderDate, DBCusOrderTime, DBCusDocID);

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, conn))
                {
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusName,customer.Name);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusSurName, customer.Surname);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusPhone, customer.Phone);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusEmail, customer.Email);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusDescription, customer.Description);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusBirthYear, customer.BirthYear);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusOrderDate, customer.OrderDate.ToString());
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusOrderTime, customer.OrderTime);
                    sqlCommand.Parameters.AddWithValue("@" + DBCusDocID, customer.DoctorID);

                    check = sqlCommand.ExecuteNonQuery();
                }
            }

            return check == 1 ? true : false;
        }


        /// <summary>
        /// Takes all orders in this month and code it into on string
        /// </summary>
        /// <param name="month">to be found in it</param>
        /// <param name="year">to be found in it</param>
        /// <returns>codes of times. Format: YYYY_MM_DD_HH:MM-TT, where DD is day, HH is hour, MM are minutes and TT are times</returns>
        public List<String> getTakenTimes(int month, int year)
        {
            List<String> takenTimes = new List<String>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                String selectingTimes = string.Format("SELECT {0}, {1} FROM {2} WHERE {0} LIKE @regexMonth", DBCusOrderDate, DBCusOrderTime, DBName);
                String regexMonth = year + "_" + month + "_%";
                using (SqlCommand sqlCommand = new SqlCommand(selectingTimes, conn))
                {
                    sqlCommand.Parameters.AddWithValue("@regexMonth", regexMonth);
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                    while (sqlReader.Read())
                    {

                        String cusDate = sqlReader[DBCusOrderDate].ToString();
                        String cusTime = sqlReader[DBCusOrderTime].ToString();

                        //codes it into format
                        String coded = cusDate+"-"+cusTime;

                        takenTimes.Add(coded);

                      
                    }
                }
            }

            return takenTimes;

        }

    }
}