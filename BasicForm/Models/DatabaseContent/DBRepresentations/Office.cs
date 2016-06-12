using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBRepresentations
{
    public class Office : IRepresentation
    {
        [Key]
        public int ID { get; set; }
        public int ProviderID { get; set; }
        public TimeSpan OpensAt { get; set; }
        public TimeSpan ClosesAt { get; set; }

        public IRepresentation getNewInstance()
        {
            return new Office();
        }
    }
}