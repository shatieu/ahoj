using System;
using System.Collections.Generic;
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
            using (var file = new System.IO.StreamWriter("~/custom_logs.txt", true))
            {
                file.WriteLine("[{0}]({1}) - {2}", level.ToString(), DateTime.Now.ToString(), lines);
            }
            

        }
}
}