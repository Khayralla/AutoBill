using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoBill.Models
{
    //e.g. 4 dr Sedan, pickup
    public class BodyType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BodyTypeId { get; set; }

        [Required(ErrorMessage = "Body type required")]
        [StringLength(50, ErrorMessage = "Body type cannot be longer than 50 characters.", MinimumLength = 1)]
        [DisplayName("Body Type")]
        public string BodyTypeName { get; set; }
    }
}
