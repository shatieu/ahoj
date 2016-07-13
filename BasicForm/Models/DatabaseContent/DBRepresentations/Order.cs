using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBRepresentations
{
    public class Order : ARepresentation
    {
        [Key]
        public override int ID { get; set; }
        public int CustomerID { get; set; }
        public int ProcedureID { get; set; }
        public int OfficeID { get; set; }
        [Key]
        public DateTime DateAndTime { get; set; }
        public string DescProvider { get; set; }
        public string DescCustomer { get; set; }
        public static string DBName = "Order";

        public Order():base(DBName)
        {
            DateAndTime = new DateTime(2000, 1, 1);
        }

        public override ARepresentation getNewInstance()
        {
            return new Order();
        }
        

    }
}