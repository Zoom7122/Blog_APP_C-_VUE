using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogAPP_Core.Models
{
    public class UserEnrance
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Avatar_url { get; set; }

        public string Bio { get; set; }

        public string Role { get; set; }

        public int CountPost { get; set; }
    }
}
