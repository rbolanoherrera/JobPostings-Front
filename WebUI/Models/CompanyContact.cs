using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class CompanyContact
    {
        public int CompanyId { get; set; }

        [Display(Name ="Contact number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}