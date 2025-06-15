﻿using System.ComponentModel.DataAnnotations;

namespace LearnHubBackendDotNet.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string usertype { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
