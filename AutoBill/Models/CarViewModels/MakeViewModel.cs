using System.Collections.Generic;

namespace AutoBill.Models.CarViewModels
{
    public class MakeViewModel
    {
        public Make Make { get; set; }
        public IEnumerable<Make> Makes { get; set; }
    }
}
