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
        private string DBName = Office.DBName;

        public bool insert(Office office)
        {
            return base.dBInsertRepresentation(office, DBName);
        }

        //private secure

        public Office getByID(int ID)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "ID", ID);
            List<Office> toReturn = executeQuery(sqlQuery);
            return toReturn.ElementAt(0);
        }

        public List<Office> getAll()
        {
            Office office = new Office();
            List<Office> offices = base.dBGetAll(office, DBName).Cast<Office>().ToList();

            return offices;
        }

        private List<Office> executeQuery(string sqlQuery)
        {
            Office office = new Office();
            List<Office> offices = base.dBGetAllWhere(sqlQuery, office).Cast<Office>().ToList();

            return offices;
        }

        private List<Office> getAllActiveOrInactiveBasedOnParam(bool resultActive)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "Active", resultActive ? "1" : "0");
            return executeQuery(sqlQuery);
        }

        private List<Office> getByProviderIDActiveOrInactiveBasedOnParam(int providerID, bool resultActive)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2} AND [{3}] = {4}", DBName, "ProviderID", providerID, "Active", resultActive ? "1" : "0");
            return executeQuery(sqlQuery);
        }

        public List<Office> getAllActive()
        {
            return getAllActiveOrInactiveBasedOnParam(true);
        }

        public List<Office> getByProviderIDActive(int providerID)
        {
            return getByProviderIDActiveOrInactiveBasedOnParam(providerID,true);
        }

        public List<Office> getByProviderIDAll(int providerID)
        {
            string sqlQuery = string.Format("SELECT * FROM [{0}] WHERE [{1}] = {2}", DBName, "ProviderID", providerID);
            return executeQuery(sqlQuery);
        }
    }
}
