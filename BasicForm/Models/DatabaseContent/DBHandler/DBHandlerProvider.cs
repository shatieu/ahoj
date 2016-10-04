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

        private string DBName = BasicForm.Models.DBRepresentations.Provider.DBName;

        public bool insert(BasicForm.Models.DBRepresentations.Provider provider)
        {
            return base.insertRepresentation(provider);
        }

        public BasicForm.Models.DBRepresentations.Provider getByID(int ID)
        {
            return (BasicForm.Models.DBRepresentations.Provider)Convert.ChangeType(base.selectWhereID(ID, new BasicForm.Models.DBRepresentations.Provider()), typeof(BasicForm.Models.DBRepresentations.Provider));
        }

        public List<BasicForm.Models.DBRepresentations.Provider> getAll()
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}]", DBName);
            return executeQuery(sqlQuery);
        }

        private List<BasicForm.Models.DBRepresentations.Provider> executeQuery(string sqlQuery)
        {
            BasicForm.Models.DBRepresentations.Provider provider = new BasicForm.Models.DBRepresentations.Provider();
            List<BasicForm.Models.DBRepresentations.Provider> providers = base.selectByQuery(sqlQuery, provider).Cast<BasicForm.Models.DBRepresentations.Provider>().ToList();

            return providers;
        }


    }
}
