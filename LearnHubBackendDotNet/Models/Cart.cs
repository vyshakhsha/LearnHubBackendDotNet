using System.ComponentModel.DataAnnotations;

namespace LearnHubBackendDotNet.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int userId { get; set; } = 0;

        [Required]
        public int courseId { get; set; } = 0;

        [Required]
        public decimal price { get; set; } = 0;

        [Required]
        public string status { get; set; } = string.Empty;
    }
}
