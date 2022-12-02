using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class Company
    {
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public CompanyContact CompanyContact { get; set; }
    }
}