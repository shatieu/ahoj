namespace BasicForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(OfficeMetaData))]
    public partial class Office : IEntity
    {
        public int addToDatabase()
        {
            throw new NotImplementedException();
        }
    }

    public interface OfficeMetaData
    {
        [Key]
        int ID { get; set; }
        int ProviderID { get; set; }
        System.TimeSpan OpenMo { get; set; }
        System.TimeSpan CloseMo { get; set; }
        bool Active { get; set; }
        System.TimeSpan OpenTu { get; set; }
        System.TimeSpan CloseTu { get; set; }
        System.TimeSpan OpenWe { get; set; }
        System.TimeSpan CloseWe { get; set; }
        System.TimeSpan OpenTh { get; set; }
        System.TimeSpan CloseTh { get; set; }
        System.TimeSpan OpenFr { get; set; }
        System.TimeSpan CloseFr { get; set; }
        System.TimeSpan OpenSa { get; set; }
        System.TimeSpan CloseSa { get; set; }
        System.TimeSpan OpenSu { get; set; }
        System.TimeSpan CloseSu { get; set; }

    }
}