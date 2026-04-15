using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebATB.Data.Entities;
using WebATB.Data.Entities.Identity;

namespace WebATB.Data;

public class MyContextATB : IdentityDbContext<UserEntity, RoleEntity, int>
{
    public MyContextATB(DbContextOptions<MyContextATB> contextOptions)
        : base(contextOptions)
    {
        
    }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // identity 
        modelBuilder.Entity<UserRoleEntity>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRoleEntity>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

    }
}
