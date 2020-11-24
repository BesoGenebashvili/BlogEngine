using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs.Category
{
    public enum CategoryOrderBy
    {
        [Display(Name = "Featured")]
        Featured = 1,

        [Display(Name = "Newest")]
        Newest = 2,
    }
}