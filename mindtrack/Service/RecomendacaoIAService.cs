using mindtrack.DTO.Request;
using mindtrack.DTO.Response;
using mindtrack.Model;
using mindtrack.Repository;

namespace mindtrack.Service
{
    public class RecomendacaoIAService
    {
        private readonly IRecomendacaoIARepository _repository;

        public RecomendacaoIAService(IRecomendacaoIARepository repository)
        {
            _repository = repository;
        }

        public async Task<RecomendacaoIAResponse> SaveAsync(RecomendacaoIADto dto)
        {
            var entity = MapToEntity(dto);
            var savedEntity = await _repository.AddAsync(entity);
            return MapToResponse(savedEntity);
        }

        public async Task<RecomendacaoIAResponse> UpdateAsync(int id, RecomendacaoIADto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null) throw new KeyNotFoundException("Recomendação não encontrada");

            var updatedEntity = await _repository.UpdateAsync(MapToUpdate(existingEntity, dto));
            return MapToResponse(updatedEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null) throw new KeyNotFoundException("Recomendação não encontrada");

            await _repository.DeleteAsync(existingEntity);
        }

        public async Task<RecomendacaoIAResponse?> GetByIdAsync(int id)
        {
            var found = await _repository.GetByIdAsync(id);
            if (found == null) return null;
            return MapToResponse(found);
        }



        private RecomendacaoIA MapToEntity(RecomendacaoIADto dto)
        {
            return new RecomendacaoIA
            {
                Texto = dto.Texto,
                DataCriacao = dto.DataCriacao,
                IdUser = dto.IdUser,
                IdCheckin = dto.IdCheckin
            };
        }

        private RecomendacaoIAResponse MapToResponse(RecomendacaoIA model)
        {
            return new RecomendacaoIAResponse
            {
                IdRecomendacao = model.IdRecomendacao,
                Texto = model.Texto,
                DataCriacao = model.DataCriacao,
                IdUser = model.IdUser,
                IdCheckin = model.IdCheckin
            };
        }

        private RecomendacaoIA MapToUpdate(RecomendacaoIA model, RecomendacaoIADto dto)
        {
            model.Texto = dto.Texto;
            model.DataCriacao = dto.DataCriacao;
            model.IdUser = dto.IdUser;
            model.IdCheckin = dto.IdCheckin;
            return model;
        }
    }
}
