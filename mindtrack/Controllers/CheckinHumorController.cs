using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using mindtrack.DTO.Request;
using mindtrack.DTO.Response;
using mindtrack.DTO.Shared;
using mindtrack.Service;

namespace mindtrack.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CheckinHumorController : Controller
    {
        // Use a Interface ICheckinHumorService
        private readonly CheckinHumorService _checkinHumorService;

        public CheckinHumorController(CheckinHumorService checkinHumorService)
        {
            _checkinHumorService = checkinHumorService;
        }

        /// <summary>
        /// Registra um novo check-in de humor.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/v1/CheckinHumor
        ///     {
        ///        "idUser": 1,
        ///        "statusHumor": "Feliz",
        ///        "comentario": "Dia produtivo!"
        ///     }
        ///
        /// </remarks>
        /// <param name="dto">Dados do check-in.</param>
        /// <returns>O check-in criado com links HATEOAS.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CheckinHumorResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] CheckinDto dto)
        {
            try
            {
                var checkinResponse = await _checkinHumorService.SaveAsync(dto);
                GerarLinks(checkinResponse);

                // CORREÇÃO: Sintaxe 'new { id = ... }' e apontando para o ID do Checkin
                return CreatedAtAction(nameof(GetById), new { id = checkinResponse.IdCheckin }, checkinResponse);
            }
            catch (Exception ex)
            {
                var erroReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { message = "Erro no Banco", detalhe = erroReal });
    
            }
         
        }

        /// <summary>
        /// Atualiza um check-in existente.
        /// </summary>
        /// <param name="id">ID do Check-in.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CheckinHumorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CheckinDto dto)
        {
            try
            {
                var checkinUpdate = await _checkinHumorService.UpdateAsync(id, dto);
                GerarLinks(checkinUpdate);
                return Ok(checkinUpdate);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca um check-in pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        // CORREÇÃO: Tipo de retorno ajustado para CheckinHumorResponse
        [ProducesResponseType(typeof(CheckinHumorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetById(int id)
        {
            var checkinResponse = await _checkinHumorService.GetCheckin(id);

            if (checkinResponse == null) return NoContent();

            GerarLinks(checkinResponse);
            return Ok(checkinResponse);
        }

        /// <summary>
        /// Remove um check-in.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _checkinHumorService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                // Erro genérico no delete deve ser 500 ou BadRequest, não 404
                return BadRequest();
            }
        }

        private void GerarLinks(CheckinHumorResponse dto)
        {
      
            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(GetById), new { id = dto.IdCheckin }),
                rel: "self",
                method: "GET"));

            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(Update), new { id = dto.IdCheckin }),
                rel: "update",
                method: "PUT"));

            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(Delete), new { id = dto.IdCheckin }),
                rel: "delete",
                method: "DELETE"));
        }
    }
}