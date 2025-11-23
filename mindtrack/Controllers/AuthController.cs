using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mindtrack.DTO.Request;
using mindtrack.Repository;
using mindtrack.Service;
using System.ComponentModel.DataAnnotations;

namespace mindtrack.Controllers
{

    /// <summary>
    /// Controladora responsável pela autenticação de usuários e geração de tokens JWT.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : Controller
    {

        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AuthController(ITokenService tokenService, IUserRepository userRepository)
        {
            this._tokenService = tokenService;
            this._userRepository = userRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var user = await _userRepository.GetByUsernameAsync(model.Username);

            if (user == null) return Unauthorized();

            if (!VerificarSenha(model.Password, user.Senha)) return Unauthorized("Usuário ou senha inválidos");

            var token = _tokenService.GenerateToken(user);

            return Ok(new LoginResponseDto { Token = token });

        }



        /// <summary>
        /// Verifica se a senha fornecida (em texto puro) corresponde ao hash salvo no banco.
        /// </summary>
        /// <param name="password">A senha em texto puro enviada pelo usuário.</param>
        /// <param name="passwordHash">O hash da senha (BCrypt) armazenado no banco de dados.</param>
        /// <returns><c>true</c> se a senha for válida; caso contrário, <c>false</c>.</returns>
        private bool VerificarSenha(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public class LoginResponseDto
        {
            /// <summary>
            /// O token de autenticação JWT gerado.
            /// </summary>
            /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c</example>
            [Required]
            public string Token { get; set; }
        }
    }
}
