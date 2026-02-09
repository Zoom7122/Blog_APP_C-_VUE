using blogApp_DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogAPP_DAL.Intarface
{
    public interface ITagRepo
    {
        Task CreateTag(Tag tag);

        Task<List<Tag>> FindTagByName(string name);

        Task<Tag> FindTagById(string id);

    }
}
