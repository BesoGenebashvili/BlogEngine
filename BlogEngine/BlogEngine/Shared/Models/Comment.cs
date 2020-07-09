using System;

namespace BlogEngine.Shared.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public int Content { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool Edited { get; set; }
        
        // TODO: add rate column
    }
}