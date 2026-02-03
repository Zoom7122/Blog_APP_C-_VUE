using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace blogApp_DAL.Model
{
    [Table("Reaction")]
    public class Reaction
    {
        // id (TEXT, Not Null, Primary Key)
        // Используем string, так как в БД тип TEXT
        [Key]
        public string Id { get; set; }

        // user_id (TEXT, Not Null)
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        // article_id (TEXT, Not Null)
        [Required]
        public string ArticleId { get; set; }

        // type (TEXT, Default: 'like')
        // Поле может быть Nullable в C# (string?), так как в БД есть значение по умолчанию и Not Null не указан.
        [MaxLength(11)] // Ограничение длины из схемы
        public string Type { get; set; }

        // created_at (DATETIME, Default: CURRENT_TIMESTAMP)
        // Поле может быть Nullable в C# (DateTime?), так как Not Null не указан.
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }
}
