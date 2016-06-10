using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.Models
{
    public class OCustomer
    {
        [Required(ErrorMessage = "Tohle je potreba")]
        public String Name { get; set; }
        [Required(ErrorMessage = "A tohle taky")]
        public String Surname { get; set; }
        [Required(ErrorMessage = "Dej mi i tohle")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Cislo je pozadovano")]
        public String Phone { get; set; }
        [Required(ErrorMessage = "A chci vsechno!")]
        public string BirthYear { get; set; }
        public String Description { get; set; }
        public ODateOrder OrderDate { get; set; }
        public int ProcedureID { get; set; }
        public int DoctorID { get; set; }
        public int ID { get; set; }
        public string DescriptionDoctor { get; set; }


        public OCustomer()
        {
            Name = "Jirka";
            Surname = "";
            Email = "";
            Phone = "";
            BirthYear = "";
            Description = "";
            OrderDate = new ODateOrder();
            DoctorID = 0;
            ID = 3;

            Description = "df";
        }


        public override String ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Customer ").Append(ID).Append("\n");
            foreach (var property in this.GetType().GetProperties())
            {
                sb.Append(property.Name).Append(": ").Append(property.GetValue(this).Equals("") ? "NULL" : property.GetValue(this)).Append(", ");
            }
            sb.Append("\n");
            return sb.ToString();
        }


    }
}