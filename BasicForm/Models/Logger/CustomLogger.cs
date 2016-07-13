using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BasicForm.Models.Logger
{
    public static class CustomLogger
    {
        public enum Level
        {
            ERROR, WARN, INFO, FINE, FINEST
        }

        public static void Log(Level level,String lines)
        {
            
            string logFIlePath = HttpContext.Current.Server.MapPath("~/App_Data/loggs.txt"); 
            
                string mess = string.Format("[{0}]({1}) - {2}", level.ToString(), DateTime.Now.ToString(), lines);
                
                //System.IO.File.WriteAllText(logFIlePath,mess);
            
            

        }
}
}