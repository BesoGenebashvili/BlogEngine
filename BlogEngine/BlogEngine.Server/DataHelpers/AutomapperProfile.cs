using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Data.Entities.JoiningEntities;
using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace BlogEngine.Server.DataHelpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            #region Blog

            CreateMap<Blog, BlogDTO>()
                .ForMember(d => d.CategoryDTOs, opt => opt.MapFrom(MapCategoryDTOs))
                .ForMember(d => d.MainCommentDTOs, opt => opt.MapFrom(s => s.MainComments));

            CreateMap<BlogCreationDTO, Blog>()
                .ForMember(d => d.BlogCategories, opt => opt.MapFrom(MapCategoryIDs));

            CreateMap<BlogUpdateDTO, Blog>()
                .ForMember(d => d.BlogCategories, opt => opt.MapFrom(MapCategoryIDs))
                .ReverseMap()
                .ForMember(d => d.CategoryIDs, opt => opt.MapFrom(MapBlogCategories));

            #endregion

            #region Category

            CreateMap<Category, CategoryDTO>();

            CreateMap<CategoryCreationDTO, Category>();

            CreateMap<CategoryUpdateDTO, Category>().ReverseMap();

            #endregion

            #region Comment

            CreateMap<CommentCreationDTO, MainComment>().ReverseMap();

            CreateMap<MainComment, MainCommentDTO>()
                .ForMember(d => d.SubCommentDTOs, opt => opt.MapFrom(s => s.SubComments));

            CreateMap<CommentCreationDTO, SubComment>().ReverseMap();

            CreateMap<SubComment, SubCommentDTO>();

            #endregion
        }

        private List<BlogCategory> MapCategoryIDs(BlogCreationDTO blogCreationDTO, Blog blog)
        {
            return blogCreationDTO.CategoryIDs
                .Select(ci => new BlogCategory() { CategoryID = ci })
                .ToList();
        }

        private List<BlogCategory> MapCategoryIDs(BlogUpdateDTO blogUpdateDTO, Blog blog)
        {
            return blogUpdateDTO.CategoryIDs
                .Select(ci => new BlogCategory() { CategoryID = ci })
                .ToList();
        }

        private List<int> MapBlogCategories(Blog blog, BlogUpdateDTO blogUpdateDTO)
        {
            return blog.BlogCategories
                .Select(bc => bc.CategoryID)
                .ToList();
        }

        private List<CategoryDTO> MapCategoryDTOs(Blog blog, BlogDTO blogDTO)
        {
            return blog.BlogCategories
                .Select(bc =>
                {
                    if (bc.Category == null) return null;

                    return new CategoryDTO()
                    {
                        ID = bc.Category.ID,
                        Name = bc.Category.Name,
                        GeneralCover = bc.Category.GeneralCover,
                        DateCreated = bc.Category.DateCreated,
                        CreatedBy = bc.Category.CreatedBy,
                        LastUpdateDate = bc.Category.LastUpdateDate,
                        LastUpdateBy = bc.Category.LastUpdateBy
                    };
                }).ToList();
        }
    }
}