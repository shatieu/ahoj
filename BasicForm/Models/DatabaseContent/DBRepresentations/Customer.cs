using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBRepresentations
{
    public class Customer: ARepresentation
    {
        [Key]
        public override int ID { get; set; }
        [Required(ErrorMessage = "Tohle je potreba")]
        public String Name { get; set; }
        [Required(ErrorMessage = "A tohle taky")]
        public String Surname { get; set; }
        [EmailAddress]
        public String Email { get; set; }
        [Phone]
        [Required(ErrorMessage = "Cislo je pozadovano")]
        public String Phone { get; set; }
        [Required(ErrorMessage = "A chci vsechno!")]
        [RegularExpression(@"^([0-9]{6}[/][0-9]{4})$")]
        public string PersonalNumber { get; set; }
        public static string DBName = "Customer";

        public override ARepresentation getNewInstance()
        {
            return new Customer();
        }
        

    }
}