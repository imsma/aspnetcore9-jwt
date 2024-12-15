using System;
using aspnetcore9_jwt.Models;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore9_jwt.Db;

public class CoreDbContext : DbContext
{
    public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
}
