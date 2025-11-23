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
    public class RecomendacaoIAController : Controller
    {
        private readonly RecomendacaoIAService _service;

        public RecomendacaoIAController(RecomendacaoIAService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gera ou registra uma nova recomendação de IA.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RecomendacaoIAResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] RecomendacaoIADto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _service.SaveAsync(dto);
                GerarLinks(response);

                return CreatedAtAction(nameof(GetById), new { id = response.IdRecomendacao }, response);
            }
            catch (Exception ex)
            {
                var erroReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { message = "Erro ao salvar recomendação", detalhe = erroReal });
            }
        }

        /// <summary>
        /// Atualiza uma recomendação existente.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RecomendacaoIAResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] RecomendacaoIADto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var response = await _service.UpdateAsync(id, dto);
                GerarLinks(response);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Recomendação não encontrada para atualização." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca uma recomendação pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RecomendacaoIAResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);

                if (response == null) return NoContent();

                GerarLinks(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao buscar registro", detalhe = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma recomendação.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Recomendação não encontrada para exclusão." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao deletar registro", detalhe = ex.Message });
            }
        }

        /// <summary>
        /// Gera os links HATEOAS.
        /// </summary>
        private void GerarLinks(RecomendacaoIAResponse dto)
        {
            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(GetById), new { id = dto.IdRecomendacao }),
                rel: "self",
                method: "GET"));

            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(Update), new { id = dto.IdRecomendacao }),
                rel: "update",
                method: "PUT"));

            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(Delete), new { id = dto.IdRecomendacao }),
                rel: "delete",
                method: "DELETE"));
        }
    }
}