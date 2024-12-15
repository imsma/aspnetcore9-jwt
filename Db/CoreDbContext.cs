using System;
using aspnetcore9_jwt.Models;
using aspnetcore9_jwt.Utilities;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore9_jwt.Db;

public class CoreDbContext : DbContext
{
    public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) { }
    public DbSet<User>? Users { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     _ = modelBuilder.Entity<User>().HasData(
    //         new User { Id = 1, UserName = "admin", Password = PasswordHashHelper.HashPassword("admin"), Role = "Admin" },
    //         new User { Id = 2, UserName = "user", Password = PasswordHashHelper.HashPassword("user"), Role = "User" }
    //     );
    // }
}
