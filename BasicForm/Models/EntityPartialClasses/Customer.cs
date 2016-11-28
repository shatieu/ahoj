
namespace BasicForm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(CustomerMetaData))]
    public partial class Customer : IEntity
    {
        public int addToDatabase()
        {
            throw new NotImplementedException();
        }
    }



    public interface CustomerMetaData
    {

        [Key]
        int ID { get; set; }
        [Required(ErrorMessage = "Tohle je potreba")]
        string Name { get; set; }
        [Required(ErrorMessage = "A tohle taky")]
        string Surname { get; set; }
        [EmailAddress]
        string Email { get; set; }
        [Required(ErrorMessage = "A chci vsechno!")]
        [RegularExpression(@"^([0-9]{6}[/][0-9]{4})$")]
        string PersonalNumber { get; set; }
        [Phone]
        [Required(ErrorMessage = "Cislo je pozadovano")]
        string Phone { get; set; }
    }
}