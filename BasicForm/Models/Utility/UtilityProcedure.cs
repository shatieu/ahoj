using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models.Utility
{
    public class UtilityProcedure
    {
        public Dictionary<int, Procedure> getProceduresAsDictionary(List<Procedure> procedures)
        {

            Dictionary<int, Procedure> proceduresDic = new Dictionary<int, Procedure>();
            foreach (Procedure proc in procedures)
            {
                proceduresDic.Add(proc.ID, proc);
            }

            return proceduresDic;
        }

    }
}