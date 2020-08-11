using BlogEngine.Core.Data.Entities.JoiningEntities;
using System;
using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public byte[] GeneralCover { get; set; }

        public List<BlogCategory> BlogCategories { get; set; } = new List<BlogCategory>();
    }
}