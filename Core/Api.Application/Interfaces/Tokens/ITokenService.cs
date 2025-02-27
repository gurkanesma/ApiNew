using Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Application.Interfaces.Tokens
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(User user, IList<string> roles); //Kullanıcı bilgilerini ve rollerini kullanarak JWT Token oluşturur.
        string GenerateRefreshToken(); //Kullanıcıya uzun süreli oturumlar sağlamak için Refresh Token üretir.
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token); //Süresi dolmuş (expired) bir JWT'den kullanıcı bilgilerini (claims) çıkarmak.
    }
}
