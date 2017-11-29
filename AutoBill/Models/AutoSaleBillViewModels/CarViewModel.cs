using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoBill.Models.AutoSaleBillViewModels
{
    public class CarViewModel
    {
        //Vehicle ID Number (VIN)
        [Required(ErrorMessage = "VIN required")]
        [StringLength(17, ErrorMessage = "VIN number cannot be longer than 17 characters.", MinimumLength = 17)]
        [DisplayName("VIN")]
        public string VIN { get; set; }

        // e.g. Volkswagon, Ford, Toyota
        [Required(ErrorMessage = "Make required")]
       // [StringLength(50, ErrorMessage = "Make cannot be longer than 50 characters.", MinimumLength = 1)]
        [DisplayName("Make")]
        public int MakeId { get; set; }

        //e.g. Jetta, Taurus, Corolla
        [Required(ErrorMessage = "Model required")]
        // [StringLength(50, ErrorMessage = "Model cannot be longer than 50 characters.", MinimumLength = 1)]
        [DisplayName("Model")]
        public int ModelId { get; set; }

        //e.g. 4 dr Sedan, pickup
        [Required(ErrorMessage = "Body Type required")]
        [DisplayName("Body Type")]
        public int BodyTypeId { get; set; }

        //e.g. 1994
        [Required(ErrorMessage = "Year required")]
        [DisplayName("Year")]
        public int Year { get; set; } = DateTime.Now.Year;

        [DisplayName("Odometer")]
        public int Odometer { get; set; }

        // false = Miles
        [DisplayName("Kilometres")]
        public bool Kilometres { get; set; }

        [Required(ErrorMessage = "Color required")]
        [StringLength(20, ErrorMessage = "Color cannot be longer than 20 characters.", MinimumLength = 1)]
        [DisplayName("Color")]
        public string Color { get; set; }
    }
}
