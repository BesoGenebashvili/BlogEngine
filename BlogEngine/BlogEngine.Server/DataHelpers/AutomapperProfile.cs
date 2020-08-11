using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Shared.DTOs;

namespace BlogEngine.Server.DataHelpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            #region Blog

            CreateMap<Blog, BlogDTO>();

            CreateMap<BlogCreationDTO, Blog>();

            CreateMap<BlogUpdateDTO, Blog>().ReverseMap();

            #endregion

            #region Category

            CreateMap<Category, CategoryDTO>();

            CreateMap<CategoryCreationDTO, Category>();

            CreateMap<CategoryUpdateDTO, Category>().ReverseMap();

            #endregion
        }
    }
}