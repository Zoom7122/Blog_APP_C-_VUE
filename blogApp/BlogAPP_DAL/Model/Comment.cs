using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace blogApp_DAL.Model
{
    [Table("Comment")]
    public class Comment
    {
        // id (TEXT, Not Null, Primary Key)
        [Key]
        public string Id { get; set; }

        // content (TEXT, Not Null)
        [Required]
        public string Content { get; set; }

        // article_id (TEXT, Not Null)
        [Required]
        public string ArticleId { get; set; }

        // user_id (TEXT, Not Null)
        [Required]
        public string UserId { get; set; }

        // created_at (DATETIME, Default: CURRENT_TIMESTAMP)
        public DateTime? CreatedAt { get; set; }

        public virtual User User { get; set; }

        public virtual Article Article { get; set; } // Предполагается, что класс Article существует
    }
}
