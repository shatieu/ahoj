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
        /// Load all customers in database 
        /// </summary>
        /// <returns>List of customers</returns>
        public List<OCustomer> CustomerGetAll()
        {
            List<OCustomer> customers = new List<OCustomer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(base.getQuerySelectAll(DBName), conn))
                {
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        OCustomer customer = new OCustomer();

                        foreach (var fieldsOfObject in customer.GetType().GetProperties())
                        {
                            TypeCode typeOfVariable = Type.GetTypeCode(fieldsOfObject.PropertyType);

                            try
                            {
                                dynamic valueToSet = "";
                                //setting value of every property in customer object
                                switch (typeOfVariable)
                                {
                                    case TypeCode.Int32:
                                        valueToSet = Int32.Parse(sqlReader[fieldsOfObject.Name].ToString());
                                        break;
                                    case TypeCode.Object:
                                        if (fieldsOfObject.PropertyType == typeof(ODateOrder))
                                        {
                                            valueToSet = new ODateOrder(sqlReader[fieldsOfObject.Name].ToString());
                                        }
                                        else if (fieldsOfObject.PropertyType == typeof(OProdecure))
                                        {
                                            valueToSet = new OProdecure(Int32.Parse(sqlReader[fieldsOfObject.Name].ToString()));
                                        }
                                        break;
                                    //default is string    
                                    default:
                                        valueToSet = sqlReader[fieldsOfObject.Name].ToString();
                                        break;
                                }

                                fieldsOfObject.SetValue(customer, valueToSet);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Cannot take " + fieldsOfObject.Name + "from database\n");
                            }
                        }
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
        public bool CustomerInsert(OCustomer customer)
        {
            int check;

            //check mandatory fields
            if(customer.OrderDate == null || customer.Email == null )
            {
                return false;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                String queryInsert = base.getQueryInsertObject(customer, DBName);//string.Format("INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}) VALUES (@{1} ,@{2}, @{3}, @{4}, @{5}, @{6}, @{7}, @{8}, @{9})", DBName, DBCusName, DBCusSurName, DBCusPhone, DBCusEmail, DBCusDescription, DBCusBirthYear, DBCusOrderDate, DBCusOrderTime, DBCusDocID);

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, conn))
                {
                    /*
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusName,customer.Name);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusSurName, customer.Surname);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusPhone, customer.Phone);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusEmail, customer.Email);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusDescription, customer.Description);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusBirthYear, customer.BirthYear);
                    sqlCommand.Parameters.AddWithValue("@"+ DBCusOrderDate, customer.OrderDate.ToString());
                    sqlCommand.Parameters.AddWithValue("@" + DBCusDocID, customer.DoctorID);*/
                    base.setCommandParametersOfObjectAll(sqlCommand, customer);

                    check = sqlCommand.ExecuteNonQuery();
                }
            }

            return check == 1 ? true : false;
        }


        /// <summary>
        /// NEED TO BE DONE WITH PROCEDURE
        /// Takes all orders in this month and code it into on string
        /// </summary>
        /// <param name="month">to be found in it</param>
        /// <param name="year">to be found in it</param>
        /// <returns>codes of times. Format: YYYY_MM_DD_HH:MM-TT, where DD is day, HH is hour, MM are minutes and TT are times</returns>
        public List<String> getTakenTimes(int month, int year)
        {
            List<String> takenTimes = new List<String>();
            if (connectionString == null)
            {
                SetConnectionString();
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                String selectingTimes = string.Format("SELECT {0} FROM {1} WHERE {0} LIKE @regexMonth", DBCusOrderDate,/* DBCusOrderTime, */DBName);
                String regexMonth = year + "_" + month + "_%";
                using (SqlCommand sqlCommand = new SqlCommand(selectingTimes, conn))
                {
                    sqlCommand.Parameters.AddWithValue("@regexMonth", regexMonth);
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                    while (sqlReader.Read())
                    {

                        String cusDate = sqlReader[DBCusOrderDate].ToString();
                        // String cusTime = sqlReader[DBCusOrderTime].ToString();

                        //codes it into format
                        String coded = cusDate;// +"-"+cusTime;

                        takenTimes.Add(coded);

                      
                    }
                }
            }

            return takenTimes;

        }

    }
}