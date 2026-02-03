using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace blogApp_DAL.Model
{
    [Table("Article")]
    public class Article
    {
        [Key]
        public string Id { get; set; }

        // title (TEXT, Not Null)
        [Required]
        public string Title { get; set; }

        // content (TEXT, Not Null)
        [Required]
        public string Content { get; set; }

        // excerpt (TEXT, Nullable)
        public string Excerpt { get; set; }

        // slug (TEXT, Nullable)
        public string Slug { get; set; }

        // cover_image (TEXT, Nullable)
        public string CoverImage { get; set; }

        // status (TEXT, Default: 'draft')
        // Устанавливаем дефолтное значение на уровне C#
        public string Status { get; set; } = "draft";

        // views (INTEGER, Default: 0)
        public int Views { get; set; } = 0;

        // read_time (INTEGER, Nullable)
        public int? ReadTime { get; set; } // Nullable int в C# для Nullable INTEGER в БД

        // created_at (DATETIME, Default: CURRENT_TIMESTAMP)
        public DateTime? CreatedAt { get; set; }

        // updated_at (DATETIME, Default: CURRENT_TIMESTAMP)
        public DateTime? UpdatedAt { get; set; }

        // published_at (DATETIME, Nullable)
        public DateTime? PublishedAt { get; set; }
    }
}
