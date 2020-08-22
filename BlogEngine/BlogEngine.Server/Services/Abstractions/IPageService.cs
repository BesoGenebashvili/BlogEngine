using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IPageService
    {
        Task<IndexPageDTO> GetIndexPageDTOAsync();
    }
}