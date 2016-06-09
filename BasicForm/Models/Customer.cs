using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.Models
{
    public class Customer
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
        public String BirthYear { get; set; }
        public String Description { get; set; }
        public DateOrder OrderDate { get; set; }
        public int OrderTime { get; set; }
        public int DoctorID { get; set; }
        public int ID { get; set; }

        public Customer()
        {
            Name = "Jirka";
            Surname = "";
            Email = "";
            Phone = "";
            BirthYear = "";
            Description = "";
            OrderDate = new DateOrder();
            OrderTime = 0;
            DoctorID = 0;
            ID = 0;

            Description = "df";
        }

        
        public override String ToString()
        {
            return string.Format("Customer {0} \n (Name: {1}, Surname: {2},Email: {3},Phone: {4},Birth year: {5},Order date: {6},Order time: {7},Desc: {8},Dorctor: {9})",ID, Name, Surname, Email, Phone, BirthYear, OrderDate.ToString(), OrderTime, Description, DoctorID);

        }


    }
}