using mindtrack.DTO.Request;
using mindtrack.DTO.Response;
using mindtrack.Model;
using mindtrack.Repository;

namespace mindtrack.Service
{
    public class NotificacaoService
    {
        private readonly INotificacaoRepository _notificacaoRespository;

        public NotificacaoService(INotificacaoRepository notificacaoRespository)
        {
            _notificacaoRespository = notificacaoRespository;
        }

        public async Task<NotificacaoResponse> SaveAsync(NotificacaoDto dto)
        {
            var notificacaoEntity = MapToEntity(dto);
            var notificacaoSaved = await _notificacaoRespository.AddAsync(notificacaoEntity);

            return MapToResponse(notificacaoSaved);
        }

        public async Task<NotificacaoResponse> UpdateAsync(int id, NotificacaoDto dto)
        {
            var existingNotificacao = await _notificacaoRespository.GetByIdAsync(id);
            if (existingNotificacao == null) throw new KeyNotFoundException("Notificacao nao encontrada");

            var updated = await _notificacaoRespository.UpdateAsync(MapToUpdate(existingNotificacao, dto));
            return MapToResponse(updated);
        }

        public async Task DeleteByAsync(int id)
        {
            var existingNotificacao = await _notificacaoRespository.GetByIdAsync(id);
            if (existingNotificacao == null) throw new KeyNotFoundException("Notificacao nao encontrada");
            await _notificacaoRespository.DeleteAsync(existingNotificacao);
        }


        public async Task<NotificacaoResponse> GetByIdAsync(int id)
        {
            var found = await _notificacaoRespository.GetByIdAsync(id);
            if (found == null) return null;
            return MapToResponse(found);  
        }

        private Notificacao MapToEntity(NotificacaoDto dto)
        {
            return new Notificacao
            {
                TipoNotificacao = dto.TipoNotificacao,
                Mensagem = dto.Mensagem,
                DataEnvio = dto.DataEnvio,
                IdUser = dto.IdUser
            };
        }

        private NotificacaoResponse MapToResponse(Notificacao model)
        {
            return new NotificacaoResponse
            {
                IdNotificacao = model.IdNotificacao,
                Mensagem = model.Mensagem,
                DataEnvio = model.DataEnvio,
                TipoNotificacao = model.TipoNotificacao,
              
            };
        }

        private Notificacao MapToUpdate (Notificacao model, NotificacaoDto dto)
        {
            model.IdUser = dto.IdUser;
            model.Mensagem = dto.Mensagem;
            model.TipoNotificacao = dto.TipoNotificacao;
            model.DataEnvio = dto.DataEnvio;

            return model;

        }
    }
}
