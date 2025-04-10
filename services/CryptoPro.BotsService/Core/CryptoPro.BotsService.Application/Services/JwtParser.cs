using System.IdentityModel.Tokens.Jwt;

namespace CryptoPro.BotsService.Application.Services;

public class JwtParser : IJwtParser
{
    public TimeSpan? GetRemainingLifetime(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            
            if (jwtToken.ValidTo == DateTime.MinValue)
                return null;
                
            var remaining = jwtToken.ValidTo - DateTime.UtcNow;
            return remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
        }
        catch
        {
            return null;
        }
    }
}