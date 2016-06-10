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
        /// Sets into command all parametres of object. 
        /// Works corectelly with mathod getQueryInsertObjectAll()
        /// </summary>
        /// <param name="sqlCommand">command that will be pamameters added into</param>
        /// <param name="obj">object parameters which would be inseted</param>
        protected void setCommandParametersOfObjectAll(SqlCommand sqlCommand, Object obj)
        {
            PropertyInfo[] propertiesOfObject = obj.GetType().GetProperties();

            foreach (var property in propertiesOfObject)
            {
                sqlCommand.Parameters.AddWithValue("@" + property.Name, property.GetValue(obj).ToString());
            }
        }


        /// <summary>
        /// Get all values in string and takes then into database query 
        /// </summary>
        /// <param name="DBName">Table that in will be taken from</param>
        /// <returns>String with query for taking all customers and its customers</returns>
        protected String getQuerySelectAll(String DBName)
        {
            return string.Format("Select * FROM {0}", DBName);
        }

        /// <summary>
        /// creates new string with query to insert object with all properties
        /// </summary>
        /// <param name="obj">properties will be taken in this</param>
        /// <param name="DBName">name of database that will be inserting into</param>
        /// <returns></returns>
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

        protected void SetConnectionString()
        {
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jirka\Source\Repos\ahoj\BasicForm\App_Data\Calendar.mdf;Integrated Security=True
            /*SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = @"DESKTOP-JPGMG4M\SQLEXPRESS";
            csb.InitialCatalog = "DBCalendar";
            csb.IntegratedSecurity = true;
            connectionString = csb.ConnectionString;
            */
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True");
            connectionString = csb.ConnectionString;
        }
    }
}