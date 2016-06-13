using BasicForm.Models.DBHandler;
using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicForm.Models.DBHandler
{
    class DBHandlerProvider : DBHandlerGeneral
    {

        private string DBName = Provider.DBName;

        public Provider getByID(int ID)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "ID", ID);
            return executeQuery(sqlQuery).ElementAt(0);
        }

        public List<Provider> getAll()
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}]", DBName);
            return executeQuery(sqlQuery);
        }

        private List<Provider> executeQuery(string sqlQuery)
        {
            Provider provider = new Provider();
            List<Provider> providers = base.dBGetAllWhere(sqlQuery, provider).Cast<Provider>().ToList();

            return providers;
        }


    }
}
