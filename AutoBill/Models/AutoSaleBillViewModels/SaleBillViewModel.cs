using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AutoBill.Models.AutoSaleBillViewModels
{
    public class SaleBillViewModel
    {
        public CustomerViewModel Customer { get; set; }

        public CarViewModel Car { get; set; }

        public CarViewModel CarTradeWith { get; set; }

        public InsuranceViewModel Insurance { get; set; }

        public SaleBill SaleBill { get; set; }

        public List<SelectListItem> Makes { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> BodyTypes { get; set; } = new List<SelectListItem>();

    }
}
