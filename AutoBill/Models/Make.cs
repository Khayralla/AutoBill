using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoBill.Models
{
    // e.g. Volkswagon, Ford, Toyota
    public class Make
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MakeId { get; set; }

        [Required(ErrorMessage = "Make required")]
        [StringLength(50, ErrorMessage = "Make cannot be longer than 50 characters.", MinimumLength = 1)]
        [DisplayName("Make")]
        public string MakeName { get; set; }
    }
}
