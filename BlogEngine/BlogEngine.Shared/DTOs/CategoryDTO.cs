using BlogEngine.Core.Data.Entities.JoiningEntities;
using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs
{
    public class CategoryDTO : ReadDataDTOBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] GeneralCover { get; set; }
    }
}