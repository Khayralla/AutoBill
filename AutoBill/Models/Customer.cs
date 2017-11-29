using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoBill.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(40, ErrorMessage = "First name cannot be longer than 40 characters.", MinimumLength = 1)]
        [Required(ErrorMessage = "First name required")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required")]
        [StringLength(40, ErrorMessage = "Last name cannot be longer than 40 characters.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        //(e.g. Street, City, Province, Postal Code) 
        [Required(ErrorMessage = "Address required")]
        [StringLength(100, ErrorMessage = "Adress cannot be longer than 100 characters.", MinimumLength = 1)]
        [DisplayName("Address")]
        public string Address { get; set; }
    }
}
