using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface IDataServiceBase<TDTO, TCreationDTO, TUpdateDTO>
    {
        Task<TDTO> GetByIdAsync(int id);
        Task<List<TDTO>> GetAllAsync();
        Task<TDTO> InsertAsync(TCreationDTO creationDTO);
        Task<TDTO> UpdateAsync(int id, TUpdateDTO updateDTO);
        Task<bool> DeleteAsync(int id);
        Task<TUpdateDTO> GetUpdateDTOAsync(int id);
    }
}