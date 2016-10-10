using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace BasicForm.Models.DBHandler
{
    public class DBHandlerGeneral
    {
        static protected String connectionString = null;

        /// <summary>
        /// Sets connection string
        /// </summary>
        public DBHandlerGeneral()
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
                 connectionString = commands.Where(x => x.Contains("{ConnectionString:")).First().Split(':')[1].Replace("}","");

             }
             /*
            string pathApp_Data = HttpContext.Current.Server.MapPath("~/App_Data/DBProperties.xml");

            System.Xml.Linq.XDocument xdoc = System.Xml.Linq.XDocument.Load(pathApp_Data);
            connectionString =
                (from property in xdoc.Descendants("property")
                 where property.Attribute("name").Name == "connectionString"
                 select property.Value).First();
                       */        
            /*
            csb.DataSource = @"localhost\SQLEXPRESS";
            csb.InitialCatalog = "Calendar";
            csb.IntegratedSecurity = true;
            connectionString = csb.ConnectionString;
            */
            /*
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True");
            connectionString = csb.ConnectionString;
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="representation"></param>
        /// <param name="DBName"></param>
        protected List<ARepresentation> dBGetAll(ARepresentation representation, String DBName)
        {

            string querySelectAll = this.getQuerySelectAll(DBName);
            List<ARepresentation> repres = new List<ARepresentation>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(querySelectAll, sqlConnection))
                {
                    using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            ARepresentation toAdd = representation.getNewInstance();
                            dBReadOneRepre(sqlReader, toAdd);
                            repres.Add(toAdd);
                        }
                    }
                }
            }

            return repres;

        }

        /// <summary>
        /// This takes all represantaion. 
        /// Find only by where part
        /// 
        /// </summary>
        /// <param name="sqlCommand">whole query containing SELECT * FROM DBName WHERE ...</param>
        /// <param name="representation">object to should be taken</param>
        protected List<ARepresentation> selectByQuery(String sqlQuery, ARepresentation representation)
        {
            if (!sqlQuery.StartsWith("SELECT * FROM"))
            {
                Console.WriteLine("Command should starts with SELECT * FROM. Current command is " + sqlQuery);
            }

            List<ARepresentation> repres = new List<ARepresentation>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    try
                    {
                        using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                ARepresentation toAdd = representation.getNewInstance();
                                dBReadOneRepre(sqlReader, toAdd);
                                repres.Add(toAdd);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Wrong command\n" + e.ToString());
                    }
                }
            }

            return repres;

        }

        /// <summary>
        /// Fills Irepresntatinon with data from database
        /// </summary>
        /// <param name="sqlReader">To get data from DB</param>
        /// <param name="representation">to fill data into</param>
        /// <returns>true if everything is OK</returns>
        private Boolean dBReadOneRepre(SqlDataReader sqlReader, ARepresentation representation)
        {
            PropertyInfo[] propertiesOfRep = representation.GetType().GetProperties();
            Boolean noError = true;

            foreach (var property in propertiesOfRep)
            {
                Boolean noErrorOnce;
                noErrorOnce = dBReadOneProperty(sqlReader, representation, property);

                if (!noErrorOnce)
                {
                    noError = false;
                }
            }

            return noError;
        }

        /// <summary>
        /// Fills Irepresntatinon with data from database
        /// </summary>
        /// <param name="sqlReader">To get data from DB</param>
        /// <param name="representation">to fill data into</param>
        /// <returns>true if everything is OK</returns>
        private Boolean dBReadOneProperty(SqlDataReader sqlReader, ARepresentation representation, PropertyInfo property)
        {

            Boolean noError = true;


            Type typeOfVariable = property.PropertyType;
            int row = sqlReader.GetOrdinal(property.Name);
            //checks if row is null. If yes, no need to continue with code below for this row
            if (sqlReader.IsDBNull(row))
            {
                return noError;
            }

            try
            {
                dynamic valueToSet = "";
                //setting value of every property in customer object
                switch (typeOfVariable.Name)
                {

                    case nameof(Int32):
                        valueToSet = sqlReader.GetInt32(row);//Convert.ToInt32(sqlReader[property.Name]);//Int32.Parse(sqlReader[property.Name].ToString());
                        break;
                    case nameof(Boolean):
                        valueToSet = sqlReader.GetBoolean(row);// Convert.ToBoolean(sqlReader[property.Name]);
                        break;
                    case nameof(TimeSpan):
                        valueToSet = sqlReader.GetTimeSpan(row);//(TimeSpan) sqlReader[property.Name];
                        break;
                    case nameof(DateTime):
                        valueToSet = sqlReader.GetDateTime(row);
                        break;
                    case nameof(String):
                        valueToSet = sqlReader.GetString(row);//sqlReader[property.Name].ToString();
                        break;
                    //default is string    
                    default:
                        valueToSet = sqlReader[property.Name].ToString();
                        break;
                }

                property.SetValue(representation, valueToSet);
            }
            catch (Exception e)
            {
                Console.WriteLine( "Cannot take " + property.Name + " from DB /n" + e.ToString());
                noError = false;
            }


            return noError;
        }

        /// <summary>
        /// Get all values in string and takes then into database query 
        /// </summary>
        /// <param name="DBName">Table that in will be taken from</param>
        /// <returns>String with query for taking all customers and its customers</returns>
        protected string getQuerySelectAll(string DBName)
        {
            return string.Format("Select * FROM [{0}]", DBName);
        }


        /// <summary>
        /// Sets into command all parametres of object. 
        /// Works corectelly with mathod getQueryInsertObjectAll()
        /// </summary>
        /// <param name="sqlCommand">command that will be pamameters added into</param>
        /// <param name="obj">object parameters which would be inseted</param>
        private void setCommandParamsInsert(SqlCommand sqlCommand, ARepresentation representation)
        {
            PropertyInfo[] propertiesOfObject = representation.GetType().GetProperties();

            foreach (var property in propertiesOfObject)
            {
                var value = property.GetValue(representation);
                object toSet;
                toSet = setCommandParamsInsertValueToObject(value, property.PropertyType.Name);
                sqlCommand.Parameters.AddWithValue("@" + property.Name, toSet);
            }
        }


        /// <summary>
        /// Converting value into right format
        /// </summary>
        /// <param name="value">to be converted</param>
        /// <param name="propertyTypeName">name of property type, to recognize formating</param>
        /// <returns>value in right format for SQL command parameter</returns>
        private object setCommandParamsInsertValueToObject(object value, string propertyTypeName)
        {
            object toSet;
            if (value == null)
            {
                toSet = (object)DBNull.Value;
            }
            else if (propertyTypeName.Equals(nameof(DateTime)))
            {
                String format = "yyyy-MM-dd HH:mm:ss:fff";
                String stringDate = ((DateTime)value).ToString(format);
                toSet = DateTime.ParseExact(stringDate, format, System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                toSet = value.ToString();
            }


            return toSet;
        }

        /// <summary>
        /// Creates new string with query to insert object with all properties
        /// </summary>
        /// <param name="obj">properties will be taken in this</param>
        /// <param name="DBName">name of database that will be inserting into</param>
        /// <returns>Complete query for inserting whole object</returns>
        private string getQueryInsert(ARepresentation aRepresentation)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = aRepresentation.GetType().GetProperties();

            sb.Append("INSERT INTO [").Append(aRepresentation._DBName).Append("] (");
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

        /// <summary>
        /// Inserts into database whole object that is child of ARepresentation
        /// </summary>
        /// <param name="aRepresentation">to be added into database</param>
        /// <returns>true if added</returns>
        protected Boolean insertRepresentation(ARepresentation aRepresentation)
        {
            string sqlQuery = getQueryInsert(aRepresentation);
            Boolean check = false;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        setCommandParamsInsert(sqlCommand, aRepresentation);
                        check = sqlCommand.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot insert into " + aRepresentation._DBName + " database\n" + e.ToString());
                return check;
            }

            return check;
        }

        protected ARepresentation selectWhereID(int ID, ARepresentation aRepresentation)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", aRepresentation._DBName, "ID", ID);
            List<ARepresentation> list = selectByQuery(sqlQuery, aRepresentation);

            if (!list.Any())
            {
                return null;
            }
            else
            {
                return list.ElementAt(0);
            }
        }

    }
}