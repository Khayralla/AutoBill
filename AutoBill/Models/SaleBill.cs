using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoBill.Models
{
    public class SaleBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string AgentId { get; set; }

        public long CustomerId { get; set; }

        [StringLength(17, ErrorMessage = "VIN number cannot be longer than 17 characters.", MinimumLength = 17)]
        [DisplayName("VIN")]
        public string CarVin { get; set; }

        [StringLength(17, ErrorMessage = "VIN number cannot be longer than 17 characters.", MinimumLength = 17)]
        [DisplayName("VIN")]
        public string CarTradeWithVin { get; set; }

        public int InsuranceId { get; set; }

        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DisplayName("Tax")]
        public decimal Tax { get; set; } = 13;

        [DisplayName("Sales tax")]
        public int SalesTax { get; set; } //= 1;// SalesTaxes.IncludedInPrice;

        [DisplayName("Payment form")]
        public int PaymentForm { get; set; } //= 2;// PaymentForm.Cash;

        public decimal Total { get; set; }

    }
}
