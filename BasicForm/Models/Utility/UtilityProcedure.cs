using BasicForm.Models.DBRepresentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models.Utility
{
    public class UtilityProcedure
    {
        public Dictionary<int, BasicForm.Models.DBRepresentations.Procedure> getProceduresAsDictionary(List<BasicForm.Models.DBRepresentations.Procedure> procedures)
        {

            Dictionary<int, BasicForm.Models.DBRepresentations.Procedure> proceduresDic = new Dictionary<int, BasicForm.Models.DBRepresentations.Procedure>();
            foreach (BasicForm.Models.DBRepresentations.Procedure proc in procedures)
            {
                proceduresDic.Add(proc.ID, proc);
            }

            return proceduresDic;
        }

    }
}