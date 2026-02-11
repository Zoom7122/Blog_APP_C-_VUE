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
        [Column("Tag_id")]
        public string Tag_id { get; set; }

        [Column("Article_id")]
        public string Article_id { get; set; }

        [Column("Created_at")]
        public DateTime Created_at { get; set; }

        [ForeignKey("Tag_id")]
        public virtual Tag Tag { get; set; }

        [ForeignKey("Article_id")]
        public virtual Article Article { get; set; }
    }
}
