using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Shared.Models;

namespace BlogEngine.Server.DataHelpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<BlogModel, Blog>();
            CreateMap<Blog, BlogModel>();

            CreateMap<CommentModel, Comment>();
            CreateMap<Comment, CommentModel>();

            CreateMap<GenreModel, Genre>();
            CreateMap<Genre, GenreModel>();
        }
    }
}