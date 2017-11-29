using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoBill.Models
{
    public class Insurance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insurance name required")]
        [StringLength(100, ErrorMessage = "Insurance name cannot be longer than 100 characters.", MinimumLength = 1)]
        [DisplayName("Insurance Name")]
        public string InsuranceName { get; set; }
    }
}
