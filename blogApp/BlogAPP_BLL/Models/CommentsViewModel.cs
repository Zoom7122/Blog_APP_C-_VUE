using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Models
{
    public class CommentsViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
