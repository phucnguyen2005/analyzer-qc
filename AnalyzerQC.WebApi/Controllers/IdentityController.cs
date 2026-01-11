using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnalyzerQC.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController:ControllerBase
{
    // Ít nhất 32 ký tự
    private const string TokenSecret = "abcdefgh1234567891011_DayLaChuoiDu32KyTu";
    private static readonly TimeSpan TokenLifeTime =  TimeSpan.FromMinutes(15);

    [HttpPost("token")]
    public IActionResult GenerateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(TokenSecret);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, "dev"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifeTime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        var jwtToken = tokenHandler.WriteToken(token);
        return Ok(jwtToken);
    }
}