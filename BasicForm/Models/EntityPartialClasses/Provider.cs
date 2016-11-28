
namespace BasicForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProviderMetaData))]
    public partial class Provider : IEntity
    {
        public int addToDatabase()
        {
            throw new NotImplementedException();
        }
    }

    public interface ProviderMetaData
    {
        
       
        [Key]
        int ID { get; set; }
        string Name { get; set; }
        [Key]
        [EmailAddress]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        string Surname { get; set; }
        [EmailAddress]
        string Email { get; set; }
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        string PassHashed { get; set; }
        bool Payed { get; set; }
    }
}