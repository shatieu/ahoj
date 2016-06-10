using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BasicForm.Models
{
    public class DBCalendar
    {

        protected String connectionString = null;

        public DBCalendar()
        {
            if (connectionString == null)
            {
                SetConnectionString();
            }
        }

        public void DBconnect()
        {
            if (connectionString == null)
            {
                SetConnectionString();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
            }

        }

        public void CustomerWrite()
        {
            

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("Select ID, Name, Surname, Email, BirthYear, Description, Phone, OrderDate, OrderTime FROM Customer", conn);
                SqlDataReader sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                  /*  Console.WriteLine("{0} {1} {2} {3} {4} {5} ",
                        sqlReader["Id"], sqlReader["Name"], sqlReader["Surname"], sqlReader["DateFrom"], sqlReader["DateTo"], sqlReader["DoctorId"]
                        );
                        */

                }
            }

        }

        private void SetConnectionString()
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = @"DESKTOP-JPGMG4M\SQLEXPRESS";
            csb.InitialCatalog = "DBCalendar";
            csb.IntegratedSecurity = true;
            connectionString = csb.ConnectionString;
        }
    }
}