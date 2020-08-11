using BlogEngine.Shared.DTOs;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IBlogService : IDataServiceBase<BlogDTO, BlogCreationDTO, BlogUpdateDTO>
    {
    }
}