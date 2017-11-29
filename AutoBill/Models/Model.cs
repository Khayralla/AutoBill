using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoBill.Models
{
    //e.g. Jetta, Taurus, Corolla
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; set; }

        public int MakeId { get; set; }

        [Required(ErrorMessage = "Model required")]
        [StringLength(50, ErrorMessage = "Model name cannot be longer than 50 characters.", MinimumLength = 1)]
        [DisplayName("Model")]
        public string ModelName { get; set; }
    }
}
