using API.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utils
{
    public class JwtUtils
    {
        private IConfiguration _configuration;

        public JwtUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString())
            });
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["security:secret-key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = claims,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _configuration["security:issuer"]
                // TODO: audience?
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<Claim> GetClaims(string jwt) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwt);
            return token.Claims;
        }

        public IEnumerable<Claim>? ExtractClaimsFromAuthorizationHeader(IHttpContextAccessor httpContextAccessor) {
            var authorizationHeader = httpContextAccessor.GetAuthorizationHeader();

            if (string.IsNullOrWhiteSpace(authorizationHeader)) {
                return null;
            }

            var jwt = authorizationHeader?.Split("Bearer ", StringSplitOptions.RemoveEmptyEntries)[0];
            return GetClaims(jwt!);
        }

        public string? GetSidClaim(IHttpContextAccessor httpContextAccessor) {
            var claims = ExtractClaimsFromAuthorizationHeader(httpContextAccessor);
            return claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)?.Value;
        }

        public string? GetSidClaim(string jwt) {
            var claims = GetClaims(jwt);
            return claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)?.Value;
        }
    }
}
