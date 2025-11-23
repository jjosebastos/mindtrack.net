using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mindtrack.DTO;
using mindtrack.DTO.Response; 
using mindtrack.DTO.Shared;  
using mindtrack.Service;

namespace mindtrack.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : Controller
    {
        // Correção: Sempre injete a Interface, não a classe concreta
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Busca uma lista paginada de Usuários.
        /// </summary>
        /// <param name="pageNumber">O número da página a ser retornada (padrão: 1).</param>
        /// <param name="pageSize">A quantidade de itens por página (padrão: 10).</param>
        /// <returns>Uma lista de users com links HATEOAS.</returns>
        /// <response code="200">Retorna a lista de users com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);

            // AJUSTE HATEOAS: Precisamos gerar links para CADA item da lista
            foreach (var user in users)
            {
                GerarLinks(user);
            }

            return Ok(users);
        }

        /// <summary>
        /// Busca um usuário específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser buscado.</param>
        /// <returns>Os dados do usuário encontrado.</returns>
        /// <response code="200">Retorna os dados do usuário com sucesso.</response>
        /// <response code="404">Se o usuário com o ID especificado não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            var userResponse = await _userService.GetUserByIdAsync(id);

            if (userResponse == null) return NotFound();

            GerarLinks(userResponse);

            return Ok(userResponse);
        }

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/v1/User
        ///     {
        ///        "nome": "Ana Silva",
        ///        "email": "ana.silva@mindtrack.com",
        ///        "senha": "SenhaForte123!",
        ///        "setor": "TI",
        ///        "cargo": "Desenvolvedora",
        ///        "dataAdmissao": "2024-01-01"
        ///     }
        ///
        /// </remarks>
        /// <param name="dto">Dados para a criação de um novo usuário.</param>
        /// <returns>O objeto do usuário recém-criado, com seu ID e links HATEOAS.</returns>
        /// <response code="201">Retorna usuário recém-criado com sucesso.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [AllowAnonymous] // Cuidado: Em produção, criar usuário geralmente exige Auth
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] UserDto dto)
        {
            try
            {
                var userResponse = await _userService.SaveUserAsync(dto);

                GerarLinks(userResponse);

                return CreatedAtAction(nameof(GetById), new { id = userResponse.IdUser }, userResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="id">O ID do usuário a ser atualizado.</param>
        /// <param name="dto">Os novos dados para o usuário.</param>
        /// <response code="200">Se o usuário foi atualizado com sucesso.</response>
        /// <response code="400">Se os dados estiverem incorretos.</response>
        /// <response code="404">Se usuário com o ID especificado não for encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto dto)
        {
            try
            {
                var userResponse = await _userService.UpdateUserAsync(id, dto);

                GerarLinks(userResponse);

                return Ok(userResponse);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Exclui um usuário (soft delete ou físico).
        /// </summary>
        /// <param name="id">O ID do usuário a ser excluído.</param>
        /// <response code="204">Se usuário foi excluído com sucesso.</response>
        /// <response code="404">Se usuário com o ID especificado não for encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        private void GerarLinks(UserResponse dto)
        {
            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(GetById), new { id = dto.IdUser }),
                rel: "self",
                method: "GET"));

            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(Update), new { id = dto.IdUser }),
                rel: "update",
                method: "PUT"));

            dto.Links.Add(new LinkDto(
                href: Url.Action(nameof(Delete), new { id = dto.IdUser }),
                rel: "delete",
                method: "DELETE"));
        }
    }
}