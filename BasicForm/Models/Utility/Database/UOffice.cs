using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models.Utility.Database
{

    public static partial class UDatabase
    {
        /// <summary>
        /// This class represents work with database and Office
        /// This is sinle instance class == Singleton implementation
        /// </summary>
        public static class UOffice
        {
            
            /// <summary>
            /// Get office with currrent id
            /// </summary>
            /// <param name="id">identificator of offici</param>
            /// <param name="hasToBeActive">if true, looking only for active office</param>
            /// <returns>null if no (active if hasToBeActive==true) office, instance of Office otherwise</returns>
            public static Office getOffice(int id, bool hasToBeActive)
            {
                Office office;

                using (CalendarEntities db = new CalendarEntities())
                {
                    office = (from off in db.Offices
                              where (off.ID == id)
                              select off).SingleOrDefault();

                }

                return office;
            }

        }
    }
}