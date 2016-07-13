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

        public bool insert(Provider provider)
        {
            return base.insertRepresentation(provider);
        }

        public Provider getByID(int ID)
        {
            return (Provider)Convert.ChangeType(base.selectWhereID(ID, new Provider()), typeof(Provider));
        }

        public List<Provider> getAll()
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}]", DBName);
            return executeQuery(sqlQuery);
        }

        private List<Provider> executeQuery(string sqlQuery)
        {
            Provider provider = new Provider();
            List<Provider> providers = base.selectByQuery(sqlQuery, provider).Cast<Provider>().ToList();

            return providers;
        }


    }
}
