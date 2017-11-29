using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoBill.Models
{
    public class ManageUsersViewModel
    {
        public IEnumerable<ApplicationUser> Administrators { get; set; }

        public IEnumerable<ApplicationUser> Everyone { get; set; }
    }
}
