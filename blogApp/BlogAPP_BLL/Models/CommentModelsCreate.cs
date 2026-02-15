using blogApp_DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogAPP_BLL.Models
{
    public class CommentModelsCreate
    {

        public string Content { get; set; }


        public string ArticleId { get; set; }

        public string? UserId { get; set; }

        public DateTime? CreatedAt { get; set; }

    }
}
