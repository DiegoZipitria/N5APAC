using Microsoft.IdentityModel.Tokens;
using PAC.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace PAC.BusinessLogic

{
    public class TokenManager
    {
        public string GenerateToken(StudentClaim user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var description = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("name", user.Name),
                    new Claim("isLogged", user.IsLogged.ToString())
                })
            };
            var token = tokenHandler.CreateToken(description);
            return tokenHandler.WriteToken(token);
        }
        public StudentClaim ParseToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("Token is null or empty.");

            var parsedToken = tokenHandler.ReadJwtToken(token);

            var userClaims = new StudentClaim
            {
                Name = parsedToken.Claims.FirstOrDefault(x => x.Type == "name")?.Value,
            };

            try
            {
                userClaims.IsLogged = bool.Parse(parsedToken.Claims.FirstOrDefault(x => x.Type == "isLogged")?.Value ?? "false");
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid token claim: isLogged value is not a valid boolean.");
            }

            return userClaims;
        }

        public StudentClaim ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("Token is null or empty.");

            var claims = ParseToken(token);

            if (
                string.IsNullOrEmpty(claims.Name) || !claims.IsLogged)
                throw new ArgumentException("Invalid token claims.");

            return claims;
        }
    }
}
