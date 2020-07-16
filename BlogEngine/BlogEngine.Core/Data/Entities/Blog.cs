using System;
using System.Collections.Generic;
using BlogEngine.Core.Data.Entities.JoiningEntities;

namespace BlogEngine.Core.Data.Entities
{
    public class Blog
    {
        public int ID { get; set; }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string HTMLContent { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public TimeSpan? TimeToRead { get; set; }
        public byte[] Cover { get; set; }

        // TODO: add rate column
        // TODO: add publish date column

        public List<BlogComment> BlogComments { get; set; } = new List<BlogComment>();
        public List<BlogGenre> BlogGenres { get; set; } = new List<BlogGenre>();
    }
}