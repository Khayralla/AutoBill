using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoBill.Models
{
    public class Car
    {
        //Vehicle ID Number (VIN)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(17, ErrorMessage = "VIN number cannot be longer than 17 characters.", MinimumLength = 17)]
        [DisplayName("VIN")]
        public string VIN { get; set; }

        //e.g. 1994
        [Required(ErrorMessage = "Year required")]
        [DisplayName("Year")]
        public int Year { get; set; } = DateTime.Now.Year;

        // e.g. Volkswagon, Ford, Toyota
        [Required(ErrorMessage = "Make required")]
        [DisplayName("Make")]
        public int MakeId { get; set; }

        //e.g. Jetta, Taurus, Corolla
        [DisplayName("Model")]
        public int ModelId { get; set; }

        //e.g. 4 dr Sedan, pickup
        [DisplayName("BodyType")]
        public int BodyTypeId { get; set; }

        [DisplayName("Odometer")]
        public int Odometer { get; set; }

        // false = Miles
        [DisplayName("Kilometres")]
        public bool Kilometres { get; set; }

        [Required(ErrorMessage = "Color required")]
        [StringLength(20, ErrorMessage = "Color cannot be longer than 20 characters.", MinimumLength = 1)]
        [DisplayName("Color")]
        public string Color { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public bool Sold { get; set; } = false;
    }
}