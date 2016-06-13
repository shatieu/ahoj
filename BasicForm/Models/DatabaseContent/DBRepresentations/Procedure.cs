using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBRepresentations
{
    public class Procedure : ARepresentation
    {
        [Key]
        public override int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private int lasts;
        public int Lasts {
            get
            {
                if (lasts % 10 != 0)
                {
                    return (lasts - lasts % 10) + 10;
                }else {
                    return lasts;
                }
                
            }
            set
            {
                lasts = value;
            }
        }
        public int OfficeID { get; set; }
        public Boolean Active { get; set; }
        public static string DBName = "Procedure";


        public override ARepresentation getNewInstance()
        {
            return new Procedure();
        }
        
    }
}