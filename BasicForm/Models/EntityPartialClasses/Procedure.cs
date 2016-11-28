namespace BasicForm
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProcedureMetaData))]
    public partial class Procedure : IEntity
    {
        public int addToDatabase()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets from DB all procedures that has proper OfficeID and Active==true
        /// </summary>
        /// <param name="OfficeID"></param>
        /// <returns>All procedures that has proper OfficeID and Active==true</returns>
        public static List<Procedure> getActiveProcedures(int OfficeID)
        {
            List<Procedure> proceduresActive;

            using (CalendarEntities entities = new CalendarEntities())
            {
                proceduresActive = (from p in entities.Procedures
                                    where (p.OfficeID.Equals(OfficeID) && p.Active == true)
                                    select p).ToList();
            }

            return proceduresActive;
        }

    }

    public interface ProcedureMetaData
    {
        [Key]
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Lasts { get; set; }
        int OfficeID { get; set; }
        bool Active { get; set; }

    }
}