using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime Created_at { get; set; }
        public string Avatar_url { get; set; }
        public string Bio { get; set; }
        public string Role { get; set; }
    }
}
