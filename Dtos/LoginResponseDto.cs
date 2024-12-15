using System;

namespace aspnetcore9_jwt.Dtos;

public class LoginResponseDto
{
    public string UserName { get; set; }
    public string AccessToken { get; set; }
    public int Expiration { get; set; }
}
