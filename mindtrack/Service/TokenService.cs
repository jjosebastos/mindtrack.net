using Microsoft.IdentityModel.Tokens;
using mindtrack.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mindtrack.Service
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, ILogger<TokenService> logger) 
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string GenerateToken(User user)
        {
            try
            {
                var key = _configuration["Jwt:Key"];
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var expiresHours = _configuration.GetValue<double>("Jwt:ExpiresInHours", 2);

                if (string.IsNullOrEmpty(key))
                    throw new InvalidOperationException("Chave JWT (Jwt:Key) não encontrada na configuração.");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.IdUser.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(expiresHours),
                    signingCredentials: credentials);

                _logger.LogInformation("Token JWT gerado com sucesso para o usuário ID {UserId}.", user.IdUser);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o token JWT para o usuário ID {UserId}.", user.IdUser);
                throw; 
            }
        }
    }
}
