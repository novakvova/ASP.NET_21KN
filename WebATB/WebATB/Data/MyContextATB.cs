using Microsoft.EntityFrameworkCore;
using WebATB.Data.Entities;

namespace WebATB.Data;

public class MyContextATB : DbContext
{
    public MyContextATB(DbContextOptions<MyContextATB> contextOptions)
        : base(contextOptions)
    {
        
    }
    public DbSet<CategoryEntity> Categories { get; set; }
}
