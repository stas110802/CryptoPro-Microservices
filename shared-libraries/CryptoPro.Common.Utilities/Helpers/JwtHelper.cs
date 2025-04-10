using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CryptoPro.Common.Utilities.Helpers;

public class JwtHelper
{
    public static int? GetUserId(ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
        var isSuccessful = int.TryParse(userIdClaim, out var userId);

        return isSuccessful ? userId : null;
    }
}