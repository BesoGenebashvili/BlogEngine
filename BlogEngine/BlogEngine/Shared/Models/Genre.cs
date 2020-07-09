using BlogEngine.Shared.Models.JoiningModels;
using System.Collections.Generic;

namespace BlogEngine.Shared.Models
{
    public class Genre
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public byte[] Cover { get; set; }


        // TODO: add rate column

        public List<BlogGenre> BlogGenres { get; set; } = new List<BlogGenre>();
    }
}