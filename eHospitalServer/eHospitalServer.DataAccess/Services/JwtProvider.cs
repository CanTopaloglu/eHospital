using eHospitalServer.DataAccess.Options;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eHospitalServer.DataAccess.Services;
public class JwtProvider(
    UserManager<User> userManager,
    IOptions<JwtOptions> jwtOptions)
{
    public async Task<LoginResponseDto> CreateToken(User user, bool rememberMe)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("UserName", user.UserName ?? ""),
            new Claim("UserType", user.UserType.ToString())
        };

        DateTime expires = DateTime.UtcNow.AddHours(3);

        if (rememberMe)
        {
           expires = expires.AddDays(1); //Login atarken remember me seçeneği için.
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

        JwtSecurityToken jwtSecurityToken = new(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512));

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(jwtSecurityToken);

        string refreshToken = Guid.NewGuid().ToString();
        DateTime refreshTokenExpires = expires.AddHours(1); //tokena eklenecek süre ile refresh token

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = refreshTokenExpires;

        await userManager.UpdateAsync(user);

        return new(token, refreshToken, refreshTokenExpires);
    }
}
