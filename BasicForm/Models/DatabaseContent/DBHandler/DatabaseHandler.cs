using BasicForm.Models.DBRepresentations;
using BasicForm.Models.Logger;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace BasicForm.Models.DatabaseContent.DBHandler
{
    public class DatabaseHandler
    {

        static protected String connectionString = null;

        /// <summary>
        /// Sets connection string
        /// </summary>
        public DatabaseHandler()
        {
            if (connectionString == null)
            {
                SetConnectionString();
            }
        }

        /// <summary>
        /// Saving connection string into variable
        /// </summary>
        protected void SetConnectionString()
        {
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jirka\Source\Repos\ahoj\BasicForm\App_Data\Calendar.mdf;Integrated Security=True
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            string pathApp_Data = HttpContext.Current.Server.MapPath("~/App_Data/DBProperties.txt");
            using (System.IO.StreamReader sr = new System.IO.StreamReader(pathApp_Data))
            {
                String line = sr.ReadToEnd();
                String[] commands = line.Split('}');
                connectionString = commands.Where(x => x.Contains("{ConnectionString:")).First().Split(':')[1].Replace("}", "");

            }
           
        }

        

        public bool insert(ARepresentation aRepresentation)
        {
            Boolean check = false;

            //getting query to execute command
            String queryInsert = getQueryInsert(aRepresentation);

            try
            {
                //setting new connection
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    //init new command with insert query
                    using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                    {
                        setCommandParamsInsert(sqlCommand, aRepresentation);
                        check = sqlCommand.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (Exception e)
            {
                CustomLogger.Log(CustomLogger.Level.ERROR, "Cannot insert into " + aRepresentation._DBName + " database\n" + e.ToString());
                return check;
            }

            return check;
        }


        /// <summary>
        /// Sets into command all parametres of object. 
        /// Works corectelly with mathod getQueryInsertObjectAll()
        /// </summary>
        /// <param name="sqlCommand">command that will be pamameters added into</param>
        /// <param name="obj">object parameters which would be inseted</param>
        protected void setCommandParamsInsert(SqlCommand sqlCommand, Object obj)
        {
            PropertyInfo[] propertiesOfObject = obj.GetType().GetProperties();

            foreach (var property in propertiesOfObject)
            {
                var value = property.GetValue(obj);
                sqlCommand.Parameters.AddWithValue("@" + property.Name, value == null ? (object)DBNull.Value : value.ToString());
            }
        }


        /// <summary>
        /// creates new string with query to insert object with all properties
        /// </summary>
        /// <param name="obj">properties will be taken in this</param>
        /// <param name="DBName">name of database that will be inserting into</param>
        /// <returns></returns>
        private string getQueryInsert(ARepresentation representation)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = representation.GetType().GetProperties();

            sb.Append("INSERT INTO [").Append(representation._DBName).Append("] (");
            //creating parts with names in tables
            foreach (var property in properties)
            {
                if (!property.Name.Equals("ID"))
                {
                    sb.Append("[").Append(property.Name).Append("]").Append(", ");
                }
            }
            sb.Remove(sb.Length - 2, 2);

            sb.Append(") VALUES (");
            //attribute values
            foreach (var property in properties)
            {
                if (!property.Name.Equals("ID"))
                {
                    sb.Append("@").Append(property.Name).Append(", ");
                }
            }
            sb.Remove(sb.Length - 2, 2);

            sb.Append(")");


            return sb.ToString();
        }

        
    }
}