using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_Core.Models
{
    public class CreateArticleModel
    {
        public string Title { get; set; }

        public List<string> Tag { get; set; }

        public string CoverImage { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public int ReadTime { get; set; }

    }
}
