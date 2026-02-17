using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_BLL.Models
{
    public class ArticleReturnInAPI
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

        public string Description { get; set; }

        public string Cover_image { get; set; }

        public string Author_Name { get; set; }

        public string Author_Email { get; set; }

        public int? ReadTime { get; set; } 

        public DateTime? PublishedAt { get; set; }

        public List<CommentsViewModel>? comments { get; set; } = new List<CommentsViewModel>();

    }
}
