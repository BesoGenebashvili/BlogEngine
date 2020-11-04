using BlogEngine.Core.Data.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Core.Data.Entities
{
    public class BlogRating : BaseEntity
    {
        public int Rate { get; set; }
        public int BlogID { get; set; }
        public Blog Blog { get; set; }

        [ForeignKey("ApplicationUser")]
        public int ApplicationUserID { get; set; }
    }
}