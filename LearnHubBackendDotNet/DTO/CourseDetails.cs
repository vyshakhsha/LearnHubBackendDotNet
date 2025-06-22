using System.ComponentModel.DataAnnotations;

namespace LearnHubBackendDotNet.DTO
{
    public class CourseDetailsDto
    {
        public int id { get; set; }

        public string AuthorEmail { get; set; } = string.Empty;

        
        public string author { get; set; } = string.Empty;

        
        public string category { get; set; } = string.Empty;

        
        public string image { get; set; } = string.Empty;

        
        public string courseName { get; set; } = string.Empty;

        
        public decimal price { get; set; }

        
        public int rating { get; set; }

        
        public int students { get; set; }
    }
}
