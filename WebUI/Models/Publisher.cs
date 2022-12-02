using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class Publisher
    {
        public long Id { get; set; }

        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }
    }
}