using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using aspnetcore9_jwt.Db;
using aspnetcore9_jwt.Dtos;
using aspnetcore9_jwt.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace aspnetcore9_jwt.Service;

public interface IAuthService
{
    Task<LoginResponseDto?> Authenticate(LoginRequestDto loginRequestDto);
}

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly CoreDbContext _context;

    public AuthService(IConfiguration configuration, CoreDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<LoginResponseDto?> Authenticate(LoginRequestDto loginRequestDto)
    {
        if (string.IsNullOrWhiteSpace(loginRequestDto.UerName) || string.IsNullOrWhiteSpace(loginRequestDto.Password))
        {
            return null;
        }

        if (_context.Users == null)
        {
            return null;
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginRequestDto.UerName);
        if (user is null || !PasswordHashHelper.VerifyPassword(loginRequestDto.Password, user.Password))
        {
            return null;
        }

        var issueer = _configuration["JwtConfig:Issuer"];
        var audience = _configuration["JwtConfig:Audience"];
        var key = _configuration["JwtConfig:Key"];
        var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, loginRequestDto.UerName)
            }),
            Expires = tokenExpiryTimeStamp,
            Issuer = issueer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);

        return new LoginResponseDto
        {
            UserName = loginRequestDto.UerName,
            AccessToken = accessToken,
            Expiration = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
        };
    }

}
