using BasicForm.Models.DBHandler;
using BasicForm.Models.DBRepresentations;
using BasicForm.Models.Logger;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BasicForm.Models.Utility
{
    public class UtilityOrder
    {

        /// <summary>
        /// takes mounth and year and return list of taken times in this date
        /// </summary>
        /// <param name="mounth">to be found in</param>
        /// <param name="year">to be found in</param>
        /// <returns>Set of strings where strings are in format DD_HHMM</returns>
        public List<String> getTakenTimesMonthYear(int officeID, int month, int year)
        {
            DBHandlerProcedure hProcedure = new DBHandlerProcedure();
            List<string> formatedOrders = new List<string>();
            DateTime time;
            StringBuilder sb = new StringBuilder();
            DBHandlerOrder hOrder = new DBHandlerOrder();
            UtilityProcedure uProcedure = new UtilityProcedure();

            Dictionary<int, BasicForm.Models.DBRepresentations.Procedure> proceduresDic = uProcedure.getProceduresAsDictionary(hProcedure.getByOfficeIDAll(officeID));

            List<BasicForm.Models.DBRepresentations.Order> ordersInDate = hOrder.getByOfficeIDInMonthYear(officeID, month, year);
            try
            {
                foreach (BasicForm.Models.DBRepresentations.Order order in ordersInDate)
                {
                    for (int i = 0; i < proceduresDic[order.ProcedureID].Lasts; i = i + 10)
                    {
                        time = order.DateAndTime;
                        time = time.AddMinutes(i);

                        sb.Clear();
                        sb.Append(time.Day).Append("_").Append(time.Hour).Append(":").Append(time.Minute);
                        formatedOrders.Add(sb.ToString());
                    }
                }
            }catch(Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                CustomLogger.Log(CustomLogger.Level.ERROR, e.ToString());
                Console.WriteLine(e.ToString());
            }

            return formatedOrders;
        }

    }
}