using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IPageClient
    {
        Task<IndexPageDTO> GetIndexPageDTOAsync();
    }
}