using BlogEngine.Core.Data.Entities.JoiningEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.Models
{
    class GenreModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "name must be at least 3 and at max 20 characters long")]
        public string Name { get; set; }
        public byte[] Cover { get; set; }

        public List<BlogGenre> BlogGenres { get; set; } = new List<BlogGenre>();
    }
}