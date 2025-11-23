using mindtrack.DTO.Response;
using mindtrack.Repository;
using System.Text.Json;

namespace mindtrack.Service
{
    public class DashboardService
    {
        private readonly IDashboardRepository _repository;

        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardUserDto> GetDashboardAsync(int idUser)
        {

            var jsonString = await _repository.GetUserJsonAsync(idUser);

            if (string.IsNullOrEmpty(jsonString))
            {
                throw new KeyNotFoundException("Nenhum dado retornado pelo banco.");
            }

     
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var dashboardDto = JsonSerializer.Deserialize<DashboardUserDto>(jsonString, options);

            // 3. Trata erros de negócio vindos do PL/SQL (Ex: "email corporativo inválido")
            if (dashboardDto != null && !string.IsNullOrEmpty(dashboardDto.Erro))
            {
                // O PL/SQL retorna erros no JSON. Vamos lançar exceção para a controller tratar.
                throw new InvalidOperationException(dashboardDto.Erro);
            }

            return dashboardDto;
        }
    }
}
