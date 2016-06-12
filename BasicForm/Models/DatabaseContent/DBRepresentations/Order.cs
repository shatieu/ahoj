using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.Models.DBRepresentations
{
    public class Order : IRepresentation
    {
        [Key]
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ProcedureID { get; set; }
        public int OfficeID { get; set; }
        [Key]
        public DateTime DateAndTime { get; set; }
        public string DescProvider { get; set; }
        public string DescCustomer { get; set; }

        public IRepresentation getNewInstance()
        {
            return new Order();
        }

    }
}