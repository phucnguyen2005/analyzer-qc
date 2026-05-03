using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnalyzerQC.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AnalyzerQC.WebApi.Controllers;

public class AuthenticationController: ControllerBase
{
    private readonly IAppDbContext _dbContext;
    public AuthenticationController(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private const string TokenSecret = "abcdefgh1234567891011_DayLaChuoiDu32KyTu";
    private static readonly TimeSpan TokenLifeTime = TimeSpan.FromMinutes(15);
    
    private IActionResult GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(TokenSecret);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
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
    [HttpPost("login")]
    public async Task<IActionResult> Login(string username)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null)
        {
            return Unauthorized();
        }

        var token = GenerateToken(user);
        return Ok(token);
    }
}