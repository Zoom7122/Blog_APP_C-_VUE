using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace blogApp_DAL.Model
{
    [Table("Article_Authors")]
    public class Article_Authors
    {
        public string UserId { get; set; }

        // article_id (TEXT, Not Null, Part of Composite Key)
        public string ArticleId { get; set; }

        // role (TEXT, Default: 'author')
        public string Role { get; set; } = "author";

        // created_at (DATETIME, Default: CURRENT_TIMESTAMP)
        public DateTime? CreatedAt { get; set; }

        public virtual User User { get; set; }
        public virtual Article Article { get; set; }
    }
}
