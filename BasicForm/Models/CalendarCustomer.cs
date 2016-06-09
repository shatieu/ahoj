using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models
{
    public class CalendarCustomer
    {
        public Customer Cust { get; set; }
        public String Date { get; set; }
        public String Time { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int DaysInMonth { get; set; }
        public int firstOfMonth { get; set; }
        public int DaysInLastMonth { get; set; }
        public HashSet<String> TakenTime { get; set; }
        
        //private SortedSet<String> TakenTimes;


        public CalendarCustomer()
        {
            Month = DateTime.Today.Month+1;
            Year = DateTime.Today.Year;
            DaysInMonth = DateTime.DaysInMonth(Year, Month);


            
            Cust = new Customer();
            TakenTime = getTakenTimes(Month,Year);

            switch(new DateTime(Year, Month, 1).DayOfWeek)
            {
                case DayOfWeek.Monday: firstOfMonth = 0;
                    break;
                case DayOfWeek.Tuesday:
                    firstOfMonth = 1;
                    break;
                case DayOfWeek.Thursday:
                    firstOfMonth = 2;
                    break;
                case DayOfWeek.Wednesday:
                    firstOfMonth = 3;
                    break;
                case DayOfWeek.Friday:
                    firstOfMonth = 4;
                    break;
                case DayOfWeek.Saturday:
                    firstOfMonth = 5;
                    break;
                case DayOfWeek.Sunday:
                    firstOfMonth = 6;
                    break;
            }
            
            if(Month == 1)
            {
                DaysInLastMonth = 31;
            }else
            {
                DaysInLastMonth = DateTime.DaysInMonth(Year, Month-1);
            }
            
        }


        /// <summary>
        /// takes mounth and year and return list of taken times in this date
        /// </summary>
        /// <param name="mounth">to be found in</param>
        /// <param name="year">to be found in</param>
        /// <returns>Set of strings where strings are in format DD_HH:MM</returns>
        public HashSet<String> getTakenTimes(int mounth, int year)
        {
            List<String> takenTimes = new DBCustomer().getTakenTimes(mounth, year);
            HashSet<String> toSave = new HashSet<string>();
            DateOrder date = new DateOrder();
            String dateDatabaseFormat;
            String outputFormat;
            int lasts;

            foreach (String takenTime in takenTimes) {
                dateDatabaseFormat = takenTime.Split('-')[0];
                lasts = Int32.Parse(takenTime.Split('-')[1]);
                date.ParserFromDB(dateDatabaseFormat);

                date.ParserFromDB(dateDatabaseFormat);

                //saves all times that customer is in Doctors office
                for(int i = 0;i < lasts ; i++)
                {
                    date.Minute += i*10;
                    outputFormat = date.getValues(false, false, true, true);
                    toSave.Add(outputFormat);

                }
            }


            return toSave;
            
        }



        /// <summary>
        /// Count down new values for Month and Year (those two are now generated)
        /// </summary>
        public void reSetValues()
        {
            if(Month > 12)
            {
                Year++;
                Month -= 12;
            }

            if (Month < 1)
            {
                Year--;
                Month += 12;
            }


            DaysInMonth = DateTime.DaysInMonth(Year, Month);

            //Cust = new Customer();
            TakenTime = getTakenTimes(Month, Year);

            switch (new DateTime(Year, Month, 1).DayOfWeek)
            {
                case DayOfWeek.Monday:
                    firstOfMonth = 0;
                    break;
                case DayOfWeek.Tuesday:
                    firstOfMonth = 1;
                    break;
                case DayOfWeek.Thursday:
                    firstOfMonth = 2;
                    break;
                case DayOfWeek.Wednesday:
                    firstOfMonth = 3;
                    break;
                case DayOfWeek.Friday:
                    firstOfMonth = 4;
                    break;
                case DayOfWeek.Saturday:
                    firstOfMonth = 5;
                    break;
                case DayOfWeek.Sunday:
                    firstOfMonth = 6;
                    break;
            }

            if (Month == 1)
            {
                DaysInLastMonth = 31;
            }
            else
            {
                DaysInLastMonth = DateTime.DaysInMonth(Year, Month - 1);
            }
            
        }


        public void increaseMonth()
        {
            Month++;

        }

    }
}