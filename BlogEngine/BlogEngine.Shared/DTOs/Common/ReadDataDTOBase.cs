using System;

namespace BlogEngine.Shared.DTOs.Common
{
    public abstract class ReadDataDTOBase
    {
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
    }
}