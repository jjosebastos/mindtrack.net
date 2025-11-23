using mindtrack.DTO.Request;
using mindtrack.DTO.Response;
using mindtrack.Model;
using mindtrack.Repository;

namespace mindtrack.Service
{
    public class PlanoBemEstarService 
    {
        private readonly IPlanoBemEstarRepository _planoBemEstarRepository;

        public PlanoBemEstarService(IPlanoBemEstarRepository planoBemEstarRepository)
        {
            _planoBemEstarRepository = planoBemEstarRepository;
        }

        public async Task<PlanoResponse> SaveAsync(PlanoDto dto)
        {
            var planoEntity = MapToEntity(dto);
            var planoSaved = await _planoBemEstarRepository.AddAsync(planoEntity);
            return MapToResponse(planoSaved);

        }

        public async Task<PlanoResponse> UpdateAsync(int idPlano, PlanoDto dto)
        {
            var planoFound = await _planoBemEstarRepository.GetByIdAsync(idPlano);

            if (planoFound == null) throw new KeyNotFoundException("Plano nao encontrado");

            var planoMapped = MapToUpdate(planoFound, dto);
            var planoUpdated = await _planoBemEstarRepository.UpdateAsync(planoMapped);

            return MapToResponse(planoUpdated);
        }
        

        public async Task DeleteAsync(int idPlano)
        {
            var planoFound = await _planoBemEstarRepository.GetByIdAsync(idPlano);
            if (planoFound == null) throw new KeyNotFoundException("Plano nao encontrado");
            await _planoBemEstarRepository.DeleteAsync(planoFound);
        }

        public async Task<PlanoResponse> GetByIdAsync(int idPlano)
        {
            var planoFound = await _planoBemEstarRepository.GetByIdAsync(idPlano);
            if (planoFound == null) return null;
            return MapToResponse(planoFound);
        }


        private PlanoBemEstar MapToEntity(PlanoDto dto)
        {
            return new PlanoBemEstar
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                Status = dto.Status,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                IdUser = dto.IdUser
            };
        }

        private PlanoResponse MapToResponse(PlanoBemEstar model)
        {
            return new PlanoResponse
            {
                IdPlano = model.IdPlano,
                IdUser = model.IdUser,
                Descricao = model.Descricao,
                Status = model.Status,
                Titulo = model.Titulo,
                DataInicio = model.DataInicio,
                DataFim = model.DataFim
            };
        }

        private PlanoBemEstar MapToUpdate(PlanoBemEstar model, PlanoDto dto)
        {

            model.Titulo = dto.Titulo;
            model.Status = dto.Status;
            model.IdUser = dto.IdUser;
            model.DataInicio = dto.DataInicio;
            model.DataFim = dto.DataFim;
            model.Descricao = dto.Descricao;

            return model;
        }
    }
}
