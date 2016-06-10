using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicForm.Models
{
    public class ODateOrder
    {
        private int _day;
        public int Day
        {
            get
            {
                return _day;
            }
            set
            {
                if(Month == 0)
                {
                    Month = 1;
                }

                if(Year == 0)
                {
                    Year = 1;
                }


                int daysInMonth = DateTime.DaysInMonth(Year, Month);
                if (value > daysInMonth)
                {
                    Month++;
                    Day = value - daysInMonth;
                }
                else
                {
                    _day = value;
                }
            }
        }
        public int Year { get; set; }
        private int _month;
        public int Month {
            get
            {
                return _month;
            }
            set
            {
               if(value > 12)
                {
                   Year++;
                    Month = value - 12;
                }else
                {
                    _month = value;
                }
            }
            }
        private int _hour;
        public int Hour {
            get
            {
                return _hour;
            }
            set
            {
                
               if(value >= 24)
                {
                    Day++;
                    Hour = value - 24;
                }else
                {
                    _hour = value;
                }
            }
            }
        private int _minute;
        public int Minute {
            get
            {
                return _minute;
            }
            set
            {
                if (value >= 60)
                {
                    Hour++;
                    Minute = value - 60;
                }
                else
                {

                    _minute = value;
                    //route minute down to tens
                    _minute = (_minute / 10) * 10;
                }
            }
        }


        public ODateOrder()
        {
            Day = Year = Hour = Minute = 0;
            
        }

        /// <summary>
        /// Saves values
        /// </summary>
        /// <param name="_day">day</param>
        /// <param name="_month">month</param>
        /// <param name="_year">year</param>
        /// <param name="_minute">minute</param>
        /// <param name="_hour">hour</param>
        public ODateOrder(int day, int month, int year, int minute, int hour)
        {
            Day = day;
            Month = month;
            Year = year;
            Minute = minute;
            Hour = hour;
        }


        /// <summary>
        /// Takes string from database and parse it into object
        /// </summary>
        /// <param name="DateFromDatabase"> String to parse in format YYYY_MM_DD_HH:MM </param>
        public ODateOrder(String DateFromDatabase)
        {
            ParserFromDB(DateFromDatabase);
        }

        /// <summary>
        /// Takes string from database and parse it into object
        /// </summary>
        /// <param name="DateFromDatabase"> String to parse in format YYYY_MM_DD_HH:MM </param>
        public void ParserFromDB(String DateFromDatabase)
        {
            string[] DBsplit = DateFromDatabase.Split('_');
            Year = Int32.Parse(DBsplit[0]);
            Month = Int32.Parse(DBsplit[1]);
            Day = Int32.Parse(DBsplit[2]);

            string[] hourMin = DBsplit[3].Split(':');
            Hour = Int32.Parse(hourMin[0]);
            Minute = Int32.Parse(hourMin[1]);
        }


        /// <summary>
        /// Parse this object into string 
        /// </summary>
        /// <returns>String in format YYYY_MM_DD_HH:MM </returns>
        public override String ToString()
        {
            return string.Format("{0}_{1}_{2}_{3}:{4}", Year, Month, Day, Hour, Minute);
        }


        /// <summary>
        /// This method return only values that are requested in params
        /// </summary>
        /// <param name="year">if true, returns year in format</param>
        /// <param name="month">if true, returns month in format</param>
        /// <param name="day">if true, returns day in format</param>
        /// <param name="hours">Both hour and minute. if true, returns hours in format</param>
        /// <returns>format YYYY_MM_DD_HH:MM. If some param is false, param part will be missing (with proper cleanup of char "_")</returns>
        public string getValues(bool year, bool month, bool day, bool hours)
        {
            String formated;

            //saves param in required and if some other parameter is required too, place "_" after himself.
            formated = (year == true ? Year.ToString()+ ((month || day || hours) ? "_" : "") : "");
            formated += (month == true ? Month.ToString() + (( day || hours) ? "_" : "") : "");
            formated += (day == true ? Day.ToString() + (hours ? "_" : "") : "");
            //saves param in required 
            formated += (hours == true ? Hour.ToString()+":"+Minute.ToString() : "");

            return formated;
        }
        
    }
}