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
    public class NotificacaoController : Controller
    {
        private readonly NotificacaoService _notificacaoService;

        public NotificacaoController(NotificacaoService notificacaoService)
        {
            _notificacaoService = notificacaoService;
        }

        /// <summary>
        /// Registra uma nova notificação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/v1/Notificacao
        ///     {
        ///        "mensagem": "Sua consulta foi agendada",
        ///        "tipoNotificacao": "AVISO",
        ///        "dataEnvio": "2023-10-27T10:00:00",
        ///        "idUser": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="dto">Dados da notificação.</param>
        /// <returns>A notificação criada com links HATEOAS.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(NotificacaoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] NotificacaoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var notificacaoResponse = await _notificacaoService.SaveAsync(dto);
                GerarLinks(notificacaoResponse);

                return CreatedAtAction(nameof(GetById), new { id = notificacaoResponse.IdNotificacao }, notificacaoResponse);
            }
            catch (Exception ex)
            {
                var erroReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { message = "Erro ao salvar notificação", detalhe = erroReal });
            }
        }

        /// <summary>
        /// Atualiza uma notificação existente.
        /// </summary>
        /// <param name="id">ID da Notificação.</param>
        /// <param name="dto">Dados atualizados.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(NotificacaoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] NotificacaoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var notificacaoUpdate = await _notificacaoService.UpdateAsync(id, dto);
                GerarLinks(notificacaoUpdate);
                return Ok(notificacaoUpdate);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Notificação não encontrada para atualização." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca uma notificação pelo ID.
        /// </summary>
        /// <param name="id">ID da Notificação.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NotificacaoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var notificacaoResponse = await _notificacaoService.GetByIdAsync(id);

                if (notificacaoResponse == null) return NoContent();

                GerarLinks(notificacaoResponse);
                return Ok(notificacaoResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao buscar registro", detalhe = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma notificação.
        /// </summary>
        /// <param name="id">ID da Notificação.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _notificacaoService.DeleteByAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Notificação não encontrada para exclusão." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao deletar registro", detalhe = ex.Message });
            }
        }

        /// <summary>
        /// Gera os links HATEOAS para a resposta.
        /// </summary>
        private void GerarLinks(NotificacaoResponse dto)
        {
            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(GetById), new { id = dto.IdNotificacao }),
                rel: "self",
                method: "GET"));

            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(Update), new { id = dto.IdNotificacao }),
                rel: "update",
                method: "PUT"));

            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(Delete), new { id = dto.IdNotificacao }),
                rel: "delete",
                method: "DELETE"));
        }
    }
}