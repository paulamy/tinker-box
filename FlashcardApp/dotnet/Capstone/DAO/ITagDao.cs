using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ITagDao
    {
        List<Tag> GetAllTags();

        Tag GetTagByName(string tagName);

        Tag GetTagByID(int tagID);
    }
}
