using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoBill.Models.AutoSaleBillViewModels
{
    public class InsuranceViewModel
    {
        [Required(ErrorMessage = "Insurance name required")]
        [StringLength(100, ErrorMessage = "Insurance name cannot be longer than 100 characters.", MinimumLength = 1)]
        [DisplayName("Insurance Name")]
        public string InsuranceName { get; set; }
    }
}
