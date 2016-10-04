using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBRepresentations
{
    public class Provider : ARepresentation
    {
        [Key]
        public override int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Key]
        [EmailAddress]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string Email { get; set; }
        public string PassHashed { get; set; }
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public Boolean Payed { get; set; }
        public static string DBName = "Provider";

        public Provider() : base(DBName)
        {
        }

        public override ARepresentation getNewInstance()
        {
            return new Provider();
        }
        
    }
}