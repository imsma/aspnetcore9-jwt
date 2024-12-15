using System;

namespace aspnetcore9_jwt.Models;

public class User
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }

}
