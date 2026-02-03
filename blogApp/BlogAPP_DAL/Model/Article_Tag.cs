using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace blogApp_DAL.Model
{
    [Table("Article_Tag")]
    public class Article_Tag
    {
        // tag_id (TEXT, Not Null, Part of Composite Key)
        public string TagId { get; set; }

        // article_id (TEXT, Not Null, Part of Composite Key)
        public string ArticleId { get; set; }

        // created_at (DATETIME, Default: CURRENT_TIMESTAMP, Nullable в схеме)
        public DateTime? CreatedAt { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Article Article { get; set; }
    }
}
