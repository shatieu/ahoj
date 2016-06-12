using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBRepresentations
{
    public class Provider : IRepresentation
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Key]
        [EmailAddress]
        public string Email { get; set; }
        public string PassHashed { get; set; }
        public Boolean Payed { get; set; }

        public IRepresentation getNewInstance()
        {
            return new Provider();
        }

    }
}