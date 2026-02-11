using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace blogApp_DAL.Model
{
    [Table("Tag")]
    public class Tag
    {
        [Key]
        public string Id { get; set; }

        // name (TEXT, Not Null)
        [Required] // Указывает, что поле обязательно и в БД станет NOT NULL
        public string Name { get; set; }

        [Required]
        public DateTime Created_At { get; set; } // Nullable DateTime в C#
    }
}
