using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using mindtrack.DTO.Response;
using mindtrack.Service;

namespace mindtrack.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Obtém o dashboard consolidado do usuário (Integração via PL/SQL).
        /// </summary>
        /// <remarks>
        /// Retorna competências, cursos recomendados, últimos check-ins e índice de humor
        /// processados diretamente pelo banco de dados Oracle.
        /// </remarks>
        [HttpGet("{idUser}")]
        [ProducesResponseType(typeof(DashboardUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetDashboard(int idUser)
        {
            try
            {
                var dashboard = await _dashboardService.GetDashboardAsync(idUser);
                return Ok(dashboard);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Captura os erros de validação do PL/SQL (ex: email inválido)
                return BadRequest(new { message = "Erro de validação no Dashboard", detalhe = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao gerar dashboard", detalhe = ex.Message });
            }
        }
    }
}