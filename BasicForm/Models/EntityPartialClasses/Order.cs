namespace BasicForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(OrderMetaData))]
    public partial class Order : IEntity
    {
        public int addToDatabase()
        {
            throw new NotImplementedException();
        }
    }

    public interface OrderMetaData
    {
       
        [Key]
        int ID { get; set; }
        int CustomerID { get; set; }
        int ProcedureID { get; set; }
        int OfficeID { get; set; }
        string DescProvider { get; set; }
        string DescCustomer { get; set; }
        System.DateTime Begin { get; set; }
        System.DateTime End { get; set; } 
    }
}