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

        protected string getQueryInsertObject(Object obj, String DBName)
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
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = @"DESKTOP-JPGMG4M\SQLEXPRESS";
            csb.InitialCatalog = "DBCalendar";
            csb.IntegratedSecurity = true;
            connectionString = csb.ConnectionString;
        }
    }
}