using BasicForm.Models.DBHandler;
using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicForm.Models.DBHandler
{
    class DBHandlerOffice : DBHandlerGeneral
    {
        private string DBName = BasicForm.Models.DBRepresentations.Office.DBName;

        public bool insert(BasicForm.Models.DBRepresentations.Office office)
        {
            return base.insertRepresentation(office);
        }

        //private secure

        public BasicForm.Models.DBRepresentations.Office getByID(int ID)
        {
            return (BasicForm.Models.DBRepresentations.Office)Convert.ChangeType(base.selectWhereID(ID, new BasicForm.Models.DBRepresentations.Office()), typeof(BasicForm.Models.DBRepresentations.Office));
        }

        public List<BasicForm.Models.DBRepresentations.Office> getAll()
        {
            BasicForm.Models.DBRepresentations.Office office = new BasicForm.Models.DBRepresentations.Office();
            List<BasicForm.Models.DBRepresentations.Office> offices = base.dBGetAll(office, DBName).Cast<BasicForm.Models.DBRepresentations.Office>().ToList();

            return offices;
        }

        private List<BasicForm.Models.DBRepresentations.Office> executeQuery(string sqlQuery)
        {
            BasicForm.Models.DBRepresentations.Office office = new BasicForm.Models.DBRepresentations.Office();
            List<BasicForm.Models.DBRepresentations.Office> offices = base.selectByQuery(sqlQuery, office).Cast<BasicForm.Models.DBRepresentations.Office>().ToList();

            return offices;
        }

        private List<BasicForm.Models.DBRepresentations.Office> getAllActiveOrInactiveBasedOnParam(bool resultActive)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "Active", resultActive ? "1" : "0");
            return executeQuery(sqlQuery);
        }

        private List<BasicForm.Models.DBRepresentations.Office> getByProviderIDActiveOrInactiveBasedOnParam(int providerID, bool resultActive)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2} AND [{3}] = {4}", DBName, "ProviderID", providerID, "Active", resultActive ? "1" : "0");
            return executeQuery(sqlQuery);
        }

        public List<BasicForm.Models.DBRepresentations.Office> getAllActive()
        {
            return getAllActiveOrInactiveBasedOnParam(true);
        }

        public List<BasicForm.Models.DBRepresentations.Office> getByProviderIDActive(int providerID)
        {
            return getByProviderIDActiveOrInactiveBasedOnParam(providerID,true);
        }

        public List<BasicForm.Models.DBRepresentations.Office> getByProviderIDAll(int providerID)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "ProviderID", providerID);
            return executeQuery(sqlQuery);
        }
    }
}
