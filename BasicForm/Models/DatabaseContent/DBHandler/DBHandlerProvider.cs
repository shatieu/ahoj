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

        public List<Provider> getAll()
        {
            Provider provider = new Provider();
            List<Provider> providers = base.dBGetAll(provider, Provider.DBName).Cast<Provider>().ToList();

            return providers;
        }


    }
}
