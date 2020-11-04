using BlogEngine.Core.Data.Entities.Common;
using System.Collections.Generic;

namespace BlogEngine.Core.Data.Entities
{
    public class MainComment : BaseComment
    {
        public List<SubComment> SubComments { get; set; } = new List<SubComment>();
    }
}