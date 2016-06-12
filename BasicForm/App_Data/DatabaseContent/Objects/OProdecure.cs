using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models
{
    public class OProdecure
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TimeTenMinutes { get; set; }
        public int DoctorID { get; set; }
        public static string DBName = "Procedure";

        /// <summary>
        /// Takes string from database and parse it into object
        /// </summary>
        /// <param name="DateFromDatabase"> String to parse in format YYYY_MM_DD_HH:MM </param>
        public OProdecure(int IDFromDatabase)
        {
            ID = IDFromDatabase;
        }

        


    }
}