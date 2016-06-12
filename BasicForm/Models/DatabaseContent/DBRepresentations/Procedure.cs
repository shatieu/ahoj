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
        public int Lasts { get; set; }
        public int OfficeID { get; set; }
        public static string DBName = "Procedure";


        public override ARepresentation getNewInstance()
        {
            return new Procedure();
        }
        
    }
}