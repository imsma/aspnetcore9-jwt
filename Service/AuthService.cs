using System;
using aspnetcore9_jwt.Db;
using aspnetcore9_jwt.Dtos;

namespace aspnetcore9_jwt.Service;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly CoreDbContext _context;

    public AuthService(IConfiguration configuration, CoreDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<LoginResponseDto> Authenticate(LoginRequestDto loginRequestDto)
    {
        throw new NotImplementedException();
    }


}
