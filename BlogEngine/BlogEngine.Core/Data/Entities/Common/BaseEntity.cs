using System;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Core.Data.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "Created By should not be more than 50 Characters")]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastUpdateDate { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "Last Update By should not be more than 50 Characters")]
        public string LastUpdateBy { get; set; }
    }
}