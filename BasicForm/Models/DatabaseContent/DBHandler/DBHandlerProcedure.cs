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
        private string DBName = BasicForm.Models.DBRepresentations.Procedure.DBName;

        public bool insert(BasicForm.Models.DBRepresentations.Procedure procedure)
        {
            return base.insertRepresentation(procedure);
        }

        public BasicForm.Models.DBRepresentations.Procedure getByID(int ID)
        {
            return (BasicForm.Models.DBRepresentations.Procedure)Convert.ChangeType(base.selectWhereID(ID, new BasicForm.Models.DBRepresentations.Procedure()), typeof(BasicForm.Models.DBRepresentations.Procedure));
        }

        public List<BasicForm.Models.DBRepresentations.Procedure> getAll()
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}]", DBName);
            return executeQuery(sqlQuery);
        }

        private List<BasicForm.Models.DBRepresentations.Procedure> executeQuery(string sqlQuery)
        {
            BasicForm.Models.DBRepresentations.Procedure procedure = new BasicForm.Models.DBRepresentations.Procedure();
            List<BasicForm.Models.DBRepresentations.Procedure> procedures = base.selectByQuery(sqlQuery, procedure).Cast<BasicForm.Models.DBRepresentations.Procedure>().ToList();

            return procedures;
        }

        private List<BasicForm.Models.DBRepresentations.Procedure> getAllActiveOrInactiveBasedOnParam(bool resultActive)
        {

            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "Active", resultActive ? "1" : "0");
            return executeQuery(sqlQuery);
        }

        private List<BasicForm.Models.DBRepresentations.Procedure> getByOfficeIDActiveOrInactiveBasedOnParam(int officeID, bool resultActive)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2} AND [{3}] = {4}", DBName, "OfficeID", officeID, "Active", resultActive ? "1" : "0");
            return executeQuery(sqlQuery);
        }

        public List<BasicForm.Models.DBRepresentations.Procedure> getAllActive()
        {
            return getAllActiveOrInactiveBasedOnParam(true);
        }

        public List<BasicForm.Models.DBRepresentations.Procedure> getByOfficeIDActive(int officeID)
        {
            return getByOfficeIDActiveOrInactiveBasedOnParam(officeID, true);
        }

        public List<BasicForm.Models.DBRepresentations.Procedure> getByOfficeIDAll(int officeID)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "OfficeID", officeID);
            return executeQuery(sqlQuery);
        }

    }
}
