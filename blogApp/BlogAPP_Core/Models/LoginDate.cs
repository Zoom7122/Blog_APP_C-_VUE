using System.ComponentModel.DataAnnotations;

namespace BlogAPP_Core.Models
{
    public class LoginDate
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
