using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Shared.DTOs;

namespace BlogEngine.Server.DataHelpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Blog, BlogDTO>();

            CreateMap<BlogCreationDTO, Blog>();

            CreateMap<BlogUpdateDTO, Blog>()
                .ForMember(b => b.ID, options => options.Ignore())
                .ReverseMap();
        }
    }
}