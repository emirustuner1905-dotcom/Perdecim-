using Microsoft.EntityFrameworkCore;

namespace PerdeCim.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    
    public DbSet<Perde> Perdeler { get; set; }
    
   
    public DbSet<Satici> Saticilar { get; set; } 
    
   public DbSet<User> Users { get; set; } 
}



