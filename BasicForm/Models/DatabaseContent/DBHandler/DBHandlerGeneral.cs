using BasicForm.Models.DBRepresentations;
using BasicForm.Models.Logger;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
            csb.DataSource = @"localhost\SQLEXPRESS";
            csb.InitialCatalog = "Calendar";
            csb.IntegratedSecurity = true;
            connectionString = csb.ConnectionString;

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

            using (SqlConnection sqlConnection = new SqlConnection(connectionString)) {
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
        protected List<ARepresentation> dBGetAllWhere(String sqlQuery, ARepresentation representation)
        {

            string querySelectAll = sqlQuery;
            List<ARepresentation> repres = new List<ARepresentation>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(querySelectAll, sqlConnection))
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
                    }catch(Exception e)
                    {
                        CustomLogger.Log(CustomLogger.Level.ERROR, "Wrong command\n" + e.ToString());
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
                CustomLogger.Log(CustomLogger.Level.WARN, "Cannot take " + property.Name + " from DB /n" + e.ToString());
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
        protected void setCommandParametersOfRepresAll(SqlCommand sqlCommand, Object obj)
        {
            PropertyInfo[] propertiesOfObject = obj.GetType().GetProperties();

            foreach (var property in propertiesOfObject)
            {
                sqlCommand.Parameters.AddWithValue("@" + property.Name, property.GetValue(obj).ToString());
            }
        }
    }

    
}