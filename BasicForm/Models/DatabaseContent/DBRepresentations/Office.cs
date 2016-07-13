using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBRepresentations
{
    public class Office : ARepresentation
    {
        [Key]
        public override int ID { get; set; }
        public int ProviderID { get; set; }
        public TimeSpan OpensAt { get; set; }
        public TimeSpan ClosesAt { get; set; }
        public Boolean Active { get; set; }
        public static string DBName = "Office";

        public Office() : base(DBName)
        {
        }

        public override ARepresentation getNewInstance()
        {
            return new Office();
        }
        
    }
}