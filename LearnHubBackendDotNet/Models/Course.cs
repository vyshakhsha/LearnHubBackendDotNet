using System.ComponentModel.DataAnnotations;

namespace LearnHubBackendDotNet.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string author { get; set; } = string.Empty ;

        [Required]
        public string category { get; set; } = string.Empty;

        [Required]
        public string image { get; set; } = string.Empty;

        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        public decimal price { get; set; } 

        [Required]
        public int rating { get; set; }

        [Required]
        public int students { get; set; }

    }
}
 