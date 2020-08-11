using AutoMapper;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Implementations
{
    public class DataServiceBase<TEntity, TDTO, TCreationDTO, TUpdateDTO>
        : IDataServiceBase<TDTO, TCreationDTO, TUpdateDTO>
        where TEntity : class
        where TDTO : class
        where TCreationDTO : class
        where TUpdateDTO : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public DataServiceBase(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) return null;

            return ToDTO(entity);
        }

        public virtual async Task<TUpdateDTO> GetUpdateDTOAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) return null;

            return ToUpdateDTO(entity);
        }

        public virtual async Task<List<TDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            return _mapper.Map<List<TDTO>>(entities.ToList());
        }

        public virtual async Task<TDTO> InsertAsync(TCreationDTO creationDTO)
        {
            var entity = ToEntity(creationDTO);

            var insertedEntity = await _repository.InsertAsync(entity);

            return ToDTO(insertedEntity);
        }

        public virtual async Task<TDTO> UpdateAsync(int id, TUpdateDTO updateDTO)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) return null;

            _mapper.Map(updateDTO, entity);

            var updatedEntity = await _repository.UpdateAsync(entity);

            return ToDTO(updatedEntity);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null) return false;

            return await _repository.DeleteAsync(id);
        }

        protected TEntity ToEntity(TCreationDTO creationDTO) => _mapper.Map<TEntity>(creationDTO);
        protected TDTO ToDTO(TEntity entity) => _mapper.Map<TDTO>(entity);
        protected TUpdateDTO ToUpdateDTO(TEntity entity) => _mapper.Map<TUpdateDTO>(entity);
    }
}