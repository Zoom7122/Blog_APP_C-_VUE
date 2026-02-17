using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogAPP_Core.Models
{
    public class CreateUserDto
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public string? Avatar_url { get; set; }
        public string? Bio { get; set; }

        public string Role { get; set; } = "User"; // Значение по умолчанию
    }
}
