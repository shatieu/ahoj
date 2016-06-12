using BasicForm.Models.DBHandler;
using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicForm.Models.DBHandler
{
    class DBHandlerProcedure : DBHandlerGeneral
    {

        public List<Procedure> getAll()
        {
            Procedure procedure = new Procedure();
            List<Procedure> procedures = base.dBGetAll(procedure, Procedure.DBName).Cast<Procedure>().ToList();

            return procedures;
        }

        public List<Procedure> getInOffice(int officeID)
        {
            Procedure procedure = new Procedure();
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{2}] = {1}", Procedure.DBName, officeID, "OfficeID");
            List<Procedure> procedures = base.dBGetAllWhere(sqlQuery, procedure).Cast<Procedure>().ToList();

            return procedures;
        }

    }
}
