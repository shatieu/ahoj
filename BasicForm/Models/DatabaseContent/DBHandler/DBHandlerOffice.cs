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

        public List<Office> getAll()
        {
            Office office = new Office();
            List<Office> offices = base.dBGetAll(office, Office.DBName).Cast<Office>().ToList();

            return offices;
        }


    }
}
