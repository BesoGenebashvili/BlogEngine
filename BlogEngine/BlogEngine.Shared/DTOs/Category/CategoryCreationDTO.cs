﻿using BlogEngine.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs
{
    public class CategoryCreationDTO
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} should not be more than 100 Characters")]
        [FirstLetterUppercase(ErrorMessage = "First letter should be uppercase")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public byte[] GeneralCover { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "Created By should not be more than 50 Characters")]
        public string CreatedBy { get; set; }
    }
}