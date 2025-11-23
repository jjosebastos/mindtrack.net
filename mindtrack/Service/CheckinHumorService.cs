using Microsoft.AspNetCore.Mvc;
using mindtrack.DTO.Request;
using mindtrack.DTO.Response;
using mindtrack.Model;
using mindtrack.Repository;
using System.Threading.Tasks;

namespace mindtrack.Service
{
    public class CheckinHumorService  // Não esqueça da Interface!
    {
        // 1. LIMPEZA: Removi o _userRepository que não estava sendo usado
        private readonly ICheckinHumorRepository _checkinHumorRepository;

        public CheckinHumorService(ICheckinHumorRepository checkinHumorRepository)
        {
            _checkinHumorRepository = checkinHumorRepository;
        }

        public async Task<CheckinHumorResponse> SaveAsync(CheckinDto dto)
        {
            var checkinMapped = CheckinHumorMapper(dto);
            var checkinSaved = await _checkinHumorRepository.AddAsync(checkinMapped);
            return ToCheckinHumorResponse(checkinSaved);
        }

        public async Task<CheckinHumorResponse> UpdateAsync(int idCheckin, CheckinDto dto)
        {
            var checkinExisting = await GetCheckinById(idCheckin);
            if (checkinExisting == null) throw new KeyNotFoundException("Checkin não encontrado");

            // O Mapper altera o objeto checkinExisting na memória
            CheckinHumorUpdate(checkinExisting, dto);

            // Persiste a alteração no banco
            var checkinUpdated = await _checkinHumorRepository.UpdateAsync(checkinExisting);

            return ToCheckinHumorResponse(checkinUpdated);
        }

        public async Task DeleteByIdAsync(int idCheckin)
        {
            var checkinExisting = await GetCheckinById(idCheckin);
            if (checkinExisting == null) throw new KeyNotFoundException("Checkin não encontrado");

            await _checkinHumorRepository.DeleteAsync(checkinExisting);
        }

        public async Task<CheckinHumorResponse> GetCheckin(int idCheckin)
        {
            var checkin = await GetCheckinById(idCheckin);
            if (checkin == null) return null;
            return ToCheckinHumorResponse(checkin);
        }

      

        private CheckinHumor CheckinHumorMapper(CheckinDto dto)
        {
            return new CheckinHumor
            {
             
                IdUser = dto.IdUser,
                StatusHumor = dto.StatusHumor,
                Comentario = dto.Comentario,
                DataRegistro = DateTime.Now 
            };
        }

        private CheckinHumorResponse ToCheckinHumorResponse(CheckinHumor model)
        {
            return new CheckinHumorResponse
            {
                IdCheckin = model.IdCheckin,
                DataRegistro = model.DataRegistro,
                Comentario = model.Comentario,
                StatusHumor = model.StatusHumor,
                IdUser = model.IdUser,
            };
        }

        private void CheckinHumorUpdate(CheckinHumor model, CheckinDto dto)
        {
            // Apenas atualizamos o conteúdo
            model.StatusHumor = dto.StatusHumor;
            model.Comentario = dto.Comentario;

            // 3. CORREÇÃO DE LÓGICA:
            // Removemos a atualização da DataRegistro. 
            // Não queremos perder a data original de quando o check-in foi feito.
            // E também ignoramos o dto.IdUser para não trocar o dono do check-in.
        }

        private async Task<CheckinHumor> GetCheckinById(int idCheckin)
        {
            return await _checkinHumorRepository.GetByIdAsync(idCheckin);
        }
    }
}