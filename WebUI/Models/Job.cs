using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class Job
    {
        public long Id { get; set; }

        [Required]
        [Display(Name ="Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Category")]
        public long CategoryId { get; set; }

        [Display(Name = "Publisher")]
        public long PublisherId { get; set; }

        public JobCategory Category { get; set; }
        public Publisher Publisher { get; set; }

    }
}