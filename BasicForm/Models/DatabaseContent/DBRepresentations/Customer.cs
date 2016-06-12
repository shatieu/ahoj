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
        [Key]
        [Required(ErrorMessage = "Dej mi i tohle")]
        [EmailAddress]
        public String Email { get; set; }
        [Phone]
        [Required(ErrorMessage = "Cislo je pozadovano")]
        public String Phone { get; set; }
        [Required(ErrorMessage = "A chci vsechno!")]
        public int BirthYear { get; set; }
        public static string DBName = "Customer";

        public override ARepresentation getNewInstance()
        {
            return new Customer();
        }
        

    }
}