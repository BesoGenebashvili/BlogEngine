using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs.Blog
{
    public enum BlogOrderBy
    {
        [Display(Name = "Create Date")]
        DateCreated = 1,

        [Display(Name = "Update Date")]
        LastUpdateDate = 2,

        [Display(Name = "Rate")]
        Rate = 3
    }
}