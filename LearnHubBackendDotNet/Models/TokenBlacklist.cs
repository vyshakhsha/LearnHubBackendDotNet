using System;

namespace LearnHubBackendDotNet.Models
{
    public class TokenBlacklist
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
