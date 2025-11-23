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
    public class PlanoBemEstarController : Controller
    {
        private readonly PlanoBemEstarService _planoBemEstarService;

        public PlanoBemEstarController(PlanoBemEstarService planoBemEstarService)
        {
            _planoBemEstarService = planoBemEstarService;
        }

        /// <summary>
        /// Registra um novo plano de bem-estar.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/v1/PlanoBemEstar
        ///     {
        ///        "titulo": "Plano de Caminhada",
        ///        "descricao": "Caminhar 30 min por dia",
        ///        "status": "Ativo",
        ///        "dataInicio": "2023-11-01",
        ///        "dataFim": "2023-11-30",
        ///        "idUser": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="dto">Dados do plano.</param>
        /// <returns>O plano criado com links HATEOAS.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PlanoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] PlanoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var planoResponse = await _planoBemEstarService.SaveAsync(dto);
                GerarLinks(planoResponse);

                return CreatedAtAction(nameof(GetById), new { id = planoResponse.IdPlano }, planoResponse);
            }
            catch (Exception ex)
            {
                var erroReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { message = "Erro ao salvar plano", detalhe = erroReal });
            }
        }

        /// <summary>
        /// Atualiza um plano existente.
        /// </summary>
        /// <param name="id">ID do Plano.</param>
        /// <param name="dto">Dados atualizados.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PlanoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] PlanoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var planoUpdate = await _planoBemEstarService.UpdateAsync(id, dto);
                GerarLinks(planoUpdate);
                return Ok(planoUpdate);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Plano não encontrado para atualização." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca um plano pelo ID.
        /// </summary>
        /// <param name="id">ID do Plano.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlanoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var planoResponse = await _planoBemEstarService.GetByIdAsync(id);

                if (planoResponse == null) return NoContent();

                GerarLinks(planoResponse);
                return Ok(planoResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao buscar registro", detalhe = ex.Message });
            }
        }

        /// <summary>
        /// Remove um plano.
        /// </summary>
        /// <param name="id">ID do Plano.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _planoBemEstarService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Plano não encontrado para exclusão." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao deletar registro", detalhe = ex.Message });
            }
        }

        /// <summary>
        /// Gera os links HATEOAS para a resposta.
        /// </summary>
        private void GerarLinks(PlanoResponse dto)
        {
            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(GetById), new { id = dto.IdPlano }),
                rel: "self",
                method: "GET"));

            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(Update), new { id = dto.IdPlano }),
                rel: "update",
                method: "PUT"));

            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(Delete), new { id = dto.IdPlano }),
                rel: "delete",
                method: "DELETE"));
        }
    }
}