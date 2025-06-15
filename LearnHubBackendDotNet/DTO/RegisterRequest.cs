using System.ComponentModel.DataAnnotations;

namespace LearnHubBackendDotNet.DTO
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string usertype { get; set; }
        public string PasswordHash { get; set; }
    }
}
