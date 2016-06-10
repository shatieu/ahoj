using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
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

        /// <summary>
        /// Return query in string for inserting object to database
        /// </summary>
        /// <param name="obj">that should be inserted</param>
        /// <param name="DBName">into what database shoujd be object inserted</param>
        /// <returns>query containing INSERT INTO and VALUES</returns>
        protected string queryInsertObject(Object obj, String DBName)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propertiesOfObject = obj.GetType().GetProperties();

            sb.Append("INSERT INTO ").Append(DBName).Append(" (");
            //creating parts with names in tables
            foreach (var property in propertiesOfObject)
            {
                sb.Append(property.Name).Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);

            sb.Append(") VALUES (");
            //attribute values
            foreach (var property in propertiesOfObject)
            {
                sb.Append("@").Append(property.Name).Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);

            sb.Append(")");


            return sb.ToString();
        }

        private void SetConnectionString()
        {
            /*
            string[] curr = System.IO.File.ReadAllLines(System.IO.Directory.GetCurrentDirectory());
            string[] lines = System.IO.File.ReadAllLines( @"./DBproperties.txt"); //System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName +
            string[] lines = System.IO.File.ReadAllLines(System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Properties/DBproperties.txt"));
            string dataSource = "";

            foreach (string line in lines)
            {
                if (line.StartsWith("DataSource:"))
                {
                    dataSource = line.Split(new char[] { ':' }, 2)[1];
                }
            }

            if (dataSource.Equals(""))
            {
                Console.WriteLine("No database name!!!!");
            }*/

            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = @"DESKTOP-JPGMG4M\SQLEXPRESS"; 
            csb.InitialCatalog = "DBCalendar";
            csb.IntegratedSecurity = true;
            connectionString = csb.ConnectionString;
        }
    }
}