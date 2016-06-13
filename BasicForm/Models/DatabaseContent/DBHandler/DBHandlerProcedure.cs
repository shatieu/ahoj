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
        private string DBName = Procedure.DBName;

        public Procedure getByID(int ID)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "ID", ID);
            return executeQuery(sqlQuery).ElementAt(0);
        }

        public List<Procedure> getAll()
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}]", DBName);
            return executeQuery(sqlQuery);
        }

        private List<Procedure> executeQuery(string sqlQuery)
        {
            Procedure procedure = new Procedure();
            List<Procedure> procedures = base.dBGetAllWhere(sqlQuery, procedure).Cast<Procedure>().ToList();

            return procedures;
        }

        private List<Procedure> getAllActiveOrInactiveBasedOnParam(bool resultActive)
        {

            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "Active", resultActive ? "1" : "0");
            return executeQuery(sqlQuery);
        }

        private List<Procedure> getByOfficeIDActiveOrInactiveBasedOnParam(int officeID, bool resultActive)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2} AND [{3}] = {4}", DBName, "OfficeID", officeID, "Active", resultActive ? "1" : "0");
            return executeQuery(sqlQuery);
        }

        public List<Procedure> getAllActive()
        {
            return getAllActiveOrInactiveBasedOnParam(true);
        }

        public List<Procedure> getByOfficeIDActive(int officeID)
        {
            return getByOfficeIDActiveOrInactiveBasedOnParam(officeID, true);
        }

        public List<Procedure> getByOfficeIDAll(int officeID)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "OfficeID", officeID);
            return executeQuery(sqlQuery);
        }

    }
}
